/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapResult : IDeserializableRos1<GetMapResult>, IMessageRos1, IResult<GetMapActionResult>
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
        public GetMapResult(ref ReadBuffer b)
        {
            Map = new NavMsgs.OccupancyGrid(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GetMapResult(ref b);
        
        public GetMapResult RosDeserialize(ref ReadBuffer b) => new GetMapResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Map.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Map is null) BuiltIns.ThrowNullReference();
            Map.RosValidate();
        }
    
        public int RosMessageLength => 0 + Map.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/GetMapResult";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6cdd0a18e0aff5b0a3ca2326a89b54ff";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTW/TQBC9768YqQdalITwIYQqcUCq+DhUFFFOURVN1hN7wd41u+sG8+t5u7aTBhDi" +
                "QKmsxl7PvHnz5sOWb9dNKMOj91p3LVvdv/GmoIZb9fIf/6nLj2/Oyf42oDqh68oE8tJ6CWJjIKYn8wsq" +
                "RzYzMpZ2ldEVCeOflrq+ax0roda7DW9MbWJPbgtIN4VYKPVWuBBP1fCj1MmlRL7gyLR1PrunnC+53Z8b" +
                "u3UqE8vvqMBhpuHdbt7wZ7g5D7AZhcg+GlvSzsSKTpez5dmCaJ8gMA7UjCA1LwknBfVsS6HVcvZ4ubyB" +
                "0yf7xbqdJWgxf7xQxsYXq5scWt1TQUIshoIMCoHsx8i2YA/dIUUxSVSZshI/r+VW6pRx00qRiVHsWwmL" +
                "qYS4SrHiua576gKMoiPtmqazRnMUiqaRI394Qg2mNqmou5o97CGtscl867mRhI4ryNdOrBZ6d3EOGxtE" +
                "d9GAUA8E7YVDqsK7C1IdlHv6JDmok+udm+NRShR+Hxzqc0xk5VtqosSTwzliPBySWwAb4giiFIFO89ka" +
                "j+GMEAQUpHXow1Mwv+pj5YZy3rI3vKklAWsoANQHyenB2R1km6EtWzfBD4iHGH8Da/e4Kad5hZrVKfvQ" +
                "lRAQhui5W1PAdNNnEF0bjArVZuPZ9yp5DSHVyeukMYzglSuCXw7BaYMCFLmrVYg+oedqrDGw970e7kzi" +
                "1FmVq5EMaqzzbPqGo4FAvHFdHDKs2LOO4k3ASnDbfHi8Z8Z5zsmj/sNGGaefdhyodpiCYlAHZ+v0vE5P" +
                "dzYB+sXVXQ6+ah6lVXSjtjBMHXd4B4fLBGoKLIVVsgo3U1/mw9GgEkxW/NliOB2DOm9KdMSYUaKwamaE" +
                "y3ORtsY0eHmjCNfznfPQqkWDjU4AyiszL6dp+QBooUpxmHPfD7JfZZcc7p4q/Gs8cHt12OVDUcE6swfT" +
                "rRc0astaZmmPpONifG+GBrBFojz5LkhdOYi4N1AfOvSxtxn3YHdfLfxzgqAydTCmPbKx4+dq4o9csPwy" +
                "5aN0h556/oy+7e/6/d33/0P/IN3vPtFHeh6TT09fD7qnccVX+M8ZTXc7pX4ADsxjfpgIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
