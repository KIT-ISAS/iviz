/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/PoseArray")]
    public sealed class PoseArray : IDeserializable<PoseArray>, IMessage
    {
        // An array of poses with a header for global reference.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "poses")] public Pose[] Poses { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PoseArray()
        {
            Header = new StdMsgs.Header();
            Poses = System.Array.Empty<Pose>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseArray(StdMsgs.Header Header, Pose[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PoseArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Poses = b.DeserializeStructArray<Pose>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PoseArray(ref b);
        }
        
        PoseArray IDeserializable<PoseArray>.RosDeserialize(ref Buffer b)
        {
            return new PoseArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Poses, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Poses is null) throw new System.NullReferenceException(nameof(Poses));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += 56 * Poses.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "916c28c5764443f268b296bb671b9d97";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvbMBC9G/wfBnLY3dKk0JYeAj0slH4cCim7t1LCxB7bAlvySnKy7q/vG3ljN6f2" +
                "0G4wRLLevJn3ZuQV3Vpi73kkV1HvggQ6mdgQUyNciqfKeapbd+CWvFTixRayybM8+zydTzB9sUP09x8T" +
                "ie7f/+Nfnn29+7SlEMt9F+rwaiogz1Z0F9mW7EvqJHLJkVPVjakb8etWjtIiirteSkqncewlQMSK7hsT" +
                "CE8tVjy37UhDACo6KlzXDdYUHIWi6eSCQEMNjKOefTTF0LJHgPOlsYqvPHdq0ioBgzwM6hp9+bAFygYp" +
                "hmhQ1AiOwgsHY2scAjwYG9+81ggE3p/cGnup4fJcAcWGo1Ysj72XoMVy2GqaF5PGDehhkiBRGeg6vdtj" +
                "G24IeVCF9K5o6Brl78bYOAtGoSN7w4dWlLmAD6C90qCrm9+ptfQtWbbuzD9RLkn+htcuxCpr3aB5rVoQ" +
                "hho+Atl7dzQlsIcxsRStERupNQfPfswzDZuSguSjmg0Y4lJv8M8huMKgE2Ua5jwL0WuC1Je9Kf/jdNbi" +
                "MIR+nEZUr4TqvMXd0XZBBUcDb54umw5R5QViei7kpQ6dvi6fzk3Cwh5y3pxjN5iTncNgzIg8+zZArLeJ" +
                "eUE+o0yUM18nTEZkY0Nq3awCinBdUt0XovOsah3Hd2/pcVmix+flz2dTsZg4S5m7hpm6sPZSg+4elhbg" +
                "49OlT+QflZ2XJ0X/AjexBgiMBQAA";
                
    }
}
