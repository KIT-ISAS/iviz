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
    public static class RosUtils
    {
        public const string ProtocolTcpRosName = "TCPROS";
        public const string ProtocolUdpRosName = "UDPROS";

        static readonly Func<(byte b1, byte b2), byte> And = b => (byte)(b.b1 & b.b2);

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

        public static Dictionary<string, string> CreateHeaderDictionary(List<string> fields)
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

        public static bool IsInSameSubnet(IPAddress addressA, IPAddress addressB, IPAddress subnetMask)
        {
            byte[] addressABytes = addressA.GetAddressBytes();
            byte[] addressBBytes = addressB.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();
            var broadcastABytes = addressABytes.Zip(subnetMaskBytes).Select(And);
            var broadcastBBytes = addressBBytes.Zip(subnetMaskBytes).Select(And);
            return broadcastABytes.SequenceEqual(broadcastBBytes);
        }

        public static IEnumerable<UnicastIPAddressInformation> GetInterfaceCandidates(NetworkInterfaceType type)
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(@interface => @interface.NetworkInterfaceType == type &&
                                     @interface.OperationalStatus == OperationalStatus.Up)
                .SelectMany(@interface => @interface.GetIPProperties().UnicastAddresses)
                .Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
        }

        public static Uri? GetUriFromInterface(NetworkInterfaceType type, int usingPort)
        {
            UnicastIPAddressInformation? ipInfo = GetInterfaceCandidates(type).FirstOrDefault();
            return ipInfo is null ? null : new Uri($"http://{ipInfo.Address}:{usingPort.ToString()}/");
        }
    }
}