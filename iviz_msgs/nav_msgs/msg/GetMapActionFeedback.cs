/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapActionFeedback")]
    public sealed class GetMapActionFeedback : IDeserializable<GetMapActionFeedback>, IActionFeedback<GetMapFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public GetMapFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapActionFeedback()
        {
            Header = new StdMsgs.Header();
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = GetMapFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapActionFeedback(StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, GetMapFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMapActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = GetMapFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapActionFeedback(ref b);
        }
        
        GetMapActionFeedback IDeserializable<GetMapActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new GetMapActionFeedback(ref b);
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
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC9c0b/ATM5xM7Ubps0beoZHVRJcZSxE4+t9uoBiRWJFgRZAJSsf9+3IKmP" +
                "WJrqkERjWV/A24e3bxf7gaQiJ4r4ksgs6MoanT6WPvc/XlfSPAQZGi98fEmuKdzK+j2RSmX2j1h0bwbJ" +
                "8Cs/Bsntw/UVwqqWyodIcJC8ECBklXRKlBSkkkGKRYUD6Lwgd2FoSYbJljUpEX8N65r8Je+cF9oL/OVk" +
                "yUlj1qLxWBUqkVVl2VidyUAi6JL2AHirtkKKWrqgs8ZIhw2VU9ry+oWTJUV8fnr6tyGbkZhNrrDKesqa" +
                "oEFqDYzMkfTa5vgRixttw5vXvAMb56vqAp8pRzI2DEQoZGDG9FQ78kxW+isO86o94yXgIRIhkPLiLH73" +
                "iI/+XCAOWFBdZYU4A/27dSgqC0QSS+m0TA0xcgYdAPuSN70834Vm6lfCSlv1+C3kNsgpuHYLzMe6KJA8" +
                "wxL4JoeOWFm7aqkV1qbriJIZTTYImNBJtx4kvK0NCpD3LDaWYV/MDV6l91WmkQklVjoUg8QHxwFiXh61" +
                "GiTfzJ1Hy2WQ8HtkOcdL5MDJftdVUf/pbvppMvt0LfrHUPyE/+xTihtFIb1YU2CHpsRCZa0JOqXa8Ei/" +
                "W3JptKCj8Xz211TsgP68D8rJaZyDxvBkSizVach399Pp7d18Otkgv95HdpQRrA6TIv2wCn+DavBByEWA" +
                "r3VgARxnip5iXdh8kGypPn+8wBOGiUK07kOl1oYYQgffw4Dq2ZxciYI03B8CnfekH/4cj6fTyQ7pN/uk" +
                "V4CWWaHROBRMmbEQi4abwyEtjsYZ/fH5fisNx/nlQJy0iqdXTXTolv3BUKqh/1eHveEr1MRCatM4Okrw" +
                "fvpxOt5hOBRvnxN09DdlzPAgIS6vqglfmuaHE1imlEk02wi6idagfwYJrtwz0MO1XUqj1dEjdAbclMxQ" +
                "/Po9DLhxoK1CLMetBzcZ3Ko8Ht3cbIt6KH47lWJKuMfoIMeTFEZinqdsn7ZdaFfyjcfXyiYVsVszFVL7" +
                "x9g1y7uvcIwTpWZr7BViG4Gvk2POuPn8MN/FGorfI+LI9np0twqghELqGIVaHeRGBUa5bKcED6MbFaVL" +
                "T6lCz+AVK86yrjQUQAkh2BedFFfYyJhqFWcWXoqiwJtqe4uBT3eBcbmJnRGMtyhKmzyPWnarAj0Fxv2e" +
                "l9xs0o5T3b3cq+UDZ55PFe9saLsqNMaPeF3v9JhoFFJxZprF+SbOYQcEAwBZ9hLOSp51whxEZY2sGcPb" +
                "GdW3eVwRgm/Aex/Cn+S4yURO+9NEfwit+iEELRoc0ft2E9IPuWxO3oJJrDEB46f3MudkI02+pkwvdNZX" +
                "R2Th2UwMHwfDdgWYlU0sE7Q/jWVQoctkO6p8szxauewyuDfDc8T/AJnM/xYFDAAA";
                
    }
}
