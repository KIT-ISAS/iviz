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
        internal DisplayRobotState(ref ReadBuffer b)
        {
            State = new RobotState(ref b);
            HighlightLinks = b.DeserializeArray<ObjectColor>();
            for (int i = 0; i < HighlightLinks.Length; i++)
            {
                HighlightLinks[i] = new ObjectColor(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new DisplayRobotState(ref b);
        
        public DisplayRobotState RosDeserialize(ref ReadBuffer b) => new DisplayRobotState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
                "H4sIAAAAAAAAE+0aWW8bN/p9fgVRP8hqFaWN06KrhR8c22kcJLFru0caGAI1Q0msZ4YqybGsLPa/73eQ" +
                "nNGRpgXWBhbY7GHNkPzum7MnrudKWDMxXjgvvRLeiEK7RSlX2SW+vqK3tJZle+J84bWpZVmuBuJOWm0a" +
                "J0pd3zqRy1pMlJjr2byE/3lVZOeT31Xuj01p7IebdmVMB7Ls8L/8L3t79cNIVOZOaT+u3Mw9bTkA0q/n" +
                "2olKOSdnSuSm9lLXTuh6amwlkSshJ6bxwq9LZCD0UA3p7cI4jRudMFOhvRO/G13DH1kXLITMqdoZy8hf" +
                "4yKLj/aNkxBf8zE/l15UciXm8k6Jqim9XpRKnJy/FNKCzBcq11OtCjFXVq2Bfot7YV8HBR0fF2Y63kB2" +
                "5L3M5wAlN2WpHfJpSC9O7Mu4Blp3plLEhYAdSQb9LJ4/jsdZq6DQeHqcII8DZMT7spQzkG6hcxBuPRPL" +
                "uQKoFkCDGlyuaiXgByAGowGClV1YBUYjJMgTTHA6FUvt58IqlINPFBoCQuejTlGtxvlyJXS1MNbL2gtQ" +
                "Koi1LkpEjdwkViem0Ap4j/TAxtoQ/LxU0u7aDIhA4TKY1miUG6tGo457TBTgU6JZFMyr9ky875hcP5sY" +
                "UwKxY2Tuoax/twF2JCWTC5D5zU1ZOAFkS/J85XKrJ4qEwPGAGHfK4w9v7B8N+44FlYN82AGGItsjFJ1D" +
                "CgTIy2LfqjtTNvjeioXVDt0t7yM1hZrqGuW8GgEA8eWamxHKuUpQZIEAqv6g3XqnSpNrv9re+tTR5qeu" +
                "T+7ZHlFT0JVn7lEei0WJPqbrLoB3FZ5+1x8SY6ctL3CiqTVIAa2tULVnD52sKB7UslJBEHMlCzTU4MSO" +
                "oHtdoXGBL+h83sFHUnNiCX4OBg/mVahiKI7KcmsPQAdKDThPVCMFD3ajeBRRkAaBHMI5DPpZD4BOO++C" +
                "ZcfgI62VKzcgDOhDpMaFtH5dwkQMqp14nRlZBmeu5K3iQ2E/8I4WZkLeGIpf5qoWajgbipVpbAyhxEVt" +
                "AGDQj3QONCsxIgRXUtUAj1CiAU+9U111Et1CVQu/CtaI0mNuWLcd3t3cNGURJBfl5PRHiPXAMgiS4XS8" +
                "BneZGnS+BCzAZrKBRGZHOGgFiWgQtAW9eADGGhxmWfaKjYNtJMuctxA1IKKS/UxLI/13z+ExOkLnVTT4" +
                "ziuWwIMFFF9wNGGagQ2IKHUhbQHi9JIiBwVbyOzKPikVUIicVgvQHMeV1QK5boU5g9htsYQQjePck5uq" +
                "ApHmJEew17XzbPKSrFDnTQkxOjdg57rG7VMryb5xm1OglDpX4uxkRAau8sbrO/LVOrdKOgzOZycia0BP" +
                "B8/wQLZ3vTRPMAHNMDlF5Ck+qHvIS85RYsIY9SUzNwTYGG0BC1j3Pr0bwyOEG0ACJKiFASfYB8ovVn4e" +
                "UipWTHJSUubLJUXQHh7q9TuQawJdy9pE8AyxxfFXwNYJLvL0JOVC18xAgLBxYc2dLjh2kZ1CGATjLfXE" +
                "SrvKKFQRymzvpaVAguojjWDYXHfPYMKsjbEuHiO9bRdBwOylQnUBIzJmEM5IaKIh1JBnpoBXqJlVisLg" +
                "FH4UBqIMwJlCfjPLWDwAd03uG0uZrcXHYfXMB4E0FVoz2o2M2QLt1q2cVxXHAbcghYLJg19YWTssPvnM" +
                "TPk2GwFYWZqA/RZSJKVMkc+hZh2KlxiY70E1JUQscIxS1tLG1CUp3v10efKSMuwBlpP79xA64b9yiQaB" +
                "+RBsxyleDIV719C71LEg4Y/VAIXPYn5ZWweovCNCA8O9UxbNYyLzW2R4jYb/J9XHTapLC3Fx/peTatz+" +
                "v5RUP5VTuR3C4y6bKWghvF1xALmOJgy7kjlvbVqCQnED/t1Y+4XEBIssr4cKep+gOkrSxpAX3CGFlYny" +
                "SwV24ZdmK2OS/jDggTPJHGw5+xnkaewBny/Zq39s4ICtMQBYwyH1cZgMxOxgUUIJhGsb9IsUiMmiKoVN" +
                "INhUOklNJdoM8DBEB7PUsw2wVysMyAOaQIpi4GqYZcj9MRyvYjBkmeBrOLKPzjbAprbmXZgqqFah6gZi" +
                "tdUzXWyGUQr8gbmB8NNnYNLgUkQzIwMVApAo7f5QnE3JQZfIEDl3bNYmKtFFyd8bM8CKKoBYF+gFOVH0" +
                "VV1DSpIFaD2UkeI+/Uqlpfj4KKpubWyXtiEsW53S+ZrO8emP1kBRyJ9lKP5aPpKvUtAIbMUE69qudZ2f" +
                "iTW3CpkkE6O5Gg4kMOXKekaFLyYNCHbRV8OW9jnsexzuOPzt0BqogtXTMjcApwLiKfUgg5hy/xqLBKx9" +
                "5BnEYwwPPzH4Cul54y378aQzOqJcBb2Rvo9zEnRaSpk4ZYsVM/6m7JRBJ4KLaTJJgyioOqEMcHMJPRSJ" +
                "CXpCRRKm9S3aOC5053q4jRDuQSSROfsRqgt5JZC1CRPBIQQsHM4pD4UehqUwzdvbBS/O9rhQS2wkQTCC" +
                "ItsUFAPN4ryIJ0s8QubCsjt/i5NKnIdKrMgDDaYhwwPEhZpKqLPEk0QYk4EleQktX7HiqgyKAqY0HGgL" +
                "BAIWZ9J7GG9zqFxBBYEqKiChxs55SrTWiII2qC1gfWD2YSYp81QII7ECoEPSgKCmQOzQt3Mmy0tDjam0" +
                "piFHCFD6gzgOIxy1yjF82xVhs6rkfhY7PCykGDGqD2DGoTEkiN8JyaozErxOL0EYSN04oIhKWSoc08di" +
                "c0MZkDGn4rY2y7qNprT/MXxy2xePQsU34GHClCqDME2O7Rv5zGaByKyCxQc2gwD3yXoIFmjvLeA+8/3o" +
                "rNjY8rmo59VCsVFgVp5IRx0iSSd5D/8dYx8xq2mew7wwC9cIAcFEyO2YPkRb7HN2FO7RZ/mYtiEyoKds" +
                "Ds6pHO/cJkQBXJkS+I9ocpzNVhoHJi6jiMN00q6LuIRjqXbbZpHh1tbHLHjA9Fa5+TpUfAN7K17YCQfX" +
                "WhAv0DlQCdju4gBfYfIP0SxxNxCTcIVD22I7ykUEMN+wp4HGikJz90SC63dpu8CjyAhh+gSTuNZSd1QU" +
                "rmtGQeq4q6axC15YUDHX2QQ2eoc3aFD2qXuoFJB87Tk6c8AZZpMVlOxHJyeHX2c03kBvWMM0tabiSWh9" +
                "p62pKyx2sYe22ErtK2jDVxCayBXovsmDM7sNm9BFnzFdnr49//n08BviabHAOIU1a534ovFGCKxEtIuj" +
                "8z/nNdbYfCjyCVpomby4OH13cvgsBOEW5250hGUAUXEZLD+omor9fdwR9RY71qpxHneUauq5G8VpCEQz" +
                "Z0qUFYg2Row2mhbKgSQLJpFkc8D3oMqmkh5gwiMWoHGjicsPFRQ/H1Syvb/9T5y/eH16fI0T0r9/OPxD" +
                "4Rz/+eUqBU2akEwp4YVABmEMZ1LYrTrFMxSsGEGFyuIYZMb3dWlKwHdHYCd4PbdWVNyqdCHUxTCiN3y+" +
                "nTPZaFAziFi1KCYx2AOUCLCYdEkJCZbGZK+vzt89zU0VZ2fvj96+EQxgKI6SCUOYTQ7QaTExUEeptPNB" +
                "TuoxoQzFKVUNut6hdPIjmt0Ycwv1yq0aiS/+1UMJ90a9Y6xsTl70BqJnjfHwZu79YvT0KbQfsgRp+96/" +
                "v2AWLVVMteHBXR0iI2svVDeonI4UsHLUvgeHdE7N8q1SYWw+LcFVJ7rUcd6jdtkr3qKyEGPLfPKCbYOA" +
                "IFfo9wEzj7zQuBqQE4Y4HoDSUB4HooFZukckMCORBEDvUATwblMEo2//8f1z3oGplycEsG+b4l7AdPXj" +
                "GwFqcwovT5Oe1hBf/VG+ijsYNqESveXMHXzHb/CqeiS+fX7wjB5ht8UNGsvcsAPS/tLYYuM1VijISEQQ" +
                "b915tTJFU+I6TQW8WfSiQYNpP9RY/lPVAlB0wm46MffQAy7Q0gYiX0FpTUVbjjPRMFiMXY5V6V4YzCoO" +
                "FKHCmcQSAIBhwMeUTp7IhfPXA/jPMKPLne/Fi/NfIY3x76uLV6eXp5Ba+PH4/ZuzdyenlxDKw4vzd6eH" +
                "z6O3x/hEWQZpCru4SoshQUOidfFjkHZrey/X7ohncCqF5HcPdLaNeMaL7Qp9w8BC4FSN4rqPkarXnulx" +
                "csuCaeIqME6kcvfw60C857H9b12aUcjUMKl65tNceTMGYduU+AOhD1vZjn+FiqR9ep9kjU+/YRbvkMTy" +
                "D1TRsAvVjmET/oYLAIfVDwcVCsXMt5WFbpCE0OawBQ3X9Dq+PDo5++kKK6QOzqhkgokK5otIlgqbDo0f" +
                "aGYYy0O6dAmofhMSCo6haIeFa3DHr07Pfnh1LfYRdnjotzzxNyMdibc8zdfaq+gLYh99oc/4MM5FPMxd" +
                "wMMPHTyfwoJDxCg7Vl9oTnbjPDY1DwPiEpxv6/xNn8QLHm2pBR6yy+hFa0MkUzyPzSbae7MYhOusr4JQ" +
                "sw1PDPJLJrXBPNajradubW4FgxsfJsRtNwHUfNqt+0YsRjdnX6QtLA94nb9uQWl3hptDkfGQNt36d6bv" +
                "nX2PxaCu0+RybSTV/TpHso7X2f3MCPbhUxC2ljHxdEjFdhIDBHTX7HxWy3oGFcQ/OxH2TpaNwv5ril8D" +
                "mM4nf8Aj3mhCseM+3GSI4zoAoOujACsLwSMM7uKJ2Hvd4kditIGo2SFz+saDDz2SqCIbO0QW2eq5lij+" +
                "dOLDAdOp7sc0B3wUaqkv33nJz5fgahD6+7b/T0MCeS++wvHfVyL/CP9XiENBHbUUo0MwcDX98PUNThTT" +
                "4zf4mKfHZ/hYpMeDm3TV8OH5Db17KAF8Zoa3Mdfaee+5cSQaGjnvgynuM3THCEPfAbR7Q0Rpr/iVprYv" +
                "OeKHQbw/gVV4kHmuytBtuxvUklnfzd9H3aSZeQcXpzJ1j98J4Rwi1KFpLBKiAWa/OHXA6SB+D2CxdVm/" +
                "nqYYscHlEFjPdnzS5ba/6cIPTduXa2xtf+1VNHH8ALl/jDMg/OL34T4D6w5hO591r09DNc9f+UOMZZwv" +
                "4QAet27NUEm6bdnAe4o06DX28ocXR2Hhob9uS/hY1AfPhE2/ZunXJP2SWfYfnrQrvzwvAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
