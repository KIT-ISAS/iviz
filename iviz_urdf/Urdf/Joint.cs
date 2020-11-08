using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Joint
    {
        public enum JointType
        {
            Revolute,
            Continuous,
            Prismatic,
            Fixed,
            Floating,
            Planar
        }
        
        public string Name { get; }
        public JointType Type { get; }
        public Origin Origin { get; }
        public Parent Parent { get; }
        public Child Child { get; }
        public Axis Axis { get; }
        public Limit Limit { get; }

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
    }
}