/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MotionSequenceItem")]
    public sealed class MotionSequenceItem : IDeserializable<MotionSequenceItem>, IMessage
    {
        // The plan request for this item.
        // It is the planning request for this segment of the sequence, as if it were a solitary motion.
        [DataMember (Name = "req")] public MotionPlanRequest Req { get; set; }
        // To blend between sequence items, the motion may be smoothed using a circular motion.
        // The blend radius of the circle between this and the next command, where 0 means no blending.
        [DataMember (Name = "blend_radius")] public double BlendRadius { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MotionSequenceItem()
        {
            Req = new MotionPlanRequest();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MotionSequenceItem(MotionPlanRequest Req, double BlendRadius)
        {
            this.Req = Req;
            this.BlendRadius = BlendRadius;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MotionSequenceItem(ref Buffer b)
        {
            Req = new MotionPlanRequest(ref b);
            BlendRadius = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceItem(ref b);
        }
        
        MotionSequenceItem IDeserializable<MotionSequenceItem>.RosDeserialize(ref Buffer b)
        {
            return new MotionSequenceItem(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Req.RosSerialize(ref b);
            b.Serialize(BlendRadius);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Req is null) throw new System.NullReferenceException(nameof(Req));
            Req.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Req.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceItem";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "932aef4280f479e42c693b8b285624bf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cbXPbRpL+zqr8h6n1B0kXhXZs79aetvzBseSNU7HltbR5c7lYIDGkEIEAA4CimKv7" +
                "7/c83fMGkFondWvdXe1lq1YmMNPT09Pv3YMH5vLKmlWZVaaxv6xt25l53ZjuqmhN0dnlePTAvOoMfnVu" +
                "XFVUi92xrV0sbdWZei4DW76vZvbYZIAzByizsY01mWnrsuiyZmuWdVfU1Xj0Wv6+BeR3DiiAj7DsZW2m" +
                "pa1yM7XdxtoqABXE2mNZSKGYZbbFMNMu6xpPc7NuiWVmZkUzW5dZE1YDWMxSuE2WF+vWo8yhpQ2Lya4y" +
                "jOK7yt52ZlYvl3hwbDZX3Mojs7RZ1ZrKoYkFx6N5WWfdn57qk4kuMPps9Oyf/N9no9cXfz3Bpm5s0U2W" +
                "7aJ9uEPGz2SvcjTNTQGqzeqqywpgzB3ldl5UhdCOZ5iFE+3qhK4AIUdum0CKet2t1mCIzqya+qbIbcuz" +
                "eps12dJ2tmkdS1izqZvrdpVh4e4q6yL7AFZ7Va/LXEYYIAQgo+/96ARSgDBZhYdc7AL80/F82y7rrFmv" +
                "cvxpx+bV3Mxswz2an+ui6lq/0FQ2z3UamwMA0FnVrewexw/MiDEkQNhg3TTCyJVVFmttMlgBAgSZzXaG" +
                "nDB6V0/r7kJwAUZNNxG8hIW56bptC/CDWdRZqShHIi3r3JakuUggno7NWTa7Mra0TpwAhQOzpgGLy6lh" +
                "eqbAGrsgU8sy8oAsO7sq7I1sE3InOwLiXZMJQfSsQc6uULlQEAAP3LOuaOcFpr6IM95/EMiTBAg39qZ2" +
                "xAcps2qLTeKNycoapyInnXVXOFr9d1Pn6xmE0smqbHVTlKW5KeqSQJTKKZ6HKn6rVVlgu6BPhuGyCA6l" +
                "qjvz8xq8usm2+uwoRVkWHyJMEqULELHGtutSGAlPf7azrqZaImAlxXZ0GZ6n8OPofatU4FSvU5Idk++x" +
                "j3VrhVGhNWQg5XNlZwXpfkzW5BlnQGs4VwgGrgOAfIQ1ibZ7NynyfYsvmnq94g8nDAC2uSrAXEJbDxf/" +
                "rFe2wYarhYcrMyeEFeCul1MMJuRiyRPxIMQyQAOSuksoBpuPzcVV3YDJQca6XDs14tFv7Iov8/EIOD15" +
                "TMATb1UmWQfFvoqkXGa3xXK9NNmyXjvbgtX3UZbMUpb1BlyWCJM5BAu2FoeUg0W8bnYD47IEKnpllpXc" +
                "/jzj4aqw1CuugylboA42Fm5JUBPamhtb1jOoCIpmJRpmNoMIk6pgkLExzx1yN1kJLSviBtQOHx1/+QFv" +
                "L+8CuN0DLvKLF7CG+sepEkJekqspOdB8Wx4VCARex7SuuLEA5zaIlcGJsCBUgJwI3U4lSaBFQ1wLGslq" +
                "AYwPiyUPLqu6cku1WLRUMNWsXMMC4GBFHUMhgvqPxo+O1DjrOsLj+spZHuFvIQXP9MvxI4EF9a6EPizG" +
                "dnx8F0UAsP8mJc5RNMEYNPGTJq0e7UQx6o1Jpw/H3Y/p3mP7gvGGsLXZIjHemdASZIs2kTphvob2p6Dh" +
                "rag0iD6pfQMRhMgcZmZa3x6RY7wO8HzTFx3ipV4SHcAAuq5wLvB7nMSJlPAg6mkBy0Z7In6UM1YKGJ7f" +
                "xpblmKJ1KoZLmYJoc3Bj57Ce9Oe8NQSK2GgDgW5HX9sM5hoGln+CQiigL3DwOsgrOkyDUwY29lzuWU3x" +
                "EJss6rJu7Whha1ANqltI/53o8ScEPFGgQ93zz18KPOeW+mTc1Xa5LqpUJDfBP6nyrIEVtl0GhykTql8V" +
                "Cwj/FyU8BvFNliuwgbzttiv4VAknLCxQFvmkCeK+6RCvq2ImJpxmIZ0vIt33NWZ13cBN5nBhAkIXjnVu" +
                "/avTE7HRdgarAYS2VDCNzcSXf3VqRmu1GZgwenC5qb+gzljQrvrF1dUEsvZ2hQMinll7gjX+TTc3BuwT" +
                "bxHMoTyb4Gd7ZLAIUIB1gnDQbLzddldQtiJDWVNk9OAAGPqhBNQDTjo4SiAT7RPY36r24BViXOO3gK0C" +
                "XO7pCziluZikdr0AAWlz1el2mh16AWoeOrAspg2CqpHYR1ly9OClCJpYajkRCnrbQh/iAHJhYm/v5TTo" +
                "RXwyhtwrCkHJwSnAaWEfVG838pLMM28sNkPFODYhDBVNxMhLRC/MpONUNLRwziGGdqkbxJ8IVPIaJgpO" +
                "I2Ass2s67iCzOJXwL2Fi6dBVbanWFY8x5dCOFzBBovFklIaTgCBiUMxMUyxgHWUmFlqGyZlxu4MJnD9W" +
                "v0dw1sVwZgDS1J0zV7TA23oNVxZ7wD8aJ33iUnm8hEu6uj6m6DkQfYq+FUvojQXsRAfBh+b1hu42/Gsb" +
                "/vXrvRi3GBzdbdOKKpIwm8IL6avUjscIl0CjiRi4Ma3Qeu+W8SlE5bod8XjrRlf/hi81NJNxMTSTN60q" +
                "DGYPrrKb4DlZc3r+UmOi4GtJpJeCfs2xGJcsIdMneT2fDBZ73nWIywBlVpdl0XKf9ZQxBJRC5t/hzFuc" +
                "qeyCznqgwdHIz3/hp5/LbARnfjZMins1cZC57ssyW4C6OXU0ORj87KJd+mQzsHL038UBgzRROcB0U57m" +
                "8x1bJxhqyCzzEwOxrFs4hyY4impgvAaTINZvdVrn9JUPPT4YyKCONqW0UHR7BmMhHHjmeOvkBCbUnpwk" +
                "ofdUBF6zAVyQKSci3yUsdzSa1jXD5Ak39wnt714WDAJATRykQDjwqi5hMbzow6WeNQUVAFNpsjvZu/P8" +
                "oF1gMUV8Gpw6SKQyQC3pvJcwyTKboH7yYWPpDfJ5AztStJS4GXxCLCzuGY0KjSVsWipp3vfxULJcfPCj" +
                "4zg0OufDoQ9bGfwQFpYSGqfYOY6rCwZbQ/0Q0jgAb5ac/QZePTcmmRF9Q8+0KkAFMlwO/a9CCqNIlSCR" +
                "qxJC/ccgxxr4i5XEutEVVqguOSOZStgSeCsMZxG77YxR7wY5onCMoj9UkvxULiEnKHYYa+o2hjqwLVr1" +
                "nrOofyTdg1QHV6AYyTHSlepTWJAR45imgOjG087JJDceeyeH+XB2bL6naaOVU6vjtKjsoqoB0J3PwGMg" +
                "rOWxWKwZYmwIK3cdj1PTVAzit44bST3djZ5tsneXnVPKeTq1xa9Q99gyCKlwEqmRBKSEIliF2RTPAwHN" +
                "hDiSC/FI04+ne0B/U04QxnEYY6g3BKUq/OPMJH56QUgeeYZPHikF7ken7Noe6pZ33hlSS4qTVzVABnLn" +
                "K+QIXJbbBVws4T36WnmNo2V6AEql3nilDaqsZ92a6YG5iQsqL6tPBuojyGR+mOlAL6L0/tstPBEJomhD" +
                "xC0WHyn4TTpnYbuoApjvCInEa+gl0VNmdgVfYWxeUhpu4eCWLC5IOAqD4fQFiw2V+fu705ei1p7QjB8i" +
                "YkPKYptt6FZrGhs+ur4kE5PRknAhxU4JiT9NASg6l0Ldey9uJUd4aJDpG+ShqY+y2TU33MPh/zXZ/Wqy" +
                "DZMMV79Zk/nh/5c02V2KTN1QTm8H2YhLz8IYFdh5Z9AGpokD+Hfw7nshE14qve4rdAx47wsexbYHzRIq" +
                "eZt6J/XQDuLL0cinZ5JYcPS3NSY0FXWAj9jua59x6b1RMrR7w8BfFVRvL/z1S0ScpPhYIBj+tbm3YyRH" +
                "hZ159dtGR7K/pWlTX+MkwcyMzluGSAwTqJCRnpbkkuQgx+EY3ZD42427rw2qeOw7OxyIHlLc3zGMO9O5" +
                "1E3cI73L37ZLARZ/amRwP1nrO2JS3bI1g8ehABTCOtFnyEIWtz6A0VwqqMUI2Oem+G9fihJS9srYSNOv" +
                "WVW9ypCtFEox8xrrqwMsRg/UQ09jbg6TBR8gFQN4IlA8MW5WQKJWpzH1GBkfLWbAGWBex0XaD/bB83G3" +
                "hiVhG0kljAuw2NonlAL1aWgX8klmIxbSw3o+i8BcRZLF7+o1VDKq1NtQbvkiIKZoSNofydV8m9TGkwnR" +
                "iAiwieZW6PLFKrtiJeES/DCWeIcpX5yGuI6hT0QR1tTdkjDCVgDaZd2g3dieAYdaU4GzEg44828oS4os" +
                "OCiuyuTXqOyM5hg1XK7WoKgjmWNXk3AL8/gA0yd0klJuDNZj1RfEIHYTt4Q/lI1F2jw4MIPDQK5qbq6r" +
                "ehPiBTf+fsRyjzg+d46BpA9zoU5I9ngvX8Rmf9EFTO926mh4KAwksHCAr7H4K+SonLzGWrQ/ahQSlC+Y" +
                "3phmcL1rR6AgQPp3wsB5oV0puhndwyUhEEws5PssmlO6Unjd9e+82Oo0lDRVOVBY9tVw0mSfJ8AF2pXy" +
                "sMyMeZMlELwBsUTpKJ4y6q1/xZAxDhsmatvee7KWttC8tu1VHyqfYOxSX+yFw3cRxFeUD98YwPwaKivO" +
                "Q0jbDMzUZVhlmI9a1KGIvVM4sTyXs4CC5RJHKW5sNJKNyEp3bJLvInbPc0Z/CWMo1UNBUfKJkhBPBoFH" +
                "0SWybuEZ21v4DEQfmT01qaJzxqPpFm7d89PTZ4+4zDvRq72V5k3NQBNueHVTNHUlrTVMGkFJoNyM4leD" +
                "soyKgqSDO8izQkiK7fmRrvTu7PX5d2fPvpQ9rVZUVYxpPTe7KNjpVkHaBQwf26uvU+gkv0+cQtzk27dn" +
                "b06fPXZ6OK65fzlZBf0yduM43x21FEzQYoNAy52bD2yk+wUjSjvvNGhh0AyFhlYO0gqk9RojKlSkK0HJ" +
                "XFEU2jwhgufaVqJlEcDETzqjfqDrOvmULvXH1QrU4+/+z5x/9c3Zi0tWJH//ZPcf6fPiH5c/RG9KVnAu" +
                "Zs/pMmgyZi8Y1MA3aAedAF290Ix6iCc1tQtWYQK951pc25CvTVc4kSc6P+ZWpSAnHAOlVZl86vU9oHiA" +
                "+TRFxZlZSah8c3H+5iEbhFyW5cfnr79lYok9k+Z54GJo2iADSaWOutpTJWaS1LR7m4K+OfEdmJXdOXUR" +
                "JYny6/oaXsu1PTF/+I8DUvjg5OAF/ZvTrw6OzUGD9lE8ueq61cnDhwhFshLU7g7+8w+6RbbtSMenpHgq" +
                "pxz19JyPw8NJqED/segOMIktmBCEa2tdmXpeQlrRP4Fwx/el7mFY1jmUiL7yePqV8oYA4a4o+m5lTY6Q" +
                "uVwbo0uVSRGcqTO3WUnzC5gTEwggz0gCPBuS4OSP//7npzqC1lcLrRi3i/GBW+nib9+irAEvgbWNcE69" +
                "hS9+Kb/2IxS2LGUONov2yZ/0CYtJJ+aPT588lp8Y3XAAnOh640bA8qNVNB88ppPCjfgFfF1M36Jbal3y" +
                "vRRXu3p14BkarP3pcrh3uQyfxR4ZaTFpV2S2YzPbwscW1w0cZ43LQvlwB5zhKzfgLJ99gp8z9Y4AgFHt" +
                "07CLMKoH/egY/0NSgP0UfzZfnf8AY6b/vnj79dm7MxgY/fnix29fvTk9eweF7h6cvzl79tQLvFdRYmuI" +
                "kxulvprXCih8IL5wFds4NGbO44jQfIP6PtFPJyTDTjQhKM2XLDT6tliOJbluvbI6iHMO1MRJVdQFh9i4" +
                "oKphxA/H5kfN8f6U4kwiS+RkqwVcRt8sPlBDjJ/C/kD0caTt5Af4JfHXj4HW/PUTbXmCktLfYSX5QR47" +
                "NSf+uroXFKjiCaVGbeyaWX0zu4t3lIM8Hgp38u756au/X9BPStb0hywwecDa+6NUUdaRVIR0X3gnUTL0" +
                "bqmfDHql2E0Y2i56cCdfn73669eX5pCw3Y+juCct7CYUj3u66sVZXhbMIWXhSNejqvPr6O7cOvojWeeu" +
                "VdiO0bsI4EOU/WvCamtWwL/C/OjtD2WS1QB3BUHbtFF4iTwkNOV8Rp3atHfsah+fO6J6IR0QM7DUYPP0" +
                "SqOk7gyOhMHAe8qEMRjQKLTZqU/RKx2mwuTA6CToey1Bk+BJxhMZee14CaW5JFWbjLu/PQKZkOzrZajS" +
                "KjpSI740Fnf8kdTsfdgiBprBAiXYMrqkpkCwrVKIhrVqAW/iL4mqdS3N0s0pzdehQQfbZB0Mjk/7/sOI" +
                "i1w6AFJ0cLC4QJLK8zN8KAYv0LWyCjZ76C7lWJ10b9TyG9lHNb8zOH0BL+1cfP9EUbW3E0kO3hPCEqvv" +
                "LxBrARVSp0F/TAqEzEF2az5nWvBzM/sV/5ebZ0bC7MycPAOn2/n7Rx+YaQw/v+TPWfj5mD/z8PPJh1CL" +
                "eP/0gzz7dDT4SHbvs0G+a2/ZbDDHc5zeO/kfQz0oHOmJSa6vqIKJ7S6uuz8I5fvj5LICfvQuKnzgWdX9" +
                "0drW8CFk1JO11L7ZW/brMkXhnNOQMelfdgh1UfbGoKrRDgqcoi8G2xxj76M9nRjtbisGW8Tiw962dps0" +
                "8rXPTMAhmDA9xF695t5StOHS0D/q8/daN73qQiFN7hx5mqc3m3wKJ1zncpe+pKbur+qEhL9c8vAVY218" +
                "UZaPSAaB6F12euuOojfOn09/6Hk0yL3RiaHuT/gOyV0NjHvjb8Lj3vB7ObMBTXhu4dceOx/qWVNNX0j+" +
                "25dNJDrzJI+6xhtCee3m+tt89L3fhyVQy0E6DoXqmZ1Mwf6b47j858k7JJNu7IfgTAz7mIYj9zwX6JLo" +
                "dEWMeD8q1nTiYZhD5Itq9tAhs4qDRUDq20E1+6GdoykHmxeo7qjn8KttanepFn5BGztJ490tReJeTnyX" +
                "we8WVnevqecGhBOJu93t1NBwVzudpWg4JKi4Vczr7auHas5oPmeRkJcLQulRehyOos7OGqQFnW2ok3Eb" +
                "dhomaWlMuOPWioJAMp/cqkt6BNwVozsxNyNfl/hOR8ZBE71++r+Owe5Hp/TJkqSAMk9TnOmTU6WOKwS3" +
                "d9ajdh1mfzBsKBa47I/LmNqtq6PfWr5yh+wqZ2nP7CDqZaoQbsBvrne9igUmCRsCtGNX8QrViuBQsMyS" +
                "hyJ7SHKFO24LnLh8cWB3i/1a2t2bckt/dENJ4e1eeGWvDf1dCqnfMvRxpeSqOr1ZLnOR6Kt4OMJbsfFo" +
                "QLY74vP/rgL0XYCSLPlCwi5jm4aKw9uwpPIZrwBP5WqyndxOOHESBu+O2H50xK/DEf+a2myf0+abgZIt" +
                "xzul0tyJR3SJomun/cF50c60QqmG52insyTcIBQBkPHSwthL4WUBsHS5wBfT4hmScKysakkJ/Lhg2ch/" +
                "uYRt1gT82iEnKibix9UICjExhq6FJ6NQhPpVPdXGadXEcTrRIPQ3OPeTJGPk6SMA3pxfArh2ii2BAS+i" +
                "JveesVthkTb5WonHPPQ4e9LxewmNgkXC2ENN3AJ//Sd215IWNwWqxjGbrFQBX++6RaKnD6X3CjRAOWXr" +
                "+mGP/PcKhPdbFJnMat2I0hwHye+lXuUYxY7FL3d4HmGwQjJCwqOjql6J+85KotVTgH/xsaa/Jy82xje9" +
                "9JbmRTD0mVdQpe72cjghtUWeSv4Oc/9bGkGLHeulDa297TEgF+6mbPCqWtEaL32fgpLbc2ZXb7LG9U74" +
                "Mw0oK9NnQxZLOEt7vVY4HHCP9MdosmfJD3dI4WLQVS+3HnXMUx0g1wL0qk4l+kz8DMUoWdh9TIRxi1lt" +
                "QaNCsgKCiF73d8ealWh7JveaJ8LXKBn4D0IQ4Yks2jvPcHG9L4nKu5FFfCFVDwieklsw3hMd2C8AiMbf" +
                "kV1Ug/Z0jffccQjjQkVZb56yebvrHRJ1FmgcL6Z6YggwMV2qD5H4xGy5AtesrTboJN03uzLHqoGWfmGE" +
                "pOwnbeXtGnS8m93cFRLPbmes5LO0vriK/ERvYkfilN8GLOZlgt0zpl1iC8wD2lnGS6q+Y2zXeZBV9rZf" +
                "OR2j9PafzQHMRJY4VjSTg3QNVuK9Q23xyYRbhe56qDPagX2oO/KxdwfOMiqnklihaZUvWDiXUks2sp66" +
                "FslVOXdDl4lreeU0sdOXULkgiN9Zsrrs3ivowBj+0vN8rwLwTK0iJCH5sOvdGwnvtEWcVbYz6b4hsfmG" +
                "N/eEYxSi70h95CwuWksUH/WDQXq2pa4koLXtkYfIQg1K52wy8cbOrbwf/Nvi4WNdIYUOvNhCpSoUsBNl" +
                "EW4lyo7Zh+Q268xgcjA7esB9+ipYkczBdAoczUfSTm4bPGbn6aNjwU8vZD8KXb7dzvknpjkffGwE4yYy" +
                "TjWXfD/MzRIr2CLCwSZ5C7wezBCJLxkB9beo4B7ssfPeOOzwkGcuOqWuR6Su0BurkFmrHhiX1Bdwhk3Z" +
                "LmE67oXRVfjkjNjIQDXndAzII31j/pswPVLJ9JRWKbqpsq1DF/VAYLyH4lMcwfn5AQWKxyjl48+XxyhN" +
                "s1zhauBnby7O36HkPngQK/LuwQ+h/8EpTDmnsPa/kH9/pynRgi4feFXuv+UyvGljhB/9BzoGoaYAEHN0" +
                "LyHK3s95+RhFL3yBr3qfCZNWL0mWJ5cSYw1k8Km0flr6vwADyCiWXFEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
