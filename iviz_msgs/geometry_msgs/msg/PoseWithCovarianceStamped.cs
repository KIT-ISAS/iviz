
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class PoseWithCovarianceStamped : IMessage
    {
        // This expresses an estimated pose with a reference coordinate frame and timestamp
        
        public std_msgs.Header header;
        public PoseWithCovariance pose;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PoseWithCovarianceStamped";
    
        public IMessage Create() => new PoseWithCovarianceStamped();
    
        public int GetLength()
        {
            int size = 344;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public PoseWithCovarianceStamped()
        {
            header = new std_msgs.Header();
            pose = new PoseWithCovariance();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            pose.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            pose.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "953b798c0f514ff060a53a3498ce6246";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1VTWvbQBC9L/g/DPiQpNguNMUHQw+lpW0OhbQp9IsSxtLY2iLtKrurWMqv79uVLTt1" +
                "KD20MQZLq5k38968kcf0qdCepK2deC+e2JD4oCsOklNtvdBGh4KYnKzEicmEMmtdrg0iaOW4EuTkhBTk" +
                "cVUr9U44F0dF+lGXwPgMiFf2lp3mCBBh1Ui9+MefkXp/9XZBPuTXlV/7p30fIzWmq4AW2eVUSeCcA9PK" +
                "okG9LsRNS7mVklLvoJyehq4WP1NbcfBdixHHZdlR4xEULESoqsboLKowcN/lI1MbaFazCzprSnZHokV0" +
                "fL3cNEnUi9cLxBgvWRM0GuqAkDlhr80aD0k12oTzZzFBjT9t7BS3sobMQ3EKBQc6GGZO7Beo8aQnNwM2" +
                "1BFUyT2dprNr3PozQhG0ILXNCjpF55ddKKwBoFAa2rKUCJxBAaCexKSTswPk2PaCDBu7g+8R9zX+BtYM" +
                "uJHTtMDMysjeN2sIiMDa2VudI3TZJZCs1GIClXrp2HUqZvUl1fhNMmaI40sTwS97bzOdfB0trXxwET1N" +
                "41rn/8+Qa7Hwnet6Vx7vw2hnNCdxcKCEZvvdg2grJ2BVc7bdxAYZLjDG381U2q5+nwDy0W6mFf+EtQck" +
                "DhqK21XSa97O4bFhDbHjTrepuJB1egiHbaFJEOej3dHLSreST7k97DGFRhdfAN9h0SapxkEuO4n2O20n" +
                "1E3obkLObgvw0jaBvlBEPDr++vDxt3R8plal5TB//v18/uOAzGNOL87r5QMSH09sEl8T8TjfPtc9Ibwv" +
                "D/SeEcaIeQ4B6kMDmzqTcPdxj8cRzQymxD5Ht/l+ujsKoBMtGru+x1htx0PtcNUNV3ePxWCv34O7dU/V" +
                "33YMdzd79fE3UWHL/kxqd7UBvV/rhUr2UAcAAA==";
                
    }
}
