/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Float32MultiArray")]
    public sealed class Float32MultiArray : IDeserializable<Float32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public float[] Data; // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Float32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Float32MultiArray(MultiArrayLayout Layout, float[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Float32MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Float32MultiArray(ref b);
        }
        
        Float32MultiArray IDeserializable<Float32MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Float32MultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 4 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Float32MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6a40e0ffa6a17a503ac3f8616991b1f6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhUEHZYRQVOs6ViJbQZKbdb9+R/5O+zpm" +
                "DJbv5zlHVxrRD83CMWljDiQ8+ZTpsdBera0V7w/i3RSeMnZO7JgkJypXXpmcEmOjEUkTFxnnXpQ2vEJr" +
                "ykK6COluGkWfipGuv9UzInfkWCUqroskJIUXdVSUaCP8crHZNvGVt31GVHZq0qJoEK3+8TOIHp++35Hz" +
                "8iVzO/f1I6MBhPgJ2TreECrWwrIjQTvO2aq48l5JBbkceArdARcocBTWq7hAVkXQvx95SnTfxKOUZTJW" +
                "smVJiTUZoTVbyozzyPeGVJ7X/2eytyWgIdpDsXWrWOOiozVHBgJ2UaFyCF6ieDFJ4ri3VUchpcp3xJrD" +
                "tgOUD1hy3+mP8nGMeTHWkUtNoSWtH57Xv57olelklfecAyoBe+bOQThvlWRUELlspgJkS55XgVcvNlE2" +
                "8BwR3k74CzXZTw6XtCrBbPocvoTkl6rFZr4dq3PLYjvew3LYRqNAAVgAQlg5oeVVnApIq+nmevb7+tuM" +
                "VBYOw0n5FESADSfoDThjo42lOtihyqlkD9odF+HuygbovJltp1q8oi7gDlNWu9QPO5dTfxiarwgde9YS" +
                "LazLMdCMA5oV3S7mN7MZ0UVuPNeRtZikHO0LKFeWg9ol9su64LyP4KSkT4edpwWARj3rGQB857eLxr3o" +
                "l6t1GHa+tuCyZ2vLlbJ83knLCWOSMN7hZgqSW3Oa0B4L6F1k+aSclkP4rzpO/+sVcN9M5CAKXHA2aglw" +
                "WqoVRN+pNwx9O7zNEasFCVdgvTsfAukiHBTcBFTg2nWXbWKlWkisVp9TB9FfRg1WkdsFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
