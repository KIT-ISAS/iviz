/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/AttachedCollisionObject")]
    public sealed class AttachedCollisionObject : IDeserializable<AttachedCollisionObject>, IMessage
    {
        // The CollisionObject will be attached with a fixed joint to this link
        [DataMember (Name = "link_name")] public string LinkName { get; set; }
        //This contains the actual shapes and poses for the CollisionObject
        //to be attached to the link
        //If action is remove and no object.id is set, all objects
        //attached to the link indicated by link_name will be removed
        [DataMember (Name = "object")] public CollisionObject Object { get; set; }
        // The set of links that the attached objects are allowed to touch
        // by default - the link_name is already considered by default
        [DataMember (Name = "touch_links")] public string[] TouchLinks { get; set; }
        // If certain links were placed in a particular posture for this object to remain attached 
        // (e.g., an end effector closing around an object), the posture necessary for releasing
        // the object is stored here
        [DataMember (Name = "detach_posture")] public TrajectoryMsgs.JointTrajectory DetachPosture { get; set; }
        // The weight of the attached object, if known
        [DataMember (Name = "weight")] public double Weight { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AttachedCollisionObject()
        {
            LinkName = string.Empty;
            Object = new CollisionObject();
            TouchLinks = System.Array.Empty<string>();
            DetachPosture = new TrajectoryMsgs.JointTrajectory();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AttachedCollisionObject(string LinkName, CollisionObject Object, string[] TouchLinks, TrajectoryMsgs.JointTrajectory DetachPosture, double Weight)
        {
            this.LinkName = LinkName;
            this.Object = Object;
            this.TouchLinks = TouchLinks;
            this.DetachPosture = DetachPosture;
            this.Weight = Weight;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AttachedCollisionObject(ref Buffer b)
        {
            LinkName = b.DeserializeString();
            Object = new CollisionObject(ref b);
            TouchLinks = b.DeserializeStringArray();
            DetachPosture = new TrajectoryMsgs.JointTrajectory(ref b);
            Weight = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AttachedCollisionObject(ref b);
        }
        
        AttachedCollisionObject IDeserializable<AttachedCollisionObject>.RosDeserialize(ref Buffer b)
        {
            return new AttachedCollisionObject(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(LinkName);
            Object.RosSerialize(ref b);
            b.SerializeArray(TouchLinks, 0);
            DetachPosture.RosSerialize(ref b);
            b.Serialize(Weight);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(LinkName);
                size += Object.RosMessageLength;
                size += 4 * TouchLinks.Length;
                foreach (string s in TouchLinks)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += DetachPosture.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/AttachedCollisionObject";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3ceac60b21e85bbd6c5b0ab9d476b752";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZbW8bNxL+voD/A9F8kN0oyovTotVBHxxbbRwksRv7iiSGIVC7lMR4tVRIrm31cP/9" +
                "nhmSuyvFuVyBcxzDEcnhvM/DIfVAnC+UODRlqZ021cn0k8q9uNFlKaZKSO9lvlAFJvxCSDHTtxh8Mrry" +
                "whvhF9qJUldXO5nzVldzHkwquVQ72U724JzWc1N5qSsHajDMfS1L4RZypZyQVSFWxuHTzFhe31IEPCCm" +
                "qwhLVVHog+MZcQS5gCCrluZaMdPKCMMMBrqgJad8X0jYFGYdtt7FUeiq0Ln0mJ2uW1sadwQJxU627S8T" +
                "tQVf9ifkCTNjDmS39MH4JDJqIaRVpJW5iWqYOl8QCwgv1EzWpRePGuWCKjBGllbJYk2OdbpQNmgbN6RI" +
                "XFwGdhPWIWgGb+XKUjCiZjfYLFalzMECk1KspPU6r0tpKS6+xnKIDMQGpUlPuIF4NOYQ7101mA/g40oo" +
                "+F/NZqDFzrw0jvJCWlNjHsuBzV6f7UpCKpUr56RdszirSiVpGzEmsiiaAgmmkLiA4juZt/ITi1lPlm7u" +
                "Hr+ivDxvJuESUnAShbTRuVF6vuAA3RGVvtAzcVWZm2onm5VG+p+fxw07WTb6P/9kb85+HwpKKu2DDVuZ" +
                "BY0PYK1EmPuidtCSHAQzlV1Z5cm10Y8KQX7JhJG+tRc1EG2NjtzlVGJ2iOMbiD/2e00N66LdmoK+XqmQ" +
                "IYX0ciqdIo7spbakwoeJVbmZV5qqMpgULDknFsSnZZ4nW8VcmaWCdMIE50yuuQYZdFq1B3GfthE0qHyY" +
                "xiq3irlJ5DTQM8150nrizJRwRJKUi5XVS2h5TY5jPAraMtlpWkMZdeni7phvp9CiSzCJYSBpb5RbbHGm" +
                "KZAv48qdvGixy+YFlQ0FBVVaweRd9bmWCfAaO/tiWgeIYTKRo84AVwYOrciRdahBxK8oODCAYBKyt6Hf" +
                "Ke0le1jU14ylxa6GB0XhurkVY0B0FUl1uarUgLCnQ4TcvdamduVaqFvtPJmgfQDxgEcI9nTtlTg4Oho9" +
                "CZLeMfpuCJtZs+Sxqq61NdUS9uKz14CQNZx1rewa4MVVAhBeSo9Kd1tJoou9KOzd+M3Jn+PR02jZakVY" +
                "BoMa2ApsEgCz6o7q4tsWFwZyK+PDpmQt4tEx9fR0/PZo9KwB61bs3RJZUB/geRMLIsbdkRd2iSKFEKVi" +
                "5dqJZe08UZRqBk2WK7/eI1lAPGdKchk8nDClBd1COTi0iGqyi/aDkicrZUM6hmMaQ+DTsqE1af3esNP5" +
                "IuRnwD6qc48OQNoCteQlwRVD5gIIruyjEimB9sPL5QqR51XCJDdgaIGt+J3DqxbH8jogJCzLzXJZV9wZ" +
                "CK9Ro9395L+tszM3xqJqiXxmAbXEHb8OxauqXInjoyGf3iqvCTcgSVe5DWceFkVWA+H3n9EGNFE35hEB" +
                "/hzI3ggPiQhl1S1qyZGe0g0h48dg3AC84RxAcYXy3OW5CYZuT0AIVFArky/ELjQ/RYWYkFDX0mo5LbnL" +
                "yOEBcO3Rpt5eh3PFrCtZmcQ+cGxl/C9sq4Yv2fRogZiVXDz1HA4E4coiFYvQ2xCTvCQwQ+MytegTMtoV" +
                "RGYPfiMfhyaFI6K/OEXS0cbRmOjivrLx20dg9uBv/4iTF6/Gh+eUGH9/c/wJ5XqYWnFdUZmGypVTEw8P" +
                "PuVxrlPFULcWj10UBkIt/I2hvtYRBXIdjkQTAiw1c4XNVqBAkN4ld6K6oj6cRG53xldqnZqRrpAhzwQW" +
                "AGSCcGCzTUg3R5VUopg2PQrYtDyLaVeh2CJKJIF4dXby9jGqN9XIh4M3r0VgMRAHDbyiL2jweSmvGEFd" +
                "6C2Sb3JjqcswfBqT4NQIDcSYO19d3RF9xnlC5dKYK+TulRqKH/7VI0f3hr1DatCPXvT6omeN8ZhZeL8a" +
                "Pn5cGpQJnO57//4hGmm5eirCouqa3MOHeIhi7NEpSB0/0D1I+x42aVQnUOxKqYgbsxIHyVSX2q8Hm53e" +
                "RurCZhX8yJCInunoRUgS5kJ20bEURSNeZeE4zWr4ig7iW5RnqdyQJn+DjtFgHgvmNBSNF8IkOQKT244Y" +
                "/vTrL88jCbWNim99IPxS7V6SdvbHa4H4ObUwZdHEa1P42efyZSKJ7Fmc6N3M3f7PcWplLKZ+er7/LIyx" +
                "wRKJprtbogHk3AD2t+ep1SaDkpRJ7Jfj8tIUdUkEnhDSm1WvyXFK9/s6Nb/W70Kno1C+U3PbR5NEmdcX" +
                "+RrgzHcQpJ+is+ygbG/xyJJQ83xYLuQ1JQi16dPUvYIZtSfUjXJthtvgkz7+DTI+7X4RL07ej57Gz2en" +
                "L8fvxqNncXj44fXx26Pxu9F+mjh5Ox49z2LqJtzinoh0ilQ0nyWiAgdGRZcNt0ka73hobVuKtGepJHd0" +
                "3Q0dsqFQuDnySY6ew0cnhPaS3HWb4KvX7umFViyLOUqrMJxVDVfi933xoc/F+7GrMzmZXwJUNceNJ2q0" +
                "gUrUYQA6G/vg9EHr28n70ZPO6EPjaxp9hKu7KgX/R61MBVCnsBOQ4v+KGw/qjfoRZBifg91WFromFeLV" +
                "PWTQYCOuk3cHR8f/PIM+XZkpyMyTAhw6s+CVkDr0bME42NxsZGnYcKL5KCTa44EIcDkz0WOJ7+Tl+Pj3" +
                "l+dil3jHwV5rE51Vs67HW5sWG+8FqRbELtXCXpBHqJfkBOuinDDoyPmaFHBofBfCF2/Zd8s8NFV46kpL" +
                "2N/eVLdrEq15ri03p4NQMnrV5hD7lPbT+wnle73qB8+Kh9Gp2VYlRv81KbVlPJKrU6lfELeOIcL7gbgv" +
                "b6/8loL7JSoGcQjND/UydHihUGdWUbri8tkP0aKGIazr0ChRt8KJx3sHIjvlh9BEkP2Bq7myFfNt6b6X" +
                "gZpz+Msn10Z/7uw4xpvmNs9ct82ndfPpr++jfuu6ZEMTKMf34Nafm8rT6HPrd2qIkKz/3aLmWe87HK/0" +
                "6JMO1U4Y6J2HwK/EfZ4LETekao5e6R+d0+NalrUiT8zo6meaSPIDwLWi66ZyF5cZyTiPDOjdN/HKIjDG" +
                "R/e0I72FoM+tV0zA2tyRT2CWNn0nVyUz7nBZMgsdbaNUuCdf7Ac91e2EH/C/i7b8WsavUtuAIsMjWT8+" +
                "u7XPcs3bnbwVD+le+1Dkf+FPIUbiCQVLiuEIxatmF08u6WuAZviUhnkzfEbDohnuXzYZf/H8kufuywHf" +
                "eHLffIFuv4rgb4z4CwyXbW1JicbAdG+B+4beCT3H1M+1tBEt01OhE0rzVbcpxIs+Eg+XFIwUDWSeqzI+" +
                "eblLipLZpFYzIJS/bL7v6sgKx7S6pUchVQxSj908U0Y0oJM9vf/RsweuHNBIBqwP38yxn4loy8oBTM86" +
                "DW+jWGeuNaczuWFWZz5YkxV1egREXzOhN9kJGmIsZP8BuUWVVl4cAAA=";
                
    }
}
