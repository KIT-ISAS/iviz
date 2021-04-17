using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsWrapper
{
    internal sealed partial class RosWrapperDefinition<T> where T : IMessage
    {
        public string RosMessageType { get; }
        public string RosDefinition { get; }
        public string RosMessageMd5 { get; }
        public string RosDependencies { get; }
        public string RosDependenciesBase64 => CompressDependencies(RosDependencies);

        readonly IMessageField<T>[] messageFields;

        public RosWrapperDefinition()
        {
            try
            {
                RosMessageType = BuiltIns.GetMessageType<T>();
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
                    || property.GetSetMethod() == null
                    || property.GetCustomAttribute<IgnoreDataMemberAttribute>() != null)
                {
                    continue;
                }

                Type propertyType = property.PropertyType;
                if (Initializers.TryGetValue(propertyType, out var initializer))
                {
                    initializer(property, bi);
                }
                else if (typeof(IMessage).IsAssignableFrom(propertyType))
                {
                    InitializeMessageProperty(property, bi);
                }
                else if (propertyType.IsArray &&
                         typeof(IMessage).IsAssignableFrom(propertyType.GetElementType()))
                {
                    InitializeMessageArrayProperty(property, bi);
                }
            }

            RosDefinition = bi.RosDefinition.ToString();

            messageFields = bi.Fields.ToArray();

            RosMessageMd5 = CreateMd5(bi);

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