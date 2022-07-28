/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class ParameterEventDescriptors : IDeserializable<ParameterEventDescriptors>, IMessage
    {
        // This message contains descriptors of a parameter event.
        // It was an atomic update.
        // A specific parameter name can only be in one of the three sets.
        [DataMember (Name = "new_parameters")] public ParameterDescriptor[] NewParameters;
        [DataMember (Name = "changed_parameters")] public ParameterDescriptor[] ChangedParameters;
        [DataMember (Name = "deleted_parameters")] public ParameterDescriptor[] DeletedParameters;
    
        public ParameterEventDescriptors()
        {
            NewParameters = System.Array.Empty<ParameterDescriptor>();
            ChangedParameters = System.Array.Empty<ParameterDescriptor>();
            DeletedParameters = System.Array.Empty<ParameterDescriptor>();
        }
        
        public ParameterEventDescriptors(ParameterDescriptor[] NewParameters, ParameterDescriptor[] ChangedParameters, ParameterDescriptor[] DeletedParameters)
        {
            this.NewParameters = NewParameters;
            this.ChangedParameters = ChangedParameters;
            this.DeletedParameters = DeletedParameters;
        }
        
        public ParameterEventDescriptors(ref ReadBuffer b)
        {
            b.DeserializeArray(out NewParameters);
            for (int i = 0; i < NewParameters.Length; i++)
            {
                NewParameters[i] = new ParameterDescriptor(ref b);
            }
            b.DeserializeArray(out ChangedParameters);
            for (int i = 0; i < ChangedParameters.Length; i++)
            {
                ChangedParameters[i] = new ParameterDescriptor(ref b);
            }
            b.DeserializeArray(out DeletedParameters);
            for (int i = 0; i < DeletedParameters.Length; i++)
            {
                DeletedParameters[i] = new ParameterDescriptor(ref b);
            }
        }
        
        public ParameterEventDescriptors(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out NewParameters);
            for (int i = 0; i < NewParameters.Length; i++)
            {
                NewParameters[i] = new ParameterDescriptor(ref b);
            }
            b.DeserializeArray(out ChangedParameters);
            for (int i = 0; i < ChangedParameters.Length; i++)
            {
                ChangedParameters[i] = new ParameterDescriptor(ref b);
            }
            b.DeserializeArray(out DeletedParameters);
            for (int i = 0; i < DeletedParameters.Length; i++)
            {
                DeletedParameters[i] = new ParameterDescriptor(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new ParameterEventDescriptors(ref b);
        
        public ParameterEventDescriptors RosDeserialize(ref ReadBuffer b) => new ParameterEventDescriptors(ref b);
        
        public ParameterEventDescriptors RosDeserialize(ref ReadBuffer2 b) => new ParameterEventDescriptors(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(NewParameters);
            b.SerializeArray(ChangedParameters);
            b.SerializeArray(DeletedParameters);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(NewParameters);
            b.SerializeArray(ChangedParameters);
            b.SerializeArray(DeletedParameters);
        }
        
        public void RosValidate()
        {
            if (NewParameters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < NewParameters.Length; i++)
            {
                if (NewParameters[i] is null) BuiltIns.ThrowNullReference(nameof(NewParameters), i);
                NewParameters[i].RosValidate();
            }
            if (ChangedParameters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < ChangedParameters.Length; i++)
            {
                if (ChangedParameters[i] is null) BuiltIns.ThrowNullReference(nameof(ChangedParameters), i);
                ChangedParameters[i].RosValidate();
            }
            if (DeletedParameters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < DeletedParameters.Length; i++)
            {
                if (DeletedParameters[i] is null) BuiltIns.ThrowNullReference(nameof(DeletedParameters), i);
                DeletedParameters[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += WriteBuffer.GetArraySize(NewParameters);
                size += WriteBuffer.GetArraySize(ChangedParameters);
                size += WriteBuffer.GetArraySize(DeletedParameters);
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, NewParameters);
            WriteBuffer2.AddLength(ref c, ChangedParameters);
            WriteBuffer2.AddLength(ref c, DeletedParameters);
        }
    
        public const string MessageType = "rcl_interfaces/ParameterEventDescriptors";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a2c7c988dc9c0e2396a40b4fabba8c52";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE91XTY/bNhC9+1cQu4e9CGoaZItigR5a9AN7KBAkuRWFQ4sjmwBFKiRl1Sny3/uGlGg5" +
                "dtrdTZFDFvshS5yZxzdvHrXX4s1OB9FRCHJLonE2Sm2DUBQar/vofBCuFVL00suOInlBe7KxXl2L+yhG" +
                "GYS0QkbX6UYMvZKR+NGPIvTU6BY3j4EWf0WD5c6ag9iQ0HxJnD/uCD+eSASKoV6tXs5RPxcgf/wpLI3r" +
                "ki98YlGzk3ZL6r8XKjK4d7Jw9cP//LX6/fVvd8I3Zq0tKrSyofDNBTjgLDUC38zF3I/o0JKuG6xuwOyy" +
                "DTfLHoEwjqdM8cRnWVqvQvTabtNTXvmLHTqxl2YgdM8TErXakuJ+cODbgu/Noae6C9u3M6B6NWAf34uI" +
                "B5xp3oB29qxsJfY66I0h0XrXIXn0jlWRFkfnTCjA1DENZy31WY9YAkXGkB4YXAL+1uiwW0YljSql+Vqa" +
                "ZZgYd7rZseysiyw6+qv32Awp5Bt13CXQci+1kYx1EVoJqre1uEpy7b0GtSBuA51cQeMI/+nAy3kcULZC" +
                "InQv7NxgVJH4gEKpi0Z63R4+iQzZNly861mTiC0wjyD7S7QgzLixMHkkYf0Rd/etuIl+oBvOlRudJDBT" +
                "k8dGyJYL6Ch2knNjpbbIKI1+T6pebdA34UmqddphK02AEHJ6aQ+zCFpngIoBBXo3kG0mpXEh6vp4qI4o" +
                "jjhRKmjFAkZCPOqE7HtUAX+J24Wimf5fjZMRNV46xL7K6K0S9xi0LfnpBop2QxykSZw2Zgh6T2leLoQz" +
                "FB3iZHks23XiqMKn6Mo1ikgRIvX16jwJjKWdbq57vrv2fDtxtET2+FrLcFTR+eOc/os41/l2sa1XxFKl" +
                "JEY3WBUWqCeNtc7zJqdokXhJJqKWTUWu11H6uAjClVaTV1XYcmlgyvXdiwVx2dnUY4JnnlNlCJx7kYMY" +
                "O+s/jjTJNJkYbwxqTNvkcy4hxi4hztRPRT7P+4bNupNbzM6gKKndEzBhBGgLEva0NOCIiYrJkbhSYBPH" +
                "9PUu6MXK6pxWwH1P3gkN29CcKx3g2g5uCEiWlDHtCQny1Nwr4mGocinONSbHAmIDw8FtPqFxzL/D0OTR" +
                "wwkBpUpM8ZIRtqzU7orBjmSMyG8D/IKAoTNRw8xmR7iUocRvBjZAS5ppSox4lNeeHec6m0ty6U1wZoh0" +
                "3H3ZAxoAh8U4/Dv+ZLPH+nF0BUPxo0nDo8aGNpRuJa9bqqnOR4OejGpiWodylIKJv4/SvBPf1s+qIrc7" +
                "8Zw/MvA7cVs/+1CKI92yTAGB8NR9xJ0wUvZeOvcQrk8XaZYKG7M8axqnXzBzFd1VzpOBSTPKQ8haX8Cu" +
                "kG+i52HkPD8l5/ZIDp58yBSXE+ucnBT+gn8xRbdM0TzgnOULWePSnR9hinb28c+zQyR5qhnm0M+1wvzS" +
                "/zBrOjOmc1Pil7On2dJlU2JJPsWWvm5TOrGkYkgfPpq3bEondjSZUT4Bvy4vOnGi4kOZk4nYyy4ED5od" +
                "KP2XVOznH+PK0dRkDwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
