using System.Runtime.Serialization;

namespace Iviz.Bridge.Client
{
    [DataContract]
    public sealed class GenericResponse
    {
        [DataMember(Name = "op")]
        public string Op { get; set; } = "";

        [DataMember(Name = "id")]
        public string Id { get; set; } = "";

        [DataMember(Name = "value")]
        public string Value { get; set; } = "";
    }
}
