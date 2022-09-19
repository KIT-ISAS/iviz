/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class LaserEcho : IHasSerializer<LaserEcho>, IMessage
    {
        // This message is a submessage of MultiEchoLaserScan and is not intended
        // to be used separately.
        /// <summary> Multiple values of ranges or intensities. </summary>
        [DataMember (Name = "echoes")] public float[] Echoes;
        // Each array represents data from the same angle increment.
    
        public LaserEcho()
        {
            Echoes = EmptyArray<float>.Value;
        }
        
        public LaserEcho(float[] Echoes)
        {
            this.Echoes = Echoes;
        }
        
        public LaserEcho(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(ref Unsafe.As<float, byte>(ref array[0]), n * 4);
                }
                Echoes = array;
            }
        }
        
        public LaserEcho(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<float>.Value
                    : new float[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(ref Unsafe.As<float, byte>(ref array[0]), n * 4);
                }
                Echoes = array;
            }
        }
        
        public LaserEcho RosDeserialize(ref ReadBuffer b) => new LaserEcho(ref b);
        
        public LaserEcho RosDeserialize(ref ReadBuffer2 b) => new LaserEcho(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Echoes);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Echoes);
        }
        
        public void RosValidate()
        {
            if (Echoes is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += 4 * Echoes.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Echoes.Length
            size += 4 * Echoes.Length;
            return size;
        }
    
        public const string MessageType = "sensor_msgs/LaserEcho";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8bc5ae449b200fba4d552b4225586696";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE2WOsQ7CMBBD93yFpe4d4BvYYIINMRyN20ZKk+ruitS/Jwgx4ck+Wc/X4TYnw0IzmYhm" +
                "BbY9f7mOuGzZ02mY61mMeh2kQEr8NEt1pOIskTF08IonsRkjjKuoOPPehzDmKn483B9go9CA7gtdM/GS" +
                "vLVT21Ep08fpl2nJE60P+FOHkwwzRFV2KFelsbghigtGrQt8JkwWtkentpHKoFxapw9vt18nv+8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<LaserEcho> CreateSerializer() => new Serializer();
        public Deserializer<LaserEcho> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<LaserEcho>
        {
            public override void RosSerialize(LaserEcho msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(LaserEcho msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(LaserEcho msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(LaserEcho msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(LaserEcho msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<LaserEcho>
        {
            public override void RosDeserialize(ref ReadBuffer b, out LaserEcho msg) => msg = new LaserEcho(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out LaserEcho msg) => msg = new LaserEcho(ref b);
        }
    }
}
