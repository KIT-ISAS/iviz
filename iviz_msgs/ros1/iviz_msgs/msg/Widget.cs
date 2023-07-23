/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Widget : IHasSerializer<Widget>, IMessage
    {
        public const byte ACTION_ADD = 0;
        public const byte ACTION_REMOVE = 1;
        public const byte ACTION_REMOVEALL = 2;
        public const byte TYPE_TOOLTIP = 0;
        public const byte TYPE_ROTATIONDISC = 1;
        public const byte TYPE_SPRINGDISC = 2;
        public const byte TYPE_SPRINGDISC3D = 3;
        public const byte TYPE_TRAJECTORYDISC = 4;
        public const byte TYPE_TRAJECTORYDISC3D = 5;
        public const byte TYPE_TARGETAREA = 6;
        public const byte TYPE_POSITIONDISC = 7;
        public const byte TYPE_POSITIONDISC3D = 8;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        [DataMember (Name = "second_color")] public StdMsgs.ColorRGBA SecondColor;
        [DataMember (Name = "scale")] public double Scale;
        [DataMember (Name = "second_scale")] public double SecondScale;
        [DataMember (Name = "caption")] public string Caption;
        [DataMember (Name = "second_caption")] public string SecondCaption;
    
        public Widget()
        {
            Id = "";
            Caption = "";
            SecondCaption = "";
        }
        
        public Widget(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            Id = b.DeserializeString();
            b.Deserialize(out Type);
            b.Deserialize(out Pose);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            b.Deserialize(out Scale);
            b.Deserialize(out SecondScale);
            Caption = b.DeserializeString();
            SecondCaption = b.DeserializeString();
        }
        
        public Widget(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Action);
            b.Align4();
            Id = b.DeserializeString();
            b.Deserialize(out Type);
            b.Align8();
            b.Deserialize(out Pose);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            b.Align8();
            b.Deserialize(out Scale);
            b.Deserialize(out SecondScale);
            Caption = b.DeserializeString();
            b.Align4();
            SecondCaption = b.DeserializeString();
        }
        
        public Widget RosDeserialize(ref ReadBuffer b) => new Widget(ref b);
        
        public Widget RosDeserialize(ref ReadBuffer2 b) => new Widget(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(in Pose);
            b.Serialize(in Color);
            b.Serialize(in SecondColor);
            b.Serialize(Scale);
            b.Serialize(SecondScale);
            b.Serialize(Caption);
            b.Serialize(SecondCaption);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Align4();
            b.Serialize(Id);
            b.Serialize(Type);
            b.Align8();
            b.Serialize(in Pose);
            b.Serialize(in Color);
            b.Serialize(in SecondColor);
            b.Align8();
            b.Serialize(Scale);
            b.Serialize(SecondScale);
            b.Serialize(Caption);
            b.Align4();
            b.Serialize(SecondCaption);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 118;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetStringSize(Caption);
                size += WriteBuffer.GetStringSize(SecondCaption);
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Action
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size += 1; // Type
            size = WriteBuffer2.Align8(size);
            size += 56; // Pose
            size += 16; // Color
            size += 16; // SecondColor
            size = WriteBuffer2.Align8(size);
            size += 8; // Scale
            size += 8; // SecondScale
            size = WriteBuffer2.AddLength(size, Caption);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, SecondCaption);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/Widget";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "77f95c4e46c2856edea860d6601bd4da";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UW2/TMBR+96840h62IVbYhTFV2kNoSyna1pJGSBNClZucJpYSO7OdbeHXc+ykaQub" +
                "4IFRRc3JuXznOxe7EtJeQDCIJtObRTAcwiW8ZdW2MhxdT7+OSH/8lD64uiLTCWtt0e1stIim06toMtuC" +
                "8upwGgUucjiZD7bgvG0+Cyc349Zy8rTl1JE73UkUBp9Hg2ga3raRZ89bffS7HXsQjkf0NwrIcr5tmU3n" +
                "ky2m75+zecwLxoxNFoVJzZtPyBPUkPlXG8VjK5QkHy1kCiJp1bYukaWoCrS6bqJnyiCU9LcBHKhc6XD8" +
                "IYDYSU8ZDMZKJovGvsoVt+dnYGKe4+arcWmULZGYl9u81iitll3+4x+7no/78Euj2B7MLZcJ1wlQH3jC" +
                "LYeVogaKNEN9lOM95hTEixIT8FbXNtOjwCgTBuhJUaLmeV5DZcjJKupUUVRSxNwiWFHgTjxFCgkcSq6t" +
                "iKuca/JXOhHSua80L9Ch02PwrkIZI0yGffKR1KHKCiJUE0KskRvXt8kQ/EBPT1wA7MG3UJnj72wvelBH" +
                "pMeU9qFjATbj1rHGx1KjcYS56VOyV02VPUrSb2dh4MDrFvRpDoGyERcsVZzBAZUwq22mJAEi3HMt+DJH" +
                "B0wjzgl13wXtH24hSw8tuVRr+AZxk+NvYGWH62o6ymh4uV+fKqVOkmOp1b1IyHVZe5A4Fygt5GKpua6Z" +
                "i2pSsr2PrtnkRFF+NPTmxqhY0CQSeBA2Wy+nH8uCjs4LreXvx5AKDECjGxLR5+5MgFr5w+n2Z6WRyih5" +
                "jK/dujl10tqF96W+gNJiHdsDNlO0DZ0D+1JRlVp63I3f/yqQqKyPEO2C5UIaP62OP9VCZ8RT3im3u1Ie" +
                "O6nupB//h/6mdesaukHRBu30c5e8+7rb9J0umqLH/lDRWnp48Suxu9GbnHSh6E5KO2nZSZyxn0w2rIm9" +
                "BwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Widget> CreateSerializer() => new Serializer();
        public Deserializer<Widget> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Widget>
        {
            public override void RosSerialize(Widget msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Widget msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Widget msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Widget msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Widget msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Widget>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Widget msg) => msg = new Widget(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Widget msg) => msg = new Widget(ref b);
        }
    }
}
