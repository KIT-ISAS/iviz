using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Geometry
    {
        [DataMember] public Box? Box { get; }
        [DataMember] public Cylinder? Cylinder { get; }
        [DataMember] public Sphere? Sphere { get; }
        [DataMember] public Mesh? Mesh { get; }

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
        
        public void Deconstruct(out Box? box, out Cylinder? cylinder, out Sphere? sphere, out Mesh? mesh) => 
            (box, cylinder, sphere, mesh) = (Box, Cylinder, Sphere, Mesh);
        
        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}