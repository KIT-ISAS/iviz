/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveGroupActionFeedback : IDeserializable<MoveGroupActionFeedback>, IActionFeedback<MoveGroupFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public MoveGroupFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public MoveGroupActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new MoveGroupFeedback();
        }
        
        /// Explicit constructor.
        public MoveGroupActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal MoveGroupActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new MoveGroupFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveGroupActionFeedback(ref b);
        
        MoveGroupActionFeedback IDeserializable<MoveGroupActionFeedback>.RosDeserialize(ref Buffer b) => new MoveGroupActionFeedback(ref b);
    
        public void RosSerialize(ref Buffer b)
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1Wy3IbNxC871egSgdLqUhJ7DwcVfHAkLRMl2SrJCZXFXYx3EWCxW4ALCn+fXqwD5IR" +
                "WebBNosSX0BPY7pnMO9JKnKiiC+JzIKurNHpU+lz/8NNJc1jkKHxwseX5K5a0Y2rmvodkUpl9o9Ydm+S" +
                "0Rd+JHePN9eIq1ou71uGZwKErJJOiZKCVDJIsaxwAJ0X5C4Nrcgw2bImJeKvYVOTv8LGRaG9wDMnS04a" +
                "sxGNx6JQiawqy8bqTAYSQZe0tx87tRVS1NIFnTVGOqyvnNKWly+dLInR8fT0b0M2IzGfXmON9ZQ1QYPQ" +
                "BgiZI+m1zfGjSBptw5vXvCE5W6yrS3ykHDIMwUUoZGCy9Fw78sxT+mvE+K493BWwkRxCFOXFefzuCR/9" +
                "hUAQUKC6ygpxDub3m1BUFoAkVtJpmRpi4AwZAOor3vTqYgeZaV8LK23Vw7eI2xinwNoBl890WUAzw6f3" +
                "TY4EYmHtqpVWWJpuIkhmNNkg4D0n3SbhXW3I5Owd5xiLsCsqglfpfZVpCKDEWoci8cExelTjSavkK7nx" +
                "aH0k/BbK5njh+Czw275o2g/3s4/T+ccb0T9G4kf8Z1tS3CYK6cWGAhsyJc5P1grfJaiNDc3dCnXQYo4n" +
                "i/lfM7GD+dM+JivSOIfMwoQpcY5OAr5/mM3u7hez6QD8eh/YUUawNmwJyWEP/gbu90HIZYCTdeDTOxaI" +
                "nmMd2DzZEn35OMMfTBKz0BoOVVkbYgQdfI8CoucLciWqz3ArCHTRUX78czKZzaY7lN/sU14DWWaFRotQ" +
                "8GHGWVg23AcOJeJYmPEfnx62eeEwPx8Ik1bx6KqJttxyPxhJNfTZ1LArfIUyWEptGkfH6D3MPswmO/xG" +
                "4peX9Bz9TRnzO0iHC6pqwv/t8v3nOaaUSfTUiDkEa9AngwRT7hDo1NqupNHq2AE65w2VMhK/fgPnDdaz" +
                "VYhFuDXfIN6Q4cn49nZbySPx26kEU8JVRQcZnpJdaPJSrX3SdqldyZcaXx+DDLEvMxNSe4fYtcnbL3CI" +
                "09LMptgrvzYAXxtHPHH76XGxCzUSv0fAse2T0d0eQBIKqjEItUmQQwoY5aqdAjwMblTMW3pC7XnGxuSD" +
                "Cxr5WWscH5WDWPutMzkbG1Ot4zzCC1EKeFNtLyuQ6S4qrjGxM17xFkVpk+ecxm5RoOeQfMOrbD6NU1J3" +
                "7/ZJ8oHl5vPEOxkpXRcas0W8j3daSnQHKZ6F5nF0idPVgTxhP1n2D05JnhOEEYfKGloZg92M6Vvx1oTQ" +
                "A3RvPViSHLeUyGh3VOj4o7t044XH4rVEl9tVoR9Z2Y3YgfmqMQHjpPcyZ3khja8p00ud9cUQGXh2D6Pz" +
                "rNcuAKmyiUWBPqex6qoXj4eQryRdCSvq0Or2YiBH1I4BTx+U/Af5w+3V4QsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
