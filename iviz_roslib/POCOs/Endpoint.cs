using System.Net;
using System.Runtime.Serialization;

namespace Iviz.Roslib
{
    /// <summary>
    /// Simple class containing endpoint data for an IP connection.
    /// </summary>
    [DataContract]
    public sealed class Endpoint : JsonToString
    {
        internal Endpoint(string Hostname, int Port)
        {
            this.Hostname = Hostname;
            this.Port = Port;
        }

        internal Endpoint(IPEndPoint endPoint) : this(endPoint.Address.ToString(), endPoint.Port)
        {
        }

        [DataMember] public string Hostname { get; }
        [DataMember] public int Port { get; }
    }
}