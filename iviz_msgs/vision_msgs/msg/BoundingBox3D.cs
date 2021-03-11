/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/BoundingBox3D")]
    public sealed class BoundingBox3D : IDeserializable<BoundingBox3D>, IMessage
    {
        // A 3D bounding box that can be positioned and rotated about its center (6 DOF)
        // Dimensions of this box are in meters, and as such, it may be migrated to
        //   another package, such as geometry_msgs, in the future.
        // The 3D position and orientation of the bounding box center
        [DataMember (Name = "center")] public GeometryMsgs.Pose Center { get; set; }
        // The size of the bounding box, in meters, surrounding the object's center
        //   pose.
        [DataMember (Name = "size")] public GeometryMsgs.Vector3 Size { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public BoundingBox3D()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public BoundingBox3D(in GeometryMsgs.Pose Center, in GeometryMsgs.Vector3 Size)
        {
            this.Center = Center;
            this.Size = Size;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public BoundingBox3D(ref Buffer b)
        {
            Center = new GeometryMsgs.Pose(ref b);
            Size = new GeometryMsgs.Vector3(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new BoundingBox3D(ref b);
        }
        
        BoundingBox3D IDeserializable<BoundingBox3D>.RosDeserialize(ref Buffer b)
        {
            return new BoundingBox3D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Center.RosSerialize(ref b);
            Size.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 80;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/BoundingBox3D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "727c83f2b037373b8e968433d9c84ecb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwWrbQBC9C/wPAzk0AeFCU3Io9BAwLTmUpjT0WsbSSN5G2nF3V3Wcr++btS1H1LSX" +
                "Yp1mtTNv3puZnQu6pesFLXXwtfMtjCdKK05Usael0FqjS0691MS+pqCJk9kISORSpEp8kkCXN7T4/OGq" +
                "uKCF68VHhETSBlguZlAOQs5TL/COZQbjSHGoViVwqOetpetdG3KCpIAiuGlaAX7N1SO3UuYAC2xFARW2" +
                "3/vYAg7I8KNmSEOQeYHYBxwh7MA/J9TgwJbzOXOTqfCdlmKC/fpeoxxu9rjRPcspgPKlwjiEcLgzR13+" +
                "kCq9OlQsywM7sJ3m+wYvDdc5STEr3v/nb1Z8+vrxHf2pcQZGtxRkHSS+rJJxNF1NEChHI9CFSnv7Xe/v" +
                "T1Z4TsW9Op9Gh+LLgNYGn3GPfufTCDIm8sFGslKkd5hRa80oAXIYJ2M9UVw0nXK6eUtPo7UdredzKTjW" +
                "b5Qxtiui/JP5nvC3089j9RsNPR7J30UdrM255O0H/6Q2+pUvp6owYRd0h1Zh0/hui5fHaFzSYyQCaxcQ" +
                "mucRTzcItEveOLVKJKwXYPT8CEhsLbFoXq8BxpQC+9jtqonfCLmUeTsvabMSTLt52dtmY9GKl+AqCq51" +
                "WF4WaUUeg5n26kpKzRvauK7bcd4lwxACJC9XBFzN6a6hrQ60MUEwAtWcwEhtRx548bIzvlrSYMQzxImR" +
                "R1lixO5E7WISrv/V+FnxG4EVYiwUBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
