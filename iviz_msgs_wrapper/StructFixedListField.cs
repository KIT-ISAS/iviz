using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class StructFixedListField<T, TField> : IMessageField<T>
        where T : IDeserializable<T>, ISerializable
        where TField : unmanaged
    {
        static readonly int FieldSize = Marshal.SizeOf<TField>();

        readonly Func<T, List<TField>> getter;
        readonly uint arraySize;
        readonly string propertyName;

        public StructFixedListField(PropertyInfo property, uint arraySize, string propertyName)
        {
            this.propertyName = propertyName;
            this.arraySize = arraySize;
            getter = (Func<T, List<TField>>)Delegate.CreateDelegate(typeof(Func<T, List<TField>>),
                property.GetGetMethod()!);
        }

        public void RosSerialize(T msg, ref WriteBuffer b) => b.SerializeStructList(getter(msg), (int)arraySize);
        public void RosDeserialize(T msg, ref ReadBuffer b) => b.DeserializeStructList(getter(msg), (int)arraySize);
        public int RosLength(T msg) => FieldSize * (int)arraySize;

        public void RosValidate(T msg)
        {
            var array = getter(msg);
            if (array is null)
            {
                throw new NullReferenceException($"Field '{propertyName}' is null");
            }

            if (array.Count != arraySize)
            {
                throw new RosInvalidSizeForFixedArrayException(propertyName, array.Count, (int)arraySize);
            }
        }
    }
}