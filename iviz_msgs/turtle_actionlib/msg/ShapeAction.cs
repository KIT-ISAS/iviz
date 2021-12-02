/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ShapeAction : IDeserializable<ShapeAction>,
		IAction<ShapeActionGoal, ShapeActionFeedback, ShapeActionResult>
    {
        [DataMember (Name = "action_goal")] public ShapeActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ShapeActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ShapeActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public ShapeAction()
        {
            ActionGoal = new ShapeActionGoal();
            ActionResult = new ShapeActionResult();
            ActionFeedback = new ShapeActionFeedback();
        }
        
        /// Explicit constructor.
        public ShapeAction(ShapeActionGoal ActionGoal, ShapeActionResult ActionResult, ShapeActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal ShapeAction(ref Buffer b)
        {
            ActionGoal = new ShapeActionGoal(ref b);
            ActionResult = new ShapeActionResult(ref b);
            ActionFeedback = new ShapeActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ShapeAction(ref b);
        
        ShapeAction IDeserializable<ShapeAction>.RosDeserialize(ref Buffer b) => new ShapeAction(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVX204jRxB9n69oiYeFKLDJbi4bJD84YAgrdhdhJ69We7o800nPtNMXjP8+p3outsEI" +
                "K1pYC7DbU33q1L0Yl3JBwzxoW19aaYRMH6cFPmfj9bNb8tGE7qlLp83nF0RqJvN/Ool5e84GX/mVfRpf" +
                "nooQXTA0bZQZPXv7wIzsD5KKnCjTW9bLTStf+LcscXUu2MapVo0Zyfhk9csw9kE1yhtm2YEYB1kr6ZSo" +
                "KEglgxRzC8a6KMkdG7ojg0uyWpAS6WlYLcif4OKk1F7gp6CanDRmJaKHULAit1UVa53LQCLoirbu46au" +
                "hRQL6YLOo5EO8tYpXbP43MmKGB0/nv6NVOckrs5PIVN7ymPQILQCQu5Iel0XeCiyqOvw/h1fyA4mS3uM" +
                "IxXwe69chFIGJkv3C2QN85T+FDq+a4w7ATacQ9CivDhM301x9EcCSkCBFjYvxSGY36xCaWsAkriTTsuZ" +
                "IQbO4QGgvuFLb442kJn2qahlbTv4BnGtYx/Yusdlm45LxMyw9T4WcCAEF87eaQXR2SqB5EZTHQSSzUm3" +
                "yvhWozI7uGAfQwi3UkTwLr23uUYAlFjqUGY+OEZP0eDcfKFs3FkQKbVassKXNhqFg3VMuckngVguS42A" +
                "JCO4XMRSeuE4YTyM4AS6SvFOKQmXyLpVhiC7O6TGsqRa6CBgKHlOWuQFVQv0FmNwmzF9kzVLguoeWswI" +
                "9QEKIicXJCLHjDb92/LXqosJ3At6CItd+1l0nQnMFG40rQw16L0sKAVB+AXleq7zxsCWgT9p0blAGgGQ" +
                "qqIPYCZQdZA66eLHkXvV1peaXtZUI6mCfDY3VvLJSaWj/wZ9uBkZz3di9MEQUZzprWnG7bRpx8zLUH+S" +
                "SfZgPHCP+9DRaw43o8/nV58vRfcaiB/wt0m/lDMlimJFSHLLyYF0zJve1/aIrYpoMYdnk6u/RmID88dt" +
                "TG5K0Tk0F/ThGXGa7QV8czsafbqZjM574HfbwI5yQndHZ0bXQ4fsU17IeUDoUKyw3nEN0n0aBXWRrYk+" +
                "fh3gF9WVvND0XAymhSFG0MF3KCB6OCFXYQAZnoaBjlrK4z/Pzkaj8w3K77cpc9OReakxJdGjYs5emEce" +
                "hbsc8ZSa4e9fbtd+YTU/7VAzs8l0FVNlr7nv1KQiPesazgpv0bbmUpuIlvYEvdvRx9HZBr+B+PkxPUd/" +
                "U57a4y463M5sDA/T5fvnOc4ol+jhCbNXFrEqcOtNQxLLiq7vpEG/fcKANvP6ShmIX14h8/rUq21IRbhO" +
                "vj54vYfPhtfX60oeiF/3JdhOo10M9/EuYvI4Wtuk67l2Fe91PAv7MKTVhJmQ2jJiM00+fAUj9nMzJ8VW" +
                "+TUKeHN6Iieuv4wnm1AD8VsCHPZ7QrtAAUkoRI1BqHGC7F3AKDyP8bFdVNhvsz1qzzO2ZW+zS5ca5u/Y" +
                "UrBLDI2xy7SSsyBKwW3vERI+Sx0hrQwbg4yvKJrFomA3tkKB7sMrrwTt/O0XAd7NnbZuKuvCUP+1XFgY" +
                "WH2DBeGi+x/x/6wI3eV+n3tVA3rqWfYfPWMEUEEPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
