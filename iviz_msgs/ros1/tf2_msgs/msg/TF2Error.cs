/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class TF2Error : IHasSerializer<TF2Error>, IMessage
    {
        public const byte NO_ERROR = 0;
        public const byte LOOKUP_ERROR = 1;
        public const byte CONNECTIVITY_ERROR = 2;
        public const byte EXTRAPOLATION_ERROR = 3;
        public const byte INVALID_ARGUMENT_ERROR = 4;
        public const byte TIMEOUT_ERROR = 5;
        public const byte TRANSFORM_ERROR = 6;
        [DataMember (Name = "error")] public byte Error;
        [DataMember (Name = "error_string")] public string ErrorString;
    
        public TF2Error()
        {
            ErrorString = "";
        }
        
        public TF2Error(byte Error, string ErrorString)
        {
            this.Error = Error;
            this.ErrorString = ErrorString;
        }
        
        public TF2Error(ref ReadBuffer b)
        {
            b.Deserialize(out Error);
            b.DeserializeString(out ErrorString);
        }
        
        public TF2Error(ref ReadBuffer2 b)
        {
            b.Deserialize(out Error);
            b.Align4();
            b.DeserializeString(out ErrorString);
        }
        
        public TF2Error RosDeserialize(ref ReadBuffer b) => new TF2Error(ref b);
        
        public TF2Error RosDeserialize(ref ReadBuffer2 b) => new TF2Error(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Error);
            b.Serialize(ErrorString);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Error);
            b.Serialize(ErrorString);
        }
        
        public void RosValidate()
        {
            if (ErrorString is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 5;
                size += WriteBuffer.GetStringSize(ErrorString);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size += 1; // Error
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, ErrorString);
            return size;
        }
    
        public const string MessageType = "tf2_msgs/TF2Error";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bc6848fd6fd750c92e38575618a4917d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0XKuw7CMAyF4T1PkUfgLhaGqARkkdiVcSqYMiHUpUihfX8kasJ4/u9M/TDuLVL2zMT2" +
                "YBdm+qZAdEltzUvNDSH6RqADuVdcKfqbsGspOAHCqmtVwM4FOGbH5xQ9Sj1s9CAQPaV/3/46O7yeiGOV" +
                "nVF6lPIq5j2WfnjOI8/DmA8cjH3N2gAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TF2Error> CreateSerializer() => new Serializer();
        public Deserializer<TF2Error> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TF2Error>
        {
            public override void RosSerialize(TF2Error msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TF2Error msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TF2Error msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TF2Error msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(TF2Error msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<TF2Error>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TF2Error msg) => msg = new TF2Error(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TF2Error msg) => msg = new TF2Error(ref b);
        }
    }
}
