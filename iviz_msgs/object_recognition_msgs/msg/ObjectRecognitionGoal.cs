/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        internal ObjectRecognitionGoal(ref Buffer b)
        {
            UseRoi = b.Deserialize<bool>();
            FilterLimits = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectRecognitionGoal(ref b);
        
        ObjectRecognitionGoal IDeserializable<ObjectRecognitionGoal>.RosDeserialize(ref Buffer b) => new ObjectRecognitionGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(UseRoi);
            b.SerializeStructArray(FilterLimits, 0);
        }
        
        public void RosValidate()
        {
            if (FilterLimits is null) throw new System.NullReferenceException(nameof(FilterLimits));
        }
    
        public int RosMessageLength => 5 + 4 * FilterLimits.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "49bea2f03a1bba0ad05926e01e3525fa";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+NKys/PUSgtTo0vys/kSsvJTywxNoqOVUjLzClJLYrPyczNLCnmAgBz2M+rJgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
