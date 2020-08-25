using System.Xml;

namespace Iviz.Urdf
{
    public sealed class Mesh
    {
        public string Filename { get; }
        public Vector3 Scale { get; }

        internal Mesh(XmlNode node)
        {
            Filename = Utils.ParseString(node.Attributes?["filename"]);
            Scale = Vector3.Parse(node.Attributes?["scale"], Vector3.One);
        }
    }
}