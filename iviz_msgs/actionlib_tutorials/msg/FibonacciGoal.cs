/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciGoal : IDeserializable<FibonacciGoal>, IGoal<FibonacciActionGoal>
    {
        //goal definition
        [DataMember (Name = "order")] public int Order;
    
        /// Constructor for empty message.
        public FibonacciGoal()
        {
        }
        
        /// Explicit constructor.
        public FibonacciGoal(int Order)
        {
            this.Order = Order;
        }
        
        /// Constructor with buffer.
        public FibonacciGoal(ref ReadBuffer b)
        {
            b.Deserialize(out Order);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new FibonacciGoal(ref b);
        
        public FibonacciGoal RosDeserialize(ref ReadBuffer b) => new FibonacciGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Order);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_tutorials/FibonacciGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6889063349a00b249bd1661df429d822";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxNlLIL0pJLeICAL/qDR8NAAAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
