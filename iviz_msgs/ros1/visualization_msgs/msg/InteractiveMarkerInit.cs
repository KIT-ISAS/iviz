/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
    public sealed class InteractiveMarkerInit : IDeserializable<InteractiveMarkerInit>, IHasSerializer<InteractiveMarkerInit>, IMessage
    {
        // Identifying string. Must be unique in the topic namespace
        // that this server works on.
        [DataMember (Name = "server_id")] public string ServerId;
        // Sequence number.
        // The client will use this to detect if it has missed a subsequent
        // update.  Every update message will have the same sequence number as
        // an init message.  Clients will likely want to unsubscribe from the
        // init topic after a successful initialization to avoid receiving
        // duplicate data.
        [DataMember (Name = "seq_num")] public ulong SeqNum;
        // All markers.
        [DataMember (Name = "markers")] public InteractiveMarker[] Markers;
    
        public InteractiveMarkerInit()
        {
            ServerId = "";
            Markers = EmptyArray<InteractiveMarker>.Value;
        }
        
        public InteractiveMarkerInit(string ServerId, ulong SeqNum, InteractiveMarker[] Markers)
        {
            this.ServerId = ServerId;
            this.SeqNum = SeqNum;
            this.Markers = Markers;
        }
        
        public InteractiveMarkerInit(ref ReadBuffer b)
        {
            b.DeserializeString(out ServerId);
            b.Deserialize(out SeqNum);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<InteractiveMarker>.Value
                    : new InteractiveMarker[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new InteractiveMarker(ref b);
                }
                Markers = array;
            }
        }
        
        public InteractiveMarkerInit(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out ServerId);
            b.Align8();
            b.Deserialize(out SeqNum);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<InteractiveMarker>.Value
                    : new InteractiveMarker[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new InteractiveMarker(ref b);
                }
                Markers = array;
            }
        }
        
        public InteractiveMarkerInit RosDeserialize(ref ReadBuffer b) => new InteractiveMarkerInit(ref b);
        
        public InteractiveMarkerInit RosDeserialize(ref ReadBuffer2 b) => new InteractiveMarkerInit(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.Serialize(Markers.Length);
            foreach (var t in Markers)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(ServerId);
            b.Serialize(SeqNum);
            b.Serialize(Markers.Length);
            foreach (var t in Markers)
            {
                t.RosSerialize(ref b);
            }
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
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 16;
                size += WriteBuffer.GetStringSize(ServerId);
                foreach (var msg in Markers) size += msg.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, ServerId);
            size = WriteBuffer2.Align8(size);
            size += 8; // SeqNum
            size += 4; // Markers.Length
            foreach (var msg in Markers) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "visualization_msgs/InteractiveMarkerInit";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "d5f2c5045a72456d228676ab91048734";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VaaXMbNxL9vPwVKKtSljYUdTneRLv6IIuUzYqulegclUqxwBmQRDQc0MCMKPrX7+sG" +
                "MIcox6mttdd2WTMYoNHo83VDW2KYqrzQ07XOZ8IVFj964rJ0hZgoUeb6Q6mEzkUxV6IwS52IXC6UW8pE" +
                "dbYwKgv8p51wyj4oK1bG3jth8l7HkwrjY512MP1OgVqeKJGXi4myPQyNQDfJNFgQK51lonTKEyyMSFWh" +
                "kkLoqdCFmEsnFto5lQopXDlxTKsAiXKZykL1hBhgp3V4FWDSyZnyVOfyQfERHJgXrs2GkA5UZI5jYp+w" +
                "DuTOmC3nKWT6XmVrsZJgFKyVObGQWA0hTa1ZEHEQYQpeTHJaEGmwmiQgOS0z/qplpj/KQpucyMgHo1Nh" +
                "VaL0A6QFCmm5zHRCB8ApZK9T6rx4/YpYHoNbEuIpuFlIe6+s63WGOXaRSaEf1CWP/fZ7/NjpnPyP/3Qu" +
                "794eiwftyuoQ44Wbub0NLkixeqH2ppbkrfOpIV0Pp2KuZArFF5qGyWpYmvtdVo5n3MsbcrWqsDJ3U2MX" +
                "UDrkYEjCUIinCgkqmcwF0XKFWvbEr6b0i71AvcaXBhY1VSqdyOSeVVQbAhMizq4xZFfaqa5Yg8hCz+ZF" +
                "RSUupqVSpHo6VZbstVp9bqywD/pj11tu5J+2SUrLc19O9SMOwUte8rEna54Ae4cfvGOxBOmQkofeVJj7" +
                "HlTuTBfuMNW5cv5U+sEU+KqJD9reFKwPmMRMmQUkt/aquaHjExWm+ieuPsvMRGYw8crn2b+j19eeHh2L" +
                "1UfeMLemnM0rj6f4wM4+N7YA0+QlSzb47X+JV/simUuyFVjoTrWmMYuXJjKjjTkEkcfTEXF8WWaFSExe" +
                "WJM5sR1GTg5AaJoZWRwdCkdLKzdReQnHTile8DP4tRoilM6ZRMPLUmirmIeDsfn1OpeYOcDENfkSnsdh" +
                "FVG90JCWmdZMpNotM7kOPLbobHjFmV8EsnH5l/JRV6Re/d6wSKQF5CBtihMVkkIL8zuHoSu7m6kHlWGR" +
                "XCxxEP5arJfK+QCNI+HfTOU4DRsIaQTKScxiAWPhYOWdsLHee5oUS2kLnZSZtJhvbKpzml65Dv5V0XjY" +
                "PybBOJWUJDPspPPEKunIQoZ9wbGQVKw+iC3x261xB793tkYrs4txNYP/VFx4cwXX6nFplU8a7hib/d2f" +
                "sodNICWF7VIYEo+N8ep2BHYDL2ppEFq2cYSbdTE3PmY8SKvlJGPTh5lloPqSFr3caVDOmXQucxPJe4r1" +
                "Hn+FbF7RpTPtzqG8jL22nEGSmLi05kGnmBriSMihmZ5YadcdDrC8ZWfrnMMlmyarRm/Yf3RDVgvn6i9j" +
                "lpuhiRwVgZaUBPZ9XoR7cdCGlKZW4RgENrpkbjSchu+a55JzG6vj2p7o3HBMjBM6/y5xSpsz3Xre1zqg" +
                "ZoDCLkQuL3XuYlLS8awyhPHWcX08Q+J/rJ7W1dPHr8N+Lbp4hkpRjuBSQ55t5untQy13SuC9zmdOFJ9W" +
                "XxG2VIEe56ueKwBI4X5AAGMjjlcpkDApBCGtlWtS5QYRjqCn0D28OokKj7MoERVmpgh7hAyIfCexgrLO" +
                "XshZ9DM+qgKJGGImoEE5l2xpje3J1Y1lkARzmmaSrI65AowlhF2wbgpbJkVpOdBUqvQhZMb40wOqKlOu" +
                "PSxG6I1AOSfw/QIxHZ8RJ16IqVZZSpuYZcgiVYq1KoBiQ6CF02y1UJyIfay6bOVkLECCUDYEJQ/UNCUv" +
                "T7mCbSu5rk8elwRcF9yMDhMFcMO7gkzcaUGQRy6XCqF0omCeyu8EaKKzFJO97h8RPTNFWWNXMMtHeBTt" +
                "Q/BIoQtE7xPxYlrmL+rph5+bbs3EFI0FrzYWHLYXLO1hY/p3n5sOXeMJKzD8FtYLGXvlsjlQRcNZgU4I" +
                "mmDePzBbTAkv2DI+enKMI/ucRKJZEJJ4Yjmk3HYNWasUBAL+6XpwBlvOUpErKiCxYL8XM72vGrEbvIbD" +
                "Jp/VvzESjdt1Q5HIXAVvAQtDPzF4lJ/q5xQte8V6guS1KKkmqbiohjuR0F4gxZKOydO/kL/bWbngIBlA" +
                "Eh1S5ykjJfa2MDgmkCW2Jyozq51IJnwjQmfNaezhPnvwO4SA9yXhJQoemt1mrvJaNMHi+cSAB8m9Srla" +
                "GQz6b07PfiT8Q/6cbwa481jx1EV0Ma+x8JpEFEo33o0HXwIm8ga313e376+Ogb0IySmyc2fhGZ6KrKSD" +
                "mKPy2m+9nDigiG05MQ9qJ1C7OH1/dfauTTCTZZ7M/yuapNbvKzGc7IcBz/bJQf3q9z05DCNNrf0/i+tQ" +
                "RpBsGvk4+lQFfX35VNcnVarxIiMpVYRDzfJ8jVhXNmELUgvV2rnyhQDQpJ5R5gmu/iCzUlXGgZ8blXRs" +
                "mYi374ecIWC64FpyjnRVYSyWyhJ6oI5SiwOxrXqoXaWYlEWBb2zeO09KUBDuN0rmzABcbxQhYtsq5Evu" +
                "Fpi6YdAKOFzLPJXVDlnZaq4R9sDXRNG+XIVTrQGj495FRq89zwiVqsdBvMW68yms1Yap1JtowKyFSdVx" +
                "XXzOzaqFwlBa5wF0DK/eDW6Ho2NxDuzxZBoO94zuERmGvwxQg/2o1LI13/cuyKpCV8LRMbHgp+Hg5/H5" +
                "6dnwCqZ7mpERrHc/ChhcHmIGQI2CQ24/HpMVrVCAdsX6OFPTois+HpfLyh0Dv5QkRfRQYgcD0SMbu3Gu" +
                "C8MNTsckH98+CccLMtswYV96Xl1fDY5reE6GpcnWMgJwMrRWWi75T5EbnqseCw6IJOvLwdX7Y3FB+ZQo" +
                "dskqhaUe0i4bpk8alHdY4LTkzfvR6BoxcpApClyo+3LyVhLMbiNWX17/NBif/jK8A5fRnoTMDGzNm/Pj" +
                "rnzUrpp6c3HKJ6rmwkb9xEovPqaOTkeR8C0bLWKoKWG2z5L1848pIU3Yn+rN2NYb9KI+SRBNZZKMGrr0" +
                "52c1hu/xpISzRHPQb0PoKAbmajMGQY2p/hNGX4PzF0f9F7WdB0Nw3KGOTR/YvkGc+fbu3fB89O3Z6PYC" +
                "1uQ/HvWpd+eMraVw1G9KlsodblHQVC57GpKlqUGum/MEElFZ8Paw3Zn2Fa9s9xQb52Fq52WWide7/etz" +
                "ppgiflKMCuzEUji2ARtb+FOEnXpNaR2Rc/2jLVUe+35Tpv7DD9HnGmINPufbu6gwVLeJwxE3Qjvat0Vl" +
                "5jgtwan0JKMYEjCLErNSk4/khkvhVoDCDr3OxBhaD/DvxnE5bXsZ6D/Nd6jNEkBQCMp78JM2Awn5Qvl7" +
                "AYIwi2Wx5s6+853bJ+1GwS0Yjq0UOEyBOMxQfFQ1rl30Yt+o9dUYVYcTXVBTZiP3dBlZU7CoWsZPGt6N" +
                "xBWa3j4vbYbvHjMz9J99H9k3F0JTPR6xyx10znFxUyYG0RReaMzU55Piszxs3kFwMG6FbtKmx9sRHpDV" +
                "EICn1ntq8pdFuGapJUt9MFavpCRTd21hZ5CKlVCxWvk+Sq+lET6jozxZLqmadaSUmGsadgV4rpaAwwT0" +
                "/drx0zz8F/rZJKFV1SiPlpMa5UjVDFleXJpwMeGrv55oooNT32bVSWufieS2V95WfYhoz7bQvxo4ra57" +
                "7lBRzotieby3t1qtekDoPWNneyt9r/fobmSv7x1zRH3lsIxD1p8uGpWoeoA44gr3zdHpN4f7b6TTCX7e" +
                "zSWocZZeUBVPF012EUBODlfmuqx5acF2Q6RjJDu9vb3+uaoBzt6/GVQVwN0NzGRQwf+zXy+GV/3B7clR" +
                "GMDrYHw3uh3enLxqDl0M70Yn3zUo+pHXLbJ+LEbfm+vh1ejuJAbe0eCX0bjhMyc/VFn07t34dnB3/f72" +
                "DIxGtsHD6dXbi0D04KA6XL9fHe3yuj88/7V67Q8uBqP6cP719OICp2vfRolP/NmK3xlatS/7gg4qOO4+" +
                "RcQTuoo3yuTkMJIkxGQz+YPvf/Ner+dvHeC88Ko/UPqxjlmbvlyniwJCMbEGyfl+j1GfCoQ6sakg/va3" +
                "P+Em7Drs05b+2nZzU6JaXYR7A5S5XpYUNan1BMNOVaYKb4CRA0HIwQY+uIr/JCdbYhSq/BbzwfE/vWpf" +
                "yDTdQ2BACcdNWl7dFQd0XYYgzz2Ina449Pwp15x0VA8ibPpR96kLxWc2v2lkicB2e/FPGDL2yN/SPVns" +
                "L/1aq8VBF38Zi7sqIW+XFIUAqegD3bG7DyUyEjVQwqXXmcmMvX37hhuvxj7Zh7+K3/Z7+7sHvf3fO2lp" +
                "fcTI9FTxvcmzgn2HHMLAu8Fe6Fxlkjtd3Ej0haBE3CIfSJhRL1Rqk+6Hs9DUB1gC5x5/64IsD9D//N7D" +
                "2PDyYTNsOwkwYtcvRWbvqd4zt+aNO3PFvyERb8yR1a7zeKGnp3VvyS1Voqcao9ThdtAgw6LgTgtU38hk" +
                "PmZ1G2GwK+CoOxv2Ah6ACDg5uy+z41ZoRPLFLLQbWrxKc7+DWoqE69UHQoEB0NQrImNX11TiyGw5lxGH" +
                "rgFSiNVnTIsvcWmrDteRtNRUJ2O2uUIM8CW2CDH2qfmt0P50IfQ1HwO8mtImylsND2H1WOEgaarAIAUX" +
                "SpZf53oo+PJzd0Pigb+1b4UY6QyLJxU2d4fCSv7tCusvShjGWfYpgqwMokgn1IKV9/Q7PNT4pObTcgli" +
                "slUKsc37HlHXFxg8iz2TuIggi2qitAbd9a/liHA41DLTQw8imWe/me9hx1prp0f+SdiVUSsebLhCNxWk" +
                "B198w1sY040VBvOx4Sn1r1fkcFGZfvbi7MuoetPgq9+wsNXTrHqaVE+y0/kPvZF9MFMmAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<InteractiveMarkerInit> CreateSerializer() => new Serializer();
        public Deserializer<InteractiveMarkerInit> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<InteractiveMarkerInit>
        {
            public override void RosSerialize(InteractiveMarkerInit msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(InteractiveMarkerInit msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(InteractiveMarkerInit msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(InteractiveMarkerInit msg) => msg.Ros2MessageLength;
            public override void RosValidate(InteractiveMarkerInit msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<InteractiveMarkerInit>
        {
            public override void RosDeserialize(ref ReadBuffer b, out InteractiveMarkerInit msg) => msg = new InteractiveMarkerInit(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out InteractiveMarkerInit msg) => msg = new InteractiveMarkerInit(ref b);
        }
    }
}
