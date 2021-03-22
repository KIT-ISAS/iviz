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

        internal Endpoint(string hostname, int port) => (Hostname, Port) = (hostname, port);

        internal Endpoint(IPEndPoint endPoint) => (Hostname, Port) = (endPoint.Address.ToString(), endPoint.Port);

        public bool Equals(Endpoint? other) =>
            other != null && Hostname == other.Value.Hostname && Port == other.Value.Port;

        public bool Equals(Endpoint other) => (other.Hostname, other.Port) == (Hostname, Port);

        public void Deconstruct(out string hostname, out int port) => (hostname, port) = (Hostname, Port);

        public override string ToString() => $"[Endpoint '{Hostname}':{Port}]";
    }
}