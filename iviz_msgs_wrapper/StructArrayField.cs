using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class StructArrayField<T, TField> : IMessageField<T>
        where T : RosMessageWrapper<T>, IMessage, new()
        where TField : unmanaged
    {
        static readonly int FieldSize = Marshal.SizeOf<TField>();

        readonly Func<T, TField[]> getter;
        readonly Action<T, TField[]> setter;
        readonly string propertyName;

        public StructArrayField(PropertyInfo property, string propertyName)
        {
            this.propertyName = propertyName;
            getter = (Func<T, TField[]>) Delegate.CreateDelegate(typeof(Func<T, TField[]>),
                property.GetGetMethod()!);
            setter = (Action<T, TField[]>) Delegate.CreateDelegate(typeof(Action<T, TField[]>),
                property.GetSetMethod()!);
        }

        public void RosSerialize(T msg, ref Buffer b) => b.SerializeStructArray(getter(msg));
        public void RosDeserialize(T msg, ref Buffer b) => setter(msg, b.DeserializeStructArray<TField>());
        public int RosLength(T msg) => 4 + FieldSize * getter(msg).Length;

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