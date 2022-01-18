/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AttachedCollisionObject : IDeserializable<AttachedCollisionObject>, IMessage
    {
        // The CollisionObject will be attached with a fixed joint to this link
        [DataMember (Name = "link_name")] public string LinkName;
        //This contains the actual shapes and poses for the CollisionObject
        //to be attached to the link
        //If action is remove and no object.id is set, all objects
        //attached to the link indicated by link_name will be removed
        [DataMember (Name = "object")] public CollisionObject Object;
        // The set of links that the attached objects are allowed to touch
        // by default - the link_name is already considered by default
        [DataMember (Name = "touch_links")] public string[] TouchLinks;
        // If certain links were placed in a particular posture for this object to remain attached 
        // (e.g., an end effector closing around an object), the posture necessary for releasing
        // the object is stored here
        [DataMember (Name = "detach_posture")] public TrajectoryMsgs.JointTrajectory DetachPosture;
        // The weight of the attached object, if known
        [DataMember (Name = "weight")] public double Weight;
    
        /// Constructor for empty message.
        public AttachedCollisionObject()
        {
            LinkName = string.Empty;
            Object = new CollisionObject();
            TouchLinks = System.Array.Empty<string>();
            DetachPosture = new TrajectoryMsgs.JointTrajectory();
        }
        
        /// Explicit constructor.
        public AttachedCollisionObject(string LinkName, CollisionObject Object, string[] TouchLinks, TrajectoryMsgs.JointTrajectory DetachPosture, double Weight)
        {
            this.LinkName = LinkName;
            this.Object = Object;
            this.TouchLinks = TouchLinks;
            this.DetachPosture = DetachPosture;
            this.Weight = Weight;
        }
        
        /// Constructor with buffer.
        public AttachedCollisionObject(ref ReadBuffer b)
        {
            LinkName = b.DeserializeString();
            Object = new CollisionObject(ref b);
            TouchLinks = b.DeserializeStringArray();
            DetachPosture = new TrajectoryMsgs.JointTrajectory(ref b);
            Weight = b.Deserialize<double>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new AttachedCollisionObject(ref b);
        
        public AttachedCollisionObject RosDeserialize(ref ReadBuffer b) => new AttachedCollisionObject(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(LinkName);
            Object.RosSerialize(ref b);
            b.SerializeArray(TouchLinks);
            DetachPosture.RosSerialize(ref b);
            b.Serialize(Weight);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
            if (Object is null) throw new System.NullReferenceException(nameof(Object));
            Object.RosValidate();
            if (TouchLinks is null) throw new System.NullReferenceException(nameof(TouchLinks));
            for (int i = 0; i < TouchLinks.Length; i++)
            {
                if (TouchLinks[i] is null) throw new System.NullReferenceException($"{nameof(TouchLinks)}[{i}]");
            }
            if (DetachPosture is null) throw new System.NullReferenceException(nameof(DetachPosture));
            DetachPosture.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.GetStringSize(LinkName);
                size += Object.RosMessageLength;
                size += BuiltIns.GetArraySize(TouchLinks);
                size += DetachPosture.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/AttachedCollisionObject";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3ceac60b21e85bbd6c5b0ab9d476b752";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZbVMbORL+Pr9CtXwwbBwnG7Jbe1zxgQR2QyoJbOCuklCUS56RbYXxyBlpAO/V/fd7" +
                "uluaGRtyua06QqhgSa1+70cteUudz4166crSeuuqk8lnkwd1Y8tSTYzSIeh8bgpMhLnSampvMfjsbBVU" +
                "cCrMrVelra4yH2pbzfjzuNILk2Vb57SYuypoW3mQglseGl0qP9dL45WuCrV0Hp+mrub1DS2yLYjoK8ES" +
                "jQjcOp4SPxAriKnNwl0bZlk55Xj7yBa05E0YKg1zZNZnW/fxU7YqbK4DZierzozWESKgyDYdJUxhLbsR" +
                "spSb8nayWAcxO8mLGihdG9LI3UQdXJPPwQGCCzPVTRnU41YxUQN26LI2uliRR70tTC2axg3R/xeXwmzM" +
                "GpBW8FJuagpB1OoGO9Wy1Dn2Y1Krpa6DzZtS1xSN0GBZ4gGZojDpCPuJR2sKWG+b0WwE11bKwO1mOgUp" +
                "Nual85QKunYN5rEsXHaGbFOSUZnceK/rFUurTWk0bQNfooqCKXzgCXlzqJ2FWn9mIavxws/8k9eUh+ft" +
                "JJxB2o2jiBSUG2Nnc47LPcEYKjtVV5W7qbJp6XT45Xmkz7L9//NP9vbs9z1FaWSD6H8n39UBDNUI7lA1" +
                "HjqSa2CiqZe1CeTU6EHjs1dMF8mTqcj4aGZ04DZnD/NC9N5C9nHYScVqi7QvxXm1NJIUhQ56or0hduyd" +
                "tnrk77g2uZtVlupPbBETzokDsUmc82Simhm3MBBMle+9yy0XG+NKp/BIttk6IgNVCpPUxi9jKhI1DezU" +
                "cmIkB5y5EvYnMbla1nYBBa/hLEYc0ZOpTtMSCqZHFvfG7DqFBv31sTgekt4aP1/nSjOgXcjCvXxorWPx" +
                "goqDgoBSrGDotvnS6IRmrXVDNWkEQ5hM5agmgJGDFyvyXiOVhogVBccC6Eoidvq6ndJWMoQlfcVIWuu0" +
                "OygK30+j6HWiqkiiz01lRgQuPSLk6LV1jS9XytxaH0h9GwSdBXBG2WQVjDo4PNx/SmLeM6iuSZrWbsFj" +
                "U13b2lULGIrPwQIhVvDStalXgCYuBcDrQgcUs9/ICVvsiKT3R29P/nm0/xPbtFwSTsGUFpKERwJWVtpT" +
                "/n/b1sJBaOWCbEp2IgqdkaenR+8O959FEO5k3i+OpQyBijcx82OoPdm/TRQpbqiJWq+8WjQ+EEVpplBj" +
                "sQyrHYgCmnlXkq/g2oQYHZoWxsOThajIvtklBU+WppbskxMXQ0DPIhG6tPxQoOhDIekoqEbFHHCW67pA" +
                "2QRNaMRYOAcym/pxiTRAGxH0Yolo8yphjhf0gJn4ncGfNQ7ZlaAf7MrdYtFUfMirYFGO/f3kuo3TMHeu" +
                "RokS+bQGjBJ3/HpUqqlyo44P9/gwNnlD8ABJtsprOcawqLIG0L37jDagGbpxjwnJZ8DsVrjkH5Q1tyge" +
                "T3pqvwcZP4pxI/CGcwC1Fepxm+fGGPodBSFQwSxdPlfb0PwUVeEkla51bfWk5KYhhwfAdUCbBjs9zhWz" +
                "rnTlEnvh2Mn4X9hWLV+y6fEcMSu5ZpoZHAjCZY0sLKRVISZ5SciFVmRS4+jPaJeIzLZ+Ix9L28ERsXfO" +
                "iXRucTTGdHo9TDZ++4jLtv7yjzp58fro5Tklxl/fHH+oVF+mhtpWVKJStXri4jnBRzgObaoXar/isYqy" +
                "QKBVuHHUo3qiQKbDjegtgJ5uZrC5VigPJHfJbaWtqJ+GxPUW98qsUo/Rl7DHM7If+EuIDSiuE7zNUCCV" +
                "KiYphOCSGBaTviqx3dMIvnp9dvLuCao21cbHg7dvlDAYqYMWUHHot3C80FeMmV7ahuSV3NXUQDg+ciE3" +
                "tTcjdcQ9rK3uCTqjOsFw6dwVUvbK7Kkf/jUgDw/2Bi+pzz58MRiqQe1cwMw8hOXekyelQ3XA22Hw7x/E" +
                "xJprpiIEqq7JM3xOS/Rir03B6XmB7jE2DLDJoiaBXVfGRLSYljg4Jra0YTVa697W8hUGG3Ei4yC6ocMX" +
                "khvMhKyiUyhKRqTKwlNyNfATHbi3KMnSMBb9BgWjsTRUzGZPtQ7gOXIB5jZdsPfz3359LhTUCBq+r4Hu" +
                "rsaDKOnsjzcKYfNm7sqijdOa4LMv5atEIbxZlBrczPzuLzKzdDVmfn6++4yHoK6JwNKlK1IAXG4A8BvT" +
                "1C+TIUnAOPa9srpwRVPSeiAkDG45SAmN1H6os/FrvSs0OpQynbjbIdofyrShyleAYL5CIN0MnVgHZXfn" +
                "RlpIbfORONfXlBHUb09SQwpm1H5Qg8mVKNe4p0P8G2V8pv2qXpx8QFMln89OXx29P0KjI8OXH98cvzs8" +
                "eo/GIk6cvDvaf56qPeET9zykU6SSO0OCBBwLFV0a/DppvKGhX+0o0p6F0dyu9Tf0yPaUwbWPz2t0FiE6" +
                "QRpHctdtQqpBt2cgrVYWU5NWYTirKnfZD0P1ccjF+qmvMzmZr++mmuHqEjXaxCC6xLf2wemjzrfjD+iP" +
                "u9HH1tc0+kQ9ZU8l8X/UylUAbwo7wSb+VtxeUAc0jKDCUCx217qwDakQL92SQaO1uI7fHxwe/+OM+vWe" +
                "zBRk5kkBlv5LvCKpQ88NjHvtZUWXrr3ff1Ia7e9ICTxOXfRY4jt+dXT8+6tztU2842Cns4kOpWnf451N" +
                "87XLfqoFtU21sCPyCOeSHLEuypFBT87XpIBD6zsJX7wq3y/zpavkaSotYX9369ysSbTfua25BR1Jydhl" +
                "l0PsU9pPTx+U781yKJ5Vj6JTs41KjP5rU2rDeLoddZV6h7hzDBE+DMTdvZLyUwiujagYxEGaHOpZ6LRC" +
                "oU5rQ+mKO+VQokXtgaxbaYioMeHE470jlZ3ym2UiyP7AbdvUFfPt6L6XgZZz+O4Daas/d3Ac43Vz20eq" +
                "2/bTqv305/dRv3NdsqENlOd7bufPdeVp9KXzOzVASNb/blH7KPcdjld6xEmHai8M9HBD4Ffivs6FiHtQ" +
                "NUN39Pfe6XGty8aQJ6Z0wXNtJPmCf23oUmn8xWVGMs4jA3qtTbyyCIzxiTztSK8c6GqbJROwNvfkE5il" +
                "Td/JVcmMe1yWzEIH2yolt+GLXdHT3I75xf27aMsvYPzetAkoWl6+hvElrXtpa5/j9K16RLfXRyr/E/8V" +
                "al/x25VWe/soXjO9eHpJb/ft8Cca5u3wGQ2Ldrh72Wb8xfNLnnsoB3zjtXzjBbn9AoG/3OFvHXy2sSUl" +
                "GgPTgwXuG3on9Dyifq6jjWiZHgG9MpavtG0hXgyReLiZYGRooPPclPFdy19SlNw6tZkCocJl++1UT5Yc" +
                "0+aWnn7oxS/22O0DZEQDOtnT+x49buC6AY20YL18icZ+JqINK0cwPes1vK1ivbnOnN7kmlm9ebEmK5r0" +
                "0Ie+ZkyvrWM0xFjI/gPIdcfFCRwAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
