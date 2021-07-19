using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Core
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ARMarkerType
    {
        Aruco,
        QrCode,
        Unset,
    }
}