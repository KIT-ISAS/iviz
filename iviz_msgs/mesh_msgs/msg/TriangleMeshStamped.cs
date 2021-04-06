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
            Mesh = new MeshMsgs.TriangleMesh();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TriangleMeshStamped(in StdMsgs.Header Header, MeshMsgs.TriangleMesh Mesh)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
                "H4sIAAAAAAAAE71XXW/bNhR9168g4IckbewU6zAUGYZ9pGuXhwDFmrdiMGjpSuJGkSpJxXZ+/c4lRdlO" +
                "nawPSw0Hlsh7Dy/v54kP1bLzjb/4g2RFTrTxp+jIt2n91ilpGk03WBG8XBQ//c+f4ubj+0vhDy0pZuJj" +
                "kKaSrsKxQVYySFFbWKialtxc0x1pKMmup0rE3bDtyS+geNsqL/BtyJCTWm/F4CEUrCht1w1GlTKQCArX" +
                "2deHpjJCil66oMpBSwd56yplWLx2siNGx9fT54FMSeL67SVkjKdyCAoGbYFQOpJemQabohiUCa+/Y4Vi" +
                "dru2c7xSAz9Ph4vQysDG0qZ35NlO6S9xxot0uQWw4RzCKZUXp3FtiVd/JnAITKDelq04heUftqG1BoAk" +
                "7iTCttLEwCU8ANQTVjo520M2EdpIYzN8Qtyd8TWwZsLlO81bxEzz7f3QwIEQ7J29UxVEV9sIUmpFJgit" +
                "Vk66bcFa6chi9o59DCFoxYjgV3pvS4UAVGKtQlv44Bg9RmOpqufKxuMFUMxm4i3Vyqig4BJbI1vCuD8W" +
                "Rxa/NpUqyX/6axLwYoZL+8Bq09qP0SXKVLSBd/UAKUc1J4gVvfXxHM9peUeclNg+hYPjG22WxrpOan8u" +
                "VC0a5J85KxqyKBe3TcZ/sEg42DBpz+J5sgyD1LvVmIOd/IfE0EeBeJeZ7fl4qS+fQN1ZUUwVfGW1dX++" +
                "/+3XnUzJS4+IZGdkoScPw18YHMuiMv1eo+L43CBPAKYhXsuSlt34joPJeOuS5HUnG+JzE9RDkHfQvNKD" +
                "hyqEyvTkv12ijZlTPJ5qJ34K3dhgPr2eHKRG9Wey90hwcsdFDwlSGR8zKCdvsrxnOU7j2hGqvYeLi1pb" +
                "GX74Xmymp+30dP/sU2ZKwHQmerSbnprpaTU9yedPgP0MzoMjp3vsEEfKR8SaKVbWatFKn8vj2dz3sIxy" +
                "7HELj9ddDkgjBoNxm4eaGqVPX52LV2exswc0uR6DvA5QcxjVnCujXFEcEBIxfmZiXN7NT9/aQWO4cFf7" +
                "PKgx6+JUmeDEsc+ElWfJHhR3Pgy4tMU4JX6dfAzIOtWomOpJ4UugEiMvXfBppJebrJxKBmOAqwn4bayf" +
                "OC2eutPL7SFAZdfm6xTvDxXxZ+MU0tJEF/wnwnWSmdwJmoagYFodrI5IV9EN16a2j8HllHrAAHZ2cLLV" +
                "WpXhMQSWXFEr75SNTGRAEaGjEljDWF4tRcfuVBJwWj7P3OxcmKFbpfA5u85NF+ZUsOcL7bh8VBnFOnQG" +
                "vZmrhoSmhqdwGvvMbcEpbcX0RjqK3VKBV3hXXkTgZd72i7IHW2KHb+0g1jIlih/5srqH04ShtchkiQc0" +
                "rvM3Ags1Z/0chNj5X5iO+IW3gysJQg0tDIUYMhR6xTVMnVSaKRz3czYs4mZDFkXmY5Pl2RW/5wVcu1cb" +
                "0l7M56IEPTTg7R1Jg81zVA4qMD55mH08kBxJcBMeHraLQc00Kh0e+RGYqx4qutjvUA+91qa4v0FUliuF" +
                "/w4qzNMxcn6Pdk57P0/8PVB/YNC7QWvOBcTQNEgCWLDahnEevwFviEB7CiPlQhyc2sTddGU++TTCv4i5" +
                "dfZt5sweySlm+WHkEiN5ykRiooE51lquSBfFv4ncj7I7DgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
