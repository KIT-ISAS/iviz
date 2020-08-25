using System.Xml;

namespace Iviz.Sdf
{
    public class Wind
    {
        public Vector3 LinearVelocity { get; } = Vector3.Zero;
        
        internal Wind(XmlNode node)
        {
            if (node.ChildNodes.Count != 0 && node.ChildNodes[0].Name == "pose")
            {
                LinearVelocity = new Vector3(node.ChildNodes[0]);
            }            
        }            
    }
}