/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceGoal")]
    public sealed class MoveGroupSequenceGoal : IDeserializable<MoveGroupSequenceGoal>, IGoal<MoveGroupSequenceActionGoal>
    {
        // A list of motion commands - one for each section of the sequence
        [DataMember (Name = "request")] public MotionSequenceRequest Request { get; set; }
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceGoal()
        {
            Request = new MotionSequenceRequest();
            PlanningOptions = new PlanningOptions();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceGoal(MotionSequenceRequest Request, PlanningOptions PlanningOptions)
        {
            this.Request = Request;
            this.PlanningOptions = PlanningOptions;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupSequenceGoal(ref Buffer b)
        {
            Request = new MotionSequenceRequest(ref b);
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceGoal(ref b);
        }
        
        MoveGroupSequenceGoal IDeserializable<MoveGroupSequenceGoal>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Request.RosSerialize(ref b);
            PlanningOptions.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Request is null) throw new System.NullReferenceException(nameof(Request));
            Request.RosValidate();
            if (PlanningOptions is null) throw new System.NullReferenceException(nameof(PlanningOptions));
            PlanningOptions.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Request.RosMessageLength;
                size += PlanningOptions.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12fc6281edcaf031de4783a58087ebf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09a28jN5LfBfg/NDLAWb4oGmcmu8g56wUmYyeZIPPYsTevwUCguimp41ZT6Ydl5XD/" +
                "/epJslvtmeSwo73DXnYRq7vJIlmsdxWZ0XPX5K68sr+2tkzta/xbN0nFf0ej0avClGVeLl9usF2dbOR5" +
                "5vjF6Pwf/M/o+dXXZ8na3dq8ma3rZf1wcIqjB8l3OczULaAtfvcz08kn27xZJSaZF7bMZpXJ8rZOFq5K" +
                "rElX0yMA8KxMmpWF9vUGVmIRFj4TIFslpijkFXzb5vA0t4m9s2nbGACamDpxpU1qmRiA7M70WWPXb94m" +
                "Ofypj0YHRxQOD4u8lhV5tCAKmlVe08QYD00Cj37pMQ5949ou17ZsFEe66AliIV8ArGRrK8BJUrsib0y1" +
                "k23xWEFCisjraIQjXzveHsBss7W29HAZaxMaS/Z3bXa4AfXaOXibJW2NEzVJmldpW5gqDMiLZsCy7zJt" +
                "bAs7p6PRygy0wm+lvWuS1K3X8GKSbFe4nNNkbQ1QfSnzhBEB/qJwpvnzZx3KOuAGR5ikpdLuVLc5YC11" +
                "ZWPykjczs4u8zAl3uI3Gb2rjIrwiujzFCypc22zaBjd1U7nbPLM1b9crU5m1bWxVC2EAX7jqpt4YGLtZ" +
                "mabDP/XKtUVGLRKYE4A5Gv2gzSNQHsRs41/yeFdASA3uct2YxibtJoM/9TR5tkhSW+FKk19cXja1jjUn" +
                "FOBQlc0QAkxp4+qcZRfSDc7aMN+nbVURSZeWKQ3YPDRmiAgDic42CRLE0ei1m7vmiqZT4+xmNDWh5hUB" +
                "qHOUDktnCp53QNbaZbZA9BNDwttpcgnCKLGFFeZCMNjSVBWQO+0g9DcMrbJLJHAah14g9aar3N7yYnMh" +
                "cph+UxnCC2/8BtHITMIwAD7M3zR5vcix79PQBSQWwp5FUHh1L5xsA+DUlDtYKXwDIelgf2jbDYjbnFEL" +
                "ZJO1KTBpJJtZhN7mrkAgjO54qmPmxs2myGHJgCSUvzQI7E7pmuSXFoW62fG7k86safT9OV/30CHivi2I" +
                "quDtLzZtHAorhM0I2R2Nrv2HeIjQfHigEmhXBU2sk5DmHIgrS4QLooQaItdubJrjBkyIVHG7DUyt31lV" +
                "D0CArYJxce7ycZZnwxNYVq7d4IMwCMDbrnKgNcKygoafbmMrg+jwoKnrDIFFoNv1HNoj8HyN26NQSHM4" +
                "Zrw1CA2bTZOrlasaFDSgC1qVMbqKym7wawZyFCb2+BGCnnmrwjQg9zcxVtfmLl+368SsXSv6B2YwhGSk" +
                "nqJwWyC7iMWScY5qBXYsQ5pR2S0tw8gIVaROagrEw8LgZjMPsbUDnXawACDtXIheZ0dYTm5t4VKQH5a0" +
                "Cm1qmgJzI4KBYqZJ8kQmeGuKFlsBG8L0xqeTT9/C1+v7IO6G4AX6UbarUDiJlEHQayR05CeQjDvcM1uR" +
                "tQNUnN9ahCdrhLGBNEHPoIDEnqABUIoi1LzC2eaoSsslzHmcr3EHTdkUuwkRAsmeMi1aUBSwxSSwLamZ" +
                "0+npCStxHoio3noNpARP2MCt/XR6ysBABzC2x/nUTif3YQUgdr/E+DmJVDW0mmmvWc0bPOM5dRvFAPYa" +
                "HkTJD6hIVfPAebVZRmreEDYBcUFzophYtKAbiOvgM4k6kAWI8FtgSGCeMVjF7u4EyUaFghJPl4lwXmJQ" +
                "ocHogbsStgZMpMjqRi5Zu3leWFI3ZHKpNmPQYClubVFMmcsuSLcxbVQiuCq7ACWL5p9qTJgmrLYqyST4" +
                "xhpQ7KCI8U8kIXIQIUAB3E4FIPScsAsgFK9Ex5Op1bQAhQ2csLQO0AeSnbbgexLzjxHyjKHuy6MPMBqQ" +
                "n472geisbjIek1HJJlaZmQo0tW0MmFeGUL/KlyAJPinAsiAjZr0BeqCvzW4DFlhEEUsLEyZWRf2Ey0Yb" +
                "ui3zlNQ8aou4P3F31yRJnavAssbmRAkInUhXXIFnF2ekxNHxArEFI4Gwqawh+//ZRTJqWZFAh9GD6637" +
                "BMXHErWuDs62KUzW3m1gf3Cepj6DMf6dFzcF2GeqJJIxvZvBY32SwCAwBdBZwCWoSV7tmpVjaXtrqpwc" +
                "QQAMgqIAqMfY6fgkglwS6NKUTsEzxDDG7wFberi4pk/Ags1IRdXt0pBQFTtd5DxICBD6IA6LfF6BLzYi" +
                "lUlDjh58RdxGCpx2BDm+rkEywgZkRMNqCNBugIXxoahxkA2UtMBOgK2yaLsYEPisPUCjVxZWguJxmnjX" +
                "lcQRemrEdr4nmlR5hcpOrGaQL64CnxUcm8xZdOvQg1qbG7TwMQKAVicYoKBu0dor64IVLbyGLmM7XU4n" +
                "LPaoFfufAIF4IE+TKl+CnqSeMNDadzaJLA504eIRm0I0Zx4MNgyAVK4RtYW6eOdasHVhDfCjEtYjM0vn" +
                "RSTSODdBvhMQXYS+Io2oOgPURQNMPx15dXfnf+38r98OoOCCC3WvXsvLgD8zB2OkK0sb3EOwC9jXCP4d" +
                "xiFqtXjRmQUmuQHFgZvrKh79W/zKDhw1jB24b514CSAtMNywMrfeiLLJxcuv2G/yZhe7hDH059gYGkaj" +
                "UP9Z5hazvfGeNA34bwAodUWR17haN0cfA4SC0W+w7TVsK60lcZGhB9asAniq/V9Sd/DhtPvMg54JaB76" +
                "q8IsAc0ZimmkY6Bq8Y7RREuBoINlT+YY8FRDcpO4arHY03Y0SXaxqX9sNqxd3aDcVsuRlYxKMXJ4dblz" +
                "l6H9PNYJQUN0/lCvFNZUQ41xJNh7I1R2dgZa1J6dRa76nBifAwg4Yi7RwSaiPkDn3Dl0qme4vg+ngweJ" +
                "MUKW8fxAhLhyRVZ7CQAmdlrlczac2B+npYsdCEIGtCYxUuVIdzA3oLAUA8Z3wjiomM3jyqJpiO8r0CV5" +
                "jbyXnuBsOAaBigUVJui1mOfU/FEoJiOT/GQSmgZbvd/0YU2NH4KWRV4NXewCdqvxSptDAt7JEQAv1tj7" +
                "xcmUFnYZ1oJWapkDFpDgMlADzKugGFE4oFsriGA70rMzRwdIU8K4wS5mqBLJoSAnqBSwWNDTBXdurw1b" +
                "OBgZ1m0kMcKspF0TiZXVpItzMnoGpGGd12xJmyCGKDRUT2gEDWeTOdXFME2GdGQcLkKbHtUddZL2sHak" +
                "MHVxp8kPqOFQ2bHyEXlKqygdAJT96VkNCGs9IcWVgusNzHpr4+3kkBZ69zuhRsQer4b3Nlq7xPNWCoPw" +
                "VOe/geCHJWMcnuBEXENxS3JLYBSMtygN+GlGyKFIiU4aTXm0EtDmpB0EHdn1NUZiEYFcJfoRbQmPygjR" +
                "KyX46BVj4BACZV//wKpeq0FklGdZBiD1yOYSLjyJZXYJZhYRHtpbmYN9xWCBQ89QJTagpE2btiJZEsZj" +
                "Qma7DFAP7mbGnGyUP9H8r3dgjawZ85h9yTjiF2wn7rO0TeB/DH/4aOMNCCUSUkm6ApNhmnyFrHAHFm6B" +
                "SQnyS02lwsIQhf399cVXJNMeoyofg8e2g/+bLdrVHPoGI50/IgVTtif4C/HsjGQkgCoACvdFju58J9MS" +
                "Wyg0YOhbi7GvZG7SG1xwZw7/L8YOK8a2GGpY/W4xps3/L4mx+6QYW6LYve4FI66VhKGVJ+e9RlvYUGyA" +
                "f3vffiA0wUfG12F8Rz/rAe+R2MGLFZ/627q9wEPdczBHIw3NRM7g6G+twegkCgB12Q6zyDDwkI8MvFrl" +
                "XsZ3FoJPv4ZZIx7e6wbqr+2BNpAoSZalUrcOxmN3PfPK3VhcJDnmNbpG6BqgHDblkoJKFIOc+g2UJuFZ" +
                "2h1mdcwTA7sGW8HbExY3AX2O0dyGLN4GBdXvXCIBC4/sChwiYn2PFyoyu/fWJ4K8Eyd1Gov8Tt0VDp7m" +
                "tOobn5fCh5CWIlR28t0GrBHMua7MhhMwFG0N2dfeRAAGK7PY08Z2POiDZwuESMyEe4YLJqClEyd9mmeS" +
                "z5hwrYj61w+GIKqzzTrcryXKi+EIlIvt4svJbDUALZ4ehTZC0t0PqQEEjFREkfzGtemKstk7n3b5xE9u" +
                "pilJU1SgK3ZRFj3qcBQUCIGbSXiFakhCSp5nRiYGWGEpe26diC/sCxmOvrqEJ83BuzXC8MtB2BJ4AxGH" +
                "FR1gTHM0MC0cl4BUriW2EDCScdJBSpuiNq52NFxlC44ca25ChsaNBKA+qBMle4OnHvLCgBKc4EwGCbuz" +
                "tfly5W2Y3q5MMEV/U7ptGZJO3OEgmaV9/nwipsGEg/cLirFKsEftfGKie5MvwAOyVkHkmEiJwME+Pofh" +
                "n2GkSng4Tlfrpu82likEYxxzw4VYhKXAUvxjhnbnkmtaeEm8kmsEgXDirL9G1EQUU1J239RTXpZ+eSVC" +
                "A9lnKJ8TB/8CJq4cJkh1pBRDKOsckxUYfER5xLOlZq/0G7qPUbt+8LbuNJjJNuBoz2296kHGV9B8LV8G" +
                "YeHHGMyXyDZaS4AhN4tGggi8UJuQzNtQ12PVl2FjI5Rhwf5lWc6mN6HwpDM/rFmi9dBQ9y0WP8YzfJJl" +
                "dUxbsgc+50hxRgqXR42Adm9z19ZgM9s7sCpwCXnDQpzlEWz2fAdG35OLi/NTHuk1Sd/OYIvKrTl+Ud7m" +
                "lSupQgf9sArN8bEFV24Hwou4hOLFDXB63SOSPDuRwV5fPn/5/eX5p7KyzQZlGfq8pV8deckigGnqta+n" +
                "eeeKNZfBnXS1sB/RUl+9unxxcf7IC+sw7PCINNAEhOdWGEL2nfIqY6owkS1U34dKaKBFYRcN+zUnUpRU" +
                "uwJRBhhWmRKEbmbrnGq1aJqEosc8yZcbrXNgNQ2PaLn6tk6/fzDZ+X6hM3rwh/9JXn757eXTa0xc/vHO" +
                "8g8j6Om7syUkV8nnXpB+FEEHUg6jHOj/1Ja98qh4oHFLDrt7v5Pjv0AvFGXv2SI31sd140HO6A2DCMGL" +
                "SmlrCfKsTLK51woAJsDM5vGERClT+OXbq5cvHmKZkcRkfnry/LuEQUyTJ56gQRJ7johyeyjNFTch7iSW" +
                "gKqeaXJJtkZeDuw+cRYFBZy7ATPnxp4lH/3nMSL6+Oz4KZpEF18eT5LjyrkG3qyaZnP28CG4MKYApDfH" +
                "//WRLLIiY6t0HBIqRWzyLopVhJsU4QEtz7w5hk5Y5wkccWOt5LUXBbDuPC/AT5p2dWuHdDEpwnjUdOXF" +
                "l0wkBCWlEmGjUTCOphCZSZmkBNfqM0ofwRxlwfScEKSzxGOBXyIi4GUfEWd/+o/PP5MmqKg5RQsN96d9" +
                "rKNd/e27BPavtpgP8fvVHfzq1+IbbSLgabjkeLusH/9ZXmEO6iz502ePH/EzdKiwSY7WsrYBU2Hrqqz/" +
                "Ho0bXJCOokk1+bx2WVtgA0rPNm5z7Gkcyf1DBYDvszBCmQ3Vp9QbpLxJku7ARCerL8Xom4Sw1G+qrM/5" +
                "AJlp6AoMo7naCwAMFQLqf+JNtr9PJ/C/6YiqMT5Pvnz54/mn8vvq1TeXry/PH8nj05++e/bi4vL1+WN9" +
                "8fLF5flnIyFdlVukhXBO0grfj7RRloM6rjXrG5qGmHtooX2wQACnH3eImp1xNJFKOzFFqcW32BbRdafi" +
                "6zj0OWblNxIaxa+wcJoqOyE/TpKfOED8czxnRDL5XrZcNj6C2ZFKGMbDyuuowmgacDv78fw0evrJ4xqf" +
                "fgZUx1Ni/MusKLiI246CFP5KqLlGM4mFDMlnqZXV6nlxlpiCpp19nb1+cvHs71cwn3hM3WSCiRvMlUOM" +
                "FSYdimlQ+YbakhTel6F+TgwYJFwUyXUbHbizby6fff3NdTJG2PJwEtbEGeEI42FNq46HpryQjJEXTng8" +
                "lHo6Dq9OxuGHaJz7RsF6js7JA/Vrhsd86koOLugn6B98gz5Pzq0/9MDF4E2+CTREOMX+6LFy7d9EEicf" +
                "C1JHPU4U/HmS6i0eiCvi1L3GATHY8MOIuH1/gbzXai+zhcZqP6BGu4UGA3/nzDViO4qYTpMR18v4jF4U" +
                "543aHWqBeenDoZ0gV5x5N1KD31nue+K6H14FoSuqiieaKnqfKCAKObUFWtGUS7AnvogkrJRFUx0oFXH7" +
                "2h5YI+bOwPip37wd4RjXAoASFQJrJMJDQoHaQz20G6uFsDSbAZxT/pY7HQhVuowBlOmyjuswKa51fPOY" +
                "52nvZhRWPMhsyYcfTCdzutVOJBgQggU+omDuko8xkvhxkv4G/8qS8+QUN8skZ+dA4Hbx5vQtBif946f4" +
                "mPrHR/iY+cfHb33+4s1nb+ndh0LAewKBvRTbYIat10UJjc+x/JPmrRKGMs7RQRiWKCGZLGcCPCO+mUSn" +
                "HOChc8DhLe6S67bm2oe3PgofjcWqjE9U2myqdqgPnnQPSfj8KWaeK/RkuolQkhG9VU5h6aOBco16v14D" +
                "lhO97Cxrv5IjazU0Abp/hpGiGZV4HyaK688e3Vs/abyY7R+OjU4ukbOIGI9PSGkUxx8OkyNklHnXgz4+" +
                "N0AHQ9B1FydHszO0DWGinh9656ZeyXZ0Wuoe9Ru/DEq40z5Szv0u3+e1OMadHrf+dbfDATavhxiO4vDD" +
                "gHr3ybA5hy8oSq5pFnLHPOaDvOFICrsMbakGo54TRHv7jR/lE/iKZF6mdjYHPthOwgw+jr6ZOazhbUha" +
                "aKPwptd26AMNIOFPSXuEY1YhFxS2JBlntnQNGQSYfAdPVCtIOQbC1aYxSSdPC7D0yHb4zVZOju+CZVCH" +
                "4tOTfvLlAPu+T+n38i7uXNazBPymhLXSMZp7kjJcKE1Zxz5KybTCON89aVWOHi0WmGUcC1ESIKqQOAli" +
                "3FRL24i6cFG7rSWBHZ8Luu/AC8OYEYwZjxnmIEeV7p1/cjTSBMb33DS0mvEp1/+NtHYQKdNFTIgCGcUq" +
                "bOzjC0WPJJTre3NY++azbo7T8PCYAosteksnvz/lFR2aRj0fl6v1vF8MHTYY+v/9WbJnISVFroQHOJE0" +
                "mU9peIMD0zGZz9r7eJc/NbeEjacbDwYW2svAvWNpMvr7lxUn7A5ANYMK9o+IqW4J0u8TVZL/6fSUUEYk" +
                "xcIWEZ2FYqY+5oad9n+MZNTKQoqhfEKeWWKrCmWJ6rgobxodNZ7TOWg7u5thz5lvPdBk9/4mv+01+ReV" +
                "ckPWnc/++xWHQ6tUNJoT+UYmINcdZ3mdclqTldLJXs0KH030h1qoA9VGdsJ7xkPecShuy9m2fEMpWc4+" +
                "OTwn5smcy7cJ8nOZHgmdMEMcDmGB/wxt20p9bCZmn+tycy7JZgkduuM8pnyDQ8MJk+seVRCIFy+vATxX" +
                "pK1hDnjINTpfDQtumMTD7Sk6+SNfQK34wxsbKoabNx5sZDjoyaJQuov4uM3tNroFQ1ADxL1vP5H0HlOV" +
                "F+DBzIudVNue6F0JxAB1i2XFbUVidBoJgk50lnaTtFy4SERpBd2cnO/0iKxaNl307pdY2scwv1A3VU/m" +
                "32oIvN+SzpulKzxirwelw06xnlJk6Wnp7r0eXrBNuJyas3VDmuVKzuN686sWGfKVVjww3pVOG7c1lRRi" +
                "6O76aTMLmD69xVRm5RBP5YCSTKgZWeMtIpTj6FXv0wlLbvMZN5jIUXLDwU6Qb2SO1H1Sl9tN0N9JNjtA" +
                "VJ4FluVbBmSDTbHFogVo+Zio3FXW30iBc57V4dobv7P+vHyXOZmWA734LCxvFVhVMmg4mdrTbwAh2AeC" +
                "fZIXXD4W3dIUjlT4hj4jzYddsVa86WwWijJAdTgL61FC0DZa89PWLR3kpvN2VWu59Ceq69nnQsoQc+oY" +
                "1NOt2Os5gEV03k97cmYl0N7lLUkU1y5XgbjQ7tjjwsmQfFMewaKcpF4b5p65TU0b+GzAxqBhBuu8RPQw" +
                "1vVWH7oyI5AqNiaRJaBugKzwqCPXDhmi3UmgmtTwSd29ySsSsSQILOyVoUIZrG+kIU7FAuVkDw3I1kd0" +
                "PE8OB2Psmz6JkFZJWmJpQaGLi8YnBKjs9hSiB64XgxLB0zdzFPn1/Yp71SBq34VZM7MbqudBhOOXCc/T" +
                "M6kWxZ6KTt46mRGbzYB+LIzdkFts6xMFiYmewjZYtqLKUIa+B/6r/OEjHiIGDzPD6iyWrCfTWHr405C0" +
                "ZqpvkvWKmox2Z08syF1dXsMYASqC3W24pt1W8JrqXk8nNEM+EX7qi42bPSqIdHfWv/UEGs6ooQozuvNM" +
                "OpKerMEzgpUWOzaR4j4kAQr0nLrLFIAPBowBVRt7xKRUhuar1Jy4sqRiZXjNt5V09U5sMIjaYwKMqA9X" +
                "g36ZvwOHVKhHnZomPSRRWZreUdNFGAHoYiyecyyDnS/q7rGPmjIaLQlm0o/JefJokvwEfz6dJD9jHuRI" +
                "0+mXL65evp79fN5/8xNWDXbe/IiVfPxGJCltmZ/Av5RPcK+WIRTgs0p4vV2mf/CHSVNvC+mldAgAKqpD" +
                "+DSDN48pDdLJMyCtzo1mzsfjo9ORIcnSv9vt0AHv3v2hwe/HKwTiySurdEtdk7F63UBPCuuKvvibu6ih" +
                "nNmX6lYi8QVdcRAunCKLRrLDcilZZZu2Kun0VHSxJ99r1rscFAWhd7XkmjLphByfyO0B2HeGNTZ/aC5U" +
                "maAL/zfsKJDLZe9GJjL0pOIqGdOhFJYANbis5DiBv1VbuxZxRWXBJZlVcZkoAr01IPXpIlOsGVVLIap2" +
                "liXhkDMesrsoL4A5PH37jiWF9YjOvGdJnGxSLRNundN74b7w40fynyqOwWk6nWhUKrpVLBQ3Efb5KjqQ" +
                "BtmuBPcgpVrDcpEv6RQH2//RgnsX0j1jrUEKZRG3IzNANjXGTMtnhxW54fKouvEo0Dsa8UBCP8EWqHIa" +
                "rZy6iweIly7WcmFTIndb6gwm5NcQgVDhW0UWi+GqbiTy0lo6AAxwPRKnp6z0hxcHql4X08Uz4uU2N0N4" +
                "VUR09WxtFnbmGWiGa4rIS7gQLHjHqV1KNZL3mHEExHfVgnd/9aaULXqXUa6/1QsKafsC51aWUrfePiNW" +
                "tSXinO85JKO/LVOWQ2g0C2PQeYIouLhPttyCWEAJKVEq428DNx72bzoE39Dk5PiqsTUEdviaQxkkA82+" +
                "O6i8JxkNC9I7KbtifS9hjBgGkzy64adz1Sq971+1Gl93Gd982L3ChvJPMhqDoVYhrhv5t3IKGBkyazeF" +
                "3o3TLJLxkI8YclyUG+v7u/5QspgjoH3pjOOM79UKp6tpGrR+FQnh9BLfIHA0kosj/RGu53yzgF5jGW75" +
                "0Q7E6QCS4gsbXGS5hJG+g8dX/ATzoQi3fOx1wbsPrXTAGzGtNqcPvfuLpAp6sn+R0SSxt+IeODBI1maD" +
                "Yihe34ZcX6ozJj/YFeSHhaNajO21sCdbQZ3bDvAWaz6T8RQ7Y00A15EzrJAQ7wzbsyd+cBVfVlxkh7sh" +
                "6SAXDr2bKtUlDIeu41s4KACwZy6zZTz198M9UI7Ya5mu8kIJHhv200nhbijSlHJ7HbT6i0lWYLCffyRn" +
                "B7b5TT6tXD111fJhs/jor83iLw/NX4Gy0xsARDdEXFlLZ6Uzl7ZrH+lZSHgvtn/2rkYQAdGdbvIgCriG" +
                "g47UiN+OrsN9Jf4KgkOcuB6UBiIXtfQHMFDtQmkU2x8sHHyZGrXhMrXoTCcQ/W2eYQUlfs9D//vlEzjv" +
                "v7YGTxfUuzVnkOVCzG7NVmfA/jIu8aOfFZd/DSZz+8xMshC23haLCVUgFLULIiU2VhgpbLL4a0cCpqYd" +
                "E2R/uWrH/9raKo9st5zUPeN6XIJnX3LoQUMGaIbzCSnB5L2zD5ZtyIPsjx+ZiBr5M76mnZcD06DL03gi" +
                "e3cFE5VQGIvMP1CMqwR60An3RxQG68yXmh9JWT3jUjIZfJ82T0oMLD7RXyZPXlzEx/QiuhMQsw45oDTc" +
                "+6ZU8E/gKiJH9Be6V9GF3QDdl96Ij8YWY+ZX4Z8PMPFIqY8ekFmk3vQ7LlBQoyBcetm5/8CXeql1cKB1" +
                "kK3xB1YhdzS/ZxVitByiiiIYIlpN5Y8l4no4FbDVspMdGzyDB+T9tdgiA7hZ5g/zu+r1118+0S8f+spg" +
                "PyAjFT0Y/2vpf839L3Nwd4NMODYhu0bo3qUaFAgbvBfzOjJUSabGd+wETyEMgScRj0bSRUiAH34AKUjB" +
                "Q/n44c4pv2NwioE+viA7Gz1IMM74JgxQOxQTgfaVtf2bovbT2Y7aDebK5AYytpMGkn4StVeocjhDAGKF" +
                "9tACdFWHRNr/GFnkK1A1HEbS8T86xF3HdNoOD+c8dGnabkD/nqAaIWeP3pgy3SkqxtN583CKlkJe2BN2" +
                "DhgQjvG0MBhfD2ao08SodA8iZESnNPS/vkChgfWJrzfB7Cj+1xmkWwnucB0FDLQbApFl1GDsgJT9zadO" +
                "uStfPcy32tH5t+mKr3AxHGPZVnmjhFNj1ONz1O7AL6PRfwNpVNSZeGoAAA==";
                
    }
}
