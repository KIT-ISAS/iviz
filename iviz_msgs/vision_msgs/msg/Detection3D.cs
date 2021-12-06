/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Detection3D : IDeserializable<Detection3D>, IMessage
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
        // If this message was tracking result, this field set true.
        [DataMember (Name = "is_tracking")] public bool IsTracking;
        // ID used for consistency across multiple detection messages. This value will
        //   likely differ from the id field set in each individual ObjectHypothesis.
        // If you set this field, be sure to also set is_tracking to True.
        [DataMember (Name = "tracking_id")] public string TrackingId;
    
        /// Constructor for empty message.
        public Detection3D()
        {
            Results = System.Array.Empty<ObjectHypothesisWithPose>();
            Bbox = new BoundingBox3D();
            SourceCloud = new SensorMsgs.PointCloud2();
            TrackingId = string.Empty;
        }
        
        /// Explicit constructor.
        public Detection3D(in StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox3D Bbox, SensorMsgs.PointCloud2 SourceCloud, bool IsTracking, string TrackingId)
        {
            this.Header = Header;
            this.Results = Results;
            this.Bbox = Bbox;
            this.SourceCloud = SourceCloud;
            this.IsTracking = IsTracking;
            this.TrackingId = TrackingId;
        }
        
        /// Constructor with buffer.
        internal Detection3D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Results = b.DeserializeArray<ObjectHypothesisWithPose>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesisWithPose(ref b);
            }
            Bbox = new BoundingBox3D(ref b);
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
            IsTracking = b.Deserialize<bool>();
            TrackingId = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Detection3D(ref b);
        
        public Detection3D RosDeserialize(ref ReadBuffer b) => new Detection3D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results);
            Bbox.RosSerialize(ref b);
            SourceCloud.RosSerialize(ref b);
            b.Serialize(IsTracking);
            b.Serialize(TrackingId);
        }
        
        public void RosValidate()
        {
            if (Results is null) throw new System.NullReferenceException(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) throw new System.NullReferenceException($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (Bbox is null) throw new System.NullReferenceException(nameof(Bbox));
            Bbox.RosValidate();
            if (SourceCloud is null) throw new System.NullReferenceException(nameof(SourceCloud));
            SourceCloud.RosValidate();
            if (TrackingId is null) throw new System.NullReferenceException(nameof(TrackingId));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 89;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Results);
                size += SourceCloud.RosMessageLength;
                size += BuiltIns.GetStringSize(TrackingId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/Detection3D";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d570bbfcd5dea29f64da78e043da65ae";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71YbW/cuBH+rl9BnD/EPqzVs311gxRGcckijYFrkjbp9SUIDK5E7fIsiQpJ2d78+j4z" +
                "Q2plZ9Prh8ZGYkvUcDivz8zwQC1NY3sTlFZnS1WbaKpoXa+8CWMby+KgOFDvNzYocxdNXxPdSgdbEXXV" +
                "6hBsYyvNW1ZbZfuqHWvbr9XgguVV2zfOd0yxAC+ldNu6WyLRDxnImQr0+BYGU9GnOSel8bvTa6OiY17R" +
                "qZVRrQMDUxNB3OBV+7XxQlgWxSuja7xu+E+BbS/oVDV4t9Ir24K3CaVaOtigd1Ft9A2xT6oYtdkODlwD" +
                "PrNgbUsSBbtqDYvgVr/CZMrWYcGnh8r5TNtvaZ3ZtjaQiNobhdPHDs8i/A9l8YZZvEoH2fAPGzdvXTAf" +
                "PiaTBBIbBl+5sWfrrtydCqP3+Z0OFkHK4nlafO7uaAtIC/ahYQfrqEGto1qb3ng2G2uXT1KHtjQl3tZk" +
                "chgJyupWVXgYQOvGqFwjtgdHtvFRKREy87SyorU3n0brsS+bTgLMeRgrwMZRdXrL3GAJ0w1xWxbB9MH5" +
                "qy6sw+/eOtvHF60b61PQj74yVxW9kUaXDUTAOZ0JgULiVgcVva6uySCizUIoGmvaWgUT8X1ESKycayHh" +
                "VaZmbks1hiRo5fpA7uqrrdLQHNHSgZsdWjPLkHRuSNrf6HaEELZtWZ/WXpt2q2rbNAi+xrtODFbPpEG8" +
                "Gl1t8Le2N7YeYeeHoVCKols3ivyTOguyGEKAY1W3MCZz3ClF6+9Z3RA9v6cPVxbmu/g//xR/effnZyrE" +
                "WvwmOQfZ30Xd19rXsFbUHHxk4I1db4w/bs2NabFJdxRaEprbwbDSElEhhWkLU7J7oFTlum7sCTOgu4UX" +
                "5vuxk2BCDdpHW42AAtA7j3wg8sbrzhB3/AsITXjYwPPP2OOmGqO9Iach9b0ByMFoCItiRAyendKG4uD9" +
                "rTvGqyGAmQ6XhGKMHBB4JKcOz3DG96JcCd4wjsEpQINDXrvCazhSOAQimMEhDA4h+dtt3DiBsRvtrQbK" +
                "EOMKFgDXJ7TpydGMc8+se927zF447s74X9j2E1/S6XgDn7WkfRjXMKBlsESAghQYT0yq1pqeQG3ltd8W" +
                "tEuOLA5eko0lUNkj+AvAc5VlrLkFtOWAZG98w2i8sQF5KgH5NYiFyj/1GcM302fxKAwate3D3mJWZlRF" +
                "LCKSKFJcMwNiKW4AB4cYjkrXNbNAjs9xUgPSY4JTHHu5BC6OiAVgmQUYb8ZO98cIxppdxizh7c4spKAg" +
                "N+DxjRuBKIPxxFdpwR/nrsdBkmHKPfqF6g18f2fMPfv8ws+XkKzEOydphzpGcKdtO2GIrbPWu/K5VQKZ" +
                "DQKEQk+AMNlC4JIKh1Qn9XxLtDeInqkhiDv4TKogunIx97oHtn/44fjkY1k0rdPx/EepsVmS8yX5xzyw" +
                "/s6XCZ+ZJvFfSemuufWhZOWdmUtwCODG3uGLN0Bv1mqgSqRSDskZ2VVTNVwb7IShKlVBPyBEEule2Yax" +
                "OIMmio58KpSzhoJQ6rUjiKNIjJMGqbAm0PujJNG9kKrQJK3MpNxK6isAkTJ2KloU+FQ1+ukMKVVExGFm" +
                "KOtNbdl7TZI6yGEDugAOKFRvbpjEnBOD3cbkHJyXjLPNhT0YysEXjvFILIx0/EZQ8FuH54LjDQE4VKA+" +
                "V+zdw8RIljDoyjB8IeEr4wkYqFspiFmS/UD9zd0ed/pXWGviJE5JkXB+d47wn1SGx7y9S3HsvJ3I4S1Y" +
                "OlJ2o+xBFg7HY303lzHBEBoE8Peot9KAzvbC51SGDu8WartQnxfKuzjDHfVPRRy/WP7X/uV/8/JRzsIP" +
                "Z+cfZ8o8nusodPfY90t3LahXoOU6fRcQR3WbG7tUBbeZE0Hx1xG1yvfMd0f3WApClByOUwFK8GSzrjrh" +
                "0T11J3i8m56209PnxxF/Z7p9KXXPng9SC2+fdnYnOENy/XeN8tPtY3QQ98YqDsGHA5n0DIK+2VlUXRBu" +
                "nEn0zIlElT3h/+G5Wr55SZ3XEj1UT8elYgDTEVMaGC0NG5TQC2aGckWFZ5HGJzqus+s0y8lcjH4QIeOB" +
                "BdU14H7XVNzz1iIX2WaMmCTK2Zz4tWTZW9JElz0In78kvsF+NvsYLOYa7h9rn2SLSTED74cV5ReeK8/4" +
                "kMeJ9XTi3tpxw9/uh3hJY8clDwqux5jRGY0UxkQz7cTGGuNyJbAEk6H/QKfDnq7TBQV4dPoaLBEtMvwN" +
                "A5hpmu/60IqXIs/Wh6Zclwt1uzG9UMmtCzjwVIU+xds1JlLeuWsgiKdKyqGkNKc81YrMcpi0Krk8YP5P" +
                "I+otKYQHn4Y5vuDIcnEHG51bcEkTFnuwb5rngXsRje9vosC3cfVXbiGys7OQG9fynVjlMElVOUNeH9c5" +
                "m3UrUB3IDxZJKBceCdm/NhbkdO1ppeVEjcQvooybWHFsgIsUgTxmBThMulmtVhh4PaChdasF402rtwQ9" +
                "tQmVt6tpkkuicNSmzPyO7xfCd4Aer7cTKMhZfPuy6/1W1LasdY+cq9VpjVmTLoSO6fLjiDrdk5raj7Hn" +
                "9sTUiJS3OzZhthdCYzvvDpkzRox6rHILW6Gp8cgPM6AFE++EyU6IFG8c99Y0wLrmuGntehNFeppOqa/n" +
                "TSK8rj6NViBOQJV78gc3BTTTHVLre1ZnY4ej8ssLxdMlBPBjRSCarTgzVylXVSYZD56aDLIAF5ITi+Bz" +
                "wpLc2hoaWin6renXeNvDNF9LCIP8xptJpmVytLCpMNL3pg1ZVetzQKQKkOKFbUNBU0pP9JJC4cNHuXIK" +
                "hdyc4ceGq5UFiNRo/RRALcwG/unDn7JQIvcVnDRg74H6eVJq1sesttGEaYd3t5n+4Q58uk//FALyyfkH" +
                "xRm+yJnH3xZSfCDjYWb9vVjuqJjuA2uG1IkJ3Z4py5anW1v87+laGJMqMFOi4ZtdpD1EH3bEfvCRMZu8" +
                "PWT8Qd+RdEdi+21yMrbP71MzF0Ediaen6vL1+6ek/oU6SSt/T0sX6nRHc3LOK2czGlq6UD/uaMiPWPn9" +
                "jIaWLtR5Wnn585ufaOlC/WG+AlS/UE+LfOdA1x3ZJa+1pDLHYw4W1zR098kEb+SZ71sxI/sonT+ZQlI0" +
                "HcRBQZeNtGmZn00/EsgIKgTUbfRrGG7TORXakpgEeYUg7Oh+37SmY/TMrRRLVvwHlRyIzFgZAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
