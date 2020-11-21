/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract (Name = "tf2_msgs/TFMessage")]
    public sealed class TFMessage : IDeserializable<TFMessage>, IMessage
    {
        [DataMember (Name = "transforms")] public GeometryMsgs.TransformStamped[] Transforms { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TFMessage()
        {
            Transforms = System.Array.Empty<GeometryMsgs.TransformStamped>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TFMessage(GeometryMsgs.TransformStamped[] Transforms)
        {
            this.Transforms = Transforms;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TFMessage(ref Buffer b)
        {
            Transforms = b.DeserializeArray<GeometryMsgs.TransformStamped>();
            for (int i = 0; i < Transforms.Length; i++)
            {
                Transforms[i] = new GeometryMsgs.TransformStamped(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TFMessage(ref b);
        }
        
        TFMessage IDeserializable<TFMessage>.RosDeserialize(ref Buffer b)
        {
            return new TFMessage(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Transforms, 0);
        }
        
        public void RosValidate()
        {
            if (Transforms is null) throw new System.NullReferenceException(nameof(Transforms));
            for (int i = 0; i < Transforms.Length; i++)
            {
                if (Transforms[i] is null) throw new System.NullReferenceException($"{nameof(Transforms)}[{i}]");
                Transforms[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Transforms)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/TFMessage";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "94810edda583a504dfda3829e70d7eec";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VwW7UQAy9R8o/WOXQFrVZCRCHVeGEgB6QQK24IFS5iZOMmsykM06X5et5M+kmC0WI" +
                "A3TV1c4k9rOfn+024npRv73qQxNWl55tqJ3vL5T7QaovX0l3j0Ke5dmrf/zJsw8X79bU/DGLPHtCl60J" +
                "JN8GLyFIIF7yotq7nkrnfGUsq+DOvVArXIkv0uXKJAx1pK08NC1b01VXe5ZzwB7RuBGKRxe029IYpKLr" +
                "bQKKZmdMrZf61UGrOqxXq425MYV3oXC+WWl98FrrsxW/poHLGyAVyelCAKmBKleOvVhlNc4SyCCKxysb" +
                "eaWHRSz6+0TlnlGeBfXGNr9kTU9SShMhXF09cY1G09M8m+u6FO8/ahq0muSc0k+8lW3FvkJdlStWTpxb" +
                "07TiTzu5kw5eSXJKb3U7SChmNfDXiBXP3U4IKFq6vh+tKaOcaiDYPkB0NRbdMrBXU44d+wfyJ/z4DXI7" +
                "ii2Fzt+sYWWDlKMaJLUFRumFQyz7+RsYj8bq82fRA46XG3eKuzTQaM4A1WelvZ6tiMM6hnk6cSwAjyIJ" +
                "AlWBjtKzK1zDMSEOspDBlS0dIf2PW23RH1HPO/aGr7vUkSXqANjD6HR4vA8dU1+TZet2+BPkEuRvcO0C" +
                "HGmdthCviyUIY4M6wnLw7s5Uy0CUnUE3U2euPfttnkW3KShA3qbW1Chk0ga/HIIrDZSoaGO0nVt7GcXH" +
                "3jhzr3mJsoFNSMyWbXMtuhFB1TbuQSuhRTHGHtMdMO+xs/Lss5Tq/PMJoUtDnWefRvh4G6feu2n8H43r" +
                "fUK/Y8p0l17+QiNNx3nqZmcxDb0wVMbsza7wrIyHb1xZgBWsRGyyE2w5LDnUxTqNID3fAFTQWdGdhwFo" +
                "vF+a+Bg+R1I0xQltWhQ6WcWumMY5bQBTkjcN1tysy+zNdE/whLR+hr7quinrKRrEjCi7qh8XdF7T1o20" +
                "iZxw8Perx0HpObM0GurcSVw7O4yfy/rRYQcs/zBsUKy91AF151hfvqBvyxGzsTt+fyTZl5b7rfKWnI/D" +
                "O9XxJ/3j7XZp2Fjtv+O1O26i9Q8UCIXjaQgAAA==";
                
    }
}
