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
            Results = System.Array.Empty<ObjectHypothesisWithPose>();
            Bbox = new BoundingBox3D();
            SourceCloud = new SensorMsgs.PointCloud2();
            TrackingId = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection3D(in StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox3D Bbox, SensorMsgs.PointCloud2 SourceCloud, bool IsTracking, string TrackingId)
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
                "H4sIAAAAAAAACr1YbW/cxhH+TsD/YRF9sBTcsZGUKoYLoYh9cG0gtd1aSdoahrBHLu82Irn0LqnT+df3" +
                "mZldHk8+J/1QS7Alcjk7O6/PzOyRWpjKtiYorc4XqjS9KXrrWuVNGOo+z46yI3W1tkGZu960JdEtdbAF" +
                "URe1DsFWttC8ZblVti3qobTtSnUuWF61beV8wxQz8FJK17XbEIm+z0DOVKDHt9CZgj5NOSmN341eGdU7" +
                "5tU7tTSqdmBgSiLo13jVfmW8EOZZ9tLoEq9r/pNh23M6VXXeLfXS1uBtQq4WDjZoXa/W+pbYR1WMWm87" +
                "B64Bn1mwuiaJgl3WhkVwy99gMmXLMOPTQ+F8om1hkVLY1jaQiNobhdOHBs8i/Hd59oZZvIwH2fCr7ddv" +
                "XTDvP0STBBIbBl+6oWXrLt2dCoP36Z0OFkHy7FlcfObuaAtIafcVKMjButeg1r1amdZ4Nhtrl05SxzY3" +
                "Od5WZHIYCcrqWhV46EDrhl65SmwPjmzjk1wiZOJpZUVrbz4O1mNfMp0EmPMwVoCNe9XoLXODJUzT9ds8" +
                "C6YNzl83YRX+9NbZtn9eu6E8A/3gC3Nd0Btp9KqCCDinMSFQSGx0UL3XxQ0ZRLSZCUVlTV2qYHp8HxAS" +
                "S+dqSHidqJnbQg0hClq4Fl5AuBdbpaE5oqUBN9vVZpIh8dwQtb/V9QAhbF2zPrW9MfVWlbaqEHyVd40Y" +
                "rJxIg3g1uljjb2lvbTnAzvdDIRdFt24Q+Ud1ZmQxhADHqq5hTOa4U4rWr1jd0Ht+jx+ubZk9yi7/zz+P" +
                "sr+/+9tTFfpSPCdZ9wjiv+t1W2pfwmC95vgjG6/tam38vDa3psYu3VB0SXRuOxg1wQ7+SaTWsCZ7CHoV" +
                "rmmGlmAD6ls4YrofOwkpVKd9b4sBaAB655ESRF553cAmR0QWEJ1wsoHzn7LTTTH0FgIxkHkDnIPdEBnZ" +
                "gDA8Rwiaj9nR1cbN8WoIY8bDJacYJjvEHsmpw1Oc8a0ol4M3rGNwCgDhmNeu8RpOFA6BCKZziIRjSP52" +
                "268RXhQtt9pbDaAhxgUsAK6PadPjkwlnEvupanXrEnvhuDvjf2HbjnxJp/kaPqtJ+zCsYEAQAgoQoyAF" +
                "zBOToramJVxbeu23Ge2SI7OjF2RjiVX2CP4C81xhGW42QLcUk+yNrxqQtzYgWSUmv4SzFKU/tgnJE+BD" +
                "anYqbNpr24aDJS1P2IpwRDBRsLhqAsdS4gARDmHcK12WzAKZPkVLDWDvI6ji2FcLoOOAcACiWUDyemh0" +
                "O0c8luw1ZgmHN2YmZQXpAaev3QBc6YwnvkoLCjl3M3SSD2P60S/UcKD8O2P2DPQLP7+CZDneOU8bVDMC" +
                "PW3rEUngr6j1rohulQBnhRih6BM4jLYQ0KTyITVKPdsS7S0CaGwLWHPZFVVBgKWS7nULhH//3fz0Q55V" +
                "tdP9xfdSaZMkFwvyz3jiZ76MKM00kf9SCnjJDRDlK+9MXIJDDFf2Dl+8AYazVh3VIxXTSM5Irhpr4spg" +
                "JwxVqAL6ASSiSHvFG8biJBopGvKpUE7aCgKq145QjiKRTcTyxfIace8vkkd7IVWgVUKFSMotpcoCEylp" +
                "x9JFgU+1ox3PkIJFRBxm6GSAZ6Vl71E8cIsjh3XoBTigUMO5bRJzjgx2G6NzcF40zjaV92AoCZ87hiSx" +
                "cDBfDw3+6HhCAo4TbwjGoQU1vGLyFlZGvoROQ0wCMeR8YTxhA7UtGXET8cHkn24zb/RvMNjISfwSg+Hi" +
                "7gIZMGoNp3l7F0PZeTuSw2EwNmIkUPGDLByRc303lTEiEToF8Pcou9KJTvbC7VSMju9majtTn2bKu34C" +
                "Pepfijh+tvzvw8v/4eWTlIjvzy8+TJR5SO8xch8w8ecem1HTQMtl/C5QjjI3tXeu4EZK8USQ/WNA0fIt" +
                "893RPZyOEGYMyrESRZwSFaAOhShJvafxiJN349N2fPr0UBrs7Hcwt/asei/H8PZxZ32CNmTZ7yuVnjYP" +
                "01DsjVoSi/enNGkhBIyTy6jYIO44q+iZk4oKfSwHxxdq8eYF9WILdFWYRdCaCnDAesSUpkhYR5J7xsxQ" +
                "vagOzeJMRcc1dhUHPBmW0SEicDxwobgB+u96jD2PgYOUt2roMV6MDQ4U+1LWHKxwossBwE9fIt9gP5lD" +
                "DGZTDQ/Puo+TxaS2gff9AvMLD5vnfMhDRXw883ApueWP+4EO0AF08/TgWswejdHIZYw5405sLDFG89xJ" +
                "jQzqK9LBsLPLeHEBHo2+AUsEjAyFXQdmmKfQPIVaHEX3Gr06Nvkqn6nN2rRCJbcx4MCjFjoXb1eYVHnn" +
                "rqUgnipqhwpTnfG0KzLLYdK8pGqBe4E4um5IITz4OOHxxUeSi3va3rkZVzhhcQAFxzkfANijFf4jLPh6" +
                "I+7h+4nR30nOtav5uqxwmLDkygBh/npeppzGEMCwjfjerC1SUe5CIsp/aVZISdvSSs3pigkAiYnCbvqC" +
                "wwNcpCCk8SvAZ9Li4voOg7AHQNRuSQEUcF8G14DWhMLb5TjhRVE4cGN+fsNXD+EbAJDX1PPEKYDP4ouZ" +
                "XUMIDzu/0i0yr1RnJWZQuiua073ICbW/pyU1JEPLDYspESxvd2zCZC+ExnbeHRJnzB3lUKS+tkCb45Ei" +
                "pkNTJu4RNGRUBD4Yxw03Dbaumlc17h56kZ6mVmr2eZMIrwvcWQnQCbRyo37vBoEGvWPqh89JDXHjSf75" +
                "XePZAgL4oSAoTVacmIszRKZpMh48NRpkBi4kJxbB55Ql2dgSGvJgimtO067wdoBpuq4QBumNN5NMi+ho" +
                "YVNg1G9NHZKq1qeAiHUgxgvbhoImlxbpBYUCbiglJDK5VMMPrqCWFjhSohlUwLUwuQgYP/w1CSVyX8NJ" +
                "HfYeqZ9GpSY9zXLbmzDu8G6T6O/vwKd9+icQkE9OPyjR8EXKPP42kxIEGY8T62/FcidRK6iEoRbgNDKh" +
                "izVl2fJ0oYv/Ld0YY3wFbEo0ZA8HQOyKL+CPjN/k8C5BEBqQqD5yG54VP2P79LY1cRHgkZB6ol69vnpC" +
                "FrhUp3Hl57h0qc52NKcXvHI+oaGlS/X9juYcrsTKnyc0tHSpLuLKi5/e/EhLl+qH6Qqw/VI9ydJdBF2D" +
                "JK+8pmcoyCGZ4sVVFd2MMsEbeebbWMzOnu6yoykkS+NBHBd0D0mbFunZtLi2R0fFwBBQvdG4YeiN5xTo" +
                "T/gYbHmJOMSVzVaZ2gDsAaCpp2LJHmX/Bdnd3wx3GQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
