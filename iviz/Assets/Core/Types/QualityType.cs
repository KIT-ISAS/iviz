using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum QualityType
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh,
        Ultra,
        [Obsolete] Mega
    }
}