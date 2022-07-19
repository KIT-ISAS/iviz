/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    public sealed class Int32MultiArray : IDeserializableRos2<Int32MultiArray>, IMessageRos2
    {
        // This was originally provided as an example message.
        // It is deprecated as of Foxy
        // It is recommended to create your own semantically meaningful message.
        // However if you would like to continue using this please use the equivalent in example_msgs.
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public int[] Data;
    
        /// Constructor for empty message.
        public Int32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public Int32MultiArray(MultiArrayLayout Layout, int[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Int32MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        public Int32MultiArray RosDeserialize(ref ReadBuffer2 b) => new Int32MultiArray(ref b);
    
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
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Layout.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Int32MultiArray";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
