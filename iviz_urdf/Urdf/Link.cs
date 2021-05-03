using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Link
    {
        [DataMember] public string? Name { get; }
        [DataMember] public Inertial Inertial { get; }
        [DataMember] public ReadOnlyCollection<Visual> Visuals { get; }
        [DataMember] public ReadOnlyCollection<Collision> Collisions { get; }

        internal Link(XmlNode node)
        {
            Name = node.Attributes?["name"]?.Value;

            List<Visual> visuals = new List<Visual>();
            List<Collision> collisions = new List<Collision>();

            Inertial? inertial = null;
            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "inertial":
                        inertial = new Inertial(child);
                        break;
                    case "visual":
                        visuals.Add(new Visual(child));
                        break;
                    case "collision":
                        collisions.Add(new Collision(child));
                        break;
                }
            }

            Inertial = inertial ?? Inertial.Empty;
            Visuals = new ReadOnlyCollection<Visual>(visuals);
            Collisions = new ReadOnlyCollection<Collision>(collisions);
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}