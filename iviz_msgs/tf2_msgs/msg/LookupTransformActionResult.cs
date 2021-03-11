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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAACr1XUVMbNxB+vxn+gyY8BDrENJCkKRMy44JD3IJNbcO0T4x8t7ZV7k6OpMO4v77fSmfZ" +
                "DqbhIcED9p20Wn27++1q9ZlkRkZM/E8iU6d0mavhTWHHdv9My7zvpKussP4nOdf6tpoOjCztSJuiR7bK" +
                "nTD+Zys5/s6freSif3aEvbOA57NHuZVsC6AqM2kyUZCTmXRSAI6YqPGEzKuc7ihnxMWUMuFn3XxKtoGF" +
                "g4myAn9jKsnIPJ+LykLIaZHqoqhKlUpHwqmC1tZjpSqFFFNpnEqrXBrIa5OpksVHRhbE2vFn6UtFZUqi" +
                "fXoEmdJSWjkFQHNoSA1Jq8oxJkVSqdIdHvCCZHsw06/wSmPEIm4u3EQ6Bkv3U7iYcUp7hD1+CsY1oBve" +
                "IeySWbHjx27wancFNgEEmup0InaA/HLuJrqEQhJ30ig5zIkVp/AAtL7kRS93VzQz7CNRylIv1AeNyz2e" +
                "oraMetmmVxPELGfrbTWGAyE4NfpOZRAdzr2SNFdUOgECGmnmCa8KWybbn9jHEMIqHxH8Smt1qhCATMyU" +
                "myTWGdbuo3GjsuSHEfLRNNlK+BnBHeOHIXCM3y+SJ7xctjqn7c6ZWHyOxc/4ZmaSXyYm0oo5OebkkNhF" +
                "aYh97aOwOcJu7pCxQWfzZNC+bokVna/XdXJQKmPgXPBwSOymJym+7LVaF5eD1mlUfLCu2FBKYDeYiaiD" +
                "ITyCBLBOyJEDmZVj6w3HiO59KpTjZAn04Wcb/+CJ90LgHBJzmhNrUM4utADozoBMgQTMuRo42q0h969O" +
                "Tlqt0xXIh+uQZ9As04lClchAxZS9MKq4FGxyxGPbNH/r9pZ+4W3ebNhmqL3pWeWZucS+caesom+6hllh" +
                "NTJhJFVeGXoMXq/1e+tkBd+xePsQnqF/KGV8G+FwTunKfU2XvW9jHFIqUVa9zrhZhVLpJJBykUCxVuWd" +
                "zFX2mAE182KmHIt3z8C8SL1SO5+ES/LF4EUPnzTPz5eZfCx+eSrAIeG0oo0In+JdxORhtNZBlyNlCj7X" +
                "+ASJYfClmZFQtmbEKk3efwcjnuZmJsVa+oUN+OR4hBPn3f5gVdWx+NUrbJYLZ9QHCDSJDFFjJRScIKML" +
                "WEsjNAIWBM8z77fhE3LPsm7N3maXzhTMR+Zgr/XSmWw381zPfEvCgkgFPOjleQUw9VnFOSZW2ixektGw" +
                "Go/ZjbWQo3uXPOtp1j7lJotJEBqR2k/WccTZJH8yw6uziUKH4U/llariCUIZd0Rt38D4HmuDq7CeSqYQ" +
                "DCXLPkKjQ8UU4cpzrGadNsRvRtg6ql6wD6wkw1XFI1ptGGr8KDB1k4FqDHjz9UCMiLKhTG+ZkFgRGlk0" +
                "ldbKMYXo2CmlaqTSRT54BLZRa+eOLwgAVFH5vECpU5BqLOIHqR8XPTc6CHHb2JpvJWPCaeHMPAjF6X7d" +
                "3brFQBIVDT4dtIwBEYm/fxjw/wcW+LfS+3L4Ilj0eLp40IHXt5hGbADRd+rQV34tieM/z26WgovdFpHn" +
                "R225WVpQl9VA6oMUE0Oj4xcT56ZH+/szdasaRtuGNuN9N3rx0Y0+7MuPuCikt1DU4DV94jKJvlWnVYES" +
                "In0GcKoXvoKWbJIfbCTJ+oWsZtA6XBQjRhMswaseBSNZKIwm0Z0rAX7uSMYQGuIQwnAbakfENiQ3IxQA" +
                "N9MPQoRbGnyEaoD8kimSKblGr6LNYVife38lf1ZYYEr2p9HBsc9lZw1nk5VS3PnJr0wQsR6i3M7BNYnj" +
                "BAyNK7Eww6HiK6QvMGAaGLLHFTLTcAkOKugo5C1UEm5XvphNp1AmV93Cw1iyQ41xYy/UWC/FVPJ3WX/7" +
                "RUkzagz+xIDExVLU1u1xfUEdzPOAOWyGKHKxrB2+2xDtkZjrCuUVNuDB1Jduf4FZ4PJ9n9N6j1OqVrHu" +
                "0UuNU36ZgiUKvcQhkoxyLd27N+I+Ps3j07/PFO0l0TYGvERHy51H8OBa2Pnty5Km7Odv2RSfZs9wbgzq" +
                "cr9VN1md7k2r1+v2+GIa+67uH1eXcfh1PXzS7XRwyWhftwd/x8mDerL116DXvOyeNwftbifOHtaz7c51" +
                "87x9etPsnV1dtDqDKPCmFhi0L1rdq+X428V4r9npf+r2LuLMu6Se8sfVomL6l5vwAif+B9a0eJjnEgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
