using System.Runtime.Serialization;

namespace Iviz.Msgs.visualization_msgs
{
    [DataContract]
    public sealed class InteractiveMarkerFeedback : IMessage
    {
        // Time/frame info.
        [DataMember] public std_msgs.Header header { get; set; }
        
        // Identifying string. Must be unique in the topic namespace.
        [DataMember] public string client_id { get; set; }
        
        // Feedback message sent back from the GUI, e.g.
        // when the status of an interactive marker was modified by the user.
        
        // Specifies which interactive marker and control this message refers to
        [DataMember] public string marker_name { get; set; }
        [DataMember] public string control_name { get; set; }
        
        // Type of the event
        // KEEP_ALIVE: sent while dragging to keep up control of the marker
        // MENU_SELECT: a menu entry has been selected
        // BUTTON_CLICK: a button control has been clicked
        // POSE_UPDATE: the pose has been changed using one of the controls
        public const byte KEEP_ALIVE = 0;
        public const byte POSE_UPDATE = 1;
        public const byte MENU_SELECT = 2;
        public const byte BUTTON_CLICK = 3;
        
        public const byte MOUSE_DOWN = 4;
        public const byte MOUSE_UP = 5;
        
        [DataMember] public byte event_type { get; set; }
        
        // Current pose of the marker
        // Note: Has to be valid for all feedback types.
        [DataMember] public geometry_msgs.Pose pose { get; set; }
        
        // Contains the ID of the selected menu entry
        // Only valid for MENU_SELECT events.
        [DataMember] public uint menu_entry_id { get; set; }
        
        // If event_type is BUTTON_CLICK, MOUSE_DOWN, or MOUSE_UP, mouse_point
        // may contain the 3 dimensional position of the event on the
        // control.  If it does, mouse_point_valid will be true.  mouse_point
        // will be relative to the frame listed in the header.
        [DataMember] public geometry_msgs.Point mouse_point { get; set; }
        [DataMember] public bool mouse_point_valid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerFeedback()
        {
            header = new std_msgs.Header();
            client_id = "";
            marker_name = "";
            control_name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerFeedback(std_msgs.Header header, string client_id, string marker_name, string control_name, byte event_type, geometry_msgs.Pose pose, uint menu_entry_id, geometry_msgs.Point mouse_point, bool mouse_point_valid)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.client_id = client_id ?? throw new System.ArgumentNullException(nameof(client_id));
            this.marker_name = marker_name ?? throw new System.ArgumentNullException(nameof(marker_name));
            this.control_name = control_name ?? throw new System.ArgumentNullException(nameof(control_name));
            this.event_type = event_type;
            this.pose = pose;
            this.menu_entry_id = menu_entry_id;
            this.mouse_point = mouse_point;
            this.mouse_point_valid = mouse_point_valid;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal InteractiveMarkerFeedback(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.client_id = b.DeserializeString();
            this.marker_name = b.DeserializeString();
            this.control_name = b.DeserializeString();
            this.event_type = b.Deserialize<byte>();
            this.pose = new geometry_msgs.Pose(b);
            this.menu_entry_id = b.Deserialize<uint>();
            this.mouse_point = new geometry_msgs.Point(b);
            this.mouse_point_valid = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new InteractiveMarkerFeedback(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.Serialize(this.client_id);
            b.Serialize(this.marker_name);
            b.Serialize(this.control_name);
            b.Serialize(this.event_type);
            b.Serialize(this.pose);
            b.Serialize(this.menu_entry_id);
            b.Serialize(this.mouse_point);
            b.Serialize(this.mouse_point_valid);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
            if (client_id is null) throw new System.NullReferenceException();
            if (marker_name is null) throw new System.NullReferenceException();
            if (control_name is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 98;
                size += header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(client_id);
                size += BuiltIns.UTF8.GetByteCount(marker_name);
                size += BuiltIns.UTF8.GetByteCount(control_name);
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ab0f1eee058667e28c19ff3ffc3f4b78";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71W227bRhB951cM4Ickhay0cRoEBvLg2koixBcVltpHYkUOqYXJXWZ3aUX9+p5ZXkzV" +
                "RdGHNoYBUcuZM3POXFYntNY1vy6cqpm0Kew8+cwqZ0e7+JEkJ7TM2QRdHLQpyQeHjzndtD7Qlqk1+msr" +
                "nhR2TME2OiMDLN+ojOdJZ05ZpQGR6lzgPjLnW5U9EKy8Kpk83lE8KZytI9CnzXJGPC/nsN/vuEP3QYXW" +
                "ky1IGUQM7FQW9CNTrdwDMt4rT7XNdaE5p+0h+rSe3Vyi3jecyRsPPJ3t/s5fmZwya4KzFXy1HxN0XLDz" +
                "YDfw6RxSITpS7By7M8RbHxqWVCUJfgRDnH1ZLFbpxfXyt8V5RxqpVEy5U2UpGMHSA3NDbTPm0SN0AQFx" +
                "s7jdpPeL68Xl+pwUMjQtAckdaAf2W4ZUnivOAuew/mWzXt/dppfXy8svYr5tQ7BmBB9dUJ/sIXqs7u4X" +
                "6WZ1dbFGjhK5sZ4nhjtlSqjbesnXmpFiD+mTFsK+nzClD/RjfzjBxulP/emEEU7f9KfTzHF8lgzWdxuA" +
                "XN39fovTt0eHmxWOfh4Mo+ZpQBWkGpetcyJ4ZPNXTW9t4HP6rKTC0tOPqtI5FRYdUVVUDO0qWH6elGxr" +
                "huBp7Uv/eiWAghqjQASljY/wy6sh0FCQSbVgfGeqwyTUVIaYO0IJk7M30S2Nbv0ELYsJPUKjTtWaTTSa" +
                "kSD36swwHBiHtLE6dmOtDrFsqh/eM8qxCozX1qhKOOmAx6MeRsXlC5z7es9JktGBcsv+KEDaUdtrSAhN" +
                "g2sZxscZDC8dVyoOIvSXWN02qrQX0frsunX0XH4gHaFuLRr7WR5J8uE//ktu7j9hikPeJdItTdkzAWtE" +
                "OSl2ULkKKlZ3p8sdu9MKKlayx+oGzOLbvquwMGTj4L9kg8VUoTnAIRdJMlvX2LOZClAIJTryhycUUtQo" +
                "F3TWVsrB3rpcGzGPSgo6/j1jU5tMOvNc6uc5a0V1RNImc6ziTKNth76DQ3Ky3ttT2ZUlFuQYHBVRQZLl" +
                "b43DkkQyyp8jxg8duTmwZcUhSu7pZTxL8dW/IgRBCtxY7OCXyHx1CLuuqzAMTqttFTs6gwJAfSFOL15N" +
                "kE2ENsrYAb5DfIrxb2DNiCucTrHV8ipeb20JAWHYOPuo86d7pLvB0JNbpzC+4tWFTE4+xm6NF0asCD6V" +
                "9zbTStp3r8NuuCNiNdL/rxufryYQvMB4SZGQvhoGOq5BqFQ4Bg25qmfSZXKc9++74Zcr0To9+M4p6SZu" +
                "MEh+bcHSmYj7ZPe9CHZrJE5ONt290+WF0YgpH9FNisqq8O4tfRufDuPTH98n/SfpBg5jobz8wpnoeZy8" +
                "fPv6pDv2S42fOP/MaHjaJ8mfRXnjYvMJAAA=";
                
    }
}
