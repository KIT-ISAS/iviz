/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryGoal : IDeserializable<ExecuteTrajectoryGoal>, IGoal<ExecuteTrajectoryActionGoal>
    {
        // The trajectory to execute
        [DataMember (Name = "trajectory")] public RobotTrajectory Trajectory;
    
        /// Constructor for empty message.
        public ExecuteTrajectoryGoal()
        {
            Trajectory = new RobotTrajectory();
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryGoal(RobotTrajectory Trajectory)
        {
            this.Trajectory = Trajectory;
        }
        
        /// Constructor with buffer.
        internal ExecuteTrajectoryGoal(ref Buffer b)
        {
            Trajectory = new RobotTrajectory(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ExecuteTrajectoryGoal(ref b);
        
        ExecuteTrajectoryGoal IDeserializable<ExecuteTrajectoryGoal>.RosDeserialize(ref Buffer b) => new ExecuteTrajectoryGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Trajectory.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Trajectory is null) throw new System.NullReferenceException(nameof(Trajectory));
            Trajectory.RosValidate();
        }
    
        public int RosMessageLength => 0 + Trajectory.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "054c09e62210d7faad2f9fffdad07b57";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXXW/bNhR9568gkIc6g+MA7dCHAHsY0HXLgGLdGvSlCAxaupLYSqRKUnG8X79zSX1Z" +
                "SdACbT3DgEXxfp17z72kxT92Z8ONUx8pC9YdZBgfhfjlO3/Em3e/X8nG3pEO28aX/nLhXUze0/6fVpt5" +
                "dB95vQ1PK7zp6qBf/fV6qdjw+21ui+0DEz8I5hegiD9I5eRkFX+ED06b8sNtj9CohrxYqLzlJURa/vU/" +
                "Km4f8hRwClCcyXdBmVy5XDYUVK6CkoVF4LqsyF3UdEc1lFTTUi7jbji05DdQvKm0l/iWZMipuj7IzkMo" +
                "WJnZpumMzlQgGTSwzvWhqY1UslUu6KyrlYO8dbk2LF445Iat4+vpc0cmI3n96goyxlPWBY2ADrCQOVIe" +
                "ScWmFB1S9uI5K4izm729wJJKpH90LkOlAgdL960jz3EqfwUfPyVwG9hGcgheci9X8d0WS38u4QQhUGuz" +
                "Sq4Q+dtDqKyBQZJ3ymm1q4kNZ8gArD5jpWfnM8sc9pU0ytjBfLI4+fgas2a0y5guKtSsZvS+K5FACLbO" +
                "3ukcortDNJLVmkyQtd45xZ0EreRSnL3mHEMIWrEi+FXe20yjALnc61D1hE3V2Or8f+qi2BKA/JtC7ifZ" +
                "1CLSt5TpQhOKipBR7dZ6HTR48mEtwRIACtjFQmUZ1eBo3Ly9hUV7LE0FOB9uI/M5eTNfoPMO5b9n7lHO" +
                "zPy1rkffOYpVdwhBOZQrlc9zdkFoRKR8fBO7HhQAGVlogXID6EIUtVXh5c9xAPSBzd5NcGYvj2DN3ic0" +
                "Iu/SVmTMtnC22YIA2DhRMZ8Y1nFyUD8ZmXvD1Eg57dm7mAgSfI8bjgpycShERj9SsAG2X8xgrhzUuf3R" +
                "GCkztsAgiofHBQ6Pvk4zWyti5iW6cZfwnvFw2kT984GbSULVFj2zCGevQRc0fN3ljAJEcerAjscyX07F" +
                "vTwu6VkaW1VPo0ismkwZqoFZo7UZxdYxWUdKiY2DGLc7z9clNaOxjXj8tHqimqc5tb6SWkfzYlnWDMkf" +
                "KDYrpFx1OB2sfClh71yUZHEODn5uBikgHDWAMlIYHc82MR3U0J+H2WAYqGmdLtH0iGPK99LNXvtw3OUP" +
                "XIA5M258m59jlp18UDyR4+FKMfZoIvBUqR2FPRHC3NsHAyIO1sIRCN+qDDcI8T5y4kXSryNA8XcHBWcY" +
                "q7NpBpwGZB/MIxCZO7y3iJ/78zoOHWtw32lI8WSykyYUc+2gCgybxBUkidZSB5lb5MNYboVGfYJJwvWB" +
                "tVXb1iP7U074NVRWtCk3a7mvkN8oxcd/vKzF653OJNMrXwzAaFP24NYyFM/TvIsxJ2coIYwM2T7fyOtC" +
                "Hmwn9wwID66/VcZjdogr3n6CtWs+HHoTxwmNrY60eK9KPnl9wJRH1ftTUN6PT4fx6d+TlHri2GPVRoM6" +
                "vpil9B3VnFefJ4Jykr8IqOif9ifqVR4gA6zhKu2n6XeMZ+fsJ9AJhWKKedxFDeGyyqeTMmW8+fOfAPyZ" +
                "GHq1F5nWvZwQ/wHqm2rqyg4AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
