/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class Vertices : IDeserializable<Vertices>, IMessage
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
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Vertices_ = n == 0
                    ? EmptyArray<uint>.Value
                    : new uint[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Vertices_[0]), n * 4);
                }
            }
        }
        
        public Vertices(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Vertices_ = n == 0
                    ? EmptyArray<uint>.Value
                    : new uint[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Vertices_[0]), n * 4);
                }
            }
        }
        
        public Vertices RosDeserialize(ref ReadBuffer b) => new Vertices(ref b);
        
        public Vertices RosDeserialize(ref ReadBuffer2 b) => new Vertices(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Vertices_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Vertices_);
        }
        
        public void RosValidate()
        {
            if (Vertices_ is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Vertices_.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Vertices_ length
            c += 4 * Vertices_.Length;
            return c;
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
    }
}
