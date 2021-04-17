using System;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Buffer = Iviz.Msgs.Buffer;
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
        static RosWrapperDefinition<T>? msgDefinition;
        static RosWrapperDefinition<T> MsgDefinition => msgDefinition ??= new RosWrapperDefinition<T>();

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public static string RosMd5Sum => MsgDefinition.RosMessageMd5;

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public static string RosDependenciesBase64 => MsgDefinition.RosDependenciesBase64;

        /// <summary> Concatenated dependencies file. </summary>
        public static string RosDependencies => MsgDefinition.RosDependencies;

        public void RosSerialize(ref Buffer b) => MsgDefinition.Serialize((T) this, ref b);

        [IgnoreDataMember] public int RosMessageLength => MsgDefinition.GetLength((T) this);

        public void RosValidate() => MsgDefinition.Validate((T) this);

        ISerializable ISerializable.RosDeserialize(ref Buffer b) => RosDeserialize(ref b);

        protected RosMessageWrapper()
        {
            if (typeof(T) != GetType())
            {
                
            }
        }
        
        public T RosDeserialize(ref Buffer b)
        {
            var msg = new T();
            MsgDefinition.Deserialize(msg, ref b);
            return msg;
        }

        /// <summary>
        /// Creates a ROS definition for this message (the contents of a .msg file).
        /// </summary>
        [IgnoreDataMember] public static string RosDefinition => MsgDefinition.RosDefinition;

        /// <summary>
        /// Alias for the name of the message.
        /// </summary>
        [IgnoreDataMember] public string RosType => MsgDefinition.RosMessageType;
    }
}