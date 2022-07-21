/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class LaserEcho : IDeserializable<LaserEcho>, IMessage
    {
        // This message is a submessage of MultiEchoLaserScan and is not intended
        // to be used separately.
        /// <summary> Multiple values of ranges or intensities. </summary>
        [DataMember (Name = "echoes")] public float[] Echoes;
        // Each array represents data from the same angle increment.
    
        public LaserEcho()
        {
            Echoes = System.Array.Empty<float>();
        }
        
        public LaserEcho(float[] Echoes)
        {
            this.Echoes = Echoes;
        }
        
        public LaserEcho(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Echoes);
        }
        
        public LaserEcho(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Echoes);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new LaserEcho(ref b);
        
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
    
        public int RosMessageLength => 4 + 4 * Echoes.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Echoes);
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
    }
}
