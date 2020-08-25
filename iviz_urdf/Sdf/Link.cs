using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Link
    {
        public string Name { get; }
        public bool Gravity { get; } = true;
        public bool EnableWind { get; }
        public bool SelfCollide { get; }
        public bool Kinematic { get; }
        public bool MustBeBaseLink { get; }
        public Pose Pose { get; } = Pose.Identity;
        public ReadOnlyCollection<Visual> Visuals { get; }
        public ReadOnlyCollection<Light> Lights { get; }

        internal Link(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;

            List<Visual> visuals = new List<Visual>();
            List<Light> lights = new List<Light>();

            Visuals = visuals.AsReadOnly();
            Lights = lights.AsReadOnly();

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "gravity":
                        Gravity = BoolElement.ValueOf(child);
                        break;
                    case "enable_wind":
                        EnableWind = BoolElement.ValueOf(child);
                        break;
                    case "self_collide":
                        SelfCollide = BoolElement.ValueOf(child);
                        break;
                    case "kinematic":
                        Kinematic = BoolElement.ValueOf(child);
                        break;
                    case "must_be_base_link":
                        MustBeBaseLink = BoolElement.ValueOf(child);
                        break;
                    case "pose":
                        Pose = new Pose(child);
                        break;
                    case "visual":
                        visuals.Add(new Visual(child));
                        break;
                    case "light":
                        lights.Add(new Light(child));
                        break;
                }
            }            
        }
    }
}