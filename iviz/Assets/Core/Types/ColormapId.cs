using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ColormapId
    {
        lines,
        pink,
        copper,
        bone,
        gray,
        winter,
        autumn,
        summer,
        spring,
        cool,
        hot,
        hsv,
        jet,
        parula
    }
}