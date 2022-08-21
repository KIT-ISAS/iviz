/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
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
    
        public InteractiveMarkerFeedback()
        {
            ClientId = "";
            MarkerName = "";
            ControlName = "";
        }
        
        public InteractiveMarkerFeedback(ref ReadBuffer b)
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
        
        public InteractiveMarkerFeedback(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeString(out ClientId);
            b.Align4();
            b.DeserializeString(out MarkerName);
            b.Align4();
            b.DeserializeString(out ControlName);
            b.Deserialize(out EventType);
            b.Align8();
            b.Deserialize(out Pose);
            b.Deserialize(out MenuEntryId);
            b.Deserialize(out MousePoint);
            b.Deserialize(out MousePointValid);
        }
        
        public InteractiveMarkerFeedback RosDeserialize(ref ReadBuffer b) => new InteractiveMarkerFeedback(ref b);
        
        public InteractiveMarkerFeedback RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarkerFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        public int RosMessageLength
        {
            get {
                int size = 98;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(ClientId);
                size += WriteBuffer.GetStringSize(MarkerName);
                size += WriteBuffer.GetStringSize(ControlName);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, ClientId);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, MarkerName);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, ControlName);
            c += 1; // EventType
            c = WriteBuffer2.Align8(c);
            c += 56; // Pose
            c += 4; // MenuEntryId
            c = WriteBuffer2.Align8(c);
            c += 24; // MousePoint
            c += 1; // MousePointValid
            return c;
        }
    
        public const string MessageType = "visualization_msgs/InteractiveMarkerFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ab0f1eee058667e28c19ff3ffc3f4b78";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71W227bRhB951cM4Ickha00cRoUBvLg2koixBc1ltqHoiBW5JBamNxldpdW1K/vmeXF" +
                "VF0UfWhjGBC1nDkz58xldUQrXfPLwqmaSZvCzpKPrHJ2tI0fSXJEi5xN0MVem5J8cPiY0XXrA22YWqO/" +
                "tOJJYcsUbKMzMsDyjcp4lnTmlFUaEKnOBe49c75R2T3ByquSyeMdxZPC2ToCfVgvjoln5Qz2uy136D6o" +
                "0HqyBSmDiIGdyoJ+YKqVu0fGO+WptrkuNOe02Uef1rObSdS7hjN544Gns+3f+SuTU2ZNcLaCr/Zjgo4L" +
                "dh7sBj6dQypER4qdY3eGeKt9w5KqJMEPYIizT/P5Mj2/WvwyP+tII5WKKXeqLAUjWLpnbqhtxjx6hC4g" +
                "IK7nN+v0bn41v1idkUKGpiUguT1twX7DkMpzxVngHNY/rVer25v04mpx8UnMN20I1ozgowvqk91Hj+Xt" +
                "3TxdLy/PV8hRIjfW88Rwq0wJdVsv+VozUuwhfdJC2B8nTOkdfd8fTrBx+qo/nTDC6ev+dJo5jk+Twfp2" +
                "DZDL219vcPrm4HC9xNEPg2HUPA2oglTjonVOBI9s/qrpjQ18Rh+VVFh6+kFVOqfCoiOqioqhXQXLz5KS" +
                "bc0QPK196V8uBVBQYxSIoLTxEX5xOQQaCjKpFoxvTbWfhJrKEHNHKGFy+jq6pdGtn6BFMaFHaNSpWscT" +
                "jY5JkHt1jjEcGIe0sTp2Y632sWyqH95TyrEKjNfWqEo46YDHgx5GxeULnPt6z0iS0YFyy/4gQNpR22lI" +
                "CE2DaxnGhxkMLx1XKg4i9JdY3TaqtBfR+uy6dfRUfiAdoG4sGvtJHkny7j/+S67vPmCKQ94l0i1N2TMB" +
                "a0Q5KXZQuQoqVneryy27kwoqVrLH6gbM4tu+q7AwZOPgv2SDxVShOcAhF0kyW9fYs5kKUAglOvCHJxRS" +
                "1CgXdNZWysHeulwbMY9KCjr+PWNTm0w680zq5zlrRXVE0iZzrOJMo22HvoMDHdFvn61/9XtytNrZE1ma" +
                "JTblmAVKo4JkzV8bh22JrJQ/Q7DvOpYzBJFdh3C5p+fxLMVX/4IQDblwY7GMn4PCch+2XXthKpxWmyq2" +
                "dgYpgPpMnJ69mCCbCG2UsQN8h/gY49/AmhFXOJ1gveVVvOfaEkrCsHH2QeePF0p3laE5N05hjsWrC5kc" +
                "vY9tG2+OWBp8Ku9tppX08U6H7XBZxLKk/19bPt1RIHiOOZMiIX01THbch1CpcAwacmcfS7vJcd6/77aA" +
                "3I3W6cF3Rkk3eoNB8nMLls5E3Ee7b0Ww2ydxhLLpEp5uMcxITPmAblJUVoW3b+jr+LQfn/74Nuk/Sjdw" +
                "GAvl5afORM/D5OXbl0fdsWhq/Nb5Z0bD0y5J/gSyTuQi/AkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
