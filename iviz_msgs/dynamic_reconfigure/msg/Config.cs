/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract]
    public sealed class Config : IDeserializable<Config>, IMessage
    {
        [DataMember (Name = "bools")] public BoolParameter[] Bools;
        [DataMember (Name = "ints")] public IntParameter[] Ints;
        [DataMember (Name = "strs")] public StrParameter[] Strs;
        [DataMember (Name = "doubles")] public DoubleParameter[] Doubles;
        [DataMember (Name = "groups")] public GroupState[] Groups;
    
        /// Constructor for empty message.
        public Config()
        {
            Bools = System.Array.Empty<BoolParameter>();
            Ints = System.Array.Empty<IntParameter>();
            Strs = System.Array.Empty<StrParameter>();
            Doubles = System.Array.Empty<DoubleParameter>();
            Groups = System.Array.Empty<GroupState>();
        }
        
        /// Constructor with buffer.
        public Config(ref ReadBuffer b)
        {
            b.DeserializeArray(out Bools);
            for (int i = 0; i < Bools.Length; i++)
            {
                Bools[i] = new BoolParameter(ref b);
            }
            b.DeserializeArray(out Ints);
            for (int i = 0; i < Ints.Length; i++)
            {
                Ints[i] = new IntParameter(ref b);
            }
            b.DeserializeArray(out Strs);
            for (int i = 0; i < Strs.Length; i++)
            {
                Strs[i] = new StrParameter(ref b);
            }
            b.DeserializeArray(out Doubles);
            for (int i = 0; i < Doubles.Length; i++)
            {
                Doubles[i] = new DoubleParameter(ref b);
            }
            b.DeserializeArray(out Groups);
            for (int i = 0; i < Groups.Length; i++)
            {
                Groups[i] = new GroupState(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Config(ref b);
        
        public Config RosDeserialize(ref ReadBuffer b) => new Config(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Bools);
            b.SerializeArray(Ints);
            b.SerializeArray(Strs);
            b.SerializeArray(Doubles);
            b.SerializeArray(Groups);
        }
        
        public void RosValidate()
        {
            if (Bools is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Bools.Length; i++)
            {
                if (Bools[i] is null) BuiltIns.ThrowNullReference(nameof(Bools), i);
                Bools[i].RosValidate();
            }
            if (Ints is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Ints.Length; i++)
            {
                if (Ints[i] is null) BuiltIns.ThrowNullReference(nameof(Ints), i);
                Ints[i].RosValidate();
            }
            if (Strs is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Strs.Length; i++)
            {
                if (Strs[i] is null) BuiltIns.ThrowNullReference(nameof(Strs), i);
                Strs[i].RosValidate();
            }
            if (Doubles is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Doubles.Length; i++)
            {
                if (Doubles[i] is null) BuiltIns.ThrowNullReference(nameof(Doubles), i);
                Doubles[i].RosValidate();
            }
            if (Groups is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Groups.Length; i++)
            {
                if (Groups[i] is null) BuiltIns.ThrowNullReference(nameof(Groups), i);
                Groups[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += BuiltIns.GetArraySize(Bools);
                size += BuiltIns.GetArraySize(Ints);
                size += BuiltIns.GetArraySize(Strs);
                size += BuiltIns.GetArraySize(Doubles);
                size += BuiltIns.GetArraySize(Groups);
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "dynamic_reconfigure/Config";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "958f16a05573709014982821e6822580";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE72SwQrCMAyG73mKvoGg4kHwMoThQRB6FJFuy0aha0eaCr693RzY6VXaU/LRw5fkL5wz" +
                "F0WqR0a63kQVew8nyynUlj1IppR5Jg9HFyqDKW4m4qEkFwbJijHCbmw8wOHPD86y3IvmaVWv6zth7Wyr" +
                "u0C4KtKxILpq24n4DWEcUDyUCZjTJ13oQieudrPO75Mec+Ez19mFvpK0cGqNU7zb5pf6hPg3Qn7C7/Pp" +
                "Zi4GRWgZ4AUgtrOmVgMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
