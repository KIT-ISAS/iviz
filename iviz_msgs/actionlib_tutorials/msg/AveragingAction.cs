/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/AveragingAction")]
    public sealed class AveragingAction : IDeserializable<AveragingAction>,
		IAction<AveragingActionGoal, AveragingActionFeedback, AveragingActionResult>
    {
        [DataMember (Name = "action_goal")] public AveragingActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public AveragingActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public AveragingActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingAction()
        {
            ActionGoal = new AveragingActionGoal();
            ActionResult = new AveragingActionResult();
            ActionFeedback = new AveragingActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingAction(AveragingActionGoal ActionGoal, AveragingActionResult ActionResult, AveragingActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AveragingAction(ref Buffer b)
        {
            ActionGoal = new AveragingActionGoal(ref b);
            ActionResult = new AveragingActionResult(ref b);
            ActionFeedback = new AveragingActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingAction(ref b);
        }
        
        AveragingAction IDeserializable<AveragingAction>.RosDeserialize(ref Buffer b)
        {
            return new AveragingAction(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d73b17d6237a925511f5d7727a1dc903";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVX224bNxB9X0D/QMAPsYPaaZO0TQ3oQbVlx4WTGLbaV4PaHe2y5ZIqyZWsv+8Z7k0b" +
                "y6hQpLZgXSgNz5y5jycrcjJXJp+kQVlzaaUWMn68z/E5mQx/vyVf6dBKuHj6WuaCKJvL9K9WatGcR8n4" +
                "Gz9Gyae7y9NGj1bz+1AF65TU/s0Ou0bJR5IZOVHEt6S/Vvrcv2GRq3PBVt+rrDcquoS//d/4+5DVFGp+" +
                "o+RA3AVpMukyUVKQmQxSLCyIq7wgd6xpRRq3ZLmkTMRfw2ZJ/oRvzgrlBf5yMjBA642oPKSCFakty8qo" +
                "VAYSQZU0AOCryggpltIFlVZaOlywLlOG5RdOlhTx+enp74pMSuLq/BRSxlNaBQVSG2CkjqSH3/AjhCtl" +
                "wru3fAMXZ2t7jDPlCELHQIRCBmZMD0skFJOV/pTVvK5tPAE8nERQlHlxGL+7x9EfCegBC1ratBCHoH+z" +
                "CYU1QCSxkkiEuSZGTuEHwL7iS6+OtqGZ+qkw0tgWv4bsleyDa3pgNuu4QPA0u8BXOfwIyaWzK5VBdr6J" +
                "KKlWZIJA8jnpNqOEr9VKAXLBzoYY7sXY4F16b1OFSGRirUIxSnxwrCDGBek6Sp6hurbKpM60hrLwha10" +
                "hoN1zLtOL4GorguFyERLuILEWnrhOHk8LInpdBVDH1MUrpGmUYdwOxQgAMgIFQSsJc9JjBShcokOpFGP" +
                "BxHV1xm0JijvwMWcUDEgIVJyQSKGzGno6NYIlbXxgaPBESGyvcdF27/ADn4+gI7YA5G+XuYU4yH8klK1" +
                "UGltZsPCnzTwsWZqCTArKx9AT6AWIXbSxbKO4st0ybo/JnWxUpaTTxbaSj45manKv2TzrofOHu0bXTNU" +
                "KOH41nfwZmjVcXvOOqn5jJLhaKmb4oeGZnu6mX4+v/p8KdrHWHyP1zpBY1IVqJ0NoRQsJw8SNq2bZdNR" +
                "BnXTgk7OZld/TMUW6A9DUG5ilXPoRejdc+I03A/55nY6/XQzm553yG+HyI5SwkhAM0ebREvtqkLIRUAU" +
                "UdRwgONSpYc4P0w+Snqqjx8HeKIEoyPqLo2JttTEECr4FgZUD2fkSgwuzXM00FFL+u73s7Pp9HyL9Lsh" +
                "ae5PMi0UBizaWZWyIxYVD9FdvnhSz+TXL7e9a1jP+x165jZan1Wx+nv2O1VlFf27dzg3vEWDW0ilK3S/" +
                "pwjeTn+bnm0xHIsfHxN09CelsZXuIsRtz1bh66T5bg+Wc0olOn4E7bRV2DO4T8fZil1HmZXU6MxPmdAk" +
                "YFcyY/HTcyRgl4HGhliOfQ52Eey9fDa5vu6Leix+3pdiM712cdzLwwjM45ANaZuFciVvhjw8u1DErYap" +
                "UDY0YztZPnwDM/Z0NafGoBBrDbx2PZUZ11/uZttYY/FLRJx0y0WzfQFKZAgdo1DtB9l5gVF4fuNjs96w" +
                "6+b7VKFncMseZ7euFTywY7eJ+8dEa7uOuz2LoijccPeQcFxsD3HL2JpxfCWjeZXzgOtWiEAP4eWWiHZO" +
                "d6sD7/pOWXcvTa6p+1ouLawtX3KluOj+J/2vS0WL0K2HL2NObwiC/g+SqCuuzQ8AAA==";
                
    }
}
