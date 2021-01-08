/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Float32MultiArray")]
    public sealed class Float32MultiArray : IDeserializable<Float32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public float[] Data { get; set; } // array of data
    
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
        public Float32MultiArray(ref Buffer b)
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 4 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Float32MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6a40e0ffa6a17a503ac3f8616991b1f6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9C/QfBuuSuI7rjxCagA+GQC8pFFIoxZiw0Y6stVdas7uKm/76vtWXlbTH" +
                "UiGj9czOzHtvZzahr5qFY9LGHEh48jnTl0p7tbZWvD6IV1N5Ktg5sWOSnKlSeWVKyoyNo4SkSauCSy9q" +
                "I16hNRUhXoR4N42jOPojH+n22zwJuSOnKlNpmyYjKbxod8VRpo3wy8Vm2wU07v5JqC7WxYWScbT6xw9o" +
                "PH6+I+flU+F27uN7UkGNbxDvTB5ypVpYdiRoxyVblTbeK6mgmQNVoc/QgTuho7BepRXCGo7+9chTovsu" +
                "ALksk7GSLUvKrCkItdlSYVwNwRtSZdka3qnfZ4GSgADd1r1unYuO1hwZINjFUaVK6F4jeTJZ5nhwZEch" +
                "pSp3xJpDAwCYD3hKPzwGVEhTNI+xjlxuKi1p/fB9/eORnplOVnnPJfASGBTuLQ7nrZIcUohSdg0CzjXd" +
                "q8BusDlTtmabUPidj+BCTfaTwyWtakSbIZEPIfypqbKZb8fqrWWxHe9hOWyRsOYBQAAirJzQ8irNBUTW" +
                "dHM9+3n9aUaqCONxUj4HG+DDTL0Aa2q0sdRuhp4JnWoNQP5MSLi7tgbKb2bbqRbPSA3Mo5zVLvejgc+p" +
                "Xwz5V4SqQ3MNGublGJDGAdKKbhfzm9mM6KI0ntudraykHO0rSFjng+41gcsu43wI4qSkz0cDV48BpYbm" +
                "Nxjwnd8uev9imLEVZDRw9jmXQ2OfsRXoz4O1nDF6C10fLq6gvzWnCe2xgPhVUU7q9jmE/03V6f+9HO67" +
                "Ho2jQAcD0+qAEWpWUH+nXjAHfTv3g9eqEi7I9pze7aSLMDy4I6jCvexwgF1kI12IbFZ/q/Ibm6G8DP0F" +
                "AAA=";
                
    }
}
