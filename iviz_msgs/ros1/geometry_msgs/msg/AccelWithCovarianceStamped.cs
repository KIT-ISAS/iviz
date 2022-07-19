/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class AccelWithCovarianceStamped : IDeserializableRos1<AccelWithCovarianceStamped>, IDeserializableRos2<AccelWithCovarianceStamped>, IMessageRos1, IMessageRos2
    {
        // This represents an estimated accel with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "accel")] public AccelWithCovariance Accel;
    
        /// Constructor for empty message.
        public AccelWithCovarianceStamped()
        {
            Accel = new AccelWithCovariance();
        }
        
        /// Explicit constructor.
        public AccelWithCovarianceStamped(in StdMsgs.Header Header, AccelWithCovariance Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        /// Constructor with buffer.
        public AccelWithCovarianceStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new AccelWithCovariance(ref b);
        }
        
        /// Constructor with buffer.
        public AccelWithCovarianceStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Accel = new AccelWithCovariance(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new AccelWithCovarianceStamped(ref b);
        
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
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            Accel.AddRos2MessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/AccelWithCovarianceStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "96adb295225031ec8d57fb4251b0a886";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTY/TQAy951dY6mFb1BaJRXtYiQMCAT0gIUB8Cq3cxEkGkpngmWyb/fW8mbShaDmA" +
                "BFtFajKxn+3nZ2dGb2vjSaVT8WKDJ7YkPpiWgxTEeS4N7UyoYVKKis2Fcue0MBYGVCq3ApeC4AE3brt1" +
                "9kK4EKU6/WWPI8R7IDxx16yGI0KCzbJH//iXvXzz/JJ8KK5aX/n7Yx7ZjN4EZMhaUCuBCw5MpUN+pqpF" +
                "V41co8SUOgpOb8PQiV/DMXGDqxIryk0zUO9hFBw4aNvemjySMJV+9IenscTUsQaT9w3rLc4iOi4v3/vE" +
                "6ebpJWysl7wPBgkNQMhV2Btb4SVlvbHh/EF0yGZvd26FR6nA8hScQs0hJiv72MuYJ/tLxLg3FrcGNsgR" +
                "RCk8zdPZFR79ghAEKUjn8prmyPzVEGpnASiUWrZtJALnYACoZ9HpbHGCbBO0ZeuO8CPizxh/Amsn3FjT" +
                "qkbPmli97ysQCMNO3bUpYLodEkjeGCiWGrNV1iGLXmPIbPYs6TLE9qWO4J+9d7lJqo56znzQiJ66cWWK" +
                "/6XGShxUp8Moyd8Mw1Fmx7b5cTqgt2DAFngrVVBYx2A0TWIPLw0MBQzrbJyv40TN6LXbrVr+Cn1PMz0C" +
                "uTKRdrG/gNCmScSYq9mnHIScmskc2gUxQdRHzUPMpdlLseL96bZIplHKG+Arhm2ZYpz4skrU4Hy/pGFJ" +
                "N0tSdwjAW9cH+kAR8dbxx98ff0rHi6xsHIeLh5/PL76cFHOHLfyrpm3VfZN4iL1hsGGhaoGi49ZkW6X1" +
                "EDcFNs47yYPTczqY/Hw+2N1NhYeoxxpPvw10nd79WuA6brJN2j3OYnO1whhLFDt5wrEwCtcolyg1fEyc" +
                "yhJ0UOHAnnUBGC1/A6RgEURv7jqAYRsrW9+MxCYGaS7rar2kXQ1Wk1Uc5LR206I2OampTDF6IlA7OTMd" +
                "ioNQywcYp6YZcx6DQbwAOYpusaZNSYPraRcLwo0evg+OtjLllfZYcG6ZBmWE+JXQVw69By3ec4WVZ33A" +
                "lwmje5Ax7ae7Ybq7yX4AgIYMhpsHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
