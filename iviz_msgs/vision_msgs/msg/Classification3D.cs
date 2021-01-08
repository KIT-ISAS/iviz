/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Classification3D")]
    public sealed class Classification3D : IDeserializable<Classification3D>, IMessage
    {
        // Defines a 3D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesis[] Results { get; set; }
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all detectors, so it may be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Classification3D()
        {
            Header = new StdMsgs.Header();
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceCloud = new SensorMsgs.PointCloud2();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Classification3D(StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.PointCloud2 SourceCloud)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceCloud = SourceCloud;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Classification3D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Classification3D(ref b);
        }
        
        Classification3D IDeserializable<Classification3D>.RosDeserialize(ref Buffer b)
        {
            return new Classification3D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            SourceCloud.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                foreach (var i in Results)
                {
                    size += i.RosMessageLength;
                }
                size += SourceCloud.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Classification3D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "fcf55de4cff8870324fd6e7873a6f904";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVWUW/bNhB+D+D/cKgfmhS2tyRdFwQwhq5G1wBdWqDZXooioKSzxZUiNZJypv36fUdK" +
                "itNmwB7WIIktine8++677zinDW+15UCKzjdUGhWC3upSRe0seQ6diavZ0Ry/dFPrMCxR5WBiXaTS2ai0" +
                "JWV7al3QyU7brfNN8rGiq0iwqzjoneWK8Eac0XQW+7Cgu1qXNQXdtAZ+vNvriuWzUIU2cIrTdnrPOIeC" +
                "63zJpBu1Y4Q2O3rDqmJPdfqQhTm9Et8P7WdH74o/uIxv+tbFGtGEj5+GbPAu58eCQaWiolirSDu27FVE" +
                "0GLA42461ite4WknueIQ5K0MlfjSYq/rIrltzhF2OdCTFV0DLc9/dtpnFEgZA1giYnICQXCkIzWqp4KJ" +
                "mzb2yC6wDc7fNmEXvnvvtI2vjOuqswGE21KeZkfr//lndvTrh18uKcQqn5wRlpQ+RGUr5StqOKqElGRS" +
                "613Nfml4zwZWqhEcMo59y2E1sQe/GVSDMndBkHWgUNN0VjjHFHXDDxyIqdCLWuWjLjujPAycr7SV/Vuv" +
                "GmHBPG0MgJctyHG1uRRmBi67CNrgMG1Lzypou8NLbO6A5Tlw5D9heHPnlnjmHWg0RZA5gIj5rxaVl2BV" +
                "uJRjnuUcV3APkBgHVSBFWrvFYzgBkSUKbh1IfYzw3/exBleEDnvltSoMeBGoBA5w+1SMnp4cupbQL8kq" +
                "60b/2eX9If/Fr713LGktaxTPCASh2wFH7BxaraKiT15Ko9lGMrrwyvezIzHLh8LJawEb26SdpTb4RJu5" +
                "UqceudOxBmGjlwNSXW41uPnN2LnXAf2XCfpla0vGLy25tEz1tJ5rOmiWCNjjmnWvB+AlKCWUcdsE0OAy" +
                "ideKbhz4HElVVXICFThwQ6qAFoxCgMOvNujyDpRQ6AToSN01yi5ByyoVLvlEzRteoAAhSqOg7rXrTEUt" +
                "e3FMKvszzn3u2twZUyvKv0IFaNMH5gf4/J6+XyG2FZ5TzzbOs8iP0mY1lS0XLKd+r549YT9A24IpQsK9" +
                "MsBkACQrmOheQmZFP/eyF1otGCwO0s9mQz7gmUQvHryyO6aP3y9PPyGSrXEqvnhOoUSA307aHtfVSaig" +
                "AQGyTbUz6D2FjNBRZaoq8r5eVugLG3LJW7GfZhgEXLxMc/Fxaow0sLJiYCzyA4exXxDHEsyqkfw8+566" +
                "LWBSJBlCRAUE0GNWGFcsEpuM6mX0YNCWXhdTS4/BoB5hrNkTTF3k9YSU90rGzFTzdFyaKvnQYRw5v1NW" +
                "/w2nZxWERyba0ujPfCLMOAVp5nTcIRdMCa4w697f+wkHxggc9sk8jK5Bs6orU7gpVNDfg8rcxnqoUpjQ" +
                "Qk94dnJmkjO3XW4NRk8cMxCxQorZLCegSkzc3OILXFLSKP9qgEh/H0tTnKdUckFPVo9eLc42CMN3ZezQ" +
                "PwOeB7DhvpPXMogo2oTLAn4kWiyKo9MUzp2ukGhSJibDdoenR7xOAyu7mB6TeQ5sMxQ+uyqh9ZZNGHPW" +
                "fiTI0HUDfxJIQiIckar2WqiBq1GmiLgunDPoYQR5W2iM70orS3O6CgejYHrx0xRajv8WNWthPKe3U3Jq" +
                "SA2RFH2Uq9lo4t3daPClCV59YXCBKNPh4w80H2UZOzK9g+CCeYLv8ej7WYbwZEoNeUHXcL+b3Nx4CJVO" +
                "VUCRFf4wKbSFgOlqYMc3HGxfaVMqyb9IU5ZgqXw7qpOzI3fQ8yhxLrjYH94gRzdZkwaCXdDV9c2FgLCm" +
                "03Hpt2FtTWcHu05fpKXzw12ytqbnB7ukqFj64XCXrK3pxbj0+u27l7K2ph8fLGEGrOlCcB6Gk0zGsULX" +
                "8h25JpZO/HHbbcA4Tjve5e9b7xq5wHi5lA+w5P4dD0s8kXuqWG3G72w7EaKsGwHjFMN8DxCHk0rXwVE+" +
                "6Q2YiUHeExvGVIDMDi02BDc7+gfEJWFIZg0AAA==";
                
    }
}
