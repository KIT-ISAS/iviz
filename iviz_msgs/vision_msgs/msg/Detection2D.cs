/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection2D")]
    public sealed class Detection2D : IDeserializable<Detection2D>, IMessage
    {
        // Defines a 2D detection result.
        //
        // This is similar to a 2D classification, but includes position information,
        //   allowing a classification result for a specific crop or image point to
        //   to be located in the larger image.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Class probabilities
        [DataMember (Name = "results")] public ObjectHypothesisWithPose[] Results { get; set; }
        // 2D bounding box surrounding the object.
        [DataMember (Name = "bbox")] public BoundingBox2D Bbox { get; set; }
        // The 2D data that generated these results (i.e. region proposal cropped out of
        //   the image). Not required for all use cases, so it may be empty.
        [DataMember (Name = "source_img")] public SensorMsgs.Image SourceImg { get; set; }
        // If true, this message contains object tracking information.
        [DataMember (Name = "is_tracking")] public bool IsTracking { get; set; }
        // ID used for consistency across multiple detection messages. This value will
        //   likely differ from the id field set in each individual ObjectHypothesis.
        // If you set this field, be sure to also set is_tracking to True.
        [DataMember (Name = "tracking_id")] public string TrackingId { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Detection2D()
        {
            Header = new StdMsgs.Header();
            Results = System.Array.Empty<ObjectHypothesisWithPose>();
            Bbox = new BoundingBox2D();
            SourceImg = new SensorMsgs.Image();
            TrackingId = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection2D(StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox2D Bbox, SensorMsgs.Image SourceImg, bool IsTracking, string TrackingId)
        {
            this.Header = Header;
            this.Results = Results;
            this.Bbox = Bbox;
            this.SourceImg = SourceImg;
            this.IsTracking = IsTracking;
            this.TrackingId = TrackingId;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Detection2D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Results = b.DeserializeArray<ObjectHypothesisWithPose>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesisWithPose(ref b);
            }
            Bbox = new BoundingBox2D(ref b);
            SourceImg = new SensorMsgs.Image(ref b);
            IsTracking = b.Deserialize<bool>();
            TrackingId = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Detection2D(ref b);
        }
        
        Detection2D IDeserializable<Detection2D>.RosDeserialize(ref Buffer b)
        {
            return new Detection2D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            Bbox.RosSerialize(ref b);
            SourceImg.RosSerialize(ref b);
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
            if (SourceImg is null) throw new System.NullReferenceException(nameof(SourceImg));
            SourceImg.RosValidate();
            if (TrackingId is null) throw new System.NullReferenceException(nameof(TrackingId));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 49;
                size += Header.RosMessageLength;
                foreach (var i in Results)
                {
                    size += i.RosMessageLength;
                }
                size += SourceImg.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(TrackingId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection2D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e38d1866e74825fff6b9ec7ca5865dc2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1ZW2/cuBV+N+D/QMAPjrczk3RTGEWKom3i7q4f2qZNeg0Cg5KoGSaUqCUlzyi/vt85" +
                "h6I0vjR5aGLYHl3Iw3P9zmXO1JWpbWui0ur7K1WZ3pS99a0KJg6u35yenOFXvd3ZqPAbbWOdDqr3sr50" +
                "OkZb21LTppUqhl7ZtnRDBYqdj5Zp2bb2oZElRE0p7Zzf23YLKsck0rkKG/AudqakV6oMvlN4ZBu9NSBs" +
                "2x48CC3wUhjlPAiYCmepfodbHbYmrYcQpyc/GV3hwY4/6MGZekUnqy74QhfWgVUTT0/+UnyABn4aOw8y" +
                "0cZ/2n732kfz7n1iLcpmCF/4oa1IiMIfVBxCmO6JAc90cPTL9PSlP9AerBUCb7GINK57jQ26V1vTmsAy" +
                "0MlmOk49sRuzwd2W1ANuoVbtWCMd1npo3NdJEyDJEl9s1J99jz0/DzZgEWvTOTWAbKmjiSsVvbK9avRI" +
                "yjNN14/gNZo2+nDTxG18es2qjn4IpbmxzVa4vq5VHwazwllwh8bESKtK3/batjFJjSW6/EiaWFge5Avv" +
                "HbzoZnqdSF4RX8IkCEHnvWnLUWmICPs0UILtnFn4Zjo2bsQvb7UbjNpb50QNzn40blSVrWtYvA6+Ec3g" +
                "CGtcpaIhJ1VGlzt8VvbWVgM0etfymyTv6AfewhIzBfg5NDMEw3HgoEomOQtGz99CTaTSPvCD9ObGViT1" +
                "b//PP6cnf3rz4wsV+0qMJ95OArzpdVvpUEFpvWZnIz3v7HZnwtqZW+OwSzfkSuKKYwfFLmNe/NJBo2wm" +
                "iFb6phlailhowMIaSwK0FdrVqtOht+VAcFF6HxADtL4OuiG9nPHCCA+FsQ2c4AUb35RDb8HUSDASjI6k" +
                "PHjI6cmAmH/+Pe3Axrd7v8a9oRjPHEgYgWNz6BA8xKyOL+iY70TGDchDSQYHVYgrfnaD23gBYCMuTOfh" +
                "E0/A/uux38HTyG9udbC6gAOCcgk9gOw5bTq/WJIm1l+oVrd+oi8k50O+hG47Eyax1jsYz5EK4rCFHrES" +
                "AAB/xdpiZCqlswZg6GwRdBhPT2ibHAoiP5CyxXHZNvgE5PnSMszsAW3ZQdkuX9c7b21E9IqDPgazJPkf" +
                "2glHdvm92DbjzEOpZTPjKpwTfkV+4+sFGkuuAWp4OHWvdFUxEcT+gozSwPV+AlScfH0FtBzgFxrhADze" +
                "DY1u1/DNiq3HNGH4BqDoGLtMgPF3fgDUdCYQYaUTMnn/cegkPHI80r8CoLxRb4w5UtI/+PoavG1wz4Hb" +
                "+MBIqK2bwUWsJqLP6WykhAmN1XAX8kSByaQQAVPKH5Kn1MuR1t7Cl+YszeLLtiQPnG3KsEG3QP53z9a/" +
                "fA9Oaud1f/krFUswOHNzeUWmyqfeM2tCcF6TjijYBxQ4pLqEQpi3TmSih0fX9oA3wQDfWTQpCFJcySGT" +
                "zebMuDXYCoWhmoCUAI7E1FEah8o4qvKKhqwrK4VUTuxnlGONOCZrilnEZ4vUmyDxNxJXR/5V6pbyxyRf" +
                "MQpdwCUFcs5tHAmUW9p8imQ0WsVORyUWsrtlO9aJ8yjHdSgL2Lsow+/07aTUTGHemWxEJyYVjeJ+FI8U" +
                "l688Q5UomkL0qyHE587PaSkYQnjIQXWr6L2FqhE/sdPgk6ANKFCaQHjBtc3pCdHLEpypv/n9utEfoLdM" +
                "TQyU/OLycImQyKLDesEeJsf2web1sByUDn+JXGBpcc+1PiwZnSAKJQVOCMjOVEOZ5WY4AGerJ4eVGlfq" +
                "00oF3y8wSf1LEc17j//98OP/8OOLHJvvnl++Xwj0Tc3IzvyAnu+bbkXVBT2u0nvBeeTBpc43KAhec9BP" +
                "K05P/jogq4WWKc8rv6GYYCc7aM5UCbtECkhE7kp8Hwk94yccbLoELkyXn76ZFLMSH4y1I9XeiTnc/Tyb" +
                "gPCOo+6zkk2X+29Uehw1ZOKYd5s5KTYEpzmqKA9xUFEBIMkhAbQDpjZom0CeI5jU0CH+XZRuOGsPJAau" +
                "ZlleXKzZEQSXOclGaSfQGSN3FWumQjhdMvMb6kXAkjlopFB5yW0xm0gaPSEmvfPE91KwFfV8SS6UnFSc" +
                "z3y9ZD53ujmPyPNtyhjabWHyfifGzG1rduknWd6Lu0H6YILNyrsPEjRP4LfzSdF+MkdHPETy4dYbSoF0" +
                "6CXylGBZRhzZMRcvOO1m4ad8P35ToBSPvCKn4XkG3b126INQ1OKPRKgHON1zSdrJKEgpqUvDPIDyGXVo" +
                "Bn2fhCk7VkpNafN9KEYrGNCc8FAGdiQngkHyBEZ3nUszGvTk+qPwgqdwQSQUlCi4Ir3zAjh20rfTrZmK" +
                "Exuy1/Eqig/foWPh7pIaeHL3aMKtUAefy8KpGrjaxTkliiRcTtLXam+4yOFsik4KkeE7jHNIrjRj4b5W" +
                "hFR/vxZXxRkBntUZcnc5H0pTqNBbg+pI+nimywMCajhDDahjfXYOZd6stA3alopnBUs6XIdQPVgYViw1" +
                "DMkWEK7HPCROms6zGSIKo7Gjj+dAFEqIDiUCdAScsBg/oCbGSeeVMAeCwVhaA5sTRMmxc63H5SChN4+L" +
                "6Bpg0iPMibO5TQf9HU0KJuH6HOpU7zAbA5eiRo16rwyiLuS6k4uraR5EJ5FycUV/jpqlMYlL76RbJQNl" +
                "pK18GrCAHmRj0+YUNDX0qdp7sCSg8p9nkujObO1d9QXZBzz2+mvOY+7O07JE9yZnUAHqVag4TS54jsfV" +
                "4LOVenbBrTvNPDsMbWrqhAPinZEsLbw35ATo8c+ZSo/nQUlutTBkQ+ZICuXRwUxx2n/0k4lN84IFLQpl" +
                "xHdqeUCoxGfQj1KC8baAdiyUHfcpzW3YZ0j94pA77jQblh4VYy4utfJk9HEK4zGFyu/bL9z56XinYJ9P" +
                "0JcS1v8mQVNVQvZJpxI6EkTz00TqFauCpgKP0stxeDzsmTnhuQDwvH+UBGdZA3yxNHZHZ9OmhpX8LM3i" +
                "gOik33mT0JbHq2kct1Lt0BRixuD3MW/f2wo83dvOjx/cXXo3NG2cywNntvCSVDwRcmAa4Bm4UyVWWwBu" +
                "DOVTJn0zvY6bsusW4929Fp+JaVhKVYdGEkAhlqZjnIRW6gMsTPswmV4DsEP8PY184kbG5FiFbxtaGi3B" +
                "dgj+igu2BsOaNLiXrzyI8MQKo1Q6JXM/KeSP0wNqhbgCUuu1KjEVbJHjGgOka7craSf5iiqWR0yqYFOk" +
                "bSrZ00CcWOeRCp9OSXv67ubpErjuqg4zQzHhr2nkXVhUHhU6ymRCgvRp3Jjf/W4e3/amO+LpBypI4Baw" +
                "ZbuFO4CJYuzpmxg+Al+7MKnFDpS/lI2lH+e3IjedjZku6H/HfobO9/Tkv7TYgXbgGgAA";
                
    }
}
