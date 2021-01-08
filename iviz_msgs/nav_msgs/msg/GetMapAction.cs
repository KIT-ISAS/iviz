/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapAction")]
    public sealed class GetMapAction : IDeserializable<GetMapAction>,
		IAction<GetMapActionGoal, GetMapActionFeedback, GetMapActionResult>
    {
        [DataMember (Name = "action_goal")] public GetMapActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public GetMapActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public GetMapActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapAction()
        {
            ActionGoal = new GetMapActionGoal();
            ActionResult = new GetMapActionResult();
            ActionFeedback = new GetMapActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapAction(GetMapActionGoal ActionGoal, GetMapActionResult ActionResult, GetMapActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMapAction(ref Buffer b)
        {
            ActionGoal = new GetMapActionGoal(ref b);
            ActionResult = new GetMapActionResult(ref b);
            ActionFeedback = new GetMapActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapAction(ref b);
        }
        
        GetMapAction IDeserializable<GetMapAction>.RosDeserialize(ref Buffer b)
        {
            return new GetMapAction(ref b);
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
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e611ad23fbf237c031b7536416dc7cd7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1YbU8bRxD+fpL/w0p8CFQ2gSRN00h8cMFQKkgokH5BCK3vxr5N7m6d3T2M++v7zO69" +
                "+MAoVhVsAb6X2Zln3mc4IXcuZ8PYKV2caJkJ6S/vpriOTpZeXpItM1e/Nv6uQ3BMlIxl/K0mmVT3vejg" +
                "J3960fnVyUdRyPu73E7t68c69KI/SSZkROq/ogAoU+OKHCSnR4I1vFNJpYPXnR+9GFzrkiA/gOtFW+LK" +
                "ySKRJhE5OZlIJ8VEA7WapmQGGd1ThlMyn1Ei/Fu3mJHd5ZPXqbICP1MqyMgsW4jSgsppEes8LwsVS0fC" +
                "qZw6DPioKoQUM2mcistMGhzQJlEF00+MzMnz519L30sqYhKnRx9BVViKS6cAagEesSFpVTHFSxCXqnBv" +
                "3/AJHLye6wHuaQoPNAiES6VjxPQwQ/AwWGk/sphfgo67YA8jEQQlVmz7Z3e4tTsCcoCCZjpOxTbgXyxc" +
                "qgtwJHEvjZLjjJhzDDuA7Ss+9GpnmTVD53gpdM0/sGyFrMO3aBmzWoMUzsvYBLacwo6gnBl9rxLQjhee" +
                "S5wpKpxA5BlpFr2IjwWhYHLMxgYZznnf4Ftaq2MFTyRirlzai6wzLMD7BbHai14sOlfmSIi0CrKwqS6z" +
                "BDfaMO4QXgJenacKnvGacAaJubTCcPBYaOLD6dS73ocoTCOLShzcbe4RJfOUCqGcgLZkOYgRIpTPUG0y" +
                "5OOW52pDBM0JwhvmYkzIGIAQMRkn4UPG1DV0rYRKav/A0MAIF+nW4qIuV0AHO29Bhq93CF8rp+T9IeyM" +
                "YjVRcVCzQmF3K/Y+ZwIFkOWldYAnkIsg2218Gby4qaIYyuFmS3DoFGsUYZQ/VyIX/VdVh6s2E6y/yWgP" +
                "YHpRtzuE0vahwljfXYw+HZ1+OhH150Ds4W8IMx8aKTJgQQhozSGAsItDyavqQif6a6bDw+vTf0Ziiel+" +
                "lymXotIYVBRU4DFxMK3H+eJyNDq/uB4dNZzfdDkbigmFHSUZxQ6FsYltIScOLkRqwgCGE44efBcopr2o" +
                "hfr0s4VfJJI3RKi16EuzjJiFcrZmA6jb12RytJ+Mu6GjnRr01ZfDw9HoaAn02y5orjIyThXaJIpSGbMh" +
                "JiW3wlW2eFbO8I/Pl61pWM67FXLG2muflD6HW/QrRSUl/dg6HBtWo0xNpMpK1LDnAF6O/hodLiE8EL8+" +
                "BWjoK8W+IK4CxMVLl+5x0PTXQDmmWKJue6aNtBLTAldb3yExsajiXmaor8+pUAVgkzIH4v0mArCJwEI7" +
                "n45tDDYebK18ODw7a5P6QPy2LsSqB63CuJaF4ZinLuvCLibK5DzfcQtsXOFnE4ZCSVeN5WD58BPUWNPU" +
                "HBqdRAwSeHh6LjLOPl9dL/M6EL97jsNmRKhmKLASCVzHXCjYQTZWYC7chXFZDSlsuvE6WWiZuWaLs1nn" +
                "ChZYMaH4KWKYZXruJ3QmRVKY7gQhYThfHvyssNTg+EhC43I69basqBw9oMFtrjnXbbl5/DmOyxk8tDgx" +
                "yN1czl4eSUdks8cY4pUAjuDB7M0AzbcC1OdlJUyXhFKPIS/LlqnZD6hmYzlWmXILoSfMU9dCMHQ9mkKw" +
                "qoDiHNvWUb1tMQ+vO0zUvFDFRLeTI177Ed3DMXo+yOVXnMTaRKYfsqVx+/Zef29nV4hGU2bSYuT5VnKW" +
                "hTXDyAKz5c1ef39v7xanvhTfCj3HLGzFYB/wOUdubr3wTQTKkgka56Qa6TTGrhd7q5hc+rxAR6zqVpxK" +
                "g2QhozALwwX+4SNH15Z8vDEE23LdyDT8A0pPgYd3/OAuTPOtF+B4nYXOe5O/5nC47UUTkPL22b7kI9AF" +
                "Hkngkhums7fNluqf1iQpYdt2T2jC41q0NgqpW+vGQG7yvsCPkQm7rd7GvUtJZoO5NrDaDOtmdYg5+ej1" +
                "8VG7H5zg5ClhDHBmEXxw4Q95gS/m8KcSGd+wTa3gY0D3KgDtBAsZlhsZU993UTxOqvcqxAPqP1DXZ3eR" +
                "aRcatmwoetHfJaqkKTznlvIF4/qxmoDThDV6Kk8wVQ2ptYBG+N+Ix91Rugqy9+/EQ3uJ1K4v/92YFq0R" +
                "V5bPjmm7OvDd99YFnMq+QP5Qs/pyvulF8rj5D+L/WiXr481qvyn0Le5e9B/px0ewYBUAAA==";
                
    }
}
