/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class PickupActionResult : IDeserializable<PickupActionResult>, IActionResult<PickupResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public PickupResult Result { get; set; }
    
        /// Constructor for empty message.
        public PickupActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new PickupResult();
        }
        
        /// Explicit constructor.
        public PickupActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PickupResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        public PickupActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new PickupResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PickupActionResult(ref b);
        
        public PickupActionResult RosDeserialize(ref ReadBuffer b) => new PickupActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference();
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/PickupActionResult";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "4c148688ab234ff8a8c02f1b8360c1bb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+08a3PbNrbf+Ssw8cy11crKw2naeq8/KLaSqLUlV1bSdjMZDkRCEtcUqRKkbXXn/vd7" +
                "HgAI0lKSzq69c2duthtbInBwcN4v5p2SsSrEkn4EMiqTPEuTWbjSC/30bS7Tq1KWlRaafgSXSXRdrSdK" +
                "V2kpCvoRnPyb/wQXV2+P4cCYkXjHqO0JwCSLZRGLlSplLEsp5jlgniyWqjhM1Y1KEcvVWsWCnpabtdI9" +
                "2DhdJlrAfwuVqUKm6UZUGhaVuYjy1arKkkiWSpTJSjX2w84kE1KsZVEmUZXKAtbnRZxkuHxeyJVC6PCf" +
                "Vn9UKouUGJ4dw5pMq6gqE0BoAxCiQkmdZAt4KIIqycqjF7gh2Jve5ofwUS2A/u5wUS5liciquzXQF/GU" +
                "+hjO+IYv1wPYQBwFp8RaHNB3IXzUHQGHAApqnUdLcQCYX27KZZ4BQCVuZJHIWaoQcAQUAKj7uGm/40HO" +
                "CHQms9yCZ4j1GV8DNnNw8U6HS+BZirfX1QIICAvXRX6TxLB0tiEgUZqorBQgdIUsNgHu4iODvTdIY1gE" +
                "u4gj8FNqnUcJMCAWt0m5DHRZIHTiRpjEwQNJ407FCPBX4OwCfuD5yOAfrLbwh8vB6Gw4eivsnxPxDP5G" +
                "sVS0TSylFhtVokDOFNInYsYbAvHZwPPiBvSAYfZPp8MPA+HBfN6EiRypigIoC0I4U0ijrwJ8ORkMLi6n" +
                "gzMH+EUTcKEiBaINYgksB/HAb0D6dSnkvARJTkq8fYEMUnekB9kiEJ/5swf/ByEhKrDAgVauU4UQklJb" +
                "KIDowVQVK9C+FE1BqToG5av3p6eDwZmH8lET5VuALKNlohBtXUVIhXmFdmAbIXYd0389ntR0wWNebjlm" +
                "ltPV44rEssZ960lxpb5IGpQKnYMazGWSVoXahd5k8NPg1MPvRHx3H71C/UNF5Q4JIIXKq7ItLt0v4zhT" +
                "kQSbSjDdYRXYyVICpmghwFIn2Y1Mk3jXBYzkOU05Ea8eQfKc6GV5SUpYC59jnqPwaf/8vNbkE/H91yI4" +
                "U+Cq1FYMv4a6wJP73Goinc2TYoVODd1H6VsBwkTFjUv4YvLDv+ESX0dmFIqG+vEB6DZ2yMT5+GrqgzoR" +
                "PxLAfmaJYbwHQBIxcA2BKCaCdCRAKD2OAjQIeBoT3WZfoXsaYedIbSTpbQLXB82RWct0Bnv9NM1vKR7B" +
                "haAKBeqtc1aAjHFUqGPCi6twS6xm1WKBZDSLSnVXBo/oyoZnAUsAhyCGSLpEduN9yCcDSW+XCcQW5I89" +
                "k0LSoWKMhYYUulTGx7TpBPtVhvIDt1QaCQQhjlqtgVdpCrsRpmbm3So42oG2ogciqQo0KYSRHyoY/MG6" +
                "mPACTDGgt2lyYa5UPJPRNUoj7OD4FcJJreVCMWv0WkXJPImsMhAGumegY6zHCwCpVUVKAXYugVU9yzwM" +
                "Qh6IdSsQxaRkvvmROBx4AY+G5aAo8uI0x+sr/DWM4Hd4OslneUmaBbSX6APyYhOSGtunU/f9x0+tRQul" +
                "A3O75jM4JyqSNbIYVgRvC6nXYoF/w6d5msvy1UuxTmWWwdaQePbwhGlTIuCQG3wPSgkqM0gb8G0pb5K8" +
                "ME8phri6OnluPr/pD8/fTwYnP+KfwHx5ed4fjcD+h/h0cHZyaFcPRx/658Oz8GI8HY5HIa47OXxhHnpf" +
                "hmZhH/x0+Pr3cDD6MJyMRxeD0TQ8fdcfvR2cHB6Zbafj0XQyPndnvTTfvx/1X58Pwuk47P/yfjgZhFeD" +
                "0dV4EgLQ/snhd2bVdHgBR4zfT08OX1nsbWR3cvg9UsLyRfyXuAajuZKQ5Dh1ZvGxtLua9ifTEP6eDuAK" +
                "4ekY3OAVXAoo8GzLkg/D8Tn8vAov+9N3sHp0NZ30h6PpFax/bon5dtw/bwN74T/7HJQjf6H3yG5C3rwM" +
                "Wtx5Oxm/vwxH/Qug8vPv2g9bkGDJq9aSyfj12FwRnn7fegqBwc8W+A+tZ+PXGJvZpz8i9fUGDNuqSeY3" +
                "E1gQAgKjqzfjyUVohfDwxXMnFIZYIC6D059RFkEePsA6FApYaCno4Yp/0zNLNCMww9GbsXv2EnHyxKCB" +
                "12gcDn8Or8bn76fEp6Pnj2HgaptlU3hrqCHaQScAOX0GbgExBhcDkbcJkwrcSCmY6oqkp3r07TrXCRkq" +
                "kc8pr/hHDrfTFJhAgnqtA3D2GkwmHf4TPmSDSetCAodE+om3kZdagYMBM6LAFaRlAmGjOBu/ERI8Ve0l" +
                "luDJGqAvcC2s846g7WGcz8PWYX0IoKMlQInyNE003jOfofWFVFzaZzZBwFsIk5kTDTqB3X9qt49pN1hx" +
                "uzt0kEMDGc99k0pwZFmMlRGKWpYKoBbs+nWkMkr5OV/FEkYBjrykUgW43TiZz9mZgoMFOpQOw5yA0H6v" +
                "LLPKNSapyWoNuZOEgIvKOrZuQIGvveosjzFyOLD4wEIM5bCSkypZbFuMNn8OWLFoHR9HEEgcH3sO0cQW" +
                "1Trmu0J8QsiXnsh1glmeYywZ4uUeSvq3C6BHKelUgMRvmaexqYwABdgZzzhiIwnii2vID+AXcNhg2Ul3" +
                "ipxiblaAngj26uiPNykgID8WB4W6ydOqpAxuXSSaDEQHsYnVHAwGRnpYnxLfNNSMjlwqB0XGCGDV6dZL" +
                "b1QKcVm5ub/0qabFT3WH1LPeoubAq9LVyOR6naKOJZkPYLTC3aNOjy42qO+CoSnFaihtMYT2rKEQqaI9" +
                "yORKGUJwNdQpsd4aBNvziGraRqwgXjHEwgISgntruKCYg/JYNpLxYDWyW4XJ3zSVvhKqMW4xgDqBsNRI" +
                "tjU+sijkRnfpBNQhYiNWL5sUJmSQ7Y1qEWCxkteKN5n1cHeUsJwiPJn2xK8YwKveoic2eVVYE0q3yHIA" +
                "aPjTKtIhrFUXt1CeBpp6o3x2Et4CE4GNkUakHt+Geevd3SQnSwuD6KSTP8HWw5WBkAzH0xpcBQnPxuYD" +
                "TgbqxKAmDkqBQxoIXQBfSvTXxMEexLXvGhXzOi4m+TFBL3y0iuB9ZQXe+4op8BgG5b7bgVtNFBaZQROk" +
                "1Vm2ASg9hrlECydisVpAykaCN4df4hz4CnDmOea/1lwDSaqoxFoVLKvPY0HmFBFIX7lyhbT6idV2ExgR" +
                "5fWaytCYw2HakWl097xnocpa/wGsTHNzuotiRLSEKKEn3qAq3Eks63SxnA9xryyssZAkYe8nZ2/Iph2h" +
                "Az+4A2GF/+QtlrHRApXLXCt+iBKMUuaV533smJDwo0gACu9FjW48B6i8wkIDhYa8hDJdTE/hwg0c/t+M" +
                "Pa4Zuy1UBtu/1ozZ5f+XzNguK8YBKG7XwUJB0FZCnk8GZGpFmGsA/Pu9RbfAUFyAP1vPfiUywUOm10MZ" +
                "vR1YW0oW1uQZdXBmZabKWwVyUd7m9/p8xD80eKBMMgJZDj5QCeSI96es1b9UsKHI0AAUOZvUx7mkQWbL" +
                "FSU4HXzWwl84Q0wStVIYdoNMuZ0UxqPMwB2o+FVQlNzF6DjOFVWeyYqBqqGXIfVHc7yxxpBpgl/DlgNU" +
                "ti7X/2gVugrqsFJPFmx1kSySuG1GyfCby3VFOX8BIg0qRTjzYcBCLOQZand6YjgnBb3FC5Fy2/B4phxe" +
                "1JAo87wrTM+C8PAJeklKZHUVUs0S9KRX17Tu3G/OmYs/H4XVtYxt4zaY5SJx7rzBc/z0Ry2gSOQvXsj+" +
                "dvtIukpGw1zLOlhd5wnN+8yK/BrLyRmJmMb0F1NAdLkyW1C7Hp0GGDurq2ZJ/dmse5zbsfnbwjVgBbOn" +
                "vlwXlAqQJ9eDF0SX+3VXJGD1R876HqNcs6PUYNxz61vW45mXrJOvkuCq72xmikpLLhPrGra4jr+TdwqC" +
                "PaKjqwWZBlkFYYBeyrXiyg5E4Uq7vsw93Ngu+JUUXEYH7oElMd0LYhfelUBmuanB9MBgYTlElV1sYNjK" +
                "TLC3DZ6tpnCg5q7hCMEHxEGbUAzU9jdMLk/1qrrV586ztSGsQEmMyA0OeUWCBwdDzi6x53HoEGM0MCRP" +
                "CzByG47KIChgTM0Gr/yPwEKumKEXmbueDGNFASTE2BHn5Y3xGeAGpQXMD/Q+fEnyPCuE4a4CoI3TAKOm" +
                "gOyQKbEni9KcxmlkkVekCAZKp2sLEHRGprDPKIsNnVaolKdwcC4FAyk+GNmHXS5TpvNaG3URpm6MYIcR" +
                "sAvNEZYptypZLF1w2mIGeMy5uM7y26y2prT+MXTyvi72TcTX5SbdnCIDU7+z6RvpTDtAdP01c01DwAOS" +
                "HoIF3OPWS8fvhPE+y+fNWrFQoFeeSU0ZIlHHaQ//DDGPWGSUQfNd+ApThIBgLOS6MGqsLeY5WwJ3q7O8" +
                "LSmMZUBNaZcqKRz36reWAFc5Ti/YYyKshq0SHPPSAVkcxpNWXdpHWAiol7WDDN14HjLh4aQLpZdNqPgN" +
                "rF3xg61w8FkN4jUqB41xQLqLJVOFzt9YM3e7rpstoGU2HeUgAi5fsaYBx+I44eyJCNfxcbvErXgROmnH" +
                "JfFZjV0/jrUvRobqriVFJWIK5rxFIKM3SV5pCPvUXYJ9YQpH2ZmSwekFsw2E7P2zs5NnAZU3UBsaJ82L" +
                "fMW1p+wmKfJshcEu5tAFplIHCtLwDZgmUgWq8JegzLolE0nc4ZMmg4vxh8HJc7rTeo12CmPWzN2LyhvG" +
                "sBLS2hYrP39XG2PzJntP4EJ9yUucKzt5YYxwfeb24+iULljFWyP5htUU7B/QwIjhm81YbXs7VfOSs1Gs" +
                "hoA103mKtALSWotRW9NYaaBkzCgSbY4QwfFaFS6kxxk3VWAAahfm9vFDGcUvG5Vg7y//EdzWw7nOv77Z" +
                "/EHinH6+nUVGkyokc3J4xpCBGcOaFGarWnENBSPGFQ7BYBlkwR0SVyXgaj3ICTZEGkHFtXIleP+EYx5l" +
                "of11namwArUAi5WJeGaNPUCxAOOZj4pxsFQm++lqPHqKE1amdvZ7/+LcTMb0cKjHClJcK4CXYtr5NFfS" +
                "oPogO3XrUHpiQFFDkm1hOukR1W7y/BrilWt1LJ78cx8pvH+8f4qRzdnr/a7YL/K8hG+WZbk+fvoU0g+Z" +
                "ArXL/f95wlcsKGLKci7cZcYyMvdMdIPM8aiAkWNS7sOmJKJk+VopM+w7T0FVZ0ma2HqP2iavEQ3AIBFt" +
                "ynz2mmWDgOCtUO/NyVzyQuHiIVBbAKVRYiyImstS54bAHAtHAPoOSQDftUlw/N2PP7zkFeh6uUIA6+5j" +
                "vG9OuvrlXADbtMJ2leNT4+CrP9J3dgXDpqPE/u1CH73ib7A5eCy+e3n0gj7iiBUuSDDMNSvA7d/mRdz6" +
                "GiMUvIg9wPY5+ekqj6sUn1NVoMzX+1agQbQfqiy/K1oAjM5YTWf5HeSAa5S0rog2EFpT0BZhTdQUFm2W" +
                "UyjXiQOxsgVFiHBmNgQAYGjw0aWTJnLg/KwL/+sFZtLu9fg3cGNmoPby3WAyANfCH09/Px+OzgYTMOXm" +
                "i/FocPLSaru1T+RlECeziqM0axIScLTatt/rpXUnpF5h92BVCtH3N3jLjrnGi+kKdY2ZCOyqkVx31lLt" +
                "13v22bkFRjTxKVycUOXs4beu+J3L9n/3cZZmEC5V2aJ0deW2DdI0d2YeAtF7NW3D3yAiqT/97miNn/6O" +
                "XtxDielvsKJiF7IdzSb8zNyIXdcYFTLFfO9CxkmFKJg0hyWo1+BrOOmfDd9fYYTknWmZTDCRwfz6BFOF" +
                "RYfKD1QztOEhNV3MUX8XEgKOnqiLhQ244bvB8O27qThA2OZDp74Td+k9itd3WjbSK6sL4gB1ocPnoZ2z" +
                "5/DtzDn8wTtn1ylYRLS0Y/aZ5GT7mad5xsUA+wjnqVyc39ZJbPAkBaXAPEhYJutahoimuB+TTZT3at01" +
                "7axvDVGDliYa+jmRal0e49FaU+8trglz8mCDPPeTAEo+i3v9RgxG27Uv4haGB/yc5wmQ2l5xsycCLtK6" +
                "PqtXfffWPdYFk8xVLhslKX8eQjKPm9f9Qgn24V0QppbW8XioYjqJBgKya1a+IpHZAiKIv3kW9kamlcL8" +
                "a84Dz/WQFdwRO5oQ7OiPnwI8Y2oAUPvIwAqM8TCFO7vD5l7XOJZDCwibLTSnrjpveiRS2WtsIZm91r6u" +
                "keIXvj4eMZ7qLqQ64KNgS3n51iY/N8FV1+T3df7vigTyTnyL5b9vRfQn/BXjS0vILCmOT0DA1fzjs09Y" +
                "UXQfn+PHyH18gR9j9/Hok2s1fHz5ib57KAJ8oYbXqmtt7Xu2tlhBI+V9MMZ9AW9rYWgOoF5rLErd4lcJ" +
                "pX1OET92bf8EnsIHGeHbIJxt60/Ipby5midSPrmauXcWuzJ+6YPn/SkOdWURYw3Q+9mqA1YHcR6gwNSl" +
                "2Z4mG9G6ZQ+uHmwZotH3p2hwtK/+snGt+/M1cWXLD+D7Q6wB2eH3h2HmvTlWTwC/VGRmSSx3b2hM8ngb" +
                "21OkHohHktkdmDXHUewLIjgUSqJjXgNttdzN7Ixi50Jv19Loyha5tNzdUrTeFnKYUZNDoJTtNNWwDnja" +
                "xA7atCd7OlYFeUU9deSBoJZOkkVpFSuqndJciBfL6Ke1DD9tSu6emew02kL6Y5IO85WD5mlS173F1lI6" +
                "uwxL4sL4Wl8DCVhvx/jHDm7+Z8zh55CxPGmzFWsiVsT8AbIDDCpy8QqntTtfN+Xiyj6mbCrrrnRt/6xo" +
                "5jjM0JqA3TEn4xmze0dkDav2r53TlLL/qD2kt4R2jvRL4b1UxNpKLxSxXtzS0NSMO044qMVdY6/FqLL4" +
                "0LUKgcm82VXHu0YxMexZ5reCWutyvS5yFKAEni/gbPpFlRFllx6Wrihp0AW4G5qGa9dtpXjCB5OSPBEH" +
                "sk4ExiOvZo5DyuJXuBo2Hsw/UFBSALxjVqzu0q5mmF9uPd472wxwbi8tW1nh7Ju7Ik3yYSpWmckyghra" +
                "hM3HsMemlnqBrrfL74HdawVSu9H0keyMKKX5aMKsZIPJPlwYOaECSJ1YYHRB3P+SHwUYoUPZ79d+FQb2" +
                "dO9gbkJDOPEXkNiKQHtGvkH0BgJWABOXTKq6uPMExB78GvXin2yD1SVZ5dyKZ6UsBCO4NJjwTZJ9c28r" +
                "HrxZJ5HZCOc71fPBcOUEX+AQMRgafCW0wHyRAM/qKhCR9sA/oGPGBWZqIbPelqbhlfmnOGqps+RTGqwW" +
                "NXdBbWeSK+c0u8zvFrdEkAYq69dPEF8lteE72oAnizyPn3CLredSbz4X8iGEbo92lsINx1HrEhNVc1ug" +
                "2bXpmZouy1swKGtVTL2huFo0LUB7QAFEULLcAp/fcbfWEH39DKcV69fVD+ounJ1B6Gw9HLhlTjentQ9f" +
                "5fZk06fjwT03Q+HZr78xoT3zggaJjWRzAsPJNE4yqXTOreedgLtGyH3DZWdlHGCupjFUG1NalCWxoloz" +
                "PnC1qsi4DeWPEvMrwF2Qj3sXIULeyCSlcUG8AmoM1pjwrN5O0lIr2ict1S/kXbKqVuw4otLMfOEUL/a1" +
                "lklqroFEOPjvk2f0Qk+i8egOi+TRCwQSGgAhz3nReLobbHa1mnwGuhilyr1Mzb5Dq5XMcDD+njdwY+6m" +
                "k4Rqjs0gFT9dVxp/0HSSTeuivCrYAFik6wjSzB2FPCfk3il7mLhip9kgf4Sf7XSHzSG2pxn2X99pJRAE" +
                "gE3Pw+DfjIva8oQlFNPUaA7V1vpEW7puAIdFnlJ6lEKwHPoak4p5/QoE/Z4m87JhpYCXVMGy/1aHtUtW" +
                "KUunBAgYGW4EuzZURqP8geitU8rWsLudDhTPDjRuSl4ls1VS0AGzKHQPrIIl9WJTPjTzC9482xzNDF6k" +
                "dk04+stWSQuFHW2wnTjsACLk1C7JvOMeRhA+R6mtY912wvNfEW8Lg0fEg+B/AcPWVRkMTAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
