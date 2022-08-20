/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class WidgetArray : IDeserializable<WidgetArray>, IMessage
    {
        [DataMember (Name = "dialogs")] public IvizMsgs.Dialog[] Dialogs;
        [DataMember (Name = "widgets")] public IvizMsgs.Widget[] Widgets;
    
        public WidgetArray()
        {
            Dialogs = System.Array.Empty<IvizMsgs.Dialog>();
            Widgets = System.Array.Empty<IvizMsgs.Widget>();
        }
        
        public WidgetArray(IvizMsgs.Dialog[] Dialogs, IvizMsgs.Widget[] Widgets)
        {
            this.Dialogs = Dialogs;
            this.Widgets = Widgets;
        }
        
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
        
        public WidgetArray(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeArray(out Dialogs);
            for (int i = 0; i < Dialogs.Length; i++)
            {
                Dialogs[i] = new IvizMsgs.Dialog(ref b);
            }
            b.Align4();
            b.DeserializeArray(out Widgets);
            for (int i = 0; i < Widgets.Length; i++)
            {
                Widgets[i] = new IvizMsgs.Widget(ref b);
            }
        }
        
        public WidgetArray RosDeserialize(ref ReadBuffer b) => new WidgetArray(ref b);
        
        public WidgetArray RosDeserialize(ref ReadBuffer2 b) => new WidgetArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Dialogs.Length);
            foreach (var t in Dialogs)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Widgets.Length);
            foreach (var t in Widgets)
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
            b.Serialize(Widgets.Length);
            foreach (var t in Widgets)
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
            if (Widgets is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Widgets.Length; i++)
            {
                if (Widgets[i] is null) BuiltIns.ThrowNullReference(nameof(Widgets), i);
                Widgets[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 8 + WriteBuffer.GetArraySize(Dialogs) + WriteBuffer.GetArraySize(Widgets);
        
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
            c = WriteBuffer2.Align4(c);
            c += 4; // Widgets.Length
            foreach (var t in Widgets)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/WidgetArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bb14b5a388ecaa282d1306ec8ad794da";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71YbW/aOhT+nl9hqR+2XXXclb5sd9I+pJDSbJSwEDZV04RMYoK1EDPHtKW/fo+dFxLI" +
                "tHul21YInPPy+PHxOcdO+R1/nK2yOPu7z2ki4m/fSWQGmcUr1VcexUxBdW8GmWVZH/7nP+tmMnhP+B4b" +
                "a8NT9Y7YvcD1RjO73ycfyJum0HduvC8O5Cdtcns4hKprFbrgduzMxkPbHRGNRMgRocV6sTa1JIqrhB0T" +
                "xR7UMaFpBLUU90QsyHyjlEgzUkeaXHt+oJFOcqRsRZOEyUNEA6VB694jL3B7mnn3kAdNCQ9FWlDY97xx" +
                "RlOi5z1t8SQrlm4049UmUXyNycVacVCvI1xOg8AbAeEsR1A83ZIw4eEPOodHVI++cXB7MNdTnv8haJp2" +
                "e+jKTcjnnsy8T6Txt9va0uLWmYy8hsXJAUbPHvWc4c6iu2dx5flfbb9fwzhtt7i0e5+MJYKyZ1GpKozz" +
                "cjE6LtjJkbO3BCPv+d5k0iRu5NXKd3SNvOK6I2nkLdSMvO/aQ29QEKrJp+MK/6Jh730dFfK3dbk7uvIK" +
                "+bu6HHOW9v/U5Y7ve36xrjeHhCZG0Vjx56kz0WWpFUU1nlwQe+gORkjmYNZ3ruzpMNhPhYbN0LmqDGox" +
                "bdj08OX4NZvuoY3vDq6DOs7Zoc3HKeheuU6/tHl3aHM1nE6uG3wuDo0GjnfjBP5tQQwb28Io8MaE1JG6" +
                "5y1QN26/aXV+0oJ16SFnb+qs3nTPLCtTUd5YrxmN0KKW5seabxWaU6j7A0wkT2PCIyvaSKpFJOELpviK" +
                "6ZrnC5IKRR6ZFKj1Jdu1gCQhc0YkW4k7hrJfKOCrJc+IdrUWiaDq4oxkIU1YPqHarotR2RrMg24dO6I9" +
                "kQjpDy5tMqfhj1iKTRrNQi0smZq+Uz6E1HS5MiDF44wmPE7REVVhh0NM98cZJJKzYuI5TyPoZoZXzMSK" +
                "KbnNWXxhoRLylKjFTCwWGVO/0efBmEU8Wyc0ZGbG3yI1rJ7qLN3bceuITBTaMpURIqBoRBUlC4FM4PGS" +
                "ydcJu2MJnOhqjU00Wh2OrAPHQG8mPjFLmcQRtyWbDEZKkFCsVpuUh1TvKna74Q9PjiOMrKlUPNwkVMJe" +
                "SIRamy8kXTGNjk/Gfm5YGjLi9t/DJs1YuFEchLZACCWjmd5i5L/Z3tOudkBKfvNFdvLdOgruxWvIWawT" +
                "r2SBFKRKs2YPa8kyTZhm7zHZX/kqO5gEUWKYLsrISyOb4TF7RTAbuLC1CJfkJZYw3qolqkEn/R2V3ByR" +
                "AA71aR+RF9rpxasacmqgU5qKEj5H3M3xb2DTClev6fUSm5foMGSbGJGE4VqKOx7BdL41IDi/kVAo2rmk" +
                "cmuZwjVTWkdXOth5UZqtwS/NMhFy7ERkTvGyjsy2zNAEnjotq/rOOwQ2VVajuBrNqxF9KkatZVpmvWQ6" +
                "eRBWBIzcGZ1O6oVkiO0aNdzR+euaRBMp8nXFKPYApVF5mtuSZKbJdoDKJEPd4cLEFYkEy3RbBcaK/gAk" +
                "w65rb7peAww1KGmaJXk3hhguL1kn7hyT+yVLcyu9a6bYTHnykEge8yj3xESrypmSYnHo34tu3rgN53wy" +
                "pBBApFDG4VWHuAuyFRtyrxeEgSy6gtDdvuRlklYJHAkbTdxANAM6FihNhCXLaIz8TjOFftSxqnPhoRpt" +
                "q9Hjk79f5K80T/J+4XuBrU367qRXw8vfGMa+OxoUmpN2zWm/di80usC3Pzq9wPNvC8/ThtbzhoE7rl0O" +
                "c7HtDxx8OXbtepi/AHkTt0bw4nc6Q+RtXXvpTUd927+tXRQb8t610/tkbov/7cKxuxXs5w6Sao2vtmtB" +
                "eRc4UORNFy2wuC40byDVU2WVy/fuEZf6vgHBpXggcz3WLbUmnOQnHe4TO6DCTt8rnqVX6fCg3uxdr8lr" +
                "He9cOmjNRnWsD2stjgo9N7b6TU1IXvqineUFWxpYnzc4I2RqcHd2z7VAUClbMaKsKPqHOesq/lgLbhiG" +
                "cmO5f+gvz0J/F7q246QRzyZ5/fRzF3fdxf/YMcvR/dP/c6ZWBm0FG2JRKPX262/GH9mzMiwK1Wr2odby" +
                "tn4BPC2KzQ4TAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
