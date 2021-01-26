/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection3DArray")]
    public sealed class Detection3DArray : IDeserializable<Detection3DArray>, IMessage
    {
        // A list of 3D detections, for a multi-object 3D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection3D[] Detections { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Detection3DArray()
        {
            Header = new StdMsgs.Header();
            Detections = System.Array.Empty<Detection3D>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection3DArray(StdMsgs.Header Header, Detection3D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Detection3DArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Detections = b.DeserializeArray<Detection3D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection3D(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Detection3DArray(ref b);
        }
        
        Detection3DArray IDeserializable<Detection3DArray>.RosDeserialize(ref Buffer b)
        {
            return new Detection3DArray(ref b);
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
        [Preserve] public const string RosMessageType = "vision_msgs/Detection3DArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ce4a6fa9e38b86b8d286a82799ca586d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZbW/cuBH+vkD+A3H+EPuwu73YVzdIYRSXLNIYuCZp415fgsDgStxdXiRRISnbyq/v" +
                "MzOkpHXWvX5oYiSORA2H8/rMDHOkflKVDVG5jTpbqdJEU0TrmjBXG+eVVnVXRbtw61+xPlI4v3w0ezR7" +
                "ZXRpvNrxP7RwNGEXdyZRm1K13rUu6CosQSE889LAUtV2u4tqaxrjdTTETYGLDcLy1sadqnXTq0I3pS1B" +
                "MpF32FaqjXc1JA+22VZG2abtIqRdZdKz1fsPk42PZrOL//PP7C/v/vxMhVhe12EbfidWgjbvIgTXvlS1" +
                "iRrya7bxDlobv6jMjamwSdctdOCvsW9NWGLjFRnBZh2rqlddAFF0qnB13TW2IGNEW5u9/dhpG1ii1T7a" +
                "oqu0B73zpW2IfON1bYg7/gTzqTNNYdTl6hlommCKLloI1IND4Y0mY+KjmnW2iWentGF2dHXrFng1W8TA" +
                "cDg8piMJa+5abwLJqcMznPG9KLcEbxjH4JQyqGNeu8ZrOCGPQQTTumKnjiH52z7uXMOBdKO91WtyZ4D7" +
                "qwpcH9OmxycTzg2zbnTjMnvhOJ7xv7BtBr6k02IHn1Wkfei2MCAIEbg3tgTpumcmRWVNExGka699P6Nd" +
                "cuTs6CXZWGKYPYJ/dQiusByoFNGzED1xZ29c2/JrReONDYh2CchJKkDLldnYxkCwPQBQ8B3SFIlz9ChH" +
                "oLmLhqyq1RoBURB9UUEfu6EApE1rjpeqK0klZLflVdsgzmummEtWw9Tulmj0fQ5ybAKf0JqCPk1ZKY3f" +
                "td7CrC5BhFNroypXsFGteBbBTmHJlA9h1Qs6mty51mtb4QBkm1o52KJxUe30DZ2RFDJq17cOnAM+s3RV" +
                "RWIFi/ARORJI2hLgSSKEwvlMDNTCOvMlMKOk8IZioaslkaHBD5DzDfN4lY6y4R8IkbcIZ0CWWCaI6DD9" +
                "2nUN23nt7hCc3ud3OltkAcPnafW5u6M9oBUGVyAifzPOUMaO+MlK5uOQMUuzxNuWzD9gdoEHwhnXEdZn" +
                "qDZi75NlQqzR7xT5pLwH0FhPIJ1MmMEfNguwdQTC98IOFjF1G3soEUwTnJfgfesAOS8q15VAIdf5wlwX" +
                "9CZqXW4k2wBGgWLkVgcVvS4+kmFEpblQbKypSuBQxPeOQmTtXAUxrzN5YrgSrCV5CRjJeU3RKw0LIHi4" +
                "lLXVpBLlo0Mywo2uOshhq0rUquxHgtXSbjYIR65VbLhyIhFi2GiAoIXrADUdDH4/MJZJ2951osSg05ws" +
                "h3jg4EXFdcJyVIzWr0TnBD75C/Dnq5XDKQA9FOXUPzQ5j3bDZwlQWD9qi0p/CFeWY1SjHKKYkeNSE5L4" +
                "MdDAKQ6hHpUuS2YC007DVCOr4qTzuFwhLju4AnFkkQ27Dh3IAgWx5LLBPFFxajOXvEYOoersXAdPtsYT" +
                "Y6WT45372LVSkYcGgH4BTJFh74zZs9Ev/HwJ2ZZ45/irgScUaNpWo+9sOao+QlmvJF43KFRUAiUK73dl" +
                "CSXU855ob1DFRoiOY/AmfVDmMrp63SC53v+wePIBkmwqp+P5jwJ4ozTnK3KVueeG0a0pQZgmHbFOUFpy" +
                "TaLOgbdmNsGhmm7sHb54g/Rh1VoCBJUKuhySfTbi0tZgKwxWqAJaIvGSUHsgCpNxPR8oavKuUE4hnnPv" +
                "taOWiwIzDlokkEtN2B+lqO/FF1pXSs+s3zphHTo0aiEG6OBOmlK3GU4RwCAqDjpDhcuUlv24SZIHOQ4N" +
                "rwA7sJTLWDLqwGHcmXxEJyYT9Rlmg6G0fOG4RxJDB/PV4OG3Ts89iDfUVUIJakPE6A3sjOQJrS6MTAkd" +
                "NngCC1SP2YyYMekMTP7mbhe1/hUGGziJZ1JAnN+dIxcGneE2b+9SQDtvB3J4DMaOlO6oD5CFw3Kh76Yy" +
                "CjQRUIO/R98hjcFkL/xOvfHx3Vz1c/V5rryLEyRS/1TE8Yvlfx1e/jcvn+SEfH92/mGizLdzHc+BX9r3" +
                "S3fNaYCh5TJ9F2BHyz019lLNuOoPBLO/duhUfMN8R7pvpSBEyeE4FKUEUzbrqhMu7ak7IOXd8NQPT5+/" +
                "jfij6Q6l1J4976UW3j6NdidMQ3L9d43y0+236Cr2Wl0OwftdsvQRAsHZWVRlEG6cSfTMiUSlPpWB43O1" +
                "evPyhCB0hcmuCXzb4FKfSVypk7fU91FGz5kb6hZVoHlqaOm82m5Te53GFoypCBoPNCg+AvTHPmPPX/Nc" +
                "cjddRE+33OveH0qYg9VN9DmE88OnzDrYz+YQj/lUz8Mjx+MwcOPCBvZf1JZfuOM/42O+UUFJRx6sIjdy" +
                "+7QX7Eu6FbnkewzXoF2vjUYyo3EedmJjiUmmEICC1dCRoP1hl5dphgSPWn8ES4SNtONtC2aaGu4mVOKr" +
                "yGPPsVlul3N1uzONUMl0DA48mKFx8XaLIYF3jv0E8VRJORSXzSlPGiKzHAbXgEkuFBjN0tBwSwrhwacZ" +
                "kEfQLBc3t9G5ORc3YXEABYcxCwgY0RP/Jh58HVc/MB5mZ2chd67iy4vCVVWa1RDjrxdlTmtdCWgH8oMt" +
                "6LaxB5OE8Q+NDDltG1qpOGEj8Yso6CYWHBvgIuUg3wIFOEz6W63WttEeGFG59ZyRp9I9gVBpQuHterho" +
                "SqJw1Kbk/I4nvvAdMMhr6nXSHMBn8Vg8NoJramC2ukHSleq0xGBPs/qC5tETan2flNSIdA03KqZEpLwd" +
                "2YTJXgiN7bw7ZM6YPMquYFFJTLQ3HvlhWjRj4p0w2AmR4o3jZpvu19xmsano4lekp8sz6vR5kwivi0+d" +
                "FaATdOUm/d5FJs17x9QHn5XZ2OFkuX/rQwecriCA7woC02zFibmWcoNgkvHgqcEgc3DhC2obwOcJS3Jr" +
                "S2hopfxXptni7QDTfGsqDPIbb57xDZw4WtgUO900pgpZVetzQKRKkOKFbUNBs5Tu6CWFwvsPcgkQZnKf" +
                "gR9M/msLECnRBCqAWpjcRw4f/pSFErmv4aQWe4/Uz4NSk45m3UcThh3e3Wb6+zvwaZ/+Kd2908n5B2Ua" +
                "vsiZx9/mUn8g43Fm/b1Y7mQ23NKUDKkDE7rPUJYtT/dq+NvQzR3mV2CmRMNXu+e/jz7siMPgI9M3ebvN" +
                "+IMOJOmOxPZ9cjK2Ty+6MhdBHYmnp+ry9dVTUv9CPUkrf09LF+p0pHlyzitnExpaulA/jjTkR6z8fkJD" +
                "SxfqPK28/PnNT7R0of4wXQGqX6ins3wZQTch2SWvtaQyx2MOFrfZ0G0UE7yRZ74Cw8jso8wAZApJ0XQQ" +
                "BwX9XwhtWuVn03QEMoIKAXUbnduNyecU6ExiEuQVgpD/38hUpmb0zC0VSzb7D1mRmBAEGwAA";
                
    }
}
