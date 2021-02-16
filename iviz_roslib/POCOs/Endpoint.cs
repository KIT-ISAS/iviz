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

        internal Endpoint(string hostname, int port)
        {
            Hostname = hostname;
            Port = port;
        }
        
        internal Endpoint(IPEndPoint endPoint)
        {
            Hostname = endPoint.Address.ToString();
            Port = endPoint.Port;
        }
        
        public bool Equals(Endpoint? other)
        {
            return other != null && Hostname == other.Hostname && Port == other.Port;
        }

        public void Deconstruct(out string hostname, out int port) => (hostname, port) = (Hostname, Port);

        public override string ToString()
        {
            return $"[Endpoint '{Hostname}':{Port}]";
        }
    }
}