/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Classification3D")]
    public sealed class Classification3D : IDeserializable<Classification3D>, IMessage
    {
        // Defines a 3D classification result.
        //
        // This result does not contain any position information. It is designed for
        //   classifiers, which simply provide probabilities given a source image.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesis[] Results { get; set; }
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all detectors, so it may be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Classification3D()
        {
            Results = System.Array.Empty<ObjectHypothesis>();
            SourceCloud = new SensorMsgs.PointCloud2();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Classification3D(in StdMsgs.Header Header, ObjectHypothesis[] Results, SensorMsgs.PointCloud2 SourceCloud)
        {
            this.Header = Header;
            this.Results = Results;
            this.SourceCloud = SourceCloud;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Classification3D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Results = b.DeserializeArray<ObjectHypothesis>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesis(ref b);
            }
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Classification3D(ref b);
        }
        
        Classification3D IDeserializable<Classification3D>.RosDeserialize(ref Buffer b)
        {
            return new Classification3D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            SourceCloud.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Results is null) throw new System.NullReferenceException(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) throw new System.NullReferenceException($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (SourceCloud is null) throw new System.NullReferenceException(nameof(SourceCloud));
            SourceCloud.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in Results)
                {
                    size += i.RosMessageLength;
                }
                size += SourceCloud.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Classification3D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "fcf55de4cff8870324fd6e7873a6f904";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXW/bNhR9F5D/cFE/NClsbUm6LghgDF2DLgG6tECzvRRFQEnXFleKVEnKmffrdy4p" +
                "OU6bAXtYFySxRfEe3o9zz+WMLnilLQdSdHpBtVEh6JWuVdTOkucwmFgWs2JGN60O4wI1DgbWRaqdjUpb" +
                "UnZLvQs6WWm7cr5LCCVdRYJdw0GvLTeEN8Ci3UHsw5zuWl23FHTXG8B4t9ENy2elKm2AicPWesM4hoIb" +
                "fM2kO7XmsiguWTXsqU0fBZBfCe5D2+Jt9QfX8XLbu9jCj/Dh4xhHEIubliXyRkVFsVWR1mzZqwhnZTtP" +
                "e+lQl1ziaS0x4gTEqwzV+NJjrxsiuVWKDWbZwaOSrpEkz58H7XPwpIxBNiIcchJ6cKQjdWpLFRN3fdyW" +
                "RWAbnL/twjp8985pG18ZNzQnY+y3tTwdFMv/+Oeg+PX9L+cUYpNPzqk9QETvo7KN8g11HFXKkwTS6nXL" +
                "fmF4wwZWqpMs5Cxuew7lRBn85owaFHcIklYH3nTdYIVmTFF3/MAelkIp6pWPuh6M8tjvfKOtbF951aH0" +
                "M9kWkFm24MPVxblwMXA9RDAFJ2lbe1ZB2zVeUjEgjadIIX8uZjd3boFHXoM5u8Nz7eEs/9mj4uKnCuc4" +
                "41kOrgQ2ssM4pQEX0totHsMRiCsucO9A4kN4/m4bW1BEaLBRXqvKgA+BamQAqE/F6OnRHrK4fU5WWTfB" +
                "Z8T7M/4NrN3hSkyLFjUzEn0Y1kggNo6N1VC1TSC10WwjGV155beFWOUji9lryTE2SedKRfCJtnK1Tm1x" +
                "p2NbhOgFPVXjVjfFNyPkRgc0XObkl50s7HxpyaVlanfruZijOIlSPS5OU/uDieCR8MStUmpGwCRSJd04" +
                "MDiSapoEgabfAyFVofXHvsfJVxdo6gFMUKA+VKMdOmUXoGKTCpYgUeuO58h8iNIZqHfrBtNQz15wSSU4" +
                "49ynoc+tsOs8+VepACF6z/wgN7+n71fwrMRzatHOeRaxUdqUU8FQqjHqe5HcEjYjWyvQQ4i3UQbpGHOR" +
                "xUoULiWlpJ+3shdyLOHP7yPPVmMo4JZ4LgBe2TXTh+8Xxx/LYmWcii+eU6jh27cTsccV9GDSJPR8gD5T" +
                "6wy6TSEe9FCdyomorxcNesGGXOte7HdDCkoNkN3Ye5wSU/2trBjYitoAL27nxLEGo1oGSkLeNVjAQEii" +
                "A38qSJ3HSDCumicWGbWVAYMxWntd7Xp4dAWlCFO5nmCmIqgnpLxXGCZTsdNZaXTkE8eR4/xaWf0XEE8a" +
                "qIxMrYXRn/hIKHHcwPpwQBiYBNxgnL27hwl7tnAa5sk6TMigVzPUyVVxE4z3oC/3sR3LE3Z5Qht4dnJi" +
                "ki63WqwMpkvM3osuIbhslJ1XNSZqbug57h5pUn81I6SfD6UNTiWMXMaj8usrw8kFHPBDHQd0y5jFvXTh" +
                "CpPXcvJQqV1C5kARP7EInOPkyZ1uEGGSICbDdo2nR0CngZQBpqdkLD5djIXOMDXE3LIJU6jaT4QYW2zk" +
                "S8qNkKYsUqFeCxVw28mUKIrKOYNuhXe3lcZQbrSyNKOrsCf1uxc/TU5lv29RpB62M3qzC0qNIcGLahtx" +
                "05osvLub9n9pgVcP95/BwXTy9ANJRy2mzkvvIKngmWT1cIJ+ljN3NEaFkKBduK3tQG481EinzKOuCn8Y" +
                "A9pCpXQzsqH4/wQoleIf9CerrBS8nyTI2Yku6G1UNtcZ5vv3wQklC0+m1BldXd+cSQaWdDyu/DYuLenk" +
                "fs/xi7RyurdHlpb0/H7PKUqJlR/29sjSkl6MK6/fvH0pS0v6cX8F+r6ks2IaOTLtpqpcy3cEmCg58cWt" +
                "VgEDNm14m7+vvOvkNuLlVj2mInfpeFDihdw0xehi+s52EJ3JwhAwITGdNzydU7sBMPmcS/AQk3lLbBhi" +
                "DwEdeyl7dlD8DW8zG8MaDQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
