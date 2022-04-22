using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Iviz.Msgs;

namespace Iviz.MsgsWrapper
{
    internal sealed class StructField<T, TField> : IMessageField<T>
        where T : IDeserializable<T>, ISerializable
        where TField : unmanaged
    {
        static readonly int FieldSize = Marshal.SizeOf<TField>();
        readonly Func<T, TField> getter;
        readonly Action<T, TField> setter;

        public StructField(PropertyInfo property)
        {
            getter = (Func<T, TField>)Delegate.CreateDelegate(typeof(Func<T, TField>), property.GetGetMethod()!);
            setter = (Action<T, TField>)Delegate.CreateDelegate(typeof(Action<T, TField>),
                property.GetSetMethod()!);
        }

        public void RosSerialize(T msg, ref WriteBuffer b) => b.Serialize(getter(msg));

        public void RosDeserialize(T msg, ref ReadBuffer b)
        {
            b.Deserialize(out TField t);
            setter(msg, t);
        }

        public int RosLength(T msg) => FieldSize;

        public void RosValidate(T msg)
        {
        }
    }
}