/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class Classification3D : IDeserializable<Classification3D>, IMessage
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
    
        /// Constructor for empty message.
        public Classification3D()
        {
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceCloud = new SensorMsgs.PointCloud2();
        }
        
        /// Explicit constructor.
        public Classification3D(in StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.PointCloud2 SourceCloud)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceCloud = SourceCloud;
        }
        
        /// Constructor with buffer.
        public Classification3D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeArray(out Results);
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Classification3D(ref b);
        
        public Classification3D RosDeserialize(ref ReadBuffer b) => new Classification3D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results);
            SourceCloud.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Results is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) BuiltIns.ThrowNullReference(nameof(Results), i);
                Results[i].RosValidate();
            }
            if (SourceCloud is null) BuiltIns.ThrowNullReference();
            SourceCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Results);
                size += SourceCloud.RosMessageLength;
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "vision_msgs/Classification3D";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "fcf55de4cff8870324fd6e7873a6f904";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VXTW/cNhA9V79ikD3EDnbV2k5TI8CiSGOkMZA6QeP2EgQGJVIrNhSpktQ66q/vG1KS" +
                "14kPPTSG411RnMf5ePOGWdGFarRVgQSdXVBtRAi60bWI2lnyKgwmlsWqWNF1q8O0QNLBwLpItbNRaEvC" +
                "jtS7oJOVto3zXUIo6TIS7KQKemeVJLwBFi0HKR/WdNvquqWgu94Axru9loo/K1FpA0wcttN7hWMouMHX" +
                "inQndqositdKSOWpTR8FkF8y7n3b4m31l6rj67F3sYUf4cPHKY5QpLgURy5FFBRbEWmnrPIiwlnerua9" +
                "dKRLVeJpxzHiBMQrDNX40mOvGyK5JsUGs+zgcUlXSJJXfw/a5+BJGINsRDjkOPTgSEfqxEiVItX1cSyL" +
                "oGxw/qYLu/D9O6dtfGncIE+n2G9qfiq2//NP8dv7X59TiDKfmxOLcN5HYaXwkjoVRUoSR9HqXav8xqi9" +
                "MjASHacgp3DsVShnvuA3p9OgskPgnDqQpusGyxxTFHWn7tnDkvlEvfBR14MRHvudl9ry9saLTjE6fgPS" +
                "qizIcHnxnIkYVD1E0AQnaVt7JYK2O7ykYkAOz07ZoFhd37oNHtUOtFkOz4WHs+pzj3KznyI8xxlPcnAl" +
                "sJEchVMkiJDWbvAYjsFadkH1Dgw+gufvxtiCH8yBvfBaVEYxcI0MAPUxGz0+PkC2CdoK62b4jHh3xn+B" +
                "tQsux7RpUTPD0YdhhwTqMHeVpGpMILXRykYyuvLCjwVb5SOL1SvOMTZx23JF8ImecrVOPXGrY1uE6Bk9" +
                "VeNGy+IbsXGvA3otE/LLJkaoLyy5tErtspwrOckSa9TDsjQ3PmgIEjFJXJPyMgEmeSrp2oG+kYSUCQLt" +
                "fgBCokLTTx2Pky8v0M4DaCDAe+hFO3TCbsBDmaqVIFHoTq2R9hC5LVDs1g1GUq8845JIcMa5T0Of+2Bp" +
                "O/5TiQAJeq/UvdT8mb5fwrMSz6k/O+cVy4zQppyrxXXKUd/J40iO+8s24Aazbi8M0jHlIssUa1tKSkm/" +
                "jLwXQszhr+8iz1ZTKCAWe84AXtidog8/bE4+lkVjnIjPnlKo4du3kq+HlXNWI3R7gCxT64zkgVc7dE+d" +
                "aomQrzYSXWBDLnTP5stsgkADZJl2D/NhLr7lFQNb1hngxXFNKtYl5x4oCXlprYA5kOQG/lQQOY9JYFy1" +
                "ThQyYuS5gulZe10t3Tu5gjqEuVaPMEoR1CMS3otx4Xc+K02MfOI0aZzfCav/AeKphL7wsNoY/UkdMx9O" +
                "JKyPBoSBEaAkpti7O5hwYAunYZ6sw4wMbsmhTq6ym6C7B3dVH9upOmHJE3rAK8cnJtFyzaYxmCsxe8+K" +
                "hOCyUXZe1BikuZvXuHLIrGRfTAdu5iPugTM5Jzscl1/fFE4v4IAf6jj4hfEH6cLNpZmUkpOHSi0JWQOF" +
                "/aSkQifJk1stEWHSH0VG2R2eHgCdR1EGmJ+SMft0MRU6w9SQcatMmEPVfibE1F8TX1JumDRlkQr1iqmA" +
                "S06mRFFUzhm0Kry7qTTGsdTC0oouw4HILy9+np3Kft+gSD1sV/RmCUpMIcGLaoy4YM0W3t3O+7+0wKv7" +
                "+8/hIJ/83bT5w++g80d6gXrM3ZfeQ1PBNc7s0Qz/JGfveIoMYUG8cFGbfsAdDznSKfuorcA/zAFtIVNa" +
                "Toz4ViPrKwVKxXhYgLLGcsX7WYOcnfmCbPhxKjTMD++BM0pWnsypc7q8uj7n8Ld0Mq38MS1t6fRuz8mz" +
                "tHJ2sIeXtvT0bg/XEis/HuzhpS09m1ZevXn7gpe29NPhCtR9S+fFPHB41s0luRK5nRMnZ8K4pgkYr2nD" +
                "2/y98a7ji4jn2/SUitym00GJFHzJZKOL+buyAwtNVoaA+YjZvFfzObUbbJwceQ0idvzfFWVUlxR0aqbs" +
                "WfEv6HutrxENAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
