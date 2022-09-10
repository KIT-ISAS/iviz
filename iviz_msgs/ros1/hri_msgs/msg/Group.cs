/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class Group : IHasSerializer<Group>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "group_id")] public string GroupId;
        // List of person IDs of the people in this group
        [DataMember (Name = "members")] public string[] Members;
    
        public Group()
        {
            GroupId = "";
            Members = EmptyArray<string>.Value;
        }
        
        public Group(in StdMsgs.Header Header, string GroupId, string[] Members)
        {
            this.Header = Header;
            this.GroupId = GroupId;
            this.Members = Members;
        }
        
        public Group(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out GroupId);
            b.DeserializeStringArray(out Members);
        }
        
        public Group(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeString(out GroupId);
            b.Align4();
            b.DeserializeStringArray(out Members);
        }
        
        public Group RosDeserialize(ref ReadBuffer b) => new Group(ref b);
        
        public Group RosDeserialize(ref ReadBuffer2 b) => new Group(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(GroupId);
            b.SerializeArray(Members);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(GroupId);
            b.SerializeArray(Members);
        }
        
        public void RosValidate()
        {
            if (GroupId is null) BuiltIns.ThrowNullReference();
            if (Members is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Members.Length; i++)
            {
                if (Members[i] is null) BuiltIns.ThrowNullReference(nameof(Members), i);
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(GroupId);
                size += WriteBuffer.GetArraySize(Members);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, GroupId);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Members);
            return size;
        }
    
        public const string MessageType = "hri_msgs/Group";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "76be953e6ddf78f60b879c220d0a3c32";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61SwWrcMBC96ysGfEhS2IS0t4XeQppACyXJLYRlVpq1B2xJ0Yw39d93JDdpe8shRiAk" +
                "z3vz5j3dEAYqMLTNOdHCsYe+pDnvODjXwXcWhXSATEVShNsrqScdyG5SHgk42ollBf1heHyCiaa9QZz7" +
                "+sGf+3H/bQuiYTdJLxc3q/QO7hVjwBKss2JARTgkm4z7gcpmpCONBsIpU4D2V5dMcm7Ah6q+DkCRCo7j" +
                "ArNYkSbwaZrmyB6VQHmi//CGtNERMhZlP49YrD6VwLGWHwpOVNltCT3PFD2ZeVuriUJ+VjZBizH4QijV" +
                "9NsrcDNH/fK5AqCDx7skl0+ue3hJG7un3oJ6U2Geo1bV9CsXkioYZWvNPq1TnlsTc4msXRA4bXc7O8oZ" +
                "WDfTQjn5AU5thJ+LDim2SI9YGPc1VAFvVhjrSQWdnP3DHBt1xJhe6VfGvz3eQxvfeOtMm8HCG6sNMvfm" +
                "pBXmko4crHS/NBI/MkWFkfcFy+Iqam3puutq9voKWzS2o0jybEkEeGEdXl92i6W97N9vCLMR+gIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Group> CreateSerializer() => new Serializer();
        public Deserializer<Group> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Group>
        {
            public override void RosSerialize(Group msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Group msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Group msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Group msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Group msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Group>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Group msg) => msg = new Group(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Group msg) => msg = new Group(ref b);
        }
    }
}
