/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestHeaderArray")]
    public sealed class TestHeaderArray : IDeserializable<TestHeaderArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header[] Header { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeaderArray()
        {
            Header = System.Array.Empty<StdMsgs.Header>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestHeaderArray(StdMsgs.Header[] Header)
        {
            this.Header = Header;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestHeaderArray(ref Buffer b)
        {
            Header = b.DeserializeArray<StdMsgs.Header>();
            for (int i = 0; i < Header.Length; i++)
            {
                Header[i] = new StdMsgs.Header(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestHeaderArray(ref b);
        }
        
        TestHeaderArray IDeserializable<TestHeaderArray>.RosDeserialize(ref Buffer b)
        {
            return new TestHeaderArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Header, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            for (int i = 0; i < Header.Length; i++)
            {
                if (Header[i] is null) throw new System.NullReferenceException($"{nameof(Header)}[{i}]");
                Header[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Header)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeaderArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2RTWvcQAyG7wb/B0EO+YBNoL0t9BbycSgEklspi3ZG6xGMZ1xJ3tT/vhpvS5JbDjU2" +
                "9nje93k1klrcjTrozQNhJPnxE9L60Xff/vPVd9+f77egHwP77gyeDUtEiTCSYURDOFSBxEMi2WQ6UnYX" +
                "jhNFWHdtmUivm/MlsYLfAxUSzHmBWV1lFUIdx7lwQCMwHukDoFm5AMKEYhzmjOKGKpFL0x8ER1r57VH6" +
                "NVMJBI+3W1cVpTAbe1GLM4IQKpfBN108c7GvX5rDjS+vdeNrGkjeKgBLaK1i+j0JaSsWddtirk5nvHa8" +
                "N4k8KCpcrP92vtRL8ByvgqYaElx4+U+LpVqcSHBEYdxnauTgfXDseTOdX75Ht9K3ULDUf/wT8i3kM9xG" +
                "+Qtux9okH15uLdB58D66cpJ65Oja/bJSQmYqBpn3grL0XbOdQh1y15rtMvets/E3qtbAPokIr2yp79Sk" +
                "Baxz2XHsu777Ay382TK4AgAA";
                
    }
}
