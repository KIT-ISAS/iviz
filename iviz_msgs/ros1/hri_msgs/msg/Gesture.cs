/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class Gesture : IHasSerializer<Gesture>, IMessage
    {
        // Describes body language/attitude/gesture detected from a body.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Additional gestures might be added in the future, please open
        // issues/pull requests to suggest new ones.
        public const byte HANDS_ON_FACE = 1;
        public const byte ARMS_CROSSED = 2;
        public const byte LEFT_HAND_RAISED = 3;
        public const byte RIGHT_HAND_RAISED = 4;
        public const byte BOTH_HANDS_RAISED = 5;
        /// <summary> Eg, greeting someone with the hand </summary>
        public const byte WAVING = 6;
        public const byte OTHER = 0;
        /// <summary> One of the above constants </summary>
        [DataMember (Name = "gesture")] public byte Gesture_;
    
        public Gesture()
        {
        }
        
        public Gesture(in StdMsgs.Header Header, byte Gesture_)
        {
            this.Header = Header;
            this.Gesture_ = Gesture_;
        }
        
        public Gesture(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Gesture_);
        }
        
        public Gesture(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Gesture_);
        }
        
        public Gesture RosDeserialize(ref ReadBuffer b) => new Gesture(ref b);
        
        public Gesture RosDeserialize(ref ReadBuffer2 b) => new Gesture(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Gesture_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Gesture_);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 1;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Gesture_
            return size;
        }
    
        public const string MessageType = "hri_msgs/Gesture";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c64cc745a3c18d0a7abe6aed5be4f345";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TTWvbQBC961cM6JCkJHGTtKUEcnBjJzY0SbFNeyhFjLRjaWG1q+7OOtW/76xkpx+n" +
                "HmoMsmfmvfl4TznMKFRelxSgdKoHg7aOWNMEmTVHRZOaAkdPoIipYlKw9a4FHMrPswWhIg/N8MiyHKZK" +
                "adbOooE9MkCr64ahJEClhEBb4IZgG1P2FDpDGAhcR1bwOoRIYdJFY8DTd/nNAdhBiHXiA0vP4CyF8yyL" +
                "2vJ7WEwfZ+vi6bG4m97O4QYu9vHp6mFd3K6e1uv5TMKX+/DH+d2mSJhiNV2Oqat9arW8X/yde7PPfXja" +
                "LIqx1Uvu7T73Zfp5+XgvgXeQA9WnUHsi1raG4FqSYeFZczPs3KBVh8GFcb4S1OtD4HDpPC0IbjsgsHQ7" +
                "gsrZwGg5ZNnNf/5kD+v7awisijbUYTIKKkKspZ9Cr6AlRoWMsHUitEhJ/szQjoyAsO1E0CHLfZdUyWHT" +
                "6CAyyjqWPBrTQwxSJBpWrm2j1RUyAeuW/sAn7a34qkPPuooGvdQ7r7RN5VuPLSV2+YZkC1sRLGfXw2Wo" +
                "iqxloF4YKi9uSrdfzmC469VlAshRv65cuPiW5ZtndyZxqsW3L1PIrZHT1PSjE8umgTFcS7NX45bn0kSu" +
                "RNJOBTgeYoX8DScg3WQW6lzVwLGs8Knnxo0W36HXWBpKxJWcQliPEujo5DdmO1BbtO5APzL+6vEvtPaF" +
                "N+10loxmBgvGWi4phZ13O51ev7IfSCqjyTIYXXr0fZZQY8ssv0vHliJBDdLIE0Nwlcb0/iczZ4F9Yh9k" +
                "KbR4+ic4a1D3SgQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Gesture> CreateSerializer() => new Serializer();
        public Deserializer<Gesture> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Gesture>
        {
            public override void RosSerialize(Gesture msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Gesture msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Gesture msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Gesture msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Gesture msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Gesture>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Gesture msg) => msg = new Gesture(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Gesture msg) => msg = new Gesture(ref b);
        }
    }
}
