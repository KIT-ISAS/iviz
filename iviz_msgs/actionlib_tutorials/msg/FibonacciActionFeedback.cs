/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class FibonacciActionFeedback : IDeserializable<FibonacciActionFeedback>, IActionFeedback<FibonacciFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public FibonacciFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public FibonacciActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new FibonacciFeedback();
        }
        
        /// Explicit constructor.
        public FibonacciActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        public FibonacciActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new FibonacciFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new FibonacciActionFeedback(ref b);
        
        public FibonacciActionFeedback RosDeserialize(ref ReadBuffer b) => new FibonacciActionFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
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
                size += Feedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "73b8497a9f629a31c0020900e4148f07";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTXPbNhC981dgxofYnVppkzZNPaODKsmOOk7isdVeOh0PCK5ItCSo4kOy/n3eghRF" +
                "2VajQxKNbH0Bbx/evl3sO5IZWVHEl0Qqr2tT6vS+crl7eVXL8s5LH5xw8SW51GltpFL6kihLpfpXLNo3" +
                "yfALP5L3d1cXiJs1XN41DE8ECJlM2kxU5GUmvRSLGgfQeUH2vKQVlUy2WlIm4q9+syQ3wMZ5oZ3AMydD" +
                "VpblRgSHRb4Wqq6qYLSSnoTXFe3tx05thBRLab1WoZQW62ubacPLF1ZWxOh4OvovkFEkZpMLrDGOVPAa" +
                "hDZAUJak0ybHjyIJ2vjXr3hDcjJf1+f4SDnS0AUXvpCeydLD0pJjntJdIMZ3zeEGwIY4hCiZE6fxu3t8" +
                "dGcCQUCBlrUqxCmY32x8URsAklhJq2VaEgMrKADUF7zpxVkP2URoI029hW8QdzGOgTUdLp/pvEDOSj69" +
                "CzkExMKlrVc6w9J0E0FUqcl4Ae9ZaTcJ72pCJieXrDEWYVfMCF6lc7XSSEAm1toXifOW0WM27nWWfCU3" +
                "HqyPhN8iszleOD4n+O22aJoPN9MPk9mHK7F9DMUP+M+2pLhNFNKJDXk2ZEqsj2oS3wrUxEbO7Qp10GCO" +
                "xvPZn1PRw/xxH5MzEqyFsjBhSqzRUcA3t9Pp+5v5dNIBv9oHtqQI1oYtkXLYg7+B+50XcuHhZO359JYT" +
                "RA+xDkyeiP95nOAPJokqNIZDVS5LYgTt3RYFRE/nZCtUX8mtwNNZS/nuj/F4Op30KL/ep7wGslSFJqbt" +
                "gmIVFoH7wHNCHAoz+u3j7U4XDvPTM2HSOh49C9GWO+7PRsoCfVYadoWrUQYLqctg6RC92+nv03GP31D8" +
                "/JSepX9I+QMOiAVVB//YLt9/nmNKSqKnRswuWECf9BJMuUOgU2uzkqXODh2gdV5XKUPx5hs4r7OeqX0s" +
                "wp35uuR1Co9H19e7Sh6KX44lmBKuKnqW4THqIidPs7VP2iy0rfhS4+vD97tAZELZ3iH6Nnn7BQ5xnMxs" +
                "ir3yawLwtXHAE9cf7+Z9qKH4NQKOzFaM9vYAksiQNQahRgTZScAog2YKcDB4mUXd0iNqzzF2zWqzpGuN" +
                "46NypHnUOpOTUVnW6ziP8EKUguW67S4rkGkvKq4x0RuveEtGachzlrFd5OnBJ9/wKptNksYBzQjSiuQ8" +
                "p5vPE+9kSLouNGaLeB/3Wkp0B2U8C83i6BLaO+axTthPhv2DU5JjgTDiULVErsoSuxnTNclbE0J30Fvr" +
                "wZJkuaVERv1RoeWP7tKOF2jFoLfZz8J2ZGU3Ygfmq1B6jJPOyZya1LglKb3QalsMkYEbtOg86zULQKoK" +
                "sSjQ5zRWDbbJ4yHkq6fOByRHQ66XTwbzJIkz5l9/d2NpknwCFlrIcu0LAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
