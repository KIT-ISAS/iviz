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
                Boundary[] array;
                if (n == 0) array = EmptyArray<Boundary>.Value;
                else
                {
                    array = new Boundary[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Boundary(ref b);
                    }
                }
                Boundaries = array;
            }
        }
        
        public BoundaryArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Boundary[] array;
                if (n == 0) array = EmptyArray<Boundary>.Value;
                else
                {
                    array = new Boundary[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Boundary(ref b);
                    }
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
            b.Align4();
            b.Serialize(Boundaries.Length);
            foreach (var t in Boundaries)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Boundaries, nameof(Boundaries));
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
        public const string Md5Sum = "a5cf0cef13788d109773a665d7ed35e7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VbW/aSBD+7l8xUj40OSXcNa1OVaR+IEATSyRQQiNVVYUWe7BXZ++6u2uo++vv2TUY" +
                "aCP1PlyCeBlmZ56dZ958rWuVCtN8+UrLVpRso+j9//yK7h5urkiu5Y9FaTP75/X22qiWyr2j/mAeT+4X" +
                "/eGQ3tNfx8rZ6G7yOIL+9VP6/niMo8toezb/PB0tHuK76Xh0gBS0g3g2GI8Wt/HN7Rif+QFi6/XxU392" +
                "fN7BXo9u+4/xZLa4n9wfAnf6wWQ8joej2QHogc88/vC5NXlA5C1wZF3a5uKWRcqG8vATLRvHJBIntYKJ" +
                "kSojmbZa11TcSkvOxVpqE2WsS3amaZGm2jJV+PpJ/8iJ0+YN2UQUvL94oAttZjfXfUq89NSB5USrdLE7" +
                "D+EkogrRPVOT/JSX6IQenPDNkhIoiVQ4QSuNfMksZ3NR8JoLOImy4pTCqc+T7cFxnktLeGes2IiiaKi2" +
                "MHIahMuyVjIRPq2y5CN/eEpFgiphnEzqQhjYa5NK5c1XRpTs0fG2/K1mlTDFwyvYKKSrdhIBNUBIDAvr" +
                "ExYPKfTEm0vvQCf0Zabt66/RyXyjL6DnDOXvoiCXC+ej5u+VYesDFvYKl/3RsuzhkqttYSydBt0Cf+0Z" +
                "4TbEwpVOcjoFhWnjcq0AyLTGYItlwR4YXVAA9ZV3enV2gKwCtBJK7+BbxP0d/wVWdbie00WO4hU+DbbO" +
                "kEkYVkavZQrTZRNAkkKyclTIpfErwXu1V0YnH3yyYQSvUBr8Cmt1IlGJlDbS5buuDGVZYFSeqS1/nTQQ" +
                "7JNhXySEL/xMkF6F+fP9szIMGpVI+Ny3m1en23MZbJEX0kbufHsUTTW6oTOIPtZgaVTA3du9FEGEshsh" +
                "9IITUtlQrS5+cMGMhJCP6EarQgv391v63klNJ/14mfD3qdtx6AqFDjrK53Hw/t+3fd6xaMpe9BtGO2nz" +
                "Mty22/wpYrQOZ8eUen5TxWGlaIXNVLJAybAEO084ptJw0rbhHGuVQRx9Kx2lmi0p7XuhFP8AkjHf3ltU" +
                "FcCwbY1QtmhTCTVcTrmX9c5pk7Nqrfx8hrUaFrFMyMhMpq2nz3DnLGhL7pzc6hLzXRRtzO1laD+AGN0W" +
                "7qxH8YoaXdPGE4Jgtvtf4wHZxRXWk9P63C//LcQTvY60WCsy3wDW4cnz26o/T6l/fQa3V+LZYTop66Rl" +
                "J4ko+hehHvM0zAkAAA==";
                
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
