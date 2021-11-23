/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        
        /// Explicit constructor.
        public Config(BoolParameter[] Bools, IntParameter[] Ints, StrParameter[] Strs, DoubleParameter[] Doubles, GroupState[] Groups)
        {
            this.Bools = Bools;
            this.Ints = Ints;
            this.Strs = Strs;
            this.Doubles = Doubles;
            this.Groups = Groups;
        }
        
        /// Constructor with buffer.
        internal Config(ref Buffer b)
        {
            Bools = b.DeserializeArray<BoolParameter>();
            for (int i = 0; i < Bools.Length; i++)
            {
                Bools[i] = new BoolParameter(ref b);
            }
            Ints = b.DeserializeArray<IntParameter>();
            for (int i = 0; i < Ints.Length; i++)
            {
                Ints[i] = new IntParameter(ref b);
            }
            Strs = b.DeserializeArray<StrParameter>();
            for (int i = 0; i < Strs.Length; i++)
            {
                Strs[i] = new StrParameter(ref b);
            }
            Doubles = b.DeserializeArray<DoubleParameter>();
            for (int i = 0; i < Doubles.Length; i++)
            {
                Doubles[i] = new DoubleParameter(ref b);
            }
            Groups = b.DeserializeArray<GroupState>();
            for (int i = 0; i < Groups.Length; i++)
            {
                Groups[i] = new GroupState(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Config(ref b);
        
        Config IDeserializable<Config>.RosDeserialize(ref Buffer b) => new Config(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Bools, 0);
            b.SerializeArray(Ints, 0);
            b.SerializeArray(Strs, 0);
            b.SerializeArray(Doubles, 0);
            b.SerializeArray(Groups, 0);
        }
        
        public void RosValidate()
        {
            if (Bools is null) throw new System.NullReferenceException(nameof(Bools));
            for (int i = 0; i < Bools.Length; i++)
            {
                if (Bools[i] is null) throw new System.NullReferenceException($"{nameof(Bools)}[{i}]");
                Bools[i].RosValidate();
            }
            if (Ints is null) throw new System.NullReferenceException(nameof(Ints));
            for (int i = 0; i < Ints.Length; i++)
            {
                if (Ints[i] is null) throw new System.NullReferenceException($"{nameof(Ints)}[{i}]");
                Ints[i].RosValidate();
            }
            if (Strs is null) throw new System.NullReferenceException(nameof(Strs));
            for (int i = 0; i < Strs.Length; i++)
            {
                if (Strs[i] is null) throw new System.NullReferenceException($"{nameof(Strs)}[{i}]");
                Strs[i].RosValidate();
            }
            if (Doubles is null) throw new System.NullReferenceException(nameof(Doubles));
            for (int i = 0; i < Doubles.Length; i++)
            {
                if (Doubles[i] is null) throw new System.NullReferenceException($"{nameof(Doubles)}[{i}]");
                Doubles[i].RosValidate();
            }
            if (Groups is null) throw new System.NullReferenceException(nameof(Groups));
            for (int i = 0; i < Groups.Length; i++)
            {
                if (Groups[i] is null) throw new System.NullReferenceException($"{nameof(Groups)}[{i}]");
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
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/Config";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "958f16a05573709014982821e6822580";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE72SwQrCMAyG73mKvoGg4kHwMoThQRB6FJFuy0aha0eaCr693RzY6VXaU/LRw5fkL5wz" +
                "F0WqR0a63kQVew8nyynUlj1IppR5Jg9HFyqDKW4m4qEkFwbJijHCbmw8wOHPD86y3IvmaVWv6zth7Wyr" +
                "u0C4KtKxILpq24n4DWEcUDyUCZjTJ13oQieudrPO75Mec+Ez19mFvpK0cGqNU7zb5pf6hPg3Qn7C7/Pp" +
                "Zi4GRWgZ4AUgtrOmVgMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
