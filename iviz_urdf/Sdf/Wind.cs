using System.Xml;

namespace Iviz.Sdf
{
    public class Wind
    {
        public Vector3f LinearVelocity { get; } = Vector3f.Zero;
        
        internal Wind(XmlNode node)
        {
            if (node.ChildNodes.Count != 0 && node.ChildNodes[0].Name == "pose")
            {
                LinearVelocity = new Vector3f(node.ChildNodes[0]);
            }            
        }            
    }
}