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
    
        public TimeReference()
        {
            Source = "";
        }
        
        public TimeReference(in StdMsgs.Header Header, time TimeRef, string Source)
        {
            this.Header = Header;
            this.TimeRef = TimeRef;
            this.Source = Source;
        }
        
        public TimeReference(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out TimeRef);
            b.DeserializeString(out Source);
        }
        
        public TimeReference(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out TimeRef);
            b.DeserializeString(out Source);
        }
        
        public TimeReference RosDeserialize(ref ReadBuffer b) => new TimeReference(ref b);
        
        public TimeReference RosDeserialize(ref ReadBuffer2 b) => new TimeReference(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(TimeRef);
            b.Serialize(Source);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(TimeRef);
            b.Serialize(Source);
        }
        
        public void RosValidate()
        {
            if (Source is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + Header.RosMessageLength + WriteBuffer.GetStringSize(Source);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 8;  // TimeRef
            c = WriteBuffer2.AddLength(c, Source);
            return c;
        }
    
        public const string MessageType = "sensor_msgs/TimeReference";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "fded64a0265108ba86c3d38fb11c0c16";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TwY7TMBC9+ytG6mFbpBYBt0rcVsAeVkLs3hCqpvY0tnDGwZ60hK9n7LSl7IkDVqQo" +
                "yZv3Zt6bLOCRsIyZemKBQ049IAP9FMqMEST0BCWN2RJwEkAr4UhxgjKx9Tlx+EUOTkE8iFfkVIR6sDHZ" +
                "7xtjPhE6yuDnm54FFMF+gFAu0CZwSBlOPlgP/U0zJyxwxBicgZdnoZ1iT7vgKlVtbCzahzGNDhrrLtOh" +
                "Im3KmcqQ2AXuznp1TPFaeh10ntEUyRV1nrgpLdMgISlmBayakA63rhjz/j8f8/j0cas+uV1fuvJ69tAs" +
                "4EmQHWanHgk6FGy2+dB5yutImspsrvrQvso0UNlo4XMdVK+OmDJGDa+ZJUmt6fuRg0WhNtNf9VoZGBAG" +
                "zBLsGDErPmV1scKb/5Vdr0I/RmL16+F+qxguZMfzmgS2WSOtnj7cgxkDy7u3tUCN/follTffzOL5lNb6" +
                "njrdkWsXGg8KtIQGja82jGWrYq/mKTcqoi6RyrkCy/Zup49lBaqmvdCQdJ+WOsLnSXzitp9HzAH3kSqx" +
                "VSuU9a4W3a1umLlRM3K60M+MfzT+hZavvHWmtdfwYlutsVMnFTjkdAxOofupkdgY6tbHsM+Yp3mTm6RZ" +
                "fKhmzwvbotE7lpJs0CTmv++yuJffwpjfcAJBaNkDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
