/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceResult")]
    public sealed class PlaceResult : IDeserializable<PlaceResult>, IResult<PlaceActionResult>
    {
        // The result of the place attempt
        [DataMember (Name = "error_code")] public MoveItErrorCodes ErrorCode;
        // The full starting state of the robot at the start of the trajectory
        [DataMember (Name = "trajectory_start")] public RobotState TrajectoryStart;
        // The trajectory that moved group produced for execution
        [DataMember (Name = "trajectory_stages")] public RobotTrajectory[] TrajectoryStages;
        [DataMember (Name = "trajectory_descriptions")] public string[] TrajectoryDescriptions;
        // The successful place location, if any
        [DataMember (Name = "place_location")] public PlaceLocation PlaceLocation;
        // The amount of time in seconds it took to complete the plan
        [DataMember (Name = "planning_time")] public double PlanningTime;
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceResult()
        {
            ErrorCode = new MoveItErrorCodes();
            TrajectoryStart = new RobotState();
            TrajectoryStages = System.Array.Empty<RobotTrajectory>();
            TrajectoryDescriptions = System.Array.Empty<string>();
            PlaceLocation = new PlaceLocation();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceResult(MoveItErrorCodes ErrorCode, RobotState TrajectoryStart, RobotTrajectory[] TrajectoryStages, string[] TrajectoryDescriptions, PlaceLocation PlaceLocation, double PlanningTime)
        {
            this.ErrorCode = ErrorCode;
            this.TrajectoryStart = TrajectoryStart;
            this.TrajectoryStages = TrajectoryStages;
            this.TrajectoryDescriptions = TrajectoryDescriptions;
            this.PlaceLocation = PlaceLocation;
            this.PlanningTime = PlanningTime;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PlaceResult(ref Buffer b)
        {
            ErrorCode = new MoveItErrorCodes(ref b);
            TrajectoryStart = new RobotState(ref b);
            TrajectoryStages = b.DeserializeArray<RobotTrajectory>();
            for (int i = 0; i < TrajectoryStages.Length; i++)
            {
                TrajectoryStages[i] = new RobotTrajectory(ref b);
            }
            TrajectoryDescriptions = b.DeserializeStringArray();
            PlaceLocation = new PlaceLocation(ref b);
            PlanningTime = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceResult(ref b);
        }
        
        PlaceResult IDeserializable<PlaceResult>.RosDeserialize(ref Buffer b)
        {
            return new PlaceResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ErrorCode.RosSerialize(ref b);
            TrajectoryStart.RosSerialize(ref b);
            b.SerializeArray(TrajectoryStages, 0);
            b.SerializeArray(TrajectoryDescriptions, 0);
            PlaceLocation.RosSerialize(ref b);
            b.Serialize(PlanningTime);
        }
        
        public void RosValidate()
        {
            if (ErrorCode is null) throw new System.NullReferenceException(nameof(ErrorCode));
            ErrorCode.RosValidate();
            if (TrajectoryStart is null) throw new System.NullReferenceException(nameof(TrajectoryStart));
            TrajectoryStart.RosValidate();
            if (TrajectoryStages is null) throw new System.NullReferenceException(nameof(TrajectoryStages));
            for (int i = 0; i < TrajectoryStages.Length; i++)
            {
                if (TrajectoryStages[i] is null) throw new System.NullReferenceException($"{nameof(TrajectoryStages)}[{i}]");
                TrajectoryStages[i].RosValidate();
            }
            if (TrajectoryDescriptions is null) throw new System.NullReferenceException(nameof(TrajectoryDescriptions));
            for (int i = 0; i < TrajectoryDescriptions.Length; i++)
            {
                if (TrajectoryDescriptions[i] is null) throw new System.NullReferenceException($"{nameof(TrajectoryDescriptions)}[{i}]");
            }
            if (PlaceLocation is null) throw new System.NullReferenceException(nameof(PlaceLocation));
            PlaceLocation.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += TrajectoryStart.RosMessageLength;
                size += BuiltIns.GetArraySize(TrajectoryStages);
                size += BuiltIns.GetArraySize(TrajectoryDescriptions);
                size += PlaceLocation.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "94bc2148a619282cbe09156013d6c4c9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1b/2/bRrL/XUD+h8UZeLavttzYaa7nB/+g2Eqi1pZcSck1DQKCIlcyzxRXJSnL7sP7" +
                "399nZnaXFKU0PdyLigc8t4hJ7u7szux8n3GrdWMedK/s5rnJL02sC6XpMYjw3Gq1hmZiylEZllqVefhP" +
                "HZUmfwqKMsxLNzr23z9+akya6aLVKso8yWbrY9gnypNFmZgMM1q3aRjpaxOF9EEt6C1I7SuGp6kJy5cv" +
                "aCDLACsok7l+1rr4X/551roZvTlXcxAkKYN5MStOmsR51kqy8uxUPYRpq7WnMJqHaaom+i58SExuR0fv" +
                "Li+7o9HFc/v+utO7fjfsXvydflr24+11p9/v9d8ENNq9ujh2s3v9953r3lVwMxj3Bv2A5l0cn9rB2sfA" +
                "TuyMu1fBqw9Bt/++Nxz0b7r9cXD5ttN/0704PrPLLgf98XBw7fd6Yb+/63deXXeD8SDo/PSuN+wGo25/" +
                "NBgGANq5OP7Ozhr3brDF4N344vilO/2w2725xc4Xx38jSrirUf+h7pNMz3F1UaFy/etSF6WwVOGoM+4M" +
                "xwH+HXeBQnA5uL7ujYAUKPDtlinve4Nr/B4Ft53xW8zuj8bDTq8/HmH+c0fMN4POdRPYaX3s96Cc1SfW" +
                "htwiupsXrcbtvBkO3t0G/c4NqPz8u+ZgAxKmvGxMGQ5eDSyKGP1bY/S61//RAf++MTZ49UP3cuxGwU97" +
                "qngqSj1fJ/PrISYEOEB/9HowvAkcEx6fOkbzxAK7dC9/JF4EP7zHPGIKTHQUrJ2V/uUxRzTLML3+64Ef" +
                "A7H26mywdq7+IOj9GIwG1++Ik8Giz1s7EeVKkz3D6cZ3SaHmuiigo1RksjJMskIl2dTkc9FC4cQsS1Xe" +
                "aZXTSgV1VuojlbR1m78uTJGw/lJmqpKyUP80QLBQYRarNMnui1ahswKalHf/gQZFj/I80o4lFOye4pEC" +
                "IMNSzcMnBU2i1XyZlski1epq8FqFuVbFQkfJNNGxutO5XgN9Q3Mxr7YFLw9iMw0am3XKMozuACUyaZoU" +
                "hKeZkFIu1EHoxkqjCjPXjIXCDE+Dw5Zbf+mWD3g1lLtbDcNhhwILmfZ9nYYzUDdOSKdDS6zuNKDmAI1r" +
                "KCKdaYUHbDzBQ1bqfJHrEkcJQU8VJ9OpWiXlHfQJ0aH0JzQMhNe7O6VrNUWZPqlkvjB5GWalwqWCrFkM" +
                "hGaMjUd1YuIEJu/AnQcTM8Pwo1SH+bbJpPanOJXw1vl5ZHJ9fl6zkxON/bRaLmLBNSnl8GWN5Q5bE2NS" +
                "HDYg5L6eAGxnQS8A+D/0UsAceGfSuFA4eUhEEDONOyE6MBMJ7oUu6QGmHPqdxSfHrYNEIgNt1drjLWqL" +
                "NGgow+og1w8mXdL3XC3ypGA1cUinifUUagOkfjoHAPXXNUnjLfHuoIQxAZgfHlVTHzTchqR82px6UvDk" +
                "k+KQJbRaoqe4rlKwJ3osFimJWZLVAfTntLp/2GbEuhUuWLHMElCBGC7WWSlCOsEbZCoL58KYGmIbxsSr" +
                "Vo5J4EFW+DHgL4hDAojVfky1Qq0g6uB5cFis47bqwNNozgF0nNRAftw1sv4QSXJLaQu+QRyH9xQ0mjqw" +
                "SAqcWZjb6Z8wz8On4oh3IDHia1zA+1unMB+Grp1xnZmQeJtOMQ/vtSyy84E7cZhh3y9M2+ofdzpTuj1r" +
                "qyezzJ0WZSwyA4D2fsKiwM1il9hJk54f0RIVhZmCsBLW1XXyuZWeL8ony41EPcFG7raGe3Fnlik0q4PB" +
                "dCqS36DugTIIKXBqUkOzTIY7X2EXoOl5wB+zRhziAn9oEDrHvZRktfkG23Bw3wpzCI/UPGbmH+v94tUJ" +
                "Qu2TY/jaJ6HAV9QpZSwKRU5NygRaJYvDPAZFy5CVB6vcZAadepxqHJKQnS9weaJanhaEeEXPGTQ4edJP" +
                "almIBYrMfA6qwl4Iy66tF64PmRGTaJlCU0cGrJ5kNH2ag2wEnWhMHmgWadW7Omce19GyTHAgCGgW5Tos" +
                "SEX3rlRrKe4JFrT2xitzTGZoRibKbe5VhH6EdSronGFBauqvglwbsEnjYhcw+AF/C/AKjYNNcAS9MJCD" +
                "A5z89qm8s4b1IcyTcAJJA+AIFADUfVq0f1iDTMc+BzdkxoEXiNUefwRs5uESTsfeIhbLGQiIiYvcPECJ" +
                "sfpiVoUmBP+mySQP86cWayvesrX3mmgsUsQ3QppzXUItF8ttBEm8GyO36QwRfw413RhwEdcOqkjsEnGp" +
                "VTgsn17txXqWa2hgzJziITbQNYAzhZUzK+dFAMFlVC6hozGt2lCUa0+sSVEs58TQxDqhsxnEutZlZ21Q" +
                "LPhOwfUQjTzMCvJCZc1Ml5VNAtgwNXZ371+r6A7Oa1u9JvX8iNtJobdCjshwrdaAwZHCtu+GV6/Zzp6R" +
                "X3nwCAWK/8MV8QRZRbBPoWWQtCppvhqv108nhMSvPAEUWUtWZm0cUGWGgwbeRcRMHDIJo3tCeO0M/29a" +
                "d2taVzlU490fNq1u+v8l0/o5yypxES0vWjONWKJEVoo1yNixsGSs5Hlj0gq+Ek2g342xfzCZMCj0+np6" +
                "7zPn9t597rSedTa9ZpnocqXBGuXKbNhNvkLSeXBVkYYDCd9zzu5M1qci2D8tsSDPSAfkRrTqrvC0x9mG" +
                "ZQh3iAYbKCivjpmv5ppiQnCWX8kxJnEO0GiTmOUcwiHYL1VsQBLEhKzLIHBkbNi/JqUMpqyThT5jyQGJ" +
                "3BHFuJnMIoPBTgu7OdDYeTJL4qYyZfVvsTtS5fQUjA3B4jPLZrhFAHEEP2yr3pTFdEUIsYi7wI1CNnsu" +
                "9gJKY47ItbIg1il6y6LkJBZ5kBLS0q7Sro/+ybuZ6rcd3XbFaFsvHLY8Jx9FKLh27fT2a8WmROcv4eSf" +
                "VjsTWtIfHjNnbIsqjl1HaZKbezAVrosYraAMDWUpyPyG2Yz9YDIgUHxOaO2U6t3O2xWCogy33R0uRC6p" +
                "wu8I0oXzsyUiHMnN/2NYMrDqVRITu8krfiYlJihr1fgsMj2pZZXYeiFgSh5d/oQEmI0oJeCcG03PbK+Q" +
                "TmNS+qQl56jgh8IxKO5CBFZMKcSKeCIHt9w8RWtPdEQ95UfTeMM9aBXAY4GiGyNkGWRmbLKwDeVFeTtd" +
                "wvUjFWUTfXvb4Lm0n4QVHg1PCNkgbjUJJUBbLo8kGSdOrFpXs56ac0lMSpXiRGZlz2CWMMB7tDEySyG8" +
                "e3XsDybHICc9RRwYP0kKBG6CnNQuqJWvCFggqV2yKFMVwZfFFdhTcbaGa1fxRnSK2+BAQe6DLJEgyVZo" +
                "TjA8KgBtDQi0mwbZEc+LVYtSxP9kSnKzZFmwUA6PXJqM98h0RKo8f+Ldcg0HjpZR2EeulWxM1weYLp9c" +
                "K81VucKqsAdi0OkCu4W7lJVGhO/d1cZlwHpO1X1mVj5dYefvRiy3iGPHuoFsCWOmjs81u5iOxabpNQq2" +
                "YHqLqaXhATMQw8IFSqnw0MkrAl67zl01ch7CF2SkJyGMsbEE8gIkvwPK280yTvUIMoLDmCAQGAe5SuJb" +
                "pUt5xS3evBNbWZYwO9JMMEszrc4+eq3W4AgwMinwd9tElLad44APIBYrHTknz7p1Q5SxqqY1fY5ibZxY" +
                "i4rFe+pGF3frUOkL5s5lYCscGqtAvCL5oEugGJjS+0gCWQ+hqLA7UhNb4OFpLkYVhwLIL0XYcGNxzHcB" +
                "BUtbHNbPhsp1xojwTp9Bksaq03ViivVrjCFU9xVULmewb1ebBB5FgXlZwAvUj/AZ6PhwNcWkss5ptyZP" +
                "cOI7V1cX39I2Q9araztNc0NpBQRd2UOSm2xOvi/lrKEknkAlxObIIIkocDWqhDwXDZ5I4kPZadi9Gbzv" +
                "oshNOC0WpKrIhXXcbHMeVrfyoW14+CVcncstixyeuIUKydvbbv/q4tTq4WrP7dvxLkdQjCvL+faq2fc/" +
                "oBnu3lwYO1+iao0ZqZ6WEqJSigQKrTAp0QqkdRqjUqioloCSsRyRaXNGBxwskNp0Hj5g4pWcUTfRuOGv" +
                "pxe/rFagHv/lHyWFaEqe/uuL7Q/R5/L3q6+sN7koMWWzZ3UZNBnlqiiEhW/AmTpyHXGLOqf0yEwKej57" +
                "IJUlsArV79Zci3vty0X1Hc75i6yvSjscWzLHQGllKp44fQ8oDmA8qR/FmllOn/0wGvRPkNl2ObUPnZtr" +
                "JQBQ3vFcDE3rZaAWdJKudlSp8oZi2p1Naasu+w5UFNq4dRYlzukYcw+v5V6fq7/81z5ReP98/5L8m6tX" +
                "+0dqPzemxJe7slycn5xQJ04Kapf7//0XQRGGA/wOf5ATeplVjnJ71sehy6lRgfzHpNzHogRePwThXmub" +
                "UZ+mkNZJkiLcsRZqG8NSmVWI6ILoq1fCGwyEsCLRtztLKoyYawk6kZaTxCjn6ylRapHlKiODOVeeAPyN" +
                "SIBvTRKcf/f371/IDLK+kjPAvM0T79udRj9do6oKL4FKq/6e1jYe/Zq+dTMENm+l9lez4uylfKFa9rn6" +
                "7sXZKb9idk4T4ESblZ0By79CMqfxmZwUQsRt4MryMjo38TKlcc4TlGax7xgarP31MvafcxnITbsSSZ0Y" +
                "pIaLBTHbkYqe4GOz6waO08rmHF24A85whWNwlss1ws+ZOEcAwEjtk2FnYRQP+tsj/IekAJV+vlevBj/D" +
                "mMnz6PZtF70yp/b18gMacq66Qyh0+2HQ715wm8u4pqLY1tCZ7Czx1ZxWQPUE8YVtGKmmVoW7aoZbQ6kq" +
                "On59QW3auaR/KW7hPgchghhsItejU1b71Zp9MXHclGGDQyDOR5Uw4ucj9UEy+r/Uz0xE5shJZzO4jPZE" +
                "TTVE8ZPHD0RvV7QNfoZfUr198LSmt1/IlteOJPS3p+IMGF07aU78tmV3KFA5J5QaaWPBG6X+ZElHsPGO" +
                "cJA7h8ANhp2r3rsR+Um1Pd0lM0y6YClTClWEdTgVwYlE5yRyPcZu9YsK4Xa0VZVBXIMbvO323rwdqwOC" +
                "bV8OK5ykr6RG8Qqnu7U4y8mCOiBZOJT9SNW5fQQ7u4+81Pb53C6UWXS0k+uzIcr2PWG1JSvghqgJ0Hv7" +
                "TZmk2k+ScyzMCVZUIBcVDzFNaT1FncTvy8WRrXR9Y4nqhLRBTM9SDeTJK60kdWNyRRhM3FEmjIIBiULz" +
                "jWokeaXNVBhfGDkJMi4dMETwWsYTqW1J3vrOgFpivjZvdzjiMD7Zt5ahqjfxIDXiCqEVxl9Ize7CFlGg" +
                "6S1Q7bQUXZKmQLAtUojaejaDN/GfNVWLlmD0uuJyp9Q0YGr9gUCTqp5wfIqPn1q0ydgC4BKThUUb1FJ5" +
                "boULxeAFLslh4kafu41AE+TkbhBZtDNqOUS2Uc1hBqfPn0uaLD6eyVH1Y8DJwR0dmGP17e0AUi6H1EnQ" +
                "XyUFfOYgfFTfUFrwGxX9hn9idaE4zA7V+QU4XU8/fvuJMo3+9Tm9Rv71lF5j/3r2ydciPr74xN++Hg2+" +
                "kN171sh3bS2SNtY4jmNBLv60o3uFwy151WSrYKpuO+QFKRz0QvnxyNVYMIqXMIqQKJVAvPhEd2XWZ0tX" +
                "1SefUa/tJfZNP1JrEaUorHPqMyZWM5BJdAkJShxSax6qGkWjnM36ooFmG7i3tjSCFZudYNShWn1cQ2uz" +
                "RyxeuswEHIKA0kP2Tzt215FdZ8MvZaGFH6tZGwvWun9qC5v90DUQO+Pcz5zt2Xp/qHVsucOZGch2YjWq" +
                "9LbjRou54QY37s7awp3ujrdktbe5Irb38xjEctWoCtaBtH+6ztdmP9ChE0SZUfUq1UBw2QfNa+kyJixs" +
                "N0nNxylOKk4+WeffPdujbGWGpcjGI/aTh1aTJwkS1haJ6LlplDOnkLIphwwMnudWffiZ6/yz9OLvHcfr" +
                "x+bNUtLEcVm98+yAPA2jXtJfHxz+sfYYnxeyqdWwKmFXitBxJzzTGSWo6m1On2mwqWm1jS3APDX2+Pf2" +
                "WWe0P1kxrv05nMQMXGry1cNZHhaLzUoTV7NsmcL1JXL8SALgiCJr92r+KY2KQRAjRQroi7qYtgjkL/Ua" +
                "VcFmuz4KA8e+hrl2CipOAkfpVw9rukx0HFVCfVt+inawWvFG6bTQK0kOUZq00XfW5FoKvka2g9gf2p8Y" +
                "3TW5YQExHC29yRPUM/JxrbMHatJi6ya7xSgeIvFY/t7ailR2MjuOme/U84GFmYDLIgQDouhWtpu/QIkY" +
                "gXG0mSH3fZs2BUp9P5TF1PHJYolyWXzCxXXnd0ToEZKgnulPORuv3GzZPJAyt6tI7jA4tvcj/E4fXHHS" +
                "ccV2I+jasxvmjQHwJe9EZDevnfBwCbn1LrFKBniNLUfjihZJdA/VS54ncYsqwwK9BtR27Dt7+TlNppwm" +
                "qepeZsoRF/d6YYLjURfLyMDUAqZrt+bOt705Ya03+W3tu3NS5Fd6UFL9WsM0Bl+HuDrxPM9O3aTAD9jF" +
                "c2DvvtmI11bgak0ZU2pkIETsX1jZXjbWJNQ99UD9diW6qhdww/2WAF1tt9sexRpHb7Yqumalf4fJHQxp" +
                "ewR2/wMJCChzzz0AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
