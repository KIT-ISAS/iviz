/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlanningSceneWorld : IDeserializable<PlanningSceneWorld>, IMessage
    {
        // collision objects
        [DataMember (Name = "collision_objects")] public CollisionObject[] CollisionObjects;
        // The octomap that represents additional collision data
        [DataMember (Name = "octomap")] public OctomapMsgs.OctomapWithPose Octomap;
    
        /// Constructor for empty message.
        public PlanningSceneWorld()
        {
            CollisionObjects = System.Array.Empty<CollisionObject>();
            Octomap = new OctomapMsgs.OctomapWithPose();
        }
        
        /// Explicit constructor.
        public PlanningSceneWorld(CollisionObject[] CollisionObjects, OctomapMsgs.OctomapWithPose Octomap)
        {
            this.CollisionObjects = CollisionObjects;
            this.Octomap = Octomap;
        }
        
        /// Constructor with buffer.
        internal PlanningSceneWorld(ref Buffer b)
        {
            CollisionObjects = b.DeserializeArray<CollisionObject>();
            for (int i = 0; i < CollisionObjects.Length; i++)
            {
                CollisionObjects[i] = new CollisionObject(ref b);
            }
            Octomap = new OctomapMsgs.OctomapWithPose(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlanningSceneWorld(ref b);
        
        PlanningSceneWorld IDeserializable<PlanningSceneWorld>.RosDeserialize(ref Buffer b) => new PlanningSceneWorld(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(CollisionObjects);
            Octomap.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (CollisionObjects is null) throw new System.NullReferenceException(nameof(CollisionObjects));
            for (int i = 0; i < CollisionObjects.Length; i++)
            {
                if (CollisionObjects[i] is null) throw new System.NullReferenceException($"{nameof(CollisionObjects)}[{i}]");
                CollisionObjects[i].RosValidate();
            }
            if (Octomap is null) throw new System.NullReferenceException(nameof(Octomap));
            Octomap.RosValidate();
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(CollisionObjects) + Octomap.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningSceneWorld";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "373d88390d1db385335639f687723ee6";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1ZbVMbORL+Pr9CtXyw2TgmG9itPV/xAbCzsBUCC9xdEirlkmdkW4dGmmg0GOfq/vs9" +
                "LY1mxsTZvb06cKhgzbT6Td1Pt5odlhqlZCmNZmb2T5G6MjmJTy78g9tPLc000iQ77GYpmEmdyXnB3JI7" +
                "ZkVhRSm0KxnPMulAz1WHf8YdT+od07xclHsXYfEP6ZaXpmzYJcnh//mTnF//MmK5uRfSBdGPjIQ9R2wp" +
                "eCbsgFWlyNjcWCa1ExZGOakXsFGwAlqWyamnq8mjK2TGzNwTBR+xvua5CLykZueQfeZ2k9JZYiazxoWB" +
                "2q0L8NCMezfNOHljzu60WbUHE35PrUjNQnv/1m70z2+IA7GJnFvPL4TJBQQLnExZmlRyB61WcHtH4WHY" +
                "Jm2wknErAgkOtfAqGk9NCzmXYNA64Noo2B/FpKywMoeC93BWueSFCHp6qsv4CnHVIav3rgMlRUP3/TQ4" +
                "HpLORbnc5EpPQJuHF1v50LuWxbGpdEaHUCiuYWhffK44eZPJsrVuwGaVC4dOZCzlms3gK3gRUZHhYIkF" +
                "HnZinUTsdnW7pK1kiJf0DSPpXavdUZaV3TCqvU5UmiSWqdBiyM42Yg0xei9NVao1Ew+yhH4DJh3Zg6RU" +
                "PBXZMJmtnWBH4/HhKxJzJSgbNiTNrcn9Wuh7aY3OYSi+O2kF+PbFvbBrtwypgHTPuUvh8UcxIbPdIOlq" +
                "cn7x98nhD96mohA6I1Pgr2gX8eDKIoZqpUuK/z+2NTMQqo0Lm6KdOIXWyMvLybvx4WsSjb2tzO3ivJQB" +
                "02JVR3591ARlrE8U8dyQE5avS5ZXpSMKJeZQIy/cehei5JyVRpGv4NqIGLVkaJiJEp7MgoreN/uk4EUh" +
                "bIg+uAc8sQT05JHQxNdPBYqly0I4BlSjZHZcZ9xmSBvHCY08Fi7lYinsS4UwUNjE8wKn7d8S5pQBPWAm" +
                "fhbwp+UKjvDoB7tSk+eVlilwhzmJdOzuJ9cR8BXcOplWilvQG4sUJfK5BYwSd/yUyFShU8HOxiPQ6FKk" +
                "FcEDJEmd4mx9Tp6NWVIBuvdf04Zk52ZlXhKSL4DZjfAQf1BWPFDVIj15OYKM74NxQ/CGcwC1GvnY98+m" +
                "WJa7DEKggihMumR9aH6JrKDjw2nfcyv5TAHJKYqUAtcebertdjiT2iOmuTaRfeDYyvhv2OqGL9n0cokz" +
                "Uz5nqgUcCMLCIgqRFGy29kxSRcjFlJxZbtcJ7Qoik5035GMQUZTSiciv6kSsW/40plS9niYa/7jEJTt/" +
                "+sMujn+dnNxQYPz5zfWHUvXEaMelJuCgFA1Zy2emrhO+hKNoU76gwsSyirSg3satDMIJqAMKRDrciN4C" +
                "6GkWApstQ3oguBHJmZhL1BuuITGwiAX9Tqxjj9GVMPJPwn7gLyE2oJi6F/9igQRB8zWLRwgukWE266pS" +
                "OgN8wsnDgl+vL97tIWtjbnw4On/LAoMhO2oAFUW/geOc33nMhK0UMdErqbHUQCDOsRdyY3szZJPhYgj4" +
                "1lsO3aM6wbAy5g4heydG7Lt/9cjDvVHvxFTpcnzcG7CeNcbhydK5YrS3pwyyA952vX9/F0xED4Ng1oRA" +
                "Gm6o63Q4PQ9rUBQKd7yAPILwHjZJ5CSw606IGi3mCoVjJpV067pZ2havMFgEJ3ocRDc0Pg6x4ZmQVVSF" +
                "ask4KZWVFFwV/EQF9wEpqYTHojdQsDaWlsyzGbHGAf4ZuQDPHrtg9ONffj4IFNQIQksoB7qvNe7Vkq5/" +
                "e8twbKVYGpU157Qh+PqzOo0UgbcXxXqrRbn/U3hSGIsnPx7sv/ZLUFsikEqZVU0BcFkB4B89pn6ZDIkC" +
                "4mUjvM1NVil6D7WUcKboxYBGaD9VbfxW7wqNxiFNZ+ZhgPaHIm3A0jUg2F8hEG6CKtaRUoFL6KdDbvuS" +
                "uOT3FBHUb89iQwpm1H5Qg+kz0fosfzXAv2Hia9rP7PjiPZqq8P368nRyNUGjE5YnH96evRtPrtBY1A8u" +
                "3k0OD2K2R3zyPQ/pVFOFO0OEBJQFTZeGcpN0rgx3Px2gX20p4p5ccN+udTd0yEZMcJRKqtfoLFzthNA4" +
                "krseIlL12j290GoldWjSWxjuVR341fsB+zDwyfqxqzM5mV4roRe4utQaPcagEijZ2AenD1vfTt+jP25X" +
                "Hxpf0+oj9ZQdlYL/a62MBnjTsRNs4jcsBfRQBxT0BKIRFAe7Lc9kRSpQ/fYNNEVQ1CPwnV4djc/+dk39" +
                "ekdmPGTPkw449F/BKyF0AJhQA8KbywpXxhtONB8ZR/s7ZAEegYFig+/0dHL2y+kN6xPverHb2kRFad71" +
                "eGsTYnmxdI3P61xgfcqF3SCPcC7KCdbVcsKiI+dbUsCh8V04vvqqvF0mSjaV0qx5hf3trfNxTqL9TqX1" +
                "LSiVbepVizaGvE9pv8EhUbxXxSB4lr2onRqT9JEzm5B6ZDzdjtpM/Yq4dQwRPg3EfX0l9aOQZpYTmhzq" +
                "WahaIVHnVlC44k45CKdF7UF476uJ93YIPL93yJJLclhDkPyG27aw2vNt6Z7LQKgSrypp7Ofq0U7Q33dw" +
                "/ow3zY0QyB6ab+vm25fnUb91XbShO3Tb8Oem8rT63PqdGiAE6+9bFL+tnqG80hAnFtXOMdDghsAPc6wA" +
                "LLgH6QW6o792qsc9VxVyGVhGFzzTnKS/4GNogUulKG8/JSTjpmaAOtbwIgHEjaeuQnMYd8QpB7raihpA" +
                "EbTZEk9gFjc9k6uiGVtcFs1CB9soFW7Dt/tBT/EwheOeSVs/AfPzpseAgiSjdzRubcarftLWjOP4A3tB" +
                "t9cXLP2C/zJ2yPzsirPRIZJXzG9ffcJy1ix/oGXaLF/TMmuW+5+aiL89oKG2mD/ZNfZ3ptweX/fHiKyC" +
                "AnSGMQcmS+FKgnpSMtAjb7dPmT0Kxzmzp9s6ow2bwm2dbRk7ElAspI5c68ivGYJq65j+qcfz22T+z856" +
                "ozhiyqC6424KC+PWvm/TCBj3TJpWBWaWu/AGm1fo1P0TrlP0ccEV/eHM7Q1xuZ1LhcnuzBhVM/LzAIWL" +
                "TGfwX9cTqodh++a0/0pgPIh5FSKb5js55NYNc04zHUK4sE0btKcN/OJo623EpDYDlyrJlfzSZFPY6v92" +
                "Ee661jdOQ0z4TbhvUl1eWUkzRU9bJtRlUENPf5dJ/gOXyPb6ChoAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
