using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Tools;

namespace Iviz.Roslib.Utils;

internal static class RosUtils
{
    public const string ProtocolTcpRosName = "TCPROS";
    public const string ProtocolUdpRosName = "UDPROS";

    internal static List<string> ParseHeader(in Rent readBuffer) =>
        ParseHeader(readBuffer, readBuffer.Length);

    internal static List<string> ParseHeader(byte[] readBuffer) =>
        ParseHeader(readBuffer, readBuffer.Length);

    internal static List<string> ParseHeader(Span<byte> readBuffer, int toRead)
    {
        const int maxEntrySize = 1024 * 1024;
        int numRead = 0;

        List<string> contents = new();
        while (numRead < toRead)
        {
            int length = readBuffer[numRead..].ReadInt();
            if (length is < 0 or > maxEntrySize)
            {
                throw new RosInvalidHeaderException($"Invalid packet size {length}");
            }

            numRead += 4;

            if (numRead + length > toRead)
            {
                throw new RosInvalidHeaderException(
                    $"Invalid header entry size {length}. Buffer has only {toRead - numRead} bytes left");
            }

            string entry;
            try
            {
                entry = BuiltIns.UTF8.GetString(readBuffer.Slice(numRead, length));
            }
            catch (Exception e)
            {
                throw new RosInvalidHeaderException("Error parsing header line", e);
            }

            numRead += length;

            contents.Add(entry);
        }

        return contents;
    }

    internal static Dictionary<string, string> CreateHeaderDictionary(List<string> fields)
    {
        var values = new Dictionary<string, string>();
        foreach (string entry in fields)
        {
            int index = entry.IndexOf('=');
            switch (index)
            {
                case < 0:
                    throw new RosInvalidHeaderException($"Missing '=' separator in ROS header field '{entry}'.");
                case 0:
                    continue;
                default:
                    string key = entry[..index];
                    values[key] = entry[(index + 1)..];
                    break;
            }
        }

        return values;
    }


    internal static TopicInfo GenerateDynamicTopicInfo(string callerId, string topicName, string[] responses)
    {
        const string typePrefix = "type=";
        const string definitionPrefix = "message_definition=";

        string? dynamicMsgName = responses
            .FirstOrDefault(entry => entry.HasPrefix(typePrefix))
            ?[typePrefix.Length..];
        if (string.IsNullOrEmpty(dynamicMsgName))
        {
            throw new RosHandshakeException(
                "Partner did not send the message type in the header. " +
                "This information is required to instantiate dynamic messages.");
        }

        const bool allowDirectLookup = false;
        if (allowDirectLookup && BuiltIns.TryGetGeneratorFromMessageName(dynamicMsgName) is { } lookupGenerator)
        {
            return new TopicInfo(callerId, topicName, lookupGenerator);
        }

        string? dynamicDependencies = responses
            .FirstOrDefault(entry => entry.HasPrefix(definitionPrefix))
            ?[definitionPrefix.Length..];

        if (string.IsNullOrEmpty(dynamicDependencies))
        {
            throw new RosHandshakeException(
                "Partner did not send the message definition in the header. " +
                "This information is required to instantiate dynamic messages.");
        }

        // T == DynamicMessage
        var generator = DynamicMessage.CreateFromDependencyString(dynamicMsgName, dynamicDependencies, false);
        return new TopicInfo(callerId, topicName, generator);
    }

    static bool IsInSameSubnet(UnicastIPAddressInformation info, IPAddress addressB)
    {
        byte[] addressABytes = info.Address.GetAddressBytes();
        byte[] addressBBytes = addressB.GetAddressBytes();
        byte[] subnetMaskBytes = info.Address.AddressFamily == AddressFamily.InterNetwork
            ? info.IPv4Mask.GetAddressBytes()
            : GetSubnetMaskFromV6PrefixLength(info.PrefixLength);

        int length = addressABytes.Length;
        for (int i = 0; i < length; i++)
        {
            if ((addressABytes[i] & subnetMaskBytes[i]) != (addressBBytes[i] & subnetMaskBytes[i]))
            {
                return false;
            }
        }

        return true;
    }

    static byte[] GetSubnetMaskFromV6PrefixLength(int prefixLength)
    {
        int prefixLeft = prefixLength;
        byte[] maskBytes = new byte[16];
        for (int i = 0; i < 16; i++)
        {
            if (prefixLeft >= 8)
            {
                maskBytes[i] = 0xff;
                prefixLeft -= 8;
            }
            else if (prefixLeft != 0)
            {
                int mask = ~((1 << (8 - prefixLeft)) - 1);
                maskBytes[i] = (byte)(mask & 0xff);
                break;
            }
        }

        return maskBytes;
    }

    static IEnumerable<UnicastIPAddressInformation> GetInterfaceCandidates(NetworkInterfaceType type)
    {
        return NetworkInterface.GetAllNetworkInterfaces()
            .Where(@interface => @interface.NetworkInterfaceType == type &&
                                 @interface.OperationalStatus == OperationalStatus.Up)
            .SelectMany(@interface => @interface.GetIPProperties().UnicastAddresses)
            .Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
    }

    internal static Uri? GetUriFromInterface(NetworkInterfaceType type, string portStr)
    {
        var ipInfo = GetInterfaceCandidates(type).FirstOrDefault();

        return ipInfo == null ? null : new Uri($"http://{ipInfo.Address.ToUriString()}:{portStr}/");
    }

    internal static IPAddress? TryGetAddress(string hostname)
    {
        string resolvedHostname = ConnectionUtils.GlobalResolver.TryGetValue(hostname, out string? newHostname)
            ? newHostname
            : hostname;

        if (IPAddress.TryParse(resolvedHostname, out IPAddress? parsedAddress))
        {
            return parsedAddress;
        }

        IPAddress[] addressList;
        try
        {
            addressList = Dns.GetHostEntry(resolvedHostname).AddressList;
        }
        catch
        {
            return null;
        }

        return addressList.FirstOrDefault(address =>
            address.AddressFamily is AddressFamily.InterNetwork or AddressFamily.InterNetworkV6);
    }

    internal static IPAddress? TryGetAccessibleAddress(IPAddress masterAddress)
    {
        try
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(@interface => @interface.OperationalStatus == OperationalStatus.Up)
                .SelectMany(@interface => @interface.GetIPProperties().UnicastAddresses)
                .FirstOrDefault(info => CanAccessAddress(info, masterAddress))
                ?.Address;
        }
        catch
        {
            return null;
        }
    }

    static NetworkInterface? TryGetAccessibleInterface(IPAddress masterAddress)
    {
        try
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(@interface => @interface.OperationalStatus == OperationalStatus.Up)
                .Select(@interface => (@interface, infos: @interface.GetIPProperties().UnicastAddresses))
                .FirstOrDefault(t => t.infos.Any(info => CanAccessAddress(info, masterAddress)))
                .@interface;
        }
        catch
        {
            return null;
        }
    }

    static bool CanAccessAddress(UnicastIPAddressInformation info, IPAddress address)
    {
        if (info.Address.AddressFamily == AddressFamily.InterNetwork && address.IsIPv4MappedToIPv6)
        {
            return IsInSameSubnet(info, address.MapToIPv4());
        }

        return info.Address.AddressFamily == address.AddressFamily && IsInSameSubnet(info, address);
    }

    internal static IEnumerable<string> GetAllLocalAddresses()
    {
        try
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(@interface => @interface.OperationalStatus == OperationalStatus.Up)
                .SelectMany(@interface => @interface.GetIPProperties().UnicastAddresses)
                .Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(info => info.Address.ToUriString());
        }
        catch
        {
            return Enumerable.Empty<string>();
        }
    }


    internal static int? TryGetMaxPacketSizeForAddress(string address)
    {
        var remoteAddress = TryGetAddress(address);
        if (remoteAddress == null)
        {
            return null;
        }

        var @interface = TryGetAccessibleInterface(remoteAddress);

        int? mtuCandidate;

        try
        {
            mtuCandidate =
                @interface?.GetIPProperties()?.GetIPv4Properties()?.Mtu; // if v6 is active it will return same mtu
        }
        catch
        {
            // this shouldn't throw at all! yet it does
            return null;
        }

        if (mtuCandidate is not { } mtu || mtu is <= 0 or > ushort.MaxValue)
        {
            return null; // mono is bad at finding the mtu
        }

        int headerSize =
            remoteAddress is { AddressFamily: AddressFamily.InterNetworkV6, IsIPv4MappedToIPv6: false }
                ? UdpRosParams.Ip6UdpHeadersLength
                : UdpRosParams.Ip4UdpHeadersLength;
        return mtu - headerSize;
    }

    internal static int GetRecommendedBufferSize(int rcvLength, int defaultSize)
    {
        return rcvLength switch
        {
            < 8 * 1024 => defaultSize,
            < 32 * 1024 => 32 * 1024,
            < 128 * 1024 => 128 * 1024,
            < 1024 * 1024 => 1024 * 1024,
            _ => 4 * 1024 * 1024,
        };
    }

    public static IRequest DeserializeFrom(this IRequest generator, ReadOnlySpan<byte> bytes)
    {
        return ReadBuffer.Deserialize((IDeserializable<IRequest>)generator, bytes);
    }

    public static IResponse DeserializeFrom(this IResponse generator, ReadOnlySpan<byte> bytes)
    {
        return ReadBuffer.Deserialize((IDeserializable<IResponse>)generator, bytes);
    }

    public static string ToUriString(this IPAddress address)
    {
        return address.AddressFamily == AddressFamily.InterNetworkV6
            ? "[" + address.ToString() + "]"
            : address.ToString();
    }
}

public static class RosEventHandler
{
    /// <summary>
    /// Waits until Ctrl+C is pressed. 
    /// </summary>
    public static Task WaitForCancelAsync()
    {
        var tc = new TaskCompletionSource(TaskCreationOptions.None);
        Console.CancelKeyPress += (_, _) => tc.TrySetResult();
        return tc.Task;
    }

    /// <summary>
    /// Waits until Ctrl+C is pressed. 
    /// </summary>
    public static void WaitUntilCancel()
    {
        WaitForCancelAsync().Wait();
    }
}