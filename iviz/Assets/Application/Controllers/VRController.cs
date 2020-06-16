//#define USING_VR

using UnityEngine;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.Tf2Msgs;
using Iviz.App.Listeners;
using Iviz.Msgs.StdMsgs;
using Iviz.Resources;

#if USING_VR
using Iviz.App.Displays;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;
#endif

namespace Iviz.App
{
#if !USING_VR
    public class VRController : MonoBehaviour
    {
        public const bool UsingVR = false;
    }
#endif

#if USING_VR
    public class VRController : MonoBehaviour
        public const bool UsingVR = true;

        public GameObject LeftController;
        public GameObject RightController;
        public GameObject Head;
        //public GameObject LeftCollider;
        //public GameObject RightCollider;
        public GameObject RightShoulderMarker;
        bool prevLeftDown;
        bool prevRightDown;

        public SteamVR_Input_Sources LeftInputSource = SteamVR_Input_Sources.LeftHand;
        public SteamVR_Input_Sources RightInputSource = SteamVR_Input_Sources.RightHand;
        public SteamVR_Input_Sources RightShoulderSource = SteamVR_Input_Sources.RightShoulder;

        public string LeftControllerTopic { get; } = "/holodeck/vive_left";
        public string RightControllerTopic { get; } = "/holodeck/vive_right";
        public string RightShoulderMarkerTopic { get; } = "/holodeck/marker_rs";
        public string SmhiHeadTopic { get; } = "/smhi_interface/headpose";
        public string TfTopic { get; } = TFListener.DefaultTopic;
        public string LeftFrameName { get; } = "holodeck/left_controller";
        public string RightFrameName { get; } = "holodeck/right_controller";
        public string RightShoulderMarkerName { get; } = "holodeck/marker_rs";
        public string HeadFrameName { get; } = "holodeck/head";

        RosSender<Joy> rosSenderLeft, rosSenderRight;
        RosSender<Msgs.GeometryMsgs.PoseStamped> rosSenderHead;

        public InteractiveMarkerListener interactiveMarkerListener;


        public void Start()
        {
            Teleport.instance.CancelTeleportHint();
            //LeftCollider.GetComponent<MeshRenderer>().enabled = false;
            //RightCollider.GetComponent<MeshRenderer>().enabled = false;

            if (LeftController == null)
            {
                LeftController = GameObject.Find("LeftHand");
            }
            if (RightController == null)
            {
                RightController = GameObject.Find("RightHand");
            }
            if (Head == null)
            {
                Head = GameObject.Find("VRCamera");
            }


            rosSenderLeft = new RosSender<Joy>(LeftControllerTopic);
            rosSenderRight = new RosSender<Joy>(RightControllerTopic);
            rosSenderHead = new RosSender<Msgs.GeometryMsgs.PoseStamped>(SmhiHeadTopic);

            //ConnectionManager.Instance.Advertise<Joy>(LeftControllerTopic);
            //ConnectionManager.Instance.Advertise<Joy>(RightControllerTopic);
            //ConnectionManager.Instance.Advertise<TFMessage>(TfTopic);
        }

        enum Button
        {
            Trigger = 0,
            Trackpad_Touched = 1,
            Trackpad_Pressed = 2,
            Menu = 3,
            Gripper = 3,
        }

        uint joySeq = 0;

        Joy CreateJoyMessage(SteamVR_Input_Sources inputSource, string frameName)
        {
            int[] buttons = { 0, 0, 0, 0, 0 };
            float squeeze = SteamVR_Actions._default.Squeeze.GetAxis(inputSource);
            if (squeeze > 0.7f)
            {
                buttons[(int)Button.Trigger] = 1;
            }
            if (SteamVR_Actions._default.MenuClick.GetState(inputSource))
            {
                buttons[(int)Button.Menu] = 1;
            }
            if (SteamVR_Actions._default.TrackpadClick.GetState(inputSource))
            {
                buttons[(int)Button.Trackpad_Pressed] = 1;
            }
            Vector2 trackpadPos = SteamVR_Actions._default.TrackpadTouch.GetAxis(inputSource);
            if (trackpadPos.sqrMagnitude != 0.0f)
            {
                buttons[(int)Button.Trackpad_Touched] = 1;
            }

            return new Joy()
            {
                Header = new Header
                {
                    Stamp = RosUtils.GetRosTime(),
                    FrameId = frameName
                },
                Buttons = buttons.ToArray(),
                Axes = new[]
                {
                    squeeze, trackpadPos.x, trackpadPos.y
                }
            };
        }

        uint tfSeq = 0;
        TFMessage CreateTfMessage()
        {
            return new TFMessage
            {
                Transforms = new[]
                {
                    new Msgs.GeometryMsgs.TransformStamped
                    {
                        Header = RosUtils.CreateHeader(tfSeq++, TFListener.BaseFrame.Id),
                        ChildFrameId = LeftFrameName,
                        Transform = LeftController.transform.AsPose().Unity2RosTransform(),
                    },
                    new Msgs.GeometryMsgs.TransformStamped
                    {
                        Header = RosUtils.CreateHeader(tfSeq++, TFListener.BaseFrame.Id),
                        ChildFrameId = RightFrameName,
                        Transform = RightController.transform.AsPose().Unity2RosTransform()
                    },
                    new Msgs.GeometryMsgs.TransformStamped
                    {
                        Header = RosUtils.CreateHeader(tfSeq++, TFListener.BaseFrame.Id),
                        ChildFrameId = HeadFrameName,
                        Transform = Head.transform.AsPose().Unity2RosTransform()
                    },
                    /*
                    new RosSharp.RosBridgeClient.MessageTypes.Geometry.TransformStamped
                    {
                        header = Utils.CreateHeader(0, TFListener.BaseFrame.Id),
                        child_frame_id = RightShoulderMarkerName,
                        transform = RightShoulderMarker.transform.AsPose().Unity2RosTransform()
                    }
                    */
                }
            };
        }

        void CheckForSelectClick()
        {
            Vector3 center = LeftController.transform.position;
            float radius = 0.15f;
            Collider[] colliders = Physics.OverlapSphere(center, radius, 1 << Resource.ClickableLayer);

            Collider hitCollider = null;

            List<ClickableDisplayNode> hitObjects = new List<ClickableDisplayNode>();
            List<Collider> hitColliders = new List<Collider>();

            foreach (Collider c in colliders)
            {
                ClickableDisplayNode obj = c.GetComponentInParent<ClickableDisplayNode>();
                if (obj == null)
                {
                    continue;
                }
                hitObjects.Add(obj);
                hitColliders.Add(c);
            }

            ClickableDisplayNode hitObject;
            if (!hitObjects.Any())
            {
                hitObject = null;
            }
            else if (hitObjects.Count == 1)
            {
                hitObject = hitObjects.First();
                hitCollider = hitColliders.First();
            }
            else
            {
                float[] distances = hitColliders.Select(x => Vector3.Distance(x.ClosestPoint(center), center)).ToArray();

                int minIndex = -1;
                float minDistance = float.MaxValue;
                for (int i = 0; i < distances.Length; i++)
                {
                    if (distances[i] < minDistance)
                    {
                        minDistance = distances[i];
                        minIndex = i;
                    }
                }
                hitObject = hitObjects[minIndex];
                hitCollider = hitColliders[minIndex];
            }

            //interactiveMarkerListener.OnSelectInteractableObject(hitObject);

            bool newLeftDown = SteamVR_Actions._default.Squeeze.GetAxis(LeftInputSource) > 0.7f;
            bool leftClick = !prevLeftDown && newLeftDown;
            prevLeftDown = newLeftDown;
            if (leftClick) Debug.Log("Left Click!");

            bool newRightDown = SteamVR_Actions._default.MenuClick.GetState(LeftInputSource);
            bool rightClick = !prevRightDown && newRightDown;
            prevRightDown = newRightDown;
            if (rightClick) Debug.Log("Right Click!");

            if (rightClick)
            {
                interactiveMarkerListener = GameObject.Find("InteractiveMarkers")?.GetComponent<InteractiveMarkerListener>();
                if (interactiveMarkerListener != null)
                {
                    interactiveMarkerListener.ClearAll();
                    Debug.Log("Clear all!");
                }
                //interactiveMarkerListener.OnClickInteractableObject(hitObject, hitPoint, 1);
            }


            if (hitObject == null)
            {
                return;
            }

            if (!leftClick && !rightClick)
            {
                return;
            }

            Vector3 hitPoint = hitCollider.ClosestPoint(center);

            if (leftClick)
            {
                //interactiveMarkerListener.OnClickInteractableObject(hitObject, hitPoint, 0);
                Debug.Log(hitObject.gameObject.name);
                hitObject.OnPointerClick(new PointerEventData(EventSystem.current)
                {
                    pressPosition = hitPoint,
                    position = hitPoint,
                    clickTime = UnityEngine.Time.realtimeSinceStartup,
                    clickCount = 1,
                    pointerCurrentRaycast = new RaycastResult
                    {
                        worldPosition = hitPoint
                    }
                });
            }
        }

        uint headSeq = 0;
        public void Update()
        {
            //frame++;
            //if ((frame % 10) == 0)
            //{

            Joy leftMsg = CreateJoyMessage(LeftInputSource, LeftFrameName);
            leftMsg.Header.Seq = joySeq;
            //ConnectionManager.Instance.Publish(LeftControllerTopic, leftMsg);
            rosSenderLeft.Publish(leftMsg);

            Joy rightMsg = CreateJoyMessage(RightInputSource, RightFrameName);
            rightMsg.Header.Seq = joySeq;
            //ConnectionManager.Instance.Publish(RightControllerTopic, rightMsg);
            rosSenderRight.Publish(rightMsg);

            joySeq++;

            TFMessage tFMsg = CreateTfMessage();
            TFListener.Publisher.Publish(tFMsg);
            //}

            Msgs.GeometryMsgs.PoseStamped pose = new Msgs.GeometryMsgs.PoseStamped
            {
                Header = RosUtils.CreateHeader(headSeq++, TFListener.BaseFrame.Id),
                Pose = Head.transform.AsPose().Unity2RosPose()
            };
            rosSenderHead.Publish(pose);

            CheckForSelectClick();
        }
#endif
}
}
