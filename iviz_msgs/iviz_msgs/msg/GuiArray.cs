/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/GuiArray")]
    public sealed class GuiArray : IDeserializable<GuiArray>, IMessage
    {
        [DataMember (Name = "dialogs")] public IvizMsgs.Dialog[] Dialogs;
        [DataMember (Name = "widgets")] public IvizMsgs.Widget[] Widgets;
    
        /// <summary> Constructor for empty message. </summary>
        public GuiArray()
        {
            Dialogs = System.Array.Empty<IvizMsgs.Dialog>();
            Widgets = System.Array.Empty<IvizMsgs.Widget>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GuiArray(IvizMsgs.Dialog[] Dialogs, IvizMsgs.Widget[] Widgets)
        {
            this.Dialogs = Dialogs;
            this.Widgets = Widgets;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GuiArray(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GuiArray(ref b);
        }
        
        GuiArray IDeserializable<GuiArray>.RosDeserialize(ref Buffer b)
        {
            return new GuiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Dialogs, 0);
            b.SerializeArray(Widgets, 0);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/GuiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9f06737271ba3bd6e548108be206ae67";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VS4/bNhC+C/B/GGAP2S28LpoUOSzQQ9pF0z0USJugPRSFMBJHEhGKVEjKjvLr+5GS" +
                "5Xq76QPormHYQ3Lmm/eM3utPZR/a8OWtZuPa334nlYlQ6PXpV61aiXg6ZCIUxab45n/+bIof376+ofv2" +
                "bIoQ1Xzxg7AST13+K6opCnEdtbNg8dq2pFWhRs/pioxuJOpeisY4ji+/plCzkVkqTsNCVWOMzob5oOsM" +
                "tWj7zhnnf3797SuquH7fejdaVdbp8qgu6gjE5VDzkE0ZtY1fvTweSza6tb3YuPAhhjiNJW68lkVxpa3C" +
                "W5ntasX1Ev00W/GL1NH5FxSb0jVNkPiZ9zlnpdJhMFxL1vhZpDOux0vlvcRtigt6G9kq9gpBiKw4MjUO" +
                "GdVtJ/7ayF4MpLgfRFF+TREJOwi+63QgfFux4tmYicYApuiodn0/Wl1zSiwSfiYPSW2JaWAfdT0a9uB3" +
                "HtFO7I3nXhI6vkE+jGJrobvbG/DYIPUYNQyagFB74ZCyfHdLOcMvnieB4uLdwV3jKC3qclVOseOYjJWP" +
                "g5eQ7ORwAx1fzM7tgI3oCLSoQJf5rsQxXBGUwAQZXN3RJSx/M8UOxRw7oT17zZVBmQZUlzFAfZaEnl39" +
                "CTmZfUOWrTvCz4gnHf8G1q64yafrDjkzyfswtgggGAfv9lqBtZoySG00Sgk9V3n2U5GkZpXFxfcpxmCC" +
                "VM4I/jkEV2skQGGexO7YQTkbJXr48Qty7e3NPB6QTr9S7UpVK8WPZ9SDXbo5lryXVEIILsJG+/yYKrrx" +
                "gggP6OFdKt67XG7Oolh7YWQCfbFKQlBpD1HMox1QxQuaTrakIykngayLwOj5PSAFuU/SPAwAQwN6tsHM" +
                "IxXXELmUXbvb0qETO3Ol3OVOy72pa/K61WqWhKJ+FWZavNtiCD1H7o2ZbZ6VoZAA4l3MAlc7umtociMd" +
                "kkMg/DISHFWy2pVLNzq3TfNggTiP6BuHBkVYQuAWVW5DxDTaFete+LhS00p9eoL1Nu/U/7jeTuvrgT3V" +
                "s7brhvrL6zwQ0J4Ly/levB8zBHPAz/3t9lRtkAxIPfDqVMZzGbkm23XeA9u0BNK1Wt515sXYIoctu8ii" +
                "U+ZaODIUP40YQt5m3BPf0/kIY9ZGR3Ii8hfyPF1dgDtYXtnqM4+Lv63ep/LgFL8H59VZVM/tT6cPp+in" +
                "MfFPLblSB7j3ByoMKIS2CgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
