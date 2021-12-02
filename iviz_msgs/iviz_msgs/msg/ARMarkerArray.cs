/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ARMarkerArray : IDeserializable<ARMarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public ARMarker[] Markers;
    
        /// Constructor for empty message.
        public ARMarkerArray()
        {
            Markers = System.Array.Empty<ARMarker>();
        }
        
        /// Explicit constructor.
        public ARMarkerArray(ARMarker[] Markers)
        {
            this.Markers = Markers;
        }
        
        /// Constructor with buffer.
        internal ARMarkerArray(ref Buffer b)
        {
            Markers = b.DeserializeArray<ARMarker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new ARMarker(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ARMarkerArray(ref b);
        
        ARMarkerArray IDeserializable<ARMarkerArray>.RosDeserialize(ref Buffer b) => new ARMarkerArray(ref b);
    
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
        [Preserve] public const string RosMessageType = "iviz_msgs/ARMarkerArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "38745a121d365c2cc5cc1b47928542b2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/bRhA9e3/FAD7ELmS1TYICNZCDEaetD0Ecxy1QGAExIkfkNuQus7uSTP/6vllK" +
                "VBQYbQ+NBQLix7x5M2/e7MXNWw6fJNx9pC7fRGNe/c8/8/bDr+dk1/ah6GIdv7/YcprFkIRu/7x+U1zc" +
                "/P76Hb2iH7549/7m9bvLN3j5ozG/CVcSqMl/Y0waejExBetqKn0lphbfSQrDSPKHlMmHF3cvP+JrcGjs" +
                "6OiYensvLfU+2mS9o41NDT1k3mXrOf308u5nxHMngQvrNHm05ZEig99Aob98oOn9V4zXPsoOCwYxZuF9" +
                "Sw3HQu63kPHDlmureBHtg4Ct6DowPZZUQUWQlpNdS5F8MbIcfatRxVSN7KPs5pg+JHYVh4pQGlecmJZQ" +
                "orF1I+GslTVEjYm7XirKX3U4cQ7gbWMj4aoFI+C2HWgVEZQ8ptJ1K2dL1lnaTg7wQFpHTD2HZMtVywHx" +
                "PlTWafgyoH/NjivK55W4Uujq8hwxLkq5UpnAZF0ZhKMa5OqSzAqDe/FcAeb4duPP8Cg1TDWRU2o4abFy" +
                "3weJWifHc3B8NzY3R26II2CpIp3kdwUe4ymBBCVI78uGTlD59ZAa+Cs1QmsOlhetaOISCiDrMwU9O/0i" +
                "s5Z9To6d36UfM+45/ktaN+XVns4azKzV7uOqhoAI7INf2wqhiyEnKVsrLlFrF4HDYBQ1UprjX1RjBAGV" +
                "J4J/jtGXFgOo8uLsli9Po7DVt3Ljo3u9s1YQHRWaQHm0zt/UOcsg6KTnUuZqkqs8Vu9gik4YHcN/ExLA" +
                "ygZAcSLMkVWCwNwyI5uo8hLJ+YQcHX9CSoHGiua+RzIYPbCLupg6Fq+QE5nX8xltGnFjlGqUHZ13wJYU" +
                "bG2rEQmibgIzbZubUVo+h8ZtO9Y8kmFgSBJ8yoDTOV0tafAr2mhDuAnb1fO0kKmubJHk/Uz3bpvi6xMG" +
                "iwBZYuQabnIxYennZjqk7qe7Ybp7eJJR69mHci/2oxql8st8Ih7OeaYHir6utt/HEx4rQD7YHRZuGPvd" +
                "BZj3Kxg6uJx3H/c0Xs6l7JyMtU8M+fNiTvWjF5yCueSDds0/j+dJyt9L99g2Huh5WLw+fd7rrkvwr4bb" +
                "3W2M+Ruhwt7RtAgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
