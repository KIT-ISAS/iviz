/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwoIntsActionResult : IDeserializable<TwoIntsActionResult>, IActionResult<TwoIntsResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TwoIntsResult Result { get; set; }
    
        /// Constructor for empty message.
        public TwoIntsActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TwoIntsResult();
        }
        
        /// Explicit constructor.
        public TwoIntsActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        public TwoIntsActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TwoIntsResult(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TwoIntsActionResult(ref b);
        
        public TwoIntsActionResult RosDeserialize(ref ReadBuffer b) => new TwoIntsActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength => 8 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3ba7dea8b8cddcae4528ade4ef74b6e7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WwXLbNhC98yswo0PsTq20SZqmntFBlVRHGSfx2EqvHpBYkWhBUAVAyfr7vgUpinKs" +
                "RIckGtu0JODtw9u3i31LUpETRXwkMgu6skan96XP/fOrSpq7IEPthY+PZLGp5jb4W/K1CcLFRzL6xq/k" +
                "/d3VJSKqhsXbhttAgIpV0ilRUpBKBimWFajrvCB3YWhNhmmWK1Iifhu2K/JDbFwU2gv85GTJSWO2ovZY" +
                "FCqRVWVZW53JQCLokg72Y6e2QoqVdEFntZEO6yuntOXlSydLYnT8ePqvJpuRmE8vscZ6yuqgQWgLhMyR" +
                "9Nrm+FIktbbh5QvekAwg5gXeUo4EdMFFKGRgsvSwgr7MU/pLxPipOdwQ2BCHEEV5cRY/u8dbfy4QBBRo" +
                "VWWFOAPzm20oKgtAEmvptEwNMXAGBYD6jDc9O+8h2whtpa128A3iPsYpsLbD5TNdFMiZ4dP7OoeAWLhy" +
                "1VorLE23ESQzmmwQcJ2TbpvwriZkMviLNcYi7IoZwVN6X2UaCVBio0OR+OAYPWbjXqvkO7nxaGUk/C8y" +
                "m+PB8TnBb3bl0ry5mX2Yzj9cid1rJH7BX7YlxW2ikF5sKbAhU2J9sibxrUBNbOTcrVEHDeZ4spj/PRM9" +
                "zF8PMTkjtXNQFiZMiTU6CfjmdjZ7f7OYTTvgF4fAjjKCtWFLpBz24E/gfh+EXAY4WQc+veME0UOsA5sn" +
                "4guvAX5hkqhCYzhU5coQI+jgdyggerYgV6L6DLeCQOct5btPk8lsNu1RfnlIeQNkmRWamLavM1ZhWXMf" +
                "eEqIY2HGf3683evCYV49ESat4tFVHW255/5kJFXTV6VhV/gKZbCU2tSOjtG7nb2bTXr8RuK3z+k5+oey" +
                "cMQBsaCqOjy2y89f55hSJtFTI2YXrEafDBJMuUOgU2u7lkarYwdonddVyki8/gHO66xnqxCLcG++Lnmd" +
                "wpPx9fW+kkfi91MJpoSrip5keIq6yMnn2TokbZfalXyp8fUR+l0gMiF1cIi+Td58g0OcJjOb4qD8mgB8" +
                "bRzxxPXHu0UfaiT+iIBjuxOjvT2AJBSyxiDUiCA7CRhl2EwBHgY3KuqWnlB7nrErVpsl3WgcH5Uj7aPW" +
                "mQzGxlSbOI/wQpSC47rtLiuQaS8qrjHRG6x4i6K0znOWsV0U6CEkP/Aqm0+TxgHNCNKK5AOnm88T72RI" +
                "uik0Zot4H/daSnQHKZ6F5nF0qds75rFO2E+W/YNTkmeBMOJQuUKujMFuxvRN8jaE0B30znqwJDluKZFR" +
                "f1Ro+aO7tOMFWjHobQ+zsCRSqcz+ZTdiRzO/Ypz0XubUpMavKNNLne2KITLwwxadZ71mAUiVdSwK9DmN" +
                "VcNd8ngI+d6pe34wiCeoldevUFpl8j8DF3zl0AsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
