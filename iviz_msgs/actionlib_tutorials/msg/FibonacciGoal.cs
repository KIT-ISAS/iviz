/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciGoal")]
    public sealed class FibonacciGoal : IDeserializable<FibonacciGoal>, IGoal<FibonacciActionGoal>
    {
        //goal definition
        [DataMember (Name = "order")] public int Order { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciGoal()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciGoal(int Order)
        {
            this.Order = Order;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciGoal(ref Buffer b)
        {
            Order = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciGoal(ref b);
        }
        
        FibonacciGoal IDeserializable<FibonacciGoal>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Order);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6889063349a00b249bd1661df429d822";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PKzCsxNlLIL0pJLeICAL/qDR8NAAAA";
                
    }
}
