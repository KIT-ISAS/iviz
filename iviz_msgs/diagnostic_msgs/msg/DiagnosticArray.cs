/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DiagnosticMsgs
{
    [Preserve, DataContract (Name = "diagnostic_msgs/DiagnosticArray")]
    public sealed class DiagnosticArray : IDeserializable<DiagnosticArray>, IMessage
    {
        // This message is used to send diagnostic information about the state of the robot
        [DataMember (Name = "header")] public StdMsgs.Header Header; //for timestamp
        [DataMember (Name = "status")] public DiagnosticStatus[] Status; // an array of components being reported on
    
        /// <summary> Constructor for empty message. </summary>
        public DiagnosticArray()
        {
            Status = System.Array.Empty<DiagnosticStatus>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DiagnosticArray(in StdMsgs.Header Header, DiagnosticStatus[] Status)
        {
            this.Header = Header;
            this.Status = Status;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DiagnosticArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Status = b.DeserializeArray<DiagnosticStatus>();
            for (int i = 0; i < Status.Length; i++)
            {
                Status[i] = new DiagnosticStatus(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DiagnosticArray(ref b);
        }
        
        DiagnosticArray IDeserializable<DiagnosticArray>.RosDeserialize(ref Buffer b)
        {
            return new DiagnosticArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Status, 0);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            for (int i = 0; i < Status.Length; i++)
            {
                if (Status[i] is null) throw new System.NullReferenceException($"{nameof(Status)}[{i}]");
                Status[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Status);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "diagnostic_msgs/DiagnosticArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "60810da900de1dd6ddd437c3503511da";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
