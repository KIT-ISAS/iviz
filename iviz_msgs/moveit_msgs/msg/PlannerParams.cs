/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class PlannerParams : IDeserializable<PlannerParams>, IMessage
    {
        // parameter names (same size as values)
        [DataMember (Name = "keys")] public string[] Keys;
        // parameter values (same size as keys)
        [DataMember (Name = "values")] public string[] Values;
        // parameter description (can be empty)
        [DataMember (Name = "descriptions")] public string[] Descriptions;
    
        /// Constructor for empty message.
        public PlannerParams()
        {
            Keys = System.Array.Empty<string>();
            Values = System.Array.Empty<string>();
            Descriptions = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public PlannerParams(string[] Keys, string[] Values, string[] Descriptions)
        {
            this.Keys = Keys;
            this.Values = Values;
            this.Descriptions = Descriptions;
        }
        
        /// Constructor with buffer.
        public PlannerParams(ref ReadBuffer b)
        {
            b.DeserializeStringArray(out Keys);
            b.DeserializeStringArray(out Values);
            b.DeserializeStringArray(out Descriptions);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PlannerParams(ref b);
        
        public PlannerParams RosDeserialize(ref ReadBuffer b) => new PlannerParams(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Keys);
            b.SerializeArray(Values);
            b.SerializeArray(Descriptions);
        }
        
        public void RosValidate()
        {
            if (Keys is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Keys.Length; i++)
            {
                if (Keys[i] is null) BuiltIns.ThrowNullReference(nameof(Keys), i);
            }
            if (Values is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] is null) BuiltIns.ThrowNullReference(nameof(Values), i);
            }
            if (Descriptions is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Descriptions.Length; i++)
            {
                if (Descriptions[i] is null) BuiltIns.ThrowNullReference(nameof(Descriptions), i);
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.GetArraySize(Keys);
                size += BuiltIns.GetArraySize(Values);
                size += BuiltIns.GetArraySize(Descriptions);
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/PlannerParams";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "cebdf4927996b9026bcf59a160d64145";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE13LMQ6AIBAF0Z5T/IQGz2QsVtwYoiBh0QRPL4YGraaZpxEpkefMCaFWYKQG4m4GCS7a" +
                "T5ZBSU4urOOEjYsopTvWlp97t0615+sWFptczO4IMJYCZgb7mEvnuqfqB1KvtWCtAAAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
