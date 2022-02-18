using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Iviz.Rosbag.Reader;
using Iviz.Tools;

namespace Iviz.Rosbag
{
    internal static class Utils
    {
        public static T Read<T>(this Span<byte> intBytes) where T : unmanaged => 
            MemoryMarshal.Read<T>(intBytes);

        public static IEnumerable<T> SelectMessage<T>(this IEnumerable<MessageData> enumerable)
            where T : IMessage, IDeserializable<T>, new()
        {
            var generator = new T();
            return enumerable.Select(messageData => messageData.GetMessage(generator));
        }
    }
}