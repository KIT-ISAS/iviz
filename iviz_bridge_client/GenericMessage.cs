using System.Runtime.Serialization;

namespace Iviz.Bridge.Client
{
    [DataContract]
    public sealed class GenericMessage
    {
        [DataMember(Name = "op")]
        public string Op { get; set; } = "";

        [DataMember(Name = "id")]
        public string Id { get; set; } = "";

        [DataMember(Name = "topic")]
        public string Topic { get; set; } = "";

        [DataMember(Name = "type")]
        public string Type { get; set; } = "";
    }
}
