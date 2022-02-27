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
        public const byte TYPE_BOUNDARY = 8;
        public const byte TYPE_BOUNDARYCHECK = 9;
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
        [DataMember (Name = "boundary")] public BoundingBox Boundary;
        [DataMember (Name = "secondary_boundaries")] public BoundingBoxStamped[] SecondaryBoundaries;
    
        /// Constructor for empty message.
        public Widget()
        {
            Id = "";
            Caption = "";
            Boundary = new BoundingBox();
            SecondaryBoundaries = System.Array.Empty<BoundingBoxStamped>();
        }
        
        /// Explicit constructor.
        public Widget(in StdMsgs.Header Header, byte Action, string Id, byte Type, in GeometryMsgs.Pose Pose, in StdMsgs.ColorRGBA Color, in StdMsgs.ColorRGBA SecondaryColor, double Scale, double SecondaryScale, string Caption, BoundingBox Boundary, BoundingBoxStamped[] SecondaryBoundaries)
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
            this.Boundary = Boundary;
            this.SecondaryBoundaries = SecondaryBoundaries;
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
            Boundary = new BoundingBox(ref b);
            SecondaryBoundaries = b.DeserializeArray<BoundingBoxStamped>();
            for (int i = 0; i < SecondaryBoundaries.Length; i++)
            {
                SecondaryBoundaries[i] = new BoundingBoxStamped(ref b);
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
            Boundary.RosSerialize(ref b);
            b.SerializeArray(SecondaryBoundaries);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference(nameof(Id));
            if (Caption is null) BuiltIns.ThrowNullReference(nameof(Caption));
            if (Boundary is null) BuiltIns.ThrowNullReference(nameof(Boundary));
            Boundary.RosValidate();
            if (SecondaryBoundaries is null) BuiltIns.ThrowNullReference(nameof(SecondaryBoundaries));
            for (int i = 0; i < SecondaryBoundaries.Length; i++)
            {
                if (SecondaryBoundaries[i] is null) BuiltIns.ThrowNullReference($"{nameof(SecondaryBoundaries)}[{i}]");
                SecondaryBoundaries[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 198;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Id);
                size += BuiltIns.GetStringSize(Caption);
                size += BuiltIns.GetArraySize(SecondaryBoundaries);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Widget";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a71dfa7c0575b3eb185fffdf20b1768b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WW2/iRhR+9684Uh42qRLaTba720j74ADN0mYDC3SlqKrQYB/MqGbGOzOGkF/fb8bG" +
                "mJZq+9AEIRifyzfnflxK5d5T3J0OhvezuNejD/RDVLaJ4/6n4Zc+6K+P0eO7O7Auo5o3fRj1Z+PhNPYi" +
                "vcGk28ILvMloPLi/rTmvj3OuegGyxZuO41/63elw/FBrXh1wh8O76WAE8psDcjy+7eOnH4PzY5szGk4G" +
                "LQPf/hsvGPKuzb0Z/nbfi8cPoL8/Ru9+7Hd/BfOnKLIuna1sZr//yCJlQ8vwF823jkkkTmoFESNVRjKt" +
                "qG5bcJSxXrEz20p3pC1TgZ89XFfn2oxvb2JK/OkYw3KiVSoAUoksci3c2zdkE5Hz/qmRqui1NYkognE3" +
                "ulQpCDf6keb+DMk2ceLEquD09z9aQLWcZBtFH/7nT/RpcntNf4tqdEKww9+ZEsImUuEELTSiLbMlm4uc" +
                "15xDKZhKgeujbDtQnC6lJXwzVmxEnm+ptBByGoFdrUolE+GTIld8oA9NqUhQIYyTSZkLA3ltEBUvvjBi" +
                "xR4dX8tfS1YJ06B3DRmFOJVOwqAtEBLDwvp4D3oUKunq0itEJ9ONvsAjZ6iZ5nJyS+G8sfxYGLbeTmGv" +
                "ccd3lXMdYF/XibB0GmgzPNozwiUwgQudLOkUlo+2bqkVAJnWSJWY5+yBUQI5UF95pVdnLWQVoJVQegdf" +
                "Ie7v+C+wqsH1Pl0skbPce2/LDAGEYGH0WqYQnW8DSJJLVo5yOTe+8LxWdWV08rOPMYSgFTKCf2GtTiQS" +
                "kNJGuuWulkM2ZuivZ6rGfzYrHIzJsE8SzBe+k0gvQgv7slkYhhuFSPjcV5knpzVfBlnEhbSRO90ORSON" +
                "amgEos8lvDQq4O7lXspBmLLrHNSCE1LZkK3GfviC1ggmH7jbTJ3H5rRtTk8vY/4+dDsfmkShgg7ieWi8" +
                "f/q6jzvmy6oTfcOj3Wnz7JOwmfvVnZgjpjllzWnenMRzWSTX8qkyqbUmji20BGHG7D7kfOHEaXOF6fLE" +
                "L1MP9Y3HioHWgXdYBh0/1AdhDGuFIb5igTLHvmg0oZhKw0nVulNsIEaxoNelo1SzJaV9/6zEn4BkzESv" +
                "LYoCYFhMRiibV+UHMlROuZN1zmmzZFVJ+ZkWNlDYWTIhIzOZVpq+KhtlQbVz5+QWl5iJeV7ZXF2GlgWI" +
                "0VWxn3VosKCtLmnjHcLB1KtS05wbu8JId1qf+z1ZQxyZDwiLtSLzTWMdlvQ3O+V5Un20GOt3lujwlezo" +
                "m070F6vjDIcaCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
