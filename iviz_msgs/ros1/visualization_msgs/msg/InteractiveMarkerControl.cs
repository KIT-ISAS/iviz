/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerControl : IDeserializable<InteractiveMarkerControl>, IMessage
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
    
        public InteractiveMarkerControl()
        {
            Name = "";
            Markers = EmptyArray<Marker>.Value;
            Description = "";
        }
        
        public InteractiveMarkerControl(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Orientation);
            b.Deserialize(out OrientationMode);
            b.Deserialize(out InteractionMode);
            b.Deserialize(out AlwaysVisible);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Marker>.Value
                    : new Marker[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Marker(ref b);
                }
                Markers = array;
            }
            b.Deserialize(out IndependentMarkerOrientation);
            b.DeserializeString(out Description);
        }
        
        public InteractiveMarkerControl(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            b.Align8();
            b.Deserialize(out Orientation);
            b.Deserialize(out OrientationMode);
            b.Deserialize(out InteractionMode);
            b.Deserialize(out AlwaysVisible);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Marker>.Value
                    : new Marker[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Marker(ref b);
                }
                Markers = array;
            }
            b.Deserialize(out IndependentMarkerOrientation);
            b.Align4();
            b.DeserializeString(out Description);
        }
        
        public InteractiveMarkerControl RosDeserialize(ref ReadBuffer b) => new InteractiveMarkerControl(ref b);
        
        public InteractiveMarkerControl RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarkerControl(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(in Orientation);
            b.Serialize(OrientationMode);
            b.Serialize(InteractionMode);
            b.Serialize(AlwaysVisible);
            b.Serialize(Markers.Length);
            foreach (var t in Markers)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(IndependentMarkerOrientation);
            b.Serialize(Description);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(in Orientation);
            b.Serialize(OrientationMode);
            b.Serialize(InteractionMode);
            b.Serialize(AlwaysVisible);
            b.Serialize(Markers.Length);
            foreach (var t in Markers)
            {
                t.RosSerialize(ref b);
            }
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
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetArraySize(Markers);
                size += WriteBuffer.GetStringSize(Description);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Name);
            c = WriteBuffer2.Align8(c);
            c += 32; // Orientation
            c += 1; // OrientationMode
            c += 1; // InteractionMode
            c += 1; // AlwaysVisible
            c = WriteBuffer2.Align4(c);
            c += 4; // Markers.Length
            foreach (var t in Markers)
            {
                c = t.AddRos2MessageLength(c);
            }
            c += 1; // IndependentMarkerOrientation
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Description);
            return c;
        }
    
        public const string MessageType = "visualization_msgs/InteractiveMarkerControl";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b3c81e785788195d1840b86c28da1aac";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71YbVMbORL+vP4VqlBbgVszQMjmdn3HBwebxLWAOdvJJpVKueQZ2dYyHjnSDMb59fd0" +
                "SzMeY9jch0sIFWY0Ur/3093aEwO1tMqpLHdCithkuTWpyOcyF9qJ3IiJEol2y1SuVYL3mcrnyoqVzudC" +
                "ZkJnubIyzvWdEgtpb5VtNPZELwE9PV3rbCZcbunP1FhQBcnAIsK2j6YQmWKyQjqnZxlEKDL9pVDiTqb4" +
                "Hx/4EP5aFSviMsWBiYxvxdSaBb4q8eZdD8RMJlYkNQljMsdfCgdRl8qC+cLRjroEYl9FswgcJ0We41uc" +
                "6vj2IGoEgTO5UA1SpqOmOlOeYGpimYKAsYnOZA5pLLaJfatSyTZggZVYGqeEmfpnaWENENq11QHWILWO" +
                "52TsiSK+1uQgnMC4sIuVmUvpNfKCyCLNW8G8+boxU2ahcrseL9zMHf2nwE6bQXthrMYWSZZgHfqbd7Ew" +
                "iWqVRnBiblb17SKey2ymHDHsXb/tDnqjlrgwafpgG5R7xPd74qL3odtpiT+UWm7tn+p7UgpRlelcw4iO" +
                "1MSB973un+OL9nnv+k1LtFMKgvXhV4GAy5QPMxdbpTKxf9+iKFpJmzTFupWqad4UX1vFEj4rIMtvpbzi" +
                "TByLsMTiYOEkvNe4YfVFIyzXJB2TfTiIS/WCzXZCGHvwe92/7rbEqB5YmmItXYuFklnOx+60K2SqvzKH" +
                "f4nM8F51n2NPVpCtr7rX71riUt8qptikqBRWz+b5IQcmbyTK3uB05PW70ah/3RLdVOFjLmLkI7KVDOPP" +
                "+LC56r/vjtsfekNIWcaTkKlBrPlwvj+U99pVW28u26xRtRcx6jdWfqG9g/6oPSoJDzhohbSmQNg+Stbv" +
                "b4lzs5hwPm2YcazX6JX+JEPUnUk2qvnS689uDN9LTbF2KuqLns2ZeBkWa8yw+mt9q/+E1VeQ/Nlp59km" +
                "zkMgOLEy9tbHJiX4wgBnfhm+7V2MfjkfDS4RTf7jaUfEhXXGbqxw2qlbdorARpzAwNjqljKuW5a2Brvu" +
                "7hNyYhAgxB6xO9OckB5pPCY9sDtTuyjSVLw67PQvmGIC/CSMCuKQeuQIBiB+qVh4LQKnqG6tU0quf25b" +
                "ldd+27Wp//B7mXM1s4aco6QjeQrV9GyRIxKxkgR4geE1NJCp47KEpNKTlDBkNVcZn5gVmnIkM4Qz2wAF" +
                "DlFjYgydX8m1G5fHie1VoP+w3kkkdeFyGMpnMKpQqJZsIjLypZKE+5T/arHM10QD8cDiJB6xK2AAtCYp" +
                "YysBh8mBw3g4BHqoSsWQxQlXnYTUwIK0Ew0/2fVO7WmCgGCwYNvgJPuTKh6fDgXJp6SvVqEu7cJ3xML0" +
                "/Oe5kgmqJzbL8FmUKjaFRrfANa5kysRgmtwbjYX6dlF8VAbvi0+fS4v4uMi2oJu82RRO5VV7QFEj9FSs" +
                "0VMkJnsOexD45hvLgop3r6QiAzmrBI5hFSvhYrWCkJAp2vII6+ioThZLEIF6cEpZa2pxpbNELVVGxXns" +
                "z44f1uHh3NgcvkVJ00tOsv1/i5fHVHTJDOB2QBbiRmarV0mMcuRqblmeXZk7H2DWTEz+LBL17qAtZipT" +
                "VsdbfCbSQWWTbbs+IFrZ89QONBpn/+efxtUQFf6plgUacA21tXZ0q48hoxNqBfzD25dNv0PhHjUa09TI" +
                "/NVLcV89raunr9XT6nvptlXlvYJXZV80hOTzPF+2jo5Wq1VkjYuMnR2t9K0+snf661HHg85ovVTlMYbj" +
                "vz00KnKYCIAYTrifT9s/vzh+LZ2O8Xc4l6DGHcjCWDIaGSo0cBlgipzOcbZQzslZaLiIdInS7cGg/+fZ" +
                "cXg7f/e6e1bW3+ENUqB7Vlbf84+XvetOd3B2Ghbw2h0PR4PezdnL+tJlbzg6+7VG0a+82iLr18rKctPv" +
                "XY+GZ2VRGXU/jMY1PDj7veoQhm/Hg+6w/25wDkFLsSFD+/rNZSB6clIp1+lUql31O72Lj9Vrp3vZHW2U" +
                "86/ty0to13jroTEg5BM/e+V3bhv1Qh159K35oBo13FNEPKFrnPNRDwBDkMSh3pjJXyomEI2iiGoOVwsg" +
                "xl9F5nObval5vkIPzR1aOV/ReBM6WhUINaDp6Qva/9NPfyNN4NrrEMtpkT7KlKhmldgcgDLTy4IqAjSm" +
                "wE5UqnIfgKUEgroiG+TIkQlPSwK4oO8Ayy3hA6g9fepYyCQ5AuhhPGWE4dNNcSL2E4KemKatg6Z44eVT" +
                "rr7pdLOIkuBX3YMp7IaqHJe6XeY3tQoYxN4+/B5Lxp5i5JGpenB4yGtbp8VJE/94znBVs7FfEAqhXaQP" +
                "MKdwAEqrDhBtiedyblJjB29et+E3PD3gw1/Fp+Po+PAkOv7cSArrESPVU0WB/Lhh36I+8lBREw9Fs0gx" +
                "D0iXo/BOCYL8kCuBW5QDMQvqjZpEAt7xutDWO0QC11XOmzE6GAw0j/PmloVQzMNmYDsJLdKhP4quJVIR" +
                "CsxOg0TdjE9OYrrmbHW5WqJi92mO86nlDc9R6ZYq1lON1Tm6AQcPcssX0mkh9hWqtMesZg0GmwKJerAT" +
                "L5AB3Q43Hu77cNzLisXEN3PscVgKXa1Qmu9yYKdjmlnUF+pwQ7O2OVEKdt2n8U2my7kse+w1GjAS9ZHQ" +
                "gkaeVYNnZDpqKs1YbJ5+Q2sWgJDWntq/Be0PD8Jf8zH6BlPYWPmo4SWcHisokiQKAhK4ULH8XvW/soIv" +
                "EFT1cyCdtAllokxkLlmVOcZ6ZQ9TRBvfgyyW0JG/kq95WOReCL/cynGSsCEIyM1iAQwnnAqBWjvPt0wA" +
                "ebTXSC6grd0ZGCJ/beHgbZUBm3sdvgxyKi6oDecxk4oF9wYAeS6AQFYcQKZ9Ghh38rmxN1qZQ+oiZ8pu" +
                "pKiuDdU9NXGOZ4EWmP3DaxmBCaykwC5xYp/XxnhF0wtukEUtTTwX+1DhZp3PQ7N6J+E0DGpEmPACVJ/T" +
                "oecHNcoZk85kZkrynuKGx/9CNqvokk6HPK7xHWYxkzz+LK2504glMVn7ySGl/hTIOKHhrMHoyCwbexcM" +
                "KAxL7Bq6unHOxJpv96hIlsHr8U0nP6blphoEBdsPRlmf6U5tt9lNCjdaTsJ3XV0T1FpzzB+MYtWGxlNX" +
                "kT9GQc3XrdWVnNThOriSn4daxrVtdb8xQPwQ8UML8Ng8JO7427bMPPz18geXjnxhHk7iYKLR2XhXYbK1" +
                "XIppiue5kqAcNBbyFiRV5rjNlMsliMmt2yEulf7avOnvXHgXF3SSopw76Zoo2dxDVIelCMo1RT594edq" +
                "ltkzg4tApLx+OoiorNM4z4M8HmzASFPdckAuTuHcmGZ56cJy7MRDNeAgFnKg8zeHxe/j6t066VkCX231" +
                "NKueJtWTbDT+C7jqy5mrGQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
