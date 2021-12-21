#nullable enable

using System.Text;
using Iviz.Msgs;

namespace Iviz.Ros
{
    public interface ISender
    {
        string Topic { get; }
        string Type { get; }
        internal int Id { get; }
        RosSenderStats Stats { get; }
        int NumSubscribers { get; }
        void Dispose();
        void Publish(IMessage msg);
        void WriteDescriptionTo(StringBuilder description);
    }
}