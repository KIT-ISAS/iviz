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
                "H4sIAAAAAAAACsVZbU8bSRL+PhL/oRU+GFbGuYSEy3FCJwJkk1USsoHV3V4UofZM2+4wnna6e7Cd0/33" +
                "e6pqejyAc7ebLJwFYtxTXV0vT701m5vf8FEvTw6PT96rb9nbfLKXRhfGqwn/ybJNdT6xQU1NCHpsVFzO" +
                "jCpMyL0dmqC0mrloqmh1qUJuKqNyV43suPY6WlftgyCYqNxIueEnk8eg4kRHletKmcWs1LbCgpGtmZBc" +
                "eJO7cWVp/8U0jMPD97LwxRSnTPHhY+IG8b7JTGcnh++PXn6XmdgwpO6Oy/Pae1PlRk119HahhibOjRHV" +
                "fCt8K/SodDruPoYauVttzjaygz/4s5G9OftxX4VYiCHFtRsQ/SzqqtC+gF+jLnTUauTgczueGL9TmisD" +
                "d0Y9nUFqfktuD4MEBvyM4TCvy3Kp6gCi6KDLdFpXNtcRKLHAS3c/dsLVQIv20eZ1qT3p7gtbEfnI66kh" +
                "7vgJ5nPNtnx1vE9gCiavo4VAS3DIvdHBVmO8VFltK1iRNmSb53O3g69mDOS2hwvWICyg5oFfCKPDPs74" +
                "QZQbgDesAwdVRVBbvHaBr2Fb4RCIYGYun6gtSP5uGSdOHHqlvdXD0hDjHBYA1x5t6m13OJPY+6rSlUvs" +
                "hePqjN/Ctmr5kk47E/isJO1DPYYBQTjz7soWIB0umUleWgSjKu3Qa7/MaJccmW2+IBuDCLvYI/irQ3C5" +
                "hQMKNbdxkgWAF9zZGxe2uDtA/tZA38j+b1mwCW/JgmISoBGRGmbsTgCeDD6Df+VtX709PW/WgMSLvHR1" +
                "kZB9M6f+fpVOn/90cnSuXr19caq+S6sjV0WkXQRxhZBHwoL1lR66OrLwnN+Bs6Qde4fyN2WIGuviO0oh" +
                "DibBCyCYbGBNCbNMkUGiqlxEDsQaw3hoco0kwRwlWZh8UlmEOaDomJgo6f1UIQvdoPcgAiXIcKin7BBY" +
                "1GnyEYvMgpjrMn4NZgKuc9pGe8GHi1ZBaWdfTdwcAeaNWrpaafy1nEM4lwhHtg+JjfDGoV65irPXKyZM" +
                "yf9PTPaoNSYAQcoAOCBvmRqijwBFUxTUSpTfj5OEkqPXv5ydn7w/+1akZI17OZOyHaRgR6+hPQm9WwjM" +
                "G7sMTemQOZqgECv1KeOzTygGQvKQ83aMrI/6YqrgfMBRW2IMHKG910vkJT5TsDTRV8hhqEh+tWc7kwdx" +
                "5zsS5IjCjUpqJ/rCNT36rSKuQjFpUidwDexXKERjPCwehomeIZZxXkXy7xadmtfos4r5jKlFijcmTFpW" +
                "FzhvsuZ0Ppk1elyA3QxBF1ySqxcBby6pY9Q7bq0CRKG3n2pgfoxGShM+oqt9nyKFZF80UVBqkLDySOaI" +
                "JsQfasWl1GZ2g/WgIOzpOjoKfCozy2xsSEa/7BgTZmw1ac77xlZLvTs9O/kDslZygXRUoS6jAI9wsRy6" +
                "Ykk1nvvL/ZWP4K9ASUqyNxm14kLHFGz9AZsOXQs2Uqh7XVhdPYQ9DCMTSKxDrctbRgrm72B05LhqI1bP" +
                "mmaHzr3/urlKaBvfU1uO776y3Cok5AHyVZw7mhQ4TaBJ1OICYBeopxQLHyHzIzYKM7KE4QonNnm+iYBL" +
                "s1xXBgQQsl9RZo0WxcqnaBZYFMPU/YBLYlgMu6KE6Dx3kNDgp7PTtw/R8Ka28tfDN6+VMBiowyrVCVu0" +
                "tQujwSXlMbR0jMFklVVPgb04l3qzoQ5moE4G4wHn0Ntex7LMUKVzlwjzS5StB//qkYV7+70jV+eT4+e9" +
                "vup55yJWJjHO9h8+LB0iHtaOvX8/EBWpupF4lFVgBuKNLCve44lAinvHClTNbOxhk0U7i9RyaUzTaI9K" +
                "s7BDW9q4lEnBrAMsFDZiRB4hbK6Onws22skQWbC41lYQuHhKQsAvEGil4Tb+BQRslKWvitnsq9YAvEYm" +
                "wNpNE+w//cuzJ0KRO7QpuYyrvdsS95qTzn5+jd4ADcnElUXrp2sHn30uXyYK4c1Hqd58HHb3ZGXmPFae" +
                "Ptl9zF+pwSECpGw3byjQl88xG91YruACUiQdcNGMk/J26oq6pPcQqzTRzXoJ0ID23U2W68vwxs1bA5KX" +
                "XL4yNUXq250CpbEKgjrpJ/pqPrEYuqZ6CSZUfOiOQBcF+wJk3eQS4GfCUkUrJfbSCAh+cYkKHnPO7uAi" +
                "VTFNPd04HqIT8Us1LN2QYiqgiqLcxvaCIw1WjSgAYNvHPBBoPpCeZZByhpwlzT+fCEWoFjs/1nILgMq/" +
                "Zaewyg4F7jbV8UfUZWzVUAPjgSm2B+rdik3o7IXQ1DjQ7pA4o5Ep6pxFJTGBEq+hwAx1rmmXWjsFhLBB" +
                "/ykz8o4b7YxKarJEehoWoZxsEuF1/rm2MgP0V43sjcGdCscWJQtulsSN27dnHvX4mFJknUdqrhsrdsw1" +
                "UK9kTYwHT7UG6YMLN4OWUoG01HNbQMOmJShNNca3NUzTLYEwSN94M8l03N5k8cmYsCtTSpbDgvUJEE2x" +
                "aPDCtiHQDDJ21AuCApqmJltlQ+dKxCSkuxhapDlqKhTmg9CZv9sXf0tCNa0rnDTD3k31ulVKNypBiuEy" +
                "mtDu8G6e6G/uwKvr9M8gIJ+cPpvqEL5Ikcfv+rim+MI1YSux/kEst91oBZVQQpGZWybnnuoqW54yO35R" +
                "T2x1pUuUP0HDPSYgdsVX8g95UCJ7llIQtcOiPmIbnhU/Y3snm7VcJPEIpJ6hqT1/RhY4UI+alV+apQP1" +
                "eEXzaI9Xdjs0tHSgnqxoyJVYedqhoaUDtdesvHh9ekhLB+rP3ZW9J1h5lqVMTxUieeUtPUNBhmTCixuN" +
                "6DaWCU7leeTdlK6IPF/SiikkSpuDGBdcn7HpOD2bqqY8I4khGPh96K5MOidH+8XHYMtL4HCqKzTopZly" +
                "Ak1jIUt2d8i4PpoRJI6pdexcaKQhrbQ0GAHB1MuP0Vz8tZkQCrPAvVxJFxXejGR4T3cirAemD4xRJnz4" +
                "mNEh5w0DRFrLiw4gblpiLe2Qboebwpr6JyPSrB/G0qZ7s1ZSZJ3VkmboAVu5xO8fdkVUs7iA7e5W4DWG" +
                "aqO+6RrCmjusNo+OPEAbZhrXLHzzglBatE/L9unL/WnwlZGy1SpdYvNFCe7A0AXQzS1PujzVagEpX57f" +
                "qtFc0dLFeHajON8+/I4H2f+le6u0N6Q0pw25D7nuO1G8xg5PHqfBg5UR8cHkvZvvTPUnNCctJ2kdm1Zh" +
                "b7EHW7Vay/9v0vDibUvemYDoahIzrF2YYkcvujIyKV8Egj91Ln1BYGd68jwwbC1wrdNXX/qo0rE7J/9D" +
                "Ecdby7+uX/4nL28nsH7Y3fvYUeY+vUf+Olxj4tse69P/iWi5aN5LcBI8O/YeKGmsVCLIfq6BZV8x3xXd" +
                "fem4On0tMq/JdAOh+PZ5JTu1EMDof0856WkO9f4DkyNmQgceAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
