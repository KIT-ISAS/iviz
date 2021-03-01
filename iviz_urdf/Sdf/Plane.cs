using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Plane
    {
        public Vector3d Normal { get; } = Vector3d.Up;
        public Vector2d Size { get; } = Vector2d.One;

        internal Plane(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "normal": 
                        Normal = new Vector3d(child);
                        break;
                    case "size":
                        Size = new Vector2d(child);
                        break;
                }
            }     
        }
    }
}