/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
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
        public ARMarkerArray(ref ReadBuffer b)
        {
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new ARMarker(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ARMarkerArray(ref b);
        
        public ARMarkerArray RosDeserialize(ref ReadBuffer b) => new ARMarkerArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Markers);
        }
        
        public void RosValidate()
        {
            if (Markers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) BuiltIns.ThrowNullReference(nameof(Markers), i);
                Markers[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Markers);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/ARMarkerArray";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "bf6cdc9e7f0d5ddddaedaa788a1fcb50";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VUW/bNhB+nn7FAXloMjja1hQDFqAPRtJteSiapNmALigEWjpLXCVSJSnbyq/fd6Rl" +
                "x0Ww7mGNYVgUyfvu7rvvzvPbt8p9Ynf/kbq48Fn2+n/+ZG/f/3ZOeqUfis7X/of51me2GAPT3YfrN8X8" +
                "9o+Ld/Safny0d3N78e7yDTZ/yrLfWVXsqImPdCeMPWc+OG1qKm3F38nniO54E+I76SXd3M5Im8A1bJWn" +
                "7W0czN1Q2jyr2XYc3Jji+pPLYN3Z/auPAHAGXAjgRVxSrzfcUm+9DtoaP6O1Dg09SMx5tmytCj+/uv8F" +
                "lqpjpwp4hS+vyxjURdyk3Sa4xmozI2fXWP9t3ZexXFvPExaccoSJm3ZJoZnOAEmOWyUxUbDxZOlwJAfy" +
                "khjL6QNCh3tPlV2bGf1FS+vWylV5li2sbalRvgCQVouWJ4fweAVnbuBZxDJCbVhbWmpuK08DolHy605r" +
                "vWJDXj9wLjZL1fpkNNKaHRP7oJEzV7guJTirqFJB7Zjbiq8QBHBXdF10n3QScSWhrnuSJgm3SCysuAi2" +
                "SNxMhE0nEz+PWE2Z/Cs736YZfKhSAknY2RG9D8pUcEnITgk3EgM1um5Ab8sriM8H1fWcmIvy9zkM7xrt" +
                "Cd+aIVPVtqNUpJJkS9t1g9Glkm7RHR/YwxKMKuqVC7ocWuVw37pKG7keNSTo+Hr+PLApma4uz3HHeC4H" +
                "4ROetCkdq1jRq0vKBnB49lIMsqO7tT2dWm/nHPyrIMHypnfsJU7lz+Hj+5RcDmyQw/ACfR3HvQKv/gQa" +
                "kBC4t2VDx4j8egyNTRpfKRd1K8AlGADqCzF6cfII2URoo4yd4BPi3sd/gTU7XMnptEHNWsneDzUIxMXe" +
                "2ZWucHUxJrG1mk2gVi+ccmMmVslldvRr7NMg5YsVwVN5b0sd+0TGyzTeYjUKXX0rNT45BidpOZZSsXSH" +
                "olU8E+UsHSOTXpXoeFy9imW1BqLoWCFj6G9nCcNKO5hiSuVAxUSAuDEgdEDHsSdjAzA69QmQDI7FWvU9" +
                "wCB0p4zfTziYHHNe5xjADUZOvCUcRUXHHsB4dbrWVbKEo25nrGibHGbT8iU4btsUc3KGggHE2RANTuIo" +
                "G+1Aa0kIC7dtPUsL3sUVJRKsncV5mCC+HFJoBNDivapljvmApsdomWbfZrcad6uHZym1zEeEO9+XKlGF" +
                "/5g4Hg/qPJOBItvV9jz+ExJagKzTky3UkPKdLmQ3AwTtTMTd33seLcdQJiWj7YMC/bExd/EjF5XG/2G6" +
                "XynPs4S/p+6pbjzg8zB4efu8512a4KuCm1brLPsHPWl1AxYKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
