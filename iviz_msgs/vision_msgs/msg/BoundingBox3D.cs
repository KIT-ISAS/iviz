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
                "H4sIAAAAAAAACr1UwWrcQAy9G/wPgj00AbOFpuRQ6CGwtORQmtLSa9Hasncae2Y7M+5m8/V9Gsd2TBba" +
                "Q1mf5Bnp6T1JoxXd0NWGtq63lbENjAeKO45UsqWt0N4FE42zUhHbiryLHNVGQCQTA5Vio3i6uKbN5w+X" +
                "ebaijenEBsQEcjXATEio7IWMpU7gHoqExoFCX+4KAFHHR83XmcanDNEpFsHPxR0S7Lm850aKFKGRjThg" +
                "+eOPLjTAAzT8qO5j72WdZxr9DQcQN2pIOZ03YMzpP9GTpfhBT54t4F/fuSDT1QgdzKOcwiie6wy99+Od" +
                "OrrtTynjq7Fwg0gwVM7LnN/h5/xVSqNJ3//nL88+ff34jl4KVU435GXvJTyvlbJUabUXiEdD0I3SdXpc" +
                "Pd2frPOa8uzOGRsnjzz70qPL3ibk2fOMMkFn6CPGs3TIbzCv2qBJBRQx/pT3QnSe1a3jeP2WHmbzOJuP" +
                "Z1MxF3GSMnUtoAuLYV9o0L9fcwtq57v0Zv6qbDQPZxP59AxOKqTf6XKpTcdtRbfoGhaQbY94ioweRjeH" +
                "IrIyHrFpOvGWvaAEkhZR5SQQlo6CdHwPUGwz0XDe74HGFD3b0A5VxTFiLmTdrAs67ATDr1762jnxaMSK" +
                "NyV50xhsNQ3Vak/RTE8CC4r1GzqYth1YD9kwkYqS9i4iLtd0W9PR9XRQTTA8VRzByen2HJnxtlXGrqBe" +
                "qQ8YJ14AShMCtioKGKJw9W8z8Ae0S/rgNAYAAA==";
                
    }
}
