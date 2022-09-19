/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract]
    public sealed class Octomap : IHasSerializer<Octomap>, IMessage
    {
        // A 3D map in binary format, as Octree
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Flag to denote a binary (only free/occupied) or full occupancy octree (.bt/.ot file)
        [DataMember (Name = "binary")] public bool Binary;
        // Class id of the contained octree 
        [DataMember (Name = "id")] public string Id;
        // Resolution (in m) of the smallest octree nodes
        [DataMember (Name = "resolution")] public double Resolution;
        // binary serialization of octree, use conversions.h to read and write octrees
        [DataMember (Name = "data")] public sbyte[] Data;
    
        public Octomap()
        {
            Id = "";
            Data = EmptyArray<sbyte>.Value;
        }
        
        public Octomap(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Binary);
            b.DeserializeString(out Id);
            b.Deserialize(out Resolution);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<sbyte>.Value
                    : new sbyte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(ref Unsafe.As<sbyte, byte>(ref array[0]), n * 1);
                }
                Data = array;
            }
        }
        
        public Octomap(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Binary);
            b.Align4();
            b.DeserializeString(out Id);
            b.Align8();
            b.Deserialize(out Resolution);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<sbyte>.Value
                    : new sbyte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(ref Unsafe.As<sbyte, byte>(ref array[0]), n * 1);
                }
                Data = array;
            }
        }
        
        public Octomap RosDeserialize(ref ReadBuffer b) => new Octomap(ref b);
        
        public Octomap RosDeserialize(ref ReadBuffer2 b) => new Octomap(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Binary);
            b.Serialize(Id);
            b.Serialize(Resolution);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Binary);
            b.Serialize(Id);
            b.Serialize(Resolution);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 17;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += Data.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Binary
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Id);
            size = WriteBuffer2.Align8(size);
            size += 8; // Resolution
            size += 4; // Data.Length
            size += 1 * Data.Length;
            return size;
        }
    
        public const string MessageType = "octomap_msgs/Octomap";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9a45536b45c5e409cd49f04bb2d9999f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TwW7UMBC9+ytG2kN3UbsrKKpQJQ6IVaEHBGp7q6pq1p5NLDl28DhbwtfznHQL3DgQ" +
                "RUrizHsz897Mgj7Q+ZY67slH2vnIeaR9yh2XU2Klr7ZkEfNZ2EmmdnoYs6CrwA2VRE5iKkJ8hC5TDCAA" +
                "ZpOsHXovbkUp034IgaYTjnbEW6Wl5XpXNutUaO+DrMwupfBMVHN8DKxK3lHaU2mFbIqFfRR3hBst2ccG" +
                "ITX8RjSFofgUaYleutURpx2HIFqOsJicqNmHxOXiLeUXWCV5bkMlew7+J0904JmhpzToVMdBsuKPrtsq" +
                "QoYsxNHRU/YQY45V42N5d/9Ajgsb8/4/X+bL7adL0uIeO210MxuEBm4LCuHsqJPCNXV1k1rftJLPghwk" +
                "AMRdDxWnv2XsRdcA3rUeYis1EiVDsLH26mp7NnXdEL1l9FZ8J3/hgYTYTD3n4u0QOCM+ZQcdEb7P3Ell" +
                "x63yfZBoha63l1VDFQvZUdAIBgsNtXp5vSUzQLnzNxVAC7q/Sfr6wSzuntIZzqXBHL5UAX+51KrlRw8j" +
                "a8Gsl0j2au5yjSRQSZDOKS2ns0d86oqQDbVIn2w7zcu3sbTwug7MgeH+LkgltnV2HJ1U0MnqD+Y4UUeO" +
                "6Ug/M/7O8S+08YW39nTWwrxQZdChgZII7HM6eIfQ3TgvQfASCwW/y3VNKmpOaRZXVWwEATVZgycWKFkP" +
                "JzCbvrTHhZlseaxr8wvN5Nj4AAQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Octomap> CreateSerializer() => new Serializer();
        public Deserializer<Octomap> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Octomap>
        {
            public override void RosSerialize(Octomap msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Octomap msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Octomap msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Octomap msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Octomap msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Octomap>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Octomap msg) => msg = new Octomap(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Octomap msg) => msg = new Octomap(ref b);
        }
    }
}
