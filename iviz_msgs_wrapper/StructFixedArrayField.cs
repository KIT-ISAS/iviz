using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class StructFixedArrayField<T, TField> : IMessageField<T>
        where T : RosMessageWrapper<T>, IMessage, new()
        where TField : unmanaged
    {
        static readonly int FieldSize = Marshal.SizeOf<TField>();

        readonly Func<T, TField[]> getter;
        readonly Action<T, TField[]> setter;
        readonly uint arraySize;
        readonly string propertyName;

        public StructFixedArrayField(PropertyInfo property, uint arraySize, string propertyName)
        {
            this.propertyName = propertyName;
            this.arraySize = arraySize;
            getter = (Func<T, TField[]>) Delegate.CreateDelegate(typeof(Func<T, TField[]>),
                property.GetGetMethod()!);
            setter = (Action<T, TField[]>) Delegate.CreateDelegate(typeof(Action<T, TField[]>),
                property.GetSetMethod()!);
        }

        public void RosSerialize(T msg, ref Buffer b) => b.SerializeStructArray(getter(msg), arraySize);
        public void RosDeserialize(T msg, ref Buffer b) => setter(msg, b.DeserializeStructArray<TField>(arraySize));
        public int RosLength(T msg) => FieldSize * (int) arraySize;

        public void RosValidate(T msg)
        {
            var array = getter(msg);
            if (array is null)
            {
                throw new NullReferenceException($"Field '{propertyName}' is null");
            }

            if (array.Length != arraySize)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}