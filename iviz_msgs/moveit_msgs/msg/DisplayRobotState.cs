/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/DisplayRobotState")]
    public sealed class DisplayRobotState : IDeserializable<DisplayRobotState>, IMessage
    {
        // The robot state to display
        [DataMember (Name = "state")] public RobotState State { get; set; }
        // Optionally, various links can be highlighted
        [DataMember (Name = "highlight_links")] public ObjectColor[] HighlightLinks { get; set; }
    
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
        public DisplayRobotState(ref Buffer b)
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
        
        public void Dispose()
        {
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
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += State.RosMessageLength;
                foreach (var i in HighlightLinks)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/DisplayRobotState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6a3bab3a33a8c47aee24481a455a21aa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+0a62/bNv67gPwPxPLByea6XdINOx/yIW3SNcXadEn26IrCoCXaZiOJHknF8Q73v9/v" +
                "QVKS464bcAlwwHWPWhL5e7/JXXG1UMKaqfHCeemV8EYU2i1Lud7JLvD9Jb2mjzvZTrYrzpdem1qW5Xoo" +
                "bqTVpnGi1PW1E7msxVSJhZ4vSvjPq2InO59+VLl/bkpj339oP01ox06WHf2X/2SvL78fi8rcKO0nlZu7" +
                "xy0XQPzVQjtRKefkXInc1F7q2gldz4ytJPIl5NQ0Xvi+WIZCj9SI3i6N07jQCTMT2jvx0ega/pJ1IQJT" +
                "TtXOWMb+Cr+yDGnhpCPJV7zTL6QXlVyLhbxRompKr5elEifnL4S0IPqlyvVMq0IslFV96K9xMSzsYKH9" +
                "k8LMJnfwHXsv8wUAyk1ZaofcGlKPE3syfgMDcKZSxIuAFUkS+ztZBPA87mftgmLj9kkCPQmgGfWLUs5B" +
                "zIXOQcr1XKwWCgBbgA76cLmqlYAfgBvsB6hWdmkV2I+QIFgwyNlMrLRfCKtQGj4RaQgI7d+J2kUFG+fL" +
                "tdDV0lgvay9AvSDduigRN3KU2J2aQivgPxIEC2tDCPJSSbttMWIC3ctgZeNxbqwajzveMlWAUIlmWTC3" +
                "2jP5vmN9IM6pMSWQO0H+7s0VthtjR1gy+QMZ4sKUhRNAuKRYoFxu9VSRHDhCEOtOefzhjf29YUeyoHgQ" +
                "EXvDSGS7hKKzSYEM+bPYs+rGlA2+t2JptUPfy/eRmkLNdI2iXo8BgPiy53OEcqESFFkggGp/2C69UaXJ" +
                "tV/fXfrY0eLHbp98td2iZqAtz9yjPJbLEr1N110Abyrc/WZ/RIydtrzAjqbWIAU0uELVnn11uqbgUMtK" +
                "BUEslCzQWIM7O4LudYX2Bf6g80UHH0nNiRV4PBg9GFihipE4Lss7awA6UGrAgaIaKYywK8WtiII0COQQ" +
                "zlHQTz8aOu28C7Ydw5C0Vq7dkDCgG5Eal9L6voSJGFQ78To3sgwOXclrxZvCeuAdLcyENDISvyxULdRo" +
                "PhJr09gYT4mL2gDAoB/pHGhWYlQIzqSqIW6hvAPOeqO66iS6haqWfh2sEaXH3LBuO7y7hWnKIkguysnp" +
                "PyDwA8sgSIbT8RpcZWrQ+QqwAJvJBhKZHeGgFSSiQdAW9OIBGGtwlGXZSzYOtpEsc95C3IC4SvYzK430" +
                "3z6Fx+gInVfR4DuvWAL3FlB8wdGEaQY2IKLUhbQFiNNLihwUbyHPK/uoVEAhclotQXMcV9ZL5LoV5hzi" +
                "t8WKQjSOU1BuqgpEmpMcwV57+9nkJVmhzpsSwnRuwM51jctnVpJ94zKnQCl1rsTZyZgMXOWN1zfkq3Vu" +
                "lXQYns9ORNaAng4PcEO2e7UyjzAJzTFBReQpPqhbyE3OUXLCGPUlMzcC2BhtAQtY9x69m8AjhBtAAiSo" +
                "pQEn2APK3679ImRWLKDktKTsl0uKoAPcNNjvQK4JdC1rE8EzxBbHXwFbJ7jI06OUDl0zBwHCwqU1N7rg" +
                "2EV2CmEQjLfUUyvtOqNQRSiz3ReWAgmqjzSCYbPvnsGEWRsTXTxEertbDQGzFwrVBYzImEE4I6GJhlBD" +
                "npkCXqHmVikKgzP4URiIMgBnBvnNrGL9ANw1uW8sZbYWH4fVMx8E0lRozWg3MmYLtFu3dl5VHAfckhQK" +
                "Jg9+YWXtsBLlPXPl22wEYGVpAvZrSJGUMkW+gAJ2JF5gYL4F1ZQQscAxSllLG1OXpHj308XJC8qwh1hY" +
                "7t1C6IR/5QoNAvMh2I5T/DHU8V1D71LHgoS/rAYovBfzS+87QOUVERoY7o2yaB5TmV8jwz0a/p9UHzap" +
                "rizExcVfTqpx+f9SUv1UTuW+CLe7bK6gjfB2zQHkKpowrErmfGfRChSKC/DvjW+/kJjgI8vrvoLeJ6iO" +
                "krQx5AV3SGFlqvxKgV34lbmTMUl/GPDAmWQOtpz9DPI09pD3l+zVPzawwdYYAKzhkPowTAZitrAooQTC" +
                "bxv0ixSIyaIqhX0g2FTaSY0l2gzwMEIHs9S1DbFbKwzIA/pAimLgaphlyP0xHK9jMGSZ4GvYsofONsTG" +
                "tuZVmCqoVqHqBmK11XNdbIZRCvyBuaHwswMwaXApopmRgQoBSJT2/kiczchBV8gQOXds1qYq0UXJ3xsz" +
                "xIoqgOgL9C05UfRVXUNKkgVoPZSR4jb9SqWl+ONBVN3a2DZtQ1i2OqXzns7x6ffWQFHIn2Uo/lo9kK9S" +
                "0AhsxQTr2q61z8/UmmuFTJKJ0ZgNZxKYcmU9p8IXkwYEu+irYUn7HNY9DHcc/rZoDVTB6mmZG4JTAfGU" +
                "epBBTLl/jUUC1j7yDOIhJomfGH+F9Lzxlv142pkeUa6C3kjfxjkJOi2lTBy27cSSGR8oPdHojESZJpU0" +
                "joLCEyoBt5DQRpGkoC1UJGT6vkEIwODY0B3x4TpGugvhRObsTKgzZJiA1iZMB0cQtXBKpzxUexib4mBv" +
                "dxvEOOXjci3xksTBGIqdbFNeJlC7EwdHPGLi0TJXmN1ZXJxc4ohUYmkeyDBNvkAQgLxQMwkVl3iUiGNS" +
                "sDgvofkr1lyfQXnA1IYNO22tQODisBrBgrRyKGNBGYEyqiah4M55ZNTrSkEv1COwZjAVMauUhiqEkdhB" +
                "2CGFQIhTIH/o4jmv5aWhNlVa05BbBDD7wzgcIyS1yjGY2zWhs6rk7hYBU13FqFGRADRNkyFhfCQ0686I" +
                "8Cq9BJEggZOApNXOSuEYP5afG1qBHDoT17VZ1TttgKUNDzLxv+ufx6EKHPKAYUbVQpgyx5aOnGinXzW2" +
                "/IIPBF6DIPfIlAgc6PE1oD/DEXnwYV20W6PS10vFFoL5eiod9Y4kpdal+McEW4x5TaMeZok5uUIQCKcF" +
                "3o7yQyjGJmhLVR99OezTNgQNdJ/N0ToV651Th1YSl6YEQURMOc5uK40DFTz1wHjE1NKyt/Ebzq066zbL" +
                "ENdbMAlqQGyvlVtsQMZXsLwKX7bCwo9dMM/QbVAp2BbjrF9hkRACXuJzKKbh3IeWxbaViw0QQ8M+CPor" +
                "Cs1dFolwv0ffW9yL/BCqTzGLH7sUHheF69pW0AGuq2lEgwccVPh1FoHt3uDhG5SI6haqCmRBew7iHI9A" +
                "2dM11PfHJydHTxjTBUXfHrKZNRUPTusbbU1dYW2MLbfFzmtPQde+huBFXkIHVR483W0YiS72A7KL09fn" +
                "P58efR04Wy4xlmGVWyfuaCASAjCR7uKw/c85jlU5b4rcgj46rL59e/rm5OggBesW7XaMhGgIwXMVHCLo" +
                "nTqEPVwRVRjb3KpxHleUaua5hd1HXBDxnClRZCDhGFPaoFsoBwItApkkosN4nqps6gUALjxi5ZrWmvj9" +
                "3mLn54NOtvu3/4jzZ69On1/hcPXvbw5/WEDP//yYluIqjVdmlB9DoIMohwMtbHWd4gEMlpugSmVxhjLn" +
                "8740YuCDJ7AXOt7bqEWuVTpQ6iIZ0xsG0c6pbLStOcSzWhTTlBUATAuzmHYJCkmZJm2vLs/fPM5NFcdv" +
                "745f/yAYxEgcJ4OGSJw8otOlYjSPsmlHjKESiKlnJE6p1tD1Fu2TZ9H8x5hrKHOu1Vh88a8BCnowHjzH" +
                "kujk2WAoBtYYD28W3i/Hjx9DCyNLELof/PuLwKSlYqs2PP2rQ9hkLYaqCJXUkQNWntoPYJPOqeO+VirM" +
                "3mcluO5Ul9Anjfq5tWe6eBrLcoyN98kzNhKCgnxhIAioeXBGZtaArDD08RzVjencGmgMDNOzIEhjkaTA" +
                "L1EQ8HJTEONv/vHd07AEEzUPG2DhXbIHEdvljz8I0J9TeBCb9NVHfvl7+TIuCeAJnRis5u7w2/AKD7/H" +
                "4punhwf8DBssLtFYLcc1UCqsjC0232NxgwxFLPE0P3yuTNGUuIAGDd4sB8nG0dzva9b/qQoDaDph952a" +
                "W2gsl2h5Q5GvoUSnqi/HQWuYVsa+yap02AxmFqeUUBhNY70AwDAhYP4n3+T6+8kQ/hlldGL0nXh2/uvR" +
                "1+H35duXpxenRwfh8fm7H87enJxeHB3GF+dvTo+eZsF0Y9yiLIQ0hVX4PouLCg3p2MXrJu3S9rCvXRH3" +
                "4KgLye9u6Cwb8+AYOx+6G8FC4ISO4rqN4WvQ7hlw8suCjeJXYJxI5Sbk16F4x2cBv3VpRiFT76XquU/D" +
                "6l5UwokthM7EHwh91Mp28uvRk87TuyRrfPoNRN0lieUfqKIJGqodAyn8HU4VHJZJHGQoPjPfVha6QRJC" +
                "s8QWNOrpdXJxfHL20yXQ08UZlUwwUcF8uslSYdOhmQYNImMtSSc5AdVvQkJBMhLtBLIHd/Ly9Oz7l1di" +
                "D2GHh/2WJ76K0pF4y9Oi16FFXxB76Av7jA+jXsTD3AU8/NDB8yksOJmMsmP1hb5mO87npubhQvwE+9ve" +
                "YNMn8dRIW2qlR+wyetnaEMkU92PHivbeLIfhjOyrINRswxOD/JJJbTAPxtXx1DuLW8HgwvsJcXf7Bepe" +
                "7Z1DTCxWNwdqpC0sGPg7X5lBaXcmpiOR8eQ3XSXojPQ76x6KQV2ncWhvyNW98iNZx312PzPXvf8UhK1o" +
                "TDwdUrH7xAABXTk7n9WynkM98c9OhL2RZaOwUZvhFQPTuVQIPOIxKRQ/7v2HDHFcBQB0JhVgZSF4hFFg" +
                "3BE7tGu8e0YLiJotMqeLI7zpgUQV2dgissjWwLVE8X2M94dMp7qd0FjxQailHn7rzQE+WVfDMAxohwVp" +
                "oiBvxVc4SfxK5H/A/wpxJJ6gsqQYH4GBq9n7Jx9wOJkev8bHPD0e4GORHg8/pPOL908/0Lv7EsBnBoEb" +
                "p6lbD1M3tkRDI+e9N8V9hu4YYehyQbs2RJT23oDS1A4mR3w/jIcy8BUeZJ6rMjTi7gNqyfRX86WrD2kK" +
                "38HFqUzd4uUjVYxiHZqGJyEaYPaLUwmcLeIlA4udTP/Mm2LEBpcjYD3bck/M3b0oBux0XvbYunuFrGji" +
                "aAJy/wQnRXif+P7ulnWnuJ2b4/1BquYBLt/uWMUpFM7yYenWCSwJuK0cwrIiTYuNvfj+2XH8ct/35hJC" +
                "lvfhgbDp1zz9mqZfMsv+A73K8c+oLwAA";
                
    }
}
