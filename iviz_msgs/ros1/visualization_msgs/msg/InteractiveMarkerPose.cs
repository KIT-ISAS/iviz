/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerPose : IDeserializableRos1<InteractiveMarkerPose>, IDeserializableRos2<InteractiveMarkerPose>, IMessageRos1, IMessageRos2
    {
        // Time/frame info.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Initial pose. Also, defines the pivot point for rotations.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        [DataMember (Name = "name")] public string Name;
    
        /// Constructor for empty message.
        public InteractiveMarkerPose()
        {
            Name = "";
        }
        
        /// Explicit constructor.
        public InteractiveMarkerPose(in StdMsgs.Header Header, in GeometryMsgs.Pose Pose, string Name)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.Name = Name;
        }
        
        /// Constructor with buffer.
        public InteractiveMarkerPose(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            b.DeserializeString(out Name);
        }
        
        /// Constructor with buffer.
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarkerPose";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a6e6833209a196a38d798dadb02c81f8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UwU7cMBC9+ytG2gNQQZDaqgekHpBQWw5IVHBHs/EkGSmxg+0spF/fZ4csXfXQHlpW" +
                "0caJZ96892acDd3rIOdN4EFIXeMr803YSqCu3IzZ0LXTpNzT6KNUdNlHf0pWGnUSKXVCo+58wq66RI0P" +
                "FHzipN7FyrTiB0lhfhhiG89vAVBQCqoVl7SZ1bUUU8CtopspJtoKtb3fct/PNDl9nDIxJORSyY9aY8UJ" +
                "fxppkBi5RUCkCDi8DH5qu8osiOQgy5jP//hnbu6+XoC0XWQthoHhXWJnOVjQSmw5cbGj07aTcNbLTnok" +
                "8TCKpbKb5lFg0obusxZcrTgJi/CIoOSp9sMAE2pOEI9OHeQjUx0xjRyS1lPPAfE+WHU5vPQ0o+OKAhtd" +
                "LXR9dYEYF6WekoLQDIQ6CMfs1vUVmQld/PA+J5jN/ZM/w6O0GId98cV9kJXnMcB+kOF4gRrvFnEVsGGO" +
                "oIqNdFzePeAxnhCKgIKMvu7oGMxv59R5Vxq746C87UsnazgA1KOcdHTyC7Ir0I6dX+EXxNcafwPr9rhZ" +
                "01mHnvVlCKcWBiJwDH6nFqHbuYDUvebZ6nUbOMwmZy0lzeZLOTdlFEtHcOcYfa1ogKUnTd06iaUbD2r/" +
                "1zT+ftIg8JKC5CaBfjmR5Jty/vLYNEEgY+RaTvOU5df2ZV9LLHwhH3TNrcjcliO+BpjvE1QGV3Bf495K" +
                "IKisJwezkFjdy9do5Q8t/PJVOpBrmt5z+vSRnvereb/68Tb0X61bNewbhQk68POQfH56fPUd35ehMn9Q" +
                "tK6ejPkJ+E1BKe8FAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
