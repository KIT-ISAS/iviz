
namespace Iviz.Msgs.diagnostic_msgs
{
    public sealed class DiagnosticArray : IMessage
    {
        // This message is used to send diagnostic information about the state of the robot
        public std_msgs.Header header; //for timestamp
        public DiagnosticStatus[] status; // an array of components being reported on
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "diagnostic_msgs/DiagnosticArray";
    
        public IMessage Create() => new DiagnosticArray();
    
        public int GetLength()
        {
            int size = 4;
            size += header.GetLength();
            for (int i = 0; i < status.Length; i++)
            {
                size += status[i].GetLength();
            }
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticArray()
        {
            header = new std_msgs.Header();
            status = new DiagnosticStatus[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.DeserializeArray(out status, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.SerializeArray(status, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "60810da900de1dd6ddd437c3503511da";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UwWrcMBC9C/YfBnxIUkjSpreFPQSStiFtEzahPZQSZGtii9iSK8m79d/3SVrvbgKF" +
                "HtouXiTZM29m3rxRQfeN9tSx97JmwnbwrChY8mwUKS1rY33QFWnzaF0ng7aGZGmHQKFh8kEGJvuYDs6W" +
                "NogPLBU7avJSwIuCRoAgu15cbAHv4Dn4b98TxOCpIAlg5+QY4Srb9dawCZ5K1qYmx711AalZMxOLv/yb" +
                "iU937+fIRD10vvanuYSZKAhZGiWdAkNBKhkkxXoaXTfsjltecRvz73oklr6GsWd/Ija04qnZsJNtO26J" +
                "RWndYHQVidsSM/nDU4MG6qUDR0MrHeytU9pE80cnO47oeDz/GNhUTFcXc9gYz9UQNBIagVA5lj7SdnVB" +
                "YtAmvD2LDqK4X9tjHLlGa7bB0TwZYrL8s3dQApKRfo4Yr3JxJ8AGO4woytNheveAoz8iBEEK6E3V0CEy" +
                "vx1DA4FENayk07Jsk6gqMADUg+h0cLSHHNOek5HQxAY+I+5i/Ams2eLGmo4b9KyN1fuhBoEw7J1daQXT" +
                "ckwgVauhLWp16aQbRfTKIUXxLnIMI3iljmCV3ttKowGK1jo0wgcX0VM3HrQS/0yQu/HLunw5PVGhz+a3" +
                "sS0onAYTU4VRwlhpozTqH2S7G6xnQ5s0hf+t9V5HcpO0k7vtod849V6UIzR4c714nXdfz5efF2/y/nK5" +
                "vFkuzvLh7v784+XircinPCXFZt1HJDZDF/dRcKVdMU3UmtgD3Aik2FdO98l6k3CAaE93ZeSLAU6T78TF" +
                "b9wzMZNxg9leSxfbmBymM2FCMV+UzcQ1j19kOzCuq1Vc/YvravPyhVD24/0/kUy5zqYan3hEuus447h+" +
                "WlmiDUnfKWt8YEMrzes9DvOXSEjewS84WT0RmpQvDtTzC97AaBY8BgAA";
                
    }
}
