using System;
using System.Net;
using System.Runtime.Serialization;

namespace Iviz.Roslib;

/// <summary>
///     Simple class containing endpoint data for an IP connection.
/// </summary>
[DataContract]
public readonly struct Endpoint : IEquatable<Endpoint>
{
    [DataMember] public string Hostname { get; }
    [DataMember] public int Port { get; }

    public Endpoint(string hostname, int port) =>
        (Hostname, Port) = (hostname ?? throw new ArgumentNullException(nameof(hostname)), port);

    public Endpoint(IPEndPoint endPoint) => (Hostname, Port) = (endPoint.Address.ToString(), endPoint.Port);

    public bool Equals(Endpoint other) => (other.Hostname, other.Port) == (Hostname, Port);
    
    public static bool operator ==(in Endpoint a, in Endpoint b) => a.Equals(b);
    
    public static bool operator !=(in Endpoint a, in Endpoint b) => !a.Equals(b);

    public override bool Equals(object? obj) => obj is Endpoint other && Equals(other);
    
    public override int GetHashCode() => (Hostname, Port).GetHashCode();

    public void Deconstruct(out string hostname, out int port) => (hostname, port) = (Hostname, Port);

    public override string ToString() => $"[{nameof(Endpoint)} '{Hostname}':{Port.ToString()}]";
}