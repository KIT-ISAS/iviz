using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.MsgsWrapper
{
    internal sealed partial class RosSerializableDefinition<T> where T : ISerializable, IDeserializable<T>, new()
    {
        public string RosType { get; }
        public string RosDefinition { get; }
        public string RosInputForMd5 { get; }
        public string RosMessageMd5 { get; }
        public string RosDependencies { get; }
        public string RosDependenciesBase64 => RosWrapperBase.CompressDependencies(RosDependencies);

        readonly IMessageField<T>[] messageFields;

        public RosSerializableDefinition()
        {
            try
            {
                RosType = typeof(IMessage).IsAssignableFrom(typeof(T)) 
                    ? BuiltIns.GetMessageType(typeof(T)) 
                    : "";
            }
            catch (RosInvalidMessageException)
            {
                throw new RosIncompleteWrapperException(
                    $"Type '{typeof(T).Name}' must have a string constant named RosMessageType. " +
                    "It should also be tagged with the attribute [MessageName].");
            }

            var bi = new BuilderInfo();

            foreach (var field in typeof(T).GetFields())
            {
                if (field.IsStatic && field.IsLiteral && !field.IsInitOnly
                    && RosWrapperBase.BuiltInNames.TryGetValue(field.FieldType, out string? fieldType)
                    && field.GetCustomAttribute<MessageNameAttribute>() == null)
                {
                    InitializeConstant(bi, field, fieldType);
                }
            }

            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetGetMethod() == null
                    || (property.GetType().GetGenericTypeDefinition() != typeof(List<>) && property.GetSetMethod() == null)
                    || property.GetCustomAttribute<IgnoreDataMemberAttribute>() != null)
                {
                    continue;
                }

                Type propertyType = property.PropertyType;
                if (Initializers.TryGetValue(propertyType, out var initializer))
                {
                    initializer(property, bi);
                }
                else if (propertyType.IsArray)
                {
                    var elementType = propertyType.GetElementType()!;
                    if (elementType.IsEnum)
                    {
                        InitializeEnumArrayProperty(property, bi);
                    }
                    else if (typeof(IMessage).IsAssignableFrom(elementType))
                    {
                        InitializeMessageArrayProperty(property, bi);
                    }
                }
                else if (propertyType.IsEnum)
                {
                    InitializeEnumProperty(property, bi);
                }
                else if (typeof(IMessage).IsAssignableFrom(propertyType))
                {
                    InitializeMessageProperty(property, bi);
                }
            }

            RosDefinition = bi.RosDefinition.ToString();

            messageFields = bi.Fields.ToArray();

            (RosInputForMd5, RosMessageMd5) = CreateMd5(bi);

            RosDependencies = CreateDependencies(RosDefinition);
        }

        public void Serialize(T msg, ref Buffer b)
        {
            foreach (var field in messageFields)
            {
                field.RosSerialize(msg, ref b);
            }
        }

        public void Deserialize(T msg, ref Buffer b)
        {
            foreach (var field in messageFields)
            {
                field.RosDeserialize(msg, ref b);
            }
        }

        public int GetLength(T msg)
        {
            int length = 0;
            foreach (var field in messageFields)
            {
                length += field.RosLength(msg);
            }

            return length;
        }

        public void Validate(T msg)
        {
            foreach (var field in messageFields)
            {
                field.RosValidate(msg);
            }
        }
    }
}