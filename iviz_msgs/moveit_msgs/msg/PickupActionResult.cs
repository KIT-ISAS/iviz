/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupActionResult")]
    public sealed class PickupActionResult : IDeserializable<PickupActionResult>, IActionResult<PickupResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public PickupResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new PickupResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PickupResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new PickupResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupActionResult(ref b);
        }
        
        PickupActionResult IDeserializable<PickupActionResult>.RosDeserialize(ref Buffer b)
        {
            return new PickupActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4c148688ab234ff8a8c02f1b8360c1bb";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1c72/bRpP+LiD/AxEDZ7u1lcRO0tZ3/qDYSuLWllxbyds2CARKXEl8TYkqSUV2D/e/" +
                "3/PM7C4pWm5SvG9UHHBuGkvc3dnZ2fk9w7w1YWSyYCK/GuGwiNNZEg/603ycP3mThsl1ERaLPMjlV+My" +
                "Ht4s5lcmXyRFkMmvR43jf/PPo8bF9ZsjbBkpGm8FuUeNrQDIzKIwi4KpKcIoLMJglAL5eDwx2X5iPpmE" +
                "iE7nJgpktLibm7yJhb1JnAf4MzYzk4VJchcsckwq0mCYTqeLWTwMCxMU8dSsrMfKeBaEwTzMini4SMIM" +
                "89MsimecPsrCqSF0/MnN7wszG5rg7PQIc2a5GS6KGAjdAcIwM2Eez8YYDBqLeFYcHnBBY6u3TPfx1Yxx" +
                "BX7zoJiEBZE1t3OQmHiG+RH2+EYP1wRsUMdglygPduRZH1/z3QCbAAUzT4eTYAeYX94Vk3QGgCb4FGZx" +
                "OEgMAQ9BAUDd5qLt3Qpkon0UzMJZ6sArxHKPLwE783B5pv0J7izh6fPFGATExHmWfoojTB3cCZBhEptZ" +
                "EYDvsjC7a3CVbtnYek0aYxJWyY3gd5jn6TDGBUTBMi4mjbzICF1uox9Hja/GkA9Kx6MGP+Nyx/hFFHjH" +
                "3zuZ0S+X7c7pWedN4H6Og6f4m5xpZFkwCfPgzhTkyYEhiYZ695ZGujmuPfsEQVWYrZPe2ft2UIH5bBUm" +
                "L2WRZSAu+HBgSKYvAnx51W5fXPbapx7wwSrgzAwNuBuciVsHh/AJBCAvgnBUgJnjgqfPeEfmVkRhNm6U" +
                "iN7/2cL/4BOhgvIcBHOeGEKIi9xBAaI7PZNNIYAJtUFhdi3K1+9OTtrt0wrKh6soLwE5HE5iaIkIrDgk" +
                "FUYLqoJ1hHhom9ar7lVJF27zfM02g1SOHi2EM0vc1+4ULcxnSUOuyFNIwiiMk0VmHkLvqv1j+6SC33Hw" +
                "4j56mfmnGRK/tehQptJFUWeXvc/jODDDEGpVYPrNFlCVRQhMqSSgrOPZpzCJo4cOYDnPS8px8HIDnOdZ" +
                "b5YWIoQl8/nL8xQ+aZ2fl5J8HHz3pQgODKyVWYvhl1AXd3L/tlaRno3ibEq7Rgvir0FUMzEx0cohqmzy" +
                "/b/hEF9GZjLFivjpBrQcD/DEefe6VwV1HPwgAFszRwxrQAApiHBrBGKUCKEnAaE01RHIweBJJHQbfIHs" +
                "5YSdktok6TLG8SE52GtVdTa2WkmSLsUl4USIAj6kpb0CMtZWUcaCinfFJZEZLMZjktFOKsxt0dioNTs7" +
                "pZNFJlBHxNIpL3jjPJJYZlB1OYnhYYhVrmgVYRAT0SM6EwdGfKw1pMJ6MyML4aAmJ43g6JjpHNeVJFhN" +
                "mLne39Jgaw/acR+40mTUKoJR1WGw+EPBWCcD2hjo3a1exMiYaBAOb8iQWKGOLJzKPA/HRm8nn5thPIqH" +
                "Th4Eg7xpodPj0wlAaroQuYCqizGr6e4Ps77e7U3Bj3GhV1d1yh81Go0LjJ0V7SxLs5OUFDD82B/iM0av" +
                "0kFaiHyB/CEtQZrd9UWY3WjPP//wsTZpbPKGPeDqGPYZZvGct4wZjTdZmM+DMf/Gt1GShsXL58E8CWcz" +
                "LO3zxjZCmzotHjXU/YYRIq9QqsFzuL1J+ClOMzsqzsT19fEz+/116+z83VX7+Af+NOzDy/NWpwND0Odo" +
                "+/R4380+67xvnZ+d9i+6vbNup895x/sHdrDysG8ntmCw+69+7bc778+uup2LdqfXP3nb6rxpH+8f2mUn" +
                "3U7vqnvu93pun7/rtF6dt/u9br/187uzq3b/ut257l71AbR1vP/CzuqdXWCL7rve8f5Lh71z8Y73vyMl" +
                "3NUE/xHcQHtOQwQ8XqiVg3JHnV7rqtfH3702jtA/6cIeXuNQoMDTNVPen3XP8fu6f9nqvcXsznXvqnXW" +
                "6V1j/jNHzDfd1nkd2EF17M+gHFYnVobcIt7N80btdt5cdd9d9jutC1D52Yv6YA0SprysTbnqvuraI2L0" +
                "u9ooPISfHPDva2PdV3TS3Cj4CcrkDuptukrm11eY0AcCnevX3auLvmPC/QPHaJ5YYJf2yU/kRfDDe8wj" +
                "U2Cio2AFV/4tY45olmHOOq+7fgzE2qqywQpenW7/7Kf+dff8HTkZLPpsM2quVFxqpWBgnMaG50NrgBB/" +
                "BvtApGFr4IVblynjSgnHzF4QN01Tns7TPBZ1FaQjiTH+meKAuTgpiFdv8gYMfw7FKbv/yEFVmzKPyrCA" +
                "Pt0KZMSaqyksDTSJgU1IihguZHDafR2EMFmluUCuwqyAvuBczKtsIcv7UTrq1zZrwZkeTgBlmCZJnPOc" +
                "6YA6GJF56MZcsMBTBDZQFxrsNtz6E7e8K6uhy91q2Ak71LeQue/rJIRFm0VMlIgHMzGASkeH/tQQaRUa" +
                "fI1dmdHIYNFpsBkNBVE8GqlVhaUFHQqPYSpAZH0lSzNNcwas8XSOOCqE8yVZHpdGECfYHXWQRnQhdhw+" +
                "mEi3jomdxCDbsGYy1f4IWClvHR0N4VEcHVXMonUyFnNkHMSQF4p8UWG53cYgTelX9nm4rycA61nQCwDT" +
                "IV4KhAMnaYK0jWa/0kCtMu6EdBAm0rPnCBfwAZYb+l3EJ8Otg0QqA02ktEpPUBcZ0FCHg53MfEqTBZ9n" +
                "8LPiXNTELrGJzAhqg14fM1ZILFUlTbbEdwcljAhgurtXTkXGDD5acXd/6pNcJj9BmosSWi4xI1xX4bNm" +
                "4XyOaABZg1kVQGfK1Z1dOKdY2S7PQjdV/DYyXARPX4UUXitVwgw5JUsITZF6Oc7XOsRuP6Fa7rxXcBiy" +
                "Xc0A8cG9OZpiTCE/7hpFf6gkuaXcQm5QkmHYU49R14F5DBfVMrfTP2GWhXf5nuxAMZJrZD5zlcKCDK99" +
                "JXkELKbhjdFFdj7OTg5LxdULk2bwDzrzpjluBnfpInNaVE4xSwHQ3k8tbUdY0z0ukbANwspTl9cpeAcM" +
                "Cu4sN5J6ehq928rZbaCilHN0yuM/oO5xZBBS4VSkhrMQ/dy52MDzQBkklMQhF3ikQWhk1IYFrbbcYBMO" +
                "7moavXSQhX+s94uvThAqjxzDVx4pBTajU+7bHuqWK8PMM4RBLSluXtUAGcjer5DDc1lkxojghPdG+BCl" +
                "uFrAGUGppEuntEGVxbBg9grTyg2VlzViBPUXPoEROhFlCt56SEL8fC65aYZ0DEFmOY2+rhmbolQBABsm" +
                "qd3duzPBcAJfoRm8pjTcImhMwCahOMAwGFZfwG5h23dXp69FrR3SjO/cgl/xJ1wyt00lhEwZ8l0ySCYm" +
                "o1Vy9lXslJD4lcWAomsp1CvjgKozHDTINAIUCXwZreLAKzj8vybbrCZbIpOO5V+qydz0/0ua7CFFpm4o" +
                "l+eNsYHrViDmFw3Scyys+QD9fG/SEqaJE/i7NvYPIRMGlV5fT+89gLd3pjKn9axt95plYIqlAWsUy/Re" +
                "/U+ukDoPnkE4hDJrvJeMyKGuT1Swf15gQTajDshS1aqbOqdFZ90pQ1gfDtaOEHh1LHw1NXTBwVl+pbj0" +
                "5BwcQzJimXjMiK2KIEpBErjgossgcDQ24s5QKYMpq2ThYyzZocjtaVJQZtFgSPFVyrXQ2Fk8RkavpkxF" +
                "/dvT7QXF6ACMDcESnHUz3CKze5bgu83gbCRiuuSBRMSdn0wP2eIlhYoiTfeYvrQgVil6KaLkJBZhZwFp" +
                "aZZZrlv/yVv14I8N3XbJaGsvHLYcKVdn11eund9+L9mUdP7cmfyn5caElvrDn8wZ27wMG1aPNMjSG2aa" +
                "Z8JoOQNiBoU0v+FsLPV8GhAoPie0dkr53c7b1AFVGa67O1yIXlJ5vj1IF/AXS8QzMpb4slMKsPKrxoGb" +
                "SeM8kIFwlYfaY5XpQSWIF+uFxo/41oWrFGAxosx3uOw7P4u9QvZCSOlzRLaItoBjkE9CNIgIpeCa45Or" +
                "3dSwaGypjqhmWDhNNtyCVrHlDbkxHlZAzlKbm2lCeTFNYgq4flRRNq+ytQ6ey7JoEOqP4QmhG0SNOqEU" +
                "qCuA2ABf8lhlOdDv53JGzEwBo3RpcUgXMMBb3BiBfMiiyL5HTNGgk56gnyW604gTboJiahdUigME1tdM" +
                "Gi3KyBdtFCsJjuF1DzVYX+mywW1IoKD3QUukhxQrNCUMfxSAtgYE2s2A7Aif1KoNE4RbNCVZuhBZsFB2" +
                "91xWQvaYodYJVZ7dyW4ZyqTSrMP2FbpWujGvj2Uwm76rFD7K1ExZNmEVEtj17RbuUpYGnUreXa1dBqzn" +
                "KLiZpUsfHdr5mxHLNeLYsm6gWMJIqONTey6mE7Gpe42+BmdPamm4IwwksHCBWpnZrVbLdJ27avRuKV/Q" +
                "SA9CGOPUEsgLkP7uM00ynklkrYfRM/QIgWAc5DJnapUu0zhrvHkntrosFnbkTDBLPYspPnoltesIcJ2y" +
                "ycFtM2SWbAoEP4FYonQUT5l16YaYICin1X2OfGWcrMVS3FZwYfLJKlQ+wdypDqyFw7ESxCvKh3R7IAZm" +
                "NhVVH+sh5OXp9nwLgkxzMao6FDj8QoUNNxZFchdQsNxit4rbJZfyILLTA4fkWIldK2KsX2EMpbovWEn2" +
                "WHy7yiTwKOp5ixxeoLmFz0D04WqqSRWd02wM7uDEt05Pj59ymyvRqys7jbKUaQUEXbNPcZbOpvR9mSKE" +
                "krgDlRCboxNORUGS/wXkOa/xRBzt6k5X7Yvu+zZqijzTfE5VRRfWcbPNeVjdKkjb8PBzZ3Uuty5y58Qt" +
                "lIe8ZPvZ8YHVw+We67eTXfagGJeW8+1Vi++/I30l9t5cGOtK4IkZFRqiMkUChZanCWkF0jqNUSpUJKdB" +
                "yUhRFNocEsHuHDVZ5+GzFc5kdEbdxNQNfz29+Hm1AvX4l38CrfuxCfSvL7Y/pM/Jnxe7RG9KDngkZs/q" +
                "Mmgy5qoYwsI3kEwdXUfcosmYHhlr/cRnDzSRD1ZhuWTFtbgxPjtf3eFIm15kfZlJl9hSOAZKC905A6fv" +
                "AcUBjAZVVKyZlfTZj9fdzhP2Ytmc2q+ti3PbQ4NsuudiaFovA5Wg03Wy+VSH5A3VtDub0gza4jswB3/v" +
                "1kWUJKeTpjfwWm7MUfD4v7dJ4e2j7RP6N6evtveC7SxNCzyZFMX86MkThCJhAmoX2//zWI/Ihimipwm9" +
                "mVWOenvWx+HlVKhA/zEutrEohtcPQbgxxnYGjxJI6yBOEO5YC7WOYVnVUiK6IPr0lfKGAOGpKPp2Z02F" +
                "kbm0XdQlRqXvmIlSe1gp6giYo8ATQJ6RBHhWJ8HRix++f64zaH01Z4B59zHetjtd/3yOIha8BFay/D2t" +
                "bHz9e/LWzVDYslWwvRznhy/1CUuHR8GL54cH8pXNWJwAJzpd2hmw/Eskc2qP6aTwIG4DVwXV0WkaLRKO" +
                "S56gSOfbjqHB2l8vY/+Qy0A37VQldZAiNZzPyWx7wfAOPra4buA4E9icowt3wBmuTgfOcrlG+DkD5wgA" +
                "GNU+DbsIo3rQT/fwH5IC2pb3qvsLjJntvr1820ZrwoH9evIr+h9O21dQ6PZBt9M+lq6CXkVFia0hTnaW" +
                "+mpOK6DMhfjC1ufLqWWdpJzh1jBVRfSrCyrTjjT9y7hFyspKBDXYJNetU1bb5ZptNXFSA7fBIQ4uqGoY" +
                "8cte8Ktm9H+r4kwiS+RkZmO4jBajuhpi/OTPB6I3S9r2f4FfUn771dOa336jLa+gpPS3WEkGjNdOzYnf" +
                "tsrJJl6rV0Qb67lRWY3RgZiObLyjHOTwULj9q9bp2btr+kmVPd0lC0xesL5uoVRR1pFUhCQSnZMo9Ri7" +
                "1W9BCLejGZQZxBW4/bftszdve8EOYdsvu+WZtIxfoXh5pslKnOVkIdihLOzqflR1bh89nd1Hv1T2eWgX" +
                "ZhYd7fT6bIiyfk9Ybc0KuCH2XHlvvy6TrP3EmcTC2nKIMlvJQ0JTrmfUSX5fzPdspetbS1QnpDViepaq" +
                "HZ5eaSmp9yaXhMHEDWXCGAxoFJrdq0bSK62nwuTC6CTouDYckOCVjCdS25q89YXYSmK+Mm9zZwQyPtm3" +
                "kqGq9kwgNeIKoeWJP5Oa3YQtYqDpLVAFW0aX1BQItlUKsxiJSHgT/1lRtejARGshLnekbdJlOxaOyaon" +
                "HJ/8w8cGN+lZAFJisrC4QSWV51a4UAxe4IIOk/RVTO4FmiCnFN910cao5Q6yjmruZHD6PF76stiHQ0XV" +
                "3PYlObghhCVWX98OoOVySJ0G/WVSwGcOwtvgW6YFvw2Gf+CviC888crC4OgYnG5GH55+ZKbRf33Gr0P/" +
                "9YBfI//18KOvRXx4/lGefT0afCa796iW71pbJK2tcRwngpz/bah7hSMdUOVkq2DK5ibkBRkOeqH8sOdq" +
                "LBjFl3DI90k0EM8/8q7S1dnaxPLRZ9Qre6l909dG9HUBcU59xsRqBppEl5Bg4pCdUKhq5LVytuiL2jGb" +
                "OHtjTd9Nfr/xhg2B5cOVY91vyYkWLjMBh6DP9JBtnN9cA2yVDT+XhVZ+LGfdW7DS/VNZWG8/rYDYGOc+" +
                "gNuj1XY895YJG0qFgewbpbUqve24MWpu5EVdad1bw53ujtdktde5IrbVbh/EctWoEtaOdtu5RsN6P9Cu" +
                "E0SdUfYqVUBI2SeeDZNFxFPYbpKKj5M/KTn5ySr/btmWUCszIkU2HrGPPLSKPGmQsLJIRc9NY86cIWVd" +
                "DgUYPM+1+vCB6/y79OKfoeP1Y/1mmTRxXFbtPNuhp5EGL9nsvftl7TE+L2RTq2FZwi4VoeNOeKZjJqiq" +
                "bU4PNNhUtNq9LfgWVske/9o+q4z2NytGedno4ZcCUEEqX05SmZUXk1Q6ltJwxde3oEfY5KX15UoxEpn6" +
                "fV9UxD3rYp9E37PiSUdoki4xg27OfJ6l5KEY42PsLR9MMZTws4KlT1xadAH3TnqC67ndMHisG4uoPEaL" +
                "YhkjdDuV1Dp7nNHKBt3HV+30XzxgM/KDfWZlPXc6YAC6dvvK3rb5c3362bGLhudaPFklHwM19o7LbEKV" +
                "Uk+9E66pCldKhr4KrO+T3asYSlXSlptcf6nkAajIHHNDce/r+i3NkJQBBz0Nuf3PGVTA6HuUq5XdL8LA" +
                "7V7ZWMvVcC3+AhJrEai32K8QfQUBx4C2dVBo77M/j8H2sG5StX+8Dtae8KrGXNph5SBYxpUWhm/i2Tf3" +
                "lnLjuzlaG3Qh9veiVwWjqRW+AoK0C5RIgr5AxpEC2L6a4Um7U91g1zYWDMw4nDXX1Bav7b/tUXKdI5/J" +
                "obikBgyxHYSaXZe+Z31TucaC0oxZvsBCfNE0YO+dOuDxOE2jx1qJa/qoXPdFhETobmuvKXxLnVQ4GcDa" +
                "04JmN7a0aisxb6BQUAbrVVrpStZ0AN0GqNZDvos18PWNeacNafEHbHMsX37fKYt1rlthd+3muC27u92t" +
                "vvk0dTvbcp62+/lui4r+YpKA/FmqFyokVZKrvRqep9n2ZJKRVqgfBMzXTDT/UCou11XjAWu6TaE6z9Kh" +
                "DI0rr9oqPjjaImO+DnSrtiHr28TIxduupOp2JOSnME6kyZBHoMQwA8W9mg+SVirWVdJKXgOJvuliqoaD" +
                "1WppEGMHMGtfkzixxyARdv7r+Km8DxTn3HpXWfLwgED6FkBfm8Kktd03RfscTjqALA6Rd7FW076nkqMb" +
                "BznI4X1r4FvkbbWJYs6CkYmezBfoTIieSB+TC/GGaMdUBeCQLv1I26HU144i1/yxwTykVRyajuQD1wfi" +
                "gon18Yb7F31qkYQAEO2zIe+ozlQ8h6t9rDbkllIla2znD65IGV+CfPIi9EeOti6+4eFfopDPSTySjHTZ" +
                "YpCOJLnl/v0Pp52caOrAyALmtVv2LtWVlatqP/XaFmen3v1KD0obDVZOKrYFV+clwU7q+wEnZji9e2aT" +
                "i7bZodL/NqKy4UFKA8W2YdVNyKaw9g0Nys4IWHgvfPGs3G6z7eAVjr7fFe76Qv8VJncwtMMcp/tfTdMf" +
                "F2pMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
