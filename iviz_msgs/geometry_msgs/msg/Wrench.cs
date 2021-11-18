/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Wrench")]
    public sealed class Wrench : IDeserializable<Wrench>, IMessage
    {
        // This represents force in free space, separated into
        // its linear and angular parts.
        [DataMember (Name = "force")] public Vector3 Force;
        [DataMember (Name = "torque")] public Vector3 Torque;
    
        /// <summary> Constructor for empty message. </summary>
        public Wrench()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Wrench(in Vector3 Force, in Vector3 Torque)
        {
            this.Force = Force;
            this.Torque = Torque;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Wrench(ref Buffer b)
        {
            b.Deserialize(out Force);
            b.Deserialize(out Torque);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Wrench(ref b);
        }
        
        Wrench IDeserializable<Wrench>.RosDeserialize(ref Buffer b)
        {
            return new Wrench(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Force);
            b.Serialize(Torque);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Wrench";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4f539cf138b23283b520fd271b567936";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1SwUrEMBC9B/oPD/aiUCKoeBA8yx4EQfEqs+00G0yTmkxd69c73S67LF4ttEyT9968" +
                "N8kKr1tfkHnIXDhKQZdyw/ARXWZGGajhGoUHyiTc6oYks4JXZPCRKYNiq68bg9aKkmLNGzeS8g0WsdOv" +
                "fj9HNpV5+OenMk8vj/dwnHqWPL33xZWrQ9vK/A1J+Npvnue0UOhaoNgUw4SeKYqaPjGV2PqsVJ+iVVXO" +
                "rBF1QF7QJi6ISVSjpw+V5Fh4ZtMwqBhBMsUSaObOy0q5YOtsjd2W44Ly0SlQFRxHzr5B9s63C1Mb9Ucy" +
                "4ZCuhnTX2PkQFs9LM9myiuQke8KlxbrDlEbs5kBaZLQk6ihhoxYPvmgTZr+pxjgb30ucT/Q56fHrWEoh" +
                "N9+RIkytNaYLieTuFt/HajpWP5X5BVTczFZlAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
