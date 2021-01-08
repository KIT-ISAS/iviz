/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Float64MultiArray")]
    public sealed class Float64MultiArray : IDeserializable<Float64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public double[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Float64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<double>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Float64MultiArray(MultiArrayLayout Layout, double[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Float64MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Float64MultiArray(ref b);
        }
        
        Float64MultiArray IDeserializable<Float64MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Float64MultiArray(ref b);
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
                size += 8 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Float64MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4b7d974086d4060e7db4613a7e6c3ba4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9C/QfBuuSuo7rjxCagA+GQC8pFFIoxZiw0Y6stVdas7uKm/76vtWXlaTH" +
                "UiGj9czOzHtvZzahb5qFY9LGHEh48jnT10p7tbZWvNyLF1N5Ktg5sWOSnKlSeWVKyoyNo4SkSauCSy9q" +
                "I16hNRUhXoR4N42jOHqXj3T7bZ6E3JFTlam0TZORFF60u+Io00b466vNtgto3P2TUF2siwsl42j1jx/Q" +
                "ePhyS87Lx8Lt3Ke3pIIa3yHemTzkSrWw7EjQjku2Km28l1JBMweqQp+hA3dCR2G9SiuENRz9y5GnRHdd" +
                "AHJZJmMlW5aUWVMQarOlwrgagjekyrI1vFG/zwIlAQG6rXvdOhcdrTkyQLCLo0qVfrmokTyaLHM8OLKj" +
                "kFKVO2LNoQEAzAc8pR8eAyqkKZrHWEcuN5WWtL7/sf75QE9MJ6u85xJ4CQwK9xqH81ZJDilEKbsGAeea" +
                "7mVgN9icKVuzTSj8zkdwoSb7yeEDrWpEmyGRjyH8samymW/H6rVlsR3vYTlskbDmAUAAIqyc0PIyzQVE" +
                "1nR9Nft19XlGqgjjcVI+Bxvgw0w9A2tqtLHUboaeCZ1qDUD+TEi427YGym9m26kWT0gNzKOc1S73o4HP" +
                "qd8M+VeEqkNzDRrm5RiQxgHSim4W8+vZjOiiNJ7bna2spBztK0hY54PuNYEPXcb5EMRJSZ+PBq4eA0oN" +
                "za8w4Du/WfT+xTBjK8ho4OxzLofGPmMr0PuDtZwxegtdHy6uoL81pwntsYD4VVFO6vY5hP9N1en/vRzu" +
                "uh6No0AHA9PqgBFqVlB/p54xB30794PXqhIuyPac3uykizA8uCOowr3scIBdZCNdiGxWf6vyB4Z6TOD9" +
                "BQAA";
                
    }
}
