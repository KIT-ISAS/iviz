/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class Detection3DArray : IHasSerializer<Detection3DArray>, System.IDisposable, IMessage
    {
        // A list of 3D detections, for a multi-object 3D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection3D[] Detections;
    
        public Detection3DArray()
        {
            Detections = EmptyArray<Detection3D>.Value;
        }
        
        public Detection3DArray(in StdMsgs.Header Header, Detection3D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        public Detection3DArray(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Detection3D>.Value
                    : new Detection3D[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Detection3D(ref b);
                }
                Detections = array;
            }
        }
        
        public Detection3DArray(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Detection3D>.Value
                    : new Detection3D[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new Detection3D(ref b);
                }
                Detections = array;
            }
        }
        
        public Detection3DArray RosDeserialize(ref ReadBuffer b) => new Detection3DArray(ref b);
        
        public Detection3DArray RosDeserialize(ref ReadBuffer2 b) => new Detection3DArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Detections.Length);
            foreach (var t in Detections)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Detections.Length);
            foreach (var t in Detections)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Detections is null) BuiltIns.ThrowNullReference(nameof(Detections));
            for (int i = 0; i < Detections.Length; i++)
            {
                if (Detections[i] is null) BuiltIns.ThrowNullReference(nameof(Detections), i);
                Detections[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var msg in Detections) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Detections.Length
            foreach (var msg in Detections) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "vision_msgs/Detection3DArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "05c51d9aea1fb4cfdc8effb94f197b6f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71ZbW/cuBH+XP0K4vwh9mFXPdtXN0hhFEkWaQxckzRJry+GYXAlapcXidwjKdvKr+8z" +
                "Q1KrtTe9fqht5EWihsN5fWaGPhAvRat9ELYRpwtRq6CqoK3xM9FYJ6To+jbouV3+gvUthXVlUbxVslZO" +
                "rPm/ojiYsAprlShVLTbObqyXrS9BEfnlpZGd6PRqHcRKGeVkUGAmwET7yPFWh7XopBlEJU2ta1BMRB13" +
                "1aJxtoPQXptVq4Q2mz6UxSJTni4uryb7iuL8//xT/PXTX14IH+rrzq/876OBoMunALGlq0WngoT0ko27" +
                "hsrKzVt1o1pskt0GGvDXMGyUL7HxM5lAZw3bdhC9B1GworJd1xtdkSmC7tTOfuzUBnbYSBd01bfSgd66" +
                "Whsib5zsFHHHH69+7ZWplLhYvACN8arqg4ZAAzhUTkkyJT6KotcmnJ7QBnEgLj9af3xVHHy+tXOsqxXi" +
                "YJQCjpOBpFZ3G6c8CSz9Cxz2fdSyxCGwksJxtReHvHaNV39EjoMsamOrtTiECh+GsLaGw+lGOi2X5FWP" +
                "KGhbcH1Gm54dTTgbZm2ksZl95Lg9439ha0a+pNN8Dee1ZAbfr2BJECJ8b3QN0uXATKpWKxMQq0sn3VDQ" +
                "rnhkcfCGjB1DmV2D/6X3ttIcrxTYhQ+OuLNbrnX9WGF5oz2iPkbmJCWg5UI12igItgMBAr5DsiJQciCq" +
                "u6DIplIsERcVUVcttNENxSFtWXLYtH1NCiHDNa9qg3DvmGLGmQ0721sikfcZxDMT9viNqujTlJOQ+LeT" +
                "K9jURpSwYqlEays2qI5eRcRTSDLhPqR6TaeSG5dyqVvwRrqJhYUNjA1iLW+IfVJFifWwseDq8ZkFa1uS" +
                "yGuEDYuQ0FHXQE063VfWZVpgFtaZLUEZ5YJTFAJ9FxMZwv9QFu+Zxdt0kPb/QGB8QBADsKJJPIkNgy9t" +
                "b9i6S3uHgHQuv9PBUZCyeJUWX9k72gLSgn2o2MGMMJSiW9xk7fJJSJFSlXhbkclHqK7wQAhje4L4hNAq" +
                "2vioTFC19TRFOmntgDDaETYn02XIh7E8bBwA7ANzgyVUtwlDWXhlvHUxVD9YAMzr1vY1wMf2rlLXFb09" +
                "RZp8yytU60x2+3r8HK0K3AlSoyzti/8yOwLYDeQVBmHgEOFAWJTNxDFXThjVwklByLpmTvDC1MISwRC2" +
                "pfJiAYv2QE4JR8CN6x4lcw4MrxngDOBlFoMQbgcyrm3fojorRxyRbq21X/oN8+PqMRYr+gcZj5j4pNSO" +
                "gX7m5wvIVOKdXdwh9kkDqduygOvOfhSEaVHrbcYNwlJRMg1wlBD6RrYwx/3WIQW0eDUQ7Q1AdsQQVjnu" +
                "SpoAhHP+O2mAEJc/zI+vyqJprSQxOC2zJGcL8s944gNfpoBmmsR/GbO9ZrSkmsY7MxdvgfONvsMXpxrl" +
                "WKsNBa9IpSaekX00JtBKYWegIKigH2AqibST6TAWF5qRoiMAi5QTDKKq/s5SS0CRGEYNUi6mJuFPsdbs" +
                "xBIaK0rArNwypiQaCCpsKOweWc4tycsWWWvGM2rdsLIhIrkiVFW1Zu81SWofD0MzFsEHCc8YG805Mthu" +
                "TM7Beck4Q8YCrygHX1su29HCSMfHQYLfOjuXRaeozYEGVBmjuQ0sjFTxGwkRuXvtscERLhC+FcQsig4m" +
                "H+3tvJO/wFgjp+iTFAhnd2eI/lFjOMzpuxTG1umRHM6CoQPlNrpEyMLROJd3UxkTCh2IC/B3KIexZE32" +
                "wuXUrB3ezcQwE19nwtkwwRvxT0EcHyz/a//yv3n5KCfh5enZ1USZxwLxh67j8eShfR+6a0atNS3X6XvE" +
                "cPSAU2OXouDCNBIUf+tRSZ1hvlu6p1IQouRwHOtPQieddZUJjnbUHdHxbnwaxqevTyP+1nT7UmrHnvdS" +
                "C2+/bu1OaIbk+u8a5afbp2ggdhoxDsH7LVxsGSL4ZmdRcUG4cSbRMycSVfQE/4dnYvH+Dc0nC0waxvMQ" +
                "zGAB0xFTajFhmpjQM2aGakV1Z5YaLjoOI3fq/mInjakJIeOABdUXoP22mdjx1izX2KYPvVPlpLP8VrLs" +
                "rWhRlz0An78kvl5/VfsYzKYa7m+En2WLxVoG3vcLys/ciZ7yIU8T6+nEvbXjJl6F7IR4SVP6BY/T1mAq" +
                "75RECmNuGHdiY40Gu4qwBJOh/UCjw56u00gDHp38ApaIFh5t5GYDZpgD0Cn5NnopcDd+qMpVORO3a2Ui" +
                "VZzTwIHHBbQpTq90HXdu+wfiKZJyKCnNCcoemn2WOR4WO5VcHjAxXDRisL24JYXw4NJkwiNRlos712Dt" +
                "jEtaZLEH+3KHAtuhvZX1b6LA47j6G3NLdnYWcm1bnqIr27Zp1kaAv5vXOZtlG6Hakx90tU4jUkL2b40D" +
                "OV0NrbScqIH4BZRxFSqODXCJRSBfRng4LDazmOq1kQ7Q0NrljPGmlQNBT6185fRyvO9IonDUpsz8rtEK" +
                "Sn0H6HFyGEEhnsXz2rb1W1LbspIGOVeLkxrjJo2Q81Z/UUfU6B7X1H70htsTVSNSPmzZ+MleCI3tvNtn" +
                "zpgw6r7KHWyFpsYhP9QGLVj0jh/thEhxynJrTdc8tpk3Ld1CRunpDofaet4UhZcVRtkIcRFUuSW/d7FG" +
                "o9whdb6ndTa2PyofXkGcLCCA6ysC0WzFibk4PeLdEhkPnhoNMgMXvi3VHnyOWZJbXUNDHYt+q8wKb3uY" +
                "5lu8yCC/8eaCL4KioyObai2NUa3PqmqXAyJVgBQvbBsKmjL2RG8oFC6vRAyJolha2woaKv31UgNEarR+" +
                "AqDmJ9di44c/Z6Gi3Ndw0gZ7D8RPo1KTPmY5BOXHHc7eZvr7O/Bpl/45XQXj5N8l4suPCOcr8RL+yNnH" +
                "32exAEHOw8z++2i9o6QZ1KoZVtMPYsdhKNVsfbrrwV9Dl0kYVoGbMSIe7e75PgKxM/YDUJy0yeObjEHo" +
                "PZLusIYbkqOxfXoLk7lE5Ikx9VxcvPv8nNQ/F8dp5e9p6VycbGmOz3jldEJDS+fixy0N+RIrf5jQ0NK5" +
                "OEsrb356/5KWzsUfpytA9nPxvMgXqnThkV3yTsZ05pjMAWObxqsQCd7HZ/4FAsZkF2L3T6aIaZoO4qCg" +
                "+3natMjPiu9x4g0nfKCoZ8N8m86p0JqEJMhbBCL/JkO1qmMEze0US1b8B9u95H+RGQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            foreach (var e in Detections) e.Dispose();
        }
    
        public Serializer<Detection3DArray> CreateSerializer() => new Serializer();
        public Deserializer<Detection3DArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Detection3DArray>
        {
            public override void RosSerialize(Detection3DArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Detection3DArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Detection3DArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Detection3DArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Detection3DArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Detection3DArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Detection3DArray msg) => msg = new Detection3DArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Detection3DArray msg) => msg = new Detection3DArray(ref b);
        }
    }
}
