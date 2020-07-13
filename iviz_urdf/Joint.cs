using System.Xml;

namespace Iviz.Urdf
{
    public class Joint
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
            Name = Utils.ParseString(node.Attributes["name"]);
            string typeStr = Utils.ParseString(node.Attributes["type"]);
            Type = GetJointType(typeStr, node);

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "origin":
                        Origin = new Origin(child);
                        break;
                    case "parent":
                        Parent = new Parent(child);
                        break;
                    case "child":
                        Child = new Child(child);
                        break;
                    case "axis":
                        Axis = new Axis(child);
                        break;
                    case "limit":
                        Limit = new Limit(child);
                        break;
                }
            }

            if (Origin is null)
            {
                Origin = Origin.Identity;
            }

            if (Parent is null)
            {
                throw new MalformedUrdfException(node);
            }

            if (Child is null)
            {
                throw new MalformedUrdfException(node);
            }

            if (Axis is null)
            {
                Axis = Axis.Right;
            }

            if (Limit is null)
            {
                Limit = Limit.Empty;
            }
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