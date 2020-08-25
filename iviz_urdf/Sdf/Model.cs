using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Model
    {
        public string Name { get; }
        public string CanonicalLink { get; }
        public bool Static { get; }
        public bool SelfCollide { get; }
        public bool AllowAutoDisable { get; }
        public ReadOnlyCollection<Include> Includes { get; }
        public ReadOnlyCollection<Model> Models { get; }
        public bool EnableWind { get; }
        public ReadOnlyCollection<Frame> Frames { get; }
        public Pose Pose { get; } = Pose.Identity;
        public ReadOnlyCollection<Link> Links { get; }
        
        internal Model(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;
            CanonicalLink = node.Attributes?["canonical_link"]?.Value;

            List<Include> includes = new List<Include>();
            List<Model> models = new List<Model>();
            List<Frame> frames = new List<Frame>();
            List<Link> links = new List<Link>();

            Includes = includes.AsReadOnly();
            Models = models.AsReadOnly();
            Frames = frames.AsReadOnly();
            Links = links.AsReadOnly(); 

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "static":
                        Static = BoolElement.ValueOf(child);
                        break;
                    case "self_collide":
                        SelfCollide = BoolElement.ValueOf(child);
                        break;
                    case "allow_auto_disable":
                        AllowAutoDisable = BoolElement.ValueOf(child);
                        break;
                    case "include":
                        includes.Add(new Include(child));
                        break;
                    case "model":
                        models.Add(new Model(child));
                        break;
                    case "enable_wind":
                        EnableWind = BoolElement.ValueOf(child);
                        break;
                    case "frame":
                        frames.Add(new Frame(child));
                        break;
                    case "pose":
                        Pose = new Pose(child);
                        break;
                    case "link":
                        links.Add(new Link(child));
                        break;
                }
            }            
        }
    }
}