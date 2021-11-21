/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionResult")]
    public sealed class ObjectRecognitionResult : IDeserializable<ObjectRecognitionResult>, IResult<ObjectRecognitionActionResult>
    {
        // Send the found objects, see the msg files for docs
        [DataMember (Name = "recognized_objects")] public ObjectRecognitionMsgs.RecognizedObjectArray RecognizedObjects;
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionResult()
        {
            RecognizedObjects = new ObjectRecognitionMsgs.RecognizedObjectArray();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionResult(ObjectRecognitionMsgs.RecognizedObjectArray RecognizedObjects)
        {
            this.RecognizedObjects = RecognizedObjects;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ObjectRecognitionResult(ref Buffer b)
        {
            RecognizedObjects = new ObjectRecognitionMsgs.RecognizedObjectArray(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionResult(ref b);
        }
        
        ObjectRecognitionResult IDeserializable<ObjectRecognitionResult>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
                "H4sIAAAAAAAACsVZbW8buRH+vkD+AxF/kH2QlSZO3NSFUTi2c8khiXOxD+01CAxql5IYr5YKybWkFP3v" +
                "fWZmuVrbSpuXsyvY8Io7HM7LM2905oYfTR7PvcnduLLRuup8GsbhwTtZ+GyKE6Y48F4vlW9Xz2VjuJft" +
                "/8Gfe9nr05/31DcJdi/b+J6PenF8cHT8Tn3XZvlkL4wujFcT/pNlG+psYoOamhD02Ki4nBlVmJB7OzRB" +
                "aTVz0VTR6lKF3FRG5a4a2XHtNWm4B4JgonKjRv2g4kRHletKmcWs1LbCgpGt2dda6P2HxC37TjOdHh+8" +
                "O3zxQ2Ziw5C62y7Pa+9NlRs11dHbhRqaODdGVFshrBV6VDoddx5BjdytNme3hrwQCzGkuBbYUqdRV4X2" +
                "BfwadaGjViMHn9vxxPjt0lwauDPq6QxS81tyexgkMOBnDId5XZZLVQcQRQddptO6srmOQIkFXrr7sROu" +
                "Blq0jzavS+1Jd1/YishHXk8NccdPMJ9qtuXLoz0CUzB5HS0EWoJD7o0OthrjpcpqW8GKtCHbOJu7bXw1" +
                "YyC3PVywBmEBNQ/8Qhgd9nDGT6LcALxhHTioKoLa5LVzfA1bCodABDNz+URtQvK3yzhx4tBL7a0eloYY" +
                "57AAuPZoU2+rw5nE3lOVrlxiLxxXZ3wN26rlSzptT+CzkrQP9RgGBOHMu0tbgHS4ZCZ5aRGMqrRDr/0y" +
                "o11yZLbxnGwMIuxij+CvDsHlFg4o1NzGSRYAXnBnb5zb4vYA+bWB/n/Mgk14SxYUkwCNiNQwY3cC8GTw" +
                "Gfwrb/vqzclZswYknuelq4uE7Os59dtVOnn2y/HhmXr55vmJ+iGtDl0VkXYRxBVCHgkL1ld66OrIwnN+" +
                "B86Sduwdyt+UIWqsi+8ohTiYBC+AYLKBNSXMMkUGiapyETkQawzjock1kgRzlGRh8kllEeaAomNioqT3" +
                "U4UsdI3egwiUIMOhnrJDYFGnyUcsMgtirsr4JZgJuM5oG+0FHy5aBaWdPTVxcwSYN2rpaqXx13IO4Vwi" +
                "HNk+JDbCG4d65SrOXi+ZMCX/PzHZw9aYAAQpA+CAvGVqiD4CFE1RUCtRvh0nCSWHr347PTt+d/q9SMka" +
                "93ImZTtIwY5eQ3sSeqcQmDd2GZrSIXM0QSFW6lPGZ59QDITkIeftGFkf9cVUwfmAozbFGDhCc1Om5UzB" +
                "0kRfIoehIvnVnq1MHsSdb0mQQwo3Kqmd6AtX9Oi3irgKxaRJncA1sF+hEI3xsHgQJnqGWMZ5Fcm/U3Rq" +
                "XqPPKuYzphYpXpswaVmd47zJmtP5ZNboUQF2MwRdcEmuXgS8uaSOUe+4tQoQhd5+rIH5MRopTfiIrvZ9" +
                "ihSSfdFEQalBwsojmSOaEH+oFRdSm9kN1oOCsKfr6Cjwqcwss7EhGf2yY0yYsdWkOe87Wy319uT0+A/I" +
                "WskF0lGFuowCPMLFcuiKJdV47i/3Vj6CvwIlKcneZNSKCx1TsPUHbDp0LdhIoe51YXX1APYwjEwgsQ61" +
                "Lm8YKZi/g9Gh46qNWD1tmh069+7r5iqh3fuR2nJ0+5XlRiEhD5Cv4tzRpMBpAk2iFhcAu0A9pVj4CJkf" +
                "sVGYkSUMVzixyfNNBFyY5boyIICQ/Yoya7QoVj5Fs8CiGKbuB1wSw2LYFSVE57mDhAa/nJ68eYCGN7WV" +
                "vx+8fqWEwUAdVKlO2KKtXRgNLiiPoaVjDCarrHoK7MW51JsNdTADdTwYDziH3vQ6lmWGKp27QJhfoGzd" +
                "/1ePLNzb6x26Op8cPev1Vc87F7EyiXG29+BB6RDxsHbs/fu+qEjVjcSjrAIzEG9kWfEeTwRS3DtWoGpm" +
                "Yw+bLNpZpJYLY5pGe1SahR3a0salTApmHWChsBEj8ghhc3X0TLDRTobIgsWVtoLAxVMSAn6BQCsNt/HP" +
                "IWCjLH1VzGZPtQbgNTIB1q6bYO/JX54+ForcoU3JZVzt3ZS415x0+usr9AZoSCauLFo/XTn49FP5IlEI" +
                "bz5K9ebjsLMrKzPnsfLk8c4j/koNDhEgZbt5Q4G+fI7Z6NpyBReQIumAdGEhb6euqEt6D7FKE92slwAN" +
                "aN/eZLm+DN+7fmtA8pLLV6amSH2zXaA0VkFQJ/1EX80nFkPXVC/BhIoP3RHoomBfgKybXAL8TFiqaKXE" +
                "XhoBwS8uUcFjztkdXKQqpqmnG8dDdCJ+qYalG1JMBVRRlNvYXnCkwaoRBQBs+5j7As370rMMUs6Qs6T5" +
                "5xOhCNVi58dabgFQ+TftFFbZpsDdojr+kLqMzRpqYDwwxdZAvV2xCZ29EJoaB9odEmc0MkWds6gkJlDi" +
                "NRSYoc417VJrp4AQNug/ZUbedqPtUUlNlkhPwyKUk00ivM4/1VZmgP6qkb02uFPh2KRkwc2SuHHr5syj" +
                "Hh1RiqzzSM11Y8WOuQbqpayJ8eCp1iB9cOFm0FIqkJZ6bgto2LQEpanG+LaGabolEAbpG28mmY7amyw+" +
                "GRN2ZUrJcliwPgGiKRYNXtg2BJpBxo56TlBA09Rkq2zoXImYhHTnQ4s0R02FwnwQOvN3++JvSaimdYWT" +
                "Zti7oV61SulGJUgxXEYT2h3ezRP99R14dZX+KQTkk9NnQx3AFyny+F0f1xSfuSZsJtY/ieW2Gq2gEkoo" +
                "MnPL5MxTXWXLU2bHL+qJrS51ifInaLjDBMSu+EL+IQ9KZM9SCqJ2WNRHbMOz4mds72SzloskHoHUUzS1" +
                "Z0/JAvvqYbPyW7O0rx6taB7u8spOh4aW9tXjFQ25EitPOjS0tK92m5Xnr04OaGlf/bm7svsYK0+zlOmp" +
                "QiSvvKFnKMiQTHhxoxHdxjLBiTyPvJvSFZHnS1oxhURpcxDjguszNh2lZ1PVlGckMQQDvw/dpUnn5Gi/" +
                "+BhseQEcTnWFBr00U06gaSxkyW4PGVdHM4LEEbWOnQuNNKSVlgYjIJh6+TGai782E0JhFriXK+miwpuR" +
                "DO/pToT1wPSBMcqE9x8yOuSsYYBIa3nRAcRNS6ylHdLtcFNYU/9kRJr1w1jadGfWSoqss1rSDD1gK5f4" +
                "/f2OiGoW57Dd7Qq8xlBt1DddQ1hzh9Xm0ZEHaMNM45qFb14QSov2adk+fb47Db4wUrZapUtsvijBHRi6" +
                "ALq55UmXp1otIOXL8xs1mitauhjPrhXnm4ff8iD7v3RvlfaGlOa0IfchV30nitfY4cnjNHiwMiI+mLxz" +
                "8+2p/ojmpOUkrWPTKuwudmGrVmv5/00aXrxtyTsTEF1NYoa1C1Ns60VXRibli0Dwp86lLwjsTE+eB4bN" +
                "Ba51+upzH1U6dufkfyjieGP59/XL/+TlrQTW9zu7HzrK3KX3yF8Ha0x802N9+j8RLRfNewlOgmfH3gMl" +
                "jZVKBNmvNbDsK+a7orsrHVenr0XmFZmuIRTfPq1kpxYCGP3vKSc9zaHefwCrUf/I0B4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
