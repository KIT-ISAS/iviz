using System;
using System.Linq;
using System.Threading.Tasks;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.MoveitTest;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Msgs.TrajectoryMsgs;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Pose = UnityEngine.Pose;

namespace Iviz.App
{
    public class ExtensionTest : MonoBehaviour
    {
        RobotModel robot;
        RobotModel planRobot;

        bool initialized;
        RosChannelReader<JointState> jointsListener;

        float startTime; // start time, used only to calculate the circle

        bool hasNewTrajectory;
        [CanBeNull] JointTrajectory newTrajectory;

        /*
        void Awake()
        {
            // wait until the GUI finishes initializing
            ModuleListPanel.InitFinished += Initialize;
        }

        void Initialize()
        {
            // remove our stuff from the event
            ModuleListPanel.InitFinished -= Initialize;

            // we setup the connection data here
            Connection.ConnectionStateChanged += OnConnectionStateChanged;

            // setup the master data: change this!
            Uri masterUri = new Uri("http://141.3.59.5:11311");
            Uri myUri = RosClient.TryGetCallerUriFor(masterUri, 7635) ?? RosClient.TryGetCallerUri(7635);

            ModuleListPanel.Instance.SetConnectionData(masterUri.ToString(), myUri.ToString(), "/iviz_test");

            // make GUI invisible
            ModuleListPanel.Instance.AllGuiVisible = false;
            
            // make TF frames invisible
            TfListener.Instance.FramesVisible = false;
        }
        */

        RosClient connectionClient;
        
        // this gets called when we're connected to the master
        async void Start()
        {
            Uri masterUri = new Uri("http://141.3.59.5:11311");
            Uri myUri = RosClient.TryGetCallerUriFor(masterUri, 7635) ?? RosClient.TryGetCallerUri(7635);
            connectionClient = await RosClient.CreateAsync(masterUri, "/iviz_test", myUri); 
            
            Debug.Log($"{this}: Connected!");
            while (true)
            {
                // keep checking whether the moveit_test node is on
                const string trajectoryService = "/moveit_test/calculate_trajectory";
                var systemState = await connectionClient.GetSystemStateAsync();
                bool hasService = systemState.Services.Any(service => service.Topic == trajectoryService);
                if (hasService)
                {
                    break;
                }

                Debug.LogWarning($"{this}: Service not detected! Retrying...");
                await Task.Delay(2000);
            }

            Debug.Log($"{this}: Service found. Starting robot and listeners.");
            
            // generate two robots: 'robot' for the real robot, and 'planRobot' for the plan
            await GenerateRobotAsync();

            // start listening to the joints topic
            // thes joints apply only for 'robot'. 
            jointsListener = new RosChannelReader<JointState>();
            await jointsListener.StartAsync(connectionClient, "/joint_states");
            
            // tell Update() we're finished
            initialized = true;
        }

        async Task GenerateRobotAsync()
        {
            string pandaUrdf = UnityEngine.Resources.Load<TextAsset>("Package/iviz/robots/panda").text;

            // actual robot
            robot = new RobotModel(pandaUrdf);
            await robot.StartAsync();

            // plan robot
            planRobot = new RobotModel(pandaUrdf)
            {
                Tint = Color.yellow.WithAlpha(0.5f), 
                UnityName = "panda-plan"
            };
            await planRobot.StartAsync();

            // set the start time. this is only used by the circle motion
            startTime = Time.time;
            
            // set the first point of the trajectory
            Point rosPoint = (0.5f, -0.5f, 0.1f);
            SetTargetPose(Pose.identity.WithPosition(rosPoint.Ros2Unity()));
        }

        void SetTargetPose(Pose unityPose)
        {
            SetTargetPose(unityPose.Unity2RosPose());
        }

        // this is an async void function. it runs in the background.
        // we use hasNewTrajectory to tell Update() we have a trajectory 
        async void SetTargetPose(Msgs.GeometryMsgs.Pose rosPose)
        {
            SetManipulatorPose srv = new SetManipulatorPose
            {
                Request = {TargetPose = rosPose}
            };
            
            Debug.Log($"{this}: Setting target pose.");
            await connectionClient.CallServiceAsync("/moveit_test/calculate_trajectory", srv, true).AwaitNoThrow(this);
            
            if (srv.Response.Success)
            {
                newTrajectory = srv.Response.Trajectory;
                Debug.Log($"{this}: Plan for target pose succeeded.");
            }
            else
            {
                newTrajectory = null;
                Debug.Log($"{this}: Plan for target pose failed!");
            }
            
            // tell Update() that we have a response
            hasNewTrajectory = true;
        }

        void Update()
        {
            if (!initialized)
            {
                return;
            }

            // TryReadAll() is an enumerable that contains the new messages in the joints topic
            // we update 'robot' with this value
            foreach (var jointState in jointsListener.TryReadAll())
            {
                robot.WriteJoints(jointState);
            }

            // check if a new trajectory has arrived
            if (hasNewTrajectory)
            {
                // is the trajectory valid?
                if (newTrajectory != null)
                {
                    // a trajectory is a sequence of joint values with timestamps
                    // but for visualization we're only interested in the final value
                    JointTrajectoryPoint lastJoints = newTrajectory.Points.Last();
                    planRobot.WriteJoints(newTrajectory.JointNames, lastJoints.Positions);
                }

                hasNewTrajectory = false;
                newTrajectory = null;

                // set the next target in the circle motion
                float angle = (Time.time - startTime) * 0.5f;
                Point rosPoint = (0.5f * Mathf.Cos(angle), -0.25f, 0.6f + 0.5f * Mathf.Sin(angle));
                SetTargetPose(Pose.identity.WithPosition(rosPoint.Ros2Unity()));
            }
        }
        
        public override string ToString()
        {
            return "[ExtensionTest]";
        }
    }
}