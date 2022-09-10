/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingFeedback : IHasSerializer<AveragingFeedback>, IMessage, IFeedback<AveragingActionFeedback>
    {
        //feedback
    
        public AveragingFeedback()
        {
        }
        
        public AveragingFeedback(ref ReadBuffer b)
        {
        }
        
        public AveragingFeedback(ref ReadBuffer2 b)
        {
        }
        
        public AveragingFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public AveragingFeedback RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static AveragingFeedback? singleton;
        public static AveragingFeedback Singleton => singleton ??= new AveragingFeedback();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public const string MessageType = "actionlib_tutorials/AveragingFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PiAgBrE+NbAgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<AveragingFeedback> CreateSerializer() => new Serializer();
        public Deserializer<AveragingFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<AveragingFeedback>
        {
        }
    
        sealed class Deserializer : Deserializer<AveragingFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer _, out AveragingFeedback msg) => msg = Singleton;
            public override void RosDeserialize(ref ReadBuffer2 _, out AveragingFeedback msg) => msg = Singleton;
        }
    }
}
