using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    [DataContract]
    public sealed class MultiDOFJointState : IMessage
    {
        // Representation of state for joints with multiple degrees of freedom, 
        // following the structure of JointState.
        //
        // It is assumed that a joint in a system corresponds to a transform that gets applied 
        // along the kinematic chain. For example, a planar joint (as in URDF) is 3DOF (x, y, yaw)
        // and those 3DOF can be expressed as a transformation matrix, and that transformation
        // matrix can be converted back to (x, y, yaw)
        //
        // Each joint is uniquely identified by its name
        // The header specifies the time at which the joint states were recorded. All the joint states
        // in one message have to be recorded at the same time.
        //
        // This message consists of a multiple arrays, one for each part of the joint state. 
        // The goal is to make each of the fields optional. When e.g. your joints have no
        // wrench associated with them, you can leave the wrench array empty. 
        //
        // All arrays in this message should have the same size, or be empty.
        // This is the only way to uniquely associate the joint name with the correct
        // states.
        
        [DataMember] public std_msgs.Header header { get; set; }
        
        [DataMember] public string[] joint_names { get; set; }
        [DataMember] public geometry_msgs.Transform[] transforms { get; set; }
        [DataMember] public geometry_msgs.Twist[] twist { get; set; }
        [DataMember] public geometry_msgs.Wrench[] wrench { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointState()
        {
            header = new std_msgs.Header();
            joint_names = System.Array.Empty<string>();
            transforms = System.Array.Empty<geometry_msgs.Transform>();
            twist = System.Array.Empty<geometry_msgs.Twist>();
            wrench = System.Array.Empty<geometry_msgs.Wrench>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiDOFJointState(std_msgs.Header header, string[] joint_names, geometry_msgs.Transform[] transforms, geometry_msgs.Twist[] twist, geometry_msgs.Wrench[] wrench)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.joint_names = joint_names ?? throw new System.ArgumentNullException(nameof(joint_names));
            this.transforms = transforms ?? throw new System.ArgumentNullException(nameof(transforms));
            this.twist = twist ?? throw new System.ArgumentNullException(nameof(twist));
            this.wrench = wrench ?? throw new System.ArgumentNullException(nameof(wrench));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MultiDOFJointState(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.joint_names = b.DeserializeStringArray();
            this.transforms = b.DeserializeStructArray<geometry_msgs.Transform>();
            this.twist = b.DeserializeArray<geometry_msgs.Twist>();
            for (int i = 0; i < this.twist.Length; i++)
            {
                this.twist[i] = new geometry_msgs.Twist(b);
            }
            this.wrench = b.DeserializeArray<geometry_msgs.Wrench>();
            for (int i = 0; i < this.wrench.Length; i++)
            {
                this.wrench[i] = new geometry_msgs.Wrench(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MultiDOFJointState(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.SerializeArray(this.joint_names, 0);
            b.SerializeStructArray(this.transforms, 0);
            b.SerializeArray(this.twist, 0);
            b.SerializeArray(this.wrench, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
            if (joint_names is null) throw new System.NullReferenceException();
            if (transforms is null) throw new System.NullReferenceException();
            if (twist is null) throw new System.NullReferenceException();
            for (int i = 0; i < twist.Length; i++)
            {
                twist[i].Validate();
            }
            if (wrench is null) throw new System.NullReferenceException();
            for (int i = 0; i < wrench.Length; i++)
            {
                wrench[i].Validate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += header.RosMessageLength;
                size += 4 * joint_names.Length;
                for (int i = 0; i < joint_names.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(joint_names[i]);
                }
                size += 56 * transforms.Length;
                size += 48 * twist.Length;
                size += 48 * wrench.Length;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/MultiDOFJointState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "690f272f0640d2631c305eeb8301e59d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTW/cNhC961cQ8CF2sVaBuOjBQA8FXLcuUDRN3OZQFAZXmpVYU6RCUqsovz5vhpJ2" +
                "/VE0h9aGYetj5nHmzZsZnai31AeK5JJOxjvldyriktTOB/W3Ny5FNZrUqm6wyfSWVE1NIIpsucNF7buN" +
                "Kk5gb60fjWtUagkYYajSEIjNfmaYd4xaFicwvUnKRKVjHDqqYa6T0vksZRwu4xQTdaryAZH13tVRJY/n" +
                "KWgXEVeXfRpCbLrvrQEKYLX18+n3xlGHfCpVtdq4Ul0jGfqoO8S/AVBvtdNzeupURz7297dX12cc18XV" +
                "r9fq9ONGTfjV4xkjOw7TR8ovK+3UlgDIzEUcDoSj6DKR+BcMULIvwn34HqjZYkGrvNtTSEDb6uqeE34Q" +
                "A+x/0FW70BTV4MyHgeykTI3imR2TsMUdOHG6I9jfgomWdE1BxZ4qNolCTzIdKUQ0tgaI/CSjSuFRbkLZ" +
                "AoH+mupSfW/tExuggzLvSHVgQDc4SO+Jg94eXPkI0QLCkTNz9W9bRL+4IetoYhI16YPGdAh6ihs5gYVI" +
                "nHqvQ2K7R8GUas618doyM4ii0/eUnWZ75G4hI98z99qW6n1LTlHZlGryw6p0ycJ5AI6BHNwhUl8ZzWWR" +
                "LgAW5A4XKZslyRr4iznHrajr08RhAYfZy9kwY+k499j6wdYzcwtP0XyCRJEyC0xwFspMLp53qPmIU5Dm" +
                "qoE1zCNyWAVr0LmZqgSwXMGyKH7K2sgSKQq0LLr3z7+y+x27x6Ih31EK010Xm/j17SJhWK1yfmI0oqBs" +
                "wP8fvXsvNOFl5qsovvuPf4pf3v14iRTrfF5OEUlj+rhahxrkJ13rpEVWrWlaCueW9mSZl65HneVtmnrm" +
                "6EB9Q46CtuB64I4H+ZXvOhSgEtah7gf+uUG0aNZUg8W0qTy6wjiZrUFLN7BZJJTQVaRuri6lHagaktlL" +
                "Z7sqkI48U2+uVDGgLBev2aE4uR39OW6pQQHXw/OcQbDHk+kSZ3yVkyuBDXLQnjxST+XZHW7jGWTHIVDv" +
                "IeJTRP5mwrxzopy9DkZv0ZQArsAAUF+x06uzI2Qn0E47v8BnxMMZXwLrVlzO6bxFzSxnH4cGBMKwD35v" +
                "6jzpRNWY/ZC6Ndugw1TIYJMji5PrIGOHyycVyRvnuJlnxedq3Jn6/1LjP/TQIq6wLOB5OK9LbktpJEyp" +
                "NPon4pFpwusXo11X0FLxB7rbh4vsb/OO+W2AQ3C8joLPC/5lkpyDeSZFrfby7lH8av0skPnWkUZZ0WSr" +
                "JxxrwxMMOZQ87gOBJIxKk1TtwYfzSXYqBj/soywj/jiYltWcOeHHcDnl0b/BBgS/YsVCkLaVRseXQzCN" +
                "qR8vdfkMmZPbqLR7DSFhwEvM+TCUECAL22elutnJuhg5IVk1eb7Iplzikj5I3m94uMwQDwl9IyN92RzG" +
                "4QNJ16j6znqdvv1GfVyvpvXq04uU+qCx56qNj4Rg1o/LBzXnuw8HgTLJ/5rQcjW+UK/KCpvTWoZqhIIt" +
                "5kiaHuWzDf6eOEmRWMRUcoSxxR+A2jWyA3gdYK0svTqbHO5nu5fJLi/jZ6qGUuTyHJLboKkQvMxOTpD3" +
                "25elKGCHW/zFviuKz0fVW815DAAA";
                
    }
}
