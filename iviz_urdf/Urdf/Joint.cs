using System.Runtime.Serialization;
using System.Xml;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Urdf
{
    /// The joint element describes the kinematics and dynamics of the joint and also specifies the safety limits of the joint.
    [DataContract]
    public sealed class Joint
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum JointType
        {
            /// A hinge joint that rotates along the axis and has a limited range specified by the upper and lower limit
            Revolute,

            /// A continuous hinge joint that rotates around the axis and has no upper and lower limits
            Continuous,

            /// A sliding joint that slides along the axis, and has a limited range specified by the upper and lower limits
            Prismatic,

            ///  This is not really a joint because it cannot move. All degrees of freedom are locked. This type of joint does not require the axis, calibration, dynamics, limits or safety_controller
            Fixed,

            /// This joint allows motion for all 6 degrees of freedom
            Floating,

            /// This joint allows motion in a plane perpendicular to the axis
            Planar
        }

        /// Specifies a unique name of the joint.
        [DataMember]
        public string Name { get; }

        /// Specifies the type of joint.
        [DataMember]
        public JointType Type { get; }

        /// This is the transform from the parent link to the child link. The joint is located at the origin of the child link.
        [DataMember]
        public Origin Origin { get; }

        /// Parent link name.
        [DataMember]
        public Parent Parent { get; }

        /// Child link name.
        [DataMember]
        public Child Child { get; }

        /// The joint axis specified in the joint frame. This is the axis of rotation for revolute joints,
        /// the axis of translation for prismatic joints, and the surface normal for planar joints.
        /// The axis is specified in the joint frame of reference. Fixed and floating joints do not use the axis field.
        [DataMember]
        public Axis Axis { get; }

        [DataMember] 
        public Limit Limit { get; }

        internal Joint(XmlNode node)
        {
            Name = Utils.ParseString(node.Attributes?["name"]);
            string typeStr = Utils.ParseString(node.Attributes?["type"]);
            Type = GetJointType(typeStr, node);

            Origin? origin = null;
            Parent? parent = null;
            Child? child = null;
            Axis? axis = null;
            Limit? limit = null;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "origin":
                        origin = new Origin(childNode);
                        break;
                    case "parent":
                        parent = new Parent(childNode);
                        break;
                    case "child":
                        child = new Child(childNode);
                        break;
                    case "axis":
                        axis = new Axis(childNode);
                        break;
                    case "limit":
                        limit = new Limit(childNode);
                        break;
                }
            }

            Origin = origin ?? Origin.Identity;
            Parent = parent ?? throw new MalformedUrdfException(node);
            Child = child ?? throw new MalformedUrdfException(node);
            Axis = axis ?? Axis.Right;
            Limit = limit ?? Limit.Empty;
        }

        /// <summary>
        /// Gets the transform of this joint's origin when the joint has the given value. 
        /// </summary>
        public Transform GetOriginTransform(double jointValue)
        {
            var originTransform = Origin.AsTransform();
            if (jointValue == 0 || Type == JointType.Fixed)
            {
                return originTransform;
            }

            bool isIdentityRotation = Origin.Rpy is (0, 0, 0);
            switch (Type)
            {
                case JointType.Continuous or JointType.Revolute:
                    var jointRotation = Quaternion.AngleAxis(jointValue, Axis.Xyz);
                    if (isIdentityRotation)
                    {
                        originTransform.Rotation = jointRotation;
                    }
                    else
                    {
                        originTransform.Rotation *= jointRotation;
                    }

                    return originTransform;
                case JointType.Prismatic:
                    var jointTranslation = (Vector3)Axis.Xyz * jointValue;
                    if (isIdentityRotation)
                    {
                        originTransform.Translation = jointTranslation;
                    }
                    else
                    {
                        originTransform.Translation += originTransform.Rotation * jointTranslation;
                    }

                    return originTransform;
                default:
                    return originTransform;
            }
        }

        public Transform GetDefaultOriginTransform()
        {
            return Type is JointType.Continuous or JointType.Fixed || Limit.Lower <= 0 && 0 <= Limit.Upper
                ? Origin.AsTransform()
                : GetOriginTransform(Limit.Lower);
        }

        static JointType GetJointType(string type, XmlNode node)
        {
            return type switch
            {
                "revolute" => JointType.Revolute,
                "continuous" => JointType.Continuous,
                "prismatic" => JointType.Prismatic,
                "fixed" => JointType.Fixed,
                "floating" => JointType.Floating,
                "planar" => JointType.Planar,
                _ => throw new MalformedUrdfException(node)
            };
        }

        public override string ToString() => BuiltIns.ToJsonString(this);
    }
}