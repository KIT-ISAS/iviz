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
            Header = new StdMsgs.Header();
            Name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerPose(StdMsgs.Header Header, in GeometryMsgs.Pose Pose, string Name)
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
            return new InteractiveMarkerPose(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
            b.Serialize(Name);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACr1UwW7bMAy9G/A/EMih7dCkwDbsEGCHAsW2HAp0aO8FY9E2AVtyJTmt9/V7kptkwS49" +
                "bA2SSJbIRz4+0gt60F6uas+9kNrarcrih7ART21eyqIsFrSxGpU7GlyQFV13wV2SkVqtBIqt0KA7F3Gr" +
                "NlLtPHkXOaqzAXCNuF6inx770ISrOyBkmFdgIzZqPaltKESPZUW3Y4i0FWo6t+Wum2i0+jSm9JJHChfd" +
                "oBV2HPGngXoJgRtYBArAw6F3Y9Mi+IxJFvRSwK//+FMWt/ff18jczOzm0qU07yNbw94gt8iGI+e6tNq0" +
                "4ped7KSDF/eDGMq3cRokVWtBD4kRvo1Y8TP/AKvoqHJ9j1pUHFECyHYCkFzVEtPAPmo1duzh4LxRm+yz" +
                "whk//YKgoLYS2tysYWWDVGNUJDUBo/LCIVVtcwPjEZp++pg84Pjw7JZ4lgb9cchgFgIZy8vgoQQy4rBO" +
                "YT7MHFeAR5EEgUyg83z2iMdwQYiDLGRwVUvnSP9uiq2zWeQde+Vtl1WtUAfAniWns4s/oVPqa+hr3R5/" +
                "hjwGeQuuPQInWssW4nW5J8cGdYTl4N1ODWy3U0apOk2d1unWs5/KIrnNQQHyLY9Tbs2sDVYOwVUKJQw9" +
                "a2wPnZl1eVTzH7vz7wFMPK/JS5ILLPKokqvzXKYmqr2AzMCVXKamS8fm9V6zLcpDzuved4U+ucvDv7co" +
                "i58jyHqbkY+W70gT6RzGCZ0RWe3ry2rPAowwLjnvE9JlUXeO45fP9HLcQuP99te7sTgW8UDloBp66qS0" +
                "pxzS09NRArx8ekz/G5jtt8/J+jcDKTslHwYAAA==";
                
    }
}
