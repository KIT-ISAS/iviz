/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlanningOptions")]
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
    
        /// <summary> Constructor for empty message. </summary>
        public PlanningOptions()
        {
            PlanningSceneDiff = new PlanningScene();
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlanningOptions(ref b);
        }
        
        PlanningOptions IDeserializable<PlanningOptions>.RosDeserialize(ref Buffer b)
        {
            return new PlanningOptions(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningOptions";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3934e50ede2ecea03e532aade900ab50";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1c+2/cRpL+fQD/D40YOEkbZeRY3kVWu17AtuTYQfxYybt5GMaAQ/bMcEWyJyRHo8ni" +
                "/vf7vqruJuehOFmcJzjgnCAekt1VXdX1ru4M7pt3M2uyfDIxrTOpq5o8s7WZuNq0+DAvkqrKq6lpUltZ" +
                "c+jmbe6qpDgavPVfruRDGDeScSPCGwzum5eAOssbMymSqcHfjW2Jpq0X9ljgJynhYWRt20VdNSapjL21" +
                "6aJNxoWCNXklQ2vbzLE8a8aL1mTONqZyrUna1pbz1k8CLGMGY+cKmTpyVbH6DeswbtJR/F+Y56FWMikp" +
                "Cre0GScWzl2bpHaLKjPmsHQ3FiCrxtXNkcknmJi3eGHLBsCTVhZqK7eYzkAMWFsmgo0wb5K8EFKTsVu0" +
                "mMnV2Oomr11V2qpVYohvpPj65NwkxcISzNw1eZvf/AIxHSlc3Z3EcN8TUya3ebkoTbUoxxAGAPJsbv4S" +
                "sNsOeWEn2IfGPFDsmZ0ki6Ltvi8amx1zALiO6cu8nZlsVSVlno5qC5Gb5NNFbQd51Z4+7JM6Clg9zdy1" +
                "BIvMJ/1RRBE2ss8TogVWz9JAUuqaNhKv1M4TLAh8GNso//jWSeGwo1lmkx06CVhm+XQGFmGbqx7+Y1O6" +
                "WmWC3AcawEhMZZcq0pW1mc2GBBu5N3wgkn0HZbYiiG3+kic3ebKLoYEJg0nhkvZPj8iDUZNM7Cgqy4gE" +
                "RYny+jYGiBLqlVdAkWdJC25ki5qExInHlHAVvCan+IJ/s+TGyhoh8QKIaynnC87ntnUqClVukxrvh7A+" +
                "QS1tRWZTY3Ju2mRRiRhjCe3K63RtCXfgjda2lOp3EfggOsaLlX5akyjCSEowWra0zUuhYpmQMnKhXVpw" +
                "fRfMQ3xvyOisOYrc9RgyWySrwb3B4//lP/cGr66+PoNc3di8HZXNtDlZM8H3QBFEwK6pvVjjQdPK5vEr" +
                "6Z4sCrDSjWGUsA2tHVzy9xV/6uuRvvYsCkDFBsus0oFIlXZ1C9xCVwc8CkMGjTzOJMty3UwzqfFKxkOo" +
                "5kWewhZiUjsxh2IZaOZtqua574Bk3tFgaiGbbb1SDryrk6qhQcXqy7nN3n8wk/zWZiMZPWrDZ+63kB00" +
                "P3VFkTeURdjiOr8dPNEPz8L7V/I6jIee+PcjP576DHhFXl3DFoC8atoMvsXTW33ASvht5L+tjW/SBIIu" +
                "o6/4M4yV9xz5pG2TdIZluvG/wIvmuLfe+MreQDrJIpe2rkzmtDJ9suYmhQ5SKWk58AXeCYamcWkuKq0M" +
                "Lr0KJnWdrAxZn09y0UJ8GrwRZGCKq7FIRT1SUFynmsQeyvWo4DtXF5lZ8r8c/Vycb5WFLV/ObKu2sy9I" +
                "aoihtLaeIygQGw7TKRHKpoA0kAXjBIhKuliJvNH4Yy862OkOFVBYCfFukqk4kzbJEdP0fb64+Z4uiarB" +
                "mg7tUOVdPDncEFUuh635lwMvGBdlIiTNQAMNxf4NP6rmyrhOc+WLDz9K7KzIQQm/kc9hrc/fPMeO27jf" +
                "mQEP7RroVxyLcT0UMn2UucloA1mU1y0xNYdJ+Bb2S1QAIyIPjgZhftQ+lTuIXJjdU0APeU8S5feU2wpX" +
                "WaxMXs5d3SZwGjRhcPtZIfaLziSQOnYZdegwrAcDGQIywC5sUu8aDEQSV6hsnZ2liB/OznqWeWyBz5rF" +
                "HA5ZAtJWF9/2RO5oTwqwWwSjAtCcRC0QCZy5ImsMVp6QCZlt0jrHnpAPIkRKO2MZOhpX/7RQ9amx62CR" +
                "6sAQdsz7pDjJgof62RzW9sYViDbI7nmdN9S4FPE4ENtJXpHVqzMAMH9Y07Tg2wKUBGa3NuXRcTf0xhaw" +
                "mghDtoaeNDL4BHE/NbSbYifYrlapJz/mcHVYgc9lPIDXJWe/PkKEiZkXHS2M3KocXKDAZUgDVEnHeIJO" +
                "iVNVRsxswnRtzW5rKAO8y1kOiB0+4VpjllB1ic1qiUHh+bbGMEcAY6A/YRvFfqgmhalEITvIAIE4lYxN" +
                "G9jkDdaswh3sj3gb+DBioBrJNs4REq5zWBbDbRdapw7hg+pzmVxbneTHg3ZKWEhPh+Y7Rsx2OB2alVvU" +
                "wYoKFZVjZqf70/OHXptsecwp4jyhrKS62071kgwCV14ayT2lRve2R3szcwv4P+Vc4FOT/wxzD5LBSIXT" +
                "0xrx5khZEYSuSGaUgbjMHnMkLguLBqNr7AvzK93B4WAweKHCoTIy8OEZjKrIjw9b8RgUofcqCHzvlXLg" +
                "E9qUNlODoqumMYFVqbKkzsDRNhHjISZXMq4vCsQ/BYll3OdNy2pOwjt+TmHBa4RdK00GpcBRluAq/IWK" +
                "7Np8lXrmdjUsx6KApU4dRD2vOFxCSkInjy32pUqteXl+JjIumQ0WBAWt0tomkva9PDeDhaYfmDC4/27p" +
                "vqAbmtJFBeTRRNhbeKdGk1aaqT8ocUPAPgvZhjmUdyM8wuIACZZg5w56wJTk7aqdecd6k9S5lBUAGFEl" +
                "jegBJx0c9SBz2WeQhsoF8Aqxw/FrwFYRLmn6InrEZjEFA5kj1u4GRkzMl4gqLCHkt8jHdVKvBmKtBOXg" +
                "/nPyWLVIdoSWc11DQ5KhAX6e7cfJbQdDlM9Lyx0DLRrawRSpX6KUeoMj+hnNXmantYUFxsgJfmQOtoYJ" +
                "GbycW4YoAgQuUhTDxL91CNW4vlRv0jSLkgJN0UmCz6DoNqsGOapaA5bLsKeQeqhGyIR0ztS2nU+S1MR5" +
                "7NdwlOI4TTpD8Do0z2meb7E7BewWdAMBPrbVOzAEUkD7j8vz5+JnTxlXHt7CgOLfZEmZoFeE+KBsJx9p" +
                "VWn5erLeX50yUlOsYz+XXmbtO6DqiAANsntjWU4w4yS9JsFra/h/17pf17qsYRpnv9q1huH/l1zrXZ5V" +
                "8yJOb+6qVGBUryyxMWiJWIkD+PfGt++ETfio/Pp0du+Odcfovg5Wzweb0bKEilm7dFt+U7aQNg+hapLC" +
                "mA3+CZa6+lTnF6rYf19gQl1JidCpVd0XnX45u6hMEA7x4wYJJppjkavSMieEZMWZkmNSckAGqywIu5nC" +
                "Sek09C/ElkHh6GwkvqZRhlD22cLXmHJIlTtmjlvpKClJchUS5sBi1/k0zzaNqZh/Tx0KyZOHEGwolqxZ" +
                "kWEX2YDxDD+SqjTVdEmCRMVD4saUza9LooDWuWOGVh7EOkffiioFjUUdpIW2YONDvfQ2/ophpvl5T7vd" +
                "CdrODYcvrxmj+DZNf9v59FMnpuTzx2iKv5Z7U1raj0hZcLZNl8eukzSu3TWECttFQUNDBzEAfDzdb1JN" +
                "JQ6mA4HhC0rrh3TPfty+CFRjuGvvsCG6SR19x9AurF88EWmU9tyvolKAdY9amNhPXfGOkpiSbM3Ga9Xp" +
                "ca+qJN4r0Wq492pS8GW/DgW4EEZL3dkX6YWVsWjpG4kLBAbNLEFiJZxCruiL9+32Kgb31Ub0S34cJgjv" +
                "w6r4xqTsmDRNCbJyvlg4hPHS3ixCP5ooX+i7vwteKPtpWhHJiIxQBNlgk1EKNPQ2fMVJCqs+1OyX5kIR" +
                "k6XSXsO0dQs44PtEHJpxX8SF6TKkw4o8MFv1e4rdhC5kEGAjLe1qIy5FLIst8KuSag2i7lSrR2vZKXZD" +
                "EgXdD3oiJVK8UEkYkRSA9g6EHXZ24yYT9WppgfyfrkR7jjR+AuVI27kBR2VTmvJ6JdhqiwCO03wXwCPm" +
                "9gFmqCfDWfC1C2or2cy7+BLM4OpGHkXYlKVFhh/D1Y3NgPecmOvKLWO5wo/fj1ruUMcnPgwUT6g95Vhr" +
                "DjmdqM1m1KjUQug9pZ6HhyJAAgsb+ArIX6JE7vUVCa+fF7YaNQ+VCzrpcQJn7DyDogL5vg3rdtNKSj1K" +
                "jNLwjhAIJkDuivje6LKuuCOaD2qr03IRR46EsOzq5PV7DYEBVw4N5ogmZdm2lIMMaHPQ6Og6ZdTb8IkV" +
                "q27YZszRrH2naGEQML2yzWwdKt9gbKkfdsLhtw7EU+oHN4E5MMv7KALFgxyRumPp4ofWpQ05qgYUIH6h" +
                "yoYd63qiRHHUXxvbaEKIYLqDSH7rVvckY67fE4yN/qm0MyS26w2CjN7kbtEgCrS3iBm4fG3sSzcbNmc4" +
                "GK8QxD85P3/8gGguxa6uYZrUrtw8toLfLWJfwD1Ena5GBUlVQbpRLfS52ZCJPDtSTJcXr9788+Lxl0LT" +
                "fE5TxRA2SLOveXjbKov26eHHaI1HhmRSoBO70BH59u3F6/PHD70d7nDuRidYjvVQh0i+32qJ/Q85Iuxb" +
                "SGPLBY6PYISc+ZAUlSUSGLTGFeQVWBssRmdQ0S0BJzNdovDmlAt8M0dpM0T4gIlHBqNhoAufP51d/LhZ" +
                "gXn8zX/Mm6ffXDx7x+Lpb5/s/5A/z365+yp2U5oSE3F73pbBkrFWxRQWsYFU6hg6Yhctuuetm2pDL1YP" +
                "tLMEUWH/bi20uLaxXdTHcCZvdH7X2pHcUiQGRqsy2TjYe0AJALNxfynezUr57JurN69PeNTG19R+ePLq" +
                "W6MA0N6JUgxLG3Wgl3TSVgeudHVDde3BpwzNhcQObApt7bqoktR0eJCsyK/tmfns3wfk8MHZwTPGN+dP" +
                "D47NQe1cizeztp2fnZwgFUkKcLs9+O/PlEQ9soR4UAp6VTgwIrvnYxxuTo8LchqsPcCkHFE/FOHaWl9R" +
                "nxTQ1nHO00PeQ+0SWLZZlYkhiT5/qrIhQEgVVd9j1lIYhWsBPtHKaWFU6vUslHpipcsoYM5MZIC8Iwvw" +
                "bpMFZ3/881ePdAS9r9YMMG57xQce09Xfv0VXFVECW6txn9YQX/1UvAgjFLagMgfLaXP6J33DXvaZ+eOj" +
                "04fyiNE1ByCIdks/Ap4fRziyjdcMUkhIQBDa8voVh38WBb9LnaB184Mg0BDtT1exvytkYJh2rpo6digN" +
                "N3MKG07VrBBjS+gGiZNjMqw5hnQHkhEax/50G3UEcc44BAIARrNPxy7KqBH0g2P8g6IAWz9fmadvvocz" +
                "099Xb19cXF7Awejjsx++ffn6/OISBt2/ePP64vGjoPDBRImv4Zr8KI3VglVA9wT5hT8w0g3tGnfdiHhw" +
                "DqUqOZbYm9AbdqblX+Ytcs5BmaAOm+y6DcbqoJtzoC5ODmX45BCEy1I1jfj+2PygFf0f+2smkyVzstUU" +
                "IaNf0aYZYv4U6QPThx1vR98jLumefoi85tOP9OW9JSn//aqkAsZtp+XE377trodVxa6INVa60erPF1yC" +
                "z3dUgsI6FO7o8sn5y39cMU7q4QybLDC5wdqmVK6o6EgpQgqJIUiUfoxH9aPBOUccfugqiGtwRy8uXn79" +
                "4p05JGz/cNTRpOdKehzvaJqt5VlBF8whdeFI8dHUBTxKncejDz08d2FhZTHwTrfPpyi7ccJra1UgfML8" +
                "Ltrf1En2fvJacmEpsKIDOe9kSHjK+cw6Ke+L+bHvdH3umRqUdIOZUaQ2iGdU2mnq1uCOMRi4p0oYkwHN" +
                "QuutbiSj0s1SmGwYgwT9ridgyPBexROlbS3expMBvcJ8b9z+aMRiYrFvrULVP8SD0khohHYUf6Q0uw9f" +
                "xEQzeqDeapld0lIg2VYtRG+9miKa+EvP1Mopb6ZjEx4acL3zgSCTXU8EPs37DwMieecBSIvJwwrHNH0p" +
                "L8wIqRiiwAUDJjnoM9tKNMFOOQ2ik/bGrUDILq4FyhD0xXXpIYv3p7pUezuS4uCeFiy5+u7jANouh9Zp" +
                "0t8VBWLlILk1n7Ms+LlJf8Z/MvPYSJqdmLPHkHQ7ef/gAyuN8fFLPqbx8SEfs/h4+iH2It4/+iDvPh0P" +
                "PlLdu7dR79rZJN2YEyROFLn53ZYeDY4cyesGewPTnbZDXZDpYFTK98ehx4KveEjSFIVSTcSbD9wrtz5a" +
                "T1Xh1LqPFnq41L/ppQmWKHxwGism3jLQJYaChJzUx9E8dDWajXa22IsNMoegfbDjIFizfRKMJ1S7l2tk" +
                "bZ8RyxahMoGAYMTyEI8Kf8rDY798K2BHJ6x/IkbqV5sNay+1w3jO6H6oqG2NTGc58p9u4OYZyO7ssBR1" +
                "/SkojPprYmYw7I8/8+ngMr/Oh7Vrhq6enrSTz/7WTv56kvwNKVh6DUDScb6CY2P3KnMpjgAFW0PxkRtH" +
                "vWLH1jEFn4GtL9eoe1BKulq0DNK3g8jOjmd76oHtvJMRWmDBbYIJ0JYo43rZS+5oRIsjQ7zFCWV3eAic" +
                "SqNP5Ne8m3znFRHkITDbzBubVakF62OtdK8rXh/bJgkX/BZXpCos5cauHtzJ2Tp+brstJkDJNTY+I/Xu" +
                "PfShlBl6NSwerO04NOzd9dqmM9yORGkKDOmuxyHlBZ+Vw4fVlyh5PpTjfv7gIutJWuLyHLxz5XqEkGuL" +
                "idQ29l6/zB85IB4vl0oKFiFn6mUZvRqyohfBGMNuLaUEPHbYIUyQXuNDsZdrq5XhPk1SLvrkDKaCLJUl" +
                "+Wts2lWtzJPX570KaydoHsCoLwI8kr/1ye/876JGIoPUoo3rCd1WoD6f4iCgbIdey8sCGeFxLyvv3afC" +
                "eqUvFhzlXU3scBmrO0vTb0KHwD/cytobFXLP61fTwItgH6VBb4vthYLeNbBgfbuaMqnRmwYwTKqMbHpj" +
                "7HbTMih/UH0Z4899C/jLr58+8R8+/UnziPGeMpXXROOvafw1jr+S3+Fep1yiI9e37lZtnm2Adu68JfWu" +
                "d0dQbGn/qFPXhOzgs5o88DO8AOjDdzB/LDgEcJ+wt/QL2LXccXoudxx5UxfBmB5LgLuRi+aYgALA7n67" +
                "FEVCx13G7exW+4s1GhftaMCyDjLN411kn157gIyrd1Hw+/DtP+eX3K+TC1vwDyAyTD2U2imLLCcuTRdz" +
                "ON8jehG5YitvkipFcVW5cTgctydDBgl5Ea6oKSDp0xVoMPQjTy3vsEKl09etyKVFr1RvkfNuQgm8vopd" +
                "8j4CQ0KdVuH+ccxNuLt+GoF4MtDsyGFof475uk7VI416qFyqmUP53wNIH4hBxLLO2WCVsQ2vl39F906d" +
                "uTf4Hznn/gVQQwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
