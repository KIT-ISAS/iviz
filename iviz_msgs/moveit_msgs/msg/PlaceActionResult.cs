/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceActionResult")]
    public sealed class PlaceActionResult : IDeserializable<PlaceActionResult>, IActionResult<PlaceResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public PlaceResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new PlaceResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PlaceResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlaceActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new PlaceResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceActionResult(ref b);
        }
        
        PlaceActionResult IDeserializable<PlaceActionResult>.RosDeserialize(ref Buffer b)
        {
            return new PlaceActionResult(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a93c9107fd23796469241f1d40422dfa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+08a3MbN5Lf51egVlUnaUPRsWQ7ia74gZZom4lEKiTtjeNyTYEzIDmr4YAZDEUxW/ff" +
                "rx8A5kHK9tautHVVpzimZgA0Gv3uRtPvlIxVLhb0EcioSHSWJtNwaebm2Vst03Ehi7URhj6Cm1RGaqTM" +
                "Oi1ETh9B59/8E1yP357DfjHj8I4xOxCASBbLPBZLVchYFlLMNCCezBcqP0nVnUoRyeVKxYJGi+1KmTYs" +
                "nCwSI+DPXGUql2m6FWsDkwotIr1crrMkkoUSRbJUtfWwMsmEFCuZF0m0TmUO83UeJxlOn+VyqRA6/DHq" +
                "j7XKIiX6l+cwJzMqWhcJILQFCFGupEmyOQyKYJ1kxdkpLggOJht9Ao9qDuT3m4tiIQtEVt2vgL6IpzTn" +
                "sMdf+XBtgA3EUbBLbMQRvQvh0RwL2ARQUCsdLcQRYH6zLRY6A4BK3Mk8kdNUIeAIKABQD3HR4XEFckag" +
                "M5lpB54hlnt8C9jMw8UznSyAZyme3qznQECYuMr1XRLD1OmWgERporJCgMzlMt8GuIq3DA7eII1hEqwi" +
                "jsCnNEZHCTAgFpukWASmyBE6cSNM4uCRpPFBvQjwV+DsHD5wf2Twj05Z+OGmN7jsD94K99MR38PfKJaK" +
                "lomFNGKrChTIqUL6RMx4SyDeG3ie34EeMMzuxaT/oScqMJ/XYSJH1nkOlAUhnCqk0TcBvhn1etc3k96l" +
                "B3xaB5yrSIFog1gCy0E88A1IvymEnBUgyUmBp8+RQeqe9CCbB+ILPwfwPwgJUYEFDrRylSqEkBTGQQFE" +
                "jyYqX4L2pWgKCnVsUR6/v7jo9S4rKJ/VUd4AZBktEoVom3WEVJit0Q7sI8RD23RfD0clXXCbF3u2mWo6" +
                "erwmsSxx37tTvFZfJQ1KhdGgBjOZpOtcPYTeqPdz76KCX0e83EUvV39XUfGABJBC6XXRFJfW13GcqkiC" +
                "TSWYfrM12MlCAqZoIcBSJ9mdTJP4oQNYyfOa0hGvnkDyvOhluiAlLIXPM89T+KJ7dVVqckf88K0IThW4" +
                "KrUXw2+hLvBkl1t1pLNZki/RqaH7KKpWgDBRce0QVTH58d9wiG8jMwpFTf14A3QbD8jE1XA8qYLqiJ8I" +
                "YDdzxLDeAyCJGLiGQBQTQXoSIJQ2RwEGBDyNiW7Tb9A9g7A1UhtJukng+KA5MmuYzuCgm6Z6Q/EITgRV" +
                "yFFvvbMCZKyjQh0TlbAKl8Rqup7PkYx2UqHui+AJXVn/MmAJ4BDEEskUyG48D/lkIOlmkUBsQf64YlJI" +
                "OlSMsVCfQpe19TFNOsF6laH8wCmVQQJBiKOWK+BVmsJqhGmYeRsFW3vQTvRAJFWOJoUwqoYKFn+wLja8" +
                "AFMM6G3rXJgpFU9ldIvSCCs4foVw0hg5V8was1JRMksipwyEgWlb6Bjr8QRAarkmpQA7l8CstmMeBiGP" +
                "xLoliGJSMN8qgTjsdw0j/aKX5zq/0Hh6hb+GEfwOoyM91QUpFpBeogvQ+TYkLXajE//+0+fGpLkygT1c" +
                "fQz2ifJkhRyGGZwaXGkIppHlK3wKU/sIw7NUy+LVCxzIMoAVEg8fn1BN0gQcgoMvQqlB5QbpAz4u5F2i" +
                "cztKMcV43Hlun990+1fvR73OT/gT2Jc3V93BAPxBiKO9y86Jm90ffOhe9S/D6+GkPxyEOK9zcmoHKy9D" +
                "O7ELfjt8/THsDT70R8PBdW8wCS/edQdve52TM7vsYjiYjIZXfq8X9v37Qff1VS+cDMPur+/7o1447g3G" +
                "w1EIQLudk5d21qR/DVsM3086J68c9i7S65z8gJRwfBH/JW7BiC6Bb5FXb5YnR7vxpDuahPD3pAdHCC+G" +
                "4BbHcCigwPd7pnzoD6/gcxzedCfvYPZgPBl1+4PJGOY/d8R8O+xeNYGdVse+BOWsOrEy5BYhb14EDe68" +
                "HQ3f34SD7jVQ+fnL5mADEkx51ZgyGr4e2iPC6A+NUQgUfnHAf2yMDV9jrOZGf0Lqmy0YumWdzG9GMCEE" +
                "BAbjN8PRdeiE8OT0uRcKSywQl97FLyiLIA8fYB4KBUx0FKzgin/TmCOaFZj+4M3Qj71AnCpiUMNrMAz7" +
                "v4Tj4dX7CfHp7PlTGLzSiLmU3hluiH7QKUCOn4GbWLL9gUjchk05LqSUTLVE0lZtervSJiHLJfSM8oy/" +
                "azidoUAFEtZbE4DzN2BDafOfcZAtKM0LCRwS6WdeRl5rCQ4HzIgC15AWCYSR4nL4RkjwXKXXWIBnq4G+" +
                "xrkwr7IFLQ9jPQsbm3UhoI4WACXSaZoYPKeeojmG1Fy6MZcw4CmEzdSJBseBW3/hlg9pNZh1tzr0kEML" +
                "Gfd9k0pwbFmMlRKKYhYKoOYcCphIZVQC4PwVSxo5OPaCShfghuNkNmPnCg4X6FB4DDUBofWVMs1SG0xa" +
                "k+UKcikJARiVeVwdgQJhd9SpjjGSOHL4wEQM7bCykyqZ75uMNn8GWLFonZ9HEFicn1c8pI011quYzwrx" +
                "CiFfVETuOJhqjbFliId7LOnfL4AVSkmvAiR+C53GtlICFGDvPOUIjiSID24gX4BfwIODZSfdyTXF4KwA" +
                "bREclNEgL1JAQB4WR7m60+m6oIxulSeGDMQxYhOrGRgMjPywXiX+WlMz2nKhPBQZI4DlcauceqcgWkiK" +
                "7e7UZ4YmPzPHpJ7lEjUDXhW+ZiZXqxR1LMmqAAZLXD04btPBeuVZMFSl2A2lLYZQnzUUIle0B5lcKksI" +
                "Lo56JTZ7g2K3H1HNuAgWxCuG2FhAgrAzhwuMGpTHsZGMB6uRWypsPmeoFJZQzXGPATQJhKlWsp3xkXku" +
                "t6ZFO6AOERuxmlmnMCGDbK9VjwCLpbxVvMjOh7OjhGkK+WTaFn/DgF61522x1evcmVA6RaYBoOVPo2iH" +
                "sJYtXEJ5G2jqnaqyk/AWmBhsrTQi9fg0zNvK2W2ysnAwiE4m+RNsPRwZCMlwKlqDsyAB2rr8wMtAmSiU" +
                "xEEp8EgDoXPgS4H+mjjYhrj2Xa2AXgbKJD826IVHpwiVV07gK6+YAk9hUHbdDpxqpLDoDJognc6yDUDp" +
                "scwlWngRi9UcUjgSvBn8EmvgK8CZacyHnbkGkqyjAmtXMK3cjwWZU0Yg/dqXL6TTT6y+28CIKG9WVJbG" +
                "nA7zkMygu+c1c1WU+g9gZart7j6KEdECooS2eIOqcC+xzNPC8j7EvTJ3xkKShL0fXb4hm3aGDvzoHoQV" +
                "/sgNlrXRAhULbRQPogSjlFXK9VXsmJDwkScAhdeiRtfGASrPcNBAoSEvocwX01U4cA2H/zdjT2vGNrnK" +
                "YPm3mjE3/f+SGXvIinEAistNMFcQtBWQ+JMBmTgR5qIA/74zaQMMxQn42Rj7G5EJBplej2X0HsDaUTJ3" +
                "Js+qgzcrU1VsFMhFsdE7937EPzR4oEwyAlkOPlBN5IzXp6zVv65hQZ6hAch1YesgT3FIi8yeI0pwOjjW" +
                "wF94Q0wStVQYdoNM+ZUUxqPMwBmoGJZTlNzC6DjWiirRZMVA1dDLkPqjOd46Y8g0wdew5AiVrcX1QJqF" +
                "roJuXOmOFmx1nsyTuGlGyfDbw7VEMTsFkQaVIpx5M2AhFvYstY/boj8jBd3ggUi5XXg8VR4vuqAotG4J" +
                "e4dBeFQJekNK5HQVUs0C9KRd1rTu/W/emYs/n4TVpYzt4zaY5Tzx7rzGc3z6oxRQJPJXD+R+2zyRrpLR" +
                "sMdyDtaUeUL9PNNc32J5OSMRM5j+YgqILldmc7q+R6cBxs7pqp1SPtt5T3M6Nn97uAasYPaUh2uBUgHy" +
                "5HrwgOhyv+2IBKx85KzvKco1D5QarHtuvGU9nlaSdfJVElz1vctMUWnJZWJdwxXb8XfyTkFwQHT0tSB7" +
                "YbaGMMAs5EpxZQeicGX8Pc0ObmwXqpUUnEYbHoAlsbcZxC48K4HMtK3BtMFgYTlEFS280HCVmeBgHzxX" +
                "TeFAzR/DE4I3iIMmoRiou++wuTzVq8qrP7+fqw1hBUpiRG5x0GsSPNgYcnaJdyAnHjFGA0PyNAcjt+Wo" +
                "DIICxtQuqNwHILCQK2boRWb+joaxogCSLgPinXYa4AalBcwP9D58SPI8S4ThjwKgrdMAo6aA7JApsSeL" +
                "Uk3tNTLXa1IEC+W45QoQtEem8N5R5lvaLVcpd+VgnwoGUrwxsg9vvWyZrnLXURZhypsSvHEE7EK7hWPK" +
                "RiXzhQ9OG8wAjzkTt5neZKU1pflPoZO7uti1EV+LL+1mFBnY+p1L30hnmgGiv2+zx7QEPCLpIVjAPb56" +
                "Oa7ejPE6x+ftSrFQoFeeSkMZIlHHaw9/hphHzDPKoPksfIQJQkAwDnJZGLXWFvOcPYG701leluTWMqCm" +
                "NEuVFI5X6reOAGON3QxumwirYcsE275MQBaH8aRZN24ICwHltGaQYWrjIRMedrpWZlGHim9g7pIH9sLB" +
                "sRLEa1QOauuAdBdLpgqdv7Vm/nQt32tA01w6ykEEHH7NmgYci+OEsyci3HEVtxtcigehnR44JI6V2HXj" +
                "2FTFyFLdX0lRiZiCucokkNG7RK8NhH3qPsF7YgpH2ZmSwWkH0y2E7N3Ly873AZU3UBtqO81yveTaU3aX" +
                "5DpbYrCLOXSOqdSRgjR8C6aJVIEq/AUos2nIRBIf806j3vXwQ6/znM60WqGdwpg18+ei8oY1rIS0ccXK" +
                "L5/Vxdi8yJ0TuFAe8gb7zDqn1giXe+7fjnZpgVXcWMm3rKZg/4gaSCzfXMbqrrtTNSs4G8VqCFgzo1Ok" +
                "FZDWWYzSmsbKACVjRpFoc4YIDlcq9yE99rypHANQN1G74ccyil83KsHBP/0j+FoP+zz/+cX2B4lz8eXr" +
                "LDKaVCGZkcOzhgzMGNakMFs1imsoGDEusSkGyyBzviHxVQKu1oOc4IVILai4Vb4EX93hnFtbaH1ZZ8qd" +
                "QM3BYmUinjpjD1AcwHhaRcU6WCqT/TweDp5hx5WtnX3sXl/ZTpk2Nvk4QYpLBaikmK5fzZc0qD7ITt05" +
                "lLboUdSQZHuYTnpEtRutbyFeuVXn4i//OEQKH54fXmBkc/n6sCUOc60LeLMoitX5s2fY1JACtYvD//kL" +
                "HzGniCnTXLjLrGVk7tnoBplToQJGjklxCIuSiJLlW6Vs8+8sBVWdJmni6j1qn7xG1BCDRHQp8+Vrlg0C" +
                "gqdCvbc7c8kLhYubQl0BlFqLsSBqD0s3NwTmXHgC0DskAbxrkuD85U8/vuAZ6Hq5QgDzdjE+tDuNf70S" +
                "wDaj8LrK86m28fiP9J2bwbBpK3G4mZuzV/wGLwfPxcsXZ6f0iC1XOCHBMNfOALe/0XnceI0RCh7EbeDu" +
                "OXl0qeN1iuNUFSj06tAJNIj2Y5XlH4oWAKNLVtOpvocccIWS1hLRFkJrCtoirInawqLLcnLlb+JArFxB" +
                "ESKcqQsBABgafHTppIkcOH/fgv/age28ez38DdyYbbC9edcb9cC18OPFx6v+4LI3AlNuXwwHvc4Lp+3O" +
                "PpGXQZzsLI7SnElIwNEad/1eTi1vQsoZbg1WpRD96oLKtHOu8WK6QrfGTAR21Uiue2epDss1h+zcAiua" +
                "OAoHJ1Q5e/itJT5y2f73Ks7SNsalKpsXvq7ctEGG+tDsIBC9XdI2/A0ikvLpo6c1Pv2OXryCEtPfYkXF" +
                "LmQ7mk34zHzLXcsaFTLFfO5cxskaUbBpDktQu8bXcNS97L8fY4RU2dMxmWAig/nrFEwVFh0qP1DN0IWH" +
                "dOlit/pdSAg42qIsFtbghu96/bfvJuIIYduH4/JMfEtfoXh5pkUtvXK6II5QF455P7Rzbh8+nd2HHyr7" +
                "PLQLFhEd7Zh9NjnZv+eFzrgY4Iawn8rH+U2dxAueJKcUmBsLi2RVyhDRFNdjsonyvl617HXWd5aoQUMT" +
                "Lf28SDUOj/Foqak7k0vCdB6tkWc3CaDkM9+5b8RgtFn7Im5heMDj3E+A1K4UN9si4CKtv2etVN8r857q" +
                "gEnmK5e1klS1H0Iyj+vH/UoJ9vFdEKaWzvFUUMV0Eg0EZNesfHkiszlEEP9dsbB3Ml0rzL9m3ABdNlnB" +
                "GfFGE4Id8+lzgHtMLAC6PrKwAms8bOHOrXC51y225dAEwmYPzelWnRc9EancMfaQzB3r0JRI8RfAPp0x" +
                "nuo+pDrgk2BLefneS36+BFctm9+X+b8vEsh78R2W/74T0Z/wV4xfYkJmSXHeAQFXs0/ff8aKon98jo+R" +
                "fzzFx9g/nn32Vw2fXnymd49FgK/U8Bp1rb33no0lTtBIeR+NcV/B21kY6gMo51qLUl7xq4TSPq+In1ru" +
                "/gRG4UFG+O0QzrbNZ+SSrs/mjpTPvmZe2YtdGX8JhPv/KQ71ZRFrDdD7uaoDVgexHyDH1KV+PU02onHK" +
                "Nhw92NNEY3a7aLC1r3xZO9Zuf028duUH8P0h1oBcN/zjMHOnj7UigF8rMrMkFg8vqHXyVBY2u0grIJ5I" +
                "Zh/ArN6O4r4wgk2hJDr2a6GNK3fbO6PYudC3bal1ZY9cOu7uKVrvCzlsq8kJUMrdNJWwjrjbxDXaNDt7" +
                "jp0K8oyy66gCgq50kixK17Gi2in1hVRiGfOslOFndck9sJ2dVltIf2zSYV95aBVNavlvtTWUzk3Dkriw" +
                "vraqgQSs/UD7xwPc/M+Ywy8h43jSZCvWRJyIVRvIjjCo0OIVdmsff1uXiy/72LKpLG+lS/vnRFNjM0Oj" +
                "A/aBPpmKMdvZIqtZtX9tn7qU/UftYe1rQ6SidIfk7wTnuTSr3Sskuqay9w+ut5DSQxR9RxFee1CJQyVf" +
                "HYITYMeEpuerJhi3CPkLTY27vmZ7s8riE38zWcMCrxwlpoPcxVJaMbZueL/p25jTDRbcSwar1KgN136w" +
                "BNroHWuKLGZXY/sPGXikPcZytco1aYcmgr/Nk9VK5ZNKjw4YSHtaN9ktzmEXhaH4w2tLUtnJFCZmvtvO" +
                "JxB6CiIWpcp/449rVUYtZYbdmjvVb997acub2MGDFUoVP1utDX7QlbmLNSK9zjlnJ/qXX69E8efL8JAv" +
                "r/0XHR5H2B/kDgk7PrsrRycS+32f+yciGl6NADCHHwf/qrLuchzjeltpq3d6ldJPS1r+VniVRLdgcTHO" +
                "RDkRhTS3hpqGfV8u/Z4ms8JeN9p7AD2jtMp9odxJp8tZeGBmASPDrYvzrWtOTatdentb55z++JUeFF9o" +
                "1U4aJ1hxdKn72ambFPoBu3iZlJNtTmsv1SpNFjNsTMCDTH3VDPvRyIZgHxRes8iiwBs4ECG3JYCubPc4" +
                "gvAlSu3tNXRtR/+KeDsY3LcYBP8LJ1bz77BGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
