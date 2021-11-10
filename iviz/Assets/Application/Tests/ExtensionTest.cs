using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.MoveitMsgs;
using Iviz.Msgs.ObjectRecognitionMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.ShapeMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Msgs.TrajectoryMsgs;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Tools.Logger;
using Pose = UnityEngine.Pose;
using Quaternion = Iviz.Msgs.GeometryMsgs.Quaternion;
using Time = UnityEngine.Time;

#if false
using Iviz.Msgs.MoveitTest;


namespace Iviz.App
{
    public class ExtensionTest : MonoBehaviour
    {
        RobotModel robot;
        RobotModel planRobot;

        bool initialized;
        RosChannelReader<JointState> jointsListener;
        RosChannelWriter<PlanningScene> scenePublisher;

        float startTime; // start time, used only to calculate the circle

        bool hasNewTrajectory;
        [CanBeNull] JointTrajectory newTrajectory;

        public GameObject[] children;

        RosClient connectionClient;

        // this gets called when we're connected to the master
        async void Start()
        {
            Uri masterUri = new Uri("http://141.3.59.5:11311");
            Uri myUri = RosClient.TryGetCallerUriFor(masterUri, 7635) ?? RosClient.TryGetCallerUri(7635);
            connectionClient = await RosClient.CreateAsync(masterUri, "/iviz_test", myUri);

            scenePublisher = new RosChannelWriter<PlanningScene> {LatchingEnabled = true};
            await scenePublisher.StartAsync(connectionClient, "/move_group/monitored_planning_scene");
            
            //Logger.LogDebug = Debug.Log;
            Logger.Log = Debug.Log;
            Logger.LogError = Debug.LogWarning;

            Debug.Log($"{this}: Connected!");
            while (true)
            {
                if (this == null)
                {
                    return;
                }

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
            // these joints apply only for 'robot'. 
            jointsListener = new RosChannelReader<JointState>();
            await jointsListener.StartAsync(connectionClient, "/joint_states");

            // here we create a new collision object for the scene
            PlanningScene scene = new PlanningScene();
            scene.Name = "(noname+)"; // taken from rviz
            scene.World.CollisionObjects = new[]
            {
                new CollisionObject
                {
                    Header = new Header(0, time.Now(), "world"),
                    Id = "cube0",
                    Operation = CollisionObject.ADD,
                    Primitives = new[]
                    {
                        new SolidPrimitive
                        {
                            Type = SolidPrimitive.BOX,
                            Dimensions = new double[] {0.25, 0.25, 0.25} // width of the cube
                        }
                    },
                    PrimitivePoses = new[]
                    {
                        new Msgs.GeometryMsgs.Pose((0.2, 0, 0.3), Quaternion.Identity), // ROS coordinates!
                    }
                }
            };
            scene.IsDiff = true; // important! we mark the object as a change, not as the whole scene

            scenePublisher.Write(scene);


            // tell Update() we're finished
            initialized = true;
        }

        async ValueTask GenerateRobotAsync()
        {
            // move the world by 1 meter upwards
            TfListener.RootFrame.SetPose(new Pose(new UnityEngine.Vector3(0, 1f, 0), UnityEngine.Quaternion.identity));
            
            string pandaUrdf = UnityEngine.Resources.Load<TextAsset>("Package/iviz/robots/panda").text;

            // create actual robot
            robot = new RobotModel(pandaUrdf);
            await robot.StartAsync();
            
            // attach the robot to the movable OriginFrame node 
            robot.BaseLinkObject.transform.SetParentLocal(TfListener.OriginFrame.transform);

            // create plan robot
            planRobot = new RobotModel(pandaUrdf)
            {
                Tint = Color.yellow.WithAlpha(0.5f),
                UnityName = "panda-plan"
            };
            await planRobot.StartAsync();
            
            // attach the plan robot to the movable OriginFrame node 
            planRobot.BaseLinkObject.transform.SetParentLocal(TfListener.OriginFrame.transform);
            
            Debug.Log("Pose of the plan robot in Unity coordinates is " + planRobot.BaseLinkObject.transform.AsPose()); // displaced
            Debug.Log("Pose of the plan robot in non-translated Unity coordinates is " +  
                      TfListener.RelativePoseToOrigin(planRobot.BaseLinkObject.transform.AsPose())); // zero
            // you can also use TfListener.RelativePositionToOrigin() if you only change the position

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
            SetManipulatorPose setManipulatorSrv = new SetManipulatorPose
            {
                Request = {TargetPose = rosPose}
            };

            Debug.Log($"{this}: Setting target pose.");
            try
            {
                // plan
                await connectionClient.CallServiceAsync("/moveit_test/calculate_trajectory",
                    setManipulatorSrv, false, 3000);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Setting target pose failed with error: " + e.Message);
            }

            if (setManipulatorSrv.Response.Success)
            {
                newTrajectory = setManipulatorSrv.Response.Trajectory;
                Debug.Log($"{this}: Plan for target pose succeeded.");
            }
            else
            {
                newTrajectory = null;
                Debug.Log($"{this}: Plan for target pose failed!");
            }

            ExecuteTrajectory executeTrajectorySrv = new ExecuteTrajectory();
            Debug.Log($"{this}: Executing trajectory.");
            try
            {
                // execute the plan
                await connectionClient.CallServiceAsync("/moveit_test/execute_trajectory",
                    executeTrajectorySrv, false, 5000);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Executing target trajectory failed with error: " + e.Message);
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
#endif