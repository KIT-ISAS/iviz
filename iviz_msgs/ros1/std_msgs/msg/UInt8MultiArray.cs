/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt8MultiArray : IHasSerializer<UInt8MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public byte[] Data;
    
        public UInt8MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = EmptyArray<byte>.Value;
        }
        
        public UInt8MultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public UInt8MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                int n = b.DeserializeArrayLength();
                byte[] array;
                if (n == 0) array = EmptyArray<byte>.Value;
                else
                {
                     array = new byte[n];
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public UInt8MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                byte[] array;
                if (n == 0) array = EmptyArray<byte>.Value;
                else
                {
                     array = new byte[n];
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public UInt8MultiArray RosDeserialize(ref ReadBuffer b) => new UInt8MultiArray(ref b);
        
        public UInt8MultiArray RosDeserialize(ref ReadBuffer2 b) => new UInt8MultiArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Layout.RosSerialize(ref b);
            b.Serialize(Data.Length);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Layout.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Data.Length);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) BuiltIns.ThrowNullReference(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) BuiltIns.ThrowNullReference(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Layout.RosMessageLength;
                size += Data.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Layout.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Data.Length
            size += 1 * Data.Length;
            return size;
        }
    
        public const string MessageType = "std_msgs/UInt8MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "82373f1612381bb6ee473b5cd6f5d89c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5UUpb6ENgsJcWBh2MEUJQrXOsRLaCJDfr/vp9kh3bafc4" +
                "ZgyW73R33/fpTkP6plk4Jm3MnoQnnzM9VdqrpbXi7VG8mcpTwc6JLZPkTJXKK1NSZmwyJGnSquDSi2jD" +
                "K7SmIoSLEO4mSfIhGenmWz9DcgdOVabSJklGUnjR7EoqVfrb1Zq6J3qpC4+VTmFJkjz84yd5ev56T87L" +
                "TeG27vN7PlDhOzTrSEOlVAvLjgRtuWSr0tp7JRW0ciApdIdaIMFBWK/SClE1O/924AnRl9N+pLJMxkq2" +
                "LCmzpiBUZkuFcQGAN6TKsvk/07xNAQlRHnItW7lOLjpYc2AgYBflXswjio3JMse9czoIKVW5JdYcztyF" +
                "dgGW0nfiI32aolmMdeRyU2lJy8cfy5/P9MJ0tMp7LgGVgL1w5yCct0oyMohSnloCZCPPq8CrtzdTNvAc" +
                "Et5O+As13o33l/QQwaz6HD6F4E1dYjVbj9S5Zb4e7WDZr5NhoAAsACGsHNPiKs0FpNV0cz39dX07JVWE" +
                "STgqn4MIsGF8XoEzNdpYajY7ZDlG9qDdcRHuPhZA5dV0PdHiBXkBd5Cz2uZ+0Lmc+s0UXKjYs0a0sC5G" +
                "QDMKaB7obj67mU6JLkrjudnZiEnK0a6CcjEd1I7YL5uEsz6Co5I+H3SeFgAK9axnAPCd3c1P7nk/XaPD" +
                "oPO1CRc9W5suyvLxJC1njE5Ce4drKUhuzXFMOyygd1WU49gt+/BfV5z8x/lvZysJRDAYDX+MSr2C4lv1" +
                "io5vO/c0X40a4fJrjubdRroIU4JrgCpcuO6yDawlC4H16i81/gAxI/UV1AUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<UInt8MultiArray> CreateSerializer() => new Serializer();
        public Deserializer<UInt8MultiArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<UInt8MultiArray>
        {
            public override void RosSerialize(UInt8MultiArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(UInt8MultiArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(UInt8MultiArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(UInt8MultiArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(UInt8MultiArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<UInt8MultiArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out UInt8MultiArray msg) => msg = new UInt8MultiArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out UInt8MultiArray msg) => msg = new UInt8MultiArray(ref b);
        }
    }
}
