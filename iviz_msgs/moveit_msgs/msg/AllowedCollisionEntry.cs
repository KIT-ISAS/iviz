/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class AllowedCollisionEntry : IDeserializable<AllowedCollisionEntry>, IMessage
    {
        // whether or not collision checking is enabled
        [DataMember (Name = "enabled")] public bool[] Enabled;
    
        /// Constructor for empty message.
        public AllowedCollisionEntry()
        {
            Enabled = System.Array.Empty<bool>();
        }
        
        /// Explicit constructor.
        public AllowedCollisionEntry(bool[] Enabled)
        {
            this.Enabled = Enabled;
        }
        
        /// Constructor with buffer.
        public AllowedCollisionEntry(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Enabled);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new AllowedCollisionEntry(ref b);
        
        public AllowedCollisionEntry RosDeserialize(ref ReadBuffer b) => new AllowedCollisionEntry(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Enabled);
        }
        
        public void RosValidate()
        {
            if (Enabled is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Enabled.Length;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/AllowedCollisionEntry";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "90d1ae1850840724bb043562fe3285fc";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NWKM9ILclILVLIL1LIyy9RSM7PyckszszPU0jOSE3OzsxLV8gsVkjNS0zKSU3hSsrP" +
                "z4mOhXO5AEjNYe0/AAAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
