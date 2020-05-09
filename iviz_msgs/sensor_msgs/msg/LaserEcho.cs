using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class LaserEcho : IMessage
    {
        // This message is a submessage of MultiEchoLaserScan and is not intended
        // to be used separately.
        
        public float[] echoes { get; set; } // Multiple values of ranges or intensities.
        // Each array represents data from the same angle increment.
    
        /// <summary> Constructor for empty message. </summary>
        public LaserEcho()
        {
            echoes = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LaserEcho(float[] echoes)
        {
            this.echoes = echoes ?? throw new System.ArgumentNullException(nameof(echoes));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal LaserEcho(Buffer b)
        {
            this.echoes = BuiltIns.DeserializeStructArray<float>(b, 0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new LaserEcho(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.echoes, b, 0);
        }
        
        public void Validate()
        {
            if (echoes is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * echoes.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/LaserEcho";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "8bc5ae449b200fba4d552b4225586696";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE2WOsQ7CMBBD93yFpe4d4BvYYIINMRyN20ZKk+ruitS/Jwgx4ck+Wc/X4TYnw0IzmYhm" +
                "BbY9f7mOuGzZ02mY61mMeh2kQEr8NEt1pOIskTF08IonsRkjjKuoOPPehzDmKn483B9go9CA7gtdM/GS" +
                "vLVT21Ep08fpl2nJE60P+FOHkwwzRFV2KFelsbghigtGrQt8JkwWtkentpHKoFxapw9vt18nv+8AAAA=";
                
    }
}
