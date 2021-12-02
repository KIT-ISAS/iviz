/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GetMapAction : IDeserializable<GetMapAction>,
		IAction<GetMapActionGoal, GetMapActionFeedback, GetMapActionResult>
    {
        [DataMember (Name = "action_goal")] public GetMapActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public GetMapActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public GetMapActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public GetMapAction()
        {
            ActionGoal = new GetMapActionGoal();
            ActionResult = new GetMapActionResult();
            ActionFeedback = new GetMapActionFeedback();
        }
        
        /// Explicit constructor.
        public GetMapAction(GetMapActionGoal ActionGoal, GetMapActionResult ActionResult, GetMapActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal GetMapAction(ref Buffer b)
        {
            ActionGoal = new GetMapActionGoal(ref b);
            ActionResult = new GetMapActionResult(ref b);
            ActionFeedback = new GetMapActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMapAction(ref b);
        
        GetMapAction IDeserializable<GetMapAction>.RosDeserialize(ref Buffer b) => new GetMapAction(ref b);
    
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
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e611ad23fbf237c031b7536416dc7cd7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1YW1PbRhR+16/YGR4CHdshl6ZpZnig4FAyIaFA+sIwzFo6tjaRtM7uCuP++n5ndyVZ" +
                "YBqmE+xxYl3O5Tv3sxyRO5Hz/dQpXR1pWQjpL69nuE6OVl6eka0L17w2/q5H8J4om8j0W0MyjffJ3k/+" +
                "JCfnR+9EJW+uSzuzz+9akPxJMiMjcv+TBDSFmkRqUBwfCjbvWmXRAG+4t/hpsFqXBe0BWrIlzp2sMmky" +
                "UZKTmXRSTDUgq1lOZljQDRVgkuWcMuHfuuWc7AiMF7myAt8ZVWRkUSxFbUHktEh1WdaVSqUj4VRJPX5w" +
                "qkpIMZfGqbQupAG9NpmqmHxqZEksHV9L32uqUhLHh+9AU1lKa6cAaAkJqSFpVTXDS5HUqnKvXjJDsnWx" +
                "0EPc0gyOb5ULl0vHYOl2joRhnNK+g45fgnEjyIZzCFoyK7b9s2vc2h0BJYBAc53mYhvIT5cu1xUEkriR" +
                "RslJQSw4hQcg9RkzPdtZkcywOUcq3YgPEjsdjxFbtXLZpmGOmBVsva1ncCAI50bfqAykk6UXkhaKKieQ" +
                "bUaaZcJcQWWy9Z59DCJw+YjgV1qrU4UAZGKhXJ5YZ1i6jwYn5xNl49qK8KkVwQqb67rIcKMNQw75JBDL" +
                "Ra4QEG8El4tYSCsMJ4yFEZxAxz7ePiXhEllFZQiyuUFqLHKqhHIChpLlpEVeUDlHWykKcLNMG7JmQVDd" +
                "ihYTQn0AgkjJOInIMaJV/0b8KmtiAvcCHsKiOz+LpicBWQaO0MVQg9bKGfkgCDunVE1VGgyMCOwoSucC" +
                "CQQAVdbWAZlA1YFq1MSPI7eZpufb3Sb7axgCP+6waG+uRs35n9hk4wCJk+NpQD8IJbnT97l3vW3whZvT" +
                "8afD409HovnsiV38H9LK50KOZF8Skldz0JFmaehpsfZ7mR5l7h9cHP89FisyX/RlcrOpjUHTQH+dEKfP" +
                "owSfno3HJ6cX48NW8Mu+YEMpoWuj46KbofO1qSzk1CF2KEJYb7i26Na3+GqWdEDvf7bwD1XjvRB6KQbO" +
                "vCCWoJxtpADo9gWZEoOl4CnnaCdCPv9ycDAeH65AftWHzM1EprnC9EPvqVP2wrTmEbfOEQ+p2f/j81nn" +
                "F1bzeo2aifamZ7Wv2A77Wk1ZTT90DWeF1WhHU6mKGq3qAXhn4w/jgxV8e+LX+/AMfaXUt711cLhN6drd" +
                "TZfBjzFOKJXozV5mq6zGCsAt1Q8/LCGqupEF+ugDBsTMaytlT7zZQOa1qVdp54uwS742eK2HD/Y/fuwq" +
                "eU/89liAccqsQ/gY7yIm96PVB11NlSl5X+MZ14bBrxyMhLKeEatp8vYnGPE4N3NS9MovKOCN6IGc+Pj5" +
                "/GJV1J743Qvcb+d/XIwgSWSIGguh4ATZuoCl8JzFZVxA2G+TR9SeZdmavc0uXSiYv2b7wI6wXxR64Vdt" +
                "JkQpmP5+IOEz3xH8KrAyyZglo0k9m7EbI5GjW7epUR8nb/v0c5rWc8RleWRQq6WcPzWMnsLmIGKIF3t4" +
                "n7etl0PM14hmwMeNsC0SejrWtqJYpWb3o3NN5EQVyi2FnkKkblSMkv6KIRC6ExyUDpuDErOzzXBM+1xV" +
                "U92sgXjnt2wPw+jFsJRfwYYDD5lBqIs2ytu7g92dkRCtgZDRQeM9VXI5hVOCkRUWxcvdwYvd3Sswfam+" +
                "VXqBndaK4YtRwtVweeVVP3lerNjehCPXKJoJTmip94YppU9/TLvYmtJcGtQEGYWdFk73D/uRjR68u/EH" +
                "n3JrKDRCkoXdG8+u+f7ar+Od7xFmXYSJelk+5+BfJVMQ8nmxewcGGIEoZAjDJVPZq+ZU6R9GgpxwLnZ3" +
                "KcLTqFQbhcpsLGIIl+VA4GtkxnFqjs0+hiSL4UIb+GqO42FkgiCfpD4dmnBD0CiZEea6M8vg9lPP4tU9" +
                "UYTv6wO2/a56QlCB2qMH0ikOUTiWyJQGfi7icRbfq5AA6OqA3PCORHKq4cSWIPmrRvczlZfb0SUbMhBQ" +
                "mgzGhORdJDaIBj9swZ8uPOSeuSGn3rwWt+3Vsr36ZzPwO9eta4o9f/bB8933zu9cruh7/21Rc7XY6Knv" +
                "ffOXvP917mu428P3ZqC3oJN/Abqzf83jFAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
