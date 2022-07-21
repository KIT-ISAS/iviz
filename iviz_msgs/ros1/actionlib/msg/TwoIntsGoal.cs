/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsGoal : IDeserializableCommon<TwoIntsGoal>, IMessageCommon, IGoal<TwoIntsActionGoal>
    {
        [DataMember (Name = "a")] public long A;
        [DataMember (Name = "b")] public long B;
    
        public TwoIntsGoal()
        {
        }
        
        public TwoIntsGoal(long A, long B)
        {
            this.A = A;
            this.B = B;
        }
        
        public TwoIntsGoal(ref ReadBuffer b)
        {
            b.Deserialize(out A);
            b.Deserialize(out B);
        }
        
        public TwoIntsGoal(ref ReadBuffer2 b)
        {
            b.Deserialize(out A);
            b.Deserialize(out B);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TwoIntsGoal(ref b);
        
        public TwoIntsGoal RosDeserialize(ref ReadBuffer b) => new TwoIntsGoal(ref b);
        
        public TwoIntsGoal RosDeserialize(ref ReadBuffer2 b) => new TwoIntsGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(A);
            b.Serialize(B);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(A);
            b.Serialize(B);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 16;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, A);
            WriteBuffer2.AddLength(ref c, B);
        }
    
        public const string MessageType = "actionlib/TwoIntsGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "36d09b846be0b371c5f190354dd3153e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxM1FI5ILQSVwAD962hBEAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
