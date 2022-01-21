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
        [Preserve] public const string RosMd5Sum = "b398683eea1f66f73240cac840dbe940";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71XW2/TSBh9968YqQ/AqmRpeoFF4sFN3NSQxsFxQNVqFU3siTPC9oTxuCX8es6ML7GT" +
                "IHalLVXVjr/LmTPfbWz+wL8v0jzO/xxymoj4739IZBa5xRvVZx7FTEH1aBa5ZVnv/ucf6242ekv4Hhur" +
                "4Jl6Q+xB4HqThT0cknfkVVfoO3feJwfys2NyezyGqm9VuuB+6iymY9udEI1EyAmh1XlxNrUmiquEnRLF" +
                "vqlTQrMIaikeiViRZaGUyHLSRprden6gkc5KpDylScLkIaKB0qBt74kXuAPNvH/Ig2aEhyKrKOx73jmT" +
                "OdH7nh/xJCnLCs04LRLFN9hcbBQH9TbC9TwIvAkQLkoExbMtCRMefqFLeETt6BsHdwBzveXlL4KmaR8P" +
                "XZ2Ecu/ZwvtAOj+71NYW985s4nUszg4wBvZk4Ix3Fv09ixvP/2z7wxbG+XGLa3vwwVgiKHsWjarBuKwP" +
                "o+OCTE6cvSMY+cD3ZrMucSNvTr6ja+QN1x1JIz9CzciHrj32RhWhlnw+bfCvOvbe50klf92Wu5Mbr5K/" +
                "acuxZ23/V1vu+L7nV+d6dUhoZhSdE3+cOzPdllpRdePZFbHH7miCYg4WQ+fGno+D/VLo2Iydm8agFdOO" +
                "zQB/HL9l0z+08d3RbdDGuTi0eT8H3RvXGdY2bw5tbsbz2W2Hz9Wh0cjx7pzAv6+IIbFHGAXelJA2Uv/y" +
                "CNSdO+xaXZ4dwbr2ULN3bVav+heWlauoHKy3jEYYUWvzz1puFYZTqOcDTCTPYsIjKyok1SKS8BVTPGW6" +
                "5/mKZEKR70wK9Pqa7UZAkpAlI5Kl4oGh7VcK+GrNc6JdrVUiqLq6IHlIE1ZuqLabalWPBvOgR8eO6EAk" +
                "Qvqja5ssafgllqLIokWohTVTM3fqh5CaKVcHpHpc0ITHGSaiquxwien5uIBEclZtvORZBN3C8IqZSJmS" +
                "25LFJxYqIc+JWi3EapUz9RN9GYxFxPNNQkNmdvwpUsfqqe7SvYxbJ2SmMJapjBABRSOqKFkJVAKP10y+" +
                "TNgDS+BE0w2SaLQ6HHkPjoFOJn5jljGJK25LihxGSpBQpGmR8ZDqrCLbHX94clxhZEOl4mGRUAl7IRFq" +
                "bb6SNGUaHb85+1qwLGTEHb6FTZazsFAchLZACCWjuU4x6t+k97yvHayT4FG8xCOLdb3Vm6PyqNJk2beN" +
                "ZLnmSfO32OOP8nA9YCM4DLtEOXluZAs85i8INgEFthHhmjwH8+lWrdEEutYfqOTmZgRwqC/5iDzTTs9e" +
                "tJAzA53RTNTwJeJuj38DmzW4+kwv18hZok+fFzECCMONFA88gulya0BwbaOO0KtLSeXWMv1qtrRObnSM" +
                "y140GcF/muci5EhAZC7vun1MNhbo/aeuxqaty8GAXMpmFTerZbOiT8XoaHfWxS6ZLh6EFQEjD0ana3kl" +
                "GWK7Qev2dNm6ptBEhjJNGUUO0BGNp3lJkszM1h5QmWRoN7wncUUiwXI9TYGR0i+AZMi69qabDcDQepJm" +
                "eVIOYYjh8pz14t4peVyzrLTSWTM9ZrqSh0TymEelJzZKG2dKqsNhbK/65bw2nMvNUEIAkUIZhxc94q7I" +
                "VhTkUR8IC1kNA6GHfM3LFK0SuAkKTdxAdAM6FWhNhCXPaYz6znKFMdSzmuvgW7PaNqvvT/5ZUX7JPMln" +
                "he8FtjYZurNBC6/8UJj67mRUac6Oa86HrddBowt8+70zCDz/vvI872g9bxy409Y7YSm2/ZGDP47deiss" +
                "v3u8mdsiePUznSHy+j++Muzu9f0yQH1s8OfYxV7f5geKcn5imlUXfvcdonlqrEr53pvA7xkc+oAofnvX" +
                "+GXj4btHH7s7NU71hanFUaXnxlZ/LQnJa1/MlrJ7agPrY4GBLTODu7P7XQcElXouIuCKopnNxdPwx1lw" +
                "yxvKneP+otl/C/1d6I7N9k48u+T109dd3PVI/eX4qlePlvUD1Q5+B1kRAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
