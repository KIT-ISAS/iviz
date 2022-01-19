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
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        [DataMember (Name = "secondary_color")] public StdMsgs.ColorRGBA SecondaryColor;
        [DataMember (Name = "scale")] public double Scale;
        [DataMember (Name = "secondary_scale")] public double SecondaryScale;
        [DataMember (Name = "caption")] public string Caption;
    
        /// Constructor for empty message.
        public Widget()
        {
            Id = string.Empty;
            Caption = string.Empty;
        }
        
        /// Explicit constructor.
        public Widget(in StdMsgs.Header Header, byte Action, string Id, byte Type, in GeometryMsgs.Pose Pose, in StdMsgs.ColorRGBA Color, in StdMsgs.ColorRGBA SecondaryColor, double Scale, double SecondaryScale, string Caption)
        {
            this.Header = Header;
            this.Action = Action;
            this.Id = Id;
            this.Type = Type;
            this.Pose = Pose;
            this.Color = Color;
            this.SecondaryColor = SecondaryColor;
            this.Scale = Scale;
            this.SecondaryScale = SecondaryScale;
            this.Caption = Caption;
        }
        
        /// Constructor with buffer.
        public Widget(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Action = b.Deserialize<byte>();
            Id = b.DeserializeString();
            Type = b.Deserialize<byte>();
            b.Deserialize(out Pose);
            b.Deserialize(out Color);
            b.Deserialize(out SecondaryColor);
            Scale = b.Deserialize<double>();
            SecondaryScale = b.Deserialize<double>();
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
            b.Serialize(in Pose);
            b.Serialize(in Color);
            b.Serialize(in SecondaryColor);
            b.Serialize(Scale);
            b.Serialize(SecondaryScale);
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
                int size = 114;
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
        [Preserve] public const string RosMd5Sum = "685794af6acde4c3b87a472d26aa9a35";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UW2/TMBR+96840h62IVbYhYEq7SG0pRRtS0gjpD1VbnKaWErsYDvbwq/n2GnTFobg" +
                "gS2KkuNz+fydi90IaT9AMEpm4e0iGI/hCt6yZlcZT27CbxPSnz6lD66vyXTG1rbkLpos4jAJnMt4Nh/t" +
                "4HnbPIpnt9O15fRpy/nYQ+7Ykjj4MhklYXy3jjzfs4bhdTKLSH2xpw7i6YQ+k4As73YtUTif7RC8/JPN" +
                "E3nPmLHZojK5efMZeYYaCv9jy9Yi8NQKJclFC5mDyDqtbWtkOaoKrW672EgZhJo+W7iRKpWOpx8DSJ30" +
                "lMFgqmTGCaRzWZWK28sLMCkvcbvqvTr9mk3Ka0+Osav//LCb+XQIv9SFHcDccscjA0qcZ9xyWCmql8gL" +
                "1Ccl3mNJQbyqMQNvdXUyAwpMCmGA3hwlal6WLTSGnKyi0lRVI0XKXVlFhXvxFCkkcKi5tiJtSq7JX+lM" +
                "SOe+0rxCh06vwe8NyhRhNh6Sj6SSNVYQoZYQUo3cuIrNxuCH4fzMBbCD5EGd0BJz6nq/OdiCW0cWH2uN" +
                "xvHkZkh7vOqSGxD2cN0TA0det6ClOQbahChgrdICjoh51NpCSQJEuOda8GWJDpiaWBLqoQs6PN5Blh5a" +
                "cqk28B3ido9/gZU9rsvppKCelS570+RUQHKstboXGbkuWw+SlgKlhVIsNQ0Zc1Hdluzgk6sxOVGU7wj9" +
                "uTEqFdSADB6ELTbT6LuxoBPyTNP4+3GjBAPQ6JpE9Lk7C6BW/hC6sVlppDRqnuJrN2VOna3twvtSXUBp" +
                "sYkdAIsUTUPvwL42lKWWHnfr91IJEpXNyaFZsFxI47vV86dc6Gh4ynvp9vfGYy+1vfTjZehvS7fJoW8U" +
                "TdBePffJu9X3bd3pfqkG7C8ZbaSHZ78J+5u725PuEd1LeS8te4kz9hOzVNNlgwcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
