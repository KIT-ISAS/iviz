using System.Runtime.Serialization;

namespace Iviz.Msgs.tf2_msgs
{
    public sealed class TF2Error : IMessage
    {
        public const byte NO_ERROR = 0;
        public const byte LOOKUP_ERROR = 1;
        public const byte CONNECTIVITY_ERROR = 2;
        public const byte EXTRAPOLATION_ERROR = 3;
        public const byte INVALID_ARGUMENT_ERROR = 4;
        public const byte TIMEOUT_ERROR = 5;
        public const byte TRANSFORM_ERROR = 6;
        
        public byte error { get; set; }
        public string error_string { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TF2Error()
        {
            error_string = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TF2Error(byte error, string error_string)
        {
            this.error = error;
            this.error_string = error_string ?? throw new System.ArgumentNullException(nameof(error_string));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TF2Error(Buffer b)
        {
            this.error = b.Deserialize<byte>();
            this.error_string = b.DeserializeString();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TF2Error(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.error);
            b.Serialize(this.error_string);
        }
        
        public void Validate()
        {
            if (error_string is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(error_string);
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "tf2_msgs/TF2Error";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "bc6848fd6fd750c92e38575618a4917d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0XKuw7CMAyF4T1PkUfgLhaGqARkkdiVcSqYMiHUpUihfX8kasJ4/u9M/TDuLVL2zMT2" +
                "YBdm+qZAdEltzUvNDSH6RqADuVdcKfqbsGspOAHCqmtVwM4FOGbH5xQ9Sj1s9CAQPaV/3/46O7yeiGOV" +
                "nVF6lPIq5j2WfnjOI8/DmA8cjH3N2gAAAA==";
                
    }
}
