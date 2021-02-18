/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceActionGoal")]
    public sealed class PlaceActionGoal : IDeserializable<PlaceActionGoal>, IActionGoal<PlaceGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public PlaceGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new PlaceGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, PlaceGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlaceActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new PlaceGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceActionGoal(ref b);
        }
        
        PlaceActionGoal IDeserializable<PlaceActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new PlaceActionGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "facadaee390f685ed5e693ac12f5aa3d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09a4/bRpLfBcx/IGLgZmYjy46dXeQm6wUczyR2ED/W483LMIQW2ZK4Q7EVNjka5XD/" +
                "/erZ3aQ0dnK31t5hLxvEFtmPqup6VzX3qTWFbbIl/TEyeVu6uipn05Vf+HvfOFM9O88W8Me0LEavKpNb" +
                "fEZPRo/+wf+Mnl9+c5b5tuDNnzJId7LL1tSFaYpsZVtTmNZkcwcQl4ulbe5W9tpWMMms1rbI6G27XVs/" +
                "gYlvlqXP4N+FrW1jqmqbdR4GtS7L3WrV1WVuWpu15cr25sPMss5MtjZNW+ZdZRoY75qirHH4vDEri6vD" +
                "v97+0tk6t9mz8zMYU3ubd20JAG1hhbyxxpf1Al5mo66s24cPcMLozpuNuws/7QLoHjbP2qVpEVh7s26s" +
                "RziNP4M9/sDITWBtII6FXQqfndCzKfz0pxlsAiDYtcuX2QlA/mrbLl0NC9rs2jSlmVUWF86BArDqMU46" +
                "Pk1Wrmnp2tROl+cV4x6/Zdk6rIs43V3CmVWIve8WQEAYuG7cdVnA0NmWFsmr0tZtBszWmGY7wlm85ejO" +
                "10hjGASz6ETgT+O9y0s4gCLblO1y5NsGV6fTQN78SNy4VyCItQTYzC9dVxXwwzWW8CJE4Cw3yxIOhJBA" +
                "cck2xmcNMowHJJCBntF5E0sCSUwtm8EhN9fAGpulrbOyzQBR65FpgS/sat1mQHCYjWt65pqNha3D0tnM" +
                "zhEWk+W2aQ2cHEKU0lfgLws9EyAvgLfFTQKds7m1xczkVwBZATOAKbuqBRn03iwsHULm1zYv52XOCAoE" +
                "fiKro4DwAABq1fkWIMtA6mDURM8PT+4jHd3KXduy5XMLmgt2060XjevW0xr4Jz4zbWvypS2mbvZ3m7f6" +
                "lqZ/50BdwPm8fZet8fe0kgceF505V8lza+fJNr5br13TTn3XzPGtLCkz4CzdZrpoyvXaNlMdm7uqKj0s" +
                "DcOewAZtA8fYggiZdjnN44NkG9i6Bi1HohCeAqi0ASDUui5fClo4b1450/7p8/Ce5sOUKTELoUy/X64J" +
                "xSy8d2vB+UCnpmQHbnqcIfFI+7NYNcavjyIrHY2OlLVBvTY18OTa+bYDYXBzkk3USjLf6vQ7OKhkLPG1" +
                "ncMAILaBaSicRyOgNpLNNVuG61sH678JD2mTKZ+97Bch0bUVAlsXd2EHmtmHBOSnMmg9SAzh1xzEGvUw" +
                "6bgMFzxRI2EqEFefiJetvAWF0dhxVrsW7RdRSKT19Gi0sA7Mp2LwClT9pdi8AHgCtVmDZgBJgONA4I9G" +
                "3zCHAtK1r+g8UB0J0jo6zm9gKwugvm96JJqM5umgB5nF4PRACFoknJuB4sorG/QdHCTQyduVqcFEA7pA" +
                "yRWva2aua3mhgofnsOQMqdqhaN9bdx7/QEYrmFBgi1zXeOISOgpgp6MPiNDRxxKADzDb6GnPYQtA/h2H" +
                "kXLxo8GUV/gTtZZjnfFPgZuAgFO5QK5qU9mB54GRwf0BuwLoBZF8O87AnwKT1MJb+GHy3FbgzdHLd+9g" +
                "RdcfzeL7LshWshfIFfCBvUEvjU3w46pKhOjaVJ1lwRe+8Ch54PoBRMbTE6Iz6SGPgwZYTgD1oFuJ5AJY" +
                "8iyikzzsoZU8Z2xGRcevyJRP541bTUEi4MVHOsxbtQVpYfzNDkBUUkMPmcVPvdsB19ICqHEOBz8B3lj0" +
                "rcHrNKqSEQg8xzn4VsAJoI3GGBzg40Les/pGdFxT6txJNiKODgNGf+0M2hxaN447FIIsXBTwgH+AXh+z" +
                "a2p+jAhbD93gCNyEv23D3349DPiRdIpDOChP9iDSsw88/vol0h2NwGT0AYz0b5tDeDC7dg8wLOy8rMnn" +
                "bhN7GH0BmjLmwAAQXJf5VbcmNYfWMmuNv/Jo4HCGvQHpqtgnqsp5S04sUIyMFBw6nnpLoRoMUDuNg0g1" +
                "0ou5rExGD9fFV0XZ2Dz1WxJQh87E96T7Hqo/EaYmq1kPD4sevgXYdgOa44hPBIJjGTWNb3T+qozj2aZr" +
                "OIHOMAQwuPgcw21ESOIfmIjz2bfCwPoa4ilw8DGGQq9Ot4XFky0PwvB9iu1j+kyG/I/UrK5xTX8eFLW9" +
                "ODEcfRGeZCEShjh7Cx4ruHRoqcNMmBhYikLLhs53jLFx4UCOwOGFNVbmCj3C2rP/vF7DYn0Jg8cw5cRO" +
                "FpMxR9c0ikQGoaA0EXiTTbkohVejW0k+uSA3ztr5AzgZcB4IZt4M+A3DZMeK6nSSPZtnW9dBYA04wF8a" +
                "yU6RE6JwkQS2zpG4yxJ7dHsIu0Gvt3C4H9RyH+eoU9WWxKV62ApksEAmePAcCIQoMkuC2MmROGLJsyDf" +
                "HojpyTlDNWU40cAkTEQfHDeO/iQURPcMf5J3FgENbnKyEw57JUayN1It53Dwy2iMeuMTIzWc8j3E8rOy" +
                "Ktttb8Z1eNyfcIDDGxAGDiD82OM2sM/LVEd9RNlRTS5BuFUXgfIxDInam0bIdLQ+lgIvn70Nu9yFt+j9" +
                "goabzsA93owjBJ8m7yC2u7bvjgKf66D4ZDB23wvagKF7DKFkuViS1ZybJCRnN0qok50UFnSM9TFCL1eY" +
                "p2Fz5DJHIUvK0tmTCjzIBl/+ahtHWs1nEMD6MLU9jcAxFIfIqOxw+q2yy3YbPJWe/yiHEnGdICV7ZifJ" +
                "AriZwwRvfbVDUrJomNUMnIPDEsbB+eC+eNtS8hmhoYXI6p3G6M40C9uKY+uScRtLcVxQODDhFr9F1pjS" +
                "GlPeM8Jw7apOc9H74M+ORl8hf8Me3/PQOGra2EXwg/538dpBtEyfMECDc/J80RNgUsHBPjxX8uBRA5d5" +
                "B7pcbSBY43VTrkokhCR/yM53a65DyOE48amzE4NGp8NoAFD2S7O2DMolrvpKl0L1Hpbt5erQLY+pe5vu" +
                "juxEKV90HncDzHRNyqbJws9AWRZFqU5EXHCMMre0XvNTMQ+BvkZBfwT1iVwj2J6YBRz8GBMSexB9DmsC" +
                "KLz0e1GT3T+MFg5UjD5W0e+2k4pMM3M3Y6AR5zjzLQh5gVESSILNNJODqzBGzA9EQ0oVAsJlE0kJiyHh" +
                "LaaJsdLWkIm6P4b/gWuFxbovsq9e/vjoM/n75aunF68vHj2Qn09++u7Zi/OL148e6oOXLy4efa6FFSxA" +
                "atxEMMkofD7SQQU47bWnrHNvaMz9xBE6B0UbwU8nJMPOMovZNVIO6HRqoNlSRrywN5rWOo5zjgH5xmxx" +
                "h69FpwLiBOqYfv04zn4aU5jxcwqzkWpXZesFRCgCUe4acNrXjqiM9R8qLslLIPok0nb646P7ya+fAq3x" +
                "189A6hQkpr9ART43HjslamoMEaSOxnCCk78QfQE2yBRlhyCIM8IcNOmd6/T14/Nnf7sEeNI99ZBpTTxg" +
                "LiwzVZh10NRQEZSdP+SkyhHiOObnzNyUoJ9jzNJbd/r04tk3T99kJ7i2/DiNOGFmc55SPOK0JBUeaC6y" +
                "kJ2gLJzyfujR6T6MnezDP5J9btsFYxmlHR+f8fb2PZ/ggSCl9BXWVfrKM5FJdN/LhmrrXC1sy3XkIaIp" +
                "1WXgkJDfu/WYKZt9KkQdDSRR6BdYaoA8MFciqTuDI2Fw4EdXcaijVbEl3hXqWWRAjZnA/Jl6AYb8y0SC" +
                "JUdN/gfZk1C7Atm+ttixYP3bdyPc440sALokrDUS5gQPpINQSmfs2laCZk8oShlsnnQgUikae0imaB37" +
                "CBS3Wrx9yHDamykQ7mNCm3o8e4PD3+Ni95Odv83N1uxaOlNkKfHAo3tBPlJMmw6t/v5E9j/Gqw+VPRTi" +
                "u3R0mW0a1LganyVVzei0mhm4hV1rpzdTnDkNo/cM2X54yK87Q/5FPfR9mQk55gRjtq7zjjIvK3yEgX9M" +
                "X3ByrCh9jhVqDcpOd7qouHXqSOWBJmCGzffsiwkrb9kWbJZYpEELUZILi4MdJukCm2PazzW08nMBjxzm" +
                "CCFuh2vNthmM7RpNgTMzaxqRCs1Nl/MeyXSEg5Z/ASd/FjBIaERLvHj5BpYHlHLKWZerboV59xVwG/5V" +
                "s8we7F+7sbZOgI8Va6UfVvIbXhc8Gl02CXrZjopLB/JSUcXqurQb+FOTL0IartoNYn+KPE5w1zXQwczA" +
                "reJuplPq1PGSdfMdOJXrrqEQYJIogp57QKdJVkQ6omAJ5RVM0ZWcnE8yMhx28yr9SCVd80utvPJRIfeK" +
                "DzYcCdvBSS1Bq1ics7FVFU+KYywlFqXOOaUR2TMqtnFaW3lf70RIHWjzxNeNWyV0Vz5t3cY0he+dbgCb" +
                "RcAM+S3lMnIyqXwDnERNcB21Fq5MvWUne0J+q4As6W0e8zkPGGfMEoYLgKDfyNz7IatzzoJyddl6C4Qq" +
                "iyiy5H3oAUsTCox8SFwODi6M5EZHhHlK+w5OFvbmELQvnMzLkV8mOomPqix001gWGNg3WCHGtkJ90hfc" +
                "0UULcp3BeA9xdJEORPaonLuSSgPm9NveYaEqA1LHQkQgCa225tok6smOGk1RmYAmgZOYdW1IAIDh25VC" +
                "quUhAICXq64l11TCskjO23mPQU947+KaNIrrFsvIXOh37EjheJ9+UxkBrwl4amVYemY2N12Usz0+Bm0j" +
                "ZSo03a0mLlT1MNUBTtp7WLmEwaSyZKkrYKuVAwcYmxyABZGHxpFrcsNlkh3glYgWtBl4sBD0U5mhclwP" +
                "ui/ZE442aEP2PtAw9OISco7plShp1aQ11oIqRS7ZnwigujsWTqXaNd+rEQJ/s0RRTjotlknVk82G+HcR" +
                "ahZ2kE/bYCcZvRkznEFIqTwF+NwXm7xxAhEHY0B+LBCvKX6w/lSXxLiwsi22MKsxlK1vWf9Vee8Bb5Eu" +
                "D5CtLfrdqFlPJ6n2UHIzzrhqKfiKmUxOZ0ct8ELRwhhZVBS7W2PPLIbm8Jg65u6PCUIux93Hrbwq3z4X" +
                "JLYblYVaKQBqigOnNFCVGcq9TiQ76SHSBUyrLbtI6RzSABVGwn00ZcE7e5wBNRs7zKRchu6rNLe5usZ+" +
                "NC13D+1O6jCI2WMGTLgPscGcolKxIRMaSKeuyYBIWTmnnC1K14BgtECfYinMqQ52GYVn1Ig/7DugA9JM" +
                "f3STfsweZQ/G2U/wx2fj7Gf44/6R5nMuXly+fD39+dHwyU+PPhs8+fHRA30impSObNC98C8YEww6f2OY" +
                "Wc7nfHeC667hZEKNxecWc/Aa5AH4utYlvQk9xDRwigtKjnzOFJ1XZiEiSqxLBlSyFXQk2Ibedg116koj" +
                "H1XQceXAvZSA9Cx3wbOXxg+ZhAwGqkebtusp5hR/FyzUHKaI/xtOlJXrBTfoUs8oziS/QjLM2QlSWhjO" +
                "Q4REfjq4997alUgHQgvchFY87WvFRa8NKBnENzS5ttRSfF02rl6B4RCUcMspb9lHKsg7p42u34NSxEdU" +
                "9C0ocV1elVrdrWbAGuhaM8H9l2H/RN1Udt6ij35/rEkQg5cLwntJ5hL1cT65F8W2Bm80nzZ4S2VeLqjP" +
                "mt3NBOGpbhwwxxMk/TVPx5HVkUNNKdPx1ZvQeiSI5eiTKAkYZ+zG3+1FiFw5STCn6RJwwCzYh+8v4ZHX" +
                "CQRjcqOJQSjR35CBNFkNmpCYvLa2QDsI6wYiTu6zjdmPHFiW2IeV0hnpcl2afXRVQvTVujdzOw0CNEWc" +
                "EvYSKQSH0XFzLHVlULBScMAdplLvTuITapkmRCi0EsKzWneUVq+LRHIbS82vwR0gUbU10twTLcnH7Oqc" +
                "9RD6aCIY4GHBytEm7bItj6jlMggxUqZcxu8GDEambQVE55xtyfXIjSkpzlLbvm9ZrGvLjatE08smBRiS" +
                "7UH1PeloQKiW2Liv1nd6a5DC4AFyBAbn0cLT1/jjEv/Oz6fyXOmkS6fhPCDK/M/WA48TzbzsxsvQqJhG" +
                "TMIpcmloCjDZukJHglI78+xkX0gS2wGojWAYXr3Rni+Jsd6+y+bljS2mfM0stITx0RP+qhLClR3gKQD9" +
                "5mj0mN880RfP6Xm4TRAmTHUCSXpVcTi7RiTrBez0Hfx8xb8AHkqoysvBFJ+bysqES/y7DqcX4s9IOCwd" +
                "on6cAB4e2WvxRh04PCtDvacpfmuKtKiuSmGXq8jtjzf0mNorEU8qMCYt/vTqaPSStnuCk7F9ii9c8Vqx" +
                "d6i37cCf+ME1EARs8L+SfCGTze4kHvJmaVvWsSl7adoMggQIt1pNiJN7M+QZ75BhaRGRAlIjpRfn5QDi" +
                "GWXq1jrCzvWXRMBIAkHfTiBobJPOLsrbYUhCHVt88wmZBWgv7nBsFGOJ5tauRKK/5ZlSOdoyQ6zAvpQY" +
                "15+//JoiwVh3wCpof/XnOBgGJrvQ/Gnh5tOd/QLv7rBsdqJpnnBoJBHSEESUAGHXBYJIMgvi9Qu99xel" +
                "MtzzORRnxfvKmP/AK8QaB/B9Z71QSw6bojtzBYrUiQIEAyn1BG56ZU2zb/ARV5aNcNnZWQ4ex9lZorul" +
                "gbpbF4wtmDICf3Cl7CCisJ8ZE2KZIA/EiEtXFT502hbW500pWR1iJUZdeo0gnPulY0FqHF1jZmnApuRR" +
                "uOTLk6i1gjshTxp7TYUkaspvSo+yl5+muaTZFu9uZ3/oyZyaPl3FFJQ3PR3HoXI5aLs79J6nwfcgakBZ" +
                "jVP4ilCMrNdgBuPNNlngBRUiXpxOCLGLiEvp5UouMhxEsS3L6mxLyoEuqzIhuOrYV+M7d511P6Ka14vJ" +
                "Od6gAmcNe3SGY0Z02R4zeHqM3K9DoqRTNRtL17Fwz4mcT18b+tK3Xnhb1RAZHz+mHSjtiKhj0r9PYQKG" +
                "etERV+oyZoGmkjhNkvFUaPGhkjnJfkAPG5vKuclb9ClhUTuMC/l8BhfYyQqOqUGcks9WupTCcDKa6Chu" +
                "hRuReozN8Hqn5sO004np5MtfsVmmoTtvtE4iNWTdsY9Grn0HHoj3vyNxyG1ToLnHJ8cyIZ/gZDQa3DuI" +
                "txKJf3avw+3ehtvu3nk7gELZtT+A1eudS2KsA5B75HCJFoHFCrtoLLc54b2GwsG5UpIbPT3V2JxKlavI" +
                "cT9m5GFdwrSh65pqqH7rIWZIuqs8l1yDQ8pzFraN8k+eoZPdr0ApkZLC+hgm/3rFIkN+sWlUWRjisL+9" +
                "Pv+adNpDNOUnN8Cs8K/ZnIZKJebW6aVUDtJPV6TQMSHZzR3He7n996M7MkJXA4GmBg5QRvgVAkC4B8P/" +
                "q7HDqrENXkBa/mY1psP/L6mx27RYerf6loCRWqw0OhwO2sCB4gD8c/DuByITvGR6HeaOVoBaKTkoPEW1" +
                "EkoTG7dz9cwPLnKNwn2z5NJVei1Wr0YdCEmitiComslHB6t/kXTWuCsuHTnSGNjYabjeYuoFtQegtAGX" +
                "KJIyJP6WcYfBjvlmz/lxl8jgRrO3ADzJLCJIGeffhCItFn+yu3yIoPeWSE302uBpKEWGQIeE3HDuJt5e" +
                "Il2DceEt3Wm7t6elSVL7yrFRhvrotfIxAATWkFtOSTSK43jTO8/mmmmnM6NSAC5aOwlkJ2URKpSY1gkx" +
                "6J19K4bKGdm5gEsgB+9QHI2G9HICrablJBqi8F9Mcxo2apCN0XxSBKDPYVD9chsSy3cDcAwKlQ0a0Kfb" +
                "NEUeJyRf2eCva0gKQpLKeseMISMzTHegi53WMv3OSyjOyQ1oukiK9eOIDpWF+RKo9AuE77FguwClaDmJ" +
                "Hi5Sn4aGI9qktjlarGZL2zW24g99ac5KtsaDxI9CaeLjQ1+RKSwCuPsFmU2v23twKmOsalzVblP/U2qF" +
                "u/L5WMznOPaPhYSI+sJyl+SWdtayCM0mTMgTYiW9Dv8ctn+G2Zw9n/3RQ8f7F8QhmAfQvkKiUhQpSTii" +
                "b7bgBlxp4aXnb3AJXCcuHrNOejnJ7nyWLAFbOzzKRi/fNHZ/m0ySIIuUuLz1GtRvv9f02y8pPZfrQrdf" +
                "JPrgzSBcRi98UTyBaSmLbcai8AKe3BOl6fjQdxTuUHDbmqnTtincpH/RCVPBlr/FVd+OLL5MIXxcSBde" +
                "1A+7JeyJFrRkEPDudek6D36lvSnxG2tawKJiDTeMzLbgGD0+P8fWgyMKI6lVMV0ntAIlFdsMY5UGXdYT" +
                "i51XrXydgXKqbb6UFSKTlMWpbPb64vnL7y+wsYEwW2PDDcWF4fsPHEmKAibQveaF3o9xKJvTJMUWziNB" +
                "9dWrixfn2EQhyjpuu39H2mjMlUwSCL33hlSgBiI9Qo0P9BI4FTrJ96delJIuCSLJgMKqU6LSlXYpAZNI" +
                "9JCBfLm2TbjNP6MmJfRyw1in7z+a7vyw0hnd+d3/ZC+/+vbiyRv8zuTvnyz/MIGevL+ioPfa8AOcaB9F" +
                "0YGWoy47iBG85cgV3U04Sr4MsODUdIjN5K6moX6OoS9yZUPuM93kjJ7wEjHAb5S38NZSnRWzYBVgmeSC" +
                "xCwFSIwypSi+vXz54h5WmSVv8dPj599lvATE+YGhQRMHiUi+M4HaXGnTu/mGG6vpmWQX5GuU9Z7TJ8kK" +
                "zadVeWXPsk/+4xgJfXx2/ARdovOvjsfZceNcC0+Wbbs+u3cP72tWQPT2+D8/ESS5Zl87TpvUWh6lUxSv" +
                "iD66FOnAV+WOYVLJTUpX1spnSOcViC53N076trXHujl9ZhLpqJ/OOP+KmSR8dw0VgWzNGQdisw5ohaqP" +
                "E1CeGuoxIyUI0++MVjrLAhX4IRICHg4JcfbHf//icxmChpp7uGDgLtjHutvlX7/L4Py8xZpBOK/+5pe/" +
                "VE91iCxP22XHm4V/+Cd5hHWas+yPnz98wL9hQoNDSvSWdQy4ChuIoIfP0blBhHQXLTzJ65UrugoHUEtJ" +
                "69bHgceR3T/+DSyys3vTopw2tGMx2NGgB6tvbrJP0dv/NMt/hf8U2Ko3on6Ws0dwTHb+9j5+wW0Wfn6G" +
                "P/Pw8wH+LMLPh+/it9U+f0fPDpwwGXyxJ6YV0lwsmfmdD/WwczcJH6y9o77Hzsh8WVbacoADhynCWCHU" +
                "b8fiMjDqzyZbNnb+6BORjk15VU4a5yeuWdxr55/8pZ3/+Z75C7BifgULUZ7w0lrKBhQu71bhdOfSz58a" +
                "gp0EmbBhH9yMY6DQtq+XOnEQPx29iVnrkIg6RE5hbz+GaDe9cwkUAP8jfN6PO8C4PSNEqjSG04HpF0eL" +
                "8rosMF2A78s4//YOkTuZB0nB++x+u2I/fywf1u19d7C34RCNC3wZoOLroXu/PDBsp6BcOxy9reZj+lxG" +
                "5V1s6kjbxZgo3DQWks+RUpNeE9guutpJCaa7iV/yWZuSGq6Y1if1Z+Aacq+xVjLR2rIPEL7SeQv0sbcw" +
                "Xnza3T/JQGirvwlxNaMDYFAJnQFJnG79sgg5pIAgOcwzB0cFMyiH84ACvB68NFzK7UJLubokH1MmoKTF" +
                "jXNWdfb4xXnqiCZ8J0tMe+yARfidd8oF/wSpInbEckG/ISGeBoQ0+ZV0yXLPXhGwCL8PAHjSVjW6k3xX" +
                "2L4vRahtWfETY70MX/gukfZnHQgP6vb6HVhgQ9iHsZC2sUNcm46tYP1MTsnagutyGw2Dt9xytjcFpGpA" +
                "dQAPK0K6yjWvv/nqsb752P8fBmHD8LXBJvxtEf42C38zB2/4pCY6buLr91TtpI3fvstu6Y56k7QKkk5N" +
                "v/oXczhxC/S1j0YyRViAf/wAWpC+DisvP14k/p7NKY358Jw6HbGHF5wzzvWC2aGudBjfWDusF+7eX3U0" +
                "bm/WT+rQ7CftSV7JNR1dVSoRsiB+ZXgfAorVIYn23yYW9dRRjxZenQGPQKaeUEEXy1f3XJ53a7C/p2hG" +
                "qN2Wnpg63yopTiaz9t4EPYWysqfck8YL4R5PKoMXaqIb6vQmpExP/38IMMahDwQgi2Jz9uo0XDDH65AW" +
                "XUSeVrsifr6ZL252XNq8o2hACFiClv01hEs8lT/0yL0N9MWVyZKLFIa73DdN2SrjeOw7/wKtO8jLaPRf" +
                "/QjLeqtlAAA=";
                
    }
}
