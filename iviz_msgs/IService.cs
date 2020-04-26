using System.Runtime.Serialization;

namespace Iviz.Msgs
{
    public interface IService
    {
        /// <summary>
        /// Create an empty service message
        /// </summary>
        /// <returns>New service message</returns>
        IService Create();

        /// <summary>
        /// Returns the request message
        /// </summary>
        /// <returns>Request message</returns>
        IRequest Request { get; }

        IResponse Response { get; }

        string ErrorMessage { get; set; }

        string RosType { get; }
    }

    public interface IRequest : ISerializable
    {
    }


    public interface IResponse : ISerializable
    {
    }

    namespace Internal
    {
        public class EmptyRequest : IRequest
        {
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }

            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }

            [IgnoreDataMember]
            public int RosMessageLength => 0;
        }

        public class EmptyResponse : IResponse
        {
            public unsafe void Deserialize(ref byte* ptr, byte* end)
            {
            }

            public unsafe void Serialize(ref byte* ptr, byte* end)
            {
            }

            [IgnoreDataMember]
            public int RosMessageLength => 0;
        }
    }
}