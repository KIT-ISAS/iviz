/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MotionSequenceRequest : IDeserializable<MotionSequenceRequest>, IMessage
    {
        // List of motion planning request with a blend_radius for each.
        // In the response of the planner all of these will be executable as one sequence.
        [DataMember (Name = "items")] public MotionSequenceItem[] Items;
    
        /// Constructor for empty message.
        public MotionSequenceRequest()
        {
            Items = System.Array.Empty<MotionSequenceItem>();
        }
        
        /// Explicit constructor.
        public MotionSequenceRequest(MotionSequenceItem[] Items)
        {
            this.Items = Items;
        }
        
        /// Constructor with buffer.
        internal MotionSequenceRequest(ref Buffer b)
        {
            Items = b.DeserializeArray<MotionSequenceItem>();
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new MotionSequenceItem(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MotionSequenceRequest(ref b);
        
        MotionSequenceRequest IDeserializable<MotionSequenceRequest>.RosDeserialize(ref Buffer b) => new MotionSequenceRequest(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Items);
        }
        
        public void RosValidate()
        {
            if (Items is null) throw new System.NullReferenceException(nameof(Items));
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is null) throw new System.NullReferenceException($"{nameof(Items)}[{i}]");
                Items[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + BuiltIns.GetArraySize(Items);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MotionSequenceRequest";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c89266756409bea218f39a7f05ef21a1";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1cW3PbRpZ+569AjR8kTRRasT1Ts5ryg2MpE6diy2N5cnO5WCAJUohINAOAopit/e/7" +
                "fef06W6A1DqpHWl3azZTNTKA7tPd535rPsq+LZs2c7Ns6drSVdlqkVdVWc2zuvhlXeDTpmyvsjwbL4pq" +
                "OqrzabluspmrsyKfXA0Hj7JXVdZeFRjerFzVFATFZ4FT1Fm+WPhX+LYp8TQusuK2mKzbHDCzvMlcVWQN" +
                "V6smxXDwWvZx6Z9ftcXyw8esxJ9mMHj+T/5v8Pryb6c4+U1RtqNlM28e766OI7735wk44fnbq7KRfQkS" +
                "2gxP4dwp/sLYppgvi0pwzYF24mOioJwBVLYpaiAka9yibPN660liKHkLyO88UAAfcGNO6QKctpuiqAJQ" +
                "RdixLOQJu8y3RH2zdA5vp9m64S7zbFLWk/Uir8Nqel6F6+ntt8yhIJktJqfKMYrfquK2zSZuucSL42xz" +
                "xaOcZMsir5qs8tvEgsPBbOHy9s/POgz1cJRNkCgHFbrUNyVQNnFVm5fYLo8zLWZlVQriSMA8kLN1CVIB" +
                "IvC5x4Nbt6s1uKHNVrW7KacFDvcoe5vX+bJoi1plhwM3rr5uVjkWbq/ytiMzzZVbL6YyIsOGAGTwvY1O" +
                "IAUIo1V4ycUuwTwtidu0eVtk69UUf5ph9mqWTYqaZ8x+dmXVNrbQWA7PdepiCgDYzso1cnrQHjvjjsH+" +
                "wgPruhYurgrlL4h1HKwAAYKcVrQZ2WDwzo1deyl7wY7qdiT7Ev7loV3TlNQEc5cvdMsRSUs3LRbEuYgf" +
                "3g6zc+idrFgUXpYAhQPzugZ/C9UwPVdgdTEnR8sy8oL8Orkqixs5JoROToSNt3UuCFFaA51tqUKhIAAe" +
                "e8/bspmVmPoyzoBuIuRRAoQHe+M88oHKvNrikPgCXehAFaF0DqUKOsi/azddTyCRiQZWTXlTugWBKJbT" +
                "fR6q7K1WixLHBX6oZmUREKVybfbzmqo73+q7o3TLsnh/w0RRuoBX6euFMBLe/lxMWkedRMCKiu3gfXif" +
                "wo+j961SgVNNoaQ2h0zmoJUKYVSoDBlI+VwVk5J4PyZrksY5ttWfa6YFAKYDrMlt+2+jcrpv8Xnt1is+" +
                "eGEAsM1VCeYS3Bpc/NOtihoHruYGV2aOCCvAXS/HGEzI5ZIUMRBiFqD+iN0lFEMxHWaXV64GkwONbrH2" +
                "asS2XxcrfpwOB9jT0ycEPDKTMspbaPVVROUyvy2X62WWL93aGxasvg+zZJbFwm3AZYkwZYdgwaYAkaZg" +
                "EVPMfmBclkBFr0zyBY8/y0lcFRa34jqYssXWwcbCLcnWBLfZTbFwE6gIimYlGmYygQgTq2CQYZa98Ju7" +
                "yRfQsiJu2NrhyfEXH/H1/V0At3vARX4xAaupf7wqIeQluZqSA823JamAILov4NnypgA4f0CsDE6EAaEC" +
                "5ETodipJAi1r7rWkhazm2PFhuSTh8qpdbKkWy4YKppos1rAAIKyoYyhEYP9keHKkllnXER7XT97yCH8L" +
                "KkjTL4YnAgvqXRF9WA6L4fFdGAHA7pcUOUfR/mLQyCaNGiXtSHfUGZNO7497CLu9x/KZ5YakNfk8sdy5" +
                "IBI4iwaRCmG2huqnlOGr6DPIPVF9A/mDvBzCu3W3R2QXUwDGNF254bbUP6LrF0C7CkSBx5M4zxSNpRuX" +
                "MGs0JuJBeUulgOHzbYrFYki5OhOrpRzBbXNwXcxgOunJmSnEFnHQGtLcDL4ucthqWFf+CdqghLIA1XWQ" +
                "aTlMO1Yf3rO48ZnuQwyy6ErXFIN54YA16G3B/HeixJ8S8EiB9hXPP38pMJwtdT+s1bRTXVJxqO5SNc1r" +
                "2N+izeEq5YLyq3IOsf98AV9BvJLlCjwgX9vtCt5UwgbzAvsVyaTx4aHpB6+rciLGmwYhnS/C3PUyJs7V" +
                "8I45XDiA0IVdvTf/6uxUrDOjJqgorATVUhe5uPCvzrLBWq0FJgwevd+4z6kt5rSotrg6mdhscbsCdbjP" +
                "vDnFGn/Uww0B+9RsQXYo70Z4bI4yLIItwC5BMmgw3m7bK6hZEaC8LiWKA2BohgWgHnDSwVECmds+heWt" +
                "nIFXiHGN3wK2CnB5ps/hjk7FGDXrORBIa6vuttfpUApQ8NB+i3JcI5YaiGWUJQePvhIpExstFKGUNw00" +
                "IQgwFQ42Sy/UEP/hfrhxrxQYa8EXAKlwCCq2G/lGzpnVBU5CjTjMQugpOojRlghdmEl/qaxp2LwfDL3i" +
                "asSciE+mDpYJviJgLPNr+usM3+lLwq2EZaUfVzULNap4jSmHxXAOyyO6TkZpCAkIIgPlJKvLOYyizMRC" +
                "yzA5z/zhYPlmT9TdkT3rYiAYgNSu9VaKhnfr1vBgcQb8o/aiJ56U7UtYpHXumHLnQXQR+lYMoJkJWIgW" +
                "Qg+da/btNvxrG/716wPYtBgR3WnKyiriLx/D8+hq0pY0hBugEUQM1phHaMyjZUwKIbluBqStq3Xxb/hR" +
                "wzEZF8Mx+dKoqmC64Cq/Cd5SkZ1dfKVxUPCvJLpLQb/mWIxLlpDpo6mbjXqLvWhbxGKAMnGLRdnwnG7M" +
                "uAHqILdvIHgDgsop6KAHHBwNbP5Lm34hsxGQ2WxYEv9p5CFz3a8W+RzYnVI7k33BzD7CpR82AR9Hn12c" +
                "LogS1QIsNoVpNtsxcbJDDZNlfmIalq6BQ5gF51BNi+kuCVztqGM3pX98aPvBQAZytCaLAipuz2AsBILn" +
                "nrVOT2E5i9PTJNwei7RrBoALMsfEzbcJyx0Nxs4xNB7xcPdmdvcyYIKpPIiAsN+VW8BQmNDDh57UJUWf" +
                "iTM5mhzce3vQKzCUIjs1SA78qABQP3qPJUxi2tI7xod1QQ+Q72uYj7KhuE3gB2JhccloS2gjYcpSMTN/" +
                "x6DkU3G6j47j0OiN94c+bmTwYxhWimecUsxAqzbYaY3tQwzjAbxZcvYbuPE8mKRC9Au90aoEFshtU2h+" +
                "lVDYQuoDCVUVEeozBiHWSF+MI9aN7q9C9dkYyUvCisBJYfyKYG1njDo1zOQaGUV5qBjZVC4hFBTzizX1" +
                "GH0F2CAjLaosj8pH8jvIbXAFyz6LB9XFsGxGzGKa86HrTgsnk/x4nJ0cZvHrMPueRo32Te2NV6FyisoB" +
                "oKdPz1EgrOWx2KoJgmpIKk8dyal5KUbtW8+NxJ6eRmmbnN2n4xRzhqem/BW6Hkdm3lzgJFIjGUcJP7AK" +
                "0yfGA2GbCXIk+WGbpu9Ox4BuplAQZrEfV6gTBI0q/OMNJB5NEJJXxvDJK8XAQyiUXbODU70zH0htKMiu" +
                "OoDc44kruAgsNi3m8KyE8ehiTR3oymQANIrbmLoGStaTds1kwCyL6ykjqysG1COqZDaYyT+TT3r8zRYO" +
                "iERNWi0B/4lrFNwlnTMv2ij/zG6EtOE1lJIoqWxyBS9hmH1FUbiFU7tgHUHiT5gKryxYV6iyf7w7+0p0" +
                "2lMa8EOEaEhQbPMNXWlNWsMv14/kYKnOxBAh3Z0iEn/qElB0LiW68128SY4waBDoG2SdqYzyyTUP3NnD" +
                "/6uxh1VjG2YVrn6zGrPh/5fU2F1aTB1QTm966Yf3xsIYFdh5Z9AGdokD+Lf37XtBEz4qvh4mXAy73hMw" +
                "ilUPaiVU7DZuJ9fQ9GLKwcCSMUn8N/j7GhPqigrAorSHOWRceF9YDL1eM8xX1dQ5CJ9+ibsmHj4Z+dm/" +
                "Ng9EQOEkfyzTuk10HrvnGdfuGjQEDzMWbxgTMS6gHkYOWvJIkmscBgL6IfHZj3uY06lM7KEaSKHkiYc7" +
                "hj1nzpb6iAekO/nbjijA4qOGAg+Rl74j/vQ6u/c21HdCBOfbKmblrYUrmi0FqhjsWgKK/7ZKk+CxU6VG" +
                "Fn7NoulVjpSkoIm51Vg+3dmb+uNpeM1hsuAjpFwAT+SI5OJZBSRKcRo+D5HZ0VoFrD/zNz6ofrQPnoXY" +
                "GoSEYySFLi7AWmoXUQrUEs0+wJMkRqyTh/UsYcC0RJKnb91aGA8LWzXl87Ax3YYk9pFBnW6T0ncyIVoN" +
                "ATbSNAp9vFhE111JcATHixXcfl4X1BBfMfSA6IY1RbckjHAUgPbZNSg1tl7AfdaU32QBd5t5NlQdRRA8" +
                "FF9EsjWqYkL7ixItV6tRs5H0sK86+IVJPsC03E1SqY2ReSzqAhnc3cgvYUTZFMiNB4+lRwykpWbZdeU2" +
                "ITrw4x9CJndl8YV3AyRHOBXUhKSO+fQiM/trKuB4f0yPwEPhHoEF6r3G2q+Qi/LCGuvMRmeUCpQpmMkY" +
                "59oeJdgJ0qN/R4yR59pxomfRI7wnBIKJRXrLlnltK0XVXW/OZFanoVypmoGSsq9Ekyb1DAGX6EOahmUm" +
                "TJEsscEbIEs0ju5TRr21T4wO47B+NrbpfCdfaXvM66K56kLlG4xd6oe9cPgtgviSwmFFf+bRUDvxXkHa" +
                "QpCNfSZVhlmMok5EbIoCxaZToQW0K5c4SvfGHiI5iKx0xyH5Le7uxZSxXsIYivVQL5S8oWS9k0HgUXSA" +
                "rBv4wcUtPAVuHxk8NaaicIaD8RZ+3Iuzs+cnXOadKNXOSrPaMayE013dlLWrpG2G+SFoCJSSUd6qUXhR" +
                "UZC0bwthVghJIX16pCu9O3998d358y/kTKsV9RQjWONmH/N6xSqb9uHBp85qxQidZOcEFeIh3749f3P2" +
                "/IlXwnHN/cvJKuiFKTae8z2ppSqC9hmEVZ5uFsZIZwtGLIpZqyEKQ2RoM7RpEFdArWmMqE2RmQQmp7pF" +
                "wc1TbvBCW0a09gGYeKQDagN9R8n9+dCfViqDR7/7v+ziy2/OX75nwfH3T/b/ETkv/+sahyhNyf7NxOB5" +
                "RQY1xkQFQxh4BU2vyt+6uabNQ+ioKVzwCbPkHafiugh52XSFU3mj82MOVUpuwi7QWFU2HZuyBxQDOB2n" +
                "W/EGVnIn31xevHnMzh+fUPnxxetvmUNiJ2T2IrAw1GwQgKQWR0VtWIlJIzXqZlDQECdeA7OvO0QXOZKA" +
                "3rlr+CvXxWn2h38/IIYPTg9e0rM5+/LgODuo0RSKN1dtuzp9/BjhR74AttuD//iDHpH9ONLHKdmcymtG" +
                "pZ73bkicBAv0HMv2AJPYWwkpuC4KX4WeLSCq6I1AiGPdpnv4lcUMRaLVFs++VN4QIDwV5d6vrHkQMpfv" +
                "T/RZMalxM0vmDyvpfAFzmgUEyDuiAO/6KDj907/95ZmOoOnVUirG7e74wK90+fdvUb6Ai8AaRqBTZ+HL" +
                "XxZf2wiFLUtlB5t58/TP+oYVo9PsT8+ePpFHjK45AO6z2/gRMPvoAZ32XtND4UFsASt+6Ve0Qa0X/C7l" +
                "09atDoyhwdr3lau9y1uIzS/SO9KsyGnH2WQL11qcNrBbkflsk0U5YAsrz4CtLMsED2dsLgCAUeHTpIsk" +
                "quN8coz/IQXAXom/ZF9e/AAzpv++fPv1+btzmBZ9fPnjt6/enJ2/gyr3Ly7enD9/ZtJu+kmsDPfkR6mX" +
                "ZioB1Q2EFb4mG4fG9HgcEbpqUL7n9tMJybBTTfxJSyVLidbsyrFE161pqoM450CNm9Q9fUyIg8tWNXr4" +
                "4Tj7UXO5P6V7JpIlYCqqOZxF6//u6SCGTeF8QPow4nb0AzyS+PRjwDWffqIVT7ak+Pe7kjwgyU61ib++" +
                "uAXtqfuERqMq9i2q1p/uwxzlINuHwh29e3H26h+X9JCSNY3IApME1r4exYqyjqQfpLnC3EPJxPulfsrQ" +
                "BMUewdBV0YE7+vr81d++fp8dErZ/OIpn0tJtgvF4pqtOeGWykB1SFo50Peo5W0dP59fRh2Sdu1Zht0Wn" +
                "t9+Ck/1rwmRrMsA+YX708/syyay/v1WgzdcosEQeEpxyPoNN7cY79jWOzzxSTUh7yAws1Ts8/dEoqTuD" +
                "I2I48H5U3G4QIMFnvVOEojPaz30Jtege6HctMhPbSXITaXftZgnFtyQlm4x7qANiK5ba66Sk0iI5ciFW" +
                "/IrH/UQK9v5NEENLMzzJVhlOUkEgulbhQw9aNYcH8ddEw/r+ZOnOlE7q0HmDM7LMBWen+fBxwDXeewBS" +
                "U/CwuECSuLMZFnvB8/OtqbKbPTiXUqtOeiBU2TH2oMyOBS8vbEo7ET881X0WtyPJAz7IbiUu31v51coo" +
                "JE3j+xj/hyRBfpt9xvTfZ9nkV/zfNHueSUSdZ6fPweDF7MPJR2YUw+MXfJyExyd8nIbHpx9DqeHDs4/y" +
                "7r4Q8IkcXi+vtbcY1ptijKZ3R/6H9m0aRnpckvsnqlFi+4pvzw+C+OE4uW2Ah85Ng4+kkuuO1jaFjyFn" +
                "nqylpkwvKzIP4f3QkBbp3lYIpU72uqBo0fRqlqIjeqcc4uiDPZ0VzW5rBfu94svOsXabLqZrSz/A9o+Y" +
                "A2LjXf1ASdh45+fuRv3FHddOkxtDhvD0XpIlacJlLH9lS2rkdtEm5PPlioZVgLWLRZk97jGIQueq0ltP" +
                "h844I0536EW0vZ3RiU3uTvgO6VuNfjvjb8Lr7vD7J1gPI5qh0Yc9Jj3UqsaaoJD0tpVEJAQzfEcVY2ZP" +
                "Pvu5dhGPDvaHsATqNMi2ofA8KUZjMP7mOC7/WfIN6aKb4mPwG/odSf2Re94LdMlj+gJFvNoU6zWREtkh" +
                "MkKO3XBInIKqiDqtq1PzG9oAmrJv9hKVG/UTfi1q5y/DwgtoYkPoUb9Kcv/k3uXtO8XU30fqGP1AjnjU" +
                "3Z4LDWi1W1mqgX1sigfFtN2+QqemhGYzVv94NSDUFKVh4Siq6rxG1s+bBJeM27BhMEk5Y8IdF04UBBL1" +
                "ZFVd0jbgbwfdufNsYDWH73RkHDTSa6P/+7jrAdiri5SY4ckNoSDo0zNFjS/vNncWmnYdY6MKm4IFLtvc" +
                "cqZtXXX0W+tSnsK+JJb2vfaCWqYBYfp/cyHrVawcSXgQoB37UlYoQwQngvWTaSidhxxWuJs2B7nlNwJ2" +
                "j9gtkt19KL/0Jw+UVtTun1H2Gs7fo4q6zT+fVke+VtOZ5bMSiaaKlBHGii1EPZzdFX7/N1WfdfJJIuRz" +
                "Ca+yoq6pMsx0JfXMeGl3LJeJi9HtiBNHYfDuiO0nR/zaH/GvqMf2uWmhBh/OG2+BSncmXtENir6cNvhO" +
                "y2aiRUe1N0c7nSLh2p9wv4yXHsRObi4PgKVrBf6XlsSQXWOxVAtFYMY5i0H2KyNskybg135zolzi/rga" +
                "QSH2xdC1MGSUiFCVcmPtfFYdHKdzG4T+BkQ/TRJChh8B8ObiPYBr29cSO+DV0eSaMk4r/NEkvyxiOw9N" +
                "yoY6/rxBrWCRCTaoiTdgN3dieyxxcVOiEBzTxIoVMPWuMyQa+lB6qYADFEm2vqH1yH5eQBi/QekoW61r" +
                "UZfDIPadnKqQUSxY/KEN4xFGJ0QjxDs6p+qM2G+iRH2eAvyrRZZ2rV2si/WxdJbmHS40ilfQo/6+caCQ" +
                "WiHDkt067v70RVBhx3rlQitqe0zHpb/eGpwpZD+ZarfWA0W3cWbrNnnt2yGMpmHLyvR5n8USztLerRWI" +
                "A+6RlhdN6iz5OxtSkei1xcttRR3zTAdIX79etKlEmYmHoTtKFva//cFYJVttgaNScgCyEb2d78maL9C3" +
                "TO7NngpfoxZgv9/ADY9k0Q49w1XzriQq70YWsfKoEgg+kl8w3u/sGS8AiGbfo11Ug/ZoDfdcUgjjQp1Y" +
                "b4yy+7rtEIk6CziOF0oNGQJM7JbqQ6Q2MVtur9XrQntukoaaXZljokALurBAUs+TvvBmDTzezW7+Coix" +
                "2znr8yyYz68iP9GV2JE45bcei5lMsCEma5Y4AvN9xSTn5VJrAtv1HGSVvR1VXscovu1XbgAzkSWOFc3k" +
                "IV2DlXhlULt2cuFWwbsSdUI7sG/rHn1sx4GbjJKoZFJoV+UHJ7wzqbUYWU/9iuSim79Zy9S0fPKa2OtL" +
                "qFwgxE6WrC6nNwUdGMMuK8/2KgBjahUhCcP7netmJMxji3tW2c6loYbI5hfeuxOOUYjWYXriLS4aRnQ/" +
                "6gED9WwzXUkcWzRHBpFFGBTE2Tpixs6vvB/82/LxE10hhY59sStKVShgJ8oi3CmUE7O1yB/Wm8GEMDt6" +
                "wP9SVbAiuYfpFTj6iaQxvKjxmp2kJ8eyP71IfRK6dtsd+iemedr7bRCMG2nmP8i5zRIr2CC2wSF5e9v1" +
                "ZojELxj7dI9ohYRdO2/GYYeHjLnokfrOD1eh11UhswjdMy6pL+ANm7JdwnQ8C+Oq8AsxYiMD1rzT0UOP" +
                "tILZT7h0UCXTU1yl202VrQtd0T2BMQ/FMhvB+fkBhYgnqNHjzxfHqDmzLOGL2+dvLi/eoZbeexFL7f7F" +
                "D6GxwStMoVNY+1/Gub/TkAgC+Gx63H56pX9VJhNmtJ/U6AWZAkBt0f3sPw1O9v7wlrGe3NUCR3V+z0ta" +
                "tyQvntwnjLWO3m+adTLQ/wm6JtrHJFIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
