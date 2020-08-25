using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Visual
    {
        public string Name { get; }
        public bool CastShadows { get; } = true;
        public double Transparency { get; }
        public Pose Pose { get; } = Pose.Identity;
        public Material Material { get; }
        public Geometry Geometry { get; }

        internal Visual(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;
            
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "cast_shadows":
                        CastShadows = BoolElement.ValueOf(child);
                        break;
                    case "transparency":
                        Transparency = DoubleElement.ValueOf(child);
                        break;
                    case "pose":
                        Pose = new Pose(child);
                        break;
                    case "material":
                        Material = new Material(child);
                        break;
                    case "geometry":
                        Geometry = new Geometry(child);
                        break;
                }
            }
        }
    }
}