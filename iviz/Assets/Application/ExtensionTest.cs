using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.Tf2Msgs;
using Iviz.Ros;
using UnityEngine;

namespace Iviz.App
{
    public class ExtensionTest : MonoBehaviour
    {
        static RoslibConnection Connection => ConnectionManager.Connection;

        Listener<TFMessage> tfListener;
        LineResource lines;

        void Awake()
        {
            // wait until the GUI finishes initializing
            ModuleListPanel.InitFinished += Initialize;
        }

        void Initialize()
        {
            Debug.Log("was here");
        
            // remove our stuff from the event
            ModuleListPanel.InitFinished -= Initialize;

            // we setup the connection data here
            Connection.ConnectionStateChanged += OnConnectionStateChanged;
            Connection.KeepReconnecting = true;

            // change this!
            Connection.MasterUri = new Uri("http://141.3.59.5:11311");
            Connection.MyUri = new Uri("http://141.3.59.19:7613");
            Connection.MyId = "/iviz_test";

            // connection should start in the background

            // make GUI invisible
            ModuleListPanel.Instance.AllGuiVisible = false;

            // get a line builder
            lines = ResourcePool.GetOrCreateDisplay<LineResource>();

            //GameObject linesBaseObject = UnityEngine.Resources.Load<GameObject>("Displays/Line");
            //lines = Instantiate(linesBaseObject).GetComponent<LineResource>();
            

            // build some robots!
            GenerateRobot();
        }

        // this gets called when we're connected to the master
        void OnConnectionStateChanged(ConnectionState state)
        {
            if (state == ConnectionState.Connected)
            {
                Debug.Log("Connected!");

                // example subscription
                tfListener = new Listener<TFMessage>("/tf", TfMessageHandler);
            }
        }

        // gets called when something is published in TF
        void TfMessageHandler(TFMessage msg)
        {
            Debug.Log("Got a TF message!");
            foreach (var tf in msg.Transforms)
            {
                Debug.Log("Got update for " + tf.ChildFrameId);
                Debug.Log("Its local pose in unity coordintes is " + tf.Transform.Ros2Unity());

                if (TfListener.TryGetFrame(tf.ChildFrameId, out TfFrame frame))
                {
                    Debug.Log("Its absolute pose in unity coordinates is " + frame.WorldPose);
                }
            }
        }

        void GenerateRobot()
        {
            // you can load your own urdf as a string
            string pandaUrdf = UnityEngine.Resources.Load<TextAsset>("Package/iviz/robots/panda").text;


            // first, we create a normal robot
            RobotModel robot1 = new RobotModel(pandaUrdf, Connection);
            robot1.BaseLinkObject.transform.position = new Vector3(0, 0, 0);

            // now we create a yellow robot
            RobotModel robot2 = new RobotModel(pandaUrdf, Connection);
            robot2.Tint = Color.yellow;
            robot2.BaseLinkObject.transform.position = new Vector3(1, 0, 0);

            // now we create a semitransparent robot with a moved joint
            RobotModel robot3 = new RobotModel(pandaUrdf, Connection);
            robot3.Tint = new Color(1, 1, 1, 0.5f);

            // get any joint from the list
            string anyJoint = robot3.Joints.Keys.First();

            // set its value to 90 dev
            robot3.TryWriteJoint(anyJoint, Mathf.PI / 2, out _);

            robot3.BaseLinkObject.transform.position = new Vector3(2, 0, 0);

            // now draw a square
            Vector3[] pointsInRosCoords =
            {
                new Vector3(-1, -3, 0.5f),
                new Vector3(-1, 1, 0.5f),
                new Vector3(1, 1, 0.5f),
                new Vector3(1, -3, 0.5f),
            };


            List<LineWithColor> linesToDraw = new List<LineWithColor>
            {
                new LineWithColor(pointsInRosCoords[0].Ros2Unity(), pointsInRosCoords[1].Ros2Unity(), Color.red),
                new LineWithColor(pointsInRosCoords[1].Ros2Unity(), pointsInRosCoords[2].Ros2Unity(), Color.red),
                new LineWithColor(pointsInRosCoords[2].Ros2Unity(), pointsInRosCoords[3].Ros2Unity(), Color.red),
                new LineWithColor(pointsInRosCoords[3].Ros2Unity(), pointsInRosCoords[0].Ros2Unity(), Color.red),
            };

            lines.ElementScale = 0.015f; // 5 mm width
            lines.LinesWithColor = linesToDraw;
        }
    }
}