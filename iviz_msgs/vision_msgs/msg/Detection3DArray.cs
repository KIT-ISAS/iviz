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
                "H4sIAAAAAAAACr1ZW2/cNhZ+N+D/QNQPsYuZ2drueoMsjEWTQTYGuknaeLuXIDA4EmeGjSSqJOXx5Nfv" +
                "d84hJY09RvuwsYHEEkUenut3Lj5SP6jKhqjcUp3PVWmiKaJ1TZiopfNKq7qrop26xa9YH3Y4Pzs8ODx4" +
                "Y3RpvFrzL1o4GpGLa5N2m1K13rUu6CrMsENo5qWepKrtah3VyjTG62iImgIVG4Tkxsa1qnWzVYVuSlti" +
                "y4jf/liplt7V4DzYZlUZZZu2i+B2nreezz9+Gh0kti//zz+HB//48PcXKsTypg6r8CfREwn0IYJ37UtV" +
                "m6ghgmY1ryG48dPK3JoKp3TdQgz+GretCeD+SF2TImyWs6q2qgvYFZ0qXF13jS1IIdHWZocAHbUN1NFq" +
                "H23RVdrjgPOlbWj/0uvaMH36F8xvnWkKo67mL7CrCaboogVTW9AovNGkU3zE5s428fyMTuDg9cZN8W5W" +
                "cIaeA5hOR+LY3LXeBGJWhxd0zbci4wzkoSSDi8qgjnntBq/hhGwHLkzrirU6Bvvvt3HtGnapW+2tXpBh" +
                "AxyhqkD2GR16djImTay/UI1uXKYvJIdL/gjdZiBMYk3XMF5FKgjdCnrETjjxrS2xd7FlKkVlTRPhsAuv" +
                "/fbwgI7JpSDympQtHs22wW8dgissuy359+FBiJ4uYLvc2PIreuetDfB/cdBRcJCwc7O0jQF3O5igYEVE" +
                "LnlL75DmLhrSrlYLOEdB+4sKQtkl+SMdgmKg+aqDw60UAt7yqm3g9zXvmEigQ+VuQ3v0fQpybcKj0JqC" +
                "Po1JKY3/a72Cbl1CDacWRlWOYqKkHWQbuD45KO98DL5e0dVk1YVe2AoXIPrU3EEXjYtqrW/pjiSQUett" +
                "60A54DNzV1XEVrBwI+Ej4aYtgafEQiicz5sBZFhnuoRvFB7ekEN0tcQ1JPgOfL5jGm/SVTb8C37yHm4N" +
                "FBPNMIQdkeoXrgMyQocLdwcf9T6/093CCwi+TKsv3R2dwV4hcI1NZG/GHYrdAVJZyHwdImdmZnhbkfp7" +
                "GC/wQLDjOoL/jN5G9H0ySwA22J3cn4T3AB3rCbeTCnM+gM4CdB0B+lshB42Yuo1bCBFME5wX733vAD6v" +
                "KteVACTX+cLcFPQmYl1RKsJdgKVAPrLRQUWvi8+kGBFpIjuW1lQlACnie0cusnCuAps3eXsiOBfoJX4J" +
                "JMl4TbFVGhqA83B2awEkQ+Ckq0NSwq2uOvBhq0rEquxngtjSLpdwR05frLhyxBF82GigoYXpgDgdFH7f" +
                "MWZJ2q3rRIhepglpDv7Azosk7ITkIBitX4vMCYDyl6fDoMf8nKuKJodSDjmIxj4KA0Rtkf/3QctscGwk" +
                "SOQ2sl0qTRI9xhrYxcHbo9JlyUSg3bGnagRWHNUjV3O4ZgdrwJUsAmLdoS6ZIj+WnEGYJpJPbSYS2ggj" +
                "JKC162DM1ngirHSyvXOfu1ZSdF8T0H/AUwTZB2N2lPQLP1+Btxne2QVrQAr5mrbVYD6xmog+oNlWicsu" +
                "kbIoG4oj3q/VElCol6i0XHOLfDagNIsvx5I8SHgZYL1uEF8fv5uefgIny8rpePG9YN7AzcWcTNXf+sCs" +
                "KUZ4T7pikdC05LREZQQfzWSCQ1Zd2jt88QYRxKK1hAkq5Xa5JNtsgKaVwVEorFAFpETsJaZ2cBQq48ze" +
                "76jJurJzjPIcfm8dFWHkmKwpZjHhXCrL/iq5fce/UNBShGb5FgnuULJRMdGjB0cCRW/T3yKYQbvY6ZBb" +
                "UGuVlu1IzsFJR65DGSzYDjjlTJaU2lMYTiYb0Y1JRduMtMFQXL5yXC6JoilEvxpC/N79fSXiDVWZkIOK" +
                "EdF7A1UjfkKrwSe3Dx1OeMILziGHB0Svl+BI/ew201r/Cr311MRAyS8u7i4QEr3osJ63d9mxnbf9flgO" +
                "Soe/BEoVYIjdc6rvxoxmiAJo4waPGkSKhNFhOABXzMd3E7WdqC8T5V0cYZL6tyKaD5b/s3/5v7x80sfm" +
                "x/OLTyOBntSM0iw+1PND002ow6HlMn0XnEctPtb5DE0JFwL9jsODnzpUL75hysPOJxQT7PQO2meqhF0i" +
                "BSQidyW+d4Qe8BMOlh+BC/nxy5NJMShxb6ztqPZezOHtt8EEhHccdb8rWX7cPFHpsVMRi2Per6al2BCc" +
                "zsajVAQn5CCjZ44xqgdSrji+UPN3r7l9nKMPRKVIgwrGEiiRqFLFDyVJtE+YGpIbpalJKnzpPsxFUhme" +
                "2hu0tXAiD6QoPiMzDMXIjuVAQtLfsouo/UbFEIR7LIz2pkCRZ18y6D9l0sF+MftoTMZy7m9NnmXFpewH" +
                "8g8S0C/cGZzzNU8WAenS/anmVmZXO45PWARU5+mHa1Da10YjwFFk90dxskTXw/0BlTxIwYgPw2YvU79J" +
                "RGr9GUThO1K7ty2ooT1DqRUqMRj1o1Edm9lqNlGbtWlkl7TSRILbONQ43q7QUvDRofQgoioJiPSzPOO+" +
                "RLiW21KZk1MJOrnUY2xIJjz41DJyx5o540I4OjfhBJho7IHHvi0DMkYU0H8MIL6S2R9pKnvDZ17XruKZ" +
                "R+EwJ5IWDy7/dlrmKEf/wJgOX9+sLWIztbApBzzWZ+Qwbmil4gBG84BARe43sWA/ITKSL/IMKcB2UhVj" +
                "CoOxngdoVG5BroTRqYaFsNeEwttFP6jKzLATp3D9hnvF8A1QyWupj1IDwddxSz1UkLC18yvdIBBLdVZi" +
                "KEB9/pR62ROqmU9LLly6hisbU8Jv3g90wugwGMd5Ph4yafQsZVf0tXCBgsgjYkyLIk6sJDDJcAnIMI7r" +
                "dBrSueV0WdEkOUtAIzhqE/iYCKALzBwEAAV1ucK/NxalZvGYiuhzFkUMejLbOzc6m4MN3xUEtFmfI7Vx" +
                "zMh8kJQIo/V6mYAOz70t5jhH6pTZ2dgSgnKPi7mVaVZ420O1H8MKif6Vjwtj82R4IVVggtmYKmSZrc8O" +
                "klJF8h9WEjkRrmCrvSbXwMRJXIRI82wEP5giLCwgpkQJqQB6YTTg7D/8rWdN+L+BzVocPlI/9sKNyqDF" +
                "NpowHPFukw/cP4JP9w48p+k+XZ5/kM1hlhyR/G0iaQpsHmfa34oKkayHsQ+aZGBXT4YGJMqyFWhQh38N" +
                "jQLRDQNWxTu+5h8T7mMTm+QRaJJ+nizfZnRCuZI0gJiHicXgdH48PstkBJOSgz1XV2+vn5MSLtVpXvpn" +
                "WrtUZ6Ndpxe8dD7eRWuX6vvRLjIqlv483kVrl+oiL73+8d0PtHap/rKzBPy/VM9Jz2nSQWOWbKG39AxZ" +
                "2Ut7/3HLJY27eMc7eeYZGxpyz3+pErVI/ObL2E/ory90ap6fTYPhLGoxxo2AdI+ajxrpdFOBooYvwpk3" +
                "8Ez+a5WpDLICYDZXY8Lc4cH/AOZ67tR8GwAA";
                
    }
}
