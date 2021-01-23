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
                "H4sIAAAAAAAAE71UwWrbQBC9C/wPAz40AaFAU3Io9BAwDTmUprT0WsbSSN5G2nV3V3Wcr++blSVHxJBL" +
                "sU4r7bw3782MZkm3dL2itettZWyDwxPFDUcq2dJaaOuCicZZqYhtRd5FjnoGIJKJgUqxUTxd3NDq6+fL" +
                "RbaklenEBmACuRpkJiRW9kLGUicID3li40ChLzc5iKjjvebrTONThuiUixDn4gYJtlw+ciN5QiiyEQcu" +
                "v//VhQZ8oEYc1X3svRSLTNE/8AHmRg8pp/MGijm9J3kyNz/4WWQz+qsHF2S6GqmDeZZTHPlLn6H3frzT" +
                "QLf+LWV8FyY2NQmFqnme8yfinL9OaRZZ9uk/P9mX73cf6bVNCLolL1sv4WWhVKL6qr3AObqBVpSu08/V" +
                "4f5kkQvKHpyxcQrIvvVosLeJ9xh3LoOQktqHqSwdchuMqfZl0g8vjDeVPLOb1a3jePOBnqbTfjo9n0f+" +
                "sXSjh6lRAYWfDfdMvL79Oda9dr4rsjccjafdebwdxv2UMfqb7uaWMFhLukeTsGZsu8cPx2hZdEckgJXx" +
                "gKYxxB/rBcYlrZvKSSCsFnB0/AhKrCxRNG+3IGOKnm1oh1LiMyAXUjRFTruN2CFKf2lWFY1Y8aYkbxpT" +
                "DUit8ARmOpjLKdbvaWfadtA8JMP4gSStVgAuC7qvae962qkhHDxVHFmJsCBHXbxuVa/LqVfhieLErKMs" +
                "IWBvonYhCldvdv0fKB+06xEGAAA=";
                
    }
}
