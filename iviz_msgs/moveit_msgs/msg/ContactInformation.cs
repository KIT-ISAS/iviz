/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ContactInformation")]
    public sealed class ContactInformation : IDeserializable<ContactInformation>, IMessage
    {
        // Standard ROS header contains information 
        // about the frame in which this 
        // contact is specified
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Position of the contact point
        [DataMember (Name = "position")] public GeometryMsgs.Point Position { get; set; }
        // Normal corresponding to the contact point
        [DataMember (Name = "normal")] public GeometryMsgs.Vector3 Normal { get; set; }
        // Depth of contact point
        [DataMember (Name = "depth")] public double Depth { get; set; }
        // Name of the first body that is in contact
        // This could be a link or a namespace that represents a body
        [DataMember (Name = "contact_body_1")] public string ContactBody1 { get; set; }
        [DataMember (Name = "body_type_1")] public uint BodyType1 { get; set; }
        // Name of the second body that is in contact
        // This could be a link or a namespace that represents a body
        [DataMember (Name = "contact_body_2")] public string ContactBody2 { get; set; }
        [DataMember (Name = "body_type_2")] public uint BodyType2 { get; set; }
        public const uint ROBOT_LINK = 0;
        public const uint WORLD_OBJECT = 1;
        public const uint ROBOT_ATTACHED = 2;
    
        /// <summary> Constructor for empty message. </summary>
        public ContactInformation()
        {
            ContactBody1 = string.Empty;
            ContactBody2 = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ContactInformation(in StdMsgs.Header Header, in GeometryMsgs.Point Position, in GeometryMsgs.Vector3 Normal, double Depth, string ContactBody1, uint BodyType1, string ContactBody2, uint BodyType2)
        {
            this.Header = Header;
            this.Position = Position;
            this.Normal = Normal;
            this.Depth = Depth;
            this.ContactBody1 = ContactBody1;
            this.BodyType1 = BodyType1;
            this.ContactBody2 = ContactBody2;
            this.BodyType2 = BodyType2;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ContactInformation(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Position = new GeometryMsgs.Point(ref b);
            Normal = new GeometryMsgs.Vector3(ref b);
            Depth = b.Deserialize<double>();
            ContactBody1 = b.DeserializeString();
            BodyType1 = b.Deserialize<uint>();
            ContactBody2 = b.DeserializeString();
            BodyType2 = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ContactInformation(ref b);
        }
        
        ContactInformation IDeserializable<ContactInformation>.RosDeserialize(ref Buffer b)
        {
            return new ContactInformation(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Position.RosSerialize(ref b);
            Normal.RosSerialize(ref b);
            b.Serialize(Depth);
            b.Serialize(ContactBody1);
            b.Serialize(BodyType1);
            b.Serialize(ContactBody2);
            b.Serialize(BodyType2);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ContactBody1 is null) throw new System.NullReferenceException(nameof(ContactBody1));
            if (ContactBody2 is null) throw new System.NullReferenceException(nameof(ContactBody2));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 72;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(ContactBody1);
                size += BuiltIns.UTF8.GetByteCount(ContactBody2);
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
                
    }
}
