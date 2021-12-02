/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GripperTranslation : IDeserializable<GripperTranslation>, IMessage
    {
        // defines a translation for the gripper, used in pickup or place tasks
        // for example for lifting an object off a table or approaching the table for placing
        // the direction of the translation
        [DataMember (Name = "direction")] public GeometryMsgs.Vector3Stamped Direction;
        // the desired translation distance
        [DataMember (Name = "desired_distance")] public float DesiredDistance;
        // the min distance that must be considered feasible before the
        // grasp is even attempted
        [DataMember (Name = "min_distance")] public float MinDistance;
    
        /// Constructor for empty message.
        public GripperTranslation()
        {
            Direction = new GeometryMsgs.Vector3Stamped();
        }
        
        /// Explicit constructor.
        public GripperTranslation(GeometryMsgs.Vector3Stamped Direction, float DesiredDistance, float MinDistance)
        {
            this.Direction = Direction;
            this.DesiredDistance = DesiredDistance;
            this.MinDistance = MinDistance;
        }
        
        /// Constructor with buffer.
        internal GripperTranslation(ref Buffer b)
        {
            Direction = new GeometryMsgs.Vector3Stamped(ref b);
            DesiredDistance = b.Deserialize<float>();
            MinDistance = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GripperTranslation(ref b);
        
        GripperTranslation IDeserializable<GripperTranslation>.RosDeserialize(ref Buffer b) => new GripperTranslation(ref b);
    
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
    
        public int RosMessageLength => 8 + Direction.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/GripperTranslation";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b53bc0ad0f717cdec3b0e42dec300121";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VwW7UQAy95yss9dAWtYvUIg6VuFVAD0iVWnGtvBknGZrMhBmnbfh6nie7oRUIcQBW" +
                "KyU7Yz/72c/eA3LS+CCZmDRxyD2rj4GamEg7oTb5cZR0QlMWRz7Q6Ov7aSRcjz3XQsr5PlcHxUGeeBh7" +
                "Ke+9b9SHljhQ3H6RWik2jQXhLSxgwOOYItedGVmk5aLZAeO0AqpdOJ/gbknFZrH8kWfVShxE03w35Da/" +
                "/gzDmM5vFHkg3dVzhZKMI/eCqfNZOdRSNX1kPT/bG92tFzvnAez3ZzhgpWHKSluhOobsnRhyI5y9EdkK" +
                "qJidwL1NnEfymeRBArGqDKOKW0MC+lm4d3/5U326+XBBv6sUUrztkF6SMUmWoCaHnQk9eu1w04CfMa9j" +
                "TM4HVjQr8SDoMArqB0H6w1h9FEYlqCuPao/xUJ7/ilpWt7BagoMNeAXHyREos2PlIqzOt52k0x5d6OG0" +
                "E4nd6jxK3uzLgG8rQRL3/bwIXyN4D8MUfG3EV7p7f3hCHUwjJ/X11HP6qU6Gjm+Wr1Op49XlRRGO1JN6" +
                "JDQDoU4mHwzE1SVVkw+mDThUB7eP8RQ/pUVp1+CLCE1VT9Y2y5PzBWK8WshtgI3iCKK4TEfl7A4/8zEh" +
                "CFKQMdYdHSHz61k7zILJ/IGTL6MI4BoVAOqhOR0eP0O2tC8ocIh7+AXxR4w/gQ0rrnE67dCz3tjnqUUB" +
                "YYgN8YDBcrSdC0jde4gTu2WbOM2VeS0hq4P3RYtq7SsdwZNzjrVHA1zRcJU1GXrpxp13/3XQfjlhy1SY" +
                "cpokYDJioW5MJFelrTFAFIMwGEN/qycc18W2ASrm0lbNCXklF7HJQ1RgDHwPSEGNCd7YtgB7ueNxDJcj" +
                "2bSbE3rsbDOZVdnalkWZAV9T8q3f7UwEGlbndUWckDZnqHHfLzkvwZbVl6IWh+MNXTU0x4kejRBe0m70" +
                "oq3QfV5FIhpj+cPZb88XBb2OGASUJWduoaaQFUO/qZZd+vYNPa1v8/r2rfoOPnkDJegGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
