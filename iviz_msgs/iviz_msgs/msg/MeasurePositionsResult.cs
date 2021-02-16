/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/MeasurePositionsResult")]
    public sealed class MeasurePositionsResult : IDeserializable<MeasurePositionsResult>, IMessage
    {
        [DataMember (Name = "measurements")] public double[] Measurements { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeasurePositionsResult()
        {
            Measurements = System.Array.Empty<double>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeasurePositionsResult(double[] Measurements)
        {
            this.Measurements = Measurements;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeasurePositionsResult(ref Buffer b)
        {
            Measurements = b.DeserializeStructArray<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeasurePositionsResult(ref b);
        }
        
        MeasurePositionsResult IDeserializable<MeasurePositionsResult>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Measurements, 0);
        }
        
        public void RosValidate()
        {
            if (Measurements is null) throw new System.NullReferenceException(nameof(Measurements));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 8 * Measurements.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/MeasurePositionsResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3d9d9157c27a6df7ceb218fbcbbdcf22";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTOJjlXITU0sLi1KzU3NKynm4uICAPm1Be8ZAAAA";
                
    }
}
