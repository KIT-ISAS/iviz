/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class BoundingBox3DArray : IHasSerializer<BoundingBox3DArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "boxes")] public VisionMsgs.BoundingBox3D[] Boxes;
    
        public BoundingBox3DArray()
        {
            Boxes = EmptyArray<VisionMsgs.BoundingBox3D>.Value;
        }
        
        public BoundingBox3DArray(in StdMsgs.Header Header, VisionMsgs.BoundingBox3D[] Boxes)
        {
            this.Header = Header;
            this.Boxes = Boxes;
        }
        
        public BoundingBox3DArray(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                VisionMsgs.BoundingBox3D[] array;
                if (n == 0) array = EmptyArray<VisionMsgs.BoundingBox3D>.Value;
                else
                {
                    array = new VisionMsgs.BoundingBox3D[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new VisionMsgs.BoundingBox3D(ref b);
                    }
                }
                Boxes = array;
            }
        }
        
        public BoundingBox3DArray(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                VisionMsgs.BoundingBox3D[] array;
                if (n == 0) array = EmptyArray<VisionMsgs.BoundingBox3D>.Value;
                else
                {
                    array = new VisionMsgs.BoundingBox3D[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new VisionMsgs.BoundingBox3D(ref b);
                    }
                }
                Boxes = array;
            }
        }
        
        public BoundingBox3DArray RosDeserialize(ref ReadBuffer b) => new BoundingBox3DArray(ref b);
        
        public BoundingBox3DArray RosDeserialize(ref ReadBuffer2 b) => new BoundingBox3DArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Boxes.Length);
            foreach (var t in Boxes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Boxes.Length);
            foreach (var t in Boxes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Boxes, nameof(Boxes));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += 80 * Boxes.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Boxes.Length
            size = WriteBuffer2.Align8(size);
            size += 80 * Boxes.Length;
            return size;
        }
    
        public const string MessageType = "vision_msgs/BoundingBox3DArray";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9e1a3932308592aa1b20232d818486db";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTW/UQAy951dY6qEt2i6CIg6VOIBWQA+IAhUXhCpv4k0GkpkwM9lt+PU8TzZpI8rH" +
                "AbqKtEnGfvZ79ngSYnHVhDI8fC1ciKcq/WVbE4yzw8oL19nC2PKFuz5dffpMa3ctIXv2j3/Zmw+vzijM" +
                "08kO6ENkW7AvqJHIBUemjUOapqzEn9SylRpO3LRSUFqNfSthCcfLygTCVYoVz3XdUxdgFB3lrmk6a3KO" +
                "QtE0MvOHp7HE1LKPJu9q9rB3HgKo+cZzI4qOK8i3TmwudL46g40NknfRIKEeCLkXDhANi5R1xsbTx+pA" +
                "B/TpvQuPPmcHlzt3gvdSQvUpC4oVR81arlsvQRPmcIZgDwaWSwSBSoJwRaCj9O4Kj+GYEA25SOvyio5A" +
                "4aKPlbMAFNqyN7yuRYFzSAHUQ3U6PL6FbBO0ZetG+AHxJsbfwNoJVzmdVCherTKEroSSMGy925oCpus+" +
                "geS1ERupNmvPvs/UawiZHbxUsWEEr1Qa/HMILjeoREE7E6ssRK/oqSxXpsj+U1v+cjuA53M6XWFPDC91" +
                "cww1zNnSWqh1wUT4aiFtQd7FlDzDAXWOEA7s0QFHT2n19qXqtoICVsMFcpuBvYKyF21M7ALxYZHAGCXq" +
                "8moBHGq413CNKX0KEB2gCGYOInt0c/6VS1kkB3UsxQHK94nTQpG1GJsudh79nbaPKLEx/xTQea0Vp+eU" +
                "m8yJD1yyGfbDC/TTuLLHDea73AWwuM0wdN6Pa2ro1l8kj4ejYokeskO283gfYeX8aQryv/rhZ4apEbzo" +
                "pr0tkSaopDZeQBtVQAkwfvR1sV+/U94lZRcO02EyyN51qKu3CffG7r4IIpVxpGI2RDboTi3KlD+4YGam" +
                "lGd0s03tOD59QtfTXT/dfb+f9G+kGzlMhcJEmek5T16fvt3ojoOnwd74PaPxbnc/3Pbdfhcx2qa1OaWl" +
                "nlzn6YhxFidVI4yS4VCcPOFYGA/X1IbYrl5AXNKUKZwEwkgBRsNfAYlJJerNbQswnL6ebagHKfEaLkey" +
                "LJcL2lViByvdz+mYTQezycmb0hSDpyo8OTPtyS0obh5j3tf1kPMQDO0HkDRQ4XC8pPMN9a6jnRLCjd9/" +
                "Dzidi2Ne6biKzi30Y2APcUevQ5YQMC+hXYj4Evlj1X8A9qMA3EcJAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<BoundingBox3DArray> CreateSerializer() => new Serializer();
        public Deserializer<BoundingBox3DArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<BoundingBox3DArray>
        {
            public override void RosSerialize(BoundingBox3DArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(BoundingBox3DArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(BoundingBox3DArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(BoundingBox3DArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(BoundingBox3DArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<BoundingBox3DArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out BoundingBox3DArray msg) => msg = new BoundingBox3DArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out BoundingBox3DArray msg) => msg = new BoundingBox3DArray(ref b);
        }
    }
}
