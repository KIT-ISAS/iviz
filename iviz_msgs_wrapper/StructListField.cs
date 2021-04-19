using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class StructListField<T, TField> : IMessageField<T>
        where T : IDeserializable<T>, ISerializable   
        where TField : unmanaged
    {
        static readonly int FieldSize = Marshal.SizeOf<TField>();

        readonly Func<T, List<TField>> getter;
        readonly string propertyName;

        public StructListField(PropertyInfo property, string propertyName)
        {
            this.propertyName = propertyName;
            getter = (Func<T, List<TField>>) Delegate.CreateDelegate(typeof(Func<T, List<TField>>),
                property.GetGetMethod()!);
        }

        public void RosSerialize(T msg, ref Buffer b) => b.SerializeStructList(getter(msg));

        public void RosDeserialize(T msg, ref Buffer b) => b.DeserializeStructList(getter(msg));

        public int RosLength(T msg) => 4 + FieldSize * getter(msg).Count;

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