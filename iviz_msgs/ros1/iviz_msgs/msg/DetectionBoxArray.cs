/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class DetectionBoxArray : IHasSerializer<DetectionBoxArray>, System.IDisposable, IMessage
    {
        [DataMember (Name = "boxes")] public DetectionBox[] Boxes;
    
        public DetectionBoxArray()
        {
            Boxes = EmptyArray<DetectionBox>.Value;
        }
        
        public DetectionBoxArray(DetectionBox[] Boxes)
        {
            this.Boxes = Boxes;
        }
        
        public DetectionBoxArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                DetectionBox[] array;
                if (n == 0) array = EmptyArray<DetectionBox>.Value;
                else
                {
                    array = new DetectionBox[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new DetectionBox(ref b);
                    }
                }
                Boxes = array;
            }
        }
        
        public DetectionBoxArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                DetectionBox[] array;
                if (n == 0) array = EmptyArray<DetectionBox>.Value;
                else
                {
                    array = new DetectionBox[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new DetectionBox(ref b);
                    }
                }
                Boxes = array;
            }
        }
        
        public DetectionBoxArray RosDeserialize(ref ReadBuffer b) => new DetectionBoxArray(ref b);
        
        public DetectionBoxArray RosDeserialize(ref ReadBuffer2 b) => new DetectionBoxArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Boxes.Length);
            foreach (var t in Boxes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Boxes.Length);
            foreach (var t in Boxes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Boxes, nameof(Boxes));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Boxes) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Boxes.Length
            foreach (var msg in Boxes) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/DetectionBoxArray";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "763c1bc4114d55dc686522db8d85c44b";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71X308cNxB+7v4Vo/AQiI5rgTRFkVBFckmDRCBNaF6iCPl25+6s7NoX23uw+ev7jb3e" +
                "u6NU6UMJArE7nhnPzDe/dsKBy6CteWFvP32mqb1lXxQn//NP8fbDH89Jr/S368bP/c+TjVuLVptwTKcv" +
                "r84uL65PJxM6oV+2ie9fvb38+Ar0g/vop+fnODosCh+qpP4Nq4odLeK/YtoFJhWvA4vTZk66KooXtjUV" +
                "XmAC3MazL/pjxKGslfeIxKy2Kjx7CoovrZPQeDbeunTPOwtrXta2rQ5pKc/XpbwUxUNF8I6HxQ59CMpU" +
                "ylXUcFCVCopmFp7r+YLdfs0rriGkmiVXFE9Dt2Q/huDVQnvC75wNO1XXHbUeTMFSaZumNbpUCFvQDW/J" +
                "Q1IbUrRULuiyrZUDv3UIpLDPnGpYtOPX89eWTcl0NnkOHuO5bIOGQR00lI6VFyTOJhQxPToUAdqhT++t" +
                "P/hc7Fzd2H3QeQ4gBysoLFQQq/l2CTTEYOWf47InycsxLkGUGNdVnnYj7Rqvfo9wG2zhpS0XtAsX3nVh" +
                "YQ0UMq2U02pasyguEQpofSxCj/c2NJuo2ihjs/qkcX3Hf1FrBr3i0/4C4NUSBt/OEUkwLp1d6Qqs0y4q" +
                "KWvNJlCtp065rhCpdGWx81qCDSZIRWjwH0lrSw0kKrrRYZHTPcJyLUn/MGm5LuyNoirmbJGUrsu14uEN" +
                "fEHabp98RCuw7gjx/MYPZeE/bQEEp+RY0ghGKekOZGcoY5gJHGeOEeilKnkkBSHkqj/XkRfIkXU6y46p" +
                "iN1gYCj+bIGDM1Hvmu9HOQhTcpEjW4PSxsd8GuyHLyo1rW13c8uj2+GpG56+/Rjz16HLPgxAIce34rlt" +
                "vLx9XccdrbAZF9/xKD/d/Bjf+my/zzFaxbNtl8bSS89i07MGvbNhBcjQpgdJCFbapXk6hlZ2DMeRtzpQ" +
                "ZdmTsZILjfoClZheLNJquYQyzAOnjK9TKEGGyC6P5+MR3SzYJC7pILHxx1GhS3J6rqskKREehBX1zo0o" +
                "zA7Rgeo62ZwuQ/pBibMJuL0xnc2osy3diEN4cP2EsjTlwa7YQIO1IxlPvYp7ch1h8V7NJQF8wGz8LuoP" +
                "A/W/LAcZ7GzkwtaV4F1aTIUyl+PFfoXubjxeVZ1K0wsOGhOrUR2U9JVMqqpiEYNNmzUEvgWnErxBqSEr" +
                "8xP6QjciDmXMDWhJRZ9HhgdgcYzCnimmuOtoWtupZI+nWgEX8LIvnZ4OU6k3JWYtDJe+8mimGU49IuWc" +
                "6hB+cZn7u+JWlG6EIwKvdXNl0O8rOqwwNxtEZb/WX3gPJ3RQQXq3hRvYcbhCprxbq/EbsjAa4lHaZ80Y" +
                "oFVbRlPFTAw+h/rgZVj06PghTsgUx1ZujMPYzvZnNRankKyXSQvnklAyXpVfW5365ygOgDih76w/stLs" +
                "yhJ2VOVg+71xsb2T4uRwAgNcW4bWcY7iRrhieaQNQIIHpIaAjKBF7AQReg6iJTe6goc6NfmazRxv9yjN" +
                "u1ZSkN+isNg06YFOakqsJ4Zrn13VLieEThtOny8xNpI04zQDX0sqYGVOKVEUU2trwo/211ONJlJp9HA0" +
                "Nb+xvAwHv2ej+o0aIC0hu0Png1Mbc0t2ez9IOHuT+e9K4Gib/xgGys0/9cyf3iOdP9Mp8MjVF89HcS+R" +
                "yO5m9U9S9PZ6z+BWFdtq/4PccS0kYvSBrcKfQWs1K1Wjb6aMeLAvhLsdKIJxfwMSCFNpL3MPsibnC6Lh" +
                "uh5oiG9+6mQtqfOM+2+ys4urY3F//ZX2V0+Sj7OB5+BZpBxt8AjphJ6ueQRLUH7d4BHSCT3rKa/PL0+F" +
                "dEK/bVLQ2U/oOH/GYVlvOENyoVI5x5zMCWNnM88hMVym55mzjSzYLqRtT0KRyrS/KCaFfEWJ0CQ/s2ml" +
                "0aTO4DG71dSuON9TYisOvSFvkIiNMh1xzU3soH0xJcuKvwGqfgDLkQ8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            foreach (var e in Boxes) e.Dispose();
        }
    
        public Serializer<DetectionBoxArray> CreateSerializer() => new Serializer();
        public Deserializer<DetectionBoxArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<DetectionBoxArray>
        {
            public override void RosSerialize(DetectionBoxArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(DetectionBoxArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(DetectionBoxArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(DetectionBoxArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(DetectionBoxArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<DetectionBoxArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out DetectionBoxArray msg) => msg = new DetectionBoxArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out DetectionBoxArray msg) => msg = new DetectionBoxArray(ref b);
        }
    }
}
