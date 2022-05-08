/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class TimeReference : IDeserializable<TimeReference>, IMessage
    {
        // Measurement from an external time source not actively synchronized with the system clock.
        /// <summary> Stamp is system time for which measurement was valid </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // frame_id is not used 
        /// <summary> Corresponding time from this external source </summary>
        [DataMember (Name = "time_ref")] public time TimeRef;
        /// <summary> (optional) name of time source </summary>
        [DataMember (Name = "source")] public string Source;
    
        /// Constructor for empty message.
        public TimeReference()
        {
            Source = "";
        }
        
        /// Explicit constructor.
        public TimeReference(in StdMsgs.Header Header, time TimeRef, string Source)
        {
            this.Header = Header;
            this.TimeRef = TimeRef;
            this.Source = Source;
        }
        
        /// Constructor with buffer.
        public TimeReference(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out TimeRef);
            b.DeserializeString(out Source);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TimeReference(ref b);
        
        public TimeReference RosDeserialize(ref ReadBuffer b) => new TimeReference(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(TimeRef);
            b.Serialize(Source);
        }
        
        public void RosValidate()
        {
            if (Source is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + Header.RosMessageLength + BuiltIns.GetStringSize(Source);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/TimeReference";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "fded64a0265108ba86c3d38fb11c0c16";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TwYrbMBC96ysGfNikkBTaW6C3Zds9LBR272EiTSxReeRK46Tu13ckJ2naUw8VBmP7" +
                "zXsz7407eCEsU6aBWOCY0wDIQD+EMmMECQNBSVO2BJwE0Eo4UZyhzGx9Thx+koNzEA/iFTkXoQFsTPbb" +
                "1pgvhI4y+OWmp4MiOIwQyhXaBI4pw9kH62G4a+aMBU4YgzPw9+m0UxxoH1ylqo1NRfswptFBY91nOlak" +
                "TTlTGRO7wP1Fr44pXktvgy4zmiK5oi4TN6VVGiUkxayBVRPS8d4VYz7952NeXj/v1Ce3H0pf3i8emg5e" +
                "BdlhduqRoEPBZpsPvae8iaSpLOaqD+2rzCOVrRa+1UH16okpY9TwmlmS1JphmDhYFGoz/VGvlYEBYcQs" +
                "wU4Rs+JTVhcrvPlf2fUq9H0iVr+eH3eK4UJ2uqxJYJs10urp8yOYKbB8/FALTPd2Tht9pF5X4yauqaBA" +
                "C2bU1GqfWHaq8W4Zbqvcag6piiuwau/2+ljWoCLaAo1J12ilnX+dxSdua3nCHPAQqRJbdUBZH2rRw/qO" +
                "mRs1I6cr/cL4W+NfaPnGW2faeM0sto2aejVQgWNOp+AUepgbiY2hLnsMh4x5Xha4SZruqXq87GlLRO9Y" +
                "SrJBA1h+uuu+Xv8GY34B7F3gx9ADAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
