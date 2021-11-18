/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/AccelStamped")]
    public sealed class AccelStamped : IDeserializable<AccelStamped>, IMessage
    {
        // An accel with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "accel")] public Accel Accel;
    
        /// <summary> Constructor for empty message. </summary>
        public AccelStamped()
        {
            Accel = new Accel();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AccelStamped(in StdMsgs.Header Header, Accel Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AccelStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new Accel(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AccelStamped(ref b);
        }
        
        AccelStamped IDeserializable<AccelStamped>.RosDeserialize(ref Buffer b)
        {
            return new AccelStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Accel is null) throw new System.NullReferenceException(nameof(Accel));
            Accel.RosValidate();
        }
    
        public int RosMessageLength => 48 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d8a98a5d81351b6eb0578c78557e7659";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9C/wfBnxIUhwVktKDoYdAaZtDIZDQaxivxtISaVfdHdlRf33frmSloZce" +
                "2hqB9THzZt6bN7umG0dsjLR0tNpQkL0EcUbIeB8q61iF9oE7IXYVqe0kKnd98UW4kkBN/ituMkLGKVbF" +
                "h7/8WxVf7z9vKWr12MU6vp1qr4o13Su64lBRJ8oVK9PeoylbNxIuWzmgqdyuVJS/6thLLJH40NhIuGpx" +
                "ErhtRxoigtSDd9cNzppEfKF7ykemhV7Uc1BrhpbDbzoldFxRvg9Zx9uPW8S4KGZQi4ZGIJggHK2r8ZGK" +
                "wTq9vkoJxfrh6C/xKDWkXYqTNqypWXnug8TUJ8ctaryZyJXAhjqCKlWk8/zuEY/xglAELUjvTUPn6Pxu" +
                "1MY7AAodOFjetZKADRQA6llKOrv4BTm1vSXHzp/gJ8SXGn8C6xbcxOmywczaxD4ONQREYB/8wVYI3Y0Z" +
                "xLRWnFJrd4HDWKSsqWSx/pS9qGl8eSL45xi9sRhAlT1cRA0JPU/j0Vb/zpC1ePgujJMr8w6sTt46zQrt" +
                "pfcwmVpIBLH2QcCmZ8i4C/5J0kv4zmoEYSdQJG0auzrbKzkNjv0mRn24pjnk5XmO+18k57oLzSCJJoYF" +
                "nnTIH19zLNMy3Gb7egfzd8KYLPgumUisbEAq9CmBivMHSywbKEKVh4DOKzA6fgKkwEuEbO57gGGhA7vY" +
                "TtpmEelcyrrc0LGBsDkqeSFvbt51ayjY2mLVUyYKdUsy08xuQ7q/gpfadup5KgZjAiR4zQkXJd3uafQD" +
                "HRMh3IT5iPG0Q4tzX3kV1PtNOl9miNeK3nmMH7LEyDW2xkXF6VYWxb71rO/f0fNyNy53P1bFT5bRSEG5" +
                "BQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
