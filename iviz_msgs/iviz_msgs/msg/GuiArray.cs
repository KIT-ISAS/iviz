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
        public GuiArray(ref Buffer b)
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
        
        public void Dispose()
        {
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
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                foreach (var i in Dialogs)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Widgets)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/GuiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9f06737271ba3bd6e548108be206ae67";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTW/cNhC961cQ8CF2sd6iSZGDgR7SGk19KJA2QXsoCmFEjiQiFKmQ1G6UX99HUtJ2" +
                "XQdpgdqLxS4/Zt4M582HPuhP9RC68PWtJuO6P/4UKi9Cpber37XqOOLqmBehqqrv/udP9fPb1zdC3/Om" +
                "ClGV/U9Mir3o81/VzJEFyaidhYjXthNaVWrylI6E0S1HPXDVGkfx5bciSDJctOI8LqtmitHZUDZaZqjF" +
                "2g/OOP/r6+9fiYbk+867yapapsPVXNQRiMtG0phdmbSN37xctzUZ3dmBbVzkEEDsphonXvNiuNFW4a7O" +
                "fnXsBo5+Ll78xjI6/0LEtnZtGzh+5r4QVisdRkOSs8XPIp1JPRaP92irLsTbSFaRV4hAJEWRROtAp+56" +
                "9teGD2ygRMPISuTbFI6wh+K7XgeBb8eWPRkziylAKDoh3TBMVktKrILtM31oaitIjOSjlpMhD3nnEeok" +
                "3noaOKHjG/jDxFayuLu9gYwNLKeo4dAMBOmZQqL47lZkel88TwrVxbuju8aWOyTlZlzEnmJylj+OnkPy" +
                "k8INbHxVHrcHNoLDsKKCuMxnNbbhSsAIXODRyV5cwvM3c+yRybFncSCvqTGcgJHHBqjPktKzq78h2wxt" +
                "yboVviCebPwbWLvhpjdd9+DMpNeHqUMAITh6d9AKos2cQaTRyCMUXOPJz1XSKiarix9TjCEErcwI/ikE" +
                "JzUIUOgksV/LJ7NRo4AfOxu3si6NAVz6bdVtq2Zb0WN59GB1rsnuOSUPwoqAiUO+S7ncekZsR5TuPqXt" +
                "XU40Z5GmAxM4QEVsmlBU2nNukHugsmeUG++EjkI5DsK6CIyB3gOSwXrSpnEEGErPkw2mdFIcQ+WS991+" +
                "J4492yKVWMs1lqtSS+F1p1XRhKFhUyaxPG6H3vMcrBtTfC7GkEIA8S5mhau9uGvF7CZxTA/Cwi/NwImG" +
                "N79y0kbndqkTLBDnAX3jUJoISwjUIb9tiGhD+2obBx+31bytPj36SCtT9L+NtNPIemA2DaTtNpX+cVv6" +
                "AKpyETmfhfcDhkiO+Lk/0Z6mAJJ5kPjqlMAlgVybnTrP/l1q/OlYLfc6y6JVCef1qosaKVmwClS/TGg8" +
                "3mbck9xTPRCurPUNWiKYC7mBbv7jLZhW2eWz534haZ/E/VPoHupRZ/E8dz7tPpzinlrDF8twXR2r6i+k" +
                "5ds5nQoAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
