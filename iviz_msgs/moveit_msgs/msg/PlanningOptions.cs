/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlanningOptions : IDeserializable<PlanningOptions>, IMessage
    {
        // The diff to consider for the planning scene (optional)
        [DataMember (Name = "planning_scene_diff")] public PlanningScene PlanningSceneDiff;
        // If this flag is set to true, the action
        // returns an executable plan in the response but does not attempt execution  
        [DataMember (Name = "plan_only")] public bool PlanOnly;
        // If this flag is set to true, the action of planning &
        // executing is allowed to look around  (move sensors) if
        // it seems that not enough information is available about
        // the environment
        [DataMember (Name = "look_around")] public bool LookAround;
        // If this value is positive, the action of planning & executing
        // is allowed to look around for a maximum number of attempts;
        // If the value is left as 0, the default value is used, as set
        // with dynamic_reconfigure
        [DataMember (Name = "look_around_attempts")] public int LookAroundAttempts;
        // If set and if look_around is true, this value is used as
        // the maximum cost allowed for a path to be considered executable.
        // If the cost of a path is higher than this value, more sensing or 
        // a new plan needed. If left as 0.0 but look_around is true, then 
        // the default value set via dynamic_reconfigure is used
        [DataMember (Name = "max_safe_execution_cost")] public double MaxSafeExecutionCost;
        // If the plan becomes invalidated during execution, it is possible to have
        // that plan recomputed and execution restarted. This flag enables this
        // functionality 
        [DataMember (Name = "replan")] public bool Replan;
        // The maximum number of replanning attempts 
        [DataMember (Name = "replan_attempts")] public int ReplanAttempts;
        // The amount of time to wait in between replanning attempts (in seconds)
        [DataMember (Name = "replan_delay")] public double ReplanDelay;
    
        /// Constructor for empty message.
        public PlanningOptions()
        {
            PlanningSceneDiff = new PlanningScene();
        }
        
        /// Explicit constructor.
        public PlanningOptions(PlanningScene PlanningSceneDiff, bool PlanOnly, bool LookAround, int LookAroundAttempts, double MaxSafeExecutionCost, bool Replan, int ReplanAttempts, double ReplanDelay)
        {
            this.PlanningSceneDiff = PlanningSceneDiff;
            this.PlanOnly = PlanOnly;
            this.LookAround = LookAround;
            this.LookAroundAttempts = LookAroundAttempts;
            this.MaxSafeExecutionCost = MaxSafeExecutionCost;
            this.Replan = Replan;
            this.ReplanAttempts = ReplanAttempts;
            this.ReplanDelay = ReplanDelay;
        }
        
        /// Constructor with buffer.
        internal PlanningOptions(ref Buffer b)
        {
            PlanningSceneDiff = new PlanningScene(ref b);
            PlanOnly = b.Deserialize<bool>();
            LookAround = b.Deserialize<bool>();
            LookAroundAttempts = b.Deserialize<int>();
            MaxSafeExecutionCost = b.Deserialize<double>();
            Replan = b.Deserialize<bool>();
            ReplanAttempts = b.Deserialize<int>();
            ReplanDelay = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlanningOptions(ref b);
        
        PlanningOptions IDeserializable<PlanningOptions>.RosDeserialize(ref Buffer b) => new PlanningOptions(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            PlanningSceneDiff.RosSerialize(ref b);
            b.Serialize(PlanOnly);
            b.Serialize(LookAround);
            b.Serialize(LookAroundAttempts);
            b.Serialize(MaxSafeExecutionCost);
            b.Serialize(Replan);
            b.Serialize(ReplanAttempts);
            b.Serialize(ReplanDelay);
        }
        
        public void RosValidate()
        {
            if (PlanningSceneDiff is null) throw new System.NullReferenceException(nameof(PlanningSceneDiff));
            PlanningSceneDiff.RosValidate();
        }
    
        public int RosMessageLength => 27 + PlanningSceneDiff.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningOptions";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3934e50ede2ecea03e532aade900ab50";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1b+2/cRpL+ff6KRgScpM1k5FjeRW52vYBtybGD+LGSdvMwjAGH7JnhimRP+NBosrj/" +
                "/b6vqpvkPGQnh5MWB5wT2EOyu6qrut7VPTgwVwtrknQ2M7UzsSuqNLGlmbnS1PiwzKKiSIu5qWJbWHPk" +
                "lnXqiig7Hrz3Xy7lQxg3kXETwhsMDsxrQF2klZll0dzg38rWRFOXjR0K/CgmPIwsbd2URWWiwthbGzd1" +
                "NM0UrEkLGVraaonlWTNtapM4W5nC1Saqa5svaz8JsIwZTJ3LZOrEFdn6d6zDuFlH8X9gnodayKQoy9zK" +
                "JpyYOXdtotI1RWLMUe5uLEAWlSurY5POMDGt8cLmFYBHtSzUFq6ZL0AMWJtHgo0wb6I0E1KjqWtqzORq" +
                "bHGTlq7IbVErMcQ3UXx9cm6irLEEs3RVWqc3nyCmI4Wru5MY7ntk8ug2zZvcFE0+hTAAkGdz9eeA3XbI" +
                "MzvDPlTmkWJP7Cxqsrr73lQ2GXIAuI7pq7RemGRdRHkaT0oLkZul86a0g7SoTx/3SZ0ErJ5m7lqERaaz" +
                "/iiiCBvZ5wnRAqtnaSApdlXdEq/ULiMsCHyY2lb+8a2TwlFHs8wmO3QSsCzS+QIswjYXPfxDk7tSZYLc" +
                "BxrAiExhVyrShbWJTUYE23Jv9Egk+w7KbEEQu/wlT27SaB9DAxMGs8xF9Z+ekAeTKprZSassExLUSpTX" +
                "tylA5FCvtACKNIlqcCNpShLSThxSwlXwqpTiC/4tohsra4TECyCuJV82nM9t61QUqlxHJd6PYH2CWtqC" +
                "zKbGpNy0WVOIGGMJ9drrdGkJd+CN1q6U6ncR+CA6xouVftqQKMKIcjBatrROc6FiFZEycqFeWXB9H8wj" +
                "fK/I6KQ6brnrMSQ2i2Bznv4v/xm8ufx2DKm6sWk9yat5dbJhgEEO9t9u6LyY4kFVy87xK4meNRn46Kaw" +
                "SNiD2g4u+PuSP/X1RF97/gSgYoBlVu5AoYq6+gTunysDHoUhgyYeZ5Qkqe6kmZV4JeMhUcssjWEIMame" +
                "mSMxC7TxNlbb3Pc+Mu94MLcQzLpcKwOuyqioaE2x+nxpkw8fzSy9tclERk/q8JmbLWQHtY9dlqUVBRGG" +
                "uExvB8/0w4vw/o28DuOhJP79xI+nMgNelhbXMAQgr5hXg+/x9F4fsBJ+m/hvG+OrOIKUy+hL/gxj5T1H" +
                "PqvrKF5gmW76T/CiGvbW276yNxBNssjFtcujJU1Mn6yliaGA1EiaDXyBa4KVqVycij4rg3Ovf1FZRmtD" +
                "1qezVFQQnwbvBBmY4kosUlFPFBTXqfawh3IzJPjBlVliVvybo1+K5y2SsOWrha3VcPYFSa0wNNaWS0QE" +
                "YsBhNyU82RaQCrJgnABRSRcTkVY++Lh/Bew0R5QFy4dsV9Fc3EgdpYhm+t5eHHxPkUTPYEdHdqTCLj4c" +
                "Doj6lsLK/NOBEYyIEpGQaqAhhiL/jh9VbWVcp7byxQceObZVhCCHx0iXsNNn715iu2272YkBA+0G6Dcc" +
                "i3E9FDJ9krjZZAtZK6w7MmqOovAtbJbIP0a0PDgehPmt6qnQQd7C7J72ecgPJE5+T7mtcJLZ2qT50pV1" +
                "BHdB+wWHn2RivOhGAqlTl1CBjsJ6MJDBH0PrzEblvsFAJBGFitZ4HCNyGI97Znlqgc+aZglXLKForYuv" +
                "eyJ3/CDSv18Ae5yKWhUQ8Vu4LKkMlh2RA4mt4jLFhpAJIkFKOEMYuhhX/tKo7pTYcvBHFWAEC+a9UTvJ" +
                "goH62RyV9sZlCDLI62WZVlS3GGE4ENtZWpDP6zEAmD9sqFnwagFKBINbmvx42A29sRnsJaKPnaEnlQw+" +
                "QbhP9eym2Bn2qlbqyY8lnBxW4FMYD+BtztlvjxFYYuZ5RwsDtiIFFyhtCaJ/1dApnqBQ4k6VEQsbMUvb" +
                "sNgawQDvapECYodPuFaZFfRcQrJSQk/4vJ0xTA3AGChP2EYxHqpGYSpRyA4yNCBOJWPbAFZphTWrZAfj" +
                "I34G3osYqEOyjUtEgpsclsVw24XWuUPgoMqcR9dWJ/nxoJ0SFrLSkfmBgbIdzUdm7ZoymFChonBM6HR/" +
                "ep7Qq5LNh5wibhOaSqq77VT/yNhv7aWR3FNqdG97tFcL18DzKecCn6r0V9h6kAxGKpye1ogfR6aK2HNN" +
                "MlsZaJfZY45EZGHRYHSJfWFapTs4GgwGr1Q4VEYGPjCDRRX58dEqHoMi9F4Fge+9Ug7cm0GpE7UmumaQ" +
                "AYtSJFGZgJ11JJZDjK1kWV9lCHsyUspwz9uV9ZJUd8ycw3aXiLbWmgBKUSPPwVJ4CpXXjfkq8sznSpiN" +
                "JoONjh3kPC04XCJJQieDLTaliK15fTYWAZdsBguCdhZxaSNJ9V6fmUGjKQcmDA6uVu4rOqA5nVNA3toH" +
                "ewu/VGmiShv1ByVuBNjjkGGYI3k3wSPMDZBgCXbpoARMQ96v64V3qTdRmUopAYARTNKCHnLS4XEPMpc9" +
                "higULoBXiB2O3wK2aOGSpq9aX1g1czCQeWHpbmDBxHaJnMIMQnizdFpG5XogpkpQDg5ekseqQrIjNJub" +
                "6hlyC43r0+TepPGTQRCIvbDcLhCiER2MkHokiqg3NaKZrcFL7Ly0sL0YOcOPxMHKMAmDf3OrEDyAuiZG" +
                "9Us8W4dPzepr9SNV1eSUZspNFLwF5bZaV0hK1Q6wPoYNhchDL0L2o3Pmtu68kaQjzmO/hosUl2niBWLW" +
                "kXlJw3yLrclgsaAYCOqxp951IX4C2r9fnL0UD3vKcPLoFqYT/0crCgT9IWQHdTr5SHtKm9cT9P7qlJGa" +
                "Vg39XPqXje+AqiMCNAjujWX9wEyj+JoEb6zh/53qwzrVVQm7uPjNTjUM/7/kVO/yqZoOcXp1V3UCo3ql" +
                "iK1BK0RJHMB/t779IGzCR+XXfRm9O1YdOFkGk+djzNashPpYvXI7HlP2jwYPEWoUw5IN/gF+uvJU52eq" +
                "1X9rMKEspCDo1KQ+DJF+MXtIjBAC8dvW+k1riEWicsskEDLVzpSkkjIDGlhTQajNnE2qpKFVIVYMqkYv" +
                "IzE1zTHEsc8TvsaUIyrbkEltoaOk+shVSHQDW12m8zTZNqNi+D1xqBnPHkOkoVKyZkWGLWSvxXP7WArQ" +
                "VNAVCRLlDska0zS/LnH+tXNDRlQexCZD34sSBV1F4aOGnmDXQ2n0tv3Vhpbm1wfZ6k7G9u02XHjJuMS3" +
                "Y/p7zqdfOgElkz9LUPi1eiBdFaPhyQoOtuqy1k16pqW7hjhhoyhi6NrA78Ov0+VGxVwCXzoNGLugq35I" +
                "9+zHPQx1av727Bq2QrenI24IpcLixfWQQGnA/SYSBVj3qDWIhyge3lH48u55663q8bRXOhJfFWm92/sw" +
                "KemyHYcqW4iYpbLsy/DCx7Yy6fuEDcKAahEhhxI2ISf05Xl+31mb2oV+XY/DBOEBLInvO8p2SU+UIAvn" +
                "K4IjGCxtvSLQo1ny1byDffBCbU8ziJaMlhGKIBlsM0qBhu6FryxJ9dQHlv36W6hUsh7a64fWrhHBA+LQ" +
                "a/uqXZguQxqoSPmSdb9l2E3oAgQBNtH6rfbZYkSu2AK/KqnKIMaOtUq0kYhiNyQt0P2g91EixfPkhNGS" +
                "AtDeabCBzmbbbKaeLM6Q59N9aEuRNk+gHGu3NuAobEzzXa4FW2kRrnGar/N7xNw+wAxFYzgIvnZBZSV3" +
                "uWpfghlc3cSjCJuyskjm2+B0azPgMWfmunCrtizhxz+ETu7q4jMf8Ynr035xW00O6ZvozHaAqKRC4j2Z" +
                "noFHIj0CC7v3BrhfowjulZWJrc4L+4zahgoFvfI0gvd1njut9vi2DItz80LqOUqLknBFCAQTIHdlem9t" +
                "WTzcE7gHndVpqcgiR0JS9jXq+t2EwIBLh+ZxiyZmbTaXQwpoZNDi6Dpl1PvwiWWpbth2kFFtfKdcYRAw" +
                "vbHVYhMq32Bsrh/2wuG3DsRzKgc3gekuC/go9rSHNFrqhtKhD51JG9JRDSJAfKOahh3rWp5EcdxfG7tk" +
                "QohguoNIfutW9yxhWt8TjK32qDQsJJjrDYKM3qSuqRD22VtECly+Nu2lUw2DMxpM1wjZn52dPX1ENBdi" +
                "VDcwzUqXbx9Jwe8awS7gHqEeV6JSpKog/aYaylxtyUSaHCumi/M37/5x/vRroWm5pJ1izBqk2Zc3vGGV" +
                "RftM8HO0tseBZFKgE7vQEfn+/fnbs6ePvRHucO5HJ1iGemBDJN9vtQT7RxwR9i1krHmDoyEYIec5JBtl" +
                "NQTWrHIZeQXWBovRWVO0RMDJRJcovDnlAt8tUcIMIT1g4pEBaBjowuf7MoqfNyqDg9/9x7x7/t35iytW" +
                "SH//ZP+HzHnx6eaqGE1pO8zE4XlDBjPGmhSzVUQFUpFjxIgttOiM126u/bq2SqC9I8gJ23MbQcW1bRtC" +
                "fQxjeaPzu+aNZJIiLrBYhUmmwdgDSgCYTPtL8Q5WymTfXb57e8IzNL529tOzN98bBYAGTivCMLOtAvRS" +
                "TBrqwJWuPqhOPTiUkTmXqIFtn51NFz2S2g1PiGXptR2bL/51SA4fjg9fMLI5e344NIelczXeLOp6OT45" +
                "QfoRZeB2ffhfXyiJehYJkaAU7opwGER2z0c33JweF+SYV32ISSmCfWjBtbW+bD7LoKrTlMeCvHvaJ6/s" +
                "oioTQ8p89lxlQ4CQKuq9x6wlLwpXAz7RxGkBVIryLIh6YqWPKGDGpmWAvCML8G6bBeM//uc3T3QEXa9W" +
                "CDBud8WHHtPl375H3xQhApun7T5tIL78JXsVRihsQWUOV/Pq9E/6hq3qsfnjk9PH8ojRJQcgfHYrPwJu" +
                "H8czkq3XjFBISEAQuu76FQd7mozfpSpQu+VhEGiI9n2V5e+KFrCiM1XTqUP9t1pS0nBcZo3QWoI2iJuc" +
                "f2FhMWQ5EIvQF/Zn1qggiHCmIQQAMBp8unTRRA2cHw3xH0oAbO58Y56/+xFuTH9fvn91fnEO16KPL376" +
                "/vXbs/MLmHL/4t3b86dPgrYH+yRehmvyozRKCyYB/RGkFf4wSDe068t1I9rjcKhKyWHD3oTesLHWeJmu" +
                "yBkGZYK6arLrNliqw27OoTo3OXDhc0IQLkvV7OHHoflJy/Y/99dMJkvCZIs5gkW/om0bxLSppQ9MH3W8" +
                "nfyIiKR7+qnlNZ9+phfvLUn571clxS5uO80m/vVddT2CKkZFTLHSjU5+2nAJPs1RCQrrULiTi2dnr/9+" +
                "yQiphzNsssDkBmsjUrmioiPlB6kZhvBQmi4e1c8GpxdxtqErFm7Anbw6f/3tqytzRNj+4bijSc+M9Dje" +
                "0bTYSK+CLpgj6sKx4qOdC3iUOo9HH3p47sLCImLgnW6fT07244TL1mJA+IT5XZy/rZNs8KSlpMBSS0WP" +
                "cdnJkPCU85lsUt6b5dC3s770TA1KusXMVqS2iGc82mnqzuCOMRx4PyZuNwmQ5LPc6TcyGN2ufcluMTzQ" +
                "73q6hdzuFTdRwtYibdv171Xfe+MeikAsJZT2NkpS/dM5qIWEPmdH7mdKsPfvgphaBsfTWyrTSRoIZNeq" +
                "fGiaF3NEEH/uWVg5ss38a8bTAK535A80sqOJYKf68HFAHFcegLSPPKxw7NIX7sKMkHsh8msYJMnxncVO" +
                "ZgleyhkPnfRArApk7GFZIAtRXrsoPTrx4VTXaW8nUgd8kNVKXr63ya9NcGia5vdd/t8WCaJb8yXLf1+a" +
                "+Ff8lZinRjLqyIyfQsDt7MOjj6woto9f8zFuHx/zMWkfTz+2rYYPTz7Ku/tiwGdqeFt1rb19z60pQdBE" +
                "eat/07qDhZHDdd1Yb1G6c3Oo/THtaxXxwzD0T/AVD1EcoxSq2Xb1kbvkNkfr+SicPPeBQQ+XujK99cA6" +
                "hI9D27KItwb0fqHqIKftccgOTYtqqz0tNmKLyhFIH+w50lXtnuniQdPu5QZZu6e9kiaUH+D7J6wB8cTv" +
                "/R0D+/S5/t0WV/94i1SothvQXl5H7Ymhg1Az2xkZL1IkOd3A7aOM3flfKdv680wY9ZfILGDJn37hc75V" +
                "ep2OSleNXDk/qWdf/LWe/eUk+ivyrPgagKSJfAk3xs5U4mKc5wkmhrIj94V6FY2dMwc+zdpcrlF/oJR0" +
                "1WYZpG8HLTc7nj1If2vvlQqfqgQnCQ5AT1rp1ntaMq41NDLEG5pQVYdLwOEyekB+TbvJd17wQLIBU83k" +
                "sFrnWo8eaiF7U+X62LYpOOe3dkWqvFJN7Mq9nZBt4uee22wGlFxj5dNO78xDj0mZobe62sOxHYdGvWta" +
                "u3SGi40oPoEh3c025LXgs3L4qPgaFc3HcmrPnz9kxUiLWJ6Dd65cTwJybW22tIu91wvzRwiIxwulkoJF" +
                "yKF4WUavRKzoRTCmsFgrqfBOHXYIE6SP+Fgs5cZqZbjPhZSLPgODnSBLZUn+Bpp2TAvz7O1Zv4DaCpoH" +
                "MOmLAM/U73wKO//wOiQSyPNWm5cLun1A7T3GeT7ZC71OlwQawuMDLLt3EWpwIA2v4Bzvak2HS1TdqZh+" +
                "azlE9+1tqochQW5n/VYCeHvrswT4K173v/zeza3NjiJcBEnRGwIwRqqAbGJz6E4fMih8UHcZ409sC/SL" +
                "b58/8x/u+4R4i0/ZySud7a95+2va/ooe/A6m3HhjWL99FWr7lALUce+lpqvefT6xnP2zSl1HsYPP6vDA" +
                "z/A7rw8/wNixihDA3Vuj6BO4pYJxeiaXEXmfFkGXni6AZ5Hr4BiPtH5/51zqHKF3LuP29p39PRiNf/a0" +
                "UlnamKftjWGfN3uADJ73EfDvYNr/mFlyEU4uV8EVgMIw9UgKoaybnLg4bpZwssd0GHIRVt5ERYxKqbLi" +
                "aDStT0YMBtIs3CVTQNJxy9Aq6IeXWrFhxUmnb1qOC4uWp1705lWCHHh9STrn9QGGfjqtwC3hNvvg1vpp" +
                "BOLJQNsihWX9tc3FdaoeRdRj4FKaHMkNfunoMFhYlSn7pDK24g3wb+jGqS2D/wYYeyzy8kIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
