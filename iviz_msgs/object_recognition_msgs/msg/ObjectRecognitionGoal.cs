/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [DataContract]
    public sealed class ObjectRecognitionGoal : IDeserializable<ObjectRecognitionGoal>, IGoal<ObjectRecognitionActionGoal>
    {
        // Optional ROI to use for the object detection
        [DataMember (Name = "use_roi")] public bool UseRoi;
        [DataMember (Name = "filter_limits")] public float[] FilterLimits;
    
        /// Constructor for empty message.
        public ObjectRecognitionGoal()
        {
            FilterLimits = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionGoal(bool UseRoi, float[] FilterLimits)
        {
            this.UseRoi = UseRoi;
            this.FilterLimits = FilterLimits;
        }
        
        /// Constructor with buffer.
        public ObjectRecognitionGoal(ref ReadBuffer b)
        {
            b.Deserialize(out UseRoi);
            b.DeserializeStructArray(out FilterLimits);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectRecognitionGoal(ref b);
        
        public ObjectRecognitionGoal RosDeserialize(ref ReadBuffer b) => new ObjectRecognitionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(UseRoi);
            b.SerializeStructArray(FilterLimits);
        }
        
        public void RosValidate()
        {
            if (FilterLimits is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 5 + 4 * FilterLimits.Length;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "object_recognition_msgs/ObjectRecognitionGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "49bea2f03a1bba0ad05926e01e3525fa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+NKys/PUSgtTo0vys/kSsvJTywxNoqOVUjLzClJLYrPyczNLCnmAgBz2M+rJgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
