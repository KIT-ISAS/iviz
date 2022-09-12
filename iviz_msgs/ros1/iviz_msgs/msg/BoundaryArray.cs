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
        public const string Md5Sum = "a287707314584223d20fee328f7d8bfb";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71V32vbSBB+118xkIcmR+K7puUogT44sa9ncBpfYgqlFDOWxtJy0q66u7Kj/vX9dmXL" +
                "cRtoH5oYY41nZ76db37p0jQ6Y9t++kzLTlTikuTtb/4k13fvLkit1ddF5XL35+X22qRR2r+h4dV8cvN+" +
                "MRyN6C39dai8HV/ffBhD//Ix/XA6xdF5sj2bf5yNF3eT69l0/AApaq9uptPJaHz7AOmBfngZPQDkfNbF" +
                "+K9wJpaK+EiWrRfi1CujYWKVzkllnda3tSS5mEq8bTvfmXFCNX6+03+Q1Bv7ilzKpeyvujKlsbfvLoeU" +
                "BumxAyep0dlidx4DSLmO8TxRub7LRHJEd55D2TICJc7YM60MMqTyQuxZKWsp4cRVLRnF05AZN4DjvFCO" +
                "8M1Fi+WybKlxMPIGhKuq0SrlkEhVyYE/PJUmppqtV2lTsoW9sZnSwXxluZKAjq+TL43oVGgyuoCNRroa" +
                "rxBQC4TUCruQsMmIYulfnQcHOqJPt8a9/JwczTfmDHrJUfA+CvIF+xC13NdWXAiY3QUu+6NjOcAlF9vC" +
                "ODqOugX+uhPCbYhFapMWdAwKs9YXRgNQaI0R42UpARhdUAL1RXB6cfIAWUdozdrs4DvE/R2/Aqt73MDp" +
                "rEDxypAG1+TIJAxra9Yqg+myjSBpqUR7KtXShuEMXt2VydE/IdkwglcsDZ7snEkVKpHRRvli15WxLAsM" +
                "xxO15Y+TBoJDshKKhPA5zASZVZy/0D8rK6BRcyqnod2COtueq2iLvJCxauc7oGRm0A29QfJfA5ZWR9y9" +
                "3XMRRCi7EUIveFbaxWr18YMLZiSGfEA3WZWG/d+v6b6X2l76+jzh71O349AXCh10kM/D4MO/L/u8Y9FU" +
                "g+QnjHbS5nm4bbf5Y8RoHc8OKQ3CpprElWI0NlMljJJhCfaecMyUlbRrwznWqoA4+lZ5yow40ib0QsX/" +
                "A1Iw38Gb6xpg2LaWtSu7VEINl2MZ5INT2hSiO6swn3GtxkWsUrIqV1nnGTLcOzNtyZ2SX51jvsuyi7m7" +
                "DO0HEGu6wp0MaLKi1jS0CYQg2O3+N7SUPq64nrwxp2H5byEe6XWkxTnOQwM4jzfPT6v+NKX+8R3cXYl3" +
                "h+2lvJeWvcRJ8g1FSDgHVgkAAA==";
                
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
