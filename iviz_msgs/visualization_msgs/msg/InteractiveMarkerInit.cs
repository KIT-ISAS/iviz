/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class InteractiveMarkerInit : IDeserializable<InteractiveMarkerInit>, IMessage
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        [DataMember (Name = "server_id")] public string ServerId;
        // Sequence number.
        // The client will use this to detect if it has missed a subsequent
        // update.  Every update message will have the same sequence number as
        // an init message.  Clients will likely want to unsubscribe from the
        // init topic after a successful initialization to avoid receiving
        // duplicate data.
        [DataMember (Name = "seq_num")] public ulong SeqNum;
        // All markers.
        [DataMember (Name = "markers")] public InteractiveMarker[] Markers;
    
        /// Constructor for empty message.
        public InteractiveMarkerInit()
        {
            ServerId = string.Empty;
            Markers = System.Array.Empty<InteractiveMarker>();
        }
        
        /// Explicit constructor.
        public InteractiveMarkerInit(string ServerId, ulong SeqNum, InteractiveMarker[] Markers)
        {
            this.ServerId = ServerId;
            this.SeqNum = SeqNum;
            this.Markers = Markers;
        }
        
        /// Constructor with buffer.
        internal InteractiveMarkerInit(ref Buffer b)
        {
            ServerId = b.DeserializeString();
            SeqNum = b.Deserialize<ulong>();
            Markers = b.DeserializeArray<InteractiveMarker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new InteractiveMarker(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new InteractiveMarkerInit(ref b);
        
        InteractiveMarkerInit IDeserializable<InteractiveMarkerInit>.RosDeserialize(ref Buffer b) => new InteractiveMarkerInit(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.SerializeArray(Markers);
        }
        
        public void RosValidate()
        {
            if (ServerId is null) throw new System.NullReferenceException(nameof(ServerId));
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 16 + BuiltIns.GetStringSize(ServerId) + BuiltIns.GetArraySize(Markers);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerInit";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d5f2c5045a72456d228676ab91048734";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVabXPbNhL+XP0KTDKd2Fdbduw01+rOHxxLTjR1bJ+t9GU6HQ1EQhIaimBB0rLy6/vs" +
                "4oWk5TSdm0suycQkCCwW+/rswk/FOFV5pecbnS9EWVn86Iu3dVmJmRJ1rv+oldC5qJZKVKbQicjlSpWF" +
                "TFTvKUZlhf90KUpl75QVa2Pfl8Lk/Z4j5cenOu1h+q0CtTxRIq9XM2X7GJqAbpJpsCDWOstEXSpHsDIi" +
                "VZVKKqHnQldiKUux0mWpUiFFWc9KplWBRF2kslJ9IUbYaeNfBZgs5UI5qkt5R2SVKME8eOqwIWQJKjLH" +
                "MbGPXwdyZ8xW6Shk+r3KNmItwShYq3NiIbEaQppbsyLiIMIUnJjkvCLSYDVJQHJeZ/xVy0x/kJU2EKkR" +
                "8s7oVFiVKH0HaYFCWheZTugAOIXs92qdVy9fEMtTcEtCPAU3K2nfK1v2e+Mcu8ik0nfqLY/9+lv42Oud" +
                "/I//9N7evh6IO13W8RDTVbkoD7a4IMXqlTqYW5K3zueGdD2ei6WSKRRf4aNgq2FpHu6xchzjTt6Qq1WV" +
                "lXk5N3YFpUMOhiQMhTiqkKCSyVIQrbJSRV/8Ymq32AnUabwwsKi5UulMJu9ZRY0hMCHi7ApDdq1LtSc2" +
                "ILLSi2UVqYTFtFSKVM/nypK9xtXnxgp7pz/QKXCowD9tk9SW5z6b63scgpc842PPNjwB9g4/eMNi8dIh" +
                "JY+dqTD3fai8NHtwh7nOVelOpe9Mha+QioCAhDUV6wMmsVBmBcltnGqu6fhEhan+hasvMjOTGUw8+jz7" +
                "d/D6xtODY7H6yBuW1tSLZfR4ig/s7EtjKzBNXlKwwe/8W7w4FMlSkq3AQnfjmtYsXprIjDbmEEQeT0fE" +
                "8WWdVSIxeWVNVoodP3LyHITmmZHV8ZEoaWl0E5XXcOyU4gU/g1+rIUJZlibR8LIU2qqW/mBsfv3eW8wc" +
                "YeKGfAnPU7+KqF5oSMvMGyZSXRaZ3HgeO3S2vOLMLQLZsPxz+WhZpU79zrBIpBXkIG2KE1WSQgvzu4Sh" +
                "K7ufqTuVYZFcFTgIf602hSpdgMaR8G+hcpyGDYQ0AuUkZrWCsXCwck7YWu88TYpC2kondSYt5hub6pym" +
                "R9fBvxiNx8MBCaZUSU0yw046T6ySJVnIeCg4FpKK1R+9p5O12cerWsBt4ubOSsGsui+scrmiHGCPf7jD" +
                "9UEbwlHYJYX98NgUr+WuwCZgQRUGEWUHnF9vqiXFaDjAnbRazmCQIAzrykD1GS16ttuiTGwPYPu5CeQd" +
                "xWaPv0M2j3TpTPtL6CxjZ60XECAmFtbc6RRTffjwqTPTMyvtpsdxlbfsPT3nKMkWyRrRW2YfvI+1wSn6" +
                "81jjdkQi/0R8JSWBfZcO4VUcqyGluVU4BmGMPbIyGk79d81zyacNfNKv7YveNYfCMKH3nxqntDnTbeZ9" +
                "qQOCleA55OlS5z5qB/5xFrgGs9w5rgtjyPf38WkTnz58GfYb0YUzREXBgjry7DJPb380cqe83e994kTh" +
                "af0F0UqM7zhffI64j6L8iHDFVviOmY+gKAQhrZUbUuUWEQ6cp9A9vDoJCg+zKP9UZqEIcvjEhzQnsYKS" +
                "zYFPVfQzPKoK+RdiJnxBqZZsaYPtydWNZWwEc5pnkqyOuQJ6JWBNiyjR10lVY3ZblS6ELBh2OhwVEyTJ" +
                "BcdDxA34GN4mxROEcnxGnHgi5lplKW1iCp88YmbFPg4LG8IqnF3jQnEiDrGKJNFZgLwASOWCksNnmnKW" +
                "oxzR2hrSjicPSzyc825GhwkCuOZdQSbstCKkI4tCIZTOFMyTOdVITEudpZjsdH+P6Jkpyhr7glk+xqPo" +
                "HoJHKl0hep+IJ/M6f9JMP/rUdGtmpmoteLG14Ki7oLBHrenffmo6dI0nrMDwa1gvZOyUy+ZAhQxnBToh" +
                "aIJ598BsMSW8YMvw6MgxfBxyEglmQQDigeWQcrulY6NSEPCwZ89hMthyloocSiZ9wDJCgnfFInaD13DY" +
                "5LO6NwagYbs9XxsyV95bwMLYTfQe5aa6OVXHXrGekHgjSipFIhdxuBcIHXhSLOmQPN0L+btd1CsOkh4b" +
                "0SF1njJAYm/zg1PCVmJnpjKz3g1k/DcidNaexh7usge/Qwh4LwgmUfDQ7DZLlTei8RbPJwY8SN6rlIuU" +
                "0Wj46vTsB8I/5M/5doA7D4VOUzvDeyME3pCIfMXGu/HgM6BD3uDm6vbm3eUA2IsAnCI7Ly08w1GRUTqI" +
                "OWA3+q2TEwcUsSNn5k7temoXp+8uz950CWayzpPlf0WT1PpdFMPJoR9wbJ88b17dvidHfqSttf9nTe2r" +
                "B5JNKx8Hn4rQ11VNTVkSU40TGUkpEvalyuOlYVPQ+C1ILVRi5zATbl6UpV5Q5vGuficz/B+MAz+3CujQ" +
                "KRGv3405Q8B0wTUxA4OO9bAolCX0QI2kDgdiR/VRskoxq6sK39i8dx9UniA8bFXKmQG43qo9xI5VyJfc" +
                "JDBNn6ATcLiEeSirXbKy9VIj7IGvmaJ9ufimWgNGxy0LUHZON3QV6sCLt9o8qM4/BlOpJdGCWSuTqkFT" +
                "cy7NuoPCUFHnHnSML9+MbsaTAXoSWfZgGg73iO4RGcY/j1B6/aBU0ZnvWhZkVb4ZgcKiojD+43j00/T8" +
                "9Gx8CdM9zcgINvsfBAwu9zEDoEbBIXfuB2RFa9SdaKwMMjWv9sSHQV1Ed/T8UpIUwUOJHQwEj2ztxrnO" +
                "D7c4nZJ8XNfEH8/LbMuEXcV5eXU5GjTwnAwLjyZHxblS1OCjZR2X/JfIKaaD/D11CJFlCDuOLt8N0BFA" +
                "PiWKe2SVwlLraJ8N0yUNyjsscFry6t1kcoUYOcoUBS7UfTl5KwnGrXFm8/bqx9H09OfxLbgM9iRkZmBr" +
                "zpzv9+W9LuPU64tTPlGcCxt1E6NeXEydnE4C4Rs2WsRQU8NsHyXr5g8oIc3Yn5rN2NZb9II+SRBtZZKM" +
                "Wrp052c1+u/hpISz4qJmG0JHITDHzRgEtaa6Txh9Cc6fHA+fNHbuDaHkxnTo9cD2DeLMN7dvxueTb84m" +
                "NxewJvfxeEgtu9LYRgrHw7ZkqdzhzgRN5bKnJVma6uW6PU8gEcFAaHvY7gJfqKJ10Cb2Q1rnYWrnNdpY" +
                "L/eHV+dMMUX8pBjl2QmlcOj+tbZwp/A7Be3480BS/+xKlce+25ap+/B98LmWWL3Pua4uKgwuTiIOR9zw" +
                "XWjXDZVoYJKhw6k0Wh5Y5TEL+o61Jh/J0c6ESDoBCjv0ezMD95QZwH85DctpW5cUt/MdarMEEBSCch78" +
                "oM1AQr5Q7jqAIMyqqDbc0Od7B/Wwyyi4BcOxlQKHqRCHGYpTeRWO6L3Y9WddNUbV4UxDT8BhD3PPHiNr" +
                "ChaxU/ygz91KXL7X7fLSdvim4+w7wIu61HWRubnge+nhiEDKQAuc48KmTAyi4cxV0qmE+HRSfJSH7asH" +
                "Dsad0E3adHg7wAOyGgLw1HFPTf6s8rcrjWSpD8bqRSBekGijAyeQipVQsVq7Pkq/oxE+IwqMNW6DqJoF" +
                "Rs1jrmnZFeC5KgCHCei7tdOHefhvtLFJQgxkOlglNaokVTNkefIWIJQ5d9UfOlYtdHDquqtos7f3mUlu" +
                "ezk3eRjRHu2cfzFwGm95blFRLquqGBwcrNfrPhB639jFwVq/1wd0JXIwdI45oXayX8Yh6y8XTWpUPUAc" +
                "YUX59fHp10eHr9AKTvDzdilBjbP0iqp4ul+yKw9ycrgy12Xtuwq2GyIdItnpzc3VT7EGOHv3ahQrgNtr" +
                "mMkowv+zXy7Gl8PRzcmxH8DraHo7uRlfn7xoD12Mbycn37YoupGXHbJuLETf66vx5eT2JATeyejnybTl" +
                "Myffxyx6+2Z6M7q9endzBkYD2+Dh9PL1hSf6/Hk83HAYj/b2ajg+/yW+DkcXo0lzOPd6enGB03Uvoaj2" +
                "f+zP0/CdoVX3js/rIMLx8mNEHKHLcJFMTg4joQdSmpn9zte+eb/fd5cNcF541e8o/VjHrE1XrtP9AKGY" +
                "UINQCeBRn/KEeqGpIL766i+48bui64At3W3t9qZENd5/OwOUuS5wvVFx6wmGnapM8UvDgSDkYD0fXMV/" +
                "lBN0XH2V32HeO/7HVx0KmaYHCAwo4bhJy6v3xHO6JUOQ5x7E7p44cvxRT6iZdNwMImy60fJj94iPbH7d" +
                "yhKe7e7iHzFk7LG7nHuw2N31dVaL53v4y1i8jAl5p6YoBEhFH+hqvUSv2SpqoPi7rjOTGXvz+hU3XqGa" +
                "7j78Vfx62D/cf94//K2X1tZFjEzPoTCYzaOCfYMcwsC7xZ7vXGWSO13cSHSFoETcIh+g65yNFyq1SQ/9" +
                "WWgqul0u97hbF2R5gP7H9/Z9rJDL/bb8GwdYuu+WIrP3cUW8fVneuiqnTTfxohxZ7YpqHedaTvCu11So" +
                "RKNrguiMjFlCgwyLvDutUH0jk7mYtdcKg3sCjrq7ZS/gAYiAk3P5eXZ86huRfB8L7foWr4Kn+pYi4Xpc" +
                "LwJHeUDTrAiMXV5RiSOzYikDDt0ApBCrj5gW393SVgQN3FKuG+MttasQPXwJLUKMfWx+J7Q/XAh9LacA" +
                "r6a2uBtiq+EhrJ4qHCTFTSBwC7yBkuWXuR7yvvzY3ZC442/dWyFGOuPqQYXN3SG/kn+pAiHK3eQBxln2" +
                "KYKsDKJIJ9SClSi0MZ+AOppPRQFislMKsc27HtGeKzB4FnsmcRFAFtVEvlPTAAeiKfzhUMvMjxyIZJ7d" +
                "Zq6HHWqt3T75J2FXRq0b6MjfnDNiDXzxDW9l8OsbvsJgPrY8pfmtihwuKtEF+MTF2edR9bbBx1+ssPFp" +
                "EZ9m8Un2en8CJ55vvkomAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
