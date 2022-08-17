/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ARMarkerArray : IDeserializable<ARMarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public ARMarker[] Markers;
    
        public ARMarkerArray()
        {
            Markers = System.Array.Empty<ARMarker>();
        }
        
        public ARMarkerArray(ARMarker[] Markers)
        {
            this.Markers = Markers;
        }
        
        public ARMarkerArray(ref ReadBuffer b)
        {
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new ARMarker(ref b);
            }
        }
        
        public ARMarkerArray(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new ARMarker(ref b);
            }
        }
        
        public ARMarkerArray RosDeserialize(ref ReadBuffer b) => new ARMarkerArray(ref b);
        
        public ARMarkerArray RosDeserialize(ref ReadBuffer2 b) => new ARMarkerArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Markers.Length);
            foreach (var t in Markers)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Markers.Length);
            foreach (var t in Markers)
            {
                t.RosSerialize(ref b);
            }
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
    
        public int RosMessageLength => 4 + WriteBuffer.GetArraySize(Markers);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Markers.Length
            foreach (var t in Markers)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/ARMarkerArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bf6cdc9e7f0d5ddddaedaa788a1fcb50";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71V32/bNhB+nv6KA/zQZHC0tSkGLEAfjKTb8lA0cbMBXRAItHSWuEqkSlL+kb++35GW" +
                "nRTBuoc1hmFTJO+7u+++O83m75T7xO72jrq48Fn25n/+ZO8+/H5GeqXvi87X/qfZzme22Aamm49Xb4vZ" +
                "/M/z9/SGfn6wdz0/f3/xFpsvs+wPVhU7auJfuhO2PWc+OG1qKm3FP8hnQje8CfGZ9JKu51PSJnANW+Vp" +
                "dxsHMzeUNs9qth0Ht01x/cVlsO709vUdAJwBFwJ4HpfU6w231Fuvg7bGT2mtQ0P3EnOeLVurwi+vb3+F" +
                "perYqQJe4cvrMgZ1HjdpvwmusdpMydk11v9Y93UsV9bziAWnHGHipl1SaMYzQJLjVklMFGw8WTocyYE8" +
                "JMZy+ojQ4d5TZddmSn/T0rq1clWeZQtrW2qULwCk1aLl0SE8XsKZG3gasYxQG9aWlprbytOAaJT8upNa" +
                "r9iQ1/eci81StT4ZbWnNjol90MiZK1yXEpxWVKmg9sztxFcIArgrui66TzqJuJJQ1z1Jk4RbJBZWXARb" +
                "JG5GwsaTkZ8HrKZM/pWd79MMPlQpgSTsbEIfgjIVXBKyU8KNxECNrhvQ2/IK4vNBdT0n5qL8fQ7Dm0Z7" +
                "wrdmyFS17VYqUkmype26wehSSbfojh/ZwxKMKuqVC7ocWuVw37pKG7keNSTo+Hr+PLApmS4vznDHeC4H" +
                "4ROetCkdq1jRywvKBnB4+koMaEK3c+tf3mWTm7U9GXtwHwUKoYJEzZvesZeAlT+Dsx9TljmcgCWGOwjt" +
                "KO4VePTHEIPEwr0tGzpCClfb0Ngk9pVyUcACXIIKoL4QoxfHD5BNhDbK2BE+IR58/BdYs8eVnE4aFK8V" +
                "GvxQg0lc7J1d6QpXF9ukulazCdTqhVNum4lVcplNfosNG6SOsTT4V97bUseGkTkzzrlYlkJX30uWT87D" +
                "UWOOpVQsbaJoFc9EQkvHyKRXJVofVy9jWa2BOjpWyBhC3FvCsNIOphhXOVAxGqByTAod0HrsydgAjE59" +
                "AiSDY7FWfQ8wKN4p4w+jDiZHnNc5JnGD2RNvCUdR2rEZMGedrnWVLOGo2xsr2iWHIbV8BY7bNsWcnKFg" +
                "AHE2RIPjONO2dqC1JISF2/WgpQXv44oSCdZO42BMEF9PKzQCaPFe1TLQfED3Y8aMQ3CzX233q/tnKbUM" +
                "SoQ7O5QqUYWXTZyTj+o8lcki29XuPL4SCS1A1unRFmpI+Y4XsusBgnYm4h7uPY+WYyijktH2QYH+2Jj7" +
                "+JGLSu+Bx+l+ozzPEv6Buqe68RGfj4OXp88H3qUJvim4cbXOsi/inY9GHwoAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
