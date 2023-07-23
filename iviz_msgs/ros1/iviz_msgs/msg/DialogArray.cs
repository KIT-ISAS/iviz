/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class DialogArray : IHasSerializer<DialogArray>, IMessage
    {
        [DataMember (Name = "dialogs")] public Dialog[] Dialogs;
    
        public DialogArray()
        {
            Dialogs = EmptyArray<Dialog>.Value;
        }
        
        public DialogArray(Dialog[] Dialogs)
        {
            this.Dialogs = Dialogs;
        }
        
        public DialogArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Dialog[] array;
                if (n == 0) array = EmptyArray<Dialog>.Value;
                else
                {
                    array = new Dialog[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Dialog(ref b);
                    }
                }
                Dialogs = array;
            }
        }
        
        public DialogArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Dialog[] array;
                if (n == 0) array = EmptyArray<Dialog>.Value;
                else
                {
                    array = new Dialog[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Dialog(ref b);
                    }
                }
                Dialogs = array;
            }
        }
        
        public DialogArray RosDeserialize(ref ReadBuffer b) => new DialogArray(ref b);
        
        public DialogArray RosDeserialize(ref ReadBuffer2 b) => new DialogArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Dialogs.Length);
            foreach (var t in Dialogs)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Dialogs.Length);
            foreach (var t in Dialogs)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Dialogs, nameof(Dialogs));
            foreach (var msg in Dialogs) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Dialogs) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Dialogs.Length
            foreach (var msg in Dialogs) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/DialogArray";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "51b4af3215c8112a60f1262803313988";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VV30/bSBB+918xEg+FE+QKtL0eUh9CEmhUiLkktKqqKtrYY2eF7fXtrgPhr79v17GT" +
                "ANXdwxFZynp25ptvfrovRabSHz8p9gcTBMGn//kXXE8uz0gu5eMsN6n5ve89BZUs7Efq9qbDcDTr9vv0" +
                "id7uCseD6/DrAPLjl+TdqytcnQTru+n3m8Hs5qo7HJFDItojsQ6K7qVdkJU240Oy/GAPSRQxrrW6J5XQ" +
                "vLJWFYa2kSafw/HUIR3XSCYXWcb6OaKHcqDb1qNwOuw55ifPeYiCZKSKNYWnlteD0S05v6cvWFLOReUY" +
                "51VmZQnnqrQS1LcRzm+n03AEhHc1gpXFiqJMRndiDot4O/veYNiDunP5/l+S5mi/nLqmCLXvySz8Qju/" +
                "TWkbje+DySjc0Th+htHrjnqDq43GyRONi3D8rTvub2Gcvqxx3u198ZpIyhON9qrFeN8E4/KCSo4GT0Lw" +
                "8t44nEx2iXt5G/mGrpe3XDckvfwFal7eH3avwss1oS357U2L/2FHP/w2Wsv/2JYPRxfhWv5xWw6fjf6f" +
                "2/LBeByO13G9fU5o4i92Iv7rdjBxY+kuMI3GxvWYf2YRY2AW/m9tICLXrtDRskhJxkFcaeFElMmErczZ" +
                "taBMqFCWHlkrtN6CNx2ZZTRn0pyrJaMLEwsHdiENOdMgyZSwH96RiUTGa492VTbHplfrN9fMG7I9lSk9" +
                "vjzv0lxEd6lWVRHPIidsyPpJaF4iUW4Fgu3pBnPGBV65cTCXRYzLmWeQssrZ6lXt7StHVulTsslMJYlh" +
                "+4v7Ou5ZLE2ZiYjh5FeaQNrReq0t/qS6wR5NLBaC0DFSYEUsrKBEoeoyXbA+ynjJGYxEXqJe/talw3Rg" +
                "OHV1w5NywRrLdUWVgZJVFKk8rwoZCcu+sDv2sJRYnlQKbWVUZUJDX2mk2qknWuTs0PEY/rviImIa9s+g" +
                "UxiOKitBaAWESLMwrpTDfr33T0+cAbrvx1iZ45/B3vReHUHOqeuxhgW6TVjHmh9KzcYRFuYMzn6ro+zA" +
                "CbLEcBcb2veyGV7NAcEbuHCpogXtI4SblV2g8V1/L4WWfjkDOHLfmZjeOKM3B1vIhYcuRKEa+Bpx4+O/" +
                "wBYtrovpaIHiZS4NpkqRSSiWWi1lDNX5yoPgy4GGwnzOtdCrwM+odxnsXbhk1/PnS4N/YYyKJCoR++9H" +
                "My++LDPM+2u3ZTvH9TJAUXV7StvTvD2J12L04pg2Xa/ZNQ/SioTR0t+5pk40I7clZrjj+nfoG00V6Nec" +
                "BWqA0Wgt/Xdas9+nHaCyZswdPtXSUqzYuA0KjFzcAZJRdWctyhJgmEEtCpPVixdimOxzJ+0c0v2Ci1rL" +
                "Vc0Pmx9PGZGWqYxrSzjKW2NB6+CwqpOTekd7zrUztBBAtLLe4KBDw4RWqqJ7FxAOer0VlFvsDS/ftFZh" +
                "+1eOuIfYTeiNwmgiLcaIFP1dGIt91AnaT8BDe1q1p8fgH6P7jQztCgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<DialogArray> CreateSerializer() => new Serializer();
        public Deserializer<DialogArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<DialogArray>
        {
            public override void RosSerialize(DialogArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(DialogArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(DialogArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(DialogArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(DialogArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<DialogArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out DialogArray msg) => msg = new DialogArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out DialogArray msg) => msg = new DialogArray(ref b);
        }
    }
}
