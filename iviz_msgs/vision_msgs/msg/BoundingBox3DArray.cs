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
                "H4sIAAAAAAAAE71WwW7TQBC9W8o/jNRDW5QaiSIOlTiAIqAHRBGIC0LVxJ7YC/au2V0nNV/Pm3Xi1qII" +
                "DtDIUmzvvDfzZmZnHWJ53YYqPH4jXIqnOv0tsq0Jxtlx6aXrbWls9dLdnK8+f6G1u5GQPf/Hv+zth9cX" +
                "FObxZEf0IbIt2ZfUSuSSI9PGIU5T1eLPGtlKAxC3nZSUVuPQScgB/FibQLgqseK5aQbqA4yio8K1bW9N" +
                "wVEomlZmeCCNJaaOfTRF37CHvfNIgJpvPLei7LiCfO/FFkKXqwvY2CBFHw0CGsBQeOGApGGRst7YeP5E" +
                "AdnRx507w6NUyPbknGLNUYOVm85L0Dg5XMDHo1FcDm4kR+ClDHSS3l3jMZwSnCAE6VxR0wkivxpi7SwI" +
                "hbbsDa8bUeICGQDrsYKOT+8w20Rt2boD/ch46+NvaO3Eq5rOatSsUfWhr5BAGHbebU0J0/WQSIrGiI3U" +
                "mLVnP2SKGl1mR680xzACKlUE/xyCKwwKUNLOxDoL0St7qsa1KbP/1I2/3QXQ+YLOV9gK40vdE2MNC7a0" +
                "FupcMBFYLaQtybuYgmcAUOeIxEE9OuDkGa3evTpdgHCFFFj1F8htRvnKyl60IdH94sMysTFq1Bf1EkTU" +
                "8qD+WlP55CE65SLYOaTZo42Lb1zJMiEUWYkDlx+SqqVSazk2fey95ItskXaOqLiDhuTTea0Xp+cUnszF" +
                "j3oW2Yz+8RWaalo6UAfzQ+7jWN7VGXrvD2tq6NZfpYjHYWJTkYhQY577/AQ758+Tm8X/6oxfZaaW8KLb" +
                "926iNETVtfEC5agGSoH5o6/L/fq9Sc4pu3KYE5NB9r5Hgb1NvLd2DyUQoRxmKqZEZIM21bpM8UMLhmYK" +
                "eSY32zSO47OndDPdDdPdj4cJ/zZ1Bw1ToTBbZvmcB69P32/zjpOnzbM/KDrc7R5G277d7xNG27Q2l5Tr" +
                "0XWZDhtncVS1wigZTsUJCWBpPKCpDbFjvUC4pHFTOgmE0QKOlr+BEiNLFM1dBzIcv55taMZU4jUgJ5JX" +
                "+ZJ2tdjRSrd0OmfTyWwK8qYy5YjUDE9gpr24JcXNE0z+phljHp2h/UCSRisApzldbmhwPe1UEG78/oPA" +
                "6YA8xJUOrujcUr8G9hT39DrSEgLmJnIXIj5F/lj1n+2LN+lJCQAA";
                
    }
}
