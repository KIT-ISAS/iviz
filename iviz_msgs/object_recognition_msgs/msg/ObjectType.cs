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
            Key = "";
            Db = "";
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
                "H4sIAAAAAAAAA5VSwW7bMAy95yuI5uBL4QBrO2y+NUmHrehWDM1lp0C2GFuLLLoS3TQY9u+jLLvI2lN1" +
                "MfxEvvf4qPn8vQful7c3qw18W8O7e6czm81hRY6VcQGM25FvFRtyoErqGbhB4GOHQDtQsKPeaaDyN1ac" +
                "w6ahILcHgoAcYkWnvGqR0QdgqlGaPfTOPPZoj6BxZxyCcqKYKKL2RgT2eIzdUetUoRiQ1A9Go2OzM8Jo" +
                "3HBRmyd0oMtZYG9cHVkmQl2eWglMHjWoIBPcPtz/WFTUdh5DEPDX9fc7SAQ5XLtRWdRAEwZwxNCqPcqE" +
                "TmY9GG6mVCrywtGR09IrulqxKlXAHG7yOj+PLhPZ1mNFtTMxVYEZKuXAEu3Bmj0WcPYniwlnRbaivmrW" +
                "y+wcMk/EgjTMXbFYWKqUlbQ5+3uWRvSSSLQnNtxTTIacspC2Fz9iMS7nJAUluRrOpMlUsjWSvLCTslhv" +
                "8dmUxho+5mOCb61vZWBMIdbo0JsK1sv0NgaSOJVHpUdl2ZTVIT6uXnJyDPis2s5iKAT7IgbHYeMvDDQF" +
                "vAQwYDECwV5HUFx9/nSZKiqyVlyKOal76zgblR5+3oGsLWBDVr/s6T/hh0f7dapI3IMUZIc6XHxMSEde" +
                "kKvLiw/Dr1T7WGCspcNYoUI4kNevYCcriINMAttkNaTblnRv473YssjUZdODlqc9+wcQ+4upFQQAAA==";
                
    }
}
