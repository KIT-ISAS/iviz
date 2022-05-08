/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [DataContract]
    public sealed class ObjectRecognitionResult : IDeserializable<ObjectRecognitionResult>, IResult<ObjectRecognitionActionResult>
    {
        // Send the found objects, see the msg files for docs
        [DataMember (Name = "recognized_objects")] public ObjectRecognitionMsgs.RecognizedObjectArray RecognizedObjects;
    
        /// Constructor for empty message.
        public ObjectRecognitionResult()
        {
            RecognizedObjects = new ObjectRecognitionMsgs.RecognizedObjectArray();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionResult(ObjectRecognitionMsgs.RecognizedObjectArray RecognizedObjects)
        {
            this.RecognizedObjects = RecognizedObjects;
        }
        
        /// Constructor with buffer.
        public ObjectRecognitionResult(ref ReadBuffer b)
        {
            RecognizedObjects = new ObjectRecognitionMsgs.RecognizedObjectArray(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectRecognitionResult(ref b);
        
        public ObjectRecognitionResult RosDeserialize(ref ReadBuffer b) => new ObjectRecognitionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            RecognizedObjects.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (RecognizedObjects is null) BuiltIns.ThrowNullReference();
            RecognizedObjects.RosValidate();
        }
    
        public int RosMessageLength => 0 + RecognizedObjects.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "object_recognition_msgs/ObjectRecognitionResult";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "868e41288f9f8636e2b6c51f1af6aa9c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VZbVMbuxX+3P0VmvDBcMeYBhJK6TAdAqTJnSTkAnfaWybDaHdlW2G9ciQtttPpf+9z" +
                "jqT1Ar5twr1QDwxe7dHReXnOm8hM/lkV/sqqwoxq7bWpryZu5LbOwsJXVZ4yxaG1ciFsu3oVNrrs4Hf+" +
                "ZO/P/7YvvkusbO0hH/Hm5PD45Ew8aHP4ZG+ULJUVY/6TZWviYqydmCjn5EgJv5gqUSpXWJ0rJ6SYGq9q" +
                "r2UlXKFqJQpTD/WosZIU3AeBU16YYdTeCT+WXhSyFmo+raSusaDC1uxbDXT5KXHLHmim85PDs6M3v8lM" +
                "bBhSd9MURWOtqgslJtJbPRe58jOlgmpLeLVCDysj/c421CjMcnP2SLBzvgxmDI6F3Ode1qW0JZzqZSm9" +
                "FEMDh+vRWNnNSt0o+NLLyRQi81vyuRskJOBnBG9ZWVUL0TgQeQNFJpOm1oX0gIgGWLr7sRN+BlSk9bpo" +
                "KmlJcVvqmsiHVk4UccePU18aNuTb431CklNF4zUEWoBDYZV0uh7hpcgaXcOEtCFbu5iZTTyqEWDbHh6A" +
                "BmGBMwvwQhjp9nHGD0G5AXjDOPBOXTqxzmtXeHQbAodABDU1xVisQ/KPCz82wZs30mqZV4oYF7AAuPZo" +
                "U2+jw7lm1rWsTWIfOC7P+Ba2dcuXdNocw2cVae+aEQwIwqk1N7oEab5gJkWlEYmi0rmVdpHRrnBktvaa" +
                "bAwi7GKP4K90zhQaDijFTPtx5oBccGdvXOnysdD4rTH+/8t/MbBD/gv2ABQRo27KvgTaydpTODe87YsP" +
                "pxdxDTC8KirTlAnWd7Pp96t0+urHk6ML8fbD69OH60VaHZnaI+EigmvE+4QTtJC5aTwLz5kdIEvasXMo" +
                "c1N6aOqUvih/GJgELwBfsoFWFcwyQfrwojYe2Q9rjOFcFRIZgjmGTKGKca0R48ChYWKipPcTgRR0h96C" +
                "CJQgw6GWUoNjUSfJRywyC6Juy/hrKAvYuqBttBd8uFyVlHP2xdjMEF1WiYVphMRfzQmEE0ngyPYhsRHb" +
                "ONQKU3PqesuEKe3/kcmet8YEIEgZAAfkLVNF9B6giOVALEX5fpwklBy9+/n84uTs/KFIyaJ7OY2yHUKp" +
                "9lYWASY7ZYB5tEuuKoO0EYMiWKkvYlnnGHDJQ8bqEVI+iouqnbEOR60HY+AIyb2YDGcGLI3lDRIYypFd" +
                "7tnIwpfgzo8kyBGFGxXTTvS5W3r0W0VMjUoS8yZwnRNmKOvlZr7lxnKKWMZ5Ncm/U3YKXtRnGfMZUwcp" +
                "3is3blld4bzxitP5ZNZouwS7KYLOmSRXzwPeXE9HKHbcVDkV9n1ugPkRWihJ+PCmsX2KFJJ9HqOgkiBh" +
                "5ZHJEU2IPxSK61CY2Q3agoKwJxtvKPCpxiyykSIZ7aJjTJix1SSe98AmS3w8PT/5HbJWckHopVxT+QA8" +
                "wsUiN+WCCjx3lvtLH8FfjpJUyN5k1JqrHFOw9QdsOrQs2EihbmWpZb0FeyhGJpDYuEZW94zk1N/B6Mhw" +
                "yUasnsdOh8596qK5TGe/pbAcP35ZuVdFyPzkKD8zNCBwjkB7KIP9AVxAnvJrw9UCgVGqoSYA1zgxJvkI" +
                "/2u1WFUDAhrCfkFp1WtUKptCOWCizFPfAy6JYZl3RXHeWO4docGP56cfttDqpobyl8P370RgMBCHdSoS" +
                "umwLFyaCa8XJSzEAk1WWDQX24lzqynLp1ECcDEYDTqD3nd6n2kFprDLmGjF+jZr17F89snBvv3dkmmJ8" +
                "/KrXFz1rjMfK2Pvp/tZWZRDusLbv/ftZUNFyo1lT217fkGUMpeXgPZ4FQmXvWIFKmfY9bNJoZJFXrpWK" +
                "LfawUnOd60r7xSBacAVeobAKRuThQRfi+FXARjsQIgWWt3oKAhcPR4j2OaKsUtzAv4aAUVl6FMxmX7QG" +
                "4DUyAdbummD/5Z/3XgSKwqBHKcKU2rsvcS+edP7TOzQG6EbGpipbP906+PxL9SZRBN58lOjNRm5nN6xM" +
                "jcXKyxc72/xI3Q0RIF+bWaRARz7DVHRnuYYLSJF0QLqkCG8npmwqeu9pfPBm2kuABrQfa6BcXYHv3hSQ" +
                "sI7LVrIzhemHzRJFsXYBcqGT6IvZWGPWmtC1B5c5uheQZakjMruZxcHJBKSaVipHoeKJn1+gdvuC8zq4" +
                "hHqYhp1uEOfoQexC5JXJKaAc6ueCwjJdaqR5Koqiat92MM8CLp+FbmWQEkY4K7T9fCIUoSps7EiGyR81" +
                "f11PYJVNitoNquDPqb9Yb6AGBgNVbgzExyUb19kLoalloN0ucUYLUzYFi0piAiJWQoEpKlxslFo7OcSv" +
                "MnQij5FmuDmsqL0K0uvQyYdNQXhZfGm0iwmnbWHvzOtUNdYpU3CbFNy4cX/aEdvHlB+bwlNbHa3YMddA" +
                "vB2mFhnGg6dag/TBhdtATXkgNNMzXULD2AxUqh7haQXTdDkQGKQn3kwyHbe3V3wyButaVS6pqm0CRKwU" +
                "ES9sGwLNIGNHvSYooF2KqSrLjakQkJDuKtfIcdROCEwGrjN2ty/+moSKTSucNMXeNfGuVUpGlSBFvvDK" +
                "tTusmSX6uzvw6jb9HgSkk/8QiS/PAOdP4hD+SNHH7/vCAWtk2fXE/odgvY2oGdQquYrFD7BjqbCy9Sm1" +
                "4xcFRdc3stJpRHiyDMTOWJ2AyIUhtKcpB1EnHHSHNewiOhrbO9ms5RIyT8DUHvrZiz1S/0A8jys/x6UD" +
                "sb2keb7LKzsdGlo6EC+WNORLrLzs0NDSgdiNK6/fnR7S0oH4U3dl9wVW9rKU56k+JJd8kCGcGZMJMGY4" +
                "pCtYJjgN34fWTOhqyPLNbDBFCNN4EIOCqzM2Hafvqm4o0YTM4JSiLu9GpXMKNF8+CvIGQJzIGr15pSac" +
                "QdNEyJI9Fixuz2Qc6ugaOxcZaTirtGPVPfXwI/QVf4mTQanmAgimCwqrhmFoT3chrASmDoxPyl1+yuiM" +
                "i8gAcdbyogOImwxRlnaERof7wWbKBCzN6iEsbXoiUyU1VpgsqYXerxUqePxyJ8ip5lcw3GNKu8JGKdiL" +
                "NH3cv7Vq8+fQAqtuKgsV7loQQfP226L99vWpxP+VETKplC6s+V5EOZRtvqXlwZaHWBmwyRfl9wozl7F0" +
                "CZ7dqcj3z37UufV/KZ40too05jwh2wF+6bWgdYMNlnxNcwZrEmVfE2dmtjmRn9GOtJxkwgHhYne+C0O1" +
                "Kof/0qRZxeqWvDPw0DUkRlY9V+WmnHdlZFK+9AN/6lX6AXudYcnyfLA+7ws0pV/7qMu+Oxb/QxDHe8u/" +
                "rF7+Jy9vJJhe7ux+6ijzdK6DRocr7HvfXX36bxAtl/F9iEkCZsfYAxH6qJYg+6kBim3NfJd0T6Pg8uxV" +
                "mLwl0B1s4unLUnDqFoDO/55m0rdZlv0HcO0i+aweAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
