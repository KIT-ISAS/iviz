/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class GridMapInfo : IHasSerializer<GridMapInfo>, IMessage
    {
        // Header (time and frame)
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Resolution of the grid [m/cell].
        [DataMember (Name = "resolution")] public double Resolution;
        // Length in x-direction [m].
        [DataMember (Name = "length_x")] public double LengthX;
        // Length in y-direction [m].
        [DataMember (Name = "length_y")] public double LengthY;
        // Pose of the grid map center in the frame defined in `header` [m].
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
    
        public GridMapInfo()
        {
        }
        
        public GridMapInfo(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Resolution);
            b.Deserialize(out LengthX);
            b.Deserialize(out LengthY);
            b.Deserialize(out Pose);
        }
        
        public GridMapInfo(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align8();
            b.Deserialize(out Resolution);
            b.Deserialize(out LengthX);
            b.Deserialize(out LengthY);
            b.Deserialize(out Pose);
        }
        
        public GridMapInfo RosDeserialize(ref ReadBuffer b) => new GridMapInfo(ref b);
        
        public GridMapInfo RosDeserialize(ref ReadBuffer2 b) => new GridMapInfo(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Resolution);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.Serialize(in Pose);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align8();
            b.Serialize(Resolution);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.Serialize(in Pose);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 80;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 8; // Resolution
            size += 8; // LengthX
            size += 8; // LengthY
            size += 56; // Pose
            return size;
        }
    
        public const string MessageType = "grid_map_msgs/GridMapInfo";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "43ee5430e1c253682111cb6bedac0ef9";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71Uy27bMBC88ysW8CFJUTvoAzkE6KFA0QfQAmmSWxA4G3ItEaBIhaQSq1/fIR3LMXpI" +
                "D20MwZLI2eHMPjSjr8JGIh1m2wmxN7SK3MmRelxv602pGZ1LCm7INngKK8qtUBOtoavuWItz1wu1coHz" +
                "yXuKE7CEfRff5Jasp/Xc2Ci6Mlx1TwJchSzX+/DxGfhY4GchyZ6cjnvS4jOkg6MsVz9kZGW9mLJ4s/F0" +
                "s2FtJHSS47jsUpOOK1+PP/XhH//Uj4svp5Sy2Ry0SS8cXGQknSOUS2bDmWkVkHbbtBLnTu7FIYi7Htrr" +
                "bh57SQsEXrY2Ea5GvER2bqQhAZQD6dB1g7eas1Ap6148IpEDpp5jtnpwHIEP0Vhf4DVZhR1XkrtBvBb6" +
                "9ukUGJ9Eo6oQNIJBR+FkfYNNUoP1+d3bEkAzujoP6c21ml0+hDnWpUEpJhWoCOeiWtY9+qQI5nSKw15t" +
                "XC5wCLIkOM4kOqxrS7ymI8Jp0CJ90C0dwsLZmNuwKfE9R8u3TgqxRirAelCCDo6eMPtK7dmHLf2GcXfG" +
                "39D6ibd4mrconitpSEODTALYx3BvDaC3YyXRzqIdydnbyHFUdczqkWr2uXZmLnWspcGdUwraohKGHmxu" +
                "VcqxsNeyLK1R/6kt/xwCGPyISS5Fgnzejn0ZjdI/qyiw0bOW16XdyrJ53LcVW74kIdpt7ILUWUA3TAD1" +
                "c4DL6CvvDvdSBiFlO0LohczWp1qtST+8YEaq5D2701doPT2N09Ovl5G/S93Ww1QodNBePvfFl7e7Xd7x" +
                "oekW6hlH26cHpX4DyQZzYi0GAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<GridMapInfo> CreateSerializer() => new Serializer();
        public Deserializer<GridMapInfo> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GridMapInfo>
        {
            public override void RosSerialize(GridMapInfo msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(GridMapInfo msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(GridMapInfo msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(GridMapInfo msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(GridMapInfo msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<GridMapInfo>
        {
            public override void RosDeserialize(ref ReadBuffer b, out GridMapInfo msg) => msg = new GridMapInfo(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out GridMapInfo msg) => msg = new GridMapInfo(ref b);
        }
    }
}
