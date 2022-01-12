/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AveragingFeedback : IDeserializable<AveragingFeedback>, IFeedback<AveragingActionFeedback>
    {
        //feedback
    
        /// Constructor for empty message.
        public AveragingFeedback()
        {
        }
        
        /// Constructor with buffer.
        public AveragingFeedback(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public AveragingFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public static readonly AveragingFeedback Singleton = new AveragingFeedback();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = BuiltIns.EmptyMd5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PiAgBrE+NbAgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
