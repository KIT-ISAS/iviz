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
        static RoslibConnection Connection => ConnectionManager.Connection;

        RobotModel robot;
        RobotModel planRobot;

        bool initialized;
        RosChannelReader<JointState> jointsListener;

        float startTime;

        bool hasNewTrajectory;
        [CanBeNull] JointTrajectory newTrajectory;

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

            Uri masterUri = new Uri("http://141.3.59.5:11311");
            Uri myUri = RosClient.TryGetCallerUriFor(masterUri, 7635) ?? RosClient.TryGetCallerUri(7635);

            // change this!
            ModuleListPanel.Instance.SetConnectionData(masterUri.ToString(), myUri.ToString(), "/iviz_test");

            // make GUI invisible
            ModuleListPanel.Instance.AllGuiVisible = false;
            TfListener.Instance.FramesVisible = false;
        }

        // this gets called when we're connected to the master
        async void OnConnectionStateChanged(ConnectionState state)
        {
            if (state != ConnectionState.Connected)
            {
                return;
            }

            Debug.Log($"{this}: Connected!");
            while (true)
            {
                var systemState = await Connection.GetSystemStateAsync(2000);
                bool hasService = systemState.Services.Any(service => service.Topic == "/moveit_test/calculate_trajectory");
                if (hasService)
                {
                    break;
                }

                Debug.LogWarning($"{this}: Service not detected! Retrying...");
                await Task.Delay(2000);
            }

            Debug.Log($"{this}: Service found. Starting listeners.");
            await GenerateRobotAsync();

            jointsListener = new RosChannelReader<JointState>();
            await jointsListener.StartAsync(Connection.Client, "/joint_states");
            initialized = true;
        }

        async Task GenerateRobotAsync()
        {
            // you can load your own urdf as a string
            string pandaUrdf = UnityEngine.Resources.Load<TextAsset>("Package/iviz/robots/panda").text;

            robot = new RobotModel(pandaUrdf);
            await robot.StartAsync();

            planRobot = new RobotModel(pandaUrdf);
            planRobot.Tint = Color.yellow.WithAlpha(0.5f);
            await planRobot.StartAsync();

            startTime = Time.time;
            Point rosPoint = (0.5f, -0.5f, 0.1f);
            SetTargetPose(Pose.identity.WithPosition(rosPoint.Ros2Unity()));
            
        }

        void Update()
        {
            if (!initialized)
            {
                return;
            }

            foreach (var jointState in jointsListener.TryReadAll())
            {
                robot.WriteJoints(jointState);
            }

            if (hasNewTrajectory)
            {
                if (newTrajectory != null)
                {
                    planRobot.WriteJoints(newTrajectory);
                }

                hasNewTrajectory = false;
                newTrajectory = null;

                float angle = (Time.time - startTime) * 0.5f;
                Point rosPoint = (0.5f * Mathf.Cos(angle), -0.25f, 0.6f + 0.5f * Mathf.Sin(angle));
                SetTargetPose(Pose.identity.WithPosition(rosPoint.Ros2Unity()));
            }
        }

        void SetTargetPose(Pose unityPose)
        {
            SetTargetPose(unityPose.Unity2RosPose());
        }

        async void SetTargetPose(Msgs.GeometryMsgs.Pose rosPose)
        {
            SetManipulatorPose srv = new SetManipulatorPose
            {
                Request = {TargetPose = rosPose}
            };
            Debug.Log($"{this}: Setting target pose.");
            await Connection.Client.CallServiceAsync("/moveit_test/calculate_trajectory", srv, true).AwaitNoThrow(this);
            
            hasNewTrajectory = true;
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
        }

        public override string ToString()
        {
            return "[ExtensionTest]";
        }
    }
}