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
        public Classification3D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Classification3D(ref b);
        
        public Classification3D RosDeserialize(ref ReadBuffer b) => new Classification3D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
                "H4sIAAAAAAAAE7VXTW/cNhA9V79ikD3EDnbV2k5TI8CiSGOkMZA6QeP2EgQGJXJXbClSJSm76q/vG1KU" +
                "N4kPPTSG411RnMf5ePOGWdGF2mmrAgk6u6DWiBD0TrciamfJqzCaWFerakXXnQ7zAkkHA+sitc5GoS0J" +
                "O9Hggk5W2u6c7xNCTZeRYCdV0HurJOENsGg5SPmwprtOtx0F3Q8GMN7daqn4sxGNNsDEYXt9q3AMBTf6" +
                "VpHuxV7VVfVaCak8demjAvJLxv3Utnrb/KHa+HoaXOzgR/jwcY4jVCkuxZFLEQXFTkTaK6u8iHCWt6uy" +
                "l450rWo87TlGnIB4haEWXwbsdWMkt0uxwSw7eFzTFZLk1V+j9jl4EsYgGxEOOQ49ONKRejFRo0j1Q5zq" +
                "KigbnL/pwz58+85pG18aN8rTOfablp+q7f/8U/3y/ufnFKLM5+bEIpz3UVgpvKReRZGSxFF0et8pvzHq" +
                "VhkYiZ5TkFM4DSrUhS/4zek0qOwYOKcOpOn70TLHFEXdq0/sYcl8okH4qNvRCI/9zkttefvOi14xOn4D" +
                "0qosyHB58ZyJGFQ7RtAEJ2nbeiWCtnu8pGpEDs9O2aBaXd+5DR7VHrRZDs+Fh7Pq7wHlZj9FeI4znuTg" +
                "amAjOQqnSBAhrd3gMRyDteyCGhwYfATP302xAz+YA7fCa9EYxcAtMgDUx2z0+PgA2SZoK6wr8Bnx/oz/" +
                "AmsXXI5p06FmhqMP4x4J1KF0laRmSiCt0cpGMrrxwk8VW+Ujq9UrzjE2cdtyRfCJnnKtTj1xp2NXhegZ" +
                "PVXjRsvqK7HxVgf0Wibk502MUF9YcmmVumU5V3KWJdaoh2WpND5oCBIxSdwu5WUGTPJU07UDfSMJKRME" +
                "2v0AhESDpp87HidfXqCdR9BAgPfQi27shd2AhzJVK0Gi0L1aI+0hclug2J0bjaRBecYlkeCMc3+OQ+6D" +
                "pe34TyMCJOi9Up+k5vf0/RKe1XhO/dk7r1hmhDZ1qRbXKUd9L48TOe4vuwM3mHW3wiAdcy6yTLG2paTU" +
                "9NPEeyHEHP76PvJsNYcCYrHnDOCF3Sv68N3m5GNd7YwT8dlTCi18+1ry9bByFjVCtwfIMnXOSB54rUP3" +
                "tKmWCPlqI9EFNuRCD2y+zCYINECWafcwH0rxLa8Y2LLOAC9Oa1KxrTn3QEnIS2sFzIEkN/Cngch5TALj" +
                "mnWikBETzxVMz9brZune2RXUIZRaPcIoRVCPSHgvpoXf+aw0MfKJ86Rxfi+s/geIpxL6wsNqY/Sf6pj5" +
                "cCJhfTQiDIwAJTHF3t3DhANbOA3zZB0KMrglxza5ym6C7h7cVUPs5uqEJU/oAa8cn5hEy+02O4O5ErP3" +
                "rEgILhtl50WLQZq7eY0rh8xK9tl04GY+4h44kyXZ4bj+8qZwegEH/NjG0S+MP0gXbi67WSk5eajUkpA1" +
                "UNhPSip0kjy50xIRJv1RZJTd4+kB0DKKMkB5Ssbs08Vc6AzTQsatMqGEqn0hxNxfM19Sbpg0dZUK9Yqp" +
                "gEtOpkRVNc4ZtCq8u2k0xrHUwtKKLsOByC8vfixOZb9vUKQBtit6swQl5pDgRTNFXLCKhXd3Zf/nFnh1" +
                "v3/FBufwMB1dfiDoKEZpvfQOggqicVqPCvaTnLrj6hDjmxngw6/oiY9zxAgXooYL3IJ/7SFTOlUFNRf4" +
                "h/mgLeRLy5kpX2uUfaFMqUgPC1PWXmbCULTJ2cIjBOinmQAwP7wfFpSsSJlr53R5dX3O4W/pZF75bV7a" +
                "0un9npNnaeXsYA8vbenp/R6uMVa+P9jDS1t6Nq+8evP2BS9t6YfDFaj+ls6rMoh4BpaSXInc5omrhUhu" +
                "twsYu2nD2/x9513PFxTPt+w5Fbl954MSD/jyyUYX5buyIwtQVoyAuYmZfavKOa0bbZwdeQ2C9vzfGGVU" +
                "n5R1brLsWfUvPKZRkSkNAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
