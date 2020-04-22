
namespace Iviz.Msgs.std_msgs
{
    public sealed class Int16MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public short[] data; // array of data
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Int16MultiArray";
    
        public IMessage Create() => new Int16MultiArray();
    
        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 2 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Int16MultiArray()
        {
            layout = new MultiArrayLayout();
            data = new short[0];
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
        public const string Md5Sum = "d9338d7f523fcb692fae9d0a0e9f067c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR9N+Q/XJKXNkuzfJSyFvIQKOylhUEHZYRQVOs6VmJbQZKbdb9+R/Jn2tcx" +
                "YbB9P885utKIfmQsLFOm9YGEI5cyPZaZU2tjxPuDeNelo5ytFTsmyYkqlFO6oESbaERSx2XOhRPBhkdk" +
                "GeU+Xfh0O42iT8Uoq9/VGpE9cqwSFddFEpLCiToqUoWb32y2TTRW8LZrRKFTkxZFg2j1j9cgenz6fkfW" +
                "yZfc7uzXj4wGEOInZOt4Q6g4E4YtCdpxwUbFlfdKKshlwVNkHXCBAkdhnIpLZFUE3fuRp0T3TTxKGSZt" +
                "JBuWlBidE1qzoVxbh3ynSRVF/X8me1sCKqI9FFu3ijUuOhp9ZCBgG5WQfLkIKF50kljubdVRSKmKHXHG" +
                "ftsBynkshev0R/k4xrxoY8mmuswkrR+e17+e6JXpZJRzXAAqAXtuz0FYZ5RkVBCFbKYCZAPPK8+rF5so" +
                "43mOCE8n/IWa7CeHS1oFMJs+hy8++aVqsZlvx+rcstiO97ActtHIUwAWgBBGTmh5FacC0mZ0cz37ff1t" +
                "Rir3h+GkXAoiwIYT9Aacsc60oTrYosopsAftjouwd6EBOm9m22kmXlEXcIcpq13qhp3Lqj8MzVeEjj1r" +
                "QAvrcgw0Y49mRbeL+c1sRnRRaMd1ZC0mKUv7EsqFclA7YL+sC877CE5KunTYeVoAaNSzngHAe367aNyL" +
                "frlah2Hnawsue7a2XJDl804aThiThPH2N5OX3OjThPb4gN5lXkzCtBz8f9Vx+l+vgPtmIgeR54KzUUuA" +
                "01J9QfSdesPQt8PbHLFaEH8F1rvzIZAu/EHBTUAlrl172SZWqvnE6utz6iD6C/9dXXvbBQAA";
                
    }
}
