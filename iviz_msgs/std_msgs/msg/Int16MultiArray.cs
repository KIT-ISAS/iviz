/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Int16MultiArray : IDeserializable<Int16MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public short[] Data; // array of data
    
        /// Constructor for empty message.
        public Int16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<short>();
        }
        
        /// Explicit constructor.
        public Int16MultiArray(MultiArrayLayout Layout, short[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Int16MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<short>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Int16MultiArray(ref b);
        
        Int16MultiArray IDeserializable<Int16MultiArray>.RosDeserialize(ref Buffer b) => new Int16MultiArray(ref b);
    
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 2 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Int16MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d9338d7f523fcb692fae9d0a0e9f067c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9tlmT5KGEt5CEw2EsLgw5GCaGo1nWsxLaCJDfrfv2O5M+0exwT" +
                "Btv385wjXQ3pe8bCMmVaH0k4cinTQ5k5tTFGvN2LN106ytlasWeSnKhCOaULSrSJhiR1XOZcOBFseESW" +
                "Ue7ThU+30yj6UIyy+l2tIdkTxypRcV0kISmcqKMiVbj5artrorGCt11DCp2atCiK1v94RQ+P3+7IOvmc" +
                "2739/J4PVPgBzTrSUCnOhGFLgvZcsFFx5Z1IBa0sSIqsQy1Q4CSMU3GJrIqdezvxlOhrE49ShkkbyYYl" +
                "JUbnhM5sKNfWA3CaVFHU/xeatyUgIdpDrk0rV+Oik9EnBgK2UQm9l4uA4lknieXePp2ElKrYE2fs9xyg" +
                "nMdSuE58lI9jHBZtLNlUl5mkzf3PzdMjvTCdjXKOC0AlYM/tJQjrjJKMCqKQzZEA2cBz4nn1YhNlPM8h" +
                "4emEv1Ljw/h4TesAZtvn8MknP1cttvPdSF1aFrvRAZbjLhp6CsACEMLIMS0ncSogbUarm9mvmy8zUrmf" +
                "hLNyKYgAG8bnFThjnWlDdbBFlXNgD9odF2HvQgN03s5200y8oC7gDlJW+9QNOpdVvxmarwkde9aAFtbl" +
                "CGhGHs2abhfz1WxGdFVox3VkLSYpS4cSyoVyUDtgv64LzvsIzkq6dNB5WgBo1LNeAMB7frto3It+uVqH" +
                "QedrCy57trZckOXjThpOGCcJx9tfS15yo89jOuADepd5MQ6n5ej/q47T/zj/7WxFnggGo+aPUam+oPhe" +
                "veLEtye3ma9aDX/51VvzLpCu/JTgGqASF669bhMryXxi9fWXHn8A3/BMx9QFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
