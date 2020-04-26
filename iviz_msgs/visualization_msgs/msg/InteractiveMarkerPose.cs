using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.visualization_msgs
{
    public sealed class InteractiveMarkerPose : IMessage
    {
        // Time/frame info.
        public std_msgs.Header header;
        
        // Initial pose. Also, defines the pivot point for rotations.
        public geometry_msgs.Pose pose;
        
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        public string name;
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerPose()
        {
            header = new std_msgs.Header();
            name = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            pose.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out name, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            pose.Serialize(ref ptr, end);
            BuiltIns.Serialize(name, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 60;
                size += header.RosMessageLength;
                size += Encoding.UTF8.GetByteCount(name);
                return size;
            }
        }
    
        public IMessage Create() => new InteractiveMarkerPose();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "visualization_msgs/InteractiveMarkerPose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "a6e6833209a196a38d798dadb02c81f8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
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
