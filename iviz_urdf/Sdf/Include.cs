using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Include
    {
        public Uri Uri { get; }
        public string? Name { get; }
        public bool? Static { get; }
        public Pose Pose { get; } = Pose.Identity;
        
        internal Include(XmlNode node)
        {
            Uri? uri = null;
            
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "uri":
                        uri = new Uri(child);
                        break;
                    case "name":
                        Name = child.InnerText;
                        break;
                    case "static":
                        Static = BoolElement.ValueOf(child);
                        break;
                    case "pose":
                        Pose = new Pose(child);
                        break;
                }
            }

            Uri = uri ?? throw new MalformedSdfException(node, "Expected uri!");
        }        
    }
}