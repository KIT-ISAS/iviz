/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [Preserve, DataContract (Name = "pcl_msgs/ModelCoefficients")]
    public sealed class ModelCoefficients : IDeserializable<ModelCoefficients>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "values")] public float[] Values { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ModelCoefficients()
        {
            Values = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ModelCoefficients(in StdMsgs.Header Header, float[] Values)
        {
            this.Header = Header;
            this.Values = Values;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ModelCoefficients(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Values = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ModelCoefficients(ref b);
        }
        
        ModelCoefficients IDeserializable<ModelCoefficients>.RosDeserialize(ref Buffer b)
        {
            return new ModelCoefficients(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Values, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Values is null) throw new System.NullReferenceException(nameof(Values));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += 4 * Values.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "pcl_msgs/ModelCoefficients";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ca27dea75e72cb894cd36f9e5005e93e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
