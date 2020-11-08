using System.Runtime.Versioning;
using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Mesh
    {
        public Uri Uri { get; }
        public Vector3f Scale { get; } = Vector3f.One; 

        internal Mesh(XmlNode node)
        {
            Uri? uri = null;
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "uri": 
                        uri = new Uri(child);
                        break;
                    case "scale":
                        Scale = new Vector3f(child);
                        break;
                }
            }

            Uri = uri ?? throw new MalformedSdfException(node, "Expected mesh!");
        }

        internal Mesh(Uri uri, Vector3f scale)
        {
            Uri = uri;
            Scale = scale;
        }
    }
}