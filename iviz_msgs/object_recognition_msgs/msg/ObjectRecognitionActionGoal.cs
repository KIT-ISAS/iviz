/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionActionGoal : IDeserializable<ObjectRecognitionActionGoal>, IActionGoal<ObjectRecognitionGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ObjectRecognitionGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public ObjectRecognitionActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ObjectRecognitionGoal();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ObjectRecognitionGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal ObjectRecognitionActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ObjectRecognitionGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectRecognitionActionGoal(ref b);
        
        ObjectRecognitionActionGoal IDeserializable<ObjectRecognitionActionGoal>.RosDeserialize(ref Buffer b) => new ObjectRecognitionActionGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "195eff91387a5f42dbd13be53431366b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwW7bMAy9+ysI9NB2QDNguwXYrVjbw7Bh7W0YAlpibG6y5Ilysvz9nuyk7YAedlgD" +
                "A4Yj8vGR71G3wl4y9fOrYVc0xaDtZrDO3t4kDnfX1OG1Ud98bn+IK1/FpS5qDazn82nz4T//mk/3N2uy" +
                "4hcitwu9M7ovHD1nT4MU9lyYtgnsteslXwXZSUASD6N4mk/LYRRbIfGhVyM8nUTJHMKBJkNQSeTSMExR" +
                "HRehooP8lY9MjcQ0ci7qpsAZ8Sl7jTV8m3mQio7H5Nck0QndXa8RE03cVBSEDkBwWdg0djikZtJY3r+r" +
                "Cc3Zwz5d4VM6aPBYnErPpZKV32MWqzzZ1qjxZmluBWwMBzpEb3Qx/7fBp10SioCCjMn1dAHmXw6lTxGA" +
                "QjvOym2QCuwwAaCe16Tzy2fIlfaaIsd0gl8Qn2r8C2x8xK09XfXQLNTubeowQASOOe3UI7Q9zCAuqMRC" +
                "MF7mfGhq1lKyOftYZ4wgZM2K4M1mySkE8LTX0jdWckWf1ag+fSU3vrgcs7WOZMn6NAWPj5Qr5cVPBC33" +
                "vUKQuYm6LrRno1wNY2iiGuhu1nu2JEbC8VgMIucdrLHvJZIWQqNi1bTwhQxjIQwc2RXTFtfsBaUfoakV" +
                "7AcokJNcGMpVRs/ne+Sv/qQJxgt6kCU9zZm2Ir5l9xPMPDKy2BQKdtCMO5lFIBvF6Vbd0uCRga2O6HVB" +
                "lgCQGiYrYEbYOkStTvpV5V5JujTfXJv8dHUtGr54ozVNm1KoYmxy0mYbEmNfv32nrYYieRN00GLNH+D4" +
                "pxE6BQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
