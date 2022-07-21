/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformAction : IDeserializable<LookupTransformAction>, IMessage,
		IAction<LookupTransformActionGoal, LookupTransformActionFeedback, LookupTransformActionResult>
    {
        [DataMember (Name = "action_goal")] public LookupTransformActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public LookupTransformActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public LookupTransformActionFeedback ActionFeedback { get; set; }
    
        public LookupTransformAction()
        {
            ActionGoal = new LookupTransformActionGoal();
            ActionResult = new LookupTransformActionResult();
            ActionFeedback = new LookupTransformActionFeedback();
        }
        
        public LookupTransformAction(LookupTransformActionGoal ActionGoal, LookupTransformActionResult ActionResult, LookupTransformActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        public LookupTransformAction(ref ReadBuffer b)
        {
            ActionGoal = new LookupTransformActionGoal(ref b);
            ActionResult = new LookupTransformActionResult(ref b);
            ActionFeedback = new LookupTransformActionFeedback(ref b);
        }
        
        public LookupTransformAction(ref ReadBuffer2 b)
        {
            ActionGoal = new LookupTransformActionGoal(ref b);
            ActionResult = new LookupTransformActionResult(ref b);
            ActionFeedback = new LookupTransformActionFeedback(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new LookupTransformAction(ref b);
        
        public LookupTransformAction RosDeserialize(ref ReadBuffer b) => new LookupTransformAction(ref b);
        
        public LookupTransformAction RosDeserialize(ref ReadBuffer2 b) => new LookupTransformAction(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) BuiltIns.ThrowNullReference();
            ActionGoal.RosValidate();
            if (ActionResult is null) BuiltIns.ThrowNullReference();
            ActionResult.RosValidate();
            if (ActionFeedback is null) BuiltIns.ThrowNullReference();
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            ActionGoal.AddRos2MessageLength(ref c);
            ActionResult.AddRos2MessageLength(ref c);
            ActionFeedback.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "tf2_msgs/LookupTransformAction";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7ee01ba91a56c2245c610992dbaa3c37";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VYbVPbRhD+rl9xEz4EOsQ0kKQpEzLjgiBuwabGZNrpdDxnaSVdkXTO3Qnj/vru3p3O" +
                "NpjATAPxgC2d9vaefd/VqZRXzXSkeK0zqapuYoSsTyQvGbeX4xyvo9N1VEPQTWlaOmXv1lMeA6QTnly1" +
                "tJm/jw6+8Sc6uzjZZybbHVc61ztrwZBw0SfgKShW2J/IwSrFxG0jit4RI8nHIr0tklWO1crToNcmdTAc" +
                "xmiDXRhep1ylrALDU244QxysEHkB6lUJ11DiJl5NIWX2qZlPQXdw46gQmuFfDjUoXpZz1mgkMpIlsqqa" +
                "WiTcADOigpX9uFPUjLMpV0YkTckV0kuViprIM8UrIO74p+FLA3UCrHe0jzS1hqQxAgHNkUOigGtR5/iQ" +
                "RY2ozd4ubWAb7K+h1K//jjZGM/kK1yFHUwQUzBTcEGq4maJTEWCu9/GwH5yUHTwEtQR4XKrZpl0b463e" +
                "YngaYoGpTAq2iSKcz00ha2QI7JorwSclEOMEVYFcX9Kml1tLnGvLuua1bNk7joszHsO2DnxJplcFGq8k" +
                "NegmR00i4VTJa5Ei6WRumSSlgNow9D/F1TyiXe7IaOOYlI1EuMuaBn+51jIRaImUzYQpIm0UcbdmIXd9" +
                "IrdcGyPWxzxYpgvZlCneSAVWLisI2nJWCDSIFYLihs24Zoo8R6MQ5Ek9a2/rm6gSXvvD0MjqGl1jVkDN" +
                "hGEoKGjyXvQLqKaYesoSdxNP7bxmBnh0YM0mkBEWzhJQhqPlCNGyfj1+kbY2QfUivDkdEvTMspC+6hR3" +
                "uEyHwag1z8EagekpJCITiRPQI9Adz50ixREgqKrRBpExDD+k6rT2I8s9cz60mbA933CVgxlbN2rXtGxU" +
                "An7Nqc2tWD2mjeLWTHQnG4P4LY3nZGla5xQ3kHo+UTSREstLes0xtp7MX79eBFztergMYOo1jSZvwZ/b" +
                "lcAXQF/5nkaMezFFt6oUJdj3LVB3cx73j3r9E9Z+DtiP+O1c3vppgYE4B0Pejg6JIZC4fOvz0koUep7d" +
                "w1Hvc8yWeL5e5UmJsFEKExoWgQmQ9R/F+HwYx2fno/goMN5dZawgASwtKYUzeU4IM8Yzg0bEBIHSK4p7" +
                "uLF1qM4j9pXPBv5jRFstuDyPVXFaAnEQRrdcEOjmCFSF1a+kUmxgy0O+uDw8jOOjJch7q5Ap0fGkEECw" +
                "dZOQFrKG6vA6Rdx3TPeXwXChFzrmzZpjJtKKjkFJKl9gX3tS2sCDqiGv0BLjOeOibDCN3gNvGP8aHy7h" +
                "O2Bv78JT8A8k5h4PsCkUE8htd9l+GOMEEo51w/IMhzXYp1C6t4UZOyVRX/MSc/w9AnjPC5FywN49g+cF" +
                "16ulsUG4cL5gvKDhw+7p6SKSD9hPjwXoK+A6hI/RLtrkrrVWQdeZUBU1lVR/zXIWsEggXRFi2U3efwMh" +
                "HqdmcoqV8HMHULd2j0+cDi5Gy6wO2M+WYTf0Jr5pQ04sRasRE3BK4EEFxKXjunDfHJHeJo+IPU28JWmb" +
                "VDoTKP6azgj7l25ZypmdB4gQQ0Gt9i6c+RJs25SlkkZbUpg0eU5qbJsAuDHP3ob4WpwDJhuj5o4mPL3w" +
                "k4lpF6LAZ3S8GyuFcgB9PxHqr8Nqh6x2VqH+MSDFnlxWd0Yn3210QsO+QRazc8BtSiwdpW+bLGF7Wtt6" +
                "0qXUpp3rfG5Fqg+cFQqygxeFMdP9nZ2ZuBIdJXVHqnzHZC8+muzDDv+IE15yhYw6tOcCwJa9VCZNhe7n" +
                "ejtyk8pGX00i2cVOdKtx8t6zChcdmdA4SfBWZk5IInKrUdDmknWf14ytRhWQ/VBq7SaXAGwCZgY4fpiZ" +
                "vGMfTWNyhrMIdvc8wVY++oxFTqo9t7+0yop+b3CDqkmZSjqtPo+QHswaETm7ts9u4WdhFMOOc45exmvb" +
                "G4aduDHFVGRTkJ1tlM3M25SaUwm2LCCPil8BZTCqy5iLplNkxpd1Qsu4ZRM6eWfbjXeWipzIvn6wLyxw" +
                "mlIiF0uhHzZz5oXbpqyCua0sHWZ3GJqQ5jSv7a0O62VsLhusAygDXij/nsS2vS0u2y0YKbeZbygsjmWF" +
                "nkusDYvgq3HG5Di/RlkpuXn3ht2Eq3m4+vdZTL3wsXXWrrEJEiGiV2xOd18WDkpKflCg9mr25IWiTfC+" +
                "KPcH43g4HAxpkAl1evDb5XlYfu2XDwf9fkyTSm/0Z3i46x/Gf4yG3fPBaXfUG/TD0z3/tNf/3D3tHY27" +
                "w5PLs7g/CgRvPMGodxYPLhfrb9v1Ybd/cTwYnoUn7yL/yNUnnyXtzdjdfJ/ht30d+z/H3/BW9zu9zg1i" +
                "RP8BrN3uPcUWAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
