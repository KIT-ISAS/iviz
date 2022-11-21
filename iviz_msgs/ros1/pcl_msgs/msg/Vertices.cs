/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class Vertices : IHasSerializer<Vertices>, IMessage
    {
        // List of point indices
        [DataMember (Name = "vertices")] public uint[] Vertices_;
    
        public Vertices()
        {
            Vertices_ = EmptyArray<uint>.Value;
        }
        
        public Vertices(uint[] Vertices_)
        {
            this.Vertices_ = Vertices_;
        }
        
        public Vertices(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                uint[] array;
                if (n == 0) array = EmptyArray<uint>.Value;
                else
                {
                     array = new uint[n];
                    b.DeserializeStructArray(array);
                }
                Vertices_ = array;
            }
        }
        
        public Vertices(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                uint[] array;
                if (n == 0) array = EmptyArray<uint>.Value;
                else
                {
                     array = new uint[n];
                    b.DeserializeStructArray(array);
                }
                Vertices_ = array;
            }
        }
        
        public Vertices RosDeserialize(ref ReadBuffer b) => new Vertices(ref b);
        
        public Vertices RosDeserialize(ref ReadBuffer2 b) => new Vertices(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Vertices_.Length);
            b.SerializeStructArray(Vertices_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Vertices_.Length);
            b.SerializeStructArray(Vertices_);
        }
        
        public void RosValidate()
        {
            if (Vertices_ is null) BuiltIns.ThrowNullReference(nameof(Vertices_));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += 4 * Vertices_.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Vertices_.Length
            size += 4 * Vertices_.Length;
            return size;
        }
    
        public const string MessageType = "pcl_msgs/Vertices";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "39bd7b1c23763ddd1b882b97cb7cfe11";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NW8MksLlHIT1MoyM/MK1HIzEvJTE4t5ioFcoyNomMVylKLSsAiXAA/hR0KKwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Vertices> CreateSerializer() => new Serializer();
        public Deserializer<Vertices> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Vertices>
        {
            public override void RosSerialize(Vertices msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Vertices msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Vertices msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Vertices msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Vertices msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Vertices>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Vertices msg) => msg = new Vertices(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Vertices msg) => msg = new Vertices(ref b);
        }
    }
}
