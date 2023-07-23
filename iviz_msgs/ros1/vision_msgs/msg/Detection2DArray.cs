/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class Detection2DArray : IHasSerializer<Detection2DArray>, System.IDisposable, IMessage
    {
        // A list of 2D detections, for a multi-object 2D detector.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A list of the detected proposals. A multi-proposal detector might generate
        //   this list with many candidate detections generated from a single input.
        [DataMember (Name = "detections")] public Detection2D[] Detections;
    
        public Detection2DArray()
        {
            Detections = EmptyArray<Detection2D>.Value;
        }
        
        public Detection2DArray(in StdMsgs.Header Header, Detection2D[] Detections)
        {
            this.Header = Header;
            this.Detections = Detections;
        }
        
        public Detection2DArray(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                Detection2D[] array;
                if (n == 0) array = EmptyArray<Detection2D>.Value;
                else
                {
                    array = new Detection2D[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Detection2D(ref b);
                    }
                }
                Detections = array;
            }
        }
        
        public Detection2DArray(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Detection2D[] array;
                if (n == 0) array = EmptyArray<Detection2D>.Value;
                else
                {
                    array = new Detection2D[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Detection2D(ref b);
                    }
                }
                Detections = array;
            }
        }
        
        public Detection2DArray RosDeserialize(ref ReadBuffer b) => new Detection2DArray(ref b);
        
        public Detection2DArray RosDeserialize(ref ReadBuffer2 b) => new Detection2DArray(ref b);
    
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
            Header.RosValidate();
            BuiltIns.ThrowIfNull(Detections, nameof(Detections));
            foreach (var msg in Detections) msg.RosValidate();
        }
    
        [IgnoreDataMember]
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
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Detections.Length
            foreach (var msg in Detections) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "vision_msgs/Detection2DArray";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "402071f61477de256df9f1aa45e6e4c8";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71ZW4/buBV+168gMA8zs2sr2aQYLFIU7SbT7c7DtmmSXoNgQEu0zaxEKiQ1tvLr+51z" +
                "KFmecbL70GQQxDJFHp7rdy4+Uz+oxsak/Fo9uVa1SaZK1ru4UGsflFZt3yS79Kv3WD/s8KEsip+Mrk1Q" +
                "W/4oirMZqbQ1eaepVRd856NuYokdQm9cmsip1m62SW2MM0EnA2IKRGwUijubtqrVblCVdrWtsWPG6nSq" +
                "VuvgWzAdrds0RlnX9aksrsedT67fvpudK4o//J//ip9f/+WZiqm+beMmPhIFQZbXCWzrUKvWJA3uNSt3" +
                "C5FNWDbmzjQ4pNsOEvDbNHQmljj4hlRgRwmbZlB9xKbkVeXbtne2IlUk25qj8zhpHfTQ6ZBs1Tc6YL8P" +
                "tXW0fR10a4g6/kXzoTeuMurm+hn2uGiqPlkwNIBCFYwmVeKlKnrr0tMndECdqbevfPzuXXH2ZueXWDcb" +
                "+MHEBQynE3Ft9l0wkRjW8Rku+0akLHEJtGRwXR3VBa/d4mu8JMOBF9P5aqsuIMLLIW29Y3e608HqFVk1" +
                "wguaBlTP6dD55YyyY9JOOz+SF4qHO34LWTfRJZmWWxivITXEfgNNYiPc987W2LoamEjVWOMSfHUVdBgK" +
                "OiVXFmc/krLFldk0+NQx+sqyv5JjFzEFos5mubX1l3LLOxvh9eKZs5CAlNdmbZ0BY0cQoGA7BCscZeaI" +
                "0baW3AkeyLurBtLYNfkhjizUqk/kOE1fgx5C3DIl6+DvrWzh0Iai/Y6E1vco5Esz+MTOVPRKVcALhSXb" +
                "6o0BXfgcWBCU8GplVOMrVqgVq4JFcknefgqpXtClZMaVXtkGTJpY/I0x7qeh86AQbfwXbPMSfgTMEKYi" +
                "nYTMK98DhMD8yu/hEyGM3+lmQcqyeJ4Xn/s9HcHWgtVoWMcc5BQlB+iiS814E7y0NCW+bUgnE1qSGijI" +
                "fU8om0HSiJiXpfqrTzjyobeBoJA02DQEGHDsaIDo0SubgKMDacy0XRrKIhoXfRCvuGHtRt+HCo7Ybr6G" +
                "J35K65ROXNam2k6vRWsI7aStO+1h5ahowCPATbm+NQE+BBBDZsoUx+RUqjceRkhK1zVTgpZntJSGsdMh" +
                "G91cQ4s9wEkjGmCmbY+stARM1owhDhG84JRFZkW0bH3fIAGaQBTh0I33v/Qd02OAnvIB/beClUr12pgj" +
                "Bf2Tn2/AU4nvbNbWB85/2jZlgVC4+p0i2BCpD049UMRAVWtAFYHgnW6gjvvZOTusej7Q3jvg2BSlLLKc" +
                "ypIA58YQC9rBWd4+Xn73rizWjdfERqzA2sjJ1TXZZ7rxgS1LQRXek+mvJPPXDEiUNvjkSCV6QOna7vEm" +
                "mLUJLJWAQUZzuWO00RQgG4OTiZyggnxAgszSUSRDWYzl046WMEJ2MqUxtM8o0Ix4YpokwKdD/OU8/HuB" +
                "8yNfQu1CgTcKtxqYKnI05Q7kzojo46z/Q4NIddMdtV2zsEmwkoAVAW7ZeuvMdZTLUO8IuFCQb/VdVudE" +
                "4HAwGwf3ZeUM4m4UexSDLzxnRtEwwvHLIMGv3T1mnmCokoAElKRE3Q4aRqjEToNFLhB7HAiEC4RrBRET" +
                "1kHkld8tW/0eypooiU2yI1ztr+D9k8QwWLD77MY+2Gk7jAVFJ4ptwlUt3rjU+zmPGYXO1A3oB2SchXjx" +
                "4SxMTvXQxX6hhoX6uFDBpxneqH8rovhg+T+nl//Ly5djEL59evVuJsyXAvGHpuMO4KF+H5prQdUrLdf5" +
                "vWA4yqy5sksFG1JsjxuKv/fIlMEx3cO+ryUgWBndcco/GZ3sKKvOcHQk7oSO++lpmJ4+fh32D6o7FVJH" +
                "+rwXWvj24aB3QjME1+clGp92X6OAOCq02AXvl2hSMgj4cuhQZuHIoRQueC+wi34UpbuL3FUiSEn4DhHe" +
                "RKlsJ52BQs+dEYuKh2WX/UNJwoxcIVOVi1y0WjIRQt+KOS/VzZq0bvYaGVFeconLdpECjmlJGTwyPZdq" +
                "QaVcFgp9CzV5B66eM5db3Z5TA+1Mrrk3sHLatuWsEp2c92IS9fJ+IJ7MlqPaHsIAdQX8crwm2o/miP4p" +
                "eqfraGgDctmcx6DSeTkwt95UgOCu2/3x1+HrQWBup8jQZA58edmghzacLYj3dQ8neyq5l+2AHJGbe1T1" +
                "lJ6osTeuzqHIfpRTTT76EGHNnQnoaLmfclITwAhT86S7rsntVURq+0U4wSo8LlAfSjXj+3GYAjfOam60" +
                "M2OBYcPkZbyLosF3aHR5KLGzDXt3NOFOqIPPeelT99zi4p4KdQ4eRfa12hkuUzg5ovlGGPgOzdg4jkIp" +
                "w7MQEVH940Z8EzcEeFNnyLnldqhM9Z1yxogLeaFb91zQw0fWADPWZtegTDuorESfUavB90d0uKCgem5l" +
                "WK1U3WdLQLTkfRNHPU8NFhGFydi5h3OgB2W5BhkfGgIo2HpBBS1uOq+FORAMxtKeloozmwddh3KNC7oo" +
                "MveJnoEc6VwqzcNwp6FaL9STcGmKbSMtQIujYpdB75RBpIWpcuQ6KVeffBMp1zD66YY6myGLS+9kwkEG" +
                "mkC19lIEEz3IBsOOGWYc/+Sa7WSSp8KdBwnoo+zaN/WvJBcwl/QXG9zd74VHSUb1THkfcqPahF7zfMvm" +
                "3RePF+rxJQ95aEDRLRuzpnY1OGkp8r57EwmV/85UXj6M0qa+SOkKeSErkQdMEzl16m+iNY6VZqQocBHN" +
                "uUUBnQqfQX+KEMy1sQw0cuAhoUPP9HlK3+6nljiPcKSV5Nlv7is/K9O3wzGB2u/cbzv48figoJzPIJcT" +
                "0mcp3MieSZ0SJRIvh9VM6QWrgZr2T5GbIu54GHjggzt34Hb6FAVOoQZAYj0PJZE8pbMsxlktYJsUezgi" +
                "hGV5MY5pFzQhWYn5gt/F8fTO1uDnwWlePnm48k3fujim/cZs4Bq5GiJ0QLvuGZxzZbW2ANUYqkdM+HZ8" +
                "Hcuq6yQ7MCprcZSYZ+hUTGjgPAqrPDflLLNQ72FYHAs+LgHJIf6J5jCxlHEWNm1M6WjQA5NFQnmqv1pt" +
                "mzxek2kk0R0ZARTlKybOR1X8eVyg/oWrGrVcqmqrnUMOaw3AzG0W0v3xExUipw1JlkROdvLbBQ8v848o" +
                "cjll5HGk+miOUPe1thW7fw+r3K4siooa/V+2XJxNoKd3f5xm+sl0Rwz9SKUGfAE2dBs4AThYDcmIa3xP" +
                "P6IQodmBt6+AAO+AUYnyrbTPvEkkJwYu+JZv2MUui+J/NZEp83kaAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            foreach (var e in Detections) e.Dispose();
        }
    
        public Serializer<Detection2DArray> CreateSerializer() => new Serializer();
        public Deserializer<Detection2DArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Detection2DArray>
        {
            public override void RosSerialize(Detection2DArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Detection2DArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Detection2DArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Detection2DArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Detection2DArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Detection2DArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Detection2DArray msg) => msg = new Detection2DArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Detection2DArray msg) => msg = new Detection2DArray(ref b);
        }
    }
}
