/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupSequenceResult")]
    public sealed class MoveGroupSequenceResult : IDeserializable<MoveGroupSequenceResult>, IResult<MoveGroupSequenceActionResult>
    {
        // Response comprising information on all sections of the sequence
        [DataMember (Name = "response")] public MotionSequenceResponse Response { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupSequenceResult()
        {
            Response = new MotionSequenceResponse();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupSequenceResult(MotionSequenceResponse Response)
        {
            this.Response = Response;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupSequenceResult(ref Buffer b)
        {
            Response = new MotionSequenceResponse(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceResult(ref b);
        }
        
        MoveGroupSequenceResult IDeserializable<MoveGroupSequenceResult>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupSequenceResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Response.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Response is null) throw new System.NullReferenceException(nameof(Response));
            Response.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Response.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupSequenceResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "039cee462ada3f0feb5f4e2e12baefae";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+07bW/bRprfBfg/DNbA2d7KSmKn2dYHfVBsOVFrS66kZJsGATEiRxLXFEedoSyrh/vv" +
                "+7zMDElZTrrYsw8HXFo4JvnMM8/720wajWtdpDofqd9XKo/VUNmlzq0Sxv3SaLT/h/80rkfvzsRC36m0" +
                "iBZ2Zl/sJqGxLzq5UMZoI2KdIEnTTMVFms/Eei4LsVY5/DA6n+0BF3eqV3QR+BxgLa+LcN1eYw9QjedK" +
                "TFdZJmwhDeGAXwol9FQU8MnoiS4EIMUHAvFfrCNrrzFEmBGt8i8jAi13KIz8B5CoTQokFHOHb5nJPFdG" +
                "LI1OVrFKxBRYUvcqXiHfDvHYL918/uJWJFEVX7mLXOhVzgSmCyVS2EXrW/gBclosMwUE4rYLEivh2mtM" +
                "My2LN68ZM7Af4dK959FuXTWNNC9OT8SdzJgj+GwkKGai5vIu1WbPAYw+nJ93R6P2K//istO7+jDstn/E" +
                "P7iWX99cdfr9Xv9dhN+7F+3jsKDX/9i56l1E14Nxb9CPELB9fOK/Vt5GDrIz7l5Ebz9F3f7H3nDQv+72" +
                "x9H5+07/Xbd9fOrXnQ/64+HgKmz32n/40O+8vepG40HU+eVDb9iNRt3+aDCMAG2nffy9Bxv3rmGXwYdx" +
                "+/hN4GHY7V7fjBHd31gqXk/iP8RtmquFLNLYgguA3dmCrdsGSY07w3EEP8dd4CQ6H1xd9UbAG4ji5S6Y" +
                "j73BFfw9im464/cA3h+Nh51efzyCBa9Kwb4bdK628Z3UPn4N0WkNsvLNr0JNvS5388p6Nxx8uIn6nWuQ" +
                "+avvH3zdQgYwb7ZhhoO3A8cqfP7b9uerXv9nj/+H7Y+Dtz91z8f+84+sC7uxhVpsCf1yCDARkNEfXQ6G" +
                "15G3zuOTV6WlOMGBEXXPf0YbBRv5CIBoKAAZpFkhGX/SxyBAZ0a9/uUgfHzNlFVMo05dfxD1fo5Gg6sP" +
                "Y1Lc6atn8fUyRFKoSq1YKGvlTEFkyguZ5lakOUQ/pBlCk5zoVVGJvxSSmyJtqRYHTm1TBLQY69LCin9o" +
                "YM8KmSciS/NbYNeq3EKgp91/wq8coAkwInwsqZ94JcXkhdwIiDUQIVdZkULAFBeDSyENxPWlitNpCiF6" +
                "royqY79GYACs7ELro0RPowf7dYpCxnNAFOssSy1yqycYy604lP4bRGyrIYIjLwIggiSO9hoewblfP6Dl" +
                "kBz88iigjhxq3voykzMQc5LG0iVLBYgNYAd92FjlkDIs7j2BX/JCmaWBlJEICYIVSTqdinVazKkCAKSB" +
                "SE1IaP2e1y4qWNsi24h0sdSmkJCWMLnNQUMZ7o0cBXYnOsG8eOgJAsBc0wZxpqTZBUwJYgp0sZWdncXa" +
                "qLOzSiqeKNhQidUyYW4hGxL5RcX6QJwTrTMgN0L+nswVdhtjRVgy+AMZ4lxniRVAuEQhQG6MTTpRvgbh" +
                "6kQCVs712kDwJ0cyoHgQEXtDSzT2XWEQFimQIX8Wh0bd6WyF77ECSS3FiyOkJlFTiB8g6s0ZIBB/rfmc" +
                "r388FpkggsVRswS9U5mO02LzEPSFJeAX9oh8tVyipqCtgrlHeSyXGXpbmlcR9Be4un/UIsa6JS+wYpWn" +
                "IAU0uATKP/bVyYaCQy4XyglirmSCxurc2RJ2Kpawbpyn8byyH0nNQjlpsMYEA0tU0hIdKEm2YQA7UKrB" +
                "gbwaKYywK/mloYoEcmjPltNPPRra1BbW2bYPQ9IYubFN2oFqRGR9WSlGK8Sg2onXmZaZc+iFvFW8yMED" +
                "72hheokalVlL/H2uoKZuzVpio1fGx1PiIteA0OlHWgualRgVnDOpRROXiFjmApz1TlXVSXQLtVgWG2eN" +
                "KD3mhnVb4d3O9SpLnOS8nGz6BwR+YBkEyXgqXoNQOgedr2EXYDPYQCCzIhy0gkA0CNqAXgpM4qTBVqPR" +
                "eM/GwTbSaNjCQNyAuEr24+pkrMGdI1ReeYOvvGIJPFlAKRKOJkwzsAERJU+kSUCchaTIQfE2nUFAPc4U" +
                "UIicLpagOY4rmyVyXQpzBvEb6+2NWFlOQdA0LECkMckR7LW2nk1ekhWm8SqT2IyBnac5gk+NJPtGMN8T" +
                "id7FGRk49Td35Kt5bJS0GJ57F6Kx4hoFFjT2x2t9jElohgnKbx7ig7qH3GQtJSeMUX9l5lqAG6Mt7ALW" +
                "fUjvIniEcAObAAlqqcEJDoHym00xd5n1TppUTjLKfrGkCHqAiw6OKphzQp3LXHv0jLHc48+gzQNe5Ok4" +
                "pEO7moEAARBawbs04dhFdgphEIw3SydGmk2DQhVt2di/NBRIUH2kEQybdfd0JszaiNLkOdLbw2oImB0q" +
                "VBcwIn0G4YyEJupCDXlmCHiJmhmlKAxO4ZdEQ5QBPFPIb3rt6wfgbhUXK0OZrdyPw2qvcAJZLdCa0W6k" +
                "zxZot652pziAc4WEAqXETj23WInympkqymwEaGWm3e6hxBbxHArYlrik5l1io91Ex4BGTRqfuiTFuw/D" +
                "i0vKsKdYWB7eQ+iE/+UaDQLzIdiOVfwR4ynGvIqhV6ljQcJfJgUsvBbzS+07YGUIjw0MF5pqNI+JjGkw" +
                "UKPh/5Pq8ybVtYG4OP/TSdWD/19Kqo/lVO6LcLltzBS0EYXZcAAZexMGqGDOD4DWoFAEwL+3vv2dxAQf" +
                "WV5PFfQeodpL0viQ59whhJWJKtYK7KJY6wcZk/SHAQ+cScZgy42PNOU75fUZe/UvK1hgcgwARnNIfR4m" +
                "HTE7WJRQAuG3LfpFCMRkUQuFfSDYVFhJjSXaDPDQQgcz1LU1sVtLNMgD+kCKYreKsgy5P4bjjQ+GLBN8" +
                "DUsO0dma2NjmDIWpgmoVqm4gVpt0libbYZQCv2OuKYrpCZg0uBTRzJuBCgGJl/ZRS/Sm5KBrZIic2zdr" +
                "ExXoouRfaN3EisqhqAv0hpzI+2qaQ0qSCWjdD2Xvw2+htBR/PIuqSxvbpW0IyyYN6bymc3z6vTRQFPI3" +
                "GfK/rZ/JVyloOLZ8grVl11rnZ2L0rUImycQszmRwJoEpV+YzKnwxaUCw877qQMpnB/c83HH426E1UAWr" +
                "p2SuCU4FxFPqQQYx5f45FglZ+cgziOeYJD4y/nLpeest+/GkMj2iXAW9UXrv5yTotJQycdi250tmfKD0" +
                "RKMzEmWYVNI4CgpPqATsXEIbRZKCtlCRkOn7FiGAg2NDdcSHcLzpPoQTGbMzoc6QYUKaazcdbEHUwimd" +
                "KqDaw9jkB3v7uzD6KR+Xa4GXIA7eIdlrbMtLO2r9iZIbMdFMtTy3Clv6ySWOSCWW5o4MvYrniAI2T9RU" +
                "QsUljgNxTAoW5xk0f8mG6zMoD5hat2CvrBUIXeTmuogWpBVDGQvKcJRRNQkFd8wjo1pXCnqhHoE1g6mI" +
                "WaU0tEAcgR3E7VIIhDgF8ocunvNanGlqU6XRK3ILh+ao6YdjtEmuYgzmZkPbGZVxd4uIqa7irVGRgDRM" +
                "k8Np3qYyIizP/UAkSGDkNim1s1bQ3odydUsrkEOn4jbX68ohHy94lon/Q//suCqwyQOGKVULbsrsWzpy" +
                "or161VjyCz7geHWCPCRTInSgRz5QPAo+nCblUq/0zVKxhWC+nkhLvSNJqXQp/iXCFmOW06iHWWJOxogC" +
                "8ZTIy1G+C8XYBO2o6r0vu3WpcUED3Wd7tE7FeuXUoZTESGcgCL9TjLPbRYoDFTz1wHjE1BLYjf+Gc6sK" +
                "3HYZYmsAkVMD7nat7HwLM74C8IX7shMXfqyieYtug0rBthhn/QqLBBfwAp9NMVmVR+PKt61cbIAYVuyD" +
                "oL8kSbnLIhEe1ei7wbXusPxxZvFjlcJOktiqbTkdhPNWOuCgwq8CBLZ7l+qVhRJR3UNVgSykBQdxjkeg" +
                "7MkG6vvOxUX7Je80pOhb22xq9IIHp/ldanS+wNoYW26Dndehgq59A8GLvIQOqgrwdLtlJGly5DYbdq8H" +
                "H7vtV46z5RJjGVa5eeCOBiIuABPp1g/bv86xr8p5kecW9FFh9eam279on4RgXW67e0faqAnBc+0cwumd" +
                "OoRDhPAq9G3uYmULhMjUtOAW9gj3gohndYYiAwn7mFIG3URZEGjiyCQRnTKRg6UyoRcAvPCIlWuA1f77" +
                "k8XObwedxv6//EfwsTUOV//1xe4PC+j868e0FFdpvDKl/OgCHUQ5HGhhq2sVD2Cw3ARVKoMzlBmf94UR" +
                "Ax88CbyXsv+gFrlV4UCpuskZvWEU5ZzKeNuaQTzLRTIJWQHQlDiTSZUgl5Rp0vbTaNB/gddm3PjtU+f6" +
                "SjCKFt4/8laVlB5R6VIxmnvZlCNGVwn41NMSXao10nyH9smzaP6DV3iy9Fadib/81wEK+uDs4BxLoou3" +
                "B01xYLQu4M28KJZnL15ACyMzEHpx8N9/cUwaKrZyzdO/3IVN1qKrilBJFTlg5ZkWB7AojanjvlXKzd6n" +
                "GbjuJM2gT2rVc2vNdPE0luXoG++Lt2wkhAX5wkDgtubBGZnZCmSFoY/nqPaMzq2BRscwPQvCdCaCFPgl" +
                "CgJebgvi7Psff3jtQDBR87ABAB+SfeB3G/1yJUB/VuFBbNBXffPR79l7D+LQ03biYD2zp2/cKzz8PhPf" +
                "vz494WdYYBAkxWrZw0CpsNYm2X6PxQ0y5Hfxp/nu80InqwwBaNBQ6OVBsHE096ea9T9WYQBNF+y+E30P" +
                "jeUSLa8p4g2U6FT1xThoddNK3zcZFQ6bwcz8lBIKo4mvFwAZJgTM/+SbXH+/bMJ/rQadGP0g3g5+bb9y" +
                "v49u3neH3faJezz/dNXrX3SH7VP/YtDvtl83/L08F7coCyFNDgrfNzxQkkI6tv66SQlaHvaVEH4NjrqQ" +
                "/OqCCtgZD46x86G7ESwETugornsfvg7KNQec/BrORvErME6kchPya1N84rOA36o0o5Cp91L5rAjD6lpU" +
                "woltmqjAHwi9Vco2+rX9svL0Kcgan34DUVdJYvk7qmiChmrHQAp/u1MFi2USBxmKz8y3kUm6QhJcs8QW" +
                "1KrpNRp2LnofRkBPdU+vZMKJCubTTZYKmw7NNGgQ6WtJOslxW/0mJBQkLVFOIGt4o/fd3rv3Y3GIuN3D" +
                "UckTX0WpSLzkaV7r0LwviEP0hSPeD6Oe34e5c/vwQ2Wfx3bByaSXHavP9TW79zzXOQ8X/Ce8VRh6g22f" +
                "xFOj1FAr3WKXSZelDZFMcT12rGjvq2XTnZF954Ta2PJEJ79gUlvMg3FVPPUBcCkYBHyaEPewX6Du1Tw4" +
                "xMRidXugRtrCgoG/85UZlHZlYtoSDZ78hqsElZF+Be65GEzzMA6tDbmqV34k67jO7jfmuk+fgrAV9Ymn" +
                "Qip2nxggoCtn5zOpzGdQT/xnJcLeyWylsFGb4hUDXblUCDziMSkUP/bzlwbuMXYI6EzK4Wq44OFGgX6F" +
                "79Bu8e4Z37dGanbInC6O8KJnEpVnY4fIPFsHtiSK72N8PmU61X1EY8VnoZZ6+J03B/hkXTXdMKAcFoSJ" +
                "grwX3+Ek8TsR/wE/EtEWL1FZUpy1wcDV9PPLLzicDI+v8DEOjyf4mITH0y/h/OLz6y/07qkE8I1B4NZp" +
                "6s7D1K0l3tDIeZ9Mcd+g20cYulxQwrqIUt4bUCm1g8ERPzf9oQx8hQcZxypzjbj9glrSdWi+dPUlTOEr" +
                "e3Eq439coZKWr0PD8MRFA8x+fiqBs0W8ZGCwk6mfeVOM2OKyBaw3dtwTsw8vigE7lZc1th5eIUtWfjQB" +
                "uT/CSRH/65Jnu7ddMcBvTarZEkuoh7Pt2v2gysrtK9NVHM9ktY+QVr/l4upXuv1MxuPuaW2d5LsrOYrT" +
                "C11/oxsxOyzT69duX5bYXXS4GyzHICp/gFXiOuRLLP7+zvaFoSPvhAxRXmaqoKAzojSPs1WiaNRK100q" +
                "1Yx9UVrxi7rt7rvry85fyINc2+FeBWwVX+JeoLaI3c6D4TBduGxb9UFC1nrkVskj2vzfCYhfI8brZFut" +
                "OCTxJla9l3aIZYUWb/AfJxz9ucszYRDkBquyPOwuI6A3TY13JLaueT9y/aYSzh5skdfi2r+3T93KvhIR" +
                "/wkIfIrEtzgAAA==";
                
    }
}
