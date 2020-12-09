using Iviz.Msgs.ActionlibMsgs;
using Iviz.Msgs.StdMsgs;

namespace Iviz.Msgs
{
    public interface IGoal<TActionGoal> : IMessage where TActionGoal : IActionGoal
    {
    }

    public interface IActionGoal : IMessage
    {
        public Header Header { get; set; }
        public GoalID GoalId { get; set; }
    }
    
    public interface IActionGoal<TGoal> : IActionGoal
    {
        public TGoal Goal { get; set; }
    }

    public interface IFeedback<TActionFeedback> : IMessage where TActionFeedback : IActionFeedback
    {
    }
    
    public interface IActionFeedback : IMessage
    {
        public Header Header { get; set; }
        public GoalStatus Status { get; set; }
    }

    public interface IResult<TActionResult> : IMessage where TActionResult : IActionResult
    {
    }
    
    public interface IActionResult : IMessage
    {
        public Header Header { get; set; }
        public GoalStatus Status { get; set; }
    }
    
    public interface IActionResult<TResult> : IActionResult
    {
        public TResult Result { get; set; }
    }

    public interface IActionFeedback<TFeedback> : IActionFeedback
    {
        public TFeedback Feedback { get; set; }
    }


    public interface IAction<TActionGoal, TActionFeedback, TActionResult> : IMessage
        where TActionGoal : IActionGoal
        where TActionFeedback : IActionFeedback
        where TActionResult : IActionResult
    {
        public TActionGoal ActionGoal { get; set; }
        public TActionFeedback ActionFeedback { get; set; }
        public TActionResult ActionResult { get; set; }
    }
}