using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ARMarkerAction
    {
        None,
        Publish,
        Origin
    }
}