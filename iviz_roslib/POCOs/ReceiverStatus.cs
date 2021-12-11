using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Roslib;

[JsonConverter(typeof(StringEnumConverter))]
public enum ReceiverStatus
{
    UnknownError,
    ConnectingRpc,
    OutOfRetries,
    Canceled,
    Connected,
    ConnectingTcp,
    Running,
    Dead,
}