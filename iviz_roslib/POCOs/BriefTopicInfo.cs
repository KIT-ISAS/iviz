using Iviz.Roslib.Utils;

namespace Iviz.Roslib
{
    /// <summary>
    /// Topic name and message type.
    /// </summary>
    public sealed class BriefTopicInfo : JsonToString
    {
        /// <summary>
        /// Topic name
        /// </summary>
        public string Topic { get; }

        /// <summary>
        /// Topic type
        /// </summary>
        public string Type { get; }

        internal BriefTopicInfo(string topic, string type) => (Topic, Type) = (topic, type);

        public override string ToString() => $"[Topic='{Topic}' Type='{Type}']";
    }
}
