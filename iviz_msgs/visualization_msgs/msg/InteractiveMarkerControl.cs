/* This file was created automatically, do not edit! */

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
    
        /// Constructor for empty message.
        public InteractiveMarkerControl()
        {
            Name = "";
            Markers = System.Array.Empty<Marker>();
            Description = "";
        }
        
        /// Constructor with buffer.
        public InteractiveMarkerControl(ref ReadBuffer b)
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new InteractiveMarkerControl(ref b);
        
        public InteractiveMarkerControl RosDeserialize(ref ReadBuffer b) => new InteractiveMarkerControl(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetArraySize(Markers);
                size += BuiltIns.GetStringSize(Description);
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarkerControl";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b3c81e785788195d1840b86c28da1aac";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71YbXPbNhL+XP0KTDyd2FeZfktzre78QbHkRFPb8klKmkyno4FISEJNEQpAWlZ+/T27" +
                "ACnKspv7cInjiUkQ2Pd9dhd7YqCWVjmV5U5IEZsstyYV+VzmQjuRGzFRItFumcq1SvA+U/lcWbHS+VzI" +
                "TOgsV1bGub5XYiHtnbKNxp7oJaCnp2udzYTLLf2ZGguqIBlYRNj2yRQiU0xWSOf0LIMIRaY/F0rcyxT/" +
                "4wMfwl+rYkVcpjgwkfGdmFqzwFcl3r7vgZjJxIqkJmFM5vhL4SDqUlkwXzjaUZdA7KtoFoHjpMhzfItT" +
                "Hd8dRI0gcCYXqkHKdNRUZ8oTTE0sUxAwNtGZzCGNxTaxb1Uq2QYssBJL45QwU/8sLawBQru2OsAapNbx" +
                "nIw9UcTXmhyEExgXdrEycym9Rl4QWaR5K5g3XzdmyixUbtfjhZu5o/8U2GkzaC+M1dgiyRKsQ3/zLhYm" +
                "Ua3SCE7Mzaq+XcRzmc2UI4a9m3fdQW/UEpcmTR9tg3JP+H5PXPY+djst8ZtSy639U/1ASiGqMp1rGNGR" +
                "mjjwodf9fXzZvujdvG2JdkpBsD78IhBwmfJh5mKrVCb2H1oURStpk6ZYt1I1zZviS6tYwmcFZPmllFec" +
                "i2MRllgcLJyE9xo3rJ42wnJN0jHZh4O4VC/YbCeEsQe/N/2bbkuM6oGlKdbStVgomeV87F67Qqb6C3P4" +
                "l8gM71UPOfZkBdn6unvzviWu9J1iik2KSmH1bJ4fcmDyRqLsDU5H3rwfjfo3LdFNFT7mIkY+IlvJMP6M" +
                "D5vr/ofuuP2xN4SUZTwJmRrEmg/nh0P5oF219faqzRpVexGjfmPlF9o76I/ao5LwgINWSGsKhO2TZP3+" +
                "lrgwiwnn04YZx3qNXulPMkTdmWSjmi+9/uzG8L3UFGtnor7o2ZyLV2GxxgyrP9e3+k9YfQ3JX5x1Xmzi" +
                "PASCEytj73xsUoIvDHDmp+G73uXop4vR4ArR5D+edURcWGfsxgpnnbplpwhsxAkMjK1uKeO6ZWlrsOvu" +
                "PiEnBgFC7BG7M80J6ZHGY9IjuzO1yyJNxevDTv+SKSbAT8KoIA6pR45gAOKXioXXInCK6tY6o+T657ZV" +
                "ee2XXZv6D7+WOVcza8g5SjqSp1BNzxY5IhErSYAXGF5DA5k6LktIKj1JCUNWc5XxiVmhKUcyQzizDVDg" +
                "EDUmxtD5lVy7cXmc2F4H+o/rnURSFy6HoXwGowqFaskmIiNfKUm4T/mvFst8TTQQDyxO4hG7AgZAa5Iy" +
                "thJwmBw4jIdDoIeqVAxZnHDVSUgNLEg70fCTXe/UniYICAYLtg1Osj+p4vHpUJB8SvpqFerSLnxHLEzP" +
                "f54rmaB6YrMMn0WpYlNodAtc40qmTAymyb3RWKivF8UnZfC++OPP0iI+LrIt6CZvNoVTedUeUNQIPRVr" +
                "9BSJyV7CHgS++cayoOLdK6nIQM4qgWNYxUq4WK0gJGSKtjzCOjqqk8USRKAenFLWmlpc6SxRS5VRcR77" +
                "s+PHdXg4NzaHb1HS9JKTbP/f4tUxFV0yA7gdkIW4kdnqVRKjHLmaW5YX1+beB5g1E5O/iES9O2iLmcqU" +
                "1fEWn4l0UNlk264PiFb2PLUDjcb5//mncT1EhX+uZYEGXENtrR3d6mPI6IRaAf/w9nnT71C4R43GNDUy" +
                "f/1KPFRP6+rpS/W0+la6bVV5r+B12RcNIfk8z5eto6PVahVZ4yJjZ0crfaeP7L3+ctTxoDNaL1V5jOH4" +
                "bw+NihwmAiCGE+7Hs/aPp8dvpNMx/g7nEtS4A1kYS0YjQ4UGLgNMkdM5zhbKOTkLDReRLlG6PRj0fz8/" +
                "Dm8X7990z8v6O7xFCnTPy+p78emqd9PpDs7PwgJeu+PhaNC7PX9VX7rqDUfnP9co+pXXW2T9WllZbvu9" +
                "m9HwvCwqo+7H0biGB+e/Vh3C8N140B323w8uIGgpNmRo37y9CkRPTirlOp1Ktet+p3f5qXrtdK+6o41y" +
                "/rV9dQXtGu88NAaEfOZnr/zObaNeqCOPvjUfVKOGe46IJ3SDcz7qAWAIkjjUGzP5S8UEolEUUc3hagHE" +
                "+KvIfG6zNzXPV+ihuUMr5ysab0JHqwKhBjQ9O6X9P/zwN9IErr0OsZwW6ZNMiWpWic0BKDO9LKgiQGMK" +
                "7ESlKvcBWEogqCuyQY4cmfC8JIAL+g6w3BI+gNrzp46FTJIjgB7GU0YYPt0UJ2I/IeiJado6aIpTL59y" +
                "9U1nm0WUBL/qHk1ht1TluNTtMr+tVcAg9vbhD1gy9gwjj0zVo8NDXts6LU6a+Mdzhquajf2CUAjtIn2A" +
                "OYUDUFp1gGhLPJcLkxo7ePumDb/h6REf/ir+OI6OD0+i4z8bSWE9YqR6qiiQnzbsO9RHHipq4qFoFinm" +
                "AelyFN4pQZAfciVwi3IgZkG9UZNIwDteF9p6j0jgusp5M0YHg4Hmad7cshCKedgMbCehRTr0R9G1RCpC" +
                "gdlpkKib8clJTNecrS5XS1TsPs1xPrW84Tkq3VLFeqqxOkc34OBBbvlCOi3EvkKV9pjVrMFgUyBRD3bi" +
                "BTKg2+HGw30bjntZsZj4Zo49DkuhqxVK810O7HRMM4v6TB1uaNY2J0rBbvo0vsl0OZdlj71GA0aiPhFa" +
                "0MizavCMTEdNpRmLzdNvaM0CENLac/u3oP3xQfhrPkbfYAobKx81vITTYwVFkkRBQAIXKpbfqv5XVvAF" +
                "gqp+DqSTNqFMlInMJasyx1iv7GGKaON7kMUSOvJX8jUPi9wL4ZdbOU4SNgQBuVksgOGEUyFQa+f5lgkg" +
                "j/YayQW0tTsDQ+SvLRy8rTJgc6/Dl0FOxQW14TxmUrHg3gAgzwUQyIoDjb3RyhxS8zhTdsO8ui1UD9S7" +
                "OR4BWuDxD69cBNowjgKXxIl9XhvjFb0umEAEtTTxXOxD8tt1Pg896r2ErzCfEWGCCVB9SYdeHtQoZ0w6" +
                "k5kpyXuKGx7/C9msoks6HfKUxleXxUzy1LO05l4jhMRk7QeGlNpSAOKEZrIGgyKzbOxdMo4wGrFH6MbG" +
                "ORNrvtSj2ljGrIc1nXyfTptKDxRsP5pgfYI7td1dNynKaDkJ33V1O1DryDF2MHhVGxrP3UB+HwU137JW" +
                "N3FSh1vgSn6eZRnOttX9ytzwXcQPlf+pMUjc87dtmXnm6+WP7hr5njycxMFEo6HxrsJAa7kC0/DO4yQh" +
                "OGgs5B1IqsxxdymXSxCTW5dCXCH9bXnTX7XwLq7jJEU5btLtULK5fqgOSxGUa4p8eurHaZbZM4OLQKS8" +
                "dTqIqJrTFM/zOx5sgEZTXW5ALk7h3JhmedfCcuzEQzXXIBZygPJXZ8Rv4+rd8uhZAlZt9TSrnibVk2w0" +
                "/gtC63d+ohkAAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
