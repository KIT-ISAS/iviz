/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class OccupancyGrid : IDeserializable<OccupancyGrid>, IMessage
    {
        // This represents a 2-D grid map, in which each cell represents the probability of
        // occupancy.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        //MetaData for the map
        [DataMember (Name = "info")] public MapMetaData Info;
        // The map data, in row-major order, starting with (0,0).  Occupancy
        // probabilities are in the range [0,100].  Unknown is -1.
        [DataMember (Name = "data")] public sbyte[] Data;
    
        public OccupancyGrid()
        {
            Info = new MapMetaData();
            Data = System.Array.Empty<sbyte>();
        }
        
        public OccupancyGrid(in StdMsgs.Header Header, MapMetaData Info, sbyte[] Data)
        {
            this.Header = Header;
            this.Info = Info;
            this.Data = Data;
        }
        
        public OccupancyGrid(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Info = new MapMetaData(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        public OccupancyGrid(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Info = new MapMetaData(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        public OccupancyGrid RosDeserialize(ref ReadBuffer b) => new OccupancyGrid(ref b);
        
        public OccupancyGrid RosDeserialize(ref ReadBuffer2 b) => new OccupancyGrid(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Info.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Info.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Info is null) BuiltIns.ThrowNullReference();
            Info.RosValidate();
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 80 + Header.RosMessageLength + Data.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            Info.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        public const string MessageType = "nav_msgs/OccupancyGrid";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3381f2d731d4076ec5c71b0759edbe4e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTW/TQBC9768YKQdalIQUEEKVOCBVfBwqytcpiqLJemIv2Ltmd91gfj1v13GSgoQ4" +
                "UCqrsdfz8ebNm/GEPlUmkJfWSxAbAzE9nl1R6U1BDbdTMpZ2ldEVCeOflro+tY6VUOvdhjemNrEnt1UT" +
                "clp3LVvdz5V6I1yIp2r4UWpyLZGvODJtnc/uyKKuuT2cG7t1sAOw/I4KHGYY3u1mDX+Bm/MINqUQ2Udj" +
                "S9qZWNHZYro4nxO9G7MjxhGaEZTmJcVJST3bUmi5mF4sFis4fbZfrdtZAhezi7kyNj5frnJqpV784z91" +
                "/fH1JcAX6yaU4dHAEMB+jGwL9uAdVBQjRZUpK/GzWm6lThU3rRQZGMW+lTDPTAE2rlKseK7rnroAo+hI" +
                "u6bprNEchaJp5I4/PMEGU5tY1F3NHvag1thkvvXcSIqOK8i3TqwWent1CRsbRHfRAFCPCNoLh9SFt1ek" +
                "OjD35HFyoAktP7hwsVKTTzs3w7mUUMABBdrAMaGW70lNCTCHSyR7OFQ5RxKwJEhXBDrLZ2s8hnNCNmCR" +
                "1kGQZyjhpo+VG/p6y97wppYUWIMKRH2QnB6cn0S2ObRl68bwQ8Rjjr8Jaw9xU02zCs2rEw2hK8EkDCG+" +
                "W1PAdNPnILo2mBmqzcaz71XyGlKqyatENozglVuDXw7BaYNOFFneKkSfoue2rE1xX7K0fDvI8mQkR4lV" +
                "rkYxaLbOQ+objgYE8cZ1caiwYs86ijcBu8Ft8+FhHl/7BHsY7Fw8+j+slv0aoB0Hqh3GoRjYwdk6Pa/T" +
                "08lKgF5c3eXky+ZR2kkrtYVhkt7xHRyuU1BTYDssk1VYjQLNh3uDSjBi8VeL4XSf1HlTQhH7ihKEZTMl" +
                "XJ6LtD7GCcyrRbie7ZwHVy0EtndCoLw785YatxACzVUpDgPv+4H2m+yS091Th3/PB2wvj0t9aCpQZ/RA" +
                "uvUCobasZZoWSjou9u/NIABbJMij75zUjQOJBwP1voOOvc1xj3b3JeFfCwSUUcGY9sjG7r9bI37Ugi2Y" +
                "Id8pd9DUs6f0/XDXH+5+/B/4R+rGGk6/1Xf4vAs+PX078p7GFZ/jP1c03u2U+gkDSqydGQgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
