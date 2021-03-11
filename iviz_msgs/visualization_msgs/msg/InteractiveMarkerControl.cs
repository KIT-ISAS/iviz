/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/InteractiveMarkerControl")]
    public sealed class InteractiveMarkerControl : IDeserializable<InteractiveMarkerControl>, IMessage
    {
        // Represents a control that is to be displayed together with an interactive marker
        // Identifying string for this control.
        // You need to assign a unique value to this to receive feedback from the GUI
        // on what actions the user performs on this control (e.g. a button click).
        [DataMember (Name = "name")] public string Name { get; set; }
        // Defines the local coordinate frame (relative to the pose of the parent
        // interactive marker) in which is being rotated and translated.
        // Default: Identity
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation { get; set; }
        // Orientation mode: controls how orientation changes.
        // INHERIT: Follow orientation of interactive marker
        // FIXED: Keep orientation fixed at initial state
        // VIEW_FACING: Align y-z plane with screen (x: forward, y:left, z:up).
        public const byte INHERIT = 0;
        public const byte FIXED = 1;
        public const byte VIEW_FACING = 2;
        [DataMember (Name = "orientation_mode")] public byte OrientationMode { get; set; }
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
        [DataMember (Name = "interaction_mode")] public byte InteractionMode { get; set; }
        // If true, the contained markers will also be visible
        // when the gui is not in interactive mode.
        [DataMember (Name = "always_visible")] public bool AlwaysVisible { get; set; }
        // Markers to be displayed as custom visual representation.
        // Leave this empty to use the default control handles.
        //
        // Note: 
        // - The markers can be defined in an arbitrary coordinate frame,
        //   but will be transformed into the local frame of the interactive marker.
        // - If the header of a marker is empty, its pose will be interpreted as 
        //   relative to the pose of the parent interactive marker.
        [DataMember (Name = "markers")] public Marker[] Markers { get; set; }
        // In VIEW_FACING mode, set this to true if you don't want the markers
        // to be aligned with the camera view point. The markers will show up
        // as in INHERIT mode.
        [DataMember (Name = "independent_marker_orientation")] public bool IndependentMarkerOrientation { get; set; }
        // Short description (< 40 characters) of what this control does,
        // e.g. "Move the robot". 
        // Default: A generic description based on the interaction mode
        [DataMember (Name = "description")] public string Description { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerControl()
        {
            Name = string.Empty;
            Markers = System.Array.Empty<Marker>();
            Description = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerControl(string Name, in GeometryMsgs.Quaternion Orientation, byte OrientationMode, byte InteractionMode, bool AlwaysVisible, Marker[] Markers, bool IndependentMarkerOrientation, string Description)
        {
            this.Name = Name;
            this.Orientation = Orientation;
            this.OrientationMode = OrientationMode;
            this.InteractionMode = InteractionMode;
            this.AlwaysVisible = AlwaysVisible;
            this.Markers = Markers;
            this.IndependentMarkerOrientation = IndependentMarkerOrientation;
            this.Description = Description;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public InteractiveMarkerControl(ref Buffer b)
        {
            Name = b.DeserializeString();
            Orientation = new GeometryMsgs.Quaternion(ref b);
            OrientationMode = b.Deserialize<byte>();
            InteractionMode = b.Deserialize<byte>();
            AlwaysVisible = b.Deserialize<bool>();
            Markers = b.DeserializeArray<Marker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new Marker(ref b);
            }
            IndependentMarkerOrientation = b.Deserialize<bool>();
            Description = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerControl(ref b);
        }
        
        InteractiveMarkerControl IDeserializable<InteractiveMarkerControl>.RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerControl(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            Orientation.RosSerialize(ref b);
            b.Serialize(OrientationMode);
            b.Serialize(InteractionMode);
            b.Serialize(AlwaysVisible);
            b.SerializeArray(Markers, 0);
            b.Serialize(IndependentMarkerOrientation);
            b.Serialize(Description);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
            if (Description is null) throw new System.NullReferenceException(nameof(Description));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += BuiltIns.UTF8.GetByteCount(Name);
                foreach (var i in Markers)
                {
                    size += i.RosMessageLength;
                }
                size += BuiltIns.UTF8.GetByteCount(Description);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerControl";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b3c81e785788195d1840b86c28da1aac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1YbXMaRxL+HKr8H6asSllKAEmW4yTc6QMWyKYigQ6wY1cqRQ27A0y07JCZXSH86+/p" +
                "ntllEVJ8H862VdLu7Ey/99PdcyCGamWVU2nmhBSRSTNrEpEtZCa0E5kRUyVi7VaJ3KgY73OVLZQVa50t" +
                "hEyFTjNlZZTpOyWW0t4qW6sdiF4Menq20elcuMzSn5mxoAqSgUUT2z6ZXKSKyQrpnJ6nECFP9d+5Ency" +
                "wW984EP4a1WkiMsMB6YyuhUza5b4qsTb9z0QM6lYk9QkjEkdf8kdRF0pC+ZLRzuqEohD1Zw3wXGaZxm+" +
                "RYmObo+atSBwKpeqRsp01EynyhNMTCQTEDA21qnMII3FNnFoVSLZBiywEivjlDAz/ywtrAFC+7Y6whqk" +
                "1tGCjD1VxNeaDIRjGBd2sTJ1oKziphdE5knWCubNNrW5MkuV2c1k6ebu+D85dtoU2gtjNbZAJJOyDoPt" +
                "u1iaWLUKIzixMOvqdhEtZDpXjhj2+u+6w964JS5NkjzYBuUe8f2BuOx97HZa4jelVjv7Z/qelEJUpTrT" +
                "MKIjNXHgQ6/7++SyfdHrv22JdkJBsGl8Fgi4VPkwc5FVKhWH9y2KorW0cV1sWomaZXXxuZWv4LMcsvxS" +
                "yCvOxYkISywOFk7De4UbVl/WwnJF0gnZh4O4UC/YbC+EsQc//UG/2xLjamDh0aTJRiyVTDM+dqddLhP9" +
                "mTn8S6SG96r7DHvSnGx93e2/b4krfauYYp2iUlg9X2QNDkzeSJS9wenIm/fj8aDfEt1E4WMmIuQjspUM" +
                "48/4sLkefOhO2h97I0hZxJOQiUGs+XC+b8h77cqtN1dt1qjcixj1G0u/0N7hYNweF4SHHLRCWpMjbB8l" +
                "6/e3xIVZTjmftsw41iv0Cn+SIarOJBtVfOn1ZzeG74WmWDsrD23ZnItXYbHCDKs/Vbf6T1h9Dcmfn3We" +
                "b+M8BIITa2NvfWxSgi8NcObH0bve5fjHi/HwCtHkP551RJRbZ+zWCmedqmVnCGzECQyMrW4lo6plaWuw" +
                "6/4+IacGAULsEbtzzQnpkcZj0gO7M7XLPEnE60ZncMkUY+AnYVQQh9QjRzAA8UvJwmsROBXeCfrAUj/v" +
                "WpXXftm3qf/wa5FzFbOGnKOkI3lyVfdskSMSsRIHeIHhNTSQieOyhKTS04QwZL0APtCJea4pR1JDOLML" +
                "UODQrE0N0lMma7lxk+I4sb0O9B/WO4mkzl0GQ/kMRhUK1ZJNREa+UpJwn/JfLVfZhmggHlic2CN2CQyA" +
                "1jhhbCXgMBlwGA8NoEeBoK7IYhxlzaEGFqSdavjJbvZqTx0EBIMF2wYn2Z9U8fh0KEg+JX21CnVpH75J" +
                "nQa7AJ8XSsaontgsw2dRqFgXGt0C17iCKRODabhyOdJKiC8XxUdl8L7448/CIj4u0h3oJm/WhVNZ2R5Q" +
                "1Ag9Exv0FLFJX8AeBL6cn4HOQXAvgHhOpi0TOIJVrISL1RpCQqbmjkdYR0d1Ml+BCNSDU4paU4krncZq" +
                "pfArzSb+7ORhHR4tjM3gW5Q0veIkO/y3eHVCRZfMAG5HZCFuZHZ6ldgoR67mluX5teGQU8jVqcmeN0W1" +
                "O2iLuUqV1dEOn6l0UJlbILWHaEXPUzlQe1Y7/z//e1a7HqHGP9W0PIMSXEbLHEOx22llyO4EXAEC8fb3" +
                "tuWhiG/WarPEyOz1K3FfPm3Kp8/l0/rrqbdT6r2OPqJJvxGEX2TZqnV8vF6vm9a4prHz47W+1cf2Tn8+" +
                "7njoGW9WqjjHoPyPh8Z5BisBFsMJ9/1Z+/uXJ2+k0xH+jhYS1LgPWRpLdiNbhTYuBViR6znalso5OQ9t" +
                "F5EusLo9HA5+Pz8Jbxfv33TPiyo8ukEidM+LGnzx6arX73SH52dhAa/dyWg87N2cv6ouXfVG4/OfKhT9" +
                "yusdsn6tqC83g15/PDovSsu4+3E8qaDC+a9lnzB6Nxl2R4P3wwsIWogNGdr9t1eB6OlpqVynU6p2Pej0" +
                "Lj+Vr53uVXe8Vc6/tq+uoF3tnQfIgJNP/DsovnPzqJfq2GNwxQflwOGeIuIJ9XHOBz5gDEFCD+Q0M/1L" +
                "RQSlzWaTKg/XDODGX3nqM5y9qXnKQifNfVoxZdGQE/paFQjVoOnZS9r/3Xf/IE3g2usQy1mePMqUqKal" +
                "2ByAMtWrnOoCNKbAjlWi+GUrgaDeyAY5MmTC05IAMeg7IHNH+ABtT586ETKOjwF9GFIZZPh0XZyKQ2A4" +
                "Rk2auY7q4qWXD9lT2XS2XURh8KvuwSx2Q7WOC94+85tKHQxi7x7+gCVjzzD4yGSXABCE13ZOi9M6/vO0" +
                "4cqW4zAnGELTSB9gTuGAlVYdIdpiz+XCJMYO375pw294esCHv4o/TponjdPmyZ+1OLceMRI9g8MQNo8a" +
                "9h2qJI8WFfFQOvMEU4F0GcovokCFUVcCtygHIhbUGzVuCnjH60Jb7xAJXF05byboYzDWPM6bGxdCMQ+b" +
                "gS0KPh9t+KPoXZqqiRqz1yZRT+OTk5iij4OSLlMr1O0BTXM+tbzhOSrdSkV6prG6QE/g4EFu/EI6LXG/" +
                "gFrtMategcG6QKIe7cULZEDPw+2H+zocD9J8OfUtHXsclkJvKxQyFauw0wlNLgpxkhQt2/ZEIVh/QEOc" +
                "TFYLWXTaG7RhJOojoQWNPCtqfvxRU2rGYvMMHBq0AIS09tT+HWh/eBD+WkzQOpjcRspHDS/h9ERBkThW" +
                "EJDAhYrl12sBSjv4EsGFPwPY4eaCklHGMpOszQLzvbKNBAHHFyLLFdTkr+Runhq5I8IP93ScJ2wLwnKz" +
                "XALGCapCrFbO83UTcB59NvILgGv3Jgeijh8Hh6sU8NzDhAgEdyrKqR/neZPqBbcHwHmugQBXHKgdjNem" +
                "QV3kHNFRMi+vDdU9dXAkp3Qt8PjBK9cEbVhHgUvsxCGvTfCKphdMIIJaGVyFHULym022CM3qnYS7MKgR" +
                "YUIKUH1Bh14cVSiT2C3UmtQU5D3FLY//hWxa0iWdGjyu8R1mPocBsXFlzZ1GFInpxk8OCTWnwMQpDWc1" +
                "xkVmWTu4ZChhQGKP0NWNcybSfLtH5bEIW49sOv5WLTfVH4rJ9oNp1qc58GSnza5ToNFyHL7r8qag0ppj" +
                "BGEIKzfUnriN/GY6QphyoggXCf4Ot1SBR1vGtV2NvzBDfCMNQhPw6FQk7vjjrtg8BfayB7ePfHMeTuJg" +
                "rNHceIdhxLVcjWmc5wGT0Bw0lhKXkNhPlxi4mF+tQEzuXBNxtfT353V/+cK7uKaTFMUASvdF4RZ7O3IQ" +
                "TRG0wz3P7KUfsFlmzwxeApHiHuqoSZWd5nqe6PFgA0byNF/IxbmcGVMvbl9Yjr2YKGcchEMGeP7SyPj1" +
                "i0RZLJ95roBYWz7Ny6dp+SQRgv8Fj0TwZLgZAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
