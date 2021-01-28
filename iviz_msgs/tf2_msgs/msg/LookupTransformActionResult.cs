/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/LookupTransformActionResult")]
    public sealed class LookupTransformActionResult : IDeserializable<LookupTransformActionResult>, IActionResult<LookupTransformResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public LookupTransformResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LookupTransformActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new LookupTransformResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, LookupTransformResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public LookupTransformActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new LookupTransformResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionResult(ref b);
        }
        
        LookupTransformActionResult IDeserializable<LookupTransformActionResult>.RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionResult(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ac26ce75a41384fa8bb4dc10f491ab90";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA71XbVPbOBD+7l+hKR8KHRqu0PZ6TOlMDlyaO0i4EDp3nxjFXic6bCuVZELu19+zku0k" +
                "vLR8KGQgsaXd1bPvqy8kUzJi6n8imTily1yNLws7sTvHWubnTrrKCut/ohOtr6rZyMjSZtoUQ7JV7oTx" +
                "P9HBT/5Ep+fH+zg5DWi+BIwbApDKVJpUFORkKp0UwCKmajIl8zqna8oZbjGjVPhdt5iR7YBxNFVW4G9C" +
                "JRmZ5wtRWRA5LRJdFFWpEulIOFXQGj84VSmkmEnjVFLl0oBem1SVTJ4ZWRBLx5+lbxWVCYne0T5oSktJ" +
                "5RQALSAhMSStKifYFFGlSre3ywzRxmiuX+OVJnBEe7hwU+kYLN3MYF/GKe0+zngVlOtANoxDOCW1YtOv" +
                "XeLVbgkcAgg008lUbAL52cJNdQmBJK6lUXKcEwtOYAFIfclML7dWJJdedClL3YgPEpdnPEZs2cplnV5P" +
                "4bOctbfVBAYE4czoa5WCdLzwQpJcUekEos9Is4iYKxwZbXxmG4MIXN4j+JXW6kTBAamYKzeNrDMs3Xvj" +
                "UqXRE0XjgxkS8SM8O8EPn88O/tCkTXg5i/tHvf6xaD4H4hd8c1iSZxNTacWCHAfkmNg+SXB8baBwNnxu" +
                "rpEHQWb3cNT7GosVmW/WZbJHKmNgWQThmNhGjxJ8Nozj07NRfNQK3l0XbCghhDbCEi5HePAKot86ITOH" +
                "SFaOtTfsILrxeVBOIvGdzwb+ESTeCiHgkJWznFiCcraRAqCbIzIFsi/nUuBoq4Z8fnF4GMdHK5D31iHP" +
                "IVkmU0UM21YJWyGruA7cZ4iHjun+Phgu7cLHvL3nmLH2qqeVD8sl9ntPSiv6oWk4KqxGGmRS5ZWhh+AN" +
                "4z/iwxV8B+LdXXiG/qXEPRABPqF05W6Hy/aPMY4pkaipXmZ7WIU66SSQcoVApVbltcxV+pACdeS1mXIg" +
                "3j9D5LWhV2rnk3AZfK3zWgsfdk9Olpl8IH59LMAxoVXRvQgfY1345K631kGXmTIFNzVuH261CngklK4p" +
                "sRomH36CEo8zMwfFWvqFA7htPBATJ4Pz0aqoA/GbF9gtG2PU3QOSRAqvsRAKRpCtCVhKJ0wBFgGep95u" +
                "40fknmXZmq3NJp0rqI/MkeWt0hltdPNcz/08woRIBcN52zYrgKkbFeeYWBmwmCWlcTWZsBlrIkc3LnrG" +
                "VtY7ikIEhBGkNpJ17G7Wx/dkmHQ+VZgtfD9eKSk+OijlWajnR5eq7jG37QR+Kjl+oCVZNhBGHCpm8FWe" +
                "g5tl2uC8OeHoVnQTeghJMlxSPKLVUaHGj+pSjxcoxYC3WPdCRpSOZXLF0QiOML9inLRWTii4xs4oUZlK" +
                "mmTwCGynls6zXiAAqKLySYE6p0DVaZzHQ8gTuc5lu8Fp987j0YTQJ5xZBJp297weal2zELVyRp93Y2MQ" +
                "gsTfT4T6+7Ca+byZd9lxLVLMdbq4M3XX15ZOO/RtsJv9LHmbEl0/Ty+XhM1pjc/5UVvXXAnqtgiqj1JM" +
                "DWUHL6bOzfZ3dubqSnWMth1tJjsue/HJZR935CdcDpIrCOowzzmRn1hSnVQFKof0sc8ZXvjCWbJKfrET" +
                "RV/WbmB17KzDRQ1iNEETvOosKMlEYTVqrbni3ed1Y2NRQ+w/aG1DyWiBjcnNCXnv5vqOfyzfsDIUAaSV" +
                "TJBD0VfMJ9rsBf7cGyv6qwKDKdmYRgerPo+SNZh7VJTi2u/dwi/aGogKu0CUydKP9S0nGFN0EV8VfVEx" +
                "vrJtc1VMNfmODhmFvCJuPjxSoYDNZhAmV23Cy2DZpM6ksx3qqqfiIPI3V3/XRRkzaqJWUr9llqJWbpur" +
                "CmpfngfM4TC4kAtkbe2tjuhlYqErlFTogAdTX7H9jaXB5Qc9p/W2qGdBj2PVoGcabX2ZfCWKu0TjiLJc" +
                "S/f+rbhpnxbt03/P4upljN3n7RLzq2ozes3n/PZtGaBs5B8q1DzNn7xRNAW+nqf6g8t4OBwM+Q7ajliD" +
                "Py/O2uU39fLhoN+P+ZLZG/3Tbu7Wm/Hfo2H3bHDSHfUG/XZ3r97t9b92T3pHl93h8cVp3B+1BG9rglHv" +
                "NB5cLNffNevDbv/882B42u68j+qt0J/qKulfLsNLFP0PNqf4d8sSAAA=";
                
    }
}
