/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupActionResult : IDeserializable<MoveGroupActionResult>, IActionResult<MoveGroupResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public MoveGroupResult Result { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new MoveGroupResult();
        }
        
        /// Explicit constructor.
        public MoveGroupActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal MoveGroupActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new MoveGroupResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupActionResult(ref b);
        
        MoveGroupActionResult IDeserializable<MoveGroupActionResult>.RosDeserialize(ref Buffer b) => new MoveGroupActionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6ee8682a508d60603228accdc4a2b30b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRrb+rl9BrIFre2srjZ2krS/8QbGVRK0tuZKSbRoEAiWOJK4pUiUpy+5i//t9" +
                "nnNmSIqWkyzu2osLXDe1RM7MmfP+NuN3xg9M6s3lo+FP8jCJo3A8WmSz7NnbxI8GuZ+vMi+Tj8ZlcmPe" +
                "pslq2TfZKsq9VD4ap//mn8bl4O0J9gwUj3eK3Y4HZOLATwNvYXI/8HPfmyZAPpzNTXoYmRsTEdHF0gSe" +
                "jOZ3S5M1sXA4DzMP/2YmNqkfRXfeKsOkPPEmyWKxisOJnxsvDxdmYz1WhrHne0s/zcPJKvJTzE/SIIw5" +
                "fZr6C0Po+JeZP1Ymnhivc36COXFmJqs8BEJ3gDBJjZ+F8QyDXmMVxvnxERc0dobr5BCPZgYRFJt7+dzP" +
                "iay5XYK/xNPPTrDHX5W4JmCDOQa7BJm3J+9GeMz2PWwCFMwymcy9PWB+dZfPkxgAjXfjp6E/jgwBT8AB" +
                "QN3lot39CmSifeLFfpw48Aqx3ONbwMYFXNJ0OIfMIlKfrWZgICYu0+QmDDB1fCdAJlFo4tyD3qV+etfg" +
                "Kt2ysfOGPMYkrBKJ4NPPsmQSQgCBtw7zeSPLU0IXaYzCoPFI2vigbTT4FZKd4YP7U8A/OoPRh6t297zT" +
                "feu5n1Pve/ymWhpZ5s39zLszORVybMifiQreMkj3hszTG9iBwmydDTsf2l4F5vNNmJTIKk3BWSjh2JBH" +
                "3wT4qt9uX14N2+cF4KNNwKmZGKg21BIih3rwDbQ/yz1/mkOTw5zUpxSQuRU7iGeNEtH7Pzv4H0oiXFCF" +
                "g1UuI0MIYZ45KEB0b2jSBawvoivIzb5FefD+7KzdPq+gfLyJ8hqQ/ck8hIsIoIcTcmG6oh/YxoiHtmm9" +
                "7vVLvnCbF1u2GSdCerAStSxx37pTsDJfZQ21IktgBlM/jFapeQi9fvvn9lkFv1Pv5X30UvN3MyF+W9Gh" +
                "QSWrvK4uB1/HcWwmPnyqwCw2W8FP5j4wpYeApw7jGz8Kg4cIsJpXWMqp9+oJNK9QvTjJxQhL5SuEV3D4" +
                "rHVxUVryqffDtyI4NghVZiuG38JdyOS+tDaRjqdhumBQY/goxCB+mZiYYIOIqpr8+G8g4tvYTKXYMD/d" +
                "gGHjAZ246A2GVVCn3k8CsBU7ZtjoAUheAKkRiFEm+AULCKWpWUAGBY8C4dv4G2wvI2xkPQjQ4M86BPmw" +
                "HOy16TobO60oStaSj3AiTAFfkjJYARkbqGhjXiW14pLAjFezGdloJ+XmNm88YSjrnEuWZOOuY1KWU9yk" +
                "R2IyWLqeh8gtJB5XXIpohwmYC3UkdZHsagufsN7E1B9QaTIyCCmOWSwhqyjCasLMVHhrg60L0E71oJIm" +
                "pUsRjKqpgsUf3sWmF3DFQO9uUwpTY4KxP7mmNmKF5q9IJ7PMnxkVTbY0k3AaTpwxCAZZ00JnrqcTgNRi" +
                "JUYBPxdiVtMJj0nII4luAVUMc5VbLRnHnnzTydtpmqRnCTlg+HU0wXeMVtf2k3GSi6FBFD5DQpLejcSq" +
                "t80cFnO8ZeTHsQlG5bKvLFAXUV8xjRI/f/VCwYFpI5HnE3GtyqOG5uMITFQhWjpUEUKd+zdhktpRSTAG" +
                "g9Pn9vlNq3Pxvt8+/Yk/Dfvy6qLV7SI4jDjaPj89dLM73Q+ti8756LI37PS6I847PTyyg5WXIzuxhSA+" +
                "ev1x1O5+6PR73ct2dzg6e9fqvm2fHh7bZWe97rDfuyj2emHfv++2Xl+0R8PeqPXr+06/PRq0u4NefwSg" +
                "rdPDl3bWsHOJLXrvh6eHrxz2Lu07PfyBnHCC8f7Lu4ZHXfiogApbV8VyvBsMW/3hCL+HbZAwOushRg5A" +
                "FDjw/ZYpHzq9C3wORlet4TvM7g6G/VanOxxg/nPHzLe91kUd2FF17EtQjqsTK0NuEWXzolGTztt+7/3V" +
                "qNu6BJefv6wP1iBhyqvalH7vdc+SiNEfaqPIGn5xwH+sjfVeM3Fzo9An+Jg7eL3FJpvf9DFhBAS6gze9" +
                "/uXIKeHhkVO0gllQl/bZL9RF6MMHzKNSYKLjYAVX/pYxxzSrMJ3um14xBmbtVNVgA69ub9T5ZTToXbyn" +
                "JkNFnz+FHZcezNX3zosjFWKEQMEfI2YQY8QfpOU2h0q5UOozc+CFTdOUt8skCzkx85KpFB1/T0BdJlkL" +
                "qtfrrIFMIIMzlc1/5qC6T5lHzwlEgImM2BC2QPSBGzGIE1EeIqf0zntvPB9hrAwh6FyYDdCXnIt5lS1k" +
                "+ShIpqPaZi1k15M5oEySKAoz0pmM6WNRp/tuzFUPpMKzZbvwYL/h1p+55T1Z/ekzQr2OIHbYoZGFzH3f" +
                "RD6iXBywbSIpzdwAKjMfJlgTNFmYBGgxy/5GiijPIM7yyAvC6VQjLaIv+JAXGCYCRNZXejaLJGMFGy6W" +
                "KKx8ZGPS83FNBcmKHanjJGBasefwwUTmeWzzRAa9hy2T6fOnwEpV6+Rkgizj5KQSHm3isVqi/yDBPVfk" +
                "84rK7TfGScJEc0TiHkv7tytghVN+YQKifvMkQgdHG2EJUsxskoYQCJkgGqSEZyge8AVhGZ5dbCeFyMEf" +
                "NYAmultlaqiLDBiow95eam6SCOGdvF6mYSYOYp/YBGYKh8E0kM0r9JiqZiZb4tlB8QMCWOwflFPRPEPS" +
                "lt/dn/osk8nP0PGieZZLzBSyyosGmr9cojZADyGuAuguuLq7j2wVK9slLcxbJZGjtgXI+9VCkcbSH8Ro" +
                "L1lGaLe0MOJsa4bs9hOuZS6dhXqh8dX0UC3cm6PdxgTG48QozkPNyC3lFiJB6YthTyWj7gCzEDmr1Wzn" +
                "fPw09e+yA9mBNiRiZGtzk8OCDMW+0UoCFgv/2ugiOx+0U8OSJSXqR03vb8zuTXPW9O6SVepcqFARJwBo" +
                "5VPr4BHW4oBLpIiDpZLqUpyCt8cq4c5qI7mn1KhsK7TbykU55/iUhX/C14NkMFLhVKyGs1AN3bliodCB" +
                "smoomUMtKJAGo9Ffm+SM1yLBJhLbzY66LQngUUV/bNaLR2cIlVdO4SuvlANP4VDuhx1Q1TfsQMMSNIZC" +
                "7OoDqD1WuMKLQsUCM0M9J4o3xZcggVwBZwqPkqyduwZLVpOcjSxMK/dTRdb6EaxfFb0M39knW/E2MRLO" +
                "Z0vpUbPAYxUTZwz3umZm8tL+AdaPErt7kcV4kzmyhKb3hqZwixIygo74kvciVFhngYiFbd/3z9+ITztm" +
                "AN+7hbLin79mj5seCE0ztL5kkBpMLav07qvYKSPxkYaAomtp0RvjgKozHDQYNOoSKYNZu4LgDRz+3409" +
                "rRtbo6mO5d/qxtz0/0tu7CEvpgkol2eNmUHSlqNtIA5k6FQYswp1vjdpjbjECfysjf1N2IRB5ddjOb0H" +
                "sHacTJ3Ls1G9cCtjk68N9CJfJ/cOAUV+dHjICfwJPFnjgzQ6jnV9pFb96woL0pgOIE3UpT4NkRaZLST6" +
                "CDocq+HvFY5YNGphmHZDp4qVksZTZ0CDdMZSyZJRT+VekIAfSLvFi8HUGGUki6E7hjpWecLXWLJHYzvQ" +
                "5qDMYqiQ41c5sIWvTsMZOns1NyqO3xJ34OXTI6g0TEpw1s0gQnb5LLf3m15nKga6JkFi3C49ZmJs8ZLT" +
                "ijxJDtjGtCA2GXolRuRsFaVmDjtplk2t2+JbEcy9P59E1KWObZM2Qjj6ri6cb8icT3+UCkomf5Ug9239" +
                "RLYqTsOS5QJsVtYJm/SM0+SaveZYVCxj+csSkCHXj2dyls+gAWfnbNVOKZ/tvKehTt3fFqlBFCqekrgD" +
                "GBWQl9BDAlk5fBuJAqx81KrvKdo1D7QabHiuvVU7HleKdYlVuO4R3rrKlEYrIZN9Ddd553eJTuhSCB+L" +
                "XpA9PVshDcjmPq6FCJuQheObO7S5h5v6hWonhdNkwx14Enu0IeIirQIyTmwPpgmHxXaIyZHo0S3Z/snO" +
                "Nnium6L1ZkFGwQjdIGjUGaVA3eGHreWlX1WeAxb7ud4QO1DAKFlbHJKVKB42Rs3u80DksEBM0WBKHuEW" +
                "S3CnxSWSAsXULigTBAE20o4Zo8i0OLBRrKQORo490bp8424NpCFlgcqD0UeJlMizIIyCFIC2QQNOzYDt" +
                "qJQ0kk0iVFYMHzgZEUOwUPYPXANC9ohxyAn3jfMJ7pbifFSu6PDSChMp3Zji4xGYbdNVTkrKJkzlpAPH" +
                "j8BuZLdwQlkb3E8qktOaMBAxp951nKyLQtDOfwqbvG+LLZvxSegLhDVF/86Vb2Iz9QSxOHyzZFoG7on2" +
                "CCxIT49e9qvHZLrOyRnXtVQpGJXHPqJvYrlTWI9+jtgOmcVSQSstSsKQEAjGQS4bo9bbsl2zJXF3NqvL" +
                "QtFFzoSm1FuVko5X+reOAYOEVxvcNhN2wxZA8AbMEo+jeMqsKzfERkA5rZ5kZBvj1CtMwk6XJptvQuUb" +
                "zF3owFY4HCtBvKZxyB0PlLtsmeJcx2YFWUndQXHxQKa5clSTCBC/UkuDxIJAZAHvyi32q7hdcSkJkZ0e" +
                "IJJjJXatgGV9RTGU68WRlLSIJZmrTIKO4sRulSHtM7fIFIg+cksNpuJwmo3xHVL21vn56ffcpi9OdWOn" +
                "aZqwg4D6Kr4J0yReMNllKxAe4g5cQhmOy29qCtLhz2HMWU0nwmBfd+q3L3sf2jg1JE3LJf0Uc1anzba9" +
                "YR2rIG0rwa/R6nJsXeTohBRKIq946ez0yDrhcs/t28kuB/CKa6v5VtSS7O/JbRIrN1exurPvyExzrUbZ" +
                "DYE3y5KIvAJrnccovSma0OBkoCgKb46JYG+JU1eX0vMCnEmZgLqJiRt+LKf4dafS2PmXfzw91uOlz399" +
                "sf0hc86+fJwlTlMavVMJeNaRwY2xJ8VqFVmBdOSYMUKEJmUbZKYnJEWXQLv10BMeiGwkFdemaMFXdzjR" +
                "ey6yvmyXSyUp6gKPhQs5Y+fsAcUBDMZVVGyAlTbZz4Ne9xmvX9ne2cfW5YW9NoOWeaHCcLOFAVRKTHd5" +
                "rWhpSH9Qg7oLKE2vLVkDG+33hC52JL2bJLlGvnJtTry//GOXHN492T1jZnP+evfA202TJMebeZ4vT549" +
                "Q/nhR+B2vvvPvyiJvCNF9LRxF1vPqNKz2Q2FU+ECM8cw38WiEMk+rODaGHsTeBrBVMdhhBLHhqdt+spz" +
                "K2WiK5nPX6tuCBBSRbu3O2vLi8qlN0RdA1TuGbMhaomVkxsBc+IVDJB3ZAHe1Vlw8vKnH1/oDIZe7RBg" +
                "3n2Md+1Og18vcFKFFIHHVYWcNjYe/BG9czMUtmzl7a5n2fErfcPDwRPv5YvjI3nk/StOQPqcrO0MhP01" +
                "+ja118xQSIjbwJ1z6ugiCVYRx6UrkCfLXafQUO3Hass/lC0Ao3M103GC/m+2pKYdeJM7pNaStEHdjGcb" +
                "i67KgVq4kziolWsoIsMZuxQAwOjwGdLFEjVx/v4A/6EFoNfwXvd+Qxizt22v3rVx7eDIPp59xN2G83Yf" +
                "rty+6HXbp3JjYFjxTxJliJOdpVmacwk4yEJZYY/fy6nlSUg5w61hV4roVxdUpp1oj5flipwaKxM0VJNd" +
                "t85T7ZZrdjW4yRG3rQlBuKCq1cNvB95Hbdv/XsWZTJaCycQzJIsWo7oPYtlU0AemN0vejn5DRlI+fSx4" +
                "zaffGcUrKCn/LVbS7KLY6Tbxac8xeWnXOhVxxUo3zk5D3DhMprbMUQ1yeCjcUb913nk/YIZU2dMJWWBS" +
                "wPq3FcoVVR1pP0jP0KWHcuhit/rd85FwNL2yWbgBd/Su3Xn7bujtEbZ92C9p0lP6CsdLmuYb5ZWzBW+P" +
                "trCv+9HPuX2UOruPPlT2eWgXNhEd71R8tjjZvidCtjYD3BDvUxV5ft0mecATplIC6y1DnKWVOiQ85XoW" +
                "m9T31fLAHmd9Z5nqjLTGzEKlasQzHy0t9d7kkjGc+Dgu7n4RIMVneu+8kclovfcl0mJ6oON6n4DcrjQ3" +
                "0cLWJm1xzlrpvlfmPRWBQMW19jZaUtX7EOiFuHPOktyvtGAfPwSxtHSBp4Iqy0k6CFTXanxpiJ4jMoj/" +
                "rnhYXKrEbUGIdaq3octLVqCRJ5pIdrJPnxvcY2gByPGRhcUNKo07t8LVXsj8VkyS5MLE/F5lCV7Kqbou" +
                "eiJWOTK2sMyRhSyvQEr/GuzTseJpbkfSB3wSbKUu33rIr4fgsDSt78v6v2gS+Lfed2z/fedN/sSvgH/R" +
                "RGH53skpFNxMP33/mR3F4vE5HyfF4xEfg+Lx+HNx1PDpxWd591gM+EoPr9bX2nruWVviFE2MN/sP4e08" +
                "jFxnKudaj1LeVELvj2VfYYifDtz5CUbx4E/4pyJabWefKaVkc7beSPlc9Mwre2koc9e9my4PLdoi1hsw" +
                "+rmuA7uDvNaEQ4usdjwtPqJGZROkN7Zcosnu36Lh1b7y5QZZ9+/XBCvXfkDsH7EH5K7CP44wv3Bd/qtN" +
                "ZtXE/OEFGzd5Kgvrt0ir1/Efh8xvxGzzVp376xFeChXVsX8jWjtyt3dnjAYX+dNbuYG3RS+ddLc0rbel" +
                "HPbG3CE45U6aSlh7emnO3Res3+zZdyaoM8pbRxUQcqQTxpNoFZAKey+kkstkz0odfrapuTv2Zqe1FrEf" +
                "W3TYVwW0iiVpJbCxSI3OTWNLnHVj3QIFGNLLrW7wAWn+Z9zhl5BxMqmLlT0Rp2LVC2R7TCoS7xVva+9/" +
                "2y2Xhmv72LYp71XYU+nS/znVRPqJP/XavK30wD2ZijO7twX/tKrUjf/dPpta9gV/+D+W14fbMEAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
