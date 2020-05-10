using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class Float32MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout { get; set; } // specification of data layout
        public float[] data { get; set; } // array of data
        
    
        /// <summary> Constructor for empty message. </summary>
        public Float32MultiArray()
        {
            layout = new MultiArrayLayout();
            data = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Float32MultiArray(MultiArrayLayout layout, float[] data)
        {
            this.layout = layout ?? throw new System.ArgumentNullException(nameof(layout));
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Float32MultiArray(Buffer b)
        {
            this.layout = new MultiArrayLayout(b);
            this.data = b.DeserializeStructArray<float>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Float32MultiArray(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.layout.Serialize(b);
            b.SerializeStructArray(this.data, 0);
        }
        
        public void Validate()
        {
            if (layout is null) throw new System.NullReferenceException();
            if (data is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += layout.RosMessageLength;
                size += 4 * data.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "std_msgs/Float32MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "6a40e0ffa6a17a503ac3f8616991b1f6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
