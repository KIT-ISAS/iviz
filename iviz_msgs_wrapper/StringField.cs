using System;
using System.Reflection;
using Iviz.Msgs;

namespace Iviz.MsgsWrapper
{
    internal sealed class StringField<T> : IMessageField<T> where T : IDeserializable<T>, ISerializable
    {
        readonly Func<T, string> getter;
        readonly Action<T, string> setter;
        readonly string propertyName;

        public StringField(PropertyInfo property, string propertyName)
        {
            getter = (Func<T, string>) Delegate.CreateDelegate(typeof(Func<T, string>), property.GetGetMethod()!);
            setter = (Action<T, string>) Delegate.CreateDelegate(typeof(Action<T, string>),
                property.GetSetMethod()!);
            this.propertyName = propertyName;
        }

        public void RosSerialize(T msg, ref WriteBuffer b) => b.Serialize(getter(msg));
        public void RosDeserialize(T msg, ref ReadBuffer b) => setter(msg, b.DeserializeString());
        public int RosLength(T msg) => 4 + BuiltIns.UTF8.GetByteCount(getter(msg));

        public void RosValidate(T msg)
        {
            var array = getter(msg);
            if (array is null)
            {
                throw new NullReferenceException($"Field '{propertyName}' is null");
            }
        }
    }
}