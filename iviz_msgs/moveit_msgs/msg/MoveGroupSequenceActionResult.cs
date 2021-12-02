/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupSequenceActionResult : IDeserializable<MoveGroupSequenceActionResult>, IActionResult<MoveGroupSequenceResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public MoveGroupSequenceResult Result { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupSequenceActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new MoveGroupSequenceResult();
        }
        
        /// Explicit constructor.
        public MoveGroupSequenceActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupSequenceResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal MoveGroupSequenceActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new MoveGroupSequenceResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupSequenceActionResult(ref b);
        
        MoveGroupSequenceActionResult IDeserializable<MoveGroupSequenceActionResult>.RosDeserialize(ref Buffer b) => new MoveGroupSequenceActionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e995e5b2d6c322a0395df341caa51d8e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRrb+rl9BrIFre2sriZ2krS/8QbHlRK0tuZKSbRoEAiWOJK4pUiUpy+pi//t9" +
                "nnNmSOrFTRZ348UFrrcbW+TMmfP+NkfvjB+Y1JvKr5o/ysMkjsLhYJZNsmdvEz/q5X6+yLxMftVuknvz" +
                "Nk0W8575fWHikemabBHlXiq/auf/5p/aTe/tGc4OFJ93iuWeB6TiwE8Db2ZyP/Bz3xsnICKcTE16HJl7" +
                "ExHh2dwEnrzNV3OT1bGxPw0zD/9NTGxSP4pW3iLDojzxRslstojDkZ8bLw9nZm0/doax53tzP83D0SLy" +
                "U6xP0iCMuXyc+jND6Pgvs3zxWpdnWBNnZrTIQyC0AoRRavwsjCd46dUWYZyfnnBDba+/TI7x0UwgiuJw" +
                "L5/6OZE1D3Pwl3j62RnO+KsSVwdsMMfglCDzDuTZAB+zQw+HAAUzT0ZT7wCY367yaRIDoPHu/TT0h5Eh" +
                "4BE4AKj73LR/WIFMtM+82I8TB14hlmd8Ddi4gEuajqeQWUTqs8UEDMTCeZrchwGWDlcCZBSFJs496F/q" +
                "p6sad+mRtb0r8hiLsEskgt9+liWjEAIIvGWYT2tZnhK6SGMQBrVvpI2P2kiNf0KyE/zi+RTwD85w9MNt" +
                "s33Zar/13M+59xz/Ui2NbPOmfuatTE6FHBryZ6SCtwzSsyHz9B52oDAbF/3Wh6ZXgfliHSYlskhTcBZK" +
                "ODTk0VcBvu02mze3/eZlAfhkHXBqRgaqDbWEyKEefALtz3LPH+fQ5DAn9SkFZB7EDuJJrUR0+2cP/4eS" +
                "CBdU4WCV88gQQphnDgoQPeibdAbri+gKcnNoUe69v7hoNi8rKJ+uo7wEZH80DeEiAujhiFwYL+gHdjHi" +
                "sWMabzrdki885uWOY4aJkB4sRC1L3HeeFCzMF1lDrcgSmMHYD6NFah5Dr9v8qXlRwe/ce7WNXmr+bkbE" +
                "byc6NKhkkW+qy9GXcRyakQ+fKjCLwxbwk7kPTOkh4KnD+N6PwuAxAqzmFZZy7r1+As0rVC9OcjHCUvkK" +
                "4RUcvmhcX5eWfO59/7UIDg1CldmJ4ddwFzLZltY60vE4TGcMagwfhRjELxMTE6wRUVWTH/4NRHwdm6kU" +
                "a+anBzBsPKIT151evwrq3PtRADZixwwbPQDJCyA1AjHKBL9gAaHUNQvIoOBRIHwbfoXtZYSN7AcBGvxZ" +
                "hiAfloOz1l1nba8RRclS8hEuhCngj6QMVkDGBiramFdJsbglMMPFZEI22kW5echrTxjKWpeSJdm465iU" +
                "5RQ36ZGYDJYupyFyC4nHFZci2mEC5kItSV0ku9rBJ+w3MfUHVJqMDEKKY2ZzyCqKsJswMxXe0uDoArRT" +
                "PaikSelSBKNqqmDxh3ex6QVcMdBbrUthbEww9Ed31Ebs0PwV6WSW+ROjosnmZhSOw5EzBsEgq1vozPV0" +
                "AZCaLcQo4OdCrKo74TEJ+Uaim0EVw1zl9khSjrNvEvK88njOjJTUyh9Pg9wuFGpiuCZNofOjJCBK4wjx" +
                "SOxCpQ5zW6YJLIHktfIm115gKdIA/jngNids+hB1RZJf0m94yVgUNk2GsHDrCWWJe+NS9VqXS8TZFM8G" +
                "stKBz1OfwTJRXXVudR75MaoIqlmwGEELaMBlpBCofbdz9emz3RAMquDcEf4sWcSKGlWZlpEkd7YwkSgk" +
                "Z86EmwKpNo4SP3/9UsGC7oFYwhPpW1UgNa1kENJJDX0kjBjmMPXvwyS1byU16/XOX9jPV43W9ftu8/xH" +
                "/tTsw9vrRruNsDrg2+bl+bFb3Wp/aFy3Lgc3nX6r0x5w3fnxiX1ZeTiwCxtIfwZvPg6a7Q+tbqd902z3" +
                "BxfvGu23zfPjU7vtotPudzvXxVkv7fP37cab6+ag3xk0fnnf6jYHvWa71+kOALRxfvzKruq3bnBE533/" +
                "/Pi1w94lzOfH35MTTjDef3l3iEUzH7Vj4SVVix3vev1Gtz/Av/0mSBhcdJBd9EAUOPB8x5IPrc41fvcG" +
                "t43+O6xu9/rdRqvd72H9C8fMt53G9Sawk+q7P4NyWl1YeeU2UTYvaxvSedvtvL8dtBs34PKLV5svNyBh" +
                "yeuNJd3Om44lEW+/33iLfOtnB/yHjXedN0x53VvoE7zzCvFits7mqy4WDIBAu3fV6d4MnBIenzhFK5gF" +
                "dWle/ExdhD58wDoqBRY6DlZw5b/yzjHNKkyrfdUp3oFZe1U1WMOr3Rm0fh70OtfvqclQ0RdPYcel33Od" +
                "ERf/kEQytqJVEsOpEWM4HRQ0NvtUnypu9sgL66au/jDJQi7M6MVYrv09AXWZ5Huo+++yGnKoDJ5bDv+J" +
                "L9Xpyjp6XCACTOSN9bMzxG24ETg+BLQQftC77Fx5PhKAMvii52PWQN9wLdZVjpDtgyAZDzYOa6AuGU0B" +
                "ZZREUZiRzmRI74wOh+/eubqLVHi24SE8OKy5/Rdue0d2w9u73QhU9tXAQua5V5GP/CAO2HDSoGcAlTkj" +
                "U9MR2lNMn7QNwM5QivyI6Q8LSy8Ix2PNURjJAbLAMBEgsr/S7ZolGWv/cDZHSeojzEi3zLVjpJ5wpA6T" +
                "gEHuwOGDhcyQGYcig67NjsX0+WNgpap1djZCfnZ2VgmqNmVbzNG5kbQoV+Tzisod1oZJwhR9QOK+lfbv" +
                "VsAKp/zCBET9pkmE3pe2EBMk59koDSEQm0pokoF83mjgTlJ4drGdFCIHf9QA6ugLlkm1bjJgoL72DlJz" +
                "n0QLPmcuEWbiIA6JTWDGcBhMoNn2Q3euamYujXFQ/IAAZodH5VK0HZHu5qvtpc8yWfwMvUKaZ7nFjCGr" +
                "vGg9+vM5qip0X+IqgPaMu9uHyPOxs1nSwoxfUmBqW4AUTi0UBQD9QYzGnGWE9psLI8521hbuPOFa5goB" +
                "qBdahnUPddbWGu3TJjAeJ0ZxHmpGbmuRDEpHEWcqGZsOMAuR7VvNds7HT1N/lR3JCZLtkXQ2hdc5LMhQ" +
                "7GtNOGAx8++MbrLrQTs1LJlTon5U9/7GusjUJ3VvlSxS50KFijgBQCufjd4nYc2OuEXKX1gqqS7FKXh7" +
                "rK9WVhvJPaVGZVuh3dZ8yjnHpyz8A74eJIORCqdiNVyFOnLlyqxCB8p6q2QOtaBAGoxGZ3KUM16LBOuo" +
                "WtbvJGwxBY8q+mOzXqbT1hAqj5zCVx4pB57CoWyHHVDVNezdwxI0hkLs6gOoPVa4wotCxQIzQSUsijfG" +
                "H0ECuQLOGB4lWTp3DZYsRjlbgFhWnqeKrJU3WL8oukC+s09eYtjESDjPagz6x9KYVU6cMdzrnonJS/sH" +
                "WD9K7OlFFuONpsgS6t6VFD4oviPoiC95L0KFdRaIWDj2fffySnzaKQP4wQOUFf/5S94O0AOh3YjCVF5S" +
                "g6lllVuPKnbKSPxKQ0DRvbTotfeAqiscNBg06hJpILDqB8FrOPy/G3taN7bEdQS2f60bc8v/L7mxx7yY" +
                "JqDcntUmBklbnq7UgfSdCmNVoc5bi5aIS1zA3xvv/iZswkvl17dyeo9g7TiZOpdno3rhVoYmXxroRb5M" +
                "tq5PRX50eMgJ/BE8We2DtEhOdX+kVv3LAhvSmA4gTdSlPg2RFpkdJPoIOny3gb9XOGLRqJlh2g2dKnZK" +
                "Gk+dAQ3SU0wlS0Y9lXtBAn4g7RYvBlNjlJEshu4Y6ljlCR9jywGN7UjbqrKKoUIuruWqG746DSfoiW64" +
                "UXH8lrgjLx+fQKVhUoKzHgYRsj9quX1Y91pjMdAlCRLjdukxE2OLl9zzoIF1xAawBbHO0FsxImerKDVz" +
                "2Amk7ppaD8VfRTD3/ngSUZc6tkvaCOFo27lwviZzfvq9VFAy+YsEub+WT2Sr4jQsWS7AZmWdsE7PME3u" +
                "2KWPRcUylr8sARly/XgiUxAMGnB2zlbtkvKzXfc01Kn72yE1iELFUxJ3BKMC8hJ6SCArh68jUYCVH7Xq" +
                "e4p2zSOtBhueN56qHQ8rxbrEKgzKhA+uMqXRSshkX8PdWfBviU7oUggfi16QvXdcIA3Ipj4GaoRNyMLx" +
                "l7vu2sJN/UK1k8JlcuAePIm9FBJxkVYBGSe2B1OHw2I7xORI9OiWbP9kbxc8103RerMgo2CEHhDUNhml" +
                "QF0f3tby0q8qW/3Fea43xA4UMEqWFodkIYqHg1Gz+7xKOi4QUzSYkkeY/wlWWlwiKVBM7YYyQRBgA+2Y" +
                "MYqMi6suxUrqYOTYvHLYnEqCNKQsUHkw+iiREnlmhFGQAtA2aMCpGbAdlZJGslGEyorhA3dKYggWyuGR" +
                "a0DIGTGuh+G+05WcluJmWYabOO7DREoPpvh4eWjbdMXVhzVZqV3KSxJe3AK7gT3CCWVpMNlVJKcbwkDE" +
                "HHt3cbIsb0R0/VPY5LYtNmzGJ6FP74SK/p0r38RmNhPE4trSkmkZeCDaI7AgPb16OaxeMOo+J2cMuqlS" +
                "MCoPfUTfxHKnsB79PWA7ZBJLBa20KAl9QiAYB7lsjFpvy3bNjsTd2axuC0UXuRKastmqlHS80r91DOgl" +
                "HApxx4zYDZsBwXswSzyO4imrbt0rNgLKZZtJRrb2nnqlV243JpuuQ+UTrJ3pi51w+K4E8YbGIdMxKHfZ" +
                "MsW9js0KspK6o2JkQ5a5clSTCBC/UEuDxIJAZAHvyiMOq7jdcqu9QHyUSL4rsWsELOsriqFcL66kpEUs" +
                "yVxlEXQUN3aLDGmfeUCmQPSRW2owFYdTrw1XSNkbl5fnz3lMV5zq2knjNGEHAfVVfB/iAnfGZJetQHiI" +
                "FbiEMhxjg2oK0uHPYczZhk6EwaGe1G3edD40cWtImuZz+inmrE6bbXvDOlZB2laCX6LV5di6ydEJKZRE" +
                "3nJc7/zEOuHyzN3HySlH8IpLq/lW1JLsH8gcjpWbq1jd1EBkxrlWo+yGwJtlSURegbXOY5TeFE1ocDJQ" +
                "FIU3p0SwM8etq0vpOTpoUiagbmHiXn8rp/hlp1Lb+5d/PL3W47jsv77Z/pA5F39+nSVOUxq9Ywl41pHB" +
                "jbEnxWoVWYF05JgxQoQmZRtkojckRZdAu/XQE16IrCUVd6ZowVdPONMJIdlftsulkhR1gcfCKNPQOXtA" +
                "cQCDYRUVG2ClTfZTr9N+xpEB2zv72Li5tgNHaJkXKgw3WxhApcR0Y39FS0P6gxrUXUCpe03JGtho3xK6" +
                "2JH0bji9EIV35sz7yz/2yeH9s/0LZjaXb/aPvP00SXI8meb5/OzZM5QffgRu5/v//IuSyOkyoqeNu9h6" +
                "RpWezW4onAoXmDmG+T42hUj2YQV3xtgZaoyWPITDMEKJY8PTLn3lvZUy0ZXMl29UNwQIqaLd25O15UXl" +
                "0tla1wCVCW02RC2xcnMjYM68ggHyjCzAs00WnL368YeXuoKhVzsEWLeN8b49qffLNW6qkCLwuqqQ09rB" +
                "vd+jd26FwpajvP3lJDt9rU94OXjmvXp5eiIfObnGBUifk6VdgbC/RN9m4zEzFBLiDnD3nPp2humYiO+l" +
                "K5An832n0FDtb9WWfyxbAEaXaqbDBP3fbE5NO/JGK6TWkrRB3YxnG4uuyoFauJs4qJVrKCLDGboUAMDo" +
                "8BnSxRI1cX5+hP+hBaADjG86vyKM2Tnl23dNjB2c2I8XHzHbcNnswpXbB5128/xlMX5k/ZNEGeJkV2mW" +
                "5lwCLrJQVtjr93JpeRNSrnB72JUi+tUNlWVn2uNluSK3xsoEDdVk14PzVPvlnn0NbnLFbWtCEC6oavXw" +
                "65H3Udv2v1VxJpOlYDLxBMmixWjTB7FsKugD0+slbwe/IiMpP30seM1PvzGKV1BS/luspNlFsdNt4re9" +
                "x+S4s3Uq4oqVbtydhpjVTMa2zFENcngo3EG3cdl632OGVDnTCVlgUsD6rRTliqqOtB+kZ+jSQ7l0sUf9" +
                "5vlIOOpe2Sxcgzt412y9fdf3DgjbfjgsadJb+grHS5qma+WVswXvgLZwqOfRz7lzlDp7jn6onPPYKWwi" +
                "Ot6p+GxxsvtMhGxtBrhXnKcq8vxNm+QFT5hKCazzmbhLK3VIeMr9LDap74v5kb3O+s4y1RnpBjMLldog" +
                "nvloaalbi0vGcOG3cXHbRYAUn+nWfSOT0c3el0iL6YG+13kCcrvS3EQLW5u0xT1rpfteWfdUBAIV19pb" +
                "a0lV5yHQC3H3nCW5X2jBfvsQxNLSBZ4Kqiwn6SBQXavxpSF6jsgg/rviYTFUiWlBjsfqHHk5ZAUaeaOJ" +
                "ZCf79LnGM/oWgFwfWVg8oNK4cztc7YXMb8EkSQYmpluVJXgpt+q66YlY5cjYwTJHFrK8Ain9Ht2nU8XT" +
                "PAykD/gk2EpdvvOSXy/BYWla35f1f9Ek8B+879j++84b/YF/An4XjMLyvbNzKLgZf3r+mR3F4uMLfhwV" +
                "H0/4MSg+nn4urho+vfwsz74VA77Qw9voa+2899zY4hRNjDf7D+HtPIyMM5VrrUcpJ5XQ+2PZVxjipyN3" +
                "f4K3+OCP+CUbrbazz5RSsr5aJ1I+Fz3zylkaynSGXL9GIXlo0Rax3oDRz3Ud2B3kWBMuLbKN62nxERtU" +
                "1kF6bccQTbY9RcPRvvLhGlnb8zXBwrUfEPsH7AG5EfonmmOtKOCXmsyqifnjG9YmeSobN6dIKyCeSGcf" +
                "wWx9qs5974ZDoaI69tu1G1fudnZGvnuBhJJft5QJvB166aS7o2m9K+WwE3PH4JS7aSphHejQnJsX3Jzs" +
                "OXQmqCvKqaMKCLnSCeNRtAhIhZ0LqeQy2bNSh5+ta+6eney01iL2Y4sO+6iAVrEkrQTWNqnRuWVsibNu" +
                "3LRAAYb0cqcbfESa/xl3+GfIOJlsipU9Eadi1QGyAyYVifea09qHXzflUnNtH9s25VyFvZUu/Z9TTaSf" +
                "+JLc+rTSI3MyFWe2dQS/lFbqxv/unHUt+xN/+D9MPv/yckEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
