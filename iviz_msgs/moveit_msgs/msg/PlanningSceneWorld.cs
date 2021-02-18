/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlanningSceneWorld")]
    public sealed class PlanningSceneWorld : IDeserializable<PlanningSceneWorld>, IMessage
    {
        // collision objects
        [DataMember (Name = "collision_objects")] public CollisionObject[] CollisionObjects { get; set; }
        // The octomap that represents additional collision data
        [DataMember (Name = "octomap")] public OctomapMsgs.OctomapWithPose Octomap { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlanningSceneWorld()
        {
            CollisionObjects = System.Array.Empty<CollisionObject>();
            Octomap = new OctomapMsgs.OctomapWithPose();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlanningSceneWorld(CollisionObject[] CollisionObjects, OctomapMsgs.OctomapWithPose Octomap)
        {
            this.CollisionObjects = CollisionObjects;
            this.Octomap = Octomap;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlanningSceneWorld(ref Buffer b)
        {
            CollisionObjects = b.DeserializeArray<CollisionObject>();
            for (int i = 0; i < CollisionObjects.Length; i++)
            {
                CollisionObjects[i] = new CollisionObject(ref b);
            }
            Octomap = new OctomapMsgs.OctomapWithPose(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlanningSceneWorld(ref b);
        }
        
        PlanningSceneWorld IDeserializable<PlanningSceneWorld>.RosDeserialize(ref Buffer b)
        {
            return new PlanningSceneWorld(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(CollisionObjects, 0);
            Octomap.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (CollisionObjects is null) throw new System.NullReferenceException(nameof(CollisionObjects));
            for (int i = 0; i < CollisionObjects.Length; i++)
            {
                if (CollisionObjects[i] is null) throw new System.NullReferenceException($"{nameof(CollisionObjects)}[{i}]");
                CollisionObjects[i].RosValidate();
            }
            if (Octomap is null) throw new System.NullReferenceException(nameof(Octomap));
            Octomap.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                foreach (var i in CollisionObjects)
                {
                    size += i.RosMessageLength;
                }
                size += Octomap.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlanningSceneWorld";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "373d88390d1db385335639f687723ee6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZW28bNxZ+H8D/gWgeZG0UOYnToquFHhzLaVwksRt7d5MYgUHNUBJrDjnhcGwri/73" +
                "foeXmZHttNvF2k6QiNThuXznSvoRy41SspZGMzP/VeSu3sr209aR3zn73BGdt0Rb2SN2uhLM5M6UvGJu" +
                "xR2zorKiFtrVjBeFdDjBVU9EwR3fyuKR87Je1jtHYfFv6VbHpm75bWXZ9P/8k709+WnCSnMppAuybxgK" +
                "i/bYSvBC2BFralGwhbFMaicszHJSL2GlYBXUBACvPWGk7/CQBTMLTxegYtualyKwk5q9hfhDN9zKameJ" +
                "oSx6UIYDbl0JIuUerjknUBbsQpurno/Ch3MrcrPUHugIp98/JRbEp2PeOWEpTCkgXcBJdW1yyR10u4ID" +
                "emqP4zlpg72MWxFo4ODKq2k8OS3kQoJDH4kTowBEkpSzysoSWl4ScPWKVyJo68mO03cItD5dPL0OpBQc" +
                "fYLz6AaS9lbUqxucaQvkZfzmTl70ZZ/NS9PogpxSKa5h8rb40nCClsm6s3PE5o0LgUBkLOeazQEbANUE" +
                "ZFMTC2z2MoCEDDf0O6azZI8X9S1j6cu+hntFUfdjK/qA6DRJrXOhxZgdbgQgYvdSmqZWayauZe3IBOnI" +
                "JqSr4rko4Oz52gm2N5tNnwZJ7wUlyoawhTWlXwt9Ka3RJezFZyetAOttcSns2q1ClqAWlNzlq8ihCxJZ" +
                "DKOw9wdvj/51MH0WLasqoQsyiOvWOmLDlUVcRdVryos/t7gwkKuNC4eStfBHz9Tj44N3s+nzIB3HO7F3" +
                "S/SCRkyLq5gQ0e9U7dg2USQXIlUsX9esbGpHFEosoElZufWQZMkFq40iyIBwqilRNLQsRA1Ai6imh2g3" +
                "KHlUCRvCESiBL5aoT2VLa9L391Y7a1eE+Ay1j/LccV1wWyCXHKdy5UvmSi5Xwj5RCAmFQ7ys4Hn/LdWk" +
                "euxLC2zF3yVQtVwBDV8hYVluyrLRMkdVYk4iR/vnCT+qjBW3TuaN4hb0xiJriXxhUWqJO/7WSF6hc8EO" +
                "ZxPQ6FrkDdUNSJI6h4d9mh7OWNagwu8+pwPZo9Mr84QK/hKVvRUeAhHKimtqb6QnryeQ8bdg3Bi8AQ5K" +
                "sUZ6bvu9cyzrIYMQqCAqk6/YNjQ/RoaYEFCX3Eo+V4IY50AAXAd0aDDscdaetebaJPaBYyfjv2GrW75k" +
                "05MVfKZ88jRLAAjCyiIUkR1svvZMckXFjCk5t9yuMzoVRGaPXhHGIKJQJY/IW10ktTbvjXNZ3Fc0/nkL" +
                "zB795R929PLng/1TCoy/fjj+hHTdN9pxqamAUJqGzOVzE5uH7/Lo65QxaDup7SIxaApyVwYBheoDCsQ6" +
                "gMQQglpqlgKHLUOCILwRy4VYSDQhrklk4NH1/AuxTsNIX8jE7wQWKMhUwlGbbap0S2QJRrV5O6OATcez" +
                "mPcVqp2xPh1gx88nR+92kL0pRz7uvX3DAosx22vLK+aCtj6X/MJX0DrMFgmb3FiaMozvxiQ4DUJjdjBe" +
                "jkek6W3v+zpPVVkZc4HYvRAT9t1/BgT0YDLYN02+mr0cjNjAGuOws3KumuzsKIM0Aehu8Nt30Ujrs0dT" +
                "LdKXBI9v4sGLvsA576QeDsgoSB/gkER2oopdCBHrxkKhkcylkm493pz0NkIXNouAoy+JmJlmL0OQeC5k" +
                "F7WlKBr+UkXtw6wBVtSIr5GeStQT2nwFHaPBfs08pwlrUQibBAQ2bwIx+f7vP76IJDQ2QlVoCMLbag+S" +
                "tJNf3jD4rxYro4rWX5vCT76o14kksvfi2OBqWe/+ELcqY7H1/Yvd52GNA5ZIpFLmKtGg5Fyh7N/cp1Gb" +
                "DEpS0nUlfl2aolFE4KhCOlMN2hincL+vrvmteRc6zUL6zs31CEMSRd6I5WsUZ38HQfgJ6mV7SgUuYQwP" +
                "Oe+b5YpfUoDQmD5P0yuY0XhC06jPTetT/+kIf8aZ73Y/spdHH6bP4ueT49cH7w+mz+Ny/+Obw3ezg/fT" +
                "3bRx9O5g+iKLoZvqlp+JSKdIRftZIirQMDRdNupN0oUy3P3wAqNtR5HOlIL7ia5/oEc2YYKjiVInx8zh" +
                "IghhvCS4rlP5GnRnBmEUy2KM0rcw3Ks68qsPI/Zx5JP3U19nApm+VkIvceOJGm1UJZowUDpb+wD6uMP2" +
                "/MP0aW/1scWaVp8AdV+lgH/UymgUdXI7FVL8r/3gQbPRKBYZX5+D3ZYXsiEVqLP7MZsiaLzh1/P3e7PD" +
                "f55An77M5GTPkxwcJrOASggdVFDt62B7s+HKeMOJ5hPjGI/HLJTLhYmIJb7nrw8Of3p9yraJd1wMO5uo" +
                "Vy36iHc2IZaXK9diHnOBbVMuDIM8qnpJTrAuygmLnpxvSQGHFrvgvnjLvlvmvvEttmi/wvnupnozJzGa" +
                "59L64XQcUkZWXQx5TOm8gZMo3ptqFJBljyOo2Y1MjPi1IXXDeARXL1NvEXfAEOH9lLjbt1f/ltI+B4Xh" +
                "h2YZal5I1IUVFK64fI6Ct2hgCN/LMCjRtOIDz58ds+yYAGsJsl9wNRdWe74d3UMZKH0M+0tMnua8+DYk" +
                "k608+HjT3FQC2XX7ad1++vow6nfQJRv673YbeG4qT6svHe40ECFY/9ii9OnqAdorPfqkptpzA73zUPFT" +
                "uM/7RMQNSS8xK/2j1z0uuWoEIbGgq59pPekfAC4FXTdFffY5IxmnkQH6WMsri4WR567BsJhOpLcQzLlN" +
                "5Qm8NnfEE5ilQw8EVTLjDsiSWZhoW6XCPflsN+gprs8B3ANp61/L/KvUzYLCwyPZKD67dc9y7dsdv2aP" +
                "6V77mOVf8U/BpuwpOYuzyRTJKxZnTz9jOW+Xz2iZt8vntCza5e7nNuLPXnz2e/d2wf2Dd3JfX3dnjJ7e" +
                "EaBzqXFRj1eUEd0jQI+83XylTqOWr8LpldrT3fm0Gw6Fezy744WSCsVS6sQ1Rn5kCKo7H/qTVQ8J2v8M" +
                "1ivFl4QG7sjGwcJ0dNuPaVQYd0yeN5UUxRBosEWjCADscJ2vExTb47nbGeO6u5BKDLO5MSoyIhn7CpeZ" +
                "3q8NYj+hfhiOd78qyHwK1EY1PrLp5acctgNzSa89VOHCMW0wnrbl17bHiEk0AzcryZX82mZTOOp/+RHu" +
                "vtYPTuMVgeCvn9SXr6x0KXDqjKYMGuhx5cuy3wHQEs3wUBoAAA==";
                
    }
}
