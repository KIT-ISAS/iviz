
#if USING_VR
using Valve.VR;

namespace ISAS
{
    public class ControllerManager : MonoBehaviour
    {
        const float SendPeriod = 0.033f;
        public GameObject LeftController;
        public GameObject RightController;
        public GameObject LeftCollider;
        public GameObject RightCollider;
        public GameObject RightShoulderMarker;
        bool prevLeftDown;
        bool prevRightDown;

        public SteamVR_Input_Sources LeftInputSource = SteamVR_Input_Sources.LeftHand;
        public SteamVR_Input_Sources RightInputSource = SteamVR_Input_Sources.RightHand;
        public SteamVR_Input_Sources RightShoulderSource = SteamVR_Input_Sources.RightShoulder;

        public string LeftControllerTopic = "/vr/left_controller";
        public string RightControllerTopic = "/vr/right_controller";
        public string RightShoulderMarkerTopic = "/vr/marker_rs";
        public string TfTopic = "/tf";
        public string LeftFrameName = "vr/left_controller";
        public string RightFrameName = "vr/right_controller";
        public string RightShoulderMarkerName = "vr/marker_rs";

        /*
        RosSender<Joy> rosSenderLeft, rosSenderRight;
        RosSender<TFMessage> rosSenderTF;
        */

        public InteractiveMarkerListener interactiveMarkerListener;


        public void Start()
        {
            //LeftCollider.GetComponent<MeshRenderer>().enabled = false;
            //RightCollider.GetComponent<MeshRenderer>().enabled = false;

            /*
            rosSenderLeft = new RosSender<Joy>(LeftControllerTopic);
            rosSenderRight = new RosSender<Joy>(RightControllerTopic);
            rosSenderTF = new RosSender<TFMessage>(TfTopic);
            */
            ConnectionManager.Instance.Advertise<Joy>(LeftControllerTopic);
            ConnectionManager.Instance.Advertise<Joy>(RightControllerTopic);
            ConnectionManager.Instance.Advertise<TFMessage>(TfTopic);
        }

        enum Button
        {
            Grip = 0,
            Menu = 1,
            Trackpad = 2
        }

        Joy CreateJoyMessage(SteamVR_Input_Sources inputSource, string frameName)
        {
            List<int> buttons = new List<int>();
            float squeeze = SteamVR_Actions._default.Squeeze.GetAxis(inputSource);
            if (squeeze > 0.7f)
            {
                buttons.Add((int)Button.Grip);
            }
            if (SteamVR_Actions._default.MenuClick.GetState(inputSource))
            {
                buttons.Add((int)Button.Menu);
            }
            if (SteamVR_Actions._default.TrackpadClick.GetState(inputSource))
            {
                buttons.Add((int)Button.Trackpad);
            }
            Vector2 trackpadPos = SteamVR_Actions._default.TrackpadTouch.GetAxis(inputSource);

            return new Joy()
            {
                header = new Header(0, Utils.GetRosTime(), frameName),
                buttons = buttons.ToArray(),
                axes = new[]
                {
                    squeeze, trackpadPos.x, trackpadPos.y
                }
            };
        }

        TFMessage CreateTfMessage()
        {
            return new TFMessage
            {
                transforms = new[]
                {
                    new RosSharp.RosBridgeClient.MessageTypes.Geometry.TransformStamped
                    {
                        header = Utils.CreateHeader(0, TFListener.BaseFrame.Id),
                        child_frame_id = LeftFrameName,
                        transform = LeftController.transform.AsPose().Unity2RosTransform(),
                    },
                    new RosSharp.RosBridgeClient.MessageTypes.Geometry.TransformStamped
                    {
                        header = Utils.CreateHeader(0, TFListener.BaseFrame.Id),
                        child_frame_id = RightFrameName,
                        transform = RightController.transform.AsPose().Unity2RosTransform()
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

            List<ClickableDisplay> hitObjects = new List<ClickableDisplay>();
            List<Collider> hitColliders = new List<Collider>();

            foreach (Collider c in colliders)
            {
                ClickableDisplay obj = c.GetComponentInParent<ClickableDisplay>();
                if (obj == null)
                {
                    continue;
                }
                hitObjects.Add(obj);
                hitColliders.Add(c);
            }

            ClickableDisplay hitObject;
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
                interactiveMarkerListener = GameObject.Find("InteractiveMarkers").GetComponent<InteractiveMarkerListener>();
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

        int frame = 0;
        public void Update()
        {
            frame++;
            if ((frame % 10) == 0)
            {

                Joy leftMsg = CreateJoyMessage(LeftInputSource, LeftFrameName);
                ConnectionManager.Instance.Publish(LeftControllerTopic, leftMsg);
                //rosSenderLeft.PublishPeriodic(leftMsg, 0.033f);

                Joy rightMsg = CreateJoyMessage(RightInputSource, RightFrameName);
                ConnectionManager.Instance.Publish(RightControllerTopic, rightMsg);
                //rosSenderRight.PublishPeriodic(rightMsg, 0.033f);

                TFMessage tFMsg = CreateTfMessage();
                ConnectionManager.Instance.Publish(TfTopic, tFMsg);
            }

            CheckForSelectClick();
        }

    }
}
#endif
