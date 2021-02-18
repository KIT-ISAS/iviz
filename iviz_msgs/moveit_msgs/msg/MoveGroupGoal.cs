/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupGoal")]
    public sealed class MoveGroupGoal : IDeserializable<MoveGroupGoal>, IGoal<MoveGroupActionGoal>
    {
        // Motion planning request to pass to planner
        [DataMember (Name = "request")] public MotionPlanRequest Request { get; set; }
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupGoal()
        {
            Request = new MotionPlanRequest();
            PlanningOptions = new PlanningOptions();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupGoal(MotionPlanRequest Request, PlanningOptions PlanningOptions)
        {
            this.Request = Request;
            this.PlanningOptions = PlanningOptions;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupGoal(ref Buffer b)
        {
            Request = new MotionPlanRequest(ref b);
            PlanningOptions = new PlanningOptions(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupGoal(ref b);
        }
        
        MoveGroupGoal IDeserializable<MoveGroupGoal>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupGoal(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a6de2db49c561a49babce1a8172e8906";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+09a48bx5HfCex/GFjALfdMU2vJCXKbbABZu45lWI9oN34JAtGcaZKTHU7T8yBFH+6/" +
                "Xz27e4azkn2ImDvknMDLmemu7q6ud1W3R89dk7vyVWHK1/bn1tZNUvHf0WiEb8u8XL7cYJs62cjzzPGL" +
                "0ejyH/zP6PnNXy6StdvavJmt62X98GB+owfJ7Sqvk9pW2zy1SerKxuQwu2Zlk8wu8jLHHsnCVYnRxSSN" +
                "o+9rgnYCMGgtFpqUGX1xbbNpmyRvkk3ltnlm65MRtntlKrO2ja1qgohNd666qzcGxm5WpqFXCq1eubbI" +
                "qEUCcwIwJ6PvtXkEyoOYbfxLHu+mMVUDOE7qxjQ2aTcZ/KmnybNFktoKV5r83eVlU+tYc0IBDlXZDCHA" +
                "lDauznnLHMwOZ21KmmfaVpUtm8SVtp7gm9pGjRkiwgCgtW0S6AsLeO3mrrmh6dQ4uxlNjad7uyIAdT4v" +
                "bLJ0puB5B2StXWYLRD+iCN9Ok2uTrhJb2DXNZIFgsKWpKrPnHYT+hqFVdglTm9I49AJ2HrrndsuLzRe8" +
                "Lph+UxnCC2/8BtGYtoWpBAbAh/mbJq8XOfZ9Grq8eUuwZxEUXt0LJ9sAODXlHlYK3xJTONgf2nbTrGCf" +
                "+Xflsja1mdAYr3eXF0WyzV2BQBjd8VTHDVKy2WyKHJYMSDLQnAaB3Sldk/y9BdrdmT2/O+vMmkY/nPNt" +
                "Dx04t8rWbUFUBW//btPGVftkjbAZIfuT0a3/EA8Rmg8PVALtwhZGvOVZAVbT1pYIt3TcELl2Y9McN2BC" +
                "pIrbbWBq/c6ENyBCgABbBePi3OXjLM+GJ7CsXLvBB2EQgLdb5UBrhGUFDT/dxlYG0eFBU9cZAotAt+s5" +
                "tEfg+Rq3R6EgCFgdMd4ahIbNpsnNylUNCpraFa3KGF1FZTf4NZuejGBijx8h6JkXpqZp7HoTY3Vt3uXr" +
                "dp2YtWuJRWgGQ0hG6ikKtwOyi1gsGQNN1hZ2LEOaWRTONL//QluGkRGqSJ3UFIiHhcHNZh5iIQ+d9rAA" +
                "IO1ciF5nR1hOtrZwKcgPZNmS5U+aAnMjgoFipknyRCa4NUWLrYANYXrj88nnb+Hr7X0Q90PwAv0o21Uo" +
                "nETKIOg1EjryE0jGPe6ZRQGA3Zp8axGerBHGBtIEPYMCEnuCBkApilDzCmebZ0llyiXMeZyvcQdN2RT7" +
                "CRECyZ4yLVpQFLDFJLAtqZnz6fkZiVYZiKjeeg2kBE/YwK39fHrOwEAHMLbH+dROJ/dhBSB2v8T4OZuG" +
                "7YZWM+01q3mDZzynbqMYwEHDk2Mo+QEVqWoeOK82y0jNG8ImIC5oThQTixZ0A3EdfCZRB7IAEb4FhgTm" +
                "GZtk7t6dIdmoUFDi6TIRzmt6osPnHrgrYWt2K1t6M4i4ZO3meWFJ3dQ4K9VmDNqAqrdFMWUuuyLdxrRR" +
                "ieCq7AKUbAnGhGpMmCastirJJPjaGlDsoIjxTyQhchAhQAHcTgUg9JyA0AGdJBSvRMeTqdW0AIUNnLC0" +
                "DtAHkp224DsS848R8oyhHsqjjzAakJ+O9pHorG4yHpNRySZWmZkKNLVtDJhXhlC/ypcgCT4rwLIgI2a9" +
                "AXqgr81+AxZYRBFLCxMmVkX9hMsGRbBuyzwlNY/aIu5P3N01SVLnqiwvsTlRAkIn0gVrFWnh2dUFKXGb" +
                "tii2YCQQNpU1NSL02VUyalmRQIfRg9ud+wzFxxK1rg7OtilM1r7bwP7gPE19AWP8Oy9uCrAvVEkkY3o3" +
                "g8f6LIFBYAqgs4BLUJO82jcrx9J2a6rcoKkHgEFQFAD1FDudnkWQSwJdmtIpeIYYxvg1YEsPF9f0GViw" +
                "Gamoul0aEqpip4ucBwkBQh/EYZHPK1PtR6QyacjRg6+I20iB044gx9c1SEbYgIxoWA0B2g2wMD4WNQ6y" +
                "gZIW2AmwVRZtFwMCn7UHaPTKwkpQPE6RSJ7RtpI4WltQSUh/vieaVHmFyk6sZpAvrrITdGwyB7oKrEqA" +
                "sTZ3aOEDjsnqBAMU1C1ae2VdsKKF19BlbKdLUEUk9qgV4ogomnggT5MqX4KepJ4w0Np3NoksDnTh4hGb" +
                "QjRnHgw2DIBUrhG1hbp471qwdWEN8KMS1iMzS+dFJNI4N0G+ExBdhL4ijag6A9RFA0w/HXl1987/2vtf" +
                "vxxBwQUX6l69lpcBf2YOxkhXlja4h2AXsK8R/LsFbFOtFi86s8Akd6A4cHNdxaN/g1/ZgaOGsQP3jRMv" +
                "AaTFGvyvldl6I8omVy+/Yr/Jm13sEsbQn2NjaBiNQv1nmVvMDsZ70jTgvwGg1BVFXuNq3Rx9DBAKRr/B" +
                "ttewrbSWxEWGHlizCuCp9n9J3cGH0+4zD3omoHnorwqzBDRnKKaRjoGqxTtGEy0Fgg6WPZljwFMNyU3i" +
                "qsXiQNvRJNnFpv6x2bB2dYNyWy1HVjIqxcjh1eXOXYb281gnBA3R+UO9UlhTDTXGkWDvjVDZxQVoUXtx" +
                "Ebnqc2J8DiDgiMDNNP0moj5A59w5dKpnuL6Pp4MHiTFClvH8QIS4ckVWewkAJnZa5XM2nNgfp6WLHQhC" +
                "BrQmMVLlSHcwN6CwFAPGd7IYemCzeVxZNA3xfQW6JK+R99IznA3HIFCxoMIEvRbznJo/CsVkZJKfTULT" +
                "YKv3mz6sqfFD0LLIq6GLXcBuNV5pc0jAOzkC4MUae784m9LCrsNa0Eotc8ACElwGaoB5FRQjCgd0awUR" +
                "bEd6duboAGlKGDfYxQxVIjk74HggeiCwDD1dcOcO2rCF44CBdBtJjDAraddEYmU16eKcjJ4BaVjnNVvS" +
                "JoghCg3VExoB2Yi2Ec2pLoZpMqQj43AR2vSo7qiTtIe1I4WpiztNvkcNh8qOlY/IU1pF6QCg7E/PakBY" +
                "6wkprhRcb2DWrY23k0Na6N3vhRoRe7wa3tto7RLPWykMwlOd/wKCH5YMiGQ4EddQ3JLcEhgF4y1KA36a" +
                "EXIoUqKTRlMerQS0OWkHQUd2fY2RWEQgV4l+RFvCozJC9EoJPnrFGDiGQDnUP7Cq12oQGeVZlgFIPbK5" +
                "hAtPYpldgplFhIf2VuZgXzFY4NAzVIkNKGnTpq1IloTxmJDZLgPUg7uZMScb5U80/+s9WCNrxny9IbuY" +
                "7CRvO3GfpW0C/2P4w0cb70AokZBK0hWYDNPkK2SFd2DhFkAjhvxSU6mwMERhf3t99RXJtMeoysfgse3h" +
                "/2aHdjWHvsFI549IwUhlkb8Qz44RCX+qHKBwX+TozncyLbGFQgOG3lqMfSVzk97hgjtz+H8xdlwxtsNQ" +
                "w+pXizFt/n9JjN0nxdgSxe51LxhxqyQMrTw5HzTawYZiA/zb+/Y9oQk+Mr6O4zv6WQ94j8QOXqzMbbOz" +
                "QBfNzh0EHuqegzkaaWgmcgZHf20NRidRAKjLdpxFhoGHfGTg1Sr3Mr6zEHz6Ocwa8fBBN1B/7Y60gURJ" +
                "siyVunUwHrvrmVfuzuIiyTGv0TVC1wDlsCmXFFSiGOTUb6A0Cc/S7jirY54Y2DXYCt6esLgJ6HOM5jZk" +
                "8TYoqH7lEglYeGRX4BgR63u8UJHZvbc+EeSdOBJgBuT3O3VXOHia06rvfF4KH0JailDZyXcbsEYw57oy" +
                "G07AULQ1ZF97EwEYrMxiTxvb8aAPni0QIjET7hkumICWTpz0aZ5JPmNCuUrvXz8YgqjONutwv5YoL4Yj" +
                "UC62iy8ns9UAtHh6FNoISXc/pAYQMFIRRfIb16YrymbvfdrlMz+5maYkTVGBrthHWfSow0lQIARuJuEV" +
                "BBul5HlmZGKAFZay59aJ+MK+kOHIO4MBPF4qBe/WCMMvB2FL4A1EnAX8gzHN0cC0cBQCNpVriS0EjGSc" +
                "dJDSpqiNqz0NV9mCI8eam5ChcSMBqA/qRMne4KmHvDCgBCc4k0HC7uxsvlx5G6a3KxNM0d+VbleGpBN3" +
                "OEpm6ZA/n4hpMOHg/YJirBLsUTufmOje5AvwgKxVEDkmUiJwsI/PYfhnGKkSHo7T1brp+41lCsEYx9zU" +
                "5FAQlgJL8Y8Z2p1LrmnhJfFKbhEEwomz/hpRE1FMSdlDU095WfrllQgNZJ+hfE4c/AuYuHGYINWRUgyh" +
                "rHNMVmDwEeURz5aavdJv6D5G7frB27rTYCbbgKM9t/WqBxlfQfO1fBmEhR9jMF8i22gtAYbcLBoJIvBC" +
                "bUIyb0Ndj1Vfho0NQEPLPAj7l2U5m96EwrPO/LBmidZDQ923WPwYz/BJltUxbcke+JwjxRkpXB41Atrd" +
                "5q6twWa278CqwCXkDQtxlkew2fM9GH1Prq4uz3mk1yR9O4MtKrfm+EW5zStXUoUO+mEVmuNjC67cHoQX" +
                "cQnFixvg9LpHJHl2JoO9vn7+8rvry89lZZsNyjL0eUu/OvKSRQDT1GtfT/PeFWsugzvpamE/oqW+enX9" +
                "4urykRfWYdjhEWmgCQjPnTCE7DvlVcZUYSJbqL4PldBAi8IuGvZrzqQoqXYFogwwrDIlCN3M1jnVatE0" +
                "CUWPeZIvN1rnwGoaHtFy9W2dfv9osvPDQmf04Df/k7z88pvrp7eYuPztneUfRtDT92dLSK6Sz70g/SiC" +
                "DqQcRjnQ/6kte+VR8UDjlhx2934nx3+BXijK3rNF7qyP68aDXNAbBhGCF5XS1hLkWZlkc68VAEyAmc3j" +
                "CYlSpvDLNzcvXzzEMiOJyfz45Pm3CYOYJk88QYMk9hwR5fZQmituQtxJLAFVPdPkmmyNvBzYfeIsCgo4" +
                "dwdmzp29SD75z1NE9OnF6VM0ia6+PJ0kp5VzDbxZNc3m4uFDcGFMAUhvTv/rE1lkRcZW6TgkVIrY5F0U" +
                "qwg3KcIDWp55cwqdsM4TOOLOWslrLwpg3XlegJ807erWDuliUoTxqOnKqy+ZSAgKrgsFgQzN0RQiMymT" +
                "lOBafUHpI5ijLJieE4J0kXgs8EtEBLzsI+Lid//xhy+kCSpqTtFCw8Npn+poN3/9NoH9qy3mQ/x+dQe/" +
                "+bn4WpsIeBouOd0t68e/l1eYg7pIfvfF40f8DB0qbJKjtaxtwFTYuSrrv0fjBheko2hSTT6vXdYW2IDS" +
                "s43bnHoaR3L/WAHg+yyMUGZD9Sn1BilvkqR7MNHJ6ksx+iYhLPWbKutzPkBmGroCw2iu9gIAQ4WA+p94" +
                "k+3v8wn8bzqiaow/JF++/OHyc/l98+rr69fXl4/k8emP3z57cXX9+vKxvnj54vryi5GQrsot0kI4J2mF" +
                "70faKMtBHdea9Q1NQ8w9tNA+WCCA0487RM0uOJpIpZ2YotTiW2yL6Hqn4us09Dll5TcSGsWvsHCaKjsh" +
                "P0ySHzlA/FM8Z0Qy+V62XDY+gtmRShjGw8rrqMJoGnA7++HyPHr60eMan34CVMdTYvzLrCi4iNuOghT+" +
                "Sqi5RjOJhQzJZ6mVNVne4hTEWWIKmnb2dfb6ydWzv93AfOIxdZMJJm4wVw4xVph0KKZB5RtqS1J4X4b6" +
                "KTFgkHBRJNdtdODOvr5+9pevb5MxwpaHs7AmzghHGA9rWnU8NOWFZIy8cMbjodTTcXh1Mg4/ROPcNwrW" +
                "cyjuePvErxke86krObign6B/8A36PImphLwiV5qLwZt8E2iIcIr90WPl2r+JJE4+FaSOepwo+PMk1Vs8" +
                "EFfEqQeNA2Kw4ccRcYf+Anmv1UFmC43VfkCNdgsNBv7OmWvEdhQxnSYjrpfxGb0ozhu1O9YC89KHQztB" +
                "rjjzbqQGv7PcD8R1P74KQldUFU80VfQ+UUCAV87MV+WmXII98cdIwkpZNNWBUhG3r+2BNWLuDIyf+s3b" +
                "EY5xKwAoUSGwRiI8JBSoPdRDu7NaCEuzGcA55W+505FQpcsYQJku67QOk+JaxzePeZ723YzCikeZLfnw" +
                "g+lkTrfaiQQDQrDARxTMu+RTjCR+mqS/wL+y5DI5x80yycUlELhdvDl/i8FJ//g5Pqb+8RE+Zv7x8Vuf" +
                "v3jzxVt697EQ8IFAYC/FNphh63VRQuNzLP+keauEoYxzdBCGJUpIJsuZAM+IbybRKQd46BxweIu75Lqt" +
                "ufbhrY/CR2OxKrPvsLDXZlO1Q33wpHtIwudPMfNcoSfTTYSSjOitcgpLHw2Ua9SH9RqwnOhlZ1mHlRxZ" +
                "q6EJ0P0zjBTNqMT7OFFcf/bo3vpJ48VsfEYGeTM6uUTOImI8PiGlURx/OEyOkFHmXQ/6+NwAHQxB112c" +
                "HM3O0DaEiXp+6J2beiXb0Wmpe9Rv/DIo4U77SDn3u3yX1+IYd3ps/etuhyNsXg8xHMXhhwH17pNhcw5f" +
                "UJRc0yzkjnnMB3nDkRR2GdpSDUY9J4j29hs/ymfwFcm8TO1sDnywm4QZfBp9M3NYw9uQtNBG4U2v7dAH" +
                "GkDCn5L2CMesQi4obEkyzmzpGjIIMPkOnqhWkHIMhKtNY5JOnhZg6ZHt8IutHPl8NbhZdR2KT8/6yZcj" +
                "7Pshpd/Lu7hzWc8S8JsS1krHaO5JynChNGUd+ygl0wrjfPekVTl6tFhglnEsREmAqELiLIhxUy1tI+rC" +
                "Re12lgR2fC7ovgMvDGNGMGY8ZpiDHFW6d/7JyUgTGN9x09Bqxqdc/zfS2lGkTBcxIQpkFKuwsY+vFD2S" +
                "UK7vzWEdms+6OU7Dw2MKLLboLZ39+pRXdGga9XxcrtbzfjF02GDo/9dnyZ6FlBS5Eh7gRNJkPqXhDQ5M" +
                "x2Q+a+/jXf7U3BI2foKWx8BCexm49yxNRv/wsuKE3RGoZlDB/hYx1S1B+nWiSvI/nZ4SyoikWNgiorNQ" +
                "zNTH3LDT/o+RjFpZSDGUz8gzS2xVoSxRHRflTaOjxnM6B21n72bYc+ZbDzTZf7jJLwdN/kWl3JB157P/" +
                "fsXh0CoVjeZEvpEJyHXHWV6nnNZkpXR2ULPCRxP9oRbqQLWRnfCe8ZD3HIrbcbYt31BKlrNPDs+JeTLn" +
                "8m2C/FymR0InzBCHQ1jgP0PbtlIfm4nZ57rcnEuyWUKH7jiPKd/g0HDC5LZHFQTixctbAM8VaWuYAx5y" +
                "jc5Xw4IbJnFfQuknf+ILqBV/eGNDxXDzxoONDAc9WRRKdxEf29zuolswBDVA3If2E0nvMVV5AR7MvNhL" +
                "te2Z3pVADFC3WFbcViRGp5Eg6ERnaTdJy4WLRJRW0M3J+U6PyKpl04WhdKV9DPOP6qbqyfythsD7Lem8" +
                "WbrCI/Z6UDrsFOspRZaelu7e6+EF24TLqTlbN6RZbuQ8rje/apEhX2nFA+Nd6bRxO1NJIYburp82s4Dp" +
                "01tMZVYO8VQOKMmEmpE13iJCOY5e9T6dsOQ2X3CDiRwlNxzsBPlG5kjdJ3W53QT9nWSzB0TlWWBZvmVA" +
                "NtgUOyxagJaPicpdZf2NFDjnWR2uvfE768/Ld5mTaTnQi8/C8laBVSWDhpOpPf0GEIJ9INgnecHlYwSw" +
                "f6TCN/QZaT7sirXiTWezUJQBqsNZWI8SgrbRmp+2bukgN523q1rLpT9RXc8hF1KGmFPHoJ62Yq/nABbR" +
                "eT/tyZmVQHvXW5Iorl2uAnGh3XHAhZMh+aY8gkU5Sb02zD1zm5o28NmAjUHDDNZ5iehhrOutPnRlRiBV" +
                "bEwiS0DdAVnhUUeuHTJEu5NANanhk7oHk1ckYkkQWNgrQ4UyWN9IQ5yLBcrJHhqQrY/oeJ4cDsbYN30S" +
                "Ia2StMTSgkIXF41PCFDZ7SlED1wvBiWCp2/mKPLr+xX3qkHUvguzZmY3VM+DCMcvE56nZ1Itij0Xnbxz" +
                "MiM2mwH9WBi7IbfY1mcKEhM9hW2wbEWVoQx9D/xX+cNHPEQMHmaG1VksWc+msfTwpyFpzVTfJOsVNRnt" +
                "zoFYkLu6vIYxAlQEu9twTbut4DXVvZ5PaIZ8IvzcFxs3B1QQ6e6sf+sJNJxRQxVmyPfakfRkDZ4RrLTY" +
                "s4kU9yEJUKDn1F2mAHwwYAyo2jggJqUyNF+l5sSVJRUrw2u+raSrd2KDQdQeE2BEfbga9Mv8HTikQj3q" +
                "1DTpIYnK0vSOmi7CCEAXY/GcYxnsfFF3j33UlNFoSTCTfkguk0eT5Ef48/kk+QnzICeaTr9+cfPy9eyn" +
                "y/6bH7FqsPPmB6zk4zciSWnL/AT+pXyCe7UMoQCfVcLr7TL9gz9MmnpbSC+lQwBQUR3Dpxm8eUxpkE6e" +
                "AWl1bjRzPh4fnY4MSZb+3W7HDnj3rk0Mfj9eIRBPXlmlW+qajNXrBnpSWDf0xd/cRQ3lzL5UtxKJL+iK" +
                "g3DhFFk0kh2WS8kq27RVSaenOA1Ft2rwvWYiTqggp2ZB6F0tuaZMOiHHJ3J7APadYY3Nb5oLVSbowv8N" +
                "Owrkctm7kYkMPam4SsZ0KIUlQA0uKzlO4G/V1q5FXFFZcElmVVwmikC3BqQ+rpdqRtVSiKqdZUk45IyH" +
                "7C7KC2AOT2/fs6SwHtGZ9yyJk02qZcKtc3ov3B/9+JH8p4pjcJrOJxqVim4VC8VNhH2+ig6kQbYvwT1I" +
                "qdawXORLOsXB9n+04N6FdM9Ya5BCWcTtyAyQTY0x0/LZYUVuuDyqbjwK9I5GPJDQT7AFqpxGK6fu4gHi" +
                "pYu1XNiUyN2WOoMJ+TVEIFT4VpHFYriqG4m8tJYOAANcj8TpOSv94cWBqtfFdPGMeNnmZgivioiunq3N" +
                "ws48A81wTRF5CReCBe84tUupRvIeM46A+K5a8O6v3pSyRe8yEiScD19QSNsXOLeylLr19hmxqi0R53zP" +
                "IRn9bZmyHEKjWRiDzhNEwcVDsuUWxAJKSIlSGX8buPGwf9Mh+IYmJ8dXja0hsMPXHMogGWj2/VHlPclo" +
                "WJDeSdkV6wcJY8QwmOTRDT+dq1bpff+q1fi6y/jmw+4VNpR/ktEYDLUKcd3Iv5VTwMiQWbsp9G6cZpGM" +
                "h3zEkOOi3Fjf3/WHksUcAe1LZxxnfK9WOF1N06D1q0gIp5f4BoGTkVwc6Y9wPeebBfQay3DLj3YgTgeQ" +
                "FF/Y4CLLJYz0LTy+4ieYD0W45WOvC959aKUD3ohptTl96N1fJFXQk8OLjCaJ3Yp74MAgWZsNiqF4fRty" +
                "fanOmPxgV5AfFo5qMbbXwp5sBXVuO1iDbOQzGU+xM9YEcB05wwoJ8c6wPXvie1fxZcVFdrwbko5y4dD7" +
                "qVJdwnDoOr6FgwIAB+YyW8ZTfz/cA+WIg5bpKi+U4LFhP50U7oYiTSm310GrP5lkBQb75SdydmCX3+XT" +
                "ytVTVy0fNotP/tws/vTQ/BkoO70DQHRDxI21dFY6c2m79pGehYT3Yvvn4GoEERDd6SYPooBrOOhIjfjt" +
                "6DbcV+KvIDjGietBaSByUUt/AAPVPpRGsf3BwsGXqVEbLlOLznQC0W/zDCso8Xse+t8vn8B5/7k1eLqg" +
                "3q85gywXYnZrtjoD9pdxjR/9rLj8azCZ22dmkoWw9bZYTKgCoahdECmxscJIYZPFXzsSMDXtmCCHy1U7" +
                "/ufWVnlku+Wk7hnX4xI8+5JDDxoyQDOcT0gJJu+dfbBsQx7kcPzIRNTIn/E17bwcmAZdnsYTObgrmKiE" +
                "wlhk/oFiXCXQg064P6IwWGe+1PxEyuoZl5LJ4Pu0eVJiYPGJ/jJ58uIqPqYX0Z2AmHXIAaXhwTelgn8C" +
                "VxE5or/QvYou7AbovvROfDS2GDO/Cv98hIlHSn30gMwi9abfc4GCGgXh0svO/Qe+1EutgyOtg2yN37AK" +
                "uaP5A6sQo+UYVRTBENFqKn8sEdfDqYCdlp3s2eAZPCDvr8UWGcDNMn+Y31Wv//LlE/3ysa8M9gMyUtGD" +
                "8b+W/tfc/zJHdzfIhGMTsmuEHlyqQYGwwXsxbyNDlWRqfMdO8BTCEHgS8WQkXYQE+OF7kIIUPJSPH++c" +
                "8nsGpxjo4yuys9GDBOOMb8IAtUMxEWhfWdu/Keowne2o3WCuTG4gYztpIOknUXuFKoczBCBWaA8tQFd1" +
                "TKT9j5FFvgJVw2EkHSwC6Tqm03Z4OOehS9N2A/r3DNUIOXv0xpTpXlExns6bh1O0FPLCnrFzwIBwjKeF" +
                "wfh6MEOdJkalexAhIzqlof/1BQoNrM98vQlmR/G/ziDdSnCH6yhgoN0QiCyjBmMHpOwvPnXKXfnqYb7V" +
                "js6/TVd8hYvhGMuuyhslnBqjHn9A7Q78Mhr9N9YBuadrZwAA";
                
    }
}
