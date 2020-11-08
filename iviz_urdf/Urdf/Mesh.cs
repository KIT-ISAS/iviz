using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Mesh
    {
        public string Filename { get; }
        public Vector3f Scale { get; }

        internal Mesh(XmlNode node)
        {
            Filename = Utils.ParseString(node.Attributes?["filename"]);
            Scale = Vector3f.Parse(node.Attributes?["scale"], Vector3f.One);
        }
    }
}