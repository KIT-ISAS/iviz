/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/ByteMultiArray")]
    public sealed class ByteMultiArray : IDeserializable<ByteMultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public byte[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public ByteMultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ByteMultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ByteMultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ByteMultiArray(ref b);
        }
        
        ByteMultiArray IDeserializable<ByteMultiArray>.RosDeserialize(ref Buffer b)
        {
            return new ByteMultiArray(ref b);
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
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/ByteMultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "70ea476cbcfd65ac2f68f3cda1e891fe";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9G/QfBuuSuI7rjxCagA+GQC8pFFIIxZiw0Y6stSWt2V3FdX99364+rCQ9" +
                "li4ykmd2Zt57O7Mxfc9ZWKZc6z0JRy5j+lblTq2MEacHcdKVo4KtFVsmyakqlVO6pFSbaBCT1ElVcOlE" +
                "MOIReU6Fjxc+3k6iQTT4kI/y5l2vmOyBE5WqpEmTkhRONLuiwcvJ8XrT7vYruLsVUyjWxvmS0WD5jxdo" +
                "PH69I+vkc2G39vN7Ul6NHxDvTB5yJbkwbEnQlks2Kqm9V1JBMwuqIj9DB+6YDsI4lVQIqzm604EnRPdt" +
                "AHIZJm0kG5aUGl0QarOhQtsAwWlSZdkY3qnfZYGYgADdVp1urYsORh8YINhGg0qVbjEPSJ51mlruHdlB" +
                "SKnKLXHOvgEAzHk8pesfAyokCZpHG0s201UuafXwtPr5SC9MR6Oc4xJ4CQwK+xaHdUZJ9ilEKdsGAedA" +
                "98qz621OlQlsY/K/8xFcqPFuvL+kZUC07hP55MOf6yrr2Wak3lrmm9EOlv0GCQMPAAIQYeSYFldJJiBy" +
                "TjfX01/XX6akCj8eR+UysAE+zNQrsCY614aazdAzpmPQAOTPhIS9a2qg/Hq6meTiBamBeZix2mZu2PNZ" +
                "9Zsh/5JQtW8OoGFejABp5CEt6XY+u5lOiS5K7bjZ2chKytKugoQhH3QPBC7bjLM+iKOSLhv2XB0GlOqb" +
                "32DAe3Y77/zzfsZGkGHP2eVc9I1dxkagjwdrOGX0FrreX1xef6OPY9rhA+JXRTkO7bP3/+uqk/97Ody3" +
                "PRoNPB0MTKMDRqj+gvpb9Yo56Nq5G7xGFX9BNuf0bidd+OHBHUEV7mWLA2wja+l8ZP31typ/AJbAv139" +
                "BQAA";
                
    }
}
