namespace Iviz.Roslib.Actionlib
{
    public enum RosGoalStatus
    {
        /// <summary>
        /// The goal has yet to be processed by the action server
        /// </summary>
        Pending = 0,

        /// <summary>
        /// The goal is currently being processed by the action server
        /// </summary>
        Active = 1,

        /// <summary>
        /// The goal received a cancel request after it started executing and has since completed its execution (Terminal State)
        /// </summary>
        Preempted = 2,

        /// <summary>
        /// The goal was achieved successfully by the action server (Terminal State)
        /// </summary>
        Succeeded = 3,

        /// <summary>
        /// The goal was aborted during execution by the action server due to some failure (Terminal State)
        /// </summary>
        Aborted = 4,

        /// <summary>
        /// The goal was rejected by the action server without being processed, because the goal was unattainable or invalid (Terminal State)
        /// </summary>
        Rejected = 5,

        /// <summary>
        /// The goal received a cancel request after it started executing and has not yet completed execution
        /// </summary>
        Preempting = 6,

        /// <summary>
        /// The goal received a cancel request before it started executing, but the action server has not yet confirmed that the goal is canceled
        /// </summary>
        Recalling = 7,

        /// <summary>
        /// The goal received a cancel request before it started executing and was successfully cancelled (Terminal State)
        /// </summary>
        Recalled = 8,

        /// <summary>
        /// An action client can determine that a goal is LOST. This should not be sent over the wire by an action server
        /// </summary>
        Lost = 9
    }
}