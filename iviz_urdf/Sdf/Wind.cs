using System.Xml;

namespace Iviz.Sdf
{
    public class Wind
    {
        public Vector3d LinearVelocity { get; } = Vector3d.Zero;
        
        internal Wind(XmlNode node)
        {
            if (node.ChildNodes.Count != 0 && node.ChildNodes[0].Name == "pose")
            {
                LinearVelocity = new Vector3d(node.ChildNodes[0]);
            }            
        }            
    }
}