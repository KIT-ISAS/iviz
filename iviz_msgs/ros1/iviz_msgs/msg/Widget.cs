/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Widget : IDeserializable<Widget>, IMessage
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
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Deserialize(out Pose);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            b.Deserialize(out Scale);
            b.Deserialize(out SecondScale);
            b.DeserializeString(out Caption);
            b.DeserializeString(out SecondCaption);
        }
        
        public Widget(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.Align4();
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Align8();
            b.Deserialize(out Pose);
            b.Deserialize(out Color);
            b.Deserialize(out SecondColor);
            b.Align8();
            b.Deserialize(out Scale);
            b.Deserialize(out SecondScale);
            b.DeserializeString(out Caption);
            b.Align4();
            b.DeserializeString(out SecondCaption);
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
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Caption is null) BuiltIns.ThrowNullReference();
            if (SecondCaption is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 118;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetStringSize(Caption);
                size += WriteBuffer.GetStringSize(SecondCaption);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c += 1; // Action
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Id);
            c += 1; // Type
            c = WriteBuffer2.Align8(c);
            c += 56; // Pose
            c += 16; // Color
            c += 16; // SecondColor
            c = WriteBuffer2.Align8(c);
            c += 8; // Scale
            c += 8; // SecondScale
            c = WriteBuffer2.AddLength(c, Caption);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, SecondCaption);
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Widget";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "72f7b419f49008628556f9e63a819859";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UW2/aMBR+9684Eg9tp5Wtl3UVUh8yYIyqLSxEk6ppQiY5JJYSO7Wdttmv37EDAbZW" +
                "28NaFJGTc/nOdy52JaQ9h6AfjSc382AwgAt4z6ptZTi8nnwbkv7oKX1wdUWmY7ayRbfT4TyaTK6i8XQL" +
                "yqvDSRS4yMF41t+C87bZNBzfjFaW46ctJ47cyU6iMLgc9qNJeLuKPH3e6qM/7NiDcDSkv2FAlrNty3Qy" +
                "G28x/ficzWOeM2ZsMi9Mat59QZ6ghsy/2KK2CDy2Qkly0UKmIJJGa+sSWYqqQKvrJnaqDEJJfxu4vsqV" +
                "DkefAoid9JTBYKxkMm/sy1xxe3YKJuY5br4al0a54hHzcpvWGmWlZRf/+ceuZ6Me/NYm1oGZ5TLhOgHq" +
                "A0+45bBU1D6RZqgPc7zHnIJ4UWIC3uraZroUGGXCAD0pStQ8z2uoDDlZRZ0qikqKmLsuiwJ34ilSSOBQ" +
                "cm1FXOVck7/SiZDOfal5gQ6dHoN3FcoYYTzokY+kDlVWEKGaEGKN3Li+jQfgd+Pk2AVAB76Hyhz9YJ3o" +
                "QR2SHlPahpYF2IxbxxofS43GEeamR8neNFV2KUlvNQsD+143p09zAJSNuGCp4gz2qYRpbTMlCRDhnmvB" +
                "Fzk6YBpxTqh7LmjvYAtZemjJpVrDN4ibHP8CK1tcV9NhRsPL/fpUKXWSHEut7kVCrovag8S5QGkhFwvN" +
                "dc1cVJOSdT67ZpMTRfnR0Jsbo2JBk0jgQdhsvZx+LHM6OS+0ln8eQyowAI1uSESfuzMBaukPp9ufpUYq" +
                "o+QxvnXr5tTJyi68L/UFlBbr2C6wqaJtaB3Y14qq1NLjbvxeq0Cisj5CtAuWC2n8tFr+VAudEU95p9z2" +
                "SnlspbqVfr4O/U3r1jW0g6IN2unnLnn3dbfpO100RZf9paK19PDiV2J7ozc56ULRrZS20qKVOGO/AOuh" +
                "srW7BwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
