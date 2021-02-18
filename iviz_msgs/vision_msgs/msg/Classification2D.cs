/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Classification2D")]
    public sealed class Classification2D : IDeserializable<Classification2D>, IMessage
    {
        // Defines a 2D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide class probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // A list of class probabilities. This list need not provide a probability for
        //   every possible class, just ones that are nonzero, or above some
        //   user-defined threshold.
        [DataMember (Name = "results")] public ObjectHypothesis[] Results { get; set; }
        // The 2D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all use cases, so it may be empty.
        [DataMember (Name = "source_img")] public SensorMsgs.Image SourceImg { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Classification2D()
        {
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceImg = new SensorMsgs.Image();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Classification2D(in StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.Image SourceImg)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceImg = SourceImg;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Classification2D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceImg = new SensorMsgs.Image(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Classification2D(ref b);
        }
        
        Classification2D IDeserializable<Classification2D>.RosDeserialize(ref Buffer b)
        {
            return new Classification2D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            SourceImg.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Results is null) throw new System.NullReferenceException(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) throw new System.NullReferenceException($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (SourceImg is null) throw new System.NullReferenceException(nameof(SourceImg));
            SourceImg.RosValidate();
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
                size += SourceImg.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Classification2D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "90b6e63f97920f8c0f94c08bd35cb96d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VW224bNxB9XyD/MIAfYieSkqZFEQQoenPT+KEXIEFfgsCglqNdJlxyS3Ily1/fM+Tu" +
                "SnKcIA+NIFsSl3M4lzOHc0aXvDGOIyl6dkm1VTGajalVMt5R4DjYtHpQneFNb1oTxyXSHibOJ6q9S8o4" +
                "Um5PvY8m2xm38aHLGCu6SgQ7zdE0jjXhiYDRfBaHuKBda+qWoul6C5zgt0Zz2SG/1mptLKBxZmO2jNMo" +
                "+iHUTKZTDcPBB9UrVpoDtflDFs7oZ7ImJvKb+5BWJZ68wzEck2imk9XR3v3BZd5yyGFGs7ajfwt6P8gh" +
                "ksPUqkQqMLDcLQe/IB9Irf2W4W/HBWSIHJY6Z13DAgltvdWI4a/1e67Tq33vU4tsxbfvxmzHEs6blqVG" +
                "WiVVTmrYcVApw3DkaTedmxWv8KuRWiAQOKws1fjSY68fJCXFF9iVFF6s6E/EH/jfwYRSJVLWirNUq8gI" +
                "M3oyiTq1pzUTd33aw+fILvpw3cUmPrkSoLEw16Zrqh/+51f1x+vfX1BMupxXKo44XifltAqaOk4qp0fc" +
                "b03TItMWRbMwUp0EX5K371H/idF4l0RaUA/xIpsetO66wUkfMCXT8Yk9LIXx1KuQTD1YFbDfB22cbN8E" +
                "1bGg4x2RT3bg6dXlC2mVyPWQwGCcZFwdWEXjGjykajAufftMDKqzNzu/xE9uwOf58FJyOMs3PQotfqr4" +
                "Amc8KsGtgI3kME7RoEBeu8bPeIG2Ehe492ixc3j+9z61YIYUf6uCUcJlANfIAFAfitHDiyNkl6Gdcn6C" +
                "L4iHM74E1s24EtOyRc2sRB+HBgk0cWo+Tet9BqmtYZfQoeugwr4Sq3JkdfZScoxNIixSEXyiFX1tcjfs" +
                "TGqrmIKg52pcG119JTZuTUSXFULebWCRIEc+r1I7L5dKjsIpKnq/cB6aHkQEjYQmkDLJzAiZ9Qc65kFg" +
                "6I7WGQStfgQj8jOkqdtx+NUlWnkAExSoD7Foh065Jaio1axpqHXHi6yN0hmod+sHq6nnIMCkCp71/sPQ" +
                "l16YW0/+raEYK3rNfJKef/L3K/i2wu/co52HWGqYGitiUkpm9CH0Yxn20mZuA4oI+bbKIidjQgCBhIi4" +
                "5cys6Je97MVVITlYHIVfzMZ4QDDxXhCCchCvt0+X37yDJxvrVfr+O4o1HPxaOnZXOidBQsNHUdKZIcqB" +
                "AlCkqe/NuPv86YKeXmTyJ4hWD63bCLECaiaZGfdVp3cjja8zGpcPEjOmBfqualwEIylz481wdN9rxpra" +
                "7QjK9xBJkDI/ypcxPoP6FJAPpkFNsK8YfAxUo6olwM8jPb6ZeeshpyLrudK4F9JU9s/G9Hh/CqD9zn2Z" +
                "4e2pIf68HN9b5WbOfhbhquyZ04mbDEVx+nR1RPo1p0Ea61NwE6XuiOTBj9xZ1tTpUwiyc82t2hqfxXpw" +
                "4whTTVdXyzmxB5MCXJYX0/W1IDd061K+4Hdxst4ZDX8+ss7L9xrX3g6di9UoFJYbUCN3d8zSApHwWuRE" +
                "5jEUbWOgbjHUTzLw9fQ4ruoeF4okfO8H2qlClDiOFOZWhkHHO5rukyysGPpQWJgFH5cy0MWfRCzjqkw/" +
                "2ISp1Ikoo2RodC09zB1kbpzHsmMZd3JkVU36N3s+peK3aQFh9+aGbaTlkmrcoA6jTcfK4aHMmujA/C3C" +
                "7fsLKZVUHzBBb4LvclGnEbkcHiVVuNztoPnJsULdzVpb6v4cVbleGwxQ2iDEUrl4dDPPz36cR5zE/YlD" +
                "LwcMmuACaugakAAerPeJCzWeYwrOQEcGqk4Dio06BHOTn5aQ5eTzDP8oc+uiqv4DxUPfYeAMAAA=";
                
    }
}
