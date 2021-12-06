/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        internal ObjectRecognitionResult(ref ReadBuffer b)
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
            if (RecognizedObjects is null) throw new System.NullReferenceException(nameof(RecognizedObjects));
            RecognizedObjects.RosValidate();
        }
    
        public int RosMessageLength => 0 + RecognizedObjects.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "868e41288f9f8636e2b6c51f1af6aa9c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VZbW8buRH+vr+CiD/IPshyEydu6sIoHL+cc0jiXORDew0Cg7tLSYxXpEJyLSlF/3uf" +
                "GS5Xa1vXJr6zK9iwljsczsszb3Rm88+qCJdOFXZsdNDWXE792O98iAtfVXnOFIfOyaVw7epl3Oizgz/4" +
                "k70d/rgvvkusbOM+H3F2cnh88kHca3P8ZGdKlsqJCf/Jsg1xMdFeTJX3cqxEWM6UKJUvnM6VF1LMbFAm" +
                "aFkJXyijRGHNSI9rJ0nBfRB4FYQdNdp7ESYyiEIaoRazSmqDBRW3Zt9qoI+fErfsnmYanhx+ODr7XWZi" +
                "w5C627YoaueUKZSYyuD0QuQqzJWKqq3g1Qo9qqwMu8+gRmFXm7MHgp0PZTRjdCzkHgZpSulKODXIUgYp" +
                "RhYO1+OJctuVulbwZZDTGUTmt+RzP0hIwM8Y3nKyqpai9iAKFopMp7XRhQyAiAZYuvuxE34GVKQLuqgr" +
                "6UhxV2pD5CMnp4q448erLzUb8vXxPiHJq6IOGgItwaFwSnptxngpslobmJA2ZBsXc7uNRzUGbNvDI9Ag" +
                "LHDmAF4II/0+zvghKjcAbxgH3jGlF5u8dolHvyVwCERQM1tMxCYkf78MExu9eS2dlnmliHEBC4Brjzb1" +
                "tjqcDbM20tjEPnJcnfEtbE3Ll3TansBnFWnv6zEMCMKZs9e6BGm+ZCZFpRGJotK5k26Z0a54ZLZxSjYG" +
                "EXaxR/BXem8LDQeUYq7DJPNALrizNy51+VBo/NYY///lvyawY/6L9gAUEaN+xr4E2snaMzg3vu2Ld+cX" +
                "zRpgeFlUti4TrG9n0+9X6fzVTydHF+L1u9Pz++tFWh1ZE5BwEcEG8T7lBC1kbuvAwnNmB8iSduwcytyU" +
                "HmqT0hflDwuT4AXgSzbQqoJZpkgfQRgbkP2wxhjOVSGRIZhjzBSqmBiNGAcOLRMTJb2fCqSgW/QORKAE" +
                "GQ51lBo8izpNPmKRWRB1U8bfQlnE1gVto73gw+WqpJyzLyZ2juhySixtLST+ak4gnEgiR7YPiY3YxqFO" +
                "WMOp6zUTprT/JyZ72hoTgCBlAByQt0wV0QeAoikHYiXK9+MkoeTozS/Di5MPw/siJWvcy2mU7RBLdXCy" +
                "iDDZLSPMG7vkqrJIG01QRCv1RVPWOQZ88pB1eoyUj+KijLfO46jNaAwcIbkXk/HMiKWJvEYCQzlyqz1b" +
                "WfwS3fmeBDmicKNi2ok+f0OPfquINagkTd4ErnPCDGW93C52/ETOEMs4z5D8u2Wn4DX6rGI+Y+ooxVvl" +
                "Jy2rS5w3WXM6n8waPSvBboag8zbJ1QuAN9fTMYodN1VexX2fa2B+jBZKEj6CrV2fIoVkXzRRUEmQsPLI" +
                "5IgmxB8KxVUszOwG7UBB2JN1sBT4VGOW2ViRjG7ZMSbM2GrSnHfPJku8Px+e/AFZK7kg9lK+rkIEHuFi" +
                "mdtySQWeO8v9lY/gL09JKmZvMqrhKscUbP0Bmw4tCzZSqDtZaml2YA/FyAQSa1/L6o6RvPo7GB1ZLtmI" +
                "1WHT6dC5j100V+ns9xSW44cvK3eqCJmfHBXmlgYEzhFoD2W0P4ALyFN+rblaIDBKNdIEYIMTmyTfwP9K" +
                "LdfVgIiGuF9QWg0alcqlUI6YKPPU94BLYljmXVF8sI57R2jw0/D83Q5a3dRQ/nr49o2IDAbi0KQiocu2" +
                "cGEiuFKcvBQDMFll1VBgL86lriyXXg3EyWA84AR61+l9qh2UxiprrxDjV6hZT/7VIwv39ntHti4mx696" +
                "fdFz1gasTEKY7e/sVBbhDmuH3r+fRBUdN5qG2nZzTZaxlJaj93gWiJW9YwUqZTr0sEmjkUVeuVKqabFH" +
                "lVroXFc6LAeNBdfgFQqraEQeHnQhjl9FbLQDIVJgeaOnIHDxcIRoXyDKKsUN/CkEbJSlR8Fs9kVrAF4j" +
                "E2Dttgn2X/zl5fNIUVj0KEWcUnt3Je41Jw1/foPGAN3IxFZl66cbBw+/VGeJIvLmo0RvPva7e3FlZh1W" +
                "XjzffcaP1N0QAfK1nTcU6MjnmIpuLRu4gBRJB6RLivh2asu6oveBxodgZ70EaED7oQbK9RX49k0BCeu5" +
                "bCU7U5i+2y5RFI2PkIudRF/MJxqz1pSuPbjM0b2ALEvdILObWTycTEAytFJ5CpVA/MIStTsUnNfBJdbD" +
                "NOx0gzhHD+KWIq9sTgHlUT+XFJbpUiPNU40oyoS2g3kScfkkdiuDlDDiWbHt5xOhCFVh68YyTv6o+Zt6" +
                "CqtsU9RuUQV/Sv3FZg01MBiocmsg3q/Y+M5eCE0tA+32iTNamLIuWFQSExBxEgrMUOGaRqm1k0f8Kksn" +
                "8hhpR9ujitqrKL2OnXzcFIWXxZda+ybhtC3srXmdqsYmZQpuk6Ibt+5OO+LZMeXHugjUVjdW7JhrIF6P" +
                "UosM48FTrUH64MJtoKY8EJvpuS6hYdMMVMqM8bSGabociAzSE28mmY7b2ys+GYO1UZVPqmqXANFUigYv" +
                "bBsCzSBjR50SFNAuNakqy62tEJCQ7jLXyHHUTghMBr4zdrcv/paEappWOGmGvRviTauUbFSCFPkyKN/u" +
                "cHae6G/vwKub9C8hIJ+cPhviEL5Ikcfv+sIDZ2TVzcT6h2i5rUYrqFRyBWuZXDgqqmx5Suv4RTHR5lpW" +
                "Oo0Hj5Z92BHrkw+5L4b1LOUf6oKj7ghst2ycjO2dTNZyiVkn4ukletmLl6T+gXjarPzSLB2IZyuap3u8" +
                "stuhoaUD8XxFQ37EyosODS0diL1m5fTN+SEtHYg/d1f2nmPlZZZyPNWG5JJ3MoYy4zGBxY5GdP3KBOfx" +
                "+8jZKV0LOb6VjaaIIdocxKDgyoxNx+m7MjUlmZgVvFLU4V2rdE6Bxis0gpwBhFNp0JdXasrZM02DLNlD" +
                "weLmPMZhjo6xc4mRBrNKe1Y9UP8+Rk/x12YqKNVCAMF0OeHUKA7s6R6ElcDEgdFJ+Y+fMjrjomGAGGt5" +
                "0QHETcYoSztik8O9YD1jApZm/QCWNj2SqZIaa0yW1ELf1woVPf5xN8qpFpcw3ENKu8ZGKdiLNHncvbFq" +
                "c+fIAat+JgsV71kQQYv227L99vWxxP+N8TGplC6r+U5EeZRsvqHloZYHWBmxyZfkd4oyl7B0AZ7dqsZ3" +
                "z37QmfV/KZ40doo05jwh2+F95bWodY0NjnxNMwZr0si+IT7Y+fZUfkYr0nKSCQeEi73FHgzVqhz/Q5Pm" +
                "FKdb8s6wQ1eQGFf1QpXbctGVkUn5wg/8qU/pR+x1BiXHs8Hmoi/QkH7toyaH7kj8D0Ec7yz/un75n7y8" +
                "lWD6cXfvU0eZx3MdNDpcY9+77urTf4JouWzex5gkYHaMPRCxh2oJsp9roNgZ5ruiexwFV2evw+QNgW5h" +
                "E09fVoJTtwB0/vc0k77Ns+w/bTBuPageAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
