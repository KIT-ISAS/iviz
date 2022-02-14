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
        public const byte TYPE_BOUNDARYCHECK = 8;
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
        [DataMember (Name = "bounding_boxes")] public BoundingBox[] BoundingBoxes;
    
        /// Constructor for empty message.
        public Widget()
        {
            Id = "";
            Caption = "";
            BoundingBoxes = System.Array.Empty<BoundingBox>();
        }
        
        /// Explicit constructor.
        public Widget(in StdMsgs.Header Header, byte Action, string Id, byte Type, in GeometryMsgs.Pose Pose, in StdMsgs.ColorRGBA Color, in StdMsgs.ColorRGBA SecondaryColor, double Scale, double SecondaryScale, string Caption, BoundingBox[] BoundingBoxes)
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
            this.BoundingBoxes = BoundingBoxes;
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
            BoundingBoxes = b.DeserializeArray<BoundingBox>();
            for (int i = 0; i < BoundingBoxes.Length; i++)
            {
                BoundingBoxes[i] = new BoundingBox(ref b);
            }
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
            b.SerializeArray(BoundingBoxes);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Caption is null) throw new System.NullReferenceException(nameof(Caption));
            if (BoundingBoxes is null) throw new System.NullReferenceException(nameof(BoundingBoxes));
            for (int i = 0; i < BoundingBoxes.Length; i++)
            {
                if (BoundingBoxes[i] is null) throw new System.NullReferenceException($"{nameof(BoundingBoxes)}[{i}]");
                BoundingBoxes[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 118;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Id);
                size += BuiltIns.GetStringSize(Caption);
                size += BuiltIns.GetArraySize(BoundingBoxes);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Widget";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "50349c7140fd26cc7ff97ad4814cb3f0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71V32/bNhB+119xQB6aDIm3Jl1bBOiDYnuptzR2ba9AMAwGLZ1lYhKpkpQd56/fR0qW" +
                "7dVD97DEMGzyfnz87nh3rKRy7ynuTgfD+1nc69EH+imq9oXj/qfhlz7kr4/J47s7qC6jRjd9GPVn4+E0" +
                "9ia9waS7hxd0k9F4cH/baF4f11z1AuSebjqOf+13p8PxQ+N5daAdDu+mgxHEbw7E8fi2j59+DM3P+5rR" +
                "cDLYI/j233SByLt97c3w9/tePH7ofux3f4PyfRRZl84Km9kfP7JI2dAy/EXzjWMSiZNawcRIlZFMa6nb" +
                "lBxlrAt2ZlP7jrRlKvGzg+vqXJvx7U1MiV8dU1hOtEoFQGqTRa6Fe/uGbCJy3u1aq1resElEGcjd6Eql" +
                "ENzoxz/+pHmzm831I9so+vA/f6JPk9tr+kfOohOaOOE5poSkiFQ4QQuNXMpsyeYi5xXncBJFySkFrc+h" +
                "7cBxupSW8M1YsRF5vqHKwshppK0oKiUT4VMuCz7wh6dUJKgUxsmkyoWBvTYI3ZsvjCjYo+Nr+WvFKmEa" +
                "9K5ho5DOykkQ2gAhMSysz+agR6FOri69Q3QyXesLbDlDRbSHk1sK58nyY2nYep7CXuOMH+rgOsC+bu7L" +
                "0mmQzbC1Z4RDQIFLnSzpFMxHG7fUCoBMK2GkmOfsgXHBOVBfeadXZ3vIKkArofQWvkbcnfFfYFWL62O6" +
                "WOLOch+9rTIkEIal0SuZwnS+CSBJLlk5yuXcoAAj71UfGZ384nMMI3iFG8G/sFYnEheQ0lq65bZSw23M" +
                "0D3PVI3ftiICjMmwvyTQF75PSC9Cg/qyWRhGGKVI+NxXmRenjV4GW+SFtJFb3w5FI41qaA2izxWiNCrg" +
                "7uxeKkBQ2XYOasEJqWy4rZY/YkFrBMoH4bYz5bFdbdrV08vQ36VuG0N7Uaigg3wekve7r7u8Y74Uneg7" +
                "EW1X62efhO1Ur8/EHDHtKmtX83YlnouRXMmnmtLeyxAdPm5HHq8ESf9G84UTp80VZs0Tv0x1NCceKw1a" +
                "Bd1hUXT8iB+EoawVRnrBAkWP16P1hGMqDSd1I0/xHjFKB50vHaWaLSntu6kQfwGSMSG9tyhLgOGZMkLZ" +
                "vC5GiOFyyp2sc07rJavayk+48B6FF0wmZGQm09rT12jrLKgJ7pzc4hITMs9rzvVhaGCAGF2X/lmHBgva" +
                "6IrWPiAsTPNwappzyysMeKf1uX81G4gj0wJpsVZkvoWsQwl8t2/+BlMvTjZXCgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
