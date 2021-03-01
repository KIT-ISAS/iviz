/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlannerParams")]
    public sealed class PlannerParams : IDeserializable<PlannerParams>, IMessage
    {
        // parameter names (same size as values)
        [DataMember (Name = "keys")] public string[] Keys { get; set; }
        // parameter values (same size as keys)
        [DataMember (Name = "values")] public string[] Values { get; set; }
        // parameter description (can be empty)
        [DataMember (Name = "descriptions")] public string[] Descriptions { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlannerParams()
        {
            Keys = System.Array.Empty<string>();
            Values = System.Array.Empty<string>();
            Descriptions = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlannerParams(string[] Keys, string[] Values, string[] Descriptions)
        {
            this.Keys = Keys;
            this.Values = Values;
            this.Descriptions = Descriptions;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlannerParams(ref Buffer b)
        {
            Keys = b.DeserializeStringArray();
            Values = b.DeserializeStringArray();
            Descriptions = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlannerParams(ref b);
        }
        
        PlannerParams IDeserializable<PlannerParams>.RosDeserialize(ref Buffer b)
        {
            return new PlannerParams(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Keys, 0);
            b.SerializeArray(Values, 0);
            b.SerializeArray(Descriptions, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Keys is null) throw new System.NullReferenceException(nameof(Keys));
            for (int i = 0; i < Keys.Length; i++)
            {
                if (Keys[i] is null) throw new System.NullReferenceException($"{nameof(Keys)}[{i}]");
            }
            if (Values is null) throw new System.NullReferenceException(nameof(Values));
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] is null) throw new System.NullReferenceException($"{nameof(Values)}[{i}]");
            }
            if (Descriptions is null) throw new System.NullReferenceException(nameof(Descriptions));
            for (int i = 0; i < Descriptions.Length; i++)
            {
                if (Descriptions[i] is null) throw new System.NullReferenceException($"{nameof(Descriptions)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += 4 * Keys.Length;
                foreach (string s in Keys)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 4 * Values.Length;
                foreach (string s in Values)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 4 * Descriptions.Length;
                foreach (string s in Descriptions)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlannerParams";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cebdf4927996b9026bcf59a160d64145";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE13LMQ6AIBAF0Z5T/IQGz2QsVtwYoiBh0QRPL4YGraaZpxEpkefMCaFWYKQG4m4GCS7a" +
                "T5ZBSU4urOOEjYsopTvWlp97t0615+sWFptczO4IMJYCZgb7mEvnuqfqB1KvtWCtAAAA";
                
    }
}
