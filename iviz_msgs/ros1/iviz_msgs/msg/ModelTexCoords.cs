/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelTexCoords : IDeserializable<ModelTexCoords>, IMessage
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
                Coords = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Coords[0]), n * 12);
                }
            }
        }
        
        public ModelTexCoords(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Coords = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Coords[0]), n * 12);
                }
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
    
        public int RosMessageLength => 4 + 12 * Coords.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Coords length
            c += 12 * Coords.Length;
            return c;
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
    }
}
