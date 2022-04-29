/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectHypothesis : IDeserializable<ObjectHypothesis>, IMessage
    {
        // An object hypothesis that contains no position information.
        // The unique ID of the object class. To get additional information about
        //   this ID, such as its human-readable class name, listeners should perform a
        //   lookup in a metadata database. See vision_msgs/VisionInfo.msg for more detail.
        [DataMember (Name = "id")] public string Id;
        // The probability or confidence value of the detected object. By convention,
        //   this value should lie in the range [0-1].
        [DataMember (Name = "score")] public double Score;
    
        /// Constructor for empty message.
        public ObjectHypothesis()
        {
            Id = "";
        }
        
        /// Explicit constructor.
        public ObjectHypothesis(string Id, double Score)
        {
            this.Id = Id;
            this.Score = Score;
        }
        
        /// Constructor with buffer.
        public ObjectHypothesis(ref ReadBuffer b)
        {
            b.DeserializeString(out Id);
            b.Deserialize(out Score);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectHypothesis(ref b);
        
        public ObjectHypothesis RosDeserialize(ref ReadBuffer b) => new ObjectHypothesis(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.Serialize(Score);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(Id);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/ObjectHypothesis";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6d51bda6d3783ccca113b20d066cc679";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE02QwU7DQAxE7/mKkXptA0iIO6iXnqm4IIScrJMYNuuw3lTq3+OkreCy8kqe55nZ4DlB" +
                "my9uC4bzpGVgE0MZqKDVVEiSISkmNSmiCZI6zSMtc11VGxwHxpzkZ2Yc9tDOpXwDtpHMahwVPRdQCCuC" +
                "4n8IqNG5OAiu9MuH/RY2twPIIMUwzCOlXWYK1ES+IJFo5C2iWOHE2WCDzjFg4rxwQSsuqn7Pk98CYeTi" +
                "gEJYnoaMa7wy4yTmFj5H6+3ubZ0P7qz2PxyEUTMjuFRiXVnJknpIuKWesjbUSJRyhi97W50ETq1jKXod" +
                "1y5c711wuJZS4+W87J44LfG3f8kvqmuUKLw4XwCZUs94v989fNRVF5XK0yOsdW/VLy8kXqm+AQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
