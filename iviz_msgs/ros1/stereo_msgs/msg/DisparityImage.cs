/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StereoMsgs
{
    [DataContract]
    public sealed class DisparityImage : IDeserializable<DisparityImage>, IMessage
    {
        // Separate header for compatibility with current TimeSynchronizer.
        // Likely to be removed in a later release, use image.header instead.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Floating point disparity image. The disparities are pre-adjusted for any
        // x-offset between the principal points of the two cameras (in the case
        // that they are verged). That is: d = x_l - x_r - (cx_l - cx_r)
        [DataMember (Name = "image")] public SensorMsgs.Image Image;
        // Stereo geometry. For disparity d, the depth from the camera is Z = fT/d.
        /// <summary> Focal length, pixels </summary>
        [DataMember (Name = "f")] public float F;
        /// <summary> Baseline, world units </summary>
        [DataMember] public float T;
        // Subwindow of (potentially) valid disparity values.
        [DataMember (Name = "valid_window")] public SensorMsgs.RegionOfInterest ValidWindow;
        // The range of disparities searched.
        // In the disparity image, any disparity less than min_disparity is invalid.
        // The disparity search range defines the horopter, or 3D volume that the
        // stereo algorithm can "see". Points with Z outside of:
        //     Z_min = fT / max_disparity
        //     Z_max = fT / min_disparity
        // could not be found.
        [DataMember (Name = "min_disparity")] public float MinDisparity;
        [DataMember (Name = "max_disparity")] public float MaxDisparity;
        // Smallest allowed disparity increment. The smallest achievable depth range
        // resolution is delta_Z = (Z^2/fT)*delta_d.
        [DataMember (Name = "delta_d")] public float DeltaD;
    
        public DisparityImage()
        {
            Image = new SensorMsgs.Image();
            ValidWindow = new SensorMsgs.RegionOfInterest();
        }
        
        public DisparityImage(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Image = new SensorMsgs.Image(ref b);
            b.Deserialize(out F);
            b.Deserialize(out T);
            ValidWindow = new SensorMsgs.RegionOfInterest(ref b);
            b.Deserialize(out MinDisparity);
            b.Deserialize(out MaxDisparity);
            b.Deserialize(out DeltaD);
        }
        
        public DisparityImage(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Image = new SensorMsgs.Image(ref b);
            b.Align4();
            b.Deserialize(out F);
            b.Deserialize(out T);
            ValidWindow = new SensorMsgs.RegionOfInterest(ref b);
            b.Align4();
            b.Deserialize(out MinDisparity);
            b.Deserialize(out MaxDisparity);
            b.Deserialize(out DeltaD);
        }
        
        public DisparityImage RosDeserialize(ref ReadBuffer b) => new DisparityImage(ref b);
        
        public DisparityImage RosDeserialize(ref ReadBuffer2 b) => new DisparityImage(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Image.RosSerialize(ref b);
            b.Serialize(F);
            b.Serialize(T);
            ValidWindow.RosSerialize(ref b);
            b.Serialize(MinDisparity);
            b.Serialize(MaxDisparity);
            b.Serialize(DeltaD);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Image.RosSerialize(ref b);
            b.Serialize(F);
            b.Serialize(T);
            ValidWindow.RosSerialize(ref b);
            b.Serialize(MinDisparity);
            b.Serialize(MaxDisparity);
            b.Serialize(DeltaD);
        }
        
        public void RosValidate()
        {
            if (Image is null) BuiltIns.ThrowNullReference();
            Image.RosValidate();
            if (ValidWindow is null) BuiltIns.ThrowNullReference();
            ValidWindow.RosValidate();
        }
    
        public int RosMessageLength => 37 + Header.RosMessageLength + Image.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = Image.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // F
            c += 4; // T
            c += 17; // ValidWindow
            c = WriteBuffer2.Align4(c);
            c += 4; // MinDisparity
            c += 4; // MaxDisparity
            c += 4; // DeltaD
            return c;
        }
    
        public const string MessageType = "stereo_msgs/DisparityImage";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "04a177815f75271039fa21f16acad8c9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XbY/UNhD+nl9hcR+4hb09Cl8QFWrV0mtPogIdJ1UC0ZU3nmwMjh1sZ1/49X3GTrLZ" +
                "5aV8oF0JbteZN88888zkTLyiVnoZSdQkFXlROS9K17Qy6pU2Ou7FVsdalJ33ZKO41Q292tuy9s7qj+QX" +
                "xZl4rt+T2YvoxIqEp8ZtSAlthRQGhj2ODMlAc9EFErqRa1r0zrQNEd8WxR/5dz4uYPPKOERg16J1Gm6V" +
                "DgiTo8n64ram8VBTENKTaD1dSPWug02V7iHtHqZ2F66qAkVEF7dEVsSaZbUtdStNdhCEq9J53DpRyoa8" +
                "DOJcZ9kSwcNOrGXk3/vkbEN+TWrGkeBYhydCiaditzTiAv97/H9e5l/442dFIBucXzZhHS6v+Q75JnzX" +
                "V0gSObEm11D0+4W4QuyHG6t5ikJRizpU3jV9UBwkHIvX8FvdXiKLFSft0UNRCSTQlbicIbuO9Vy0ekcm" +
                "jAK3EPgFtzLaoixb540SndUxpHC61VZb5back/PWRdRdS2P2M7GRRqtJaPjdUVgcXe6G1trZF9W15WuF" +
                "mJWW2STb59J5aZEB2J/WMJD0ZU2KMXWdU39S9zmXdHJoKASuixWNtsuJcAC0kt9F7/DwLHvpI1BUIQUh" +
                "+aqddy1inguk/9EzsXGma2gsOwyFXChp1g6m6gZFsOJOILqzEC8zjFKzvBaui0ErvuET6PHn9RIhpkqJ" +
                "S9HI3SHag4DcjQLT60CgdB1KZB2DGNDu7KTax7Lj6ZELLmuDGnI98MdtaVpGtALaFmXOfRVGybLWtJEr" +
                "M6AvJQ22UFckJ6LOnGpFJsolw/D89d8PL6vb2b18NAmyPyiKp9/5U/z56vcnqIzK6MtEkppKWiW9Eugp" +
                "qWSUiRFqva7JXxjakIGSbFpOBD+N+5aRzGhh9AS0o0WDAfZMW4rZDbTYoEtKZssIHjzSh2aiPKQ06rIz" +
                "kmnUeaUti1ce7crWGUX0oSNbkrh+9gQyNlCJVG6YQlMhZGDeu34mig6QQu6ggH59c+PCD2+Ls9utu8A5" +
                "rcGXYxQZpYiadmDBwAHLwNC7l2+5gBNkieBOgdjS2RI/w0zAG2Kh1qEpmPFe7mPtcvdtAI9UfRguGRNK" +
                "3GWlu7OJZZtMW2ndYD5bPPj4FrN2tMt3ukBLK8NpCN0amYRg690GHaXEap/5z2ieR0avvPT7grWyy+Ls" +
                "ipMNIYYmlwZ/ZQiu1JIHA3doEaJn66ksS/3fwfKU9Ad8oWyBZwASFiWmIHgNBMxzt6+e7qXPH8zFg1m6" +
                "AljItYBuFaHmAU7mz16uOJ6gov+cif74AJRQJyoBjcjyQ6eDTl2c0jeaE5/7jLaGpE1MgTc1j5v0iO3k" +
                "6fQlQ2DPNSABuazwqaGSeHr8u6X7u0E57wnoUgYH7NdR9OP7q3e6vz82gBllv03x47Ei/jl23xppadgm" +
                "vmrhOsuM6QQxoShWHZ/2ln5Nabi2lfuSuQFSJ1A/xMFgq4wu45cssOSKarnRLrUcpkwaj2iPnolqSok9" +
                "qGTD+Xg+kNBc2K5Z5fJ5tw2D9lYrxPOJdjr+rHLJE9iGYWkwtAY08s6R2Bws6hT3MS9kKFqlQSrBl5fJ" +
                "8HJ4HBZl2xYp4XvXia3MQAn9hMAaC962tBUDKzjfSFznHQrLw86FC4wAH342OsSwCK7zJUEIe6jFWskl" +
                "Q6Mr7mFqpDbMVa1LLJ7tDoEsioF4xsiHVPw2HODaeVkTF1gdwYMWk6ohafGQNxN0YPoWEPbnC8mVlO+x" +
                "5467IsfNhrNz3o140phO0eWUoU6zVue6P0ZVliuNeag0rpgrFyb8Oj77aZxYkdqjgK46YxgL/ULKEaz2" +
                "kTI0Hr95mw1NFN7cgAHegqNih5qjHF7vklC+OQdwnrzcSxCb/R8MfrrZnpI5t0y/KoSWSl3hTQGrEisl" +
                "bh0WYm5L3hVs/zID+j4Tf9Wo2Kk6F+/mxTWiiLEHx2T339Y0ISrgOhR96fMbQ9+tjM/cepUmw/M58xYh" +
                "DPQZclvWJ/Kwc6TBzcYSE25JPn/kVblXe9qrPBUP0i6k0qIUxv1ZVAkDk81xiBr3aWPnefEfALRb9u9s" +
                "gMJzTLzGIW+pL4YcIC2fwp/npdCjwIDyvN+nyUlqfczOs8HlfuLy1rXfwyPm9VccHsiUR2v6Djn28glf" +
                "Ah7pe/+cgec7Yr+S13hgo4xIbRk1CqYyZMaZinnK+2h6KR4Y4Y6X2zswk0POvdzDGO8A+5aHML/S8/nB" +
                "0pU0gYbbpnIOa4o+FFGcs1F+VWE0z+YJf4dww/h2eaoDM0ljUaycM5jEy3wjvL38A71PilenEAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
