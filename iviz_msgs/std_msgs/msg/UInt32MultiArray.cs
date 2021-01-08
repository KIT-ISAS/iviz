/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/UInt32MultiArray")]
    public sealed class UInt32MultiArray : IDeserializable<UInt32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public uint[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public UInt32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<uint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public UInt32MultiArray(MultiArrayLayout Layout, uint[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public UInt32MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new UInt32MultiArray(ref b);
        }
        
        UInt32MultiArray IDeserializable<UInt32MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new UInt32MultiArray(ref b);
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
        [Preserve] public const string RosMessageType = "std_msgs/UInt32MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4d6a180abc9be191b96a7eda6c8a233d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1U32vbMBB+D+R/OOKXNkuz/ChlLeQhUNhLB4MOxgihqNY5ViJbQZKbdX/9Ptmy43Z7" +
                "HDMOVu50d9/36U4JfdUsHJM25kDCk8+ZvlTaq7W14vVBvJrKU8HOiR2T5EyVyitTUmbscJCQNGlVcOlF" +
                "bcQrtKYixIsQ76bDwXDwRz7S8ds8CbkjpypTaUyTkRRexF3DQaVKv1xstu1+atzdk1BdrI0LJYeD1T9+" +
                "QOPx8x05L58Kt3Mf35MKanyDeGfykCvVwrIjQTsu2aq08V5JBc0cqAp9hg7cCR2F9SqtENZw9K9HnhLd" +
                "twHIZZmMlWxZUmZNQajNlgrjagjekCrLaHinfpcFUgICdFt3urUuOlpzZIBg1wpfI3kyWea4d2RHIaUq" +
                "d8SaQwMAmA94St8/BlRIUzSPsY5cbiotaf3wff3jkZ6ZTlZ5zyXwEhgU7i0O562SHFKIUrYNAs413avA" +
                "rrc5U7Zmm1D4nY/gQk32k8MlrWpEmz6RDyH8qamymW/H6q1lsR3vYTlskbDmAUAAIqyc0PIqzQVE1nRz" +
                "Pft5/WlGqgjjcVI+Bxvgw0y9AGtqtLEUN0PPhE61BiB/JiTcXayB8pvZdqrFM1ID8yhntcv9qOdz6hdD" +
                "/hWhat9cg4Z5OQakcYC0otvF/GY2I7oojee4M8pKytG+goR1PuheE7hsM877IE5K+nzUc3UYUKpvfoMB" +
                "3/ntovMv+hmjIKOes8u57Bu7jFGgPw/WcsboLXR9uLiC/tacJrTHAuJXRTmp2+cQ/jdVp//3crhve3Q4" +
                "CHQwMFEHjFCzgvo79YI56Nq5G7yoSrgg4zm920kXYXhwR1CFe9nhANvIRroQ2az+VuU3TEwoy/0FAAA=";
                
    }
}
