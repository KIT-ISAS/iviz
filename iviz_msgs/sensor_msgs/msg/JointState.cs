/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/JointState")]
    public sealed class JointState : IDeserializable<JointState>, IMessage
    {
        // This is a message that holds data to describe the state of a set of torque controlled joints. 
        //
        // The state of each joint (revolute or prismatic) is defined by:
        //  * the position of the joint (rad or m),
        //  * the velocity of the joint (rad/s or m/s) and 
        //  * the effort that is applied in the joint (Nm or N).
        //
        // Each joint is uniquely identified by its name
        // The header specifies the time at which the joint states were recorded. All the joint states
        // in one message have to be recorded at the same time.
        //
        // This message consists of a multiple arrays, one for each part of the joint state. 
        // The goal is to make each of the fields optional. When e.g. your joints have no
        // effort associated with them, you can leave the effort array empty. 
        //
        // All arrays in this message should have the same size, or be empty.
        // This is the only way to uniquely associate the joint name with the correct
        // states.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "name")] public string[] Name { get; set; }
        [DataMember (Name = "position")] public double[] Position { get; set; }
        [DataMember (Name = "velocity")] public double[] Velocity { get; set; }
        [DataMember (Name = "effort")] public double[] Effort { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public JointState()
        {
            Header = new StdMsgs.Header();
            Name = System.Array.Empty<string>();
            Position = System.Array.Empty<double>();
            Velocity = System.Array.Empty<double>();
            Effort = System.Array.Empty<double>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public JointState(StdMsgs.Header Header, string[] Name, double[] Position, double[] Velocity, double[] Effort)
        {
            this.Header = Header;
            this.Name = Name;
            this.Position = Position;
            this.Velocity = Velocity;
            this.Effort = Effort;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public JointState(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Name = b.DeserializeStringArray();
            Position = b.DeserializeStructArray<double>();
            Velocity = b.DeserializeStructArray<double>();
            Effort = b.DeserializeStructArray<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new JointState(ref b);
        }
        
        JointState IDeserializable<JointState>.RosDeserialize(ref Buffer b)
        {
            return new JointState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Name, 0);
            b.SerializeStructArray(Position, 0);
            b.SerializeStructArray(Velocity, 0);
            b.SerializeStructArray(Effort, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            for (int i = 0; i < Name.Length; i++)
            {
                if (Name[i] is null) throw new System.NullReferenceException($"{nameof(Name)}[{i}]");
            }
            if (Position is null) throw new System.NullReferenceException(nameof(Position));
            if (Velocity is null) throw new System.NullReferenceException(nameof(Velocity));
            if (Effort is null) throw new System.NullReferenceException(nameof(Effort));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                size += 4 * Name.Length;
                foreach (string s in Name)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 8 * Position.Length;
                size += 8 * Velocity.Length;
                size += 8 * Effort.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/JointState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3066dcd76a6cfaef579bd0f34173e9fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1UTW/bMAy9F+h/INBDm6JNgW3YIcAOA/Z5WDGgBXYohkKxGFubLHmSnMz79XuUbMdF" +
                "LzuszSF2yEfyvUee0X1jIuGjqOUYVc2UGpWo8VZH0iopSp40xyqYrfzGFJNKTH6HlMhJviQffvVMlXcp" +
                "eGtZ0w9vXIprOj05wwdFFmmsqqYE0EXgvbe9vA/UBRNblUy1kn4074wD0nbYCAJd5tqdjyYZ73JVPE8w" +
                "SgtCu7paxO7Z+sqk4XnsTczRN3FFymla5PBu50MqFAgpXWcNmjBuiXDbSvrtaj1O9/44EHJ6Z0CGHcho" +
                "dsnsJH+LpxTJqZYnOhpWmgPFjiuJiblAMi0TSh8aA8hjycxdpAMHpsCVD5r1mt5a+yxG4NGtdzzr2ag9" +
                "kD1tj7lSI0uJhnLRaZTshikRekYT0XfWuu1tMp1FfyGoIV7lGmCr6Nkp0PaE6NxPdkCet/bKCj1opFU/" +
                "uWSNCZhf3OY7kVbZNX1r2BGv6zUNvg+jm8ogzgviqJOKERKjjqaDSZmx9kpyqFKOLOfJj7LmzonbLg2z" +
                "NYXEMlFReTF/bHxv9cjfxFY0fxizB6GzIM28yXQI8w7iH1AIo85mmDtdMCR2mPsG2wHyJEErUgJY/j8V" +
                "oxS/yIuYgnH1w/fRTjvrVXr9Cs/TcizfTUuwfFfYEKg3//nv9OTL3ccN+tePbazjTeldRrpL2DQVNMhN" +
                "Kp8VsU5j6obDtWW0KVO3HZQsR2foMgNHamt2HJQFl31EFMitfNuC4CqzChM/ARgXQWVnmqq3KiAB7jdO" +
                "4ncB5GX8TDhDJVcxfX63ybbnqk8GTWFvXRVYRTCOHxHcQ7mXLyQDifcHf41nriHQ3MF8Pfh3F2AlWbeY" +
                "j9hlmXENeJCEXXRw/UV+94hHnCPUQRfceSzHBdr/OqQG1y6fMxWM2mL/gFyBB8CeS9L5agktrW/gDOcn" +
                "/AJ5LPIvuIIyAstY1w3Es0JB7GvwiMgu+D0OXD5t2b24k7C0NdugAtyWL1kuCpAPQnbZrayNXNanmzu5" +
                "uujyaLS48y890dX7nQYAAA==";
                
    }
}
