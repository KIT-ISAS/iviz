namespace Iviz.Roslib
{
    /// <summary>
    /// Topic name and message type.
    /// </summary>
    public class BriefTopicInfo : JsonToString
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
    }
}
