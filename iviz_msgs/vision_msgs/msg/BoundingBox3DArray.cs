/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class BoundingBox3DArray : IDeserializable<BoundingBox3DArray>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "boxes")] public VisionMsgs.BoundingBox3D[] Boxes;
    
        /// Constructor for empty message.
        public BoundingBox3DArray()
        {
            Boxes = System.Array.Empty<VisionMsgs.BoundingBox3D>();
        }
        
        /// Explicit constructor.
        public BoundingBox3DArray(in StdMsgs.Header Header, VisionMsgs.BoundingBox3D[] Boxes)
        {
            this.Header = Header;
            this.Boxes = Boxes;
        }
        
        /// Constructor with buffer.
        internal BoundingBox3DArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Boxes = b.DeserializeArray<VisionMsgs.BoundingBox3D>();
            for (int i = 0; i < Boxes.Length; i++)
            {
                Boxes[i] = new VisionMsgs.BoundingBox3D(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new BoundingBox3DArray(ref b);
        
        BoundingBox3DArray IDeserializable<BoundingBox3DArray>.RosDeserialize(ref Buffer b) => new BoundingBox3DArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Boxes);
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
    
        public int RosMessageLength => 4 + Header.RosMessageLength + 80 * Boxes.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/BoundingBox3DArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9e1a3932308592aa1b20232d818486db";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WwW7cRgy96ysI+GC7WG+AuMjBQA8NFml9KOoiQS9FYXAlrjSNNLPhjHatfH0fRyvZ" +
                "Qp20h8YLAStpyEe+Rw5HMVX3Xazjq5+FK1Fq8l9xcNEFP668Db2vnK/fhofrzR9/0jY8SCx++J9/xS/v" +
                "f7qhuEynOKP3iX3FWlEniStOTLuANF3diF61cpAWTtztpaK8moa9xDUcPzQuEq5avCi37UB9hFEKVIau" +
                "670rOQkl18nCH57OE9OeNbmyb1lhHxQCmPlOuRNDxxXlUy++FLrd3MDGRyn75JDQAIRShSNEwyIVvfPp" +
                "+rU5FGcfjuEKj1JD7Dk4pYaTJSsPe5VoeXK8QYzvRnJrYEMcQZQq0kV+d4/HeEkIghRkH8qGLpD53ZCa" +
                "4AEodGB1vG3FgEsoANRzczq/fIJsad+QZx8m+BHxMcZ/gfUzrnG6alCz1tjHvoaAMNxrOLgKptshg5St" +
                "E5+odVtlHQrzGkMWZ+9MYxjBK1cE/xxjKB0KUNHRpaaISQ09V+PeVcU36sYv7gLw/JGuN9gK40vbE2MN" +
                "S/a0FdqH6BJ8rZC+Ig0pJ89wQJ0ThAN7dMDFG9r8+s5020ABb+Eihd3I3kBZobO35heNqwzGKFFfNivg" +
                "UMeDhetcrTlACoAimAWIrGji8iPXssoO5lhLAJQOmRMQxoru+tQr2jrvGjFiU/45YFCrFefnnJssiY9c" +
                "igX2qzv007Rywo3uszwHsHrKMPaq05oZhu1fUqbzSbFMD9kh22W832EV9DoH+Vb98E+GuRFUbNM+lcgS" +
                "NFI7FdBGFVACTB17XZ3Wn5V3TcVdwHSYDYrfetRVfcZ9tHspgkhlmqSYDYkdutOKMucPLhiVOeUF3WLX" +
                "Bk5vvqeH+W6Y7z6/TPqP0k0c5kJhoiz0XCZvT58edcd502FvfJ3RdHd8GW6nbn+OGB3y2pISGuuMbvMR" +
                "EzwOqE4YJcNZOHvCsXIK19yG2K4qIC55ylRBImGkAKPjj4DEpBLz5v0eYDh0lX1sRynxGi4Xsq7XKzo2" +
                "giY3K9vP+XTN57ErSV3tMLDM0xSenZlO5FaUdq8x79t2zHkMhvYDSB6ocLhc0+2OhtDT0QjhRk+fAcHm" +
                "4pRXPq5SCCv7BjhBPNPrkCVGzEtoFxM+QP616n8DdVxpJD4JAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
