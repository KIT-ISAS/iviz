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
            Id = string.Empty;
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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAACk2QzU7DQAyE75F4h5F6bQNIiDuol56puCCEnKyTuGzWYb2p1LfH6Y/gsvJKns8zs8JL" +
                "gjYHbguG06RlYBNDGaig1VRIkiEpJjUpogmSOs0jLXNdVSvsB8ac5Gdm7LbQzqV8A7aRzGrsFT0XUAhn" +
                "BMX/EFCjc3EQXOmXd9s1bG4HkEGKYZhHSpvMFKiJfEEi0chrRLHCibPBBp1jwMR54YLOuKj6PU9+C4SR" +
                "iwMKYXkaMq7xxoyjmFv4Gq23+/fzvHNntf/hIIyaGcGlEuvKSpbUQ8It9ZS1oUailBN82dvqJHBqHUvR" +
                "67h24XrvgsO1lBqvp2X3yGmJv/5LflFdo0ThxfkCyJR6xsfD5vGzrrqoVJ6fYK17u6t+ASnz7ta/AQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
