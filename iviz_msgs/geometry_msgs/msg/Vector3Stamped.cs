using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Vector3Stamped")]
    public sealed class Vector3Stamped : IMessage
    {
        // This represents a Vector3 with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "vector")] public Vector3 Vector { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Vector3Stamped()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Vector3Stamped(StdMsgs.Header Header, in Vector3 Vector)
        {
            this.Header = Header;
            this.Vector = Vector;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Vector3Stamped(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            Vector = new Vector3(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Vector3Stamped(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Header);
            b.Serialize(Vector);
        }
        
        public void Validate()
        {
            if (Header is null) throw new System.NullReferenceException();
            Header.Validate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Vector3Stamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7b324c7325e683bf02a9b14b01090ec7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VUwWrcMBC96ysG9pBN2biQlB4WeittcygEEnoNs9bYFrUlVxrvxv36PslZtyGXHtrF" +
                "INnSezPz5s1u6KFziaKMUZJ4TcT0TWoN8YZOTjucNBLF10J1CNE6zyrURB6E2FtSN0hSHkbzRdhKpK4s" +
                "5sxxLKsxH/7xz3y9/7ynpPZxSG16uwQ3G7pXZMXR0iDKlpWpCUjKtZ3Eq16O0lNJVyyVU51HSRWARQY8" +
                "rXiJ3PczTQmXNKDuYZi8q3Pha7lnPJDOQ7ORo7p66jm+0imz40nyYyo63n7c445PUk/qkNAMhjoKJ+db" +
                "HJKZnNeb6wwwm4dTuMKrtJB2DU7aseZk5Sm3LefJaY8Yb5biKnBDHEEUm2hbvj3iNV0SgiAFGUPd0RaZ" +
                "383aBQ9CoSNHx4deMnENBcB6kUEXl38w+0Lt2Ycz/cL4O8bf0PqVN9d01aFnfa4+TS0ExMUxhqOzuHqY" +
                "C0ndO5iTeneIHGeTUUtIs/lUvKi5faUjWDmlUDs0wBYPm6Qxs5duPDr7v9zYSoDr4rxY8nkCztZ6MWHL" +
                "VGTnNFFQyci1VNkkt6WtwcMUgzAqhv9WJIDWRUBd8BVYMZcwt+zIKdkgiXxQcAz8HZQCjTOaxxFkMHpk" +
                "n3rO2PwZkK1UbbWjUyd+uZU1Ko4uM+Bqiq51dkEi0LCC17+IHWlzDY37fsl5CYaGgSQGLYDLim4bmsNE" +
                "p1wQNvF59AIdZM2rWERD2OW5e6Z4KehdwCBAlpS4hZt8Ugx9ZUzTB9b37+hp3c3r7qf5BVYBeVDhBAAA";
                
    }
}
