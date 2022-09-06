/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelTexCoords : IDeserializable<ModelTexCoords>, IHasSerializer<ModelTexCoords>, IMessage
    {
        [DataMember (Name = "coords")] public GeometryMsgs.Point32[] Coords;
    
        public ModelTexCoords()
        {
            Coords = EmptyArray<GeometryMsgs.Point32>.Value;
        }
        
        public ModelTexCoords(GeometryMsgs.Point32[] Coords)
        {
            this.Coords = Coords;
        }
        
        public ModelTexCoords(ref ReadBuffer b)
        {
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 12);
                }
                Coords = array;
            }
        }
        
        public ModelTexCoords(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 12);
                }
                Coords = array;
            }
        }
        
        public ModelTexCoords RosDeserialize(ref ReadBuffer b) => new ModelTexCoords(ref b);
        
        public ModelTexCoords RosDeserialize(ref ReadBuffer2 b) => new ModelTexCoords(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Coords);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Coords);
        }
        
        public void RosValidate()
        {
            if (Coords is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += 12 * Coords.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Coords.Length
            size += 12 * Coords.Length;
            return size;
        }
    
        public const string MessageType = "iviz_msgs/ModelTexCoords";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8b5f64b5842ecf35ee87d85a0105c1ad";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61QTUvEQAy991c88KIgK+zeBE8exIMg6E1Epp1sG2wnZZK61l9vptvdk0fnlEnyvtKS" +
                "DGR5/hi01Ztn4WS77ds7GpEctaru/vlVTy8Pt2j/Uq0u8NqxunSywElhHWEUZWNJkD2C/3wTnLDPRNAx" +
                "NHR5YOuw26Jm07I1ZmpYHXK1ccZHX1d4S4aBIkWYYFLCoolDR5m+KBcZ5bon51ajEAvRamsDOM/J3MqU" +
                "YlhceccJxyyDWAEbZRkph5p7tnmBnpADqYaWCiSScpuOZix8EqYRvY+PiYqrBHUNTq2je1mDFT+KYJDU" +
                "0DWClkuUIzXBEy0HWjzf9zLFol3tewkeAd/naj5XP9UvIXnll/wBAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ModelTexCoords> CreateSerializer() => new Serializer();
        public Deserializer<ModelTexCoords> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ModelTexCoords>
        {
            public override void RosSerialize(ModelTexCoords msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ModelTexCoords msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ModelTexCoords msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ModelTexCoords msg) => msg.Ros2MessageLength;
            public override void RosValidate(ModelTexCoords msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ModelTexCoords>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ModelTexCoords msg) => msg = new ModelTexCoords(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ModelTexCoords msg) => msg = new ModelTexCoords(ref b);
        }
    }
}
