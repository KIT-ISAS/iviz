using System.Runtime.Serialization;

namespace Iviz.Msgs.visualization_msgs
{
    public sealed class InteractiveMarkerInit : IMessage
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        public string server_id;
        
        // Sequence number.
        // The client will use this to detect if it has missed a subsequent
        // update.  Every update message will have the same sequence number as
        // an init message.  Clients will likely want to unsubscribe from the
        // init topic after a successful initialization to avoid receiving
        // duplicate data.
        public ulong seq_num;
        
        // All markers.
        public InteractiveMarker[] markers;
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerInit()
        {
            server_id = "";
            markers = System.Array.Empty<InteractiveMarker>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out server_id, ref ptr, end);
            BuiltIns.Deserialize(out seq_num, ref ptr, end);
            BuiltIns.DeserializeArray(out markers, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(server_id, ref ptr, end);
            BuiltIns.Serialize(seq_num, ref ptr, end);
            BuiltIns.SerializeArray(markers, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.UTF8.GetByteCount(server_id);
                for (int i = 0; i < markers.Length; i++)
                {
                    size += markers[i].RosMessageLength;
                }
                return size;
            }
        }
    
        public IMessage Create() => new InteractiveMarkerInit();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "visualization_msgs/InteractiveMarkerInit";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "d5f2c5045a72456d228676ab91048734";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VabXPbNhL+fPoVmGQ6sa+y7NhprvWdPziWnGjqt7OVvkyno4FISEJDEQxAWlF+/T27" +
                "APhiOU3n5pJLMjEJAovFvj678FMxTlVe6vlG5wvhSosfA3FZuVLMlKhy/b5SQueiXCpRmkInIpcr5QqZ" +
                "qN5TjMoS/2knnLL3yoq1se+cMPmg50mF8alOe5h+p0AtT5TIq9VM2QGGJqCbZBosiLXOMlE55QmWRqSq" +
                "VEkp9FzoUiylEyvtnEqFFK6aOaZVgkRVpLJUAyFG2GkTXgWYdHKhPNWlvFd8BAfmheuyIaQDFZnjmNgn" +
                "rAO5M2bLeQqZfqeyjVhLMArWqpxYSKyGkObWrIg4iDAFLyY5L4k0WE0SkJxXGX/VMtMfZalNTmTkvdGp" +
                "sCpR+h7SAoW0KjKd0AFwCjnoVTovX74glqfgloR4Cm5W0r5T1g164xy7yKTU9+qSx377PX7s9U7+x396" +
                "l3evj8W9dlV9iOnKLdz+FhekWL1S+3NL8tb53JCux3OxVDKF4ktNw2Q1LM2DPivHM+7lDblaVVqZu7mx" +
                "KygdcjAkYSjEU4UElUyWgmi5UhUD8aup/GIvUK/xwsCi5kqlM5m8YxU1hsCEiLNrDNm1dqovNiCy0otl" +
                "WVOJi2mpFKmez5Ule61Xnxsr7L3+2PeWG/mnbZLK8txnc/0Bh+Alz/jYsw1PgL3DD96wWIJ0SMljbyrM" +
                "/QAqd6YPd5jrXDl/Kn1vSnzVxAdtb0rWB0xiocwKktt41dzQ8YkKU/0TV19kZiYzmHjt8+zf0esbT4+O" +
                "xeojb1haUy2WtcdTfGBnXxpbgmnykoINfudf4sWBSJaSbAUWuluvac3ipYnMaGMOQeTxdEQcX1ZZKRKT" +
                "l9ZkTuyEkZPnIDTPjCyPDoWjpbWbqLyCY6cUL/gZ/FoNEUrnTKLhZSm0VS7Dwdj8Br1LzBxh4oZ8Cc/T" +
                "sIqoXmhIy8wbJlLtikxuAo8dOlteceYXgWxc/qV81JWpV783LBJpCTlIm+JEpaTQwvwuYejK7mXqXmVY" +
                "JFcFDsJfy02hnA/QOBL+LVSO07CBkEagnMSsVjAWDlbeCVvrvadJUUhb6qTKpMV8Y1Od0/TadfCvjsbj" +
                "4TEJxqmkIplhJ50nVklHFjIeCo6FpGL1vvd0sjZ7eFULuE29ubdSMKs+FFb5XOGOscff/eEGoA3hKOyS" +
                "wn54bIpXtyuwCVhQhUFE2QHnN5tyaXyouJdWy1nGFg/rykD1GS16ttuinDPpXOYmkvcUmz3+Ctm8pktn" +
                "2ltCZxk7a7WAADGxsOZep5gawkdInZmeWWk3PY6rvGXv6TlHSbZI1ojeMvvofawNTtFfxhq3IxL5J+Ir" +
                "KQns+3QIr+JYDSnNrcIxCGP0ycpoOA3fNc8lnzZWx7UD0bvhUBgn9P5d4ZQ2Z7rNvK91QM24hD2HPF3q" +
                "3MVcpONZZYjeneP6MIZ8/6F+2tRPH78O+43o4hlqRTlCSS15dpmnt/eN3ClvD3qfOVF8Wn9FtFLHd5yv" +
                "fq5xH0X5EeGKrfBdZz6CohCEtFZuSJVbRDhwnkL38OokKjzOovxTmoUiyBESH9KcxApKNvshVdHP+KhK" +
                "5F+ImfAFpVqypQ22J1c3lrERzGmeSbI65grolYB1ybopbZWUleVAU6vSh5AFw06Po+oEufFoGBE34uOc" +
                "MPcThHJ8Rpx4IuZaZSltYoqQPOrMalXAwoawCmfXeqE4EQdYddlJxViAvKBsCEoen2nKWZ5yjdbWctOc" +
                "PC4JcC64GR0mCuCGdwWZuNOKkI4sCoVQOlMwT+V3AiLRWYrJXvcfED0zRVljTzDLR3gU3UPwSKlLRO8T" +
                "8WRe5U+a6Yefm27NzJStBS+2Fhx2FxT2sDX9u89Nh67xhBUYfg3rhYy9ctkcqJDhrEAnBE0w7x+YLaaE" +
                "F2wZHz05ho9DTiLRLAhAPLAcUm63dGxUCgIB9vQ9JoMtZ6nIFdWNWHAwiAneF4vYDV7DYZPP6t8YgMbt" +
                "+qE2ZK6Ct4CFsZ8YPMpP9XPKjr1iPSHxRpRUitRc1MO9SGg/kGJJx+TpX8jf7aJacZAM2IgOqfOUARJ7" +
                "WxicErYSOzOVmfVuJBO+EaGz9jT2cJ89+B1CwHtBMImCh2a3Waq8EU2weD4x4EHyTqVcpIxGw1enZz8S" +
                "/iF/zrcD3HksdJrauVw2EHhDIgoVG+/Gg8+ADnmD2+u727dXx8BeBOAU2bmz8AxPRdbSQcxReeO3Xk4c" +
                "UMSOnJl7tRuoXZy+vTp70yWYySpPlv8VTVLr97UYTg7CgGf75Hnz6vc9OQwjba39P2vqUD2QbFr5OPpU" +
                "DX191dSUJXWq8SIjKdWEQ6nyeGnYFDRhC1ILldi58vgfaFIvKPMEV7+XWaVq48DPrQI6dkrE67djzhAw" +
                "XXAtOUe6uh4WhbKEHqiR1OFA7KgBSlYpZlVZ4hub9+6DyhOEh61KOTMA11u1h9ixCvmSmwSm6RN0Ag6X" +
                "MA9ltUtWtl5qhD3wNVO0LxffVGvA6LhlkdHrwDNCFepxEG+56X0Ka3VhKrUkWjBrZVJ13NScS7PuoDBU" +
                "1HkAHeOrN6Pb8eRYnAN7PJiGwz2ie0SG8S8jlF4/KlV05vuWBVlVaEY4OiYW/DQe/Tw9Pz0bX8F0TzMy" +
                "gs3eRwGDy0PMAKhRcMidD8dkRWvUnX2xOc7UvOyLj8dVUbtj4JeSpIgeSuxgIHpkazfOdWG4xemU5OO7" +
                "JuF4QWZbJuwrzqvrq9FxA8/JsDTZWkYAToaOSscl/ylyw3PVh5IDIsn6cnT19lhcUD4lin2ySmGpdbTH" +
                "humTBuUdFjgtefV2MrlGjBxligIX6r6cvJUEs9eK1ZfXP42mp7+M78BltCchMwNb8+b8YU9+0K6eenNx" +
                "yieq58JG/cRaLz6mTk4nkfAtGy1iqKlgto+S9fOPKSHN2J+azdjWW/SiPkkQbWWSjFq69OdnNYbv8aSE" +
                "s0R70G9D6CgG5nozBkGtqf4TRl+C8ydHwyeNnQdDcNyYjr0e2L5BnPn27s34fPLt2eT2AtbkPx4NqWXn" +
                "jG2kcDRsS5bKHe5M0FQue1qSpalBrtvzBBJRVfL2sN2F9hWv7LYSW+dhaudVlomXe8Prc6aYIn5SjArs" +
                "xFI4dv9aW/hThJ0GbWkdkXP9oytVHvt+W6b+ww/R51piDT7nu7qoMFS/jcMRN0IX2ndDZeY4LcGp9Cyj" +
                "GBIwixKLSpOP5IZL4U6Awg6D3swYWg/w76ZxOW17Geg/zHeozRJAUAjKe/CDNgMJ+UL56wCCMKui3HBD" +
                "3/mG7YMuo+AWDMdWChymRBxmKD6p+9UuerHvz/pqjKrDmS6pKbOVe/qMrClY1J3iB33uVuIKvW6fl7bD" +
                "94CZGfvPvn3smwuhlx6P2OfGOee4uCkTg2hKLzRm6vNJ8VEetq8eOBh3Qjdp0+PtCA/IagjAU8c9Nfmz" +
                "MtyuNJKlPhirV1KSaZq1sDNIxUqoWK19H2XQ0Qif0VGerAqqZh0pJeaall0BnqsCcJiAvl87fZiH/0Ib" +
                "myS0rvvj0XJSoxypmiHLk0sT7iN89TcQbXRw6rurOunsM5Pc9sq7qg8R7dHO+VcDp/Utzx0qymVZFsf7" +
                "++v1egCEPjB2sb/W7/Q+XYnsD71jTqidHJZxyPrTRZMKVQ8QR1zhvjk6/ebw4JV0OsHPu6UENc7SK6ri" +
                "6X7JrgLIyeHKXJe17yrYboh0jGSnt7fXP9c1wNnbV6O6Ari7gZmMavh/9uvF+Go4uj05CgN4HU3vJrfj" +
                "m5MX7aGL8d3k5LsWRT/yskPWj8Xoe3M9vprcncTAOxn9Mpm2fObkhzqL3r2Z3o7urt/enoHRyDZ4OL16" +
                "fRGIPn9eH244rI92eT0cn/9avw5HF6NJczj/enpxgdN1L6HEJ/48jd8ZWnXv+IIOajjuPkXEE7qKF8nk" +
                "5DCSJMRkM/uDr33zwWDgLxvgvPCqP1D6sY5Zm75cp/sBQjGxBsn5Wo9RnwqEerGpIP72tz/hJuw6HtKW" +
                "/rZ2e1OiWt9/ewOUuS4qiprUeoJhpypTpTfAyIEg5GADH1zFf5KTp2ISqvwO88HxP73qQMg03UdgQAnH" +
                "TVpe3RfP6ZYMQZ57ELt9cej5U6496agZRNj0o+5T94iPbH7TyhKB7e7inzBk7JG/nHuw2N/1dVaL5338" +
                "ZSzu6oS8U1EUAqSiD3S17t5XyEjUQAl3XWcmM/b29StuvBr7YB/+Kn47GBzsPR8c/N5LK+sjRqbniu9N" +
                "HhXsG+QQBt4t9kLnKpPc6eJGoi8EJeIW+UDCjHqhUpv0IJyFpt7DEjj3+FsXZHmA/sf3HseGlw+bYdtZ" +
                "gBF7fiky+0ANHrksb12VK/7FiHhRjqx2ncd7PD1vekuuUImea4xSh9tBgwyLgjutUH0jk/mY1W+Fwb6A" +
                "o+5u2Qt4ACLg5Oy+zI5PQyOS72Oh3dDiVZr7HdRSJFyv3hMKDICmWREZu7qmEkdmxVJGHLoBSCFWHzEt" +
                "vrulrXpcR9JSU5+M2eYKMcCX2CLE2Kfmd0L7w4XQ13IK8GoqmyhvNTyE1VOFg6SpAoMUXChZfp3roeDL" +
                "j90NiXv+1r0VYqQzLh9U2NwdCiv5lyqsvyhhGGfZpwiyMoginVALVr6jX92hxic1n4oCxGSnFGKb9z2i" +
                "vi8weBZ7JnERQRbVRGkDupvfxhHhcKhl5oceRDLPfjPfw4611u6A/JOwK6NWPNhwc25qSA+++Ia3NKYf" +
                "KwzmY8tTmt+qyOGiMv3sxdmXUfW2wde/WGHrp0X9NKufZK/3Hyeeb75KJgAA";
                
    }
}
