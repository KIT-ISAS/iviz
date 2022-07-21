/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerPose : IDeserializable<InteractiveMarkerPose>, IMessage
    {
        // Time/frame info.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Initial pose. Also, defines the pivot point for rotations.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        [DataMember (Name = "name")] public string Name;
    
        public InteractiveMarkerPose()
        {
            Name = "";
        }
        
        public InteractiveMarkerPose(in StdMsgs.Header Header, in GeometryMsgs.Pose Pose, string Name)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.Name = Name;
        }
        
        public InteractiveMarkerPose(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            b.DeserializeString(out Name);
        }
        
        public InteractiveMarkerPose(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            b.DeserializeString(out Name);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new InteractiveMarkerPose(ref b);
        
        public InteractiveMarkerPose RosDeserialize(ref ReadBuffer b) => new InteractiveMarkerPose(ref b);
        
        public InteractiveMarkerPose RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarkerPose(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Pose);
            b.Serialize(Name);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Pose);
            b.Serialize(Name);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 60 + Header.RosMessageLength + WriteBuffer.GetStringSize(Name);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Pose);
            WriteBuffer2.AddLength(ref c, Name);
        }
    
        public const string MessageType = "visualization_msgs/InteractiveMarkerPose";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a6e6833209a196a38d798dadb02c81f8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTWvcMBC961cM7CFJSRz6QQ+BHgKhbQ6BtMmtlGXWHtsDtuRI8ibur++THO926aE9" +
                "tFnMWpZn3rz3ZuQV3Wsv57XnXkht7QrzWbgST22+GbOia6tRuaPBBSnosgvulCqp1Uqg2AoNunURb9VG" +
                "qp0n7yJHdTYUphHXS/TTug9NOL8FQEbJqJXYqPWktqEQPW4F3Ywh0kao6dyGu26i0erDmIghIZWKbtAS" +
                "K47400C9hMANAgIFwGHTu7FpCzMjkoUsYz7845+5uft0AdLVLGs2DAzvItuKfQVakSuOnO1otWnFn3Wy" +
                "lQ5J3A9SUX4bp0Fg0orukxZcjVjxs/CAoOiodH0PE0qOEI9OHeQjUy0xDeyjlmPHHvHOV2pTeO5pQscV" +
                "BDbaUuj66gIxNkg5RgWhCQilFw7JresrMiO6+PZNSqAVffvqwuvvZnX/6M6wLw3mYsdibgNYy9Pg0Qew" +
                "4nCBYq9mlQWKwCVBuSrQcd5b4zGcEKqBiwyubOkYEm6n2DqbO7xlr7zpcktLWAHUo5R0dPILss3Qlq1b" +
                "4GfEfY2/gbU73KTprEXzujyNYwMnETh4t9UKoZspg5SdpiHrdOPZTyZlzSXN6mM+QHkmc2tw5xBcqehE" +
                "RY8a22Ukc1vWWv2vsfz9yEHgJXlJTQL9fDTJ1fkgpvmpvUDGwKWcpnFL29Xze82x8IWc1yW3IHObz/oS" +
                "YL6MUOltxt3HvZRAUFmOEGYhstrnz9LCH1r4+fN0INfUneP4/h097VbTbvXjZejvrVs07BqFCTrw85B8" +
                "enrY+44PTV+YPyhaVo/G/AQXDOMv+AUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
