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
/// </summary>
internal readonly struct ConfigurationSerializer : IDisposable
{
    static readonly Dictionary<ModuleType, string> ModuleNames =
        typeof(ModuleType).GetEnumValues()
            .Cast<ModuleType>()
            .Select(module => (key: module, value: module.ToString()))
            .ToDictionary(entry => entry.key, entry => entry.value);

    readonly JsonTextWriter writer;
    readonly List<string> fields;

    public ConfigurationSerializer(StringBuilder str, List<string> fields)
    {
        this.fields = fields;
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

        writer.WritePropertyName(fieldName!);
        writer.WriteValue(field);
        fields.Add(fieldName!);
    }

    public void Serialize(bool? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }

        writer.WritePropertyName(fieldName!);
        writer.WriteValue(value);
        fields.Add(fieldName!);
    }

    public void Serialize(in ColorRGBA? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }

        writer.WritePropertyName(fieldName!);
        writer.WriteValueExtended(value);
        fields.Add(fieldName!);
    }

    public void Serialize(in Vector3? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }

        writer.WritePropertyName(fieldName!);
        writer.WriteValueExtended(value);
        fields.Add(fieldName!);
    }

    public void Serialize(double? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }

        writer.WritePropertyName(fieldName!);
        writer.WriteValue(value);
        fields.Add(fieldName!);
    }

    public void Serialize(ModuleType field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        writer.WritePropertyName(fieldName!);
        writer.WriteValue(ModuleNames[field]);
        fields.Add(fieldName!);
    }

    public void Serialize(bool[]? field, [CallerArgumentExpression("field")] string? fieldName = null)
    {
        if (field is not { } value)
        {
            return;
        }

        writer.WritePropertyName(fieldName!);
        writer.WriteStartArray();
        foreach (bool b in value)
        {
            writer.WriteValue(b);
        }

        writer.WriteEndArrayAsync();
        fields.Add(fieldName!);
    }
}