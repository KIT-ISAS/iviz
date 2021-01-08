/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/AveragingActionResult")]
    public sealed class AveragingActionResult : IDeserializable<AveragingActionResult>, IActionResult<AveragingResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public AveragingResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingActionResult()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Result = new AveragingResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionResult(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AveragingActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new AveragingResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingActionResult(ref b);
        }
        
        AveragingActionResult IDeserializable<AveragingActionResult>.RosDeserialize(ref Buffer b)
        {
            return new AveragingActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WwW4bNxC9L6B/IJBD7CJ22qRtUgM6qJLqqHASw1Z7NbjL0S5bLqmSXMv6+77haleS" +
                "LaM6pBZsryWRbx7fvBnOJ5KKvKjSI5NF1M4and/VoQxvL500t1HGJoiQHtnonrwstS1vKDQmCp8eg2z4" +
                "jV+D7PPt5QWiqpbJp8RvkL0S4GOV9ErUFKWSUYqFA39dVuTPDN2TYa71kpRI38b1ksI575xXOgj8lGRx" +
                "CGPWoglYFZ0oXF03Vhcykoi6pj0A3qqtkGIpfdRFY6THBueVtrx+4WVNCZ9/A/3TkC1IzCYXWGUDFU3U" +
                "ILUGRuFJBmiHL7G40Ta+f8c7sHG+cmd4TyVy0TMQsZKRGdPDEkIzWRkuOMx37RnPAQ+RCIFUECfpszu8" +
                "DacCccCClq6oxAnoX69j5SwQSdxLr2VuiJEL6ADY17zp9ekuNFO/EFZa1+G3kNsgx+DaLTAf66xC8gxL" +
                "EJoSOmLl0rt7rbA2XyeUwmiyUcCDXvr1IONtbVCA/MZiYxn2pdzgKUNwhUYmlFjpWA2yED0HSHm502qQ" +
                "/W/ufLZaBhn/jyyXeCQOnOyPmyLq3l1Pv0xmXy5F9xqK7/GXfUppo6hkEGuK7NCcWKiiNcFGqTY80u9R" +
                "lB3oaDyf/TkVO6A/7INychrvoTE8mRNLdRzy9c10+vl6Pp30yO/2kT0VBKvDpEg/rMKfoBpCFHIR4Wsd" +
                "WQDPmaKHVBe2HGRbqk9fr/ALwyQhWvehUpeGGELH0MGA6smcfI2CNNwfIp12pG//GI+n08kO6ff7pFeA" +
                "lkWl0TgUTFmwEIuGm8MhLZ6NM/r1681WGo7z44E4uUunV01y6Jb9wVCqof9Wh70RHGpiIbVpPD1L8Gb6" +
                "+3S8w3AofnpK0NNfVDDDg4S4vFwTH5vmzREscyokmm0C7aM16J9Rgiv3DPRwbe+l0erZI2wM2JfMUPz8" +
                "EgbsHWhdTOW49WCfwa3K49HV1baoh+LDsRRzwj1GBzkepTAS8zRl+7TtQvuabzy+VvpUpG7NVEjtH2PX" +
                "LB+/wTGOlJqtsVeIbQS+Tp5zxtXX2/ku1lD8khBHttNjc6sASiikjlGo1UH2KjDKeTslBBjdqCRdfkwV" +
                "BgZ3rDjLutJQACWEYI86Ka6wkTFulWYWXoqiwD9ue4uBz+YC43ITOxMYb1GUNyWPX/01F+kB49fLXnKz" +
                "STtObe7lTq0QOfN8qnRnQ9tVpTF+pOt6p8cko5BKM9MszTdpDjsgGADIspdwVgqsE+YgqpfImjG8nVFD" +
                "m8cVIXgP3vkQ/iTPTSZx2p8mukOg5WyGELRocFzvJ2RBpHJZ/M3m5C2+nXwxowVZcrKRprCkQi900VVH" +
                "YhHYTAyfBsN2BZjVTSoTtD+NZVBhk8l2VHmBPMYGidIQ7u2jaX6QZQvjJE+lPIt67fydtKWh/mO5dMhv" +
                "Pcj+BYDTjCk3DAAA";
                
    }
}
