/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Joy : IDeserializable<Joy>, IMessage
    {
        // Reports the state of a joysticks axes and buttons.
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp in the header is the time the data is received from the joystick
        [DataMember (Name = "axes")] public float[] Axes; // the axes measurements from a joystick
        [DataMember (Name = "buttons")] public int[] Buttons; // the buttons measurements from a joystick 
    
        /// Constructor for empty message.
        public Joy()
        {
            Axes = System.Array.Empty<float>();
            Buttons = System.Array.Empty<int>();
        }
        
        /// Explicit constructor.
        public Joy(in StdMsgs.Header Header, float[] Axes, int[] Buttons)
        {
            this.Header = Header;
            this.Axes = Axes;
            this.Buttons = Buttons;
        }
        
        /// Constructor with buffer.
        internal Joy(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Axes = b.DeserializeStructArray<float>();
            Buttons = b.DeserializeStructArray<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Joy(ref b);
        
        Joy IDeserializable<Joy>.RosDeserialize(ref Buffer b) => new Joy(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Axes);
            b.SerializeStructArray(Buttons);
        }
        
        public void RosValidate()
        {
            if (Axes is null) throw new System.NullReferenceException(nameof(Axes));
            if (Buttons is null) throw new System.NullReferenceException(nameof(Buttons));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += 4 * Axes.Length;
                size += 4 * Buttons.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/Joy";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "5a9ea5f83505693b71e785041e67a8bb";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1TTW/UMBC9+1eMlENbpC0S3Fbihvg4ICHaG0KrWXs2MTh2sCfb5t/z7DRsxQFxIIqy" +
                "if3mzfN7sx19kSllLaSDUFFWoXQipu9pKertj0L8KHhER8dZNcVyaz4IO8k0rD+XqyP1o4BknMjHxviE" +
                "8St/3W4vjpXrYhYr/iyOTjmNbWfra04hsb5+9fXbKuB5E8Da2ihc5iyjROhvDBfdxse1+kn1H/Xb6t8o" +
                "yJg3//kyn+7e72GzO4ylLy9XI01HdwqDOTvoUW7mnBIM9v0geRfkLKFmM05wqu3qMgmC6Oh+gIu4e4mS" +
                "OYSF5gKQJrJpHOfobU30dyxbPSoRENPEGSedA2fgU3Y+Vvgp8yiVHXeRn7NEK/Tx7R6YWMTOisjQyUeb" +
                "YZ+PPTbJzM3xWmC6+4e0w6f0yP4yEzqwVrHyOGUpVSeXPXq8WA93C26YI+jiCl23tQM+yw2hCSRgUu1A" +
                "11D+edEhrRN25uz5GKQSWzgA1qtadHXzjLnK3lPkmDb6lfHS419oK8vKW8+0G5BZqKcvcw8DAZxyOnsH" +
                "6HFpJDZ4DBYFf8ycF9PGv7U03bvqMUCo2v4MXEqyHgE4evA6mKK5src0Dt4Z8wtW3XPdrQMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
