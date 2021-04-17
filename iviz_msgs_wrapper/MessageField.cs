using System;
using System.Reflection;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed class MessageField<T, TField> : IMessageField<T> 
        where T : RosMessageWrapper<T>, new()    
        where TField : IMessage, IDeserializable<TField>, new()
    {
        static readonly IDeserializable<TField> Generator = new TField();
        static readonly bool IsValueType = typeof(T).IsValueType;
        static readonly int? FieldSize =
            BuiltIns.TryGetFixedSize(typeof(TField), out int realFieldSize) ? realFieldSize : null;

        readonly Func<T, TField> getter;
        readonly Action<T, TField> setter;
        readonly string propertyName;

        public MessageField(PropertyInfo property, string propertyName)
        {
            getter = (Func<T, TField>) Delegate.CreateDelegate(typeof(Func<T, TField>), property.GetGetMethod()!);
            setter = (Action<T, TField>) Delegate.CreateDelegate(typeof(Action<T, TField>),
                property.GetSetMethod()!);
            this.propertyName = propertyName;
        }

        public void RosSerialize(T msg, ref Buffer b) => getter(msg).RosSerialize(ref b);
        public void RosDeserialize(T msg, ref Buffer b) => setter(msg, Generator.RosDeserialize(ref b));
        public int RosLength(T msg) => FieldSize ?? getter(msg).RosMessageLength;

        public void RosValidate(T msg)
        {
            if (IsValueType)
            {
                return;
            }

            var value = getter(msg);
            if (value is null)
            {
                throw new NullReferenceException($"Field '{propertyName}' is null");
            }
            
            value.RosValidate();
        }
    }
}