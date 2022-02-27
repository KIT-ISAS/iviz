using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Material
    {
        [DataMember] public string Name { get; }
        [DataMember] public Color? Color { get; }
        [DataMember] public Texture? Texture { get; }

        internal Material(XmlNode node)
        {
            Name = Utils.ParseString(node.Attributes?["name"], "");
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "color":
                        Color = new Color(child);
                        break;
                    case "texture":
                        Texture = new Texture(child);
                        break;
                }
            }
        }
        
        public void Deconstruct(out string? name, out Color? color, out Texture? texture) =>
            (name, color, texture) = (Name, Color, Texture);         
        
        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}