/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract (Name = "vision_msgs/BoundingBox3D")]
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
                "H4sIAAAAAAAAE71UwWrcQAy9+ysEe2gCxoWm5FDoIbCk5FCa0tJr0dqydxp75M6Mu9l8fd+M196YLO0l" +
                "rE8aj/T0nqTRim7oak0bHWxlbAPjkcKWA5VsaSPUqzfBqJWK2FbkNHCINgICmeCpFBvE0cU1rb/cXmYr" +
                "WptOrEeIJ62BZXwCZSdkLHUCb58nMPbkh3KbA4c63sd0nWlcShAUUAQ3DVvA91w+cCN5CoiBjSig3P5n" +
                "5xvAARl+VA9hcFJkiP2OI4RN/FNCdQZsOZ0TN1kKH7VkC+y39+plujngevMkpwDy5wr94Nx0Fx1180vK" +
                "8GaqWJIHdmC7zPcDXuquUpIs+/jKX/b526cP9FIh6NyQk96Jf16iSDCKqp1ANrqAFpTaxd/V4f5keQvK" +
                "7tXYMDtkXwf01dmEe/Q7l0BQSb3DMJaK3AbTGZsy84cWxilSXsjN6lY5XL+nx9naz9bTeegfSzdpmBvl" +
                "UfjFWC/Ix9PvY91rdR3exr8VTdbuPNoO035KGP1Jd0tJGKwV3aFJ2C623eO1MVoW9BiJwMo4hKYxxHN1" +
                "AuGStkyl4gkrBRgdPwASm0piNPc9wJiCY+vbsZT4jZALKZoip91W7OgV3zNHFo1YcaYkZxpTjZGxwnMw" +
                "00FcTqF+RzvTtiPnMRnGDyBpoSLgsqC7mvY60C4KguGo4sARCHtx4sWbNvLVnIZIPEGcmHWUxXvsS9TO" +
                "B+Hqv13/CwflxGsHBgAA";
                
    }
}
