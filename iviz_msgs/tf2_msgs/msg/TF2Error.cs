/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/TF2Error")]
    public sealed class TF2Error : IDeserializable<TF2Error>, IMessage
    {
        public const byte NO_ERROR = 0;
        public const byte LOOKUP_ERROR = 1;
        public const byte CONNECTIVITY_ERROR = 2;
        public const byte EXTRAPOLATION_ERROR = 3;
        public const byte INVALID_ARGUMENT_ERROR = 4;
        public const byte TIMEOUT_ERROR = 5;
        public const byte TRANSFORM_ERROR = 6;
        [DataMember (Name = "error")] public byte Error { get; set; }
        [DataMember (Name = "error_string")] public string ErrorString { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TF2Error()
        {
            ErrorString = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TF2Error(byte Error, string ErrorString)
        {
            this.Error = Error;
            this.ErrorString = ErrorString;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TF2Error(ref Buffer b)
        {
            Error = b.Deserialize<byte>();
            ErrorString = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TF2Error(ref b);
        }
        
        TF2Error IDeserializable<TF2Error>.RosDeserialize(ref Buffer b)
        {
            return new TF2Error(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Error);
            b.Serialize(ErrorString);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ErrorString is null) throw new System.NullReferenceException(nameof(ErrorString));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += BuiltIns.UTF8.GetByteCount(ErrorString);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/TF2Error";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bc6848fd6fd750c92e38575618a4917d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACkXKywrCMBCF4X3Ad8gjeMeNi1BjGUxmyjgpdpWVSDctxPb9BRvj8vzfmfthOmmkaJmJ" +
                "9Vmv1fxNjugWmpI3OVeEaCuBFqQruM1oH8KmIWcECIvusgK2xsElGq6DtyjlsM8HAW8p/Pvh19ng/Urs" +
                "ixxVpmdKY1LvKfXDaxlxGWqlPpexWmnbAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
