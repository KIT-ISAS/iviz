/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/OrientedBoundingBox")]
    public sealed class OrientedBoundingBox : IDeserializable<OrientedBoundingBox>, IMessage
    {
        // the pose of the box
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
        // the extents of the box, assuming the center is at the origin
        [DataMember (Name = "extents")] public GeometryMsgs.Point32 Extents { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public OrientedBoundingBox()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public OrientedBoundingBox(in GeometryMsgs.Pose Pose, in GeometryMsgs.Point32 Extents)
        {
            this.Pose = Pose;
            this.Extents = Extents;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public OrientedBoundingBox(ref Buffer b)
        {
            Pose = new GeometryMsgs.Pose(ref b);
            Extents = new GeometryMsgs.Point32(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new OrientedBoundingBox(ref b);
        }
        
        OrientedBoundingBox IDeserializable<OrientedBoundingBox>.RosDeserialize(ref Buffer b)
        {
            return new OrientedBoundingBox(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Pose.RosSerialize(ref b);
            Extents.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 68;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/OrientedBoundingBox";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "da3bd98e7cb14efa4141367a9d886ee7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71US0vDQBC+L/Q/DPSiIBFa8SB4EA/iQVD0Lttkmg4mO3FnYx+/3tnNo5YeBJHmNNl5" +
                "7Pd9M7NTCCuEhgWBl8le8GZiSuQag9++11LK5XN0x5iJmZhpisJNQBfkR9IFWJG2Jlemk1zd6IEEbEgH" +
                "7Kkkd1yaXJjPhnoTY27/+TNPrw83cExIidyBx8aj6M02ELvIJklBDpYeEaSxOV5AznU8Lno/pVjrishp" +
                "yM3AJC5jgHlprSrgUt193KkIKhRl+LbSBuSsd5OTodU0cLX6FyEf0DXLim24voLNaG1Ha3ca+HvpBg5j" +
                "o3SeDvQ8BB//Pve6L9nXmfmF0WCtT9ia+exvzTlbU1iB7suCuuVTVXISTTnPtOJjgKSVDmyNhQ5sYGh1" +
                "nrvJXK/Q45cupV4jtKiiWhLQprnuYWUAWmeveqrkil5qiQUbzzWHmKwyc4PeLqiisE2pQ2aNIrbEmFKg" +
                "UOk6MMF+ILQNVOruO6aoHGhnC305NLvinljEkx4PdnEHrUQl0stiu8fKdpjvK26LeHfXSNVmM1rb0dqZ" +
                "bzYQi7bqBAAA";
                
    }
}
