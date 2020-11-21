/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract (Name = "visualization_msgs/MarkerArray")]
    public sealed class MarkerArray : IDeserializable<MarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public Marker[] Markers { get; set; }
    
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
                "H4sIAAAAAAAACr1XWW8TSRB+xpL/Q0sRIlk54xwsy0byg4lNYinX2gYWIWS1Z9qeJjPTQ3dPjPn1+1X3" +
                "HDZJEA8bQmD6qrvqq+KS61uhP31mqVuYdqvd6v3PP+3W5eTshN1JU/BEfudWqmyWmqXpXjqh7dYOmwjB" +
                "Ymvzk253tVoFWplA6WV3JW9lV9/J792BNHnC19N1Lio6xrPo50TTwioteVJRmOfH/edHB2+4kSG+k5iD" +
                "G1sozVKlBZMZlqnTj+G3MDJbMhtLw1JhDF8KtpI2ZsSa/FTIzL5m/fH4+kPvoNqevnsz7B1Wu8nN+XA8" +
                "7B3Vtx8vRleD4bh3XJ1gP5xNpuPRTe/l1tnFaDLt/bnJ1h+92ubtD/+qDm+uR1fTSe91tZ8O/53O3o+G" +
                "H2Zv+6ejq7Pe39XN5XByPhsPJ9fvxqfQuDYAqvSvzi5KxoeHG5YOBo2dl9eD0duPzX4wvBhONyz1+/7F" +
                "BZnabp0LHiFisf888rNT3VNErExFd6F5uhWXdstYTWHJzGNcPKcrEJqch4JZxZA5tKBIqvkXEVqwDIIA" +
                "ERYRlixU2ZciC13gXYhlRGShFtwKxlmRya+FYBkp43SLRcmo3YK1x0dE8OzZT9QpxY4GJHNRJA9KJbZZ" +
                "rbdLS57JvEhgOkymdI9EItymUYHhmorIK2JRII+rssOogJha/KA+91o8TnbAeBR1UxXJxRqKlOQddsh2" +
                "I5FrEUKHaK/DjryGqKqNR8fNYZKUp0CapVCpsHrtseBGGcFy+ue+dHcHpTf9vk39HmdKHzMT8mSbA7DF" +
                "nW2Rs8MO/qCsOfIoEgteJJbtFgRQyRpGgTPS0HwtuBZ7lHORF3OqEqXHZ2/6CB5WPwhyt+zTQXCwfxgc" +
                "fG63okJ7NEnkAmFD9jzo3HO1YonaDqqJVZFEiK2xbC6QCwIflwXANKqF0KnqHRsFDBHy1tDTO8qHuVIJ" +
                "cwU0S1R4i0x/UPhoUUKcx9RS7lx40n1P2mEyEAHTcDhkUDW6wkGNSAuRrkpJ6tqVrbEip6Lfuc6goi8y" +
                "732XnSYXoVxInMbcMIMw0puqsFK2K5ZBCWSdDXzsMJTs3v20gRroXzl9zVPJ3MmKdA7vIIlc4OGuAoER" +
                "qFqcwlkHDJwE8iUh4HCFXFPUql1dT4cnqIE85gwez5Rla2Gdsg/lGKzywpwvmadWtXlOd/HNNq27hEY6" +
                "fJxkC/fv0SJ48UwLowodijKH3BnoZwIWRZGAnoQ41FefcFyo/eFbhxsSLCCQ64jKk0fccmdQLJcIwn6C" +
                "/EtAxdMclrpbirwJiHJKGY7fpciEdoXj/EEYr9IU8E74VebuBgMiBVBzlnONigMQaxAoHcmM3ru8d/zp" +
                "r0H4RQbgHg1OCNuNCAsrodQaPKiVuHECHcC3SKAuKEA4Xal97MUS2VJrgBTi6FKGiW9AV0PKcnNCYv7w" +
                "NgZgDycJCIoM23VnM2zNHoMcaCFyFcZsF+rfrG0MDKKkvOMI2xxoCM4EIGD7gohe7G2yJtVP0IkyVfH3" +
                "LBshv8I3axiTWfsxgpeQC0yxhB/xMtfqTiKf2HztuISJFBn6mZxrrtftlkNMJxRM3jqQcVDlYoMvN0aF" +
                "kjqP6591EnvQk9ETZuf93kV29oGQFC5YUU6RVP3AGXhroTHius7eoaSj46i8l+4ttXdMqxVtgDxx4Fa/" +
                "aLf+KajwMse5efkbzYQ6dTkhMyyX6DgUutoKWIRycXpvGd1uLRLF7auX7FuzRIyr5fffZkXjxNqUOmp+" +
                "bGlcu20D7b42IaA2iOr/Bcuq5eq3GVnOQw9ayO7c5bZtlG4YBhzmuIZB04QlhKxJQRlJjHo+O6cAXTeX" +
                "YDawLFLCNTRikvJbMBWofyLneQ5uAGOaHGiSJciguQE9N1gGHbaKReZfufHG6eFwWoZMyyWN4tXQUVNz" +
                "VhrYYXZxhOqnuZK09tKQkcRFKx/FvYCmnLUq2IpswkKXDUJR7640cwBmleq40aDk8UAF1P8bRPJbNKdf" +
                "y4EnCvv9saGUiv6Cplktl81y3iw5af4ft7ps0QMQAAA=";
                
    }
}
