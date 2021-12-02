/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectInformation : IDeserializable<ObjectInformation>, IMessage
    {
        //############################################# VISUALIZATION INFO ######################################################
        //################## THIS INFO SHOULD BE OBTAINED INDEPENDENTLY FROM THE CORE, LIKE IN AN RVIZ PLUGIN ###################
        // The human readable name of the object
        [DataMember (Name = "name")] public string Name;
        // The full mesh of the object: this can be useful for display purposes, augmented reality ... but it can be big
        // Make sure the type is MESH
        [DataMember (Name = "ground_truth_mesh")] public ShapeMsgs.Mesh GroundTruthMesh;
        // Sometimes, you only have a cloud in the DB
        // Make sure the type is POINTS
        [DataMember (Name = "ground_truth_point_cloud")] public SensorMsgs.PointCloud2 GroundTruthPointCloud;
    
        /// Constructor for empty message.
        public ObjectInformation()
        {
            Name = string.Empty;
            GroundTruthMesh = new ShapeMsgs.Mesh();
            GroundTruthPointCloud = new SensorMsgs.PointCloud2();
        }
        
        /// Explicit constructor.
        public ObjectInformation(string Name, ShapeMsgs.Mesh GroundTruthMesh, SensorMsgs.PointCloud2 GroundTruthPointCloud)
        {
            this.Name = Name;
            this.GroundTruthMesh = GroundTruthMesh;
            this.GroundTruthPointCloud = GroundTruthPointCloud;
        }
        
        /// Constructor with buffer.
        internal ObjectInformation(ref Buffer b)
        {
            Name = b.DeserializeString();
            GroundTruthMesh = new ShapeMsgs.Mesh(ref b);
            GroundTruthPointCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectInformation(ref b);
        
        ObjectInformation IDeserializable<ObjectInformation>.RosDeserialize(ref Buffer b) => new ObjectInformation(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            GroundTruthMesh.RosSerialize(ref b);
            GroundTruthPointCloud.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (GroundTruthMesh is null) throw new System.NullReferenceException(nameof(GroundTruthMesh));
            GroundTruthMesh.RosValidate();
            if (GroundTruthPointCloud is null) throw new System.NullReferenceException(nameof(GroundTruthPointCloud));
            GroundTruthPointCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.GetStringSize(Name);
                size += GroundTruthMesh.RosMessageLength;
                size += GroundTruthPointCloud.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectInformation";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "921ec39f51c7b927902059cf3300ecde";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XXW/bNhR916+4aB6aFImHJF0XZDCGtHYao44dNE6BtSgMWqJtrjKpkpQT9dfvXFK0" +
                "3S4DVmCt4MQSxXvu97n03t73XPRucHt3MRy8v5gMxiMajC7H9F0Amyt7DH1yNbiNoLdX47thj172afxy" +
                "cjEY9XtY7/Vv+vg3mgz/pMu342vs79Or8dv+IQ0Hb/rYQRcjevtu8J5uhnev8fiY4gx6lpKW9UposlIU" +
                "YlZK0mIlyczJ45WZ/SVznzlvlV6EN0loXpclraRbfr31HPfKUQ7AmaTaSeyjubFUKFeVoqGqtpVx0h2S" +
                "qBcrqb0sWHWpfEOdTodmtSflE8BMLaDvWnyS5GorgyLfVJKg47p/e5W5pajkdOUW7pdrNmZhTa2Lqbe1" +
                "X07ZPLb31qykV3g6pMbUZHTZ0FKsJQnKS1MXpHRA7r38V2U348Focps5qZ2xUd+NUdq/YvmTr9VW/GIa" +
                "kLOs+z9f2fXt63P6xm2Y3ZNzpZVXRnNCBCXfS+V8SJFVQi9K6X4PfildyAdai7KWDvGfS0veEDITIBxH" +
                "ZC2tV7l0Hz5mrGPSAnz4uMViBYwmcl+LciOBNeFpxYGsq7AhWLOQnAfb7IQPYEnoJ4UqufFIyJJbT93W" +
                "qBpGnp58OI12yocpAvcjrX0kRqHhuKeM9kIhNxzQlKloeag4ztncShRvJXKZzUsj/Ivn9LC5azZ3X35Y" +
                "sB/vj+QCysCJBRjHlIXj7jNlCdJo/RgdFWhS7fCIYgo+oWPvlypfopgagLQhIFEUwXtsUxrsshIBw9XY" +
                "KRxpXikhCwTG880hSZ93mLiAEqNVCC+4s503FhwEMQG60cI2NCvNDMLeERjLgJAK6XKrZtg2azj8rSlg" +
                "L5fo78lcSTj1hIS1oukkmoy6IssEjXCEic3YhdDqCxBPCtpXK0TlqFSf5AHe0HEB6f0abhQSth106GYL" +
                "43ZkYTTEg7RLyJU1RZ0HU9lMULYVcKDyyzY7bhMn5wFvWCOz45GZH81LtVj6aD2W2LkoFI0X+edaxcID" +
                "f+sieJ4b2InAeQwFy7Nj0KN9pvxTdiOm8aCTXWG+gGWW4YsVnPRggK1BHeDaNoo74erQIK61FO1oE5BD" +
                "oLCdWATOcbDkXhXwELtYpJR6gadHQNuGbgHSUxBmm3ptoiNMvhRay9IlV5VNBdEOjLZeQmy4aDpZSNQl" +
                "lwKoLZZEls2MKQmXclMMNAkGwXTbowFrgcVRPL34IxkV7Z4iSRVk92i4cWqn4WeN37AUkTX3af+3Enj1" +
                "9f4zGBg0p2uPLiKNb/vjkBzqjKO6n6CfxcgdtF7BpQIFIrcgE1tDIkQeeRX40waaMWpU0VbDD2MfX0Tq" +
                "ibXGk98jdcIWYB4vgrdcmEt4IO1RKdeyhJBYVWiX8JbHveskusIHSUH/lDgw4CyDIjAo99Wq1irneg+H" +
                "il15SDI9USV4ftSlsP9oD0bHx8nPtdQ5d8s5k4mTee0VDGqAkONE5PjEhVZKyYVAtje5N0dMague10l5" +
                "HLcwVj5UFgwbiOEcOp5F5zrAZmqGFrDHflib4tEdIL1sgqwMCGEflt80fmliba8FxiGfB8NxDjxd0FMW" +
                "enqwg8xmn+NcqE2Cj4hbHf8FVm9wAw+h64qSvXf1AgHERnDaWhUb+kUvK3AvzjYzi/7LWCqqzPYuAwVt" +
                "+wrfwjmTKySAWQJ93p5mQzamqvhpszDQwuOjkJ2KQ6ZK09DoxFxwFSQTKQfiu+fOhBJnYGS3M5z+J2fc" +
                "jF06blfu2qUunWz3HL8IK6c7e3ipS8+3e5hVsPLrzh5e6tKLduVyOL7gpS79truCQ0aXzrLdXw6JIEbt" +
                "74vAjqm6zXzupI8bxvF+bs2Ks2rD+TWGIg6MVlFIcDigQ6iX7qWueeTFGeVwHBIzs5ZJT45zelADkStQ" +
                "In75NCRLyb9EwqmXMxEty/4GcuAI5REOAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
