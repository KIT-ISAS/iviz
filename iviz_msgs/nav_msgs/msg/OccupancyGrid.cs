/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public OccupancyGrid()
        {
            Info = new MapMetaData();
            Data = System.Array.Empty<sbyte>();
        }
        
        /// Explicit constructor.
        public OccupancyGrid(in StdMsgs.Header Header, MapMetaData Info, sbyte[] Data)
        {
            this.Header = Header;
            this.Info = Info;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal OccupancyGrid(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Info = new MapMetaData(ref b);
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new OccupancyGrid(ref b);
        
        OccupancyGrid IDeserializable<OccupancyGrid>.RosDeserialize(ref Buffer b) => new OccupancyGrid(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Info.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Info is null) throw new System.NullReferenceException(nameof(Info));
            Info.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 80 + Header.RosMessageLength + Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/OccupancyGrid";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3381f2d731d4076ec5c71b0759edbe4e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/TQBC9768YKQdalIRQEEKVOCBVFA4VRZRTVEWT9cResHfd3XVT8+t5u46TFCTE" +
                "AYis2DuejzdvPjyhm8oE8tJ6CWJjIKaz2QWV3hTUcDslY2lbGV2RMP601PWxdqyEWu/WvDa1iT25jZqQ" +
                "07pr2ep+rtR74UI8VcNNqcmVRL7gyLRxPpsjirridi83duOgB2D5HRUQZhjebWcNf4WZ83A2pRDZR2NL" +
                "2ppY0cliujidE30co8PHAZoRpOYl+UlBPdtSaLmYPl8sbmH0xX6zbmsJXMyez5Wx8fXyNodW6s1f/qmr" +
                "z5fnAF+smlCGZwNDAPs5si3Yg3dQUYwUVaasxM9quZc6Zdy0UmRgFPtWwjwzBdi4SrHiua576gKUoiPt" +
                "mqazRnMUiqaRR/awBBtMbWJRdzV76INaY5P6xnMjyTuuIHedWC304eIcOjaI7qIBoB4etBcOqQofLkh1" +
                "YO7FWTJQk5utm+EoJQq/Dw72OSaw8pCaKOHkcI4YT4fk5vANcgRRikAnWbbCMZwSggCCtA59eALk132s" +
                "3FDOe/aG1zXKG0iDAXh9koyenB55TrDPybJ1o/vB4yHGn7i1e78pp1mFmtUp+9CVIBCK6Ll7U0B13Wcn" +
                "ujYYFarN2rPvVbIaQqrJu8QxlGCVK4I7h+C0QQGK3NUqRJ+852qsTPGvutHy/dCNR5M4dlblaiSDGus8" +
                "m77haEAQr10Xhwwr9qyjeBOwEtwmC/djeIlVMs5zTh71HzbKbvppy4FqhykoBnYgW6XzKp2ONgH6xdVd" +
                "Dr5snqVVdKs2UEwdd3gHAyQB9goshWXSCrdjX2bhTqESTFb8WWOQ7oI6b0p0xC6jBGHZTAmX5yJtjXHw" +
                "8kYRrmdb58FViwbbGcFRXpl5OY3LB47mqhSHOff9QPt1Nsnh/lGFf40HbG8Pu3woKlBn9EC68YJGbVnL" +
                "NO2RJC52783QABZnb0bbOalrBxL3CupThz72Nvs96Kn/lCCgjB2MaY9s7O5zNeJHLlh+GfKjdFXuqVcv" +
                "6WH/1O+fvv8f+AfqxhyOP9GP+HwMPp3uDrynccVX+PcZjU9bpX4AKZHLdhAIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
