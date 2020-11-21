/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/WrenchStamped")]
    public sealed class WrenchStamped : IDeserializable<WrenchStamped>, IMessage
    {
        // A wrench with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "wrench")] public Wrench Wrench { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public WrenchStamped()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public WrenchStamped(StdMsgs.Header Header, in Wrench Wrench)
        {
            this.Header = Header;
            this.Wrench = Wrench;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public WrenchStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Wrench = new Wrench(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new WrenchStamped(ref b);
        }
        
        WrenchStamped IDeserializable<WrenchStamped>.RosDeserialize(ref Buffer b)
        {
            return new WrenchStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Wrench.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/WrenchStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d78d3cb249ce23087ade7e7d0c40cfa7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwWrbQBC9C/QPAzkkKY4KScnB0EOhtM2hEEhoj2EsjaSlq111d2VH/fq+WdlOQnvo" +
                "oa2xLa20782bmTd7Qu9oF8TVPe1M6ilIK7oUqr0PjXGchNrAgxC7hpIZJCYexrL4JNxIoD5fyuLrniRf" +
                "yqIs3v7lT1l8vvu4ppiahyF28fUSvyxO6C5BGoeGBknccGJqPYSZrpdwYWUrlrJmaSi/TfMosVLkfW8i" +
                "4duJk8DWzjRF7Eoe2Q/D5Eyt6R+TPhAo1DhiGjkkU0+Wwy/lyvz6i/J9ygW9eb/GLhelnpKBqBkcdRCO" +
                "xnV4ic2TcenqUhEA3u/8BdbSochHBZR6TqpYHscgUcVyXGuYV0uOFehRJEGgJtJZfvaAZTwnxIEKGT3a" +
                "dAb5t3PqvQOj0JaD4Y0VZa5RB9CeKuj0/Dm1Sl+TY+cP/AvlU5A/4XVPxJrWRY/mWS1BnDrUETvH4Lem" +
                "wd7NnFlqa8QlsmYTOMxlobAlKEg+ZG8mbWTuDa4co68NOtFkT5dFTEED5L48mOYfurMTDxOGebHoMhNH" +
                "owXRniGVqAZF4VCsNghyGbmWFboGP2XZ6LvPJsNW1EZQF50+dl32mtpODfxF6uTDFS10z9b4h+n+W577" +
                "uL9LlGmbX77MtcqTcZOd7B0mYRBGhzF3RyiQjQnAGu8q0OJUQpaokknUeInkfFKSgb+BVOAqhfM4gg0j" +
                "HthFywrWx8CcSdVVK9r1gsHVXeqIZZTz9JuagukMhl+hCDUc0Uz7BFeU2kt4ytpF9RINFlWW4FNGnFd0" +
                "09LsJ9ppTrgJ+2PH0wYi98ryWCTvV3rkHDhelvXWwwcoTYzcqVliwpGHtpdFaz2n6zf0+HSLuTjc/iiL" +
                "n5etawvXBQAA";
                
    }
}
