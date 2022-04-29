/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ContactInformation : IDeserializable<ContactInformation>, IMessage
    {
        // Standard ROS header contains information 
        // about the frame in which this 
        // contact is specified
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Position of the contact point
        [DataMember (Name = "position")] public GeometryMsgs.Point Position;
        // Normal corresponding to the contact point
        [DataMember (Name = "normal")] public GeometryMsgs.Vector3 Normal;
        // Depth of contact point
        [DataMember (Name = "depth")] public double Depth;
        // Name of the first body that is in contact
        // This could be a link or a namespace that represents a body
        [DataMember (Name = "contact_body_1")] public string ContactBody1;
        [DataMember (Name = "body_type_1")] public uint BodyType1;
        // Name of the second body that is in contact
        // This could be a link or a namespace that represents a body
        [DataMember (Name = "contact_body_2")] public string ContactBody2;
        [DataMember (Name = "body_type_2")] public uint BodyType2;
        public const uint ROBOT_LINK = 0;
        public const uint WORLD_OBJECT = 1;
        public const uint ROBOT_ATTACHED = 2;
    
        /// Constructor for empty message.
        public ContactInformation()
        {
            ContactBody1 = "";
            ContactBody2 = "";
        }
        
        /// Constructor with buffer.
        public ContactInformation(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Position);
            b.Deserialize(out Normal);
            b.Deserialize(out Depth);
            b.DeserializeString(out ContactBody1);
            b.Deserialize(out BodyType1);
            b.DeserializeString(out ContactBody2);
            b.Deserialize(out BodyType2);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ContactInformation(ref b);
        
        public ContactInformation RosDeserialize(ref ReadBuffer b) => new ContactInformation(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Position);
            b.Serialize(in Normal);
            b.Serialize(Depth);
            b.Serialize(ContactBody1);
            b.Serialize(BodyType1);
            b.Serialize(ContactBody2);
            b.Serialize(BodyType2);
        }
        
        public void RosValidate()
        {
            if (ContactBody1 is null) BuiltIns.ThrowNullReference();
            if (ContactBody2 is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 72;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(ContactBody1);
                size += BuiltIns.GetStringSize(ContactBody2);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ContactInformation";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "116228ca08b0c286ec5ca32a50fdc17b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTW/UMBC951eM1AMtahcoiEOlHoDlY/naql3BceWNJ4lFYgfbaRt+PW/s7LYFJDgA" +
                "q0ib2POe5+PNeI8uorJaeU3nywtqWGn2VDoblbGBjK2c71Q0zlKxR2rjhkixYaq86hjbdNWYssGSCWKQ" +
                "gGUkfIWeS1MZ1sWbTJq5C1iduWASpasS2RbVO2NjUbPrOPpx3YU6PDiTNexkhKA/ikctQN5z6J3VxtYU" +
                "3W+ZPnEZnX9MNuOFas59bMSLu7iqdSo+fUJattOREuzkbGV8iLRxesSnSqEiDRMBbFeSitINraYNk6LW" +
                "2C/kPN4sWEKvSs5Azz0CYBsD9oSvCNFLLBPXWtbWj4oBLj0+ThbrOPaMpR9cCgyI/n8+Hf/s03GxXTtf" +
                "Pl+u1u8XH9+dPtyufV6ev5+vl8/fvnyxOn101/LZavXsxZuX81NQnP7lX/Hh4vUJhaizArIQkY+d5iEP" +
                "pVVUBJlTY+qG/VHLl9wCpLqeNaVdCTHMtonEU7Nlr9p2pCHACOorXdcN1pQqIpUGSb2NBxL1UNQrH005" +
                "tEpazHkoV8xTLwk7nsBfB7Yox2J+IklHaYdo4NAIhtKzClKNxZy2SQSg2FtduSN8co022x2+kwJfS1HF" +
                "TxVOcMb9HNwM3CeTdgLtp7U1PsMB4RC4wL1Da+/D87MxNmhX0dql8kZtWhbiEhkA6z0B3Tu4xWwTtVXW" +
                "bekz480Zf0Jrd7wS01GDmrUSfRhqJBCGvXeXRsN0M+bubw2kC3VvvPJjIah8ZLH3Ks2rNKZSRfCvQnCl" +
                "QQE0XRn0+aT0VI210f9Kjb8YbzcdOo1dCaa/NSRVHkwiosozgpKO3Y2p693buHv79n/cn2bqNoA74+My" +
                "7d31eSYaXyRVOgtNd6wQFtpnhwRQGw8oQp+BlT2jN/mQTCTtOGB8S7469UXmHiQiaNX3IEOfemVDm68r" +
                "LAOyz7N6dohbim22khKnhkwtbErypjY6I2/uOuGkKbhDitUxJNK22ed8GEoEEu9iAhzMaFHR6Aa6koDw" +
                "4qfJ4WTibv1KCo/OHcrYmCh+dd2hg4Oq5X4NETNrVvym1t8BntOmX8YHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
