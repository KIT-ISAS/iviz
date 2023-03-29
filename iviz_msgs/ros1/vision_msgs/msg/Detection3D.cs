/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class Detection3D : IHasSerializer<Detection3D>, System.IDisposable, IMessage
    {
        // Defines a 3D detection result.
        //
        // This extends a basic 3D classification by including position information,
        //   allowing a classification result for a specific position in an image to
        //   to be located in the larger image.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Class probabilities. Does not have to include hypotheses for all possible
        //   object ids, the scores for any ids not listed are assumed to be 0.
        [DataMember (Name = "results")] public ObjectHypothesisWithPose[] Results;
        // 3D bounding box surrounding the object.
        [DataMember (Name = "bbox")] public BoundingBox3D Bbox;
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). This information is not required for all detectors, so it may
        //   be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud;
    
        public Detection3D()
        {
            Results = EmptyArray<ObjectHypothesisWithPose>.Value;
            Bbox = new BoundingBox3D();
            SourceCloud = new SensorMsgs.PointCloud2();
        }
        
        public Detection3D(in StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox3D Bbox, SensorMsgs.PointCloud2 SourceCloud)
        {
            this.Header = Header;
            this.Results = Results;
            this.Bbox = Bbox;
            this.SourceCloud = SourceCloud;
        }
        
        public Detection3D(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ObjectHypothesisWithPose>.Value
                    : new ObjectHypothesisWithPose[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ObjectHypothesisWithPose(ref b);
                }
                Results = array;
            }
            Bbox = new BoundingBox3D(ref b);
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public Detection3D(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ObjectHypothesisWithPose>.Value
                    : new ObjectHypothesisWithPose[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ObjectHypothesisWithPose(ref b);
                }
                Results = array;
            }
            Bbox = new BoundingBox3D(ref b);
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public Detection3D RosDeserialize(ref ReadBuffer b) => new Detection3D(ref b);
        
        public Detection3D RosDeserialize(ref ReadBuffer2 b) => new Detection3D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Results.Length);
            foreach (var t in Results)
            {
                t.RosSerialize(ref b);
            }
            Bbox.RosSerialize(ref b);
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
            Bbox.RosSerialize(ref b);
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
            if (Bbox is null) BuiltIns.ThrowNullReference(nameof(Bbox));
            Bbox.RosValidate();
            if (SourceCloud is null) BuiltIns.ThrowNullReference(nameof(SourceCloud));
            SourceCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 84;
                size += Header.RosMessageLength;
                size += 360 * Results.Length;
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
            size += 360 * Results.Length;
            size += 80; // Bbox
            size = SourceCloud.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "vision_msgs/Detection3D";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7f3d8e29f3ab9853108801543aec1a5d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71YbW/cuBH+XP0K4vwh9mFXPdtXX5DCKJIs0hi4Jrkkvb4YhsGVqF1eJFIhKdubX99n" +
                "hqRWa296/VAncGyJGg7n9ZkZHoiFarRRXkhxuhC1CqoK2hrhlB/aUBYHxYH4uNZeqLugTE10S+l1RdRV" +
                "K73Xja4kb1luhDZVO9TarERvveZVbRrrOqaYgZcQsm3tLZHI+wzimQL0+OZ7VdGnKSch8buTKyWCZV7B" +
                "iqUSrQUDVRNBWONVupVykbAsitdK1nhd858C217SqaJ3dimXugVv5UuxsLCBsUGs5Q2xT6oosd70Flw9" +
                "PrNgbUsSeb1sFYtgl7/BZELXfsan+8q6TGs2tM5sW+1JROmUwOlDh+co/A9l8ZZZvE4Haf8PHdbvrFeX" +
                "V8kknsSGwZd2MGzdpb0TfnAuv9PBUZCyeJEWX9g72gLSgn2o2MEySFDLIFbKKMdmY+3ySeJQl6rE24pM" +
                "DiNBWdmKCg89aO0QhG2i7cGRbXxUxgiZeFroqLVTnwftsC+bLgaYdTCWh42D6OSGucESquvDpiy8Mt66" +
                "686v/B/fWW3Cy9YO9QnoB1ep64reiuL8//yv+NuHvz4TPtTx4Bg0kOxDkKaWrhadCpKtR6qs9Wqt3LxV" +
                "N6rFJtmRbaJtNz3CKScNfqKd23YjBh+dXtmuGwwFPQJNd2pnP3ZSnIteuqCrAbEMeuvgUCJvnOwUcceP" +
                "h22VqZS4WDwDjfGqGoKGQJyGTiFLERkXC1EMMOLpCW0QB+LyvfXHV8XBx1s7x7qiVBmliKHB2d4jIEhg" +
                "6Z/hsO+jliUOgZUUjkNcH/LaNV79kcBpkEX1tlojhox4twlrGxPyRjotkS/EuIIpwPUJbXpyNOFsmLWR" +
                "xmb2keP2jP+FrRn5kk7zNZzXkhn8sIIlNaf9ja5BCrQiJlWrlaH0XDrpNgXtikcWB6/I2CDCLnYN/iJ1" +
                "baU5a26RpIUPjrizW671o4XljfZIqhiZXwMLqPzcZDRaj5+jR2HQILXxe2G5zPiAoERICQN0cgBehI5t" +
                "MseYuKpGrlvEdBCyrpkTwGGa+BIYFRI+4PSLBRJ9QEhIJAPQZT100swRnDV7zsBus4iNyBK4fG2Htha9" +
                "csQRadBa+2nomR+nxZiF9AuFCFD1QakdA/3KzxeQqcQ7p2sHSCYNpG7LAkF/9qMgZ0Wtt4VgIyxlm2kQ" +
                "IBR6N7KFOWADCpRsgIyz4sWGaG8QPWNpY5XjrqQJoiuXJScNCtflD/Pjq7JoWitJDK4WWZKzBflnPPGB" +
                "LxPOMk3iv4xFqOYiTsnKOzMXbxHAjb7DF6ca5VirnjBVpByKZ2Qfjbi+UtgZKAgq6AeESCLtFCAYizNo" +
                "pOiorkbKSWkkuHpjCesoEsOoQSoRCf3+HJNoJ5YqlPulGpVbxkoBZKSMBWJ5FB/G2uctiokZz6h1w8qG" +
                "2GAoynpVa/Zek6T28bAe9YzjCXWIS38058hguzE5B+cl42xyifKKcvClZTyKFkY6Pg4S/N7ZufA4RfgN" +
                "Dahhi+Y2sDBSxfcSIhJ6Id8r5QgXqOwWxCyKDibv7e28k7/BWCOn6JMUCGd3Z4j+UWM4zOm7FMbW6ZEc" +
                "zoKhA+U2yh9k4Wicy7upjAmFDsQF+DvU3dhJTfbC5VSFDu9mYjMTX2bC2TDBG/FPQRwfLP9r//K/efko" +
                "J+Hl6dnVRJnHAvGHrqPI3WPfh+6aUc9Ay3X6HjEcxW1q7FIU3C+NBMUvA0qVM8x3S/etFIQoORzH+pPQ" +
                "SWddZYKjHXVHdLwbnzbj05dvI/7WdPtSasee91ILb5+3dic0Q3L9d43y0+23aCB25gMOwfuTRWwZIvhm" +
                "Z1FxQbhxJtEzJxJV9AT/h2di8fYVNV4LtFCGjku1AKYjpjT5wDQxoWfMDNWK6s4szQF0XKdXaSiJAx7a" +
                "QYSMAxZUn4D222Zix1uzXGObIQxOlZOB52vJsreiRV32AHz+kvh6/UXtYzCbarh/PnuSLRZrGXjfLyi/" +
                "8oB0yod8m1hPJ+6tHTf8bTfESxo/LnhOsAbjRqckUhiTzbgTG2vMfVWEJZgM7QcaHfZ0nSZt8OjkJ7BE" +
                "tPDELfsezDBCoVPybfRS4CHxUJWrciZu18pEqnh9AA48XaFNcXql67hz2z8QT5GUQ0lpTlD2MIOyzPGw" +
                "2Knk8oBB9qIRGzuIW1IIDy4NdTypZ7m4cw3WzrikRRZ7sC93KLAd2ltZ/y4KPI6rvzJOZ2dnIde25cud" +
                "ymKQqnKGvJnXOZtlG6Hakx80kjBO7gnZvzYO5HQ1tNJyogbiF1DGVag4NsAlFoE8ZXk4LDazUiwx+DpA" +
                "Q2uXM8abVm4IemrlK6eX4yCXROGoTZn5XaMVlPoO0OPkZgSFeBZfI2xbvyW1LStpkHO1OKkxatLNxrzV" +
                "n9QRNbrHNbUfg+H2RNWIlHdbNn6yF0JjO+/2mTMmjHqocgdboalxyA/VowWL3vGjnRApTllurWl+tc28" +
                "afVqHaL0NJxSW8+bovCy+jzoCHERVLklv3djQKPcIXW+p3U2tj8qH96MnSwggBsqAtFsxYm5OD3i0EzG" +
                "g6dGg8zAheTEIvgcsyS3uoaGOhb9VpkV3vYwzdcTkUF+480k0yI5OrKpMNEb1fqsqnY5IFIFSPHCtqGg" +
                "KWNP9IpC4fJKxJAoiqW1raCh0l8vNUCkRusnAGp+Mu+PH/6ShYpyX8NJPfYeiJ9HpSZ9zHITlB93OHub" +
                "6e/vwKdd+qcQkE7+QyK+fI9wvhLP4Y+cffx9FgsQ5DzM7L+P1jtKmkGtmmE1/UPsOAylmq1PV5D4b+iO" +
                "E8MqcDNGxKNdqt1HIHbGfgCKkzZ5vM8YhN4j6Q5ruE1yNLZPLwczl4g8Maaeios3H5+S+ufiOK38PS2d" +
                "i5MtzfEZr5xOaGjpXPy4pSFfYuVPExpaOhdnaeXVz2+f09K5+Gm6AmQ/F0+LfFNEFx7ZJW9kTGeOyRww" +
                "tmm8CpHgbXxunO1oTHYhdv9kipim6SAOCrp4pE2L/Kz4HidevMMHino2zLfpnAqtSUiCvEYgdnRZrVrV" +
                "MYLmdoolK/4DQ2z6UiUYAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            SourceCloud.Dispose();
        }
    
        public Serializer<Detection3D> CreateSerializer() => new Serializer();
        public Deserializer<Detection3D> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Detection3D>
        {
            public override void RosSerialize(Detection3D msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Detection3D msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Detection3D msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Detection3D msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Detection3D msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Detection3D>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Detection3D msg) => msg = new Detection3D(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Detection3D msg) => msg = new Detection3D(ref b);
        }
    }
}
