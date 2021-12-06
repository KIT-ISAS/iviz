using System;
using System.Collections.Generic;
using System.Reflection;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class StringFixedListField<T> : IMessageField<T> where T : IDeserializable<T>, ISerializable
    {
        readonly Func<T, List<string>> getter;
        readonly uint arraySize;
        readonly string propertyName;

        public StringFixedListField(PropertyInfo property, uint arraySize, string propertyName)
        {
            this.propertyName = propertyName;
            this.arraySize = arraySize;
            getter = (Func<T, List<string>>) Delegate.CreateDelegate(typeof(Func<T, List<string>>),
                property.GetGetMethod()!);
        }

        public void RosSerialize(T msg, ref WriteBuffer b) => b.SerializeArray(getter(msg), (int) arraySize);
        public void RosDeserialize(T msg, ref ReadBuffer b) => b.DeserializeStringArray(getter(msg), (int) arraySize);

        public int RosLength(T msg)
        {
            var array = getter(msg);
            int count = 4 * array.Count;
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

            if (array.Count != arraySize)
            {
                throw new RosInvalidSizeForFixedArrayException(propertyName, array.Count, (int) arraySize);
            }

            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] is null) throw new NullReferenceException($"{propertyName}[{i}]");
            }
        }
    }
}