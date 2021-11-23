/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionAction : IDeserializable<ObjectRecognitionAction>,
		IAction<ObjectRecognitionActionGoal, ObjectRecognitionActionFeedback, ObjectRecognitionActionResult>
    {
        [DataMember (Name = "action_goal")] public ObjectRecognitionActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public ObjectRecognitionActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public ObjectRecognitionActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public ObjectRecognitionAction()
        {
            ActionGoal = new ObjectRecognitionActionGoal();
            ActionResult = new ObjectRecognitionActionResult();
            ActionFeedback = new ObjectRecognitionActionFeedback();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionAction(ObjectRecognitionActionGoal ActionGoal, ObjectRecognitionActionResult ActionResult, ObjectRecognitionActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal ObjectRecognitionAction(ref Buffer b)
        {
            ActionGoal = new ObjectRecognitionActionGoal(ref b);
            ActionResult = new ObjectRecognitionActionResult(ref b);
            ActionFeedback = new ObjectRecognitionActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectRecognitionAction(ref b);
        
        ObjectRecognitionAction IDeserializable<ObjectRecognitionAction>.RosDeserialize(ref Buffer b) => new ObjectRecognitionAction(ref b);
    
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
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7d8979a0cf97e5078553ee3efee047d2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE81abVMbRxL+rl8xZT4IUkKOjUMcrqgrDHJMCgMBcnc5V4oa7Y6kCasdeWYWSbm6/35P" +
                "98ysVkLExAn4KBu0uzM9/d5P9+qs/6vK/IXKzLDUXpvyIKPf3xtZCMkfr4f43Dpbv+5CuarwaaXlq/vW" +
                "vlUq78vsJq0exOvW/l/803p/+f2eMMwFeKrZuB67oXt+D3ckceudkrmyYsR/WoHPQvfDRlpxfCRIHdc6" +
                "vysl64yV9TgCOZ8HRgKXrQ1x6WWZS5uLsfIyl16KgQH3ejhSdrtQt6rAJjmeqFzwUz+fKNfFxquRdgL/" +
                "hqpUVhbFXFQOi7wRmRmPq1Jn0ivh9Vgt7cdOXQopJtJ6nVWFtFhvbK5LWj6wcqyIOv459bFSZabE8dEe" +
                "1pROZZXXYGgOCplV0ulyiIeiVenS77ykDa2Nq6nZxqUawgb14cKPpCdm1WwCByM+pdvDGV8F4bqgDeXA" +
                "DmXuxCbfu8al2xI4BCyoiclGYhOcn8/9yJQgqMSttFr2C0WEM2gAVNu0qb3VoFwy6VKWJpEPFBdnPIRs" +
                "WdMlmbZHsFlB0rtqCAVi4cSaW51jaX/ORLJCq9ILOJ6Vdt6iXeHI1sZb0jEWYRdbBH+lcybTMEAuptqP" +
                "Ws5bos7WID99JG9cGxzsWpFZ4UamKnJcGKtYLhYEtpyONAzCQlC4iKl0wpLDOAhBDnTM9maXhEpkGQ+D" +
                "ke0tXGM6UqXQXkBQ5chp4RdqPEEaKgrsJpoueM1U4eiatOirAfEiRaasl7AccdTUb+Rf58kmUC/Ym9Mh" +
                "tZ7FoE5kZY4dIeshBp2TQ8VGEG6iMj3QWRAwcuC6kToFSFgApsaV8+BMIOqwqpvsR5b70pmRc2Krb0xB" +
                "xri2RrcGhZGI1w+/iIEuvLLXhR5r7740p81q9OksjrzpK0c2x5+7iTwWtVjNHkeye7lqrZQZyo+vE6vh" +
                "4rx3enR8+r1IP/via/wOrsv+NkJAzZUnr4VjwZWzkDdjflmKpkjz4PDq+B890aD5YpkmJbTKWiQm5PC+" +
                "Ihd9EOHzi17v/flV76gm/HKZMIyrUBlyCkuJ7FqHi5ADOBgFOqS3FL9qxmWkHLbE7/xs4D8ik7UQ8jWK" +
                "2qRQRAGumqiA0c0rZccoXgVVUq+2IsuXPx0e9npHDZZ3llmmhCWzkVbEtqsy0sKgojK6ThH3HXPw5uxi" +
                "oRc65tWaY/qGRc8rzgoL3teelFfqk6ohr3AGKW8gdVEhHd7D3kXvh95hg7998c1d9qyi2LnHAzgVmsqv" +
                "ukvn0zz2VSaRcphmfVgFmEFpmwssgI4ub2WBXH2PANHz6kjZF7tP4Hm165XGcxAunK82Xq3hw4OTk0Uk" +
                "74tvH8pgrGTrOHyIdmGTu9ZaZrocaDsmTEh11DezAHOi8iUhmm7y+i8Q4mFqJqdYCr9wAKGue3zi5Ozy" +
                "qklqX3zHBA9qjBHBFyiJHFYjIiooQdYqICrdAKIjyCG99R8Qe45oG9I2qXSqIf4ahAMcclAUZspwnhYi" +
                "FOwyBpEi4gSGG42iRlty1a+GQ1JjXOTVzH95OBHLc+u+DXHpbyoPWw+sBfSy9d3rsPGpwcZatlobn/Mj" +
                "3vUOjnoX4rM2h58VdJOauYQ+qcWD/V1mdZ/R68R4+JyG47oM3V6I7GFlJQm4R36EcDeDKH3EzeT9aLcK" +
                "xsgE6Gnrg+0GcJgs9ZlquuwdXBy++1NqilA+M9smC8AFQGAsEQ8zRKqfKhVEW7hXzfQC46K7rTc/dfis" +
                "avXLeVxUZfC40FhCM9CKm3BTjMREmpygSw5PO+L07CreQ9q9zgpT5Wk+sOq/f1ykszeETMTx6duzz5eL" +
                "pDo0JeEJBySBtDnmkCDEFcsjxxLVmSgdG4diheYsVZkchgYxhKfwAHCHdKBVAbWM9XDkY2WgbqngDnSB" +
                "a8LIRWUjbvzQ0BteTCvp+Zggzsp6pHajuEpz3xnKKK1NNmKWmRG1zOPvJ+kr2kZ7QYcTRE4evydGqEGO" +
                "MOLcVEKGWq1jjggUWT/EtsSvERg0pVq08CnQvuZlL2plwiFiRcPymqii9Si5KQDFgpU/7ifJSw5Pfrq8" +
                "6l1cfq6ntKJ5eR7FegjJ0VuUbGZ6Jw9uHvXSV4WhkhuCImipI2Ii5RhwyULG6iFjFMACZ6zDUZtBGYQI" +
                "uPrJcGbwpZG8RS5G12EXe7Za4UMw5zkxckjhRumrEX1uSY5OLQh60HkadsCv++QzhBj6ZvbcjeQEsYzz" +
                "SuJ/J29MDqM8i5hv8erAxXvlRjWpa5w3WnM6n8wSvURfVk4QdM4kvtrUfvNgcgjsyGXMqbDvV5qXDBXh" +
                "MfiHN5XtUKQQ77MYBYXEEhYe8AjRhPgrdHkTJpxsBm2xgnxPVt5Q4NOwbt4aKuLRzhvKhBprSeJ5n1nW" +
                "xPnZZe8vyFrJBKF68aiCHY/8Yt43+ZxKN9fyvYWNYK/Q9HH2JqWWATrSCtZ+GE5VJbXIFOpW5lqWz8cE" +
                "gskzqQFzlSzuKMmpf4LQoeHZJ2L1Mo6M6dwvgzkpnf2ZwnL0+GXlThUh9ZOh/NQQJOMcMZFkLdI/HBcu" +
                "T/k1jAkRGLkaUGsiS5wYk3x0/xs1X1cDgjeE/YLSqqcBpE2hHHwi76eeAVQSwbzfZIUHujSEhwQ/XJ6d" +
                "PqcON07mfz54fxI7ky41VbFIoElPhQsY7EZx8lL1fIBLQg0oqG/Z4PF2XzrVFb3usMsJ9K7RO1Q7KI0V" +
                "xtwgxm9Qs579p00abu+1D02VjY7etDuibY3xuDPyfrL3/HlhEO7Qtm//91kQ0fLEvqT3H+UtacZQWg7W" +
                "i10YGaehBSpl2rexSQNYIq/cKBXfVQwKNdN9XWg/70YNrvHXjEfRpER+C6MzcfQm+EYNwZEC8yVMQc4V" +
                "hnCIcUlzBX4T8hYMRmHpUjCZPVErgO+RCnBvVQV733z3+lVYkRlglCz0Be27HLfjSZc/ngAYAI2MTJHX" +
                "dlo6+PJj8S6tCLT5KNGeDt3ObrgzMRZ3vnm185IvCd3QAk2Nb1yBdndqbL5yu4QJSJB0QGoLw9OxyauC" +
                "nnsaE3kzaSeHhms/1pu59RV4tTcjZh2XraRnCtPT7RxFsXTB5QKS6MR3JGNqNLnMUScm81xHz2xmFgcj" +
                "C57d4E7hKFQ80fNz1G6fcV4HlVAP01ujZhD3gUHsXPQL0+/whLSQcwrL1Eam6V5kRZW+RjDPgl8+C2il" +
                "mxJGOCvAfj4RglAVNnYoQ6+Fmr+px9DKNkXtFlXwF4QvNiuIgcZA5Vtdcb4g4xp7wTRBBtrtEmVAmLzK" +
                "mFViEy5iJQSY0LuYAJRqPTnErzJ0Ir+PM4PtQUHwKnCvA5IPmwLzMvtYaRcTTg1hV158UtXYpEzBMCmY" +
                "cetutyNeHlF+rDJPsDpqsaGurjgeJIgM5dFrsKSQDqgwDNSUBwKYnuocEkYwUKhyiKs1RNNb1kAgXfFm" +
                "4umonhfwySNZlqpwSVRtk0PEShH9hXVDTtNtsaHekivwiyFOVeG9EX60u+5r5DiCEwKdgWu8v6wf/D0x" +
                "FUErjDThodlJLZSMImkafnvl6h3WTNP61R14tLz+NRjkkxdjuQPYIkUeP+sIBz8jrW4m0l8FzW1FqSBS" +
                "zhWsJnJlqaiy5imt4z+KSRpQB294suzDhliffMh8IawnKf8QCg6yI7DtPBoZ2xuZrKYSsk43TlOPT69e" +
                "hynqi3jnp3hrX7xcrHmxy3d2Gmvo1r54tVhDdqRXDI01dGtf7MY7b0/ODujWvvi2eWf3FU2cWynHU21I" +
                "JjmVIZTZH5OzmMGABl684Cx8HlgzDhNoxlysihCi8SB2Cq7M2HSUPquyoiQTsoJTihDerUrnZABePjLy" +
                "Dk44liVweaHGnD1TN8icPZZbLPdjHOZAjI0hRmrMCu1YdE/4fQhM8bfYFeRqJuDBNJywahAa9jQHYSHQ" +
                "caB1Uu7DLy064yoSQIzVtOiA+K6BoiztCCCHsWA14QXMzfoGLG16IlUlMdaoLIkF3FczFSz+YSfwqWbX" +
                "UNxjcrtGRynYs9R53J1Y1blzYOGrbiIzFeYsiKBZ/Wlef/rtqdi/p31MIqVv/fBMRDmUbP6qCze13MDK" +
                "4JsqvOhdKcpcwtI3iVor1fju2Y/as35K8CSxVSQx5wlZN+8LqwWpqzJ+h4V6DJYk8r4hLsx0eyx/BRSp" +
                "KcnkB+QXu7NdKKoWOczEU59idb280ezQCBLtqp6pfFvOmjzyUh74gT7hlE7wvUajZLk32Jx1BADpbx3U" +
                "ZN9sif8liOKd2z+vv/1vvr2V3PTDzu4vDWGeznSQ6GCNfu+aq8MvgA01x+F5iElyzIayuyJgqHpB68cK" +
                "XmxLprtY9zQCLs5e55NLDK34Jq4+LhgntADv/P00kz5Nv/j7yeWvqf7prxHV33f9f/miay1Z63/hpesH" +
                "9isAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
