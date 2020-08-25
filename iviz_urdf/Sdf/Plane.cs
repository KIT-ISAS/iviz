using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Plane
    {
        public Vector3 Normal { get; } = Vector3.Up;
        public Vector2 Size { get; } = Vector2.One;

        internal Plane(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "normal": 
                        Normal = new Vector3(child);
                        break;
                    case "size":
                        Size = new Vector2(child);
                        break;
                }
            }     
        }
    }
}