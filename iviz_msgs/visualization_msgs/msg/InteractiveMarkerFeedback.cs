/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class InteractiveMarkerFeedback : IDeserializable<InteractiveMarkerFeedback>, IMessage
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
            ClientId = string.Empty;
            MarkerName = string.Empty;
            ControlName = string.Empty;
        }
        
        /// Explicit constructor.
        public InteractiveMarkerFeedback(in StdMsgs.Header Header, string ClientId, string MarkerName, string ControlName, byte EventType, in GeometryMsgs.Pose Pose, uint MenuEntryId, in GeometryMsgs.Point MousePoint, bool MousePointValid)
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
        
        /// Constructor with buffer.
        internal InteractiveMarkerFeedback(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            ClientId = b.DeserializeString();
            MarkerName = b.DeserializeString();
            ControlName = b.DeserializeString();
            EventType = b.Deserialize<byte>();
            b.Deserialize(out Pose);
            MenuEntryId = b.Deserialize<uint>();
            b.Deserialize(out MousePoint);
            MousePointValid = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new InteractiveMarkerFeedback(ref b);
        
        InteractiveMarkerFeedback IDeserializable<InteractiveMarkerFeedback>.RosDeserialize(ref Buffer b) => new InteractiveMarkerFeedback(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ClientId);
            b.Serialize(MarkerName);
            b.Serialize(ControlName);
            b.Serialize(EventType);
            b.Serialize(ref Pose);
            b.Serialize(MenuEntryId);
            b.Serialize(ref MousePoint);
            b.Serialize(MousePointValid);
        }
        
        public void RosValidate()
        {
            if (ClientId is null) throw new System.NullReferenceException(nameof(ClientId));
            if (MarkerName is null) throw new System.NullReferenceException(nameof(MarkerName));
            if (ControlName is null) throw new System.NullReferenceException(nameof(ControlName));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 98;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(ClientId);
                size += BuiltIns.GetStringSize(MarkerName);
                size += BuiltIns.GetStringSize(ControlName);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ab0f1eee058667e28c19ff3ffc3f4b78";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W227bRhB951cM4Ickhay0cRoEAvLg2koixBcVltpHYkUOqYXJXWZ3aUf9+p5ZXkzV" +
                "RdGHNoYAUcuZM3POXNYntNE1vy6cqpm0Kew8+cwqZ0f7+JUkJ7TK2QRdHLQpyQeHrzldtz7Qjqk1+msr" +
                "nhT2TME2OiMDLN+ojOdJZ05ZpQGR6lzgPjLnO5XdE6y8Kpk83lE8KZytI9Cn7WpGPC/nsH/cc4fugwqt" +
                "J1uQMogY2Kks6AemWrl7ZPyoPNU214XmnHaH6NN6dnOJetdwJm888HS2/zt/ZXLKrAnOVvDVfkzQccHO" +
                "g93Ap3NIhehIsXPszhBvc2hYUpUk+AEMcfZluVyn51er35aLjjRSqZhyp8pSMIKle+aG2mbMo0foAgLi" +
                "enmzTe+WV8uLzYIUMjQtAckdaA/2O4ZUnivOAuew/mW72dzepBdXq4svYr5rQ7BmBB9dUJ/sPnqsb++W" +
                "6XZ9eb5BjhK5sZ4nhntlSqjbesnXmpFiD+mTFsK+nzClD/RjfzjBxulP/emEEU7f9KfTzHF8lgzWt1uA" +
                "XN7+foPTt0eH2zWOfh4Mo+ZpQBWkGhetcyJ4ZPNXTW9s4AV9BkcUAD39oCqdU2HREVVFxdCuguXnScm2" +
                "Zgie1r70r9cCKKgxCkRQ2vgIv7ocAg0FmVQLxremOkxCTWWIuSOUMDl7E93S6NZP0KqY0CM06lSt2USj" +
                "GQlyr84Mw4FxSBsLWKDU6hDLhoxjmmeUYxUYr61RlXDSAY9HPYyKyw849/WekySjA+WW/VGAtKP2qCEh" +
                "NA2uZRgfZzC8dFypOIjQX2J126jSXkTrs+vW0XP5gXSEurNo7Gd5JMmH//gvub77hCkOeZdItzRlzwSs" +
                "EeWk2EHlKqhY3b0u9+xOK6hYyR6rGzCLb/uuwsKQjYNPyQaLqUJzgEMukmS2rrFnMxWgEEp05A9PKKSo" +
                "US7orK2Ug711uTZiHpUUdHw8Y1ObTDpzIfXznLWiOiJpkzlWcabRtkPfwSE52TzaU9mVJRbkGBwVUUGS" +
                "5W+Nw5JEMsovEOOHjtwc2LLiECX39DKepfjpXxGCIAVuLHbwS2S+PoR911UYBqfVDgsRwBkUAOoLcXrx" +
                "aoIsaS9wxRg7wHeITzH+DawZcYXTKbZaXsXrrS0hIAwbZx90/nSPdDcYenLnFMZXvLqQycnH2K3xwogV" +
                "wbfy3mYaBZABCPvhjojVSP+/bny+mkDwHOMlRUL6ahjouAahUuEYNOSqnkmXyXHev++GX65E6/TgO6ek" +
                "m7jBIPm1BUtnIu6T3fci2K2RODnZdPdOlxdGI6Z8RDcpKqvCu7f0bXw6jE9/fJ/0n6QbOIyFQgcd6Xmc" +
                "vPz6+qQ79kuNf3H+mdHw9JgkfwJFeeNi8wkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
