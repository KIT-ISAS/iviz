using System;
using System.Reflection;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class MessageArrayField<T, TField> : IMessageField<T>
        where T : IDeserializable<T>, ISerializable    
        where TField : IDeserializable<TField>, IMessage, new()
    {
        static readonly IDeserializable<TField> Generator = new TField();
        static readonly bool IsValueType = typeof(T).IsValueType;

        static readonly int? FieldSize = BuiltIns.TryGetFixedSize<TField>(out int realFieldSize) ? realFieldSize : null;

        readonly Func<T, TField[]> getter;
        readonly Action<T, TField[]> setter;
        readonly string propertyName;

        public MessageArrayField(PropertyInfo property, string propertyName)
        {
            this.propertyName = propertyName;
            getter = (Func<T, TField[]>) Delegate.CreateDelegate(typeof(Func<T, TField[]>),
                property.GetGetMethod()!);
            setter = (Action<T, TField[]>) Delegate.CreateDelegate(typeof(Action<T, TField[]>),
                property.GetSetMethod()!);
        }

        public void RosSerialize(T msg, ref WriteBuffer b) => b.SerializeArray(getter(msg));

        public void RosDeserialize(T msg, ref ReadBuffer b)
        {
            TField[] array = b.DeserializeArray<TField>();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Generator.RosDeserialize(ref b);
            }

            setter(msg, array);
        }

        public int RosLength(T msg)
        {
            var array = getter(msg);
            if (FieldSize != null)
            {
                return 4 + array.Length * FieldSize.Value;
            }

            int count = 4;
            for (int i = 0; i < array.Length; i++)
            {
                count += array[i].RosMessageLength;
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

            if (IsValueType)
            {
                return;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] is null)
                {
                    throw new NullReferenceException($"Element '{propertyName}[{i}]' is null");
                }

                array[i].RosValidate();
            }
        }
    }
}