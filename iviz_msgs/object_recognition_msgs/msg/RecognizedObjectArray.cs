/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class RecognizedObjectArray : IDeserializable<RecognizedObjectArray>, IMessage
    {
        //#################################################### HEADER ###########################################################
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // This message type describes a potential scene configuration: a set of objects that can explain the scene
        [DataMember (Name = "objects")] public ObjectRecognitionMsgs.RecognizedObject[] Objects;
        //#################################################### SEARCH ###########################################################
        // The co-occurrence matrix between the recognized objects
        [DataMember (Name = "cooccurrence")] public float[] Cooccurrence;
    
        /// Constructor for empty message.
        public RecognizedObjectArray()
        {
            Objects = System.Array.Empty<ObjectRecognitionMsgs.RecognizedObject>();
            Cooccurrence = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public RecognizedObjectArray(in StdMsgs.Header Header, ObjectRecognitionMsgs.RecognizedObject[] Objects, float[] Cooccurrence)
        {
            this.Header = Header;
            this.Objects = Objects;
            this.Cooccurrence = Cooccurrence;
        }
        
        /// Constructor with buffer.
        public RecognizedObjectArray(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Objects = b.DeserializeArray<ObjectRecognitionMsgs.RecognizedObject>();
            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i] = new ObjectRecognitionMsgs.RecognizedObject(ref b);
            }
            Cooccurrence = b.DeserializeStructArray<float>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new RecognizedObjectArray(ref b);
        
        public RecognizedObjectArray RosDeserialize(ref ReadBuffer b) => new RecognizedObjectArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Objects);
            b.SerializeStructArray(Cooccurrence);
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
                size += BuiltIns.GetArraySize(Objects);
                size += 4 * Cooccurrence.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/RecognizedObjectArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "bad6b1546b9ebcabb49fb3b858d78964";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZbVMbORL+fPMrVOGDYcuYS0i4HFfUFQtkk60kZIGtu71UitLMyLbCeORIGuzJ1f33" +
                "e7oljQfw3u2yARcUHk2r1S9Pv4mNjXt8xOuTw+OTM3GfvfGTvVayVFZM+U+WbYiLqXZippyTEyV8O1ei" +
                "VK6wOldOSDE3XtVey0q4QtVKFKYe60ljpdem3geBU16YsTD5Z1V4J/xUelHIWqjlvJK6xoIKW7NAcmlV" +
                "YSa1pv2XMzdxO2dh4asqT5ni46fEDeLdy0znJ4dnR6//kJnYMKTutimKxlpVF0rMpLd6KXLlF0oF1Wwn" +
                "fCf0uDLS7z6DGoVZbc6yg2/8yd6d/7AvnC+DGYNjIfe5l3UpbQmnellKL8XYwOF6MlV2u1LXCr70cjaH" +
                "yPyWfO5GCQn4mcBbVlZVKxoHIm+gyGzW1LqQHhDRAEt/P3bCz4CKtF4XTSUtKW5LXRP52MqZIu74cepL" +
                "w4Z8c7xPSHKqaLyGQC04FFZJp+sJXoqs0TVMSBuyjYuF2cajmgC23eEBaBAWOLMAL4SRbh9nfBeUG4E3" +
                "jAPv1KUTm7x2iUe3JXAIRFBzU0zFJiT/0PqpCd68llbLvFLEuIAFwHVAmwZbPc41s65lbRL7wHF1xm9h" +
                "W3d8SaftKXxWkfaumcCAIJxbc61LkOYtMykqjUgUlc6ttG1Gu8KR2cYrsjGIsIs9gr/SOVNoOKAUC+2n" +
                "mQNywZ29canLh0Ljb43xewb2N8h/MbBD/gv2ABQRo27OvgTaydpzODe8HYr3pxdxDTC8LCrTlAnWt7Pp" +
                "71fp9PsfT44uxJv3r07vrxdpdWRqj4SLCK4R7zNO0ELmpvEsPGd2gCxpx86hzE3poalT+qL8YWASvAB8" +
                "yQZaVTDLDOnDi9p4ZD+sMYZzVUhkCOYYMoUqprVGjAOHhomJkt7PBFLQLXoLIlCCDIdaSg2ORZ0lH7HI" +
                "LIi6KeOvoSxg64K20V7w4XJVUs7ZF1OzQHRZJVrTCIm/mhMIJ5LAke1DYiO2cagVpubU9YYJU9r/M5M9" +
                "7YwJQJAyAA7IO6aK6D1AEcuBWIny+3GSUHL09ufzi5Oz8/siJYvu5TTKdgil2ltZBJjslgHm0S65qgzS" +
                "RgyKYKWhiGWdY8AlDxmrJ0j5KC6qdsY6HLUZjIEjpLWyRVLiMwOWpvIaCQzlyK72bGXhS3DnBxLkiMKN" +
                "imkv+twNPYadIqZGJYl5E7jOCTOU9XKz3HFTOUcs47ya5N8tewUv6rOK+YypgxTvlJt2rC5x3nTN6Xwy" +
                "a/SsBLs5gs6ZJNfAA95cTycodtxUORX2fW6A+QlaKEn48KaxQ4oUkn0Zo6CSIGHlkckRTYg/FIqrUJjZ" +
                "DdqCgrAnG28o8KnGtNlEkYy27RkTZuw0iefds8kSH07PT75B1kouCL2UayofgEe4aHNTtlTgubPcX/kI" +
                "/nKUpEL2JqPWXOWYgq0/YtOhZcFGCnUrSy3rHdhDMTKBxMY1srpjJKf+AUZHhks2YvU8djp07mMXzVU6" +
                "+yOF5fjhy8qdKkLmJ0f5haEBgXME2kMZ7A/gAvKUXxuuFgiMUo01AbjGiTHJR/hfqXZdDQhoCPsFpVWv" +
                "UalsCuWAiTJPfQ+4JIZl3hfFeWO5d4QGP56fvt9Bq5sayl8O370VgcFIHNapSOiyK1yYCK4UJy/FAExW" +
                "WTUU2ItzqSvLpVMjcTKajDiB3nX6kGoHpbHKmCvE+BVq1pN/D8jCg/3BkWmK6fH3g6EYWGM8Vqbez/d3" +
                "diqDcIe1/eA/T4KKlhvNmtr2+posYygtB+/xLBAqe88KVMq0H2CTRiOLvHKlVGyxx5Va6lxX2rejaME1" +
                "eIXCKhiRhwddiOPvAza6gRApsLzRUxC4eDhCtC8RZZXiBv4VBIzK0qNgNvuiMwCvkQmwdtsE+y/++vJ5" +
                "oCgMepQiTKmDuxIP4knnP71FY4BuZGqqsvPTjYPPv1SvE0XgzUeJwWLidvfCytxYrLx4vvuMH6m7IQLk" +
                "a7OIFOjIF5iKbi3XcAEpkg64jFNkeDszZVPRe0/jgzfzQQI0oP1QA+X6Cnz7poCEdVy2kp0pTN9vlyiK" +
                "tQuQC53EUCymGrPWTLZgUoR8ImRZ6ojMfmZxcDIBqaaVylGoeOLnW9RuX3BeB5dQD9Ow0w/iHD2IbUVe" +
                "mZwCyqF+thSW6VIjzVNRFFX7roN5EnD5JHQro5Qwwlmh7ecToQhVYWMnMkz+qPmbegarbFPUblEFf0r9" +
                "xWYDNTAYqHJrJD6s2LjeXghNLQPtdokzWpiyKVhUEhMQsRIKzFHhYqPU2ckhfpWhE3mMNOPtcUXtVZBe" +
                "h04+bArCy+JLo11MOF0Le2tep6qxSZmC26Tgxq270454dkz5sSk8tdXRij1zjcSbcWqRYTx4qjPIEFy4" +
                "DdSUB0IzvdAlNIzNQKXqCZ7WME2XA4FBeuLNJNNxd3vFJ2OwrlXlkqraJkDEShHxwrYh0IwydtQrggLa" +
                "pZiqstyYCgEJ6S5zjRxH7YTAZOB6Y3f34u9JqNi0wklz7N0QbzulZFQJUuStV67bYc0i0d/egVcr+g3a" +
                "8BIS8tHpsyEO4YwUevxuKByARmbdTLy/C6bbyvo8/hQZfDxDTHyKGkPdkqtbx//CUsFlr1DKxy8Kja6v" +
                "ZaXT6PBomYmdtD4xkWtDyM9TbqIOOZgFCto2AgDbe1mu4xIyUsDaS/S5Fy9J/QPxNK78HJcOxLMVzdM9" +
                "Xtnt0dDSgXi+oiEfY+VFj4aWDsReXHn19vSQlg7EX/ore8+x8jJL+Z/qRnLJexnCnLGagGTGY7qaZYLT" +
                "8H1szYyujCzf2AZThPCNBzEOuGpj03H6ruqGElDIGE4p6v6uVTqnQFPmoyCvAdCZrNGzV2rGmTVNiizZ" +
                "Q8Hi5qzGKQDdZO+CIw1tlXasuqfefoJ+429xYijVUgDBdHFh1TgM8+mOhJXANIKxSrmPnzI64yIyQOh0" +
                "vOgA4iZDAKYdoQHiPrGZMwFLs344S5seyVRJjTUmS2qhJ+yECh7/uBvkVMtLGO4hpV1joxTsRZpK7t5m" +
                "dXl1bIFVN5eFCncwiKBl963tvn19LPF/ZbRMKqWLbL4vUQ7lnG9veeDl4VYGbPIF+p2CzeUtXY5ntyr1" +
                "3bMfdJ79f4onja0ijTlPyG6wX3ktaN1ggyVf0/zBmkTZN8SZWWzP5Ge0KR0nmXBAuNhb7sFQncrhvzdp" +
                "hrG6I+8NQnQ9iVFWL1W5LZd9GZmULwPBn3qYYcBeb4iyPDdsLocCzerXIeq174/L/xTE8c7yL+uX/8XL" +
                "WwmmH3f3PvWUeTzXQaPDNfa9664h/ZeIlsv4PsQkAbNn7JEI/VVHkP3UAMW2Zr4rusdRcHX2OkzeEOgW" +
                "NvH0ZSU4dQtA5/9OM+nbIsv+C/HYElb+HQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
