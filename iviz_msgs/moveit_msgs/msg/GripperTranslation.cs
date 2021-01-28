/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/GripperTranslation")]
    public sealed class GripperTranslation : IDeserializable<GripperTranslation>, IMessage
    {
        // defines a translation for the gripper, used in pickup or place tasks
        // for example for lifting an object off a table or approaching the table for placing
        // the direction of the translation
        [DataMember (Name = "direction")] public GeometryMsgs.Vector3Stamped Direction { get; set; }
        // the desired translation distance
        [DataMember (Name = "desired_distance")] public float DesiredDistance { get; set; }
        // the min distance that must be considered feasible before the
        // grasp is even attempted
        [DataMember (Name = "min_distance")] public float MinDistance { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GripperTranslation()
        {
            Direction = new GeometryMsgs.Vector3Stamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GripperTranslation(GeometryMsgs.Vector3Stamped Direction, float DesiredDistance, float MinDistance)
        {
            this.Direction = Direction;
            this.DesiredDistance = DesiredDistance;
            this.MinDistance = MinDistance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GripperTranslation(ref Buffer b)
        {
            Direction = new GeometryMsgs.Vector3Stamped(ref b);
            DesiredDistance = b.Deserialize<float>();
            MinDistance = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GripperTranslation(ref b);
        }
        
        GripperTranslation IDeserializable<GripperTranslation>.RosDeserialize(ref Buffer b)
        {
            return new GripperTranslation(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Direction.RosSerialize(ref b);
            b.Serialize(DesiredDistance);
            b.Serialize(MinDistance);
        }
        
        public void RosValidate()
        {
            if (Direction is null) throw new System.NullReferenceException(nameof(Direction));
            Direction.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Direction.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/GripperTranslation";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b53bc0ad0f717cdec3b0e42dec300121";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA71VwW7UQAy95yss9dAWtYtUEIdK3CqgB6RKrbhW3oyTDE1mwozTNnw9z5PdtBUIcQBW" +
                "KyU7Yz/72c/eA3LS+CCZmDRxyD2rj4GamEg7oTb5cZR0QlMWRz7Q6Ou7aSRcjz3XQsr5LlcHxUEeeRh7" +
                "Ke+9b9SHljhQ3H6VWik2jQXhLSxgwOOYItedGVmk5aLZAeO0AqpdOJ/gbknFZrF8yrNqJQ6iab4dcptf" +
                "f4FhTG+uFXkg3dVzhZKMI/eCqfNZOdRSNX1kfXO2N7pdL3bOg38yxgErDVNW2grVMWTvxJAb4eyNyFZA" +
                "xewE7m3iPJLPJPcSiFVlGFXcGhLQz8K9/8uf6vP1x3P6XaWQ4k2H9JKMSbIENTnsTOjBa4ebBvyMeR1j" +
                "cj6wolmJB0GHUVA/SDas6pMwKkFdeVR7jPvy/FfUsrqF1RIcbMArOE6OQJkdKxdhdb7tJJ326EJPeS8S" +
                "u9V5lLzZlwHfVoIk7vt5Eb5G8B6GKfjaiK909/7whDqYRk7q66nn9FOdDB3fLN+mUsfLi/MiHKkn9Uho" +
                "BkKdTD4YiMsLqiYfTBtwqA5uHuIpfkqL0q7BFxGaqh6tbZYn53PEeLWQ2wAbxRFEcZmOytktfuZjQhCk" +
                "IGOsOzpC5lezdpgFk/k9J19GEcA1KgDUQ3M6PH6GHAp04BD38AviU4w/gQ0rrnE67dCz3tjnqUUBYYgN" +
                "cY/BcrSdC0jde4gTu2WbOM2VeS0hq4MPRYtq7SsdwZNzjrVHA1zRcJU1GXrpxq13/3XQfjlhy1SYcpok" +
                "YDJioW5MJJelrTFAFIMwGEN/qycc18W2ASrm0lbNCXklF7HJQ1RgDHwHSEGNzRvbFmAvdzyO4XIkm3Zz" +
                "Qg+dbSazKlvbsigz4GtKvvW7nYlAw+q8rogT0uYMNe77Jecl2LL6UtTicLyhy4bmONGDEcJL2o1etBW6" +
                "z6tIRGMsfzj77fmioFcRg4Cy5Mwt1BSyYug31bJL372lx/VtXt++Vz8APnkDJegGAAA=";
                
    }
}
