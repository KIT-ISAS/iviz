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
                "H4sIAAAAAAAAE5VSTW/bMAy9B8h/IJqDL4UDrO2w+tYkHbaiWzEkl50C2WJsLbLoSnTTYNh/H+WPrGtP" +
                "1cXwE/ne46Nms/ceeFjc3S438HUF7+4dz3QyncxgSY6VcQGM25GvFRtyoHJqGbhC4GODQDtQsKPWaaD8" +
                "FxacwqaiILcHgoAcYkWjvKqR0QdgKlGaPbTOPLZoj6BxZxyCclGy5+jlN6Kxx2MkiHIvRbIO6SnAaHRs" +
                "dkZIjesuSvOEDnQ+nQT2xpWR5h+nzl8aCkweNaggc9ytH77PC6objyEI+PPm2z30FCncuEFcBEETBnDE" +
                "UKs9ypxOJj4YrsZsCvLC0ZDT0huFtWKVq4Ap3KZleh6d9mxbjwWVzsRwBWYolANLtAdr9pjB2e8kBp1k" +
                "yZLaolotknNIPBELUjE32XxuqVBWQufkz9kwpJdYokEx4p5iPOSUhX6L8SMm45Je5KAkXMOJNJlCtkeS" +
                "GTZSFustPpvcWMPHdAzxrfmtzIx9jiU69KaA1aJ/JB1LnMuj0oO07Mvq0D2zVrJyDPis6sZiyCL4WTwO" +
                "A3f/0DFlcEqhB2MQAr4OIru6/nQ5lBRkrVgVh1L41nYyqq1/3IPsL2BFVp/29b/4+tF+GUsG+k4OkkMZ" +
                "Lj4OUENeoKvLiw/9vzT4WGKspcNYo0I4kNevcSf7iAONKtvecRiua9KtjQXiziJTk5zeeHzuk78xccTZ" +
                "LwQAAA==";
                
    }
}
