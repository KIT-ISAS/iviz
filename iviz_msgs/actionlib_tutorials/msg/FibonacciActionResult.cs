/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class FibonacciActionResult : IDeserializable<FibonacciActionResult>, IActionResult<FibonacciResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public FibonacciResult Result { get; set; }
    
        /// Constructor for empty message.
        public FibonacciActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new FibonacciResult();
        }
        
        /// Explicit constructor.
        public FibonacciActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, FibonacciResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        public FibonacciActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new FibonacciResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new FibonacciActionResult(ref b);
        
        public FibonacciActionResult RosDeserialize(ref ReadBuffer b) => new FibonacciActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference();
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Result.RosMessageLength;
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_tutorials/FibonacciActionResult";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "bee73a9fe29ae25e966e105f5553dd03";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYnVppkzZNPaODKimOOk7isdVeOh0PCK5ItCSo4kOy/n3eghRF" +
                "OVajQxKNbVoS8Pbh7dvFviWZkRVFfCRSeV2bUqf3lcvd86talnde+uCEi4/kjU5rI5XSt+RC6YWNj2T0" +
                "hV/Ju7urS8TMGh5vG3YDATImkzYTFXmZSS/FsgZ5nRdkL0paU8lEqxVlIn7rtytyQ2xcFNoJ/ORkyMqy" +
                "3IrgsMjXQtVVFYxW0pPwuqKD/dipjZBiJa3XKpTSYn1tM214+dLKihgdP47+C2QUifn0EmuMIxW8BqEt" +
                "EJQl6bTJ8aVIgjb+5QvekAwWm/oCbylHCrrgwhfSM1l6WEFf5indJWJ81xxuCGyIQ4iSOXEWP7vHW3cu" +
                "EAQUaFWrQpyB+c3WF7UBIIm1tFqmJTGwggJAfcabnp33kE2ENtLUO/gGcR/jFFjT4fKZLgrkrOTTu5BD" +
                "QCxc2XqtMyxNtxFElZqMF/CdlXab8K4mZDJ4wxpjEXbFjOApnauVRgIysdG+SJy3jB6zca+z5Cu58Wht" +
                "JPwvMpvjwfE5wa93BdO8uZm9n87fX4ndayR+wF+2JcVtopBObMmzIVNifVST+FagJjZybteogwZzPFnM" +
                "/5yJHuaPh5ickWAtlIUJU2KNTgK+uZ3N3t0sZtMO+MUhsCVFsDZsiZTDHvwJ3O+8kEsPJ2vPp7ecIHqI" +
                "dWDyRPzPa4BfmCSq0BgOVbkqiRG0dzsUED1bkK1QfSW3Ak/nLeW7PyaT2Wzao/zykPIGyFIVmpi2C4pV" +
                "WAbuA08JcSzM+LcPt3tdOMxPT4RJ63j0LERb7rk/GSkL9Flp2BWuRhkspS6DpWP0bme/zyY9fiPx86f0" +
                "LP1Dyh9xQCyoOvjHdvn+8xxTUhI9NWJ2wQL6pJdgyh0CnVqbtSx1duwArfO6ShmJV9/AeZ31TO1jEe7N" +
                "1yWvU3gyvr7eV/JI/HIqwZRwVdGTDE9RFzn5NFuHpM1S24ovNb4+fL8LRCaUHRyib5PXX+AQp8nMpjgo" +
                "vyYAXxtHPHH94W7RhxqJXyPg2OzEaG8PIIkMWWMQakSQnQSMMmymAAeDl1nULT2h9hxj16w2S7rROD4q" +
                "R5pHrTMZjMuy3sR5hBeiFCzXbXdZgUx7UXGNid5oxVsySkOes4ztIk8PPvmGV9l8mjQOaEaQViTnOd18" +
                "nngnQ9JNoTFbxPu411KiOyjjWWgeR5fQ3jGPdcJ+MuwfnJIcC4QRh6oVclWW2M2YrknehhC6g95ZD5Yk" +
                "yy0lMuqPCi1/dJd2vEArBr3tYRaWRFkq1b/sRuxo5leMk87JnJrUuBUpvdRqVwyRgRu26DzrNQtAqgqx" +
                "KNDnNFYNd8njIeSrp84HJEdDruePhvIkiRPmX393Q2nyEYHLfDjmCwAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
