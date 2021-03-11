/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsActionFeedback")]
    public sealed class TwoIntsActionFeedback : IDeserializable<TwoIntsActionFeedback>, IActionFeedback<TwoIntsFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TwoIntsFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionFeedback(ref b);
        }
        
        TwoIntsActionFeedback IDeserializable<TwoIntsActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionFeedback(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC9c8b/ATM6xO7UTpv0I/WMDqqkOOo4icdWe/WAxIpEC4IqAErWv+9bkKKo" +
                "WJrokFojW1/A24e3bxf7gaQiJ4r4ksgs6MoanT6WPvevbyppHoIMtRc+viTzdTWzwb8nUqnM/hGL9s1Z" +
                "MvzGj7Pk48PNNeKqhsuHyPAsGQgwsko6JUoKUskgxaLCCXRekLs0tCLDbMslKRF/DZsl+StsnBfaCzxz" +
                "suSkMRtReywKlciqsqytzmQgEXRJe/uxU1shxVK6oLPaSIf1lVPa8vKFkyUxOp6e/q3JZiRmk2ussZ6y" +
                "OmgQ2gAhcyS9tjl+FEmtbXj7hjckA2h6iY+UIw9dcBEKGZgsPS0deeYp/TVifNcc7grYUIcQRXlxHr97" +
                "xEd/IRAEFGhZZYU4B/O7TSgqC0ASK+m0TA0xcAYFgPqKN7266CEz7Wthpa228A3iLsYpsLbD5TNdFsiZ" +
                "4dP7OoeAWLh01UorLE03ESQzmmwQMJ+TbpPwriZkMnjPGmMRdsWM4FV6X2UaCVBirUOR+OAYPWbjUavk" +
                "fzPk0RI5S/g9kpvjhSlwjt9tC6f5cDf9NJl9uhHbx1D8gP/sTIrbRCG92FBgT6bEEmVN7luNmuBIu1uh" +
                "WhvM0Xg++2sqepg/7mNyUmrnIC58mBLLdBLw3f10+vFuPp10wG/2gR1lBHfDmcg6HMLfoAB8EHIRYGYd" +
                "+PSOc0RPsRRsnuyIPn8M8AefRBUaz6Ewl4YYQQe/RQHR8zm5EgVouBsEumgpP/w5Hk+nkx7lt/uU10CW" +
                "WaHRJRSsmLEKi5pbwSEhjoUZ/f75fqcLh/npQJi0ikdXdXTmjvvBSKqmr0rDrvAVKmEhtakdHaN3P/1j" +
                "Ou7xG4qfn9Nz9DdlzO8gHa6pqg5f2uX7r3NMKZNoqxGzC1ajVQYJptwk0Ky1XUmj1bEDtM7rKmUofnkB" +
                "53XWs1WIRbgzX5e8TuHx6PZ2V8lD8eupBFPCbUUHGZ6iLnLyPFv7pO1Cu5LvNb5BujTE1sxMSO0dom+T" +
                "d9/gEKfJzKbYK78mAN8cRzxx+/lh3ocait8i4MhuxWgvECAJhawxCDUiyE4CRrlqBgEPgxsVdUtPqD3P" +
                "2BWrzZKuNY6PykGs/daZDEbGVOs4kvBClALeVLv7CmTau4prTPRGLN6iKK3znGVsFwV6CsmL3mazCQ9Z" +
                "bIJmEGl18oEzzkeKNzNUXRcaE0a8lXtdJRqEFE9EszjAxBnrgFTYT5YthIOSZ40w6FC5RLqMwW7G9E3+" +
                "1oTQHfTWfXAlOe4qkVF/YEga/mgw7ZCBbgx6aHT9RGxnVzYkdmDKqk3AUOm9zDnDyI5fUqYXOtvWQ2Tg" +
                "2UCMjk3tApAq61gXaHUaq662+cOqF8je6y/m8jPE/A+4nCZa2gsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
