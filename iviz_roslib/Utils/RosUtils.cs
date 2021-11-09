using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Tools;

namespace Iviz.Roslib.Utils
{
    internal static class RosUtils
    {
        public const string ProtocolTcpRosName = "TCPROS";
        public const string ProtocolUdpRosName = "UDPROS";

        internal static List<string> ParseHeader(in Rent<byte> readBuffer) =>
            ParseHeader(readBuffer.Array, readBuffer.Length);

        internal static List<string> ParseHeader(byte[] readBuffer) =>
            ParseHeader(readBuffer, readBuffer.Length);

        internal static List<string> ParseHeader(byte[] readBuffer, int toRead)
        {
            const int maxEntrySize = 1024 * 1024;
            int numRead = 0;

            List<string> contents = new();
            while (numRead < toRead)
            {
                int length = BitConverter.ToInt32(readBuffer, numRead);
                if (length is < 0 or > maxEntrySize)
                {
                    throw new RosInvalidHeaderException($"Invalid packet size {length}");
                }

                numRead += 4;

                if (numRead + length > toRead)
                {
                    throw new RosInvalidHeaderException(
                        $"Invalid header entry size {length}, buffer has only {toRead - numRead} bytes left.");
                }

                string entry;
                try
                {
                    entry = BuiltIns.UTF8.GetString(readBuffer, numRead, length);
                }
                catch (Exception e)
                {
                    throw new RosInvalidHeaderException("Error parsing header line.", e);
                }

                numRead += length;

                contents.Add(entry);
            }

            return contents;
        }

        internal static Dictionary<string, string> CreateHeaderDictionary(List<string> fields)
        {
            Dictionary<string, string> values = new();
            foreach (string entry in fields)
            {
                int index = entry.IndexOf('=');
                if (index < 0)
                {
                    throw new RosInvalidHeaderException($"Missing '=' separator in ROS header field '{entry}'.");
                }

                string key = entry.Substring(0, index);
                values[key] = entry.Substring(index + 1);
            }

            return values;
        }


        internal static TopicInfo<T> GenerateDynamicTopicInfo<T>(string callerId, string topicName,
            IReadOnlyCollection<string> responses) where T : IMessage
        {
            const string typePrefix = "type=";
            const string definitionPrefix = "message_definition=";

            string? dynamicMsgName = responses.FirstOrDefault(
                entry => entry.HasPrefix(typePrefix))?.Substring(typePrefix.Length);
            string? dynamicDependencies = responses.FirstOrDefault(
                entry => entry.HasPrefix(definitionPrefix))?.Substring(definitionPrefix.Length);
            if (dynamicMsgName == null || dynamicDependencies == null)
            {
                throw new RosHandshakeException(
                    "Partner did not send type and definition, required to instantiate dynamic messages.");
            }

            Type? lookupMsgName;
            object? lookupGenerator;
            if (DynamicMessage.IsGenericMessage<T>()
                && (lookupMsgName = BuiltIns.TryGetTypeFromMessageName(dynamicMsgName)) != null
                && (lookupGenerator = Activator.CreateInstance(lookupMsgName)) != null)
            {
                return new TopicInfo<T>(callerId, topicName, (IDeserializable<T>)lookupGenerator);
            }

            DynamicMessage generator =
                DynamicMessage.CreateFromDependencyString(dynamicMsgName, dynamicDependencies);
            return new TopicInfo<T>(callerId, topicName, generator);
        }

        static bool IsInSameSubnet(UnicastIPAddressInformation info, IPAddress addressB)
        {
            byte[] addressABytes = info.Address.GetAddressBytes();
            byte[] addressBBytes = addressB.GetAddressBytes();
            byte[] subnetMaskBytes = info.Address.AddressFamily == AddressFamily.InterNetwork
                ? info.IPv4Mask.GetAddressBytes()
                : GtSubnetMaskFromV6PrefixLength(info.PrefixLength);

            for (int i = 0; i < addressABytes.Length; i++)
            {
                if ((addressABytes[i] & subnetMaskBytes[i]) != (addressBBytes[i] & subnetMaskBytes[i]))
                {
                    return false;
                }
            }

            return true;
        }

        static byte[] GtSubnetMaskFromV6PrefixLength(int prefixLength)
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
            return ipInfo is null ? null : new Uri($"http://{ipInfo.Address}:{portStr}/");
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
                    .Select(info => info.Address.ToString());
            }
            catch (Exception)
            {
                return Array.Empty<string>();
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
            catch (Exception)
            {
                // this shouldn't throw at all!
                return null;
            }

            if (mtuCandidate is not { } mtu || mtu == 0)
            {
                return null; // mono is bad at finding the mtu
            }

            int headerSize =
                (remoteAddress.AddressFamily == AddressFamily.InterNetworkV6 && !remoteAddress.IsIPv4MappedToIPv6)
                    ? UdpRosParams.Ip6UdpHeadersLength
                    : UdpRosParams.Ip4UdpHeadersLength;
            return mtu - headerSize;
        }

        internal static int GetRecommendedBufferSize(int rcvLength, int defaultSize)
        {
            return rcvLength switch
            {
                < 64 * 1024 => defaultSize,
                < 128 * 1024 => 128 * 1024,
                < 1024 * 1024 => 1024 * 1024,
                _ => 4 * 1024 * 1024,
            };
        }
    }
}