/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LookupTransformActionResult : IDeserializable<LookupTransformActionResult>, IActionResult<LookupTransformResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public LookupTransformResult Result { get; set; }
    
        /// Constructor for empty message.
        public LookupTransformActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new LookupTransformResult();
        }
        
        /// Explicit constructor.
        public LookupTransformActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, LookupTransformResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal LookupTransformActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new LookupTransformResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new LookupTransformActionResult(ref b);
        
        LookupTransformActionResult IDeserializable<LookupTransformActionResult>.RosDeserialize(ref Buffer b) => new LookupTransformActionResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ac26ce75a41384fa8bb4dc10f491ab90";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XXVPbRhR916/YCQ8hHWIaSNKUCZlxwSFuwabGMO0Ts5au5C2S1tldYdxf33N3JdkO" +
                "puEh4AFb2o+759577sd+IZmQEVP/E8nYKV3manJd2MzunmiZXzjpKius/4lOtb6pZmMjS5tqU4zIVrkT" +
                "xv9Ehz/4E51dnBzg5CSg+RIwbglAKhNpElGQk4l0UgCLmKpsSuZ1TreUM9xiRonws24xI9vBxvFUWYG/" +
                "jEoyMs8XorJY5LSIdVFUpYqlI+FUQWv7sVOVQoqZNE7FVS4N1muTqJKXp0YWxNLxZ+lrRWVMon98gDWl" +
                "pbhyCoAWkBAbklaVGSZFVKnS7e/xhmhrPNev8UoZHNEeLtxUOgZLdzPYl3FKe4AzfgrKdSAbxiGcklix" +
                "7ceu8WpfCRwCCDTT8VRsA/n5wk11CYEkbqVRcpITC45hAUh9yZtevlqRzLAPRClL3YgPEpdnPEZs2cpl" +
                "nV5P4bOctbdVBgNi4czoW5Vg6WThhcS5otIJsM9Is4h4Vzgy2vrMNsYi7PIewa+0VscKDkjEXLlpZJ1h" +
                "6d4b1yqJnoiND0ZIxI/wbIYfPp8d/KEJm/By3hsc9wcnovkcip/xzbQkv01MpRULckzICbF94uD42kDh" +
                "bPjc3CIOgszu0bh/1RMrMt+sy2SPVMbAsiDhhNhGjxJ8Pur1zs7HveNW8N66YEMxgdqgJVwOevAI2G+d" +
                "kKkDk5Vj7Q07iO58HJRZtAR6/7OFf5DEWyEQDlE5y4klKGcbKQC6PSZTIPpyTgWOXtWQLy6Pjnq94xXI" +
                "++uQ55As46lCikjAw5itkFacBzYZ4qFjur8NR0u78DFvNxwz0V71pPK0XGLfeFJS0XdNw6ywGmGQSpVX" +
                "hh6CN+r93jtawXco3t2HZ+gfihnfRjgcULpy39Jl5/sYJxRL5FQvsz2sQp50Ekg5QyBTq/JW5ip5SIGa" +
                "eW2kHIr3z8C8lnqldj4Il+Rrndda+Kh7erqM5EPxy2MBTgilijYifIx14ZP73loHXabKFFzUuHy0bvB5" +
                "mZFQsqbEKk0+/AAlHmdmJsVa+IUDuGw8wInT4cV4VdSh+NUL7JaNMerqAUkigddYCAUjyNYELKUTugAL" +
                "gueJt9vkEbFnWbZma7NJ5wrqI3Jw1nrqjLa6ea7nvh/hhQgFPOhlsQKYulBxjImVBou3JDSpsozNWC9y" +
                "dOeiZyxl/WPfJdV1tzGSdexu1sfXZJh0PlXoLXw9Xkkpnh2UcC/U962L76422An7qWT+QEuybCC0OFTM" +
                "4Ks8x26WaYPz5oSjW9EN9UBJMpxSPKLVVqHGj+xStxdIxYC3WPdCSpRMZHzDbMSO0L+inbRWZhRcY2cU" +
                "q1TFTTB4BLZTS+deLywAqKLyQYE8p7Cq0ziPm5Ancp1L94LTNvbjUUaoE84swpp29qJual0zELVyxp/3" +
                "esaAgsTfT4T6/2E1/XnT77LjWqTo63Rxr+uury2dtulDr6lDL/ntSlT9PLleLmxOa3zOj9pyj9SQlsVg" +
                "1UcppobSwxdT52YHu7tzdaM6RtuONtmuS198cunHXfkJl4P4BoI6vOeCODuiV9VxVSBzSM99jvDCJ86S" +
                "VfKDnShav4HV3FmHixzEaIImeNVpUJIXhdGoteaKd5/XjY1FDbH/oLUNKaMFNiE3J8S9m+t7/sG1DAZC" +
                "EkBYyRgxFF2hP9FmP+zPvbGiPytsMCUb0+hg1edRsgazQUUpbv3cN/hFmwORYRdgmUT9ADfbndiYoIr4" +
                "rOiTCjgGbuxwVkw07IHKBBmFvIFIwl3KJ7DZDMLkqk14GFu2qZN1dkJe9auYRP7m6u+6SGNGZWBO6412" +
                "sxS1cjucVZD78jxgDofBhZwga2u/6oh+Kha6QkqFDngw9RXb31gaXL7Rc1rvcDDVItYNeq5R1pfBVyK5" +
                "SxSOKM21dO/firv2adE+/fssrl5ybJO3S/Sv3GcE8635nN++LgnKRv6uQs3T/MkLxbhO8HU/NRhe90aj" +
                "4YjvoG2LNfzj8rwdflMPHw0HA9wn+lf98d/t5F492ftrPOqeD0+74/5w0M7u17P9wVX3tH983R2dXJ71" +
                "BuN2wdt6wbh/1hteLsffNeOj7uDi83B01s68j+qpUJ/qLOlfrsNLFP0HNqf4d8sSAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
