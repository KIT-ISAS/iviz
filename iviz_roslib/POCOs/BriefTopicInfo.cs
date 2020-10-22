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

        internal BriefTopicInfo(string topic, string type)
        {
            Topic = topic;
            Type = type;
        }

        public override string ToString()
        {
            return $"[Topic='{Topic}' Type='{Type}']";
        }
    }
}
