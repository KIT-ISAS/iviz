/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract (Name = "visualization_msgs/InteractiveMarkerUpdate")]
    public sealed class InteractiveMarkerUpdate : IDeserializable<InteractiveMarkerUpdate>, IMessage
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        [DataMember (Name = "server_id")] public string ServerId { get; set; }
        // Sequence number.
        // The client will use this to detect if it has missed an update.
        [DataMember (Name = "seq_num")] public ulong SeqNum { get; set; }
        // Type holds the purpose of this message.  It must be one of UPDATE or KEEP_ALIVE.
        // UPDATE: Incremental update to previous state. 
        //         The sequence number must be 1 higher than for
        //         the previous update.
        // KEEP_ALIVE: Indicates the that the server is still living.
        //             The sequence number does not increase.
        //             No payload data should be filled out (markers, poses, or erases).
        public const byte KEEP_ALIVE = 0;
        public const byte UPDATE = 1;
        [DataMember (Name = "type")] public byte Type { get; set; }
        //Note: No guarantees on the order of processing.
        //      Contents must be kept consistent by sender.
        //Markers to be added or updated
        [DataMember (Name = "markers")] public InteractiveMarker[] Markers { get; set; }
        //Poses of markers that should be moved
        [DataMember (Name = "poses")] public InteractiveMarkerPose[] Poses { get; set; }
        //Names of markers to be erased
        [DataMember (Name = "erases")] public string[] Erases { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerUpdate()
        {
            ServerId = "";
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
        public InteractiveMarkerUpdate(ref Buffer b)
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
                size += BuiltIns.UTF8.GetByteCount(ServerId);
                foreach (var i in Markers)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Poses)
                {
                    size += i.RosMessageLength;
                }
                size += 4 * Erases.Length;
                foreach (string s in Erases)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
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
                "H4sIAAAAAAAACt1aWW8bRxJ+XgH+Dw0bgaUNRcmS4020qwdapGwisqSVKCdBEBBNTpPseDg9nkMU/ev3" +
                "q6ruOUQpDhZrB1jl4Bxd1XVfPc/UMDJJYWdrm8xVXmT46ap3ZV6oiVFlYj+WRtlEFQujCpfaqUr00uSp" +
                "nponW8/wWBf4n81VbrJbk6mVyz7kyiXdJ1uCzL8Y2+jJFkFcG2BMpkYl5XJiMqx7pkZAPo0t6FArG8eq" +
                "zI0gLZyKTGGmhbIzZQu10Lla2jw3kdKJKtNIFwYYSpsUr15ip49jYJV9RuvUqIWLI2AB+rTMUge0biaY" +
                "wUOu56ar1LBQS8+uS3jBzWW/Nxool6kfB4PLce9s+H7AdMqLIzVMpplZglwdeyKI0jQzt9aVEEVBZCmC" +
                "CH/EYd7mvNr1hVrY+QIPIMxEzVzWBGTaA+KK4WcNyoicyE7xQjj1KjFBI6SbgqQa21tSbhP7Y6RFDsgS" +
                "B7ETpzqXPZt/5+BXr2OnIwWatMoXrowjYmeGvaAfVxZqe6mzDybLO4pkjx+I1GRAl+94rX3fYEQdq/3w" +
                "1KvgWL0gbcqzAhpl3Z67whwRBfNSZzopDIh1YqIui0A+dJhmbgoVtxg+cVibFHkl+Q8mLdTUJbnN6YWa" +
                "rCGJBBgAA6h3Qj3pFot1FBFbmdcCzHkIdJmeFvbWyNJff1OeY4a/JKaJGP9QVFNLauluH0RDcEDFMhOG" +
                "yeVaiJgiFmUUHA0QIlsCOf4f/z3Zenf95kjd2rzUsf2kC+uS8TKf53sb1LPz2aXZm2UgGxY0c6yC4Uwt" +
                "jCbhFnjLZmlgqE7td1h1wpsEADCXmQK6zeEOS4gdBuAIiYX2BC8UbvR0oQgZtJd21S+uFOjMTA3IEd8h" +
                "p58ZE0309ANjEEPJCQdjYuIu8Cxb2dx01BpolnDIosITwAlWq8jOZiYjc6nBT2EW2a39RJyAscADbTQt" +
                "M178fGbvwAjDPGfWYW20AMGO7O0ty8aLSELYMLGFRYghHrqqF+eug3A4s4n39NTewkVTB+FQ2FCZK1gx" +
                "OdDNjVtCgmtRElkUo/GI/yDkz2M30XG8rmO/xPkQ/uuI70OoaBIkFIvMlfNFHfkpU/igv3BZAdrzaWZT" +
                "IlFt/0u93FfTBRx4CgPigODBGss89FTHHGEpI1HsJ2YhCF3G7L5F5uJcbfsnxy8I1wyhqTg8UDnBCpoe" +
                "lIKgXSJ1RCovJ3wNwjMLeeo8d1NLfg3tFQvPIZsksL3D0gFWrsnDcT32YIL4DOGDvLMiJbJ5Guu1p7SN" +
                "asNfTgQKmAP8F3TgvIjEIsTcWLoF5KGzCIwVmmM5ES0paTc2tyamhLZMwQ2/pTCc+7QNxvDv3CRgiW2G" +
                "tANFTd1yCfuhpOQdtIHAe6FGAskKOy1jnQEAkdsmtL72KvqvykzD/hGHajMtSXbYzOcmMplhX0mWII2b" +
                "jwAcrdwu7s2c8mqgQIwXFJs75FQpI/Ij2ubvwmMX6CEkg41QN2zzszFu8x2FfUCFSR1izjbIv1wXC590" +
                "bnVm9QQmCswwN8p+zwno+U4TNZF+BJ9IXMAvKOtN/gzepEZMbO2iZIhi9uNyDjliJTLfraVU5eOLL6xi" +
                "O8l0tn6yxdGXNwWSU46lbKGsG7vhCZVbsl58GfeFrHMzaLHfIhCTuqje4thB6Z0CGqQ1ywyYoWq0Q0ZH" +
                "jyP/3vJa8nUHV/WwVJVdcsAMK55s/bsEs1nCmOuVX5FNkFO5E8UAbRMf4AMX4AjuwnS3mPZxDrXvXX0J" +
                "HYfLT1+Ni1qIFSuV1mBTLdG2eaC7j7UKKOFz+fVZzsLl6usWPFUuIEarm6qhkJQwoNJkI9RXKZP6GMhE" +
                "Z5lek3I30EiA7cEc4PbTYANhGSWsws0NFS0+XVKBSiCUnfZ8cqPfcGkKpG7InCoUytJkX2sQQKHAZVxh" +
                "wcRmsWZTZMK60hsQFFUJ5bQosbypWAkyc+4rpBqrcipLBywiMvuegjxRq6cI+niPOPIUnYKJI9rFpT7R" +
                "VNkYG4FAwoGeLJeUXEFSowAwkkYLgot/H7akzLOc4QR3VfWtIPOK+wDjy0LvfsRPkMElb0t4wl7cPug0" +
                "NQi4EwODZWItstjCxhFWByO4Q5CNDSeYXcV0H0oz0mJFHhW2QKQ/Vk9nZfK0AXHweYjMTVzRhHm5CXNw" +
                "DybNDpoQ330eAtrHFQHRizcwakhdFM4mEtsPkkmYXyAGI/6KCfRt2C4S1EF1LUh9Vdrn9BPshaqQezZF" +
                "Sm9PJmpVEwZfQHWkwpM+K4HySU0wmapECLMI7MjzAOMZb0wHwpYdP3lgyrwz0dBAFgaPk7WyqGhZMxBQ" +
                "rV8LlhqempLquRDEuPY8NpZ8lX79HYeFbF7S9CEPpRaxa/0QgF3SPxxTraa2JyZ2q50Kk38puE6aKzkU" +
                "+CkC3UMcuE+p6qIwY9m5FiaphRTcgnlHpTH9YCLpiAaD/uveyY9UTZHjJ5vR8DS0VSEqspdX5fWapOVb" +
                "RN6PHz5HxSk7XF1cX92cH6GWo5LQkBvkGXxH0OhKRghPoLjyb5EWhx61rSdowXcCurPezfnJ2zbGWJfJ" +
                "dPHfIZWxRZDEcTXcEMqPXzTuZevjg/CoqcC/uJ/3/QnLqJHRg7NVNbX0Z3XrUyUoER1Jq8Ls26HHutG6" +
                "bfKbsIaov09gM7QTilQ7p4zl48CtjvH/YCn43ejdZ5lbsrbe3Awlr8CUQTkRBAOvWnGVmoxqED9SqmlQ" +
                "26aLPlmrSVkUeMfWvrPZ7RLyfqNLjx1q940GR21nBsmWxxSunlS0wpE0SveFtkNmt1pYREYQNzG0N3f+" +
                "PBSFdGhsAtTeEfvSFR95MRdIzI9VbvfLXx6MNMq2pYswd6u63IVbtao6NPNJqFyG528HV8PRESYjcXxv" +
                "HXh8yBQQM4Y/D9Dl/WhM2gKQyQlZmR+I8ICVIN4PBz+NT3snw3MYcy8mk1jvflKwQExy2e5QGxl46vbd" +
                "ERnVCn0uJjxHsZkVHfXpqExrP/UkU35Vle8SRTKFlAeNDSVFhhcNesckpzDB8Xx66W0Ydmhzzy/OMcgd" +
                "Nc0Nly5Bm7s0GHEyYMtb/4n5LK81dxhgU1bienRwfnOEgQRSMaHskLGqjKZZu2yvkmEoT7HwGeb1zWh0" +
                "gUA6iHmmjVYzIU8mGQmQt6R3F+8H497Pw2sQGmxM6djB/sTG73b1nc3rtZdnPeaqWgzDlZWVknzoHWHY" +
                "61FfsSkj1LoSxvwwYgE4ouQ1YT+rt2MXaCCs1EviaOmWRNVUrYhBtOpXBIapcKvh6r240goxvNpSyqnG" +
                "YnmHx6+Ig6eH/ae1A3jDyPngJIyeaDSMWPTt9dvh6ejbk9HVGc2d+eVhnyaKucsa4jjsN4VMnRVPR2gt" +
                "d1hNIdNaL+LNhQqpC/Yig3Q7xxtqpaU0qscyDZ4Y3WmJwdqr3f7FKaOMEGcpjHmCQhMehpONPYQRv1Wl" +
                "KM8SxPWPe8Llh98/IFp580PtjQ3xVt7oZ9BoZLgJqmp9hJYwVue5rcaclewf3mYxfCEwX/NgOFpach45" +
                "GWlHMewCFiYOnqtjtBj5uEIge7dPE+o8iVZwiqIWUhP/vjftYJGfGc3zbKqClmmB2tD5QzJzfxCqeCIk" +
                "YZgjCx+WSKlP7Vzg1Xu5zJOl/aOGdGKhOJRz9/NVR0p2CifVdPvefL6R7fyIXpLZZrBnnnaliEYzLJNv" +
                "nnH4Q4DAJ6pv1BqcGcOujA0C4nSX+0O2z+fSh6l46NhG4nYr0pNypY4P5QVZEXUGdFgQueQ5pEJRuj7H" +
                "4NbAHxxRXqrnyjA8CCfTULdZyVCn29IMc4ruZYWTJu6iUfImVXpqGhpqfpPSgRVaCIEeP5DC/8TwnWTF" +
                "1VCr4KFTQNY7Fz5P36GsZfql35QDzqq86MkYGCcEzZ0mdEIVDufuB73HJv5fr9itT6yu0cEuiiI92ttb" +
                "rVZdVP5dl833VvaD3aOTnb2+OCwdKwc4jmt/CDQq0VGhYgkQ+TeHvW8O9l9jaj3F7/VCAxvn9iVNEeis" +
                "LFv6Kgnn2zzbbp21sA0R6jrW9a6uLn6qm4uTm9eDurW4voTVDOq+4uSXs+F5f3B1fBie4H4wvh5dDS+P" +
                "X7aenQ2vR8ffNdHKo1dt3PKwCtWXF8Pz0fVxFaVHg59H44YvHf9Q59/rt+OrwfXFzdUJKK4YACm98zdn" +
                "HvGLxgFwr9+v+Xx30R+e/lLf9wdng1GDU7nvnZ0Rq/eO11on2Y2/Z+E9l2ntM0yvl7rczx/DIpj4zJaT" +
                "Kn0aEPMFadJNfufvGZJutyvnJXBueNzv6DNZ8axiGRTQ+QbVQqHLoRbDl5DGI3qyFUYa6m9/+wNy/LaY" +
                "eGDPWRk/uCuhrT7vELPUiU1xQlPwRAzmHpnY8E1NgqKyIwuE8ODgUVL8dxkIN23yfVR4HGyfzt/3EDXQ" +
                "KPI4mcE7+HgC535IBzz62OmoA6GQZlP1osP6IUKrPM0fPSN9YPfLRj4JhLeh3+OZyw7lvPEetJxftsDV" +
                "iw7+4do+rzL4dkkBCiUZvYBIVY65eGZ4cuOP7U5c7LKrN695Mgz9tDfit+rX/e7+7ovu/m9PtqIyk2gS" +
                "2xnUBut5ULhvkWu4jm8Q6MdnseZ5Gw85pdfUiGnkC3QitfaCpSnuvueGlmLk5lOUnBqhKkAX8fDmfpQW" +
                "Un/jQxIC3RVQFAJdnIFvfhTQ+CKAdl1X3wNw7rugBkqcTKQvY67UTC2GNQjeyK051MjFlHesJRp9pDsJ" +
                "ZJ1GfOwouCyU8cDxDX+tgd/8S+35zA9F+ZQZSvZDaAOv9dNN/rYG9hKHGqiGqEg7v6CuScfpQocydo2i" +
                "hoh9yMb4SJo2kzpCoLknrU7hpfsMFU8YVeLh4yCtuL8BC+Utxih/XZnR8RbbED8D/NiAI/oKB8UO3IPy" +
                "6lc73/L+/eDhlrrll+1jLSmP8HFZu4/n0ZQH5Y9JELrkYBIlYMZ+RkVv9QUWT4U12nkAUMGP0VeaAptu" +
                "NVjsBzKg6ki3wqvYXZmOUJtRq+VHRHWpQUiVZxCt0exASlCmWnbz8/XQw+10yWup9uWqFxeZ/zaAC95A" +
                "GZ9dFw5frfheRSjZ8J76S5IEnqujP3f894XUvukG9cckCGrhcl5fTupL/RfPai/9kfnm91f/x58X/Qfe" +
                "1o73wyoAAA==";
                
    }
}
