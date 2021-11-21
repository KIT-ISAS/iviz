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
            Status = new ActionlibMsgs.GoalStatus();
            Result = new AveragingResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AveragingActionResult(ref Buffer b)
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
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength => 8 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QECH2EXttEk/UgM6qLLqOHASw1Z7NbjL0S5bLqmSXMv6933DXa2k" +
                "WEJ0SC3YXksi3zy+eTOc9yQVeVGlRyaLqJ01On+oQxleXzlp7qOMTRAhPbLxI3lZalveUWhMFD49Btno" +
                "G78G2cf7qwtEVS2T94nfIBsK8LFKeiVqilLJKMXcgb8uK/Jnhh7JMNd6QUqkb+NqQeEcG2eVDgI/JVmc" +
                "wZiVaAIWRScKV9eN1YWMJKKuaWc/dmorpFhIH3XRGOmx3nmlLS+fe1kTo+Mn0L8N2YLE9eUF1thARRM1" +
                "CK2AUHiSAbrhS5E12sa3b3hDNpwt3RneUoks9MFFrGRksvS0gMTMU4YLxPiuPdw5sKEOIYoK4iR99oC3" +
                "4VQgCCjQwhWVOAHz21WsnAUgiUfptcwNMXABBYD6ije9Ot1CZtoXwkrr1vAt4ibGMbC2x+UznVXImeHT" +
                "h6aEgFi48O5RKyzNVwmkMJpsFLCel36V8a42ZDb8gzXGIuxKGcFThuAKjQQosdSxykL0jJ6y8aBV9r8Z" +
                "8mCBDDL+H8kt8WAKnON367Jp39xOP11ef7oS69dI/IC/7ExK20Qlg1hRZE/mxBIVbe47jdrgSLtHFXaY" +
                "48ns+q+p2ML8cReTk9J4D3Hhw5xYpqOAb++m04+3s+llD/xmF9hTQXA3nImswyH8CQogRCHnEWbWkU/v" +
                "OUf0lErBltmG6PPXEL/wSVKh9RwKc2GIEXQMaxQQPZmRr1GAhrtBpNOO8v2fk8l0erlF+e0u5SWQZVFp" +
                "dAkFKxaswrzhVrBPiENhxr9/vtvowmF+2hMmd+noqknO3HDfG0k19FVp2BXBoRLmUpvG0yF6d9MP08kW" +
                "v5H4+Tk9T39Twfz20uGack380i7ff51jToVEW02YfbAGrTJKMOUmgWat7aM0Wh06QOe8vlJG4pcXcF5v" +
                "PetiKsKN+frk9QpPxjc3m0oeiV+PJZgTbivay/AYdZGT59naJW3n2td8r/EN0qchtWZmQmrnENs2efcN" +
                "DnGczGyKnfJrA/DNccATN5/vZ9tQI/FbAhzbtRjdBQIkoZA1BqFWBNlLwCjn7SAQYHCjkm75EbUXGNux" +
                "2izpUuP4qBzE2m2d2XBsjFumkYQXohTwj9vcVyDT3VVcY2JrwOItivKm5OlqfaFFeorZi95m15c8ZLEJ" +
                "2kGk0ylEzjgfKd3MUHVZaUwY6Vbe6irJIKR4IrpOA0yasfZIhf1k2UI4KAXWCIMO1QukyxjsZszQ5m9J" +
                "CN1Dr90HV5LnrpIYbQ8MHX80mG7IQDcGvdVuIuZEKpfFP2zIbNgNshgqQ5AlZxjZCQsq9FwX63pIDAIb" +
                "iNGxqVsAUnWT6gKtTmPV+Tp/WPUS2YsN0qMh2Osv5vNBls2Nkzxs8ozptfMP0paG+o/lwiGr9SD7DyMB" +
                "8kEJDAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
