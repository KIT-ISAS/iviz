/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public InteractiveMarkerUpdate()
        {
            ServerId = string.Empty;
            Markers = System.Array.Empty<InteractiveMarker>();
            Poses = System.Array.Empty<InteractiveMarkerPose>();
            Erases = System.Array.Empty<string>();
        }
        
        /// Explicit constructor.
        public InteractiveMarkerUpdate(string ServerId, ulong SeqNum, byte Type, InteractiveMarker[] Markers, InteractiveMarkerPose[] Poses, string[] Erases)
        {
            this.ServerId = ServerId;
            this.SeqNum = SeqNum;
            this.Type = Type;
            this.Markers = Markers;
            this.Poses = Poses;
            this.Erases = Erases;
        }
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new InteractiveMarkerUpdate(ref b);
        
        InteractiveMarkerUpdate IDeserializable<InteractiveMarkerUpdate>.RosDeserialize(ref Buffer b) => new InteractiveMarkerUpdate(ref b);
    
        public void RosSerialize(ref Buffer b)
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerUpdate";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "710d308d0a9276d65945e92dd30b3946";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACt1aW2/bRhZ+Xv2KQYIi9laWHTvNtt71g2LJiVDH9tpyLygKYSSOpGkoDjMkLSu/fr9z" +
                "5kLScpJisUmBdYuIGs6cOfernopRorJSzzc6W4iitPjoibdVUYqpElWm31dK6EyUSyVKk+uZyORKFbmc" +
                "qc5TrMoS/+hCFMreKSvWxr4rhMl6HQfKr0900sH2GwVo2UyJrFpNle1haQy4s1QDBbHWaSqqQjmApRGJ" +
                "KtWsFHoudCmWshArXRQqETITVZ7IUvU6lc7Kly9wy/sJYNId402uxNKkCUAAdl7Z3ACmmTuwwL2QC9UT" +
                "YlSKlSfTZLzh9mrQHw+FseLH4fBq0j8f/TQkHN36sRhlM6tWQFWmHgHCMrfqTpsKLCgJJYED4Y+IK9o0" +
                "xzufi6VeLLEAHmZibmzjHCMewAZSnzawIlwSPcO6o9LLQQUxkEBKYmeq70igDdgfwysxgJUZsJuolAXf" +
                "2Py7AKlykxqZCCAkRbE0VZoQKXPcBLGYqhQ7K2nfKVt0BXEdH2CmsoBW7Dphfd+gQpyIA7/oWX8innf8" +
                "Sgk5Qp4XplTHdPeiklZmpQKWxqmjsQnwhtxya2YQa5PQU4OtWVlEdr9TeSlmJit0QS/EdAMOZADQwyVv" +
                "HdIkTWyVSULUWM/5pDMCLCtnpb5TbudvvwtPJw5fEaGEhl9ywqi5szJ3j8GgY4DDbCI6yapaUBgX5l3i" +
                "jQnbHS87nZP/8V/n7c3rY3Gni0qm+oMstckmq2JR7G/hTSamV2p/boExlGVuiOmjuVgqSews8ZL1T0Ej" +
                "jTjosqwcVc7EQZZVJYRZQOtXYDXkbQBDQ1wOKgSs5GwpCBbElffEr6Zyh62aKeDiTIQMe65UMpWzdwTA" +
                "6UVBIBgQYXaJJbvWheqKDYCsYHRlhBIO01EpEj2fK0vaEU+fQQ3snf5AVICogD9dM6ss73021/cggo88" +
                "Y7KhW7QBrgza9YbZ4rlDDmqU6VLDgxD2PdFPC9OFp5vrzNtyru9ghbkBV8gtCGtKlkfR6yyUWYFzGyca" +
                "0iCGwlA/4cUXqZnKNN3U7pxdd3DotRP3ztGJD9eXS2uqxTI6c3L97MeXxpZAuphZnRNuYudf4sWBmC1h" +
                "ozMoDFm7P9PYxUdnMmW3SdGFnDmRCPJllbJ9ltakhdjxKyfPAWgOn1MeHYqCjhKMPoQAN1whECSiqKb8" +
                "DHytBgtlUZiZJruFtMqlJ4zVr9d5i51DbNyQBeN54k8R1HN4BjLAiESiizyVG49jC86WVZy6QwAbjn8p" +
                "Gy3KxInfKRaxtAQfpE1AUSnZNRO+LrrspepOpRSaVjkI4bfkWQsXe0ES/l+oDNSwgpBEIJyZWa2gLBRg" +
                "vBE2zjtLk4gGttSzKpUW++GMdUbbo+ng/xhjRoNjdr5qVhHPcJOPMqQho4Fgn08iVu87T8drs4evakHB" +
                "MVzutBTIqntERpcGFMe44++OuB5ggzkKtyDy7/DaBF+LXYFLgILKDTzKDjC/2pRLH0LupNVyCoUEYGgX" +
                "RbFndOjZbgMyoX0M3c9MAO8g1nf8GbBZhEs07SHoJykba7UAA7ERUexOU+Tx7sNnRameWmk3HfarfGXn" +
                "6Rl7SdZIlojeUvtgfSwNzr6+jDZueySyT/hXEhJlSuwdKEaTswKX5laBDEofu6RltJz495r3kk0b2KQ/" +
                "i3zqil1h2ND5dwUqbcZw631fi0CgEiyHLF3qzHvtgD9ogWkwyi1ynRtDrnofnzbx6cPXQb9mXaAhCgoa" +
                "1OJnG3n69r7mO8Vt5E2fpig8rb9ithL9O+iLzzHnJy8/pLxiy33HyEdVBhghrZUbEuUWEHacfcgeVj0L" +
                "Ag+7KP6UZqEo5fCBj/JJnKBgs+9DFX2GR1Ui/oLNlF9QqCVd2uB6MnVjOTeCOs1TSVrHWPVc+k6HKNBX" +
                "s7LC7qYonQtZcObv8qgYIIkvIA8e12f9ZG1SPIErx2v4iSdI5lWa0CUm98EjRlbcA/QAAvVS4aJrPEi5" +
                "PE4RJ1oHOEn3TsnlZ5piloMcs7U1uB0pD0d8OufNjIgJDLjiWwEm3MRZvsxzBVc6VVBPxlQjMC11mmCz" +
                "k/09vGeqKGrsCUb5iOuFFhG8UuoS3vtEPJlX2ZN6++HntlszNWXjwIutA4ftA7k9bGz/7nPbIWs84QSW" +
                "X0N7wWMnXFaHVL9zUYEoBEwg7x4YLVca7SHOHIZHB47TxwEHkaAWlEA80BwSbrsrUIsUAHza03U5mSt+" +
                "MgiZ5AHNCAHe9QFwG9fjytPaqM7DdV1f9jNW3lqoaHcbvUW5rW5P2dJXnKdMvGYllSIRi7jcCYD2PSjm" +
                "dAie7gvZu11UVPcXITciIrWvwNna/OKEciuxM1WpWe8GMP4dATptbmML9/U7fQcT8D2nNImch2azWaqs" +
                "Zo3XeKYY6cHsnUq4SBkOB6/6pz9S/kP2nG07uLNQ6ARHx9YbU+ANschXbHwbLz5DdsgXXF/eXN9eHCP3" +
                "ogROkZ4XFpbhoMjIHfgcoBvt1vGJHYrYkVPUwbse2nn/9uL0TRtgKqtstvyvYLqOQWDDSWgqOLRPntdf" +
                "3b0nh36lKbW/sqb21QPxphGPg03F1NdVTXVZEkONYxlxKQL2pcrjpWFd0PgrSCxUYmdQE7oG2aReUOTx" +
                "pn4nU/wblAOfWwX03JoVi+j17YgjBFQXWBMyUOhYD4tcWcoefBOnxkDsqB5KVimmVVniHav37oPKE4AH" +
                "jUo5NUiut2oPsWMV4iU3CUzdJ2g5HC5hHvJql7RsvdRwe8BrquheLr655Qi+UMsCkJ3RDVyFeuzZW24e" +
                "VOcfS1OpJdFIs1YmQX8r1pxLs25lYaioM590jC7eDK9H42P0JNL0wTYQ94js4RlGvwxRev2oVN7a71oW" +
                "pFW+GcG9Sxz4aTT8eXLWPx1dQHX7KSnBZu+DgMKhRcpqhqRGwSB37o9Ji9aoO9FYOU7VvOyKD8dVHs3R" +
                "40tBUgQLJXS4yee+N27jWOeXG5hOiD+ua+LJ8zzbUmFXcV5cXqA5Om4qFh5NhopzpdA+5GMtk/wnmp68" +
                "V92jIUxRhnLH4cXtMToCiKcEsUtaKSy1jvZYMV3QoLjDDKcjr27H40v4yGHKLWLUfRlZKzHGnXFq8/by" +
                "p+Gk/8voBlgGfRIyNdA1p873e/JeF3Hr1XmfKYp7oaNuY5SL86lj9E894GtWWvhQU0FtHwXr9h9TQJqy" +
                "PdWXsa434AV5EiOawiQeNWTp6Gcx+veBUsqz4qH6GsqOgmOOl3ES1NjqXmH1JTB/cjR4Uuu5V4SCZw6h" +
                "10PtVviZb2/ejM7G356Or8+pkcsvjwbUsiuMrblwNGhylsod7kzQVi57GpylrZ6v2/sEAhEUxDWl9QJv" +
                "qKJ1qU3shzToYWhnFdpYL/cGl2cMMYH/JB/l0QmlcOj+Na5wVPibgnQ8PeDUP9pc5bXvt3nqXvwQbK7B" +
                "Vm9zrquLCoOLk5iHw2+EFjV3QyUamKToMCqNlgdO+ZwFfcdKk424oULbQeGGXmdqYJ4yRfJfTMJxurbd" +
                "jq/jHWqzGVJQMMpZ8IM2AzH5XEluDlMKs8pLJHTGj5TUwy6j4BYM+1ZyHDxn4FScyqtAordi15911RhV" +
                "h1MNOSEPexh7upxZk7OIneIHfe5G4PK9bheXtt03kbPnEl7Upa6LzM0F30sPJCJTRrbAMS5cysDAGo5c" +
                "hZtIfT4oPorD9sCDnXHLdZM0Xb4d0gPSGkrgqeOemOwZ+EHOt54EUAbvpy0UZOpmLfQMXLESIlZr10fp" +
                "tSTCNKLAWGM8Q9UsctQsxpqGXiE9VzlNeJDou7OTh3H4T7SxiUOcyLRyFZqVkag5ZXnyFkkoY+6qP54A" +
                "xuyg77qraLM375nSVCfMsR56tEc7518tOY1TnhtUlMuyzI/399frdQ8Zes/Yxf5av9P7NBLZHzjDpIFr" +
                "OMYu65OHxhWqHmQc4UTxzVH/m8ODV2gFz/B5s5SAxlF6RVU8zZfsyic5GPtyw7g1q2C9IdDBk/Wvry9/" +
                "jjXA6e2rYawAbq6gJsOY/p/+ej66GAyvT478Ar4OJzfj69HVyYvm0vnoZnzyXQOiW3nZAuvWgve9uhxd" +
                "jG9OguMdD38ZTxo2c/JDjKI3bybXw5vL2+tTIBrQBg79i9fnHujzOBXtDwaRtLeXg9HZr/HrYHg+HNfE" +
                "ua/983NQ1x5Ctaa6jb+n4T2nVu0Zn5dBTMeLjwFxgHiayZGRBuQpP5DQzPQPnuhnvV7PDRtgvLCqP1D6" +
                "sYxZmq5cp/kAZTGhBqESwGd9ygPqhKaC+NvfPoGNvxVdB1w5r9JHLyWo8acNTgFlpnOMN0puPUGxE5Uq" +
                "/lJjIChzsB4PruI/ion/aQIcSgt5b/gfP3VAA+l9OAaUcNyk5dNd/IAAUzI4ee5B7HbFocOPekL1pqN6" +
                "EW7TrRYfmyM+cvlVI0p4tNuHf8KSsUduOPfgsJv1tU6L5138x7l4EQPyTkVeCCkVvQA7RYFes1XUQPGz" +
                "rlOTGnv9+hU3XiGa9j38Vvx20DvYe947+L2TVNZ5jFTPITCozaOMfYMYwol3Az3fuUold7q4kegKQQm/" +
                "RTZA45yNZyq1SQ88LbQV3S4Xe9zUBVEeSf/jd/s+Vojljd9S0NE9dxSRvYcR8fawvDEqp0s3cVCOqHZJ" +
                "tY4zLcd412vK1UyjawLvjIhZQIKcFnlzWqH6RiRzPqvbcINdAUPd3dIX4MA/XcBn8WVufOobkTyPhXR9" +
                "i1fBUn1LkX9ZAj1JQ0JTnwiIXVxSiSPTfClDHrpBkkKoPqJaPLulqyg1cEe5boxTalch+vQltAix9rH9" +
                "Ldf+8CDktZwgeTWVxWyItYaXcHqiQAj9BgV5C6yBguXXGQ95W35sNiTu+F17KsSZDn5G1a6wuTvkT/KP" +
                "KuCi3CQPaZxlm6KUNf7giFqwEoU29lOijuZTngOYbJVCrPOuR9R1BQbvYsskLEKSRTWR79TUiQPBFJ44" +
                "1DLzQ5dEMs7uMtfDDrXWbo/sk3JXzlrxYP3knDPWgBdPeEuDn2/4CoPx2LKU+lcVGUxUJp8dnH0ZUW8r" +
                "fPxhhY1Pi/g0jU/yr+yMXrmJ8tYvjv7/flXzH9X6ooCJKQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
