
namespace Iviz.Msgs.std_msgs
{
    public sealed class Float64MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public double[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Float64MultiArray";
    
        public IMessage Create() => new Float64MultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 8 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Float64MultiArray()
        {
            layout = new MultiArrayLayout();
            data = System.Array.Empty<0>();
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
        public const string Md5Sum = "4b7d974086d4060e7db4613a7e6c3ba4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
