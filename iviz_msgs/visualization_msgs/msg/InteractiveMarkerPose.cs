/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/InteractiveMarkerPose")]
    public sealed class InteractiveMarkerPose : IDeserializable<InteractiveMarkerPose>, IMessage
    {
        // Time/frame info.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Initial pose. Also, defines the pivot point for rotations.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        [DataMember (Name = "name")] public string Name { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerPose()
        {
            Name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerPose(in StdMsgs.Header Header, in GeometryMsgs.Pose Pose, string Name)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.Name = Name;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public InteractiveMarkerPose(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Pose = new GeometryMsgs.Pose(ref b);
            Name = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerPose(ref b);
        }
        
        InteractiveMarkerPose IDeserializable<InteractiveMarkerPose>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
            b.Serialize(Name);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 60;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Name);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerPose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a6e6833209a196a38d798dadb02c81f8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
    }
}
