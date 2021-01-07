/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/InteractiveMarkerFeedback")]
    public sealed class InteractiveMarkerFeedback : IDeserializable<InteractiveMarkerFeedback>, IMessage
    {
        // Time/frame info.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Identifying string. Must be unique in the topic namespace.
        [DataMember (Name = "client_id")] public string ClientId { get; set; }
        // Feedback message sent back from the GUI, e.g.
        // when the status of an interactive marker was modified by the user.
        // Specifies which interactive marker and control this message refers to
        [DataMember (Name = "marker_name")] public string MarkerName { get; set; }
        [DataMember (Name = "control_name")] public string ControlName { get; set; }
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
        [DataMember (Name = "event_type")] public byte EventType { get; set; }
        // Current pose of the marker
        // Note: Has to be valid for all feedback types.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
        // Contains the ID of the selected menu entry
        // Only valid for MENU_SELECT events.
        [DataMember (Name = "menu_entry_id")] public uint MenuEntryId { get; set; }
        // If event_type is BUTTON_CLICK, MOUSE_DOWN, or MOUSE_UP, mouse_point
        // may contain the 3 dimensional position of the event on the
        // control.  If it does, mouse_point_valid will be true.  mouse_point
        // will be relative to the frame listed in the header.
        [DataMember (Name = "mouse_point")] public GeometryMsgs.Point MousePoint { get; set; }
        [DataMember (Name = "mouse_point_valid")] public bool MousePointValid { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerFeedback()
        {
            Header = new StdMsgs.Header();
            ClientId = "";
            MarkerName = "";
            ControlName = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerFeedback(StdMsgs.Header Header, string ClientId, string MarkerName, string ControlName, byte EventType, in GeometryMsgs.Pose Pose, uint MenuEntryId, in GeometryMsgs.Point MousePoint, bool MousePointValid)
        {
            this.Header = Header;
            this.ClientId = ClientId;
            this.MarkerName = MarkerName;
            this.ControlName = ControlName;
            this.EventType = EventType;
            this.Pose = Pose;
            this.MenuEntryId = MenuEntryId;
            this.MousePoint = MousePoint;
            this.MousePointValid = MousePointValid;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public InteractiveMarkerFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            ClientId = b.DeserializeString();
            MarkerName = b.DeserializeString();
            ControlName = b.DeserializeString();
            EventType = b.Deserialize<byte>();
            Pose = new GeometryMsgs.Pose(ref b);
            MenuEntryId = b.Deserialize<uint>();
            MousePoint = new GeometryMsgs.Point(ref b);
            MousePointValid = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerFeedback(ref b);
        }
        
        InteractiveMarkerFeedback IDeserializable<InteractiveMarkerFeedback>.RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ClientId);
            b.Serialize(MarkerName);
            b.Serialize(ControlName);
            b.Serialize(EventType);
            Pose.RosSerialize(ref b);
            b.Serialize(MenuEntryId);
            MousePoint.RosSerialize(ref b);
            b.Serialize(MousePointValid);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (ClientId is null) throw new System.NullReferenceException(nameof(ClientId));
            if (MarkerName is null) throw new System.NullReferenceException(nameof(MarkerName));
            if (ControlName is null) throw new System.NullReferenceException(nameof(ControlName));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 98;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(ClientId);
                size += BuiltIns.UTF8.GetByteCount(MarkerName);
                size += BuiltIns.UTF8.GetByteCount(ControlName);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
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
