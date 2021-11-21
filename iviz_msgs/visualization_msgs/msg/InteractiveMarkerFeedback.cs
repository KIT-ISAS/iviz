/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/InteractiveMarkerFeedback")]
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
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerFeedback()
        {
            ClientId = string.Empty;
            MarkerName = string.Empty;
            ControlName = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
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
            b.Serialize(Pose);
            b.Serialize(MenuEntryId);
            b.Serialize(MousePoint);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ab0f1eee058667e28c19ff3ffc3f4b78";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W227bRhB9J+B/GMAPucBW2jgpCgF5cG0lEeKLCkvtI7Eih9TC5C6zu7Sjfn3PLC+m" +
                "6qLoQxtDgKjlzJk5Zy7rY1rrmt8UTtVM2hR2lnxmlbOjXfxKkmNa5myCLvbalOSDw9eMrlsfaMvUGv21" +
                "FU8KO6ZgG52RAZZvVMazpDOnrNKASHUucB+Z863K7glWXpVMHu8onhTO1hHo02Z5QjwrZ7B/3HGH7oMK" +
                "rSdbkDKIGNipLOgHplq5e2T8qDzVNteF5py2++jTenYziXrXcCZvPPB0tvs7f2VyyqwJzlbw1X5M0HHB" +
                "zoPdwKdzSIXoSLFz7M4Qb71vWFKVJPgBDHH2ZbFYpedXy98W8440UqmYcqfKUjCCpXvmhtpmzKNH6AIC" +
                "4npxs0nvFleLi/WcFDI0LQHJ7WkH9luGVJ4rzgLnsP5ls17f3qQXV8uLL2K+bUOwZgQfXVCf7D56rG7v" +
                "FulmdXm+Ro4SubGeJ4Y7ZUqo23rJ15qRYg/pkxbC/jxhSh/oh/5wgo3TH/vTCSOcvu1Pp5nj+CwZrG83" +
                "ALm8/f0Gp+8ODjcrHL0fDKPmaUAVpBoXrXMieGTzV01vbOA5fQZHFAA9/aAqnVNh0RFVRcXQroLlZ0nJ" +
                "tmYInta+9G9WAiioMQpEUNr4CL+8HAINBZlUC8a3ptpPQk1liLkjlDA5exvd0ujWT9CymNAjNOpUrZOJ" +
                "RickyL06JxgOjEPaWMACpVb7WDZkHNM8oxyrwHhtjaqEkw54POhhVFx+wLmv94wkGR0ot+wPAqQdtUcN" +
                "CaFpcC3D+DCD4aXjSsVBhP4Sq9tGlfYiWp9dt46eyw+kA9StRWM/yyM5Sj78x39HyfXdJ8xxyLtUurV5" +
                "JKsmYJMoJ/UOKldBxQLvdLljd1pByEpWWd2AXHzbNxZ2hiwdfEo22E0V+gM0clEls3WNVZupAJFQpQN/" +
                "eEIkRY1yQWdtpRzsrcu1EfMopqDj4xnL2mTSnHMpoeesFeERSZvMsYpjjc4dWg8OyfH60Z7KuiyxI8fg" +
                "KIoKkix/axz2JJJRfo4YrztyM2DLlkOU3NPLeJbip39FCIIUuLFYwy+R+Wofdl1jYR6cVlvsRABnUACo" +
                "L8TpxasJsqQ9xy1j7ADfIT7F+DewZsQVTqdYbHkVb7i2hIAwbJx90PnTVdJdYmjLrVOYYPHqQibHH2PD" +
                "xjsjVgTfynubaRRAZiDshmsiViP9Pxvy+X6SnjzHkEmdwEANYx2XIYQqHIOJXNgn0mhynPfvuxUgF6N1" +
                "evCdUdLN3WCQ/NqCqDMR98nu+3FEMkfD/GTTJTzdYhiQmPUB46SorAo/vaNv49N+fPrjezF40m+kMZYL" +
                "rXSg6mH+8uvrk/pYNDX+3flnUsPTI+j9CRMLpKUACgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
