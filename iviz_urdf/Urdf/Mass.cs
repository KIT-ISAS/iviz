using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Mass
    {
        [DataMember] public float Value { get; }

        public Mass()
        {
            Value = 0;
        }

        internal Mass(XmlNode node)
        {
            Value = Utils.ParseFloat(node.Attributes?["value"]);
        }
        
        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}