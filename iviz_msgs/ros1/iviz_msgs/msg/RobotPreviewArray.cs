/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class RobotPreviewArray : IHasSerializer<RobotPreviewArray>, IMessage
    {
        [DataMember (Name = "previews")] public RobotPreview[] Previews;
    
        public RobotPreviewArray()
        {
            Previews = EmptyArray<RobotPreview>.Value;
        }
        
        public RobotPreviewArray(RobotPreview[] Previews)
        {
            this.Previews = Previews;
        }
        
        public RobotPreviewArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<RobotPreview>.Value
                    : new RobotPreview[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new RobotPreview(ref b);
                }
                Previews = array;
            }
        }
        
        public RobotPreviewArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<RobotPreview>.Value
                    : new RobotPreview[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new RobotPreview(ref b);
                }
                Previews = array;
            }
        }
        
        public RobotPreviewArray RosDeserialize(ref ReadBuffer b) => new RobotPreviewArray(ref b);
        
        public RobotPreviewArray RosDeserialize(ref ReadBuffer2 b) => new RobotPreviewArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Previews.Length);
            foreach (var t in Previews)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Previews.Length);
            foreach (var t in Previews)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Previews is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Previews.Length; i++)
            {
                if (Previews[i] is null) BuiltIns.ThrowNullReference(nameof(Previews), i);
                Previews[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Previews) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Previews.Length
            foreach (var msg in Previews) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/RobotPreviewArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2993e476a743dae156a176dedc5c0eee";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTwU7cMBC9+ytG2gNQCVropULisGUpRYKCFtQLQtHEnk1ceT3BdkLTr2ecdMMW9dBD" +
                "iSLFeZ5582aeveSS002gztLT/QM04yoqpU7+86Oubs+PwXb2V7GOVXy/3KqsWuvTJ5if3l1cfyvmiwWc" +
                "wIc/weXZ1fX3M8EP/4bPLy9l60ipmMxI/5XQUIB6+KiyTwSok2UvIcH6CqxRv5fS9w8WzsLjmqJaOcb0" +
                "8WhCO3StwJu8kHUXhqIOttkmjNwGTYVnQ6+gBoMwJ9GxwbEjU4xMueiL7FN2HJbnn+eQpPZGC0g2Omf1" +
                "BMQ1c6o9xahKZgeYEupaSBMXaTVigbz0XmAsWGvXRtFasHf9uFtbQ6OEN7L6lRVqBrcJvcFghnYMJoQV" +
                "i0W2qinsO+rISRKuGzIw7Ka+oXggiXe1jSBvRZ6CDKKHNkpQYtC8XrfeahR/kxX7tvMl03pAkPknq1uH" +
                "QeI5GOtz+CqbktnljfTYktcEF4tjifGRdJusCOqFQQfCmG27WMBw+PL86RFmcL/kePigZndPvC84VXLi" +
                "JhWQakxZNf2UaxWzYIzHUuzd2OWBFJEpkZQzEXYHrJDfuAdSTbRQw7qGXWnhpk81eyEk6DBYLB1lYi2j" +
                "ENadnLSzt8XsB2qPnjf0I+NLjX+h9RNv7mm/FvPccHrbSiYpgU3gTk6RgbIfSLSz5BM4WwYMvcpZY0k1" +
                "+5KHLUGSNVgjX4yRtRUnDDzZVG/uxmBLkW/nGx/L6apNlypMq2paldMKlXoG/lbejy4FAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<RobotPreviewArray> CreateSerializer() => new Serializer();
        public Deserializer<RobotPreviewArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<RobotPreviewArray>
        {
            public override void RosSerialize(RobotPreviewArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(RobotPreviewArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(RobotPreviewArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(RobotPreviewArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(RobotPreviewArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<RobotPreviewArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out RobotPreviewArray msg) => msg = new RobotPreviewArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out RobotPreviewArray msg) => msg = new RobotPreviewArray(ref b);
        }
    }
}
