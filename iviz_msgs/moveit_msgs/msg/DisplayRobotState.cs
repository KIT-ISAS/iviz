/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class DisplayRobotState : IDeserializable<DisplayRobotState>, IMessage
    {
        // The robot state to display
        [DataMember (Name = "state")] public RobotState State;
        // Optionally, various links can be highlighted
        [DataMember (Name = "highlight_links")] public ObjectColor[] HighlightLinks;
    
        /// Constructor for empty message.
        public DisplayRobotState()
        {
            State = new RobotState();
            HighlightLinks = System.Array.Empty<ObjectColor>();
        }
        
        /// Explicit constructor.
        public DisplayRobotState(RobotState State, ObjectColor[] HighlightLinks)
        {
            this.State = State;
            this.HighlightLinks = HighlightLinks;
        }
        
        /// Constructor with buffer.
        internal DisplayRobotState(ref Buffer b)
        {
            State = new RobotState(ref b);
            HighlightLinks = b.DeserializeArray<ObjectColor>();
            for (int i = 0; i < HighlightLinks.Length; i++)
            {
                HighlightLinks[i] = new ObjectColor(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new DisplayRobotState(ref b);
        
        DisplayRobotState IDeserializable<DisplayRobotState>.RosDeserialize(ref Buffer b) => new DisplayRobotState(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            State.RosSerialize(ref b);
            b.SerializeArray(HighlightLinks);
        }
        
        public void RosValidate()
        {
            if (State is null) throw new System.NullReferenceException(nameof(State));
            State.RosValidate();
            if (HighlightLinks is null) throw new System.NullReferenceException(nameof(HighlightLinks));
            for (int i = 0; i < HighlightLinks.Length; i++)
            {
                if (HighlightLinks[i] is null) throw new System.NullReferenceException($"{nameof(HighlightLinks)}[{i}]");
                HighlightLinks[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + State.RosMessageLength + BuiltIns.GetArraySize(HighlightLinks);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/DisplayRobotState";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6a3bab3a33a8c47aee24481a455a21aa";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1aW2/bRhZ+168Y1A+2W0Vp47ToapEHJ3YaF02c2u41CASKHEmsKY7KoSyri/3v+33n" +
                "zJCU5GxaYO3FApu9WCRnzv0+s2euZtZUbuxq4+uktqZ2Jsv9okjWvQu+vpS38q3X2zPnizp3ZVIU6765" +
                "SarcLb0p8vLamzQpzdiaWT6dFfhfbbPe+fg3m9YvXOGqd+/bLyPZ0Os9+w//672+/GZo5u7G5vVo7qf+" +
                "ccsBSL+a5d7MrffJ1JrUlXWSl97k5cRV84RcmWTslrWpNyXSN/nADuTtwvmcC71xE5PX3vzm8hJ/kjJT" +
                "IfS8Lb2rFPm3/Kjik3WjRojyxQNkUpt5sjaz5Maa+bKo80Vhzcn5S5NUkPnCpvkkt5mZ2cpugH7NtVjX" +
                "QSHbR5mbjLaQHdd1ks4AJXVFkXvy6UQv3hwk8Ru07t3cChcGKxoZHPbi/hdxu2oVCo27Rw3kUYBMvC+L" +
                "ZArpZnkK4ZZTs5pZQK0AGmrwqS2twQ8ghtGAYFstKgujMQnkCROcTMwqr2emspRD3VDoBIjsjzqlWp2v" +
                "i7XJ5wtX1UlZGygVYi0zMDQVbhpWxy7LLXiP9GBh6QR+WtikumsxEEHhSTCt4TB1lR0OO+4xtsBnzXKR" +
                "Ka95rcTXHZM77I2dK0DsiMzdl/XfbYAdSSWNC4j5zVyReQOyE/F869Mqh0IoBI0Hwri3NX/Urvp9qb5T" +
                "QeWQjzrAwPT2BEVnk4UA9bM5qOyNK5Z8X5lFlXu6W3pIajI7yUvKeT0EAPPphpsJSjxHKElGAPPDfrv0" +
                "xhYuzev17tLHXhY/9ofinu0WO4GuauWe8lgsCvpYXnYBvJlz95vDgTB22vKCHcsyhxRobZkta/XQMZ7g" +
                "UGUyV1kjDtoko6EGJ6a3Q6w5LBh4V7McEFt8IjVvVvBzGDzMK7PZwBwXxc4aQAelDs4T1SjBQ90obiUK" +
                "0SDIEZzKxnYA9LkHzWrZMfgkVZWsfV8w0IdEjYsEAtuQsBBDtQuvU5fQsEnFPLm2uimsB++0MBfyxsD8" +
                "NLOlsYPpwKzdsoohVLgoHQAG/STeQ7PAkkVXsvM+t0iigaeS61adQrex80W9DtZI6Sk3qtsO737mlgXC" +
                "aoQhcvL5H4j1YBmCVDgdr+EqV0LnK2ABm40NNGR2hEMraIiGoCvopQYw1eCg1+u9UuNQG+n1fF0haiCi" +
                "iv1MCpfUXz3FY3SEzqto8J1XKoF7Cyh1ptFEaQYbiChlllQZxFknEjkk2CKz2+pRYUEhOZ0voDmNK+sF" +
                "uW6FOUXsrlhCmKXX3JO6+RwiRaZQe93YryafiBXm6bJAjE4d7DwvuXxSQWaETgFbKKVMrTk7GYqB23RZ" +
                "5yAI3lmmlU08g/PZiektoaejJ9zQ27tauUdMQFMmp4i8iQ/2FnnJk87EM0Z9qswNAJvRFlhg3QfyboRH" +
                "hBsgAQl24eAEB6D87bqehZTKiikZw80AOIUEAHWfm/YPO5BJ9hCmULoIXiG2OP4M2LKBS54eNbnQL6cQ" +
                "IBYuKneDCCaxS+wUYRDGW+TjKqnWPQlVgrK395IyVhcSjTBsbrpnMGHVxijPHiK97RZBYPbCUl1gRCs6" +
                "BCHNSDTREGrEM5uAl9lpZRF7sXKCH5lDlAGcCfKbW8XiAdwt03qJ6IxlLT4Nq2eaR7xfzmnNtJskZgva" +
                "rV/72s41DviFKBQmD7+oktKz+NQ9U1u32Qhgk8IF7NdIkZIyTTpDzTowLxmYb6GaAhELjlEkJXQaUhfq" +
                "J6D94eLkpWTYI5aTB7cInfhvsqJBMB/CdrzVj6Fw7xp6lzoVJP5UOaDoXuaXje+AqisiNBjuja1oHuMk" +
                "vSbDGzT8P6k+bFJdVYiLsz+dVOPy/6Wk+qGcqu0Qt/ve1KKFqKu1BpCraMJY1ZjzzqIVqiQu4N+tbz+J" +
                "mPBR5XVfQe8DVEdJVjHkhRqzCStjW68s7KJeuZ2MKfpjwEOFmqSIZL0fIU9XHen+Qr36+yU2VCUDQOU0" +
                "pD4Mk4GYO1hMUALx2xb9pgnEYlFzyyYQNtXslKaSNgMeBnSwSno2dPe1yRzkgSZQohhcjVlGamqGY5hj" +
                "VyZ8jS0HdLY+m9pSVzFVSK0i1Q1idZVP82w7jErgD8z1TT15ApOGSwnNigwqBJAo7cOBOZuIg67IkDh3" +
                "bNbYpgW6JPnXzvVZUQUQmwJ9K04UfRWDjxp+Aq2HMtLcNr+a0tL88SCqbm3sLm0jhVesS1R8Gzrn0++t" +
                "gVLIH2Uo/lo9kK9K0AhsxQTr2651k59x5a5hTlAUTUzmahxIMOUm5VQKXyYNBLvoq2FJ+xzWPQx3Gv7u" +
                "0BpUoeppmevDqUC8pB4yyKL+z7EowNpHnUE8xPDwA4OvkJ633qofjzujI8lV6I3y2zgnodNKyuSULVbM" +
                "/C3ZCTMzkWMzmZRBFKpOlAF+lqCHEjGhJ8QvlrP8vkObxoXuXI/LBOEeIgngiR9RXeRVQJYuTAQHCFgc" +
                "ztkahR7DUpjm7d0FL872tINo2GgEoQiy3ragFGgvzot0sqQjZC0su/O3OKnkPBQUuVWgwS3F8IAYE6QE" +
                "tbx51BCmZLAkL9DyZWsddaAoUErDhrZAEGBxJo0sMjEpKleoIFAlUxnU2KlOiTYaUWhD2gLVB7OPMimZ" +
                "Z04YDSsAHZIGgpqF2NG3ayZLC/T5TB+VW4ojBCiH/TgOExylTRm+q7VgqyzKNW5jh8dCShFTfYAZh8ZI" +
                "EHztostK73LVvIQwSN0ooIhKWVmO6WOxuaUMZMyJuS7dqhlLhPUP4ZO7vngcKj5JfZmIppkmx/ZNfGa7" +
                "QFRWYfGBzSDAA7EegQXtvQbuMwzBg7OysdV9Uc+YbahRMCuPE2RfF6TTeI/+HXE4Ny1lnqO8KAtXhEAw" +
                "EXI7pg/RlsPDOwr36LO6LRdb5EpYyvbgXMrxzmlCFMClK8B/RJNyNjsHgTcQlkQcpVNWvY2fOJZql20X" +
                "GX7jO+0Ki4DptfWzTah8g7Vz/XAnHH5rQTync1AJbHc5wMewJ1QFvuWub8bhCEeWxXZUiwgwv1RPg8ay" +
                "THSB6EoUh13a3nIrGRFMH2CS31rqjjO29R3DUKlzVSljFx5YSDHXWQQbveEJGso+e4tKgeSjttRkKgFn" +
                "0BuvUbIfn5w8+5xoLiSobmCaVI4TBPRX5U1euXLOYpeDaUSINaSENhyTInUFOW+q4cx+yyby7FAxXZy+" +
                "Pv/x9NkXwtNiwTjFmjVacxhvhMAqRIdO8GO8xhpbN0U+oYWWybdvT9+cPHsSgnCL8250gqWPqLgKlh9U" +
                "LcX+AVdEvcWOdb70NVcUdlJrN8ppCKKZdwVlBdHGiNFGUxyJQJKZkiiyOdJzUIwwY0kPmHhkARoXuvj5" +
                "voLix4NKb+8v/zPnz789fXHFCelf3xz+UTgv/v3hqgRNOXaYSMILgQxhjDMpdquoCmQix4oRKrQVxyBT" +
                "Pa9rpgR6dgQ74fHcRlFxbZsDoS6GobzR/e3hjXSSYi6IWKXJxjHYA0oEmI27pIQEK2Oyby/P3zzG+DrO" +
                "zn45fv2dUQA4wGlMGGG2cYBOi8lAHaXSzgc1qceEMjCnUjXw2GdH6eJHMrtx7hr1yrUdmk/+sU8J7w/3" +
                "X7CyOXm+3zf7lXM13szqejF8/BjtR1JA2vX+Pz9RFpE1YOyoBGVwV4bIqNoL1Q2V05ECK8e83semHMU+" +
                "vODa2jA2nxRw1XFeoMUJ6ekue+Upqgoxtswnz9U2BAi5ot8HzDryonEtISeGOB2AylCeA9HArJwjCpih" +
                "aQQg7ygCvNsWwfDLv339VFcw9eqEAOt2Kd4PmC6//w7npigReHja6GkD8eXvxau4QmELKrO/mvqjr/QN" +
                "j6qH5sunR0/kEasrLkD57FZhBdL+CnObrdesUMhIRBBP3fXr3GXLgt9lKlC7xX40aJj2fY3lP1QtgKIT" +
                "ddOxw/zXL2hpfZOuUVpL0QZzsyYMFmOXA7OI58IwqzhQRIUzjiUAgDHgM6WLJ2rh/Hkf/8EIgIc7X5vn" +
                "5z8jjenvy7evTi9OkVr08cUv3529OTm9QCgPL87fnD57Gr09xifJMqQprNIqLYYEnI+grQiXQdql7blc" +
                "uyLu4VSK5Hc3dJYNdcbLdkXuMKgQNFVTXLcxUu23e/Y1ucmFi9ATgnEhVbuHn/vmFx3b/9qlmUKWhsmW" +
                "UxSLgaLtGMS2qeEPQh+0sh39jIqkffqlkTWffmUW75Ck8g9UybCLamfYxN9wqo7oqXQiojEUK984yc9x" +
                "xchNQpujFhTpULiji+OTsx8uWSF1cEYlC0wqWA8iVSpqOjJ+kJlhLA/l0CWg+tUkKDgGph0WbsAdvTo9" +
                "++bVlTkg7PBw2PKkd0Y6Em95mm20V9EXzAF94VDxMc5FPMpdwKMPHTwfwsIhYpSdqi80J3fjRMrWYUD8" +
                "hP1tnb/tkzzgyStpgWWWijPGRWtDIlPuZ7NJe18u+uE467Mg1OikW8JsTGqLedajrafuLG4Fw4X3E+J2" +
                "mwBpPqud80YWo9uzL9EWywP9rrdbKO3OcBMjbB3SNqf+nel7Z91DMQhS4mhvYyTVvZ2DWUg852zZ/cgI" +
                "9v5TEFvLmHg6pLKdZIBAd63Oh0PzcooK4u+dCHuTFEvYO/ydtwFc58ofeOSJJood/+59jziuAgA5Pgqw" +
                "iKAzuIs7Yu+Fym/JIkmu78x2OkvIUu546KYHElVk4w6RRbZQ5TVE6dWJd0dKp70dyRzwQaiVvvzOQ349" +
                "BIenaX/f9v/NkCC5NZ9x/PeZSf/A/2XmmZGOOjHDZzBwO3n3+XtOFJvHL/iYNo9P+Jg1j0fvm6OGd0/f" +
                "y7v7EsBHZnhbc607zz23tkRDE+f1/yW6Y4SRy3Xt2hBR2ntzmP2x7Wsc8V0/np/gKx6SNMUoVLtt/55a" +
                "cpur9X7U+2Zm3sGlqcze8p4Q5xChDm3GIiEaMPvFqQOng7xkh0MLv3U8LTFii8sBWO/dcaXL797p4kXT" +
                "9uUGW7u3vbJlHD8g9484A+KN3+pBhrCda929jWkoulwKWW83ruJ8iQN4Lt2ZoYp027JB14TbZgL94pvn" +
                "x+HDfd9ua/CpqHE3rGp+TZtf4+ZX0uv9C560K788LwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
