/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class Expression : IDeserializable<Expression>, IHasSerializer<Expression>, IMessage
    {
        // Represents a human facial expression, either in a categorical way, or
        // using the valence/arousal model of emotions
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // the list of expressions is based on Chambers MSc thesis, Bristol Robotics Lab 2020, and includes the six basic emotions in Eckman's model.
        //
        // Note that a node making use of this message definition is not supposed or
        // expected to handle all listed expressions.
        //
        // This list might change based on future needs/requests.
        public const string NEUTRAL = "neutral";
        public const string ANGRY = "angry";
        public const string SAD = "sad";
        public const string HAPPY = "happy";
        public const string SURPRISED = "surprised";
        public const string DISGUSTED = "disgusted";
        public const string SCARED = "scared";
        public const string PLEADING = "pleading";
        public const string VULNERABLE = "vulnerable";
        public const string DESPAIRED = "despaired";
        public const string GUILTY = "guilty";
        public const string DISAPPOINTED = "disappointed";
        public const string EMBARRASSED = "embarrassed";
        public const string HORRIFIED = "horrified";
        public const string SKEPTICAL = "skeptical";
        public const string ANNOYED = "annoyed";
        public const string FURIOUS = "furious";
        public const string SUSPICIOUS = "suspicious";
        public const string REJECTED = "rejected";
        public const string BORED = "bored";
        public const string TIRED = "tired";
        public const string ASLEEP = "asleep";
        public const string CONFUSED = "confused";
        public const string AMAZED = "amazed";
        public const string EXCITED = "excited";
        [DataMember (Name = "expression")] public string Expression_;
        // Valence/Arousal model of emotions
        /// <summary> From -1.0 to +1.0 </summary>
        [DataMember (Name = "valence")] public float Valence;
        /// <summary> From -1.0 to +1.0 </summary>
        [DataMember (Name = "arousal")] public float Arousal;
        /// <summary> From 0.0 to 1.0 </summary>
        [DataMember (Name = "confidence")] public float Confidence;
    
        public Expression()
        {
            Expression_ = "";
        }
        
        public Expression(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Expression_);
            b.Deserialize(out Valence);
            b.Deserialize(out Arousal);
            b.Deserialize(out Confidence);
        }
        
        public Expression(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeString(out Expression_);
            b.Align4();
            b.Deserialize(out Valence);
            b.Deserialize(out Arousal);
            b.Deserialize(out Confidence);
        }
        
        public Expression RosDeserialize(ref ReadBuffer b) => new Expression(ref b);
        
        public Expression RosDeserialize(ref ReadBuffer2 b) => new Expression(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Expression_);
            b.Serialize(Valence);
            b.Serialize(Arousal);
            b.Serialize(Confidence);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Expression_);
            b.Serialize(Valence);
            b.Serialize(Arousal);
            b.Serialize(Confidence);
        }
        
        public void RosValidate()
        {
            if (Expression_ is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 16;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Expression_);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Expression_);
            size = WriteBuffer2.Align4(size);
            size += 4; // Valence
            size += 4; // Arousal
            size += 4; // Confidence
            return size;
        }
    
        public const string MessageType = "hri_msgs/Expression";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "20901229fa0011d84b0639c140e98214";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61VTW/jNhC961cQ9iG7bb42ewvgg2IriVrHNiR70W1RBLQ0sthQpMqPJOqv7wy1lrVA" +
                "C/RQw4AgcubNmyfO45Rl0BqwoJxlnNW+4YpVvBBcMninHSu0OmcgXA2GCYVBBXdw0EYUGPPGu3OmTTRl" +
                "3gp1YBjFXrkEVcAVN9pbjGl0CZLpikGjHaLZ6BF4iWh1eESYTGlSWBeihrKWCcv23ELJtGLzmjd7MJY9" +
                "5QUlWGHP2Z3BLC1ZpveIXVi25Ht2c31zfc64KpFvIX0JNhSw4p3QRDEQoX6S4gV7PrM9zctoinRW2gGm" +
                "cIfdKlxmDX+h7rwFYuhq5NUgR34AVkIllCA4Yqu0Y9a3rQ6kSRdsBwqHb06zGjlJYFzK0C0ujprtS28J" +
                "O0jRiEPtWIE5WGVQofLOG2AKoLRXBv70YB2mRtYZYrhKdtssXs4mCrwzXE6OG/HqIfs6myCY6YbFPF7M" +
                "JpaXw8JjvNlgVM3bdhS1yzZZmicU602LisMpY5HmD7t8S5ulsAdPXZ0y53EW0gpuRsubZRIv0tXDbNJK" +
                "PAK4NGx92S1XSRbfLZPZ5NVLBYbvJZyqJfkmTgMmftWWizHswy5dbpH9wQvpujFD7Gqdro4ksTkt1Jhn" +
                "8nQXZ1mchx6h2XNjuB13+bjOsvQ+pe1aGyMqMe7y52SzTeekun2B1tFgjHRfrb9SHldKd6Os+12Wrnf5" +
                "bFJ5I3BQRnLnm3Te71lvW1F8t50lPyXz0IqBP8LJGrbu1kGZvR6rsu3lct9JFefLJNkgKSsB2mF5vl7d" +
                "74IIhVaVHysQP8W/hjYa/tdYuV/maSAD74UIXI47p5NNA/7lmyfE/+oJldTcfb45ugebssrohl18urym" +
                "0fkRn0PM0Vn+KWYIohZEOca67sNC1Ox//kVP+cMts658buzBXvUOh43nDmeemxL9wvGSO84qjc6Hsw3m" +
                "QsIrqmAdb1qc7rDruhbICnojwP8BaAik7Mh9gosUumm8EmTDzAn0oXE+ZgaTbrnBg+glNxivDQ4ZhVeG" +
                "N0Do+LdkHqROurglsSwU3gkk1JFtGuDB0NMFizxOCwqKCajkb5m2n36Ppts3fUFTdEAjH1j0pomsv319" +
                "ZMXtLRb7oe/yEougSoDlSss+hLVnfLUf0Z2JC7S6qNkHbGHTuRr9rr9QjCAbIGCcLYmoZ5R09nGErAK0" +
                "4kof4XvEU43/AqsGXOrpIhg2yWD9AZXEwNboVzxVJdt3AaSQAu9OdOy94aaLKKsvGU3vSez+sgifBp/o" +
                "KhrvVnL+N7xRj7MSPsuzKKPobyrWNcSQBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Expression> CreateSerializer() => new Serializer();
        public Deserializer<Expression> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Expression>
        {
            public override void RosSerialize(Expression msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Expression msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Expression msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Expression msg) => msg.Ros2MessageLength;
            public override void RosValidate(Expression msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<Expression>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Expression msg) => msg = new Expression(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Expression msg) => msg = new Expression(ref b);
        }
    }
}
