/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ShapeActionResult : IDeserializable<ShapeActionResult>, IActionResult<ShapeResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ShapeResult Result { get; set; }
    
        /// Constructor for empty message.
        public ShapeActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ShapeResult();
        }
        
        /// Explicit constructor.
        public ShapeActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        public ShapeActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ShapeResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ShapeActionResult(ref b);
        
        public ShapeActionResult RosDeserialize(ref ReadBuffer b) => new ShapeActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength => 8 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTXPbNhC981dgxofYnVppk36kntFBtVVHGSfx2GqvHpBckWhBgMWHZf37vgUpmnKs" +
                "RockGtu0JODtw9u3i31LsiQn6vTIZBGUNVrld42v/MtLK/VtkCF64dMju61lSzfkow7CpUc2/cKv7P3t" +
                "5RnilR2Htx2zIwEippSuFA0FWcogxcqCuKpqcqea7kkzyaalUqRvw6YlP8HGZa28wE9FhpzUeiOix6Jg" +
                "RWGbJhpVyEAiqIZ29mOnMkKKVrqgiqilw3rrSmV4+crJhhgdP57+jWQKEouLM6wxnooYFAhtgFA4kl6Z" +
                "Cl+KLCoTXr/iDdnRcm1P8ZYqyD8EF6GWgcnSQwt9maf0Z4jxXXe4CbAhDiFK6cVx+uwOb/2JQBBQoNYW" +
                "tTgG8+tNqK0BIIl76ZTMNTFwAQWA+oI3vTgZIZsEbaSxW/gO8THGIbBmwOUzndbImebT+1hBQCxsnb1X" +
                "JZbmmwRSaEUmCHjOSbfJeFcXMjv6gzXGIuxKGcFTem8LhQSUYq1CnfngGD1l406V2Vdy4966yPhfZLbC" +
                "g+Nzgt9si6V7cz3/cLH4cCm2r6n4AX/ZlpS2iVp6saHAhsyJ9Sm6xPcCdbGRc3ePOugwZ+fLxV9zMcL8" +
                "cReTMxKdg7IwYU6s0UHA1zfz+fvr5fxiAH61C+yoIFgbtkTKYQ/+BO73QchVgJNV4NM7ThA9pDowVSb+" +
                "53WEX5gkqdAZDlXZamIEFfwWBUSPl+QaVJ/mVhDopKd8++f5+Xx+MaL8epfyGsiyqBUxbR8LVmEVuQ88" +
                "J8S+MLPfP9486sJhfnomTG7T0cuYbPnI/dlIZaTPSsOu8BZlsJJKR0f76N3M383PR/ym4udP6Tn6m4qw" +
                "xwGpoGwMT+3y/ec55lRI9NSEOQSL6JNBgil3CHRqZe6lVuW+A/TOGyplKn75Bs4brGdsSEX4aL4heYPC" +
                "57Orq8dKnopfDyWYE64qepbhIeoiJ59ma5e0WSnX8KXG10cYd4HEhMqdQ4xt8uYLHOIwmdkUO+XXBeBr" +
                "Y48nrj7eLsdQU/FbApyZrRj97QEkUSJrDEKdCHKQgFEm3RTgYXBdJt3yA2rPM7ZltVnStcLxUTnSPGmd" +
                "2dFMa7tO8wgvRCk4rtvhsgKZ/qLiGhOjsYq3lJTHqmIZ+0WBHkL2Da+yxUXWOaAbQXqRfOB083nSnQxJ" +
                "17XCbJHu41FLSe6gkmehRRpdYn/HPNUJ+8mwf3BK8iwQRhxqWuRKa+xmTN8lb00IPUBvrQdLkuOWkhiN" +
                "R4WeP7pLP16gFYPeZjcLK6Iyl8U/7Ebs6OZXjJPey4q61PiWCrVSxbYYEgM/6dF51usWgFQTU1Ggzyms" +
                "mmyTx0PIV0pdiC5ouhsy+HI0jWfZSlvJwyXPlE5ZdydNpWn4WLYWuWyy/wA+Bzam8QsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
