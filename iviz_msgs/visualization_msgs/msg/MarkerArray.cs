/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MarkerArray : IDeserializable<MarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public Marker[] Markers;
    
        /// Constructor for empty message.
        public MarkerArray()
        {
            Markers = System.Array.Empty<Marker>();
        }
        
        /// Explicit constructor.
        public MarkerArray(Marker[] Markers)
        {
            this.Markers = Markers;
        }
        
        /// Constructor with buffer.
        internal MarkerArray(ref Buffer b)
        {
            Markers = b.DeserializeArray<Marker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new Marker(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MarkerArray(ref b);
        
        MarkerArray IDeserializable<MarkerArray>.RosDeserialize(ref Buffer b) => new MarkerArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Markers);
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
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Markers);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/MarkerArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d155b9ce5188fbaf89745847fd5882d7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1X227bOBB9Xn3FAEHRZOHIuXS73QB+SGM3MZDb2m67RVEYtETbbGRRJam47tfvGVKS" +
                "4yZp92HTxIgpijNz5naGuRDmRpqPn2jhFzaKOv/zT3QxPD2iW2VLkalvwimdjxd2ZtsX3mK0RUMpae5c" +
                "cdRuL5fL2GgbazNrL9WNaptb9a3dVbbIxGq0KmQtRiJPfyw0Kp02SmS1hH12ePzsYO+1sCrB93AuoI2m" +
                "2tBCG0kqx3Lh4RE+pVX5jNxcWVpIa8VM0lK5ObHqKCpV7l7R8WBw9b6zVz2dvH3d6+xXD8Prs96g1zmo" +
                "33047192e4POYbWBx954OBr0rzsv7m6d94ejzh93NIadlxtqw96f1d71Vf9yNOy8qh5HvX9G43f93vvx" +
                "m+OT/uVp56/qxUVveDYe9IZXbwcnAFrDBobjy9PzSun+fuNct9u4dnHV7b/50Dx2e+e90dq58Hh8fg7v" +
                "ojMpUmRnHr4e+dmq33P0nVrI9tSIxUYOIusMZyC3jykJii4hZwuRSHKaUCS84KTpyWeZOGiM4xjJlCmW" +
                "lOj8c5knPsc+myplscRI4SQJKnP1pZSUMxYPbS4rRRE8PTzg87/99gM0ldV+l01Oy+xBo6w1b2D7AhS5" +
                "KsoMjsNjLuxUZtI/rBEQXqNbAg6HTngcyRZxp5CeboIXAcPjUnsk0rS90KmargCjkm7RPm2nsjAyAYJ0" +
                "p0UHAR+6586hw/VmllW7NppJvZDOrELHX2srqeA/9437d4B8J+abwu+wpc0h2URkmwrAIH5vQ5r2W/hF" +
                "8wqUUCqnoswcbZfMQtkKLkEzCtB+KYWRO6i2NFg50Zk2g9PXx8gbVt/Z8W/p4168t7sf732K0tIExsjU" +
                "FAlD2TwY2DO9pExvptPOdZmlyKp1NJGoAokvn3/wFvdA4oGGoKYxITvBFz56i0qYaJ2R75txppMbVPiD" +
                "tvvTisUCbVZmJzKI7gbRFqlYxmQQbZjgJvQNg95QDhZ9c7LRle9W62QRRVtXOfCF1gqB91VpC5moqcLu" +
                "XFiyyCCfqdtpQdtyFlec1bpDgy1Co+7cqxdgwGwq+Ns+jcWtvFxMEBgUj884IlUiJRKdil3EaY+gSKJO" +
                "MuYK37yNRA3s8mrUO0LhF3NBiHWuHa2k81AfKC14FEwhiBREdeOZhy2/umYgV0TIe4+d36D27wWRr/nY" +
                "SKtLk8hQNX4L0mMJR9JUAiCTCw/Lp5r/TRTCgOCp78B0wqTciSIVTnhX5mqGuO9mqLYMQmJRwEf/lnNt" +
                "YwiOuJzxmclcGt8kPhBM5HqxAIczT1WFekcekmBjQYUwaC6wrcF5bVKV83Ff46wdH4tsyxzc3O8eMX1b" +
                "mZROAdAKGnhY+LsBSN4PQDArBKKt0VLv4lHOUBqNcdSLwBSyJL+CQC3jFPYINn4PzsXQjeBIWEktbfu9" +
                "MR7tDsEIIMhCJ3PaBvLrlZuDabgAbwVyNQHjQTHTBLQ+Z6HnO3c0M+wjDJpc1+qDxrWN/6I2b/SyT7tz" +
                "5Cxj7205QwBxsDD6VqGEaLLySpJMyRzTSk2MMKvIk6I3GW298Tzi2chnBN/CWp0onit+NtY1G2hNpU9V" +
                "jffHEhw8Bv9xkgC/ugZyg4NJEKWpwR3VD+wWVxlvp9V75c/y1MZ1s5aNKfLk1RyI/i65xXKvd33uVzkI" +
                "KHXnoBacUBgknK0GP3xBa3jIG+5G00wL9/IFfW1Wq2b17dfAX4eu9qFJVLiCrOO5CZ6fvqzjznMtjn7i" +
                "Ub1a/hrfqlvNQ47RrX+36RIKCzPdU4qfAXwncMx9jSQEU4XLWijDEdjU3y4w4h2lWvrpBB0LcQOVEv3N" +
                "0qIooAwky/OfL6JMCTz9MT3jWdyi5Vzm4ZS/ozAKz78qIaNmfI+ubw6NsKDKuRa56QH6m++FjDkYQ/lB" +
                "idEhcTsx31RWuqQlO4SFqWhf8xCucXl6clq3/IgPKh6o9eZ/NtS5w8D5adafJtX3R38wiZFhmtWsWU2a" +
                "lYiifwGxBaBwmQ8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
