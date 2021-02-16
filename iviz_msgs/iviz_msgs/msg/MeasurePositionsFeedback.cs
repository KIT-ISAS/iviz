/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/MeasurePositionsFeedback")]
    public sealed class MeasurePositionsFeedback : IDeserializable<MeasurePositionsFeedback>, IMessage
    {
        [DataMember (Name = "progress")] public float Progress { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeasurePositionsFeedback()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeasurePositionsFeedback(float Progress)
        {
            this.Progress = Progress;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeasurePositionsFeedback(ref Buffer b)
        {
            Progress = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeasurePositionsFeedback(ref b);
        }
        
        MeasurePositionsFeedback IDeserializable<MeasurePositionsFeedback>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Progress);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/MeasurePositionsFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "70805092fd20e110765c7b79e8efb737";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSKCjKTy9KLS7m4uICAK5iVncTAAAA";
                
    }
}
