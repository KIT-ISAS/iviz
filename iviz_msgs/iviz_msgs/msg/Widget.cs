/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Widget : IDeserializable<Widget>, IMessage
    {
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
        internal Widget(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new Widget(ref b);
        
        Widget IDeserializable<Widget>.RosDeserialize(ref Buffer b) => new Widget(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(ref MainColor);
            b.Serialize(ref SecondaryColor);
            b.Serialize(Scale);
            b.Serialize(ref Pose);
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
        [Preserve] public const string RosMd5Sum = "3b0c5b861de617f50925ad80644446b3";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwW4TMRC9+ytG6qEtIkECxCESh0JF6QEp0N6jiXeya8lrb21v0uXreXayGwJIcKCN" +
                "Vtnxeub5zZsZx1St2ljHV5+FKwnUlJdaD0mIdTLeqZiCcTWZav81DZ2oKeyjtz58u/lwRS0bt9J5+afd" +
                "KNq7isNwcNlYz+ndW4qarahafCsJmyVq6aNQh7/xaM1dYaLU+//8U1/ubhb0iwjqjO4SZ7YVgRZXnJg2" +
                "HuKYupEws7IViyBuO6mo7GZR4hyB942JhKcWJ4GtHaiPcEqetG/b3hnNWUPTykk8Io0jpo5DMrq3HODv" +
                "Q2Vcdt8EbiWj44ny0IvTQrfXC/g4CNsnA0IDEHQQjlmx22tSvXHpzescoM7ud36GpdQo8XQ4pYZTJiuP" +
                "XZCYeXJc4IwX++TmwF4cKhfponxbYRkvCYeAgnReN3QB5sshNd4BUGjLwfDaSgZGcS1Qz3PQ+eVPyJn2" +
                "ghw7P8LvEY9n/Ausm3BzTrMGNbM5+9jXEBCOXfBbU8F1PRQQbY24RNasA1pR5aj9kersU9YYTogqFcGb" +
                "Y/TaoAAV7Uxqxm4s1VhhHJ66G6fp2U8LanmYG1j1ZK0ni5+K0e/jCcmvKEhuGwjKeTrJb8rQ5kbeBIGw" +
                "HWt5mfs+f64O+6b4olLkgxlj56SWHv05OaivPXQPruAe/Z4rQVAZZxndmXCzxdI/E3/kgmEtlE/SVeO9" +
                "9jhZw2R9fx76R+nGHKZCoadP9Dwln1cPR91x47Vz9ZeMRmun1A88lQDkSwYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
