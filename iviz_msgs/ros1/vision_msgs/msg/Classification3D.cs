/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class Classification3D : IHasSerializer<Classification3D>, System.IDisposable, IMessage
    {
        // Defines a 3D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesis[] Results;
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all detectors, so it may be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud;
    
        public Classification3D()
        {
            Results = EmptyArray<ObjectHypothesis>.Value;
            SourceCloud = new SensorMsgs.PointCloud2();
        }
        
        public Classification3D(in StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.PointCloud2 SourceCloud)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceCloud = SourceCloud;
        }
        
        public Classification3D(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ObjectHypothesis>.Value
                    : new ObjectHypothesis[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ObjectHypothesis(ref b);
                }
                Results = array;
            }
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public Classification3D(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ObjectHypothesis>.Value
                    : new ObjectHypothesis[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ObjectHypothesis(ref b);
                }
                Results = array;
            }
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public Classification3D RosDeserialize(ref ReadBuffer b) => new Classification3D(ref b);
        
        public Classification3D RosDeserialize(ref ReadBuffer2 b) => new Classification3D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Results.Length);
            foreach (var t in Results)
            {
                t.RosSerialize(ref b);
            }
            SourceCloud.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Results.Length);
            foreach (var t in Results)
            {
                t.RosSerialize(ref b);
            }
            SourceCloud.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Results is null) BuiltIns.ThrowNullReference(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) BuiltIns.ThrowNullReference(nameof(Results), i);
                Results[i].RosValidate();
            }
            if (SourceCloud is null) BuiltIns.ThrowNullReference(nameof(SourceCloud));
            SourceCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += 16 * Results.Length;
                size += SourceCloud.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Results.Length
            size = WriteBuffer2.Align8(size);
            size += 16 * Results.Length;
            size = SourceCloud.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "vision_msgs/Classification3D";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2c0fe97799b60ee2995363b3fbf44715";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VXTW/cNhA9V79iEB9iB161ttPUCGAUaYw0BlInSNxeDMOgRO6KDUWqJLWu+uv7hhTl" +
                "deJDD43heFcU53E+3rxh9uhcrbVVgQSdnFNrRAh6rVsRtbPkVRhNrKu9ao+uOh3mBZIOBtZFap2NQlsS" +
                "dqLBBZ2stF073yeEmi4iwU6qoDdWScIbYNFykPLhkO463XYUdD8YwHi31VLxZyMabYCJwzZ6q3AMBTf6" +
                "VpHuxUbVVfVWCak8demjAvJrxn1oW71v/lRtfDsNLnbwI1zfzHGEKsWlOHIpoqDYiUgbZZUXEc7ydlX2" +
                "0r6uVY2nDceIExCvMNTiy4C9bozk1ik2mGUHD2q6RJK8+mvUPgdPwhhkI8Ihx6EHRzpSLyZqFKl+iFNd" +
                "BWWD87d92ITvPzht42vjRnk8x37b8lN19j//VL99+vUlhSjzuTmxCOdTFFYKL6lXUaQkcRSd3nTKr4za" +
                "KgMj0XMKcgqnQYW68AW/OZ0GlR0D59SBNH0/WuaYoqh79cAelswnGoSPuh2N8NjvvNSWt6+96BWj4zcg" +
                "rcqCDBfnL5mIQbVjBE1wkratVyJou8FLqkbk8OSYDWiPrj+6cHRT7V3duRXW1Qb8WbzIDIDX6u8BdWeH" +
                "RXiJw57lKGscgiwpHCfBiLR2i8dwAPqyL2pwoPI+QvgwxQ5EYTJshdeiMYqBW6QCqE/Z6OnBDrJN0FZY" +
                "V+Az4v0Z/wXWLrgc06pD8QynIYwbZFKH0l6SmimBtEYrG8noxgs/VWyVj6z23nCysYn7l0uDTzSXa3Vq" +
                "jjsduypEz+ipLLdaVt+Illsd0HSZmV92M0J9ZcmlVeqW5VzJWZ9YrB7Xp6IA4CPYRHbsldcts8atC2hu" +
                "VyVrunKgcyQhZUJC++9gkWggArMCwIGLc7T3CDYI9AH0oxt7YVfgpUxFs0jZIfIeIjcIqt250UgalGdE" +
                "dIBx7vM4JLzUEUsD8p9GBIjRJ6Ue5OaP9P0CPtV4Tp3aO684AqFNXYHvL54T1ylHfa+TEzluNLsGN5h1" +
                "W2GQDuSAOVISMCekpl8m3gtF5rgP70POVnMkIBY7zgBe2I2i6x9WRzd1tTZOsBuhhWvfSscel9AiS+j2" +
                "AH2mzhnJk6916J42FREhX64kusCGXOGBzZchBaUGyDL2HidCqbrlFQNb1hngxemQVGxrzj1QEvLSWgED" +
                "IckN/Gmgdh4jwbjmMHHHiIkHDMZo63WzdO/sCuoQSq2eYKYiqCckvBfTwu98Vhod+cR55Di/EVb/A8Rj" +
                "CX3hqbUy+rM6YD4cSVjvjwgDs0BJjLMP9zBhxxZOwzxZh4IMbsmxTa6ym2C7B3XVELu5OmHJE1rAK8cn" +
                "JtFy69XaYMDE7D0rEoLLRtl50WKi5m4+xN1DZiX7YkxwE+9zC5zIkuxwUH99ZTg+hwN+bOPoF8bvpAtX" +
                "mPWslJw8VGpJyCFQ2E9KKnSUPLnTEhEm/VFklN3g6RHQMpMyQHlKxuzT+VzoDNNCxq0yoYSqfSHE3F8z" +
                "X1JumDR1lQr1hqmA206mRFU1zhliOQm3jcZcllpYTMSLsCPyy4ufi1PZ71sUaYDtHr1bghJzSPCimSJu" +
                "WsXCu7uy/0sLvHq4/xQO8snfzZuvP4LON/QK9Sjdl95DTME1zux+gX+Ws3cwR4awIF64sc0/4I6HHOmU" +
                "fdRW4B/mgLaQKS1nRnyrkfWVAqViPC5AWWO54kPRIGcLX5ANP82FhvnuhbCgZOXJnDqli8urUw7/jI7m" +
                "ld/npTM6vt9z9CKtnOzs4aUzen6/h2uJlR939vDSGb2YV968e/+Kl87op90VqPsZnVblesCjrpTkUuR2" +
                "TpwshHHrdcBcTRve5+9r73q+iHi+Vs+pyG06H5RIwbdNNjov31Wa4CIrQ8B4xFDeqnJO60YbZ0fegog9" +
                "/79FGdUnBZ2bKXtW/Qs0cpeWGg0AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            SourceCloud.Dispose();
        }
    
        public Serializer<Classification3D> CreateSerializer() => new Serializer();
        public Deserializer<Classification3D> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Classification3D>
        {
            public override void RosSerialize(Classification3D msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Classification3D msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Classification3D msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Classification3D msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Classification3D msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Classification3D>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Classification3D msg) => msg = new Classification3D(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Classification3D msg) => msg = new Classification3D(ref b);
        }
    }
}
