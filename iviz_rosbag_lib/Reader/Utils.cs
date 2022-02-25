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
        /// <summary>
        /// LINQ-like function to retrieve the internal message of the <see cref="MessageData"/>. 
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        public static IEnumerable<T> SelectMessage<T>(this IEnumerable<MessageData> enumerable)
            where T : IMessage, IDeserializable<T>, new()
        {
            var generator = new T();
            return enumerable.Select(messageData => messageData.GetMessage(generator));
        }
    }
}