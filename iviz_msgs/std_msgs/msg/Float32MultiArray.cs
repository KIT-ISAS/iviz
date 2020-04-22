
namespace Iviz.Msgs.std_msgs
{
    public sealed class Float32MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public float[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Float32MultiArray";
    
        public IMessage Create() => new Float32MultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 4 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Float32MultiArray()
        {
            layout = new MultiArrayLayout();
            data = new float[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            layout.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            layout.Serialize(ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6a40e0ffa6a17a503ac3f8616991b1f6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
