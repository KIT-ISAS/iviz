/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/AccelStamped")]
    public sealed class AccelStamped : IDeserializable<AccelStamped>, IMessage
    {
        // An accel with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "accel")] public Accel Accel { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AccelStamped()
        {
            Header = new StdMsgs.Header();
            Accel = new Accel();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AccelStamped(StdMsgs.Header Header, Accel Accel)
        {
            this.Header = Header;
            this.Accel = Accel;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AccelStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Accel = new Accel(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AccelStamped(ref b);
        }
        
        AccelStamped IDeserializable<AccelStamped>.RosDeserialize(ref Buffer b)
        {
            return new AccelStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Accel.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Accel is null) throw new System.NullReferenceException(nameof(Accel));
            Accel.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d8a98a5d81351b6eb0578c78557e7659";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwWrbQBC9C/QPAzkkKY4KSenB0EOgtM2hEEjoNYxXI2mJtKvuruyoX983K9tJyKWH" +
                "tsZGsjTzZt6bN3tC147YGOlpZ1NHQRoJ4oyQ8T7U1nESagIPQuxqSnaQmHgYy+KbcC2Bunwpi+uMkZHK" +
                "oiw+/eVPWXy/+7qmmOqHIbbx/VK9LE7oLqExDjUNkrjmxNR4tGXbTsJFL1t0lTuWmvLbNI8SK82872wk" +
                "fFtxErjvZ5oiopIH92GYnDVK/kj5AKCpFqLRyCFZM/Uc3oiV8fUX5eeU5bz5vEaUi2KmZNHUDAwThKN1" +
                "LV4ieLIuXV1qBhLvd/4C/6WFxMcOKHWctGN5GoNEbZbjWsu8WzhWgIdIgkJ1pLP87AF/4zmhDrqQ0ZuO" +
                "ztD+7Zw674AotOVgedOLIhvoANhTTTo9fwmtra/JsfMH/AXyucif4LpnYKV10WF4vUoQpxY6InIMfmtr" +
                "xG7mjGJ6Ky5RbzeBw1wWmrYUBciX7Mykg8yzwZVj9MZiEnV2dFnEFLRAnsuDrf+hO1vxMGGYF4vmjTj6" +
                "7DAyNKjPYbhkoRMUa4KA0MjQchP8o+hDeNCmCNJOoIpuHrs2O01Np/b9ISb5cEX7mBcP9pH/jee+8JFp" +
                "EGWKmYEqbfPL1zSrvBk32cneYRMGYUwYnI+pyKxtQC40qgCLMwlbLSuoQrWHiM4nBRn4EaACV2k6jyPQ" +
                "sOKBXewXgbOSdCZVW61o10HdHKWOWFY5b781FGxrsfyailLDMZtpT3BFqbmEp/p+6XqpBosqSvApZ5xX" +
                "dNPQ7CfaKSfchP2x42mDJved5bVI3q/0yDlgvJb11sMGkCZGbrFCLiYceRh8WTS95/TxAz0932IvDre/" +
                "yuI334YZ29UFAAA=";
                
    }
}
