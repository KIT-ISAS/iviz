
namespace Iviz.Msgs.tf2_msgs
{
    public sealed class TFMessage : IMessage
    {
        public geometry_msgs.TransformStamped[] transforms;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "tf2_msgs/TFMessage";
    
        public IMessage Create() => new TFMessage();
    
        public int GetLength()
        {
            int size = 4;
            for (int i = 0; i < transforms.Length; i++)
            {
                size += transforms[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TFMessage()
        {
            transforms = System.Array.Empty<geometry_msgs.TransformStamped>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out transforms, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(transforms, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "94810edda583a504dfda3829e70d7eec";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
