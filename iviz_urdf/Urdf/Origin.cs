using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;

namespace Iviz.Urdf
{
    [DataContract]
    public sealed class Origin
    {
        public static readonly Origin Identity = new Origin();

        /// Represents the fixed axis roll, pitch and yaw angles in radians
        [DataMember]
        public Vector3f Rpy { get; }

        /// Represents the Cartesian coordinates of the translation
        [DataMember]
        public Vector3f Xyz { get; }

        Origin()
        {
            Rpy = Vector3f.Zero;
            Xyz = Vector3f.Zero;
        }

        internal Origin(XmlNode node)
        {
            Rpy = Vector3f.Parse(node.Attributes?["rpy"], Vector3f.Zero);
            Xyz = Vector3f.Parse(node.Attributes?["xyz"], Vector3f.Zero);
        }

        public void Deconstruct(out Vector3f rpy, out Vector3f xyz) => (rpy, xyz) = (Rpy, Xyz);

        public override string ToString() => BuiltIns.ToJsonString(this);

        public Transform AsTransform() => new(Xyz, ((Vector3)Rpy).ToRpyRotation());
    }
}