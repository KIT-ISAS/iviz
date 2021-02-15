/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestRequestActionResult")]
    public sealed class TestRequestActionResult : IDeserializable<TestRequestActionResult>, IActionResult<TestRequestResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TestRequestResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestRequestResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestRequestResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionResult(ref b);
        }
        
        TestRequestActionResult IDeserializable<TestRequestActionResult>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestActionResult(ref b);
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
                int size = 5;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestRequestActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "0476d1fdf437a3a6e7d6d0e9f5561298";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WwXLbNhC98ysw40PsTq20SZukntFBlVRHGSfx2GqvGpBckWhJQAVAyfr7vgUoirKt" +
                "RockGtu0JODtw9u3i31PMicryvBIZOaV0ZVKF7Ur3MtrI6t7L33jhAuPZE7O39G/TXi4pvLChkcy/Mqv" +
                "5OP99RWi5pHJ+8jvTICOzqXNRU1e5tJLsTSgr4qS7GVFa6qYar2iXIRv/XZFboCN81I5gZ+CNFlZVVvR" +
                "OCzyRmSmrhutMulJeFXTwX7sVFpIsZLWq6yppMV6Y3OlefnSypoYHT+OZdEZidnkCmu0o6zxCoS2QMgs" +
                "Sad0gS9F0ijtX7/iDcnZfGMu8ZYKJKELLnwpPZOlhxX0ZZ7SXSHGD/FwA2BDHEKU3Inz8NkCb92FQBBQ" +
                "oJXJSnEO5rdbXxoNQBJraZVMK2LgDAoA9QVvenHRQ9YBWkttdvARcR/jFFjd4fKZLkvkrOLTu6aAgFi4" +
                "smatcixNtwEkqxRpL+A8K+024V0xZHL2B2uMRdgVMoKndM5kCgnIxUb5MnHeMnrIxkLlyTdy49HqSPhf" +
                "ZLbAg+Nzgt/tSia+uZ1+msw+XYvdayh+wl+2JYVtopRObMmzIVNifbKY+FagGBs5t2vUQcQcjeezv6ai" +
                "h/nzISZnpLEWysKEKbFGJwHf3k2nH2/n00kH/OoQ2FJGsDZsiZTDHvxJaApCLj2crDyf3nKC6CHUgS4S" +
                "8T+vM/zCJEGFaDhU5aoiRlDe7VBA9HxOtkb1VdwKPF20lO//HI+n00mP8utDyhsgy6xUxLRdk7EKy4b7" +
                "wHNCHAsz+v3z3V4XDvPLM2FSE46eN8GWe+7PRsob+qI07ApnUAZLqarG0jF6d9MP03GP31D8+pSepb8p" +
                "80ccEArKNP6xXX78MseUMomeGjC7YA36pJdgyh0CnVrptaxUfuwArfO6ShmKN9/BeZ31tPGhCPfm65LX" +
                "KTwe3dzsK3ko3p5KMCVcVfQsw1PURU6eZuuQtF4qW/OlxteH73eBwITyg0P0bfLuKxziNJnZFAflFwPw" +
                "tXHEEzef7+d9qKH4LQCO9E6M9vYAksiRNQahKILsJGCUQZwCHAxe5UG39ITac4xtWG2WdKNwfFSO1I9a" +
                "Z3I2qiqzCfMIL0QpWK7b7rICmfai4hoTveGKt+SUNkXBMraLPD345DteZbNJEh0QR5BWJOc53XyecCdD" +
                "0k2pMFuE+7jXUoI7KOdZaBZGl6a9Yx7rhP2k2T84JTkWCCMO1SvkqqqwmzFdTN6GELqD3lkPliTLLSUw" +
                "6o8KLX90l3a8QCsGve1hFpZEeSqzf9iN2BHnV4yTzsmCYmrcijK1VNmuGAIDN2jRedaLC0CqbkJRoM8p" +
                "rBrsksdDyLdO3csnw3gSB0tka9GO5akx7P6FU9zQFq1V/wO7i/w79QsAAA==";
                
    }
}
