/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection2DArray")]
    public sealed class Detection2DArray : IDeserializable<Detection2DArray>, IMessage
    {
        // A list of 2D detections, for a multi-object 2D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection2D[] Detections { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Detection2DArray()
        {
            Header = new StdMsgs.Header();
            Detections = System.Array.Empty<Detection2D>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection2DArray(StdMsgs.Header Header, Detection2D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Detection2DArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Detections = b.DeserializeArray<Detection2D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection2D(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Detection2DArray(ref b);
        }
        
        Detection2DArray IDeserializable<Detection2DArray>.RosDeserialize(ref Buffer b)
        {
            return new Detection2DArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Detections, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Detections is null) throw new System.NullReferenceException(nameof(Detections));
            for (int i = 0; i < Detections.Length; i++)
            {
                if (Detections[i] is null) throw new System.NullReferenceException($"{nameof(Detections)}[{i}]");
                Detections[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in Detections)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection2DArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "5f62729a3134356dd48cf97c678c6753";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZW28buRV+F5D/QMAPtnclJU0KY5GiaJu42/VD27RJr0FgUDOUxIRDTkiOrcmv73fO" +
                "4VxkO9l9aGIE0YhDHp7rdy46UX9QzqaswlY9vVS1yabKNvi0VNsQlVZN57Jdhc17rE87Qlw/Wjxa/GR0" +
                "baLa8wctnMzI5b0pu02t2hjakLRLa+wQmsPSSFI1drfPame8iToboqZAxSYheWvzXjXa96rSvrY1tsz4" +
                "HY/VahtDA86T9TtnlPVtl8Ht5bD16eXbd7ODjxaL3/6f/xZ/fv2n5yrl+rpJu/RYtARpXmcwrmOtGpM1" +
                "+Nes4z2kNnHlzI1xOKSbFjLw29y3Jq1x8A0pwQ4yOterLmFTDqoKTdN5W5Eysm3M0XmctB6aaHXMtuqc" +
                "jtgfYm09bd9G3Riijn/JfOyMr4y6unyOPT6ZqssWDPWgUEWjSZl4qRad9fnZUzqwOHlzG1b4anbwgfFy" +
                "WExnYtYc2mgS8anTc9zxnQi3Bm0ox+CWOqkzXrvG13ROFgMLpg3VXp2B81d93gfPjnSjo9UbMmeC+Z0D" +
                "1VM6dHo+o+yZtNc+DOSF4nTHLyHrR7ok02oPmzmSPnU7KBAb4bg3tsbWTc9EKmeNz3DSTdSxX9ApuXJx" +
                "8iPpWHyYLYJPnVKoLDsqefQi5UjU2RrXtv5a3nhjE7xdHHIWCpDy0mytN2DsCAAUbIcwReCcPJp5YLKN" +
                "JT+C6/H+ykEeuyUHxKGl2nSZPMZ1NSgivC3Tsh6O3sgWCWvoOtyS3PoOiXJvQZ/UmopeqQpgobBkG70z" +
                "IAy3Aw8FIoLaGOVCxUq1YlkwSW7J+z+HVS/pZjLnRm+sA6sGaPBXhrqf+jaATLLpXzDSKzgUQENYS3IY" +
                "wm9CByCCEJtwgHvEOHwnBgQycfWLsvoiHOgM9gqBN9hEGudIp5iZEIxuNsN18Nm1WePbjtQzoiZphCI9" +
                "dIS2A1gakfh8rf4SMs587GwkSCRtOkewAT9PBvCegrIZeNqT8kzT5h68JuNTiOIlV6zqFLpYwTGbnXB9" +
                "BWCPnVmKUyPmE+1CtGVtgcIlUeSoqw+kiZnlQX4TgoMXXQ+vC8lLgTNikrAHYA8s6pWGiLAPZ4vWzcB+" +
                "uBbJhP3yRrvOIJqcEzU4+4GQq7bbLSzO6YA1gyuscTXAh5xUGQ2csbANormDRu9afl3k7UPHR1hiprAk" +
                "ncHghuPAQZVMchKM1t9ATaRSie/hDUL8q2WceYx/zo0pRfvBTPvxtXjgaMaHInc9uS0yDvIFGa7k+UKP" +
                "QxlGCfDlrHRdMxGodkZGaYRNniX3q0s4YwdTaCAM3H3fIcmvkHNqRmamCVBv4HOOXcNE4NA+dLBkayIR" +
                "VroYPoQPXStJb8yx9N8GPr9Wr4050tE/+fkKvK3xnf2vCZEdTVs32Y5MNog+oUWvxF+3yAWUZcQL7xY+" +
                "BQbUi5723iBRTCCYJ+ct8iCTDAAWtUdgvX2y+tU7cLJ1QeeLX6tUgcGJm4tLMpW5Y4bJrCVAeE+5YlMK" +
                "q5phn5IzHx3IpICEtbUHvIkG4cOiCd6WnCmXDDabgGdncBQKA1hDSgReYeoIJaEyTpnjjoasKzuF1Iib" +
                "JwRhRhwzj1Lg0wPZSp3zG8mbR/6F6pDCc5Bv0wtdFEGUpUfo4GKVQtePtwhg0C52OspgAE/LdtwWzpNc" +
                "h5pSkJsAdK9vBqWOFKaTxUZ0Y1FRL+5H4Uhh+TJwGSKKTuarwcPP3T6k+WiocIMQVBOI0j30jOBJra6M" +
                "FOIdDkQCC+SNxYKI8dYFiPw93K4a/R4KGymJZYpDXBwuEAujzDBbtIfi0CHacTssBmVnCnfKW1rccqUP" +
                "cx4FmgioQT8itS/FnaezsDuVn2eHpeqX6tNSxZBnSKT+rYjiveX/PLz8X14+HwLy7bOLdzNhvp3puNW6" +
                "r9/75lpSj0DLdXkvwI6qdq7stYINKciHDYu/dShFome6075vJSBYGdxxTEoFpuwgqy64dCTuiJSH8akf" +
                "nz59G/Yn1T0UUkf6vBNa+PZx0jthGoLryxINT7ffoqo4qmXZBe+WwVJHCARz6FCK4cih3C64X7AXnT9a" +
                "JZ+4fUeUkvQtQtwl6SNGpYFExw0oy4qHFRteIJfzZ5JCDD0F0tJmxVQIgivmfU1VHFgyB43sKC+5oWDL" +
                "SIksxKTrGPieC7akarnIhU6RuumJrxfM5143pzSr8GbocXawdN4366OCf3Ths1He87vh+GDuHJV3Hw2o" +
                "E+O3003JfjJHVzxE8uGmBUqBdHZIalDtvEI4suNYl+C268Od7/03TGWllSWbk2Hw5ZUzqPo4dRD72w4O" +
                "90xy8XohCaPMU9BDUa6iWYrxdYlLdqmSd8rR+3BrbkzsUZxQI+ulSIApxq5Vt60rfS36GP1BOMEqnC/S" +
                "DIDKyffDBAsuXTTttDdDxWHj6G+8iyIjtBltHs2BqOlRzFW8Eergc14N1R2XsLinQuWDR5F9q24N1y2c" +
                "KZ2jiAgtGuBhEIjahsdPIqL6x5W4KG6I8KjWkJvL7VCZQtHtjalL58N0uaWi6VDcAtlYm61D5TapbI1O" +
                "pObuak6Hqwsq8TaG1Uo9QLEERMvoINOg57GbJaIwGTt4fxoNpzyH9A8NAR8sGjaUubjptBbmQDAaS3sa" +
                "KtZsmS5O5RtXeElk7jI9A0TyqRSf0zzNUfEX61G4PIa4ke6gwVGxS69vlUG0xbGU5KJp6KDpJlKuYSTU" +
                "jvqfvohL72S6RAYaEbYOpSUFPcgGww7pZhi9lQLuwYxP5TyPcNBt2W1w9c9kGjCX9Vebld6dOwyS3Bsw" +
                "QG6UntBrmS3asvvsyVI9OecBG02G2pUzW2poo5c+o+xbHM+BVPk7UWV5GmOO7ZLSFVJEUSIP90Zy6qG/" +
                "kdYw0puRosBFNJeuBXQqfEb9OUIw184y0MiB+4SmRurLlL4/jC1zmZ1Jk8kT99JxflGm7/tjAnW49b/s" +
                "4Kfjg4JyoYBcSUpfpHAle0Z1SpRIvEyrhdJLVgP19J8jN0bc8SB24oObeuB2/hwFTqMGQGIDD4SRQKXZ" +
                "XAzjccA2KXY6IoRleTmMyJfKd81GzBfDbRpO39oa/Nw7zcsPHq6C6xqfFiX1O7ODa5TCiNABTXxgcC5V" +
                "1tYCVFOsHjPh6+F1WldtO828brU4Sio/W1BBoYHzqLHKzJqzzFK9h2FxLIa0AiTH9Hua06S1jA6xaWfW" +
                "nuZBnkZv+I9KsUZbV4aZMgYmugMjgKJyxcj5oIo/DgvUzHBlo1YrVe2198hhjQGY+d1SWkF+okrkYUOS" +
                "JZGT/TQhHH66ksspIw/D7MdzhLqrtb3Y/QeaAG4siooazWCxXJpN/8d3vxt/RsmmPWLoRyo14Auwod9l" +
                "Gk+qTZ+NuMYP9MMVEZodQEVLiVaaaH4rItPNZ0z+O/at88Xifwgwz/vsGwAA";
                
    }
}
