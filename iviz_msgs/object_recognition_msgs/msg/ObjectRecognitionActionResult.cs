/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionActionResult : IDeserializable<ObjectRecognitionActionResult>, IActionResult<ObjectRecognitionResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ObjectRecognitionResult Result { get; set; }
    
        /// Constructor for empty message.
        public ObjectRecognitionActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ObjectRecognitionResult();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ObjectRecognitionResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        internal ObjectRecognitionActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ObjectRecognitionResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectRecognitionActionResult(ref b);
        
        public ObjectRecognitionActionResult RosDeserialize(ref ReadBuffer b) => new ObjectRecognitionActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1ef766aeca50bc1bb70773fc73d4471d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8Va/1MbNxb/ff8KTfgB6BinCSlNuWFuCDiFDgEK9O56mQ4j78q2ynrlSFpst3P/+33e" +
                "k7Reg2lpWiiTBFsrPb3v7/Pe5kjJQlkx4l+ZzL02Van712M3dC+/NbK89NLXTjj+lZ31f1a5v1C5GVaa" +
                "9l4oV5deWP6V7f3FP9mHy293cXcR+DkKXK4JMFUV0hZirLwspJdiYCCEHo6U3SrVrSqJ4fFEFYKf+vlE" +
                "uS4OXo20E/gzVJWysiznonbY5I3IzXhcVzqXXgmvx2rpPE7qSkgxkdbrvC6lxX5jC13R9oGVY0XU8cep" +
                "T7WqciWOD3exp3Iqr70GQ3NQyK2STldDPBRZrSu//ZoOZGtXU7OFr2oIUzSXCz+SnphVswn0S3xKt4s7" +
                "vgjCdUEbyoEtqsKJDV67xle3KXAJWFATk4/EBjg/n/uRqUBQiVtpteyXigjn0ACortOh9c0W5YpJV7Iy" +
                "iXyguLjjMWSrhi7JtDWCzUqS3tVDKBAbJ9bc6gJb+3MmkpdaVV7A/6y084xOhSuztfekY2zCKbYIfkvn" +
                "TK5hgEJMtR9lzluizta41kX2RN74YIxk9BGWHeIX3U8GfpsCJ3w5750eHp9+K9LPnvgS/5JbKj4mRtKJ" +
                "ufLkkH1F+smD4aOCwt2wub1FHASa+wdXx//qiRbNV8s0ySK1tdAsnLCvSEePInx+0et9OL/qHTaEXy8T" +
                "tipXcG24JUwO96AVeL/zQg48PFl7kt6SgdSM46AaZuI3ftbwF07CWggOh6iclIooaO8SFTC6caXsGNFX" +
                "UirwajOyfPnDwUGvd9hieXuZ5Skoy3ykFbHt6py0MKgpD6xSxEPX7L87u1joha55s+KavmHRi5rdcsH7" +
                "ypuKWv2uasgrnEEYDKQua6seYu+i913voMXfnvjqPntWUSZ/wAM4oEzt77pL5/d57KtcIqcyzeayGnnS" +
                "S3BKGQKZWle3stTFQwJEz2siZU/sPIPnNa5XGc9BuHC+xniNhg/2T04Wkbwnvn4sg32FUqVWcvgY7cIm" +
                "9621zHQ10HZMRY3Kh29nAeZEFUtCtN3k7V8gxOPUTE6xFH7hAiobD/jEydnlVZvUnviGCe5XSRmxeoCS" +
                "KGA1IqKCEmSjAqLSDSjAwcHLgvXWf0TsOaJtSNuk0qmG+IgcWd1JndnaflmaKeMR2ohQsBS3TbECM7FQ" +
                "UYyJFsSiI4Xq18MhqTFu8mrms2csZceHWfCAAEGikpwnc5M8XJOh0ulIA1twPW6lFPYOVRAWOmboUsca" +
                "c1dPOK8q8h9IqRwpCBBHjSewVVniNNF0wXhThasb0sn14JLKUkphjtpQIfKP7BLhBVIx2JsvW2GgVNGX" +
                "+Q15I04E/Ao46ZwcqmAaN1G5Hug8BQNz4LqROmG9sAFMjWsOCuQ5jV3dZDwCIU9kOsNA/NoukHiw4QMA" +
                "PcseOhC3/qKKcHTfWujKNqvX4aB7ZjlWspWtfc6POOrtH/YuxGcdDj/Z0VKblBqJ5C7UXiB0XW51n91t" +
                "YjzShYbbuBydRkjKw9pKEnCXUgAytRlE6aOjU+IC1C/ZqSkC6eij7fbxp0Qt+0w1Xfb2Lw6O/pSaYuzl" +
                "ZsvkAXMCw40lomGG8PBTpYJoC/dqmB6URqIfghjorJrDzx0+d7X693lcVGXwuNDUQDPQiptwQ4ZsRpqc" +
                "oEMLTzvi9OwqrqFiXuelqYvUm9713z8u0tk7ApXi+PT92efLRVIdmIryNtrwCpl8zCFBYDkiG44lgghR" +
                "OjYOxQr1+HWVHIaGAASF8QBIlXSgVQm1jPVw5GNRxxojijYkDe2+ykecqdFMGt5MO+n5mNDpnf2oykYx" +
                "wOJCERAQ7U02YpaZEbXM428n6Ss6RmdBhxNEQR6/K0aAD47g/dzUQgaYpWOOCBRZP8Q2GnRcaoWp1KLm" +
                "pkD7kre9apQJh4hgBNsboor2Ay2lABQLVv64nyQvOTj54fKqd3H5uZ6SRfPyLIT1EJKjt0ARzPR2Edw8" +
                "6qWvSkNoKQRF0FJHxETKMeCShYzVQ4aXQHTOWIerNoIyCKRw9ZPhzuBLI3mLXIyG0S7ObGbhQzDnOTFy" +
                "QOFG6asVfW5Jjk4jCDDXPKET+HWffIbwQt/MXrqRnCCWcV9F/G8XralVlGcR8xnvDlx8UG7UkLrGfaMV" +
                "t/PNLNFrtNTVBEHnTOJrneZ4PBQbAvZzGXMqnPuZAM5QEZSGf3hT2w5FCvE+i1FQSmxh4QGfEE2Iv1JX" +
                "N2G6xmbQFjvI92TtDQU+DYrm2VARj3beUibU2EgS7/vMsibOzy57f0HWSiYI1YvBIjse+cW8b4o5lW6u" +
                "5bsLG8FeoV/n7E1KrQK0pB2s/YAmARtxkELdykLL6uWY+hfyTOqdXS3Le0py6t8gdGB47oZYvYzjSrr3" +
                "78GclM7+TGE5fPqycq+KkPrJUH5qCJJxjphIshbpH44Ll6f8GnA9AqNQA+oqZYUbY5KP7n+j5qtqQPCG" +
                "cF5QWvXUMdgUysEnin7qGEAlESz6bVa4A6MBMCT47vLs9CUNJ+JU+Mf9DyexqexSPxyLBDqgVLiAwW4U" +
                "Jy/VjHa4JDSAglrONR6t9qVTXdHrDrucQO8bvUO1g9JYacwNYvwGNevFr+uk4fXd9QNT56PDd+sdsW6N" +
                "8VgZeT/ZffmyNAh3aNuv/+9FENHytLii2Xt1S5oxlJaD9WIDTcZpaYFKmfbrOKQBLJFXbpSKc/JBqWa6" +
                "r0vt592owRX+mnPvSErkNwBo7g7fBd9oIDhSYLGEKci5wvwUMS5pJMRT+PdgMApLXwWT2RWNAniNVIC1" +
                "uyrY/eqbt2/CjtwAo+ShL1i/z/F6vOny+xMAA6CRkSmLxk5LF19+Ko/SjkCbrxLr06Hb3gkrE2Ox8tWb" +
                "7df8ldANbdA0s4g70CNPjS3uLFcwAQmSLkhtYXg6NkVd0nNPEz5vJuvJoeHaT/VWaHUFvtubEbOOy1bS" +
                "M4Xp6VaBoli54HIBSXTiUGNMjSaXOerEZFHo6JntzOJgZMFjN6yUjkLFEz0/R+32Oed1UAn1ML2xaAdx" +
                "HxjEzkW/NP0OD7dLOaewTG1kGsxGVlTlGwTzIvjli4BWuilhhLsC7OcbIQhVYWOHMvRaqPkbegytbFHU" +
                "blIFf0X4YqOGGGgMVLHZFecLMq51FkwTZKDTLlEGhCnqnFklNuEiVkKACQ1PAlBq9OQQv8rQjfwuyAy2" +
                "BiXBq8C9Dkg+HArMy/xTrV1MOA2EvfPSjarGBmUKhknBjJv3ux3x+pDyY517gtVRiy11dcXxIEFkKI/m" +
                "VkkhHVBhGKgpDwQwPdUFJIxgoFTVEN9WEE1v+AKB9I0PE0+HzbyAbx7JqlKlS6JqmxwiVoroL6wbcppu" +
                "xoZ6T64AuBRTVdY3pqRxpXbXfY0cR3BCoDNwrXdnzYN/JqYiaIWRJjzvPGmEklEkTe8tvHLNCWumaf/d" +
                "E3i0vP8tGOSbFxPVfdgiRR4/6wgHPyOtbiTSXwTNbUapIFLBFawhcmWpqLLmKa3jL4pJercQvOHZsg8b" +
                "YnXyIfOFsJ6k/EMoOMiOwLbzaGQcb2WyhkrIOt04CD8+vXobBuCv4soPcWlPvF7sebXDK9utPbS0J94s" +
                "9pAd6e1Qaw8t7YmduPL+5GyflvbE1+2VnTf0siBLOZ5qQzLJqQyhzP6YnMUMBjTw4g1n4fPAmnF4ecCY" +
                "i1URQjRexE7BlRmHDtNnVdWUZEJWcEoRwrtV6Z4cwMtHRo7ghGNZAZeXaszZM3WDzNlTucVyP8ZhDsTY" +
                "GmKkxqzUjkX3hN+HwBT/iF1BoWYCHkzDCasGoWFPcxAWAh0HWiflPv6U0R1XkQBirKFFF8TXRBRl6UQA" +
                "OYwF6wlvYG5WN2Dp0DOpKomxQmVJLOC+hqlg8Y/bgU81u4binpLbFTpKwZ6nzuP+xKrJnQMLX3UTmasw" +
                "Z0EEzZpP8+bTL8/F/gPtYxIp/Y8Tnokoh5LN/82Cm1puYGXwTRXe0d8pylzC0v9iye5U4/t3P2nP+nuC" +
                "J4mtIok5T8imeV9YLUhdV/GlE/UYLEnkfU1cmOnWWP4MKNJQkskPyC92ZjtQVCNymImnPsXqZnur2aER" +
                "JNpVPVPFlpy1eeStPPADfcIpneB7rUbJcm+wMesIANJfOqjJvt0S/0cQxXvLP65e/i8vbyY3/bi981NL" +
                "mOczHSTaX6Hf++bq8Lt7Q81xeB5ikhyzpeyuCBiq2ZB9X8OLbcV0F/ueR8DF3at8comhO76Jb58WjBNa" +
                "gHf+dppJn6ZZ9n/G+OjVAigAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
