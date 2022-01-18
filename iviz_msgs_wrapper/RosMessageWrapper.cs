using System.Runtime.Serialization;
using Iviz.Msgs;
using Newtonsoft.Json;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.MsgsWrapper
{
    /// <summary>
    /// Creates a ROS message out of a C# class.
    /// </summary>
    /// <typeparam name="T">The type around with to create the message.</typeparam>
    public abstract class RosMessageWrapper<T> : IMessage, IDeserializable<T>
        where T : RosMessageWrapper<T>, IMessage, new()
    {
        static RosSerializableDefinition<T>? definition;
        static RosSerializableDefinition<T> Definition => definition ??= new RosSerializableDefinition<T>();

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public static string RosMd5Sum => Definition.RosMessageMd5;

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public static string RosDependenciesBase64 => Definition.RosDependenciesBase64;

        /// <summary> Concatenated dependencies file. </summary>
        public static string RosDependencies => Definition.RosDependencies;

        public void RosSerialize(ref WriteBuffer b) => Definition.Serialize((T) this, ref b);

        [IgnoreDataMember] public int RosMessageLength => Definition.GetLength((T) this);

        public void RosValidate() => Definition.Validate((T) this);

        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => RosDeserialize(ref b);

        protected RosMessageWrapper()
        {
            if (typeof(T) != GetType())
            {
                throw new RosInvalidMessageException("Message types do not match");
            }
        }
        
        public T RosDeserialize(ref ReadBuffer b)
        {
            var msg = new T();
            Definition.Deserialize(msg, ref b);
            return msg;
        }

        /// <summary>
        /// Creates a ROS definition for this message (the contents of a .msg file).
        /// </summary>
        [IgnoreDataMember] public static string RosDefinition => Definition.RosDefinition;

        /// <summary>
        /// Alias for the name of the message.
        /// </summary>
        [IgnoreDataMember] public string RosType => Definition.RosType;

        public override string ToString()
        {
            return this.ToJsonString();
        }
    }
}