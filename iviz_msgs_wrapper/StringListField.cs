using System;
using System.Collections.Generic;
using System.Reflection;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class StringListField<T> : IMessageField<T> where T : IDeserializable<T>, ISerializable
    {
        readonly Func<T, List<string>> getter;
        readonly string propertyName;

        public StringListField(PropertyInfo property, string propertyName)
        {
            this.propertyName = propertyName;
            getter = (Func<T, List<string>>) Delegate.CreateDelegate(typeof(Func<T, List<string>>),
                property.GetGetMethod()!);
        }

        public void RosSerialize(T msg, ref WriteBuffer b) => b.SerializeArray(getter(msg));
        public void RosDeserialize(T msg, ref ReadBuffer b) => b.DeserializeStringList(getter(msg));

        public int RosLength(T msg)
        {
            var array = getter(msg);
            int count = 4 + 4 * array.Count;
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

            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] is null)
                {
                    throw new NullReferenceException($"{propertyName}[{i}]");
                }
            }
        }
    }
}