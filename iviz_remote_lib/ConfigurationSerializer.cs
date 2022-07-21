using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Newtonsoft.Json;

namespace Iviz.RemoteLib;

/// <summary>
/// Class that filters out null values from <see cref="IConfiguration"/> objects and converts the rest to JSON.
/// Pretty much the same as <see cref="JsonConvert.SerializeObject(object)"/> but without reflection and boxing.
/// The Serialize fields use the name of the property as the second argument.
/// </summary>
internal readonly struct ConfigurationSerializer : IDisposable
{
    static readonly Dictionary<ModuleType, string> ModuleNames =
        typeof(ModuleType).GetEnumValues()
            .Cast<ModuleType>()
            .Select(module => (key: module, value: module.ToString()))
            .ToDictionary(entry => entry.key, entry => entry.value);

    readonly JsonTextWriter writer;

    public ConfigurationSerializer(StringBuilder str)
    {
        writer = new JsonTextWriter(new StringWriter(str, BuiltIns.Culture)) { Formatting = Formatting.Indented };
        writer.WriteStartObject();
    }

    public void Dispose()
    {
        using (writer)
        {
            writer.WriteEndObject();
        }
    }

    public void Serialize(string? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field == null)
        {
            return;
        }

        ThrowIfNull(fieldName);
        writer.WritePropertyName(fieldName);
        writer.WriteValue(field);
        //fields.Add(fieldName!);
    }

    public void Serialize(bool? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }
        
        ThrowIfNull(fieldName);
        writer.WritePropertyName(fieldName);
        writer.WriteValue(value);
        //fields.Add(fieldName!);
    }

    public void Serialize(in ColorRGBA? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }

        ThrowIfNull(fieldName);
        writer.WritePropertyName(fieldName);
        writer.WriteValueExtended(value);
        //fields.Add(fieldName!);
    }

    public void Serialize(in Vector3? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }

        ThrowIfNull(fieldName);
        writer.WritePropertyName(fieldName);
        writer.WriteValueExtended(value);
        //fields.Add(fieldName!);
    }

    public void Serialize(double? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }

        ThrowIfNull(fieldName);
        writer.WritePropertyName(fieldName);
        writer.WriteValue(value);
        //fields.Add(fieldName!);
    }

    public void Serialize(ModuleType field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        ThrowIfNull(fieldName);
        writer.WritePropertyName(fieldName);
        writer.WriteValue(ModuleNames[field]);
        //fields.Add(fieldName!);
    }

    public void Serialize(bool[]? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }

        ThrowIfNull(fieldName);
        
        writer.WritePropertyName(fieldName);
        writer.WriteStartArray();
        foreach (bool b in value)
        {
            writer.WriteValue(b);
        }

        writer.WriteEndArray();
        //fields.Add(fieldName!);
    }

    static void ThrowIfNull([NotNull] string? fieldName)
    {
        if (fieldName == null) throw new ArgumentNullException(nameof(fieldName));
    }
}