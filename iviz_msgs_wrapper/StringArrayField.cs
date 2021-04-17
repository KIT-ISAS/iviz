using System;
using System.Reflection;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class StringArrayField<T> : IMessageField<T> where T : IMessage
    {
        readonly Func<T, string[]> getter;
        readonly Action<T, string[]> setter;
        readonly string propertyName;

        public StringArrayField(PropertyInfo property, string propertyName)
        {
            this.propertyName = propertyName;
            getter = (Func<T, string[]>) Delegate.CreateDelegate(typeof(Func<T, string[]>),
                property.GetGetMethod()!);
            setter = (Action<T, string[]>) Delegate.CreateDelegate(typeof(Action<T, string[]>),
                property.GetSetMethod()!);
        }

        public void RosSerialize(T msg, ref Buffer b) => b.SerializeArray(getter(msg));
        public void RosDeserialize(T msg, ref Buffer b) => setter(msg, b.DeserializeStringArray());

        public int RosLength(T msg)
        {
            string[] array = getter(msg);
            int count = 4 + 4 * array.Length;
            foreach (var s in array)
            {
                count += BuiltIns.UTF8.GetByteCount(s);
            }

            return count;
        }

        public void RosValidate(T msg)
        {
            var array = getter(msg);
            if (array is null)
            {
                throw new NullReferenceException($"Field '{propertyName}' is null");
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] is null)
                {
                    throw new NullReferenceException($"{propertyName}[{i}]");
                }
            }
        }
    }
}