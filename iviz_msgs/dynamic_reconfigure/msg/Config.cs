/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/Config")]
    public sealed class Config : IDeserializable<Config>, IMessage
    {
        [DataMember (Name = "bools")] public BoolParameter[] Bools { get; set; }
        [DataMember (Name = "ints")] public IntParameter[] Ints { get; set; }
        [DataMember (Name = "strs")] public StrParameter[] Strs { get; set; }
        [DataMember (Name = "doubles")] public DoubleParameter[] Doubles { get; set; }
        [DataMember (Name = "groups")] public GroupState[] Groups { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Config()
        {
            Bools = System.Array.Empty<BoolParameter>();
            Ints = System.Array.Empty<IntParameter>();
            Strs = System.Array.Empty<StrParameter>();
            Doubles = System.Array.Empty<DoubleParameter>();
            Groups = System.Array.Empty<GroupState>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Config(BoolParameter[] Bools, IntParameter[] Ints, StrParameter[] Strs, DoubleParameter[] Doubles, GroupState[] Groups)
        {
            this.Bools = Bools;
            this.Ints = Ints;
            this.Strs = Strs;
            this.Doubles = Doubles;
            this.Groups = Groups;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Config(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Config(ref b);
        }
        
        Config IDeserializable<Config>.RosDeserialize(ref Buffer b)
        {
            return new Config(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeArray(Bools, 0);
            b.SerializeArray(Ints, 0);
            b.SerializeArray(Strs, 0);
            b.SerializeArray(Doubles, 0);
            b.SerializeArray(Groups, 0);
        }
        
        public void Dispose()
        {
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
                foreach (var i in Bools)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Ints)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Strs)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Doubles)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Groups)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/Config";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "958f16a05573709014982821e6822580";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE72SwQrCMAyG73mKvoGg4kHwMoThQRB6FJFuy0aha0eaCr693RzY6VXaU/LRw5fkL5wz" +
                "F0WqR0a63kQVew8nyynUlj1IppR5Jg9HFyqDKW4m4qEkFwbJijHCbmw8wOHPD86y3IvmaVWv6zth7Wyr" +
                "u0C4KtKxILpq24n4DWEcUDyUCZjTJ13oQieudrPO75Mec+Ez19mFvpK0cGqNU7zb5pf6hPg3Qn7C7/Pp" +
                "Zi4GRWgZ4AUgtrOmVgMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
