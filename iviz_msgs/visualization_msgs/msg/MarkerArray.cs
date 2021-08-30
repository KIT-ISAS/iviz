/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/MarkerArray")]
    public sealed class MarkerArray : IDeserializable<MarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public Marker[] Markers;
    
        /// <summary> Constructor for empty message. </summary>
        public MarkerArray()
        {
            Markers = System.Array.Empty<Marker>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MarkerArray(Marker[] Markers)
        {
            this.Markers = Markers;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MarkerArray(ref Buffer b)
        {
            Markers = b.DeserializeArray<Marker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new Marker(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MarkerArray(ref b);
        }
        
        MarkerArray IDeserializable<MarkerArray>.RosDeserialize(ref Buffer b)
        {
            return new MarkerArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Markers, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in Markers)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/MarkerArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d155b9ce5188fbaf89745847fd5882d7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71X227bOBB9Xn3FAEHRZOHIuXS73QB+SGM3MZDb2m67RVEYtETbbGRRJam47tfvGVKS" +
                "4yZp92HTxIgpijNz5naGuRDmRpqPn2jhFzaKOv/zT3QxPD2iW2VLkalvwimdjxd2ZtsX3mK0RUMpae5c" +
                "cdRuL5fL2GgbazNrL9WNaptb9a3dVbbIxGq0KmQtRiJPfyw0Kp02SmS1hH12ePzsYO+1sCrB93AuoI2m" +
                "2tBCG0kqx3Lh4RE+pVX5jNxcWVpIa8VM0lK5ObHqKCpV7l7R8WBw9b6zVz2dvH3d6+xXD8Prs96g1zmo" +
                "33047192e4POYbWBx954OBr0rzsv7m6d94ejzh93NIadlxtqw96f1d71Vf9yNOy8qh5HvX9G43f93vvx" +
                "m+OT/uVp56/qxUVveDYe9IZXbwcnAFrDBobjy9PzSun+fuNct9u4dnHV7b/50Dx2e+e90dq58Hh8fg7v" +
                "ojMpUmRnHr4e+dmq33P0nVrI9tSIxUYOIusMZyC3jykJii4hZwuRSHKaUCS84KTpyWeZOGiM4xjJlCmW" +
                "lOj8c5knPsc+myplscRI4SQJKnP1pZSUMxYPbS4rRRE8PTzg87/99gM0ldV+l01Oy+xBo6w1b2D7AhS5" +
                "KsoMjsNjLuxUZtKFAqwREF6jWwIOh054HMkWcaeQnm6CFwHD41J7JNK0vdCpmq4Ao5Ju0T5tp7IwMgGC" +
                "dKdFBwEfuufOocP1ZpZVuzaaSb2QzqxCx19rK6ngP/eN+3eAfCfmm8LvsKXNIdlEZPI74aHf25Cm/RZ+" +
                "0bwCJZTKqSgzR9sls1C2In6BcJL9Ugojd1BtabByojNtBqevj5E3rL6z49/Sx714b3c/3vsUpaUJjJGp" +
                "qeRCfjiwZ3pJmd5Mp53rMkuRVetoIqdMQRPp8w/e4h5IPNAQ1DQmZCf4wkdvUQkTrTPyfTPOdHKDCn/Q" +
                "dn9asVigzcrsRAbR3SDaIhXLmAyiDRPchL5h0BvK2XCS2OjKd6t1soiirasc+EJrhcD7qrSFTNRUYXcu" +
                "LFlkkM/U7bSgbTmLK85q3aHBFqFRd+7VCzBgNhX8bZ/G4lZeLiYIDIrHZxyRKpESiU7FLuK0R1AkUScZ" +
                "c4Vv3kaiBnZ5NeodofCLuSDEOteOVtJ5qA+UFjwKphBECqK68czDll9dM5ArIuS9x85vUPv3gsjXfGyk" +
                "1aVJZKgavwXpsYQjaSoBkMmFh+VTzf8mCmFA8NR3YDphUu5EkQonvCtzNUPcdzNUWwYhsSjgo3/LubYx" +
                "BEdczvjMZC6NbxIfCCZyvViAw5mnqkK9Iw9JsLGgQhg0F9jW4Lw2qcr5uK9x1o6PRbZlDm7ud4+Yvq1M" +
                "SqcAaAUNPCz83QAk7wcgmBUC0dZoqXfxKGfSrI2jXoRjsPIrCNQyTmGPYOP34FwM3QiOhJXU0rbfG+PR" +
                "7hCMAIIsdDKnbSC/Xrk5mIYL8FYgVxMwHhQzTUDrcxZ6vnNHc+5V5yLXtfqgcW3jv6jNG73s0+4cOcvY" +
                "e1vOEEAcLIy+VSghmqy8kiRTMse0UhMjzCrypOhNRltvPI94NvIZwbewVieK54qfjXXNBlpT6VNV4/2x" +
                "BAePwX+cJMCvroHTMKwQpamRTDQY2C2uMt5Oq/fKn+WpjetmLRtT5MmrORD9XXKL5V7v+tyvchBQ6s5B" +
                "LTihMEg4Ww1++CICnW26G00zLdzLF/S1Wa2a1bdfA38dutqHJlHhCrKO5yZ4fvqyjjvPtTj6iUf1avlr" +
                "fKtuNQ85Rrf+3aZLMRNU31OKnwF8J3DMfY0kBFOFy1oowxHY1N8uMOIdpVr66QQdC3EDlRL9zdKiKKAM" +
                "JMvzPwuh9NMf0zOexS1azmUeTvk7CqPw/KsSMmrG9+j65tAIC6qca5GbHqC/+V7ImIMxlB+UGB0StxPz" +
                "TWWlS1qyQ1iYivY1D+Eal6cnp3XLj/ig4oFab/5nQ507DJyfZv1pUn1/9AeTGBmmWc2a1aRZiSj6F7EF" +
                "oHCZDwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
