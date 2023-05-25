using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Iviz.Msgs;

namespace Iviz.Roslib;

[DataContract]
public sealed class MessageInfo
{
    public IRosConnection Connection { get; }
    [DataMember] public int MessageSize;

    public MessageInfo(IRosConnection connection) => Connection = connection;
}

public abstract class RosCallback<T>
{
    public abstract void Handle(T message, MessageInfo info);
}

public sealed class Action2RosCallback<T> : RosCallback<T>
{
    readonly Action<T, MessageInfo> action;
    public Action2RosCallback(Action<T, MessageInfo> action) => this.action = action;
    public override void Handle(T message, MessageInfo info) => action(message, info);
}

public sealed class ActionRosCallback<T> : RosCallback<T>
{
    readonly Action<T> action;
    public ActionRosCallback(Action<T> action) => this.action = action;
    public override void Handle(T message, MessageInfo _) => action(message);
}

public sealed class GenericRosCallback<T> : RosCallback<T> where T : IMessage
{
    readonly Action<IMessage> action;
    public GenericRosCallback(Action<IMessage> action) => this.action = action;
    public override void Handle(T message, MessageInfo _) => action(message);
}

public sealed class Generic2RosCallback<T> : RosCallback<T> where T : IMessage
{
    readonly Action<IMessage, MessageInfo> action;
    public Generic2RosCallback(Action<IMessage, MessageInfo> action) => this.action = action;
    public override void Handle(T message, MessageInfo info) => action(message, info);
}