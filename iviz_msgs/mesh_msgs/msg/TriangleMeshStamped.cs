/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/TriangleMeshStamped")]
    public sealed class TriangleMeshStamped : IDeserializable<TriangleMeshStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "mesh")] public MeshMsgs.TriangleMesh Mesh { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TriangleMeshStamped()
        {
            Header = new StdMsgs.Header();
            Mesh = new MeshMsgs.TriangleMesh();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TriangleMeshStamped(StdMsgs.Header Header, MeshMsgs.TriangleMesh Mesh)
        {
            this.Header = Header;
            this.Mesh = Mesh;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TriangleMeshStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Mesh = new MeshMsgs.TriangleMesh(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TriangleMeshStamped(ref b);
        }
        
        TriangleMeshStamped IDeserializable<TriangleMeshStamped>.RosDeserialize(ref Buffer b)
        {
            return new TriangleMeshStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Mesh.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Mesh is null) throw new System.NullReferenceException(nameof(Mesh));
            Mesh.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Mesh.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/TriangleMeshStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3e766dd12107291d682eb5e6c7442b9d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1X32/bNhB+N+D/gYAfkrRxUqzDUGQY9iNdWj8EKNa8FYNBSyeJG0WqJBXb+ev3HSVK" +
                "dhKnfVhi2DBF3h2Pd9/xPvmQL2tf+vOPJHNyoop/00lNvuoWbpySptR0jRnB09PJdPLL//yZTq4/f7gQ" +
                "ft+b6WQmPgdpculybB1kLoMUhYWbqqzIzTXdkoaWrBvKRVwN24b8GWveVMoLfEsy5KTWW9F6SAUrMlvX" +
                "rVGZDCSCwpl2DbCqMkKKRrqgslZLBwXrcmVYvnCypmiff56+tmQyEov3F5AynrI2KDi1hY3MkfTKlFiE" +
                "cKtMePsDa0DxZm3neKYSIR88EKGSgT2mTePIs7PSX/A2r7oznsE8gkTYKPfiOM4t8ehPBPaBF9TYrBLH" +
                "cP/TNlTWwCKJW4kMrjSx5QxxgNkjVjo62TXNrl8II41N9juT4ybfY9eMhvlY8wrJ0xwC35aIIyQbZ29V" +
                "DtnVNlrJtCIThFYrJ912OmG1blMYueJgQwx6MTf4l97bTCETuVirADD64HiDmJelyp8RnY/XBNycifdU" +
                "KKOCQmhsAeyEXmAomKSxMLnKyH/5exDxYobT+8CKw9zPMTbK5LRBnHULKUcFg8WKxvq4E8BtxC0xSLF8" +
                "jEjHJ9osjXW11P5UqEKUQKNBQkqyqCC37Q7wyQJ9cGJQn8UNZRZaqcfZCMha/kuiZXSm48xsww5IffGU" +
                "3dERTlJf2JdWW/fXhz9+H4UynjokkyIySD25H36hdSyMeoXwmDHO1DVQA3Ma8oXMaFn3z7w3GW9dJ7qo" +
                "ZUm8dWfsgZkr6F7q1kMZUlk38i+Kux5FXGaHkHfkhzSmu+fL2yFQQFa38mxOP5Kn4VLG/RKkAoAZUgnO" +
                "nfsNCzKwC0e4BxrEejoptJXhpx/FZhzipkjDu5doSAMm+31xk6M9pWE5DlfjUL4IJnaRPXSZVAjxBnms" +
                "tJADjKaTlbVaVNKn0nnOWN6vsQEPOIzH84gLaURr0KVTG1RJ/PjNqXhzEvtAwF3YgAEUAXoOLZ4BlAT5" +
                "GHuMRvSfmeinx67rK9tq9CO+/r62qkdj7EOjxaS/9xmMpeazY4uvSHTFbokNZfh38qAl61QJ2EOw03ho" +
                "KUOf7E75DVOvN0m7qyb0DC40bFDF0oqt5cljvd7uW8jt2nyn5t2+Jn42Ni0tTQzDt00sOqEhpiB6SA26" +
                "295sb+oyhmJhCnvQXkLXPeYwesK4K7TKwkETLLqiSt4qcE+Ar0VZ4dqlyDb6kqsoxndU6mx306eJ250K" +
                "09arLo3Oroe7GS7l8OmBepx+VBsF3NYmXuFcRiQ0ldy7O7bAJBnE1IK1lkIigHylKhAS77LzaHqZlv1Z" +
                "1jDZ4sBvbSvWssOM75m3ukPshKG1SFSLuzqO9A8yzHrO+jmotfO/MY/xZ962LiNIlXRmKMTcofjBZo2g" +
                "WirNJJCvfXYtGk6ugFcPhG7wPgXkzzSBwzdqQ9qL+VxkoJgGLwE1SYPFUxQSKjKOPDw/kFLOKWgNtxlb" +
                "x/QmCtbtHrkV+K9uczrfvbjuhy6SO07hO6RnuVJ42cjRgPsU+h3uOqz9Or4LBGr2fLpqtWZYCE2mBBzg" +
                "xGobUgd/B7oRTe1o9IwN+XBqE1e7c/PeeEGA/VcRZ2CAL9SLdugRwDGMegrSU6+BfwxUcsi7livC03Ty" +
                "H7KT9A6bDgAA";
                
    }
}
