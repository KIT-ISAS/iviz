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
        public GuiArray(ref ReadBuffer b)
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GuiArray(ref b);
        
        public GuiArray RosDeserialize(ref ReadBuffer b) => new GuiArray(ref b);
    
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
        [Preserve] public const string RosMessageType = "iviz_msgs/GuiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "36009101b34a5d35990a50aa9e1bf68c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WW2/bNhR+168gkIcmg+MtSZcNAfrgxV7mIq092+hQDINAi0cyUYlUSSqu+uv3kZJl" +
                "K3PQDVgSGDZ5Lt85PNfIB/k1Lmxmvx9Lnuvsz7+YCAcbyY71hxQZObC24WCjKHrzP/9F75Z3N0w+8iay" +
                "TjT334gLMmwTfqJ17YjxxEmtIGKkypgUkagM9ySWy5ScLChKc83d9WtmE55To+Xqsj2tK+e0ss1FJgGq" +
                "tXarc20Wd7+M2JonnzKjKyXixBN35px0QGwvCS+DK5VU7uJ6d415LjNVkHKtHAKIWxWDYiS1htdSCfDi" +
                "4FdGuiBn6saLD5Q4ba6YS2OdppbcE/wmYbGQtsx5QsHik0g9qefK46O0RSds6bgS3AhEwHHBHWepRjpl" +
                "tiFzntMD5VDiRUmCBa4Phx1CcbWRluGTkSLD87xmlYWQ0yzRRVEpmXCfVWS7pw9NqRhnJTdOJlXODeS1" +
                "Qai9eGp4QR4dH0ufK1IJsen4BjLKUlI5CYdqICSGuPUpno5ZSO/VpVeITlZbfY4rZSjKzjhzG+68s/Sl" +
                "NGS9n9zewMZ3zeOGwEZwCFaEZaeBFuNqzxiMwAUqdbJhp/B8XrsNKtltiD1wI/k6Jw+MOs6B+sorvTo7" +
                "QFYBWnGld/AN4t7Gv4FVHa5/0/kGOcv9622VIYAQLI1+kAKi6zqAJLlEHaHh1oabOvJajcno5FcfYwhB" +
                "K2QEv9xanUgkQGCSuM2ufUI2YjTwc1dj19bNYEAuTXfKutO6O/Hn8uhod+6K3ZAvHoQVAWMPgedrOTWE" +
                "2JZo3aEv22koNK1QpgVx5AAd0WlCUUhDYUAOgUqG0G40YNIxockypR0wCv4JkISse21elgBD6xmubN5M" +
                "UpChckrDbDhg2w2pRspnLfRY6EqZMCMzKRpNGCo6Zc7axw0wey6R9TxvfG6MoYQAYrQLCmdDNk1ZrSu2" +
                "9Q/CwbTDQLM1dX6FonVaD/wkaCH6AZ1rtCbCYi3PUN/KOoyhYdStgy/dqe5OX599pTVbNEyRn9nodjWd" +
                "vY9H4zF7w37oExeTd7MPE9AvjtFH9/dgXUYtb/VxPokXs9XIi4yny9sDvMBbzhfT93ct5+I452ocIA94" +
                "q8Xo7eR2NVt8bDWvetzZ7H41nYP8ukceLe4m+JqMwPnxkDOfLacHDl4/xQuO/BT9t72/3+tHFnjBpepW" +
                "9z+4zbDE6GpF+v8wPK4qlFuJr8dr/2WmhDePSh/tu7zpMp0Gp/ojYuC3oyeLli+DLOY500budDFImlbZ" +
                "CUS/V5jORgXcvdxLPRCu7IYg0uKQORu2TOc/3oKVHlzuPfcbnf0i7u9Dd2yQ9+LZd97fPu/j7ufnN2fV" +
                "7rSNor8BnU2pYcILAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
