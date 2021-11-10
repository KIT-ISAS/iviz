#nullable enable

using Iviz.Msgs;
using UnityEngine;

namespace Iviz.Ros
{
    public interface ISender
    {
        string Topic { get; }
        string Type { get; }
        int Id { get; }
        RosSenderStats Stats { get; }
        int NumSubscribers { get; }
        void Stop();
        void Publish(IMessage msg);
    }
}