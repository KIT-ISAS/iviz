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
        internal Detection3DArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Detections = b.DeserializeArray<Detection3D>();
            for (int i = 0; i < Detections.Length; i++)
            {
                Detections[i] = new Detection3D(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Detection3DArray(ref b);
        
        Detection3DArray IDeserializable<Detection3DArray>.RosDeserialize(ref Buffer b) => new Detection3DArray(ref b);
    
        public void RosSerialize(ref Buffer b)
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/Detection3DArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ce4a6fa9e38b86b8d286a82799ca586d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1ZbW/bOBL+rl9BbD40Wdi+TbKXK3oIDtsavQbYa7vX3N5LUQS0RNvcSKKWpOK4v36f" +
                "mSFlOXFv78M1QV8kihzOyzPPDJkj9YOqbYjKLdX5XFUmmjJa14aJWjqvtGr6OtqpW/yC8d0M52dF8cbo" +
                "yni15v+K4mgkKq5Nmmkq1XnXuaDrMMMMkZeHBnGqsat1VCvTGq+jgTAFITaIxI2Na9XodqtK3Va2woyR" +
                "qsOqSi29a6B0sO2qNsq2XR9nxTzPPJ9//DRaVxSX/+ef4m8f/vpChVjdNGEV/iAOgi0fItTWvlKNiRra" +
                "a3buGiYbP63NnamxSDcdLOCvcduZMMPCa3KBzRbW9Vb1AZOiU6Vrmr61Jbki2sbsrcdK28IPnfbRln2t" +
                "PeY7X9mWpi+9bgxJx59gfu1NWxp1NX+BOW0wZR8tFNpCQumNJlfioyp628bzM1pQHF1v3BSvZoXwD5sj" +
                "XjqSsua+8yaQnjq8wB7finEzyIZzDHapgjrmsRu8hhOKF1QwnSvX6hiav9/GtWsZRXfaW72gYAYEv64h" +
                "9RktenYykkxqv1Ctbl0WLxJ3e/wvYttBLtk0XSNmNVkf+hUciIlA7Z2tMHWxZSFlbU0bAdGF135b0CrZ" +
                "sjh6TT4WBHNE8L8OwZWWYUp4LkL0JJ2jcWOrr4XGOxsAdgHkKBNg5dwsbWug2F7mK8QOOQp8ZPyZ+2jI" +
                "p1otAIeSZpc1rLFLgh8tgT/g77oHwFYKiW151LZAecMzJpzQ8LPb0BT9UIDsmSgndKakT2NJSuPfRq/g" +
                "Uyfk4NTCqNpRAlQ0gQICoBMkeeIhgnpFu1IYF3pha8hGlqm5gw9aF9Va35H4ZIpR623nIDXgMytW16RR" +
                "sIANq5BI0VYgS9o9lM7nuaAqjLNYYjDKBW8IAn0j+Qvlv5sV71jEm7SRDf8EMN4DxOApcQlI6ogcvnA9" +
                "iA+uW7h7ANL7/E4biyKz4mUafOnuaQmm0uprzKAAM7FQiu7okq3LOyFFZmaGtxW5fGDoEg9ELK4nZk/E" +
                "bMTHJ7PEULtIE9LJag9isZ4oObkuMz2cFeDjCD7fsjR4wjRd3M6KYNrgvED1vQPBvKpdX4FzXO9Lc1PS" +
                "G1l0RQUG+4B4AkFio4OKXpe35BCxZiIzltbUFTgn4nsPSCycq6HhTZ7N0uZCqqQoMSCFqy23SsNyoIUL" +
                "Vgem2GVI2jck6+903UMJW9dsT21viT4ru1wCfFyR2GHVSBvg1WiQnUW8QCk9/PwQCjMxdOt60X8wZ0Ie" +
                "AwQYq6iqTiTujKLxazY3UUz+8EQs8yVQU4fQ5qzJyQWrGJTwfNQWxfwQfcwyjlHxUK8oZKnJSNKYThAO" +
                "B3BHpauKRcCrY2RqJFHcdRZXcyCxRxCAHgv4r3t0GFOUvIoLA4tETWnMRFIYSYO6snY9YtgZT3KVlog7" +
                "d9t3UnKHCk//gC+RUR+M2fPPz/x8Bc1meGfcNWAOApi29RA1CpZYvSOsrRKQLlGGqMAJ9B42XIkP1Ev0" +
                "S669Q40aKJgtl1XJFNSwTJ9et8imj99NTz/NimXtdLz4Xlgta3Ixp/gMOz6KZcoInpPkL4QsKy421BLw" +
                "yiwlOJTJpb3HF2+QL2xVR7mvUqWWPXKoBv5ZGayEo0pVwj4kWlJpjyjhLK7Tw4yGYiozRxROqfbWUSNF" +
                "SGQXsX6JylJr9Wcp1XuQQjtK2ZiNWwijoe2ivmCgCQI+5Wk77CHkQJMYZqgaaJkqy9EjPHA5kc3Qwgp3" +
                "gy+5RIk7BwG7hSk42C85Z5upNBjKwVeOux7xMNLxK1HB722e2wpvqE2ECdRZiL9buBjJEjoNHbnp77HA" +
                "EzFQfShIWNL9SP3dbaaN/gXeGiRJUBISLu4vAP/BZETM2/uEY+ftMB3RgqcBkEB1ALowHKf6fqxjoiFQ" +
                "MuR7tBNS8kdrEXNqdo/vJ2o7UZ8nyrs44h31L0USHw3/+/Dwf3j4JGfhx/OLTyNjni50fKp77N/H4ZrQ" +
                "iYSGq/RdSBw99NjZM4UYUnLnCcVPPToR37Lc3bynMhCqZDgOBSjRk+gPWwicpPKeuQM93g9P2+Hp89Oo" +
                "v3PdoZTa8+eD1MLbrzu/E50huf67Rflp8xQdxF4jyxB82AJLzyDsm4NF1QVw40yiZ04kquyJ/48v1Pzd" +
                "azrfzXFSQ6NHdwdMFnAdCaUWHa6RhJ6wMJQrKjyT1LDSdripSN2znERw6gRkPLigvAXd75qKvWhBgtSz" +
                "ZR/Ruw0dDQz7UrIcLGliywGGz1+S3GA/m0MCJmMLDx8knmWPSTGD7IcV5Wfu5M95k6fBetrxYO24kxuk" +
                "PYiDaMDVfB3hWnTjjdFIYTTGw0osrHBA4Y6e2hZUUySC4UhX6UgIGY2+hUigRdrtroMwnKPQKoVaokQn" +
                "xqiOzWw1m6jN2rQyS865kMDHLfQp3q5wBuCVuwaCZKpkHErK8ozPEaKzbCatSi4POHGlQ8GGDMKDTyc7" +
                "PlJmvbiDjc5NuKSJiAPcN5ygwHsRje/vssDXCfUXzn052FnJtav5FqJ0uK+RkxgA/nZa5WxGv89UDWRv" +
                "1hZJKEfMxOxfOhbkdG1ppOZERbOPlEQZN7FkbECKFIF8mRMQMOlmcSuCazUPaqjdgtCDO0uNuGCuCaW3" +
                "i+G+KKnCqE2Z+Q2f6MI3oB6vqcNJDT/vxefdXe+H8Dq/0i1yrlJnFY7rdASf0nHzhDrd04raj77l9sRU" +
                "QMr7nZgwWgulsZxXhywZR4yqL3MLW6Kp8cgP06EFk+gIDzIfghmM496arsnccrqs6fJWtKc7MOrreZEo" +
                "r0tcBQjFCalyT/7gPpLOdMfU+p6TGRLGk9njK5yzORTwfUkkmr04chenh9zNkfMQqcEhE0jhS2YbIOeU" +
                "NdnYChbyGRS3R6Zd4e2A0Hz5KQLyGy8mneYp0CKmxMVha+qQTbU+AyJVgIQX9g2BZiY90WuCAi5+BBKF" +
                "3FXgByf7hQWJVGj9FEgtjK4Vhw9/yUqJ3jcIUoe1R+rHwahRH7PYRhOGFd5t8vyHK/Bpf/5zukGnnfMP" +
                "ijNikTOPv02k+EDH4yz6W/HcSbIKJuH8CmYahNB9hbLsebonw9+WLuJwUgVnChqKp2IfDsRh8pFjNkW7" +
                "y/yDviPZjsRGWCXIWD6+wcpShHUET8/V1dvr52T+pTpNI/9IQ5fqbDfn9IJHzkdzaOhSfb+bQ3HEyB9H" +
                "c2joUl2kkdc/vvuBhi7Vn8YjYPVL9bzIdw503ZFD8paeYSDjMYPFLZd028QT3skz33DhjOz5Nz/iCknR" +
                "tBGDgn6lQYvm+dm0uApFI8WsEFC30a/hcJv2KdGW8DZY8gYg5F/+mNqA6cGeuZVizYrfALiBlOXEGgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
