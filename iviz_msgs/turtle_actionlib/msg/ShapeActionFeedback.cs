/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeActionFeedback")]
    public sealed class ShapeActionFeedback : IDeserializable<ShapeActionFeedback>, IActionFeedback<ShapeFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ShapeFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = ShapeFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ShapeActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ShapeActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = ShapeFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeActionFeedback(ref b);
        }
        
        ShapeActionFeedback IDeserializable<ShapeActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new ShapeActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXXMaNxR93xn+g2Z4iN2JnTbpR+oZHigQh46TeAztq0e7uuyq1WqppAXz73uuFpYl" +
                "hgkPSRhsvqRzj8499+q+J6nIiSK+JDILurJGp4+lz/2r20qaWZCh9sLHl2RWyCW9I1KpzP4Vi+2bXjL4" +
                "yo9e8mF2e4OoqmHyPvLrJX0BPlZJp0RJQSoZpFhU4K/zgtyVoRUZ5louSYn4a9gsyV9j47zQXuCZkyUn" +
                "jdmI2mNRqERWlWVtdSYDiaBLOtiPndoKKZbSBZ3VRjqsr5zSlpcvnCyJ0fH09F9NNiMxHd9gjfWU1UGD" +
                "0AYImSPptc3xo0hqbcOb17wh6c/X1RU+Uo4stMFFKGRgsvS0dOSZp/Q3iPFDc7hrYEMdQhTlxUX87hEf" +
                "/aVAEFCgZZUV4gLM7zehqCwASayk0zI1xMAZFADqC9704rKDzLRvhJW22sE3iPsY58DaFpfPdFUgZ4ZP" +
                "7+scAmLh0lUrrbA03USQzGiyQcB6TrpNwruakEn/HWuMRdgVM4JX6X2VaSRAibUOReKDY/SYjUetkm9m" +
                "yJMF0kv4PZKb44UpcI7f7sqm+XA/+TiefrwVu8dA/Ij/7EyK20QhvdhQYE+mxBJlTe63GjXBkXa3Qq02" +
                "mMPRfPr3RHQwfzrE5KTUzkFc+DAlluks4PuHyeTD/XwyboFfHwI7ygjuhjORdTiEv0EB+CDkIsDMOvDp" +
                "HeeInmIp2DzZE33+6OMPPokqNJ5DYS4NMYIOfocCohdzciUK0HA3CHS5pTz7azSaTMYdym8OKa+BLLNC" +
                "o0soWDFjFRY1t4JjQpwKM/zj08NeFw7z85EwaRWPrurozD33o5FUTV+Uhl3hK1TCQmpTOzpF72Hy52TU" +
                "4TcQvzyn5+gfypjfUTpcU1UdPrfLyy9zTCmTaKsRsw1Wo1UGCabcJNCstV1Jo9WpA2yd11bKQPz6HZzX" +
                "Ws9WIRbh3nxt8lqFR8O7u30lD8Rv5xJMCbcVHWV4jrrIyfNsHZK2C+1Kvtf4BmnTEFszMyF1cIiuTd5+" +
                "hUOcJzOb4qD8mgB8c5zwxN2n2bwLNRC/R8Ch3YmxvUCAJBSyxiDUiCBbCRjluhkEPAxuVNQtPaP2PGNX" +
                "rDZLutY4PioHsQ5bZ9IfGlOt40jCC1EKeFPt7yuQ2d5VXGOiM2DxFkVpnecs43ZRoKeQfNfbbDrmIYtN" +
                "0AwiW5184IzzkeLNDFXXhcaEEW/lTleJBiHFE9E0DjBxxjoiFfaTZQvhoORZIww6VC6RLmOwmzF9k781" +
                "IXQLvXMfXEmOu0pk1B0Ytvy12g0Z6Magh0bXTcRudmVDYgemrNoEDJXey5wzjOz4JWV6obNdPUQGng3E" +
                "6DzxNQtAqqxjXaDVaay63uUPq75d9kLtgqHHNomvDobzHkf+H1IdlNPeCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
