/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionSequenceItem")]
    public sealed class MotionSequenceItem : IDeserializable<MotionSequenceItem>, IMessage
    {
        // The plan request for this item.
        // It is the planning request for this segment of the sequence, as if it were a solitary motion.
        [DataMember (Name = "req")] public MotionPlanRequest Req { get; set; }
        // To blend between sequence items, the motion may be smoothed using a circular motion.
        // The blend radius of the circle between this and the next command, where 0 means no blending.
        [DataMember (Name = "blend_radius")] public double BlendRadius { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MotionSequenceItem()
        {
            Req = new MotionPlanRequest();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionSequenceItem(MotionPlanRequest Req, double BlendRadius)
        {
            this.Req = Req;
            this.BlendRadius = BlendRadius;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MotionSequenceItem(ref Buffer b)
        {
            Req = new MotionPlanRequest(ref b);
            BlendRadius = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceItem(ref b);
        }
        
        MotionSequenceItem IDeserializable<MotionSequenceItem>.RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceItem(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Req.RosSerialize(ref b);
            b.Serialize(BlendRadius);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Req is null) throw new System.NullReferenceException(nameof(Req));
            Req.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Req.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceItem";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "932aef4280f479e42c693b8b285624bf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+1cW3Mbx5V+R5X+Q1f0QCKGIEpUUlmm+CCLVCyXJSoiY1tWqVANTAMYczANz4UgvLX/" +
                "fc93Tt9mAFpyVcRkK+vdioiZ7tPd537reaiulkatC12qyvzSmrpRc1upZpnXKm/Mavxg8FC9ahT9bNzA" +
                "Mi8Xu4Nrs1iZslF2zgNrvC9nZqQ0AZoTLLUxlVFa1bbIG11t1co2uS1pgdf8x1sC/c5BJegPBlj5yqpp" +
                "YcpMTU2zMaYMcHlz9YjXEkBqpbc0TNUra+lpptoaG9VqllezttBVXPAhH1oAVzrL29pvG2MLE1bjk2ka" +
                "hXeluW3UzK5W9GCkNksc50itjC5rVbp90ooEf15Y3fz5mTyayAoPBoPTf/J/g9eXfzuhU92YvJms6kX9" +
                "eAeTfFSmTnWTE9Zmtmx0XgoxMzPPy5xxBzLqQNTGJngFupjspgqosG2zbhsQdV3ZmzwztZDrra70yjSm" +
                "qh1jGLWx1XW91rR2s9RNZCKCVi9tW2Q8QtGeCMyDwQ9+eAIqgJisw0NZ75IYqQGV60Y3RrXrjP6px+rV" +
                "XM1MhZOqn21eNrVfa8oowFKVyQCBtrS2NSOBmIB2h13rUpihrSpm6dIIp9UmGSwQAQNMZxoFhngweGen" +
                "trnk7dTY3YS35rh5yQDqnBhDLawuZN8RWSubmQLoZ4Gkp2N1rmdLZQrjhAtgMFJXFbE7U5Dma4FWmQUY" +
                "nNfhB+De2TI3N3LY3DE5bb+pNONFCL8GGkVIBAbBp/3rJq/nOea+iFM+fGTYkwSKnO6NdWQgnOpySyel" +
                "d0oXlujDZNfNkugsf1c2a2ckpE52+bybvCjUTW4LABF0p1s9FGlcr4ucjkxI0jScFyHqlLZRP7fEuxu9" +
                "lWfDzq559d09X/XQgb1Vpm4L5ip6+rOZNRbKCrAFIdsHg6vwIl0iDt+/UEm86xVNcm6IAp2mrQ0zLqkS" +
                "HgipXZtZDgKMmFVBbk1b609mvBETEgQiFa2LvbuXkzzbv4FFZds1fjgBIXibZU68xlj2oOlPuzaVBjoC" +
                "aJ46AbAEdLua0ngAz1cgj4fClsOK4K1IaZhsrC6XtmqgaMgWtF7H+FNUZo23GelR2tjxU4CeeLsz0Q3p" +
                "/XWK1ZW+zVftSumVbZ39oR3sQzK4pyjshtguETF1mMOsEMUy8IzX3W5kXBlQndaZ6QJ4mGsQW2TIrrEU" +
                "TdrSAYi1c8f0fneMZXVjCjsj/WHYqjBRZzMSbiCYOGas1HO3wRtdtBhFYkjbOzwaPflIb6/ugrjdBy/y" +
                "jxe7CsrJaRmAXoHRIU+kGbegmYECwLQmvzGA585IaxNrkp2BgsRMsgDQooCaV9htDlNaLmjPh/kKFNRl" +
                "U2xHzAise8pZ0ZKhIBKzwjZsZo7GR0Mx4rIQc70JFsgzPGMDpH0yPhJgZAME24f52IxHd2GFIHbfpPgZ" +
                "JqaaRk38rEktBJ7InrqDUgA7A+/FyO8xkd7Mk+TVepGYec3YJMRFywk1MW/JNrDU0WtWdaQLgPAbEkgS" +
                "nkOtpvZ2CLbxSsEzT1eIsC/nUMFhDMBtSaQhF6mMHiOkZGWneWHY3LDL5a2ZgCZPcWOKYixSdsa2TXij" +
                "coqrMnMysnD/vMWkbdJpq5Jdgm+MJsNOhhj/JBoiJxVCHCDjvAKkmeTFEUd7jvdMJ5upvWtBBpskYWEs" +
                "oY80O5Pge1bzx4A8Eai7+ugLrEbs51f7QnxWN5msKagUF6vMdEWW2jSa3CvNqF/mC9IEjwryLNiJWa2J" +
                "H/hts12TB5ZwxMLQhllUYZ9wbPjQbZnP2MzDWqTzWbq7LsnM2oo8awxnTgB0Zl0XCrw6O2EjbmYt1Bat" +
                "RMqmMpr9/1dnatCKIaEJg4dXG/sI6mMBq+sXF9+UNmtu10Qf7FPXJ7TGH+VwY4J94o2EOuRnE/pZDxUt" +
                "Qlsgm0VSAkvydtssrWjbG13lGq4eASZFURDUA0w6GCaQSwZd6tJ68AIxrvE5YMsAF2d6RB5sxiaqbhea" +
                "larz052eJw1BSp/UYZFPK4rFBmwyecnBw5csbWzAmSKQ+LomzUgEyJiHvSPA1CAP40tx414x8KxFfgKR" +
                "ysB30aTwxXqQRa8MnQTqcaxC6MrqCJEai12YCZcqr2DsnNdM+sVWFLNSYJNZg7AOEdRKX8PDJxyz10kO" +
                "KJlbeHtlXYihpcc05dCMF+ORqD0eJfEnQWAZyGeqyhdkJ3kmLbQKk7VyhyNbOH8qrhDvWRYjghGQyjbO" +
                "bMEWb21Lvi6dgf6onOixm+X3xSzSWDuC3DkQXYS+ZYvobQaZi4aEfjwI5u42/LUNf/16DwYuhlB32rW8" +
                "jPjTU3JGurq0AQ3JL5BYI8Z3yEPU3uNFMEtCck2GA8S1laz+Ld5KAMcD0wDuW+uiBNIWSDcs9U1woow6" +
                "u3gpcVNwuyQkTKG/xmAamKzC8yeZnU921nveNBS/EaCZLYq8xmntFDEGKQXt3xHZayIrn0XZxNEjb9YD" +
                "eOHnX/B0iuH89EkAPXGgZemXhV4QmjOoafAxcbWLjuGizYiho2fP7hjJVMN6k6VqPt+xdrxJCbF5fuo2" +
                "rGzdQG97z1GMjNdiHPD6405tBv/50G+IBiL4g10pjK72DcZKRHvtuOzkhKyoOTlJQvUpC74kELAi0lXY" +
                "fpNwH6Fzai2C6gnO9+Vs8F5mTJClgzwwIy5tkdVBA5CLPavyqThOEo/z0Z0fSEqGrCYLUmXZdog0QFk6" +
                "ByZMMkg9iNt8WBm4hnhekS3Ja8jebIjdSA4ChgUGk+xaKnPe/fFQdMYu+XAUh0ZfvT/0cc2DH5OVhazG" +
                "KWZO1GqC0ZaUQAhyHIA3K8x+Mxzzwc7jWeClljlhAQyXkRkQWSXDCOWAsNYhQvzIIM6SHWBLSetGv1ig" +
                "ukwOJznJpJDHgkiXwrmdMeLhWBIgT0ZWIyJKfqpyubKabXHOTs8ebVjntXjSOqohTg3VI14BYsRkhDvV" +
                "xTBvhm1kmi6CTw9zx5PceDo7OMyHuGP1AywcjJ0YH6dP+RSlJYCOPj2vAbBWIzZcMwq9SVhvTEpOSWkh" +
                "ut86bgT25DRC2+TsLp+39DAYT3X+Kyl+OjIhUuAkUsN5Sw5LaBXkWzwPhG0myOFMid80XHl4CfA5mYJk" +
                "I7uxxsB5RKRXmX+ctaSfXhCSR57hk0eCgftQKLv2h071zjtE2sus6ABwjyMu4yKwWGYW5GYx48HfyizR" +
                "FckCi8jQa2xCSTtr2op1SVxPGFn8MkI9hZuZSLL28gn3v96SN7ISzNdr9ovZTwq+k8xZmCbKP9IfIdt4" +
                "TUqJlZSaLcllGKuXEIVb8nALFCU4LtWVVxaaOewf785esk47hik/pIhtS/+vN/CrJfVNTrq8BAeDy5J4" +
                "Id2ddhUJ4gqCInMh0Z337FpihIdGAn1jkPtSUz27xoE7e/h/NXa/amyDVMPys9WYH/5/SY3dpcXEE8X0" +
                "upeMuPIsTKMCO+8M2hBBMQD/9t79wGiil4Kv+4kdw673RI8sDkGthNLfxu4kHupegDkY+NRMEgwO/t5q" +
                "ZCehAHzIdj+HjAvvi5FJVqs86PjOQfDrl7hr4OGTYaD/a3NPBGROcsfyWreOzmP3PNPKXhsckgPzGqER" +
                "QgPoYV0uOKnEOchxIKAbEn+7cfdzOpGJPVQjUgh54uFGZM+RzW3Y422gqD7ziAws/pRQ4D4y1ndEoU5n" +
                "956GQlAI4liBadLftz5ckeRpzqe+DnUp/IhlKUZlp96tyRtBzXWp11KA4WxrrL72NkIwxJilkTbGyaIP" +
                "X80BkYUJNMOBGWhpXZA+zjNXzxhxrTLE1w/3QfTBttjwcJakLoYVuBbbxZd1u/UJaBfpcWojFt3Dkj6B" +
                "gExFkslvbDtbcjV7G8ouj8LmJr4kqYuKbMU2qaInEx5EA8LgJi69wj0ksSQvO2MXg7ywmURunYwv0YUd" +
                "x9BdIpuW5N0KMMJxANsl3kjFoaODnGnJBs4KKy0glW1ZLBwYV3Hyi5RmBmtcbXm5yhSSOfa1Cbc0CElA" +
                "Q1InKfbGSD3WhQkl2ODELRKpszH5Yhl8mB5VRijRX5d2U8aik0y4l8rSrnw+d67BSJL3c86xumSP9/NZ" +
                "iO4svpAMuLM6RB4yKzE4ouNrWv4VMlVOhtNytSf6dm2EQ5DjmOqaAwrGUhQp+WMCv3MhPS1yJDnJFUAA" +
                "Tlr19xk1p4q5KLvr6nlZdvPyyikNiM++ek6a/IuYuLQokPqVZkihrHIUK5B8hD6S3fKwt/4dwsdkXD95" +
                "W3cGTBwZsNprUy97kPGIhq/cm72w8DIF8zXExvcSIOVm4CQ4hRd7E9S0jX09xscy4mzENiyiX5bl4noz" +
                "Coed/aFnic/DS911WLxMd/g8y+qUtxwNQs2R84ycLk8GEe/e5LatyWc2t+RV4Ah5I0pc9BERe7olp+/5" +
                "2dnpkaz0jrVvZ7F5ZVeSvyhv8sqW3KGDOKyCO35oKJTbkvJiKeF8cUOSXveYJM+GbrF3568vvj8/feJO" +
                "tl5DlyHmLcPpOEp2Cpi3Xod+mt88sa9lyCR/WqJHctS3b8/fnJ0+Dco6Lrt/RV5oRMpz4wTC0Z3rKofc" +
                "YeJI6GMfbqGhEYWZNxLXDF1TUm0LoIww7HVKVLqZqXPu1eJtMoqOZZMXa9/nIGaafsJzDWOtf//FdOen" +
                "lc7g4e/+T118/e35iysULn//ZPefIOjFb1dLWK9yzD1n++gUHWk5ZDkQ/9RGovKkeaCxC0m7h7hT8r/E" +
                "L5xl7/ki1ybkddNFTviJgIjJi8rz1oL0WamyabAKBCbCzKbphpxR5vTLt5cXbx6jzcjlZN4/f/2dEhBj" +
                "9TwwNGniIBFJbQ/a3OMm5p2cJ+BNz1ids6+Rl3uoz5LFSQFrr8nNuTYn6g//fQBEH5wcvIBLdPb1wUgd" +
                "VNY29GTZNOuTx48phNEFIb05+J8/uENW7GyVVlJCpVObQkXnFYFICR7geebNAU1CnydJxLUxrq49L0h0" +
                "p3lBcdK4a1s7rIuiiODRlyvPvhYmYSgzbhHWPgsm2RRmM9cm6ZJr9QmXj2iP7sD8WzGkExWwIA+BCHrY" +
                "R8TJn/7rL8/cEBhqKdHSwN1tH/jVLv/+nSL61Qb1kECv7uKXvxTf+CEOPC+nDjaL+vjP7hFqUCfqT8+O" +
                "n8pvmlBhSA5v2Y8hV2Fjq6z/HM4NDuRX8UU193pls7bAAC7PNnZ9EHgc7P6lEsB3eRixzYb7U+o1OG+k" +
                "Zlty0dnrmyH75lJYPm6qTKj5EJv51BU5RlPvLxAwGATYf5ZN8b+PRvR/4wF3Y/xFfX3x4+kT9/fl22/O" +
                "352fPnU/X7z/7tWbs/N3p8f+wcWb89NnA8e6Xm+xFcKe3Cg8H/hBWU7muPZV3zg05tzjCD8HDQLYfjoh" +
                "GXYi2URu7USJ0jffYizQdevV10GccyDGb+B4FG/p4LxVCUJ+HKn3kiD+Kd0zkMyxlykXTchgdrQS0njo" +
                "vE46jMYRt5MfT4+SX+8DrvHrJ0J1uiXBv9sVJxdBdihS+telmmu4SaJkWD+7XlnfPe+CJeGgcYeuk3fP" +
                "z17945L2k67picwwQWDpHBKsCOtwToPbN7wvyel9t9RPSpNDIk2R0rfRgTv55vzV3765UoeA7X4M45mk" +
                "IpxgPJ5p2YnQvCyoQ8jCUNaD1vPryOncOvIjWeeuVdDP0bl54OOa/Wu+sKUkF/wrmh9jg75MTk249CDN" +
                "4E2+jjzEOMV8RKzS+zdyhZOvHFIHPUl0+Ass1Ts8MVciqTuDI2Iw8MuouN14gaPXaqeyBWe1n1BjasFh" +
                "kPdSuQa2k4zpWA2kXyZU9JI8bzLuvg6YlyEd2klypZV37XrwO8f9RF73y5sghKLe8CRbRfQJBUFRuQhf" +
                "letyQf7EXxMN69qiuQ+Um7hDbw+dEbUzcn7qDx8HWOPKAeBChYM1cMrDpQL9DB+hXRvfCMu72YNzrt/K" +
                "pHtClT/GHpT5Yx3UcVPS6/jhWPZpbiecVryX3XIMv7ecLOVWM3LJgJgsCBkFfau+QibxKzX7lf4nU6fq" +
                "CMTS6uSUGNzMPxx9RHIy/HyCn7Pw8yl+ZuHn8cdQv/jw7CM/+1II+EQisFdi21th603xjCb3WP5F+/Ya" +
                "hivOyUUY0SixmOzuBARB/DBKbjnQj84Fh4+gku2Olt6HjyELn6wlpszcorHXZGPvh4bkSfeSRKifovJc" +
                "IZLpFkJZR/ROOaajD/a0a9S7/Rp0nORh51i7nRxZ61MTZPsnyBRNuMX7frK44e7Rnf2TOqjZ9I4MZDO5" +
                "ucTBIjCe3pDyWZxwOcxdIePKu7/oE2oDfDEEobsLcnx1hskQNxrkoXdv6q0jR2ekp1F/8EU0wp3xiXHu" +
                "T/k+r11g3JlxEx53J9wD8XqIkSyO/Nhj3kMxbCrpC86S+zILh2MB81HfSCZFQoa29A6jvycIf/tDWOUR" +
                "vQWblzMzmZIcbEZxB18l7/SUzvAxFi38oPikN3bfC17ApT9d2SNes4q1oEgSdZiZ0jbsEKD4TpGo7yCV" +
                "HIh0m6YsrV4U5Omx7/Crqay7vkueQR2bT4f94ss90H2X0++UXVAu63kCgSjxrHyN5o6ijDRKc9Wxj1J2" +
                "rZDnu6OsKtmj+RxVxkPHlAyIOySGUY3ramEaZy5sMm5jWGGn94LuuvAiMCYMYyJrxj24q0p37l89GPgC" +
                "xvcyNI6ayC3Xf0deuxct00VMzAJpj1Ui7PGZR48rKNd31rB23WdPHOvTw4ecWGwRLQ0/v+SVXJqGnU/b" +
                "1XrRL1KHDVL/n18lexVLUhxKBIAjVyYLJY3gcKAck4Wqfch3hVtzCyI8f/Fgz0F7FbjfOJpb/dPHSgt2" +
                "98A1ew3s71FT3Rakz1NVrv7TmelSGYkWiyRiPovNTH3M7Q/a/zma0XcWcg7lEUdmylQVdIm3cUndNLlq" +
                "POV70GZyO8HMSRi9Z8j200N+3RnyH6rl9nl3ofofThwvrXLTaM7sm7iA0nec5fVMyppilIY7PStyNTFc" +
                "auEJ3BvZSe/pAHkrqbiNVNvyNZdkpfpkcU8ssLm0bzPk1257rHTiDrEcYFH8TGPbysfYwsyh1mWn0pIt" +
                "GjpOxz7G8gWHRgomVz2uYBBvLq4IvHSkrWgPuOSa3K+mAzfC4vHrKX7zD0IDtccfvthQCdy8CWATx8Hf" +
                "LIqtu8DHTW42yVcwHGqIuXf9J9beh9zlRXjQ02Lrum2H/lsJLAB1i7bitmI1Ok4UQSc7y9RkKxc/JOJ5" +
                "BWFOLt/0SLxacV38t19SbZ/C/KsPU/3N/BufAu+P5PtmsyWu2PuL0pFSYqc8svxt6e53PYJiG0k7tVTr" +
                "9lmWS3cfN7hftdMhL33Hg+Dd82ljN7pyjRieumHbIgK6z28plxl3iaeyxEk69oys8BURrnH0uvf5hqWM" +
                "eSYDRu4quZZkJ+k3dkfqPqu7r5sg3lHrLSEqz6LIylcGHIF1sUHTAo08Zi63lQlfpMCeJ3X87E2gbLgv" +
                "3xVO4eXIL6EKK6Qir8otGm+m9uwbQYj+gcM+6wtpH0u+0hSvVISBoSItl13RK950iAVVRqiOd2EDShja" +
                "2vf8tHXLF7n5vl3VGmn9Sfp6dqWQK8RSOibzdOP89ZzAAp138567sxJ57/yGNYptF8vIXPA7dqRwtE+/" +
                "eRlBU46qV1qkZ2pmuo1ytsfH4GX29nk51SNY91/14U9mRFbFYFZZDtQ1sRWuOkrvkGbeHUWumWm5qbuz" +
                "eY9EtASRh73U3CiD/kZe4sh5oFLs4QXF+0iu57nLwch98yunpL0mLdFaUPjDJeszArzuDhziL1zP92qE" +
                "wN8iURzX9zvuvQXx/l3ctQi75n4eIBxvRrLPIKS+KfbI2eSNdTsSt5nQj8bYNYfFph56kCj0FKZB24o3" +
                "hm7pO+C/zR8/lSVS8LQzdGeJZh2OU+0RbkPymbm/yZ3XmcmEOjtqwX2rK1gY7YA6xW7X0tNuKnrMfa9H" +
                "I96h3Ag/Cs3GzQ4XJLY763/1hAZOeKBXZvzNMzeR7WRNkRGdtNiKi5TOYQ1QIHLqHtMBfLjHGfBmY4eZ" +
                "PJfBfXU9J7YsuVmZHsvXSrp2J3UYnNkTBky4D6dBXBa+gcMmNKDOuyY9JHFbmv9GTRdhDKCLsXTPqQ62" +
                "oam7Jz7elfHZkugm/ahO1dORek//PBmpn1AHeeDL6edvLi/eTX467T95j67BzpMf0cknT5wmZZKFDfxH" +
                "xQR3WhlGAX57De+/LtO/+COs6b8W0ivpMAAYqvuIafZ+eczzIN88I9bqfNHMhnx8cjsyFln633brJrz/" +
                "F9hc1/MfUgAA";
                
    }
}
