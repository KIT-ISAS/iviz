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
            Name = "";
            Markers = System.Array.Empty<Marker>();
            Description = "";
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
                "H4sIAAAAAAAACr1Z23IbNxJ9jqr0DyirUpYSkpIsx0m4ywdapGxWZFFL0Y5dqRQL5IAkouGAAWZE0V+/" +
                "pxuYmyhl/bC2b5rBoBt9Pd0NH4iRWlvlVJI6IcXMJKk1sUiXMhXaidSIqRKRdutYblWE94VKl8qKjU6X" +
                "QiZCJ6mycpbqOyVW0t4qu7+3v3cgBhE46vlWJwvhUks/5saCL5iGQ1q075PJRKKYs5DO6UUCKbJE/50p" +
                "cSdj/IsPTIWfVs0UHTQHwVTObsXcmhW+KvHm/YC4mURsSHISyCSOP2UO4q6VxfErRzuqMohD1Vq0cOQ0" +
                "S1N8m8V6dnsEyYLMiVwpUoiY99RcJ8ozjc1MxmBibKQTmUIki53i0KpYsi1YaiXWxilh5v5ZWtiEOO0a" +
                "7QhrEF3PlmT1qaKzrUnBOYKVYR0rEwfWKmoFUWQWp+1g5nS7v7dQZqVSu52s3MId/yfDXpvACMJYjT2Q" +
                "yiS5JsNySaxMpNq5OZxYmk2VQsyWMlkox6cOrt72R4NxW1yYOH6wDzo+FgoH4mLwsd9ri9+UWtcI5vqe" +
                "dEOUJTrVMKYjbYniw6D/++Siez64etMW3ZhCYtv8LBCBifJx52ZWqUQc3rcpqDbSRg2xbcdqnjbE53a2" +
                "JgdmEOeXXGTRESciX2OJsHKaL1QOxPILspH/UJF3QnYKoZ3rGay3E9i0if5eDa/6bTGuhhseTRJvxUrJ" +
                "JGXCO+0yGevPfMq/RGJ4r7pPsSfJ2O7v+lfv2+JS3ypm2aBgFVYvlmmT45V3EmtvfKZ5/X48Hl61RT9W" +
                "+JqKGXIVmUw28kQhkt4NP/Qn3Y+DGwiax5iQsUH8+Ri/b8p77cq915dd1qrYjMD1Owsn8ebRcNwd56xH" +
                "HMpCWpMhmB9n7Ana4tysppxn5XGcAhWGhXvJHDXfkqmqrvVm8F4NO3KFsXhW0pVndcTLfLVyJJZ/qm32" +
                "37D8ijR4dtZ7ViZACAwnNsbe+pAlAFgZYNGPN28HF+Mfz8ejS8SX/3jWE7PMOmMr5jjrVY08R8AjbGBr" +
                "7HVrOasZmfYGE+9uFHJqEC8kAOJ5oTlXPRZ51HroAmZ3kcWxeNXsDS+YZQScJRgLApGG5BOGKH4pzvCK" +
                "hKMKRwWVYK6fHxiXF395xLT+y69lNlbMW2Qj5yPJlamGPx7JIxE+UYAg+EBDExk7rmTINj2NGWY2S0AI" +
                "kSwyTcmTGMKiOorhFKgwNchcGW/k1k0KBv7sd+GQh3VSIuUzl8JqPr9RukKVZXuxyS+VpEJB8KBW63RL" +
                "TBAgLFPkAb7ADaBwFHsYZmQxKUCbnprAlxxuXZ7loGYTQB0sSDvVcJzd7tSrBnEQDCdsJZCyh6lUMnmo" +
                "Yj5hfYkLxWwX7FmnJnsD35dKRqi72C3Dd5Hr2RAavQZXxvxU5gYDcblzrJgQ/7uWPi6F98kff+ZWKeIk" +
                "qSE9ObchnEqL9oKiSOi52KIniUzyHFYhlObUzVkdBFcDshdk4iK5ZzCOlXC32kBSCNaqeYY1dVRdszVx" +
                "gZbwTl6eqoGmk0itFf5J0omnnjxSwm+WxqZwNEqhXnMKHv5bvDyhek0GwZFHZCvuhmoNT2SUY79z4/Ps" +
                "neEYVEjlqUmftUStveiKhUqU1bPaSVPpoDl3UmoH9IrWqUJBInf+z7/g5xv0B0+1PaQGV94i8VAea80Q" +
                "2Z+gLaAk3v4umybKADhkf28eG5m+einuy0c0W/nj5/Jx8xWVrPUIXlMf5BwJUGGZpuv28fFms2lZ41rG" +
                "Lo43+lYf2zv9+bjnUWm8XaucjsH7H4nGWQpbATYDhfv+rPv9i5PX0ukZft4sJbhxA7MylqxHFgutYAIY" +
                "owjguFsp5+QiNG7EugT07mg0/L1zkr+ev3/d7xSV++YaqdHvFHX7/NPl4KrXH3XO8hW89yc349HguvOy" +
                "tnY5uBl3fqqy9Uuv6rz9YlGProeDq/FNpyhF4/7H8aQCGJ1fyybj5u1k1L8Zvh+dQ+JCAYjSvXpzGRif" +
                "nlY07fVKPd8Ne4OLT+V7r3/ZH1c09e/dy0tSdX/vrUfSAKhP/DrIv3Mvqlfq2KN1xS/lTOOe4uI5XYHQ" +
                "5wSgDpFDD+RJM/1LzQhzW60WFSquL4CVv7LEpz+7WPMohwadG758lKM5KvTJKjDa34O2Zy+I4Lvv/kGc" +
                "cOygR2fOs/jRU4ltUsjNYSkTvc6ogkBlCvdIxYpfShEE9VY2FyRFgjwtCuCEvgNT6+IH6Hua7ETIKDoG" +
                "NGIaZghi8oY4FYcAeoy0NNUdNcQLLyGyqrLprFxE/fCrqEJ11LumwsjVcff060rRzAWvU3/AmrFnGKpk" +
                "XOcAbOG1Grk4beA3DzCuaFMOMwIo9J30ASYVDlhq1RHFXOSPOTexsaM3r7twHp4eHMRfxR8nrZPmaevk" +
                "z/29KLMeTWI9h9sQPY8a9y0KKg8rFQFRZbMYU4Z0KSo1YkGFgVoC0ygXZiyqN2zUEvCQ14a23lE8cB3m" +
                "BJqg9cGo9Pjh3OoQxHlMDeeiOWDSpidFt9NSLZShnc6KuiCfpXQq2j9o6VKF9gCwPqQp0SeZtz5Hp1ur" +
                "mZ5rrC7RQDi4kTvGkFgr3Gagpnsga1TwsSGQske7YQMx0CZxt+K+1pkHSbaa+k6QHQ9zoTEWClmLVRjr" +
                "hMYghXiJ80avpChEuxrSaCjj9VLmvfoWnRsJ+1iMQSt/mG+WPLUp1GPZecTO27oAjbT4NEkN93do4bzl" +
                "BK2GyexMhRjiNdBPFDSKIgU5CXGorn7FdqGwhy8d3CSkgEDck1B6ykimkhVa4g5B2WaM+OP7l9UamvJX" +
                "8rwfRrmJwh9uBDlx2B6E8Wa1ArwTfoXYrTDw11woAOjUkXEAYrszfjB/+uvgfpUAuAeYPIHtTs0y6ul5" +
                "kKVSwu0EKoAvkUBdUIBwvDFN6j8XiJZCguLuUt1T50fCStemY37wOrbAHkZSOChy4pDXJnhFx4xzIIVa" +
                "G9zDHUL86226DH3unYTbMPkRZwIQsH1ORM+PqqxJ9DYqUWJy/p5leciX8E1KxqRWk6c/vknNFrAjdq6t" +
                "udOIJzHd+ukjpr4WYDmlQW9/jxGTDwWTCwYZhir2Dd0UOWdmmq8XqX4WQexBT0ffrGOn+kR6dh9MyD77" +
                "gTO1Lr1BQUfLUfiui6uISmdPQwyDW7Fjf+/pC9FvpCbEKdIpXFL4q+RCC56VGfHqSn/BEPKNtAitwqOz" +
                "lbjjj3XR/Tg5SB/ce/JVfiAFZaTRBXnHYWS2XLLpkoBnVcJ6YrKSuP4EAV2Q4L8K1mtwk7ULKS6p/kK/" +
                "4W93eBdXfpYjn2XpaipcqZdTCzEVQUFcJc1f+JGdpfanwVnEJb/zOmpRA0B3BXxLgAcbsNMUFymQjHM7" +
                "NaaR3+14SXaCoxiUEBcpcPvLps+v5PbdihpOBfSinuSPi/JxWj5Kkvy/z3XqFVgaAAA=";
                
    }
}
