using System;
using Iviz.Tools;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Iviz.Bridge.Client;

public static class CustomJsonFormatters
{
    public static void Initialize()
    {
        try
        {
            CompositeResolver.RegisterAndSetAsDefault(
                new IJsonFormatter[] { new SharedRentFormatter() },
                new[] { StandardResolver.Default });
        }
        catch (InvalidOperationException)
        {
            // ignore, we already called it before
        }
    }

    public sealed class SharedRentFormatter : IJsonFormatter<SharedRent>, IObjectPropertyNameFormatter<SharedRent>
    {
        public void Serialize(ref JsonWriter writer, SharedRent value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteString("");
        }

        public SharedRent Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            reader.ReadString();
            return SharedRent.Empty;
        }

        public void SerializeToPropertyName(ref JsonWriter writer, SharedRent value,
            IJsonFormatterResolver formatterResolver)
        {
            writer.WriteString("");
        }

        public SharedRent DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            reader.ReadString();
            return SharedRent.Empty;
        }
    }
}