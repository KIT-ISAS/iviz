using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;

namespace Iviz.Urdf
{
    /// The link element describes a rigid body with an inertia, visual features, and collision properties. 
    [DataContract]
    public sealed class Link
    {
        /// The name of the link itself
        [DataMember]
        public string? Name { get; }

        /// The inertial properties of the link
        [DataMember]
        public Inertial Inertial { get; }

        /// The visual properties of the link. This element specifies the shape of the object (box, cylinder, etc.) for visualization purposes
        [DataMember]
        public ReadOnlyCollection<Visual> Visuals { get; }

        /// The collision properties of a link
        [DataMember]
        public ReadOnlyCollection<Collision> Collisions { get; }

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

        public void Deconstruct(out string? name, out Inertial inertial, out ReadOnlyCollection<Visual> visuals,
            out ReadOnlyCollection<Collision> collisions) =>
            (name, inertial, visuals, collisions) = (Name, Inertial, Visuals, Collisions);

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}