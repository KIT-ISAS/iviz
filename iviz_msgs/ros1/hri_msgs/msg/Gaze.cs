/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class Gaze : IHasSerializer<Gaze>, IMessage
    {
        // Represents the gaze direction from a particular person (sender ID) to the
        // person that is being gazed to (receiver ID).
        //
        // If the sender or receiver IDs are empty, it means that the gaze respectively
        // originates or is targeted to the robot itself.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "sender")] public string Sender;
        [DataMember (Name = "receiver")] public string Receiver;
    
        public Gaze()
        {
            Sender = "";
            Receiver = "";
        }
        
        public Gaze(in StdMsgs.Header Header, string Sender, string Receiver)
        {
            this.Header = Header;
            this.Sender = Sender;
            this.Receiver = Receiver;
        }
        
        public Gaze(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Sender = b.DeserializeString();
            Receiver = b.DeserializeString();
        }
        
        public Gaze(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            Sender = b.DeserializeString();
            b.Align4();
            Receiver = b.DeserializeString();
        }
        
        public Gaze RosDeserialize(ref ReadBuffer b) => new Gaze(ref b);
        
        public Gaze RosDeserialize(ref ReadBuffer2 b) => new Gaze(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Sender);
            b.Serialize(Receiver);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Sender);
            b.Align4();
            b.Serialize(Receiver);
        }
        
        public void RosValidate()
        {
            if (Sender is null) BuiltIns.ThrowNullReference(nameof(Sender));
            if (Receiver is null) BuiltIns.ThrowNullReference(nameof(Receiver));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Sender);
                size += WriteBuffer.GetStringSize(Receiver);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Sender);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Receiver);
            return size;
        }
    
        public const string MessageType = "hri_msgs/Gaze";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1408dc110169ebd2a0cd704f3af52beb";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TwY7TQAy9z1dYymFbRIuAWyVuK9gekNDu3hCq3Bk3GSmZydpOl/D1eCa07N44EEWK" +
                "kvF7fs9+aeCeRiahpALaEbT4iyBEJq8xJzhxHgBhRNbopx4ZRmKxg5VBAjHsb9eguUBdcznTDhWiwJFi" +
                "aitjKDUrI6V4XkBb1xhgf6pN/3BlhhclAsgENIw6v4WoMBAmWbivQk35WJSeqZ+NLnNsY0IlKVymQJFb" +
                "0qV9AXE+ZpOmQv1p6+4IS9uuPpwT5aJ3EXN5uwhy7tN/vtzXhy87EA2HQVp5t4gxEw+KKSAHM6wYUBFO" +
                "ZqaLbUe86cmsGgiH0VzVU51HEhsnPHbm2O6WEjH2/QyTLNZ9HoYpRW+TAY0DvcIbMqbXO/Y5c6iDtADg" +
                "QIXdbqGniZInW87OapKQn5bZG4NnQikT29+Cm2LSjx8KABr4fp/l/Q/XPD7njX2n1mZ+VXENC/0sOSyC" +
                "UXbW7M3icmtNbEpk7YJY6sq3g73KGqybaaEx+w5WZuHbrF1NH8EZOeKxp0LsbRTGelNAN+sXzKlSJ0z5" +
                "Qr8w/u3xL7Tpyls8bTpbXl9jNLVYMzhyPsdgpce5kvg+2t8GfTwy8uwKamnpms9l2FZkqLoae6JI9hFL" +
                "hp+jdpdY1rUcYnDuN29uEQvCAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Gaze> CreateSerializer() => new Serializer();
        public Deserializer<Gaze> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Gaze>
        {
            public override void RosSerialize(Gaze msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Gaze msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Gaze msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Gaze msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Gaze msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Gaze>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Gaze msg) => msg = new Gaze(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Gaze msg) => msg = new Gaze(ref b);
        }
    }
}
