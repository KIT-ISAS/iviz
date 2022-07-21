/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerUpdate : IDeserializable<InteractiveMarkerUpdate>, IMessage
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        [DataMember (Name = "server_id")] public string ServerId;
        // Sequence number.
        // The client will use this to detect if it has missed an update.
        [DataMember (Name = "seq_num")] public ulong SeqNum;
        // Type holds the purpose of this message.  It must be one of UPDATE or KEEP_ALIVE.
        // UPDATE: Incremental update to previous state. 
        //         The sequence number must be 1 higher than for
        //         the previous update.
        // KEEP_ALIVE: Indicates the that the server is still living.
        //             The sequence number does not increase.
        //             No payload data should be filled out (markers, poses, or erases).
        public const byte KEEP_ALIVE = 0;
        public const byte UPDATE = 1;
        [DataMember (Name = "type")] public byte Type;
        //Note: No guarantees on the order of processing.
        //      Contents must be kept consistent by sender.
        //Markers to be added or updated
        [DataMember (Name = "markers")] public InteractiveMarker[] Markers;
        //Poses of markers that should be moved
        [DataMember (Name = "poses")] public InteractiveMarkerPose[] Poses;
        //Names of markers to be erased
        [DataMember (Name = "erases")] public string[] Erases;
    
        public InteractiveMarkerUpdate()
        {
            ServerId = "";
            Markers = System.Array.Empty<InteractiveMarker>();
            Poses = System.Array.Empty<InteractiveMarkerPose>();
            Erases = System.Array.Empty<string>();
        }
        
        public InteractiveMarkerUpdate(ref ReadBuffer b)
        {
            b.DeserializeString(out ServerId);
            b.Deserialize(out SeqNum);
            b.Deserialize(out Type);
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new InteractiveMarker(ref b);
            }
            b.DeserializeArray(out Poses);
            for (int i = 0; i < Poses.Length; i++)
            {
                Poses[i] = new InteractiveMarkerPose(ref b);
            }
            b.DeserializeStringArray(out Erases);
        }
        
        public InteractiveMarkerUpdate(ref ReadBuffer2 b)
        {
            b.DeserializeString(out ServerId);
            b.Deserialize(out SeqNum);
            b.Deserialize(out Type);
            b.DeserializeArray(out Markers);
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new InteractiveMarker(ref b);
            }
            b.DeserializeArray(out Poses);
            for (int i = 0; i < Poses.Length; i++)
            {
                Poses[i] = new InteractiveMarkerPose(ref b);
            }
            b.DeserializeStringArray(out Erases);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new InteractiveMarkerUpdate(ref b);
        
        public InteractiveMarkerUpdate RosDeserialize(ref ReadBuffer b) => new InteractiveMarkerUpdate(ref b);
        
        public InteractiveMarkerUpdate RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarkerUpdate(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.Serialize(Type);
            b.SerializeArray(Markers);
            b.SerializeArray(Poses);
            b.SerializeArray(Erases);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.Serialize(Type);
            b.SerializeArray(Markers);
            b.SerializeArray(Poses);
            b.SerializeArray(Erases);
        }
        
        public void RosValidate()
        {
            if (ServerId is null) BuiltIns.ThrowNullReference();
            if (Markers is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) BuiltIns.ThrowNullReference(nameof(Markers), i);
                Markers[i].RosValidate();
            }
            if (Poses is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Poses.Length; i++)
            {
                if (Poses[i] is null) BuiltIns.ThrowNullReference(nameof(Poses), i);
                Poses[i].RosValidate();
            }
            if (Erases is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Erases.Length; i++)
            {
                if (Erases[i] is null) BuiltIns.ThrowNullReference(nameof(Erases), i);
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 25;
                size += WriteBuffer.GetStringSize(ServerId);
                size += WriteBuffer.GetArraySize(Markers);
                size += WriteBuffer.GetArraySize(Poses);
                size += WriteBuffer.GetArraySize(Erases);
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, ServerId);
            WriteBuffer2.AddLength(ref c, SeqNum);
            WriteBuffer2.AddLength(ref c, Type);
            WriteBuffer2.AddLength(ref c, Markers);
            WriteBuffer2.AddLength(ref c, Poses);
            WriteBuffer2.AddLength(ref c, Erases);
        }
    
        public const string MessageType = "visualization_msgs/InteractiveMarkerUpdate";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "710d308d0a9276d65945e92dd30b3946";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
                "bKGjMpEW+xGMdUrbK9fBf1WOGfa7HHxVVJLMcJLPMmQhw77gmE8qVh/Elvjt2uQHv7e2Rkuzi3U1oywZ" +
                "qHDmCqrVA1KkqwfyLg77h+Oyg0MgJYXjUAJs89oYP/MdgdNAi8oMQss2WLhaFXOfS+6l1XKSsOnDzCid" +
                "vSSglzs1zCmjTmVqAnqHcX3GX0GbVniJp11k/zhhry1nkjM30tm9phTk44gvjxI9sdKuWhxg+cjW1imH" +
                "SzZNVo3esP/ghqwWLsO+jFluhiZyVARaUhKVTBwmKFlT1IKUplaBDaoj22RutBz795r3knMbqwMsCqsr" +
                "jolhQ+s/Jbi0KeNd7/taDIKU4ELk8lKneUhKOvAqfRhvsOviGYrWh+ppVT19/Drkr0UXeKgUlVN5XZNn" +
                "k3j69WEtd0rgKKA+zVF4Wn7FsqUK9OCveq6Kfwr3AyowNuJ4lQKp3YAgpLVyRarcQMIRtAfdw6ujoPCw" +
                "ixJRYWaKag+fAamwBARlnT2fs+hveFQFEjHETIUG5VyypRWOJ1c3loskmNM0kWR1TFXH1fEF66awZVSU" +
                "lgNNpUoXQmbcAriCqsqUJBewh9Dry3/yNileIKbjNeLEC1T1KonpEJP5LFKlWMsVEFAYKlo4zVaAVNQD" +
                "6ryRk62v1n1QcoWapuTlMFdl21Ku1pwHEF/XeTcjZoIArvhUoAkncbkvs0whlE4UzFO5k1Ca6CTGZqf7" +
                "B0TPRFHW2BVM8hE3Dg0meKXQBaL3sXgxLdMX6+2Hn9tuzcQUNYBXGwCHTYDMHta2f/+57dA1ngCB5bew" +
                "XsjYKZfNIdF3LisQh8AJ4t0Dk+V6pF3kmcPw6NBxHdnnJBLMgiqJR5ZDym2OB9YqBQJf/7Rdcea6oFRR" +
                "UwqA/U7I9G4ggNO4MVee11qbHo5r+/6fqfLeQt272+g9ym11e4qGvQKeSvK1KKknqaiollsB0Z5HxZIO" +
                "ydP9IH+3s3LBQdIXScSk9q04e5tfHFORJbYnKjHLnYDGvyNEJ/Vt7OG+kaffEAJ+Z1QvUfDQ7DZzla5F" +
                "4y2eOUZ5EN2pmLuVwaD/pnfyU5d7W3bwxwHuNHQ8IdCx91a18IpE5Fs3Po0XX6JM5AOuL2+uby+6qL2o" +
                "klNk57mFZzgsspIOYo5K137r5MQBRWzLCRriHY/trHd7cfKuiTCRZRrN/yucbnQQxHAcpguO7OOD9U93" +
                "7vGhX6lr7e9srn0bQbKp5ePgU1Xp69qndX9SpRonMpJShdj3LE/3iOvOxh9BaqFeO1WuEUA1qWeUebyr" +
                "38ukVJVx4O9GJz21ZsEqens75AwB0wXVknNkXjXGIlOWqgc/zVlTILZVB72rFJOyKPCOzXvnUQsKxP1a" +
                "y5wYFNcbTYjYtgr5kqcFZj0waAQc7mUey2qHrGw51wh7oGui6Fzuwnn2CLnQ7CKhnx1HCLWqXS/eYtV6" +
                "rtZqlqk0m6iVWQsTq+66+ZybZaMKQ2ud+qJjePFucD0cdcUpao9H28DcE7pHZBj+MkAP9pNSWWO/m12Q" +
                "VfmpBA8xAfB+OPh5fNo7GV7AdHsJGcFq96OAwaU+ZqCoUXDI7YcuWdESDWhbrLqJmhZt8bFbZpU7enop" +
                "SYrgoUQOT/vc79ppnOv8co3SMcnHjU88e15mGybsWs+Ly4tBd12ek2FpsrWECjjpRysNl/yXSA3vVQ8F" +
                "B0SS9fng4rYrziifEsY2WaWwNEPaZcN0SYPyDgucQN7cjkaXiJGDhGfF6PtS8lYSzG4tVp9fvh+Me78M" +
                "b0BlsCchEwNbc+b8sCsfdF5tvTrrMUfVXtio21jpxcXUUW8UEF+z0SKGmhJm+yRat79LCWnC/rQ+jG29" +
                "hi/okwRRVybJqKZLxz+r0b8PnFKdJeqL7hiqjkJgrg7jIqi21b3C6mtQ/uKo/2Jt594Qcr58CEMfmrsi" +
                "znx38254OvruZHR9RhNdfnnUp9ldbuxaCkf9umSp3eERBW3ltqcmWdrq5bq5TyARlYWfTuuZdh2vbM4U" +
                "a/wwttMyScTr3f7lKWOMET8pRnlyQiscxoC1IxwX/qROXVpH5Fz/bEqV137YlKl78WPwuZpYvc+58S46" +
                "DNWu1+GIG2FWzWNRmeScluBUepJQDPE1ixKzUpOPuNuFZoDCCZ3WxBiCR/GfjwM4Hducy6/zHXqzCCUo" +
                "BOU8+NGYgYR8puS9n4yoRVasCIe7W1KPx42CRzAcWylw8IUDl+KjanCdBy92g1rXjVF3ONEFDWU2ck+b" +
                "K2sKFtXI+NHAu5a4/NDb5aXN8N1hYobutZsju+GCH6oHFts8QeccFw5lZBBN4YTGRH0+KT5Jw+bNBwfj" +
                "Rugmbbp6O5QHZDVUwNPoPTbpS8hDpkXtSoAqeH/tQklmPbWFnUEqVkLFaunmKJ2GRpjHnPJkmVE3m5NS" +
                "Qq6p2RXKc5XRVQ8KfQc7fpyH/8I8myS0rAblwXLo0oxUzSXLi3PjLyZc98dXgVV10HNjVh01zplIHnul" +
                "TdX7iPbkCP2rFafVdc8NOsp5UWTdvb3lctlBhd4xdra31Hd6j+5G9vrOMenmNYBxyPok0KhE14OKI0Dk" +
                "3x71vj3cfyNzHeHvzVwCG2fpBXXxdNFkF77ISeHK3JfVLy3Ybgh1iGS96+vLn6se4OT2zaDqAG6uYCaD" +
                "qvw/+fVseNEfXB8f+QX8HIxvRtfDq+NX9aWz4c3o+PsaRrfyuoHWrYXoe3U5vBjdHIfAOxr8MhrXfOb4" +
                "xyqL3rwbXw9uLm+vT0BoIBs09C7ennmkB9X1aK/fr1g7v+wPT3+tfvYHZ4PRmjn3s3d2Bu6at1HimX9b" +
                "4T2XVs3LPq+DqhzPn0PiEF2EjwX4pjzhB1KamfzBV/tpp9Nxtw5wXnjVH2j9WMesTdeu00UBVTGhB0n5" +
                "fo+rPuURtcJQQXzzzSeo8acO+3TktEyePJSwVt84OAOUqc5Kipo0eoJhxypRhTPAQIGgysF6OriLf5YS" +
                "/40CAkqDeO/4z0Pt0830HgIDWjge0jJ0WxzQdRmCPM8gdtri0NGn8vqmo/UiwqZbzZ+7UHzi8KtalvBk" +
                "N4HfY8nYI3dL9wjYXfo1oMVBG//jWjyvEvJ2SVEIJRW9gDhF/qFERqIBir/0OjGJsddv3/Dg1dhH5/Bb" +
                "8dt+Z3/3oLP/eysurYsYiZ4qvjd5UrDvkEO48K6R5ydXieRJFw8SXSMoEbfIByIm1AmVxqT7nhfaeg9L" +
                "4Nzjbl2Q5VH0P332MAy8XNisfVRBoLsOFJm9ozpP3JrX7szp0FV1Y46sdpmGCz09Xc+W8kxFeqqxShPu" +
                "HBrkssi70wLdNzKZi1ntWhhsCzjqzoa9gAb+hgF/8y9z4pYfRPLFLLTrR7xK87yDRor8ickHqgJ9QbOG" +
                "CIRdXFKLI5NsLkMdukKRUrpPKx6bFl/i0lEt7iMJ1FScMdncIfryJYwIsfbc/kZofwwIfc3HKF5NaSPl" +
                "rIaXAD1WYIQ+RkHdAm+gZPl1roe8Lz91NyTu+V3zVogrnWHxqMPm6ZCH5K8rrLso4TLOsk9RyVp9eUQj" +
                "WHlHXybR4JOGT1kGZLLRCrHNuxlR2zUYvIs9k6gIRRb1RPG66K6ApfDMoZeZHroikml2h7kZdui1djrk" +
                "n1S7ctWKB+uv0E1V0oMuvuEtjGmHDoPp2PCU9ecVKVxUxp+9OPsyqt40+OoLC1s9zaqnSfUk/87JqL9R" +
                "3vj06P/v85o/AR49bT+SKQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
