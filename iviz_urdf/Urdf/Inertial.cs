using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Inertial
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
            Mass? mass = null;
            Origin? origin = null;
            Inertia? inertia = null; 
            
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "mass":
                        mass = new Mass(child);
                        break;
                    case "origin":
                        origin = new Origin(child);
                        break;
                    case "inertia":
                        inertia = new Inertia(child);
                        break;
                }
            }

            Mass = mass ?? throw new MalformedUrdfException(node);
            Origin = origin ?? Origin.Identity;
            Inertia = inertia ?? throw new MalformedUrdfException(node);
        }
    }
}