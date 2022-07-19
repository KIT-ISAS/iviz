/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarker : IDeserializableRos2<InteractiveMarker>, IMessageRos2
    {
        // Time/frame info.
        // If header.time is set to 0, the marker will be retransformed into
        // its frame on each timestep. You will receive the pose feedback
        // in the same frame.
        // Otherwise, you might receive feedback in a different frame.
        // For rviz, this will be the current 'fixed frame' set by the user.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Initial pose. Also, defines the pivot point for rotations.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        [DataMember (Name = "name")] public string Name;
        // Short description (< 40 characters).
        [DataMember (Name = "description")] public string Description;
        // Scale to be used for default controls (default=1).
        [DataMember (Name = "scale")] public float Scale;
        // All menu and submenu entries associated with this marker.
        [DataMember (Name = "menu_entries")] public MenuEntry[] MenuEntries;
        // List of controls displayed for this marker.
        [DataMember (Name = "controls")] public InteractiveMarkerControl[] Controls;
    
        /// Constructor for empty message.
        public InteractiveMarker()
        {
            Name = "";
            Description = "";
            MenuEntries = System.Array.Empty<MenuEntry>();
            Controls = System.Array.Empty<InteractiveMarkerControl>();
        }
        
        /// Constructor with buffer.
        public InteractiveMarker(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            b.DeserializeString(out Name);
            b.DeserializeString(out Description);
            b.Deserialize(out Scale);
            b.DeserializeArray(out MenuEntries);
            for (int i = 0; i < MenuEntries.Length; i++)
            {
                MenuEntries[i] = new MenuEntry(ref b);
            }
            b.DeserializeArray(out Controls);
            for (int i = 0; i < Controls.Length; i++)
            {
                Controls[i] = new InteractiveMarkerControl(ref b);
            }
        }
        
        public InteractiveMarker RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarker(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Pose);
            b.Serialize(Name);
            b.Serialize(Description);
            b.Serialize(Scale);
            b.SerializeArray(MenuEntries);
            b.SerializeArray(Controls);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Description is null) BuiltIns.ThrowNullReference();
            if (MenuEntries is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < MenuEntries.Length; i++)
            {
                if (MenuEntries[i] is null) BuiltIns.ThrowNullReference(nameof(MenuEntries), i);
                MenuEntries[i].RosValidate();
            }
            if (Controls is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Controls.Length; i++)
            {
                if (Controls[i] is null) BuiltIns.ThrowNullReference(nameof(Controls), i);
                Controls[i].RosValidate();
            }
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Pose);
            WriteBuffer2.AddLength(ref c, Name);
            WriteBuffer2.AddLength(ref c, Description);
            WriteBuffer2.AddLength(ref c, Scale);
            WriteBuffer2.AddLength(ref c, MenuEntries);
            WriteBuffer2.AddLength(ref c, Controls);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarker";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
