/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/PoseWithCovarianceStamped")]
    public sealed class PoseWithCovarianceStamped : IDeserializable<PoseWithCovarianceStamped>, IMessage
    {
        // This expresses an estimated pose with a reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "pose")] public PoseWithCovariance Pose;
    
        /// <summary> Constructor for empty message. </summary>
        public PoseWithCovarianceStamped()
        {
            Pose = new PoseWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseWithCovarianceStamped(in StdMsgs.Header Header, PoseWithCovariance Pose)
        {
            this.Header = Header;
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PoseWithCovarianceStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Pose = new PoseWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PoseWithCovarianceStamped(ref b);
        }
        
        PoseWithCovarianceStamped IDeserializable<PoseWithCovarianceStamped>.RosDeserialize(ref Buffer b)
        {
            return new PoseWithCovarianceStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
        }
    
        public int RosMessageLength => 344 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseWithCovarianceStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "953b798c0f514ff060a53a3498ce6246";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
