/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    public sealed class MultiArrayLayout : IDeserializableRos2<MultiArrayLayout>, IMessageRos2
    {
        // This was originally provided as an example message.
        // It is deprecated as of Foxy
        // It is recommended to create your own semantically meaningful message.
        // However if you would like to continue using this please use the equivalent in example_msgs.
        // The multiarray declares a generic multi-dimensional array of a
        // particular data type.  Dimensions are ordered from outer most
        // to inner most.
        //
        // Accessors should ALWAYS be written in terms of dimension stride
        // and specified outer-most dimension first.
        //
        // multiarray(i,j,k) = data[data_offset + dim_stride[1]*i + dim_stride[2]*j + k]
        //
        // A standard, 3-channel 640x480 image with interleaved color channels
        // would be specified as:
        //
        // dim[0].label  = "height"
        // dim[0].size   = 480
        // dim[0].stride = 3*640*480 = 921600  (note dim[0] stride is just size of image)
        // dim[1].label  = "width"
        // dim[1].size   = 640
        // dim[1].stride = 3*640 = 1920
        // dim[2].label  = "channel"
        // dim[2].size   = 3
        // dim[2].stride = 3
        //
        // multiarray(i,j,k) refers to the ith row, jth column, and kth channel.
        /// <summary> Array of dimension properties </summary>
        [DataMember (Name = "dim")] public MultiArrayDimension[] Dim;
        /// <summary> Padding bytes at front of data </summary>
        [DataMember (Name = "data_offset")] public uint DataOffset;
    
        /// Constructor for empty message.
        public MultiArrayLayout()
        {
            Dim = System.Array.Empty<MultiArrayDimension>();
        }
        
        /// Explicit constructor.
        public MultiArrayLayout(MultiArrayDimension[] Dim, uint DataOffset)
        {
            this.Dim = Dim;
            this.DataOffset = DataOffset;
        }
        
        /// Constructor with buffer.
        public MultiArrayLayout(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Dim);
            for (int i = 0; i < Dim.Length; i++)
            {
                Dim[i] = new MultiArrayDimension(ref b);
            }
            b.Deserialize(out DataOffset);
        }
        
        public MultiArrayLayout RosDeserialize(ref ReadBuffer2 b) => new MultiArrayLayout(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Dim);
            b.Serialize(DataOffset);
        }
        
        public void RosValidate()
        {
            if (Dim is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Dim.Length; i++)
            {
                if (Dim[i] is null) BuiltIns.ThrowNullReference(nameof(Dim), i);
                Dim[i].RosValidate();
            }
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Dim);
            WriteBuffer2.AddLength(ref c, DataOffset);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/MultiArrayLayout";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
