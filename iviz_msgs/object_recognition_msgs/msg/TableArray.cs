/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TableArray : IDeserializable<TableArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Just an array of tables
        [DataMember (Name = "tables")] public ObjectRecognitionMsgs.Table[] Tables;
    
        /// Constructor for empty message.
        public TableArray()
        {
            Tables = System.Array.Empty<ObjectRecognitionMsgs.Table>();
        }
        
        /// Explicit constructor.
        public TableArray(in StdMsgs.Header Header, ObjectRecognitionMsgs.Table[] Tables)
        {
            this.Header = Header;
            this.Tables = Tables;
        }
        
        /// Constructor with buffer.
        internal TableArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Tables = b.DeserializeArray<ObjectRecognitionMsgs.Table>();
            for (int i = 0; i < Tables.Length; i++)
            {
                Tables[i] = new ObjectRecognitionMsgs.Table(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TableArray(ref b);
        
        TableArray IDeserializable<TableArray>.RosDeserialize(ref Buffer b) => new TableArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Tables);
        }
        
        public void RosValidate()
        {
            if (Tables is null) throw new System.NullReferenceException(nameof(Tables));
            for (int i = 0; i < Tables.Length; i++)
            {
                if (Tables[i] is null) throw new System.NullReferenceException($"{nameof(Tables)}[{i}]");
                Tables[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Tables);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/TableArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d1c853e5acd0ed273eb6682dc01ab428";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/UMBC9+1eM1EMpokUCxKGIAxICigQUdW8IrWaT2cSQ2Knt7Db8et7Y3SxLJeAA" +
                "RJHy4Zk38948+41wLYHa/DDmiN6OMRE74hB4Ir+mxKtOovGrL1KlZZDKN84m692yj018uNDlT593Yeb5" +
                "X77Mu6vX5xRTXcq9KY0e0VViV3OoqZfENSemtQcP27QSTjvZSIck7gepKa+maZB4hsRFayPhbsRJ4K6b" +
                "aIwISp4q3/ejsxUnoWR7OchHpoUqNHBItho7Doj3obZOw9eBe1F03FGuR3GV0MXLc8S4KNWYLBqagFAF" +
                "4Whdg0Uyo3Xp8SNNMEeLrT/FpzSYxlycUstJm5WbIUjUPjmeo8b9Qu4M2BAHM3F1pHv53xKf8YRQBC3I" +
                "4KuW7qHzyym13gFQaMPB6rQUuIICQD3WpOOTH5C17XNy7PwOviDua/wJrJtxldNpi5l1yj6ODQRE4BD8" +
                "xtYIXU0ZpOqsuESdXQUOk9GsUtIcvVKNEYSsPBE8OUZfWQygpq1NrYkpKHqextLW/8qNv9wL4Hrh4MU+" +
                "lunBMh07sM07hFqOtBJxVEsCiE5UYxoYxFHnYT7AGXNnXy4gzoBZ5MhIkx+zXimwi1qtFEv8VcqaL3Lu" +
                "LRqnmKQHku5pzdR2HmTd8rcPtsFAo+9liz2EMZaxlraVg+BcqPM/oHwjvsEEHEpztyuXo0wjwEhhKqJc" +
                "atPa+S0LRdY8akZG80nktvW5Vu1B8P2HBVyfBBXXY8AiBIR9NAw4cORGbqgdu+5ZsQTuL/noirSG2Hhs" +
                "5XgDWqsoYQOZbSq7f2b6ow53hVJA6yK8eSvkXFB5XEnS/MFjy6IgRFDf/RR4UOGOKsjEsVnClwX337j1" +
                "7jhA4AUF0SMFmy07rrDBpCDMOmAmceAK9sCZqL/r2/Vs9mwDiLjLPSOT6cwB5uMIIYPLuPu4/0UQrezO" +
                "ecibGGMs7tz1Dy7YlbnlA7pm3XlOT5/Qzfw2zW/f/k/7e+l2HOZB4bw70POwef263uuunjwzv2G0e9sa" +
                "8x3AE6xsCQgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
