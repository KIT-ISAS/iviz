namespace Iviz.Msgs;

public interface IHasSerializer<TMessage> where TMessage : IMessage
{
    Serializer<TMessage> CreateSerializer();
    Deserializer<TMessage> CreateDeserializer();
}

public class Serializer<TMessage> : Serializer where TMessage : IMessage
{
    public virtual void RosSerialize(TMessage msg, ref WriteBuffer b)
    {
    }

    public virtual void RosSerialize(TMessage msg, ref WriteBuffer2 b)
    {
    }

    public virtual int RosMessageLength(TMessage msg) => 0;

    public virtual int Ros2MessageLength(TMessage msg) => 0;

    public virtual void RosValidate(TMessage msg)
    {
    }

    public sealed override void RosSerialize(IMessage msg, ref WriteBuffer b) => RosSerialize((TMessage)msg, ref b);

    public sealed override void RosSerialize(IMessage msg, ref WriteBuffer2 b) => RosSerialize((TMessage)msg, ref b);

    public sealed override int RosMessageLength(IMessage msg) => RosMessageLength((TMessage)msg);

    public sealed override int Ros2MessageLength(IMessage msg) => RosMessageLength((TMessage)msg);

    public sealed override void RosValidate(IMessage msg) => RosValidate((TMessage)msg);
}

public abstract class Serializer
{
    public abstract void RosSerialize(IMessage msg, ref WriteBuffer b);
    public abstract void RosSerialize(IMessage msg, ref WriteBuffer2 b);
    public abstract int RosMessageLength(IMessage msg);
    public abstract int Ros2MessageLength(IMessage msg);
    public abstract void RosValidate(IMessage msg);
}

public abstract class Deserializer<TMessage> : Deserializer where TMessage : IMessage
{
    public abstract void RosDeserialize(ref ReadBuffer b, out TMessage msg);
    public abstract void RosDeserialize(ref ReadBuffer2 b, out TMessage msg);

    public sealed override void RosDeserialize(ref ReadBuffer b, out IMessage msg)
    {
        RosDeserialize(ref b, out TMessage tMessage);
        msg = tMessage;
    }

    public sealed override void RosDeserialize(ref ReadBuffer2 b, out IMessage msg)
    {
        RosDeserialize(ref b, out TMessage tMessage);
        msg = tMessage;
    }
}

public abstract class Deserializer
{
    public abstract void RosDeserialize(ref ReadBuffer b, out IMessage msg);
    public abstract void RosDeserialize(ref ReadBuffer2 b, out IMessage msg);
}