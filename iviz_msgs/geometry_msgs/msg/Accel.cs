/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Accel")]
    public sealed class Accel : IDeserializable<Accel>, IMessage
    {
        // This expresses acceleration in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear;
        [DataMember (Name = "angular")] public Vector3 Angular;
    
        /// <summary> Constructor for empty message. </summary>
        public Accel()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Accel(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Accel(ref Buffer b)
        {
            b.Deserialize(out Linear);
            b.Deserialize(out Angular);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Accel(ref b);
        }
        
        Accel IDeserializable<Accel>.RosDeserialize(ref Buffer b)
        {
            return new Accel(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Linear);
            b.Serialize(Angular);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 48;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Accel";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1Ry0rEQBC8D+QfCvaisERQ8SB4lj0IguJVepNOdtjJTOjpdTd+vZ0HEe8GBjqZquqq" +
                "ygbvB5/Bl144Z86gquLAQupThI9ohBm5p4qxl3Tk8aMmeM0IPjIJKNZ22lOwuSfRXLoPrjTJHRbI7/uC" +
                "c4V7+uencC9vz49oOXWsMnx2uc03y97CbeaYwmNMjmae8DVd/s1YwqA7hWFTDAM6pqiwvCvTiLUXo1o/" +
                "pamycJOEt9YI6mQFxqSm0dHRJDlmHtnU9yZGUKGYw9ztVCKuuGzLLc4HK3ZC+dga0BRajiy+gvjW1zPT" +
                "FnUrmbCk20KbW5x9CLPneZke2EQk6US4LrFrMKQTzmMgGwQ1qTlK2JvFxRftw+g3bXEajU8Sfxt9Tfb7" +
                "rZacqWXrLitTXTrXhET6cI/LOg3r9F24H4Euk0NnAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
