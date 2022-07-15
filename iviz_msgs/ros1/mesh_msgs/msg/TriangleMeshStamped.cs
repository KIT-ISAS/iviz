/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class TriangleMeshStamped : IDeserializable<TriangleMeshStamped>, IMessageRos1
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
        public TriangleMeshStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Mesh = new MeshMsgs.TriangleMesh(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TriangleMeshStamped(ref b);
        
        public TriangleMeshStamped RosDeserialize(ref ReadBuffer b) => new TriangleMeshStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Mesh.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Mesh is null) BuiltIns.ThrowNullReference();
            Mesh.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Mesh.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/TriangleMeshStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "3e766dd12107291d682eb5e6c7442b9d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XXW/bNhR9168g4IckbewU6zAUGYZ9pGuXhwBFm7egMGjpSuJGkSpJJXZ+/c4lRdlO" +
                "nawPSw0Hlsh7Dy/v54kP1bLzjT/7i2RFTrTxp+jIt2n92ilpGk1XWBG8XBS//M+f4urT+3Ph9y0pZuJT" +
                "kKaSrsKxQVYySFFbWKialtxc0y1pKMmup0rE3bDpyS+geN0qL/BtyJCTWm/E4CEUrCht1w1GlTKQCArX" +
                "2dWHpjJCil66oMpBSwd56yplWLx2siNGx9fTl4FMSeLy7TlkjKdyCAoGbYBQOpJemQabohiUCa9/YIVi" +
                "dn1n53ilBn6eDhehlYGNpXXvyLOd0p/jjBfpcgtgwzmEUyovjuPaEq/+ROAQmEC9LVtxDMs/bEJrDQBJ" +
                "3EqEbaWJgUt4AKhHrHR0soNsIrSRxmb4hLg941tgzYTLd5q3iJnm2/uhgQMh2Dt7qyqIrjYRpNSKTBBa" +
                "rZx0m4K10pHF7B37GELQihHBr/TelgoBqMSdCm3hg2P0GI2lqp4rGw8XQDGbibdUK6OCgktsjWwJ4/5Y" +
                "HFn80lSqJH/zeRLwYoZL+8Bq09rP0SXKVLSGd/UAKUc1J4gVvfXxHM9peUuclNg+hoPjG62XxrpOan8q" +
                "VC0a5J85KRqyKBe3ScZ/sEg42DBpz+J5sgyD1NvVmIOd/IfE0EeBeJeZ7fl4qc+fQN1aUUwVfGG1dR/f" +
                "//H7VqbkpUdEsjOy0JOH4S8MjmVRmX6nUXF8rpAnANMQr2VJy258x8FkvHVJ8rKTDfG5CeohyDtoXujB" +
                "QxVCZXry3y/RxswpHk+1Iz+FbmwwN68nB6lR/ZnsPRCc3HHRQ4JUxscMysmbLO9ZjtO4doRq7+HiotZW" +
                "hp9+FOvpaTM93T/7lJkSMJ2JHu2mp2Z6Wk1P8vkTYDeD8+DI6R47xIHyEbFmipW1WrTS5/J4Nvc9LKMc" +
                "e9zC43WbA9KIwWDc5qGmRunjV6fi1Uns7AFNrscgrwPUHEY158ooVxR7hESMn5kYl7fz07d20Bgu3NW+" +
                "DGrMujhVJjhx6DNh5VmyA8WdDwMubTFOiV8nHwOyTjUqpnpS+BqoxMhLF3wa6eU6K6eSwRjgagJ+G+sn" +
                "Toun7vRysw9Q2TvzbYr3+4r4s3EKaWmiC/4T4TLJTO4ETUNQMK32Vkeki+iGS1Pbx+BySj1gAFs7ONlq" +
                "rcrwGAJLrqiVt8pGJjKgiNBRCaxhLK+WomO3Kgk4LZ9mbnYqzNCtUvicvctNF+ZUsOcr7bh8UBnFOnQG" +
                "vZmrhoSmhqdwGvvMbcEpbcX0RjqK3VKBV3hXnkXgZd72i7IHW2KHb+wg7mRKFD/yZXUPpwlDdyKTJR7Q" +
                "uM7fCCzUnPVzEGLnf2M64hfeDq4kCDW0MBRiyFDoFdcwdVJppnDcz9mwiJsNWRSZj02WZ1f8mRdw7V6t" +
                "SXsxn4sS9NCAt3ckDTZPUTmowPjkYfbhQHIkwU14eNguBjXTqHR45Edgrnqo6Gy3Qz30Wpvi/gZRWa4U" +
                "/juoME/HyPkd2jnt/Trx90D9nkHvBq05FxBD0yAJYMFqE8Z5/Aa8IQLtKNx8RAf4nJkXwuHUOgqlm7MB" +
                "x/GUFzHFTr7PuNnhOsUsP4yUYuRQmU9MbDCHXMsV6aL4F7VzcNBCDgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
