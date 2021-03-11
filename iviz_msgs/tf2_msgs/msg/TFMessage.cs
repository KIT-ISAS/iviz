/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/TFMessage")]
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Transforms is null) throw new System.NullReferenceException(nameof(Transforms));
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
                "H4sIAAAAAAAACr1VwW7UMBC9R+o/jOiBFrVZqSAOK+BUAT0ggVpxQaiaJpPEamKn9qTb8PU8O7tJoQg4" +
                "QFeR1nY8b+bNm5nU4jpRP152oQ6rC882VM5358pdL+WXr6S7o5DtZa//8W8v+3D+bk31b4PYy/bpojGB" +
                "5K73EoIE4iUsqrzrqHDOl8ayCvbcCTXCpfg8bS5NCQh1pI08vFk0pi0vl4s7bx1ccS0Uly5oO9IQpKSr" +
                "McHg1iumxkv1+kmj2q9Xq425Nrl3IXe+Xmn15I1Wr1b8hnourgGUR5tzAaAGKl0xdGKV1ThL4AEfHq9s" +
                "pJQO8yx7nzhsqWRBvbH1T+HSfopmYoKtqyaS8dJ0ms3pXHL2/5QMWk4iTrFH6aCiLdmXSKhyycqJbmPq" +
                "RvxxK7fSwioJTemtjr2EfKcCnlqseG53AkDHwnXdYE0RRVQDoe7bw9JYVEjPXk0xtOwfaB7R8QS5GcQW" +
                "Qmena9yxQYpBDQIagVB44RATfnZK2WCsPj+JBtn+xcYdYys1pJmdI+usdK9GS+Kwho9nE7kc2MiOwEsZ" +
                "6CCdXWIbDglOEIL0rmjoAJF/HLVBTUQZb9kbvmpTDRbIAFCfRqOnh/eQY9hrsmzdDn5CXHz8DaydcSOn" +
                "4waatZF9GGokEBd7725NuTRA0RrUL7XmyrMfs2g1ucz236Zq1ChfUgT/HIIrDAQoaWO02RXz3HWPPVrm" +
                "meIl6gUmIbFaxsqV6EYECdu4B/WDskTTevRyQHOjnLLPUqjzzyf7NjVw9mmAgbexwb2bOv2xeG7D+RVL" +
                "ptv08icKsR/OUgU7i/rvhCEuWm22hGFpPEzjbAKqYPRhZB1hnGGaISXWKTA6vgakoJyiNfc9wPh+WuIx" +
                "TA4kr/Mj2jRIcboVyyE1b2p3U5A3NQbaLMhszLRld0RanaCc2naKeXIGFQGyS/hhTmcVjW6gTSSEhd9O" +
                "GQeF57hSN6hzR3HEbCF+zOhHh55fvgk2KAYchK9ax/ryBd3Nq3FefXsktZdC+6XglpyPvTpl8AfZ4+5m" +
                "KdOY5z9xmlcbFPN3XKc7Qz4IAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
