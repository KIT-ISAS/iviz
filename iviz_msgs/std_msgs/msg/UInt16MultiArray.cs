/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class UInt16MultiArray : IDeserializable<UInt16MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public ushort[] Data; // array of data
    
        /// Constructor for empty message.
        public UInt16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<ushort>();
        }
        
        /// Explicit constructor.
        public UInt16MultiArray(MultiArrayLayout Layout, ushort[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal UInt16MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<ushort>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new UInt16MultiArray(ref b);
        
        UInt16MultiArray IDeserializable<UInt16MultiArray>.RosDeserialize(ref Buffer b) => new UInt16MultiArray(ref b);
    
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
        [Preserve] public const string RosMessageType = "std_msgs/UInt16MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "52f264f1c973c4b73790d384c6cb4484";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9tlmT5KGEt5CEw2EsLgw5GCaGo1nWsxLaCJDfrfv2O5M+0exwz" +
                "Bsv385wjXQ3pe8bCMmVaH0k4cinTQ5k5tTFGvN2LN106ytlasWeSnKhCOaULSrSJhiR1XOZcOBFseEWW" +
                "Ue7ThU+30yj6UIyy+ls9Q7InjlWi4rpIQlI4UUdFpSrcfLXdNeH+Cf42PXRq0qIoWv/jJ3p4/HZH1snn" +
                "3O7t5/d8oMIPaNaRhkpxJgxbErTngo2KK+9EKmhlQVJkHWqBAidhnIpLZFXc3NuJp0Rfm3iUMkzaSDYs" +
                "KTE6J3RmQ7m2HoDTpIqi/r/QvC0BBdEecm1auRoXnYw+MRCwDXIvFwHFs04Sy719OgkpVbEnztjvOUA5" +
                "j6VwnfgoH8c4LNpYsqkuM0mb+5+bp0d6YTob5RwXgErAnttLENYZJRkVRCGbIwGygefE8+rFJsp4nkPC" +
                "2wl/pcaH8fGa1gHMts/hk09+rlps57uRurQsdqMDLMddNPQUgAUghJFjWk7iVEDajFY3s183X2akcj8J" +
                "Z+VSEAE2jM8rcMY604bqYIsq58AetDsuwt6FBui8ne2mmXhBXcAdpKz2qRt0Lqt+MzRfEzr2rAEtrMsR" +
                "0Iw8mjXdLuar2YzoqtCO68haTFKWDiWUC+WgdsB+XRec9xGclXTpoPO0ANCoZ70AgO/8dtG4F/1ytQ6D" +
                "ztcWXPZsbbkgy8edNJwwThKOt7+WvORGn8d0wAJ6l3kxDqfl6P+rjtP/OP/tbEWeCAaj5o9RqVZQfK9e" +
                "ceLbk9vMV62Gv/zqrXkXSFd+SnANUIkL1163iZVkPrFa/aXHH0u+LDbUBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
