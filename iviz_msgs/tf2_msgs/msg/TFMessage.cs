/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TFMessage : IDeserializable<TFMessage>, IMessage
    {
        [DataMember (Name = "transforms")] public GeometryMsgs.TransformStamped[] Transforms;
    
        /// Constructor for empty message.
        public TFMessage()
        {
            Transforms = System.Array.Empty<GeometryMsgs.TransformStamped>();
        }
        
        /// Explicit constructor.
        public TFMessage(GeometryMsgs.TransformStamped[] Transforms)
        {
            this.Transforms = Transforms;
        }
        
        /// Constructor with buffer.
        internal TFMessage(ref ReadBuffer b)
        {
            Transforms = b.DeserializeArray<GeometryMsgs.TransformStamped>();
            for (int i = 0; i < Transforms.Length; i++)
            {
                GeometryMsgs.TransformStamped.Deserialize(ref b, out Transforms[i]);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TFMessage(ref b);
        
        public TFMessage RosDeserialize(ref ReadBuffer b) => new TFMessage(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Transforms);
        }
        
        public void RosValidate()
        {
            if (Transforms is null) throw new System.NullReferenceException(nameof(Transforms));
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Transforms);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "tf2_msgs/TFMessage";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "94810edda583a504dfda3829e70d7eec";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTW/UMBC951eM2gMtarMSRRwq4ISAHpCKWnFBqJomk8RqYgd70m349Tw7m6RQBByg" +
                "q0hrO543H+/NpBbXifrxqgt12Fx6tqFyvrtQ7nopP38hnY9Clr36x7/sw8W7U6p/G0K2T5eNCSR3vZcQ" +
                "JBCvMVHlXUeFc740llWw506oES7F52lzZSKEOtJGHt4sGtOWV+vF2VsHV1wLxaUL2o40BCnpekwwuPWS" +
                "qfFSvdprVPvTzWZrbkzuXcidrzda7b3W6uWGX1PPxQ2A8mhzIQDUQKUrhk6sshpnCXnAh8crG1NKh3mW" +
                "vU857FLJgnpj65/Cpf0UzZQJtq6akoyXptNsqeZas/9FY9ByYnCKPOarbEv2JaqpXLJyyrUxdSP+uJVb" +
                "aWGUSKb0VsdeQj5TgKcWK57bufogsXBdN1hTRAbVgKX79rA0FvLo2asphpb9A8IjOp4gXwexhdDZm1Pc" +
                "sUGKQQ0CGoFQeOEQq332hrLBWD15Fg2y/cutO8ZWavCyOEfJWemeQEvicAofT6fkcmCjOAIvZaCDdHaF" +
                "bTgkOEEI0ruioQNEfj5qA0FEDm/ZG75ukwALVACoT6LRk8N7yDZBW7Zuhp8QVx9/A2sX3JjTcQPO2ph9" +
                "GGoUEBd7725Nuaq/aA3ES6259uzHLFpNLrP9t0mKGulLjOCfQ3CFAQElbY02s5KXlnvcoTKLy0skC2mE" +
                "lNI6UK5FtyKo1tY9EE+I8qo8ujigraGl7JMU6vzJZN+m1s0+DjDwNra2d1OPP06Su2B+kSLTbXr3U/yx" +
                "E86Sdp2F8jth0IomWyxhWBoP0ziSgCqYeJhUR5hiGGKoh3UKjI5vACkQUrTmvgcY369JPIbJgeR1fkTb" +
                "BvVNt6IQUtumRjcFeVNjji1sLMZMu+SOSKtnEFLbTjFPzkAhQOZqH+Z0VtHoBtrGhLDwu/niQO8SV+oD" +
                "de4oDpcdxI8FPXfo9vVTYINisoH1qnWsL57T3bIal9W3R6F61div2LbkvFm+Lz9wHndfV4HGIv8xoXm1" +
                "zbLvqQ5/iy4IAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
