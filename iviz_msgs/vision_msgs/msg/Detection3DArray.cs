/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection3DArray")]
    public sealed class Detection3DArray : IDeserializable<Detection3DArray>, IMessage
    {
        // A list of 3D detections, for a multi-object 3D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection3D[] Detections;
    
        /// <summary> Constructor for empty message. </summary>
        public Detection3DArray()
        {
            Detections = System.Array.Empty<Detection3D>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection3DArray(in StdMsgs.Header Header, Detection3D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Detection3DArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
                "H4sIAAAAAAAACr1ZbW8buRH+vkD+A3H+EPsgqWf76gYpjCKJkMbANUkb9/oSBAa1S0k87y73SK5l5df3" +
                "mRlytXKUXj80NvIi7ZLDeXnmmRn6SL1QtQ1RuaU6n6vKRFNG69owUUvnlVZNX0c7dYtf8Hy3wvlZUbwx" +
                "ujJerfm/ojgaiYprk1aaSnXedS7oOsywQuTlR4M41djVOqqVaY3X0UCYghAbROLGxrVqdLtVpW4rW2HF" +
                "SNVhV6WW3jVQOth2VRtl266Ps2KeV57PP34a7SueFJf/558nxV8+/Pm5CrG6acIq/E5c9ATmfIjQXPtK" +
                "NSZqGKDZv2tYbfy0Nnemxi7ddDCC38ZtZ8IMG6/JCzYbWddb1Qcsik6Vrmn61pbkjWgbs7cfO20LV3Ta" +
                "R1v2tfZY73xlW1q+9LoxJB1/gvm1N21p1NX8Oda0wZR9tFBoCwmlN5q8iZeq6G0bz89oQ3F0vXFTfDUr" +
                "IGA4HCHTkZQ19503gfTU4TnO+F6Mm0E2vGNwShXUMT+7wddwQiGDCqZz5VodQ/P327h2LQPpTnurFxTP" +
                "gPjXNaQ+pU1PT0aSSe3nqtWty+JF4u6M/0VsO8glm6ZrxKwm60O/ggOxEMC9sxWWLrYspKytaSNQuvDa" +
                "bwvaJUcWR6/JxwJijgj+1yG40jJSCdJFiJ6kczRubPXtAHlnAxAvmBylAwFzbpa2NdBtL/8VwodMBUQy" +
                "BM19NORWrRZAREmryxoG2SUhkLbAJXB53QNjK4X0tvzUtgB6wysmnNZwtdvQEv1QgJyZiCd0pqRXY0lK" +
                "499Gr+BWJxTh1MKo2lEOVLSAYgKsEyp54SGaekWnUiQXemFryEaiqbmDD1oX1VrfkfhkilHrbecgNeA1" +
                "K1bXpFGwQA6rkKjRVqBMOj2Uzue1ICw8Z7HEY5QO3hAK+kZSGMr/MCvesYg36SAb/gFsvAeOwVbikkBq" +
                "w+EL14P+4LqFuwcmvc/f6WBRZFa8TA9funvagqW0+xorKMDMLZSlO9Jk6/JJyJKZmeHbilw+8HSJD8Qt" +
                "rid+T/RsxMcns0RSu0gT2MlqD26xnog5uS7zPZwV4OMIVt+yNHjCNF3czopg2uC8YPW9A8e8ql1fgXZc" +
                "70tzU9I3suiKygzOAfcEgsRGBxW9Lm/JIWLNRFYsrakr0E7E+x6QWDhXQ8ObvJqlzYVXSVEiQQpXW26V" +
                "huVAC5etDmSxy5B0bkjW3+m6hxK2rtme2t4Sg1Z2uQT4uC6xw6qRNsCr0eA7i3iBVXr4+SEUZmLo1vWi" +
                "/2DOhDwGCDBWUVudSNwZRc+v2dzEMvnFoxHN12BNrPOizYmT8wuGMS7h/KgtqvohBpllKKPuoWpR1FK3" +
                "kaQxoyAiDviOSlcVi4Bjx+DUyKO4azGu5gBjjzgAQBYZsO7RakxR+CouDywSlaUxE8li5A2qy9r1CGNn" +
                "PMlVWoLu3G3fSeEd6jz9A8pEUn0wZs9BP/PnK2g2w3eGXgPyIIxpWw+BQ7yS1TvO2irB6RLFiMqcoO9h" +
                "55UoQb1E4+TaO1SqgYXZctmVTEElywzqdYuE+vjD9PTTrFjWTseLH4XYsiYXc4rPcOIXsUxJwWuS/IXw" +
                "ZcX1hhoD3pmlBIdiubT3eOMNUoat6ij9VarXckYO1UBBK4OdcFSpStiHXEsq7XElnMXVeljRUExl5YjF" +
                "KdveOmqnCInsItYvsVlqsP4oBXsPUuhLKSGzcQshNTRf1B0MTEHAp1RthzOEH2gRwwyFA41TZTl6hAeu" +
                "KHIYelmhb1AmVylx5yBgtzEFB+cl52wzmwZDSfjKce8jHg7m27HBbx1PTMA48Yb6RVhB/YW4vIWXkS+h" +
                "01CTB4AeOzxxA1WJgqSJ+hDyN7eZNvoXOGyQJHFJYLi4v0AGDFYjaN7eJyg7b4flCBicDYwEqgbQhRE5" +
                "1fdjHRMTgZgh36OpkMI/2ouwU9d7fD9R24n6PFHexRH1qH8qkvjF438dfvxvfnySE/Hj+cWnkTGPGT1m" +
                "7gMu/jJiE5pO6HGV3guVo58e+3umEEZK8byg+GuPlsS3LHe37vFshDIDKIdKlHhKTIA5BFHSes/igSfv" +
                "h0/b4dPnx7Jg57+DubXn1Qc5hm+/7rxP1IYs++9G5U+bx2ko9jpbweLDplhaCCHjHDIqNsAdZxV95qSi" +
                "Qp/KwfGFmr97TUPfHOMbWj+6U2DigPdIKDXt8I4k94SFoXpRHZqkFpaOww1G6qdlNsEoCuB48EJ5C/bf" +
                "9Rh7EYMEKW/LPqKbGxocGPa1rDlY4cSWA4Sf3yS5wX42hwRMxhYeHi2eZo9JbYPshwXmZ+7tz/mQx0J8" +
                "OvNwKbmTy6U9oIN0QN18TeFatOiN0chldMvDTmysMLVwm0+NDOor0sFwsKs0J0JGo28hEoCRHrzrIAzD" +
                "FZqnUEugaIyM6tjMVrOJ2qxNK6tk+IUEnsHQuXi7wmDAO3ctBclUyTpUmOUZDxeisxwmzUuuFhjD0qSw" +
                "IYPwwadxj+fMrBf3tNG5CVc4EXGABYexCgQY0Qr/Fhd8u7u0w+PgEO+s59rVfDtROlzlyIQGmL+dVjmn" +
                "MQQwbQPfm7VFKsromVj+a7NCTtqWntScrpgAkJgo7CaWDA9IkYKQ73kCYiYtLm5LcOPmQRC1WxCAcKOp" +
                "ERqsNaH0djFcJSVVGLgpP7/jSS98BwLymnqeNAXwWTwH7xpCRNj5lW6ReZU6qzDG02g+pTH0hNrf04oa" +
                "kr7lhsVUAMv7nZgw2gulsZ13hywZc0fVl7mvLdHmeKSI6dCUSXiEDZkVwQ/GccNNN2huOV3WdLUr2tP1" +
                "GDX7vEmU1yWuCITohFq5UX9wVUmD3jH1w+dkhoTxZPbl1c7ZHAr4viQqzV4cuYszRK7tyHmI1OCQCaTw" +
                "FbQNkHPKmmxsBQt5MMWtkmlX+HZAaL4XFQH5G28mneYp0CKmxJ1ia+qQTbU+AyLVgYQX9g2BZiYt0muC" +
                "Ai6EBBKF3GHgBxP/woJHKjSDCrwWRjeOw4s/ZaVE7xsEqcPeI/XTYNSop1lsownDDu82ef3DHXi1v/4Z" +
                "3a/TyfkHJRqxyJnH7yZSgqDjcRb9vXjuJFkFkzDUgpwGIXSPoSx7nu7P8LelCzqMr6BNQUPxeATEofgK" +
                "/8j4TQHvMgWhAUnmI7cRWYkzto8vt7IUIR6B1DN19fb6GXngUp2mJ39Pjy7V2W7N6QU/OR+toUeX6sfd" +
                "mnOEEk9+P1pDjy7VRXry+qd3L+jRpfrD+Am4/VI9K/JdBF2D5Ki8pc8wkCGZ8eKWS7qI4gXv5DNffmF2" +
                "9vyrIXGFZGk6iHFBv/CgTfP82bS4JUVHxcQQUL3RuGHoTeeU6E/4GGx5Axzyb4dMbUD2INDcU7FmT4r/" +
                "AMiDzArmGgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
