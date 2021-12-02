/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlaceActionGoal : IDeserializable<PlaceActionGoal>, IActionGoal<PlaceGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public PlaceGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public PlaceActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new PlaceGoal();
        }
        
        /// Explicit constructor.
        public PlaceActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, PlaceGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal PlaceActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new PlaceGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlaceActionGoal(ref b);
        
        PlaceActionGoal IDeserializable<PlaceActionGoal>.RosDeserialize(ref Buffer b) => new PlaceActionGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "facadaee390f685ed5e693ac12f5aa3d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1ca3Mb15H9jl8xZVUtyZiiZMlJZZkoVbJI20pZj4iKH3GpUAPMBTAhMAPPDEjBW/vf" +
                "95zuvo8ZgJK9WTK7lU1SETFzH919+91952uXF67JFvLPKJ92ZV0ty8l41c7bB1/V+fL5WTbHP+OyGL1e" +
                "5lPHZ/Jk9OR/+D+jFxdfnWZtV+jmXytI97KLLq+KvCmylevyIu/ybFYD4nK+cM39pbtyS0zKV2tXZPK2" +
                "265de4KJbxdlm+F/c1e5Jl8ut9mmxaCuzqb1arWpymneuawrV643HzPLKsuzdd505XSzzBuMr5uirDh8" +
                "1uQrx9Xxv9b9tHHV1GXPz04xpmrddNOVAGiLFaaNy9uymuNlNtqUVff4ESeM7r29ru/jp5uD7mHzrFvk" +
                "HYF179eNawln3p5ij98ocidYG8Rx2KVos0N5NsbP9ijDJgDBrevpIjsE5K+33aKusKDLrvKmzCdLx4Wn" +
                "oABWPeCkg6NkZYJ9mlV5VfvldcW4xy9ZtgrrEqf7C5zZkti3mzkIiIHrpr4qCwydbGWR6bJ0VZeB2Zq8" +
                "2Y44S7cc3fuSNMYgzJITwb9529bTEgdQZNdltxi1XcPV5TTIm7fEjXsFQljLgM3aRb1ZFvhRNwRZ+SnD" +
                "WV4vShyIIEFxya7zNmvIMC2QIAM9l/MWlgRJ8so2wyE3V2CN64WrsrLLgKhrybTgC7dadxkIjtlcs1Wu" +
                "uXbYOiydTRzkAyBkU9d0OU6OEKX0NfjLwp8JyAvwcCx1pHM2c66Y5NNLQFZgBphys+wgg22bz50cQtau" +
                "3bSclVNF0CBoT2x1CogOAFCrTdsBsgxSh1En/vx4crd0dKv6ypWdnlvQXNjNbz1v6s16XIF/4rO86/Lp" +
                "whXjevJ3N+38W5n+TQ11gfP58V225u/x0h60XHRS10t77tws2abdrNd1043bTTPjW1vSZuAs6+vxvCnX" +
                "a9eM/dhpvVyWLZbGsGfYoGtwjB1EKO8WeBkeJNtg6wpaTkQhPAWosgEQ6urNdGFocd5sWefd7z4P72U+" +
                "poyFWQRl+f1qLShm4X2tD+7s1DzZwU1PoaTAxNT+KlZN3q5TTjK+hm5tKjDkum67DSShnolgUiXZZGdz" +
                "73FMqRjyrZvhPQidYxYFcwRCk2J1s1WQ/lxj9bfhoWxB2uFgbTcPhV/Y7+6q4j6Wl3l9KCA4S6B45UT+" +
                "8GsGeaYCFuVGO3PojUO+hJi2iVi5ZeugKBp3nFU1hlA/Y5xJ6dFo7mpYTQ/9a2j4CzN1AegAcb6GOgD7" +
                "4wyE4F8pVwLbqiWAwAUqyLD1g/3kBrs4APmBuZFUNphzofaUo3Be4PmO5Kon0FPTpQvqDUcH6rRulVew" +
                "yMAS9Fvpqvmk3nSyTqGjp1gRekb43RUP1puW/5CtCiUPLE+9aVphC6E/uOej8nI7zP4R7hr1nbMA4985" +
                "TBRJOxpMec2f1FD8958FtwCBMzknM8WxClRgXrg6sCFAL4jgj8cZfCeYnw5v8SOfTt0Snpu8fPcOK9b9" +
                "0Squ74I4JXtBlMAF7j09MjW3T5fLRHCu8iWMlQi6cUVLYYObB4hgqflE6Cw6B25klQ2wPAHqQY8KyQ2w" +
                "5FlEJ3nYQyt5rtiMio2+ErM9njX1agxxwItbOswbVYRoXP5WYx/10tAbVuHznuyAa2UBVTN3Bb8A3jj6" +
                "0fAwlZgQdgLBc5zBjwInQBEdMxDg48Leq8YmOjW8Lpt7kgEJcoIfMPrLBrg3lawbx90VgipcEtzAF6CH" +
                "p+yaWhxELwJyD91g9N+Hv7bhr5/vBvxIOo9DOCia4JSefeD566dId1qAk9FHMPJ/Xd+Ft7Jr8oBh4WZl" +
                "Jf51l5jCaP5lyrEGAUBwXU4vN2tRczSUWZe3ly2W4QT3HsKFmIt/L8tZJ/4qCCYmCmfOQ+8kKsMAb545" +
                "SDSjvJjZwrR4DNTwoigbTE/8lATOgffwrei9x96BCDPDUq7Fo6KHaQGTnkNn6FEgArZB4/DCJq+AvX+m" +
                "htxHDPR3EaNw5RkjaiJiIQ4mMhISJ4qh8xUiJrjwjJKgvvyWWDrZ7nYY4UOU2sfomQ35h1SrX+NK/r1T" +
                "1PbipHD0xRbK00e6iKO38Ezhw9E6h5mYGFhJQsdGDveYsW9RQ3bg2GKNVX5JFxBpBnGT12ss1pcqPMaU" +
                "Q3cyPznW6FlGiZwQCkkDwX1syjlCX5kZ/UhxvQ2546ybPcLJwGEQmHUzZbamVuV0dJI9n2XbeoPAGTjg" +
                "j8ayT+J4eLhE7Lq6FhH3/Lqrz0NYDV3e4XA/qtluX50lcac/bA9ksDp5cNnV7Q9Rogitzfa+V/IoyHYL" +
                "Wrbij1E15ZpHUAomYg9fjdGdBXoaQIszFmEMXnEvOn5tFrE3zpvJ/tBX0e70Rif2qD/hWwTok3JZdtve" +
                "+KvwuD/89g9sQBFQPfzY4x6ob6ukpg6SjKdPGCGmQs7H6B2jDa+r5bXNpY2B3oX1arMfwxb38ZYuLlTa" +
                "eAIf+Po4bv9p8g7R25V7F/g6OFj+wWDknueyOgF7ijgRKWExi0i0xChb3SSjSnZYOOgTaJUQdJfwABuz" +
                "O3VWS0iSsm/2bAkPESvV2c+uqUWDtRmi0zZM7Y6iqyFA3EVyZIe3bxRTNc5wRHruoR1HRBVKp29ffGhf" +
                "T5BaQDL3coeaYriYnPTMwlGBVzgZfknrQHZjQllFLNtRjNryZo4h6rDWyTgE/ozPglLBhP0uiS2BbAZZ" +
                "Vbf0AFzVy41PJu+DPBt9QXbG+t/qyDgIeYq5+Tf/u7jrDtirTxRQ4Ew8WVp5JRMO9PGZkoYHDKZqa6hp" +
                "b91gZ9dNuQKPXfk8jlhweLddciq1ecjZIWLunNlhmNZRu8jXTuG44KKv/UpU32HVJNNGDztm3F26NTlI" +
                "MrVwxHZDxXRBSYbJqs+hDItC5UMS7361YwrXAqtalinmE+g/FPJP0JDkFcPzMJ/juJHRaPeg+AJLAg5d" +
                "+QNI2dYfRYjjPC63wyg3nk/kk0n9/hjk0dTkdAt5hkZhvI13PhnDVRQfZQIhn6T6gG7ZRCpiMdIchlj8" +
                "gbwRA/TwGP+F0mJt7ffZF6++f/KZ/X3x+uvzN+dPHtnPZz988/zl2fmbJ4/9g1cvz5987knNeqGPfgQm" +
                "G8XnIz+ogA8Oh4SJ4t7QmL6JI/wcyjLBTyckw04zxwSZaAP6kD5W5FiS673PTB3EOQdAvsm33OFLU59A" +
                "XEA9ll/fH2c/gM9Anr+lMJPIolddNUfAYRBN6wY++Bo1P0aEKNdILchegugnkbbj7588TH79EGjNX38D" +
                "qVOQlP4GlbjQPHbJtVT0+K3spXDCZ5+bkoCtyYtyQxDM1VAO8nDouuM3T8+e//UC8KR7+kOWNXnAWgdW" +
                "qijr0KpIzVKdOnLSshbEOeZvWf6+hEKOIUhv3fHX58+/+vptdsi17cdRxInJyVlK8YjTQpR2oLnJQnZI" +
                "WTjS/eis+X0UO9tHfyT73LQLQxNPOz2+XFPc+/eE16CxpX/FUkhfaSYySXe8bKQUrsW9rlxHHhKaSikF" +
                "h0R+36yPlbLZp0ZUL6QDYgaWGiAP5kokdWdwJAwH3rqKo372ii3xoKhmyYA+BILNy6s5LPcfEgm2NLN4" +
                "G2JKQrkJso0qL8oZSHW/G3GPt7YAdElYy7vccDk2CI38jF2DKtDsiSwlCa2T7ohUHo09JPNoHbQRKO2M" +
                "+PGxwunej0G424Q2dXL2Bn2/xo3u5ys/7kr7FFk6y+Qo8bKjVyFOUcx6Dg74pjT0P+i2h3ocRfe+HFjm" +
                "moZ61oddSQkyVpIncABRbBm/H3PiOAzeHbH96IifhyP+FX3wfSkGO9wEXzWms40kTlZ8xBA+5iE0tYUE" +
                "6JR1ZB9uHe30OIXGJuF+Gc/0WNuzJnlYeKua/3rBqgrtAcvTyCgw5mOGLTA2c3Z1w4VfGHDiGEf4uBuX" +
                "QhMMhm6EIaNE+BSgVIWbDVLdEj/E6QSDq7/EoZ8mhRFPH1ng5au3WBz4gAeQDy5XmxXT5CtwGf/06eEW" +
                "pq67dkgdRshDcdmTjqX2RpeF7+JXTSJZtZjmvEFG0B4EWlyV7jpxbJQqWmEbBPISXRxyUyTxJ0ghbq3L" +
                "6Eg6aFgrIOO3qFBn600jrv5JEPueFyDHKMbC+pSwgOcRZtZIRoh3TKxoIK2LpLFIuuAffIFUT4gsa37W" +
                "cCT2wgGh8QJ6FHOunbQu6QlpBOWpJMluzUxElowq7DgtgXygqyEkArSt4UtUTxNye87s6mv0E7a9Mw0g" +
                "K9PnQxZLOEt8SKmwgHukJW0jjX5oT9iqD30ibqmBa8loHfO5DjhmEKRdHZUoMzHmClGysSYgJM+Wrbeg" +
                "USmlbAFEXAt/rNYYgoGPha/hvZ6MtOeQAI9l0955Yl8NLfuSqLwbWUS38weE+N42jOn7gfHCAjFkNbKL" +
                "atDOqtj4hk4zhMZFOo48sazrSysIMPXe9Q6JOgs0jvUCTwxZTOyW6kM4S5hNtQGdgROYbLoQ0sOy7coc" +
                "HVhuD6TqpUSg0jvaslftZnZTuAO7nbMAhS7JzXwR+YmuxI7EKb8NWMzLBBwh8NFK+/wmbppvolTt8Rxk" +
                "F6sj0SyLAkp1jNIbQMrWg3IixopmspXQ7rdcoVMHBw7RyYVbhe56qFPagX2gG/nQ5USPFEG8VAFoV7nD" +
                "Q0uEaPQg+6lfQd3fizPE2ZVXpolNX0LlgiAes2R3wd4r6FjNtFrUbK8C8EytIiQp5LSSZfVItQzmsUWY" +
                "VbYhj65hN5e8ORYog1BK6QjIPDSLe10bPBpZgfSs2KLXD1zo2iO/IoO8pevYPuyNne28f/nX5YNHukO6" +
                "OuBa4xhUhWLtRFl4UivG7HM2ZM0MJgezowd0nWhFclvTFHiNPbXzDo/ZsvbwWODTOtlD7oQ0oWf79PwT" +
                "0wzt4A0RQBpz3FhjiSDnfpZYwRYBK5Bkca4ezBCJXzKg7aPoQ5NdO++Nww4PeeaiR2o9ZjV6LdEXZoH3" +
                "0LikvoAZNmW7hOmIC3OCnn6N2MhANXM6BuTJSmRZ1cF42CeVTE9plYKbKlvUSMkb0vg+LP/LufisfHB+" +
                "vs+eZI+QVcI/nx0jS/Ik84H4xfnLi1dvkP0ZPIjJIXvwfUjFmcKUc+o1EPzLOfeDBtuYZ0Qjh9xQ0PJn" +
                "OI9QBWmRzAKn+CjtKLTqXsiL0Kgr40DkGbqRYXNnSsvZMp+bMAqninW0DIO2rKA5c9NIP6y1z0kNm8sG" +
                "XpWcYasyFvxza7ywSWSpLHRFV2NmAX8FHNKP5TH+N8yzVdno2/omTU4Ud8ESwtkhCWw81iLCobMNF711" +
                "bmWiQEDBQTTQaRcp17zKoUuIqm8p1a7dq7KpK+TbOkWG+411vxSdINSa4Ln6ADIRFVXANyCjBXGvtqrN" +
                "agJmoIOsZG7/4HdPNMrSzXAOLTLjPlmRs2c/vLekq1Ad08VnKLaI+8spal1guVk5ZwuzOo4JqmO/q+HM" +
                "UxP9NEtHiTmxg0xpIro6p4eVauIpvQyPvGLL/vbd8n/kQnEefdpRs242CbvohSAec5Xsj4oNM7nkCUnF" +
                "NzR7eVZB0wlLV84VtG5YNlDv5KFaj/2YwWiEhqeUvqTJVZnvI6gnQk9lt/nMjYOwoFuglZql4SfAwfeD" +
                "58lcobRASKiB9lAiEiZKn0zi3vkaio8vZCHCslqzE1bb3IOIQpTZWxrsu4ilq0hsSozkdWebStg4F39L" +
                "xQDeEtYNhY4dLtX3wvCedTJjK33V4ygxVysQWhOpvKnCMCInZlUw1fvWZF3Zbi1FJW47FDAR27tU5aKA" +
                "gY7cUkhlXlRxv3+FZIUXp4ETzqBzozf8G5EFTLM8Hutjo49fNI27gaGyutoEnh9Mtu2ja8ggn9pLIiDx" +
                "S2Q8OGq9pEcgeZdZdrgvlIg1eKndD0Ii6XmkNrW4CEnnWfkeXX56PSu0WvGwBW0v9uGiC1gIUL8fPdUX" +
                "z/zzF/I4dOWH8WMbT2HGehJ5roleNW9H3+DXa/0BSCS3ae9649tpzgw7R1/wTz9WnotPYjGr9VgiaI/w" +
                "hkfSdCgeMJyWVS7Nmylaa4mLpKrJGKleip8eb7MpgVcmf1LdS1rk5dXolWwGotQNW5L0bpIu5QsEvS37" +
                "LsF3dQOX/Zr/LzkRsbzqBPJEkZwQp6nPSD6HBYcecZEoDiZFxD0ZMkgLXjDPSzldVATSt+p83L4ARsm5" +
                "MXm/c2ckESSRM+jRE8R2aaeUZNEYQEgLlN4QIoe0I3NjY9+Viq22SgWxlTehVrNVJljBYpQMvM9efSnx" +
                "Wsz2s+7YW/oFx2JcsoVMHxf1bDzYLDDrDo8ilejf+cMS/rdWG6HB0cjPD6KnTMf7Cv5SXJS+cC/mTtgp" +
                "3uRlZoKXa73jrjeB/VVT8bg8qpO6oAAdengwUNJBcK2XDrnFPYO1hpsba52eojzuTk8TtWxtx5s1TLG4" +
                "op0C379zdRfcv58BE0rlQQSE/Rb1EvlO36CKpMa0KS3bIhykiFsjD4KvnzYqOw2OHPRRAWAvr1mjMEla" +
                "GLSZ8LBxzPLwOdrMmxIZLCRyj9Icz2TLDH32m56YeavmV8lZH8tWR8dxqN2j2e4OfcBqfrZ6AHef4hmn" +
                "6G2aGACjh5cyZiGMLfBSqgAvj+BYMn0XcaHDJjdVyW2IOTuVUJQoqA/EnCohtL7X19g7V4D9fkK11t/X" +
                "BXuJ68lemOEYhgYgDITHH6P2xYgY+ak+Oyo3l7inojFUgC2KG6LK8qh8xM7AenEHyQYSdWbe+xQWYKSF" +
                "m7hKd64Ks5SeZZKNlzIHdrGo9CT7jo4ye7G1N9pUqGBR1Qzo9HwG97rF4B1LX7Ukg511A4XhYh/p+22N" +
                "G0k9xWZ4+9Enq3xHkdKpLX9mUwq6i5ytk0iN2HH2q9ht6MAD8Vp0JI54ZB5o7aWZMqzSE0RBZlgDDhf4" +
                "hH92b47tXhzb7l4PuwOFsmt2gNWbnftUqgPIPXa4QovAYoWbN07biXgdoKhxrpJ7pi/n1bVmOe2SbtxP" +
                "GXlYJwC7+8ZlKV622xZhQNLFJKxplwJo7nUO0nBR/sUBrG33SyglUVKsUTE/16vaILSEGwVTYcoCFgvb" +
                "/vXN2Zei0x7TgB+iy22L/+XXPmWH9D8yI/LS0vnpFx1S6JSQ6shqK5SA23+PVXWEXw0CLY0SUEa8nA+E" +
                "ezD8vxq7WzV2zXs7i1+sxvzw/0tq7CYtll5DviEelFamEPwNBl3DLnEA/x28+07IhJdKr7u52hSg9pQc" +
                "lISiWgnFAxR2hje22sH9p1G4ppXerEtad/yNojtCUqhtCHrN1EYHq3/nctLUl1rX4d0qJBihMKEQqatQ" +
                "a5AaPaUNXOKRtCHxt427G+yUb/acn/ZoDC7/tg7Ai8wSQckV/yIUZbH4U93lu4hzb4jRTK8NnoYyYYhy" +
                "RMhzTc3EC0CiaxgQ7msG271lbJ2IvnmbTSrSqu7rFDuw2T2hJATlMNnwHpKdliKX45L0PZesUMmR6Sfo" +
                "OPClQyZufOB5b996obQljnpAIxBCNyhGQ0Lpoj7RZkGQBPpmkdNQ0QfVDN2T1L18L4J1xW1IC98PgCkY" +
                "kutvoEW3aXY7ToiaVT8+oakGTQn7m1kKlQQQckNYAppez4z/6EmontkFYblyyXpuRIVlWr0taZX78H0S" +
                "Fu4lz6rZ73DN+Ci0+cgeKILSRqFbgbuhJKdfvLKUlG3M4+PHkSy/8bEvqhSO0A2/pqJ1OW+lB4eB1M0s" +
                "u6zq6+qfUMfblcWnZirtWjdJExIf3u/V6xl7e0TB8b7XQwl4KNzjr4i/wN7Pka/Z+eyNP2feZhCmYLTv" +
                "+/aEOkF6LIPIOHKu3azWDyvP33IFvfzgmw19Rslf7qHk73o8XmZ1Wim8aM3ke9tTksSXJ8DFjXeIfuml" +
                "oF96x4etyv1V+/dwPnq15l7m70hJnMBck2OrrmmzgJ02H/kkemjyCXcQtC+Mn/+K2Xlu0bsjxISuICI7" +
                "3YAk30XonhbW4RYVwG4dWQpfySDw6FVZb1r4ig79A4DP15ekqCItGpMtfJ2nZ2e8gMG4UPr/0kVC001S" +
                "PcXfHer9WPcQGfNmyxunc58a7SDM7YAnyuJId3pz/uLVt+fs9AdOa7a2SJQXvnygcaEpVgHaXOiP4Roq" +
                "1zLJ44lTiEi+fn3+8oyXW0QJxz33bye7HGttUTjf3xIj/tKn48/Nu/r+HrSUHsWNZxjJLg80PvO2fC19" +
                "VH1tai1JCqLQ5jEBfIXGn3CXHWuyPwnOqh9Y+9e3pRQ/rlRG9371f7JXX/z5/NlbfkPx10+2/5A4zz5c" +
                "B/BXwPhpSRo8U2RQY9LABjcfXoGkMugx4gi1fX6uqeUQXtldRhRG7w2ciksXcpfpDqfyROfHPKN8QkHY" +
                "BRoLDUkTr+yxSugTmaSgmIGV/MKfL169fMByryUdfnj64ptMF0CuMbAw1GwQgOTbClTUnirD62HeoJxk" +
                "5+I1MEO5c+giR6GRc1leutPsk/84IIUPTg+e0bM5++LgODto6rrDk0XXrU8fPOCNxiWo3R385yeKopbN" +
                "4QlKxqPydUs5PfNu5NNCkQp6m+wAk0rtBrp0zj6sOVtCVLVj0Df/7eFXJvyViP5bEWdfKG+ET4tR7m1n" +
                "zRWQuTagE1WcZo7ks53MJBmykvKWZU6zQAB5RhLg2ZAEp7/9999/riNoerVJCuN2IT6wnS7+8g1S/HAR" +
                "mOcP59Tb+OKn5dd+hK4tW2UH1/P28e/0Casqp9lvP3/8SH5idMMBcJ/raxsBs49KYjF4TA+FiPgNfIFI" +
                "36IGvVnyvTRxdPX6wDM0WPv2LyWJ7dybwdQMH1S22uBoo4Mhz99nn9JF/zSb/oz/K6T7TTpITp/gcNzs" +
                "x4f8Ltkk/PyMP6fh5yP+LMLPx+/iF8M+fyfP7ji3MfgmTcwApGlTMeA7n6JRL+0kfHL1nncpdkZOFyV4" +
                "IA4cFiViJc9//ZTLYNQf82zRuNmTT0wkrsvL8qSp25O6mT/oZp/8qZv98UH+J7Dh9BILSUrvAgE9A/ei" +
                "niJP7E+XCkI6fxKFv5PLMi7sg5tp9BIa3v09Rw7Sp6NAzUizOwn/9zZHmDrz1xBBAfgX4aN12nEl40J4" +
                "KUMsceeDDuj4q7JgZM+3ZZx8Y6sGMoSQEl7vbrcrddeP7bOwvS/ppbsNMTjnuwCRXpbccwF/2OAg+XCc" +
                "uVvOsCVhbO3i+KAzS4mh/VmhzBUpJJdmzCncxdO3KMI2gyCxRw2BDeisFD6s0BNbSb+urzTSoKqND5+b" +
                "vAHy2LsXrgXt7p6kCnyHPPYJHxUlKgBCytsCRuJB+09piI8J7MQBRjF7kWGCpFkeSXDWg1aGayXcqGh3" +
                "e+wDwAKS9ZJpQqnKnr48S/3LwGi2wDhlAVbHd175k797GRIOZB6/3yYQzwGhyRR1IjkLbYwrPA7+5x2A" +
                "nbQ0je4lX8G1RNq+a5zW8hS/lpVm3sIXd3xf1N2gIH1WvxQB9mF9FAFr1rp98JMerH7CBSaCqGitH8pI" +
                "BZA5Pg7dSdN4gffiLmPsk/ey+puvvnhqL277E/thv/CtvCb8NQ9/TcJf+Z13U0rvmvbN9ZuahklciOPe" +
                "9qS3SWeeaM70i3Ux4RLXp/M8shl28vrjOyg7+Zipvby1OPoDe0t28fGZtBWyMxZOlyZfYVmksRvjUeDY" +
                "n1hML2diExZC9qXlrKPFPj69m2myyyyhb1frAbYgv4m7D4F/BtH+28SSljZpk+JdE9h8m3ooNVVWkB7U" +
                "0+lmDSN7RIMhLa3yBNdQEOcrKQ5PJt2DEzoD5dJ3helCkpBYIpJK3UtNUDCnrdP7muMNryVqyzZbnlfY" +
                "125X85ogmrj9tAr9vuGjwnqbUaZxEUMDUV0JzfpzCIN0qn6iUNsL5OMiJ9KLLwEvnYXrBpfVbGzLXu7f" +
                "04xTWkb/BahK049FZAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
