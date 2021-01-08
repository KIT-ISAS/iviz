/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection3D")]
    public sealed class Detection3D : IDeserializable<Detection3D>, IMessage
    {
        // Defines a 3D detection result.
        //
        // This extends a basic 3D classification by including position information,
        //   allowing a classification result for a specific position in an image to
        //   to be located in the larger image.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Class probabilities. Does not have to include hypotheses for all possible
        //   object ids, the scores for any ids not listed are assumed to be 0.
        [DataMember (Name = "results")] public ObjectHypothesisWithPose[] Results { get; set; }
        // 3D bounding box surrounding the object.
        [DataMember (Name = "bbox")] public BoundingBox3D Bbox { get; set; }
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). This information is not required for all detectors, so it may
        //   be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud { get; set; }
        // If this message was tracking result, this field set true.
        [DataMember (Name = "is_tracking")] public bool IsTracking { get; set; }
        // ID used for consistency across multiple detection messages. This value will
        //   likely differ from the id field set in each individual ObjectHypothesis.
        // If you set this field, be sure to also set is_tracking to True.
        [DataMember (Name = "tracking_id")] public string TrackingId { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Detection3D()
        {
            Header = new StdMsgs.Header();
            Results = System.Array.Empty<ObjectHypothesisWithPose>();
            Bbox = new BoundingBox3D();
            SourceCloud = new SensorMsgs.PointCloud2();
            TrackingId = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection3D(StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox3D Bbox, SensorMsgs.PointCloud2 SourceCloud, bool IsTracking, string TrackingId)
        {
            this.Header = Header;
            this.Results = Results;
            this.Bbox = Bbox;
            this.SourceCloud = SourceCloud;
            this.IsTracking = IsTracking;
            this.TrackingId = TrackingId;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Detection3D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Detection3D(ref b);
        }
        
        Detection3D IDeserializable<Detection3D>.RosDeserialize(ref Buffer b)
        {
            return new Detection3D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            Bbox.RosSerialize(ref b);
            SourceCloud.RosSerialize(ref b);
            b.Serialize(IsTracking);
            b.Serialize(TrackingId);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                foreach (var i in Results)
                {
                    size += i.RosMessageLength;
                }
                size += SourceCloud.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(TrackingId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection3D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d570bbfcd5dea29f64da78e043da65ae";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1ZW2/cuBV+N+D/QKwfYi9mpmt76wYpjGKTQZoA2yRt3O0lCAyORM1wQ4kKKXk8+fX9" +
                "zjmkJDtj7D40NpJYosjDc/3OJUdqaSrbmKi0Ol+q0nSm6KxvVDCxd93i8OAIf9TVxkZlbjvTlLRzpaMt" +
                "aH/hdIy2soXmQ6udsk3h+tI2a9X6aHnVNpUPNe+YETGltHN+S3v0fQpyrcIBfIutKejTlJTS+LfWa6M6" +
                "L8Q6r1ZGOQ8KpqQd3QavOqxNkJ0Q4vDgldElFjb8ixaO1Au6WrXBr/TKOlxg4kItPXTR+E5t9A3dkQQy" +
                "arNrPShHfGbunCO2ol05I3z41a/QnbJlnDELsfAhb26gmFLoOhuJTx2MwvV9jWeR4Afw+ZZpvEpX2fgv" +
                "223e+Wg+fEyaicI6VL/yfcN6XvlbFfsQ8jvdLbyA4PO0+tzf0hnsFQJX2ET21p3GAd2ptWlMYA2ykPk6" +
                "dWwXZoG3NakfuoLM2qkCDy32+r5Tvkp2AEnW98lC/GVid2VF+GA+9zbgYFahOJwP0FmErjtV652Qg0ZM" +
                "3XY7CBFNE324ruM6/uGdt033wvm+PMOJPhTmuqA3Eet1BTZwV21iJB/Z6qi6oItPpBgRaSY7KmtcqaLp" +
                "8L0nF1l578Dmdd6eCC5VHxO/hW9gEsRAsVMaGoDz1CBoW2cmgZOujkkJN9r14MM6J2I5+8m4nSptVcEd" +
                "q+BrUVw54Qg+bHSxwe/S3tiyh8LvO8YiSbvzvQgxyDQjzcEf2Hm1g1aZ5CgYrV+JzLELvJC+XFtW4+X/" +
                "+efw4G/v//pMxa4UE0ookgDvO92UOpRQWqfZF0nPG7vemDB35sY4nNI1eZp46q6FYgdAwh9xWweNspkg" +
                "WuHrum8IT6ABC2tMCdBRwhDV6tDZogdM4IAPCBHaXwVdk16OeGOEs8LYBk7wjI1vir6zYIpRLhiAIJQH" +
                "Dzk86OGU53BI8xkHr7Z+jndDADRwIFHGKNrCEYlZHZ/RNd+LjAuQh5IMLgJSHPPaNV7jicI94MK0Hj5x" +
                "DPbf7boNPI385kYHqwFBRLmAHkD2CR16cjIlTaw/U41ufKYvJMdLfg/dZiRMYs03MJ4jFcR+DT1iJ/AB" +
                "/oq9SAREpXDWNAR5q6AD4pqOyaUg8pKULY7LtsFvAKIvLKPQFsg3OCjb5dt6542NiF5x0IdQmCT/qclA" +
                "nxMC+GbbQrGdtk3cm/gWI+zCOeFX5De+moC1ZEKghodTd0qXJRNB7E9xVAP2u4y3uPn1EsDZwy8AdBZw" +
                "velr3czhmyVbj2nC8LWZSeJBtMD4G98DaloTiLDSCZm8/9S3Eh5DPNI/yPZIAe+NuaOkX/j5NXhb4J0D" +
                "t0bCIyTU1o3gIlYT0cdcu1MCqBXchTxRYDIpRMCU0oukMfV8R3tv4EtjDcHiy7EkD5wtp/+gG6D/hx/m" +
                "px/BSeW87i5+lIw8cnOxJFMNt35l1oTgvCddsUq5vuSiiUKYj2Yy0cOjK3uLL8EA31m0ljKWSnEll2Sb" +
                "jYlzbXAUCitUASkBHImpO1keKuOoGnbUZF3ZOa1BGMHeeAJAckzWFLOYsnCCxD9LXN3xrwLlFfJHlm+V" +
                "kjHgkgJ5yG0cCZRbmuEWyWi0i50OlQ9wrrRsR3IOLonkuhZVA3sXkj3XWUmpA4XxZLIR3ZhUtMt1QDQU" +
                "ly88Q5UomkL0myHEb90/pKVgCOEhB5XKovcGqkb8xFaDT4I2oEBhAuEFVziHB0RvkOBI/cNv57X+FXob" +
                "qImBkl9c3F4gJAbRYb1gb7Nj+2CH/bAclA5/iZQhwRC751zfThnNEIWSAjcEZGcpYSeH4QCcrY5vZ2o3" +
                "U19mKvhugknq34pofrX8n/3L/+XlkyE2P5xffJwI9KhmZGfeo+evTTej6oKWy/RdcB55cKrzBQoCLlOH" +
                "HYcHf++R1ULDlMedjygm2BkcdMhUCbtECkhE7kp83xF6xE84WH4ELuTHL48mxajEvbF2R7X3Yg5vn0cT" +
                "EN5x1P2mZPlx+0ilx51+TRzzfq8nxYbgdDYepSI4IQcZPXOMUT2QcsXxhVq+fcml2xI1GPoYlLOCJVAi" +
                "UaV+FEqSaJ8xNSQ3SlOz1JbRfbVdpyYxNd8oKeFEAUhRfEJmGIuRO5YDCUl/Vd+hM5kUQxDuoTDamwJF" +
                "nn3JYPiUSUf7xeyjMZvKub9xfpIVl7IfyH+VgH7hvvWcr3m0CEiX7k81N/zxruMTFgHVufPwDTqX2mgE" +
                "OPqk4ShOlujJuXulkgcpGPFh2OxlmoYQkVp/AlH4jnSWbQtqaMlQakUnBqNpSaeOzWK9mKntxjSySwY9" +
                "RIK7NdQ4wa7R8PLRsfQgoioJiPRTnXHXLFzLbanMyakEc4bUAW9JJjyE1CbyPCVzxoVw5/2ME2CisQce" +
                "h6EBkLFDAf37AOIbmf2Bkcdg+MzrxjueyBUePZoMIODyb+ZljnL0D4zp8PXtxiI204Al5YCH+owcxg2t" +
                "OA5gNA8IVOR+0xXsJ0RG8kXu3yJsJ1UxZoRoqQNAw/kVuVLESA4Wwl4Ti2BXQ5OYmWEnTuH6HU8y4ndA" +
                "paClPkoNBF/HA5+xgoStfVjrBoFYqrMSrSxNoeY0aTmhmvm05MKlb7iyMSX85t1IJ04Og3Gc5+Mxk0bP" +
                "UvbFUAsXKIgCIsa0KOLESgKTDJeADOO5TqcG2VfzymGY0WUJqP2lNoGPiQC6wERMAFBQlyv8eyMJahaP" +
                "qYg+Z1HEoCeLvVPNsyXYCH1BQJv1OVEbx4z05qREGG3Qywx0iFssEqFTZmdrSwjKPS6mqqZZ420P1WEE" +
                "IiSGVz4ujC2T4YVUgelBY1zMMtuQHSSliuQ/rCRyIlzBVntJroF5qLgIkebJHX4w41pZQEyJElIB9OJk" +
                "uDB8+MvAmvB/DZu1OHykfh6Em5RBq11n4ngk+G0+cP8IPt078BRc8uX5B9kcZskRyd9mkqbA5nGm/b2o" +
                "EMl6HEqiSQZ2DWRofKcsW4HGyPjb0KAa3TBgVbzjWw7y7mMTm+QBaJJ+nizfZnRCuZI0gJiHicXgdH46" +
                "3M1kBJOSgz1Vr99cPSUlXKrTvPTPtHapzia7Ti946Xy6i9Yu1Y+TXWRULP1xuovWLtVFXnr589ufaO1S" +
                "/enOEvD/Uj0lPadJB41ZsoXe0DNkZS8d/MdXFQ1jecdbeeYJMBryQIP0pBaJ33wZ+wlNPunUMj+bBv91" +
                "gFqMcSMi3aPmo0Y63VSgqOGLcOYVPBNToZ0yziArAGZzNSbMHR78DxjLK/0GGgAA";
                
    }
}
