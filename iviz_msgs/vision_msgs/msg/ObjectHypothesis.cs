/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/ObjectHypothesis")]
    public sealed class ObjectHypothesis : IDeserializable<ObjectHypothesis>, IMessage
    {
        // An object hypothesis that contains no position information.
        // The unique ID of the object class. To get additional information about
        //   this ID, such as its human-readable class name, listeners should perform a
        //   lookup in a metadata database. See vision_msgs/VisionInfo.msg for more detail.
        [DataMember (Name = "id")] public string Id { get; set; }
        // The probability or confidence value of the detected object. By convention,
        //   this value should lie in the range [0-1].
        [DataMember (Name = "score")] public double Score { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectHypothesis()
        {
            Id = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectHypothesis(string Id, double Score)
        {
            this.Id = Id;
            this.Score = Score;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectHypothesis(ref Buffer b)
        {
            Id = b.DeserializeString();
            Score = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectHypothesis(ref b);
        }
        
        ObjectHypothesis IDeserializable<ObjectHypothesis>.RosDeserialize(ref Buffer b)
        {
            return new ObjectHypothesis(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Id);
            b.Serialize(Score);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.UTF8.GetByteCount(Id);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/ObjectHypothesis";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6d51bda6d3783ccca113b20d066cc679";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE02QQUsDQQyF74X+hwe9tquCeFd66dniRUSyO9ludHayTmYL/fdm2oq9DBnI+/LeW+E5" +
                "Qdsv7gqG06RlYBNDGaig01RIkiEpJjUpogmSes0j1blZLpaLFfYDY07yMzN2W2jvYv5DdpHMGuwVBy6g" +
                "EM4QircYUKtzqSS41I/vtmvY3A0ggxTDMI+UNpkpUBv5wkSikdeIYoUTZ4MNOseAiXMFgy68qPo9T34N" +
                "hJGLEwqhPi0ZN3hlxlHMTXyOdrC7t/O8c2+N/+EkjJoZwaUSPa+VLOkACf/Rp6wttRKlnOD7XlovgVPn" +
                "ZIreybUQR3ghHK7NNHg51d0jp9rB+ib+RXbNE4Wr+0rIlA6M9/vNw4c76aNSeXqEdW5w8QsbEScYxwEA" +
                "AA==";
                
    }
}
