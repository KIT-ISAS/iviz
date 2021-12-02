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
        internal Detection3D(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new Detection3D(ref b);
        
        Detection3D IDeserializable<Detection3D>.RosDeserialize(ref Buffer b) => new Detection3D(ref b);
    
        public void RosSerialize(ref Buffer b)
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
                "H4sIAAAAAAAACr1YbW/cuBH+rl9BnD/EPuyqZ/vqBimM4pJFmgDXJG3c60sQGFyJ2uVZEhVS8u7m1/eZ" +
                "GVKrdTa9fmhsJLZEDYfz+swMT9TCVLY1QWl1uVCl6U3RW9cqb8JQ93l2kp2om7UNymx705ZEt9TBFkRd" +
                "1DoEW9lC85blTtm2qIfStivVuWB51baV8w1TzMBLKV3XbkMk+iEDOVOBHt9CZwr6NOWkNH43emVU75hX" +
                "79TSqNqBgSmJoF/jVfuV8UKYZ9kro0u8rvlPhm0v6FTVebfUS1uDtwm5WjjYoHW9Wut7Yh9VMWq96xy4" +
                "BnxmweqaJAp2WRsWwS1/hcmULcOMTw+F84m2hUVKYVvbQCJqbxROHxo8i/A/5NlbZvEqHmTDP2y/fueC" +
                "+fAxmiSQ2DD40g0tW3fptioM3qd3OlgEybPncfG529IWkNLuG1CQg3WvQa17tTKt8Ww21i6dpE5tbnK8" +
                "rcjkMBKU1bUq8NCB1g29cpXYHhzZxme5RMjE08qK1t58GqzHvmQ6CTDnYawAG/eq0TvmBkuYput3eRZM" +
                "G5y/bcIq/O6ds23/onZDeQH6wRfmtqA30uh1BRFwTmNCoJDY6KB6r4s7MohoMxOKypq6VMH0+D4gJJbO" +
                "1ZDwNlEzt4UaQhS0cC28gHAvdkpDc0RLA262q80kQ+K5IWp/r+sBQti6Zn1qe2fqnSptVSH4Ku8aMVg5" +
                "kQbxanSxxt/S3ttygJ0fhkIuiu7cIPKP6szIYggBjlVdw5jMca8Urd+wuqH3/B4/3FqY7/r//JP95f2f" +
                "n6nQl+I3yTnI/r7Xbal9CWv1moOPDLy2q7Xx89rcmxqbdEOhJaG562DRhDn4J2Faw5TsHihVuKYZWsIM" +
                "6G7hhel+7CSYUJ32vS0GQAHonUc+EHnldQODnBBZQGjCwwaef8YeN8XQWwjEKOYNQA5GQ1hkA2LwEvFn" +
                "PmUnNxs3x6shgBkPl4RijOwQeCSnDs9wxveiXA7eMI7BKUCDU167xWs4UzgEIpjOIQxOIfm7Xb9GbFGo" +
                "3GtvNVCGGBewALg+oU1PziacSexnqtWtS+yF4/6M/4VtO/IlneZr+Kwm7cOwggFBCBxAgIIUGE9Mitqa" +
                "lkBt6bXfZbRLjsxOXpKNJVDZI/gLwHOFZazZANpSQLI3vmE03tuAPJWA/BrEQuWf2oThCeohMnsUBu21" +
                "bcPRYpYnVEUsIpIoUlw1AWIpbgAHhxjulS5LZoEcn+KkBqT3EU5x7OsFcHFALADLLMB4PTS6nSMYS3YZ" +
                "s4S3GzOTgoLcgMfXbgCidMYTX6UFf5y7GzpJhjH36BeqN/D9vTEH9vmFn19DshzvnKQN6hjBnbb1iCHk" +
                "LNF6Xz53SiCzQoBQ6AkQRlsIXFLhkOqknu+I9h7RMzYErLnsiqogulIx97oFtn/4YX7+Mc+q2un+6kep" +
                "sUmSqwX5ZzzxC19GfGaayH8ppbvk1oeSlXcmLsEhgCu7xRdvgN6sVUeVSMUckjOSq8ZquDLYCUMVqoB+" +
                "QIgo0kHZhrE4g0aKhnwqlJOGglDqjSOIo0hkE7F8sbBG0PujJNFBSBVoklAbknJLqa8ARMrYsWhR4FPV" +
                "aMczpFQREYcZehiAWWnZexQP3NzIYR26AA4oVG9umMScI4P9xugcnBeNs0uFPRjKwReO8UgsjHT8RlDw" +
                "W4enguMNAThUoD5X7N3CxEiW0GnISPCFhC+MJ2CgbiUjZlH2E/U3t5k3+ldYa+QkTomRcLW9QviPKsNj" +
                "3m5jHDtvR3J4C5ZGgAQqe5CFw3Gut1MZIwyhQQB/j3orDehkL3xOZeh0O1O7mfo8U971E9xR/1TE8Yvl" +
                "fx1f/jcvn6Us/HB59XGizOO5jkL3iH2/dNeMegVaLuN3AXFUt6mxcwUfUnInguyvA2qVb5nvnu6xFIQo" +
                "KRzHAhThSeSHLhScJPKBuiM8bsen3fj0+XHE35vuWEod2PNBauHt097uBGdIrv+uUXraPEYHcTBWcQg+" +
                "HMikZxD0Tc6i6oJw40yiZ04kquwR/0+v1OLtS+q8FuihMHagERWwgOmIKQ2MMI0k9IyZoVxR4ZnF8YmO" +
                "a+wqznIyF6MfRMh4YEFxB7jfNxUH3gIHqWfV0GOSGDsaKPa1ZDla0kSXIwifvkS+wX42xxjMphoeH2uf" +
                "JItJMQPvhxXlF54rL/mQx4n1eOLR2nHP3w5DHEADrOZBwbUYMxqjkcKYaMad2FhiXOb5ktoWVFMkgmFP" +
                "l/GCAjwafQeWiBYZ/roOzDA6oVUKtXiJ7i96dWryVT5Tm7VphUpuXcCBpyr0Kd6uMJHyzn0DQTxVVA4l" +
                "pbrgqVZklsOkVUnlAfN/HFE3pBAefBzm+IIjycUdbO/cjEuasDiCfeM8D9zr0fj+Jgp8G1d/5RYiOTsJ" +
                "uXY134kVDpOU3AsgwN/My5TN6PcZqhHZm7VFEsqFR0T2r40FKV1bWqk5UdHsIyVRxk1fcGyAixSBNGYF" +
                "OEy6WdzRYeD1gIbaLSl6Ai7F4BfQmlB4uxwnuSgKR23MzO/4fiF8B+jxmjqc2PDzWXz7su/94F7nV7pF" +
                "zpXqosSsSRdCc7r8OKNO97yk9mNouT0xJSLl3Z5NmOyF0NjOu0PijBGjHIrUwhZoajzyw3RowcQ7goOM" +
                "h0AG47i3pgHWVfOqxh1DL9LTdEp9PW8S4XWBiymBOAFV7skf3BTQTHdKre8lqSFuPMu/vFC8WEAAPxQE" +
                "osmKE3NxesjUTMaDp0aDzMCF5MQi+JyzJBtbQkOeQXGXadoV3o4wTdcSwiC98WaSaREdLWwKjPStqUNS" +
                "1foUELECxHhh21DQ5NITvaRQwDWkhEQmN2f4wT3T0gJESrR+CqAWJgP/+OFPSSiR+xZO6rD3RP08KjXp" +
                "Y5a73oRxh3ebRP9wBz4d0j+FgHxy+kFxhi9S5vG3mRQfyHiaWH8vljuLWkElzK9AppEJ3Z4py5anW1v8" +
                "b+laGJMqMFOiIXss9GFHHAcfGbPJ213CH/QdUXckNtwqTsb26X1q4iKoI/H0VL1+c/OU1L9W53Hl73Hp" +
                "Wl3sac6veOVyQkNL1+rHPQ35ESu/n9DQ0rW6iisvf377Ey1dqz9MV4Dq1+pplu4c6LojueQNPUNBjscU" +
                "LK6q6O6TCd7KM9+3Ykb2dFsdTSEpGg/ioKDLRtq0SM+mxcU8GilGhYC6jX4Nw208p0BbwsdgyysEIa5m" +
                "dsrUBkgP9EytFEuW/QeVHIjMWBkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
