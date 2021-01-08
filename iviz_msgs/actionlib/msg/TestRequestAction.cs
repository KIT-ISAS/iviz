/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestRequestAction")]
    public sealed class TestRequestAction : IDeserializable<TestRequestAction>,
		IAction<TestRequestActionGoal, TestRequestActionFeedback, TestRequestActionResult>
    {
        [DataMember (Name = "action_goal")] public TestRequestActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TestRequestActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TestRequestActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestAction()
        {
            ActionGoal = new TestRequestActionGoal();
            ActionResult = new TestRequestActionResult();
            ActionFeedback = new TestRequestActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestAction(TestRequestActionGoal ActionGoal, TestRequestActionResult ActionResult, TestRequestActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestAction(ref Buffer b)
        {
            ActionGoal = new TestRequestActionGoal(ref b);
            ActionResult = new TestRequestActionResult(ref b);
            ActionFeedback = new TestRequestActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestAction(ref b);
        }
        
        TestRequestAction IDeserializable<TestRequestAction>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestAction(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib/TestRequestAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dc44b1f4045dbf0d1db54423b3b86b30";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXW2/iRhh9t8R/GCkPm1RN9pLddhuJB0q8KVUuCGjVNzTYH/a0vtCZcQj/vuebsQ1e" +
                "iBapu1kUAra/OXPmfFdmZOyE/q3wMYisKoubUmZCuq/zBN+D2ecWEzJVZhsb7a72rT4RxQsZ/dPYLevr" +
                "XtD/yq9ecDe9uar3ydTi9cEz9YLfSMakReo+gtZ8npvEvGaT0bXgE89VvHscJwjf/2bMjY09Cc+wF5yI" +
                "qZVFLHUscrIyllaKZQnqKklJn2f0SBlWyXxFsXBP7WZF5oJXzlJlBP4SKkjLLNuIysDKliIq87wqVCQt" +
                "Caty6gDwUlUIKVZSWxVVmdRYUOpYFWy/1DInh89vw8oUEYnR9RWsCkNRZRVIbYARaZJGFQkewrhShb18" +
                "xyuwcLYuz3FNCdzQMhA2lZYZ09MKwcRkpbnibX7wZ7wAPEQibBQbceruzXFpzgT2AQtalVEqTkF/vLFp" +
                "WQCRxKPUSi4yYuQIOgD2FS96dbYLzdSvRCGLssH3kNtNjsEttsB8rPMUzstYAlMl0BGWK10+qhi2i41D" +
                "iTJFhRUIPy31phfwMr8pQD6x2DDDOucbfEpjykjBE7FYK5v2AmM1b+D8goDtBd8+r3YTxUdaTVmYtKyy" +
                "GBelZt4+vAS8uk4VPONOwhkk1tII7dOKYhdOI+d6F6KQRhb1dnC3fkSUrFMqhLICpyXDQYwQoXyF6pMh" +
                "H08cqvERtCZs3oKLBSFjQEJEpK2ED5lTV+jmECpu/AOhwREuKreKi6ZygR10PsEerv4hfI1MyPlDmBVF" +
                "aqkif8yahbmo4V3OeAswyytjQU8gF2F20frSe/Fl66OvjD5HZ+HkbnQ/mIXz6R/DYTid9t/sPRn8+jCZ" +
                "hdf9t3tPJuHv4ZAfvdt7dPswDfuXe7evJw/j/vu92+Ffw3A8Gz3c9z/Uzyzp3JWhORxnKxMsyjITKing" +
                "4Hkkka9Zo6H3zdzSk20Wp9T0KL/MzI3KVxlWuhwM4kpLF3IxZXIzl1FEq727LYXtg5VE1NaMvkdT8234" +
                "iLY2dRRFrd0OTt3IvTovWT88o17Qbbq+WXysiTZX4/D+enR/I5pXX7zBf5+4LtlS1JQNoUSUnFRI5Mg3" +
                "kbrSdupJAzoYzkZ/hmIH9G0XlIt7pTXiAz1tQRxaxyGPJ2F4N0YetMjvusiaIkKrRJMTPnSbaiHkElHG" +
                "xQ4CaC5h9OT6apH0gi3V/dcJ3ihNTgjfvdDpEeAMoaxpYED1dObDOOP5wtJZQ9rle3i9Q/qyS5rrtoxS" +
                "hcEDZb5CihizrHi4OKTFs/vU1WNH9vcH9lmU7vRINJZ9y/7gVnFFX1aHY8OUKPxLqbIKXeE5gk0R2xL8" +
                "sE9Q098UuRZziBC3g7KynwfNj0ewXFDENcWBtrtVqDrcv9zMgRlQFY8yQ8d67gh1ALYp0xc/vUQAthFY" +
                "lNal4zYGWw9uVR4Obm+3Sd0XPx9Lse7qhzgepTAcs++yLu1iqXTOEzMPFa0r3LTHVCjuHmM3WD5+hWMc" +
                "KTWHRicR/Q48jj4XGWjDs12svvjFIQ7aoaueSgGFtudbHnkdZKsCo/Bcg6/12MfSLY7JQsPgJSvOsq4V" +
                "FDgw87m5bJBl5dr95mFTJIXuzmQSwrny4KavnS7HS2JaVEnitKyteB54+eGq6dBfGkSac78kt0HnR/r/" +
                "mSLaH/rf5xf+9gi94D/c4HxhzRAAAA==";
                
    }
}
