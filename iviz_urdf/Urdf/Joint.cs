using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Joint
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum JointType
        {
            Revolute,
            Continuous,
            Prismatic,
            Fixed,
            Floating,
            Planar
        }
        
        [DataMember] public string Name { get; }
        [DataMember] public JointType Type { get; }
        [DataMember] public Origin Origin { get; }
        [DataMember] public Parent Parent { get; }
        [DataMember] public Child Child { get; }
        [DataMember] public Axis Axis { get; }
        [DataMember] public Limit Limit { get; }

        internal Joint(XmlNode node)
        {
            Name = Utils.ParseString(node.Attributes?["name"]);
            string typeStr = Utils.ParseString(node.Attributes?["type"]);
            Type = GetJointType(typeStr, node);

            Origin? origin = null;
            Parent? parent = null;
            Child? child = null;
            Axis? axis = null;
            Limit? limit = null;
            
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "origin":
                        origin = new Origin(childNode);
                        break;
                    case "parent":
                        parent = new Parent(childNode);
                        break;
                    case "child":
                        child = new Child(childNode);
                        break;
                    case "axis":
                        axis = new Axis(childNode);
                        break;
                    case "limit":
                        limit = new Limit(childNode);
                        break;
                }
            }

            Origin = origin ?? Origin.Identity;
            Parent = parent ?? throw new MalformedUrdfException(node);
            Child = child ?? throw new MalformedUrdfException(node);
            Axis = axis ?? Axis.Right;
            Limit = limit ?? Limit.Empty;
        }

        static JointType GetJointType(string type, XmlNode node)
        {
            switch (type)
            {
                case "revolute": return JointType.Revolute;
                case "continuous": return JointType.Continuous;
                case "prismatic": return JointType.Prismatic;
                case "fixed": return JointType.Fixed;
                case "floating": return JointType.Floating;
                case "planar": return JointType.Planar;
                default: throw new MalformedUrdfException(node);
            }
        }        
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}