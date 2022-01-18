/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TF2Error : IDeserializable<TF2Error>, IMessage
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
    
        /// Constructor for empty message.
        public TF2Error()
        {
            ErrorString = string.Empty;
        }
        
        /// Explicit constructor.
        public TF2Error(byte Error, string ErrorString)
        {
            this.Error = Error;
            this.ErrorString = ErrorString;
        }
        
        /// Constructor with buffer.
        public TF2Error(ref ReadBuffer b)
        {
            Error = b.Deserialize<byte>();
            ErrorString = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TF2Error(ref b);
        
        public TF2Error RosDeserialize(ref ReadBuffer b) => new TF2Error(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Error);
            b.Serialize(ErrorString);
        }
        
        public void RosValidate()
        {
            if (ErrorString is null) throw new System.NullReferenceException(nameof(ErrorString));
        }
    
        public int RosMessageLength => 5 + BuiltIns.GetStringSize(ErrorString);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "tf2_msgs/TF2Error";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "bc6848fd6fd750c92e38575618a4917d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0XKuw7CMAyF4T1PkUfgLhaGqARkkdiVcSqYMiHUpUihfX8kasJ4/u9M/TDuLVL2zMT2" +
                "YBdm+qZAdEltzUvNDSH6RqADuVdcKfqbsGspOAHCqmtVwM4FOGbH5xQ9Sj1s9CAQPaV/3/46O7yeiGOV" +
                "nVF6lPIq5j2WfnjOI8/DmA8cjH3N2gAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
