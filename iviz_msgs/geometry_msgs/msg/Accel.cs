/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Accel")]
    public sealed class Accel : IDeserializable<Accel>, IMessage
    {
        // This expresses acceleration in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear { get; set; }
        [DataMember (Name = "angular")] public Vector3 Angular { get; set; }
    
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
        public Accel(ref Buffer b)
        {
            Linear = new Vector3(ref b);
            Angular = new Vector3(ref b);
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
            Linear.RosSerialize(ref b);
            Angular.RosSerialize(ref b);
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
                "H4sIAAAAAAAACq1SwUrDQBS8B/IPA70olAgqHgTP0oMgKF7lNXlJl252w9tX2/j1vk1Cq3cDgcfuzLyZ" +
                "SVZ437kEPg3CKXEC1TV7FlIXA1xAK8xIA9WMrcQ950ONcJrgXWASUGjs7Q7e5oFEU1UWH1xrlDssmF8H" +
                "C7IsyuLpn5+yeHl7fkTHsWeV8bNPXbpZFpfFak4qnJNyMP+Er+nyb8wKGbtRGDgGP6JnCgrLfKYas3Fi" +
                "XOuoMlkWbqPw2lpBE63EEDWL9LQ3UQ6JM52GwdQIKhSSnwuemsQVV121xnFn7U4oFzoDZomOA4urIa5z" +
                "zUy1Vf2ZTVgCrqHtLY7O+9n1vE13nFUk6sS4rrBpMcYDjjmTDYKG1DxFbM3k4oy2PjuOaxyy9Vnjb62v" +
                "0X4DqyYl6tgKTMrU2Icvi9ZH0od7nC7jeBm/y+IHZO64V3MCAAA=";
                
    }
}
