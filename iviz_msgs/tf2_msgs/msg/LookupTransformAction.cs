/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/LookupTransformAction")]
    public sealed class LookupTransformAction : IDeserializable<LookupTransformAction>,
		IAction<LookupTransformActionGoal, LookupTransformActionFeedback, LookupTransformActionResult>
    {
        [DataMember (Name = "action_goal")] public LookupTransformActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public LookupTransformActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public LookupTransformActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LookupTransformAction()
        {
            ActionGoal = new LookupTransformActionGoal();
            ActionResult = new LookupTransformActionResult();
            ActionFeedback = new LookupTransformActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformAction(LookupTransformActionGoal ActionGoal, LookupTransformActionResult ActionResult, LookupTransformActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal LookupTransformAction(ref Buffer b)
        {
            ActionGoal = new LookupTransformActionGoal(ref b);
            ActionResult = new LookupTransformActionResult(ref b);
            ActionFeedback = new LookupTransformActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LookupTransformAction(ref b);
        }
        
        LookupTransformAction IDeserializable<LookupTransformAction>.RosDeserialize(ref Buffer b)
        {
            return new LookupTransformAction(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7ee01ba91a56c2245c610992dbaa3c37";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVYbVMbNxD+fjP8B034EOgQ00CSpkzIjAuGugWbGsO0nxj5Tr5TuTs5kg7j/vo+K93J" +
                "NpiEmQaXAXwnrVbPvu/6TKnbajLUvDRjpYt2bKUqTxXPGXePNymeo7NVVANhqtw2dNq9raY8ESIZ8fi2" +
                "oR3X7xvR4Xf+2YjOL08PmB3v3RQmNbtPircR/Sp4IjTL3EfkkeVy5M8RSfeYkfA3MnkoldMP7b2YAMYm" +
                "HohHuRFtskvLy4TrhBXC8oRbzgCFZTLNhH6TizuR4xQvJiJhbtfOJsK0cHCYScPwm4pSaJ7nM1YZEFnF" +
                "YlUUVSljbgWzshBL53FSloyzCddWxlXONeiVTmRJ5GPNC0Hc8WvEl0qUsWDd4wPQlEbElZUANAOHWAtu" +
                "ZJlik0WVLO3+Hh2INodT9QavIoURwuXMZtwSWHE/gUcRTm4OcMcPXrgWeEM7Arckhm25tRu8mm2GSwBB" +
                "TFScsS0gv5jZTJVgKNgd15KPckGMY2gAXF/TodfbC5wJ9gEreaka9p7j/I7nsC0DX5LpTQab5SS9qVIo" +
                "EIQTre5kAtLRzDGJcylKy+B5mutZRKf8ldHmCekYRDjlLIJPboyKJQyQsKm0WWSsJu7OGuSoL+aQK+OD" +
                "3HIIGbzpTKaqPMGL0oTauxSDOaeZhE2cHBQ0bMoN0+QzBnKQD3WdyZ1XQiu8rG+DnfUdvGOaiZJJyyCr" +
                "MOS3cA1RTJB68hyniafxjjMVuDqwZiOBEAEEFgttOYxHiBZVXOOXSWMWaBjwYBk1VzVr0hWQJTjhMx3C" +
                "0BieCmcHZiYilmMZewFrBKZVc6cY8QQAVVTGAhlD4IGq1ZgQVNHa86HPhA0Ey3Uq7I1zpmbNqErHol7z" +
                "mvMrTpVJpbmzFL2pykaRp6k5OZrGReW9SGo+UTRSChUmueOIsBf02q/XAV/AnlEJkHpthXzgPh4Wg7oM" +
                "eq9YZ/x5VBvRg1pFOfZjg9W/XHR6x93eKWt+DtmP+O993zlshoicCUSYIs9ELMQ+99Y5aikca57to2H3" +
                "usMWeL5d5klJsdIayQ11YCTIB57F+GLQ6ZxfDDvHgfHeMmMtYoHqgsqArAv/CfHG+NjCjsgUkF5TAhD3" +
                "rhSVaTQH+vhnE38IbacFn/NRGCe5IA7SmoYLgG4NhS5QAHOqxlZs15Avr46OOp3jBcj7y5Ap4/E4k6jS" +
                "SJBVTFoYV1SKVyniqWvav/QHc73QNe9WXDNSTnSEJql8jn3lTUklvqka8gqjENVjLvMK+fQJeIPOb52j" +
                "BXyH7P1jeFr8LWKXm1fBoVyKNPLQXXa+jXEkYo4C4niGyyq0KpT3XZFGsyTLO54j2T8hQO15IVIO2Yc1" +
                "eF5wvVJZF4Rz5wvGCxo+ap+dzSP5kP30XIB1KVyF8DnahU0eW2sZdDmWuqC+kgpxMINrjQgJ0vyiEItu" +
                "8vE7CPE8NZNTLIWfv4A6tyd84qx/OVxkdch+dgzboUmpGzhwYgmsRkyEVwIPKiAu1Azgse6SSG+jZ8Se" +
                "Id6KtE0qnUqIv6JFQiPTznM1dSMBESIU9HITw6EzlxFcv7JQ1ehIIkZVmpIam1ZA3KOYr70uNxU5Fcg3" +
                "Vs88Udi+rOcT2yxEgdHwZK+jNUQR9P/FgH8dmO+HF6YXaicDWHTpqng0Q9VtRyu08JgclJ8MHlKigOR1" +
                "C+UIm9uaTpQelaFy27TSxAZUnzjLtBgfvsqsnRzs7k7lrWxpZVpKp7t2/OqzHX/a5Z8x6sW3YNSiM5eC" +
                "Ag2Th4qrAk7o+zxylsLFYEkiucVW9KCDqn1oGS7cmdB4SfCqxl5IIvKrUVDngoHXbclgQi3IhBCcRgsE" +
                "dMA2EnYqMJDYqXpkIszZ0BGmE/T7PEZzH12j2im978/nTl/RHxUO6JL0qZVX7LrkrOGskpKzO7f5QAQW" +
                "5jO0nzP4GkdCgoeGkziYIC25dOQGHngaPGSH0nSioBKkOvAo+C1YCszHLi9NJmDGF9VCyziyJVppa8fP" +
                "fI6KXMl9G+G+v8CIpWUK/wkGCYc5q6XbofyCPJfnHrO/DFak4a1W+HaLdcdspirUBMiAB11/beJa4AaX" +
                "6xysUjsUUjWLZY1eKNSJeQiWGDw5htponCtuP7xj9+FpFp7+WZO154620uAleiKqXV6DS2anty9zNyU9" +
                "f0um8DRdQ91o0v1GXaZ7/ZvOYNAf0GgTKnf/96uLsPy2Xj7q93poU7vX3eFfYXOv3uz8ORy0L/pn7WG3" +
                "3wu7+/Vut3fdPuse37QHp1fnnd4wELyrCYbd807/ar7+vlkftHuXJ/3Bedj5ENVbrlw1GdO93PiX/2so" +
                "Pgnfzf63sbjhE749Wbs8c0k2on8Bha18VucWAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
