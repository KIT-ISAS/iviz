/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapResult")]
    public sealed class GetMapResult : IDeserializable<GetMapResult>, IResult<GetMapActionResult>
    {
        [DataMember (Name = "map")] public NavMsgs.OccupancyGrid Map { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapResult()
        {
            Map = new NavMsgs.OccupancyGrid();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapResult(NavMsgs.OccupancyGrid Map)
        {
            this.Map = Map;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMapResult(ref Buffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapResult(ref b);
        }
        
        GetMapResult IDeserializable<GetMapResult>.RosDeserialize(ref Buffer b)
        {
            return new GetMapResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Map is null) throw new System.NullReferenceException(nameof(Map));
            Map.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Map.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/TQBC9R8p/GIkDLUpC+BBClTggVXwcKoqAU1RFk/XEXrB3ze66Ifx63qzjuAUk" +
                "OFCqNLHXM2/evPmw4+t1E8v48J0xXcvO7F8HW1DD7XTy4h//TScXH16fkfttyOnkHn2sbKQgbZAoLkVi" +
                "ejw/p/JAaEbW0a6ypiJhfBmp65vWqRJqg9/wxtY27clvFdMPQRbTyXTyRriQQFX/oyf3LiTxOSemrQ8Z" +
                "I+d+we3xgXVbn01BMD+mAseZTvC7ecOf4ekDEGcUE4dkXUk7myo6Wc6WpwuiY6YKMnK0ghyDKJAGDuxK" +
                "odVy9mi5vILXJ/fF+Z0jiDJ/BPrWpeerqxxc6dxReWIq+vL0WinjD4ldwQFVgCTFoFVly0rCvJZrqTXv" +
                "ppUik6O0byUujhXFpxQnget6T12EVfJkfNN0zhpOQsk2cgtAXSEKU6tqmq7mAAdIbJ3abwM3kvH1P8rX" +
                "TpwRent+BisXxXTJgtQeGCYIR63H23MYd5DwyWP1gOPHnZ/jXkp0wpEBCsFJGcs3bSwly/FMwzzoc1wA" +
                "HiIJAhWRTvLZGrfxlBAHLKT1aM4T0L/cp8r3pb3mYHlTo9SRDHQA7H11un96E1qp63w4P+D3kGOQv8F1" +
                "I7CmNa9QvFoliF0JHWGJDry2BWw3+4xiaosJotpuAgf0qLr1QQHySsWGGfxybfDLMXpjUYkit/l0ElPQ" +
                "ALkua53lu18eNwb02GiVr5ET6m3yzIaGk4VQvPFd6hOtOLBJEmzEwvDbfPjTGhrmPIuAXug3zmEx0I4j" +
                "1R6DActsgcO1Hqz17uaOQPf4ussEVs1DXVZX08kWptqB40N1QS4QssDCWKldvDp2aj4dTCrBxKVfbPrj" +
                "IbQPtkSPHHJTIqtmRvgELnSpDBOZF45wPd/5ANVatNzBSZHybs3ba1hOQMLEleKxA8K+r8FldsoB76zg" +
                "v0ZUfi/Hxd/XGNRzCmC7DYLubdnITLeMHheH57bvB4f7YAffBRbDpYeWR4vp5H2H7g4uI4+Wd9jXP6cJ" +
                "Ose2xipIbN3hDTdkgYywHzPvW0kfmuzZU/o2XmKoh8vv/y2LUcTfvtxvSXs7B737OpZARzm/vv+Y2XC5" +
                "U+sfhdsU9NwIAAA=";
                
    }
}
