/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class LiveSpeech : IDeserializable<LiveSpeech>, IMessage
    {
        // This message encodes the live result of a speech recognition process.
        // A series of incremental results might be provided, until a final recognition
        // hypothesis is returned.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> Incremental recognition results </summary>
        [DataMember (Name = "incremental")] public string Incremental;
        /// <summary> Final recognised text </summary>
        [DataMember (Name = "final")] public string Final;
        /// <summary> Confidence of the final recognised speech, [0-1] </summary>
        [DataMember (Name = "confidence")] public double Confidence;
    
        public LiveSpeech()
        {
            Incremental = "";
            Final = "";
        }
        
        public LiveSpeech(in StdMsgs.Header Header, string Incremental, string Final, double Confidence)
        {
            this.Header = Header;
            this.Incremental = Incremental;
            this.Final = Final;
            this.Confidence = Confidence;
        }
        
        public LiveSpeech(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Incremental);
            b.DeserializeString(out Final);
            b.Deserialize(out Confidence);
        }
        
        public LiveSpeech(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeString(out Incremental);
            b.Align4();
            b.DeserializeString(out Final);
            b.Align8();
            b.Deserialize(out Confidence);
        }
        
        public LiveSpeech RosDeserialize(ref ReadBuffer b) => new LiveSpeech(ref b);
        
        public LiveSpeech RosDeserialize(ref ReadBuffer2 b) => new LiveSpeech(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Incremental);
            b.Serialize(Final);
            b.Serialize(Confidence);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Incremental);
            b.Serialize(Final);
            b.Serialize(Confidence);
        }
        
        public void RosValidate()
        {
            if (Incremental is null) BuiltIns.ThrowNullReference();
            if (Final is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Incremental);
                size += WriteBuffer.GetStringSize(Final);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Incremental);
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Final);
            c = WriteBuffer2.Align8(c);
            c += 8; // Confidence
            return c;
        }
    
        public const string MessageType = "hri_msgs/LiveSpeech";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d1d0f996ef223aa810d45b3a627e91cd";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TTWvcMBS861c88CFJyW4/6WGhh0Jom0OhNLmFsGilZ1sgS670vIn/fUdydrNpLz3U" +
                "CIzlNzNPM08N3fYu08A5646Jg4mWM0nP5N2eKXGevFBsSVMemU2PLRO74MTFQGOKBtC1augzZU4OWNS6" +
                "YBIPHET7JwZIuK4X2nHB7J1le0lTEOdB3LpQC4+8oOvnMaKLjOawEsuUAtu1Ut9YW07U15dSWZIL3QvF" +
                "5g/953afejmAFt3mpX5mS8KPolTro5aPH8jE0KLhYBi1Jx84aPHpL/Ti0yXdvVm9vVd4Pv3nR32/+bqh" +
                "LHY75C6/XhyBZzeig9XJIk7RVgucjXAKxnNaed6zB0gPI3qsf2UeuWZXZwCr48BJez/TVG2IOO4wTMEZ" +
                "LUziMCaneFWcRn6jTuLM5HVCfUwWhqC8TXrgwo6V+ddUPbu+2hQLM5tJMF9QqlnpXPK4viI1uSDv3xUA" +
                "zL77GTMsbG4f4gr73CH4YxcwX0vpmh9HBFsa1nkDsVfLKdcQgUvIJdhM53Vvi898QVBDLzxGjPM5jvBj" +
                "lh7jUdLc6+T0znMhNrACrGcFdHZxwhwqddAhHugXxmeNf6ENR95yplWP8HyxIU8dnETh4abQbq4kxjvM" +
                "NC7mLuk0q4JaJFXzpZiNIqBqNHjrnKNxSMLSg5P+OPSlcuusUr8BzakVCfwDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
