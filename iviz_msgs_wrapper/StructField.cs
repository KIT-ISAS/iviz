using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class EnumField<T, TField> : IMessageField<T>
        where T : RosMessageWrapper<T>, IMessage, new()
        where TField : unmanaged, Enum
    {
        static readonly int FieldSize = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TField)));

        readonly Func<T, TField> getter;
        readonly Action<T, TField> setter;

        public EnumField(PropertyInfo property)
        {
            getter = (Func<T, TField>) Delegate.CreateDelegate(typeof(Func<T, TField>), property.GetGetMethod()!);
            setter = (Action<T, TField>) Delegate.CreateDelegate(typeof(Action<T, TField>),
                property.GetSetMethod()!);
        }

        public void RosSerialize(T msg, ref Buffer b) => b.Serialize(getter(msg));
        public void RosDeserialize(T msg, ref Buffer b) => setter(msg, b.Deserialize<TField>());
        public int RosLength(T msg) => FieldSize;

        public void RosValidate(T msg)
        {
        }
    }


    internal sealed class StructField<T, TField> : IMessageField<T>
        where T : IMessage
        where TField : unmanaged
    {
        static readonly int FieldSize = Marshal.SizeOf<TField>();
        readonly Func<T, TField> getter;
        readonly Action<T, TField> setter;

        public StructField(PropertyInfo property)
        {
            getter = (Func<T, TField>) Delegate.CreateDelegate(typeof(Func<T, TField>), property.GetGetMethod()!);
            setter = (Action<T, TField>) Delegate.CreateDelegate(typeof(Action<T, TField>),
                property.GetSetMethod()!);
        }

        public void RosSerialize(T msg, ref Buffer b) => b.Serialize(getter(msg));
        public void RosDeserialize(T msg, ref Buffer b) => setter(msg, b.Deserialize<TField>());
        public int RosLength(T msg) => FieldSize;

        public void RosValidate(T msg)
        {
        }
    }
}