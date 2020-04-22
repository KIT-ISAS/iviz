
namespace Iviz.Msgs.visualization_msgs
{
    public sealed class InteractiveMarkerControl : IMessage
    {
        // Represents a control that is to be displayed together with an interactive marker
        
        // Identifying string for this control.
        // You need to assign a unique value to this to receive feedback from the GUI
        // on what actions the user performs on this control (e.g. a button click).
        public string name;
        
        
        // Defines the local coordinate frame (relative to the pose of the parent
        // interactive marker) in which is being rotated and translated.
        // Default: Identity
        public geometry_msgs.Quaternion orientation;
        
        
        // Orientation mode: controls how orientation changes.
        // INHERIT: Follow orientation of interactive marker
        // FIXED: Keep orientation fixed at initial state
        // VIEW_FACING: Align y-z plane with screen (x: forward, y:left, z:up).
        public const byte INHERIT = 0;
        public const byte FIXED = 1;
        public const byte VIEW_FACING = 2;
        
        public byte orientation_mode;
        
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
        
        public byte interaction_mode;
        
        
        // If true, the contained markers will also be visible
        // when the gui is not in interactive mode.
        public bool always_visible;
        
        
        // Markers to be displayed as custom visual representation.
        // Leave this empty to use the default control handles.
        //
        // Note: 
        // - The markers can be defined in an arbitrary coordinate frame,
        //   but will be transformed into the local frame of the interactive marker.
        // - If the header of a marker is empty, its pose will be interpreted as 
        //   relative to the pose of the parent interactive marker.
        public Marker[] markers;
        
        
        // In VIEW_FACING mode, set this to true if you don't want the markers
        // to be aligned with the camera view point. The markers will show up
        // as in INHERIT mode.
        public bool independent_marker_orientation;
        
        
        // Short description (< 40 characters) of what this control does,
        // e.g. "Move the robot". 
        // Default: A generic description based on the interaction mode
        public string description;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarkerControl";
    
        public IMessage Create() => new InteractiveMarkerControl();
    
        public int GetLength()
        {
            int size = 48;
            size += name.Length;
            for (int i = 0; i < markers.Length; i++)
            {
                size += markers[i].GetLength();
            }
            size += description.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerControl()
        {
            name = "";
            markers = new Marker[0];
            description = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out name, ref ptr, end);
            orientation.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out orientation_mode, ref ptr, end);
            BuiltIns.Deserialize(out interaction_mode, ref ptr, end);
            BuiltIns.Deserialize(out always_visible, ref ptr, end);
            BuiltIns.DeserializeArray(out markers, ref ptr, end, 0);
            BuiltIns.Deserialize(out independent_marker_orientation, ref ptr, end);
            BuiltIns.Deserialize(out description, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(name, ref ptr, end);
            orientation.Serialize(ref ptr, end);
            BuiltIns.Serialize(orientation_mode, ref ptr, end);
            BuiltIns.Serialize(interaction_mode, ref ptr, end);
            BuiltIns.Serialize(always_visible, ref ptr, end);
            BuiltIns.SerializeArray(markers, ref ptr, end, 0);
            BuiltIns.Serialize(independent_marker_orientation, ref ptr, end);
            BuiltIns.Serialize(description, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b3c81e785788195d1840b86c28da1aac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
