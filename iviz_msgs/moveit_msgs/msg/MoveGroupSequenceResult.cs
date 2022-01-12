/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceResult : IDeserializable<MoveGroupSequenceResult>, IResult<MoveGroupSequenceActionResult>
    {
        // Response comprising information on all sections of the sequence
        [DataMember (Name = "response")] public MotionSequenceResponse Response;
    
        /// Constructor for empty message.
        public MoveGroupSequenceResult()
        {
            Response = new MotionSequenceResponse();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceResult(MotionSequenceResponse Response)
        {
            this.Response = Response;
        }
        
        /// Constructor with buffer.
        public MoveGroupSequenceResult(ref ReadBuffer b)
        {
            Response = new MotionSequenceResponse(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MoveGroupSequenceResult(ref b);
        
        public MoveGroupSequenceResult RosDeserialize(ref ReadBuffer b) => new MoveGroupSequenceResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Response.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Response is null) throw new System.NullReferenceException(nameof(Response));
            Response.RosValidate();
        }
    
        public int RosMessageLength => 0 + Response.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "039cee462ada3f0feb5f4e2e12baefae";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+0bbXPbtvk7fwVuuZvtVVaaOE1b7/xBseVErS25kpw2zeV4EAlJmClCBSjL6m7/fc8L" +
                "QFK0nHS32bvdLe05JvngwfP+BiSKLk2hTT5Sv61UnqihckuTOyWs/yWKTv7Df6LL0dtjsTC3Shfxws3c" +
                "890kRM9EJxfKWmNFYlIkaZqppND5TKznshBrlcMPa/IZMHGrekUXYU8B1PGyGJdFgGc8V2K6yjLhCmkJ" +
                "AfxSKGGmooBP1kxMIQAjPhBI+OI8TdEQQUa0KLyLCTKgL6z8GxBnrIbdi7lHtsxknisrltakq0SlYgrM" +
                "qDuVrJBjxjoOKzcfP/kFaVxHF7aQC7PKmTS9UELDFsbcwA8Qz2KZKaAN91yQNAlTNM2MLF6/YrTAd4wr" +
                "n0al2wqJdF4cvRS3MkNu4KOVoI6Jmstbbaz/Oro+Pe2ORicv/PN5p3dxPeyefI9/Iv/y6qLT7/f6b2P8" +
                "2j07OQzQvf77zkXvLL4cjHuDfoxwJ4cv/cfay9gDdsbds/jNh7jbf98bDvqX3f44Pn3X6b/tnhwe+WWn" +
                "g/54OLgo93rl31/3O28uuvF4EHd+uu4Nu/Go2x8NhjEg7ZwcfuOhxr1L2GJwPT45fB2oH3a7l1djxPUt" +
                "SiIoRvxZ3OhcLWShEweWDibmCrbiILvRuDMcx/Bz3AUW4tPBxUVvBEyBBL7eAfK+N7iAv0fxVWf8DqD7" +
                "o/Gw0+uPRwD/Igjz7aBz0UT2sv7tc1iO6oC1T2ER6uZV1NDO2+Hg+irudy5Byi++aX5sYAKQ1w2Q4eDN" +
                "wLMIX79tfL3o9X8MyL9rfBu8+aF7Og5fv0fpu40r1GJbzOdDAIiBgP7ofDC8jIMRHr58URqFFxaYS/f0" +
                "R7RFsIf3AIdGAYBBgjVa8Sd9C0LzBtPrnw/Kb6+QppoZbNHVH8S9H+PR4OJ6THo6evEUflzFPQpC2omF" +
                "ck7OFMScvJA6d0LnENSQYgg6cmJWRS2mUphtCd1WbY6HxmkEdBjFdOHE3wxw54TMU5Hp/MZFTuUOIjdt" +
                "/gN+5KBLcDGhQyH9wMsozi7kRkAYgcC3ygoNcVCcDc6FtBCplyrRUw1hd66s2kJ9ibAAV9uClsepmcaN" +
                "zTpFIZM5YElMlmmHfJoJRmcn9mX4BlHYGYjKyIUAiFIGB1FYfxqWD2g1RPuwOi4xxx4z7nueyRlIN9WJ" +
                "9ElPAVYLqEENLlE55ACHG0/gl7xQdmkhB6RCgjxFqqdTsdbFnDI5oCwpNISE1gedolqNK7KN0IulsYWE" +
                "NIOZag56yXBr5KZkdWJSTHL7gR4AzA3hTzIl7S5gjPlToIpN6/g4MVYdH9eS6kTBfkqslinzCsmNiC9q" +
                "JncQTYzJgNgYmXss699tgDVJydIFyPzmJkudALIlSgBSXWL1RIVSgosMCVg5cRsLkZ18x4LKQT7sAG0R" +
                "PfNZvlykQID8WexbdWuyFb7HWkI7ChAHSE2qphAwQM6bY0Ag/rLlZqGMCVhkiggWB60K9FZlJtHF5j7o" +
                "c0fAz90BuWe1RE1BVwVzj/JYLjP0MZ3XEfQXuLp/0CbGuhUvsGKVa5ACWlsKJRx76GRD8SCXC+UFMVcy" +
                "RUP1TuwIO1U+WPvNdTKv7UdSc1ASWqwTwbxSlbZFB2qMJgxgB0oNOE9QIwUPdqOwtCwGgRzas+31sx0A" +
                "nXaF85Ydgo+0Vm5ci3agag9ZX9ZqyhoxqHbidWZk5p15IW8UL/LwwDtamFmiRmXWFj/PFdTF7VlbbMzK" +
                "hhBKXOQGEHr9SOdAsxIjgncltWjhEpHIXICn3qq6OoluoRbLYuOtEaXH3LBua7y7uVllqZdckJPTv0Os" +
                "B5ZBkIyn5jUIZXLQ+Rp2ATZLGyjJrAkHraAkGgRtQS8F5mvSYDuKondsHGwjUeQKC1EDIirZj696sZz2" +
                "jlB7FQy+9ool8GgBpUg5mjDNwAZElDyVNgVxFpIiBwVbPYNoepgpoBA5XSxBcxxXNkvkuhLmDGI3FtAb" +
                "sXKce6ADWIBIE5Ij2OvWejZ5SVaok1UmsaECO9c5gk+tJPtGsNDbiN7ZMRk4dSq35Kt5YpV0GJx7ZyJa" +
                "cVUCC6Jn47U5xAQ0w+QUNi/jg7qDvOQcJSaMUX9h5tqAG6Mt7ALWvU/vYniEcAObAAlqacAJ9oHyq00x" +
                "9yn1VlotJxllvkRSBN3DRXsHNcw5oc5lbgJ6xljt8UfQ5iVe5OmwzIVuNQMBAiA0dbc65dhFdgphEIw3" +
                "0xMr7SaiUEVbRs/OLQUSVB9pBMPmtnt6E2ZtxDp9ivR2vwgCZocK1QWMyJBBOCOhifpQQ55ZBrxUzaxS" +
                "FAan8EtqIMoAninkN7MOxQNwt0qKlaXMVu3HYbVXeIGsFmjNaDcyZAu0W1+mUxzA2UBKgVJiz507LD55" +
                "zUwVVTYCtDIzfveyphbJHGrWtjinNlxi19xCx4AuTNqQuiTFu+vh2Tll2CMsJ/fvIHTC/3KNBoH5EGzH" +
                "Kf6I8RRjXs3Q69SxIOEvqwELr8X8svUdsDJEwAaGC10ymsdEJtTlb9Hw/6T6tEl1bSEuzv9wUg3g/0tJ" +
                "9aGcyu0QLnfRTEELUdgNB5BxMGGAKs35HtAaFIoA+Hfj288kJvjI8nqsoPcA1UGSNoQ87w5lWJmoYq3A" +
                "Loq1uZcxSX8Y8MCZZAK2HL2ngd0Rr8/Yq39awQKbYwCwhkPq0zDpidnBooQSCL816BdlICaLWihsAsGm" +
                "ypXUVKLNAA9tdDBLPVsLe7XUgDygCaQodqMoy5D7YzjehGDIMsHXsGQfna2FTW3OUJgqqFah6gZitdUz" +
                "nTbDKAV+z1xLFNOXYNLgUkQzbwYqBCRB2gdt0ZuSg66RIXLu0KxNVEkXJf/CmBZWVB7FtkCvyImCr+oc" +
                "UpJMQethxHpX/laWluL3J1F1ZWO7tA1h2eoynW/pHJ9+qwwUhfxFhsJv6yfyVQoanq2QYF3VtW7zM7Hm" +
                "RiGTZGIOhzE4kMCUK/MZFb6YNCDYBV/1INWzh3sa7jj87dAaqILVUzHXAqcC4in1IIOYcv8Yi4SseuQZ" +
                "xFMMDx8YfPn03HjLfjypjY4oV0FvpO/CnASdllImTtlCxYy/U3aKoBPBj+VkkgZRUHVCGeDmEnooEhP0" +
                "hIokTN/v0cZxoT7XQzDa8BlEEpmwH6G6kFdCmRs/EWxDwMLhnCqg0MOw5Kd5z3bhC7M9LtRKNkpB8AZp" +
                "1BQUIw2nQn6yRNPT6uCp3C9MKnEeKrEi9zSYFRkebJyqqYQ6SxyWhDEZWJJn0PKlG67KoChgSv2CqkAg" +
                "ZDHPbzGLTEUClSuowFNFBSTU2AlPibYaUdAGtQWsD8w+zCRlngXiKFkB1D5pQFBTIHbo2zmTJZmhxlRa" +
                "syJH8FgOWmEcRnvkKsHwbTe0m1UZ97PY4WEhxRuj+gBnGBqXB3Gb2kiwOrIDYSB1sd8iKGWtoJkvi9OG" +
                "MiBjTsVNbtbV+RzDP4VP3vfFjq/4WjxMmFJl4KfJoX0jn2kWiMwqWLxn0wtwn6yHcIH2+CDwIDgrNra8" +
                "Luh5s1RsFJiVJ9JRh0jSKb2H/46xj5jlNM9hXpiFMWJANAFzNab30Rb7nB2Fe/BZXqatjwzoKc3BOZXj" +
                "tdOEIICRyYD/sE2Cs9mFxoGJiyjiMJ0EdRU+4ViqAmsWGW7re8yCh50ulZtvY8U3ALvgDzvx4LcKxRt0" +
                "DlQCtrs4wFeY/H00K7lricmqOrxWoR3lIgKYX7GngcbSVHP3RII7qNN2hUv9cfaDTOK3irpOmrq6GXmp" +
                "lwekdGBBxVwNCGz0VpuVg7JP3UGlgOTrgqMzB5x2NNlAyd45Ozv5OqLxBnrD1k5TaxY8Cc1vtTX5Aotd" +
                "7KEttlL7CtrwDYQmcgU6byrAmV3DJnR6wDsNu5eD992TF8TTcolxCmvWvOSLxhs+sBLRLozOP89rqLF5" +
                "UeATtFAxeXXV7Z+dvPRBuNpz93a0Swui4tpbvlc1Ffv7CBH0FjrWxcoVCJGpacHdKE5DIJo5k6GsQLQh" +
                "YlTRNFUOJJkyiSSbIyRwsFS2LOkBJzxiARoATfj8WEHxy0ElevYv/xF8yIwT0n99sf+Dwjn9/OEqBU2a" +
                "kEwp4flABmEMZ1LYrTrFMxSsGEGFyuIYZMbndeWUgM+OwE7weG6rqLhR5YFQfYdjesPrqzmTDQY1g4iV" +
                "i3QSgj1gCQjTSZ0Un2BpTPbDaNB/jhdY/OzsQ+fyQjCCNl4ACoaUVg5QazExUAepVPNBTuohobRFl6oG" +
                "ne9QOvkRzW7wLk2mb9Sx+NPf91DCe8d7p1jZnL3Za4k9a0wBb+ZFsTx+/hzaD5mBtIu9f/yJWbRUMeWG" +
                "B3e5j4ysPV/doHJqUsDKURd7sEgn1CzfKOXH5tMMXHWiMx3mPWqXveIpKgsxtMxnb9g2CAlyhX7vd+aR" +
                "FxrXCuSEIY4HoDSUx4GoZ5bOEQnNsSgFQO9QBPCuKYLjb77/7hVDYOrlCQHA3ad4z+80+ulCgNqcwsPT" +
                "Uk9bG49+y94FCMZNW4m99cwdveY3eFR9LL55dfSSHgHaIoDGMtdDQNpfG5s2XmOFgoyEDcKpO39dmHSV" +
                "4XeaChRmuRcMGkz7scbyD1ULQNEZu+nE3EEPuERLa4lkA6U1FW0JzkT9YDF0OVaV58JgVmGgCBXOJJQA" +
                "gAwDPqZ08kQunL9uwX/tiA53vhNvBr9AGuPfR1fvusMupBZ+PP1w0eufdYcQyv2LQb978qq8DOfjE2UZ" +
                "pMlDcZUWQoKGROvCZZAKtDqXqyDCGpxKIfn1BTWwY57xYrtCdxhYCJyqUVx3IVLtVWv2OLlF3jTxKzBO" +
                "pHL38EtLfOCx/a91mlHI1DCpfFaUc+VmDMK2qeQPhN6uZBv/AhVJ9fShlDU+/YpZvEYSy99TRcMuVDuG" +
                "TfjbHwA4rH44qFAoZr6tTPUKSfBtDltQe0uv8bBz1rseYYVU2zMomXCigvkgkqXCpkPjB5oZhvKQDl38" +
                "Vr8KCQVHW1TDwi288btu7+27sdhH3P7hoOKJ74zUJF7xNN9qr4IviH30hQPeD+Nc2Ie58/vwQ22fh3bB" +
                "IWKQHavPNye79zw1OQ8Dwie83VfW+U2fxAMebakFbrPL6GVlQyRTXI/NJtr7atnyx1lfeaFGDU/08itN" +
                "qsE81qOVp94DrgRz8mjXyu43AdR82nvnjViMNmdfpC0sD/g7325BadeGm20R8ZC2PPWvTd9rcE/FoM7L" +
                "yeXWSKp+O0eyjrfZ/cII9vFTELaWIfHUSMV2EgMEdNfsfFbLfAYVxF9rEfZWZiuF/dcUbwOY2pU/4BFP" +
                "NKHYcR8/RbjH2COg4yOPK/LBww/uworQe93gJTG+54zU7JA53fHgRU8kqsDGDpEFtvZcRRRfnfh4xHSq" +
                "u5jmgE9CLfXlOw/5+RBctXx/X/X/5ZBA3omvcPz3lUh+hx+pOBHUUUtxfAIGrqYfv/6EE8Xy8QU+JuXj" +
                "S3xMy8ejT+VRw8dXn+jdYwngCzO8xlxr57lnY0kwNHLeR1PcF+gOEYbuAVSwPqJUR/xKU9tXOuLHVjg/" +
                "ga/wIJNEZb7bdp9QS2Ybmu9HfSpn5rW9OJXxv2jAOYSvQ8uxiI8GmP3C1AGng3gfwGLrsn08TTGiwWUb" +
                "WI92XOly9+904UXT6uUWW/dve6WrMH6A3B/jDCj8g47HUea9W9U1A/zSkJktsXh4wdZNntrC5p3mGoon" +
                "stkHKNu+juKrV7qiTKbjL1Q1jtz93RnFyYXuqdHVlR12GbS7Y2i9q+TwV00OQVLhpKnCtc+3TcJFm+bN" +
                "noPgggxR3TqqoaAjHZ0n2SpVNDuleyG1WsY9r2z4+bblPvP3jL23kP/4psO/KrHVPIk7ga1F7HQBDEfi" +
                "wufaugcSsvYD1z8e0OZ/Jxx+jpigk6ZacSYSTKx+gWwfiwojXuO/HTj4Y7dcyrGPH5vK6lS6in/BNA1e" +
                "Zmjcx37gnkwtmN3bIt+Kav/ePttW9pl4+E+mHOnTJDgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
