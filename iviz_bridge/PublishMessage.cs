using System;
using Iviz.Msgs;
using Utf8Json;

namespace Iviz.Bridge
{
    public abstract class PublishMessage
    {
        public string op = "publish";
        public string topic = "";

        public abstract PublishMessage Generate();
        public abstract IMessage GetMessage();
        public abstract void SetMessage(IMessage msg);

        public abstract string Serialize();
        public abstract PublishMessage Deserialize(string data);

        public static PublishMessage Instantiate(Type msgType)
        {
            Type publishMsgType = typeof(PublishMessageImpl<>).MakeGenericType(msgType);
            return (PublishMessage)Activator.CreateInstance(publishMsgType);
        }
    }

    [Serializable]
    public sealed class PublishMessageImpl<T> : PublishMessage where T : IMessage
    {
        public T msg;

        public override PublishMessage Generate() => new PublishMessageImpl<T>();
        public override IMessage GetMessage() => msg;
        public override void SetMessage(IMessage msg)
        {
            this.msg = (T)msg;
        }

        public override string Serialize()
        {
            return JsonSerializer.ToJsonString(this);
        }

        public override PublishMessage Deserialize(string data)
        {
            return JsonSerializer.Deserialize<PublishMessageImpl<T>>(data);
        }
    }
}
