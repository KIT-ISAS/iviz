using System.Net;
using System.Runtime.Serialization;

namespace Iviz.Roslib
{
    /// <summary>
    ///     Simple class containing endpoint data for an IP connection.
    /// </summary>
    [DataContract]
    public sealed class Endpoint
    {
        [DataMember] public string Hostname { get; }
        [DataMember] public int Port { get; }

        internal Endpoint(string Hostname, int Port)
        {
            this.Hostname = Hostname;
            this.Port = Port;
        }

        internal Endpoint(IPEndPoint endPoint) : this(endPoint.Address.ToString(), endPoint.Port)
        {
        }
        
        public bool Equals(Endpoint? other)
        {
            return other != null && Hostname == other.Hostname && Port == other.Port;
        }

        public override string ToString()
        {
            return $"[Endpoint '{Hostname}':{Port}]";
        }
    }
}