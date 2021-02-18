/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupAction")]
    public sealed class MoveGroupAction : IDeserializable<MoveGroupAction>,
		IAction<MoveGroupActionGoal, MoveGroupActionFeedback, MoveGroupActionResult>
    {
        [DataMember (Name = "action_goal")] public MoveGroupActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public MoveGroupActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public MoveGroupActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupAction()
        {
            ActionGoal = new MoveGroupActionGoal();
            ActionResult = new MoveGroupActionResult();
            ActionFeedback = new MoveGroupActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupAction(MoveGroupActionGoal ActionGoal, MoveGroupActionResult ActionResult, MoveGroupActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupAction(ref Buffer b)
        {
            ActionGoal = new MoveGroupActionGoal(ref b);
            ActionResult = new MoveGroupActionResult(ref b);
            ActionFeedback = new MoveGroupActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupAction(ref b);
        }
        
        MoveGroupAction IDeserializable<MoveGroupAction>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupAction(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) throw new System.NullReferenceException(nameof(ActionGoal));
            ActionGoal.RosValidate();
            if (ActionResult is null) throw new System.NullReferenceException(nameof(ActionResult));
            ActionResult.RosValidate();
            if (ActionFeedback is null) throw new System.NullReferenceException(nameof(ActionFeedback));
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "be9c7984423000efe94194063f359cfc";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09a3PbRpLfWeX/gIqrztKGpmXLySba1VXJEu0oaz1Wop1XuVAgMSSxAgEGD9HM1f33" +
                "6+fMAARtZ2vN3NWedys2gZmenp6enn5N4yK/N6+KvF6eTKokz17lURpE9M9wBv/uXTTf35iyTittUdCv" +
                "dpuXxsTjaHKnrabyu3f8L/7Tu7h9dRQsYPSkChflrHxysTmb3ncmik0RzOmvHuOUJmPugC3OzwKcapjE" +
                "biZEByLA50G6rGJGgLHrPQxuqyiLoyIOFqaK4qiKgmkOWCezuSkep+bepNApWixNHNDbar005QA6juZJ" +
                "GcD/ZyYzRZSm66AuoVGVB5N8saizZBJVJqiShWn0h55JFkTBMiqqZFKnUQHt8yJOMmw+LaKFQejw/9L8" +
                "WptsYoLzsyNok5VmUlcJILQGCJPCRGWSzeBl0KuTrDp8hh16D0er/DH8NDOgvR08qOZRhcia90tgHsQz" +
                "Ko9gjD/x5AYAG4hjYJS4DPboWQg/y/0ABgEUzDKfzIM9wPx6Xc3zDACa4D4qkmicGgQ8AQoA1EfY6dG+" +
                "Bzkj0FmU5QqeIboxPgVsZuHinB7PYc1SnH1Zz4CA0HBZ5PdJDE3HawIySROTVQEwXBEV6x724iF7D18i" +
                "jaER9KIVgb+jsswnCSxAHKySat4rqwKh02ogf34mbuzcFMRagmxQzvM6jeFHXhiaF00E1nI1T2BBaBK4" +
                "XYJVVAYFMkwJk0AGOqf1JpYEkkSZDAaLXNwDa6zmJguSKoCJmhKZFvjCLJYgYtIUeiPMkrlmZWBoCzoY" +
                "myniEgUTU1QRrBxi5NNX8E9iXRMgL6C3xkEsnYOpFVZZDD1YosEeLMtoZmgRgnJpJsk0mfAEBYNyINBx" +
                "g3ADQGpRlxVgFsCug1YDXT9cuV1KP5J7IMyQ0NdpBFKbcFbcARl8mgFqV0tsA2wrv8OcH+wG3RZ+KsyQ" +
                "NRLYmrBNcWVLYrjYTJMsIdZBwRjpZHA58f2CoD0AGDQXYC1YUXqT19WyrpDJlBEe9LDddYTbqjJFSRCx" +
                "6Sov7splNDHMcvhIockOwBYg9koA86D3gzb3QFkQ4dI+5PFAwoOoRWFRIefVS9j1IMKD86nl4X/kIDJL" +
                "HWtMJMChChMjBEBpmZcJL1kO2CHWEUurSV0UKGnyzJR9fFIarzFDRBgAtDQVHIcFTOAmH+fVLaFTInYh" +
                "ocboInsDgDJBEUisTy8dsRZ5DIcSblkgET4dBMMIhIFJzYIwmSIYbBkVBew7WkHahbKRZoAabyN6gPJv" +
                "Mk/gqCNEkynPC9CviojowgvvnVgMA+AD/lGVlLgJH/ROXZdf3vHZ7kHh2V3msgxA0yhbw0zhHQidHNaH" +
                "lj2CnZ8waYFt4nqC25t4jOe7StI0uE/ylE5XIreP6h6JxGi5TEWugTzjQWB1srwK/oGiAsQRP9tvYE2j" +
                "b+I8apEDcWOBhVwFT/9hJiCg1yyGmCDrB72RfeEP4Zp3D5Th4ZRPvb1ltwLMBuQ5MW6Wc0PctSoF+8Sq" +
                "uNwRydJmZ6IbisgSl0oEpLyEI64bgRlKNfwhGwTgeQePgoZ/5kvQgZAcFjR1DRGYB7pejKE9AkfNxEFB" +
                "EDA72ngLEBpwhAW387yoUNCUeVqrjNFZFKCQFNjsQY91HwAdWmEaVRWeZR5VF9H7ZFEvgmiR17RF+CDt" +
                "IDJyT5rmK1bldIuRjiIKDPDMNM2j6uvn2tKNjFBF6oDqQjpEhIvNe4iFPOmKIFnqSSJMr9gRlQPQ8OCQ" +
                "rJB/QZTSok4msLmRwMAxgyA4EQTvo7TGVrANAb29g/7Td/B2tA3iugued4rKtitQOImUQdALZHTcT6Jc" +
                "JQYFAHZDbRThyRxhbGBNOGdQQGJPOAFQiiLUpEBsQSkoomxmUOdb4ApGWZWu+8QIJHuySVqjxjA2JLAN" +
                "HTMHg4N9Eq0yEHG9sSeQMjxRA5f26eCAgcEZwNTeSwZm0N9GFYDYfOPTZ3/glhtahdorLHmBQ8ap2cgH" +
                "sNHwwS4O+Y4jUo95VbLsMR8RNYFw7uREMTGt4WygXQevSdTVS1bQYUPC5tmLgnH+fh/ZRoWCMk9zEyFe" +
                "gweeySTAQfVdsyKq+4d2ySIfJ6mh46ZErPQ0Y9Cg565Mmg54l53R2ca8UYjgKswUDlm0MfTEBDRhtkVG" +
                "KkHDLvUkRAIiBDiA26kAhJ591kaF45XpGJlSVQs4sGEnzEwO5APJTkvwlsT8IUIOGeqmPPoMowH76Wif" +
                "ic86B9blBckMyBs8LSLYYrxfQYaCeREQQw4Ca50QAywMCAGcqO2Jh1hSGLJXSE8pyObooyoZ5yAd4BwH" +
                "GIvoDnUqMA3pnIcjHwQcnq9ZmbJog8fQZc8MZrD5idGoFZKQDHEy3cHAKJIZSCbqCQMtbOcokMmB9Jk+" +
                "48OHcObBYGXQdMkrERQo/dZ5DdoFzAH+UYjHgA42xYss2yrP+7gLBESToNckg3SXwgatgFUHPStg3tt/" +
                "re2/ftuBSHFK61ZJkmSOftEYxH+TeytcQ5DErN05jXoKy1SqjoHmA8jLO9iquLh5waN/j29ZZaaGvsr8" +
                "fS56GSiVC9B459G9PbZMcHb1kjVVe9CxEu5Dv8DG0NAbhfqHcT4NN8Y7qSrQmAHQJE/TpMTZ5mPU6uBc" +
                "i/QdLHsJy0pzCXLvaAX9QQGcav8r6g5as3YPLehQQPPQL9MI7NosRu8S8jFwtdgjeChOgKGdLkUHIOyp" +
                "itw9tKum0w35QkiyUUP9fUG9yMsK3U16VrNvTJ0vZGLodMd5jBrLniIEDVHdRndYaqKiqzGOBGsfCZcd" +
                "HYHcMkdHnnEkzgYy2cigrxj9yuM+IOc4z9GMCXF+n03qdTOjR6zI7gdixHmexqWVAKDUTIpkzEcVW0A0" +
                "dTl5QciAVU0bqcjJ5cW7AYWlcwhxJ4PGHisqe4XBwxifF2AuJSXuvck+YsNWH6ps6OcL/tTYc3rgKJQo" +
                "JiVov++aOu2o3fRJSY2flPu0V10XM4XVqqyvkY0wq1YKgMsF9r7cH9DEhm4uqBeQ+wYZLoZjgPcqqJwo" +
                "HNCQEELwyW23c9npF9PxxHYWJxYwWIy2BSjQG23YMQtmvF1GEiO8lbRrIN6JklyICflqO6RhmZSsu0RO" +
                "DJExDkYojoDbiJYRlZwmhQkZOiN9Ax21KDzuqJO0h7kjh6lRMQh+wBMODzs+fESe0iyyHADK+rScnQhr" +
                "0aeDawLGDmzWe+MvJzsR0J5aCzci9Xg2vLbe3MWDMlcYRKcy+Q0EP0wZCMlwvF1DniJSBNlFaHnA+Qod" +
                "ccg2VaRReUItARVUWkE4I1tRBzFGQa4S/8hpCT91I3iPlOG9R0yBXQiUzfMHZnWjClGke5ZlAHKPLC7R" +
                "wrJYbGagZhHjob4V57CuaJ7lqIurxAaS1JOqLkiWuPGYkVkvA9KDgh/zTo50f2LUolyDNrJgypdLcueT" +
                "nmR1J+4zM5Xb/2hwWv/OHQglElLBZA4qwyB4iVvhfbQA/PvoZAJLICpUWETEYW9uzl6STDvEo3wPdGQw" +
                "GdfRCsMB7GwEfZhfIgcjl3lhDh87JiT8VSQAhfvijm68J9USWyg02ND3piDnN3qsYcINHP5fjO1WjK3Q" +
                "uJt/shjT5v+XxNg2KcaaKHYvW+bfSFkYWll23mi0ggXFBvh3690PRCZ4yfTaje1ose6wHmk7WLEyNtXK" +
                "AF9Uq3wjXlq2DMxeT41hzxjs/b2O0B+EAkBNtt1M0g3cZSPDXi0SK+MbE8FfvzqskQ4fNQP1X6sdLSBx" +
                "kkxLpW7plMfmfMZFfodhx4wM8xJNIzQNUA5H2YwiC+T1GdgFlCbut7Tbzex4T3SsGiwFL4+bXB/Oc/Sf" +
                "VaTxViioPnGKBMz9ZFNgFz7CLVaoyOzWU+t6t0YcCbAI5Pd7NVfYXZXQrO9sJAB/uEAAkbIRYYxAG8Eo" +
                "1zxassub/Fsu3tVCBGDwYeZb2tiOB314PtVQN60ZTpiAZrkY6YMkFg9yn6JD1r5+2AVRjW0+w+1cvEgE" +
                "jkDRrya9csFWXX5i6ZFrw4U57ZDqQEBPhec7rfJ6Mqf44do6uh9b5EINAkVpAWfF2otbeh0euAOEwIXi" +
                "XkGwXhCUMSMVA7SwCVtujbAfrAspjrwy6MDjqZLzboEw7HQQtjjeQMQZoD8o0+wNnKQ5Za5ERV7TthAw" +
                "4uPXQTIzwdO4WNNwhUk54UW9wTI0LiQmR6hTxwuvOUvdReKAJIhgKIO41VmZZDa3OkxrVfoYFL3L8lXm" +
                "3PzcYSe+/M39eSKqQZ8TPKbkYxVnj+r5tIm2urthD8hchZB7xEoEDtYRUxrO0VNlMylcV1309dIwh6CP" +
                "YxyVZFAQldyW4n+EqHfOOIuAp8QzGSEIhOPHWdWjJqKYwmCbqp7uZemXFCI0cPt0edB955+jxG2OISkd" +
                "aYIulEWCUS10PqI8Ymyp2bW+Q/PRa9d23paNBqEsA452Ycp5CzI+guYLedMJC1/6YF7gttHoLbrcDCoJ" +
                "IvBcNDgY1y6Twqgtw8oGkKHmPQjrF8cJq95Ewv0GfpglQvOhobZNFl/6GJ7EcenzlqyBjfKQn5Hc5V4j" +
                "4N37JK9L0JnN+wRzjcjhzwcvySNY7PEalL6Ts7PjAx7phqRvY7BpkS/Yf5HdJ0WeUU4E2mEFquN7Bky5" +
                "NQgv2iXkL65gp5ctJknifRnsZnhx9XZ4/FRmtlyiLEObN7OzIytZBDChXtoMhg/OWGMZ3ElnC+vhTfX6" +
                "enh5dvzMCms3bPeINFAfhOdKNoSsO8VV9iimL0uoto/mTqVmWrFdsy9pIGWeIsmAwipTnNCNTZlQdgyh" +
                "SSQ6ZCSvlhpZ5mMafqLmatvm+v6zyc6PC53ew9/9J7h68f3wdIT5lr+/s/xhAp1+OFpCcpVs7imdjyLo" +
                "QMqhlwPtn9KwVe6Fa6t8xm53a3ey/xf4hbzsLV3kzli/rj/IET1hEM55UShvzUCeZUE8tqcCgHEw47GP" +
                "kBzK5H75/vbq8gkmdohP5qeTi9cBgxgEJ5ahQRLbHeHF9lCaK22c30k0AT16BsGQdI0k61h92lnkFMjz" +
                "O1Bz7sxR8MV/PUJCPzp6dIoq0dmLR/3gUZHnFTyZV9Xy6MkTMGGiFIhePfrvL2SSBSlbWc4uoUzEJq+i" +
                "aEW4SB4dUPNMqkfQCTPrYEfcGSPpuNMUtu44ScFOGjTP1gbrTijdktK3JFx59oKZhKDgvFAQyNDsTSE2" +
                "k8Q0ca6VRxQ+AhxlwvQ7IEhHgaUCP0RCwMM2IY6++vab59IED2oO0ULDTbQf6Wi3f38dwPqVBuMhdr2a" +
                "g9/+mn6nTQQ8DRc8Ws3Kw6/lEcagjoKvnh8+49/QocAmCWrL2gZUhVVexO3nqNzghHQUDarJ60Ue1yk2" +
                "oPBslS8fWR5Hdv9cDuBtGoZLbKCMgHKJnNcPJmtQ0Unrm6D3TVxYajcVxsZ8gM3UdQWK0Vj1BQCGBwKe" +
                "/7Q3Wf8+6MP/Bj1KIv8meHH14/FT+fft9XfDm+HxM/l5+tPr88uz4c3xoT64uhweP9eEX5VbdAohTtIK" +
                "n/e0UZzAcVxq1Nc1dT5310L7YIIAou938JodsTeRkukwRKnpjtgWyfVexdcj1+cRH3494VF8CxMnVNkI" +
                "+bEf/MQO4p99nCPJwk5NNqusB7MhldCNR0nPLqdj4Ggb/nh84P36ydIaf/0MpPZRYvoLVuRcxGVHQQp/" +
                "Zza/uy9ChuSzZCdGcVIjCmIsMQcNGusa3pycnb+5BXz8MXWRCSYuMF94YKow65BPg9I3VJck974M9XMQ" +
                "gULCaWict9GAG343PH/13SjYQ9jyY9/NiSPCHsXdnOYNC033QrCHe2Gfx0Opp+Pw7GQc/uGNs20UzOdQ" +
                "2vHyiV3TPeZpnrFzQV9Bf2cbtPckhhKSgkxpTr+tkqXjIaIp9keLlbOt+hI4+VKI2mvtRKGfZanW5IG5" +
                "vJ260dgRBht+HhG3aS+Q9VpsRLZQWW071Gi1UGHg9xy5Rmp7HtNB0ON8GRvR8/y8XrtdTTDJrDu04eTy" +
                "I++RZD03pvsRv+7nP4LQFNWDx0MVrU8UEGCV8+YrkiibgT7xF0/CSiIqZd5R2qzN7YE5YuwMlJ/yl3c9" +
                "HGMkAChQIbB6IjzEFag91EK7M5p6SNh00Jzit9xpR6TSaXSQTKf1qHRI8RWtXw4ZT/M+JLfiTrAlG74z" +
                "nMzhVtMXZ4BzFliPQvQ++BI9iV8Gk9/gP3FwHBz0KM386BgY3Ex/OXiHzkn78yn+nNifz/BnbH8evrPx" +
                "i1+ev6Nnn4sAH3EEtkJsnRG2VhdlNL458AfhrRKGIs7e1QOWKC6YLFnYdiP+0vfyyuFHI6X8Ha5S3mzN" +
                "uQ/vrBfeG4uPMvMe7yPyZTPSQ63zpJmWbuOnGHku0JJpBkJJRrRmOYCp9zrSNcrNfA2YjvewMa3NTI64" +
                "VtcEnP0heopCSqrdjRfX3vb4QCa2iln/VgLuTe+uCBmLSHH/Top6cex1HLm0Q5F3vVphYwOUio+muxg5" +
                "Gp2hZXCI2v3QuqlyLcvRaKlr1G585Q7hRnvvcG53eZuUYhg3etzbx80OO1i8FmHYi8M/Oo53Gwwbs/uC" +
                "vOQaZiFzzFLeyRv2pLDJUGeqMOrNLNS3f7GjPIa3yObZxIRj2AervsPgS+9dNIY5vHNBC23knrTadr2g" +
                "AcT9KWEPd7HFxYLckgR7scnyihQCDL6DJaoZpOwD4WxTn6WD0xQ0PdIdfjNFTjZfCWZWWbrk0/128GUH" +
                "677J6Vv3Lq5c3NIE7KK4udLFhS1BGU6Upqhjm6SkWqGfb0tYlb1H0ylGGfeEKQkQZUjsOzEeFTNTyXGR" +
                "e+1WhgS2fxNj2xUDhhESjJDHdDjI5ZCt+AcPehrAeMtNXauQ7xX+b+S1nUiZJmGcFyhSqsLCHp4peSSg" +
                "XG6NYW2qz7o4ubqH98ixWKO1tP/pIS/vmiqe8366Wsv6Rddhha7/T4+SnbuQFN9LV4B9CZPZkIZVODAc" +
                "E9uovfV32XtKM1j4PmoeHRNtReA+MDUZ/ePT8gN2O+CazgP294ipZgrSp4kqif80eoorw5NibomIz1wy" +
                "U5ty3Ub7v0YyamYh+VAek2UWmKJAWaJnnBc39S53junmqQnfh9gztK07mqw/3uS3jSb/plKuS7uz0X87" +
                "Y3dNkJJGE2JfTwXkvOM4KScc1uRDaX8jZ4UrqthLLdSBciMb7r3IQl6zK27F0bZkSSFZjj7leE/Msjmn" +
                "bxPkC0GPhI7DEIdDWGA/Q9u6UBubmdnGuvIxp2SzhHbdEY8B35mvOGAyanEFgbi8GgF4zkhbAA54rdC7" +
                "0QoTrpjFbQqlRf6BTaBW+uEd+YLhJpUF6ykOerPIpe4iPe4Ts/LqDghpgLk39SeS3nuU5QV0iMbpWrJt" +
                "9/V2Om2Assa04rogMTrwBEHDO0urSaecK92gvIJmTsJVFDytllUXhtKU9j7Mv6iZqneh79UF3m5J980m" +
                "c7zUrFdT3UrxOaXE0vupzUoKVrD1OZ2ao3VdJ8utlBGy6lcpMuSlZjww3ZVPq3wVFZKIoatr0eYtELX5" +
                "zecyI5d4ihw4KXI5Iwus20Axjlb2Pt2w5DbPuUFfLu9G7OwE+UbqSNlmdakngfZOsFwDoZLYbVm+1y0L" +
                "HKUrTFqAlofE5XlhbA0AxDksXaERu7L2hnJzczIvO36xUVheKtCqZFB3M7V1vgEEpx8I9UlecPoYAWxf" +
                "qbANbUSaL7tirnjVWCwUZUBqdxfWkoSgLTXnpy5ruuVO9+2K2nDqj5fXs7kLKULMoWM4nu5FX08ALJJz" +
                "O+/JnRXHe8N7kih5PZs75kK9Y2MX9rvkm+4RTMoJykXEu2dsJlHt9lmHjkHDdOZ5iehhqmsdFSpS4FgV" +
                "G5PIElB3wFZ41ZFzhyLi3b7jmknEN3U3kFciYkoQaNjziBJlML+RhjgQDZSDPTQgax/e9Ty5HIy+b3ol" +
                "QlolaYapBalOzhufCKCy23KIXriedkoEy9+8o8iub2fc6wmi+p3Dmjd7RPk8SHB802c87SbVpNgDOZNX" +
                "uWDEajOQHxNjl2QWm3JfQWKgJzUVpq3oYShDb4F/nTx5xkP44AEzzM5iybo/8KWHvQ1Jc6b8JpmvHJPe" +
                "6myIBamOZE+YSICKYM+XnNNuCnhMea8HfcKQb4Qf2GTjaoMLvLM7bteZgIYhNVRhhvteO9I5WYJlBDNN" +
                "16wi+X1IAqRoOTWnKQAfdigDemxsMJNyGaqvknOSZxklK8Njrg/RPHd8hUGOPWZAj/twNmiX2aojdIRa" +
                "0qlq0iISpaVpVZAmwQhAk2I+zr4Mzm1Sd2v7qCqj3hKnJv0YHAfP+sFP8NfTfvAzxkEeaDh9eHl7dRP+" +
                "fNx+8hNmDTae/IiZfPxEJCktmUXg38om2HrKEAnwt0p4refRvvjDrKlFDlshHQKAB9UubJrOWk/Kg3Tz" +
                "DFirUUMqt/5473akC7K0q2nt2uHdKlTn7H4sIeAjr1ulmeoa7KnVDfyksG7pja2VRA3lzr5ktxKLT6nE" +
                "gSvxQxqNRIelDFRhqrrI6PYUh6GoqgZXkhJxQgk5JQtCa2pJYSjphDs+kOoB2DfEHJvfhQtlJujE/wM7" +
                "CuRs1qqBQ4qeZFwFe3QphSVACSYrGU5gb5XGLERcUVpwRmqVnyaKQO8jkPo4X8oZVU3By3aWKeGQIQ/Z" +
                "nJQVwOyevv/AlNx85MzcMiUONukp4+p8aSWuv9jxPflPGcdgNB301Svl1XFyyU1EfS7+BdIgXmdgHkwo" +
                "1zCbJjO6xcH6vzfhVgmwcz416ECZ+u1IDZBF9SlT891hJa4r11NWlgRaFQ8vJLQDbI4rB97MqbtYgFjm" +
                "rpQ6s4FUE1QM+mTXEINQ4ltBGkvEWd3I5JkxdAEY4FoiDg740O+eHBz1OpkmnZEu90nURVclRPOcLaOp" +
                "Ce0GCnFOHnvJLgQNPufQLoUayXqM2QNiu2rCuy12KGmL1mQkSIgPl4Sj5XM7tzAUurX6GW1VkyHNubIc" +
                "Kf11NmE5hEqzbAy6T+A5FzfZllvQFlBGCpTL+F1Hjbl2bTmwDaOEDF9VtrrAdheWk0FiONnXO5X3JKNh" +
                "QloFsCnWNwLGSGFQyb0KP43ilvS8XdzSLzDo15prlrCh+JOMxmColfPrevat3ALGDRnXy1Rr41TTYK/L" +
                "RnQxLoqNte1deylZ1BE4femOY8jlgN3takKD5q8iwd1e4goCD3pSqs9e4brgygJaONBV+dEOtNMBJPkX" +
                "ljjJbAYjvYaf1/wL8CEPt7xsdcFqc0Y6YA1Co83pRat+kWRB9zcLGfUDcy/mQQ4KySJaohjy57ck05fy" +
                "jMkOzlOyw9xVLab2QrYna0GNagcLkI18J+MUO2NOAOeRMywXEG8M29InfsgLLg+bxrurkLSTgkMf5ko1" +
                "Cd2la78KBzkANtRl1owHtqz1Q90RGy0n8yRVhseG7XCSqw2lFaYRDLT6axTMQWE//kLuDqySu2RQ5OUg" +
                "L2ZPqukX/1lN//ok+k/g7MkdAKIKEbfG0F3pOJ/UC+vpmYp7z9d/NkojiIBoohs89Byu7qIjNeKnvZGr" +
                "V2JLEOzixnWnNBC5qKk/QIFi7VKjWP9g4WDT1KgNp6l5dzqB6e+TGDMo8X3i+m+XT2C8/1pHeLugXC84" +
                "giwlCJs5W40B29MY4kuLFad/dQZz25uZZCEsvUmnfcpASMvciRRfWWGisMpiy444Sg0aKsjmdFWP/7U2" +
                "ReLpbgkd90zrvQws+4xdD+oyQDWcb0gJJbdi7zRbFwfZHN9TEdXzF9mcdp4OoEHF0xiRjeqsxCXkxiL1" +
                "Dw7GeQA96Ib7M3KDNfCl5g8krZ5pKZEMKblOSImCxTf6s+Dk8sy/pufxnYAIG+yA0nDjnXLBH7CriB3R" +
                "XmiWonOrAWff5E5sNNYYYzsL+3sHiHuHeu8hqUVqTX+ggIIqBa7oZaP+gU31Uu1gR/MgXeN3zEKq4n5k" +
                "FqK07CKLwikimk1lryXifDgUsNK0kzUrPJ0X5G0hYpEB3Cy2l/nz4ubVixN987m/dGIHZKKiBWP/NbP/" +
                "Gtt/RTs3N0iFYxWyqYRuFNUgR1hnXcyRp6iSTPVr7DhLwQ2BNxEf9KSLsAD/+AGkIDkP5eXnu6f8gcHJ" +
                "B3p4Rno2WpCgnHElDDh2yCcC7Qtj2pWiNsPZObXrjJVJBTLWkzqCfuK1V6hyOUMAYoZ21wR0Vrsk2j9N" +
                "LLIVKBsOPemgEUjXPbpth5dznuSTSb2E83cfjxEy9uhJlE3WSoq9wbh6MkBNIUnNPhsHDAjHOE0j9K87" +
                "NTTXwKh0979Wgrc0tN49uQYW+zbfBKOjWA9fumVgDpeew0C7IRCZRgnKDkjZ32zolLty6WGuakf33wZz" +
                "LuESsY9lVSSVMk6JXo9v8HSH/bLTj6n4H776+Mek0N9Ql+SCqEv3PSn5bpZ8MOvzYL8Vm17rK1cS+OF3" +
                "/AOrO5xfvgr0z3FwAP/1iubNgXvXNu9mWeQTvkMvFlfjoz4C8+R0dP52GHgwnzZhosrHl8OBybnS/icB" +
                "vr4ZDi+uR8MzC/hZE3BhJibBZPUIfQMTY7/aE0RTvDiaSA1z6yBFxSf4wB+8m40ciVTgz0ZpqDgmi9F5" +
                "A/dGplgkKOLJ87Svcbc3p6fD4ZmH8mETZfxuks2xL+sJUgF3+bqTENuGOXlxdePogsM87xhmTJ+s2PCD" +
                "do8U1+ajpLHOiWmUpOiw3YLezRBLVzj8joOvNtErDB6nWzjAVmFosUv/4zhqMkflD1ZnmCOTkK4dUPUi" +
                "/jrElgkI59mdchx8vQPOs6yH1gNuQsd8dvEshU9PXr92O/k4+POnIig1rrsw/BTqSmWM5mo1kc6mSWHL" +
                "uVa+FCBMTNyYhM8m3/wLJvFpZEamaGw/HgArYW/hiddXtyMf1HHwLQE8sZ86k2/AoZsyxmocC/5UBNW0" +
                "VRIglGaCB9Jt/Al7j/LB8nsjX7BKCtP1oTU4jck8tTZRLQF4Vxc0kloorKN5hxn5s824ns2QjNKoMu+r" +
                "3X7VTI7gXo+Lgg0xdfoUlQ/Oog4n8G942/2pAP8+o1y122zp1Wfjz/DEoev2kQ56DbHZw1rB/od5dkU1" +
                "n0YSNKLMIHLD3NPHIoHDMFsM7T9uQAfV7S1mjPCDlyfnr9/cDI+/xT/Ylx9fvz65vAQ5E+L74dnxY9vh" +
                "/PLtyevzs/DianR+dRliw+PHz/St9zSUlidwIoQvfgqHl2/Pb64uL4aXo/D0u5PLV8Pjx4fa7/TqcnRz" +
                "9doO91xfvLk8efF6GI6uwpO/vzm/GYaS4gJgT44ff6XNRucXMMrVm9Hx46/tHFSPOH78Z3FpuICzLRBt" +
                "v2fIbFZaSo1ObkYh/Hc0hJmEp1cgd29hbkCKg642b8+vXsPft+H1yeg7aH55O7o5Ob8c3UKHp46wr65O" +
                "XrfhPWu8/BCgw0ZL7532wpV67kbTxXp1c/XmOrw8uQCaP/1q420LGLT5ut3m5urFlUwVXv+5/RrOpL8p" +
                "/G/aL7mklb7+VhwzXOK7SfSXN9AmBDQub19e3VyEyp2Pnz11nCKEAyYanv4NeRR45C00REaBlpaaHsr4" +
                "X3ppCShsdH758sq+fM6YeazRxO7yKjz/W3h79frNiBbu8OlOXEYtUfTR2pN819PP8Wn3aJSBH/kflmt+" +
                "i8SHsaN76FtQ6zWKmWvmBOql4ibrDC9pSqFL7aKQecddc72xXW4a711lRKRQ+eNYv1nnw9rjWuVapr1d" +
                "F35fr9W3v0nogaAEVPlKGZ31mttlb6g/cffSnzRvo0tUYS5XCyi+IoWE5JGF5t2O71st1HbioIz7Kh+8" +
                "k4CDf6uegA22FA/fspp/TImDDyGja9JeVlTqlMX8wOce3nTMwTwAePufViO9p6XdJBc9cuWrXfRFWbN5" +
                "B4hx2VJl3StQsDFEtu0bfP/EOE0u+0NrHGz5Ivk/7b6xnzT/I75lbrHXPcQJLr3/AQCWp9W7fQAA";
                
    }
}
