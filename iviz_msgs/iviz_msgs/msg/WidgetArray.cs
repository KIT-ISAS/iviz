/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
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
            b.DeserializeArray(out Dialogs);
            for (int i = 0; i < Dialogs.Length; i++)
            {
                Dialogs[i] = new IvizMsgs.Dialog(ref b);
            }
            b.DeserializeArray(out Widgets);
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
            if (Dialogs is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Dialogs.Length; i++)
            {
                if (Dialogs[i] is null) BuiltIns.ThrowNullReference(nameof(Dialogs), i);
                Dialogs[i].RosValidate();
            }
            if (Widgets is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Widgets.Length; i++)
            {
                if (Widgets[i] is null) BuiltIns.ThrowNullReference(nameof(Widgets), i);
                Widgets[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetArraySize(Dialogs) + BuiltIns.GetArraySize(Widgets);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/WidgetArray";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "bb14b5a388ecaa282d1306ec8ad794da";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
                "2LVbYf7e403dGsGrn+kMkfd1bc+bjQe2f1e7Hzbk/Run/9lcEv/bPWN/GTjMHSTVFl9tt4HyCnCkyJsu" +
                "WmBxS2hePKqnyiqXH1wfevqaAUFPPJKFHuuWWhNO8wMO14g9UGGnrxMv0qt0eFBv9r7X5LWOVy0dtGaj" +
                "OtVntBZHhZ4bW/2CJiQvfdHO8oItDawvGc4ImRjcvd1LLRBUylaMKCuK/mHOuoo/1oKLhaHcWO4v+suL" +
                "0N+Hru04acSzSV4//djHXXfxX3bMcvTw/P+TqZVBW8GGWBRKvf3Wm/In9qIMi0K1mn2otbytfwC/wH7S" +
                "BRMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
