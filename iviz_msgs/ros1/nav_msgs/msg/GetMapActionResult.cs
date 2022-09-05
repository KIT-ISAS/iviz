/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapActionResult : IDeserializable<GetMapActionResult>, IHasSerializer<GetMapActionResult>, IMessage, IActionResult<GetMapResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public GetMapResult Result { get; set; }
    
        public GetMapActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new GetMapResult();
        }
        
        public GetMapActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        public GetMapActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new GetMapResult(ref b);
        }
        
        public GetMapActionResult(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new GetMapResult(ref b);
        }
        
        public GetMapActionResult RosDeserialize(ref ReadBuffer b) => new GetMapActionResult(ref b);
        
        public GetMapActionResult RosDeserialize(ref ReadBuffer2 b) => new GetMapActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference();
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = Status.AddRos2MessageLength(c);
            c = Result.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "nav_msgs/GetMapActionResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ac66e5b9a79bb4bbd33dab245236c892";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XUVPbOBB+96/QDA+FmySFttfrdYYHDnKUDrQc0HthGEa2N7FaW0olmZD79fetZDs2" +
                "hZaH0kzAji3tfvr229XqHcmcrCjCJZGZV0aXKr2u3Nw9PzSyPPfS1064cEkOyZ/IxRm5uvTChkuy+5M/" +
                "ycn54Vs4zCOIdxHahgASnUubi4q8zKWXYmaAXM0LsuOSbqhklNWCchHe+tWC3AQTLwrlBL5z0mRlWa5E" +
                "7TDIG5GZqqq1yqQn4VVFg/mYqbSQYiGtV1ldSovxxuZK8/CZlRWxdXwdfa1JZySODt5ijHaU1V4B0AoW" +
                "MkvSKT3HS5HUSvuXL3iC2BCXZ8btXCUbF0szxnOaIxAdCuEL6Rk13S5ANAOW7i2c/RZXOYETsERwlzux" +
                "GZ5d46fbEvAGLLQwWSE2sYTTlS+MhkESN9IqmZbEhjNQAavPeNKzrZ5lHUxrqU1rPlpc+3iMWd3Z5TWN" +
                "CwSvZBpcPQeTGLiw5kblGJqugpGsVKS9gPqstKuEZ0WXycbfTDYGYVYIDa7SOZMpRCIXS+WLxHnL1kNY" +
                "rlWePJEsH8yQhG8R4jku7J8j/aZNm/jjdPrh4OjDoWg/u2Ib/1mfFKaJQjqxIs/KTIn5yWLgG4Kib8Tc" +
                "3iAhos29/Yujf6eiZ3NnaJMjUlsLZqHGlJijRxk+PZtOT04vpged4RdDw5YygsYhS4Qc8uAnSAPnhZx5" +
                "KFl5Xr3lANFtSAg9T8R3Phv4g0gCC1FwSM9FSWxBeddaAdDNC7IV0rDkmuBpq4F8/ml/fzo96EF+OYS8" +
                "hGWZFYoYtqszZmFWc0G4j4iH3Oz99fFszQu7eXWPm9SEped1kOUa+72e8pp+SA2rwhmkwUyqsrb0ELyz" +
                "6fvpfg/frvj9W3iWPlPmH1BASChT+7tyGf0YY0qZRHENNjtnNQqml0DKFQIlW+kbWar8oQU0yusyZVe8" +
                "/gXK66SnjQ9JuBZfF7yO4f294+N1Ju+KPx4LMCXsWXQvwsewi5h8G60haD1TtuLdjbcP368CAQnlg0X0" +
                "ZfLmJyzicTSzKAbpFx3wtvGAJo4/nl/0Te2KP4PBPd2S0ewesCRyRI2NUCRBdhSwlUlsBxwEXuaBt/QR" +
                "uefYtmG2mdKlwvKROVLfKZ3Jxl5ZmmVoTHggUsFy3nabFcA0GxXnmOg1WDwlp7Sez5nGZpCnW5/8wq3s" +
                "6CCJCogtSEOS8xxuXk/Yk0HpslDoLcJ+3CspQR2Uc1N0FFqXutlj7vKE+aRZP1glOSYILQ5VC8SqLDGb" +
                "bboYvCXBdWe6lR4kSZZLSkDUbxUa/KguTXuBUgx4q2EUZkR5KrMvrEbMiI0s+krn5JxiaNyCMjVTWZsM" +
                "AYGbNNa56YsDAKqqQ1KgzimMmrTB4ybkiUKn5U0TtF4/nnRPP2ZZvUBKrQ4tiKjk4qlhDBy2HbclblyR" +
                "OByCF2O0Rg2aEffVUUKE7Vhw6vdHs9IQvVSmqlR+JcwMJk3rYpIk7/qHFoGYn+BEcNCeCHg6rxnEdM+V" +
                "nplWG3gXusgAw5rluJKfMQ2dPdlRLGldgm5uj7a3JkJ0C4SNNTQWr+RKGLtgKzXUc7k92tnevsKkT/qL" +
                "NkvNmTDemSRcyC6vgusn10Vv7W04CoNUTnEUyQIbtpIhI9GoNLtKVkiLNCWrIHSQHh4OI9sweLcMRE65" +
                "CJQGIcljQuLZNf++Djm65h5hNmVshi6r5xz8q2SGgXwwWr/DhBM2qnKE4ZJHuav2+BQeNgMKwgHQ3x0R" +
                "nzZOjVUoqu2KGMJlNRL4WplznNrzYYghyXK8NBZcLXD8aSbBUBBpkEMbbhiaJHNCS+btKtJ+GqYEd08U" +
                "4W/9AdveOntiUIE6oAfSGSorapXMaBRaGsM1Ob5XUQDYkAG5nTsRyakBid2A5J8aJdPqYHc97qkkfHeB" +
                "gNIqGM0N1/ymQLT4sRac0QPkwXKjpl6/Erfd3aq7++/XwF9Td19RHPA5BM+/vq5553RF3fv+itq7ZZL8" +
                "DydSp6nWEQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<GetMapActionResult> CreateSerializer() => new Serializer();
        public Deserializer<GetMapActionResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GetMapActionResult>
        {
            public override void RosSerialize(GetMapActionResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(GetMapActionResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(GetMapActionResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(GetMapActionResult msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<GetMapActionResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out GetMapActionResult msg) => msg = new GetMapActionResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out GetMapActionResult msg) => msg = new GetMapActionResult(ref b);
        }
    }
}
