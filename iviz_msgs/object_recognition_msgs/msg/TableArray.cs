/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/TableArray")]
    public sealed class TableArray : IDeserializable<TableArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Just an array of tables
        [DataMember (Name = "tables")] public ObjectRecognitionMsgs.Table[] Tables { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TableArray()
        {
            Tables = System.Array.Empty<ObjectRecognitionMsgs.Table>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TableArray(in StdMsgs.Header Header, ObjectRecognitionMsgs.Table[] Tables)
        {
            this.Header = Header;
            this.Tables = Tables;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TableArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Tables = b.DeserializeArray<ObjectRecognitionMsgs.Table>();
            for (int i = 0; i < Tables.Length; i++)
            {
                Tables[i] = new ObjectRecognitionMsgs.Table(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TableArray(ref b);
        }
        
        TableArray IDeserializable<TableArray>.RosDeserialize(ref Buffer b)
        {
            return new TableArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Tables, 0);
        }
        
        public void Dispose()
        {
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in Tables)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/TableArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d1c853e5acd0ed273eb6682dc01ab428";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTY/bNhC981cMsIdNiqwDtEUPG+RQIEg2AdqmWN+KwhhLY4mJRCocyl7l1/cNZcu7" +
                "WSDtoYkhQB+ceTPvzSN9I1xLorbcnLugd6Nm4kCcEk8Ud5R524m6uP0gVd4kqWITfPYxbHpt9Pnalv/6" +
                "+xTmXv7PP/fb7Ztr0lzP5W7mRi/oNnOoOdXUS+aaM9MugodvWklXneylQxL3g9RUVvM0iK6QuG69Eq5G" +
                "giTuuolGRVCOVMW+H4OvOAtl38uDfGR6qEIDp+yrseOE+JhqHyx8l7gXQ8el8mmUUAm9fXWNmKBSjdmj" +
                "oQkIVRJWHxoskht9yD/9aAnuYn2IV3iVBtNYilNuOVuzcjckUeuT9Ro1fpjJrYANcTCTUCs9Kd82eNWn" +
                "hCJoQYZYtfQEnb+fchsDAIX2nLxNy4ArKADUS0u6fHoPORTowCGe4GfEc43/AhsWXON01WJmnbHXsYGA" +
                "CBxS3PsaodupgFSdl5Cp89vEaXKWNZd0F69NYwQhq0wEd1aNlccAajr43DrNydDLNDa+/lZu/OpeANe3" +
                "AV7sdZ4eLNNxANuyQ6hlpa1IoFoyQGyiFtPAIIG6CPMBzrmbL/flGuIMmEWJVJriWPTKiYNatblY5o8y" +
                "r8VZzrNFddIsPZBsT1umtfOs6FbeY/INBqqxlwP2kJjdl7jCQXAu1OUbUD4T32ECAaW5O5UrUa4RYOQ0" +
                "zaK8t6at8yOLVAwSIjUjo/kscmx9qVVHEPz9jzVcnwUVd2PCYrKw0hJw4Mi93FE7dt2L2RK4PpSjS2kH" +
                "sXE7yOUetLYqaQ+ZfV4dZTwyva/DY6EM0AeFN49CLgWNx61kyx8itqzaydOb774IfFDhkSrIxLE5h29m" +
                "3G/j1sfjAIFfKYkdKdhsxXEzGy1T3yXMRAeuYA+cifa5Pq4XsxcbQMRT7opcobMEuD9HCJlCwT3HfS+C" +
                "aOV0zkPezBjj7M5T/+DC8+we0nW7LnL+5We6W56m5enz92n/LN2JwzIotf/le3o+bN7ePp11N0+u3L8w" +
                "Oj0dnPsHwBOsbAkIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
