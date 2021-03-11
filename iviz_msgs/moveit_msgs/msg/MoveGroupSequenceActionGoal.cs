/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceActionGoal")]
    public sealed class MoveGroupSequenceActionGoal : IDeserializable<MoveGroupSequenceActionGoal>, IActionGoal<MoveGroupSequenceGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public MoveGroupSequenceGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new MoveGroupSequenceGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, MoveGroupSequenceGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupSequenceActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new MoveGroupSequenceGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionGoal(ref b);
        }
        
        MoveGroupSequenceActionGoal IDeserializable<MoveGroupSequenceActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionGoal(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "33db6638fb44f932dc55788fa9d72325";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1d+28bR5L+nYD/h0EMnKSLQjt2drGnXR/gWE7iRfxYy5vHBgYxIofUrMgZZmYoWTnc" +
                "/37fV4/uniEVJ3dr5g572WDFmemurq6urnd3viryWdFkF/JnlE+7sq6W5flk1S7ae1/W+fLZabbAn0k5" +
                "Gz2vr4ovm3qzPit+3BTVtOB3+Xpn9Ogf/M+d0fOzL0+ytpspKl8JgndGd7OzLq9meTPLVkWXz/Iuz+Y1" +
                "JlAuLormk2VxVSzRK1+ti1kmX7ubddGO0fHNRdlm+HdRVEWTL5c32aZFo67OpvVqtanKad4VWVeuil5/" +
                "9CyrLM/WedOV080yb9C+bmZlxebzJl8VhI5/WyNL9uz0BG2qtphuuhII3QDCtCnytqwW+JiNNmXVPXzA" +
                "DqO7b67rT/BYLLAMYfCsu8g7Ilu8WzdFSzzz9gRj/KtObgzYoE6BUWZtdijvJnhsjzIMAhSKdT29yA6B" +
                "+aub7qKuALDIrvKmzM+XBQFPQQFAPWCng6MEMtE+yaq8qh28Qoxj/BKwVYDLOX1ygTVbcvbtZgECouG6" +
                "qa/KGZqe3wiQ6bIsqi4D7zV5czNiLx1ydPcL0hiN0EtWBH/ztq2nJRZgll2X3cWo7RpCl9Ugq34whty5" +
                "Q8iWbzAHXbr2ot4sZ3ioG2KtLJVhOa8vSqyJzIObJrvO26whz7SYB3nomSy5cCWoklc2Gta5uQJ3XF8U" +
                "VVZ2GeZatORbsEaxWncZaI7ehNkq41wXGDqAzs4LbBGgkE2LpsuxeMQoJbHhX858WUBhoIeVqSOps3lR" +
                "zM7z6SUwm6EH+HKz7LAN2zZfFLIOWbsupuW8nOoEDYN2bNC5R7QBkFpt2g6YZdh4aDX2JUSrD7d6K0iw" +
                "stOl2ynN7owg5Uhzf/lap+BTGY1Gr5Z5VQHTl2u2AyPb86TWF3tCfgeSZMOvSyBbz9FYOMeRc/x1lfIM" +
                "u7WaTZp8Vm5akZ9FPr0QFtQtjbVdU4ARFJ8FDlgQnGav8O26xBNWsHhHQScSABxdV+AqQ2w8oOazrlj9" +
                "8BY8XKzafa3ycHzfrJxSIAtJIBuTqMWtGKaekjC0bYvFijLLaOSTPiYVyjl3quzDPGvrZdlBqtmqOFXI" +
                "SQl/yS6sdWlA1u66wHYPOkVodiwD2dqusD9B/XZV13g7g9wgltjkZaNqykfT+SpcW3JDmU2xaj6YzAqb" +
                "W75VxbtONCNeHFP2YCr3sdtz8HxlaGLA8Wi+rPPu95/1eGqfi5uQUddWlqa5KkE1KDAKPF3JWTEvq1Jo" +
                "xzXMw4pCykW6AkTgdiNFvenWGzBEF8Qj1+pVTm3TFY3uIDa8rpvLdp1jYBHD6c4xrcAWsAZaABl9660T" +
                "SAHCZB1ejtTqgflBBdpRFG/W0IQwa7Jn8yDU/17DjGh9IHAGTRCM0xQUvEBnXbcyeyw/MCPG2AHCBpum" +
                "EUauCmUxbO7YWAECBJmt6GArNsXodX1ed2eCCzBquong5YoEnduS8kDUgHyKRFrVM9hoVF/cgXg7zp5C" +
                "+mTFsrDtRKsBDfOmAYvLqolCMp2yIFPLMPKCLDu9KGH4EUfsO5kREO+aXAiia52YbwoC4IF73pUt1dHo" +
                "SewBCSUmbwKEE3tRG/FByry6wSTxBRKxxqrISucQrVSu/N3Us82UWi7KYZWXV2W9FDtTqJzieajbb71e" +
                "mnqnsJVBsChV3WV/p8aEVtZ3RynKMvgQYZIoHcAEO5Q2GQlv/15MYaRALBGwkuJm9Ca8T+HH1rtGqWig" +
                "mUxJNQ+ZrKZBI4wKqSENuT/dDDgma3KNc7El+n1dwdAicvvAvtHI2zH4ggqdD7YZACyxuhwuftZr+ACk" +
                "g8OVnhPCCnA3q3M0JmSa5RGEaAZIQFJ3BcEA4y07u6gbMDnIWC83JkYc/QbGeCMmnpr9ADwJVkPeQbCv" +
                "IylX+btytVll+aremG6hnbaDsmSW5bK+Vh/GN5MY52a5HwXZbA3jsGIBUq7AYBfLGaZmbZJMDRnxkCA7" +
                "NlPhlgQ1oW0GvwZ2YUdeVXswn06xhUlVMMg4yx4bclf5ElJWthtQO7x//OlbfH1zG8CbHeASs9E2WEP5" +
                "Y6KEkFfkau4ccyhKEIhGDHgWHhjA2QQxMjgRGoQCkB0h2ykkCbRsiCuM4CavFsD4sFxx4fKqW95QLJat" +
                "+ILT5YYG8nkh4hgCEdS/P75/pMpZxxEe10+meYS/hRRc00/H9wUWxLsS+rAcF+Pj2ygCgP0vKXGOogpG" +
                "o4l3mrS6tBPFqNcm7T5stx/VvUP3BeXtHkVQ3rnQEmSLOpEyYb6B9OdGw1cRadj64o9iC2LLHMLMrd8d" +
                "kWNcBjjf9LcO8UoDBAYabt6N+lzBBOTuWNXnJTQb9YnYUaasFDAsv+tiuRxza52K4lKmEA8WjZtiDu1J" +
                "e861IVDERBts6HbUD8a4QCghL7Dw2sgFHbodqzFvXO6spniIThZxCS9+tChqUA2iW0j/jcjxhwQ8UaBD" +
                "2fOPHwo8Z0N9MO7aOXLgKchgYA/zgtx0JR/JGHN40Jnw4TgLVr8sPA1dmWnoST1VNhQoZn804lYf0y6c" +
                "1ZAI0NGAscovaSfReaIOhzqHRKP+rNqlCjO8RpfDYrzAjhcGk1ZqvdORZ4AKPnRTLiCMpCcGWoXOeWaz" +
                "g8SZP1A1IzjrYFgaeud1Z9KBAu+m3sBywBzwo7G4mGgwx0u8t66uj8n9BqJP0VcieHxvYlt2YFMwusuV" +
                "d+HXTfj1015kSbRFbxchZRVJmJ9D6Pc5uOMyQgKr8RbtZHpxrRsTdAcgKS/bEZe3bnT0P/OjWsLSLlrC" +
                "8sUiMnTWLvKroKiK7PTlF2qCBtUmhnUK+jnbol0yhHSfzOr5ZDDY466DGQwo03q5LFvOsz6nyQY9lvs3" +
                "rHmLNZVZ0DYKNDgaef8n3v2l9IYt7L2xg+3TxCBz3C+WOYI2cAURPSUHg5/NuaAKnIKVo7kk+g67iTEp" +
                "SErup/l8S7QIhuqhSP9EMq/qFro4C3pZQ78eWxSfwad6Xs9omhw6PmhIG5rR3mUBJ2BHYwyEBc+Nt05O" +
                "ILGKk5PE07E4mjhfEquyeEqXsNzR6Lyu6ZVMOLkPJ+52s2DYANRwYRcIB17US8RyfevDgpk2JQWAxi0x" +
                "O5m7KVpIF7jGsn0arDpIpHuAUjIGO7UTQ0dmlhw2BZUv3zfwgcqWO24KFYyBRRvSOmMYG9HmdKe5qnEo" +
                "+UxMnqPj2DTaQsOm91ppfA+xb+7Q2KWYY7m6EEpXzypYkAbgxYq9X8CI4sTEEdUvNAQkNEmGm0H+6yaF" +
                "dUmRII6CEkLVddjH7c6Yr49nvrAFaMFhCIGPaSpvtdG8A6NpvowiP3QneVcOISsoEXKMqdMYysAWUUGR" +
                "ZnmUP+Jdw7PkCB4BFKumT2FBRpRj6nHTaqKek07WHnMnh7n3MM6+pWqjllOtY1JUZlHVAGjrM4jlE9bq" +
                "WDTWFC4NNitnHZdTowL0mW6MG0k9nY2ubTJ3C4Yo5ZxObfkTxD2mzNilwEl2jcR7xPLT8HfggRgHj8QR" +
                "19ORptlE84AGqawglOPQpFNnE0JV+MfUJB59IySvnOGTV0qB/ciUbd1D2fLajSHVpFh5FQNkIFtfIUfg" +
                "slmxgIklvEdba1ZjaemNQajU1y60QZXNtNvQG5tncUDlZbXJQH3Y9AzHMfriW5R5ufYGlojYrBq0BguK" +
                "jRTsJu2zKLooAuhehrjNJeSSyKlsegFbYZx9wd3wDnmRJWO5Yv1DYZi8YGy3yv76+vQLEWsPqcYPYSDD" +
                "Q7zJr5nw0qghjGH9SCaWIHlM5KXYKSHxpykBRftyU/e+i1nJFg4NexoZIcntMCGDCfdw+H9Jtl9Jdk2f" +
                "7uIXSzJv/n9Jkt0myNQMZfd24Py9cRZGq8DOW42uoZrYgH8H374VMuGj0mtfrmPAe5fzKLo9SJaQOLmu" +
                "t4oC2oF/ORq5N5z4gqO/bNChqSgD3GPb1zzj0Du9ZEh35Jld0vfmwqcfI+IkxfscwfDrem/LSI4KM3Px" +
                "20ZDsj+l86a+ZHq9Eu+8pYtEN4ECGdFAyRtIyGccltGaxGdrt68J6vbYtXZYEF2kOL9jKHdGzyibOEda" +
                "l79slgIsPqpnsJ8g4S0+qWdwB69DvD24dZbsnpfv3IHR0BWoRQ/YQ//87ZF/IWUva4io6IZJrIscdURC" +
                "KQa6YjprgMXorlroqc/NZjLgXYRirKZDVoyTFZBIjahPPUbER2PHMAYY1zFP++4ueO53q1sSppEkHjgA" +
                "c1t9QilQj/qZyyeRjZi3DON5FIGxiiRo2tUbiGQkBW9CdPuTgJiiIVFWlD3NbpJUZNIhKhEBNtHYCk2+" +
                "mNRUrMRdgh3GjNqwGAurIaZjSMsrwhq6WxFGmApAW9QN0o3ZcBjUGgqcLmGAM/6GLJDsBYNiQX0foyqm" +
                "VMdImXG0BjF0qemyELANzOVj7Y8FdJLMWXTWY5INxCB2ExvCF+W6QEFbMGAGi4FY1Ty7rOrr4C9Y+/1s" +
                "yx3b8bEZBhI+nAl1QrDHrXzZNrtj3GB6m6nR8FAYSGBhAVmq8wwxqqRESPv5UqPET/mC4Y3zXOtWhEBh" +
                "A+nfCR3nhRYB6GR0Dm8IgWBi3tSjaCZ0Jc+1bd/5ttVuyCCpcOBm2RUyT4N9ToAzVIfMwjBTxk1WQPAK" +
                "xBKho3hKq1f+iS5jbDYM1La972QtrVh4XrQXfah8g7Yr/bATDr9FEJ9zf3gelvE1lFCYhZBmdbNzi7BK" +
                "M/da1KCIpSpYsdlM1gIClkMcpbixrkMmIiPdMkl+i9g9ntH7SxhDqR7yNxJPlIB40gg8iqT8poVlXLyD" +
                "zUD0EdlTlSoyZzw6v4FZ9/j09NF9DvNa5GpvpHlT09GEGV5dlU1dSSUDg0YQEsjuoTqhQcGkbgUJB3fY" +
                "zwohyW3OjnSk10+fv/zm6aNPZU7rNUUVfVrnZvOCTbYK0uYwvG+unqfQTj5PrEKc5KtXT1+cPnpgcjiO" +
                "uXs4GQXlCcW1cb4ttSRMUNEAR8vWzR0br/tbFvNOnRY6zRBoyJyTViCtS4woUBGuBCVniqLQ5iERfKlZ" +
                "fE2LACYeaYx6Q0vyf0iT+v1iBeLxV/+Tvfz8z0+fvGGt8K/vbP+QPk9+Pv0hclOignNReybLIMkYvaBT" +
                "A9ugHSReu3qhEfXgT2poF6zCAHrPtLgsQrw2HeFE3mj/GFuVhJxwDIRWlc3OXd4DigOcnaeomJqVgMqf" +
                "z16+uMd6DIuyfP/4+dcMLLFELXscuBiSNuyBJFNHWe1UiZEkVe2uU1CmJLYDo7Jbqy5bSbz8ur6E1XJZ" +
                "nGQf/ccBKXxwcvCE9s3p5wfH2UGDaj28uei69cm9e3BF8iWo3R3850c6RVZJSIGdhHgqE466embjcHES" +
                "KtB+LLsDdGLFGzbCZVFYAfl8id2KdDXcHS8D3MGwzHMoET3zePq58oYA4ay49W1kDY6QuaxqzEJlUp7O" +
                "0JlNVsL8AuYkCwSQdyQB3g1JcPK7f/vDZ9qC2lcTrWi3jfGBjXT2l6+R1oCVwNxGWKfewGc/Lr/yFgpb" +
                "hsoOrhftw9/rGyaTTrLfffbwgTyidcMGMKLra2sBzY/KvNngNY0UTsQH8LyYfkVxymbJ75Jc7er1gTM0" +
                "WPvDxXBvMxnuxJIEyei3azLbcTa9gY0tphs4rsgsCuXuDjjDMzfgLI8+wc45d0MAwCj2qdhlM6oFff8Y" +
                "/0NQgCcd/pB9/vI7KDP9ffbqq6evn0LB6OOT779+9uL06WsIdHvx8sXTR5/5hncRJbqGOFkrtdVcKiDx" +
                "Af/CMraxaYycxxah1gH5faKfdkianWhAUGrdmGj0KkS2JbneubA6iH0OVMVJVtScQ0xcUFU34rvj7HuN" +
                "8f4txZlEFs+pqBYwGb02dyCG6D+F+YHo40jbyXewS+LT94HWfPobdXmCktLfsJL4IJedkhN/Le8FAap4" +
                "QqhRGlvtoNcOm7+jHOR4KNzJ68enz/56RjspGdMXWWBygfVUjlJFWUdCEVJ94UaiROhtqL9lKE1h8VYo" +
                "u+jBnXz19NmXX73JDgnbHo7inDSxm1A8zumi52f5XsgOuReOdDyKOh9HZ2fj6EMyzm2jsByjV3ftLsru" +
                "MaG1NSrgn1iSHKz94Z5kNsAqvrUqFomXyENCU/an16k1UseW+/jYiOqbdEDMwFKDydMqjTt1q3EkDBru" +
                "KRJGZ0C90GYrP0WrdBgKkwWjkaDfNQVNgicRT0TkteIlpOaSUG3Sbn9zBDIh2NeLUKVZdIRGPDUWZ/ye" +
                "0Ow+dBEdzaCBEmzpXVJSwNnWXdiUCETCmvhjImqtglSK56TWNRToYJrMg8HwQdX2iIO8MQCSdDBYHCAJ" +
                "5XkPd8VgBVrloGCzg+6SjtVOe6OWT2QX1XxmMPoCXnqm8IeHimrxbiLBwT0hLL767gSxJlCx69Tpj0GB" +
                "EDnI32UfMyz4cTb9Cf83yx5l4mbn2ckjcHox/+H+W0Yaw+OnfJyGxwd8nIXHh29DLuKHz97Kuw9Hg/dE" +
                "9+4M4l0702aDPs5xWub/m6EeBI7UxCSnBVTAxHIXK6YOm/KH46Q2HA+9uvC3XKu631rLGt6GiHoyluo3" +
                "PWCmZyTFOA0Rk35teciLsjYGWY12kOAUeTGY5hhzH+2oxGi3SzFYIhZf9qa1XaQx23hkAgbBhOEh1uo1" +
                "ewvRhjMaP1dWvbzltGByxMNpnh4k8RBOOD1jZ2wkp+4nI0LAX2rqPWOshS/K8hHJsCF6Z0te2VL02vn6" +
                "9Ju+jAq51zpR1P0O3yC4q45xr/1VeN1rvpc1G9CE6xaeduj5kM861/CFxL89bSLemZM8yhpXhPLZ+vrh" +
                "KdreP4QhkMtBOA6J6mkxOQf7Xx/H4T9OviGYdFW8DcbEsI5p2HLHe4EugU5LYsTjKDGnExcjO0S8qGYN" +
                "HSKrWFg4pF4OqtEPrRxNOTh7guyOWg4/FU1tZxhhF7SxkjQelVEk9rLi2wx++2a1YyQ9MyCsSJztdqWG" +
                "urta6SxJwyFBxaxiXG9XPlRjRvM5k4Q8WRRSj1LjcBRldt4gLGi6oU7aXbPSMAlLo8MthwQUBIL55FYd" +
                "0hGwEx23Yp6NPC/xjbaMjSZ62u9/HYPtR6b0yZKEgHKnKdb04alSxxLB7a35qG2D2ReGBcUCl/VxOUO7" +
                "dXX0S9NXtsiWOUtrZgdeL0OFMAN+cb7rWUww6XUJDu3YMl4hWxEMCqZZZiHJHoJc4UjRAisuB7y3p9jP" +
                "pd0+KRv6vRNKEm974ZWdOvRXCaR+ydD7hZJldXq9LHKRyKu4OMJbsfBoQLZb/PP/qQD0KkAJlnwibldW" +
                "NA0Fh+uwJPMZT1yey0nQYvJuwo6T0Hi7xc17W/w0bPHPKc12GW1eDJRMOR7hk+JOvKJJFE07rQ+ele1U" +
                "M5SqeI62KkvC3T6yAaS9lDD2Qnh5ACxVLrDFNHmGIBwzq5pSAj8umDbyiyJYZk3Azw05ETERP45GUPCJ" +
                "0XQjPBk3Rchf1edaOK2SOHYnGoT+Aut+kkSMnD4C4MXLNwCulWIrYMBzf8kxU8xWWKRNLodwzEONs5OO" +
                "x9MbBYuAsUNNzAI//hOra0mLqxJZ4xhNVqqAr7fNIpHTh1J7BRognXJj9bBHfjxceL9FkilbbxoRmuOw" +
                "83uhV1lG0WPxogTnETorJCN2eDRU1Sqxay0SqZ4C/KP7mn4sWXSMF730huZBMNSZVxCldlg0rJDqIqeS" +
                "HxntX10QpNixHtrQ3NsOBXJmd1gFq6oVqfGF1ykouZ0zu/o6b6x2wtc0oKxMnw9ZLOEsrfVaY3HAPVIf" +
                "o8GeFe9JkMTFoKpeTj1qm8+0gRwL0KM6lcgzsTMUo2Rgu7uBfku2vgGNSokKCCJ6utqWNV+i7Jncmz0U" +
                "vkbKwM/fE+GJDNpbz3BOuL8TlXcji3giVRcIlpINGM+JDvQXAETlb2QX0aA1XeMdZxxCu5BR1pOnLN7u" +
                "eotEmQUax4OpTgwBJqpL5SECn+gtR+CaTaEFOkn1zfaeY9ZAU79QQpL2k7Lylnc13c5udoTE2e0pM/lM" +
                "rS8uIj/RmtjaccpvAxbzPcHqmaxd6T1X58U05yFVrxjbNh5klJ3lVyZjlN5+SwlgJnuJbUUyGaRLsBLP" +
                "HWqJTy7cKnTXRZ1SD+xC3cjH2h0Yy8icSmCFqlUuDDCTUlM2Mp6aFslROTuhy8C1fDJJbPISIhcE8Zkl" +
                "o8vsXUAHxvBDz/OdAsCZWreQuOTDqndXEm60RZx1b+dSfUNi8wtP7gnHKESvSL1vGhelJYqP2sEgPctS" +
                "1+LQFu2RQ2SiBqlzFpm4srORd4N/Vd57oCOk0IEXS6hUhAJ2IizCqUSZMeuQbLKmBpOF2ZIDdtNQ0CK5" +
                "wTQBjuIjKScvGrxm5en9Y8FPD2TfD1W+3db6J6p5NrjbAe0m0k4ll1zXZL1EC7bwcDBJngKvBz1kxy/p" +
                "AfWnqODu7tDzrhy2eMiZi0ap1YjUFWpjFTJz1QPlktoCptiU7RKm41zoXYUbPkRHBqqZ0TEgj9SN+RUc" +
                "PVJJ95RWKbqpsK1DFfVgw7iF4iGOYPx8hwTFA6Ty8efTY6Smma6wHPjTF2cvXyPlPngRM/L24rtQ/2AC" +
                "U9YpjP1PZN/fqko0ocsXLsr96ozhSRvlR786c+BqCgBRR3txUXbenuQ+ih74Al/1bmWqQ7A8OZQYcyCD" +
                "m6n2HpYe3Hd4JxbdzOc95H2f9CtQs0N3oI/C1Yln8iHcRSTt9Li8Fp0Kj8/lYoF4jY5YLZbF1SORqC3f" +
                "NMxjVOkVhHpD0+AaQ8q+4DfZlUvWiVs900P77DphScyvwENqCHzG/4J+BpV16r1bZsSMs+qo7FBOfuje" +
                "b+F50gmC69QWxcpElBTpVmI4pcWbhHmVQ8bLbYus5DRzIKk71slwvImOl04nCFsNHl/9zGTiVFQx3jIZ" +
                "zf24Oom3ZvnNVn/00RNJL8W/8H8g3bfvR4oVSEJ1dBcBMLtBSKacSiFgNS8XPC2hBn0y1d59WhiVqyZ6" +
                "Y562EjVvC5nSRHRoTss31ZBTWn8+eb9NjuX/w0xX5EIx6r0GR3Ns1gmj6F3FXOYqGR8xSZY1kSekLq2R" +
                "U8paWU2WropC7goA2EC98X3V6rtnBmVuM+nTlzS5KvNdBHUi9FRpm8+LSdgsSIzxHtQwP0EONnmt+VXJ" +
                "9okLiAwoJxI6erV5uBzQCgrd77MLOf1ONVm2uEWxlZk+DXaXbMuiIrH1ajZa8ZtK2DgXO1i3gdTwhyjg" +
                "Npfqd2F4Z53M2Eo/bd3QNryZDe5dzplVwYTaBXPntWw2wgyq+2a/0lxEMPWS357Xl9r9bC0pCwM7uS0n" +
                "vf9RXvfvf0yv5EtvautfCsNUkI2jMKSRB14T59TO03LrzTbrpV81082zw11eXsw1SY7qtmPJZmdAp8oB" +
                "wYleHp2cVB7dlWn7zo9HgvQs/sguuAvHoZ7rEX2/ay9el2PtuZ8BT4ICa06vWrSjr/H0Sh+AiUSe7Vuv" +
                "PS9pK7Q1r+wrvK28793/YzXIx9sXAaHSQt1xrA1Mi1WOOsGmN621uKxS5Uv3tV6KCxVPPSmBV7YF1Zzp" +
                "3XuyGo/05AOIUjdMwGvxtoLy9HNvyL5V8G3d6C2py9mebhfaz3U9P8+DO44ppxdYiM++ZfWqgTsOd57f" +
                "df7faokMP4rTY8NhTiferOR3jxMMWv0pzy5gdz/6yGr1r8vLctzU7bhuFve6+Uf/3s3/dC//d3Dz9BKA" +
                "5HKFM1Qd0sec1VM4WB6Z0ZvzpEwnGDNbaSETBn10M+WZEG7z0lY20re8stSv+vBfezqgvFMCuLnv1TUg" +
                "AsJXoQBJTQuRCKEcTJpYOZj1JtdflTMWLPJrGTvfKpDgeCM7xqL+9mal2Vu7ua9fFZWONpzCU34LGGl9" +
                "1Y5c6nAbi+TDshfLOYYkjq0dFxiYIEoMNUTCtUORQuPEstiep9viP24KECQaY6WocqXwYQWnvJKAgfv6" +
                "tKX1/JFR8FbMo5Ea8hLboycWn4foMI7xpU4FSMiNY4LG1uWlwhgSchJzDqrvIkMHOQj+QEJWPWyludWw" +
                "KxUtuWA38AtKZjTpkfcqe/ziNDn+FhnNAExSFqAE3PpkK/+bbCPhQe6iweVtcSmg6aaX5mOpETjzafjj" +
                "XjBPtDfwFbPH3eDbbhhw1R9vh0xvCAiFVGYD7G0WYlX84jnYnbE/Pwe1TfZTuxCNDpe+8cAfZ6PBeggm" +
                "3YyM0aPt9onycEOvbX1pY/8NGgH/+svPH9uHD/9fvQkj3lGi0ikJvxbh13n4lf8GXoSYbKT6lsE5vHhC" +
                "wlc77pB8k1ikIkvTe2iiGxDh86jfyHoYA+jDtxB/EvGzjx/w4O/PjK6hy4enYlHTL4QxpndGQN1IWAMd" +
                "mqLYXRWTpowxCk9x7PJy7NpBtYt2ZOQsxB68Vj37YABZ9LxrBr8N3f779BL/QKrPGATnf9hEux7KwTae" +
                "gLlXT6ebNZTvEbWIOHTyBvFxhGKVGofj8+7emEYCbna2CzwVkByiXsIDSi1PrYCiq6Xd+1LkNfOlGrOg" +
                "z7/CuFb2wfwlr4i3bhW83VA4rmlWvTmeyRidBsL0JQTtTyG7qV31kl69A06Omo0lGCWHdGlEXDfIolnb" +
                "lsGMP1C9c8/cGf0Xyp/ziPFrAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
