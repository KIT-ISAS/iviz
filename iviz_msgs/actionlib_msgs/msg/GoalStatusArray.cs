/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibMsgs
{
    [Preserve, DataContract (Name = "actionlib_msgs/GoalStatusArray")]
    public sealed class GoalStatusArray : IDeserializable<GoalStatusArray>, IMessage
    {
        // Stores the statuses for goals that are currently being tracked
        // by an action server
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status_list")] public GoalStatus[] StatusList { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GoalStatusArray()
        {
            Header = new StdMsgs.Header();
            StatusList = System.Array.Empty<GoalStatus>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GoalStatusArray(StdMsgs.Header Header, GoalStatus[] StatusList)
        {
            this.Header = Header;
            this.StatusList = StatusList;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GoalStatusArray(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            StatusList = b.DeserializeArray<GoalStatus>();
            for (int i = 0; i < StatusList.Length; i++)
            {
                StatusList[i] = new GoalStatus(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GoalStatusArray(ref b);
        }
        
        GoalStatusArray IDeserializable<GoalStatusArray>.RosDeserialize(ref Buffer b)
        {
            return new GoalStatusArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(StatusList, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (StatusList is null) throw new System.NullReferenceException(nameof(StatusList));
            for (int i = 0; i < StatusList.Length; i++)
            {
                if (StatusList[i] is null) throw new System.NullReferenceException($"{nameof(StatusList)}[{i}]");
                StatusList[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in StatusList)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_msgs/GoalStatusArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8b2b82f13216d0a8ea88bd3af735e619";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW/jNhC9G/B/IJDDJosmbXf7sQ3gg2u7qYvsbhC7vRRFQJFjiS0luSRlx/9+31Cy" +
                "JCcO6sNunTiJHPLN45s3wzkTi1A68iJkJHyQofJ4WJVOpKW0/LEMQjoSqnKOimB3IiFTpCI4qf4hPRyc" +
                "iWQnZCGkCqYshCe3ITcc/EpSkxNZ/DUc3ABtEeH//KuJ82CND8MBf40+82s4eL+4uUYc/ZD71H9dk2Gu" +
                "4FBo6bTIKUgtg4xnzUyakbu0tCHL7PI1aRH/G3Zr8le8c5kZL/CdUkFOWggBqbQIpVBlnleFUTKQCCan" +
                "AwDeaqCOWEsXjKqsdNhQOm0KXr9yMqeIz29P/1ZUKBLz6TVWFZ5UFQxI7YChHEnP0s+nWFyZIrx9wzuw" +
                "cbktL/FMKRRvGdS5A2N6XCPDTFb6aw7zuj7jFeAhEiGQ9uI8fvaAR38hEAcsaF2qTJyD/t0uZEgum2Qj" +
                "nZGJJUZW0AGwr3jTq4s+NFO/FoUsyj1+DdkFOQW36ID5WJcZkmdZAl+l0BEr167cGI21MCGjKGvgUmFN" +
                "4qTbDQe8rQ4KkF9YbCzDvpgb/Jbel8ogE1psTciGAx8cB4h5eTD6C7qzLhgwrT3aVUhdLcgyl2DkwMl+" +
                "15TN/ulu9mE6/3Aj9q+R+AY/2acUN4pMerGjwA5NiIVStQkapZ7Uaw06niznf8xED/TbQ1BOzpNOcBry" +
                "3f1s9v5uOZu2yG8OkR0pgtVhUqQfVuFPUA0e7WcV4GsTWADHmaLHWBdFOhx0VJ+/zvCGYaIQtftQqWtL" +
                "DGGC38OA6vmSXI6CtNwfAl3sSS9+n0xms2mP9NtD0ltAS5UZNA4NUyoWYlVxczimxYtxxj9/vO+k4Tjf" +
                "HYmTlPH0uooO7dgfDaUr+m912Bu+RE2spLEVuvxLBO9nv80mPYYj8f1zgo7+JsUMjxLi8iqr8NQ0X53A" +
                "MiEl0WwjaButQv8MEly5Z6CHm2IjrdEvHqExYFsyI/HD/2HA1oFFGWI5dh5sM9ipPBnf3nZFPRI/nkox" +
                "IdxjdJTjSQojMc9Tdki7WBmX843H10qbititmQrPAv1j9M3y7jMc40Sp2RoHhVhH4OvkJWfcflws+1gj" +
                "8VNEHLdDTXOrAEpopI5RqBmNWhUY5aqeEjyMbnWULjmlCj2Dl6w4y7o1UODoTIUrbGxtuY0zCy9FUeCP" +
                "srvFwKe5wLjcRHenxC2akipNo5bNqkCPX3YEO3LJzaf1ONXcy3u1PM+h8VTxzoa228xg/IjXda/HRKOQ" +
                "jjPTPM43cQ47IpjYZlSwl3BWnnBLdB2ifI2sWcvbeyPulhC8Bd/7EP4kx00mcjqcJvaHQMtphhC0aHBE" +
                "7+snZEWkE4zKbE7egkmssgHjp/cy5WQjTX5NyqyM2ldHZOHZTAwfB8N6BZjlVSwTtD+DZVChyWQ9qgwH" +
                "nwAzz/rD0QsAAA==";
                
    }
}
