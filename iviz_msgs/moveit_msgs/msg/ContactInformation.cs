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
        internal ContactInformation(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Position);
            b.Deserialize(out Normal);
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
            b.Serialize(Position);
            b.Serialize(Normal);
            b.Serialize(Depth);
            b.Serialize(ContactBody1);
            b.Serialize(BodyType1);
            b.Serialize(ContactBody2);
            b.Serialize(BodyType2);
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
                "H4sIAAAAAAAACr1VTW/TQBC9W8p/GKkHWtQGCIhDpB6A8BG+UrURHKONd2yvsHfN7rrF/HrerJ20BQQc" +
                "gMhS7N15b+fjzewBXURltfKazlcXVLHS7Cl3NipjAxlbON+oaJyl7IDU1nWRYsVUeNUwtumqMnmFJRPE" +
                "IAHzSPgKLeemMKyzVwPpwJ3B6swFkyhdkch2qNYZG7OSXcPR95smlOHemaxhZ0AI+r14VAPkPYfWWW1s" +
                "SdH9lukD59H5h2QHvFAtuI2VeHEbV9ROxcePSMt2OlKCHZ0tjA+Rtk73+FQpVKRhJIDtWlKRu67WtGVS" +
                "VBv7iZzHmwVLaFXOA9BziwDYxoA94ctC9BLLyLWRtc2DrINLD2fJYhP7lrH0nUuBAdH/z6fZjz7Nst3a" +
                "+erpar15u3z/5vT+bu3j6vztYrN6+vr5s/Xpg9uWT9brJ89ePV+czrJJdvqXf5Ps3cXLOYWoBw0MUpxk" +
                "N2QPhSitoiIonSpTVuxPar7kGijVtKwp7UqUYbrLJZ6SLXtV1z11AUYQYO6aprMmVxHZNMjrTTyQKImi" +
                "Vvlo8q5W0mXOQ7xintpJ2PEE/tyxRUWWi7nkHdXtooFDPRhyzypIQZYL2uURgOxgfeVO8MklOm1/+F4N" +
                "/EXqKn6qMMcZd4fgpuCej/IJdJjWNvgMR4RD4AK3Dt19CM/P+lihY0Vul8obta3R/hAVMgDWOwK6c3SD" +
                "WdyeQ17W7egHxusz/oTW7nklppMKNYN2SwpdiQTCsPXu0miYbvthANQG6oXAt175PhPUcGR28CKNrDSp" +
                "UkXwr0JwuUEBNF0ZtPoo9lSNjdH/TpA/mXGT6z4dh6/Esxt80uoQTxqGSFvhGXFJ3+6H1Zf9W79/+/q/" +
                "Ihhn6z6GW3PkMm3ednsqSl8mbToLZTesEBmaaI8EUBsPKKKfgpU9o0P5mEwk7Thgjstoa9QnGYAQCgGt" +
                "2hZk6FavbKiHewvLgBzytJwe47pidKFYSaFTW6ZGNjl5Uxr0sSCvLz3hpDG6Y4rFDEKp68Hn4TBUCSTe" +
                "xQQ4mtKyoN51dCUB4cWP88PJ6N35lXQenTuW4TFS/OzeQx8HVcpFGyJG1zT7dbkn2Td81PdJ0AcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
