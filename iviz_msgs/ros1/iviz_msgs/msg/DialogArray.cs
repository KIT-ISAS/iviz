/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class DialogArray : IHasSerializer<DialogArray>, IMessage
    {
        [DataMember (Name = "dialogs")] public Dialog[] Dialogs;
    
        public DialogArray()
        {
            Dialogs = EmptyArray<Dialog>.Value;
        }
        
        public DialogArray(Dialog[] Dialogs)
        {
            this.Dialogs = Dialogs;
        }
        
        public DialogArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Dialog>.Value
                    : new Dialog[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Dialog(ref b);
                }
                Dialogs = array;
            }
        }
        
        public DialogArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Dialog>.Value
                    : new Dialog[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Dialog(ref b);
                }
                Dialogs = array;
            }
        }
        
        public DialogArray RosDeserialize(ref ReadBuffer b) => new DialogArray(ref b);
        
        public DialogArray RosDeserialize(ref ReadBuffer2 b) => new DialogArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Dialogs.Length);
            foreach (var t in Dialogs)
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
        }
        
        public void RosValidate()
        {
            if (Dialogs is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Dialogs.Length; i++)
            {
                if (Dialogs[i] is null) BuiltIns.ThrowNullReference(nameof(Dialogs), i);
                Dialogs[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Dialogs) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Dialogs.Length
            foreach (var msg in Dialogs) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/DialogArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ba4917c22f5a5cdc9531f1b84db1226d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VWW2/iOBR+z684Uh+mXbVsSy/brTQPFEKbHUq6IcyoGo0ikzjBahIztkNLf/0eOxeS" +
                "wmj3YYsQcY7P+c7nczMjRlKefP8BkVlIy7I+/88f62F2dwNszd6CTCby95HxZBUsV9cwGPqOOw0GoxF8" +
                "htOu0LMf3K82ys/2yQeTCW71rWrPf3q0g8fJwJmCRgI4AFIdCl6YWoJiKqXHoOirOgaSR7gt+AvwGBaF" +
                "UjyX0Eaa3buer5HOSiSZkTSlYhfRQGnQtvXU9Z2hZt7f5UFyYCHPKwrvLR/s6Ry03/M9lpDRvNCMsyJV" +
                "bIXO+UoxpN5GuJ37vjtFhIsSQbF8A2HKwmeyQIuoHX1j4AxRXbu8/Jegadr7Q1cnofQ9C9wv0PlsU1tr" +
                "PNmzqdvRONvBGA6mQ3uy1ei/0xi73reBN2phnO/XuB0MvxhNDMo7jWarwbisD6Pjgpmc2u+OYORDz53N" +
                "usSNvDn5lq6RN1y3JI18DzUjHzmDiXtXEWrJ548N/lVH3/02reR/tOXOdOxW8uu2HH3W+n+25bbnuV51" +
                "rtNdQjOz0Tnx33N7pttSb1TdeHYFg4lzN8Vi9oORPR7MJ/77UujoTOxxo9CKaUdniD+219Lp7+p4zt29" +
                "38a52NX5a450x449qnWud3XGk/nsvsPnalfpznYfbN97qohhYvcw8t1HgDZS/3IP1IMz6mpdnu3BunWx" +
                "Zh/arE77F5YlVVQO1ntKIhxRS/OwFhuFwynU8wFVBMsTYJEVFYJoEaQspoplVPc8iyHnCt6o4NjrS7od" +
                "AWkKCwqCZnxNse1jhfhqySRoUytOOVFXFyBDktLSodqsqlU9GsyLHh1bokOecuHd3Q5gQcLnRPAij4JQ" +
                "C2umZu7ULyExU64OSPUakJQlOU5EVenhJabnY4ASwWjleMHyCPcCwyuhPKNKbEoWX2mouDgHFQc8jiVV" +
                "v9gvgxFETK5SElLj8ZdIHa2PukvfZdw6gJnCsUxEhBFQJCKKQMyxEliypOIkpWuaohHJVphEs6vDIXto" +
                "6Otk4jehORV4xW2gkKikOIQ8y4qchURnFbPdsUdLhlcYrIhQLCxSIlCfCwy1Vo8FyahGx6+kPwuahxSc" +
                "0Q3q5JKGhWJIaIMIoaBE6hRj/Zv0nve1AZbkd4/Lsx/Wgf/CT1BOE114NQssQaI0a/q6ElRqwkTeoLPf" +
                "ylP20AlGiaK7SMKhkQX4Ko8AvSEXuuLhEg7xCI8btcRu0EW/JoKZKxKBQ33bR/BJG306aiHnBjonOa/h" +
                "S8Stj/8Cmze4+kwnS0xeqsMgiwQjiYorwdcsQtXFxoDg/Y0FhU27EERsLNO4xqV1MNbBLpvSpAafREoe" +
                "MsxEZG7xuo9MWgIcAh9dlk1/lxMCkyqaVdKsFs2KfBSjvW1aV72gungwrBgwWJs9XdSxoBjbFfZwT9ev" +
                "YwqN51ivGSWYA2yNxtL8WxLUDNkeolJBse/wDxNTEHEq9VhFjIw8IyTFrGtrslohGPagILlMy2mMYjQ5" +
                "pL2kdwwvS5qXWjprptlMe7IQBEtYVFqio6wxJlAdDud33C8Ht+FcOsMSQhDBlTE46oETw4YX8KIPhAtR" +
                "TQWup33NyxSt4nglFJq4gegG9JFja2JYpCQJ1ncuFc6jntXcC6/NatOs3qx/AMDIIz9zDAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<DialogArray> CreateSerializer() => new Serializer();
        public Deserializer<DialogArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<DialogArray>
        {
            public override void RosSerialize(DialogArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(DialogArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(DialogArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(DialogArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(DialogArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<DialogArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out DialogArray msg) => msg = new DialogArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out DialogArray msg) => msg = new DialogArray(ref b);
        }
    }
}
