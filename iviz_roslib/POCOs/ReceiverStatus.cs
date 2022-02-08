using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Roslib;

/// <summary>
/// Describes the state of a subscriber connection to a publisher.
/// Optimally the sequence is <see cref="ConnectingRpc"/> -> <see cref="ConnectingTcp"/> -> <see cref="Connected"/> -> <see cref="Running"/>.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ReceiverStatus
{
    /// <summary>
    /// Generic error. Check the contents of <see cref="SubscriberReceiverState.ErrorDescription"/>. 
    /// </summary>
    UnknownError,

    /// <summary>
    /// The publisher is being contacted to initiate an XML-RPC session.
    /// </summary>
    ConnectingRpc,

    /// <summary>
    /// The subscriber will not retry to contact the publisher anymore. 
    /// </summary>
    OutOfRetries,

    /// <summary>
    /// The subscriber was disposed. Likely the client unsubscribed.
    /// </summary>
    Canceled,

    /// <summary>
    /// Connection succeeded. The handshake is now being prepared.
    /// </summary>
    Connected,

    /// <summary>
    /// The publisher is being contacted to initiate a TCP session.
    /// </summary>
    ConnectingTcp,

    /// <summary>
    /// Connection succeeded and handshake finished. Data is being received.
    /// </summary>
    Running,

    /// <summary>
    /// The publisher closed the connection or an error happened.
    /// Check the contents of <see cref="SubscriberReceiverState.ErrorDescription"/>. 
    /// </summary>
    Dead,
}