/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        internal FibonacciGoal(ref ReadBuffer b)
        {
            Order = b.Deserialize<int>();
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
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6889063349a00b249bd1661df429d822";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PKzCsxNlLIL0pJLeICAL/qDR8NAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
