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
                "H4sIAAAAAAAAA02QwU7DQAxE7/mKkXptA0iIO6iXnqm4IIScrJMYNuuw3lTq3+OkreCy8kqe55nZ4DlB" +
                "my9uC4bzpGVgE0MZqKDVVEiSISkmNSmiCZI6zSMtc11VGxwHxpzkZ2Yc9tDOpXwDtpHMahwVPRdQCCuC" +
                "4n8IqNG5OAiu9MuH/RY2twPIIMUwzCOlXWYK1ES+IJFo5C2iWOHE2WCDzjFg4rxwQSsuqn7Pk98CYeTi" +
                "gEJYnoaMa7wy4yTmFj5H6+3ubZ0P7qz2PxyEUTMjuFRiXVnJknpIuKWesjbUSJRyhi97W50ETq1jKXod" +
                "1y5c711wuJZS4+W87J44LfG3f8kvqmuUKLw4XwCZUs94v989fNRVF5XK0yOsdW/VLy8kXqm+AQAA";
                
    }
}
