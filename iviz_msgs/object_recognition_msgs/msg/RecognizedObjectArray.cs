/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/RecognizedObjectArray")]
    public sealed class RecognizedObjectArray : IDeserializable<RecognizedObjectArray>, IMessage
    {
        //#################################################### HEADER ###########################################################
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // This message type describes a potential scene configuration: a set of objects that can explain the scene
        [DataMember (Name = "objects")] public ObjectRecognitionMsgs.RecognizedObject[] Objects { get; set; }
        //#################################################### SEARCH ###########################################################
        // The co-occurrence matrix between the recognized objects
        [DataMember (Name = "cooccurrence")] public float[] Cooccurrence { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RecognizedObjectArray()
        {
            Objects = System.Array.Empty<ObjectRecognitionMsgs.RecognizedObject>();
            Cooccurrence = System.Array.Empty<float>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public RecognizedObjectArray(in StdMsgs.Header Header, ObjectRecognitionMsgs.RecognizedObject[] Objects, float[] Cooccurrence)
        {
            this.Header = Header;
            this.Objects = Objects;
            this.Cooccurrence = Cooccurrence;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public RecognizedObjectArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Objects = b.DeserializeArray<ObjectRecognitionMsgs.RecognizedObject>();
            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i] = new ObjectRecognitionMsgs.RecognizedObject(ref b);
            }
            Cooccurrence = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new RecognizedObjectArray(ref b);
        }
        
        RecognizedObjectArray IDeserializable<RecognizedObjectArray>.RosDeserialize(ref Buffer b)
        {
            return new RecognizedObjectArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Objects, 0);
            b.SerializeStructArray(Cooccurrence, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Objects is null) throw new System.NullReferenceException(nameof(Objects));
            for (int i = 0; i < Objects.Length; i++)
            {
                if (Objects[i] is null) throw new System.NullReferenceException($"{nameof(Objects)}[{i}]");
                Objects[i].RosValidate();
            }
            if (Cooccurrence is null) throw new System.NullReferenceException(nameof(Cooccurrence));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                foreach (var i in Objects)
                {
                    size += i.RosMessageLength;
                }
                size += 4 * Cooccurrence.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/RecognizedObjectArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bad6b1546b9ebcabb49fb3b858d78964";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VZbW8buRH+voD/AxF/kH2Q5SZO3FSFUTi20+SQxLnIh/YaBAZ3l5IYr0iF5FpSiv73" +
                "PjNcrta2rr36EldIYC13OJyXZ96o7e17fMSrs+PTsw/iPnubz1b2SslSOTHlP1vZVrYtLqbai5nyXk6U" +
                "CKu5EqXyhdO58kKKuQ3KBC0r4QtllCisGetJ7WTQ1gxB4FUQdixs/lkVwYswlUEU0gi1nFdSGyyouHUr" +
                "izSXThV2YjQxuJz5id//EBe+qvKcKT5+SuxYwnsZa3R2/OHk1e8zVrQO6bxni6J2TplCiZkMTi9FrsJC" +
                "qaifaxVYCz6urAwHT6BLYde7t7Ls6Bt/srejvw6FD2U0ZvQwJB8FaUrpSvg2yFIGKcYWnteTqXJ7lbpW" +
                "cGmQszmE5rfkej9IgMC/CZzmZFWtRO1BFCw0mc1qowsZgBQNzHT3YyfcDcRIF3RRV9KR5q7UhsjHTs4U" +
                "ccc/r77UbMrXp0MClFdFHTQEWoFD4ZT02kzwUmS1NrAhbci2LxZ2D49qAvy2h0e8QVjAzQHDEEb6Ic74" +
                "ISo3AG8YB/4xpRc7vHaJR78rcAhEUHNbTMUOJH+/ClMb/XktnZZ5pYhxAQuAa4829XY7nA2zNtLYxD5y" +
                "XJ/xW9iali/ptDeFzyrS3tcTGBCEc2evdQnSfMVMikojIEWlcyfdKqNd8chs+yXZGETYxR7BX+m9LTQc" +
                "UIqFDtPMA7vgzt641OX3QuNvjfR7Bve3yIQpuGMqjBYBGBGmfs7eBN7J3nO4N77ti3fnF80agHhZVLYu" +
                "G2Bvyqz/u17nL348O7kQr9+9PL+/clG1E2sC0i8C2SDsZ5yuhcxtHVgDzvPAWlKRfUR5nLJEbVIeI04j" +
                "C8PgDWBMltCqgnFmSCNBGBuQB7HGWM5VIZEpmGXMGKqYGo1YBx4tExMlvZ8JpKJb9A5EoAQZneooR3gW" +
                "drZ2FovNsqhbcv4a4iLOLmgfbWZWXMNKykBDMbULxJpTYmVrIfFXczrhtBJ5splIeEQ6znXCGvL3tnjN" +
                "lKkO/IHpHrdGBTpIJ6AI9C1XRfSBANLUB7EW5j6gSZA5efPz6OLsw+i+sIkmJldzamVrxCoenCwiZg7K" +
                "CPzGOrmqLFJJEybRVn3RVHyOCp9cZZ2eoAyg4CjjrfN01k40Cc6QzskVMhUfGoE1ldfIaqhRbr1pdyuL" +
                "36Jj35MoJxSCVGM7Eelv6dJvlbEGFabJpwB6TgCibJjb5b6fyjkiHEca0uGg3OpUwkapdSqAKEQfJXmr" +
                "/LRldokTpxsl4NNZsSclOM4Rid4m2XoBiOdaO0Eh5L7Lq7jvc40wmKDLkoSVYGvXp+gh+ZcpLioJGrYB" +
                "0jxCDEGJKnIVqzb7QztQEBJlHSylAypAq61sokhKt+oYFeZstWlOvHcjJt6fj86+SUJLvoj9lq+rEGFI" +
                "IFnltlxRC8At6HDtLDjOU/qK2Z1Ma7gOMgX7YMD2Q1ODjRT+TpZamn3YREWcApe1r2V111Re/Q2sTiyX" +
                "dcTvqOmG6OTv1uf99zz3ewrP6UOUnTtVhnxA3goLS+MEpw10kTI6ARAG+inx1lxMECOlGmuCsqEjUwVI" +
                "sXClVptKRERFZCEo4QaNWuZSbEdslPlWapHAZs2zzLsC+WAdN5rQ48fR+bt99MWp+/zl+O0bEVkMxLFJ" +
                "NUSXbXXDAHGlOKkpxmKyzbr3wF46mHq4XHo1EGeDyYBT613v96m0UHKrrL1C0F+hpj36Z48M3Rv2Tmxd" +
                "TE9f9Pqi56wNWJmGMB/u71cW8Q+jh96/HjVKOu5LDXX55prMYyljRy/y6BAbgI4dqNTp0MMmjb4XmeZK" +
                "qaYjH1dqqXNd6bAaJCNugC50VtGOPGzoQpy+iCBp50ikxfJG78Ew43EKwb9EyFXKD2nxJWRsFOZnwZyG" +
                "orVCXCRDYPG2IYbP/vT8aUNSWDQ0RZxwe3fF7qXTRj+9QQOB3mVqq7L1183DR1+qV4mkYc/Hid5i4g8O" +
                "m6W5dVh69vTgSXymfohIkMztItGgmV9goLq9buAPUiidctnMoM3rmS3riggCzR7Bznstxgnu32se3Vyq" +
                "b983kLyeK1uyOMXuu70SddP4CMHYdPTFYqoxqs3kCkyKmGeELEvdILWbcTwcTrgytFJ5ip1A/MIKJT4U" +
                "nPTBJVbMNCt1wzpHu+JWIq9sThHmUWFXFKjpaiSNY40oyoS22XkUYfoo9jWDrEF/PCvODHwiFKE6bd1E" +
                "xqsDtAU7egar7FEY71KRf1xSp1RDDQwVqtwdiPdrNr6zF0JTV0G7feKMTqesCxaVxARKnIQCc5S/pqVq" +
                "7eQRz8rSiTyF2vHeuKJGLEqv4wAQN0XhZfGl1r7JQG3Le2vcp3qyQ5njoEzG9ruDm5MSHfDklDJmXQTq" +
                "wxsrdsw1EK/HqaWG8eCp1iB9cOGGUXvwic33QpfQsOkUKmUmeNrANN0tRAbpiTeTTKftHRifjLncqMon" +
                "VbVLgGjKR4MXtg2BZpCxo14SFNBPRUhkWW5thZCEdJe5RsqjXkNgkvCdqb198ZckVNPdwklz7N0Wb1ql" +
                "ZKMSpMhXQfl2h7OLRH97B17dpH8OAfnk9NkWx/BFijx+1xceOCOr7iTWP0TL7TZaQaWSa1rL5MJRpWXL" +
                "U5bHfxQXba5lpdMk8WDZhx2xOfmQ+2JYz1P+oT456o7AdqvGydjeyWQtl5h1Ip6eo9W9eE7qH4nHzcrP" +
                "zdKReLKmeXzIKwcdGlo6Ek/XNORHrDzr0NDSkThsVl6+OT+mpSPxx+7K4VOsPM9SkqfykFzyTsZQZjwm" +
                "sNjxmC5xmeA8fh87O6NbJcd3u9EUMUSbgxgUXKix6TR9V6amJBOzgleKOr9rlc4p0I2FRpBXAOFMGjTt" +
                "lZpx9kyDI0v2vWBxc2jjMEcn2bn8oOmNwr/SnlUP1NpP0GH8uRkZSrUUQDDdaTg1jgN+uj9hJTCOYLhS" +
                "/uOnjM64aBggxlpedABxkzHK0o7Y83B3WM+ZgKXZPKGlTQ9kqqTGBpMltdAHtkJFj388iHKq5SUM9z2l" +
                "3WCjFOxFmkju3nS1uXPsgFU/l4WK1zKIoGX7bdV++/pQ4v/KZJlUSnfdfHuiPEo2X/DyxMvTrYzY5Dv2" +
                "O0WZS1i6P89uVeO7ZzPb/5fiSWOnSGPOE7Kd7Ndei1rX2ODI14G6rvc8rLDs2+KDXezN5Ge0Ii0nmXBA" +
                "uDhcHsJQrcrxJ540tjjdkneGH7q5xBirl6rck8uujExKP3W8Bn/qU/oRe53BySn6jWJn2RdoSL/2UZND" +
                "d1T+uyCOd5Z/2bz8D17eTTD9eHD4qaPMw7kOGh1vsO9dd/XphyRaLpv3MSYJmB1jD0TsoVqC7KcaKHaG" +
                "+a7pHkbB9dmbMHlDoFvYxNOXteDULQCd/znNpG+LLPs38czjHSseAAA=";
                
    }
}
