/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceGoal")]
    public sealed class MoveGroupSequenceGoal : IDeserializable<MoveGroupSequenceGoal>, IGoal<MoveGroupSequenceActionGoal>
    {
        // A list of motion commands - one for each section of the sequence
        [DataMember (Name = "request")] public MotionSequenceRequest Request;
        // Planning options
        [DataMember (Name = "planning_options")] public PlanningOptions PlanningOptions;
    
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
        internal MoveGroupSequenceGoal(ref Buffer b)
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
        
        public void RosValidate()
        {
            if (Request is null) throw new System.NullReferenceException(nameof(Request));
            Request.RosValidate();
            if (PlanningOptions is null) throw new System.NullReferenceException(nameof(PlanningOptions));
            PlanningOptions.RosValidate();
        }
    
        public int RosMessageLength => 0 + Request.RosMessageLength + PlanningOptions.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12fc6281edcaf031de4783a58087ebf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1d+28cx5H+fQH9DwMLOJJneiVLTpBjogCyKMcKrEdExY8YwmK4O7uccHdmPTNLig7u" +
                "f7/vq0d3z+zSsu+i9R1yDhDuzHRXd1fXu6pbo+d1V9bVWfHDpqimxWv+bbus0b+j0ejVMq+qslq8XLNd" +
                "m63teVLrizujR//k/+6Mnp/96SRb1VdF2U1W7aK9t3OSd0Z3s69KTLaeozEbhMn5/LPrsrvI8ux8WVSz" +
                "SZPPyk2bzesmK/LpxRj9n1VZd1GgebvGWgqC4rPAKZosXy7tFb5dl3g6L7LiXTHddDlgZnmb1VWRtTax" +
                "8ag/0Wddsfr+bVbiTzv6FRDF8YmlN7akgBaioLsoW5ma4KHL8BSWnqIwtG2LxaqoBN1s6Is+JhbKOUBl" +
                "10UDnGRtvSy7vLmxXXGskJIS+hpxYrVuDdDaXRdFFYAqzo5lINvbVX5D7LerusbbWbZpOcs8m5bNdLPM" +
                "mzCarlfh2pbblNkUu+aDyapytOK3qnjXZdN6tcKL4+z6gku5n62KHDRf2TQx4Hg0X9Z599vPejS1z81N" +
                "0Kh7K1vTXJXA2rSuurzEjLmiWTEvq1Jwxz3Mw452dYJXgAjUbqioN916A4LosnVTX5WzouVevcqbfFV0" +
                "RaMcxIbXdXPZrnMM3F3kXY9z2ot6s5xJiwwTApDRN946gRQgTNbhJQc7A/103N+2y7si26xn+NOOs2fz" +
                "bFo0XGP297qsutYHOpfFc5ymmAEAprOuW1k9th8z44zBAUIGm6YRQq4KJTEwd2ysAAGCxFZ0GSlh9Lo+" +
                "r7szmQtm1HQTmZeQMBddt21JebCo86VOOSJpVc+KJXEuHIi34+wppE9WLAtjJ0Bhw7xpQOKya+ieK7Cm" +
                "WJCoZRh5QZKdXpTFlSwTfCcrwsS7JheE6F4DnV2pfKEgAB5zz7uynZfo+iT2gIQi5EkChAt7URvygcq8" +
                "usEi8QUSscauyE7nEK3YB/nd1LPNFEyZyGGVl1dlvSQQxXI6z0Nlv/V6WWK5wA+FrQyCTanqLvv7hgI8" +
                "v9F3R+mUZfDhhImidAAT7JulEBLe/r2YdjXFEgErKm5Gb8L7FH5svWuUCpTqMiXVPCSyGoKpEEKF1JCG" +
                "5M91MS2J92OSJvc4x7SGfV3BAMBshDE5bfs2KWe7Bl809WbNB2MGALu+KEFcgluHi5/1umiw4GrhcKXn" +
                "hLAC3M3qHI0JuVxxRxyEaAZIQGJ3BcFQzMbZ2UXdgMiBxnq5MTHi02+KNT/OxiPM6eEDAp4EqyHvINjX" +
                "EZWr/F252qyyfFVvTLdg9F2YJbEsl/U1qCxhpuwQJNgW2KQZSMRlszWMwxKoyJVpvuTy5zk3V5lFDRl0" +
                "ucHUQcZCLcnUBLfZVbGspxARZM1KJMx0ChYmVkEg4yx7bJO7ypeQssJumNrh/eNP3+Lrm9sA3uwAF+nF" +
                "Gayh/DFRQsgrUjU5B5LvhlsFBNGIAc2WVwXA2QIxMigRGoQCkB0h2ykkCbRsONeSSrJaYMaH5Yobl1fd" +
                "8oZisWwpYKrpcgMNgI0VcQyBCOzfH98/UuWs4wiN6yfTPELfggru6afj+wIL4l0RfViOi/HxbRgBwP6X" +
                "FDlHUQWj0cQ7TVrd2onOqNcm7T5stx/VvUP3BeUNZmvzRaK8c8El0BZ1ImXCfAPpT0bDVxFpYH1i+wos" +
                "CJY5hJlbvzsixbgMcLrpsw7npVYSDcAAuq6wL7B7Eiua3LGqz0toNuoTsaNMWSlgWH7XxXI5JmudiuJS" +
                "ouC02bgp5tCetOdcG2KKWGgDhm5HXxY51DUULP8EgVBCXmDjtZELOnQ7VmPeqNxJTechOlnEZd0Wo0VR" +
                "A2sQ3YL6r0WOPyTgiQIdyp5//lCgORvqg1FX2810UMUiqQn2STXLG2jhosthMOWC9YtyAeb/ZAmLQWyT" +
                "1RpkIF+7mzVsqoQSFgWmLPxJFcR10yDeVOVUVDjVQtpfWLpva0zruoGZzOZCBIQuFGtm/bPTE9HR9KAg" +
                "qDASBExT5GLLPzvNRhvVGegwuvvmuv6EMmNBveqDq6mJyRbv1tggzjNvTzDGv+vixoB94hohO5R3Ezy2" +
                "RxkGwRSgncAcVBuvbroLCFvhobwpxaMDYMiHJaAesNPBUQKZ0z6B/q1qB68Q4xg/B2wV4HJNn8AonYlK" +
                "ajcLIJA6V41uk+yQCxDzkIHL8ryBUzUS/ShDju5+IYwmmlp2hIzetpCH2ICZELHre9kNWhEfjCB3skIQ" +
                "cjAKsFtYB8XblXwk8cybAouhYBxnwQ0VSUTPS1gv9KThVDbUcGYQQ7rUDfxPOCqzGioKRiNgrPJLGu70" +
                "5mlUwr6EiqVBV7VL1a54jS6HxXgBFSQST1qpOwkIwgblNGvKBbSj9MRAq9A5z2x1UIHzB2r3yJx1MOwZ" +
                "gDR1Z+qKGvim3sCUxRrwozHuE5PK5yVU0tX1MVnPQPQx+ko0oSsL6IkOjA/J64ruXfh1E379uBflFp2j" +
                "23VaWUUU5uewQvoiteM2wiRQbyI6bgwrtG7d0j8Fq1y2I25v3ejof+ZHdc2kXXTN5EurAoPRg4v8KlhO" +
                "RXb68gv1iYKtJZ5eCvo526JdMoR0n8zq+WQw2OOug18GKNN6uSxbrrM+pw8BoZD7N+x5iz2VVdBYDzg4" +
                "Gnn/J979pfSGc+a9oVLs08Qgc9wvlvkC2J1RRpOCQc/m7dImm4KUo/0uBhi4icIBqpv8NJ9v6TqZobrM" +
                "0j9REKu6hXGYBUNRFYxLMHFifann9Yy28qHPBw3p1FGnLAsIuh2NMRA2PDfaOjmBCi1OThLX+1wYXqMB" +
                "HJAhJ06+S0juaHRe13STJ1zcB9S/O0kwMAAlceACocCLegmN4awPk3ralBQADKXJ6mTtZvlBukBjCvs0" +
                "2HWgSHmAUtKsl9CJsUyzkw+bgtYg3zfQI2VLjpvCJsTAYp5RqVBZQqelnOa2j0PJZ2KDHx3HptE4Hza9" +
                "10rje9Cw5NDYpZhju7qgsNXVDy6NAXixYu8XsOq5MImM6BdaplUJLJDgZpD/yqRQihQJ4rkqItR+DHys" +
                "jr9oSYwbTWGFasEZiVRCl8BaoTsL322rjVo3DO/6Nor8UE7yrhxCdlD0MMbUZQxlYIswtUizPMofCfcg" +
                "1MERPCQtplQfwzIZUY5pCIhmPPWcdLL2WDspzN3ZcfYNVRu1nGodk6KyiqoGQNufgcVAWKtj0VhT+Nhg" +
                "Vq46bqeGqejE3xg1Enu6Gt3bZO0WnVPMOZ7a8keIeyyZwXSBk3CNBCDFFcEojKY4DYRpJsiRWIhPmnY8" +
                "zQPam7KDUI5DH0OtIQhVoR9Tk3h0RkheOcEnrxQD+5Ep27qHsuW1G0OqSbHzKgZIQLa/go5AZbNiARNL" +
                "aI+21qzG1jI8AKFSX7vQBlY2027D8MA8iwMqLatNBuzDyWR8mOFAZ1Fa/+0NLBFxojSLAhIUGynYTdpn" +
                "UXRRBDDeEQKJl5BLIqey6QVshXH2BbnhHQzcJZML4o5CYZi8YLKhyv76+vQLEWsPqcYP4bEhZHGTX9Os" +
                "1jA2bHT9SCKWrE10F9LZKSLxpykBRfuSqXvfxaxkC4cGnr5CHJryKJ9ecsG9Ofy/JNuvJLtmkOHiZ0sy" +
                "b/5/SZLdJsjUDGX3dhCNeOMkjFaBnLcaXUM1sQH/Dr59I2jCR8XXvlzHMO9dzqPo9iBZQibvut4KPbQD" +
                "/3I08vBM4guO/rJBh6aiDHCPbV/rjEPv9JIh3Rs6/iqgemvh0w9x4kTF+xzB8Ot6b9tIigorc/HbRkOy" +
                "v6Tzpr7EToKY6Z23dJHoJlAgIzwtwSWJQY7DNlqT+Gzt9rVAZY9de4cN0U2K6zuGcmc4l7KJa6R1+fNW" +
                "KcDio3oG+4la3+KTeknB4HVIAAW3zqov5uU7d2A0lgps0QP22BR/eypKUNlLYyNMv2FW9SJHtFIwxchr" +
                "zK8OZjG6qxZ66nOzmQx4F6EYwBOG4o5xsQISuTr1qceI+GgyA8YA4zrmad/dBc/9bnVLwjKSTBgHYLK1" +
                "jygF6mFoc/kkshET6WE8jyIwVpFE8bt6A5GMLPVNSLd8Eiam05CwP4Krs5skN550iEpEgE00tkKTL2bZ" +
                "dVbiLsEOY4p3GPLFbojpGOpEdMIaulsRRlgKQFvUDdKN5RkwqDUUOF3CAGf8DWlJ4QWDYlkmH6MqplTH" +
                "yOFytAZJHYkcW07CBub2AaYHdJJUbnTWY9YXyODsJjaEb8p1gbB5MGAGm4FY1Ty7rOrr4C9Y+/2w5Q52" +
                "fGyGgYQPZ4KdEOxxK1/YZnfSBURvKzUcHgoBCSxs4HMM/gwxKuPXmIv2rUYiQemC4Y3zXAupBEGBgfTv" +
                "hI7zQqtSdDG6hjeEQDAxke9RNBO6knjdtu+cbbUbUpoqHMgsu3I4abDPEXCGcqVZGGbKuMkKE7wCskTo" +
                "6Dyl1Sv/RJcxNhsGatved5KWltA8L9qLPlS+QduVftgJh98iiM/JH14YwPgaMitmIaRlBtm5RVilmXst" +
                "alDE2ins2GwmewEByyGO0rmx0EgWIiPdskh+i7N7PKP3lxCGYj0kFCWeKAHxpBFoFFUimxaWcfEONgOn" +
                "j8ieqlSROePR+Q3Musenp4/uc5jXIld7I82bmo4mzPDqqmzqSkprGDSCkEC6GcmvBmkZZQUJB3fgZ4WQ" +
                "JNtnRzrS66fPX3799NGnsqb1mqKKPq1Ts3nBJltl0uYwvG+tnqfQTr5O7EJc5KtXT1+cPnpgcjiOuXs4" +
                "GQX1MsW1Ub5ttSRMUGIDR8v2zR0bqX5Bi2Ux79RpodMMgYZSDuIKqHWJEQUqwpXA5EynKLh5yAm+1LIS" +
                "TYsAJh5pjHpDqzr5kCb1+8UKxOMv/i97+fmfnz55w4zkL+9s/xE/T346/SFyU6KCc1F7JssgyRi9oFMD" +
                "26AdVAJ09UIj6sGf1NAuSIUB9J5pcVmEeG06wom80f4xtioJOaEYCK0qm527vAcUBzg7T6dialYCKn8+" +
                "e/niHguELMry3ePnXzGwxJrJ7HGgYkjawANJpo6y2rESI0mq2l2noG5ObAdGZbd2XVhJvPy6voTVclmc" +
                "ZB/944AYPjg5eEL75vTzg+PsoEH5KN5cdN365N49uCL5EtjuDv7zI10iy3ak4lNCPJUJR909s3G4OQkW" +
                "aD+W3QE6sQQTjHBZFJamni/BraifgLvjdak7CJZ5DkWiZx5PP1faECBcFVnfRtbgCInLyhgtVCZJcIbO" +
                "bLES5hcwJ1lAgLwjCvBuiIKT3/zH7z7TFtS+mmhFu+0ZH9hIZ3/5CmkNWAnMbYR96g189sPyS2+hsGWo" +
                "7OB60T78rb5hMukk+81nDx/II1o3bAAjur62FtD8KBWdDV7TSOFCfADPi+lXVEttlvwuydWuXh84QYO0" +
                "P1wM9zaT4U6skZESk3ZNYjvOpjewscV0A8UVmUWh3N0BZXjmBpTl0SfYOeduCAAYxT4VuzCjWtD3j/E/" +
                "BAVYT/G77POX30KZ6e+zV18+ff0UCkYfn3z31bMXp09fQ6Dbi5cvnj76zBneRZToGs7JWqmt5lIBiQ/4" +
                "F5axjU1j5Dy2CMU3yO9z+mmHpNmJBgSl+JKJRi+LZVui650Lq4PY50BVnGRFzTnEwmWq6kZ8e5x9pzHe" +
                "v6VzJpLFcyqqBUxGLxYfiCH6T2F9QPo44nbyLeyS+PRdwDWf/kZdnkxJ8W+zkvggt52SE38t7wUBqvOE" +
                "UKM0tmJWL2Y3f0cpyOehcCevH58+++sZ7aRkTN9kgckN1tofxYqSjoQipPrCjUSJ0NtQf8tQK8VqwlB2" +
                "0YM7+fLpsz99+SY7JGx7OIpr0sRugvG4pouen+W8kB2SF450PIo6H0dXZ+PoQzLObaOwHKN3EMBdlN1j" +
                "QmtrVMA/oX+09oc8yWyAHUHQMm0kXiINCU7Zn16nFu0dW+7jY0OqM+kAmYGkBounVRo5datxRAwa7ikS" +
                "RmdAvdBmKz9Fq3QYCpMNo5Gg3zUFTYQnEU9E5LXiJaTmklBt0m5/a8RkQrCvF6FKs+gIjXhqLK74PaHZ" +
                "fegiOppBAyWzpXdJSQFnW7kQBWvVAtbE7xNRayXNUs0pxdehQAfLZB4Mhk/7/dsRB3ljACTpYLA4QBLK" +
                "8x7uisEKtFJWmc0OvEs6VjvtDVu+kF1Y85XB6Avz0srF7x/qVIt3EwkO7mnC4qvvThBrAhVcp05/DAqE" +
                "yEH+LvuYYcGPs+mP+L9Z9igTNzvPTh6B0ov59/ffMtIYHj/l4zQ8PuDjLDw+fBtyEd9/9lbefTgcvCe6" +
                "d2cQ79qZNhv0cYrTcye/2tSDwJGamOT4igqYWO5i1f2BKb8/Tg4r4KF3UOEt96rut9ayhrchop6MpfpN" +
                "TzwyRGHGaYiY9A87hLwoa2OQ1WgHCU6RF4NljrH20Y5KjHa7FIMlYvFlb1nbRRqzjUcmYBBMGB5irV6z" +
                "txBtODT0U3X+y1uOryZnjhzn6ckmD+GE41x26Ety6n5UJwT85ZCHZ4y18EVJPk4yMETvsNMr24peO9+f" +
                "ftOXUSH3WieKut/hawR31THutb8Kr3vN97JnA5xw38LTDj0f8lnnGr6Q+LenTcQ7c5RHWeOKUD5bXz/N" +
                "R9v7+zAEcjkIxyFRPS0m5yD/6+M4/MfJNwSTroq3wZgY1jENW+54L9Al0GlJjHg+KuZ04mZkh4gX1ayh" +
                "Q2QVGwuH1MtBNfqhlaMpBWdPkN1Ry+HHoqntUC3sgjZWksazWzqJvez4NoHfzqx2rqlnBoQdiavdrtRQ" +
                "d1crnSVpOESomFWM6+3Kh2rMaD5nkpCHC0LqUWocjqLMzhuEBU031Em7a1YaJmFpdLjl1IqCQDCf1KpD" +
                "+gTsiNGtM89Gnpf4WlvGRhM9fvq/jsD2I1P6aElCQLnjFHv68FSxY4ng9tZ81LbB7BvDgmKBy/q4nKHd" +
                "ujr6uekr22TLnKU1swOvl6FCmAE/O9/1LCaYxG0I0I4t4xWyFcGgYJplFpLsIcgVzrgtsONy48D2Evu5" +
                "tNsXZUO/d0FJ4m0vtLJTh/4igdQvGXq/ULKsTq+XRS4SeRU3R2grFh4N0HaLf/4/FYBeBSjBkk/E7cqK" +
                "pqHgcB2WZD7jEeBzOZpcTN5N2HESGm+3uHlvix+HLf41pdkuo82LgZIlxzOlUtyJVzSJommn9cGzsp1q" +
                "hlIVz9FWZUk4QSgMIO2lhLEXwssDYKlygS2myTME4ZhZ1ZQS6HHBtJHfXMIyawJ+bpMTERPnx9EICj4x" +
                "mm6EJiNThPxVfa6F0yqJY3dOg9BfYN9PkoiR40cAvHj5BsC1UmyFGfAganLuGasVEmmT20p85qHG2VHH" +
                "+xIaBYuAsUNNzAI//hOra4mLqxJZ4xhNVqyArrfNIpHTh1J7BRwgnXJj9bBHfl+B0H6LJFO23jQiNMeB" +
                "83uhV9lG0WPx5g6nETorRCM4PBqqapXYPSuJVE8B/t59TT8nLzrGi156Q/MgGOrMK4hSO70cdkh1kWPJ" +
                "zzD379IIUuxYD21o7m2HAjmzk7LBqmpFanzhdQqKbqfMrr7OG6ud8D0NU1aiz4ckllCW1nqtsTmgHqmP" +
                "0WDPihd3SOJiUFUvpx61zWfaQI4F6FGdSuSZ2Bk6o2Rgu0yEfku2vgGOSokKyET0uL9ta75E2TOpN3so" +
                "dI2UgV8IwQlPZNDefoaD631OVNqNJOKJVN0gWEo2YDwnOtBfABCVv6FdRIPWdI13nHEI7UJGWU+esni7" +
                "620SZRZwHA+mOjIEmKgulYcIfKK3HIFrNoUW6CTVN9s8x6yBpn6hhCTtJ2Xl7QZ4vJ3c7AiJk9tTZvKZ" +
                "Wl9cRHqiNbHFcUpvAxJznmD1TNausATGAYtpzkOqXjG2bTzIKDvLr0zGKL792hzATHiJbUUyGaRLkBLP" +
                "HWqJTy7UKnjXTZ1SD+yauqGPtTswlpE5lcAKVavcYGEmpaZsZDw1LZKjcnZCl4Fr+WSS2OQlRC4Q4itL" +
                "RpfVu4AOhOGHnuc7BYATtbKQuOTDqndXEm60xTkrb+dSfUNk8wtP7gnFKESvSL1vGhelJToftYOBepal" +
                "rsWhLdojh8hEDVLnLDJxZWcj7wb/qrz3QEdIoWNeLKFSEQrYibAIpxJlxaxDssWaGkw2ZksO2NVXQYvk" +
                "BtMEOIqPpJy8aPCalaf3j2V+eiD7fqjy7bb2P1HNs8FlI2g3kXYqueT+MOslWrCFh4NF8hR4PeghHL+k" +
                "B9RfooK7u0PPu3LYoiEnLhqlViNSV6iNVcjMVQ+US2oLmGJTskuIjmuhdxWunBEdGbBmRscAPVI35nfC" +
                "9FAl3VNcpdNNhW0dqqgHDOMWioc4gvHzLRIUD5DKx59Pj5GaZrrCcuBPX5y9fI2U++BFzMjbi29D/YMJ" +
                "TNmnMPa/kH1/qyrRhC5fuCj3u1yGJ22UHv2CjoGrKQBEHe3FRdl5nZf7KHrgC3TVuyasDsHy5FBizIEM" +
                "rkrbe1h6cAHnnVh0M5/3Ju980q9AzQ7dgT4Kd3meyYdwOZa00+PyWnQqND6XiwXivU5itVgWV49EorZ8" +
                "0zCPUaV3YuqVYYN7NSn7gt9kd4BZJ7J6pof22XXCkphfMA+pIfAV/xv6GVTWqfeuPRIzzqqjskM5+aG8" +
                "38LzpBME16ktipWJKCnSrcRwSos3CfMqh4yX6z9ZyWnmQFJ3rIvheBMdL11OELYaPL76icXEpahivGUx" +
                "mvtxdRKvcfOr1n7voyeSXop/4f9Aum9f2BUrkATr6C4CYHaDkEw5lULAal4ueFpCDfpkqb0L3jAqd030" +
                "xjxtJWreNjLFiejQnJZvqiGntP588X69Icv/h5muSIVi1HsNjubYrBNG0RuRuM1VMj5ikixrIk1IXVoj" +
                "p5S1spokXRWF3BUAsAF74/uq1XevDMrcVtLHL3FyVea7EOpI6KnSNp8Xk8AsSIzxYt6wPpkcbPJa86uS" +
                "7RMXEBlQLiR09GrzcFulFRS632c3xPolf7JtkUXBykyfBrtL2LKoiGy9K5BW/KYSMs7FDlY2kBr+EAXc" +
                "plL9LgTvpJMZWemnrSsDh1cFwr3LubIqmFC7YO68J9BGmEF13+xXmosIpl7y6xz7UrufrSVmYWAnt+Wk" +
                "F5LK6/6FpOkdkenVgf1LYZgKsnEUhjTywGvinNp5WrLebLNe+lUz3Tw73OXlxVyT5KhuO5ZsdgZ0qhwQ" +
                "nOgVVclJ5dFdWbZzfjwSpGfxR3bjYjgO9VyP6Pvlj/G6HGtPfgY8CQqsubxq0Y6+wtMrfcBMJPJs33rt" +
                "eWtgoa15h2ThbeV97/4fq0E+3r4ICJUW6o5jb2BarHLUCTa9Za3FZZUqX7qv9VJcqHjqSRG8MhZUc6Z3" +
                "78lqPNKTD0BK3TABr8XbCsrTz70h+1bBN3Wj1/YuZ3u6XWg/1/X8NA3uOKacXmAhPvuW1asG7jjcrHbX" +
                "6X+rJTL8KE6PDYc5nXizkihAu/cNrf6QZxewux99ZLX61+VlOW7qdlw3i3vd/KM/dvM/3Mv/CGqeXgKQ" +
                "XK5whqpD+pizegoHyyMzepWjlOkEY2YrLWTCoD/dTGkmhNu8tJWN9C3v0PWrPvzXng4o75QAbu57dQ2Q" +
                "gPBVKEBS00IkQigHkyZWDma9SfVX5YwFi/xaxs63CiQ43siOsai/vVlp9taukuxXRaWjDZfwlN/CjLS+" +
                "akcudcjGIvmw7cVyjiE5x9aOCwxMEEWGGiLh2qGIoXFiWWyv023xHzYFEBKNsVJUuWL4sIJTXknAwH19" +
                "2tJ6/sgweOvMo5Ea8hLboycWn4foMI7RpS4Fk5Abx2QaW7fpCmFIyEnMOai+iwwd5CD4AwlZ9WYrza2G" +
                "XbFoyQW9XVqnZEaTHnmvsscvTpPjb5HQDMAkJQFKwK1PtvO/ChsJDZKLBpe3xa2Apptemo+lRuDMl+GP" +
                "e5l5or0xXzF73A2+7YYBV/3xdsj0hoBQSGU2wN5WIVbFz16DXWL802tQ22Q/tQvR6HDpGw/8cTUarIdg" +
                "UmZkjB5tt0+UhyujjfWljd10K+Bf/+nzx/bhw9+tG0a8o0ilUxJ+LcKv8/Ar/xW8CDHZiPUtg3N48YSE" +
                "r3bcIfkmsUhFlqb30EQ3IMLnUb+R9TAC0IdvIP4k4mcfP+DB358YXUOXD0/FoqZfCGNM74yAupGwBjo0" +
                "RbG7KiZNGWMUnuLY5eXYtYNqF+3IyFmIPXitevbBALLoedcKfh28/ffxJf6BVJ8xCM5/aUe7HsrBNp6A" +
                "uVdPp5s1lO8RtYg4dPIG8XGEYhUbh+Pz7t6YRgKuGrcLPBWQHKJewgNKLU+tgKKrpd37UuQ186Uas6DP" +
                "v8K4VvbB/CX/zQLrVsHbDYXjmmbVf8qAyRhdBsL0JQTtjyG7qV31kl69A06Omo0lGCWHdGlEXDfIolnb" +
                "lsGM31G9k2fujP4L+6/qmHFpAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
