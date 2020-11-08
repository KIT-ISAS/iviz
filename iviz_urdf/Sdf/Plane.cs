using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Plane
    {
        public Vector3f Normal { get; } = Vector3f.Up;
        public Vector2f Size { get; } = Vector2f.One;

        internal Plane(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "normal": 
                        Normal = new Vector3f(child);
                        break;
                    case "size":
                        Size = new Vector2f(child);
                        break;
                }
            }     
        }
    }
}