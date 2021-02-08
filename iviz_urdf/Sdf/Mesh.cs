using System.Runtime.Versioning;
using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Mesh
    {
        public Uri Uri { get; }
        public Vector3d Scale { get; } = Vector3d.One; 

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
                        Scale = new Vector3d(child);
                        break;
                }
            }

            Uri = uri ?? throw new MalformedSdfException(node, "Expected mesh!");
        }

        internal Mesh(Uri uri, Vector3d scale)
        {
            Uri = uri;
            Scale = scale;
        }
    }
}