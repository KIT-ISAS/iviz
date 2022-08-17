/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Float32MultiArray : IDeserializable<Float32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public float[] Data;
    
        public Float32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<float>();
        }
        
        public Float32MultiArray(MultiArrayLayout Layout, float[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public Float32MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        public Float32MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        public Float32MultiArray RosDeserialize(ref ReadBuffer b) => new Float32MultiArray(ref b);
        
        public Float32MultiArray RosDeserialize(ref ReadBuffer2 b) => new Float32MultiArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) BuiltIns.ThrowNullReference();
            Layout.RosValidate();
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 4 * Data.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Layout.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Data length
            c += 4 * Data.Length;
            return c;
        }
    
        public const string MessageType = "std_msgs/Float32MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6a40e0ffa6a17a503ac3f8616991b1f6";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
