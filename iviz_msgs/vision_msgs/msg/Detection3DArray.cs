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
                "H4sIAAAAAAAAE71ZbW/cuBH+rl9BnD/EPuyqZ/vqBimM4pJFGgPXJG3S60sQGFyJu8uzJCokZXvz6/vM" +
                "DKnVOptePzQ2EkeiyOG8PPPMkDlSP6nGhqjcSp0vVG2iqaJ1XZiplfNKq3Zoop275a8Y381wviyKV0bX" +
                "xqsN/1MURxNRcWPSTFOr3rveBd2EEjNEXh4axanWrjdRrU1nvI4GwhSE2CAS72zcqFZ3W1XprrY1ZkxU" +
                "HVfVauVdC6WD7daNUbbrh1gWizzzfPHh42RdUVz+n3+Kv7z78zMVYn3dhnX4nTgItryLUFv7WrUmamiv" +
                "2bkbmGz8vDG3psEi3fawgL/GbW9CiYXvyQU2W9g0WzUETIpOVa5th85W5IpoW7O3HittBz/02kdbDY32" +
                "mO98bTuavvK6NSQdf4L5NJiuMupq8QxzumCqIVootIWEyhtNrsRHVQy2i+dntKA4en/n5ng1a4R/3Bzx" +
                "0pGUNfe9N4H01OEZ9vhejCshG84x2KUO6pjHrvEaTiheUMH0rtqoY2j+dhs3rmMU3Wpv9ZKCGRD8poHU" +
                "J7ToyclEcseiO925LF4k7vb4X8R2o1yyab5BzBqyPgxrOBATgdpbW2PqcstCqsaaLgKiS6/9tqBVsmVx" +
                "9JJ8LAjmiOBfHYKrLMOU8FyE6Ek6R+Pa1t8Kjbc2AOwCyEkmwMqFWdnOQLG9zFeIHXIU+Mj4M/fRkE+1" +
                "WgIOFc2uGlhjVwQ/WrJktDRDTQYhsS2P2g4ob3nGjBMafnZ3NEU/FCB7JsoJvano01SS0vjd6jV86oQc" +
                "nFoa1biKHWolqgA6QZInHiKoF7QrhXGpl7aBbGSZWjj4oHNRbfQtiU+mGLXZ9g5SAz6zYk1DGgUL2LAK" +
                "iRRtDbKk3UPlfJ4LqsI4iyUGo1zwhiAwtJK/UP6HsnjDIl6ljWz4B4DxFiAGT4lLAqkNhy/d0LF3l+4e" +
                "gPQ+v9PGokhZPE+Dz909LcHUgmNoOMBMLJSiO7pk6/JOSJHSlHhbk8tHhq7wQMTiBmL2RMxGfHxSJoba" +
                "RZqQTlZ7EIv1RMnJdZnp4awAH0fw+ZalwROm7eO2LILpgvMC1bcOBPOicUMNznGDr8x1RW9k0dVKMgvE" +
                "EwgSdzqo6HV1Qw4Ra2YyY2VNU4NzIr4PgMTSuQYaXufZLG0hpEqKEgNSuLpqqzQsB1q4YPXNpODkfUOy" +
                "/lY3A5SwTcP2NPaG6LO2qxXAxxWJHVZPtAFejQbZWcQLlDLAzw+hUIqhWzeI/qM5M/IYIMBYRVV1InFn" +
                "FI2/Z3MTxeQPj8QyXwM1dQhdzprN+FlACc9HbVHMD9FHmXGMiod6RSFLTUaSxnSCcDiAOypd1ywCXp0i" +
                "UyOJ4q6zuFoAiQOCAPRYwH8zoMOYo+TVXBhYJGpKa2aSwkga1JWNGxDD3niSq7RE3LmboZeSO1Z4+gW+" +
                "REa9M2bPP7/w8xU0K/HOuGvBHAQwbZsxarbOVu8Ia6sEpCuUISpwAr2HDVfiA/V8S3NvUaNGCo47wCZT" +
                "UMMyfXrdIZs+/DA//VgWq8bpePGjsFrW5GJB8TEPvL+LZcoInpPkL4Usay421BLwyiwlOJTJlb3HF2+Q" +
                "L2xVT7mvUqWWPXKoRv5ZG6yEoypVwT4kWlJpjyjhLK7T44yWYiozJxROqfbaUSNFSIyjBYnKUmv1RynV" +
                "e5BCO0rZmI1bCqOh7aK+YKQJAj7laTfuIeRAkxhmhoqSqS1Hb5W0DrIZWljhbvAllyhx5yhgtzAFB/sl" +
                "52wzlQZDOfjCcdcjHkY6fiMq+K3Nc1vhDbWJMIE6C/F3BxcjWUKvKyNN/4AFnoiB6kNBwpLuR+pv7m7e" +
                "6l/hrVGSBCUh4eL+AvAfTUbEvL1POHbejtMRLXg6UnajDkAXhuNc3091TDQESoZ8j3ZCSv5kLWJOze7x" +
                "/UxtZ+rzTHkXJ7yj/qlI4hfD/zo8/G8ePslZ+OH84uPEmMcLHZ/qvvTvl+Ga0YmEhuv0XUgcPfTU2aUq" +
                "uLCPE4q/DuhEfMdyd/Mey0CokuE4FqBETzbbqhMf7Zk70uP9+LQdnz4/jvo71x1KqT1/PkgtvH3a+Z3o" +
                "DMn13y3KT3eP0UHsNbIMwYctsPQMwr45WFRdADfOJHrmRKLKnvj/+EIt3ryk890CJ7Uu8N2BS70kCaUW" +
                "3VJ7Rwk9Y2EoV1R4Zqlhpe1au07ds5xEcOoEZDy4oLoB3e+air1ozXKRXQ0RvVs56cy/liwHS5rYcoDh" +
                "85ckN9jP5pCA2dTCwweJJ9ljUswg+2FF+YU7+XPe5HGwnnY8WDtu5QZpD+IlXW5c8XWE69CNt0YjhdEY" +
                "jyuxsMYBpRJagsvQf6DT4UjX6UgIGa2+gUigRdrtvocwTR11FxqJUuTTzLEp1+VM3W1MJ7PknAsJfNxC" +
                "n+LtGmcAXrlrIEimSsahpKzO+BwhOstm0qrk8oATVzoU3JFBePDpZMdHyqwXd7DRuRmXNBFxgPvGExR4" +
                "L6Lx/U0W+Dah/sq5Lwc7K7lxDd9CVK5p0kkMAH89r3M260aoOlAcbLVJR8zE7F87FuR07Wik4USNJC+i" +
                "jJtYMTYgRYpAvswJCJh0s1otbac9qKFxyxnzTaO3RD21CZW3y/G+KKnCqE2Z+R2f6MJ3oB6vtyMpyF58" +
                "3t31fktqW9a6Q87V6qzGcZ2O4HM6bp5Qp3taU/sxdNyemBpIebsTEyZroTSW8+qQJeOIUQ9VbmErNDUe" +
                "+WF6tGASnTD6CUjxxnFvTddkbjVfNXR5K9rTHRj19bxIlNfVp8EKxQmpck/+4D6SznTH1Pqe19nZ4aT8" +
                "8grnbAEF/FARiWYvTtxVyuWASc5DpEaHzCCFL5ltgJxT1uTO1rDQStFvTLfG2wGh+fJTBOQ3XlzwRZoE" +
                "WsRUG911pgnZVOszIFIFSHhh3xBoSumJXhIUPnyUQ34o5K4CPzjZLy1IpEbrp0BqYXKtOH74U1ZK9L5G" +
                "kHqsPVI/j0ZN+pjlNpowrvDuLs9/uAKf9uc/pRt02jn/oDgjFjnz+NtMig90PM6ivxfPnRTjDUzNlDoK" +
                "ofsKZdnzdE+Gvx1dxOGkCs4UNHyz6/qH7MOBOEw+csymaPeZf9B3JNuR2H6bgozl0xusLEVYR/D0VF29" +
                "fv+UzL9Up2nk72noUp3t5pxe8Mj5ZA4NXaofd3Mojhj5/WQODV2qizTy8uc3P9HQpfrDdASsfqmeFvnO" +
                "ga47ckhea0llxmMGi1ut6LaJJ7yRZ77hwhnZR+n8yRWSomkjBgX9lwYtWuRn0w1EMsIKAXUb/RoOt2mf" +
                "Cm1JTIq8Agj5P39MY1pmz9xKsWbFfwC4gZTlxBoAAA==";
                
    }
}
