using Iviz.Msgs.ActionlibMsgs;
using Iviz.Msgs.StdMsgs;

namespace Iviz.Msgs
{
    public interface IGoal<TActionGoal> where TActionGoal : IActionGoal
    {
    }

    public interface IActionGoal
    {
        public Header Header { get; set; }
        public GoalID GoalId { get; set; }
    }
    
    public interface IActionGoal<TGoal> : IActionGoal
    {
        public TGoal Goal { get; set; }
    }

    public interface IFeedback<TActionFeedback> where TActionFeedback : IActionFeedback
    {
    }
    
    public interface IActionFeedback
    {
        public Header Header { get; set; }
        public GoalStatus Status { get; set; }
    }

    public interface IResult<TActionResult> where TActionResult : IActionResult
    {
    }
    
    public interface IActionResult
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


    public interface IAction<TActionGoal, TActionFeedback, TActionResult>
        where TActionGoal : IActionGoal
        where TActionFeedback : IActionFeedback
        where TActionResult : IActionResult
    {
        public TActionGoal ActionGoal { get; set; }
        public TActionFeedback ActionFeedback { get; set; }
        public TActionResult ActionResult { get; set; }
    }
}