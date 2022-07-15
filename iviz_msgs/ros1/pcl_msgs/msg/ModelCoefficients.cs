/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class ModelCoefficients : IDeserializable<ModelCoefficients>, IMessageRos1
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "values")] public float[] Values;
    
        /// Constructor for empty message.
        public ModelCoefficients()
        {
            Values = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public ModelCoefficients(in StdMsgs.Header Header, float[] Values)
        {
            this.Header = Header;
            this.Values = Values;
        }
        
        /// Constructor with buffer.
        public ModelCoefficients(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Values);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ModelCoefficients(ref b);
        
        public ModelCoefficients RosDeserialize(ref ReadBuffer b) => new ModelCoefficients(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Values);
        }
        
        public void RosValidate()
        {
            if (Values is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + 4 * Values.Length;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "pcl_msgs/ModelCoefficients";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ca27dea75e72cb894cd36f9e5005e93e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE62RQYsUMRCF7/kVBXPYXWFW0NuAN9H1IAi7N5GhJqnpLkgnbap61v73vvTgqjcPNoGQ" +
                "znvfq1Q9CCdpNG5bOOfK/vbN12904byIhRDe/ecvfH78eCDzdJxssNcP1+AdPTqXxC3RJM6JnelcUZcO" +
                "o7R9lotkmHiaJdF26+ssdg/j06hGWIMUaZzzSotB5JVinaalaGQXcp3kLz+cWohp5uYal8wN+tqSli4/" +
                "N56k07FMvi9SotCn9wdoiklcXFHQCkJswqZlwCWFRQu61w1h9/Rc9zjKgO6+hJOP7L1Y+TE3sV4n2wEZ" +
                "r66PuwcbzRGkJKPb7d8RR7sjhKAEmWsc6RaVf1l9rAVAwaya8ilLB0d0ANSbbrq5+4NcNnThUn/hr8Tf" +
                "Gf+CLS/c/qb9iJnl/npbBjQQwrnViyZIT+sGiVmlOGU9NW5r6K5rZNh96D2GCK5tItjZrEbFABI9q4/B" +
                "vHX6No2jphB+ArSKNHWvAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
