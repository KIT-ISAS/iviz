/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class LaserEcho : IDeserializableRos1<LaserEcho>, IMessageRos1
    {
        // This message is a submessage of MultiEchoLaserScan and is not intended
        // to be used separately.
        /// <summary> Multiple values of ranges or intensities. </summary>
        [DataMember (Name = "echoes")] public float[] Echoes;
        // Each array represents data from the same angle increment.
    
        /// Constructor for empty message.
        public LaserEcho()
        {
            Echoes = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public LaserEcho(float[] Echoes)
        {
            this.Echoes = Echoes;
        }
        
        /// Constructor with buffer.
        public LaserEcho(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Echoes);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new LaserEcho(ref b);
        
        public LaserEcho RosDeserialize(ref ReadBuffer b) => new LaserEcho(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Echoes);
        }
        
        public void RosValidate()
        {
            if (Echoes is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Echoes.Length;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/LaserEcho";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8bc5ae449b200fba4d552b4225586696";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE2WOsQ7CMBBD93yFpe4d4BvYYIINMRyN20ZKk+ruitS/Jwgx4ck+Wc/X4TYnw0IzmYhm" +
                "BbY9f7mOuGzZ02mY61mMeh2kQEr8NEt1pOIskTF08IonsRkjjKuoOPPehzDmKn483B9go9CA7gtdM/GS" +
                "vLVT21Ep08fpl2nJE60P+FOHkwwzRFV2KFelsbghigtGrQt8JkwWtkentpHKoFxapw9vt18nv+8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
