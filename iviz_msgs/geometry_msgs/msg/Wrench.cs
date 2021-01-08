/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Wrench")]
    public sealed class Wrench : IDeserializable<Wrench>, IMessage
    {
        // This represents force in free space, separated into
        // its linear and angular parts.
        [DataMember (Name = "force")] public Vector3 Force { get; set; }
        [DataMember (Name = "torque")] public Vector3 Torque { get; set; }
    
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
        public Wrench(ref Buffer b)
        {
            Force = new Vector3(ref b);
            Torque = new Vector3(ref b);
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
            Force.RosSerialize(ref b);
            Torque.RosSerialize(ref b);
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
                "H4sIAAAAAAAACq1SwUrEMBC9F/oPD/aiUCqoeBA8yx4EQfEqs800G0yTmkxd69c72S6u69lCyyR57817" +
                "06zwvHUZicfEmYNk9DF1DBfQJ2bkkTpukHmkRMJGDyTW1QpOod4FpgQKRl87ea0VJrmtqxfuJKYrLHK/" +
                "1vp9n3Sjru7++amrh6f7W1iOA0uaX4ds88Whb3H8NyjhY394mrVFwa4FCo7BzxiYgqjtI1WZxiXluhha" +
                "leXEmlKn5AQmckaIUkQGelNRDpkLncZR1QiSKGRPhVy2lXPGrW0b7LYcFpQLVoFFwnLg5DokZ51ZqNpq" +
                "+GETDgEbSH+JnfN+cb10k61OeoUUZc84b7HuMccJu5JJiwRDop4iNmry4Iw2vjiODaZifdE4Hetj1Hug" +
                "o8mZbLksWZiM/va66n0kubnG57Gcj+VXXX0DuHC833ICAAA=";
                
    }
}
