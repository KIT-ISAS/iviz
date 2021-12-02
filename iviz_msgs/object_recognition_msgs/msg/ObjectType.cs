/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectType : IDeserializable<ObjectType>, IMessage
    {
        //################################################# OBJECT ID #########################################################
        // Contains information about the type of a found object. Those two sets of parameters together uniquely define an
        // object
        // The key of the found object: the unique identifier in the given db
        [DataMember (Name = "key")] public string Key;
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
        [DataMember (Name = "db")] public string Db;
    
        /// Constructor for empty message.
        public ObjectType()
        {
            Key = string.Empty;
            Db = string.Empty;
        }
        
        /// Explicit constructor.
        public ObjectType(string Key, string Db)
        {
            this.Key = Key;
            this.Db = Db;
        }
        
        /// Constructor with buffer.
        internal ObjectType(ref Buffer b)
        {
            Key = b.DeserializeString();
            Db = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectType(ref b);
        
        ObjectType IDeserializable<ObjectType>.RosDeserialize(ref Buffer b) => new ObjectType(ref b);
    
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
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Key) + BuiltIns.GetStringSize(Db);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectType";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ac757ec5be1998b0167e7efcda79e3cf";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACpVSwW7bMAy95yuI5uBL4QBrO2y+NUmHrehWDMllp0C2GFuLLLoSvTQY9u+jLHvI2lN1" +
                "MfxEvvf4qPn8rQcel/d3qy18WcObe6czm81hRY6VcQGM25NvFRtyoErqGbhB4FOHQHtQsKfeaaDyJ1ac" +
                "w7ahILdHgoAcYkWnvGqR0QdgqlGaPfTOPPVoT6BxbxyCcqKYKKL2VgQOeIrdUetcoRiQ1A9Go2OzN8Jo" +
                "3HBRm1/oQJezwN64OrJMhLo8txKYPGpQQSa43zx+W1TUdh5DEPDH7dcHSAQ53LpRWdRAEwZwxNCqA8qE" +
                "TmY9Gm6mVCrywtGR09IrulqxKlXAHO7yOr+MLhPZzmNFtTMxVYEZKuXAEh3AmgMWcPE7iwlnRbaivmrW" +
                "y+wSMk/EgjTMXbFYWKqUlbQ5+3ORRvSSSLQnNpzEELmVhbS9+BGLcTlnKSjJ1XAmTaaSrZHkhZ2UxXqL" +
                "z6Y01vApHxN8bX0nA2MKsUaH3lSwXqa3MZDEqTwqPSrLpqwO8XH1kpNjwGfVdhZDIdgnMTgOG39hoCng" +
                "XwAD5iUCwV5GUNx8/HCdKiqyVlyKOal77TgWRaXN9weQtQVsyMpexz39J7x5sp+nisQ9SEF2rMPV+4R0" +
                "5AW5ub56N/xKtY8Fxlo6jhUqhCN5/QJ2soI4yCSwS1ZDum1J9zbeiy2LTF02PWh52rO/EPuLqRUEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
