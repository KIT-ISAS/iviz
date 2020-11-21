/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/VectorFieldStamped")]
    public sealed class VectorFieldStamped : IDeserializable<VectorFieldStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "vector_field")] public MeshMsgs.VectorField VectorField { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public VectorFieldStamped()
        {
            Header = new StdMsgs.Header();
            VectorField = new MeshMsgs.VectorField();
        }
        
        /// <summary> Explicit constructor. </summary>
        public VectorFieldStamped(StdMsgs.Header Header, MeshMsgs.VectorField VectorField)
        {
            this.Header = Header;
            this.VectorField = VectorField;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public VectorFieldStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            VectorField = new MeshMsgs.VectorField(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new VectorFieldStamped(ref b);
        }
        
        VectorFieldStamped IDeserializable<VectorFieldStamped>.RosDeserialize(ref Buffer b)
        {
            return new VectorFieldStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            VectorField.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (VectorField is null) throw new System.NullReferenceException(nameof(VectorField));
            VectorField.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += VectorField.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/VectorFieldStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3d9fc2de2c0939ad4bbe0890ccb68ce5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9C/QfBnpIUhwXktKDobeSNodCIKGXUsxYGklLV7vq7sqO+uv7ZmU7Sckh" +
                "h6bGRivvvDdvPmOq131s47svwrUE6vKjLHqJ3XzxTarkw5URW9M2n9eNvpRFWXz8x5+y+Hr7eUXxqaiy" +
                "eEO3iV3NoaZeEtecmBoPtabtJJxb2YoFivtBasq3aRokLhV515lI+LbiJLC1E40RVslT5ft+dKbiJJQM" +
                "In5MoFDjiGngkEw1Wg4A+FAbp/ZN4F4yv/6i/BrFVULXn1awclGqMRmImsBRBeFoXItLGI/GpcsLRQB4" +
                "t/PneJcWmT8qoNRxUsVyPwSJKpbjSt28nWNcgh5JEjiqI53m/9Z4jWcEP1Ahg686OoX8myl13oFRaMvB" +
                "8MaKMlfIA2hPFHRy9phapa/IsfMH/pnywclLeN0DsYZ13qF4VlMQxxZ5hOUQ/NbUsN1MmaWyRlwiazaB" +
                "w1QWCpudguRKkw0z4HJt8OQYfWVQiZp2JnVlEVNQB7kua/Oa3fnsaJRFKx69Gab56sajrt9/0OCjSQYd" +
                "8bfBjL2EyTxU8RUVPyPtOBkocmLjYq7CQS35RjtfDXUImiAoxsCVlEVjPacP7+n+4YhyHY6//1sU+/wd" +
                "4wiiw4IeQm/sU/pU+jJP6nWeLO8wmb0wwsMeOEKBrE0AFilYglaCYMvIgkyi2ksk53Piev4JUkGXK5yH" +
                "AWxYOYFdtJzzh7+BOZVlu1zQrhMsErXSDp1XS95GpqJgWoNlpFC46o9opn2AC0rNBXrc2ln17A3FUpbg" +
                "U0acLem6ocmPtNOYcAj7NehpA5F7ZXlMk/cLXYEHjmeaQ1s8couRdjFhBWPPvaDwfwDxhrMTSwYAAA==";
                
    }
}
