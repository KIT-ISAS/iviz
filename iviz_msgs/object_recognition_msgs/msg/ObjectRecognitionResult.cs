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
            if (RecognizedObjects is null) throw new System.NullReferenceException(nameof(RecognizedObjects));
            RecognizedObjects.RosValidate();
        }
    
        public int RosMessageLength => 0 + RecognizedObjects.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "868e41288f9f8636e2b6c51f1af6aa9c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VZbVMbuxX+3P0VmvDBcMeYBhJK6TAdAqTJnSTkAnfaWybDaHdlW2EtOZIW2+n0v/c5" +
                "R6v1Ar5twr1QDwxe7dHReXnOm8hs/lkV4cqpwo6MDtqaq4kf+a2zuPBVladMceicXAjXrl7FjT47+J0/" +
                "2fvzv+2L7xIrW3vIR7w5OTw+ORMP2hw/2RslS+XEmP9k2Zq4GGsvJsp7OVIiLKZKlMoXTufKCymmNigT" +
                "tKyEL5RRorBmqEe1k6TgPgi8CsIOG+29CGMZRCGNUPNpJbXBgopbs2810OWnxC17oJnOTw7Pjt78JjOx" +
                "YUjdTVsUtXPKFEpMZHB6LnIVZkpF1ZbwaoUeVlaGnW2oUdjl5uyRYOdDGc0YHQu5z4M0pXQlnBpkKYMU" +
                "QwuH69FYuc1K3Sj4MsjJFCLzW/K5HyQk4GcEbzlZVQtRexAFC0Umk9roQgZARAMs3f3YCT8DKtIFXdSV" +
                "dKS4K7Uh8qGTE0Xc8ePVl5oN+fZ4n5DkVVEHDYEW4FA4Jb02I7wUWa0NTEgbsrWLmd3EoxoBtu3hEWgQ" +
                "FjhzAC+EkX4fZ/wQlRuAN4wD75jSi3Veu8Kj3xA4BCKoqS3GYh2Sf1yEsY3evJFOy7xSxLiABcC1R5t6" +
                "Gx3OhlkbaWxiHzkuz/gWtqblSzptjuGzirT39QgGBOHU2RtdgjRfMJOi0ohEUencSbfIaFc8Mlt7TTYG" +
                "EXaxR/BXem8LDQeUYqbDOPNALrizN650+Vho/NYY///lvyawY/6L9gAUEaN+yr4E2snaUzg3vu2LD6cX" +
                "zRpgeFVUti4TrO9m0+9X6fTVjydHF+Lth9enD9eLtDqyJiDhIoIN4n3CCVrI3NaBhefMDpAl7dg5lLkp" +
                "PdQmpS/KHxYmwQvAl2ygVQWzTJA+gjA2IPthjTGcq0IiQzDHmClUMTYaMQ4cWiYmSno/EUhBd+gdiEAJ" +
                "MhzqKDV4FnWSfMQisyDqtoy/hrKIrQvaRnvBh8tVSTlnX4ztDNHllFjYWkj81ZxAOJFEjmwfEhuxjUOd" +
                "sIZT11smTGn/j0z2vDUmAEHKADggb5kqog8ARVMOxFKU78dJQsnRu5/PL07Ozh+KlKxxL6dRtkMs1cHJ" +
                "IsJkp4wwb+ySq8oibTRBEa3UF01Z5xjwyUPW6RFSPoqLMt46j6PWozFwhOReTMYzI5bG8gYJDOXILfds" +
                "ZPFLdOdHEuSIwo2KaSf6/C09+q0i1qCSNHkTuM4JM5T1cjvf8mM5RSzjPEPy75Sdgtfos4z5jKmjFO+V" +
                "H7esrnDeeMXpfDJrtF2C3RRB522SqxcAb66nIxQ7bqq8ivs+18D8CC2UJHwEW7s+RQrJPm+ioJIgYeWR" +
                "yRFNiD8UiutYmNkN2oGCsCfrYCnwqcYsspEiGd2iY0yYsdWkOe+BTZb4eHp+8jtkreSC2Ev5ugoReISL" +
                "RW7LBRV47iz3lz6CvzwlqZi9yaiGqxxTsPUHbDq0LNhIoe5kqaXZgj0UIxNIrH0tq3tG8urvYHRkuWQj" +
                "Vs+bTofOfeqiuUxnv6WwHD9+WblXRcj85KgwszQgcI5Aeyij/QFcQJ7ya83VAoFRqqEmABuc2CT5Bv7X" +
                "arGqBkQ0xP2C0mrQqFQuhXLERJmnvgdcEsMy74rig3XcO0KDH89PP2yh1U0N5S+H79+JyGAgDk0qErps" +
                "CxcmgmvFyUsxAJNVlg0F9uJc6spy6dVAnAxGA06g953ep9pBaayy9hoxfo2a9exfPbJwb793ZOtifPyq" +
                "1xc9Z23AyjiE6f7WVmUR7rB26P37WVTRcaNpqG03N2QZS2k5eo9ngVjZO1agUqZDD5s0GlnklWulmhZ7" +
                "WKm5znWlw2LQWHAFXqGwikbk4UEX4vhVxEY7ECIFlrd6CgIXD0eI9jmirFLcwL+GgI2y9CiYzb5oDcBr" +
                "ZAKs3TXB/ss/772IFIVFj1LEKbV3X+Jec9L5T+/QGKAbGduqbP106+DzL9WbRBF581GiNxv5nd24MrUO" +
                "Ky9f7GzzI3U3RIB8bWcNBTryGaaiO8sGLiBF0gHpkiK+ndiyruh9oPEh2GkvARrQfqyBcnUFvntTQMJ6" +
                "LlvJzhSmHzZLFEXjI+RiJ9EXs7HGrDWhaw8uc3QvIMtSN8jsZhYPJxOQDK1UnkIlEL+wQO0OBed1cIn1" +
                "MA073SDO0YO4hcgrm1NAedTPBYVlutRI81QjijKh7WCeRVw+i93KICWMeFZs+/lEKEJV2LqRjJM/av66" +
                "nsAqmxS1G1TBn1N/sV5DDQwGqtwYiI9LNr6zF0JTy0C7feKMFqasCxaVxAREnIQCU1S4plFq7eQRv8rS" +
                "iTxG2uHmsKL2KkqvYycfN0XhZfGl1r5JOG0Le2dep6qxTpmC26Toxo37047YPqb8WBeB2urGih1zDcTb" +
                "YWqRYTx4qjVIH1y4DdSUB2IzPdMlNGyagUqZEZ5WME2XA5FBeuLNJNNxe3vFJ2OwNqrySVXtEiCaStHg" +
                "hW1DoBlk7KjXBAW0S02qynJrKwQkpLvKNXIctRMCk4HvjN3ti78moZqmFU6aYu+aeNcqJRuVIEW+CMq3" +
                "O5ydJfq7O/BqSb9GG/YgIR+dPmviEM5Iocfv+sIDaGTW9cT7h2i6jazL4w8Ng8szxMSnRmOoW3J1a/lf" +
                "OCq47BVK+fhFodHmRlY6jQ5PlpnYSasTE7k2hvw05SbqkKNZoKBbNADA9k6Wa7nEjBSxtoc+92KP1D8Q" +
                "z5uVn5ulA7G9pHm+yys7HRpaOhAvljTkY6y87NDQ0oHYbVZevzs9pKUD8afuyu4LrOxlKf9T3Ugu+SBj" +
                "mDNWE5DscEhXs0xwGr8PnZ3QlZHjG9toihi+zUGMA67a2HScvitTUwKKGcMrRd3fjUrnFGjKQiPIGwB0" +
                "Ig169kpNOLOmSZEleyxY3J7VOAWgm+xccKShrdKeVQ/U24/Qb/ylmRhKNRdAMF1cODWMw3y6I2ElMI1g" +
                "rFL+8lNGZ1w0DBA6LS86gLjJGIBpR2yAuE+sp0zA0qweztKmJzJVUmOFyZJa6AlboaLHL3einGp+BcM9" +
                "prQrbJSCvUhTyf3brDavDh2w6qeyUPEOBhE0b78t2m9fn0r8Xxktk0rpIpvvS5RHOefbWx54ebiVEZt8" +
                "gX6vYHN5S5fj2Z1Kff/sR51n/5fiSWOnSGPOE7Id7Jdei1rX2ODI1zR/sCaN7GvizM42J/Iz2pSWk0w4" +
                "IFzszndhqFbl+N+bNMM43ZJ3BiG6nsQoq+eq3JTzroxMypeB4E89TD9irzNEOZ4b1ud9gWb1ax/1OnTH" +
                "5X8I4nhv+ZfVy//k5Y0E08ud3U8dZZ7OddDocIV977urT/8louWyeR9jkoDZMfZAxP6qJch+qoFiZ5jv" +
                "ku5pFFyevQqTtwS6g008fVkKTt0C0Pnf00z6Nsuy/wDh2jSRxB4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
