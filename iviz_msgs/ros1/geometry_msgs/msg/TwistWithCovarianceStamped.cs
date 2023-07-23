/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class TwistWithCovarianceStamped : IHasSerializer<TwistWithCovarianceStamped>, IMessage
    {
        // This represents an estimated twist with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "twist")] public TwistWithCovariance Twist;
    
        public TwistWithCovarianceStamped()
        {
            Twist = new TwistWithCovariance();
        }
        
        public TwistWithCovarianceStamped(in StdMsgs.Header Header, TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        public TwistWithCovarianceStamped(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Twist = new TwistWithCovariance(ref b);
        }
        
        public TwistWithCovarianceStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Twist = new TwistWithCovariance(ref b);
        }
        
        public TwistWithCovarianceStamped RosDeserialize(ref ReadBuffer b) => new TwistWithCovarianceStamped(ref b);
        
        public TwistWithCovarianceStamped RosDeserialize(ref ReadBuffer2 b) => new TwistWithCovarianceStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            Twist.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 336;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 336; // Twist
            return size;
        }
    
        public const string MessageType = "geometry_msgs/TwistWithCovarianceStamped";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8927a1a12fb2607ceea095b2dc440a96";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTW/bMAy9+1cQyKHJkHhYO+RQYKcN23IYULTFvoqiYGza1mpLniQ38X79nuTETdEe" +
                "etgaGIgtk4/k4yM9octKObLSWnGivSPWJM6rhr3k5DfKedooX8GkECs6E8qMsbnSMKDCciNwgaVq4MZN" +
                "myafhXOxVMW/5DJAfAPCe3PHVnFAiLBJ8u4f/5IvF59Oyfn8pnGlez3kkUzowiNDtjk14jlnz1QY5KfK" +
                "SuyiljupKaaOguNb37fiUjhGbnCVosVyXffUucCKAQdN02mVBRLG0vf+8FSamFq2XmVdzfYRZwEdl5Pf" +
                "XeR09eEUNtpJ1nmFhHogZFbYKV3iJSWd0v7kODjQhK7OjXtznUwuN2aBcylB95gF+Yp9yFq2oakhYXan" +
                "CPZqqDJFELAkCJc7msazGzy6GSEacpHWZBVNUcJZ7yujASgUe7euJQBnoAKoR8HpaHaArCO0Zm328APi" +
                "fYznwOoRN9S0qNC8OtDguhJMwrC15k7lMF33ESSrFaRLtVpbtn0SvIaQyeRjFKgPfYytwT87ZzIV5R2E" +
                "nThvA3psy43K/5csSzGQn+0HbT4xFXu97dvmCDpApj5oAekJimoZbMZx7OBhPaP7fZoMQ7Yfqwmdm82i" +
                "4V8Q+TjY7BUoN0UkbLldQm3jOGLWrdrG+ELGqtEcAgYpXqwLwoeiC7WVfMHbw5URTYOeV8C3mLh5jHHg" +
                "y1aC/qbbOfVz+jMna3YBeG06T98pID46/vH08c94PEuK2rBfvr06WV4fFPOC7Xt2w9bW3IrGIRaHwoqF" +
                "mgVKDmuTdRn3Q1gVWDlfJfPGntDO5P55Z/cy1e2i7us7/DigxPDuYYFpWGWruHOMxupqhDGOKHb0hGOu" +
                "LFyDVILM8DUxVuagg3ID5rQJdDZ8C0jBAgje3LYAwzq2rF09SCAySFNJy3ROmwqsRqswwHHvxk2tMrKq" +
                "VPngiUDN6My0Kw4iLY4xSnU95DwEg3ABshfcLKVVQb3paBMKwo3dfSAMrWXMK+4vb8w8DskA8ZDQM4Pe" +
                "gxbnuMSq087j04Sx3UmYtuNdP979Sf4CrI7DM5wHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TwistWithCovarianceStamped> CreateSerializer() => new Serializer();
        public Deserializer<TwistWithCovarianceStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TwistWithCovarianceStamped>
        {
            public override void RosSerialize(TwistWithCovarianceStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TwistWithCovarianceStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TwistWithCovarianceStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TwistWithCovarianceStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(TwistWithCovarianceStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<TwistWithCovarianceStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TwistWithCovarianceStamped msg) => msg = new TwistWithCovarianceStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TwistWithCovarianceStamped msg) => msg = new TwistWithCovarianceStamped(ref b);
        }
    }
}
