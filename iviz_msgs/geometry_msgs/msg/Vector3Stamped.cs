/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Vector3Stamped : IDeserializable<Vector3Stamped>, IMessage
    {
        // This represents a Vector3 with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "vector")] public Vector3 Vector;
    
        /// Constructor for empty message.
        public Vector3Stamped()
        {
        }
        
        /// Explicit constructor.
        public Vector3Stamped(in StdMsgs.Header Header, in Vector3 Vector)
        {
            this.Header = Header;
            this.Vector = Vector;
        }
        
        /// Constructor with buffer.
        internal Vector3Stamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Vector);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Vector3Stamped(ref b);
        
        Vector3Stamped IDeserializable<Vector3Stamped>.RosDeserialize(ref Buffer b) => new Vector3Stamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Vector);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 24 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Vector3Stamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7b324c7325e683bf02a9b14b01090ec7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwW7UMBC9+ytG2kNbtBukFnFYiRsCekCq1IprNetMEovEDvZkt+HreXa2AVQOHGAV" +
                "yU7s92bmzZvd0EPnEkUZoyTxmojpi1gN8YZOTjucNBLFWyEbQqydZxVqIg9C7GtSN0hSHkbzSbiWSF1Z" +
                "zDPHsazGvPvHP/P5/uOektaPQ2rT6yW42dC9IiuONQ2iXLMyNQFJubaTuOvlKD2VdKWmcqrzKKkyZxnw" +
                "tOIlct/PNCVc0oC6h2HyzubC13Kf8UA6D81Gjurs1HN8oVNmx5Pk21R0vH2/xx2fxE7qkNAMBhuFk/Mt" +
                "DslMzuvNdQaYzcMp7PAqLaRdg5N2rDlZecpty3ly2iPGq6W4CtwQRxClTnRZvj3iNV0RgiAFGYPt6BKZ" +
                "383aBQ9CoSNHx4deMrGFAmC9yKCLq1+Yc9p78uzDM/3C+DPG39D6lTfXtOvQsz5Xn6YWAuLiGMPR1bh6" +
                "mAuJ7R3MSb07RI6zyaglpNl8KF7U3L7SEaycUrAODaiLh03SmNlLNx5d/b/c2EqA6+K8WPI8AeZPE7ZM" +
                "RXZOEwWVjGylyia5LW0NHqYYhFEx/LciAaxdBNQFX4EVcwlzy5acUh0kkQ8KjoG/glKgMQHN4wgyGD2y" +
                "Tz1nbP4MyKVUbbWlUycwcL6VNSqOLjPgLEXXOoxARiLQsILXv4gtaXMNjft+yXkJhoaBJAYtgKuKbhua" +
                "w0SnXBA28Tx6gQ5I8ZxXsYiGsM1zd6b4XdC7gEGALClxCzf5pBj6ypimD6xv39DTupvX3XfzA1YBeVDh" +
                "BAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
