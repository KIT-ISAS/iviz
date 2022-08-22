/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Widget : IDeserializable<Widget>, IMessage
    {
        public const byte ACTION_ADD = 0;
        public const byte ACTION_REMOVE = 1;
        public const byte ACTION_REMOVEALL = 2;
        public const byte TYPE_ROTATIONDISC = 0;
        public const byte TYPE_SPRINGDISC = 1;
        public const byte TYPE_SPRINGDISC3D = 2;
        public const byte TYPE_TRAJECTORYDISC = 3;
        public const byte TYPE_TOOLTIP = 4;
        public const byte TYPE_TARGETAREA = 5;
        public const byte TYPE_POSITIONDISC = 6;
        public const byte TYPE_POSITIONDISC3D = 7;
        public const byte TYPE_BOUNDARY = 8;
        public const byte TYPE_BOUNDARYCHECK = 9;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "action")] public byte Action;
        [DataMember (Name = "id")] public string Id;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        [DataMember (Name = "secondary_color")] public StdMsgs.ColorRGBA SecondaryColor;
        [DataMember (Name = "scale")] public double Scale;
        [DataMember (Name = "secondary_scale")] public double SecondaryScale;
        [DataMember (Name = "caption")] public string Caption;
        [DataMember (Name = "boundary")] public BoundingBox Boundary;
        [DataMember (Name = "secondary_boundaries")] public BoundingBoxStamped[] SecondaryBoundaries;
    
        public Widget()
        {
            Id = "";
            Caption = "";
            Boundary = new BoundingBox();
            SecondaryBoundaries = EmptyArray<BoundingBoxStamped>.Value;
        }
        
        public Widget(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Deserialize(out Pose);
            b.Deserialize(out Color);
            b.Deserialize(out SecondaryColor);
            b.Deserialize(out Scale);
            b.Deserialize(out SecondaryScale);
            b.DeserializeString(out Caption);
            Boundary = new BoundingBox(ref b);
            {
                int n = b.DeserializeArrayLength();
                SecondaryBoundaries = n == 0
                    ? EmptyArray<BoundingBoxStamped>.Value
                    : new BoundingBoxStamped[n];
                for (int i = 0; i < n; i++)
                {
                    SecondaryBoundaries[i] = new BoundingBoxStamped(ref b);
                }
            }
        }
        
        public Widget(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Action);
            b.Align4();
            b.DeserializeString(out Id);
            b.Deserialize(out Type);
            b.Align8();
            b.Deserialize(out Pose);
            b.Deserialize(out Color);
            b.Deserialize(out SecondaryColor);
            b.Align8();
            b.Deserialize(out Scale);
            b.Deserialize(out SecondaryScale);
            b.DeserializeString(out Caption);
            Boundary = new BoundingBox(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                SecondaryBoundaries = n == 0
                    ? EmptyArray<BoundingBoxStamped>.Value
                    : new BoundingBoxStamped[n];
                for (int i = 0; i < n; i++)
                {
                    SecondaryBoundaries[i] = new BoundingBoxStamped(ref b);
                }
            }
        }
        
        public Widget RosDeserialize(ref ReadBuffer b) => new Widget(ref b);
        
        public Widget RosDeserialize(ref ReadBuffer2 b) => new Widget(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(in Pose);
            b.Serialize(in Color);
            b.Serialize(in SecondaryColor);
            b.Serialize(Scale);
            b.Serialize(SecondaryScale);
            b.Serialize(Caption);
            Boundary.RosSerialize(ref b);
            b.Serialize(SecondaryBoundaries.Length);
            foreach (var t in SecondaryBoundaries)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Action);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(in Pose);
            b.Serialize(in Color);
            b.Serialize(in SecondaryColor);
            b.Serialize(Scale);
            b.Serialize(SecondaryScale);
            b.Serialize(Caption);
            Boundary.RosSerialize(ref b);
            b.Serialize(SecondaryBoundaries.Length);
            foreach (var t in SecondaryBoundaries)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Caption is null) BuiltIns.ThrowNullReference();
            if (Boundary is null) BuiltIns.ThrowNullReference();
            Boundary.RosValidate();
            if (SecondaryBoundaries is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < SecondaryBoundaries.Length; i++)
            {
                if (SecondaryBoundaries[i] is null) BuiltIns.ThrowNullReference(nameof(SecondaryBoundaries), i);
                SecondaryBoundaries[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 198;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Id);
                size += WriteBuffer.GetStringSize(Caption);
                size += WriteBuffer.GetArraySize(SecondaryBoundaries);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c += 1; // Action
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Id);
            c += 1; // Type
            c = WriteBuffer2.Align8(c);
            c += 56; // Pose
            c += 16; // Color
            c += 16; // SecondaryColor
            c = WriteBuffer2.Align8(c);
            c += 8; // Scale
            c += 8; // SecondaryScale
            c = WriteBuffer2.AddLength(c, Caption);
            c = WriteBuffer2.Align8(c);
            c += 80; // Boundary
            c += 4; // SecondaryBoundaries.Length
            foreach (var t in SecondaryBoundaries)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Widget";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a71dfa7c0575b3eb185fffdf20b1768b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WW2/bNhR+1684QB6aDIm3JF3bBeiDYnuptzR2ba9AUBQBLR3LxGRSJSk7zq/fR0qW" +
                "5c1D97DEMGzqXD6e+1EplXtHcXc6GN49xL0evaeforJNHPc/Dj/3QT8/RI9vb8G6iGre9H7UfxgPp7EX" +
                "6Q0m3RZe4E1G48HdTc05P8y57AXIFm86jn/rd6fD8X2tebnHHQ5vp4MRyK/3yPH4po+ffgzOz23OaDgZ" +
                "tAx882+8YMjbNvd6+MddLx7fg/7uEL37od/9Hcxfosi69GFpM/vjBxYpG1qEv2i2cUwicVIriBipMpJp" +
                "RXWbgqOM9ZKd2VS6I22ZCvzs4Lo612Z8cx1T4k+HGJYTrVIBkEpknmvh3rwmm4icd0+NVEWvrUlEEYy7" +
                "1qVKQbjWjzTzZ0i2iRMnlgWnX762gGo5yTaK3v/Pn+jj5OaK/hbV6Ihgh78zJYRNpMIJmmtEW2YLNmc5" +
                "rziHUjCVAtdH2XagOF1IS/hmrNiIPN9QaSHkNAK7XJZKJsInRS55Tx+aUpGgQhgnkzIXBvLaICpefG7E" +
                "kj06vpa/lawSpkHvCjIKcSqdhEEbICSGhfXxHvQoVNLlhVegI/oy1vb8a3Q0Xesz0DlD8TRWkFsI563m" +
                "x8Kw9QYLe4XLfqi87OCSqzojlo4D7QGP9oRwG2zhQicLOoYLo41baAVAphVyJmY5e2DUQg7UV17p1UkL" +
                "WQVoJZTewleIuzv+C6xqcL1PZwskL/dhsGWGSEKwMHolU4jONgEkySUrR7mcGV+BXqu6Mjr61QcbQtAK" +
                "qcG/sFYnEplIaS3dYlvUIS0PaLRnKst/di0cjMmwTxLMF76lSM9DL/v6mRuGG4VI+NSXmyenNV8GWcSF" +
                "tJFb3Q5FI41qaASiTyW8NCrg7uReykGYsm0h1IITUtmQrcZ++IIeCSbvuduMn8fmtGlOTy9j/i50Wx+a" +
                "RKGC9uK5b7x/+raLOwbNshN9x6Ptaf3sI7FZANWdGCimOWXNadacxHNZJFfyqTKptS8ObbYEYcYQ3+d8" +
                "5sRpc4np8sQvUw/1jYeKgVaBt18GHT/dB2EMa4VpvmSBMsfiaDShmErDSdW6U6wiRrGg16WjVLMlpX3/" +
                "LMWfgGTMRK8tigJg2FBGKJtX5QcyVI65k3VOab1gVUn5mRZWUVheMiEjM5lWmr4qG2VBtXOn5OYXmIl5" +
                "XtlcXYaWBYjRVbGfdGgwp40uae0dwsHUO1PTjBu7wkh3Wp/6hVlDHJgPCIu1IvNNYx229Xc75XlSfbAY" +
                "65eXaP/d7OArT/QXDFURSSMLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
