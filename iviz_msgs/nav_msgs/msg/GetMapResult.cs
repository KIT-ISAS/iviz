/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GetMapResult : IDeserializable<GetMapResult>, IResult<GetMapActionResult>
    {
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map;
    
        /// Constructor for empty message.
        public GetMapResult()
        {
            Map = new NavMsgs.OccupancyGrid();
        }
        
        /// Explicit constructor.
        public GetMapResult(NavMsgs.OccupancyGrid Map)
        {
            this.Map = Map;
        }
        
        /// Constructor with buffer.
        internal GetMapResult(ref Buffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMapResult(ref b);
        
        GetMapResult IDeserializable<GetMapResult>.RosDeserialize(ref Buffer b) => new GetMapResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Map is null) throw new System.NullReferenceException(nameof(Map));
            Map.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VS2/TQBC+768YqQdalITwEEKVOCBVPA4VRZRTVEWT9cResHfN7rrB/Hq+XdtJAwhx" +
                "oERW7B3P45tvHrZ8u25CGR6917pr2er+jTcFNdyql//4py4/vjkn+9uA6oSuKxPIS+sliI2BmJ7ML6gc" +
                "0czIWNpVRlckjD8tdX1XO1ZCrXcb3pjaxJ7cFi7dFGKh1FvhQjxVw02pk0uJfMGRaet8Nk85X3K7lxu7" +
                "dSoDy++ogDDD8G43b/gzzJyHsxmFyD4aW9LOxIpOl7Pl2YJonyB8HKAZQWpekp8U1LMthVbL2ePl8gZG" +
                "n+wX63aWwMX88UIZG1+sbnJodU8FCbEYCjIwBLAfI9uCPXgHFcVEUWXKSvy8llupU8ZNK0UGRrFvJSym" +
                "EuIqxYrnuu6pC1CKjrRrms4azVEomkaO7GEJNpjaxKLuavbQB7XGJvWt50aSd1xBvnZitdC7i3Po2CC6" +
                "iwaAenjQXjikKry7INWBuadPkoE6ud65OY5SovD74GCfYwIr31ITJZwczhHj4ZDcAr5BjiBKEeg0y9Y4" +
                "hjNCEECQ1qEPT4H8qo+VG8p5y97wpkZ5A2kwAK8PktGDszueE+w0CNZN7gePhxh/49bu/aac5hVqVqfs" +
                "Q1eCQCii525NAdVNn53o2mBUqDYbz75XyWoIqU5eJ46hBKtcEdw5BKcNClDkrlYh+uQ9V2ONgb3v9XBn" +
                "EqfOqlyNZFBjnWfTNxwNCOKN6+KQYcWedRRvAlaC22bh8Z4Z5zknj/oPG2WcftpxoNphCoqBHcjW6bxO" +
                "pzubAP3i6i4HXzWP0iq6UVsopo47vIMBkgB7BZbCKmmFm6kvs3BUqASTFX/WGKRjUOdNiY4YM0oQVs2M" +
                "cHku0taYBi9vFOF6vnMeXLVosNEIjvLKzMtpWj5wtFClOMy57wfar7JJDndPFf41HrC9OuzyoahAndED" +
                "6dYLGrVlLbO0R5K4GN+boQEszt5MtgtSVw4k7hXUhw597G32e9BT/ylBQJk6GNMe2djxczXhRy5Yfhny" +
                "Uboq99TzZ/Rt/9Tvn77/H/gH6n73iT7i8xh8On098J7GFV/hP2c0Pe2U+gEOzGN+mAgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
