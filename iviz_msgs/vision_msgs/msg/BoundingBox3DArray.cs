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
            Header = new StdMsgs.Header();
            Boxes = System.Array.Empty<VisionMsgs.BoundingBox3D>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public BoundingBox3DArray(StdMsgs.Header Header, VisionMsgs.BoundingBox3D[] Boxes)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACr1WwW7bSAy9C9A/EOghSeG4QLPIIUAPWxjd5lBsFi16KYqAlmhpttKMOjOyo379Po4i" +
                "OUJTtIdtDBuWNOQj3yOHoxDL2zZU4cVb4VI81ekvz/YmGGfHpdeut6Wx1Wt3d7H59Jm27k5Cnr36nz95" +
                "9u79X1cUlhnl2TN6H9mW7EtqJXLJkWnnkKqpavHnjeylgRe3nZSUVuPQSVir54faBMK3Eiuem2agPsAq" +
                "Oipc2/bWFByFomllAaCuxhJTxz6aom/Yw8F5qKD2O8+tJHz9Bfnaiy2ErjdXsLJBij4aJDUAo/DCAdph" +
                "Eca9sfHipXrA8cPBneNeKsg+Z0Cx5qgZy13nJWiyHK40zPOR4xrwEEkQqAx0mp7d4jacEeIgC+lcUdMp" +
                "0r8ZYu0sEIX27A1vG1HkAjoA9kSdTs4eQmvqV2TZugl/hDwG+RVcewRWWuc1iteoBKGvoCMsO+/2poTt" +
                "dkgoRWPERmrM1rMf8kzdxqAAeaNiwwx+qTb45xBcYVCJkg4m1nkWotcAqS63psyz39adP9wXSvdPuthg" +
                "d4xPdZuM1SzY0laoc8FEOGtJbUnexcSA4YCKRwgIEdALp5e0+ftN0m8DIawGDOR2owaKyh6CW90N4sMq" +
                "oTFq1Rf1CkDU8qDxWlP5FCE6xSLYOajt0dTFF65klTzUsxIHLD8kWoAYa7vrY++1zceNJEpu4pBiOq9l" +
                "43Sf0pMl+ZFPni3gX9yguealCTqYb/IYxuohz9B7P62podv+K0U8mYQbSSJDzXkZ8yPsnL9IYX5jc3xP" +
                "dOwKL7qXH2qlWSq1nReQR0FQDUwkfVzerz+q8xpD5MZhaswWefZPjyp7m5CPlk9IE+nMsxZjI7JBv2qB" +
                "ZhZghFma8l6QzrNd4zhe/kF3x0sMgOny25OxOIo4U5mrhoGzkHbJQe++HkuAk6lNe+anzKbLw5ORvN8G" +
                "jzKkfVpcctN2e0bX6UByFgdaK4wa4vicXeFZGg/f1J3Yy14ggaRBVDoJhKGjIC1/ASimGWa5I+46oOGk" +
                "9mxDM6qKx/A5lXW1XtGhFjS/WuluH0/kdIibgrypDKaauqraszfTPcEVxd1LHA1NM2Y9RkNHKkqau/A4" +
                "W9P1jgbX00E54cLfvz04nZ5TZul0i86t9M1hwnhkB0CaEDBVIWCIeHP5tR74D/zMzHh/CQAA";
                
    }
}
