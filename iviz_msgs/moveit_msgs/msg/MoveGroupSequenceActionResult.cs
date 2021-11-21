/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceActionResult")]
    public sealed class MoveGroupSequenceActionResult : IDeserializable<MoveGroupSequenceActionResult>, IActionResult<MoveGroupSequenceResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public MoveGroupSequenceResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new MoveGroupSequenceResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupSequenceResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MoveGroupSequenceActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new MoveGroupSequenceResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionResult(ref b);
        }
        
        MoveGroupSequenceActionResult IDeserializable<MoveGroupSequenceActionResult>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceActionResult(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e995e5b2d6c322a0395df341caa51d8e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1bbW/bRrb+LiD/gVgD1/atrSR2kra+8AfFVhK1tuRKSrZpEAiUOJK4pkiVpCyri/3v" +
                "+zznzJCULDdZ3I0WF7jebmyRM2fO+9scvTN+YFJvKr9q/igPkzgKh4NZNsmevk38qJf7+SLzMvlVu07u" +
                "zNs0Wcx75veFiUema7JFlHup/HpSO/83/zypXffenuH0QDF6J3g+qe15wCsO/DTwZib3Az/3vXECOsLJ" +
                "1KTHkbkzEXGezU3gydt8NTdZHRv70zDz8N/ExCb1o2jlLTIsyhNvlMxmizgc+bnx8nBm1vZjZxh7vjf3" +
                "0zwcLSI/xfokDcKYy8epPzOEjv8yyxqvdXmGNXFmRos8BEIrQBilxs/CeIKXXm0RxvnpCTfU9vrL5Bgf" +
                "zQTSKA738qmfE1lzPweLiaefneGM/1bi6oAN7hicEmTegTwb4GN26OEQoGDmyWjqHQDzm1U+TWIANN6d" +
                "n4b+MDIEPAIHAHWfm/YPK5CJ9pkX+3HiwCvE8oyvARsXcEnT8RQyi0h9tpiAgVg4T5O7MMDS4UqAjKLQ" +
                "xLkHFUz9dFXjLj2ytveGPMYi7BKJ4LefZckohAACbxnm01qWp4Qu0hiEQe2bKeSjhvKkxr8h3Al+EQXK" +
                "+AdnPvrhptm+bLXfeu7n3HuGf6mZRrZ5Uz/zVianTg4NWTRS2Vse6eEQe3oHm1WYjYt+60PTq8B8vg6T" +
                "QlmkKZgLPRwasumrAN90m83rm37zsgB8sg44NSMD7YZmQurQED6BAWS5549zKHOYk/qUMjL3YgrxpFYi" +
                "+vBnD/+HnggXVOdgmPPIEEKYZw4KED3om3QGA4zoDXJzaFHuvb+4aDYvKyifrqO8BGR/NA3hJQKo4ohc" +
                "GC/oCrYx4rFjGq873ZIvPObFlmOGiZAeLEQzS9y3nhQszBdZQ63IEljC2A+jRWoeQ6/b/Kl5UcHv3Hv5" +
                "EL3U/M2MiN9WdGhTySLfVJejL+M4NCMfblVgFoct4CpzH5jSScBZh/GdH4XBYwRYzSss5dx7tQPNK1Qv" +
                "TnIxwlL5CuEVHL5oXF2Vlnzuff+1CA4NopXZiuHXcBcyeSitdaTjcZjOGNcYQQoxiGsmJiZYI6KqJj/8" +
                "G4j4OjZTKdbMTw9g5HhEJ646vX4V1Ln3owBsxI4ZNoAAkhdAagRilAl+wQJCqWsikEHBo0D4NvwK28sI" +
                "GzkQYjT4swxBPiwHZ627ztpeI4qSpaQkXAhTwB9JGa+AjI1VtDGvkmhxS2CGi8mEbLSLcnOf13YazVqX" +
                "TLKoBJqIWD5lOSVOkiQyg6vLaYgMQ6JyxauIgpiAGVFLEhjJsbawCvtNTBUCoSYjj5DomNkc4ooi7CbM" +
                "TOW3NDi6AO20D1ppUnoVwaiaMFj84WBskgFvDPRW64IYGxMM/dEtFRI7NJFFUpll/sSodLK5GYXjcOTs" +
                "QTDI6hY6Mz5dAKRmC7ELuLoQq+pOflj17aQ3gz6GuYrukfz8Sa2G1J1srzyfMzUlwfLHrvDbhgRVDRZs" +
                "0hTKP0oCYjWOEJjEQFT2sLtlmsAkSGIrb3LtBZYiH+CfA25zIqczUZ8kuSYdiJeMRW3TZAhTty5Rlrg3" +
                "Lm2vdblEvE7xbCArHfg89Rk1E9VY51/nkR+joqCyBYsRdIGWXIYMgdp3O1efPtsNwaAKzh3hz5JFrKhR" +
                "oWkfSXJrixQJR3LmTNgpkGrjKPHzVy8ULOgeiD3sTOuqInlS08IG4Z0E0V/CmmEXU/8uTFL7VtK0Xu/8" +
                "uf38ptG6et9tnv/In5p9eHPVaLcRYgd827w8P3arW+0PjavW5eC602912gOuOz8+sS8rDwd2YQOp0OD1" +
                "x0Gz/aHV7bSvm+3+4OJdo/22eX58ardddNr9bueqOOuFff6+3Xh91Rz0O4PGL+9b3eag12z3Ot0BgDbO" +
                "j1/aVf3WNY7ovO+fH79y2Lvk+fz4e3LCycb7L+8WcWnmo5Qs3KUqcua40290+wP822+ChMFFB5lGD0SB" +
                "A8+2LPnQ6lzhd29w0+i/w+p2r99ttNr9HtY/d8x822lcbQI7qb77Myin1YWVV24TZfOitiGdt93O+5tB" +
                "u3ENLj9/uflyAxKWvNpY0u287lgS8fb7jbfIvX52wH/YeNd5zfTXvYU+wU2vEDhm62x+08WCARBo9950" +
                "utcDp4THJ07RCmZBXZoXP1MXoQ8fsI5KgYWOgxVc+a+8c0yzCtNqv+kU78CsvaoarOHV7gxaPw96nav3" +
                "1GSo6PPdmHLp/DT+I3S7WIicknEWzZMYro1Iw/WgvrHJqHpWcbZHXlg3dfWKSRZyYUZfxurtbwkIzCT9" +
                "QyfgNqshpcrgv+X0n/hSXa+so9/Nxa3LG+ttZ4jh8CRwfwhtIbyhd9l54/lIBspAjC6QWQN9zbVYVzlC" +
                "tg+CZDzYOKyBMmU0BZRREkVhRjqTIX00eh6+e+fKMFLh2RaI8OCw5vZfuO0d2Q2f73YjXNlXAwuZ576J" +
                "fOQKccAWlIY+A6hMIZmpjtCwYiqlXQH2ilLkSkyFWGd6QTgea77CkA6QBYaJAJH9lf7XLMnYCghnc1So" +
                "PoKN9M9cg0bKC0fqMAkY6g4cPljIhJnRKDLo42xZTLc/BlaqW2dnI+RqZ2eV0GrTt8UcvRxJkXJFPq+o" +
                "3GFtmCTM2Ack7tsZwHYVLAyAjabCCkQDp0mEhpj2FROk69koDSETm1NotoEM32gET1L4dzGfFFIHi9QG" +
                "6mgWljm2bjLgob72DlJzl0QLPmdSEWbiJg6JTWDGcBvMp9kLRMuuamkun3FQ/IAAZodH5VL0IpH95quH" +
                "S59msvgpGoi00HKLGUNcedGP9Odz1Fnox8RVAO0Zd7cPkfZjZ7OkhQWAZMRUuAC5nBop6gG6hBjdOssI" +
                "7UMXdpxtLTXcecK1zNUF0DD0EeseKq8Ha7R5m8B+nBjFf6glua1FVihtRpypZGz6wCxE8m+V2/kfP039" +
                "VXYkJ0jaR9LZKV7nsCBDsa+15YDFzL81usmuB+3UsGROifpR3fsryyRTn9S9VbJInRcVKuIEAK18Nhqi" +
                "hDU74hYpiGGspLoUp+DtsdxaWW0k95QalW2FdlsCKuccn7LwD7h7kAxGKpyK1XAV6sqVq7oKHSjLr5I5" +
                "1IICaTAavcpRzqgtEqyjglm/q7C1FZyq6I9Nf5lXW0OoPHIKX3mkHNiNT3kYe+hbuoY9fRiDRlJIXt0A" +
                "FcjKV9hRaFlgJqiNRffG+CNIIFrAGcOpJEvntMGVxShnXxDLygNVl7UWB/cXRWvIdybKyw2bIQnzWZpB" +
                "BVkss+KJMwZ93TMxeekCANaPEnt6kc54oylyhbr3RooglOMR1MSXBBgBw/oLxC0c+757+Ubc2inD+ME9" +
                "9BX/+UveGtAJoQeJOlVeUompaJXbkCp2ykj8SkNA0b006rX3gKorHDTYNAoUaSmwDwCC13D4f0+2W0+2" +
                "xB0Ftn+tJ3PL/y95ssccmaah3J7VJgapW56u1IP0nQpjVaHODxYtEZq4gL833v1V2ISXyq9v5/cewbtI" +
                "plLn9WxsLzzL0ORLA9XIl8mDm1URIX0eMgN/BGdW+yAdk1PdH6lh/7LAhjSmD0gT9aq7otOis41KH9GH" +
                "LzdI8Ap3LHo1M0zBoVnFTknpqTkgQ3qNqWTMqK1yL0jAEqTg4stgcAw2ks7QKUMpq2zhY2w5oMkdabtV" +
                "VjFgyLW2XITDY6fhBL3SDWcq7t9Sd+Tl4xMoNgxLcNbDIEX2TS3DD+teayxmuiRBYuIuT2aGbPGSKyC0" +
                "tI7YGLYg1jl6I6bkLBZlZw5rgeBdm+u++KuI6t4fO5J2qWhbBY5Yjl6ei+trYuen30s1JZ+/RFPx13Jn" +
                "Rkv/UVDmgm1Wlg3rJA3T5JY9/FgULWNBzKKQ4dePJzIpwQACx+eM1i4pP9t1uyJQneE22UEgKqSSviNY" +
                "F/CXSEQaWUt8HZUCrPyodeBu2jiPdCDcnc7GY7XpYaWIl+iFkZrw3pWrNGAJoux3uHsN/i3xCt0LYWXR" +
                "I7LXkwskBtnUx+iNcAqpOf5yt2IbWNT21EdUOyxcJgfuwavYiyORGIkVkHFiezN1OC+2SUyO1I8uyvZV" +
                "9rbBc10WLUILMgpG6AFBbZNRCtR16W2BL32s8iKgOM/1jNiZAkbJ0uKQLBCA93gwCnmf103HBWKKBpP0" +
                "CJNCwUorTqQJiqndUKYMAmygnTRGlHFxHaZYSXGMrJsXEpvzS5CGFAoqD0YiJVKi0IwwClIA2gYQeDcD" +
                "tqN80qg2ilBuMZTg1klswUI5PHJdCTkjxi0yXHm6ktNSXEDLGBQHg5ha6cEUHy8YbfuuuBixZivVTHmF" +
                "wvtdYDewRzihLA1mwIp0dUMYiJ5j7zZOluV9ia7fjVluMceGTQMlEuqlUdHaczWdmM1m1ljcblpKLQ8P" +
                "RIEEFgSoNzOH1XtI3edEjak41QsG6aGPYJxYBhUGpL8HbJNMYqmslRiloU8IBOMglz1T63TZxtmSzTuz" +
                "1W2hqCNXQlk2u5iSo1dau44BvYTjI+6YEbtkMyB4B2aJ01E8ZdWNe8UGQblsM+fI1t5TtfRO7tpk03Wo" +
                "fIK1M32xFQ7flSBe0z5kjgY1MLupuPWxGUJWUndUDHfIMlejakIB4hdqbJBYEIgs4GB5xGEVtxtutTeM" +
                "jxLJdyV2jYC1fkUxlOvFhZV0jyW3qyyCjuI+b5EhCzT3yBmIPlJNDanic+q14QpJfOPy8vwZj+mKX107" +
                "aZwmbCug6IrvQtzwzpj7skUIJ7ECl1CbY8ZQTUGa/znsOdvQiTA41JO6zevOhybuFEnTfE5XxRTWabPt" +
                "eVjfKkjb8vBLtLqUWzc5OiGFksgbDvadn1g/XJ65/Tg55QiOcWk134pacv8DmdixcnNlrBsuiMw41xKV" +
                "LRI4tCyJyCuw1nmM0qGiOQ1OBoqi8OaUCHbmuJN1GT6HDE3KZNQtTNzrb+cXv+xW4B7/5R9P7/04Xvuv" +
                "b7Y/5M/Fn192id+UHvBYwp71ZfBk7FWxhEVuIJ06po6QoknZHpno/UnRPdBGPlSF1yVrqcWtKbrz1RPO" +
                "dJxI9peddKktRWPgtDD3NHT+HlAcwGBYRcWGWWmf/dTrtJ9yrMD21D42rq/sdBK66YUWw9MWNlApOt2M" +
                "YNHqkL6hhnYXU+peU3IH9uAfSF1MSXo6nHCIwltz5v3l7/vk8P7Z/gXzm8vX+0fefpokOZ5M83x+9vQp" +
                "ShE/Arfz/X/8RUnkKBrR04ZebJ2jSs/mOBROhQvMH8N8H5tCZP0whFtj7Mw1xk/uw2EYodyxEWqbwvJW" +
                "S5noiujL16obAoRU0fTtydoKo3LpIK5rjMpENxullli51BEwZ17BAHlGFuDZJgvOXv74wwtdweirPQOs" +
                "e4jxvj2p98sVLrGQJfAmq5DT2sG936N3boXClqO8/eUkO32lT3h1eOa9fHF6Ih855sYFSKKTpV2ByL9E" +
                "M2fjMZMUEuIOcLeg+naGCZqI76VPkCfzfafQUO1v17F/LGVgmnapljpM0BrO5lS2I2+0Qo4tqRs0zni2" +
                "5+jKHWiGu6eDZrleI/KcoUsEAIxun4FdjFEz6GdH+B+aAjrw+LrzK4KZnWu+edfEaMKJ/XjxEfMPl80u" +
                "HLp90Gk3z18UU0rWRUmsIU52leZqzivgmgv1hb2fL5eW9yTlCreHrSqiX91QWXam7V/WLXKtrEzQgE12" +
                "3TtntV/u2dcQJ3fgtjgE4YKqlhG/HnkftaP/WxVnMlkqJxNPkDJajDbdEOungj4wvV7ydvAr8pLy08eC" +
                "1/z0G2N5BSXlv8VKOmAUOz0nfttbTo5HW78i3ljpxs1qiNnOZGzrHdUgh4fCHXQbl633PeZJlTOdkAUm" +
                "BaxfZFGuqOpIK0IaiS5JlPsYe9Rvno+0o+6VHcQ1uIN3zdbbd33vgLDth8OSJr3Gr3C8pGm6Vmc5W/AO" +
                "aAuHeh5dnTtHqbPn6IfKOY+dws6i452Kz5Yo289E1NaugHvFmasi29+0Sd79hKnUwjrMiWu2UoeEp9zP" +
                "qpP6vpgf2Zuu7yxTnZFuMLNQqQ3imZWWlvpgcckYLNxRJ4zFgFah6YPbSGalm60wERiTBH2vAwdkeKXj" +
                "ida2Nm+Li9hKY76ybnc0Apmi2bfWoarOTKA14i5CS4q/0JrdRSxioVlEoAq2rC7pKVBsqxWmIRqRyCb+" +
                "p+JqMYGJ0UKO0+oAejmOBTJ564nEJ/v0ucZD+haAXDFZWDyg0spzO1wphixwwYRJ5iqmDwpNsFMu33XT" +
                "zrjlCNnGNUcZkr4CL/0a3qdTRdXcD6Q5uCOEpVbfPg6g1+WwOi36y6ZA0Tnw773v2Bb8zhv9gX8CfpWM" +
                "IvO9s3Nouhl/evaZncbi43N+HBUfT/gxKD6efi7uIj69+CzPvh0PvtDde7LR79p6Sbqxx2mcGHL2H0O9" +
                "cDgyAVUutg6mHG5CX5DlYGGUn47cHQve4oM/4jd1tBDPPlNWyfpqHWL5XHTUK2dpfNP5c/0ihiSnRcfE" +
                "egaGRNeQYOOQk1C41cg2rrPFX2yQWQfttS1zN9nDwRsOBJYP18h6OJITLFxnAgnBgO0hO36/uwHYqhp+" +
                "qQut+liuerBhbfqnsnFz/LQCYmea+whuT9bH8dz3dzhQKgpkv6u7cUtvJ27k2xvINfnNTRnd26KdTsZb" +
                "utrbUhE7ancMZrnbqBLWgU7buUHDzXmgQ2eIuqKcVaqAkGufMB5Fi4BU2GmSSo6TPS01+em6/u7ZkVBr" +
                "M2JFth6xjwpoFXvSImFtk5qeW8aeOUvKTTsUYMg8t/rDR8T5n/KLf4ZO4R83JcumidOy6uTZATONxHvF" +
                "Ye/DrxuPqbm+kG2tchTDXmGXjtBpJzJTfOVufczpkQGbild7cAS/31aqx//unHVF+zPH+E8fZ0L7x0EA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
