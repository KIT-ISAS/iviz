/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/AttachedCollisionObject")]
    public sealed class AttachedCollisionObject : IDeserializable<AttachedCollisionObject>, IMessage
    {
        // The CollisionObject will be attached with a fixed joint to this link
        [DataMember (Name = "link_name")] public string LinkName;
        //This contains the actual shapes and poses for the CollisionObject
        //to be attached to the link
        //If action is remove and no object.id is set, all objects
        //attached to the link indicated by link_name will be removed
        [DataMember (Name = "object")] public CollisionObject Object;
        // The set of links that the attached objects are allowed to touch
        // by default - the link_name is already considered by default
        [DataMember (Name = "touch_links")] public string[] TouchLinks;
        // If certain links were placed in a particular posture for this object to remain attached 
        // (e.g., an end effector closing around an object), the posture necessary for releasing
        // the object is stored here
        [DataMember (Name = "detach_posture")] public TrajectoryMsgs.JointTrajectory DetachPosture;
        // The weight of the attached object, if known
        [DataMember (Name = "weight")] public double Weight;
    
        /// <summary> Constructor for empty message. </summary>
        public AttachedCollisionObject()
        {
            LinkName = string.Empty;
            Object = new CollisionObject();
            TouchLinks = System.Array.Empty<string>();
            DetachPosture = new TrajectoryMsgs.JointTrajectory();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AttachedCollisionObject(string LinkName, CollisionObject Object, string[] TouchLinks, TrajectoryMsgs.JointTrajectory DetachPosture, double Weight)
        {
            this.LinkName = LinkName;
            this.Object = Object;
            this.TouchLinks = TouchLinks;
            this.DetachPosture = DetachPosture;
            this.Weight = Weight;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AttachedCollisionObject(ref Buffer b)
        {
            LinkName = b.DeserializeString();
            Object = new CollisionObject(ref b);
            TouchLinks = b.DeserializeStringArray();
            DetachPosture = new TrajectoryMsgs.JointTrajectory(ref b);
            Weight = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AttachedCollisionObject(ref b);
        }
        
        AttachedCollisionObject IDeserializable<AttachedCollisionObject>.RosDeserialize(ref Buffer b)
        {
            return new AttachedCollisionObject(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(LinkName);
            Object.RosSerialize(ref b);
            b.SerializeArray(TouchLinks, 0);
            DetachPosture.RosSerialize(ref b);
            b.Serialize(Weight);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
            if (Object is null) throw new System.NullReferenceException(nameof(Object));
            Object.RosValidate();
            if (TouchLinks is null) throw new System.NullReferenceException(nameof(TouchLinks));
            for (int i = 0; i < TouchLinks.Length; i++)
            {
                if (TouchLinks[i] is null) throw new System.NullReferenceException($"{nameof(TouchLinks)}[{i}]");
            }
            if (DetachPosture is null) throw new System.NullReferenceException(nameof(DetachPosture));
            DetachPosture.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.GetStringSize(LinkName);
                size += Object.RosMessageLength;
                size += BuiltIns.GetArraySize(TouchLinks);
                size += DetachPosture.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/AttachedCollisionObject";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3ceac60b21e85bbd6c5b0ab9d476b752";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVZW08bSRZ+b4n/UBoeDBvHyYTMaNYrHgh4JkRJYIBdJUHIKneX7R7aXU5XNcazmv8+" +
                "3zmnqrsxZLMjbViCgqvq1Llfy9vqYm7UoS2K3OW2PJn8ZlKvVnlRqIlR2nudzk2GDT9XWk3zWyx+s3np" +
                "lbfKz3Oniry8Tpyv8nLGn8elXpgk2b6gw9SWXuelAyiwpb7WhXJzvTRO6TJTS+vwaWorPt/gItkGiS4T" +
                "TNEIwe3jKeEDsAKZyizsjWGUpVWWrw/yjI6c8X2lIY7sumT7IXwqL7M81R67k3UrRqMIIZAlm4oSpJCW" +
                "1Qhayk75OkmsvYgd6QUOlK6wWRR2FXiwdToHBhDOzFTXhVdPG8aEDcihi8robE0adXlmKuE0XAj6v7wS" +
                "ZGPmgLiCllJTkQkCVyvcVMtCp7iPTa2WuvJ5Whe6Imv4GsdiD9AUholHyE84GlGAescMZgOotlQGajfT" +
                "KUBxMS2sI1fQla2xj2PBsttnmSKN0qTGOV2tmVplCqPpGvASVCBM5gNO0JuD7cRXmrZttR4v3Mw9e0N+" +
                "eNFsQhnE3TiQiEZZmXw2Z7s8YIy+yqfqurSrMpkWVvsfXwb4ZCvZ/x//bCXvzn8ZKnKk3IsEG+60BZYP" +
                "IKuGffuqdmCTtAMpTbWsjCe9BiUal7xmuAAepYXTB0mDDnfYgRgXDPgOxI/9bozXPIv3oqnXSyN+kWmv" +
                "J9rhJCioCSD5O65MamdlTiEowogMF4SB0ETMaZRRzYxdGBCm4HfOpjnHG6eWluGBXMvZHQkSzsIglXHL" +
                "4I0ETYt8mrNvRAWc2wLyRzKpWlb5AgzeQFmcdIRPhjqNR4iZDli4GxzsFBx0z8m1AARK74yb38VKO4Bd" +
                "yMGDeOisRfGK4oOMgGgsIeiO+VzrmNAa6fpqUksaYTCVIqCQjyy0CK/IYFgONlgsy9gWSLBEYrfL2yld" +
                "JUGY0heEpLOWu4Msk6Td5oDIREkUXWpKM6D80gGCj97ktnbFWpnb3IE/xBeHcWUk5wySydobdXB0tP+c" +
                "yJxxXr1DaVrZBa9NeZNXtlxAUHz2OZLEGlq6MdUa2YlDARl2oT3i2W34RJ7tCqWz0buTf432v2eZlktK" +
                "VRClyUqCI+ZWZtqR/39d1syCaGm9XIpywgqtkKeno/dH+y9CHm5pPkyOqfSRGFfB84OpHcm/QxDRboiJ" +
                "Sq+dWtTOE0RhpmBjsfTrXZBCQnO2IF1BtTFjtAk1Mw6azIRF1s0eMXiyNJV4nxRdLJF6FhHQxuNvlxed" +
                "z8QhJa9RLjz3qOi6yhA5XlNC4nQ4R3421dMCnoBmwuvFEgbnU0o7ThIIJMXvDCqtUGrXkgAhWmoXi7rk" +
                "Uq98jojs3iftbdTE1NoKUUrg0wqZlLDj1yFYTZkadXw05JJs0poyBCjlZQrzclgeH6mkRvbee0EX0BKt" +
                "7FNK5jOk7Ya4uCCYNbeIH0d8ajcEjb+JcAPghnaQbUuE5A7vjbF0uwpEwIJZ2nSudsD5KQKDLAiD3+gq" +
                "15OCW4cUGgDWHl3q7XYwE9tDVerSRvSCsaXx36AtG7wk09M5bFZw2NQzKBCAywqOmEnDQkjSgpIXGpJJ" +
                "hQYgoVtCMtn+mXQszQdbJL9XKmLpYmuMUcC+mUN+vc7BR//yjzp59WZ0eEG+8dcvhx8K2MPYWeclBarE" +
                "rp7YUC24kKN0U8hQHxaKKyIDtlZ+ZalZRYc3JWeHJtFhIIfamcHlSiFC4N8F95c5qo4uQfFur3tt1rHT" +
                "6FIY8o7cRxamvI2ETD0MH8wQI6XKJtGKwBIRZpMuK6Hv07C/enN+8v4ZAjeGx8eDd2+VIBiogyatovQ3" +
                "SXmhrzlzQlZymqiV1FbURsDVpdeMTc5AjbiZBZf3rc65nZJxYe01vPbaDNV3/+6RhnvD3iE13Eeven3V" +
                "q6z12Jl7vxw+e1ZYBAi07Xt/fCciopOBP2NAQaxBDaFai/VC003G6WiBBprc93ApR1gifV0bExLGtED5" +
                "mORF7tehZXrIYSGwESVyKkRPdPRKfIORkFRUiwJlWKrIMCGpwxp6orJ7i6gsDKejn8FgEJaWitEMVaMA" +
                "3iMVYG9TBcMf/v7TS4GgdhBcgjnA3ee4Fyid//pWwWzOzG2RNXa6Q/j8c/E6QghuJqV6q5nb+1F2lrbC" +
                "zg8v917wEtAVAWCqs6sAgfyyQo7f2KaumQSJBMah+5XThc3qgs7BVmG8XfaiQ8O1v12F/FIPS7XySCJ1" +
                "Ym/76IPI2foqXSMR8ywBjzNUtw6Kdv6GZ0h4c2GcawzQnhvvSexMgYz6EOo0ORhlpHvex79BwpXtJ/Xq" +
                "5AO6K/l8fvp6dDZCxyPLw49vj98fjc7QYYSNk/ej/Zcx4GOK4uaHeApQMjzErIDigIEXFfYuaJjW0Li2" +
                "EPHOwmju27oXOmBDZTACctVGf+GDEqSDJHXdxmTVa+/0pOdKgnfSKQRnVmWu/dBXH2kYztSnLs+kZB7l" +
                "TTnDDBM42kxDNNA38kHpg1a34w9olNvVx0bXtPpEzWWHJdF/4MqWyN9kdsqc+AtJkX2oDxI+kdQoG4vc" +
                "lc7ymlgIA7h4UORD8I7PDo6O/3lOjXuHZjQy4yQDSxcmWhHXoacHTn3N1KIL28z6n5RGHzxQkiGRBs0d" +
                "vOPXo+NfXl+oHcIdFrutTFSXpl2NtzLN7wz+MRbUDsXCrtCjVBfpiHSBjiw6dL5EBRga3Yn5wsz8ME1U" +
                "bXmmike4346fmzGJPjzNK25EqXJTx7psfYh1SvfpGYT8vV72RbPqSVBqDNINZTYutSE8jUltpN4DbhUD" +
                "wG+W5e5Pp/IsghESQQNTSKtDnQvVLMTqtDLksZgv+2IwahLknGsKK1x8j+8OVHLKT5gRIPkVk7epSsbb" +
                "wj2ejGCGhLz/ZNqIwK0cW/quxM2z1W3zad18+v2xJGj114jRmIu6mK5W7/JPq8+t9qkZgtf+Z6Gal7pH" +
                "KbX0sNMU2I4x6DWHEiEetyTJYDIqZ2iW/tGpJDe6qBHXyGs08tnGnjz14yUDY6Zxl1cJEbkICOgVN+Ii" +
                "Ap2n83gjPn2gya2pHzTCzQNuBWTx0qNpKwrykNaiZOhpG75kRL7cE1bN7Zgf4x+JYX4bI07P7uUXBBwd" +
                "0kts8/LKj3DNS52+VU9oqn2i0t/xX6b2FT9raTXcRyCb6eXzK3rZb5bf0zJtli9omTXLvavG9S9fXvHe" +
                "t9PBV17Ttzbel5tvGPjbH/5awiUbd6LHcZ5y/zfWm3w6oj6vBQ75M74S4sUFoyEkbILysg8PxNCCFaIS" +
                "xTRN8cWEPHy5K7IVmp4uNL71wIBx1XyD1aEl5dvc0sMQPQmG3rt5oQyZgSp+fACkpw9MIuAIExvtyBdt" +
                "rGoC2hBzANlbfyGtB8Y6e604nc07YnX2RZokq+NLIPqdMT3HjtEo42Ar+RPlnzA4LhwAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
