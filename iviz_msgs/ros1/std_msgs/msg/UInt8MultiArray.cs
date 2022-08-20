/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt8MultiArray : IDeserializable<UInt8MultiArray>, IMessage
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
            Data = System.Array.Empty<byte>();
        }
        
        public UInt8MultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public UInt8MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        public UInt8MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.Align4();
            b.DeserializeStructArray(out Data);
        }
        
        public UInt8MultiArray RosDeserialize(ref ReadBuffer b) => new UInt8MultiArray(ref b);
        
        public UInt8MultiArray RosDeserialize(ref ReadBuffer2 b) => new UInt8MultiArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) BuiltIns.ThrowNullReference();
            Layout.RosValidate();
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + Data.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Layout.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Data length
            c += 1 * Data.Length;
            return c;
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
    }
}
