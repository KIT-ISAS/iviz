/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshTexture")]
    public sealed class MeshTexture : IDeserializable<MeshTexture>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
        [DataMember (Name = "texture_index")] public uint TextureIndex { get; set; }
        [DataMember (Name = "image")] public SensorMsgs.Image Image { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshTexture()
        {
            Uuid = "";
            Image = new SensorMsgs.Image();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshTexture(string Uuid, uint TextureIndex, SensorMsgs.Image Image)
        {
            this.Uuid = Uuid;
            this.TextureIndex = TextureIndex;
            this.Image = Image;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshTexture(ref Buffer b)
        {
            Uuid = b.DeserializeString();
            TextureIndex = b.Deserialize<uint>();
            Image = new SensorMsgs.Image(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshTexture(ref b);
        }
        
        MeshTexture IDeserializable<MeshTexture>.RosDeserialize(ref Buffer b)
        {
            return new MeshTexture(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uuid);
            b.Serialize(TextureIndex);
            Image.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (Image is null) throw new System.NullReferenceException(nameof(Image));
            Image.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
                size += Image.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshTexture";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "831d05ad895f7916c0c27143f387dfa0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VVTW8bNxC9768YQIfYiSUH7SUwUKRF07Q6BCiQ3IpCGC1Hu2y55Jof+siv7yOplaza" +
                "TnJoF7LkJTlvPt6b4Yw+SOjppxi9Xqco+TVwJ03Agu0oJa2apG38/juKso/Jy0pbJfsmiA3Or4bQhdvl" +
                "ABPS+btpfviPn+bDx1/v6JG7Zkafeh1oqAFT62xkbQOxpWRbN4weO6KOYc3o6vUNvb4mmHCk6Ma5kU2E" +
                "mbfiyW2mc03zm7DCUl9/js+MjstRw2PkYaTQu2QUrYW4vU866KidLftnOHrqOWFtPA+op3oA5caoWzZ1" +
                "K+O0+PX8HJDzutM2n6sGj4FasbEm+GWkV/vJeHTgGxWi2AsBv48EF/nlizm9OlwCKLez32b4+dIQfy67" +
                "Hw3bUoKvIizrmVM5e/EgxarL1SPSz6UMS7txz8FNkuIQXKs5QkQ7HftzHFlsG6Pb+BxCPrmWnrfa+ay4" +
                "hJbZaCuqmZqpl1LYs0kFrss3AIBIdbghm4Z1pc+7XZisd1ohnkfWZflJ49aZNNjQlK4RMtJBGls2SQJt" +
                "EKOgY1RueEblQNpGG4jJt7cFeDVth0U7jk0p+MEl2nEVCvrBKvZKf0bRyMqOjuMD0AMjnb9ALMy8C/MU" +
                "xIcfjQ4xLIJLvhUc6mRhJRbK0Ogq97AMrA2N3o0ulMAK7hTIopkm1CnyqRS/TAtIe9R7MYHmc2p7tlYM" +
                "uGWLzRt0Djqw/BcQ9tNEZib5b0E5vBsKqTnuDFydh1wqbVuTlNw+nFD/rlpfeX8DVlZr3SFFjRQrcwHA" +
                "+FIcmU57byemQ5TxIqD3yZisBXBoO4gAEawPUao03vzxZwV6YMBtTCAbPHi9L7s15ez5qsC/LNq6/t9G" +
                "d1S1KnXqQQcfj3oBG5FLvFmDPaQvHmN5C57KiEXjld14GCUspomPD6qEHjbmQCmPeEgQA39IFhMPd9hp" +
                "RE/2sESZmEb2GIrJsMd5CEDbfLwMiIyOT5D7BNqElu/ucpcHaVPUCOiQefbCRYvLd3TiR+6b2aedm+NV" +
                "uov74diGJPvpKuJwBx8va3ILYOd7DV5UYQJrK7yGa9CTQ5DRtT1dIfLfD7F3dZJu2Wtem8IexrsB6ots" +
                "9OL6AbIt0Jatm+Ar4tnHt8DaE27OaY4GUiZnH1LHZaqhObda4ej6UEBao3HZoEfWnv2hKTdhcdnM3pfb" +
                "6Sz0fAtfTtepn6dx3TT/AOsDVU+cCAAA";
                
    }
}
