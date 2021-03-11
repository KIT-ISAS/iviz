/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/BoundingBox3DArray")]
    public sealed class BoundingBox3DArray : IDeserializable<BoundingBox3DArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "boxes")] public VisionMsgs.BoundingBox3D[] Boxes { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public BoundingBox3DArray()
        {
            Boxes = System.Array.Empty<VisionMsgs.BoundingBox3D>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public BoundingBox3DArray(in StdMsgs.Header Header, VisionMsgs.BoundingBox3D[] Boxes)
        {
            this.Header = Header;
            this.Boxes = Boxes;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public BoundingBox3DArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Boxes = b.DeserializeArray<VisionMsgs.BoundingBox3D>();
            for (int i = 0; i < Boxes.Length; i++)
            {
                Boxes[i] = new VisionMsgs.BoundingBox3D(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new BoundingBox3DArray(ref b);
        }
        
        BoundingBox3DArray IDeserializable<BoundingBox3DArray>.RosDeserialize(ref Buffer b)
        {
            return new BoundingBox3DArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Boxes, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Boxes is null) throw new System.NullReferenceException(nameof(Boxes));
            for (int i = 0; i < Boxes.Length; i++)
            {
                if (Boxes[i] is null) throw new System.NullReferenceException($"{nameof(Boxes)}[{i}]");
                Boxes[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += 80 * Boxes.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/BoundingBox3DArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9e1a3932308592aa1b20232d818486db";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WwW7bRhC9E9A/DOCD7UJWgLjIwUAPDYS0PhR1kaCXojBG5IjchtxVZpeSma/vm6VE" +
                "m4jT9tBYICCSO/Nm3pvZWcZU3Xexjq9+Fq5Eqcl/xd5FF/y48jb0vnK+fhsertd//Emb8CBxUfzwP/8W" +
                "xS/vf7qhOE9oUZzR+8S+Yq2ok8QVJ6ZtQKaubkSvWtlLCy/udlJRXk3DTuIKjh8aFwlXLV6U23agPsIo" +
                "BSpD1/XelZyEkutk5g9P54lpx5pc2bessA8KDcx8q9yJoeOK8qkXXwrdrm9g46OUfXJIaABCqcIRumGR" +
                "it75dP3aHIqzD4dwhUepofcUnFLDyZKVh51KtDw53iDGdyO5FbChjiBKFekiv7vHY7wkBEEKsgtlQxfI" +
                "/G5ITfAAFNqzOt60YsAlFADquTmdXz5BtrRvyLMPJ/gR8THGf4H1E65xumpQs9bYx76GgDDcadi7Cqab" +
                "IYOUrROfqHUbZR0K8xpDFmfvTGMYwStXBP8cYygdClDRwaWmiEkNPVfj3lXFN2vIr24Fa80f6XqNDTG+" +
                "tZ0xlrFkTxuhXYguwdlq6SvSkHL+DAeUOkE7CIAmuHhD61/fmXRriOAtXqSwHQUwUFZI7a3/ReMygzGq" +
                "1JfNEjjU8WDhOldrDpACoAhmATor+rj8yLUss4M51hIApUMmBYSxqNs+9YrOzhtHjNgp/xwwqJWL83PO" +
                "TebERy7FDPvVHVrqtHLEje6zPAewfMow9qqnNTMMm7+kTOcnxTI9ZIds5/F+h1XQ6xzk27XElxzHXlCx" +
                "rftUJcvReG1VwByFQBUwe+x1dVx/VuEVFXcBM2IyKH7rUVr1GffR7uU4IpnFaaRiSCR26FErzUQBdDAz" +
                "c9YzxsW2DZzefE8P090w3X1+KQaP+k00pnJhusxUnedvT58e1cfZ02GT/DOp093hpegdG/9ZbrTPi3NW" +
                "6LAzus0nTvA4rzphFA5H4+QJx8opXHM/YuuqgLvkiVMFiYTxAoyOPwISUwsDOxDvdgDDGazsYzuqiddw" +
                "uZBVvVrSoRF0u1nZ3s6HbT6eXUnqaofhZZ4m8uTMdGS3pLR9jfHftmPOYzA0IUDycIXD5YputzSEng5G" +
                "CDd6/CoINiNPeeXTK4WwtE+CI8QzLQ9ZYsTshHYx4YPk3wq/KP4Gg2OLUlEJAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
