/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionGoal")]
    public sealed class ObjectRecognitionGoal : IDeserializable<ObjectRecognitionGoal>, IGoal<ObjectRecognitionActionGoal>
    {
        // Optional ROI to use for the object detection
        [DataMember (Name = "use_roi")] public bool UseRoi { get; set; }
        [DataMember (Name = "filter_limits")] public float[] FilterLimits { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionGoal()
        {
            FilterLimits = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionGoal(bool UseRoi, float[] FilterLimits)
        {
            this.UseRoi = UseRoi;
            this.FilterLimits = FilterLimits;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectRecognitionGoal(ref Buffer b)
        {
            UseRoi = b.Deserialize<bool>();
            FilterLimits = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionGoal(ref b);
        }
        
        ObjectRecognitionGoal IDeserializable<ObjectRecognitionGoal>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(UseRoi);
            b.SerializeStructArray(FilterLimits, 0);
        }
        
        public void RosValidate()
        {
            if (FilterLimits is null) throw new System.NullReferenceException(nameof(FilterLimits));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += 4 * FilterLimits.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "49bea2f03a1bba0ad05926e01e3525fa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA+NKys/PUSgtTo0vys/kSsvJTywxNoqOVUjLzClJLYrPyczNLCnmAgBz2M+rJgAAAA==";
                
    }
}
