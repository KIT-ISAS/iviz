/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectInformation")]
    public sealed class ObjectInformation : IDeserializable<ObjectInformation>, IMessage
    {
        //############################################# VISUALIZATION INFO ######################################################
        //################## THIS INFO SHOULD BE OBTAINED INDEPENDENTLY FROM THE CORE, LIKE IN AN RVIZ PLUGIN ###################
        // The human readable name of the object
        [DataMember (Name = "name")] public string Name { get; set; }
        // The full mesh of the object: this can be useful for display purposes, augmented reality ... but it can be big
        // Make sure the type is MESH
        [DataMember (Name = "ground_truth_mesh")] public ShapeMsgs.Mesh GroundTruthMesh { get; set; }
        // Sometimes, you only have a cloud in the DB
        // Make sure the type is POINTS
        [DataMember (Name = "ground_truth_point_cloud")] public SensorMsgs.PointCloud2 GroundTruthPointCloud { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectInformation()
        {
            Name = string.Empty;
            GroundTruthMesh = new ShapeMsgs.Mesh();
            GroundTruthPointCloud = new SensorMsgs.PointCloud2();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectInformation(string Name, ShapeMsgs.Mesh GroundTruthMesh, SensorMsgs.PointCloud2 GroundTruthPointCloud)
        {
            this.Name = Name;
            this.GroundTruthMesh = GroundTruthMesh;
            this.GroundTruthPointCloud = GroundTruthPointCloud;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectInformation(ref Buffer b)
        {
            Name = b.DeserializeString();
            GroundTruthMesh = new ShapeMsgs.Mesh(ref b);
            GroundTruthPointCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectInformation(ref b);
        }
        
        ObjectInformation IDeserializable<ObjectInformation>.RosDeserialize(ref Buffer b)
        {
            return new ObjectInformation(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            GroundTruthMesh.RosSerialize(ref b);
            GroundTruthPointCloud.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (GroundTruthMesh is null) throw new System.NullReferenceException(nameof(GroundTruthMesh));
            GroundTruthMesh.RosValidate();
            if (GroundTruthPointCloud is null) throw new System.NullReferenceException(nameof(GroundTruthPointCloud));
            GroundTruthPointCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += GroundTruthMesh.RosMessageLength;
                size += GroundTruthPointCloud.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectInformation";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "921ec39f51c7b927902059cf3300ecde";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1Xa2/bNhT9LiD/4aL50KRIPCTpuiBDMKS10xhL7KB2CqxFYdASbXOVSZWknKi/fueS" +
                "ou12KbACa4U8JIr33Pe51O7u91z0tj+6u7juv7sY94cD6g8uh/RdAOsrewx9fNUfRdDR1fDuuksvezR8" +
                "Ob7oD3pdrHd7tz38GYyv/6LLN8Mb7O/Rq+Gb3gFd9//sYQddDOjN2/47ur2+e43HxxRn0LOQtKiXQpOV" +
                "ohDTUpIWS0lmRh6vzPRvmfvMeav0PLxJQrO6LGkp3eLLrWe4V45yAE4l1U5iH82MpUK5qhQNVbWtjJPu" +
                "gEQ9X0rtZcGqS+Ub6nQ6NK09KZ8ApmoOfTfioyRXWxkU+aaSBB03vdFV5haikpOlm7tfbtiYuTW1Libe" +
                "1n4xYfPY3pFZSq/wdECNqcnosqGFWEkSlJemLkjpgNx9+U1lt8P+YDzKnNTO2Kjv1ijtX7H88ZdqK34x" +
                "CcjZTnb+P1872c3o9Rl95fgOLO/KmdLKK6M5J4KS+6VyPmTJKqHnpXS/B9eULuQDrURZS4cUzKQlbwjJ" +
                "CRCOg7KS1qtcuvcfMlYybgHef9hgsQJGE7mvRbmWwJrwtORY1lXYEKyZS06FbbYiCLAk9NOilRx5LGrJ" +
                "s6duY1cNO0+O359EU+XDBLH7sQY/Eig2dhyay2gvFDLEYU35isaH0uPMzaxEFVcil9msNMK/eE4P67tm" +
                "fff5B4b88VZZe4F6cGIO9jFl4bgTTVmCQFpXBocFGlY7PKKqglvo3vuFyheoqgYgbRRIFEUIALYpDaZZ" +
                "ioDhauwUjjSvlJAFAuP55oCkzztMYkCJASuEF9zlzhsLPoKYAPVoYRualmYKYe8I7GVAToV0uVVTbJs2" +
                "nIHWFDCZS1T4ZKYknHpCwlrRdBJlRl2RcYJGOMIkZ+xcaPUZiMcF7aklonJYqo9yH2/oqID0Xg03Cgnb" +
                "9jt0u4FxW7IwGuJB2iXkypqizoOpbCbo2wo4UPlFmx63jpPzgDeskZny0MwOZ6WaL3y0HkvsXBSKxov8" +
                "U61i7YHLdRE8zw3sROA8BoTlOdLv0h7T/wm7EdO438muMGtAN4vwjxUcd2GArcEh4N02ilvh6lA/rrV0" +
                "7WgdkAOgsJ1YBM5RsOReFfAQu1iklHqOp0dA27ZuAdJTEGabum2iI0y+EFrL0iVXlU0F0Q6Ptl5CbLho" +
                "OllI1CWXAjgulkSWTY0pCZdyEww3CR7BpNulPmuBxVE8vfgjGRXtniBJFWR36Xrt1FbPTxu/5ioia+7T" +
                "/q8l8OrL/acwMGhO1y5dRD7f9McBOdQZR3UvQT+LkdtvvYJLBQpEbkDGtoZEiDzyKvCrDTRj5qiirYYf" +
                "SEC+iOwTq42JZ+SRPWELkI8XwWGuzQWckPawlCtZQkosK3RMeMvT33USY+EHeUELlTg/4GiDOjCo+OWy" +
                "1irnkg9njG15SDJDUSV4kNSlsP/qEEbHj5Ofaqlzbpgz5hMn89orGNQAIccByfEBDN2U8guBbHd8bw6Z" +
                "1+Y8u5PyOHphrHyoLEg2cMMZdDyLznWAzfQMLSCQvbA2waPbR4bZBFkZcMIeLL9t/MLE8l4JzEU+HobT" +
                "Hai6oKcs9HR/C5nNPsMxUZsEHxE3Ov4LrF7jBipC4xUle+/qOQKIjaC1lSrWDIx2VqBfnHOmFi2YsVRU" +
                "me1eBhbatBb+C+dMrpAAJgq0enu4DdmYqOInTsTADd8YiOxXHDVVmolGJ/6Ct6CaSDwQ3z6JJpQ4CSPH" +
                "neJ7YHzKLXlOR+3KXbt0TsebPUcvwsrJ1h5eOqfnmz0n4Bas/Lq1h5fO6UW7cnk9vOClc/ptewWnjXM6" +
                "zba/JRJNDNovjsCRqcDNbOakjxuG8X5mzZITa8NxNoYijo1WUchxOLJDqJvupa558MVJ5XAuElOzkklP" +
                "jpN7UAORKxAjvoUakqXkb5NwCOZMRMt2sn8AOX59RiQOAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
