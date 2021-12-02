/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class UInt32MultiArray : IDeserializable<UInt32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public uint[] Data; // array of data
    
        /// Constructor for empty message.
        public UInt32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<uint>();
        }
        
        /// Explicit constructor.
        public UInt32MultiArray(MultiArrayLayout Layout, uint[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal UInt32MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new UInt32MultiArray(ref b);
        
        UInt32MultiArray IDeserializable<UInt32MultiArray>.RosDeserialize(ref Buffer b) => new UInt32MultiArray(ref b);
    
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 4 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/UInt32MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4d6a180abc9be191b96a7eda6c8a233d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9NlmT5KGUt5CEw2EsLgw5GCaGo1nWsxLaCJDfrfv2ObPkj7R7H" +
                "jMHy/TznSFdD+p6xsEyZ1kcSjlzK9FBmTm2MEW/34k2XjnK2VuyZJCeqUE7pghJtoiFJHZc5F05UNrwi" +
                "yyj36cKn21kUfShGWfjWz5DsiWOVqDgUSUgKJ0JUVKrCrZbbXRNOtbd9hlR1atKiKFr/4yd6ePx2R9bJ" +
                "59zu7ef3fKDCD2jWkYZKcSYMWxK054KNimvvVCpoZUFSZB1qgQInYZyKS2TV7NzbiWdEX5t4lDJM2kg2" +
                "LCkxOid0ZkO5th6A06SKIvxfaN6WgIJoD7k2rVyNi05GnxgI2Aa5KxTPOkks9/bpJKRUxZ44Y7/nAOU8" +
                "lsJ14qN8HOOwaGPJprrMJG3uf26eHumF6WyUc1wAKgF7bi9BWGeUZFQQhWyOBMhWPKeeVy82UcbzHBLe" +
                "TvgrNTlMjiNaV2C2fQ6ffPJz3WK72I3VpWW5Gx9gOe6ioacALAAhjJzQahqnAtJmdHM9/3X9ZU4q95Nw" +
                "Vi4FEWDD+LwCZ6wzbSgEW1Q5V+xBu+Mi7F3VAJ23890sEy+oC7iDlNU+dYPOZdVvhuZrQseetUIL62oM" +
                "NGOPZk23y8XNfE50VWjHITKIScrSoYRyVTmoXWEfhYKLPoKzki4ddJ4WABr1rBcA8F3cLhv3sl8u6DDo" +
                "fG3BVc/Wlqtk+biThhPGScLx9teSl9zo84QOWEDvMi8m1Wk5+v+64+w/zn87W5EngsEI/DEq9QqK79Ur" +
                "Tnx7cpv5Cmr4yy9szbtAuvJTgmuASly4dtQm1pL5xHr1lx5/APgORpHUBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
