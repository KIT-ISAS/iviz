/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        internal AllowedCollisionEntry(ref Buffer b)
        {
            Enabled = b.DeserializeStructArray<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AllowedCollisionEntry(ref b);
        
        AllowedCollisionEntry IDeserializable<AllowedCollisionEntry>.RosDeserialize(ref Buffer b) => new AllowedCollisionEntry(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Enabled);
        }
        
        public void RosValidate()
        {
            if (Enabled is null) throw new System.NullReferenceException(nameof(Enabled));
        }
    
        public int RosMessageLength => 4 + Enabled.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/AllowedCollisionEntry";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "90d1ae1850840724bb043562fe3285fc";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClNWKM9ILclILVLIL1LIyy9RSM7PyckszszPU0jOSE3OzsxLV8gsVkjNS0zKSU3hSsrP" +
                "z4mOhXO5AEjNYe0/AAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
