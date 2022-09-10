using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Rosbag.Reader;

namespace Iviz.Rosbag;

public static class LinqUtils
{
    /// <summary>
    /// LINQ-like function to retrieve the internal message of the <see cref="MessageData"/>. 
    /// </summary>
    /// <typeparam name="T">The message type</typeparam>
    public static IEnumerable<T> SelectMessage<T>(this IEnumerable<MessageData> enumerable)
        where T : IMessage, new()
    {
        var deserializer = new T().CreateDeserializer();
        return enumerable.Select(messageData => messageData.GetMessage(deserializer));
    }

    /// <summary>
    /// LINQ-like function to retrieve the internal message of the <see cref="MessageData"/>, but for dynamic messages. 
    /// </summary>
    public static IEnumerable<IMessage> SelectAnyMessage(this IEnumerable<MessageData> enumerable)
    {
        using var enumerator = enumerable.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            yield break;
        }

        var messageData = enumerator.Current;
        if (messageData is { Type: null } or { MessageDefinition: null })
        {
            throw new InvalidOperationException($"Failed to create message of type '{messageData.Type}'");
        }

        var deserializer = 
            DynamicMessage.CreateFromDependencyString(messageData.Type, messageData.MessageDefinition)
            .CreateDeserializer();

        yield return messageData.GetMessage(deserializer);

        while (enumerator.MoveNext())
        {
            yield return enumerator.Current.GetMessage(deserializer);
        }
    }
}