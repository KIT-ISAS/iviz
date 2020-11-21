/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/InertiaStamped")]
    public sealed class InertiaStamped : IDeserializable<InertiaStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "inertia")] public Inertia Inertia { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InertiaStamped()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public InertiaStamped(StdMsgs.Header Header, in Inertia Inertia)
        {
            this.Header = Header;
            this.Inertia = Inertia;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public InertiaStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Inertia = new Inertia(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InertiaStamped(ref b);
        }
        
        InertiaStamped IDeserializable<InertiaStamped>.RosDeserialize(ref Buffer b)
        {
            return new InertiaStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Inertia.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 80;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/InertiaStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ddee48caeab5a966c5e8d166654a9ac7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9G/wfBnJIUmwXktKDIaeWNj4EAgm9lNaMpbG0RNpVd1e2ZfLj+2Ylf+TW" +
                "Q1thIe/uvDdv5+teOBdPZfqMRwsrPhom03/Ho/Ho7i8/49HD09c5hZgv61CE9/eD6wt6imxz9jnVEjnn" +
                "yLR2kGaKUvy0ko1UQHHdSE7pNHaNhJkin0sTCL9CoJurqqM2wCo6ylxdt9ZkHIWiqeUNgUKNJaaGcdus" +
                "rdgD4HxurNqvPdeS+PUN8qsVmwktPs9hZYNkbTQQ1YEj88LB2AKHMG6Njbc3igDweeumWEuBMB8VUCw5" +
                "qmLZNV6CiuUwVzfv+jvOQI8gCRzlga7S3hLLcE3wAxXSuKykK8h/7GLpLBiFNuwNrypR5gxxAO2lgi6v" +
                "z6lV+pwsW3fg7ylPTv6E156I9VrTEsmrNAShLRBHWDbebUwO21WXWLLKiI1UmZVn341HCuudguSLBhtm" +
                "wKXc4MshuMwgEzltTSzHoxC9Okh5WZr8H1ZnIQ5F6Lu+RIeu0Ks+QBR9fyl+jEfrynH8+IFq1XFBn3A3" +
                "pNitqU42NUze0nyTLDp/qzXZQw7d9iw2oNJBO61/3gB3Qfq8ktnt8KLAdnt6TQi6S9vY6vQdtg/W+7Rl" +
                "9mn7oA8k5wsE/rTYny2685Pu/GS//2+hHmJ0bGov2h8ILcqBNulQW3btBaXTcCaz1JyL1EzOohlrYRQZ" +
                "Wv8IBTI3Hljj7Ay04gWDRSZkIuVOAlkXlaTmF5AiF6hDR9w0YMOU8WxDxQrWbWCuZFbMJrQtBbNDrbQo" +
                "+2mSBpDJyJvCYP4oFK7qI5ppuOCE4voGZV1VvereG7pEWbyLCXE9o8WaOtfSVu+EP36YfI5WEDkoS50Z" +
                "nZvo1DtwvA3ro8MIQmhC4AJdbEPE1MVoOyX5rEbO6gCJ/w2kdWCjIgYAAA==";
                
    }
}
