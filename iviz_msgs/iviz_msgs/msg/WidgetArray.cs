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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/WidgetArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9c6e629280a808caf83dc9d9a478ba57";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71Y227bOBB911cQyEPbReptnEu7BfZBthVHrWO5stwiWCwMWqJtorLoUlQS5+v3kLpY" +
                "slV0F9gkMGxqLoeHw5khFX7Pn+abdJX+PuA0Fqu//iaRGaQWr1TfeLRiCqoHM0gty/rzf/6zbqfDj4Qf" +
                "sLEynqgPxO4Hrjee24MB+ZO8awp959b76kB+1ia3RyOoulahC+4mznwyst0x0UiEnBBarBdrU2uiuIrZ" +
                "KVHsUZ0SmkRQS/FAxJIsMqVEkpI60vTG8wONdJYjpRsax0weIxooDVr3HnuB29fMu8c8aEJ4KJKCwqHn" +
                "rTOeET3veYsn2bAk04w3Waz4FpOLreKgXkfozYLAGwPhIkdQPNmRMObhd7qAR1SPvnFw+zDXU17+Imia" +
                "dnvoyk3I557Ovc+k8bff2tLizpmOvYbF2RFG3x73ndHeontgce3532x/UMM4b7fo2f3PxhJBObCoVBXG" +
                "ZbkYHRfs5Ng5WIKR931vOm0SN/Jq5Xu6Rl5x3ZM08hZqRj5w7ZE3LAjV5LNJhX/VsPe+jQv5+7rcHV97" +
                "hfxDXY45S/s/6nLH9z2/WNe7Y0JTo2is+MvMmeqy1IqiGs+uiD1yh2MkczAfONf2bBQcpkLDZuRcVwa1" +
                "mDZs+vhy/JpN99jGd4c3QR3n4tjm0wx0r11nUNp8OLa5Hs2mNw0+V8dGQ8e7dQL/riCGjW1hFHgTQupI" +
                "3csWqFt30LS6PGvB6nnI2ds6q3fdC8tKVZQ31htGI7SotfmxFjuF5hTq/gATyZMV4ZEVZZJqEYn5kim+" +
                "Ybrm+ZIkQpEnJgVqfc32LSCOyYIRyTbinqHslwr4as1Tol2tZSyourogaUhjlk+odttiVLYG86Bbx55o" +
                "X8RC+sOeTRY0/L6SIkuieaiFJVPTd8qHkJouVwakeJzTmK8SdERV2OEQ0/1xDonkrJh4wZMIurnhtWJi" +
                "w5Tc5Sy+slAJeU7Uci6Wy5Spn+jzYMwjnm5jGjIz40+RGlbPdZYe7Lh1QqYKbZnKCBFQNKKKkqVAJvDV" +
                "msm3MbtnMZzoZotNNFodjrQDx0BvJj4rljCJI25HshRGSpBQbDZZwkOqdxW73fCHJ8cRRrZUKh5mMZWw" +
                "FxKh1uZLSTdMo+OTsh8ZS0JG3MFH2CQpCzPFQWgHhFAymuotRv6b7T3vagfrJHgQb/HIVjrfysmReVRp" +
                "suxxK1mqedL0I+b4LV9cB9gIDsMsUUpeG9kcj+kbgklAgW1FuCavwXyyU2sUgc71eyq5ORkBHOpDPiKv" +
                "tNOrNzXkxEAnNBElfI64n+PfwCYVrl7T2zX2LNarT7MVAgjDrRT3PILpYmdAcGwjj1CrC0nlzjL1aqa0" +
                "Tq51jPNaNDuCX5qmIuTYgMgc3mX5mN2Yo/afOxurss4bA/ZSVqNVNVpUI/pcjFqrs0x2yXTyIKwIGLk3" +
                "Op3LS8kQ2y1Kt6PT1jWJJhKk6YZR7AEqovI0lyTJTG/tAJVJhnLDPYkrEgmW6m4KjA39DkiGXdfedLsF" +
                "GEpP0iSN8yYMMVxes86qc0oe1izJrfSumRozVclDIvmKR7knJtpUzpQUi0PbXnbzfm0455MhhQAihTIO" +
                "bzrEXZKdyMiDXhAGsmgGQjf5kpdJWiVwEmSauIFoBnQiUJoIS5rSFfI7SRXaUMeqjoPHarSrRk/P/lqR" +
                "v8k8y2uF7wW2Nhm4034NL39RmPjueFhozto154PaddDoAt/+5PQDz78rPM8bWs8bBe6kdifMxbY/dPDl" +
                "2LVbYf7e403dGsGrn+kMkfd1bc+bjQc2LjE3Tv+zuQz9t/vE/tA/zBEkzxZfbad+edQfKfLmilZX3Aaa" +
                "F4zqqbLK5QfXhJ6+TkDQE49koce6ddaE0/wgw3VhD1TY6WvDi/QkHR7Ulb3vKXlN45VKB63ZkE71WazF" +
                "UaHnxla/iAnJS1+0rbwwSwPrS4azQCYGd2/3UgsElbLlIsqKok+YM63ij7XgAmEoN5b7iz7yIvT3oWs7" +
                "NhrxbJLXTz/2cdfd+pedsRw9PP//Xmpl0FawIRaFUm+/3ab8ib0ow6JQrWYfai1v6x+WCCTz7RIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
