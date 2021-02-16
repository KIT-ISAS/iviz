/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/AllowedCollisionEntry")]
    public sealed class AllowedCollisionEntry : IDeserializable<AllowedCollisionEntry>, IMessage
    {
        // whether or not collision checking is enabled
        [DataMember (Name = "enabled")] public bool[] Enabled { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AllowedCollisionEntry()
        {
            Enabled = System.Array.Empty<bool>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AllowedCollisionEntry(bool[] Enabled)
        {
            this.Enabled = Enabled;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AllowedCollisionEntry(ref Buffer b)
        {
            Enabled = b.DeserializeStructArray<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AllowedCollisionEntry(ref b);
        }
        
        AllowedCollisionEntry IDeserializable<AllowedCollisionEntry>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Enabled, 0);
        }
        
        public void RosValidate()
        {
            if (Enabled is null) throw new System.NullReferenceException(nameof(Enabled));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 1 * Enabled.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/AllowedCollisionEntry";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "90d1ae1850840724bb043562fe3285fc";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NWKM9ILclILVLIL1LIyy9RSM7PyckszszPU0jOSE3OzsxLV8gsVkjNS0zKSU3hSsrP" +
                "z4mOhXO5AEjNYe0/AAAA";
                
    }
}
