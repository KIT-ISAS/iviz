/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class MultiDOFJointState : IDeserializableRos1<MultiDOFJointState>, IDeserializableRos2<MultiDOFJointState>, IMessageRos1, IMessageRos2
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
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "transforms")] public GeometryMsgs.Transform[] Transforms;
        [DataMember (Name = "twist")] public GeometryMsgs.Twist[] Twist;
        [DataMember (Name = "wrench")] public GeometryMsgs.Wrench[] Wrench;
    
        /// Constructor for empty message.
        public MultiDOFJointState()
        {
            JointNames = System.Array.Empty<string>();
            Transforms = System.Array.Empty<GeometryMsgs.Transform>();
            Twist = System.Array.Empty<GeometryMsgs.Twist>();
            Wrench = System.Array.Empty<GeometryMsgs.Wrench>();
        }
        
        /// Constructor with buffer.
        public MultiDOFJointState(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStringArray(out JointNames);
            b.DeserializeStructArray(out Transforms);
            b.DeserializeArray(out Twist);
            for (int i = 0; i < Twist.Length; i++)
            {
                Twist[i] = new GeometryMsgs.Twist(ref b);
            }
            b.DeserializeArray(out Wrench);
            for (int i = 0; i < Wrench.Length; i++)
            {
                Wrench[i] = new GeometryMsgs.Wrench(ref b);
            }
        }
        
        /// Constructor with buffer.
        public MultiDOFJointState(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStringArray(out JointNames);
            b.DeserializeStructArray(out Transforms);
            b.DeserializeArray(out Twist);
            for (int i = 0; i < Twist.Length; i++)
            {
                Twist[i] = new GeometryMsgs.Twist(ref b);
            }
            b.DeserializeArray(out Wrench);
            for (int i = 0; i < Wrench.Length; i++)
            {
                Wrench[i] = new GeometryMsgs.Wrench(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MultiDOFJointState(ref b);
        
        public MultiDOFJointState RosDeserialize(ref ReadBuffer b) => new MultiDOFJointState(ref b);
        
        public MultiDOFJointState RosDeserialize(ref ReadBuffer2 b) => new MultiDOFJointState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeStructArray(Transforms);
            b.SerializeArray(Twist);
            b.SerializeArray(Wrench);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeStructArray(Transforms);
            b.SerializeArray(Twist);
            b.SerializeArray(Wrench);
        }
        
        public void RosValidate()
        {
            if (JointNames is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) BuiltIns.ThrowNullReference(nameof(JointNames), i);
            }
            if (Transforms is null) BuiltIns.ThrowNullReference();
            if (Twist is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Twist.Length; i++)
            {
                if (Twist[i] is null) BuiltIns.ThrowNullReference(nameof(Twist), i);
                Twist[i].RosValidate();
            }
            if (Wrench is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Wrench.Length; i++)
            {
                if (Wrench[i] is null) BuiltIns.ThrowNullReference(nameof(Wrench), i);
                Wrench[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetArraySize(JointNames);
                size += 56 * Transforms.Length;
                size += 48 * Twist.Length;
                size += 48 * Wrench.Length;
                return size;
            }
        }
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, JointNames);
            WriteBuffer2.AddLength(ref c, Transforms);
            WriteBuffer2.AddLength(ref c, Twist);
            WriteBuffer2.AddLength(ref c, Wrench);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/MultiDOFJointState";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "690f272f0640d2631c305eeb8301e59d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
