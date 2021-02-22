using System;
using System.Net;
using System.Runtime.Serialization;

namespace Iviz.Roslib
{
    /// <summary>
    ///     Simple class containing endpoint data for an IP connection.
    /// </summary>
    [DataContract]
    public readonly struct Endpoint : IEquatable<Endpoint>
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
            return other != null && Hostname == other.Value.Hostname && Port == other.Value.Port;
        }

        public bool Equals(Endpoint other)
        {
            var (hostname, port) = other;
            return Hostname == hostname && Port == port;
        }


        public void Deconstruct(out string hostname, out int port) => (hostname, port) = (Hostname, Port);

        public override string ToString()
        {
            return $"[Endpoint '{Hostname}':{Port}]";
        }
    }
}