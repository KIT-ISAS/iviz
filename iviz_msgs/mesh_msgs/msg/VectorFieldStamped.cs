/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/VectorFieldStamped")]
    public sealed class VectorFieldStamped : IDeserializable<VectorFieldStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "vector_field")] public MeshMsgs.VectorField VectorField;
    
        /// <summary> Constructor for empty message. </summary>
        public VectorFieldStamped()
        {
            VectorField = new MeshMsgs.VectorField();
        }
        
        /// <summary> Explicit constructor. </summary>
        public VectorFieldStamped(in StdMsgs.Header Header, MeshMsgs.VectorField VectorField)
        {
            this.Header = Header;
            this.VectorField = VectorField;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal VectorFieldStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
            if (VectorField is null) throw new System.NullReferenceException(nameof(VectorField));
            VectorField.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + VectorField.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/VectorFieldStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3d9fc2de2c0939ad4bbe0890ccb68ce5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9L/g/DOQQpzgqJKUHQ28lrQ+FQEIvpZixNJKWrnbV3ZUd9df37cpRk2JK" +
                "D02NwCvtvDdvPkOstl1owuuPwpV4avOf6iS00/fPUkbnb7SYivb5vK3Ti1qod//4t1Cf7j6sKTyXtFBn" +
                "dBfZVuwr6iRyxZGpdtCqm1b8pZG9GKC466WifBvHXkIB4H2rA+FpxIpnY0YaAoyio9J13WB1yVEoasT7" +
                "FA+ktsTUs4+6HAx72DtfaZvMa8+dJHY8Qb4PYkuhzfs1bGyQcogagkYwlF44aNvgktSgbby+SgB1dn9w" +
                "l3iVBhmfnVNsOSax8tB7CUknhzV8vJqCK8CN7Ai8VIGW+dsWr+GC4AQSpHdlS0sovx1j6ywIhfbsNe+M" +
                "JOISGQDreQKdXzxhTrLXZNm6R/qJ8ZePv6G1M2+K6bJFzUyKPgwNEgjD3ru9rmC6GzNJabTYSEbvPPtR" +
                "JdTkUp3dpBzDCKhcEfxzCK7UKEBFBx1bFaJP7LkaW/2CDXlyGBaqEYd29ON0detQ0C9fqXdBR41G+O1+" +
                "gl7DYpqi8HJ6TwhbPM4CqhtZ25Dz/6iVXJ2aPRmmvq+9oA49l6Jq4zi+fUMP82mcTz/+VwTH1M0xeEnz" +
                "gc5BTxyz+Vx2kSZzk2fJWUxiJ4zIMPQzEsBKe0ARfQFW8YKNIivSkSongayL4Oj4GygFjU1Ac9+DDNvF" +
                "sw2Gc+bwGZClFE2xokMr2BrJKjVmXiN58eiSvG409k5CwlE3g5mO0a0o1ldobGMmzZMzVAkk3sUMuCho" +
                "U9PoBjqkgHDwx33naAeJR115LqNzq7TsjhQneiL1deAGI2xDxKot1J/LvVA/AbRA7ictBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
