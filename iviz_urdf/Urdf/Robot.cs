using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Robot
    {
        [DataMember] public string Name { get; }
        [DataMember] public Link[] Links { get; }
        [DataMember] public Joint[] Joints { get; }
        [DataMember] public Material[] Materials { get; }

        Robot(XmlNode node)
        {
            List<Link> links = new List<Link>();
            List<Joint> joints = new List<Joint>();
            List<Material> materials = new List<Material>();

            Name = node.Attributes?["name"]?.Value ?? ""; // !

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "link":
                        links.Add(new Link(child));
                        break;
                    case "joint":
                        joints.Add(new Joint(child));
                        break;
                    case "material":
                        materials.Add(new Material(child));
                        break;
                }
            }

            Links = links.ToArray();
            Joints = joints.ToArray();
            Materials = materials.ToArray();
        }

        internal static Robot Create(XmlDocument document)
        {
            XmlNode root = document.FirstChild;
            while (root != null && root.Name != "robot")
            {
                root = root.NextSibling;
            }

            if (root is null)
            {
                throw new MalformedUrdfException("Urdf has no root node");
            }

            return new Robot(root);
        }

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}