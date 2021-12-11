namespace Iviz.Roslib.Actionlib;

public enum RosActionClientState
{
    /// <summary>
    /// Waiting for acknowledgement of a goal
    /// </summary>
    WaitingForGoalAck = 0,

    /// <summary>
    /// The goal has yet to be processed by the action server
    /// </summary>        
    Pending = 1,

    /// <summary>
    /// The goal is currently being processed by the action server
    /// </summary>
    Active = 2,

    /// <summary>
    /// Waiting for result
    /// </summary>
    WaitingForResult = 3,

    /// <summary>
    /// Waiting for acknowledgement of a cancel
    /// </summary>
    WaitingForCancelAck = 4,

    /// <summary>
    /// The goal received a cancel request before it started executing, but the action server has not yet confirmed that the goal is canceled
    /// </summary>
    Recalling = 5,

    /// <summary>
    /// The goal received a cancel request after it started executing and has not yet completed execution
    /// </summary>
    Preempting = 6,

    /// <summary>
    /// The goal received a cancel request after it started executing and has not yet completed execution
    /// </summary>
    Done = 7
}