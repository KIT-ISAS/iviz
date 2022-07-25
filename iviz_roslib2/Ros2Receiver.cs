using Iviz.Msgs;

namespace Iviz.Roslib2;

public sealed class Ros2Receiver : IRosConnection
{
    internal Guid guid;

    public string Topic { get; }
    public string Type { get; }
    public ref readonly Guid Guid => ref guid;
    public IReadOnlyCollection<string> RosHeader => Array.Empty<string>();

    public Ros2Receiver(string topic, string type)
    {
        Topic = topic;
        Type = type;
    }
}