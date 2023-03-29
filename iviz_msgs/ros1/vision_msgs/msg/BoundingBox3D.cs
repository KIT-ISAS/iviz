/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class BoundingBox3D : IHasSerializer<BoundingBox3D>, IMessage
    {
        // A 3D bounding box that can be positioned and rotated about its center (6 DOF)
        // Dimensions of this box are in meters, and as such, it may be migrated to
        //   another package, such as geometry_msgs, in the future.
        // The 3D position and orientation of the bounding box center
        [DataMember (Name = "center")] public GeometryMsgs.Pose Center;
        // The size of the bounding box, in meters, surrounding the object's center
        //   pose.
        [DataMember (Name = "size")] public GeometryMsgs.Vector3 Size;
    
        public BoundingBox3D()
        {
        }
        
        public BoundingBox3D(in GeometryMsgs.Pose Center, in GeometryMsgs.Vector3 Size)
        {
            this.Center = Center;
            this.Size = Size;
        }
        
        public BoundingBox3D(ref ReadBuffer b)
        {
            b.Deserialize(out Center);
            b.Deserialize(out Size);
        }
        
        public BoundingBox3D(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Center);
            b.Deserialize(out Size);
        }
        
        public BoundingBox3D RosDeserialize(ref ReadBuffer b) => new BoundingBox3D(ref b);
        
        public BoundingBox3D RosDeserialize(ref ReadBuffer2 b) => new BoundingBox3D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Center);
            b.Serialize(in Size);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align8();
            b.Serialize(in Center);
            b.Serialize(in Size);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 80;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 80;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "vision_msgs/BoundingBox3D";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "727c83f2b037373b8e968433d9c84ecb";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<BoundingBox3D> CreateSerializer() => new Serializer();
        public Deserializer<BoundingBox3D> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<BoundingBox3D>
        {
            public override void RosSerialize(BoundingBox3D msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(BoundingBox3D msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(BoundingBox3D _) => RosFixedMessageLength;
            public override int Ros2MessageLength(BoundingBox3D _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<BoundingBox3D>
        {
            public override void RosDeserialize(ref ReadBuffer b, out BoundingBox3D msg) => msg = new BoundingBox3D(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out BoundingBox3D msg) => msg = new BoundingBox3D(ref b);
        }
    }
}
