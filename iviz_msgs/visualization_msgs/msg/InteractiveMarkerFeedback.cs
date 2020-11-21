/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract (Name = "visualization_msgs/InteractiveMarkerFeedback")]
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
                "H4sIAAAAAAAACr1WTW/bRhC9C9B/GMCHJIWstHFaFAJycG0lEWJbKiy1R2JFDqmFyV1md2lH+fV9sxQp" +
                "qg7QHtoYNkQNd97Me/OxPqO1rvh17lTFpE1up+PRR1YZO9rFj/FoPDqjRcYm6HyvTUE+OHxM6bbxgbZM" +
                "jdGfG3GmsGMKttYpGcD5WqUMuPY8paUGRqKzFvE9c7ZV6QPhoFcFk8dbipbc2SpifdgsJsTTAiBn9LTj" +
                "NoIPKjSebE7KIGpgp9KgH5kq5R6Q95PyVNlM55oz2u6jT+PZAUVw7mtO5Z0Hok5330JQJqPUmuBsCW/t" +
                "+xwd5+w8OPasWo9E+B6Ztq4Ho8Rc72uWhCUVfgRRMX6az1fJ5c3ij/msJY98SqbMqaIQmGDpgbmmpu6T" +
                "OUC0QQXjdn63Se7nN/Or9YwU8jQNAcrtaQcVtgzJPJecBobsZ/TbZr1e3iVXN4urT3J+24RgTQ/f+6BW" +
                "6UPrslrez5PN6vpyjTQleG09D07ulCmgc+MlZWt6mgdMPx41UPjXAVt6Rz921gE6zD915gEtmN905mH6" +
                "sF+IuAeH5QZA18s/72B/e2rdrGD7+Xg2FiAJKElbnKvGOZE/Enum8J0NPKOP4It6oNsfVakzyi26pCwp" +
                "77pY4Dw6rGBbMfRPKl/41yuBFNxDJGiitPExxOK6C9ZVaFA+Ob005X4QbqhJpCDhhNHFm+iYRMd+vhb5" +
                "gCihh4fiTQaCTUjAD0pNMDmYlaS2ABaYSu1jKZF2zPWCMuwL47U1qhRqOuDxpLfRBfJFvA9NMCVJRwfK" +
                "LPuTEEnL70lDS4gbXMM4/LccureOSxXHFJWQaO3SKrUX7Q75tUvrG4UA1inu1qLjn6Ui4r37j3/Go9v7" +
                "D5jxkLXJtPs17qKAVaOcVD6oTAUVK73TxY7deQk1S9l2VQ1+8W3XZdgospbwW7DB9irRKSCSiTKprSqs" +
                "5FQFCIVanQCIK5RSVCsXdNqUysHBukwbOR8Vjfjy5xl73aTSqjMppee0Ef0RTJvUsYozjz7u+xAecFw/" +
                "2XPZqgVWaZ8BqqOCZMxfaod1ioyUn0mYH1qOU8DLIkSgzNPLaEvw1b8ixEEWXFus65dIf7UPu7bJMB9O" +
                "qy3WJpBT6ADYF+L04tUQWlKf4VIytsNvIY9B/g2uOQILrXOsvqyMN2JTQEecrJ191Nnx2mnvPLTo1ikZ" +
                "anFrgwLkfWzfeL/E2uBTeW9TjUrISIRdf6PEuiT/a3c+X1zC8xJDJ+UCC9UNetyTUCt3DDJyyU+k6cSc" +
                "Hd63S0GuUet05ztFn7Rz2J0Yj35vQNaZiHw8+R1pHlZMHKd0uJ6Hqw3jEvM+IT0e5aVV4Ze39OX4iBp3" +
                "j1+/G4ujiD2VvmroqRNpTznIt8/HEmD5VPG/pH9k1j0+yem/ACe0HINECgAA";
                
    }
}
