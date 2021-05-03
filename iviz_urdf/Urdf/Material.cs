using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

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
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}