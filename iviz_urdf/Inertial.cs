using System.Xml;

namespace Iviz.Urdf
{
    public class Inertial
    {
        public static readonly Inertial Empty = new Inertial();

        public Mass Mass { get; }
        public Origin Origin { get; }
        public Inertia Inertia { get; }

        Inertial()
        {
            Mass = new Mass();
            Origin = Origin.Identity;
            Inertia = new Inertia();
        }

        internal Inertial(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "mass":
                        Mass = new Mass(child);
                        break;
                    case "origin":
                        Origin = new Origin(child);
                        break;
                    case "inertia":
                        Inertia = new Inertia(child);
                        break;
                }
            }

            if (Mass is null)
            {
                throw new MalformedUrdfException(node);
            }

            if (Origin is null)
            {
                Origin = Origin.Identity;
            }

            if (Inertia is null)
            {
                throw new MalformedUrdfException(node);
            }
        }
    }
}