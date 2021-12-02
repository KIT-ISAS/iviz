/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class UInt64MultiArray : IDeserializable<UInt64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public ulong[] Data; // array of data
    
        /// Constructor for empty message.
        public UInt64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<ulong>();
        }
        
        /// Explicit constructor.
        public UInt64MultiArray(MultiArrayLayout Layout, ulong[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal UInt64MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<ulong>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new UInt64MultiArray(ref b);
        
        UInt64MultiArray IDeserializable<UInt64MultiArray>.RosDeserialize(ref Buffer b) => new UInt64MultiArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 8 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/UInt64MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6088f127afb1d6c72927aa1247e945af";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9plmT5KGUt5CEw2EsLgw5GCaGo1nWsxJaCJDfrfv2O/J12j2PG" +
                "YPl+nnOkqyF9z1g4psyYIwlPPmV6KDKvNtaKt3vxZgpPOTsn9kySE6WVV0ZTYmw0JGniImftRWnDK7KM" +
                "8pAuQrqbRdGHYpTV3+oZkjtxrBIV10USksKLOioqlPY319tdE06Vt32GVHZq0qIoWv/jJ3p4/HZHzsvn" +
                "3O3d5/d8oMIPaNaRhkpxJiw7ErRnzVbFlXcqFbRyICmyDrVAgZOwXsUFsip2/u3EM6KvTTxKWSZjJVuW" +
                "lFiTEzqzpdy4AMAbUlrX/xeatyWgINpDrk0rV+OikzUnBgJ2pdyrZYni2SSJ494+nYSUSu+JMw57DlA+" +
                "YNG+Ex/l4xiHxVhHLjVFJmlz/3Pz9EgvTGervGcNqATsubsE4bxVklFBaNkcCZAteU4Dr15somzgOSS8" +
                "nfAjNTlMjle0LsFs+xw+heTnqsV2sRurS8tyNz7ActxFw0ABWABCWDmh1TROBaTN6OZ6/uv6y5xUHibh" +
                "rHwKIsCG8XkFzthkxlId7FDlXLIH7Y6LcHdlA3TeznezTLygLuAOUlb71A86l1O/GZqvCR171hItrKsx" +
                "0IwDmjXdLhc38znRSBvPdWQtJilHhwLKleWgdon9qi646CM4K+nTQedpAaBRz3oBAN/F7bJxL/vlah0G" +
                "na8tuOrZ2nKlLB930nLCOEk43uFaCpJbc57QAQvoXeR6Up6WY/ivOs7+4/y3sxUFIhiMmj9GpVpB8b16" +
                "xYlvT24zX7Ua4fKrt+ZdII3ClOAaoAIXrrtqEyvJQmK1+kuPP3n84cDUBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
