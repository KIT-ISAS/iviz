/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwistStamped : IDeserializable<TwistStamped>, IMessage
    {
        // A twist with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "twist")] public Twist Twist;
    
        /// Constructor for empty message.
        public TwistStamped()
        {
        }
        
        /// Explicit constructor.
        public TwistStamped(in StdMsgs.Header Header, in Twist Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        /// Constructor with buffer.
        internal TwistStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Twist);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TwistStamped(ref b);
        
        TwistStamped IDeserializable<TwistStamped>.RosDeserialize(ref Buffer b) => new TwistStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Twist);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 48 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "98d34b0043a2093cf9d9345ab6eef12e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/UMBC9+1eMtIe2aBukFnFYiQMSAnpAqtSKazUbzyZWEzvYk27Dr+fZyQYqLhyA" +
                "VaTNx7w382beeEPvSY8uKR2dthTlIFF8LVSHEK3zrEKHyL0Qe0vqeknK/WA+C1uJ1JY/c18YCo8x7/7y" +
                "z3y5+7SjpPahT016PWc2G7pTlMTRUi/KlpXpEFCRa1qJl508SUelVrFUvuo0SKoAvG9dIlyNeIncdRON" +
                "CUEaILrvR+/qrHrVesID6TwxDRzV1WPH8bcmZXZcSb6NpYk3H3aI8UnqUR0KmsBQR+HkfIOPZEbn9foq" +
                "A8zm/hgu8SgN+romJ21Zc7HyPERJuU5OO+R4NYurwI3mCLLYROfl3QMe0wUhCUqQIdQtnaPy20nb4EEo" +
                "9MTR8b6TTFyjA2A9y6Czi1+Yc9k78uzDiX5m/JnjT2j9yps1XbaYWZfVp7FBAxE4xPDkLEL3UyGpOyde" +
                "qXP7yHEyGTWnNJuPxYiax1cmgn9OKdQOA7DFwCZpzOxlGg/O/is3NhLgujjNliz2PxnrNKhEGDhq0zx0" +
                "FCSQMTD6t4/hUTxewnBOE5R6QSvyfrFviq+yxWDVr1JriNe0hPx8XuL+j7ol60lflKwPI0LzITF/eymw" +
                "yitwU0wbPCzfC2OeELsiAbQuAuqCr8CKIwerK1u0g2xA53zI7ez5EZQCBxHQPAwgwxpH9qnjjM2vATmX" +
                "qqm2dGzR1RKVHVD2tWy4qym6xmHBMxKJ+hXMtIjbkh6u4KCum2uek8GOIIlBC+CiopsDTWGkYxaEm7gc" +
                "LIH2KHGpqyyAhrDNp8pC8bKhtwGzR1tS4ga74pPiSKuMOXSB9e0bel7vpvXuu/kBDsMixaoFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
