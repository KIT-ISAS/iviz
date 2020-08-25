using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Link
    {
        public string Name { get; }
        public Inertial Inertial { get; }
        public ReadOnlyCollection<Visual> Visuals { get; }
        public ReadOnlyCollection<Collision> Collisions { get; }

        internal Link(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;

            List<Visual> visuals = new List<Visual>();
            List<Collision> collisions = new List<Collision>();

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "inertial":
                        Inertial = new Inertial(child);
                        break;
                    case "visual":
                        visuals.Add(new Visual(child));
                        break;
                    case "collision":
                        collisions.Add(new Collision(child));
                        break;
                }
            }

            Inertial ??= Inertial.Empty;
            Visuals = new ReadOnlyCollection<Visual>(visuals);
            Collisions = new ReadOnlyCollection<Collision>(collisions);
        }
    }
}