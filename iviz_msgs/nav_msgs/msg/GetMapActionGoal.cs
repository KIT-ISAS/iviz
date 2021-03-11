/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapActionGoal")]
    public sealed class GetMapActionGoal : IDeserializable<GetMapActionGoal>, IActionGoal<GetMapGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public GetMapGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = GetMapGoal.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, GetMapGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMapActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = GetMapGoal.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapActionGoal(ref b);
        }
        
        GetMapActionGoal IDeserializable<GetMapActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new GetMapActionGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4b30be6cd12b9e72826df56b481f40e0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwYrbMBC9G/IPAznsbiEptLdAb0uzOSwUNvcwkSa2WFlyNXJc/32f7CS7hR566BqD" +
                "kDzz5s28Jz8JW0nUTEvFJrsYvDseWq318zay3z1SjeXgbLWV/MxdOZyOFtW3//wsqueX7YY027n+08Rq" +
                "US3pJXOwnCy1ktlyZjpFsHZ1I2nl5SweWdx2Ymn6msdOdI3EfeOU8NYSJLH3I/WKoBzJxLbtgzOchbJr" +
                "5Y98ZLpATB2n7EzvOSE+JutCCT8lbqWg41X52UswQrvHDWKCiumzA6ERCCYJqws1PlLVu5C/fikJ1XI/" +
                "xBW2UmP2t+KUG86FrPzqkmjhybpBjU9zc2tgYzqCKlbpfjo7YKsPhCKgIF00Dd2D+Y8xNzEAUOjMyfHR" +
                "SwE2mABQ70rS3cM75EJ7Q4FDvMLPiG81/gU23HBLT6sGmvnSvfY1BojALsWzswg9jhOI8U5CJhgucRqr" +
                "kjWXrJbfy4wRhKxJEaysGo2DAJYGl5tKcyrokxrFnx9myL9ei2LLPXqYpdMm9t5iE1NhPVuKIOfQOGgy" +
                "9VEuDQ2slIpnFH0UD+0mySdXYiocLtWgczrDHUMjgVwm9CpafAtrSNtlwsyRXTB1Ns4gKH2DpqPgioAC" +
                "GUmZIV5h9H7E1czf2assmDDoQZn4Nmo6idgjm1cws8iAL3ufcQ1VuZZJB9JOjDs5Mzd4YaDrCzqSLgEg" +
                "1faawYxw8RC1vkqIqI9TL/D5otvt/7VAtd8Xvrze+gQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
