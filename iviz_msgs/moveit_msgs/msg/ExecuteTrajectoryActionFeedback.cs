/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ExecuteTrajectoryActionFeedback")]
    public sealed class ExecuteTrajectoryActionFeedback : IDeserializable<ExecuteTrajectoryActionFeedback>, IActionFeedback<ExecuteTrajectoryFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ExecuteTrajectoryFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new ExecuteTrajectoryFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ExecuteTrajectoryFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ExecuteTrajectoryActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new ExecuteTrajectoryFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryActionFeedback(ref b);
        }
        
        ExecuteTrajectoryActionFeedback IDeserializable<ExecuteTrajectoryActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Feedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QECH2EHttknTpgZ0UGXFUeAkhq32anDJ0S7bXa5KciXr3/cN90NS" +
                "LSE6JBFk64t88/jmzXDek9TkRB5fEqmCqWxh0sfSZ/7Hm0oWD0GG2gsfX5LpE6k60NzJv0mFym3eEelU" +
                "qn/Eon0zSEZf+TFIPj7cXIGBbli9j1wHyVCAm9XSaVFSkFoGKRYVzmKynNxFQSsqmHe5JC3ir2GzJH+J" +
                "jfPceIFnRpacLIqNqD0WhUqoqixra5QMJIIpaW8/dhorpFhKF4yqC+mwvnLaWF6+cLIkRsfT0781WUVi" +
                "dn2FNdazbAaENkBQjqQ3NsOPIqmNDa9f8YZkOF9XF/hIGTLSBxchl4HJ0tPSkWee0l8hxsvmcJfAhjqE" +
                "KNqLs/jdIz76c4EgoEDLSuXiDMzvNiGvLABJrKQzMi2IgRUUAOoL3vTifAeZaV8JK23VwTeI2xinwNoe" +
                "l890kSNnBZ/e1xkExMKlq1ZGY2m6iSCqMGSDgA2ddJuEdzUhk+E71hiLsCtmBK/S+0oZJECLtQl54oNj" +
                "9JiNR6OTb2bIo8UySPg9kpvhhSlwjt92JdR8uJt+up59uhHdYyR+wn92JsVtIpdebCiwJ1NiiVST+1aj" +
                "JjjS7lao2wZzPJnP/pqKHcyf9zE5KbVzEBc+TIllOgn47n46/Xg3n173wK/2gR0pgrvhTGQdDuFvUAA+" +
                "CLkIMLMJfHrHOaLYQRA62RJ9/hjiDz6JKjSeQ2EuC2IEE3yHAqJnc3IlCrDgbhDovKX88OdkMp1e71B+" +
                "vU95DWSpcoMuoWFFxSosam4Fh4Q4Fmb8x+f7rS4c5pcDYdIqHl3X0Zlb7gcj6Zq+KA27wleohIU0Re3o" +
                "GL376YfpZIffSLx5Ts8R9/IjDog1VdXh/3b54cscU1ISbTVi9sFqtMogwZSbBJq1sStZGH3sAK3z+koZ" +
                "iV+/g/N669kqxCLcmq9PXq/wZHx7u63kkfjtVIIp4baigwxPURc5eZ6tfdJ2YVzJ9xrfIH0aYmtmJqT3" +
                "DrFrk7df4RCnycym2Cu/JgDfHEc8cfv5Yb4LNRK/R8Cx7cRoLxAgCY2sMQg1IsheAka5bAYBD4MXOuqW" +
                "nlB7nrErVpslXRscH5WDWPutMxmOi6Jax5GEF6IU8Kba3lcg095VXGNiZ9jiLZrSOstYxnZRoKeQfNfb" +
                "bHbNQxaboBlEWp085r6mquPNDFXXucGEEW/lna4SDUKaJ6JZHGDijHVAKuwnyxbCQcmzRhh0qFwiXUWB" +
                "3Yzpm/ytCaF76M59cCU57iqR0e7A0PJHg2mHDHRj0NvsJ2LRDbEwJHZgyqqLgKHSe5lxhpEdvyRlFkZ1" +
                "9RAZeDYQo/PE1ywAqbKOdYFWZ7DqsssfVn277JXwowlN6o7O6IMk6cjwLEKD5D9NqkU0/gsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
