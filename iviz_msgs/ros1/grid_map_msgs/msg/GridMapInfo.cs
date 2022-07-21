/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class GridMapInfo : IDeserializableCommon<GridMapInfo>, IMessageCommon
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
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Resolution);
            b.Deserialize(out LengthX);
            b.Deserialize(out LengthY);
            b.Deserialize(out Pose);
        }
        
        public GridMapInfo(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Resolution);
            b.Deserialize(out LengthX);
            b.Deserialize(out LengthY);
            b.Deserialize(out Pose);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new GridMapInfo(ref b);
        
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
            b.Serialize(Resolution);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.Serialize(in Pose);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 80 + Header.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Resolution);
            WriteBuffer2.AddLength(ref c, LengthX);
            WriteBuffer2.AddLength(ref c, LengthY);
            WriteBuffer2.AddLength(ref c, Pose);
        }
    
        public const string MessageType = "grid_map_msgs/GridMapInfo";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "43ee5430e1c253682111cb6bedac0ef9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
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
    }
}
