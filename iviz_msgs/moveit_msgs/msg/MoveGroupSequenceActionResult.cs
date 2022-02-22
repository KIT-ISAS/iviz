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
        public MoveGroupSequenceActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new MoveGroupSequenceResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MoveGroupSequenceActionResult(ref b);
        
        public MoveGroupSequenceActionResult RosDeserialize(ref ReadBuffer b) => new MoveGroupSequenceActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference(nameof(Status));
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference(nameof(Result));
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e995e5b2d6c322a0395df341caa51d8e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+07a3PbNrbf+Ssw65lreysrTZymre/og2IriVpbciUl2zaT4UAkJHFNESpAWlZ39r/v" +
                "eQAgJctJdu7ad+7MdVPLJICD834BeqdkqoxY0EckkzLTRZ5N46Wd22dvtczHpSwrKyx9RFf6Vr01ulqN" +
                "1R+VKhI1UrbKS2HoI+r8h3+iq/HbM9g7ZXzeMZYHApAqUmlSsVSlTGUpxUwDEdl8ocxJrm5VjggvVyoV" +
                "NFpuVsq2YeFkkVkB/+aqUEbm+UZUFiaVWiR6uayKLJGlEmW2VFvrYWVWCClW0pRZUuXSwHxt0qzA6TMj" +
                "lwqhwz/r+CL6F2cwp7AqqcoMENoAhMQoabNiDoMiqrKiPH2BC6KDyVqfwKOagyjC5qJcyBKRVXcr4C/i" +
                "Ke0Z7PFXJq4NsIE5CnZJrTiidzE82mMBmwAKaqWThTgCzK835UIXAFCJW2kyOc0VAk6AAwD1EBcdHjcg" +
                "FwS6kIX24BlivcfXgC0CXKTpZAEyy5F6W82BgTBxZfRtlsLU6YaAJHmmilKA/hlpNhGu4i2jgzfIY5gE" +
                "q0gi8Cmt1UkGAkjFOisXkS0NQidpxFkaPZI2PmgjEf4Jkp3DB+6PAv7BGw4/XPcGF/3BW+F/OuJb+I1q" +
                "qWiZWEgrNqpEhZwq5E/CgncM4r1B5uYW7IBhds8n/Q890YD5fBsmSqQyBjgLSjhVyKOvAnw96vWurie9" +
                "iwD4xTZgoxIFqg1qCSIH9cA3oP22FHJWgiZnJVJvUEDqjuygmEfiMz8H8D8oCXGBFQ6scpUrhJCV1kMB" +
                "RI8myizB+nJ0BaU6diiP35+f93oXDZRPt1FeA2SZLDKFaNsqQS7MKvQD+xjx0Dbd18NRzRfc5uWebaaa" +
                "SE8rUssa9707pZX6ImtQK6wGM5jJLK+Megi9Ue+n3nkDv4747j56Rv1dJeUDGkAGpatyV11aX8ZxqhIJ" +
                "PpVghs0q8JOlBEzRQ4CnzopbmWfpQwQ4zQuW0hGvnkDzguoVuiQjrJUvCC9w+Lx7eVlbckd8/7UIThWE" +
                "KrUXw6/hLsjkvrS2kS5mmVliUMPwUTa9AGGi0i0immryw3+AiK9jMyrFlvnxBhg2HtCJy+F40gTVET8S" +
                "wG7hmeGiB0ASKUgNgShmggwsQChtzgIsKHieEt+mX2F7FmFr5DaydJ0B+WA5sthxndFBN8/1mvIRnAim" +
                "YNBuQ7ACZFygQhsTjRQLl6RqWs3nyEY3qVR3ZfSEoax/EbEGcArimGRLFDfSQzEZWLpeZJBbUDxuuBTS" +
                "DpViLtSn1KVyMWaXT7BeFag/QKWyyCBIcdRyBbLKc1iNMC0Lb61g6wDaqx6opDLoUgijZqrg8Afv4tIL" +
                "cMWA3mZbCjOl0qlMblAbYQXnr5BOWivnikVjVyrJZlnijYEwsG0HHXM9ngBILSsyCvBzGcxqe+FhEvJI" +
                "oluCKmYly+2BpBz2vtLI88brFWakSC398TTI7UMhIsNVxmjMpFNEaZZDPCK7YKmDua2NBktA8vplD+ee" +
                "axQnLYtxmRc2+hB2RZRfot8QekYKa/QULNx5QpriR3yqHo1wCjmb8C6mmR58aSQGS8266t3qKpcFVBGo" +
                "ZmmVgBagAdeRgqBO/MrNx09uQRo3wfkt5FJXBaOGqoyWofWNK0woCtGeS+ImQYpmuZblq5cMFuiOyRKe" +
                "SN+aAom4koGQjtSgjwQjBnNYyNtMGzdKqdl43Hnunt90+5fvR73Oj/gTuZfXl93BAMJqjKO9i86Jn90f" +
                "fOhe9i/iq+GkPxzEOK9z8sINNl7GbmIX0p/49W9xb/ChPxoOrnqDSXz+rjt42+ucnLpl58PBZDS8DHu9" +
                "dO/fD7qvL3vxZBh3f3nfH/XicW8wHo5iANrtnHznZk36V7DF8P2kc/LKY+8T5s7J98gJLxjxX+IGYtFS" +
                "Qu0YvCRrsefdeNIdTWL4PekBCfH5ELKLMRAFHPh2z5QP/eElfI7j6+7kHcwejCejbn8wGcP8556Zb4fd" +
                "y11gL5pjn4Ny2pzYGPKLUDYvox3pvB0N31/Hg+4VcPn5d7uDO5BgyqudKaPh66EjEUa/3xmFfOtnD/yH" +
                "nbHha0x5/eiPyH27gXix3GbzmxFMiAGBwfjNcHQVeyU8efE8KIVjFqhL7/xn1EXQhw8wD5UCJnoONnDF" +
                "3zTmmeYUpj94MwxjLxGnhhps4TUYxv2f4/Hw8v2E5HT6/CnsuPZ7vjPi4x8kkRhbLSTq4NQQY3A6UNC4" +
                "7JN9KrnZlsjaqs3+UNsMJ1r0Yliu/V0DdZbyPaj7b2wEOZQFz02b/4SD7HRpXkzgkEk/8TLys0uI2+BG" +
                "wPFBQMvAD4qL4RshIQGog+8CEoQt0Fc4F+Y1tqDlcapn8c5mXahLkgVASXSeZxbp1FP0zlYcST/m6y6k" +
                "QriGB/HgOPLrz/3yIa0Gb+9XxwFy7CDjvm9yCflBkWLDiYOeAqiGMyqbqII6KdwGwM6QgfyopA4QZDNp" +
                "NptxjoKRHEAGDDUBofWNbtdSW6z9s+UKSlIJYYa6Zb4dQ/WEJ3WqUwxyRx4fmIgZMsahXEmzbzL6/Blg" +
                "xap1dpZAfnZ21giqLmWrVinTCsGNkC8bKnccTbXGFD1G4h5L+/crYINTMpgAqd9C56lrOAEHINQlJpsq" +
                "n0pwkgH5vOLArQ14drIdo6mUYQNoi+igTqp5kQIG8rA4MupW51VJhfHKZJYcxDFik6oZOAxMoLHtJ/66" +
                "ZWY+jfFQZIoAlseteuqtyiHdLTf3pz6zNPmZPSbzrJeoGciqDK1HuVrlaGNZ0QQwWOLqwXGbCOvVtGDG" +
                "TykwalsKKRxbKBQA6A8KuVSOEdxvDkZs99YWfj/imvWFAKhXCiWGgDrr3hzu02owHi9Gch5sRn5pSAap" +
                "o5hR63aPA7QZZPtOs73zkcbIjW3RDpTtIemrRk7ZQAbFvtWEAyyW8kbxIjcfaEcN0yuUqMzb4m9YF6n2" +
                "vC02ujLehRIVhQaATj47vU+EtWzhEip/wVJvVVOchLfA+mrjtBG5x9SwbBu0u5pv4WEQn2z2J/h6IBkY" +
                "yXAaVoOzoI7c+DIr6EBdb9XMQS0ISAOjDcilxHhNEmxD1fJu60zCFVPgUUl/XNaL6bQzhMYrr/CNV8yB" +
                "p3Ao98MOUDVS2LsHS5DeZtkHoPY44RIvgoqlag6VMCneDP5INcgV4Mw0thW8uwaWVEmJLUCYVu/HisyV" +
                "N7C+Cl0g6e0TDzFcYkScx2osJdWUWOUUFsM9r5mrsrZ/ACtz7XYPWYxIFpAltMUbKnwk1iktPCWBvFca" +
                "7ywkadj70cUb8mmnGMCP7kBZ4Z9c4+kAeqByoaEwpUHUYNSyxqlHEztmJHyYDKDwWrTorXGAyjM8NDBo" +
                "qEuogYBVPxC8hcP/u7GndWNrA1X24qvdmJ/+f8mNPeTFOAHF5TaaK0jaSrNhBzLxKgyzgjrfm7QGgeIE" +
                "/NwZ+xuxCQaZX4/l9B7A2nPSeJfnzCG4lakq1wr0olzre8enJD90eGBMMgFdjj5Qi+SU1+ds1b9UsMAU" +
                "6ACMZpf6NEQ6ZPaQKCHo4NgO/iI4YtKopcK0G3QqrKQ0HnUGaKCeoqEsuYXZcaoVNfTJi90oijJk/uiO" +
                "N94ZMk/wNSw5QmNrcVuVZmGooINrOuoGX22yeZbuulFy/I64lihnL0ClwaQIZ94MRIj9Ucft47boz8hA" +
                "10gQGbdPj6cq4EXnPKXWLeGOggiPJkOvyYi8rUKpWYKdgNR9U+su/BWCufjzSURd69g+aYNbNlkI51sy" +
                "x6c/agVFJn+RIP/X+olslZyGI8sHWFvXCdv0TI2+wS59QSpmsfzFEhBDrizmdAsCgwY4O2+rbkr97OY9" +
                "DXXs/vZIDUTB4qmJa4FRAfIUepBADLlfRyIBqx+56nuKds0DrQYXnnfesh1PG8U6xSoJofrOV6ZotBQy" +
                "sa/hzyzwb4pOUXRAfAy9IHfuWEEaYBdypbizA1m4suG46x5u7BeanRScRhsegCdxh0IkLqSVQBba9WDa" +
                "4LCwHaLKFp4L+c5MdLAPnu+mcKIWyAiM4A3SaJdRDNT34V0tT/2qutUf9vO9IexASczIHQ66IsWDjaFm" +
                "l3iUdBIQYzQwJc8NOLkNZ2WQFDCmbkGdIBCwmDtmGEVm4aiLsaIEEnLshOvyrVtJIA0qC1geGH2YSIo8" +
                "S4QRSAHQLmiAU1PAdqiUOJIluaZbStLoigzBQTlu+QYE7VEoPL6VZkO7GZXz5Sa87oOJFG+M4sPDQ9em" +
                "C0cfm0YTpj4kwYNbwC52W3ihrFU2X4TkdEcYEDFn4qbQ6/pEhOc/hU3et8Wuy/hafPY5o8zA9e98+UY2" +
                "s5sghmNLR6Zj4BFpD8EC6fHRy3HzgJHXeTlvVoqVAqPyVFqqEIk7wXr4M8Y6Yl5QBc20MAkThIBgPOS6" +
                "Meq8LdY5exJ3b7O8LDPOM6Cl7LYqKR1v9G89A8YaL4X4bRLshi0zvD1nI/I4jCfNuvZD2Aiop+0mGXZr" +
                "PGbGw05Xyi62oeIbmLvkgb1wcKwG8RqNg27HQLmLLVOFwd95s0BdK1zZoGm+HOUkAoiv2NJAYmmacfVE" +
                "jDtu4naNS90B4oNE4liNXTdNbVONHNfDkRS1iCmZa0wCHb3NdGUh7VN3GR63UzrKwZQcTjuabiBl715c" +
                "dL6NqL2B1rC108zoJfeeitvM6GKJyS7W0AZLqSMFZfgGXBOZAnX4SzBmu6MTWXrMO416V8MPvc5zomm1" +
                "Qj+FOWsR6KL2hnOshLT1zcrP0+pzbF7k6QQp1ERe43W9zgvnhOs9929Hu7TAK66d5jtRU7J/RPdwnNx8" +
                "xepvDeRqVnI1it0Q8GZW58grYK33GLU3TZUFTqaMIvHmFBEcrpQJKT1eHVQGE1A/Ufvhx3KKX3Yq0cG/" +
                "/SP4WA+vy/77i90PMuf888dZ5DSpQzKjgOccGbgx7ElhtWoV91AwY1zi3SJsg8z5hCR0CbhbD3qCByJb" +
                "ScWNCi345g5nfEOI1td9JuMVag4eqxDp1Dt7gOIBptMmKi7AUpvsp/Fw8AyvDLje2W/dq0t34aiNVy68" +
                "IqW1ATRKTH/tL7Q0qD/IQd0HlLboUdaQFXuETnZEvRu8vZBnN+pM/OUfh8jhw7PDc8xsLl4ftsSh0bqE" +
                "N4uyXJ09ewblh8yB2+XhP//CJBrKmArNjbvCeUaWnstuUDgNLmDmmJWHsChLqFi+UcrdoZ7lYKrTLM98" +
                "v0ft09eE7hUhE33JfPGadYOAIFVo925nbnmhcvHdWt8ApRva2BB1xNLJDYE5E4EB9A5ZAO92WXD23Y8/" +
                "vOQZGHq5QwDz7mN86HYa/3IpQGxW4XFVkNPWxuM/8nd+BsOmrcThem5PX/EbPBw8E9+9PH1Bj3hzDSdk" +
                "mOa6GRD219qkO68xQ0FC/Ab+nJNHlzqtchynrkCpV4deoUG1H6st/1C2ABhdsJlO9R3UgCvUtJZINpBa" +
                "U9KWYE/UNRZ9lWNUOIkDtfINRchwpj4FAGDo8DGkkyVy4vxtC/5rR+4C4+vhrxDG3D3l63e9UQ9CCz+e" +
                "/3bZH1z0RuDK3YvhoNd5Ga4fOf9EUQZxcrM4S/MuIYNAa/3xez21PgmpZ/g12JVC9JsLGtPOuMeL5Qqd" +
                "GjMTOFQju+68pzqs1xxycIucauIoEE6ocvXwa0v8xm3735s4S3e/MFfFvAx95V0fZOk6nxsEprdr3sa/" +
                "QkZSP/0WeI1Pv2MUb6DE/HdYUbMLxY5uEz6LcHOx5ZwKuWKm28g0qxAFV+awBrW35BqPuhf992PMkBp7" +
                "eiETTBQwfyuFucKqQ+0H6hn69JAOXdxWvwsJCUdb1M3CLbjxu17/7buJOELY7uG4polP6Rscr2labJVX" +
                "3hbEEdrCMe+Hfs7vw9S5ffihsc9Du2AT0fOOxeeKk/17nuuCmwF+CO9ThTx/1ybxgCczVALz/cwyW9U6" +
                "RDzF9Vhsor5Xq5Y7zvrGMTXasUTHv6BSO8RjPlpb6r3JNWM6j3aR534RQMWnuXfeiMnobu+LpIXpAY/z" +
                "fQLkdqO52RYRN2nDOWuj+96Y91QEZkXoXG61pJr3ISTLeJvcL7RgHz8EYWnpA08DVSwn0UFAdc3GZzJZ" +
                "zCGD+O+Gh72VeaWw/prxPfL6khXQiCeakOzYj58i3GPiANDxkYMVOefhGnd+ha+9bvBaDt8sRWz28JxO" +
                "1XnRE7HKk7GHZZ6sQ1sjxd+j+3jKeKq7mPqAT4It1eV7D/n5EFy1XH1f1/+hSSDvxDfY/vtGJH/CrxS/" +
                "C4bCkuKsAwquZh+//YQdxfD4HB+T8PgCH9PwePopHDV8fPmJ3j0WA77Qw9vpa+0999xZ4hWNjPfRBPcF" +
                "vL2HoXsA9VznUeojfpVR2RcM8WPLn5/AKDzIBL9kw9W2/YRS0tuz+UbKp9Azb+zFoYzvkPPXKCgPDW0R" +
                "5w0w+vmuA3YH8T6AwdJl+3iafMQOlW0gPdpzicbev0WDV/vql1tk3b9fk1a+/QCxP8YekL9C/zjCvHeP" +
                "taGAX2oysyaWDy/YusnTWLh7i7QB4ol09gHMtq+j+O/d4KVQUh337dqdI3d3d0ZxcKEvLdPVlT166aW7" +
                "p2m9L+VwV01OgFP+pKmGdcS3TfxFm92bPcfeBHlGfeuoAYKOdLIiyatUUe+U7oU0chn7rNbhZ9uae+Bu" +
                "djprIftxRYd7FaA1LKkVvhy4Y3R+GrbEhYu1TQskYO0Hrn88IM3/HXf4OWS8THbFij0Rr2LNC2RHmFRo" +
                "8Qpvax9/3S2X0PZxbVNZn0rX/s+rpsbLDDs3YB+4J9NwZve2KLa82v9sn20t+4w//BdMPv/yckEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
