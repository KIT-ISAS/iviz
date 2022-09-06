/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class TransformStamped : IDeserializable<TransformStamped>, IHasSerializer<TransformStamped>, IMessage
    {
        // This expresses a transform from coordinate frame header.frame_id
        // to the coordinate frame child_frame_id
        //
        // This message is mostly used by the 
        // <a href="http://wiki.ros.org/tf">tf</a> package. 
        // See its documentation for more information.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> The frame id of the child frame </summary>
        [DataMember (Name = "child_frame_id")] public string ChildFrameId;
        [DataMember (Name = "transform")] public Transform Transform;
    
        public TransformStamped()
        {
            ChildFrameId = "";
        }
        
        public TransformStamped(in StdMsgs.Header Header, string ChildFrameId, in Transform Transform)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Transform = Transform;
        }
        
        public TransformStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out ChildFrameId);
            b.Deserialize(out Transform);
        }
        
        public TransformStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeString(out ChildFrameId);
            b.Align8();
            b.Deserialize(out Transform);
        }
        
        public TransformStamped RosDeserialize(ref ReadBuffer b) => new TransformStamped(ref b);
        
        public TransformStamped RosDeserialize(ref ReadBuffer2 b) => new TransformStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId);
            b.Serialize(in Transform);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId);
            b.Serialize(in Transform);
        }
        
        public void RosValidate()
        {
            if (ChildFrameId is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 60;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(ChildFrameId);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, ChildFrameId);
            size = WriteBuffer2.Align8(size);
            size += 56; // Transform
            return size;
        }
    
        public const string MessageType = "geometry_msgs/TransformStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b5764a33bfeb3588febc2682852579b0";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTW/UMBC951eMugdatM2KgjhUtCcE9IAEtOKCUDVNJonVxA72pNvw63l2drNLiwQH" +
                "YBVpbcfz9d6byYKuGhNI7nsvIUggJvVsQ+V8R5V3HRXO+dJYVsGeO6FGuBSfp821KbMFqSNt5PHNojFt" +
                "eb27iKspWodQXAvFpQvajjQEKelmTG5w6xVT46U6O2hU+9PVam1uTe5dyJ2vV1odnGv1asXn1HNxC0d5" +
                "tLkUONRApSuGTqyyGmcJdSCGxysbS0qHeZa9SzVsSsmCemPrB+nSImUzVYKtq6Yi46XpNLuakZoxy7Kz" +
                "v/zL3l++PaWg5XUX6rCaMo/1KtuSfQk0lUtWTrU2pm7EH7dyJy2MuOsBbHqrYy8h31KApxYrntst+iCx" +
                "cF03WFNEBtWApX17WBoLefTs1RRDy/4R4dE7niDfBrGF0MXrU9yxQYpBDRIa4aHwwiGiffGassFYfX4S" +
                "DYD2l08uPPuaLa7W7hjnUoOgOQtgz0p7Si2JwymCPZ2qzBEEKAnClYEO09k1tuGIEA25SO+Khg5RwodR" +
                "GygjknnH3vBNm5RYAAp4fRKNnhztebbJtWXrtu4nj7sYf+LWzn5jTccNyGsjDGGogSQu9t7dmXLXBkVr" +
                "oGJqzY1nP2bRagqZLd4kTWrkMVGDfw7BFQZMlLQ22mwlPffeP5JlLQ7y8+OkzbkftirzEslCGSGVtJss" +
                "N6JrEaC1do9UFKLOKo92DuhviCr7LIU6/3yyb1MPZx8HGHgbe9y7qdn/T5GbZH5RItNdevcg/9gSF0m7" +
                "zqIFOmHQim6bLWFYGg/TOJvgVTD6MLKWGGeYZsDDOoWPjm/hUiCkaM19D2e8j0k8hsmh5HW+pHUDfNOt" +
                "KITUv6njTUHe1BhoMxuzMdOmuCVpdQIhte2U8xQMFMLJFu2jnC4qGt1A61gQFn4zaBzonfNKfaDOLeOU" +
                "2bj4GdAPDt2++ybYoBhxYL1qHevLF3Q/r8Z59f2/UL3T2K/YtuS8mT80P3Eed992Ao0g/7ag7WqdZT8A" +
                "PklETZUHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TransformStamped> CreateSerializer() => new Serializer();
        public Deserializer<TransformStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TransformStamped>
        {
            public override void RosSerialize(TransformStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TransformStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TransformStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TransformStamped msg) => msg.Ros2MessageLength;
            public override void RosValidate(TransformStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<TransformStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TransformStamped msg) => msg = new TransformStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TransformStamped msg) => msg = new TransformStamped(ref b);
        }
    }
}
