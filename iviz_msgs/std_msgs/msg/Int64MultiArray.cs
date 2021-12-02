/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Int64MultiArray : IDeserializable<Int64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public long[] Data; // array of data
    
        /// Constructor for empty message.
        public Int64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<long>();
        }
        
        /// Explicit constructor.
        public Int64MultiArray(MultiArrayLayout Layout, long[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Int64MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Int64MultiArray(ref b);
        
        Int64MultiArray IDeserializable<Int64MultiArray>.RosDeserialize(ref Buffer b) => new Int64MultiArray(ref b);
    
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
        [Preserve] public const string RosMessageType = "std_msgs/Int64MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "54865aa6c65be0448113a2afc6a49270";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9plmT5KGUt5CEw2EsLgw5GCaGo1nWsxJaCJDfrfv2O/J12j2PC" +
                "YPt+nnOkqyF9z1g4psyYIwlPPmV6KDKvNtaKt3vxZgpPOTsn9kySE6WVV0ZTYmw0JGniImftRWnDI7KM" +
                "8pAuQrqbRdGHYpTV72oNyZ04VomK6yIJSeFFHRUp7W+ut7smGqv0tmtIZacmLYqi9T9e0cPjtztyXj7n" +
                "bu8+v+cDFX5As440VIozYdmRoD1rtiquvFOpoJUDSZF1qAUKnIT1Ki6QVbHzbyeeEX1t4lHKMhkr2bKk" +
                "xJqc0Jkt5cYFAN6Q0rr+v9C8LQEJ0R5ybVq5GhedrDkxELCLCui9WpYonk2SOO7t00lIqfSeOOOw5wDl" +
                "AxbtO/FRPo5xWIx15FJTZJI29z83T4/0wnS2ynvWgErAnrtLEM5bJRkVhJbNkQDZkuc08OrFJsoGnkPC" +
                "0wk/UpPD5HhF6xLMts/hU0h+rlpsF7uxurQsd+MDLMddNAwUgAUghJUTWk3jVEDajG6u57+uv8xJ5WES" +
                "zsqnIAJsGJ9X4IxNZizVwQ5VziV70O64CHdXNkDn7Xw3y8QL6gLuIGW1T/2gczn1m6H5mtCxZy3Rwroa" +
                "A804oFnT7XJxM58TjbTxXEfWYpJydCigXFkOapfYr+qCiz6Cs5I+HXSeFgAa9awXAPBe3C4b97JfrtZh" +
                "0PnagquerS1XyvJxJy0njJOE4x2upSC5NecJHfABvYtcT8rTcgz/VcfZf5z/draiQASDUfPHqFRfUHyv" +
                "XnHi25PbzFetRrj86q15F0ijMCW4BqjAheuu2sRKspBYff2lxx/ubP5D1AUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
