using System.Xml;

namespace Iviz.Urdf
{
    public class Link
    {
        public string Name { get; }
        public Inertial Inertial { get; }
        public Visual Visual { get; }
        public Collision Collision { get; }

        internal Link(XmlNode node)
        {
            Name = Utils.ParseString(node.Attributes["name"]);

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "inertial":
                        Inertial = new Inertial(child);
                        break;
                    case "visual":
                        Visual = new Visual(child);
                        break;
                    case "collision":
                        Collision = new Collision(child);
                        break;
                }
            }

            if (Inertial == null)
            {
                Inertial = Inertial.Empty;
            }
        }
    }
}