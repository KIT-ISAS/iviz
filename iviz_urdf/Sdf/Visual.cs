using System.Collections.Generic;
using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Visual
    {
        public string? Name { get; }
        public bool CastShadows { get; } = true;
        public double Transparency { get; }
        public Pose Pose { get; } = Pose.Identity;
        public Material? Material { get; }
        public Geometry Geometry { get; }

        internal bool HasUri { get; }
    
        internal Visual(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;

            Geometry? geometry = null;
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
                        geometry = new Geometry(child);
                        break;
                }
            }

            Geometry = geometry ?? throw new MalformedSdfException(node, "Expected geometry!"); 
            HasUri = Geometry.HasUri;
        }

        Visual(Visual source, IReadOnlyDictionary<string, string> modelPaths)
        {
            Name = source.Name;
            CastShadows = source.CastShadows;
            Transparency = source.Transparency;
            Pose = source.Pose;
            Material = source.Material;
            Geometry = source.Geometry.ResolveUris(modelPaths);
        }
        
        internal Visual ResolveUris(IReadOnlyDictionary<string, string> modelPaths)
        {
            return HasUri ? new Visual(this, modelPaths) : this;
        }        
    }
}