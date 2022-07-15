/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerControl : IDeserializable<InteractiveMarkerControl>, IMessageRos2
    {
        // Represents a control that is to be displayed together with an interactive marker
        // Identifying string for this control.
        // You need to assign a unique value to this to receive feedback from the GUI
        // on what actions the user performs on this control (e.g. a button click).
        [DataMember (Name = "name")] public string Name;
        // Defines the local coordinate frame (relative to the pose of the parent
        // interactive marker) in which is being rotated and translated.
        // Default: Identity
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation;
        // Orientation mode: controls how orientation changes.
        // INHERIT: Follow orientation of interactive marker
        // FIXED: Keep orientation fixed at initial state
        // VIEW_FACING: Align y-z plane with screen (x: forward, y:left, z:up).
        public const byte INHERIT = 0;
        public const byte FIXED = 1;
        public const byte VIEW_FACING = 2;
        [DataMember (Name = "orientation_mode")] public byte OrientationMode;
        // Interaction mode for this control
        //
        // NONE: This control is only meant for visualization; no context menu.
        // MENU: Like NONE, but right-click menu is active.
        // BUTTON: Element can be left-clicked.
        // MOVE_AXIS: Translate along local x-axis.
        // MOVE_PLANE: Translate in local y-z plane.
        // ROTATE_AXIS: Rotate around local x-axis.
        // MOVE_ROTATE: Combines MOVE_PLANE and ROTATE_AXIS.
        public const byte NONE = 0;
        public const byte MENU = 1;
        public const byte BUTTON = 2;
        public const byte MOVE_AXIS = 3;
        public const byte MOVE_PLANE = 4;
        public const byte ROTATE_AXIS = 5;
        public const byte MOVE_ROTATE = 6;
        // "3D" interaction modes work with the mouse+SHIFT+CTRL or with 3D cursors.
        // MOVE_3D: Translate freely in 3D space.
        // ROTATE_3D: Rotate freely in 3D space about the origin of parent frame.
        // MOVE_ROTATE_3D: Full 6-DOF freedom of translation and rotation about the cursor origin.
        public const byte MOVE_3D = 7;
        public const byte ROTATE_3D = 8;
        public const byte MOVE_ROTATE_3D = 9;
        [DataMember (Name = "interaction_mode")] public byte InteractionMode;
        // If true, the contained markers will also be visible
        // when the gui is not in interactive mode.
        [DataMember (Name = "always_visible")] public bool AlwaysVisible;
        // Markers to be displayed as custom visual representation.
        // Leave this empty to use the default control handles.
        //
        // Note:
        // - The markers can be defined in an arbitrary coordinate frame,
        //   but will be transformed into the local frame of the interactive marker.
        // - If the header of a marker is empty, its pose will be interpreted as
        //   relative to the pose of the parent interactive marker.
        [DataMember (Name = "markers")] public Marker[] Markers;
        // In VIEW_FACING mode, set this to true if you don't want the markers
        // to be aligned with the camera view point. The markers will show up
        // as in INHERIT mode.
        [DataMember (Name = "independent_marker_orientation")] public bool IndependentMarkerOrientation;
        // Short description (< 40 characters) of what this control does,
        // e.g. "Move the robot".
        // Default: A generic description based on the interaction mode
        [DataMember (Name = "description")] public string Description;
    
        /// Constructor for empty message.
        public InteractiveMarkerControl()
        {
            Name = "";
            Markers = System.Array.Empty<Marker>();
            Description = "";
        }
        
        /// Constructor with buffer.
        public InteractiveMarkerControl(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Orientation);
            b.Deserialize(out OrientationMode);
            b.Deserialize(out InteractionMode);
            b.Deserialize(out AlwaysVisible);
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new Marker(ref b);
            }
            b.Deserialize(out IndependentMarkerOrientation);
            b.DeserializeString(out Description);
        }
        
        public InteractiveMarkerControl RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarkerControl(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(in Orientation);
            b.Serialize(OrientationMode);
            b.Serialize(InteractionMode);
            b.Serialize(AlwaysVisible);
            b.SerializeArray(Markers);
            b.Serialize(IndependentMarkerOrientation);
            b.Serialize(Description);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Markers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) BuiltIns.ThrowNullReference(nameof(Markers), i);
                Markers[i].RosValidate();
            }
            if (Description is null) BuiltIns.ThrowNullReference();
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Name);
            WriteBuffer2.Advance(ref c, Orientation);
            WriteBuffer2.Advance(ref c, OrientationMode);
            WriteBuffer2.Advance(ref c, InteractionMode);
            WriteBuffer2.Advance(ref c, AlwaysVisible);
            WriteBuffer2.Advance(ref c, Markers);
            WriteBuffer2.Advance(ref c, IndependentMarkerOrientation);
            WriteBuffer2.Advance(ref c, Description);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarkerControl";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
