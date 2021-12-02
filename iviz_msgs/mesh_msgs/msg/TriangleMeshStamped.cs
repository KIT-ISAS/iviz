/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TriangleMeshStamped : IDeserializable<TriangleMeshStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "mesh")] public MeshMsgs.TriangleMesh Mesh;
    
        /// Constructor for empty message.
        public TriangleMeshStamped()
        {
            Mesh = new MeshMsgs.TriangleMesh();
        }
        
        /// Explicit constructor.
        public TriangleMeshStamped(in StdMsgs.Header Header, MeshMsgs.TriangleMesh Mesh)
        {
            this.Header = Header;
            this.Mesh = Mesh;
        }
        
        /// Constructor with buffer.
        internal TriangleMeshStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Mesh = new MeshMsgs.TriangleMesh(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TriangleMeshStamped(ref b);
        
        TriangleMeshStamped IDeserializable<TriangleMeshStamped>.RosDeserialize(ref Buffer b) => new TriangleMeshStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Mesh.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Mesh is null) throw new System.NullReferenceException(nameof(Mesh));
            Mesh.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Mesh.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/TriangleMeshStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3e766dd12107291d682eb5e6c7442b9d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XXW/bNhR9168g4IckbewU6zAUGYZ9pEuXhwDFmreiMGjpSuJGkSpJxXZ+/c4lRdlO" +
                "k7QPSw0Hlsh7Dy/v54kP1bLzjT/7i2RFTrTxp+jIt2n9xilpGk3XWBG8XBS//M+f4vrDu3PhDy0pZuJD" +
                "kKaSrsKxQVYySFFbWKialtxc0y1pKMmup0rE3bDtyS+geNMqL/BtyJCTWm/F4CEUrCht1w1GlTKQCArX" +
                "2deHpjJCil66oMpBSwd56yplWLx2siNGx9fT54FMSeLq7TlkjKdyCAoGbYFQOpJemQabohiUCa9/YIVi" +
                "drO2c7xSAz9Ph4vQysDG0qZ35NlO6c9xxot0uQWw4RzCKZUXx3FtiVd/InAITKDelq04huXvt6G1BoAk" +
                "biXCttLEwCU8ANQjVjo62UNms8+FkcZm+IS4O+NbYM2Ey3eat4iZ5tv7oYEDIdg7e6sqiK62EaTUikwQ" +
                "Wq2cdNuCtdKRxeySfQwhaMWI4Fd6b0uFAFRirUJb+OAYPUZjqarnysaHC6CYzcRbqpVRQcEltka2hHF/" +
                "LI4sfmUqVZL/+GkS8GKGS/vAatPaz9ElylS0gXf1AClHNSeIFb318RzkshG3xEmJ7WM4OL7RZmms66T2" +
                "p0LVokH+mZOiIYtycdtk/HuLhIMNk/YsnifLMEi9W4052Ml/SQyckONdZrbn46U+fwJ1Z0UxVfCF1db9" +
                "/e6P33cyJS89IpKdkYWePAx/YXAsi8r0e42K43ONPAGYhngtS1p24zsOJuOtS5JXnWyIz01Q90EuoXmh" +
                "Bw9VCJXpyX+/RBszp3g81Y78FLqxwXx8PTkIuRQ3nsneB4KTOy56SJAK6coZlJM3Wd6zHKdx7QjV3sPF" +
                "Ra2tDD/9KDbT03Z6unv2KTMlYDoTPdpNT830tJqe5PMnwH4G58GR0z12iAfKB07HU7GyVotW+lwez+a+" +
                "+2WUY49beLzuckAaMRiM2zzU1Ch9/OpUvDqJnT2gyfUY5HWAmsOo5lwZ5YrigJCI8TMT4/JufvrWDhrD" +
                "hbva50GNWRenygSX1Q8+E1aeJXtQ3Pkw4NIW45T4dfIxIOtUg+SGXFL4EqjEyEsXfBrp5SYrp5LBGOBq" +
                "An4b6ydOi6fu9HJ7CFDZtfk2xbtDRfzZOIW0NNEFX0W4SjKTO0HTEBRMq4PVEekiuuHK1PYxuJxS9xjA" +
                "zg5OtlqrMjyGwJIrauWtAm9Exg0oInRUAmsYy6ul6NidSgJOy6eZm50KM3SrFD5n17npwpwK9nyhHZcf" +
                "VEaxDp1Bb+aqIaGp4Smcxj5zW3BKC7rZCAnPcbdU4BXelWcReJm3/aLswZbY4Vs7iLVMieJHvqzu4DRh" +
                "aC0yWeIBjev8g8BCzVk/ByF2/jemI37h7eBKglBDC0MhhgyFDiJqBHVSaaZw3M/ZsIibDVkUmY9NlmdX" +
                "/JkXcO1ebUh7MZ+LEvTQgLd3JA02T1E5qMD45GH2w4HkSIKb8PCwXQxqplHp8MiPwFz1UNHZfoe67zXQ" +
                "M47cG0RluVL476DCPB0j5/do57T368TfA/UHBl0OWnMuIIamQRLAgtU2jPP4DXhDBNpTGCkX4uDUJu6m" +
                "K/PJYPWAfxFz6+T7zJk9klPM8sPIJUbylInERANzrLVckS6K/wCJ3I+yOw4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
