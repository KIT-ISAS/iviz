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
        [Preserve] public const string RosMd5Sum = "80b7738e5376e3779af4f631adfb2c57";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WW28aRxR+319xJD/Ermza2GkSWcrDGqhD6hgKNJJVVWjYPSyjLjObmVkw/vX9ZnZZ" +
                "lpQqfaiNEMyeyzfnfraUyr2nuDsdDO9nca9HH+inqGwTx/3Pwy990F8fo8d3d2BdRjVv+jDqz8bDaexF" +
                "eoNJt4UXeJPReHB/W3NeH+dc9QJkizcdx5/63elw/FBrXh1wh8O76WAE8psDcjy+7eOnH4Pzc5szGk4G" +
                "LQPf/hsvGPKuzb0Z/n7fi8cP3Y/97q9gvo8i69LZymb2x48sUja0DH/RfOuYROKkVhAxUmUk04rqtgVH" +
                "GesVO7OtdEfaMhX42cN1da7N+PYmpsSfjjEsJ1qlAiCVyCLXwr19QzYROe+fGqmKXluTiCIYd6NLlYJw" +
                "ox9p7s+QbBMnTqwKTv/4swVUy0m2UfThf/5Enye31/RNVKMTgh3+zpQQNpEKJ2ihEW2ZLdlc5LzmHErB" +
                "VApcH2XbgeJ0KS3hm7FiI/J8S6WFkNMI7GpVKpkInxS54gN9aEpFggphnEzKXBjIa4OoePGFESv26Pha" +
                "/lqySpgGvWvIKMSpdBIGbYGQGBbWx3vQo1BJV5deITqZbvQFHjlDzTSXk1sK543lx8Kw9XYKe407fqic" +
                "6wD7uk6EpdNAm+HRnhEugQlc6GRJp7B8tHVLrQDItEaqxDxnD4wSyIH6yiu9OmshqwCthNI7+Apxf8d/" +
                "gVUNrvfpYomc5d57W2YIIAQLo9cyheh8G0CSXLJylMu58YXntaoro5NffIwhBK2QEfwLa3UikYCUNtIt" +
                "d7UcsjFDfz1TNf6zWeFgTIZ9kmC+8J1EehFa2JfNwjDcKETC577KPDmt+TLIIi6kjdzpdigaaVRDIxD9" +
                "VsJLowLuXu6lHIQpu85BLTghlQ3ZauyHL2iNYPKBu83UeWxO2+b09DLm70O386FJFCroIJ6Hxvunr/u4" +
                "Y76sOtF3PNqdNs8+CZu5X92JOWKaU9ac5s1JPJdFci2fKpNaa+LYQksQZszuQ84XTpw2V5guT/wy9VDf" +
                "eKwYaB14h2XQ8UN9EMawVhjiKxYoc+yLRhOKqTScVK07xQZiFAt6XTpKNVtS2vfPSvwFSMZM9NqiKACG" +
                "xWSEsnlVfiBD5ZQ7WeecNktWlZSfaWEDhZ0lEzIyk2ml6auyURZUO3dObnGJmZjnlc3VZWhZgBhdFftZ" +
                "hwYL2uqSNt4hHEy9KjXNubErjHSn9bnfkzXEkfmAsFgrMt801mFJf7dTnifVR4uxfmeJDl/Jjr7pRH8D" +
                "Wd91FQILAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
