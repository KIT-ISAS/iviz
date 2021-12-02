/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwoIntsAction : IDeserializable<TwoIntsAction>,
		IAction<TwoIntsActionGoal, TwoIntsActionFeedback, TwoIntsActionResult>
    {
        [DataMember (Name = "action_goal")] public TwoIntsActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TwoIntsActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TwoIntsActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public TwoIntsAction()
        {
            ActionGoal = new TwoIntsActionGoal();
            ActionResult = new TwoIntsActionResult();
            ActionFeedback = new TwoIntsActionFeedback();
        }
        
        /// Explicit constructor.
        public TwoIntsAction(TwoIntsActionGoal ActionGoal, TwoIntsActionResult ActionResult, TwoIntsActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal TwoIntsAction(ref Buffer b)
        {
            ActionGoal = new TwoIntsActionGoal(ref b);
            ActionResult = new TwoIntsActionResult(ref b);
            ActionFeedback = new TwoIntsActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TwoIntsAction(ref b);
        
        TwoIntsAction IDeserializable<TwoIntsAction>.RosDeserialize(ref Buffer b) => new TwoIntsAction(ref b);
    
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
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6d1aa538c4bd6183a2dfb7fcac41ee50";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XXVPbRhR916+4MzwEOoW0SZqmzPjBBUOdIQkDbl+ZlXRtbSvtursrjP99z1192AYz" +
                "eDrBHoMktHvuuV/nLpOFHZvgh1nQ1lxaVZKKt3cz3CeT9bc37OsydO9dfNpcccGcpyr7p1szbZ+TwXf+" +
                "JF9uL09bK6VO3z7xIvmDVc6OinhJ+pV3lZ/5t7JifE7i4p3OOx+i99Ht16HrQ96Yb7glB3QblMmVy6ni" +
                "oHIVFE0tOOtZwe645HsusUlVc84pvg3LOfsTbJwU2hO+MzbsVFkuqfZYFCxltqpqozMVmIKueGM/dmpD" +
                "iubKBZ3VpXJYb12ujSyfOlWxoOPr+d+aTcY0Pj/FGuM5q4MGoSUQMsfKazPDS0pqbcL7d7IhOUAgj/HI" +
                "M0S+N06hUEHI8sMcRSM8lT+FjR8a506AjeAwrOSeDuPf7vDojwhGQIHnNivoEMyvl6GwBoBM98pplZYs" +
                "wBkiANQ3sunN0Rqy0D4lo4zt4BvElY1dYE2PKz4dF8hZKd77eoYAYuHc2XudY2m6jCBZqdkEQrk55ZaJ" +
                "7GpMJgcXEmMswq6YEVyV9zbTSEBOCx2KxAcn6DEbUp2v3TzrLRFLqyVLvrB1mePBOqHc1BMhl4tCIyHR" +
                "CWkXWihPTgrGwwkpoHHMdyxJhESZ1hiS7O5RGouCDelAcJS9FC3qgqs5pKUssVswfVM1C4bpHppSRn+A" +
                "AmXsgkLmhNF6fFv+Ou9ygvCCHtJiV3GmTpbALMeORsnQg96rGcckkJ9zpqc6axxsGfiTFl0apFkAUlXt" +
                "A5gRug6rTrr8Seb2pXtR8RJ03scPpNprul/RbYbDy7ILyQs1+jBeOuVtJ0s7Ul6Z92MuyaNpIIL2qSPY" +
                "PFyPvp6Pv15S9xnQT/jd1FoskAIdsGRUtJVKQO1ljdC1grBR/i3m8Gwy/mtEa5g/b2KKAtXOQUkguilL" +
                "Te0EfH0zGn25nozOe+B3m8COM4aUQ4YhcZDDvr5JTQOSh86E904ajh+i7ptZsiL69HOAH7RSjEIjsJhC" +
                "85IFQQffoYDo4YRdhWlTyugLfNRSvv3z7Gw0Ol+j/H6TsiiMygqNkQhBqjOJwrSWubctEM+ZGf7+7WYV" +
                "FzHzYYuZ1EbX8zq28Yr7Vkt5zS+GRqrCW2jUVOmyhn49Q+9m9Hl0tsZvQL88pef4b86iFm6jI9pl6/C4" +
                "XH58mWPKmYJgR8zeWI1zgehsnIg4mWhzr0qI6zMOtJXXd8qAPu6h8vrSMzbEJlwVX5+8PsJnw6urVScP" +
                "6NddCbajZxvDXaKLnDzN1iZpM9WukkOcDL4+DfEcIkw433BivUw+fQcndguzFMVG+zUG5Jj0TE1cfbud" +
                "rEMN6LcIOOwPBe1pCUiUI2sCwk0QVB8CQZHhi9v2VCJxS3foPS/YVqItIV1ouL/lSIKDw7As7SKev2Uh" +
                "WsFtHhoUYhYVIZ4P1kaZbMk5rWczCWO7KPBD2N/8b4dvM/h9Xe139F90/+X9v+Hfbe+PZfti3/NO/gMt" +
                "R+uSBQ8AAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
