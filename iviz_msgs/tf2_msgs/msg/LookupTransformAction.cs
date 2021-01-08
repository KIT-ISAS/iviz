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
        public LookupTransformAction(ref Buffer b)
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
                "H4sIAAAAAAAACsVYbVMbNxD+fjP+D5rwIdAhpoUkTZmQGRcMdQs2NYZpPzHy3dpWOZ8cSYdxf32fle7F" +
                "BtN6pgnxgH13Wq2efd+9c63v8tnAyMyOtJm2Yqd0dqZlKqS/vB3jOjpfR9Unm6eupDP+bj3lKVEylPFd" +
                "STsq7hvR0Rf+NKKLq7ND4Ub7t1M7tnvPiteIfiGZkBET/xMFZKkahn1M0jkRLPytSh5L5fXDa19NAOuS" +
                "ACSgbERb4srJLJEmEVNyMpFOCkAREzWekHmT0j2l2CWnM0qEX3WLGdkm7xxMlBX4G1NGRqbpQuQWVE6L" +
                "WE+neaZi6Ug4NaUVBrxVZUKKmTROxXkqDTZok6iM6UdGTsnz539Ln3PKYhKdk0NQZZbi3CmAWoBHbEha" +
                "lY2xCOJcZe5gn3dg42Cu3+CexjBFhUC4iXSMmB5m8CsGK+0hH/NdkLEJ9lAS4aDEim3/7Ba3dkfgHKCg" +
                "mY4nYhvwLxduojNwJHEvjZLDlJhzDD2A7Wve9HpnmTVDPxSZzHTJP7CsD9mEb1YzZrHeTGC8lFVg8zH0" +
                "CMqZ0fcqAe1w4bnEqaLMCbigkWbRiHhbOBRMTlnZIMM+bxv8Smt1rGCJRMyVmzQi6wwf4O0Cp21EX807" +
                "1wZL8LQCsrATnacJbrRh3MG9BKw6nyhYxkvCESTm0grDzmMhiXenjje9d1GoRmbFcTC3uYeXzCeUCeUE" +
                "pCXLTgwXoekMiShFPG55rjZ40JxweMVcDAkRAxAiJuMkbMiYVhVdCqGS0j5QNDDCRLrWuCgTGNBBz1s4" +
                "w6dCuK+VY/L2EHZGsRqpOIhZoLDNgr2PmUABZNPcOsATiEWQNStbBiu+eJoMCbIE4aQZk7v1flU+szo3" +
                "MRXPggLDE76OktxIbzK+07mLokBTcPI0pbeqB0oKPlE01BqFJ7mXCLjk5eVermsbFAhkZJcjPfifxzWi" +
                "qI7BM14yEgOqRrRawkLa/VCALe8u292TTvdMlJ8j8T2+Qwh4t50gOheEYNPsngiJOKTjImetRGbJtHU8" +
                "6Ny0xRLTH1aZcprMjUG2Q3UYErvBZpwv++32xeWgfVJx3l/lbCgmFB2UCyRi+FAVd0KOHGyJtAEFGE4G" +
                "9OArVDZuRDXUp58t/CPIvSJCHUDNnKXELJSzJRtA3R6QmaI0plypHe2UoK+uj4/b7ZMl0AeroDkDynii" +
                "UMKRMPOYFTHKuUyv08Wz57R+7vVr1fA5b9ecM9ReekQoq71Gv/aoJKf/1g77htWI7pFUaY78+hzAfvvX" +
                "9vESwiPx7ilAQ39R7JP1OkCcWJFPHjvN7gYohxRL1BTPtDotRyfDlcBXb3RTKruXKXL/cyIUDliFzJF4" +
                "/xIOWHlgpp0Px9oHKwvWWj5unZ/XQX0kftwUYlEf12HcSMMwzFOTrcLORspMuffk8lyZwvdNDIWSVTGW" +
                "neXDFxBjQ1Wza6wEYjiBG7vnPOO8dzVY5nUkfvIcW1X7UvR3YCUSmI65UNCDrLTAXLhDwGXRQLHqhptE" +
                "oWXmmjXOap0raGBN9+Q7nFaa6rmfHpgUQWFWuxsJxfn04PuYpUrHWxIa5uOx12XZH9CD+yZtSlmox4T0" +
                "48wiEFXLV8Uw48oHUcVocLrfNgbSEH9/NeD/Dqwazcoph1vNCiz6eD19Mm8V3UizbvIxX+gwPzwmRUlJ" +
                "i9YqUFYHln0qX2rLVbjstpkRk32UYmJodPRq4tzscG9vru5U02jb1Ga850avPrnRxz35CaNhfAdOTb/p" +
                "ijjqMKHoOJ/CHUMPyE4z9QGZsVz+ITrcR/1V5U2rqOHbDCkIhFs9CrIyUXjaiCq91sr7is74jE0r3Rpi" +
                "Y0J6nkEQ4BW4Ibk5YXRxc/3EVhjPoSjMMZgJZMwDQCO6QR3U5iBwSL3aGtHvOfaYjPVqdFDwi8laAFon" +
                "qRT3fvGRGKIe59CkLuB3EkkK/lptxc4EqcqnKD8awengK7ucvRMNvSD/MZOpvANTwlTtU9VsBm5yWTX8" +
                "GHu2qTlu7oYZ0VOxS4VXGf7tB8Yxo8ZwpMou1W4pCgF3OeEg96VpQB1OgzH9pFdofacpOiOx0DmKBcTA" +
                "hSleu/hOuUTmGwun9S5HWMljVa2XGgWkDskMo6rkQbgRjVIt3fu34qG+xHuB8vLvFzJ77XJrLZ+hb+LC" +
                "FvS4Yn+++1w7LGt7M7nKy/lLFJWyFpSVvNu7bff7vT4PQ3V17/12fVk9/6F8ftzrdtHTdm46gz+r1f1y" +
                "tf3HoN+67J23Bp1et1o+KJc73ZvWeefkttU/u75odwcVxduSYtC5aPeu64V31UK/1b067fUvqqX3rKqw" +
                "WNS0Ip/6u9tw8w1qdGvlBfD/HaqrF8nf7A1yLUkj+ge+3aryPBcAAA==";
                
    }
}
