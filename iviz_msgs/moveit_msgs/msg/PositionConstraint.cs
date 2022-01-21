/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PositionConstraint : IDeserializable<PositionConstraint>, IMessage
    {
        // This message contains the definition of a position constraint.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The robot link this constraint refers to
        [DataMember (Name = "link_name")] public string LinkName;
        // The offset (in the link frame) for the target point on the link we are planning for
        [DataMember (Name = "target_point_offset")] public GeometryMsgs.Vector3 TargetPointOffset;
        // The volume this constraint refers to 
        [DataMember (Name = "constraint_region")] public BoundingVolume ConstraintRegion;
        // A weighting factor for this constraint (denotes relative importance to other constraints. Closer to zero means less important)
        [DataMember (Name = "weight")] public double Weight;
    
        /// Constructor for empty message.
        public PositionConstraint()
        {
            LinkName = string.Empty;
            ConstraintRegion = new BoundingVolume();
        }
        
        /// Explicit constructor.
        public PositionConstraint(in StdMsgs.Header Header, string LinkName, in GeometryMsgs.Vector3 TargetPointOffset, BoundingVolume ConstraintRegion, double Weight)
        {
            this.Header = Header;
            this.LinkName = LinkName;
            this.TargetPointOffset = TargetPointOffset;
            this.ConstraintRegion = ConstraintRegion;
            this.Weight = Weight;
        }
        
        /// Constructor with buffer.
        public PositionConstraint(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            LinkName = b.DeserializeString();
            b.Deserialize(out TargetPointOffset);
            ConstraintRegion = new BoundingVolume(ref b);
            Weight = b.Deserialize<double>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PositionConstraint(ref b);
        
        public PositionConstraint RosDeserialize(ref ReadBuffer b) => new PositionConstraint(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(LinkName);
            b.Serialize(in TargetPointOffset);
            ConstraintRegion.RosSerialize(ref b);
            b.Serialize(Weight);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
            if (ConstraintRegion is null) throw new System.NullReferenceException(nameof(ConstraintRegion));
            ConstraintRegion.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 36;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(LinkName);
                size += ConstraintRegion.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PositionConstraint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c83edf208d87d3aa3169f47775a58e6a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71X224bNxB9368g4AdL7UZN46AoXPghiZ1YQJO4thvkgkCgdke7RFbkhuTKVr6+Z8i9" +
                "ybGbPtRODEirnTkzc+ZG7onLUjmxJudkQSIz2kulnfAliZxWSiuvjBZmJaSojYtPkHLeQs7PkuSUZE5W" +
                "lOEjSfYASMKapfGiUvoLkIA/aAhLK7IwYBL8onQRpBZarqlTNquVIy8mSgc3AsrKQmAqVsaG37y0BURq" +
                "w4hmJHdFQloSdSW1ZnAoJAWZNXm7Xaxd4X55R5k39qCFWASIRTTZObAxVbOmuz0XyXPT6Bz476LkILSw" +
                "VIAiRnoGZ1RR+uCGZKOt+7uok5y08eQAX0mvNiTUujbWS50R2zKIzY4U3Ey8qIzDb3j5jaxB8iQyViGF" +
                "vaqfJqvKSP/b09aJJDn6n/8lry9eHQrn80hrLAOEfQHzubQ53PIyl16GqEs4QfZRRRuqoCTXNeUivPXb" +
                "mtwsMA9i8FeQJiuraisaByFEmZn1utEqkx6MKNTqWB+aKBRUp7ReZU0lmSxjkR0WD3XD6Phz9LUhZnV+" +
                "fBgIpaxhwmFJ6cySdJyq+bFIGvB88IQVkr3LK/MIj1Qw451xZFF6dpauawvi4Yx0h7DxUwxuBmyQQ7CS" +
                "OzEJvy3w6KYCRuAC1SYrQ4mfbX3ZVvBGWiWXFTFwBgaAus9K+9MRsg7QWmrTwUfEwcZ/gdU9Lsf0qETO" +
                "Ko7eNQUIhGBtzUblEF1uA0hWKdLc0ksr7TZhrWgy2XvJHMe6DhnBp3TOZAoJyMWV8mXX6iEbC5XfVzXe" +
                "2uldaVniVCEIuCc24R1XzsoSIqllRjMuknlIq9EoCu4rz/XXa0IxVxaqaPEZjwqMBGMpFcqL3KCH0cnA" +
                "WMsvgCRwzNqyrgGGQrdoU25xTothlQnNilkqrkrSUYo5ChUdekBlwqpC5VEThta9shRtcKnwqyfguKqi" +
                "z9EYEgYQa3xQmM7EfCW2phFXHBC+2Lb1jFhS71coEW9Myn3XQuwSehbGbbcrsCY8mh4roBs11/23bf/t" +
                "2z2lem02pHz0a3caw+1j3lzEaY7zGWk+OI4zmTcLtpkzFYhtwwPRtVVrxcPAxdYOKWzq2D/tOjBxI5KY" +
                "SC4hTCRwm7hS1hT9uGDQsw7p0+cRardasENhAgauSoUBwPAj07y6KsNzLk9uUu92ABcBiFHnqJ08V11l" +
                "DGgpZ6ok7nnNeXY1ZWql4kiFCn+w/WXLXhfnRBbYMyl6+JYQXwMSfkTkfwmqNf3DgFiui+V+CuXO/Ax1" +
                "sjTXKejhdk5FtsUgxCpLeUUQN+MzNFdAifHEIgj0lXITOkXZgUWAMeeYFoDDWrD8QjxO8R+twpvld/H8" +
                "7fujX9vvF2enJ+cnR0/axxcf/py/OT45Pzrofnj75uToaUc1b0uuX+Y4+NRK8e9JJ5RjOGuH1Lld0bYn" +
                "Qfsg0enwsGP3xwojsUNBEqkNxxAeIpGEeE5kuq5Fe1rbH3T2EbyVW7bwsj23IfDgahqe3qfiA+oM9Hwc" +
                "+8wkhwMd6cKXnUeZsRjCtQks8zbHdurjA+mzgdvF+6PHo6cPPdf89BFUj12K/LdehRnKaceRA6Z45GMb" +
                "8Dkk+omhXbRDAodcmauGXeAtypyFCprt5HVx/ux4/vcF/Bnb7JIcMDnB8RQUWYmlw8fZsLGN5cXLlVSZ" +
                "EDjLfBTyWuEkOOygHdzF6cn81emlmDB2+zAdYgIIeBsxPsRUhtNiz3nbC2LCvTCN9qDd24nRtXbiw8jO" +
                "XVZ4N3XcxfRJR3fbfMEJYaa6V9C/MTRHPYlJlykbDoKz2DKqHmoocMr6vMq43ps6jcyKn1tSkxud2PLX" +
                "l9SN4FFco079TngghgXvZ8R9P1nDquuPLbK7w/Gg3T3zpDFbfICN7+MiYbZj4cl41kni7u8Ekr8azHSr" +
                "A+4g91ABqlDDl+1Narix9v6391V2eSfcHxxVHsT9gbrbTqY7fO46z09fB975QPjDw1d/C3yA9cpng26p" +
                "jtLAK56HX6VcHCy4jegC19U/RttjI6sm3IFX8WLbZdJx0Bviqx25T58TtnHZAmCP9VhJOxhxz25k1Wt8" +
                "f5gL3txSTwDrlB6Iqi6MWyjrwtp3g1PxTvrpIPpJ1wsQF739B1NWj7bEEQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
