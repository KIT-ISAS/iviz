using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Geometry
    {
        public Box Box { get; }
        public Cylinder Cylinder { get; }
        public Sphere Sphere { get; }
        public Mesh Mesh { get; }

        internal Geometry(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "box":
                        Box = new Box(child);
                        break;
                    case "cylinder":
                        Cylinder = new Cylinder(child);
                        break;
                    case "sphere":
                        Sphere = new Sphere(child);
                        break;
                    case "mesh":
                        Mesh = new Mesh(child);
                        break;
                }
            }
        }
    }
}