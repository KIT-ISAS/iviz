/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/DisplayRobotState")]
    public sealed class DisplayRobotState : IDeserializable<DisplayRobotState>, IMessage
    {
        // The robot state to display
        [DataMember (Name = "state")] public RobotState State;
        // Optionally, various links can be highlighted
        [DataMember (Name = "highlight_links")] public ObjectColor[] HighlightLinks;
    
        /// <summary> Constructor for empty message. </summary>
        public DisplayRobotState()
        {
            State = new RobotState();
            HighlightLinks = System.Array.Empty<ObjectColor>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DisplayRobotState(RobotState State, ObjectColor[] HighlightLinks)
        {
            this.State = State;
            this.HighlightLinks = HighlightLinks;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal DisplayRobotState(ref Buffer b)
        {
            State = new RobotState(ref b);
            HighlightLinks = b.DeserializeArray<ObjectColor>();
            for (int i = 0; i < HighlightLinks.Length; i++)
            {
                HighlightLinks[i] = new ObjectColor(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DisplayRobotState(ref b);
        }
        
        DisplayRobotState IDeserializable<DisplayRobotState>.RosDeserialize(ref Buffer b)
        {
            return new DisplayRobotState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            State.RosSerialize(ref b);
            b.SerializeArray(HighlightLinks, 0);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/DisplayRobotState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6a3bab3a33a8c47aee24481a455a21aa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1aW2/bRhZ+F5D/MKgfbLeq0sZp0dUiD07sNC6aOLXdSxoEAkWOJNYUR+VQltXF/vf9" +
                "vnNmSEpWNi2w8WKBTS8RyZlzv8/smauZNZUbu9r4OqmtqZ3Jcr8oknXvgq8v5a186/X2zPmizl2ZFMW6" +
                "b26SKndLb4q8vPYmTUoztmaWT2cF/qtt1jsf/2bT+pkrXPX2XftlJBt6D3pP/sN/HvReXn47NHN3Y/N6" +
                "NPdT/7Dl4QGov5rl3syt98nUmtSVdZKX3uTlxFXzhIyZZOyWtak3hdI3+cAO5O3C+ZwLvXETk9fe/Oby" +
                "En8lZaZy6Hlbelcp9u/4USUo60aNHOWLB8ikNvNkbWbJjTXzZVHni8Kak/PnJqkg9oVN80luMzOzld0A" +
                "/ZJrsa6DQraPMjcZbSE7rusknQFK6ooi9+TTiWq8OUjiNyjeu7kVLgxWNDI47MX9z+J2VSx0GnePGsij" +
                "AJl4nxfJFNLN8hTCLadmNbOAWgE01OBTW1qDH0AMuwHBtlpUFnZjEsgTVjiZmFVez0xlKYe6odAJENkf" +
                "dUq1Ol8Xa5PPF66qk7I2UCrEWmZgaCrcNKyOXZZb8B7pwcLSCfy0sEm1azEQQeFJsK3hMHWVHQ47HjK2" +
                "wGfNcpEpr3mtxNcdkzvsjZ0rQOyIzH08B9htgo0D4N+k8QKxwJkrMm9AeSL+b31a5dAJ5aBRQXj3tuaP" +
                "2lW/L9V9KmgdIlIfGJjenqDobLKQoX42B5W9ccWS7yuzqHJPj0sPSU1mJ3lJUa+HAGA+3fA0QYnnCCXJ" +
                "CGB+2G+X3tjCpXm9vrv0oZfFD/2heGi7xU6grlq5pzwWi4JulpddAK/m3P3qcCCMnba8YMeyzCEFGlxm" +
                "y1qddIwn+FSZzNUwEQ1tktFWgx/T4SHWHEYMvKtZDogtPpGaNyu4OmweFpbZbGCOi+LOGkAHpQ7+E9Uo" +
                "8UM9KW4lCtEgyBGcysZ2DPS5B81q3DH+JFWVrH1fMNCNRI2LBALbkLAQQ7ULr1OX0LZJxTy5troprAfv" +
                "tDAXssfA/DyzpbGD6cCs3bKKUVS4KB0ABv0k3kOzwJJFb7LzPrdIuoGzkutWnUK3sfNFvQ7WSOkpN6rb" +
                "Du9+5pYFImuEIXLy+R8I92AZglQ4Ha/hKldC5ytgAZuNDTRkdoRDK2iIhqAr6KUGMNXgoNfrvVDjUBvp" +
                "9XxdIXAgqIr9TAqX1F8/xmN0hM6raPCdVyqBjxhT6kwDilLNYIKoUmZJlUGidSLBQ0IuUrytPi8siCSz" +
                "8wWUp6FlvSDjrTyniOAVawmz9JqBUjefQ6rIF2qyG/vV6hMxxDxdFojUqYOp5yWXTyqIjdApYwu9lKk1" +
                "ZydDsXGbLuscBMFBy7SyiWeIPjsxvSVUdfSIG3p7Vyv3OdPQlCkqIm9ChL1FdvKkM/EMU58qcwPAZsQF" +
                "Fhj4gbwb4RERB0hAgl04+MEBKH+9rmchsbJ0SsbwNABOIQFA3eem/cMOZJI9hDWULoJXiC2OPwO2bOCS" +
                "p8+bjOiXUwgQCxeVu0EQk/AlpopICPst8nGVVOueRCtB2dt7ThmrF4lGGDk3PTRYsWpjlGf3k+TuFkO0" +
                "zwtLjYEXLe0QijQv0UpDwBH/bMJeZqeVRQTGygl+ZA6xBnAmyHJuFasIMLhM6yViNJa1CDW4nmk28X45" +
                "p0HTdJKYM2i6fu1rO9do4BeiU1g9XKNKSs8qVPdMbd3mJIBNChewXyNRSuI06QzF68A8Z3i+hXYKxC34" +
                "RpGUUGtIYCikgPbHi5PnkmePWFce3CKA4t9kRZtgVoT5eKsfQxHftfUudSpI/FXlgKJ7mWU2vgOqrojQ" +
                "YLs3tqKFjJP0mgxv0PD/1Hq/qXVVITTO/nRqjcv/l1Lr+zKr9kXc7ntTi16irtYaQa6iCWNVY853Fq1Q" +
                "K3EB/9769rOICR9VXh8v7r2H7qa6r2LUC8VmE1nGtl5ZmEa9cnfypqiQMQ+lapIimPV+gkhddaT7C3Xs" +
                "H5bYUJWMAZXTqHpffAZydnGZoBzixy0WTBOOxa7mlj0hLKvZKT0mLQdsDOhmlbRwaPZrkzmIBD2hxDI4" +
                "HJON1NcMyjDKrlj4GlsO6HJ99rilrmLCkKJFyhxE7Cqf5tl2MJXwH7jrm3ryCIYNxxKaFRm0CCBR4IcD" +
                "czYRN12RIXHx2LixZQt0SRVQO9dnaRVAbEr0tbhS9FjMQWp4CxQfSkpz2/xqykzzxz1puzW0nQpHLq9Y" +
                "o6gEN9TOp99bM6WcP8RT82t1b07L+NFwFpOtb/vYTZbGlbuGUUFdNDSZt3FKwfSblFOpg5lAEPii04Yl" +
                "7XNYd18MajDcpTsoRJXU8teHd4F+yUTkkWX+n+NSgLWPOpi4n7nie0ZiyrI1W6/Vp8edqZJkLzRM+W2c" +
                "n9CBJYlyABfLaP6WfIVxmoiyGVrKjAp1KAoDP0vQWImk0CviFwvc+i4VvT2NEd2RH5cJwj1EFcATh6LG" +
                "yKyALF0YFg4QvDi3szVKP4aoMOjb2wUvjv20rWjYaAShCLLetqAUaC/OkXTipANmLTW7o7k4xOSoFBS5" +
                "VaDBLZGA94gYk6UE1b35vCFMyWCRXqAPzNY6AkGZoJSGDW3JIMDCxJoZZWJS1LJQQaBKpjWoulOdHm10" +
                "p9CGNAqqD2YiZVKy0JwwGlYAOiQQRDcLsaOf16yWFuj/mUoqtxRfCFAO+3FMJjhKmzKUV2vBVlkUcNzG" +
                "to+llSKm+gAzzpORLPjaRbeVbuaqeQlhkLpRQBGVsrIc4sfyc0sZyJ4Tc126VTOuCOvvxy13uONxKAMl" +
                "E2YinWbWHHs6cZvtqlG5hdEHToMMD8SABBYU+BLIzzAiD/6Khjfsi6rGzEPtgkl6nCAZuyCgxoH07xHn" +
                "dtNSRj3KjPJwRQgEEyG3Q/wQdDlX3FHNR7fVbbmYI1fCWLbH6lKjd84aogAuXQH+I5qUY9s5CLyBsCTo" +
                "KJ2y6nX8xIlVu2y75vAb32laWARML62fbULlG6yd64edcPitBfGU/kElsAfmeB9DoFAh+Ja7vhmHAx5Z" +
                "FntULSjA/FKdDRrLMtEFAixRHHZpe82tZEQwvYdJfmupO87Y63cMQ6XOVaWMY3icIbVdZxFs9IZHbKgC" +
                "7S1qBpKPUlNTqsScQW+8RhF/fHLy5AuiuZC4uoFpUjmOFdB0lTd55co5a1/OrBEk1pASenNMkNQV5DSq" +
                "hj/7LZvIs0PFdHH68vyn0ydfCk+LBUMVS9hozWHmEWKrEB3aww/xGktu3RT5hBZaJl+/Pn118uRRiMMt" +
                "zt3oBEsfgXEVLD+oWmr/A66Ieott7Hzpa64o7KTWFpUjEgQ07wrKCqKNEaMNqDgtgSQzJVFkc6QHpRht" +
                "xgofMPHIYjQudPHzx4uLHw4rCI9/+Y85f/rd6bMrDk//+ubwh/J59u9PXyVuyqHERNJeiGWIZJxVsYVF" +
                "bSCTOpaO0KKtOB6Z6oFeMz3QkyWYCs/vNkqLa9scF3UxDOWN7m+PdqS3FItB0CpNNo7xHlAiwGzcJSWk" +
                "WRmffXd5/uohJttxpvbm+OX3RgHgeKexYkTaxgc6TSdjdZRKOzfU1B5zysCcSu3AQ6E7WhdXkpmOc9eo" +
                "Wq7t0Hzyj31KeH+4/4z1zcnT/b7Zr5yr8WZW14vhw4doRZIC0q73//mJsojEAXtHPSgDvTIER9VeqHGo" +
                "nI4UWD/m9T425aj64QjX1oaJ+qSAt47zAu1OyFC7DJbHrCrE2ESfPFXbECDkiq4fMOsojMa1hJwY5XQw" +
                "KvN6DkoDs3LKKGCGphGAvKMI8G5bBMOv/vbNY13B7KszA6y7S/F+wHT5w/c4VUWVwKPVRk8biC9/L17E" +
                "FQpbUJn91dQffa1veJY9NF89Pnokj1hdcQGKaLcKK5D5VxjmbL1mkUJGIoJ4LK9f5y5bFvwuc4LaLfaj" +
                "QcO0P97E/n0lA8u0E/XUscNo2C9obH2TrlFjS+kGi7MmzBxjuwPLiAfHsKw4a0SdM46FAIAx7DOxizNq" +
                "Bf1FH/9gKMCjn2/M0/NfkMz09+XrF6cXp0gw+vjszfdnr05OLxDQw4vzV6dPHkeHjyFKcg1pCqu0VotR" +
                "Aacn6C/ChZF2aXtw166IeziqIvndDZ1lQx3/sm+Rew4qBE3YFNdtDFb77Z59TXFyKSM0h2BcSNU24pe+" +
                "eaMT/V+7NFPI0jnZcoqSMVC0HYbYPzX8QeiDVrajX1CXtE9vGlnz6Vfm8g5JKv9AlUzAqHZGTvwdjt0R" +
                "QJVOBDVGY+UbR/05biK5Seh31IIiHQp3dHF8cvbjJeukDs6oZIFJBesxpUpFTUdGETJIjEWinMcEVL+a" +
                "BGXHwLQTxA24oxenZ9++uDIHhB0eDlue9F5JR+ItT7ONPiv6gjmgLxwqPoa6iEe5C3j0oYPnfVg4WYyy" +
                "U/WFFmU3TmRtnQrET9jfVvvbPsmzn7ySXlgGrDiBXLQ2JDLlfnadtPfloh9Ouj4LQo1OuiXMxqS2mGdV" +
                "2nrqncWtYLDwniZhbAa0C63unEayKt0ehYnCWCTod70BQ4F3Jp4YbevwtrkZ0BnMd9bdH48gphn2bUyo" +
                "upd4MBqJB6Etxx8Yzd5HLmKj2WSgDrXsLhkp0GyrF+JsvZyimvh7J9TeJMUShg/H56UB17kfCDZ56onC" +
                "x7991yOSqwBAjpgCLCLojPLijtiKoQpcsmCSiz6zO40mxCm3QXTTvUkrMrJLapEzFH0NXXrJ4u2Rkmpv" +
                "RzIcvCeCpVfffR1Aj8vhddr0t0OBZnKQ3JrPOBb8zKR/4H+ZeWKkzU7M8Aks3U7efvGOk8bm8Us+ps3j" +
                "Iz5mzePRu+Ys4u3jd/Lu48ngA9O9B1vzrp2HpFt7osWJI/v/GulNwJEree3iEGDa23aYC7IdbJzybT+e" +
                "seArHpI0xaBUG3H/jrpym6v1VtW7ZqLewaX5zd7yahFHFKE4bSYmITIwJcaBBAeHvJqHUw2/dZwt8WKL" +
                "zQF47+24CObv3gTjDdX25QZbd++IZcs4mUBBMOJ4iFeFq3sa0XYuhT/YnJWiAaac9VrkKk6fOKHH2rsT" +
                "VhFwW07omnBJTcBffPv0OHz4+NfiGowPVOC4VFY1v6bNr3HzKwFR/wJn554Ffy8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
