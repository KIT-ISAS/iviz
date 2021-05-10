/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/GuiArray")]
    public sealed class GuiArray : IDeserializable<GuiArray>, IMessage
    {
        [DataMember (Name = "dialogs")] public IvizMsgs.Dialog[] Dialogs { get; set; }
        [DataMember (Name = "widgets")] public IvizMsgs.Widget[] Widgets { get; set; }
    
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
        [Preserve] public const string RosMd5Sum = "aa8468159a55e10b9f5abcf62ffc64fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTY/bNhC961cQ2EN2C6+LJkUOC/SQZpF0DwXSJkgOQSGMyJFEhCIVkrKj/Po+krJc" +
                "bzZIC9QxDJsfM2+G8+ZD7/Tneghd+PFWk3Hd+7+EyotQ6fXqnVYdR1zt8yJUVfXL//ypfn/98kboe95U" +
                "Iaqy/41JsRd9/quaObIgGbWzEPHadkKrSk2e0pEwuuWoB65a4yg+/VkESYaLVpzHZdVMMTobykbLDLVY" +
                "e+6M83++/PWZaEh+6LybrKplOjyYizoCcdlIGrMrk7bxp6eHbU1Gd3ZgGxc5BBC7qcaJ17wYbrRVuKuz" +
                "Xx27gaOfixdvWUbnn4jY1q5tA8ev3BfCaqXDaEhytvhVpBOpc/F4j7bqQryOZBV5hQhEUhRJtA506q5n" +
                "f214xwZKNIysRL5N4QhbKL7pdRD4dmzZkzGzmAKEohPSDcNktaTEKtg+0YemtoLESD5qORnykHceoU7i" +
                "raeBEzq+gT9ObCWLu9sbyNjAcooaDs1AkJ4pJIrvbkWm98njpFBdvNm7a2y5Q1KuxkXsKSZn+dPoOSQ/" +
                "KdzAxg/lcVtgIzgMKyqIy3xWYxuuBIzABR6d7MUlPH81xx6ZHHsWO/KaGsMJGHlsgPooKT26+geyzdCW" +
                "rDvAF8SjjX8Da1fc9KbrHpyZ9PowdQggBEfvdlpBtJkziDQaeYSCazz5uUpaxWR18SLFGELQyozgn0Jw" +
                "UoMAhU4S+0P5ZDZqFPC5s3Et69IYwKVfV926atYVncujB6vzkOyeU/IgrAiY2OW7lMutZ8R2ROluU9re" +
                "5URzFmk6MIEDVMSqCUWlPecGuQUqe0a58UboKJTjIKyLwBjoAyAZrCdtGkeAofQ82WBKJ8UxVC552203" +
                "Yt+zLVKJtVxjuSq1FF53WhVNGBpWZRLL4zboPY/BujHF52IMKQQQ72JWuNqKu1bMbhL79CAs/NIMnGh4" +
                "9SsnbXRukzrBAnEa0FcOpYmwhEAd8tuGiDa0rdZx8Gldzevq89lHWpmi/22kHUfWA7NpIG3XqfTFbekD" +
                "qMpF5HQW3g8YIjni52wT4Ut7YO3ZMWNLxrg2e3Ga7pvU6dOxWu51lkVvEs7rgy6KotB+EKj+mNBpvM24" +
                "R7nv9UC4ciho8BBBVcgdc/Ufb8F4yi6fPPcbWfpd3D+G7qGmdBLPU+fT7uMx7qkXfLPuDqt9Vf0NGGRp" +
                "Uo4KAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
