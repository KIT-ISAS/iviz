/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Int16MultiArray")]
    public sealed class Int16MultiArray : IDeserializable<Int16MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public short[] Data; // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<short>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int16MultiArray(MultiArrayLayout Layout, short[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Int16MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<short>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int16MultiArray(ref b);
        }
        
        Int16MultiArray IDeserializable<Int16MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int16MultiArray(ref b);
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 2 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int16MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d9338d7f523fcb692fae9d0a0e9f067c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
