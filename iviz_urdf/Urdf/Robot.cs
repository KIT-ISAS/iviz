using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Robot
    {
        public string Name { get; }
        public ReadOnlyCollection<Link> Links { get; }
        public ReadOnlyCollection<Joint> Joints { get; }
        public ReadOnlyCollection<Material> Materials { get; }

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

            Links = new ReadOnlyCollection<Link>(links);
            Joints = new ReadOnlyCollection<Joint>(joints);
            Materials = new ReadOnlyCollection<Material>(materials);
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
    }
}