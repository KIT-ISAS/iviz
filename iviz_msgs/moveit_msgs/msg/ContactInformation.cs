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
                "H4sIAAAAAAAAE71VTW/cNhC9C9j/MIAPsQt7mzhFDwZ8SLJps22aNexFc1xwxZFERCIVkrKj/vq+IbWy" +
                "YxhID2kWAlYi5z3Ox5vhEd1EZbXymq43N9Sw0uypdDYqYwMZWznfqWicpUVxRGrvhkixYaq86hj7dNeY" +
                "ssGSCckiQctI+Aw9l6YyrBfFu8yb6ReFGF65YBKvqxLhAdg7Y+OiqNl1HP2460Idfr6SRWxlSCb4IJ61" +
                "wHnPoXdWG1tTdN8m+5vL6PxLspkgs624j4348ghatU7FX38hLfvTwRL55HVlfIi0d3rEp0phIycThxhv" +
                "JTGlG1pNeyZFrbGfyHm8WdCEXpWckZ57xME2BuwJ4aII0UtME9tOFncvFsUAx16eJ5tdHHuWtcd+BQZK" +
                "/0jHzp9w7Fwcm1avN68329379Yc/L5/Pix831+9Xu83rP96+2V6+eGT7art99ebd29UlaIrL7/wr/rr5" +
                "/YJC1FkTWaBIy9wOEIzSKipCB1Bj6ob9Wcu33AKkup41pV2JMywP+cRTs2Wv2nakIcAIgixd1w3WlCoi" +
                "owa5fYgHEnVR1CsfTTm0SrrPeYhZzFOXCTuewJ8HtqjKenUhqUeJh2jg0AiG0rMKUpP1ig5ZBKA42t65" +
                "M3xyjfabD58lwV+ktuKnChc446cc3BLcF5OGAh2ntR0+wwnhELjAvUPTH8PzqzE2aGLR3K3yRu1bFuIS" +
                "GQDrMwE9O3nAbBO1VdYd6DPj/Rn/hdbOvBLTWYOatRJ9GGokEIa9d7dGw3Q/5oHQGigYIt975cdCUPnI" +
                "4ui3NMnSAEsVwb8KwZUGBdB0Z2Jz0Huqxs7o/0uNT8y8+0adJrIE0z8YnSoPKhFR5RlBSePOU+vL/DbO" +
                "b//8GPenKXsI4Kspcpv2vvZ5KRpfJ1U6C013rBAW2mdGAqiNBxShL8HKntGbfEomknYcMNAlX536JPMP" +
                "EhG06nuQoU+9sqHNNxmWATnmZb08xf3FNltJiVNDphY2JXlTG52R99egcNIU3CnF6hwSadvscz4MJQKJ" +
                "dzEBTpa0rmh0A91JQHjx0+RwMngPfiWFR+dOZWxMFE/dgejgoGq5eUPEzFoW36j1vxPlICfhBwAA";
                
    }
}
