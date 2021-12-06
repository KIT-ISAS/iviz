using System.Runtime.Serialization;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.MsgsWrapper
{
    public abstract class RosRequestWrapper<TService, T, TResponse> : IRequest<TService, TResponse>, IDeserializable<T>
        where TService : RosServiceWrapper<TService, T, TResponse>, IService, new()
        where T : RosRequestWrapper<TService, T, TResponse>, IDeserializable<T>, new()
        where TResponse : RosResponseWrapper<TService, T, TResponse>, new()
    {
        static RosSerializableDefinition<T>? definition;
        static RosSerializableDefinition<T> Definition => definition ??= new RosSerializableDefinition<T>();

        internal static string RosInputForMd5 => Definition.RosInputForMd5;

        public void RosSerialize(ref WriteBuffer b) => Definition.Serialize((T) this, ref b);
        
        [IgnoreDataMember] public int RosMessageLength => Definition.GetLength((T) this);
        
        public void RosValidate() => Definition.Validate((T) this);
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => RosDeserialize(ref b);

        /// <summary>
        /// Creates a ROS definition for this message (the contents of a .msg file).
        /// </summary>
        [IgnoreDataMember]
        public static string RosDefinition => Definition.RosDefinition;

        public T RosDeserialize(ref ReadBuffer b)
        {
            var msg = new T();
            Definition.Deserialize(msg, ref b);
            return msg;
        }
    }
}