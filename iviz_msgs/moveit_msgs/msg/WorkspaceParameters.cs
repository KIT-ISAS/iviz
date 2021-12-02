/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class WorkspaceParameters : IDeserializable<WorkspaceParameters>, IMessage
    {
        // This message contains a set of parameters useful in
        // setting up the volume (a box) in which the robot is allowed to move.
        // This is useful only when planning for mobile parts of 
        // the robot as well.
        // Define the frame of reference for the box corners
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The minumum corner of the box, with respect to the robot starting pose
        [DataMember (Name = "min_corner")] public GeometryMsgs.Vector3 MinCorner;
        // The maximum corner of the box, with respect to the robot starting pose
        [DataMember (Name = "max_corner")] public GeometryMsgs.Vector3 MaxCorner;
    
        /// Constructor for empty message.
        public WorkspaceParameters()
        {
        }
        
        /// Explicit constructor.
        public WorkspaceParameters(in StdMsgs.Header Header, in GeometryMsgs.Vector3 MinCorner, in GeometryMsgs.Vector3 MaxCorner)
        {
            this.Header = Header;
            this.MinCorner = MinCorner;
            this.MaxCorner = MaxCorner;
        }
        
        /// Constructor with buffer.
        internal WorkspaceParameters(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out MinCorner);
            b.Deserialize(out MaxCorner);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new WorkspaceParameters(ref b);
        
        WorkspaceParameters IDeserializable<WorkspaceParameters>.RosDeserialize(ref Buffer b) => new WorkspaceParameters(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref MinCorner);
            b.Serialize(ref MaxCorner);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 48 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/WorkspaceParameters";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d639a834e7b1f927e9f1d6c30e920016";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVVsW7bQAzd9RUEMsQuEhdIig4BugVtMxQIkKBrQEuUdOjpqN6dbKtf38ezo6RAhw6N" +
                "F8s6vsdH8vF8Ro+9SzRIStwJ1Royu5CIKUkmbWnkyINkiYmmJO3kyYXqzE6zCx1NI+VeaKd+GoRWTFs9" +
                "rBFC+97VfTmLutVMSMLe614aykqD7mRTnZK7hVqDn4GUQKPnECxBqxHRW+fFpORkmgB8IeZEe/F+U+Ht" +
                "rbQuSDlsTbYFR2klSqilUNkRJKLQGFBT9VW4kUh9+TKKRwQMLkzDNJyCjOQEu6C9yz0o0yh1tkJedKQM" +
                "eaZ41CRVJ4quxflpSF16/x3RGq+N+OlIuqTig3ubVHxYUn36z5/q28OXG6hojimPPURBD5lDw7GBnTI3" +
                "nLm0vHddL/HSy068SR9GeKCc5nmU9NoGnUAvbDKbIYpRah2GKbiaM8bqYNPXeCDhNC7GcPXkOSJeY+OC" +
                "hRcHGHux68+peODu9sZMnqSesoOgGQx1FE7WzrtbqiYX8vWVAaqzx71e4qd0mMySHHPgYmc5jJiO6eR0" +
                "gxzvjsVtwI3mCLI0iVbl3RN+pjUhCSTIqNiMFZTfz7nXcFwgjo63MDmIa3QArOcGOl+/YjbZNxQ46DP9" +
                "kfElx7/QhoXXarrsMTNv1aepQwMROEbduQah27mQ1N5JyOTdNnKcK0MdU1Znn8uWZRtfmYhteUpaOwyg" +
                "KQ6uUo5ljy3yyTVv5ca/bsGztaLYqFCEXWy7cmbOaaOgkpFr2ZhJ7spYyx00CKNi+G9BAti4CKjTsLHV" +
                "xb2iUS7IZWpUEgXN4Bj4BygFPSageRxBBqNHDsmzYe01ICvZdBvsuN11Jcp6VBxddsDVFF3nsAKGRKJh" +
                "ATOdirug3F6hx/50bx6TYWAgiZoLYL2hu5ZmnWhvBeEhnlZPaQuJJ13FIln1wvbuRPFnQ+8Vi7D8TeAf" +
                "ImPpcee2Xjl//ECH5Wlenn5VvwH43XqtXgYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
