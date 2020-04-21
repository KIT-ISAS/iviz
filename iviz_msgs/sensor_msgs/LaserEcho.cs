
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class LaserEcho : IMessage 
    {
        // This message is a submessage of MultiEchoLaserScan and is not intended
        // to be used separately.
        
        public float[] echoes; // Multiple values of ranges or intensities.
        // Each array represents data from the same angle increment.

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/LaserEcho";

        public IMessage Create() => new LaserEcho();

        public int GetLength()
        {
            int size = 4;
            size += 4 * echoes.Length;
            return size;
        }

        /// <summary> Constructor for empty message. </summary>
        public LaserEcho()
        {
            echoes = new float[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out echoes, ref ptr, end, 0);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(echoes, ref ptr, end, 0);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8bc5ae449b200fba4d552b4225586696";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE2WOsQ7CMBBD93yFpe4d4BvYYIINMRyN20ZKk+ruitS/Jwgx4ck+Wc/X4TYnw0IzmYhm" +
            "BbY9f7mOuGzZ02mY61mMeh2kQEr8NEt1pOIskTF08IonsRkjjKuoOPPehzDmKn483B9go9CA7gtdM/GS" +
            "vLVT21Ep08fpl2nJE60P+FOHkwwzRFV2KFelsbghigtGrQt8JkwWtkentpHKoFxapw9vt18nv+8AAAA=";

    }
}
