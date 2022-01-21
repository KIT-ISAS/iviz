/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Detection3DArray : IDeserializable<Detection3DArray>, IMessage
    {
        // A list of 3D detections, for a multi-object 3D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection3D[] Detections;
    
        /// Constructor for empty message.
        public Detection3DArray()
        {
            Detections = System.Array.Empty<Detection3D>();
        }
        
        /// Explicit constructor.
        public Detection3DArray(in StdMsgs.Header Header, Detection3D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// Constructor with buffer.
        public Detection3DArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Detections = b.DeserializeArray<Detection3D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection3D(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Detection3DArray(ref b);
        
        public Detection3DArray RosDeserialize(ref ReadBuffer b) => new Detection3DArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Detections);
        }
        
        public void RosValidate()
        {
            if (Detections is null) throw new System.NullReferenceException(nameof(Detections));
            for (int i = 0; i < Detections.Length; i++)
            {
                if (Detections[i] is null) throw new System.NullReferenceException($"{nameof(Detections)}[{i}]");
                Detections[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Detections);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection3DArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ce4a6fa9e38b86b8d286a82799ca586d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZbW/cuBH+XP0K4vwh9mFXvcRXX5DCKC5ZpDFwTXKX9PoSBAZX4u7yIokKSdlWfn2f" +
                "mSG1srPp9UNjI3EkihzOyzPPDJkj9aNqbIjKbdTpStUmmipa14WF2jivtGqHJtqlW/+G8f0M58uieGF0" +
                "bbza8T9FcTQTFXcmzTS16r3rXdBNKDFD5OWhSZxq7XYX1dZ0xutoIExBiA0i8drGnWp1N6pKd7WtMWOm" +
                "6rSqVhvvWigdbLdtjLJdP8SyWOWZp6t372friuL8//xT/O3NX5+oEOvLNmzDH8VBsOVNhNra16o1UUN7" +
                "zc7dwWTjl425Mg0W6baHBfw1jr0JJRa+JRfYbGHTjGoImBSdqlzbDp2tyBXRtubWeqy0HfzQax9tNTTa" +
                "Y77zte1o+sbr1pB0/Anm42C6yqiL1RPM6YKphmih0AgJlTeaXImPqhhsF08f0YLi6O21W+LVbBH+aXPE" +
                "S0dS1tz03gTSU4cn2ONbMa6EbDjHYJc6qGMeu8RrOKF4QQXTu2qnjqH56zHuXMcoutLe6jUFMyD4TQOp" +
                "D2jRg5OZ5I5Fd7pzWbxI3O/xv4jtJrlk03KHmDVkfRi2cCAmArVXtsbU9chCqsaaLgKia6/9WNAq2bI4" +
                "ek4+FgRzRPCvDsFVlmFKeC5C9CSdo3Fp66+FxisbAHYB5CwTYOXKbGxnoNitzFeIHXIU+Mj4MzfRkE+1" +
                "WgMOFc2uGlhjNwQ/WrJmtDRDTQYhsS2P2g4ob3nGghMafnbXNEXfFSB7JsoJvano01yS0vjd6i186oQc" +
                "nFob1biKHWolqgA6QZInHiKoZ7QrhXGt17aBbGSZWjn4oHNR7fQViU+mGLUbewepAZ9ZsaYhjYIFbFiF" +
                "RIq2BlnS7qFyPs8FVWGcxRKDUS54QxAYWslfKP9dWbxiES/SRjb8A8B4DRCDp8QlgdSGw9du6Ni7a3cD" +
                "QHqf32ljUaQsnqbBp+6GlmBqwTE0HGAmFkrRPV2ydXknpEhpSrxtyeUTQ1d4IGJxAzF7ImYjPj4pE0Pt" +
                "I01IJ6s9iMV6ouTkusz0cFaAjyP4fGRp8IRp+ziWRTBdcF6g+tqBYJ41bqjBOW7wlbms6I0suthIZoF4" +
                "AkHiWgcVva4+kEPEmoXM2FjT1OCciO8DILF2roGGl3k2S1sJqZKixIAUrq4alYblQAsXrL6ZFZy8b0jW" +
                "X+lmgBK2adiexn4g+qztZgPwcUVih9UzbYBXo0F2FvECpQzw810olGLo6AbRfzJnQR4DBBirqKpOJO6N" +
                "ovG3bG6imPzhnljmS6CmDqHLWbObPgso4fmoLYr5IfooM45R8VCvKGSpyUjSmE4QDgdwR6XrmkXAq3Nk" +
                "aiRR3HcWFysgcUAQgB4L+O8GdBhLlLyaCwOLRE1pzUJSGEmDurJzA2LYG09ylZaIO/dh6KXkThWefoEv" +
                "kVFvjLnln1/5+QKalXhn3LVgDgKYts0UNVtnq/eENSoB6QZliAqcQO9uw5X4QD0dae4VatREwXEP2GQK" +
                "alimT687ZNO775YP35fFpnE6nn0vrJY1OVtRfMwd7+9jmTKC5yT5ayHLmosNtQS8MksJDmVyY2/wxRvk" +
                "C1vVU+6rVKlljxyqiX+2BivhqEpVsA+JllS6RZRwFtfpaUZLMZWZMwqnVHvpqJEiJMbJgkRlqbX6s5Tq" +
                "W5BCO0rZmI1bC6Oh7aK+YKIJAj7laTftIeRAkxhmhoqSqS1Hb5O0DrIZWljhbvAllyhx5yRgvzAFB/sl" +
                "54yZSoOhHHzmuOsRDyMdvxIV/N7mua3whtpEmECdhfi7g4uRLKHXlZGmf8ACT8RA9aEgYUn3I/WLu162" +
                "+jd4a5IkQUlIOLs5A/wnkxExb28Sjp2303REC56OlN2oA9CF4bjUN3MdEw2BkiHfo52Qkj9bi5hTs3t8" +
                "s1DjQn1aKO/ijHfUPxVJ/Gz4X4eH/83DJzkL352evZ8Zc3+h41Pd5/79PFwLOpHQcJ2+C4mjh547u1QF" +
                "F/ZpQvHzgE7Edyx3P+++DIQqGY5TAUr0ZLOtOvHRLXMneryZnsbp6dP9qL933aGUuuXPO6mFt497vxOd" +
                "Ibn+u0X56fo+OohbjSxD8G4LLD2DsG8OFlUXwI0ziZ45kaiyJ/4/PlOrV8/pfLfCSa0LfHfgUi9JQqlF" +
                "t9TeUUIvWBjKFRWeRWpYabvWblP3LCcRnDoBGQ8uqD6A7vdNxa1oLXKR3QwRvVs568y/lCwHS5rYcoDh" +
                "85ckN9hP5pCAxdzCwweJB9ljUswg+25F+ZU7+VPe5H6wnnY8WDuu5AbpFsRLuty44OsI16Ebb41GCqMx" +
                "nlZiYY0DSiW0BJeh/0Cnw5Gu05EQMlr9ASKBFmm3+x7CNHXUXWgkSpFPM8em3JYLdb0zncyScy4k8HEL" +
                "fYq3W5wBeOW+gSCZKhmHkrJ5xOcI0Vk2k1YllwecuNKh4JoMwoNPJzs+Uma9uIONzi24pImIA9w3naDA" +
                "exGN7++ywNcJ9RfOfTnYWcmda/gWonJNk05iAPjLZZ2zWTdC1YHiYKtdOmImZv/SsSCna0cjDSdqJHkR" +
                "ZdzEirEBKVIE8mVOQMCkm9VqbTvtQQ2NWy+Ybxo9EvXUJlTerqf7oqQKozZl5jd8ogvfgHq8HidSkL34" +
                "vLvv/dbUtmx1h5yr1aMax3U6gi/puHlCne7DmtqPoeP2xNRAyuu9mDBbC6WxnFeHLBlHjHqocgtboanx" +
                "yA/TowWT6ITJT0CKN457a7omc5vlpqHLW9Ge7sCor+dForyuPg5WKE5IlXvyO/eRdKY7ptb3tM7ODifl" +
                "51c4j1ZQwA8VkWj24sxdpVwOmOQ8RGpyyAJS+JLZBsh5yJpc2xoWWin6jem2eDsgNF9+ioD8xosLvkiT" +
                "QIuYaqe7zjQhm2p9BkSqAAkv7BsCTSk90XOCwrv3csgPhdxV4Acn+7UFidRo/RRILcyuFacPf8lKid6X" +
                "CFKPtUfqp8moWR+zHqMJ0wrvrvP8uyvwaT//iBY8pit02jr/oDojGDn1+NtCqg+UPM6yvxXXnRRzGX9I" +
                "At79gpx4X0y3MzXT7SSf7jKU5ajQHRr+dnRJh1Ms+FSQ8tWu8u8yEwfpMDHJEZyQ0GduQk+S3AID/ZgA" +
                "gOXz260sRRhJsPZYXbx8+5jMP1cP08jf09C5erSf8/CMR05nc2joXH2/n0MxxsifZnNo6FydpZHnP736" +
                "kYbO1Q/zETD+uXpc5PsIugrJIXmpJc0ZqxlIbrOhmyie8Eqe+fYL52cf5VRArpD0TRsxDui/O2jRKj+b" +
                "biACEsYIqOno5XDwTftUaFliUuQFAMr/MWQa0zKz5jaLNSv+AxQq7y7gGgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
