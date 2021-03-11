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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhUEHZYRQVOs6ViJbQZKbdb9+R/5O+zpm" +
                "DJbv5zlHVxrRD83CMWljDiQ8+ZTpsdBera0V7w/i3RSeMnZO7JgkJypXXpmcEmOjEUkTFxnnXpQ2vEJr" +
                "ykK6COluGkWfipGuv9UzInfkWCUqroskJIUXdVSUaCP8zfVm28RX3vYZUdmpSYuiQbT6x88genz6fkfO" +
                "y5fM7dzXj4wGEOInZOt4Q6hYC8uOBO04Z6viynslFeRy4Cl0B1ygwFFYr+ICWRVB/37kKdF9E49SlslY" +
                "yZYlJdZkhNZsKTPOI98bUnle/5/J3paAhmgPxdatYo2LjtYcGQjYRYXK/XJRongxSeK4t1VHIaXKd8Sa" +
                "w7YDlA9Yct/pj/JxjHkx1pFLTaElrR+e17+e6JXpZJX3nAMqAXvmzkE4b5VkVBC5bKYCZEueV4FXLzZR" +
                "NvAcEd5O+As12U8Ol7QqwWz6HL6E5JeqxWa+Hatzy2I73sNy2EajQAFYAEJYOaHlVZwKSKvp5nr2+/rb" +
                "jFQWDsNJ+RREgA0n6A04Y6ONpTrYocqpZA/aHRfh7soG6LyZbadavKIu4A5TVrvUDzuXU38Ymq8IHXvW" +
                "Ei2syzHQjAOaFd0u5jezGdFFbjzXkbWYpBztCyhXloPaJfbLuuC8j+CkpE+HnacFgEY96xkAfOe3i8a9" +
                "6JerdRh2vrbgsmdry5WyfN5JywljkjDe4WYKkltzmtAeC+hdZPmknJZD+K86Tv/rFXDfTOQgClxwNmoJ" +
                "cFqqFUTfqTcMfTu8zRGrBQlXYL07HwLpIhwU3ARU4Np1l21ipVpIrFafUwfRXz2sjMvbBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
