using System.Runtime.Versioning;
using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Mesh
    {
        public Uri Uri { get; }
        public Vector3 Scale { get; } = Vector3.One; 

        internal Mesh(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "uri": 
                        Uri = new Uri(child);
                        break;
                    case "scale":
                        Scale = new Vector3(child);
                        break;
                }
            }
        }

        internal Mesh(Uri uri, Vector3 scale)
        {
            Uri = uri;
            Scale = scale;
        }
    }
}