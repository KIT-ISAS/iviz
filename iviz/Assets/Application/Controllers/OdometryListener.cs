using UnityEngine;

namespace Iviz.App
{
    /*
    public class OdometryListener : MonoBehaviour
    {
        RosListener<Odometry> listener;

        public string Topic = "/odometry";
        public bool ShowPoseCovariance;
        GameObject axis;

        public bool Connected;
        public bool Subscribed;
        public float MessagesPerSecond;

        // Start is called before the first frame update
        void Start()
        {
            listener = new RosListener<Odometry>(Topic, Handler);
            axis = Instantiate(Resources.Load<GameObject>("Axis"));
            axis.transform.parent = transform;

            GameThread.EverySecond += UpdateStats;
        }

        void OnDestroy()
        {
            GameThread.EverySecond -= UpdateStats;
            listener?.Stop();
        }

        void Handler(Odometry msg)
        {
            string parentId = msg.header.frame_id;
            transform.parent = TFListener.GetOrCreateFrame(parentId, null).transform;
            transform.SetLocalPose(msg.pose.pose.Ros2Unity());
        }

        void UpdateStats()
        {
            Connected = listener.Connected;
            Subscribed = listener.Subscribed;
            MessagesPerSecond = listener.UpdateStats().MessagesPerSecond;
        }

    }
    */
}