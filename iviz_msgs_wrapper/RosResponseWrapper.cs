using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.MsgsWrapper
{
    public abstract class RosResponseWrapper<TService, TRequest, T> : IResponse, IDeserializable<T>
        where TService : RosServiceWrapper<TService, TRequest, T>, IService, new()
        where TRequest : RosRequestWrapper<TService, TRequest, T>, new()
        where T : RosResponseWrapper<TService, TRequest, T>, new()
    {
        static RosSerializableDefinition<T>? definition;
        static RosSerializableDefinition<T> Definition => definition ??= new RosSerializableDefinition<T>();

        internal static string RosInputForMd5 => Definition.RosInputForMd5;
        
        public void RosSerialize(ref Buffer b) => Definition.Serialize((T) this, ref b);
        
        [IgnoreDataMember] public int RosMessageLength => Definition.GetLength((T) this);
        
        public void RosValidate() => Definition.Validate((T) this);
        
        ISerializable ISerializable.RosDeserialize(ref Buffer b) => RosDeserialize(ref b);
        
        /// <summary>
        /// Creates a ROS definition for this message (the contents of a .msg file).
        /// </summary>
        [IgnoreDataMember] public static string RosDefinition => Definition.RosDefinition;        
        
        public T RosDeserialize(ref Buffer b)
        {
            var msg = new T();
            Definition.Deserialize(msg, ref b);
            return msg;
        }
    }
}