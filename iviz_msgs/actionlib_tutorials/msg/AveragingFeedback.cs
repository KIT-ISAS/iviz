/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/AveragingFeedback")]
    public sealed class AveragingFeedback : IDeserializable<AveragingFeedback>, IFeedback<AveragingActionFeedback>
    {
        //feedback
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingFeedback()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AveragingFeedback(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        AveragingFeedback IDeserializable<AveragingFeedback>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly AveragingFeedback Singleton = new AveragingFeedback();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PiAgBrE+NbAgAAAA==";
                
    }
}
