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
        [Preserve] public const string RosMd5Sum = "34a0d7ab7f55543995b141cd5124de2d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71YbW/aOhT+nl9hqR+2XXXclb5sd9I+BEhpNkpYCJuqqytkEhOshZg5Tlv66/fYeSEB" +
                "pt0r3a5C4Jxz/PjxebNTfs+f5usszv4ccJqI+O9/SGQGmcVr1VcexUxB9WAGmWVZH/7nP+t2OnxP+B4b" +
                "K+epekfsfuB647k9GJAP5E1b6Du33hcH8rNjcns0gqprlbrgbuLMJyPbHRONRMgJoeV+sTe1IoqrhJ0S" +
                "xR7VKaFpBLUUD0QsySJXSqQZaSJNbzw/0EhnBVK2pknC5CGigdKgzdljL3D7mnn3kAdNCQ9FWlLYn3nr" +
                "jGdEr3t+ZCZZszTXjNd5ovgGi4uN4qDeROjNgsAbA+GiQFA83ZIw4eE3usCMqOl9M8Htw1wvefkLp2na" +
                "x11XBaFYezr3PpHW3y60lcWdMx17LYuzA4y+Pe47o51Fd8/i2vO/2v6ggXF+3KJn9z8ZSzhlz6JW1RiX" +
                "1Wa0XxDJsbO3BSPv+9502iZu5PXOd3SNvOa6I2nkR6gZ+cC1R96wJNSQzyY1/lXL3vs6LuVvm3J3fO2V" +
                "8ndNOdas7P9qyh3f9/xyX28OCU2NorXjzzNnqstSK8pqPLsi9sgdjpHMwXzgXNuzUbCfCi2bkXNdGzR8" +
                "2rLp48vxGzbdQxvfHd4ETZyLQ5uPM9C9dp1BZfPu0OZ6NJvetPhcHRoNHe/WCfy7khgCe4RR4E0IaSJ1" +
                "L49A3bqDttXl2RGsnoecvW2yetO9sKxMRUVjvWE0QotamR9rsVVoTqHuDzCRPI0Jj6wol1SLSMKXTPE1" +
                "0zXPlyQVijwxKVDrK7ZrAUlCFoxIthb3DGW/VMBXK54RPdVaJoKqqwuShTRhxYJquylHVWswD7p17Ij2" +
                "RSKkP+zZZEHDb7EUeRrNQy2smJq+Uz2E1HS5yiHl45wmPE7REVVph0NM98c5JJKzcuEFTyPo5oZXzMSa" +
                "KbktWHxhoRLynKjlXCyXGVM/0RfOmEc82yQ0ZGbFnyK1rJ7rLN2LuHVCpgptmcoIHlA0ooqSpUAm8HjF" +
                "5OuE3bMEk+h6gyAarXZH1sHEQAcTn5ilTOKI25I8g5ESJBTrdZ7ykOqoItqt+ZjJcYSRDZWKh3lCJeyF" +
                "hKu1+VLSNdPo+GTse87SkBF38B42acbCXHEQ2gIhlIxmOsTIfxPe866eYJ0ED+I1Hlms861aHJlHlSbL" +
                "HjeSZZonzd5jjT+KzXWADecwrBJl5KWRzfGYvSJYBBTYRoQr8hLMJ1u1QhHoXL+nkpuTEcChPuQj8kJP" +
                "evGqgZwa6JSmooIvEHdr/BvYtMbVe3q9QswSvfssj+FAGG6kuOcRTBdbA4JjG3mEWl1IKreWqVezpHVy" +
                "rX1c1KKJCH5plomQIwCRObyr8jHRmKP2nzsb67IuGgNiKetRXI8W9Yg+F6Oj1Vklu2Q6eeBWOIzcG53O" +
                "5aVk8O0GpdvRaeuaRBMp0nTNKGKAiqhnmkuSZKa3doDKJEO54Z7EFYkEy3Q3BcaafgMkQ9T1bLrZAAyl" +
                "J2maJUUThhhTXrJO3DklDyuWFlY6aqbGTFXykEge86iYiYXW9WRKys2hbS+7Rb82nIvFkEIAkUKZCa86" +
                "xF2SrcjJg94QBrJsBkI3+YqXSVolcBLkmriBaDt0IlCacEuW0Rj5nWYKbahj1cfBYz3a1qOnZ3+tKN5k" +
                "nuW1wvcCW5sM3Gm/gVe8KEx8dzwsNWfHNeeDxnXQ6ALf/uj0A8+/K2eet7SeNwrcSeNOWIhtf+jgy7Eb" +
                "t8Livcebug2CVz/TGSJvm9qeNxsPbFxibpz+J3MZ+m/3id2hv58jSJ4Nvo6d+tVRf6AomitaXXkbaF8w" +
                "6qfaqpDvXRN6+joBQU884kqwKJ/mC/GIS8Fv6Th686gae9cxiorFC5N2SbvdnOqTVoujUs+NrX7NEpJX" +
                "c9GUirKrDKzPOTq9TA3uzu53bRBUqoaKYCiKLmBOrJo/9oLrgaHc2u4vusRvob9z3bFDoeXPNnn99H3n" +
                "d92Lf9n3qtHD8/9npZH5Vrt4jxRniC0eaKqbbMafmGX9ALDa6cdCEgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
