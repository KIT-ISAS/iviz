/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class BoundaryArray : IHasSerializer<BoundaryArray>, IMessage
    {
        [DataMember (Name = "boundaries")] public Boundary[] Boundaries;
    
        public BoundaryArray()
        {
            Boundaries = EmptyArray<Boundary>.Value;
        }
        
        public BoundaryArray(Boundary[] Boundaries)
        {
            this.Boundaries = Boundaries;
        }
        
        public BoundaryArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Boundary>.Value
                    : new Boundary[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Boundary(ref b);
                }
                Boundaries = array;
            }
        }
        
        public BoundaryArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Boundary>.Value
                    : new Boundary[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Boundary(ref b);
                }
                Boundaries = array;
            }
        }
        
        public BoundaryArray RosDeserialize(ref ReadBuffer b) => new BoundaryArray(ref b);
        
        public BoundaryArray RosDeserialize(ref ReadBuffer2 b) => new BoundaryArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Boundaries.Length);
            foreach (var t in Boundaries)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Boundaries.Length);
            foreach (var t in Boundaries)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Boundaries is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Boundaries.Length; i++)
            {
                if (Boundaries[i] is null) BuiltIns.ThrowNullReference(nameof(Boundaries), i);
                Boundaries[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Boundaries) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Boundaries.Length
            foreach (var msg in Boundaries) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/BoundaryArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "460beb77fde0cfe466442430a8799457";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VW2/aShB+968YKQ9NqoRzmlRHR5H6QAJNLJFACYpUVRVa7MFe1d51d9dQ99f32zUY" +
                "aCO1D00Ql2Eu39zHV7pWqTDNp8+0aEnJNore/eVXdPdwc0lyJb/PS5vZf642bqNaKvc/9a9n8fh+3h8M" +
                "6B39e8icDu/Gj0Pw3zzF749GEJ1HG9ns42Q4f4jvJqPhHlLg9qfD/vw2vrkd4TOD9GJfOutPb4azA/nb" +
                "LejV8Lb/GI+n8/vx/T5sx78ej0bxYDjdC3LPZha//9iqPCDuNtrIurStxC2LlA3l4SdaNI5JJE5qBRUj" +
                "VUYybbmuqbilFpyLldQmyliX7EzTIk20Zarw9RP/kROnzQXZRBS8c3ytC22mN1d9Sjz1lMByolU638pD" +
                "OImoQnTPNCI/1SU6ogcn/KikhJREKpygpUa9ZJazOSt4xQWMRFlxSkHq62R7MJzl0hLeGSs2oigaqi2U" +
                "nEbCZVkrmQhfVlnygT0spSJBlTBOJnUhDPS1SaXy6ksjSvboeFv+WrNKmOLBJXQUylU7iYAaICSGhfUF" +
                "iwcUZuLi3BvQEX2aavvmc3Q0W+sz8DlD+7soyOXC+aj5W2XY+oCFvYSz122WPTi53DTG0nHgzfHXnhC8" +
                "IRaudJLTMVKYNC7XCoBMK6y1WBTsgTEFBVBfeaNXJ3vIKkArofQWvkXc+fgTWNXh+pzOcjSv8GWwdYZK" +
                "QrEyeiVTqC6aAJIUkpWjQi6MPwjeqnUZHb33xYYSrEJr8Cus1YlEJ1JaS5dvpzK0ZY5Veaax/HXTkGCf" +
                "DPsmIXzhd4L0Muyfn5+lYaRRiYRP/bh5drqRy6CLupA2cmvbo2iiMQ2dQvShRpZGBdyd3ksliFC2K4RZ" +
                "cEIqG7rVxY9csCMh5IN0o2WhhfvvLX3rqKajvr9M+LvSbXPoGoUJOqjnYfD+39dd3XFoyl70m4y21Ppl" +
                "cttc86cSo1WQHabU85cqDidFK1ymkgVahiPYWcIwlYaTdgxnOKuMxDG30lGq2ZLSfhZK8QWQjP321qKq" +
                "AIZra4SyRVtKsGFyzL2sd0rrnFWr5fcznNVwiGVCRmYybS19hTtjQZvkTsktz7HfRdHG3DrD+AHE6LZx" +
                "Jz2Kl9TomtY+IRBmc/81HpBdXOE8Oa1P/fHfQDwx6yiLtSLzA2Adnjy/7frztPrXZ3DrEs8O01FZRy06" +
                "SkTRD0Ghr8LKCQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<BoundaryArray> CreateSerializer() => new Serializer();
        public Deserializer<BoundaryArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<BoundaryArray>
        {
            public override void RosSerialize(BoundaryArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(BoundaryArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(BoundaryArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(BoundaryArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(BoundaryArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<BoundaryArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out BoundaryArray msg) => msg = new BoundaryArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out BoundaryArray msg) => msg = new BoundaryArray(ref b);
        }
    }
}
