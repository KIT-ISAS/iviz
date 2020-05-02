using Iviz.Msgs;
using UnityEngine;


namespace Iviz.App
{
    public abstract class RosSender
    {
        public string Topic { get; }
        public string Type { get; }
        public abstract int Id { get; set; }

        protected RosSender(string topic, string type)
        {
            Topic = topic;
            Type = type;

            Debug.Log("RosListener: Requesting advertisement for topic " + Topic);
        }

        public abstract void Stop();
    }

    public class RosSender<T> : RosSender where T : IMessage
    {
        public override int Id { get; set; }

        public RosSender(string topic) :
            base(topic, BuiltIns.GetMessageType(typeof(T)))
        {
            ConnectionManager.Advertise(this);
        }

        public void Publish(T msg)
        {
            ConnectionManager.Publish(this, msg);
        }

        public override void Stop()
        {
            ConnectionManager.Unadvertise(this);
        }
    }
}


