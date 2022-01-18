/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Widget : IDeserializable<Widget>, IMessage
    {
        public const byte ACTION_ADD = 0;
        public const byte ACTION_REMOVE = 1;
        public const byte ACTION_REMOVEALL = 2;
        public const byte TYPE_ROTATIONDISC = 0;
        public const byte TYPE_SPRINGDISC = 1;
        public const byte TYPE_SPRINGDISC3D = 2;
        public const byte TYPE_TRAJECTORYDISC = 3;
        public const byte TYPE_TOOLTIP = 4;
        public const byte TYPE_TARGETAREA = 5;
        public const byte TYPE_POSITIONDISC = 6;
        public const byte TYPE_POSITIONDISC3D = 7;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "main_color")] public StdMsgs.ColorRGBA MainColor;
        [DataMember (Name = "secondary_color")] public StdMsgs.ColorRGBA SecondaryColor;
        [DataMember (Name = "scale")] public double Scale;
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        [DataMember (Name = "caption")] public string Caption;
    
        /// Constructor for empty message.
        public Widget()
        {
            Id = string.Empty;
            Caption = string.Empty;
        }
        
        /// Explicit constructor.
        public Widget(in StdMsgs.Header Header, byte Action, string Id, byte Type, in StdMsgs.ColorRGBA MainColor, in StdMsgs.ColorRGBA SecondaryColor, double Scale, in GeometryMsgs.Pose Pose, string Caption)
        {
            this.Header = Header;
            this.Action = Action;
            this.Id = Id;
            this.Type = Type;
            this.MainColor = MainColor;
            this.SecondaryColor = SecondaryColor;
            this.Scale = Scale;
            this.Pose = Pose;
            this.Caption = Caption;
        }
        
        /// Constructor with buffer.
        public Widget(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Action = b.Deserialize<byte>();
            Id = b.DeserializeString();
            Type = b.Deserialize<byte>();
            b.Deserialize(out MainColor);
            b.Deserialize(out SecondaryColor);
            Scale = b.Deserialize<double>();
            b.Deserialize(out Pose);
            Caption = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Widget(ref b);
        
        public Widget RosDeserialize(ref ReadBuffer b) => new Widget(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(in MainColor);
            b.Serialize(in SecondaryColor);
            b.Serialize(Scale);
            b.Serialize(in Pose);
            b.Serialize(Caption);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Caption is null) throw new System.NullReferenceException(nameof(Caption));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 106;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Id);
                size += BuiltIns.GetStringSize(Caption);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Widget";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e5c26faa1ff9ca708404f6eb6ea3a838";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71U32/TMBB+919x0h62IVbYDwaqtIfQllK0LSGNkPZUuck1sZTYwXa2hb+es9ukLQzB" +
                "A1sVNbbv7vN3392lEdJ+gGCUzMLbRTAewxW8Zc3uYTy5Cb9N6Pz0qfPg+ppMZ2xjS+6iySIOk8C5jGfz" +
                "0Q6et82jeHY73VhOn7acjz3kji2Jgy+TURLGd5vI8z1rGF4ns4iOL/aOg3g6ob9JQJZ3u5YonM92CF7+" +
                "yeaJvGfM2GxRmdy8+Yw8Qw2Ff7FlaxF4aoWS5KKFzEFk61Pb1rgNG6lS6Xj6MYCKC7lI3fYpq8FUyYzr" +
                "duOyKhW3lxdgUl4iy1FVaMnooyJlEGr6665Oee2ZMHb1n3/sZj4dwi8isAOYW+7YZkC0eMYth5UicURe" +
                "oD4p8R5LCuJVjRl4qxPFDCgwKYQBenKUqHlZttAYcrIKUlVVjRQpdxqKCvfiKVJI4FBzbUXalFyTv9KZ" +
                "kM59pXmFDp0eg98blCnCbDwkH0nCNlYQoZYQUo3cOMVmY/CVPz9zAewgeVAntMWcStxfDrbg1pHFx1qj" +
                "cTy5GdIdr9bJDQh7uKmcgSN/tqCtOQa6hChgrdICjoh51NpCSQJEuOda8GWJDpiKWxLqoQs6PN5Blh5a" +
                "cqk6+DXi9o5/gZU9rsvppKCalS570+QkIDnWWt2LjFyXrQdJS4HSQimWmlqRuaj1lezgk9OYnCjKV4Te" +
                "3BiVCipABg/CFl03+mosaByeuxv76VlPC9VS96u8Xy37FX8uRr+PJ0kegEbXNiQod9MJauWH1jXySiMJ" +
                "W/MUX7u+d8fZxi68L1UKlBZd7ABYpKg/ewf2tSHdtfS4W7+XSpCodLNM3Wnpy2Z8//T8KRcaVk95L93+" +
                "u/bYr9p+9eNl6G+l63LoC0U9vafnPnm3+77Vnb541YD9JaNu9cDYTyymTadwBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
