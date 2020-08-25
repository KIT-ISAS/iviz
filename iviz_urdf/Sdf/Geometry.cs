using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Geometry
    {
        public Empty Empty { get; }
        public Box Box { get; }
        public Cylinder Cylinder { get; }
        public Mesh Mesh { get; }
        public Plane Plane { get; }
        public Sphere Sphere { get; }
        
        internal Geometry(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "empty": 
                        Empty = Empty.Instance;
                        break;
                    case "box":
                        Box = new Box(child);
                        break;
                    case "cylinder":
                        Cylinder = new Cylinder(child);
                        break;
                    case "mesh":
                        Mesh = new Mesh(child);
                        break;
                    case "plane":
                        Plane = new Plane(child);
                        break;
                    case "sphere":
                        Sphere = new Sphere(child);
                        break;
                }
            }     
        }
    }
}