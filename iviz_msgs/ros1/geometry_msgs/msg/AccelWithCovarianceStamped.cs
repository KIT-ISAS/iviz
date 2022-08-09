/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class AccelWithCovarianceStamped : IDeserializable<AccelWithCovarianceStamped>, IMessage
    {
        // This represents an estimated accel with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "accel")] public AccelWithCovariance Accel;
    
        public AccelWithCovarianceStamped()
        {
            Accel = new AccelWithCovariance();
        }
        
        public AccelWithCovarianceStamped(in StdMsgs.Header Header, AccelWithCovariance Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        public AccelWithCovarianceStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new AccelWithCovariance(ref b);
        }
        
        public AccelWithCovarianceStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new AccelWithCovariance(ref b);
        }
        
        public AccelWithCovarianceStamped RosDeserialize(ref ReadBuffer b) => new AccelWithCovarianceStamped(ref b);
        
        public AccelWithCovarianceStamped RosDeserialize(ref ReadBuffer2 b) => new AccelWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Accel is null) BuiltIns.ThrowNullReference();
            Accel.RosValidate();
        }
    
        public int RosMessageLength => 336 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 336;  // Accel
            return c;
        }
    
        public const string MessageType = "geometry_msgs/AccelWithCovarianceStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "96adb295225031ec8d57fb4251b0a886";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTW/TQBC9+1eMlENblARBUQ+VOCAQ0ANSBYhPVdXEHttL7V0zu27i/nrerhOTqj2A" +
                "BI0sxV7PvJl582Y8o4+18aTSqXixwRNbEh9My0EK4jyXhtYm1DApRcXmQrlzWhgLAyqVW4FLQfCAG7fd" +
                "MnsrXIhSnf6yFxHiMxBeumtWwxEhwWbZ83/8y959eHNKPhSXra/84zGPbEYfAjJkLaiVwAUHptIhP1PV" +
                "ootGrlFiSh0Fp7dh6MQv4Zi4wVWJFeWmGaj3MAoOHLRtb00eSZhK3/nD01hi6liDyfuG9Q5nER2Xl599" +
                "4vTs1SlsrJe8DwYJDUDIVdgbW+ElZb2x4fhpdKAZfX/v/JOLbPZx7RY4lwp0T1lQqDnErGUTmxoTZn+K" +
                "YI/GKpcIApYE4QpPh+nsEo/+iBANuUjn8poOUcL5EGpnASiUerdqJALnoAKoB9Hp4GgP2SZoy9bt4EfE" +
                "3zH+BNZOuLGmRY3mNZEG31dgEoadumtTwHQ1JJC8MZAuNWalrEMWvcaQ2ex1EmiIfUytwT9773KT5B2F" +
                "nfmgET215dIU/0uWlTjIT4dRm/dMxU5vu7b5cUwgvGDAFngrVVBYx2A0jWQPLw0MBQzLbBy03WjN6L1b" +
                "L1r+AaFPwz0CuTKRdrI5geKmkcS8q9mkHIScmskcIgYxQdRH8UPVpdlIseDN/tpIplHTZ8BXTN08xdjz" +
                "ZZWowcPNnIY53cxJ3TYAr1wf6AtFxDvHX+8//paOj7KycRxOnn0/PrnYK+YBW/hXTVupu5J4iAVisGqh" +
                "aoGi4/pkW6U9EVcGVs8nyYPTY9qa/H7e2j1Mhduouxr3PxJ0nd7dLnAZV9pZ2j3OYoW1whhLFDt5wrEw" +
                "Ctcolyg1fFWcyhx0UOHAnnUBGC1fAVKwCKI3dx3AsJaVrW9GYhODdCjLajmndQ1Wk1Uc5LR/08Y2Oamp" +
                "TDF6IlA7OTNti4NQy6cYp6YZcx6DQbwA2YnuaElnJQ2up3UsCDe6/VA4WsmUV9pjwbl5GpQR4jah5w69" +
                "By3ec4WVZ33AJwqju5Uxbaa7Ybq7yX4Bc0iLTqQHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
