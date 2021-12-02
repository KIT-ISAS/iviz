/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Wrench : IDeserializable<Wrench>, IMessage
    {
        // This represents force in free space, separated into
        // its linear and angular parts.
        [DataMember (Name = "force")] public Vector3 Force;
        [DataMember (Name = "torque")] public Vector3 Torque;
    
        /// Constructor for empty message.
        public Wrench()
        {
        }
        
        /// Explicit constructor.
        public Wrench(in Vector3 Force, in Vector3 Torque)
        {
            this.Force = Force;
            this.Torque = Torque;
        }
        
        /// Constructor with buffer.
        internal Wrench(ref Buffer b)
        {
            b.Deserialize(out Force);
            b.Deserialize(out Torque);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Wrench(ref b);
        
        Wrench IDeserializable<Wrench>.RosDeserialize(ref Buffer b) => new Wrench(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ref Force);
            b.Serialize(ref Torque);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Wrench";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4f539cf138b23283b520fd271b567936";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1SwUrEMBC95yse7EWhVFDxIHiWPQiC4lVm22k2mCZ1MnWtX+90u+yyeLXQMk3ee/Pe" +
                "JCu8bkOB8CBcOGlBl6VhhIROmFEGarhC4YGElFvb0OxWCIaMITEJKLX2+jFabSgttXvjRrPcYBE7/dr3" +
                "c2TnHv75cU8vj/fwnHtWmd774svVoan7m5Dwtd87D1nDoGuFYXOKE3qmpOb4xDRiG8SoIafaVFnY8tl0" +
                "gqLNXJCymkZPHybJqfDMpmEwMYIKpRJp5s7LRrng2tcVdltOCyokb0BT8JxYQgMJPrQL0xr1RzLhEK6C" +
                "dtfYhRgXz0sz3bKJSNY94bLGusOUR+zmQFYIWlJzlLExiwdftImz31xhnI3vJc4H+pzt7G0spZCfL0hR" +
                "prZ2rouZ9O4W38dqOlY/7hc/NsAeYQIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
