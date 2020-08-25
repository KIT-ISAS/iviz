using System.Xml;

namespace Iviz.Sdf
{
    public enum LightType
    {
        Point,
        Directional,
        Spot
    }
    
    public sealed class Light
    {
        static readonly Color DefaultSpecular = new Color(0.1, 0.1, 0.1, 1);
        
        public string Name { get; }
        public LightType Type { get; }
        
        public bool CastShadows { get; }
        public Color Diffuse { get; } = Color.White;
        public Color Specular { get; } = DefaultSpecular;
        public Attenuation Attenuation { get; } = Attenuation.Default;
        public Vector3 Direction { get; } = Vector3.Down;
        public Spot Spot { get; } = Spot.Default;
        public Pose Pose { get; } = Pose.Identity;
        
        internal Light(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;
            
            string type = node.Attributes?["type"]?.Value;
            switch (type)
            {
                case "directional":
                    Type = LightType.Directional;
                    break;
                case "spot":
                    Type = LightType.Spot;
                    break;
                case "point":
                case null:
                    Type = LightType.Point;
                    break;
            }
            
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "cast_shadows":
                        CastShadows = BoolElement.ValueOf(child);
                        break;
                    case "diffuse":
                        Diffuse = new Color(child);
                        break;
                    case "specular":
                        Specular = new Color(child);
                        break;
                    case "attenuation":
                        Attenuation = new Attenuation(child);
                        break;
                    case "direction":
                        Direction = new Vector3(child);
                        break;
                    case "spot":
                        Spot = new Spot(child);
                        break;
                    case "pose":
                        Pose = new Pose(child);
                        break;
                }
            }
        }
    }
}