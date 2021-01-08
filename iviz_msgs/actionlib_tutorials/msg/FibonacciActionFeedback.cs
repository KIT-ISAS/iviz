/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciActionFeedback")]
    public sealed class FibonacciActionFeedback : IDeserializable<FibonacciActionFeedback>, IActionFeedback<FibonacciFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public FibonacciFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciActionFeedback()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new FibonacciFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciActionFeedback(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new FibonacciFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionFeedback(ref b);
        }
        
        FibonacciActionFeedback IDeserializable<FibonacciActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                size += Feedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "73b8497a9f629a31c0020900e4148f07";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W224bNxB9X0D/QCAPsYPaaZNeUgN6UGXZUeEkhq32pSgMLjnaZbvLVUiuZP19znAv" +
                "kmIJ1UNqwbJu5JnDM2eG856kJify+JJIFUxlC5M+lD7zr68rWdwHGWovfHxJrkxaWamUuSLSqVT/inn7" +
                "ZpAMv/FjkHy4v75AZN2weR85DpIXApyslk6LkoLUMkgxr3AGk+XkzgpaUsF8ywVpEX8N6wX5c945y40X" +
                "+MvIkpNFsRa1x6pQCVWVZW2NkoFEMCXtAPBWY4UUC+mCUXUhHTZUThvL6+dOlhTx+enpc01WkZheXmCV" +
                "9aTqYEBqDQzlSHpjM/yIxbWx4e0b3oGNs1V1hs+UIR89AxFyGZgxPS4ceSYr/QWHedWc8RzwEIkQSHtx" +
                "Er97wEd/KhAHLGhRqVycgP7tOuSVBSKJpXRGpgUxsoIOgH3Jm16ebkMz9Qthpa06/AZyE+QYXLsB5mOd" +
                "5UhewRL4OoOOWLlw1dJorE3XEUUVhmwQ8KGTbj1IeFsTFCBXLDaWYV/MDV6l95UyyIQWKxPyQeKD4wAx" +
                "Lw9GD5L/zZ0HK2aQ8HtkOcNL5MDJftcWUvfpdvLxcvrxWnSPofge/9mnFDeKXHqxpsAOTYmFUo0JWqWa" +
                "8Ei/W3JpNKCj8Wz650Rsgf6wC8rJqZ2DxvBkSizVcci3d5PJh9vZ5LJHfrOL7EgRrA6TIv2wCn+DavBB" +
                "yHmAr01gARxnih5jXdhskGyoPn28wBOGiUI07kOlLgpiCBN8BwOqJzNyJQqy4P4Q6LQjff/HeDyZXG6R" +
                "frtLegVoqXKDxqFhSsVCzGtuDvu0OBhn9Nunu400HOfHPXHSKp5e19GhG/Z7Q+ma/lsd9oavUBNzaYra" +
                "0UGCd5PfJ+MthkPx01OCjv4hxQz3EuLyqurwtWm+O4JlSkqi2UbQPlqN/hkkuHLPQA83dikLow8eoTVg" +
                "XzJD8fNzGLB3oK1CLMeNB/sMblQej25uNkU9FL8cSzEl3GO0l+NRCiMxT1O2S9vOjSv5xuNrpU9F7NZM" +
                "hfTuMbbN8u4bHONIqdkaO4XYRODr5JAzbj7dz7axhuLXiDiynR7trQIooZE6RqFGB9mrwCjnzZTgYfRC" +
                "R+nSY6rQM3jFirOsKwMFUEII9lUnxRU2KopqFWcWXoqiwJtqc4uBT3uBcbmJrSmMt2hK6yyLWrarAj0G" +
                "xn3OS2562YxT7b3cqeUDZ55PFe9saLvKDcaPeF1v9ZhoFNJxZprG+SbOYXsEAwBZ9hLOSp51whxE5QJZ" +
                "Kwrezqi+yeOKELwH73wIf5LjJhM57U4T3SHQctohBC0aHNe7CZl30y7MyVswidVFwPjpvcw42UiTX5Ay" +
                "c6O66ogsPJuJ4eNg2KwAs7KOZYL2Z7AMKrSZbEaVZ8hjqJEoA+FeP5noB0kSp9K//u4nWTD6Auh/Oqwo" +
                "DAAA";
                
    }
}
