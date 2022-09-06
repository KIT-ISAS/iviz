using System;
using Iviz.Msgs;

namespace Iviz.Roslib;

public abstract class RosCallback<T>
{
    public abstract void Handle(T message, IRosConnection info);
}

public sealed class ActionRosCallback<T> : RosCallback<T>
{
    readonly RosCallbackDel action;
    public delegate void RosCallbackDel(in T message, IRosConnection info);
    public ActionRosCallback(RosCallbackDel action) => this.action = action;
    public override void Handle(T message, IRosConnection info) => action(in message, info);
}

public sealed class DirectRosCallback<T> : RosCallback<T>
{
    readonly Action<T> action;
    public DirectRosCallback(Action<T> action) => this.action = action;
    public override void Handle(T message, IRosConnection _) => action(message);
}