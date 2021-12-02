/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Classification3D : IDeserializable<Classification3D>, IMessage
    {
        // Defines a 3D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesis[] Results;
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all detectors, so it may be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud;
    
        /// Constructor for empty message.
        public Classification3D()
        {
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceCloud = new SensorMsgs.PointCloud2();
        }
        
        /// Explicit constructor.
        public Classification3D(in StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.PointCloud2 SourceCloud)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceCloud = SourceCloud;
        }
        
        /// Constructor with buffer.
        internal Classification3D(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Classification3D(ref b);
        
        Classification3D IDeserializable<Classification3D>.RosDeserialize(ref Buffer b) => new Classification3D(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results);
            SourceCloud.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Results is null) throw new System.NullReferenceException(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) throw new System.NullReferenceException($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (SourceCloud is null) throw new System.NullReferenceException(nameof(SourceCloud));
            SourceCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Results);
                size += SourceCloud.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/Classification3D";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "fcf55de4cff8870324fd6e7873a6f904";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW/cNhC961cMuofYwa5a22lqGFgUaYw0BlInQNxegsCgpNkVG4pUSGpd9df3DSmt" +
                "14kPPTQ1bO+K4jzOx5s3XNAlb7TlQIrOLqk2KgS90bWK2lnyHAYTy2JRLOim1WFaoMbBwLpItbNRaUvK" +
                "jtS7oJOVthvnu4RQ0lUk2DUc9NZyQ3gDLNofxD4s6a7VdUtBd70BjHc73bB8VqrSBpg4bKt3jGMouMHX" +
                "TLpTWy6L4jWrhj216aMA8kvBfWhbvK3+5Dq+HnsXW/gRPnyc4ghicdOyRN6oqCi2KtKWLXsV4axs53kv" +
                "HemSSzxtJUacgHiVoRpfeux1QyS3SbHBLDt4XNI1kuT586B9Dp6UMchGhENOQg+OdKROjVQxcdfHsSwC" +
                "2+D8bRe24ft3Ttv40rihOZ1iv63lqVj/xz/Fb+9/vaAQm3xuTizCeR+VbZRvqOOoUpIkilZvW/Yrwzs2" +
                "MFKdpCCncOw5lDNf8JvTaVDZIUhOHUjTdYMVjjFF3fEDe1gKn6hXPup6MMpjv/ONtrJ941WHui9kW0Ba" +
                "2YIMV5cXQsTA9RBBE5ykbe1ZBW23eEnFgByeIX/8uVjc3LkVHnkL2uwPz4WHs/xXj3KLnypc4IynObgS" +
                "2EgO45QGREhrt3gMx2CtuMC9A4OP4Pm7Mbbgh3Bgp7xWlQEZAtXIAFCfiNGT4wNkcfuCrLJuhs+I92f8" +
                "G1i7x5WYVi1qZiT6MGyRQGycuqqhakwgtdFsIxldeeXHQqzykcXileQYm6RtpSL4RE+5WqeeuNOxLUL0" +
                "gp6qcaub4huxcacDei0T8ssmRqgvLLm0Su1+OVdykiXRqMdlaW580BAkEpK4TcrLBJjkqaQbB/pGUk2T" +
                "INDuByCkKjT91PE4+eoS7TyABgq8h160Q6fsCjxsUrUSJArd8RJpD1HaAsVu3WAa6tkLLqkEZ5z7NPS5" +
                "D/ZtJ/8qFSBB75kfpOaP9P0KnpV4Tv3ZOc8iM0qbcq6W1ClHfS+PI2EzsrUBN4R1O2WQjikXWaZE21JS" +
                "SvpllL0QYgl/eR95tppCAbHEcwHwym6ZPvywOvlYFhvjVHz+jEIN376VfD2unLMaodsDZJlaZ9BnCsGg" +
                "e+pUS4R8vWrQBTbkQvdivp9NEGiA7Kfd43yYi29lxcBWdAZ4cVwSxxp0ahkoCXnfWgFzIMkN/Kkgch6T" +
                "wLhqmShk1ChzBdOz9rrad+/kCuoQ5lp9h1GKoL4j5b3CDJkrnc5KEyOfOE0a57fK6r+BeNpAX2RYrYz+" +
                "xMfCh5MG1kcDwsAI4AZT7N09TDiwhdMwT9ZhRga3mqFOroqboLsHd7mP7VSdsM8TesCzkxOTaLnNamMw" +
                "V2L2XhQJwWWj7LyqMUhzNy9x5UgD+qvpIM18JD1wJmHkMh6XX98UTi/hgB/qOKBVpiwepAs3l7yWk4dK" +
                "7ROyBIr4iUXgnCRP7nSDCJP+MBm2Wzw9AjqPogwwPyVj8elyKnSGqSHjlk2YQ9V+JsTUXxNfUm6ENGWR" +
                "CvVKqIBLTqZEUVTOGbQqvLutNMZxo5WlBV2FA5Hfv/h5dir7fYsi9bBd0Jt9UGoKCV5UY8QFa7bw7m7e" +
                "/6UFXj3cfw4H08nzD/QctZg7L72DnoJnktWjGfppztzxFBVCgnDhkrYHufGQIp0yj7oq/GEGaAuJ0s3E" +
                "huL/Up9UiMfFJ+urVLuf9cfZmStobJQ1Fxnmh3fAGSWrTubTOV1d35xL+Gs6mVZ+n5bWdHq/5+R5Wjk7" +
                "2CNLa3p2v0fqiJUfD/bI0pqeTyuv3rx9IUtr+ulwBcq+pvNiHjYy5+aSXMt3BJj4OJPFbTYBozVteJu/" +
                "b7zr5BLi5SY9pSK36HRQIoVcMMXocv7OdhCRyaoQMBsxl3c8n1O7ATD5nNcgIWbySGwYSg/1nBope1b8" +
                "A7yWssYNDQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
