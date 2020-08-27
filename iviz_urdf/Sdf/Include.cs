using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Include
    {
        public Uri Uri { get; }
        public string Name { get; }
        public bool Static { get; }
        public Pose Pose { get; } = Pose.Identity;
        
        internal Include(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "uri":
                        Uri = new Uri(child);
                        break;
                    case "name":
                        Name = child.InnerText;
                        break;
                    case "static":
                        Static = BoolElement.ValueOf(child);
                        break;
                    case "Pose":
                        Pose = new Pose(child);
                        break;
                }
            }            
        }        
    }
}