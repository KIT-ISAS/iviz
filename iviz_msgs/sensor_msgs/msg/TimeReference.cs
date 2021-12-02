/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TimeReference : IDeserializable<TimeReference>, IMessage
    {
        // Measurement from an external time source not actively synchronized with the system clock.
        [DataMember (Name = "header")] public StdMsgs.Header Header; // stamp is system time for which measurement was valid
        // frame_id is not used 
        [DataMember (Name = "time_ref")] public time TimeRef; // corresponding time from this external source
        [DataMember (Name = "source")] public string Source; // (optional) name of time source
    
        /// Constructor for empty message.
        public TimeReference()
        {
            Source = string.Empty;
        }
        
        /// Explicit constructor.
        public TimeReference(in StdMsgs.Header Header, time TimeRef, string Source)
        {
            this.Header = Header;
            this.TimeRef = TimeRef;
            this.Source = Source;
        }
        
        /// Constructor with buffer.
        internal TimeReference(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            TimeRef = b.Deserialize<time>();
            Source = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TimeReference(ref b);
        
        TimeReference IDeserializable<TimeReference>.RosDeserialize(ref Buffer b) => new TimeReference(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(TimeRef);
            b.Serialize(Source);
        }
        
        public void RosValidate()
        {
            if (Source is null) throw new System.NullReferenceException(nameof(Source));
        }
    
        public int RosMessageLength => 12 + Header.RosMessageLength + BuiltIns.GetStringSize(Source);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/TimeReference";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "fded64a0265108ba86c3d38fb11c0c16";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TwW7bMAy96ysI+NBkQDJguwXYrdjWQ4EC7T1gJMYSJksuRSf1vn6UnHTpbYcJBgzb" +
                "j++R79EdPBKWiWmgJHDkPAAmoDchThhBwkBQ8sSWIGUBtBJOFGcoc7Kecwq/ycE5iAfxipyL0AA2Zvtr" +
                "a8xPQkcMfrnp6aAIDiOEcoU2gWNmOPtgPQw3zZyxwAljcKaWfjiddooD7YOrVLWxqWgfxjQ6aKx7pmNF" +
                "2sxMZczJhdRf9OqY4rX0fdBlRlOEK+oycVNa5VFCVswakmpCPt66Ysy3/3zM4/OPnfrk9kPpy+fFQ9PB" +
                "s2ByyE49EnQo2GzzoffEm0iaymKu+tC+yjxS2WrhSx1Ur54SMUYNr5klWa0ZhikFi0Jtpg/1WhkSIIzI" +
                "EuwUkRWfWV2s8OZ/Zder0OtESf16uN8pJhWy02VNQrKskVZPH+7BTCHJ1y+1wHQv57zRR+p1Nd7FNRWU" +
                "2iy9jZpa7RPLTjU+LcNtlVvNIVVxBVbt3V4fyxpURFugMesarbTzp1l8Tm0tT8gBD5EqsVUHlPWuFt2t" +
                "b5hr2zuNOOUr/cL4V+NfaCvLwltn2njNLLaNmno1UIEj51NwCj3MjcTGUJc9hgMjz8sCN0nTfa8eL3va" +
                "EtE7lpJt0ACWn+66r9e/wZg/7F3gx9ADAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
