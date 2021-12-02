/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GuiArray : IDeserializable<GuiArray>, IMessage
    {
        [DataMember (Name = "dialogs")] public IvizMsgs.Dialog[] Dialogs;
        [DataMember (Name = "widgets")] public IvizMsgs.Widget[] Widgets;
    
        /// Constructor for empty message.
        public GuiArray()
        {
            Dialogs = System.Array.Empty<IvizMsgs.Dialog>();
            Widgets = System.Array.Empty<IvizMsgs.Widget>();
        }
        
        /// Explicit constructor.
        public GuiArray(IvizMsgs.Dialog[] Dialogs, IvizMsgs.Widget[] Widgets)
        {
            this.Dialogs = Dialogs;
            this.Widgets = Widgets;
        }
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new GuiArray(ref b);
        
        GuiArray IDeserializable<GuiArray>.RosDeserialize(ref Buffer b) => new GuiArray(ref b);
    
        public void RosSerialize(ref Buffer b)
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
        [Preserve] public const string RosMessageType = "iviz_msgs/GuiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9f06737271ba3bd6e548108be206ae67";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VS4/bNhC+61cMsIfsFl4XTYocFugh7aLpHgqkTdAeisIYiSOJCEUqJGVH+fX9SMpy" +
                "vd30AXTXMGw+5sX5vpnRe/1pN4QufHmr2bjut99J5UWo9Hr1q1adRFwd8iJUVfXN//ypfnz7+obuR1OF" +
                "qMr+B2Elnvr8V9VzFOImamch4rXtSKtKTZ7TERndStSDVK1xHF9+TaFhI0UrzuOyqqcYnQ1lo5tsavH2" +
                "nTPO//z621dUc/O+826yatekw6O7qCMsLpuGxxzKpG386uVxu2OjOzuIjYscEojdtMOJ17I4rrVVuNvl" +
                "uDpxg0Q/lyh+kSY6/4Jiu3NtGyR+5r4AtlM6jIYbyR4/a+lM6rFwvAdbdUFvI1vFXiEDkRVHptYBTt31" +
                "4q+N7MVAiYdRFOXblI6wheK7XgfCtxMrno2ZaQoQio4aNwyT1Q0nVIH2mT40tSWmkX3UzWTYQ955pDqJ" +
                "t54HSdbxDfJhEtsI3d3eQMYGaaaoEdAMC40XDgniu1vK8L54nhSqi3cHd42tdCDl6pxizzEFKx9HLyHF" +
                "yeEGPr4oj9vCNpIj8KICXeazHbbhiuAEIcjomp4uEfmbOfZgcuyF9uw11wYcDaCWMbD6LCk9u/qT5RT2" +
                "DVm27mi+WDz5+Ddm7Wo3vem6B2YmvT5MHRIIwdG7vVYQredspDEaPELB1Z79XCWt4rK6+D7lGELQyojg" +
                "n0NwjQYACp0k9sfyyWjsUMCPzca1rEtjAJZ+XXXrql5X/FgRPVidR7J7SeRBWpEw2ue7xOXWC3I7onS3" +
                "ibZ3mWjOgqaDMDBARayaUFTaQxVtaAur4gXlJhvSkZSTQNZF2Bj4PUwKUE/aPI4whtLzbIMpnRTHULmU" +
                "bbfd0KEXW6QSarnGclXqhrzutCqacDSsykzL4zboPc+BujEl5uIMFIIR72JWuNrSXUuzm+iQHoSFX5qB" +
                "o1rWuDJpo3Ob1AkWE+cJfeNQmkhLCNyB3zZEtKFttY6Dj+tqXlefHn2klSn630baaWQ9MJsG1nadSn+5" +
                "LX0AVbmInM/C+wlDJkf83J9oT1MAyT1AfHUicCGQa3NQ5+zfpMafjtVyr7MsWhU5jNVFFzVSWHAUqH6a" +
                "0Hi8zXZPck/1QIRyrG/AEoFcyA10jR9vwbTKIZ89t/p70j5J+KfUPdSjzvJ5HnzafTjlPbWGfyzD4+pQ" +
                "VX8ApOXbOZ0KAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
