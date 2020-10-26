using Iviz.Msgs;
using Iviz.Roslib;
using System.Collections.Generic;

namespace Iviz.Bridge
{
    class TopicAction
    {
        public readonly string topic;
        public readonly TypeInfo type;
        public readonly string topicId;
        readonly HashSet<string> ids = new HashSet<string>();

        public TopicAction(string topic, TypeInfo type, string topicId)
        {
            this.topic = topic;
            this.type = type;
            this.topicId = topicId;
        }

        public void AddId(string id)
        {
            ids.Add(id);
        }

        public void RemoveId(string id)
        {
            ids.Remove(id);
        }

        public bool Empty()
        {
            return ids.Count == 0;
        }
    }

    class Subscription : TopicAction
    {
        public readonly IRosSubscriber subscriber;

        public Subscription(string topic, TypeInfo type, IRosSubscriber subscriber, string topicId)
            : base(topic, type, topicId)
        {
            this.subscriber = subscriber;
        }

        public void Stop()
        {
            subscriber.Unsubscribe(topicId);
        }

        public PublishMessage GeneratePublishMessage()
        {
            return type.generator.Generate();
        }
    }

    class Advertisement : TopicAction
    {
        public readonly IRosPublisher publisher;

        public Advertisement(string topic, TypeInfo type, IRosPublisher publisher, string topicId)
            : base(topic, type, topicId)
        {
            this.publisher = publisher;
        }

        public void Stop()
        {
            publisher.Unadvertise(topicId);
        }

        public IMessage Deserialize(string data)
        {
            return type.generator.Deserialize(data).GetMessage();
        }
    }
}
