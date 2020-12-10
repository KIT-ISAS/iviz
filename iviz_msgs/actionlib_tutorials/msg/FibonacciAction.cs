/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract (Name = "actionlib_tutorials/FibonacciAction")]
    public sealed class FibonacciAction : IDeserializable<FibonacciAction>,
		IAction<FibonacciActionGoal, FibonacciActionFeedback, FibonacciActionResult>
    {
        [DataMember (Name = "action_goal")] public FibonacciActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public FibonacciActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public FibonacciActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciAction()
        {
            ActionGoal = new FibonacciActionGoal();
            ActionResult = new FibonacciActionResult();
            ActionFeedback = new FibonacciActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciAction(FibonacciActionGoal ActionGoal, FibonacciActionResult ActionResult, FibonacciActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciAction(ref Buffer b)
        {
            ActionGoal = new FibonacciActionGoal(ref b);
            ActionResult = new FibonacciActionResult(ref b);
            ActionFeedback = new FibonacciActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciAction(ref b);
        }
        
        FibonacciAction IDeserializable<FibonacciAction>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciAction(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f59df5767bf7634684781c92598b2406";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VXXW/bNhR9168gkIcmw5Ju7dZ2AfzgJU6WIm2DxNvLMAQUdS1xo0iPpOL43+9cSpbl" +
                "fCzG0CyCE1n25bnnfl+f6NxZqZQeq6idPXXSCJneXpd4n51sfn9JoTFxJeHT012ZE6Iil+qvldSse85G" +
                "X/nKPl2dHnZajM6vYxOd19KE1yf3rcp+IVmQF1W6ZetTdSjDa5Y4OxZs8rUu1hYlfyRHPA/5EIuWQMsu" +
                "2xFXUdpC+kLUFGUhoxQzB9a6rMjvG7ohg0OynlMh0rdxOadwgIPTSgeBV0mWvDRmKZoAoeiEcnXdWK1k" +
                "JBF1TRvncVJbIcVc+qhVY6SHvPOFtiw+87ImRscr0N8NWUXi7PgQMjaQaqIGoSUQlCcZtC3xpcgabePb" +
                "N3wg25ku3D4eqYTve+UiVjIyWbqdI4mYpwyH0PFNa9wBsOEcgpYiiN302TUew56AElCguVOV2AXzi2Ws" +
                "nAUgiRuJ6OeGGFjBA0B9xYde7Q2QbYK20roVfIu41rENrO1x2ab9CjEzbH1oSjgQgnPvbnQB0XyZQJTR" +
                "ZKNAwnnplxmfalVmOyfsYwjhVIoI7jIEpzQCUIiFjlUWomf0FA3Oz2cvpUFRpNTqyIpQucYUeHCekl3J" +
                "EMRyUWkEJBnB5SIWMgjPCRNgBCfQWYp3Skm4RNpOGYLsb5Aai4qs0FHAUAqctMgLqudoNcbgNGOGNmsW" +
                "BNU9tMhpxlykUOSjROSY0dC/HX9drGIC94LekpX0fhazvmnZAifazoYaDEGWlIIgwpyUnmnVGtgxCAcd" +
                "OhdIKwBSdRMimAlUHaQOVvHjyL1EF0z9L2uLEqWNPvMCJIYD5OlmjDYYm8AhxG3dj7v50w2e57biDpvs" +
                "zpTgNvdhRbF9uJh8Pj77fCpW10h8h/9tBqa0qVAXS4qcfMgPZKRq21/XJjaKosMcH03PfpuIAeb3m5jc" +
                "lxrv0V/QinPiTNsK+OJyMvl0MZ0c98BvNoE9KUKDL7i6JJpkn/VCziLCh3qF9Z7LkG7TNLBlJv7l2sEf" +
                "Cix5oW27mE1zQ4ygY1ihgOjulHyNGWR4IEba6yhf/Xp0NJkcDyi/3aTMfUeqShPTDo1iL8wanoYPOeIx" +
                "NeOfv1yu/cJqfnhATe6S6UWTinvN/UFNRUNPuoazIjh0rpnUpkFXe4Te5eTj5GjAbyR+vE/P05+k4iMZ" +
                "kDqaa+LddPn2aY45KYk2njB7ZQ22Be6+aU5iX9H2Rhq03EcM6DKvr5SRePc/ZF6fetbFVITr5OuD13v4" +
                "aHx+vq7kkXi/LcFuID3EcBvvIib3o7VJ2s60r3m143EYh10gMaFiw4hhmnz4CkZs52ZOio3yaxXw8vRI" +
                "Tpx/uZoOoUbipwQ47leFbocCkigQNQah1gmydwGjHLS7cLersN/yLWovMLZjb7NLFxrmP7CoYJ0YG+MW" +
                "aStnQZSC31wlpOjGfdoaBsOMjxSUN2XJbuyEIt3Gl9kKulHc7gW//9Hv9y+3HKx+Of7n9aD/6fmSvzl7" +
                "K+57NvsHfd36728PAAA=";
                
    }
}
