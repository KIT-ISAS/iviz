/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class XRMarkerArray : IDeserializable<XRMarkerArray>, IMessage
    {
        [DataMember (Name = "markers")] public XRMarker[] Markers;
    
        public XRMarkerArray()
        {
            Markers = System.Array.Empty<XRMarker>();
        }
        
        public XRMarkerArray(XRMarker[] Markers)
        {
            this.Markers = Markers;
        }
        
        public XRMarkerArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Markers = n == 0
                    ? System.Array.Empty<XRMarker>()
                    : new XRMarker[n];
                for (int i = 0; i < n; i++)
                {
                    Markers[i] = new XRMarker(ref b);
                }
            }
        }
        
        public XRMarkerArray(ref ReadBuffer2 b)
        {
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Markers = n == 0
                    ? System.Array.Empty<XRMarker>()
                    : new XRMarker[n];
                for (int i = 0; i < n; i++)
                {
                    Markers[i] = new XRMarker(ref b);
                }
            }
        }
        
        public XRMarkerArray RosDeserialize(ref ReadBuffer b) => new XRMarkerArray(ref b);
        
        public XRMarkerArray RosDeserialize(ref ReadBuffer2 b) => new XRMarkerArray(ref b);
    
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
    
        public const string MessageType = "iviz_msgs/XRMarkerArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bf6cdc9e7f0d5ddddaedaa788a1fcb50";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71V32/bNhB+nv6KA/zQZHC0tSkKLEAfgqTb8lA0cbOiXRAItHSWuEqkSlL+kb++35GW" +
                "nRTBuoc1hmFTJO+7u+++O32cvVXuM7ubW+riwmfZ6//5k719/8cJ6aW+Kzpf+18+bn1m801guv50+aY4" +
                "nf119o5e06/39q5mZ+/O32DzeZb9yapiR038S3fCpufMB6dNTaWt+Cf5TOia1yE+k17Q1WxK2gSuYas8" +
                "bW/j4NQNpc2zmm3HwW1SXB+4DNYd37y8BYAz4EIAz+KSer3mlnrrddDW+CmtdGjoTmLOs0VrVXj18uY3" +
                "WKqOnSrgFb68LmNQZ3GTdpvgGqv1lJxdYf2Pdd/Gcmk9j1hwyhEmbtoFhWY8AyQ5bpXERMHGk4XDkRzI" +
                "Q2Isp08IHe49VXZlpvQ3LaxbKVflWTa3tqVG+QJAWs1bHh3C4wWcuYGnEcsItWFlaaG5rTwNiEbJrzuq" +
                "9ZINeX3HudgsVOuT0YZW7JjYB42cucJ1KcFxRZUKasfcVnyFIIC7ouui+6STiCsJdd2jNEm4RWJhyUWw" +
                "ReJmJGw8Gfm5x2rK5F/Z+THN4EOVEkjCzib0PihTwSUhOyXcSAzU6LoBvS0vIT4fVNdzYi7K3+cwvG60" +
                "J3xrhkxV226kIpUkW9quG4wulXSL7viBPSzBqKJeuaDLoVUO962rtJHrUUOCjq/nLwObkuni/AR3jOdy" +
                "ED7hSZvSsYoVvTinbACHxy/EgCZ0M7P++W02uV7Zo7EHd1GgECpI1LzuHXsJWPkTOPs5ZZnDCVhiuIPQ" +
                "DuJegUd/CDFILNzbsqEDpHC5CY1NYl8qFwUswCWoAOozMXp2eA/ZRGijjB3hE+Lex3+BNTtcyemoQfFa" +
                "ocEPNZjExd7Zpa5wdb5Jqms1m0CtnjvlNplYJZfZ5PfYsEHqGEuDf+W9LXVsGJkz45yLZSl09aNk+eg8" +
                "HDXmWErF0iaKlvFMJLRwjEx6VaL1cfUiltUaqKNjhYwhxJ0lDCvtYIpxlQMVowEqx6TQAa3HnowNwOjU" +
                "Z0AyOBZr1fcAg+KdMn4/6mBywHmdYxI3mD3xlnAUpR2bAXPW6VpXyRKOup2xom1yGFKLF+C4bVPMyRkK" +
                "BhBnQzQ4jDNtYwdaSUJYuG0PWprzLq4okWDtNA7GBPHttEIjgBbvVS0DzQd0P2bMOATXu9Vmt7p7klLL" +
                "oES4p/tSJarwsolz8kGdpzJZZLvansdXIqEFyDo92kINKd/xQnY1QNDORNz9vafRcgxlVDLaPijQHxtz" +
                "Fz9yUek98DDd75TnScLfU/dYNz7g82Hw8vRlz7s0wXcFN65WWfYVyg8zVR8KAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}