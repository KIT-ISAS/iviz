/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/InteractiveMarkerUpdate")]
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
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerUpdate()
        {
            ServerId = string.Empty;
            Markers = System.Array.Empty<InteractiveMarker>();
            Poses = System.Array.Empty<InteractiveMarkerPose>();
            Erases = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerUpdate(string ServerId, ulong SeqNum, byte Type, InteractiveMarker[] Markers, InteractiveMarkerPose[] Poses, string[] Erases)
        {
            this.ServerId = ServerId;
            this.SeqNum = SeqNum;
            this.Type = Type;
            this.Markers = Markers;
            this.Poses = Poses;
            this.Erases = Erases;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal InteractiveMarkerUpdate(ref Buffer b)
        {
            ServerId = b.DeserializeString();
            SeqNum = b.Deserialize<ulong>();
            Type = b.Deserialize<byte>();
            Markers = b.DeserializeArray<InteractiveMarker>();
            for (int i = 0; i < Markers.Length; i++)
            {
                Markers[i] = new InteractiveMarker(ref b);
            }
            Poses = b.DeserializeArray<InteractiveMarkerPose>();
            for (int i = 0; i < Poses.Length; i++)
            {
                Poses[i] = new InteractiveMarkerPose(ref b);
            }
            Erases = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerUpdate(ref b);
        }
        
        InteractiveMarkerUpdate IDeserializable<InteractiveMarkerUpdate>.RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerUpdate(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.Serialize(Type);
            b.SerializeArray(Markers, 0);
            b.SerializeArray(Poses, 0);
            b.SerializeArray(Erases, 0);
        }
        
        public void RosValidate()
        {
            if (ServerId is null) throw new System.NullReferenceException(nameof(ServerId));
            if (Markers is null) throw new System.NullReferenceException(nameof(Markers));
            for (int i = 0; i < Markers.Length; i++)
            {
                if (Markers[i] is null) throw new System.NullReferenceException($"{nameof(Markers)}[{i}]");
                Markers[i].RosValidate();
            }
            if (Poses is null) throw new System.NullReferenceException(nameof(Poses));
            for (int i = 0; i < Poses.Length; i++)
            {
                if (Poses[i] is null) throw new System.NullReferenceException($"{nameof(Poses)}[{i}]");
                Poses[i].RosValidate();
            }
            if (Erases is null) throw new System.NullReferenceException(nameof(Erases));
            for (int i = 0; i < Erases.Length; i++)
            {
                if (Erases[i] is null) throw new System.NullReferenceException($"{nameof(Erases)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 25;
                size += BuiltIns.GetStringSize(ServerId);
                size += BuiltIns.GetArraySize(Markers);
                size += BuiltIns.GetArraySize(Poses);
                size += BuiltIns.GetArraySize(Erases);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerUpdate";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "710d308d0a9276d65945e92dd30b3946";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACt1aW28bNxZ+roD8ByJBEXsry46dZlvt+kGx5ESoY3ttOW1RFAKloSQ2o+GEM2NZ+fX9" +
                "zuFFM5aTFItNCqxbRBKHPDz36zwRw0RlpZ6tdTYXRWnx0RFvqqIUEyWqTL+vlNCZKBdKlCbXU5HJpSpy" +
                "OVWtJ1iVJf7RhSiUvVVWrIx9VwiTdVoOlF8f66SF7dcK0LKpElm1nCjbwdIIcKepBgpipdNUVIVyAEsj" +
                "ElWqaSn0TOhSLGQhlrooVCJkJqo8kaXqtCqdlS+e45b3Y8CkO0brXImFSROAAOy8srkBTDNzYIF7Ieeq" +
                "I8SwFEtPpsl4w81lvzcaCGPFT4PB5bh3Nnw7IBzdelcMs6lVS6AqU48AYZlbdatNBRaUhJLAgfBHxBVN" +
                "muOdz8RCzxdYAA8zMTO2do4RD2ADqU9qWBEuiZ5i3VHp5aCCGEggJbEz1bck0Brsj+GVGMDKDNhNVMqC" +
                "b6z/nYNUuU6NTAQQkqJYmCpNiJQZboJYTFWKnaW075Qt2oK4jg8wU1lAK3adsH6oUSGOxYFf9Kw/Fs9a" +
                "fqWEHCHPc1OqLt09r6SVWamApXHqaGwCvCG33JopxFon9MRga1YWkd3vVF6KqckKXdADMVmDAxkAdHDJ" +
                "G4c0SRNbZZIQNdZzPmkNAcvKaalvldv52+/C04nDl0QooeGXnDA23Fma24dg0DHAYTYRnWRVDSiMC/Mu" +
                "8caE7Y6XrUet4//x36PWm+tXXXGri0qm+oMstcnGy2Je7G9h/oisTC/V/swCaejLzBDfhzOxUJI4WuIh" +
                "q6CCUhpx0GZxOcKclYMyq0rIs4DiL8FtiNwAhobEHFTIWMnpQhAsSCzviF9N5Q5bNVVAxlkJ2fZMqWQi" +
                "p+8IgFONgkAwIMLsAkt2pQvVFmsAWcLuygglHKajUiR6NlOWFCSePoUm2Fv9gagAUQF/umZaWd77dKbv" +
                "QAQfecpkQ71oA7wZFOw1s8Vzh3zUMNOlhhMh7DuilxamDWc305k351zfwhBzA66QZxDWlCyQotOaK7ME" +
                "59ZONqREDIWhfsKRz1MzkWm63nh09t7Bp2/8uPePTny4vlxYU80X0Z+T92dXvjC2BNLF1OqccBM7/xbP" +
                "D8R0ATOdQmPI4P2Z2i4+OpUpe04KMOTPiUSQL6uUTbS0Ji3Ejl85fgZAM7id8uhQFHSUYPQgBHjiCrEg" +
                "EUU14e/A12qwUBaFmWoyXUirXHjCWP06rTfYOcDGNRkxvo/9KYJ6BudANhiRSHSRp3LtcWzA2TKLE3cI" +
                "YMPxL2emRZk4BXCqRRZ5XYIV0iYgqpTsoAllF2P2UnWrUgpQyxy08FPyr4WLwKAK/89VBoJYR0gokM/U" +
                "LJfQFwoz3g5r552xScQEW+pplUqL/XDJOqPt0Xrwf4w0w36XXbCaVsQ23ORjDSnJsC/Y85OU1fvWk9HK" +
                "7OGnmlOIDJc7RQWy6g7x0SUDRRd3/MMR1wFscEfhFsT/HV4b42exK3AJUFC5gVPZAeaX63LhA8mttFpO" +
                "oJMADAWjWPaUDj3drUEmtLtQ/8wE8A7i5o6/AjaLcImmPYT+JGV7reZgIDYilt1qij/eg/jcKNUTK+26" +
                "xa6Vr2w9OWVHyUrJEtFbmh8MkKVBOdgXU8htt0Q62YOXJTlRysQ+goI1uSwwamYVKKE8sk2KRsuJf655" +
                "L1m2gWX6s0isLtkhhg2t/1Qg1GYMd7Pv69EIZDgUkgDI5KXOvPsOJIAcGAhj3aDY+TPkrXfx2zp++/C1" +
                "KNjwL5IRxQVVanC1iT/9er/hPsVwpFGfJip8W33V5CV6eyIx/ohVADn9AaUZW948BkKqO8ALaa1ck0C3" +
                "gLAT7UEDYOHTIPawi8JRaeaKMhAfBynDxAmKPfs+ctFn+KpKhGNwmtINirykUWtcT2ZvLKdKUKpZKksA" +
                "Yaw6LqGnQxT3q2lZYXddms6dzLkWcGlVjJdrgpKR9/V1AJmdFI/h1vEYPuMx0nuVJnSJyX0giYEW9wA9" +
                "gEAFVbhgGw9Sdo9TxInGAU7bvYNy6Zqm2sdBjsnbCtyOlIcjPrvzxkbEBAZc8q0AE27ivF/muYJbnSho" +
                "KGOqEaQWOk2w2cn+Dp40VRRB9gSjfMQVRIMIXil1CU9+LB7PquzxZvvh57ZbMzFl7cDzrQOHzQO5Paxt" +
                "//5z2yFrfMMJLL+C9oLHTrisDql+5yIEUQiYQN59YbRcsbSHmHMYvjpwnE32OaAEtaBk4p7mkHCbfYKN" +
                "SAHAZ0Ftl6K5ciiDkEke0IwQ7F1nALdxha48rbV6PVzX9o0AxspbC5XxbqO3KLfV7Skb+orzlJhvWEmV" +
                "ScQiLrcCoH0PijkdAqn7QfZu5xV1AoqQJxGR2tfkbG1+cUx5ltiZqNSsdgMY/4wAndS3sYX7ip5+gwn4" +
                "nVPKRM5Ds9ksVLZhjdd4phipwvSdSrhmGQz6L3snP1EuRPacbTu401D3BEfH1hsz4jWxyBdwfBsvPkWm" +
                "yBdcXVxf3Zx3kYdRMqdIzwsLy3BQZOQOfA7QjXbr+MQORezICSrjXQ/trHdzfvK6CTCVVTZd/FcwXQ8h" +
                "sOE4tBkc2sfPNj/dvceHfqUutb+3yvblBMWtq1pUDmYVM2FXR20KlRhtHNeIURGyL14eLhY3JY6/giRD" +
                "RXcGTaFrkFzqOQUfb+23MsW/QT/wuVVSz6xZspRe3Qw5SEB7gTUhA52OFbLIlaUcwnd2NhiIHdVBESvF" +
                "pCpLPGMN371XiwJwv1Y7pwa59lYpInasQsjktoHZdA4aPocrmvu82iVFWy00PB/wmii6l8tx7kOCL9TE" +
                "AGRnd31Xs3Y9e8v1vXr9Iykr0XBRS7aWJkHTK1ahC7Nq5GKosTOfdwzPXw+uhqMuuhRpem8biHtA9nAO" +
                "w18GqMR+Uipv7HdNDNIq357ghiYOvB0Ofh6f9k6G59DdXkpKsN77IKBw6JuymiGvUbDJnbsuadEKZSha" +
                "Ld1Uzcq2+NCt8miRHl+KkyIYKaHDnT/3u3Ybhzu/XMN0TPxxfRRPnufZlgq7AvT84hwd01FdsfDVZChA" +
                "lwo9RT7WsMl/oRPKe9UdusQUaCh9HJzfdNEjQEgliG3SSmGpmbTHiuniBoUeZjgdeXkzGl3ATQ5S7huj" +
                "DMzIWokx7oxTmzcXbwfj3i/Da2AZ9EnI1EDXnDrf7ck7XcStl2c9pijuhY66jVEuzq2O0FT1gK9YaeFG" +
                "TQW1fRCs29+lmDRhe9pcxrpegxfkSYyoC5N4VJOlo5/F6J8HSinVioc211CCFHxzvIzzoNpW9wirL4D5" +
                "46P+442ee0UoeBARuj/Ug4Wf+e769fB09N3J6OqMurv88KhPTbzC2A0Xjvp1zlLRw40K2srFT42ztNXz" +
                "dXufQCyCgrhOtZ7jCVW3LruJ7ZEaPQzttEJj68Ve/+KUISbwn+SjPDqhLA79wNoVjgp/U5COpwec+meT" +
                "q7z2wzZP3YMfg83V2OptzvV5UWRwfRJTcfiN0Lfm/qhES5MUHUal0QHBKZ+2oBNZabIRN2loOijc0GlN" +
                "DMxTpsj/i3E4Ttc2e/SbeIfybIosFIxyFnyv5UBMPlOS28WUxSzzEjmd8XMmdb/vKLgjw76VHAcPHzgb" +
                "pworkOit2HVsXUFGBeJEQ05Ixe7HnjYn1+QsYu/4Xue7Frh899vFpW33TeTsuZwXpanrK3OXwXfXA4lI" +
                "lpEtcIwLlzIwsIYjV+HGVJ8Pig/isD0FYWfccN0kTZdyh/SAtIZyeOrBJyZ7Cn6Q893MBiiJ9yMYCjKb" +
                "9i30DFyxEiJWK9dQ6TQkwjSixlhhZkMFLdLULMaaml4hQ1c5jX2Q67uz4/tx+C80tolDnMg0chUaoJGo" +
                "OWV5/AZ5KGPuCkAeC8bsoOearWi81++Z0KgnDLfue7SHeulftZESRz/XqCsXZZl39/dXq1UHeXrH2Pn+" +
                "Sr/T+zQn2e8726RBbDjHXuuTh0YVah8kHeFE8e1R79vDg5doDk/xeb2QgMaBekm1PA2d7NLnORgHcwu5" +
                "McBg1SHQwZn1rq4ufo6VwMnNy0GsA64voSmDWASc/Ho2PO8Pro6P/AJ+DsbXo6vh5fHz+tLZ8Hp0/H0N" +
                "olt50QDr1oIDvrwYno+uj4PvHQ1+GY1rZnP8Ywyk16/HV4Pri5urEyAa0AYOvfNXZx7oszgt7fX7kbQ3" +
                "F/3h6a/xZ39wNhhtiHM/e2dnoK45mWpMe2t/T8Jzzq6agz8vg5iRFx8D4gDxlJODIw3OU/5CQjOTP3jS" +
                "n3U6HTd+gP3CsP5AAcgyZmm6op0mBpTIhDKEqgCf+CkPqBVaC+Kbbz6Bjb8VvQdcOavSBy8lqPGVB6eA" +
                "MtM5Bh4lN6Cg2IlKFf/YYCAoebAeD67lP4qJf2UBPqWBvLf9j586oEH1PnwDqjju1vLpNl4swOgMfp47" +
                "Ebttcejwo87QZtPRZhGe060WHxsuPnD5ZS1QeLSbh99iydgjN7G7d9gNABunxbM2/uN0vIgxeaciN4Ss" +
                "ih6AnaJA09kqaqP48deJSY29evWS268QTfMefip+O+gc7D3rHPzeSirrPEaqZxAY1OZBxr5GGOHcu4ae" +
                "71+lkvtd3E50taCE3yIboAHP2jOVmqUHnhbaip6XCz9uDoNAj7z/4bt9NyuE89o7FnR0zx1FcO9gbrw9" +
                "Qa/Nz+nSdZyeI7BdULnjTMsx3nWccjXV6J3AOyNoFpAgZ0benJYowBHMnM9q19xgW8BQd7f0BTjwKw34" +
                "LL7MjU98O5KHtJCub/QqWKpvLPIbJ9CTNOQ0mxMBsfMLqnJkmi9kSEXXyFMI1QdUiwe6dBVlB+4ol45x" +
                "dO2KRJ/BhEYh1j62v+Ha7x+EvBZj5K+mspgTsdbwEk6PFQihd1OQusAaKFh+rVGRt+YH50Tilh82J0Sc" +
                "7+ANq2adzT0if5JftoCXcrM9JHOWzYoS1/guEvViJcpt7Kd0HS2oPAcw2SiIWO1dp6jtygzexcZJWIRU" +
                "iyoj36/Z5A4EU3jqUNHMDl0qyTi7y1wzO1Rcux0yUcpgOXfFF+vH6Zy3Brx47FsavNbh6wzGY8tYNm9b" +
                "ZLBSmXxuiPbl3yeIWv8ovnNh47d5/DaJ3+Tf2yW99JPmrfeR/u/euXnU+hMzFe6pqykAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
