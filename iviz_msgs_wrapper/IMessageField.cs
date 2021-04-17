using Iviz.Msgs;

namespace Iviz.MsgsWrapper
{
    internal interface IMessageField<in T> where T : ISerializable, IDeserializable<T>
    {
        void RosSerialize(T msg, ref Buffer b);
        void RosDeserialize(T msg, ref Buffer b);
        void RosValidate(T msg);
        int RosLength(T msg);
    }
}