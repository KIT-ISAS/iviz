/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int32MultiArray")]
    public sealed class Int32MultiArray : IDeserializable<Int32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public int[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<int>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int32MultiArray(MultiArrayLayout Layout, int[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Int32MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int32MultiArray(ref b);
        }
        
        Int32MultiArray IDeserializable<Int32MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int32MultiArray(ref b);
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
        [Preserve] public const string RosMessageType = "std_msgs/Int32MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1d99f79f8b325b44fee908053e9c945b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9C/QfBuuSuI7rjxCagA+GQC8pFFIoxZiw0Y6stSWt2V3FTX99364+rKQ9" +
                "li4ykmd2Zt57O7MJfS1YWKZC6wMJRy5n+lIXTq2NEa8P4lXXjkq2VuyYJGeqUk7pijJt4ighqdO65MqJ" +
                "YMQjioJKHy98vJ3GURz9kY+K9t2shOyRU5WptE2TkRROtLviSFVuudhsu+1Ywd2vhEKxLs6XjKPVP16g" +
                "8fj5jqyTT6Xd2Y/vSXk1vkG8M3nIlRbCsCVBO67YqLTxXkkFzSyoiuIMHbgTOgrjVFojrOHoXo88Jbrv" +
                "ApDLMGkj2bCkzOiSUJsNldoGCE6TqqrW8E79Pgu0BATotu5161x0NPrIAME2juqgfEDypLPM8uDIjkJK" +
                "Ve2IC/YNAGDO46nc8BhQIU3RPNpYsrmuC0nrh+/rH4/0zHQyyjmugJfAoLRvcVhnlGSfQlSyaxBwDnSv" +
                "PLvB5kyZwDYh/zsfwYWa7CeHS1oFRJshkQ8+/Kmpsplvx+qtZbEd72E5bJEw8AAgABFGTmh5leYCIhd0" +
                "cz37ef1pRqr043FSLgcb4MNMvQBrqgttqN0MPRM6BQ1A/kxI2Lu2BspvZttpIZ6RGphHOatd7kYDn1W/" +
                "GPKvCFWH5gAa5uUYkMYe0opuF/Ob2YzootKO252trKQs7WtIGPJB90Dgsss4H4I4Keny0cDVY0CpofkN" +
                "Brznt4vevxhmbAUZDZx9zuXQ2GdsBfrzYA1njN5C1/uLy+tv9GlCe3xA/LqsJqF9Dv5/U3X6fy+H+65H" +
                "48jTwcC0OmCEmi+ov1MvmIO+nfvBa1XxF2R7Tu920oUfHtwRVONetjjALrKRzkc2X3+r8hu4Xp4s/QUA" +
                "AA==";
                
    }
}
