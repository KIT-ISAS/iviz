/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MotionSequenceItem : IDeserializable<MotionSequenceItem>, IMessage
    {
        // The plan request for this item.
        // It is the planning request for this segment of the sequence, as if it were a solitary motion.
        [DataMember (Name = "req")] public MotionPlanRequest Req;
        // To blend between sequence items, the motion may be smoothed using a circular motion.
        // The blend radius of the circle between this and the next command, where 0 means no blending.
        [DataMember (Name = "blend_radius")] public double BlendRadius;
    
        /// Constructor for empty message.
        public MotionSequenceItem()
        {
            Req = new MotionPlanRequest();
        }
        
        /// Explicit constructor.
        public MotionSequenceItem(MotionPlanRequest Req, double BlendRadius)
        {
            this.Req = Req;
            this.BlendRadius = BlendRadius;
        }
        
        /// Constructor with buffer.
        internal MotionSequenceItem(ref Buffer b)
        {
            Req = new MotionPlanRequest(ref b);
            BlendRadius = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MotionSequenceItem(ref b);
        
        MotionSequenceItem IDeserializable<MotionSequenceItem>.RosDeserialize(ref Buffer b) => new MotionSequenceItem(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Req.RosSerialize(ref b);
            b.Serialize(BlendRadius);
        }
        
        public void RosValidate()
        {
            if (Req is null) throw new System.NullReferenceException(nameof(Req));
            Req.RosValidate();
        }
    
        public int RosMessageLength => 8 + Req.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceItem";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "932aef4280f479e42c693b8b285624bf";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cbXPbRpL+zl8xtf4gaaPQju3d2tOWPziWsnEqtryWNm8uFwskQQoRCTAAKIq5uv9+" +
                "z9M9PTMAqXNSt9Ld1V62amUCMz09Pf3ePXjkLq9yt1pkpavzX9Z507pZVbv2qmhc0ebL4eCRe906/Gr9" +
                "uLIo57tjm3y+zMvWVTMZ2PB9OcmPXQY4M4Bym7zOXeaaalG0Wb11y6otqnI4eCN/3wHyew8UwAdY9rJy" +
                "40VeTt04bzd5XgagglhzLAspFLfMthjmmmVV4enUrRtimblJUU/Wi6wOqwEsZincOpsW68ZQ5tBFHhaT" +
                "XWUYxXdlftu6SbVc4sGx21xxK0/cMs/KxpUeTSw4HMwWVdb++bk+GekCg8GLf/J/gzcXfzvBlm7yoh0t" +
                "m3nzeIeIslE5l/qmAMkmVdlmBdDldqb5rCgLIRwPMAvH2VYJUQFCzjuvAx2qdbtagxtat6qrm2KaY3OP" +
                "3LuszpZ5m9eN54fcbar6ulllWLi9ytrIO4DVXFXrxVRGOCAEIIPvbXQCKUAYrcJDLnYB5ml5uE2btblb" +
                "r6b40wzd65mb5DX36H6uirJtbKGxbJ7r1PkUAIDOqmpk9zh7YEaMwf7CA+u6Fi4uc+WvJk8GK0CAIKfl" +
                "rSMbDN5X46q9EFyAUd2OBC/hX266apoCzODmVbZQlCORltU0X5DmIn54OnRn2eTK5YvcyxKgcGBW1+Bv" +
                "OTVMzxRYnc/J0bKMPCC/Tq6K/Ea2CaGTHQHxts6EIHrWIGdbqFAoCIAH7llbNLMCU1/FGR8+CuRRAoQb" +
                "e1t54oOUWbnFJvHGZYsKpyInnbVXOFr9d11N1xNIpBdU2eqmWCzcTVEtCESpnOJ5qLK3Wi0KbBf0yTBc" +
                "FsGhlFXrfl6DVzfZVp8dpSjL4n2ESaJ0ASJW5816IYyEpz/nk7aiTiJgJcV2cBmep/Dj6H2rlOBUUyjJ" +
                "jsn32Me6yYVRoTJkIOVzlU8K0v2YrMkzzoBWf64QDFwHANMB1iTa/t2omO5bfF5X6xV/eGEAsM1VAeYS" +
                "2hpc/LNa5TU2XM4NrswcEVaAu16OMZiQiyVPxECIWYD6I3WXUAz5dOgurqoaTA4yVou1VyOGfp2v+HI6" +
                "HACnZ08JeGQmZZS10OqrSMpldlss10uXLau1NyxYfR9lySyLRbUBlyXC5A7Bgk2OQ5qCRUwx+4FxWQIV" +
                "vTLJFtz+LOPhqrBUK66DKVugDjYWbklQE9q6m3xRTaAiKJqlaJjJBCJMqoJBhs699MjdZAtoWRE3oHb4" +
                "5PiLj3h7eRfA7R5wkV9MwGrqH69KCHlJrqbkQPNteVQgEHgd09riJgc4v0GsDE6EAaEC5ETodipJAi1q" +
                "4lrQQpZzYHxYLHlwWdkutlSLRUMFU04Wa1gAHKyoYyhEUP/J8MmRWmZdR3hcX3nLI/wtpOCZfjF8IrCg" +
                "3pXQh8UwHx7fRREA7L5JiXMU7S8GjWzSqNGjHSlGnTHp9P64h7DbeyyfWW5IWpPNE8udCSFBs2gQqRBm" +
                "a6h+Shneij6D3JPUN5A/yMth5sbV7RHZxRSAMU1XboiW+kd0/QLoqsShwOPx4iYiwlOoxgXMGo2JeFDe" +
                "Uilg+HybfLEYUq5OxWopRxBtDq7zGUwnPTkzhUARG60hzc3g6zyDrYZ15Z+gDQooC5y6DjIth2lwx8DD" +
                "xuLGZ4qHGGTRlVWTD+Z5BapBbwvlvxMl/oyARwq0r3j++UuB4Wyp+2Gtpp3qkkpDdZfKaVbD/uZtBlcp" +
                "E5JfFXOI/ecL+ArilSxX4AF5225X8KYSNpjnwFckk8aHm6YfvC6LiRhvGoR0vghz18uYVFUN75jDhQMI" +
                "XdjVe/OvT0/EOucT2AsgtKVqqfNMXPjXp26wVmuBCYNHl5vqc2qLOS2qLa5OJpDNb1c4HeKZNSdY44+6" +
                "uSFgn5gtcIfybISfzZHDIkABdgmSQYPxbtteQc2KAGV1kdF3A2BohgWgHnDSwVECmWifwPKWlYFXiHGN" +
                "3wK2DHC5p8/hjk7FGDXrOQhIa6vuttfpUApQ8NB+i2JcI5YaiGWUJQePvhIpExstJ0IpbxpoQhzAVDjY" +
                "LL2chvgP98ONe6XAWAu+AI4Km6Biu5F35JxZnWMn1IhDF0JP0UGMtkTowkz6S0VNw+b9YOiVqkbMifhk" +
                "WsEywVcEjGV2TX8dNBZfEm4lLCv9uLJZqFHFY0w5zIdzWB7RdTJKQ0hAEBkoJq4u5jCKMhMLLcPkzPnN" +
                "wfLNnqq7IzjrYjgwAKmr1lspGt5ttYYHiz3gH7UXPfGkDC9hkbaqjil3HkSXoO/EAJqZgIVoIfTQuWbf" +
                "bsO/tuFfvz6ATYsR0Z2mrCgj/bIxPI+uJm15hnADNIKIwRrzCI15tIxJISTXzYBnW9W6+Dd8qeGYjIvh" +
                "mLxpVFUwXXCV3QRvKXen519pHBT8K4nuUtBvOBbjkiVk+mhazUa9xV62LWIxQJlUi0XRcJ/VmHED1EFm" +
                "73DgDQ5UdkEHPdDgaGDzX9n0c5mNgMxmw5L4VyMPmet+tcjmoO6U2pnsC2b2ES79sAn4OPrs4nRBlKgW" +
                "YLEpTLPZjokTDDVMlvmJaVhWDRxCF5xDNS2muyRwta2Oqyn940PDBwMZyNGaLHKouD2DsRAOPPOsdXIC" +
                "y5mfnCTh9likXTMAXJA5JiLfJix3NBhXFUPjETd3b2Z3LwMmlMqCCAj7XVULGAoTevjQk7qg6DNxJluT" +
                "jXtvD3oFhlJkp8aRgz4qANSP3mMJk3KmD9QxPqxzeoB8XsN8FA3FbQI/EAuLS0ZbQhsJU5aKmfk7BiWb" +
                "itN9dByHRm+8P/RxI4Mfw7BSPOOUfIazaoOd1tg+xDAewNslZ7+FG8+NSSpE39AbLQtQgdw2heZXCYUt" +
                "pD6QUFUJoT5jEGKN9MU4Yt3o/ipUn42RvCSsCJwUxq8I1nbGqFODpFA4RlEeKkY2lUvICYr5xZq6jb4C" +
                "bIpGPeYsKh/J7yC3wRUoQ3KM9KC6FBZkxCymOR+67rRwMsmPx97JYRa/Dt33NGq0b2pvvAqVXZQVAPrz" +
                "6TkKhLU8Fls1QVANSeWu43FqXopR+9ZzI6mnu9GzTfbu03FKOaNTU/wKXY8tg5AKJ5EayThK+IFVmD4x" +
                "HghoJsSR5IchTd+djgHdTDlBmMV+XKFOEDSq8I83kPhpgpA8MoZPHikFHkKh7Jod7Oq9+UBqQ3HsqgPI" +
                "Pf5whRaBxab5HJ6VMB5drGmFc2UyABql2pi6BknWk3bNZMDMxfWUkdUVA+kRVTIbzOSfySc9/mYLB0Si" +
                "JloPcYXFNQruks6Z522Uf2Y3QtrwGkpJlJSbXMFLGLqvKAq3cGoXrCNI/AlT4ZUF6wql+8f7069Epz2j" +
                "AT9EiIYExTbb0JXWpDX8cn1JDiaXJSFCip0SEn/qAlB0LiW68168SY4waBDoG2SdqYyyyTU33MHh/9XY" +
                "w6qxDbMKV79Zjdnw/0tq7C4tpg4opze99MOlsTBGBXbeGbSBXeIA/u29+17IhJdKr4cJFwPWewJGsepB" +
                "rYSK3abayTU0vZhyMLBkTBL/Df6+xoS6pAKwKO1hNhkX3hcWQ6/XDPNVNXU2wl+/RKxJh09GfvavzQMd" +
                "oHCS35Zp3SY6j939jOvqGmcIHmYs3jAmYlxAPYwctOSRJNc4DAfoh8TfftzD7E5lYs+p4Sj0eOLmjmHP" +
                "mbOlPuIG6U7+ti0KsPhTQ4GHyEvfEX96nd17Guo7IYITBYZUY3Fr4YpmS0EqBruWgOK/rdIkdOxUqZGF" +
                "X7NoepUhJSlkYm41lk93cFN/PA2vOUwWfISUC+CJHPG4uFcBiVKchs9DZHa0VgHrz/yND6of7YNnIbYG" +
                "IWEbSaGLC7CW2iWUArVEsw/wJIkR6+RhPUsYMC2R5Onbai2Mh4WtmvJ5QEzRkMQ+MqjTbVL6TiZEqyHA" +
                "RppGoY8Xi+iKlQRHcLxYwe3ndXEa4iuGHhBFWFN0S8IIWwFon12DUmPrBdxnTflNFnC3mWdD1VEEwUPx" +
                "RSRbo8wntL8o0XK1GjUbSQ/7qoNfmMcHmJa7SSq1MTKPRV0Qg9iN/BJ2KJscufHgsfQOA2mpmbsuq02I" +
                "Dvz4h5DJXVl86d0AyRFOhTQhqWM+vcjM/poKON5v0xPwULhHYOH03mDt18hFeWGNdWY7Z5QKlCmYyRhn" +
                "cLQrT50gPfp3xBh5rh0nuhfdwiUhEEws0lu2zGtbKaruenMmszoN5UrVDJSUfSWaNKlnBLhAH9I0LDNh" +
                "imQJBG9ALNE4iqeMemevGB3GYf1sbNN5T77S9pg3eXPVhconGLvUF3vh8F0E8SWFw4r+zKOhduK9grSF" +
                "wI19JlWGWYyiTkRsisKJTadyFtCuXOIoxY09RLIRWemOTfJdxO7llLFewhhK9VAvlLyhZL2TQeBRdICs" +
                "G/jB+S08BaKPDJ4aU1E4w8F4Cz/u5enpiydc5r0o1c5Ks7piWAmnu7wp6qqUthnmh6AhUEpGeatG4UVF" +
                "QdK+LYRZISSF9OmRrvT+7M35d2cvvpA9rVbUU4xgjZt9zOsVqyDtw4NP7dWKETrJ9olTiJt89+7s7emL" +
                "p14JxzX3LyeroBcm33jO90ctVRG0zyCs8udmYYx0tmDEIp+1GqIwRIY2Q5sGaQXSmsaI2hSZSVByqigK" +
                "bZ4RwXNtGdHaB2DiJx1QG+g7Su7Ph/60Uhk8+t3/ufMvvzl7dcmC4++f7P8jcV791zUOUZqS/ZuJwfOK" +
                "DGqMiQqGMPAKml6Vv63mmjYPoaOmcMEnzJJ3nIrrPORl0xVO5InOjzlUKbkJu0BjlW46NmUPKAZwOk5R" +
                "8QZWciffXJy/fczOH59Q+fHlm2+ZQ2InpHsZWBhqNghAUoujojaqxKSRGnUzKGiIE6+B2dedQxc5koC+" +
                "qq7hr1znJ+4P/35ACh+cHLyiZ3P65cGxO6jRFIonV227Onn8GOFHtgC124P/+INukf040scp2ZzSa0Y9" +
                "Pe/d8HASKtBzLNoDTGJvJaTgOs99FXq2gKiiNwIhjnWb7uFXFjOUiFZbPP1SeUOAcFeUe7+y5kHIXL4/" +
                "0WfFpMbNLJnfrKTzBcyJCwSQZyQBnvVJcPKnf/vLcx1B06ulVIzbxfjAr3Tx929RvoCLwBpGOKfOwhe/" +
                "LL62EQpblnIHm3nz7M/6hBWjE/en58+eyk+MrjkA7nO18SNg9tEDOu09pofCjdgCVvzSt2iDWi/4Xsqn" +
                "bbU6MIYGa99XrvYubyE2v0jvSLMipx27yRautThtYLfc+WyTRTlgCyvPgK0sywQPZ2wuAIBR4dOkiySq" +
                "4/zkGP9DCoC9En9xX57/ADOm/7549/XZ+zOYFv356sdvX789PXsPVe4fnL89e/HcpN30k1gZ4uRHqZdm" +
                "KgHVDYQVviYbh8b0eBwRumpQvif66YRk2Ikm/qSlkqVEa3blWJLr1jTVQZxzoMZN6p4+JsTGBVWNHn44" +
                "dj9qLvenFGcSWQKmvJzDWbT+754OYtgU9geiDyNtRz/AI4m/fgy05q+faMUTlJT+HivJA/LYqTbx1xe3" +
                "oD0VT2g0qmLfomr96T7MUQ4yPBTu6P3L09f/uKCHlKxphywwecDa16NUUdaR9IM0V5h7KJl4v9RPDk1Q" +
                "7BEMXRUduKOvz17/7etLd0jY/sdR3JOWbhOKxz1ddcIrkwV3SFk40vWo52wd3Z1fR38k69y1CrstOr39" +
                "FpzsXxMmW5MB9grzo5/fl0lm/f2tAm2+RoEl8pDQlPMZbGo33rGvcXzmiWpC2iNmYKne5umPRkndGRwJ" +
                "w4H3o+J2gwAJPuudIhSd0X7uS06L7oG+1yIzqZ0kN5F2126WUHxLUrLJuIfaIFCx1F4nJZUWyZELseJX" +
                "3O4nUrD3b4IYWprhSVBlOEkFgehahQ89aOUcHsRfEw3r+5OlO1M6qUPnDfbIMhecnebDxwHXuPQApKbg" +
                "YXGBJHFnMyz2gufnW1MFmz00l1KrTnogUtk29pDMtgUvLyClnYgfnime+e1I8oAPgq3E5Xsrv1oZhaRp" +
                "fB/j/5AkyG7dZ0z/feYmv+L/pu6Fk4g6cycvwOD57MOTj8wohp9f8Ock/HzKn9Pw89nHUGr48PyjPLsv" +
                "Anwih9fLa+0thvWmGKPp3ZH/IbxNw0iPS3L/RDVKbF/x7flBED8cJ7cN8KNz0+AjT6nqjtY2hY8hZ56s" +
                "paYsv2XbLfMQ3g8NaZHubYVQ6mSvC4oWTa9mKTqit8shtj7Y01nR7LZWsN8rPuxsa7fpYrq29ANs/4g5" +
                "IDbe1Q+UhI13fu5u1Dc1m15UoWwmN4aM4Om9JEvShMtY/sqW1Mjtok3I58sVDasAaxeLMnvEMYhC56rS" +
                "O38OnXF2ON2h59H2dkYnNrk74TukbzX67Yy/CY+7w+//wHoU0QyN/thj0kOtaqwJCklvW0lEQjCjd1Qx" +
                "ZvbktZ9rF/HoYH8IS6BOg2wbCs+TfDQG42+O4/KfJe+QLrrJPwa/od+R1B+557lAlzymL1DEq02xXhNP" +
                "wh0iI1SxGw6JU5wqok7r6tT8hjaApuzrXqFyo37Cr3ld+cuw8AKa2BB61K+S3P9x7/L2nWLq7yN1jH44" +
                "jrjV3Z4LDWi1W1mqgX1qigfFtN2+QqemhGYzVv94NSDUFKVh4Siq6qxG1s+bhCoZt2HDYJJyxoQ7Lpwo" +
                "CCTqyaq6pCHgbwfdibkbWM3hOx0ZB4302uj/Pu56APbqEiVmeDIjKA702amSxpd3mzsLTbuOsZ0Km4IF" +
                "LtvcMqZtq/Lot9al/An7klja99oLapkGhOn/zYWs17FyJOFBgHbsS1mhDBGcCNZPpqF0HnJY4W7aHMct" +
                "3wjY3WK3SHb3pvzSn9xQWlG7f0bZazh/jyrqNv98Wh35Wk1nls9KJJoqnowwVmwh6tHsrvD7v6n6rJNP" +
                "EiGfS3jl8rqmyjDTldQz46XdsVwmzke3I04chcG7I7afHPFrf8S/oh7b56aFGnzYb7wFKt2ZeEQ3KPpy" +
                "2uA7LZqJFh3V3hztdIqEa3/C/TJeehA7ubksAJauFfhfWhJDdo3FUi0UgRnnLAbZV0bYJk3Abzxyolwi" +
                "flyNoBD7YuhaGDJKRKhKVWPtfFYdHKcTDUJ/i0M/SRJCRh8B8Pb8EsC17WsJDHh1NLmmjN0KfzTJl0UM" +
                "89CkbKTj5w1qBYtMsEFNvAG7uRPbY0mLmwKF4JgmVqqAqXedIdHQh9JLBRqgSLL1Da1H9nkBYfwGpSO3" +
                "WteiLodB7Ds5VTlGsWDxQxvGI4xOSEaId3RO1Rmxb6JEfZ4C/KtFlnatXayL9bF0luYdLjSKl9Cj/r5x" +
                "OCG1QkYlu3Xc/fRFUGHHeuVCK2p7TMeFv94anClkP5lqt9YDJbdxZlttstq3Q9iZBpSV6bM+iyWcpb1b" +
                "KxwOuEdaXjSps+R3NqQi0WuLl9uKOua5DpC+fr1oU4oyEw9DMUoW9t/+YKziVlvQqJAcgCCit/P9sWYL" +
                "9C2Te90z4WvUAuz7DUR4JIt2zjNcNe9KovJuZBErj+oBwUfyC8b7nT3jBQDR7Huyi2rQHq3hnksKYVyo" +
                "E+uNUXZft51Dos4CjeOFUiOGABO7pfoQqU3Mlttr9TrXnpukoWZX5pgo0IIuLJDU86QvvFmDjnezm78C" +
                "Yux2xvo8C+bzq8hPdCV2JE75rcdiJhNsiHHNEltgvi+fZLxcak1gu56DrLK3o8rrGKW3feUGMBNZ4ljR" +
                "TB7SNViJVwa1aycTbhW666FOaAf2oe7Jx3YcuMkoiUomhXZVPjjhnUmtxch66lckF938zVqmpuWV18Re" +
                "X0LlgiC2s2R12b0p6MAYdll5tlcBGFOrCEkY3u9cNyNhHlvEWWU7k4YaEptveO9OOEYhWofpE29x0TCi" +
                "+KgHDNKzzXQlcWzeHBlEFmFQEGfriBk7v/J+8O+Kx091hRQ68GJXlKpQwE6URbhTKDtma5HfrDeDycHs" +
                "6AH/papgRTIP0ytw9BNJY3he4zE7SZ8cC356kfpJ6Nptd84/Mc3T3rdBMG6kmf8g5zZLrGCD2Aab5O3t" +
                "qjdDJH7B2Ke7RSsk7Np5Mw47PGTMRY/Ud35UJXpdFTKL0D3jkvoC3rAp2yVMx70wrgpfiBEbGajmnY4e" +
                "eaQVzD7h0iGVTE9plaKbKtsqdEX3BMY8FMtsBOfnBxQinqJGjz9fHKPmzLKEL26fvb04f49aeu9BLLX7" +
                "Bz+ExgavMOWcwtr/Ms79nYZECMDfpsft0yv9qzJOmNE+qdELMgWA2qL7wT8NTvZ+eMtYT+5qgaM63/OS" +
                "1i3Jiyf3CWOto/dNs04G+j8BhYUJhgFRAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
