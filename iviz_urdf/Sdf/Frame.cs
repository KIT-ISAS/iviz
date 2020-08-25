using System.Xml;

namespace Iviz.Sdf
{
    public sealed class Frame
    {
        public string Name { get; }
        public string AttachedTo { get; }
        public Pose Pose { get; } = Pose.Identity;
        
        internal Frame(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;
            AttachedTo = node.Attributes?["attached_to"]?.Value;

            if (node.ChildNodes.Count != 0 && node.ChildNodes[0].Name == "pose")
            {
                Pose = new Pose(node.ChildNodes[0]);
            }            
        }        
    }
}