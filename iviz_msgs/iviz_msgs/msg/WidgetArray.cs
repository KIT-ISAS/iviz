/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class WidgetArray : IDeserializable<WidgetArray>, IMessage
    {
        [DataMember (Name = "dialogs")] public IvizMsgs.Dialog[] Dialogs;
        [DataMember (Name = "widgets")] public IvizMsgs.Widget[] Widgets;
    
        /// Constructor for empty message.
        public WidgetArray()
        {
            Dialogs = System.Array.Empty<IvizMsgs.Dialog>();
            Widgets = System.Array.Empty<IvizMsgs.Widget>();
        }
        
        /// Explicit constructor.
        public WidgetArray(IvizMsgs.Dialog[] Dialogs, IvizMsgs.Widget[] Widgets)
        {
            this.Dialogs = Dialogs;
            this.Widgets = Widgets;
        }
        
        /// Constructor with buffer.
        public WidgetArray(ref ReadBuffer b)
        {
            Dialogs = b.DeserializeArray<IvizMsgs.Dialog>();
            for (int i = 0; i < Dialogs.Length; i++)
            {
                Dialogs[i] = new IvizMsgs.Dialog(ref b);
            }
            Widgets = b.DeserializeArray<IvizMsgs.Widget>();
            for (int i = 0; i < Widgets.Length; i++)
            {
                Widgets[i] = new IvizMsgs.Widget(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new WidgetArray(ref b);
        
        public WidgetArray RosDeserialize(ref ReadBuffer b) => new WidgetArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Dialogs);
            b.SerializeArray(Widgets);
        }
        
        public void RosValidate()
        {
            if (Dialogs is null) throw new System.NullReferenceException(nameof(Dialogs));
            for (int i = 0; i < Dialogs.Length; i++)
            {
                if (Dialogs[i] is null) throw new System.NullReferenceException($"{nameof(Dialogs)}[{i}]");
                Dialogs[i].RosValidate();
            }
            if (Widgets is null) throw new System.NullReferenceException(nameof(Widgets));
            for (int i = 0; i < Widgets.Length; i++)
            {
                if (Widgets[i] is null) throw new System.NullReferenceException($"{nameof(Widgets)}[{i}]");
                Widgets[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetArraySize(Dialogs) + BuiltIns.GetArraySize(Widgets);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/WidgetArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "46ab9d1949dfa5de6e5d9210bf58b8c2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WW2/bNhR+168gkIcmg+MtSZcNAfrgxV7mIq092+hQDINAS0cSUYlUSSqu+uv3kbrY" +
                "yhx0A5YERkyey3cOz9XiQXwNC5Oa76eC5yr98y8W+4MJRM/6Q8QpWbB2/mCCIHjzP/8F79Z3N0w88iYw" +
                "Nm7uvxGPSbPMfwXb2hLjkRVKQkQLmTIRB3GluSOxXCRkRUFBkitur18zE/GcGi1bl+1pW1mrpGkuIvJQ" +
                "rbVblSu9uvtlwrY8+pRqVck4jByxM2eFBWJ7iXjpXamEtBfX3TXkuUhlQdK2cgggblUIihbUGt4KGYMX" +
                "er9SUgVZXTdefKDIKn3FbBKqJDFkn+A3CQtjYcqcR+QtPok0kHquPD5KW3DC1pbLmOsYEbA85pazRCGd" +
                "Is1In+f0QDmUeFFSzDzXhcOMobjJhGH4pCRJ8zyvWWUgZBWLVFFUUkTcZRXZHuhDU0jGWcm1FVGVcw15" +
                "pRFqJ55oXpBDx8fQ54pkRGw+vYGMNBRVVsChGgiRJm5ciudT5tN7dekUgpPNTp3jSimKsjfObMatc5a+" +
                "lJqM85ObG9j4rnncGNgIDsFKbNipp4W4mjMGI3CBShVl7BSeL2uboZJtRuyBa8G3OTlg1HEO1FdO6dXZ" +
                "AbL00JJL1cE3iHsb/wZW9rjuTecZcpa715sqRQAhWGr1IGKIbmsPEuUCdYSG22qu68BpNSaDk19djCEE" +
                "LZ8RfHNjVCSQgBiTxGZd+/hshGjg567Gvq2bwYBc6v6U9qdtf+LP5dHR7uyKXZMrHoQVAWMPnudqOdGE" +
                "2JZo3bEr27kvNCVRpgVx5AAd0WtCMRaa/IAcA5U0od1oxIRlsSLDpLLAKPgnQBKy7rR5WQIMrae5NHkz" +
                "SUGGyimN0/GI7TKSjZTLmu8x35UiYlqkIm40YajolTlrHzfC7LlE1vO88bkxhhICiFbWK5yN2TxhtarY" +
                "zj0IB90OA8W21Pvli9YqNXKToIUYBnSp0JoIizE8RX1LYzGGxkG/Dr70p7o/fX32ldZsUT9FfmaT2818" +
                "8T6cTKfsDfthSFzN3i0+zEC/OEaf3N+DdRm0vM3H5SxcLTYTJzKdr28P8DxvvVzN39+1nIvjnKuphzzg" +
                "bVaTt7PbzWL1sdW8GnAXi/vNfAny6wF5srqb4d9sAs6Ph5zlYj0/cPD6KZ535Kfgv+39/V5/XAaojxL/" +
                "ji32bpv/g9HMT0yzduEPf0P0t16qoT/6JfAyg8M9EMU/2Td+03gq8c8eTo2RW5iOHLd84WUx4pnSotPF" +
                "bGm6pxMIfq8wsLX0uHu5l3ogXOnmIgJuOZrZL57ef7wFW967PHjuN5r9Rdzfh+7YbB/Ec+i8u33ex92N" +
                "1G+Or+60C4K/AVWzxJDVCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
