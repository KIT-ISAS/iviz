using System.Runtime.Serialization;

namespace Iviz.Msgs.visualization_msgs
{
    [DataContract]
    public sealed class InteractiveMarkerUpdate : IMessage
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        [DataMember] public string server_id { get; set; }
        
        // Sequence number.
        // The client will use this to detect if it has missed an update.
        [DataMember] public ulong seq_num { get; set; }
        
        // Type holds the purpose of this message.  It must be one of UPDATE or KEEP_ALIVE.
        // UPDATE: Incremental update to previous state. 
        //         The sequence number must be 1 higher than for
        //         the previous update.
        // KEEP_ALIVE: Indicates the that the server is still living.
        //             The sequence number does not increase.
        //             No payload data should be filled out (markers, poses, or erases).
        public const byte KEEP_ALIVE = 0;
        public const byte UPDATE = 1;
        
        [DataMember] public byte type { get; set; }
        
        //Note: No guarantees on the order of processing.
        //      Contents must be kept consistent by sender.
        
        //Markers to be added or updated
        [DataMember] public InteractiveMarker[] markers { get; set; }
        
        //Poses of markers that should be moved
        [DataMember] public InteractiveMarkerPose[] poses { get; set; }
        
        //Names of markers to be erased
        [DataMember] public string[] erases { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerUpdate()
        {
            server_id = "";
            markers = System.Array.Empty<InteractiveMarker>();
            poses = System.Array.Empty<InteractiveMarkerPose>();
            erases = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerUpdate(string server_id, ulong seq_num, byte type, InteractiveMarker[] markers, InteractiveMarkerPose[] poses, string[] erases)
        {
            this.server_id = server_id ?? throw new System.ArgumentNullException(nameof(server_id));
            this.seq_num = seq_num;
            this.type = type;
            this.markers = markers ?? throw new System.ArgumentNullException(nameof(markers));
            this.poses = poses ?? throw new System.ArgumentNullException(nameof(poses));
            this.erases = erases ?? throw new System.ArgumentNullException(nameof(erases));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal InteractiveMarkerUpdate(Buffer b)
        {
            this.server_id = b.DeserializeString();
            this.seq_num = b.Deserialize<ulong>();
            this.type = b.Deserialize<byte>();
            this.markers = b.DeserializeArray<InteractiveMarker>();
            for (int i = 0; i < this.markers.Length; i++)
            {
                this.markers[i] = new InteractiveMarker(b);
            }
            this.poses = b.DeserializeArray<InteractiveMarkerPose>();
            for (int i = 0; i < this.poses.Length; i++)
            {
                this.poses[i] = new InteractiveMarkerPose(b);
            }
            this.erases = b.DeserializeStringArray();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new InteractiveMarkerUpdate(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.server_id);
            b.Serialize(this.seq_num);
            b.Serialize(this.type);
            b.SerializeArray(this.markers, 0);
            b.SerializeArray(this.poses, 0);
            b.SerializeArray(this.erases, 0);
        }
        
        public void Validate()
        {
            if (server_id is null) throw new System.NullReferenceException();
            if (markers is null) throw new System.NullReferenceException();
            if (poses is null) throw new System.NullReferenceException();
            if (erases is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 25;
                size += BuiltIns.UTF8.GetByteCount(server_id);
                for (int i = 0; i < markers.Length; i++)
                {
                    size += markers[i].RosMessageLength;
                }
                for (int i = 0; i < poses.Length; i++)
                {
                    size += poses[i].RosMessageLength;
                }
                size += 4 * erases.Length;
                for (int i = 0; i < erases.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(erases[i]);
                }
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerUpdate";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "710d308d0a9276d65945e92dd30b3946";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE91aW3PbNhZ+rn4FJp5O7K0s39Jsq10/KJacaOrb2nLaTqejgUhIQk0RDEhaVn59v3MA" +
                "UKRlJ52dTTqz2c6aAnEOzv0GbolhrNJCT1c6nYm8sPjTEedlXoiJEmWqP5RK6FQUcyUKk+lIpHKh8kxG" +
                "qrWFVVng/3QucmXvlRVLY+9yYdJOy6Hy62Mdt7D9RgFbGimRlouJsh0sjYA3SjRIEEudJKLMlUNYGBGr" +
                "QkWF0FOhCzGXuVjoPFexkKkos1gWqtMqdVq8foVTPoyBk84YrTIl5iaJc6Y5K21mgNNMHVrQnsuZ6ggx" +
                "LMTCs2lS3nB71e+NBsJY8dNgcDXunQ3fD4hGt94VwzSyagFSZeIJICozq+61KSGCgkgSAAj/iLm8yXN1" +
                "5oGY69kcC5BhKqbG1uCY8IA2sLpVo4poiXWEdcel14MKaiCFFCTORN+TQmu4n6MrNsCVGoibuJS5egx0" +
                "AVblKjEyFiBIinxuyiQmVqY4CWoxZSG2F9LeKZu3BUkdfyBMZYEt33HK+qHGhTgW+37Ri/5YHLT8SgE9" +
                "Qp8XplBdOntWSivTQikyL+bV2Bh0Q2+ZNRHUWmf0xGBrWuSVuO9UVojIpLnO6YWYrCCBNCYjbG2dO6JJ" +
                "m9gq45i4sV7ycWsIXFZGhb5XbudvvwvPJ4CviFEiYxGwkDLW0lmY+6dwEBjwsJiIT/KqBhamhWUXe2fC" +
                "difLVuv4f/yvdX7ztivudV7KRH+UhTbpeJHP8r0NusnF9ELtTS0ohrFMDQl9OBVzJUmchaZlCggF8bDf" +
                "Zl05rpyLgy2rCigzh9UvIGro2wCHhrocVihYyWguCBfUlXXEr6Z0wFZFCrQ4FyHHnioVT2R0RwicXeSE" +
                "ghERZZdYskudq7ZYAckCTldUWAIwgUoR6+lUWbKOCvoUZmDv9ce2ix6BfjomKi3vfTnVD2CCQV4y27At" +
                "2oBQBut6x2Lx0qEANUx1oRFBiPqO6CW5aSPSTXXqfTnT9/DCzGiig443Besj77RmyiwguZVTDVkQY2Gs" +
                "n4jis8RMZJKs1uGcQ3cI6Osg7oOjU19Ki9aUs3kVzCn0cxyfG1uA6DyyOiPaxPa/xat9Ec0l2QrMd6eC" +
                "qe1i0Egmyht3ScGcWAT7skzYPwtrklxs+5XjAyCaIuYUR4ciJ1DC0YMSEIZLJIJY5OWEn0Gv1RChzHMT" +
                "afJbaKuYe8bY/Dqtc+wcYOOKPBjPYw9FWM8QGcgBKyJinWeJXHkaG3g2vOLEAQFtAP9SPpoXsVO/MywS" +
                "aQE5SBuDo0JyaCZ6XXbZTdS9Sig1LTLlAzdF1tzlXrCE/2YqBTdsIKQRKCcyiwWMJeIUx05Yg3eeJpEN" +
                "bKGjMpEW+xGMdUrbK9fBf1WOGfa7HHxVVJLMcJLPMmQhw77gmE8qVh9aW6Ol2cVPNaPkGA53Vgpi1QMy" +
                "oysD8i7O+IdjrgPcEI7CKcj827w2xs98R+AQkKAyg4iyDcqvVsXcp5B7abWcJGzxsC7KYi8J6OVODXPK" +
                "qFOZmoDeYVyf8VfQphVe4mkXST9O2FnLmeSEjSx2rynz+PDhq6JET6y0qxbHVT6ytXXKUZItkjWiN8w+" +
                "eB9rg6uvL2ONmxGJ/BPxlZRElRJHB8rRFKwgpalVYIPKxzZZGS3H/r3mveTTxuoAi3rqikNh2ND6Twku" +
                "bcp41/u+FoMgJXgOebrUaR5ykQ68Sh+9G+y6MIZa9aF6WlVPH78O+WvRBR4qReVUVdfk2SSefn1Yy53y" +
                "NuqmT3MUnpZfsVqp4jv4q56rmp+i/IDqio3wXWU+6jIgCGmtXJEqN5Bw4OxB9/DqKCg87KL8U5iZopLD" +
                "Jz6qJwFByWbPpyr6Gx5VgfwLMVN9QamWbGmF48nVjeXaCOY0TSRZHVPVceV7wbopbBkVpeVAU6nShZAZ" +
                "V/6ujqoSJMkF7CHi+qqfvE2KFwjleI048QLFvEpiOsRkPnlUmdVy4QMUhmoVzq4VINXygDpvpGLri3Qf" +
                "lFx9pilnOcxVtbaUqzXnAcSXc97NiJkggCs+FWjCSVzlyyxTCKUTBfNU7iRUJDqJsdnp/gHRM1GUNXYF" +
                "k3zE/UKDCV4pdIHofSxeTMv0xXr74ee2WzMxRQ3g1QbAYRMgs4e17d9/bjt0jSdAYPktrBcydsplc0j0" +
                "ncsKxCFwgnj3wGS51mgXeeYwPDp0XD72OYkEs6AC4pHlkHKbU4G1SoHAlz1tV5O55idV1IsCYL8TEryb" +
                "A+A07seV57XWnYfj2r7tZ6q8t1DT7jZ6j3Jb3Z6iYa+Ap0p8LUpqRSoqquVWQLTnUbGkQ/J0P8jf7axc" +
                "cJD0tRExqX0Hzt7mF8dUW4ntiUrMcieg8e8I0Ul9G3u479/pN4SA3xmVSRQ8NLvNXKVr0XiLZ45RHkR3" +
                "KuYmZTDov+md/NTllpYd/HGAOw2NTgh07L1VCbwiEfmOjU/jxZeoDvmA68ub69uLLmovKuAU2Xlu4RkO" +
                "i6ykg5ij0rXfOjlxQBHbcoI+eMdjO+vdXpy8ayJMZJlG8/8Kp5sYBDEch6GCI/v4YP3TnXt86FfqWvs7" +
                "e2rfPZBsavk4+FRV+rquad2WVKnGiYykVCH2rcrTreG6ofFHkFqoxU6Vq/9RTeoZZR7v6vcyKVVlHPi7" +
                "0UBPrVmwit7eDjlDwHRBteQcmVf9sMiUperBD3HWFIht1UHLKsWkLAq8Y/PeedR5AnG/1iknBsX1Ru8h" +
                "tq1CvuQhgVnPCRoBh1uYx7LaIStbzjXCHuiaKDqXm28eOUIuNLJI6GfHEUIdateLt1i1nqu1mmUqjSRq" +
                "ZdbCxKq77jnnZtmowtBRp77oGF68G1wPR11xitrj0TYw94TuERmGvwzQev2kVNbY70YWZFV+GMGzSwC8" +
                "Hw5+Hp/2ToYXMN1eQkaw2v0oYHCpjxkoahQccvuhS1a0RN/ZFqtuoqZFW3zsllnljp5eSpIieCiRw0M+" +
                "97t2Guc6v1yjdEzycVMTz56X2YYJu47z4vJi0F2X52RYmmwtoQJO+olKwyX/JVLDe9VDwQGRZH0+uLjt" +
                "ijPKp4SxTVYpLI2OdtkwXdKgvMMCJ5A3t6PRJWLkIOERMfq+lLyVBLNbi9Xnl+8H494vwxtQGexJyMTA" +
                "1pw5P+zKB51XW6/OesxRtRc26jZWenExddQbBcTXbLSIoaaE2T6J1u3vUkKasD+tD2Nbr+EL+iRB1JVJ" +
                "Mqrp0vHPavTvA6dUZ4n6ojuGqqMQmKvDuAiqbXWvsPoalL846r9Y27k3hJzvHMKsh8atiDPf3bwbno6+" +
                "Oxldn9Egl18e9Wlklxu7lsJRvy5Zand4MkFbue2pSZa2erlu7hNIRGXhh9J6pl3HK5ujxBo/jO20TBLx" +
                "erd/ecoYY8RPilGenNAKh+lf7QjHhT+pU5fWETnXP5tS5bUfNmXqXvwYfK4mVu9zbqqLDkO163U44kYY" +
                "UfM0VCY5pyU4lZ4kFEN8zaLErNTkI+5SoRmgcEKnNTGG4FH85+MATsc2x/HrfIfeLEIJCkE5D340ZiAh" +
                "nyl57ycjapEVK8LhrpTU4ymj4BEMx1YKHHzPwKX4qJpX58GL3XzWdWPUHU50QUOZjdzT5sqagkU1KX40" +
                "564lLj/rdnlpM3x3mJihe+3Gx2644GfpgcU2D845x4VDGRlEUzihMVGfT4pP0rB54cHBuBG6SZuu3g7l" +
                "AVkNFfA0cY9N+hLykGlRuwmgCt7ftlCSWQ9rYWeQipVQsVq6OUqnoRHmMac8WWbUzeaklJBranaF8lxl" +
                "dMODQt/Bjh/n4b8wxiYJLav5eLAcuisjVXPJ8uLc+PsI1/3xDWBVHfTcdFVHjXMmksdeaVP1PqI9OTn/" +
                "asVpdctzg45yXhRZd29vuVx2UKF3jJ3tLfWd3qMrkb2+c0y6cA1gHLI+CTQq0fWg4ggQ+bdHvW8P99/I" +
                "XEf4ezOXwMZZekFdPN0v2YUvclK4Mvdl9bsKthtCHSJZ7/r68ueqBzi5fTOoOoCbK5jJoCr/T349G170" +
                "B9fHR34BPwfjm9H18Or4VX3pbHgzOv6+htGtvG6gdWsh+l5dDi9GN8ch8I4Gv4zGNZ85/rHKojfvxteD" +
                "m8vb6xMQGsgGDb2Lt2ce6UF1K9rr9yvWzi/7w9Nfq5/9wdlgtGbO/eydnYG75iWUeObfVnjPpVXzjs/r" +
                "oCrH8+eQOEQX4RsBviBP+IGUZiZ/8I1+2ul03GUDnBde9QdaP9Yxa9O163Q/QFVM6EFSvtbjqk95RK0w" +
                "VBDffPMJavypwz4dOS2TJw8lrNWnDc4AZaqzkqImjZ5g2LFKVOEMMFAgqHKwng7u4p+lxH+agIDSIN47" +
                "/vNQ+3QhvYfAgBaOh7QM3RYHdEuGIM8ziJ22OHT0qby+6Wi9iLDpVvPn7hGfOPyqliU82U3g91gy9shd" +
                "zj0Cdnd9DWhx0Mb/uBbPq4S8XVIUQklFLyBOkX8okZFogOLvuk5MYuz12zc8eDX20Tn8Vvy239nfPejs" +
                "/96KS+siRqKniu9NnhTsO+QQLrxr5PnJVSJ50sWDRNcISsQt8oGICXVCpTHpvueFtt7DEjj3uFsXZHkU" +
                "/U+fPQwDLxc2a99SEOiuA0Vm76jOE5fltatyOnRVXZQjq12m4R5PT9ezpTxTkZ5qrNKEO4cGuSzy7rRA" +
                "941M5mJWuxYG2wKOurNhL6CBP13A3/zLnLjlB5F8Hwvt+hGv0jzvoJEif1nygapAX9CsIQJhF5fU4sgk" +
                "m8tQh65QpJTui4rHpsV3t3RUi/tIAjUVZ0w2d4i+fAkjQqw9t78R2h8DQl/zMYpXU9pIOavhJUCPFRih" +
                "b1BQt8AbKFl+nesh78tP3Q2Je37XvBXiSmdYPOqweTrkIfmjCusuSriMs+xTVLJWHxzRCFbe0QdJNPik" +
                "4VOWAZlstEJs825G1HYNBu9izyQqQpFFPVG8LrorYCk8c+hlpoeuiGSa3WFuhh16rZ0O+SfVrly14sH6" +
                "m3NTlfSgi294C2PaocNgOjY8Zf1VRQoXlfFnL86+jKo3Db76sMJWT7PqaVI9yb9zMupvlDe+OPr/+6rm" +
                "T9X6ooCJKQAA";
                
    }
}
