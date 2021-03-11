/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ExecuteTrajectoryAction")]
    public sealed class ExecuteTrajectoryAction : IDeserializable<ExecuteTrajectoryAction>,
		IAction<ExecuteTrajectoryActionGoal, ExecuteTrajectoryActionFeedback, ExecuteTrajectoryActionResult>
    {
        [DataMember (Name = "action_goal")] public ExecuteTrajectoryActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ExecuteTrajectoryActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ExecuteTrajectoryActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryAction()
        {
            ActionGoal = new ExecuteTrajectoryActionGoal();
            ActionResult = new ExecuteTrajectoryActionResult();
            ActionFeedback = new ExecuteTrajectoryActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryAction(ExecuteTrajectoryActionGoal ActionGoal, ExecuteTrajectoryActionResult ActionResult, ExecuteTrajectoryActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryAction(ref Buffer b)
        {
            ActionGoal = new ExecuteTrajectoryActionGoal(ref b);
            ActionResult = new ExecuteTrajectoryActionResult(ref b);
            ActionFeedback = new ExecuteTrajectoryActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryAction(ref b);
        }
        
        ExecuteTrajectoryAction IDeserializable<ExecuteTrajectoryAction>.RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryAction(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) throw new System.NullReferenceException(nameof(ActionGoal));
            ActionGoal.RosValidate();
            if (ActionResult is null) throw new System.NullReferenceException(nameof(ActionResult));
            ActionResult.RosValidate();
            if (ActionFeedback is null) throw new System.NullReferenceException(nameof(ActionFeedback));
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
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "24e882ecd7f84f3e3299d504b8e3deae";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVZa0/bSBf+bon/MBLSLrwidIHetlI+hGBotiFmE4O0WlXWxJ4k3jqerD0mzf769zkz" +
                "HtsxoUXaLaCWxJ4zZ87lOZc5uF9FWCjhZ/wvESqZbXqhimV6JXnCuP4azPHdcXfTjUVeJMpSZvrpMdpL" +
                "IaIpD79Y6ln5vOd0/+OfPed6cvWBLeW9iFWwzOf5q2/oued8FDwSGVvoD8eIl8RTs5NIBheMrBDE0UPl" +
                "tKlo9YfpkavIiGLk3HP22UTxNOJZxJZC8YgrzmYSCsTzhcg6ibgXCXbx5UpETK+qzUrkx9joL+Kc4d9c" +
                "pCLjSbJhRQ4iJVkol8sijUOuBFPxUmztx844ZZyteKbisEh4BnqZRXFK5LOMLwVxx79c/F2INBRscPEB" +
                "NGlO5ooh0AYcwkzwPE7nWGROEafq7JQ2OPv+WnbwKOZwQ3U4UwuuSFjxdQVokZw8/4Az/meUOwZvWEfg" +
                "lChnB/pdgMf8kOEQiCBWMlywA0h+s1ELmYKhYPc8i/k0EcQ4hAXA9Wfa9PNhgzOJ/YGlPJWWveFYn/EU" +
                "tmnFl3TqLOCzhLTPizkMCMJVJu/jCKTTjWYSJrFIFQP2Mp5tHNpljnT2L8nGIMIu7RF88jyXYQwHRGwd" +
                "q4WTq4y4a28QVH8YIHdGCMHShw7GdflCFkmEB5mR1AZSDO5cL2L4ROtBQcPWPGcZYSaHHoShgXa5RiWs" +
                "wtPyNPg5uwc61guRslgx6Cpywi2gIZYr5KAkwW7imRvgrAWOrlizqUCIQAQWikxxOI8kapq4lD+OrFtg" +
                "YYgHz8ja1MxmLUgWYYdJeQjDPOdzof3A8pUI41kcGgVLCfLjkjvFiCGAUMsiV5CMIfBAdWxdCCrnZdKi" +
                "SYjOWE6lqt/C2vbr88jVOn/PqQUwBL9JpIuGgH/Rc9AQs73hGm6KL7zL9sYlvQ8iOQsesPhhmn5HmXZB" +
                "MqD483OpZIrwzp3Wnht6BMmKPvMXE12LQZnA5RTltZm1XBXwkdIRKNBwJfOYojv/84ihRiDGFFbxwMNQ" +
                "JKhQevHzZ3CU29RihnBWn3Xd0/mlAVZJISUMtHVO6SVJI+jueYKIZBzZgNIA5SzKrShnkAjpiN5oUzNt" +
                "aiJqqXkM3R1nlkiu3r7WVi8Fa7yr1Wm83FKr8d5o40SFWdK5KZhlchkgN2Hh2fz5SJjY5G4QWSVoWNqY" +
                "tSxfrZaAcqReyMQMyZgKqM63O3xmNc9b2CfnYTvVf1RGYxw5Q2LWcdtB3JauavA6EAQ+gzgqk7SW5jh0" +
                "qfcfWngaCp5IZNyWOOsYiEHFT4qItABWMhQCHFZ5+lXt31fbXt035WdRIkljKxHpHJWhfFVxa6DsSBtr" +
                "a5MBpCWjek8NVhudmtmxsztLPOLOl8oW3xKnyhptz4awv0VZw5fsoECHKNlbBoaHzlxI9ML2IN9SQclq" +
                "R16WeMQ98USO4DZKN430YNEps3iO0Icctcnbx6zjXG3H+oMjqIGp4fHvztkG2guki0esbNIDYq2KVAPj" +
                "2llTodYCnZtaywdpQmfYGdo4GIeH6IKcO42LM7M/0To6vxfYkKWkbiZNJnguPUtxdmlJCKLFlgqsamTR" +
                "J6PJEJxSlKx3YmMUZ9gKNXRnmOn29Iha20jCJKlU4LHkX8BS4CKhu9DVKqliwJiFXmPLgTieHx+Z5lhT" +
                "URepr236oodelEAWtTKh5slK7Y6Ymp2axKdlNofBi9TllgY/PGaDGdvIAn0xdMCXrLxf6pJr5dL3ICXl" +
                "EVWJksW2RXXEV11znKJD56jUtiKyr9W3TfXtn2fydg20nQ5HpOLqYWvRltvp6e8apmTn7+lUfVs/W9BS" +
                "Lqk0s1frvM6E2ypNM/mFblypBlqOu2kqcHmlYsXTuZ4E0FAAwwUbtCVJ/VzSvdCNpjmoesKoB5MVVeC6" +
                "rz8eTnvKgZe59j3nBdvItee0xlE0RHlvpTUPN+7oYjC6Yvany37Bb9O/6RspNRkbocqgxWU3NMOVcgix" +
                "dd8uefb6/uDOZQ2eJ9s8aepRZOjxFJLGVFD6eRLjm7HrXt/47kXF+HSbMXKkwPgIaKOKivJnL9SMzxBo" +
                "lPx0zQOF6fhxtFML+vBnH/9tq2WGOph8rRJBHAjgJRcIeuCLbIk6ldC4TYnDUuTJbb/vuhcNkc+2RaaR" +
                "BhqZGGM4TEAK1Ow8nxU0a9tliMeO6Z1749oudMzrHcdMcW2gIV2h5wa17DtPigrxXdPonh75gs14nBTo" +
                "Yx4Rb+z+5vYb8nXZm4fiZYLC5hEE6GGJLFQbLkffl3EqQl6WlvqwAh0FDXZ09dEVGRc91LxHFCiRV0VK" +
                "F53kj0deBT3Udx2ENfgq51UW7veGwzqSu+zdUwUsZ127JHyKdeGTh97aFjqdxdmSboB01ancoGefJImI" +
                "tpRowuT9f6DE08xMoNgKP3MAjWYfwcTQm/hNVl32q2bYq6aQ5YSWmvsIXiMmwhiBVyYgLtTT4Ws5BiW7" +
                "TZ8Qe7ozRDEzF4M1GsRdM1DcMTDQkOvqBoFQyLanlBw20xlBDyQbdY22RGJazHHlmNtxoxJf1UsNHG1h" +
                "dpxrkA2Um2Uy60uavgr6GoT4/jyytQXYc8xfKJBG6FpHfsGQGY5c8PtYZuWqLgeTSfekfL7sDYa3Y7f7" +
                "K/045cubYW80QigHtOpedDuWejC66w0HF8G15w+8UUB03c5pudh4GZSEPaTc4PyPwB3dDcbe6Nod+UH/" +
                "Y2905XY7Z+W2vjfyx96wOut1+f521DsfuoHvBb3fbwdjN5i4o4k3DsC01+28Kan8wTWO8G79buetld4W" +
                "6W7nHVlilfA0JeD8xL4A/3SZwB9EbNxqt+XWOn5v7Af47btQIeh7yGgTKAUL/LKD5G7gDfE5CW56/kdQ" +
                "jyb+uDcY+RPQn1hjXnm9YZvZaXPtW1zOmoSNJbuJfPPaaXnnauzd3gSj3jWsfPKmvdjiBJK3LZKxd+6V" +
                "KmL1XWsVOf6TZf6+teadU5m1q8AT/nqwwW1puW3myzEIAggwmlx64+vAgrBzaoFWGQtwcfufCIvAwx3o" +
                "CBQgtBZsyEq/9Zo1WgmYwejSq9ZgrP0mDLbkGnnB4FMw8Ya3hGRA9ORFbwGX1Z+c/+09wHKq/h70MmrV" +
                "CtkBoJZY7Dn/BzErlQ7ZHwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
