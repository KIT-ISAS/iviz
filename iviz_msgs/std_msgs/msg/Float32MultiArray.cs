/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Float32MultiArray")]
    public sealed class Float32MultiArray : IMessage, IDeserializable<Float32MultiArray>
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public float[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Float32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Float32MultiArray(MultiArrayLayout Layout, float[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Float32MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Float32MultiArray(ref b);
        }
        
        Float32MultiArray IDeserializable<Float32MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Float32MultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 4 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Float32MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6a40e0ffa6a17a503ac3f8616991b1f6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UXWvbMBR996+4JC9tlmb5KGUt9CEw2EsLgw3GCKGo1nWsRJaCJDfrfv2O/J12j2PG" +
                "YFn365yjezWmr5qFZ9LWHkgECjnTY6mDWjsnXh/Eqy0DFey92DFJzpRRQVlDmXXJmKRNy4JNENUeXqE1" +
                "FTFcxHA/S5J3yUg33/oZkz9yqjKVNkkykiKIxivJtBVhtdxsW//aSn14VakNS5Lk/h8/yeO3L3fkg3wq" +
                "/M5/fMsHKnyHZj1pqJRq4diToB0bdiqtrVdSQSsPkkL3qAUSHIULKi0RVbMLr0eeEX1u/ZHKMVkn2bGk" +
                "zNmCUJkdFdZHAMGSMqb5P9O8SwEBUR5yrTu5WhMdnT0yELBPSmWgdoXiyWaZ58E5HYWUyuyINccz97Fd" +
                "gMWEXnykT1M0i3WefG5LLWn98GP98xs9M52cCoENoBKwF/4chA9OSUYGYWTbEiBb8byKvAa+mXKR55jw" +
                "9sJfqOl+erik+wrMZsjhQwx+qktsFtuJOt9Zbid77By2yThSABaAEE5OaXWV5gLSarq5nv+6/jQnVcRJ" +
                "OKmQgwiwYXxegDO12jpqnD2ynCr2oN1zEf6uKoDKm/l2psUz8gLuKGe1y8OoN3n1mymaUHGwW6HF7moC" +
                "NJOI5p5ul4ub+ZzowtjAjWcjJilP+xLKVemgdoX9skm4GCI4KRnyUW/pAKDQYPcMAL6L22VrXg7TNTqM" +
                "eluXcDXY69JVsrw/SccZo5PQ3vFaipI7e5rSHgvoXRZmWnXLIf7XFWf/cf672UoiEQxGwx+jUq+g+E69" +
                "oOO7zm3nq1EjXn7N0bxxpIs4JbgGqMSF6y+7wFqyGFiv/lLjDzq27MTUBQAA";
                
    }
}
