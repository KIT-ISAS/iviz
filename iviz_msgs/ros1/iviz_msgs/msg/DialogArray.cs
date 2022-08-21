/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class DialogArray : IDeserializable<DialogArray>, IMessage
    {
        [DataMember (Name = "dialogs")] public IvizMsgs.Dialog[] Dialogs;
    
        public DialogArray()
        {
            Dialogs = System.Array.Empty<IvizMsgs.Dialog>();
        }
        
        public DialogArray(IvizMsgs.Dialog[] Dialogs)
        {
            this.Dialogs = Dialogs;
        }
        
        public DialogArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Dialogs = n == 0
                    ? System.Array.Empty<IvizMsgs.Dialog>()
                    : new IvizMsgs.Dialog[n];
                for (int i = 0; i < n; i++)
                {
                    Dialogs[i] = new IvizMsgs.Dialog(ref b);
                }
            }
        }
        
        public DialogArray(ref ReadBuffer2 b)
        {
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Dialogs = n == 0
                    ? System.Array.Empty<IvizMsgs.Dialog>()
                    : new IvizMsgs.Dialog[n];
                for (int i = 0; i < n; i++)
                {
                    Dialogs[i] = new IvizMsgs.Dialog(ref b);
                }
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
            b.Serialize(Dialogs.Length);
            foreach (var t in Dialogs)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Dialogs is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Dialogs.Length; i++)
            {
                if (Dialogs[i] is null) BuiltIns.ThrowNullReference(nameof(Dialogs), i);
                Dialogs[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Dialogs);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Dialogs.Length
            foreach (var t in Dialogs)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/DialogArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ba4917c22f5a5cdc9531f1b84db1226d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWW2/iOBR+z684Uh+mXbVsSy/brTQPFEKbHUq6IcyoGo0ikzjBahIzttOW/vo9di4k" +
                "wGj3YYsQcY7P+c7nczPshb0HmUzk7yNGUp58/wGRWUjLsj7/zx/rYXZ3A2zLpVWwXF3DYOg77jQYjEbw" +
                "GU67Qs9+cL/aKD/bJx9MJrjVt6o9/+nRDh4nA2cKGgngAEh1KHhlagmKqZQeg6Jv6hhIHuG24K/AY1gU" +
                "SvFcQhtpdu96vkY6K5FkRtKUil1EA6VB29ZT13eGmnl/lwfJgYU8ryhsWz7Y0zlov+d7LCGjeaEZZ0Wq" +
                "2Aqd85ViSL2NcDv3fXeKCBclgmL5GsKUhc9kgRZRO/rGwBmiunZ5+S9B07T3h65OQul7FrhfoPPZpLbW" +
                "eLJnU7ejcbaDMRxMh/Zko9Hf0hi73reBN2phnO/XuB0MvxhNDMqWRrPVYFzWh9FxwUxO7a0jGPnQc2ez" +
                "LnEjb06+oWvkDdcNSSPfQ83IR85g4t5VhFry+WODf9XRd79NK/kfbbkzHbuV/LotR5+1/p9tue15rled" +
                "63SX0MxsdE7899ye6bbUG1U3nl3BYOLcTbGY/WBkjwfzib9dCh2diT1uFFox7egM8cf2Wjr9XR3Pubv3" +
                "2zgXuzp/zZHu2LFHtc71rs54Mp/dd/hc7Srd2e6D7XtPFTFM7B5GvvsI0EbqX+6BenBGXa3Lsz1Yty7W" +
                "7EOb1Wn/wrKkisrBek9JhCNqaR7WYq1wOIV6PqCKYHkCLLKiQhAtgpTFVLGM6p5nMeRcwTsVHHt9STcj" +
                "IE1hQUHQjL9QbPtYIb5aMgna1IpTTtTVBciQpLR0qNaralWPBvOiR8eG6JCnXHh3twNYkPA5EbzIoyDU" +
                "wpqpmTv1S0jMlKsDUr0GJGVJjhNRVXp4ien5GKBEMFo5XrA8wr3A8Eooz6gS65LFVxoqLs5BxQGPY0nV" +
                "L/bLYAQRk6uUhNR4/CVSR+uj7tKtjFsHMFM4lomIMAKKREQRiDlWAkuWVJyk9IWmaESyFSbR7OpwyB4a" +
                "+jqZ+E1oTgVecWsoJCopDiHPsiJnIdFZxWx37NGS4RUGKyIUC4uUCNTnAkOt1WNBMqrR8Svpz4LmIQVn" +
                "dIM6uaRhoRgSWiNCKCiROsVY/ya9531tgCX53ePy7Id14L/yE5TTRBdezQJLkCjNmr6tBJWaMJE36Oy3" +
                "8pQ9dIJRouguknBoZAG+yiNAb8iFrni4hEM8wuNaLbEbdNG/EMHMFYnAob7tI/ikjT4dtZBzA52TnNfw" +
                "JeLGx3+BzRtcfaaTJSYv1WGQRYKRRMWV4C8sQtXF2oDg/Y0FhU27EESsLdO4xqV1MNbBLpvSpAafREoe" +
                "MsxEZG7xuo9MWgIcAh9dlk1/lxMCkyqaVdKsFs2KfBSjvW1aV72gungwrBgweDF7uqhjQTG2K+zhnq5f" +
                "xxQaz7FeM0owB9gajaX5tySoGbI9RKWCYt/hHyamIOJU6rGKGBl5RkiKWdfWZLVCMOxBQXKZltMYxWhy" +
                "SHtJ7xhelzQvtXTWTLOZ9mQhCJawqLRER1ljTKA6HM7vuF8ObsO5dIYlhCCCK2Nw1AMnhjUv4FUfCBei" +
                "mgpcT/ualylaxfFKKDRxA9EN6CPH1sSwSEkSrO9cKpxHPau5F96a1bpZvVv/ALCPTiV9DAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
