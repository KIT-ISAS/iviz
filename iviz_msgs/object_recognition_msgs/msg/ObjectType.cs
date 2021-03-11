/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectType")]
    public sealed class ObjectType : IDeserializable<ObjectType>, IMessage
    {
        //################################################# OBJECT ID #########################################################
        // Contains information about the type of a found object. Those two sets of parameters together uniquely define an
        // object
        // The key of the found object: the unique identifier in the given db
        [DataMember (Name = "key")] public string Key { get; set; }
        // The db parameters stored as a JSON/compressed YAML string. An object id does not make sense without the corresponding
        // database. E.g., in object_recognition, it can look like: "{'type':'CouchDB', 'root':'http://localhost'}"
        // There is no conventional format for those parameters and it's nice to keep that flexibility.
        // The object_recognition_core as a generic DB type that can read those fields
        // Current examples:
        // For CouchDB:
        //   type: 'CouchDB'
        //   root: 'http://localhost:5984'
        //   collection: 'object_recognition'
        // For SQL household database:
        //   type: 'SqlHousehold'
        //   host: 'wgs36'
        //   port: 5432
        //   user: 'willow'
        //   password: 'willow'
        //   name: 'household_objects'
        //   module: 'tabletop'
        [DataMember (Name = "db")] public string Db { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectType()
        {
            Key = string.Empty;
            Db = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectType(string Key, string Db)
        {
            this.Key = Key;
            this.Db = Db;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectType(ref Buffer b)
        {
            Key = b.DeserializeString();
            Db = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectType(ref b);
        }
        
        ObjectType IDeserializable<ObjectType>.RosDeserialize(ref Buffer b)
        {
            return new ObjectType(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Key);
            b.Serialize(Db);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Key is null) throw new System.NullReferenceException(nameof(Key));
            if (Db is null) throw new System.NullReferenceException(nameof(Db));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Key);
                size += BuiltIns.UTF8.GetByteCount(Db);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectType";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ac757ec5be1998b0167e7efcda79e3cf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACpVSwW7bMAy9B9g/EM3Bl8IF1nZYfWuSDlvRrRiSy06BbDG2Fll0JXppMOzfR1n2kLWn" +
                "6mL4iXzv8VHz+VsPPC7u75Yb+LKCN/dOZzabw5IcK+MCGLcj3yo25ECV1DNwg8DHDoF2oGBHvdNA5U+s" +
                "OIdNQ0FuDwQBOcSKTnnVIqMPwFSjNHvonXnq0R5B4844BOVEMVFE7Y0I7PEYu6PWqUIxIKkfjEbHZmeE" +
                "0bjhoja/0IEuZ4G9cXVkmQh1eWolMHnUoIJMcL9+/HZRUdt5DEHAH7dfHyAR5HDrRmVRA00YwBFDq/Yo" +
                "EzqZ9WC4mVKpyAtHR05Lr+hqxapUAXO4y+v8PLpMZFuPFdXOxFQFZqiUA0u0B2v2WMDZ7ywmnBXZkvqq" +
                "WS2yc8g8EQvSMHfFxYWlSllJm7M/Z2lEL4lEe2LDSQyRW1lI24sfsRiXc5KCklwNZ9JkKtkaSV7YSVms" +
                "t/hsSmMNH/MxwdfWtzIwphBrdOhNBatFehsDSZzKo9KjsmzK6hAfVy85OQZ8Vm1nMRSCfRKD47DxFwaa" +
                "Av4FMGAxAsFeRlBc33y8ShUVWSsuxZzUvXYci6LS+vsDyNoCNmRlr+Oe/hNeP9nPU0XiHqQgO9Th8kNC" +
                "OvKCXF9dvh9+pdrHAmMtHcYKFcKBvH4BO1lBHGQS2CarId22pHsb78WWRaYumx60PO13s7/LC4hgFgQA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
