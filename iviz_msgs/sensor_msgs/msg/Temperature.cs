/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Temperature")]
    public sealed class Temperature : IDeserializable<Temperature>, IMessage
    {
        // Single temperature reading.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp is the time the temperature was measured
        // frame_id is the location of the temperature reading
        [DataMember (Name = "temperature")] public double Temperature_ { get; set; } // Measurement of the Temperature in Degrees Celsius
        [DataMember (Name = "variance")] public double Variance { get; set; } // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public Temperature()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Temperature(StdMsgs.Header Header, double Temperature_, double Variance)
        {
            this.Header = Header;
            this.Temperature_ = Temperature_;
            this.Variance = Variance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Temperature(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Temperature_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Temperature(ref b);
        }
        
        Temperature IDeserializable<Temperature>.RosDeserialize(ref Buffer b)
        {
            return new Temperature(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Temperature_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Temperature";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ff71b307acdbe7c871a5a6d7ed359100";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2TT4vbQAzF7wZ/B0EOu1vItrSlh0BPXfrnsFDY3INiK/bQ8Yw7kpPm2/fNeNMkLIUe" +
                "OmBsj6WfNO/JtKAnFzovZDKMktimJJSEW+ze11Vd0Ve8SKJ+vp3XgswNosbDSE7Jeikb88MF7MBKg7Di" +
                "uQXub2tBu8SDbFx7ovnYsLkYKO5eQJ87LA3ufGT78P7q+0x8nMsOEuwEWV8EuUAP0iURpU/i1U16xdtz" +
                "chyaGVZ4b3JrLpikMYlJSzjan6gp/AjxEOrq439edfX49GVFau1m0E5fz4bUFawzDi2nFvoat2xMuwin" +
                "XNdLWnrZi6fiDzotX+04isLVBa37fBKlTgLU8P5IkyLKIjVxGKbgIP3s5xUgp0I1ppGTuWbynJAQE7zI" +
                "8cXBws+Xys9JsjLfHlaICirNZA5NHcFo4KDCQXxE8ARR373NGUhcH+Iyi9xh3M4jZj1b7lh+QXvNzbKu" +
                "cplX8xnvgYdIgkKt0m3Z2+BV7wh10IWMsenpFu1/P1qPscrjUNzbYvxBbqADsDc56ebuEp1bX1HgEE/8" +
                "GXku8i/cTHkG52Mte5jnswQ6ddARkWOKe9cidnsslMa7PLrebROnY12V/6sUBeRzFhthyCve4M6qsXFw" +
                "oqWDs76u1FIucPqz8nj/BokQ66byAwAA";
                
    }
}
