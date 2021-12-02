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
            ContactBody1 = string.Empty;
            ContactBody2 = string.Empty;
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new ContactInformation(ref b);
        
        ContactInformation IDeserializable<ContactInformation>.RosDeserialize(ref Buffer b) => new ContactInformation(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Position);
            b.Serialize(ref Normal);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/ContactInformation";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "116228ca08b0c286ec5ca32a50fdc17b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/UMBC951eM1AMtahcoiMNKPRSWj+Vrq3YFx5U3niQWiR1sp2349bxxkm0LSHCA" +
                "riJtYs97no834z26iMpq5TWdry6oYqXZU+5sVMYGMrZwvlHROEvZHqmt6yLFiqnwqmFs01Vl8gpLJohB" +
                "AuaR8BVazk1hWGdvB9KBO4PVmQsmUboikU2o1hkbs5Jdw9H3myaU4dGZrGFnQAj6k3hUA+Q9h9ZZbWxJ" +
                "0f2R6TPn0fmnZAe8UC24jZV4cRdX1E7F589Iy3Y6UoIdnS2MD5G2Tvf4VClUpGEkgO1aUpG7rta0ZVJU" +
                "G/uVnMebBUtoVc4D0HOLANjGgD3hy0L0EsvItZG1zZOsg0tPj5PFJvYtY+knlwIDou/Pp+NffTrOprXz" +
                "1YvVevNh+en9yeNp7cvq/MNis3rx7tXL9cmTu5an6/Xpy7evFiegOPnHv+zjxZs5hagHBQxCzG5pHvJQ" +
                "WkVFkDlVpqzYH9V8yTVAqmlZU9qVEMNsSiSeki17Vdc9dQFGUF/umqazJlcRqTRI6m08kKiHolb5aPKu" +
                "VtJizkO5Yp56SdjxBP7WsUU5lou5JB2l7aKBQz0Ycs8qSDWWC5qSCEC2t75yR/jkEm22O3wnBb6Wooqf" +
                "KsxxxsMhuBm456N2Au2ntQ0+wwHhELjArUNr78Pzsz5WaFfR2qXyRm1r9D4UhQyA9YGAHhzcYha359CW" +
                "dRP9wHhzxt/Q2h2vxHRUoWYQbkmhK5FAGLbeXRoN020/dH9tIF2oe+uV7zNBDUdme6/TvEpjKlUE/yoE" +
                "lxsUQNOVQZ+PSk/V2Bj9v9T4m/F206Hj2JVgppEnTQ7lpDGInBWeEZR07G5MXe/e+t3b9/txf5ypUwB3" +
                "xsdl2rvr80w0vkyqdBaablghLLTPDgmgNh5QhD4DK3tGb/IhmUjaccD4lnw16qvMPUiEgFZtCzL0qVc2" +
                "1MN1hWVA9nlWzg5xSzH6T6ykxKkhUwubnLwpDTpYkDd3nXDSGNwhxeIYEqnrwefhMJQIJN7FBDiY0bKg" +
                "3nV0JQHhxY+Tw8nEnfxKCo/OHcrYGCl+d92hg4Mq5X4NETNrlv2h1j8AntOmX8YHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
