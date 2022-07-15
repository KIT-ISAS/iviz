/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerFeedback : IDeserializable<InteractiveMarkerFeedback>, IMessageRos2
    {
        // Time/frame info.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Identifying string. Must be unique in the topic namespace.
        [DataMember (Name = "client_id")] public string ClientId;
        // Feedback message sent back from the GUI, e.g.
        // when the status of an interactive marker was modified by the user.
        // Specifies which interactive marker and control this message refers to
        [DataMember (Name = "marker_name")] public string MarkerName;
        [DataMember (Name = "control_name")] public string ControlName;
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
        [DataMember (Name = "event_type")] public byte EventType;
        // Current pose of the marker
        // Note: Has to be valid for all feedback types.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // Contains the ID of the selected menu entry
        // Only valid for MENU_SELECT events.
        [DataMember (Name = "menu_entry_id")] public uint MenuEntryId;
        // If event_type is BUTTON_CLICK, MOUSE_DOWN, or MOUSE_UP, mouse_point
        // may contain the 3 dimensional position of the event on the
        // control.  If it does, mouse_point_valid will be true.  mouse_point
        // will be relative to the frame listed in the header.
        [DataMember (Name = "mouse_point")] public GeometryMsgs.Point MousePoint;
        [DataMember (Name = "mouse_point_valid")] public bool MousePointValid;
    
        /// Constructor for empty message.
        public InteractiveMarkerFeedback()
        {
            ClientId = "";
            MarkerName = "";
            ControlName = "";
        }
        
        /// Constructor with buffer.
        public InteractiveMarkerFeedback(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out ClientId);
            b.DeserializeString(out MarkerName);
            b.DeserializeString(out ControlName);
            b.Deserialize(out EventType);
            b.Deserialize(out Pose);
            b.Deserialize(out MenuEntryId);
            b.Deserialize(out MousePoint);
            b.Deserialize(out MousePointValid);
        }
        
        public InteractiveMarkerFeedback RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarkerFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ClientId);
            b.Serialize(MarkerName);
            b.Serialize(ControlName);
            b.Serialize(EventType);
            b.Serialize(in Pose);
            b.Serialize(MenuEntryId);
            b.Serialize(in MousePoint);
            b.Serialize(MousePointValid);
        }
        
        public void RosValidate()
        {
            if (ClientId is null) BuiltIns.ThrowNullReference();
            if (MarkerName is null) BuiltIns.ThrowNullReference();
            if (ControlName is null) BuiltIns.ThrowNullReference();
        }
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, ClientId);
            WriteBuffer2.Advance(ref c, MarkerName);
            WriteBuffer2.Advance(ref c, ControlName);
            WriteBuffer2.Advance(ref c, EventType);
            WriteBuffer2.Advance(ref c, Pose);
            WriteBuffer2.Advance(ref c, MenuEntryId);
            WriteBuffer2.Advance(ref c, MousePoint);
            WriteBuffer2.Advance(ref c, MousePointValid);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarkerFeedback";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
