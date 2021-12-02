/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LookupTransformAction : IDeserializable<LookupTransformAction>,
		IAction<LookupTransformActionGoal, LookupTransformActionFeedback, LookupTransformActionResult>
    {
        [DataMember (Name = "action_goal")] public LookupTransformActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public LookupTransformActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public LookupTransformActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public LookupTransformAction()
        {
            ActionGoal = new LookupTransformActionGoal();
            ActionResult = new LookupTransformActionResult();
            ActionFeedback = new LookupTransformActionFeedback();
        }
        
        /// Explicit constructor.
        public LookupTransformAction(LookupTransformActionGoal ActionGoal, LookupTransformActionResult ActionResult, LookupTransformActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal LookupTransformAction(ref Buffer b)
        {
            ActionGoal = new LookupTransformActionGoal(ref b);
            ActionResult = new LookupTransformActionResult(ref b);
            ActionFeedback = new LookupTransformActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new LookupTransformAction(ref b);
        
        LookupTransformAction IDeserializable<LookupTransformAction>.RosDeserialize(ref Buffer b) => new LookupTransformAction(ref b);
    
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
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7ee01ba91a56c2245c610992dbaa3c37";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVYbVMbNxD+fr9CEz6EdIhpIElTJmTGBUPdgk2NYdpPjHwnn1XuTo6kw7i/vs9KOtkG" +
                "E5hpoAzgk261evZ91ydKXdfToeaVGStdtlMrVXWseMG4e7zK8ZycrKMaCFMXtqHTbrWe8kiIbMTT64Z2" +
                "HNbJ/nf+SU7Pj/eYHe9clSY32w8Kl/wqeCY0m7iPxMMq5MgfI4ruISPJr2R2VySnHKeV50FvbOZheIzJ" +
                "Bju3vMq4zlgpLM+45Qw42ETmE6HfFuJGFDjEy6nImHtr51NhWjg4nEjD8JuLSmheFHNWGxBZxVJVlnUl" +
                "U24Fs7IUK+dxUlaMsynXVqZ1wTXolc5kReRjzUtB3PFrxNdaVKlg3cM90FRGpLWVADQHh1QLbmSV4yVL" +
                "alnZ3R06kGwMZ+otliKHBeLlzE64JbDidgpfIpzc7OGOH7xwLfCGcgRuyQzbdHtXWJo3DJcAgpiqdMI2" +
                "gfxsbieqAkPBbriWfFQIYpxCA+D6mg69frPEmWDvsYpXqmHvOS7ueArbKvIlmd5OYLOCpDd1DgWCcKrV" +
                "jcxAOpo7JmkhRWUZ3E5zPU/olL8y2TgiHYMIp5xF8MmNUamEATI2k3aSGKuJu7MGeekzeePa0HCuFcAy" +
                "M1F1kWGhNEH2/sRgy9lEwiBOCAoXNuOGaXIYAyHIgbrO3s4loRJehctgZH0D15hNRMWkZRBUGHJa+IUo" +
                "p8g4RYHTxNN4r5kJXB1Zs5FAfAACS4W2HJYjRMv6Dfhl1tgE6gU8mEUt9MyaLAVkGU74BIcYNIbnwhmB" +
                "malI5VimXsCAwLQCdwoQTwBQZW0skDFEHahajf3Ici+cBl0CbO63XOfCXjk3avaMqnUqwp5Xm99xesxq" +
                "zZ2ZaKVqC/yOJnByNI1zyluRBT5JMlIKVSW74YitZ/PXb+d+X7Iez/7IuLZGGnAfdwtAqHuh4D2PGA9i" +
                "Su4UJ8qrnxqgfnHW6R12e8es+dlnP+K/d3nnpxME4lwgsBQ5JEIg9fk25KWVKAw82wfD7mWHLfF8t8qT" +
                "EmGtNRIacv9IkPWfxPhs0Omcng07h5HxzipjLVKBioJqgEwLz4lhxvjYwohIEJBeU9yLW1d+qjxZAL3/" +
                "s4E/RLTTgs/zKIbTQhAHaU3DBUA3h0KXKHoFVWAr3gTI5xcHB53O4RLk3VXIlOh4OpGozMiLdUpaGNdU" +
                "ftcp4qFr2r/0Bwu90DXv11wzUk50BCWpfIF97U1ZLR5VDXmFUYjnMZdFjTT6ALxB57fOwRK+ffbhPjwt" +
                "/hapS8nr4FAKRQK56y5bj2MciZSjbjie8bIa7Qmle1eY0SDJ6oYXyPEPCBA8L0bKPvv4Ap4XXa9S1gXh" +
                "wvmi8aKGD9onJ4tI3mc/PRVgqIDrED5Fu7DJfWutgq7GUpfUS1L9jWZw7RAhQYJfFmLZTT59ByGepmZy" +
                "ipXw8xdQt/aAT5z0z4fLrPbZz45hO/YmoWkDJ5bBasREeCXwqALiQj0AHkNzRHobPSH2DPFWpG1S6UxC" +
                "/DWdEfqXdlGomRsDiBChoFd7Fw6duYzg2pSlkkZHMjGq85zU2DQB4pbK+MtW5FCLc4FkY/Xc08S352Eg" +
                "sc1GEvkMj3Y6WkMOQf+fCfW3YTWzVTOrUP8YkaInV+W9iSl0G63YsGNOUH4OuEuJ0lGEtskRNrc1rSc9" +
                "KkOFtumdiQ2oPnM20WK8/2pi7XRve3smr2VLK9NSOt+241df7PjzNv+CwS69BqMWnTkXFGKYM1Ral3A/" +
                "39uRm5Qu+ioSyW22kjuNU/CeVbhwZELjJcFSjb2QROR3k6jNJeu+rBkbjWpB9oPUNEggjiOwkbAzgfHD" +
                "ztQ9+2CkhoIwi6C75yla+eQSRU7pXX++cMpK/qhxQFekTK28Vl9GyABmjYic3bh3d/CzOIqh45zDyziS" +
                "EHwznsTBDKnIpSA328DH4BtblJozBX0gvYFHya/BUmAOdrloOgUzvqwT2saRTdHKW1t+vHNU5ETuWwf3" +
                "PQWmKS1zeE60RjzMWRBui7IKcltReMz+MpiQ5rSg7Tct1h2zuapRByADHnT4esS1vQ0u1y1YpbYomAKL" +
                "VYWeKdSGRfBVmDE55tdkXChuP75nt/FpHp/+eRFTL3xsnbUrNEFUrLz6VmxOq68LByUlPypQ8zR79kLR" +
                "JPhQlHv9q85g0B/QIBPrdP/3i7O4/S5sH/R7PTSl3cvu8K/4cie87Pw5HLTP+iftYbffi293w9tu77J9" +
                "0j28ag+OL047vWEkeB8Iht3TTv9isf+h2R+0e+dH/cFpfPMxCa98fQpZ0i2u/OL/GX6Pmm9d/9v427CJ" +
                "34+8sDBRjORfpQg2NLwWAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
