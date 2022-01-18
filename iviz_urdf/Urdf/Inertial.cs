using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;
using Newtonsoft.Json;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Inertial
    {
        public static readonly Inertial Empty = new();

        /// The mass of the link is represented by the value attribute of this element
        [DataMember]
        public Mass Mass { get; }

        /// This is the pose of the inertial reference frame, relative to the link reference frame.
        /// The origin of the inertial reference frame needs to be at the center of gravity.
        /// The axes of the inertial reference frame do not need to be aligned with the principal axes of the inertia
        [DataMember]
        public Origin Origin { get; }

        /// The 3x3 rotational inertia matrix, represented in the inertia frame
        [DataMember]
        public Inertia Inertia { get; }

        Inertial()
        {
            Mass = new Mass();
            Origin = Origin.Identity;
            Inertia = new Inertia();
        }

        internal Inertial(XmlNode node)
        {
            Mass? mass = null;
            Origin? origin = null;
            Inertia? inertia = null;

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "mass":
                        mass = new Mass(child);
                        break;
                    case "origin":
                        origin = new Origin(child);
                        break;
                    case "inertia":
                        inertia = new Inertia(child);
                        break;
                }
            }

            Mass = mass ?? throw new MalformedUrdfException(node);
            Origin = origin ?? Origin.Identity;
            Inertia = inertia ?? throw new MalformedUrdfException(node);
        }

        public void Deconstruct(out Mass mass, out Origin origin, out Inertia inertia) =>
            (mass, origin, inertia) = (Mass, Origin, Inertia);

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}