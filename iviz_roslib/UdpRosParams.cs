namespace Iviz.Roslib;

internal static class UdpRosParams
{
    public const int HeaderLength = 8;

    public const int DefaultMTU = 1500;
    public const int Ip4UdpHeadersLength = 20 /* IPv4*/ + 8 /* UDP */;
    public const int Ip6UdpHeadersLength = 40 /* IPv6*/ + 8 /* UDP */;

    public const byte OpCodeData0 = 0;
    public const byte OpCodeDataN = 1;
    public const byte OpCodePing = 2;
    public const byte OpCodeErr = 3;
}