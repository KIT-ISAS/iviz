using System;

namespace Iviz.RoslibSharp
{
    /// <summary>
    /// Topic name and message type.
    /// </summary>
    [Serializable]
    public class BriefTopicInfo : JsonToString
    {
        /// <summary>
        /// Topic name
        /// </summary>
        public readonly string topic;

        /// <summary>
        /// Topic type
        /// </summary>
        public readonly string type;

        public BriefTopicInfo(string topic, string type)
        {
            this.topic = topic;
            this.type = type;
        }
    }
}
