#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Ros
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RosVersion
    {
        ROS1,
        ROS2
    }

    public interface IRosProvider : Displays.IServiceProvider
    {
        const bool IsRos2VersionSupported = RosConnection.IsRos2VersionSupported;

        static event Action<ConnectionState>? ConnectionStateChanged;

        static event Action<bool>? ConnectionWarningStateChanged;

        IRosClient Client { get; }
        
        string? MyId { get; set; }
        
        Uri? MasterUri { get; set; }
        
        Uri? MyUri { get; set; }
        
        bool KeepReconnecting { set; }
        
        BagListener? BagListener { get; set; }
        
        RosVersion RosVersion { get; set; }
        
        int DomainId { set; }
        
        Endpoint? DiscoveryServer { set; }

        void SetHostAliases(IEnumerable<(string hostname, string address)> newHostAliases);

        void TryOnceToConnect();

        TopicNameType[] GetSystemPublishedTopicTypes(bool withRefresh = true);

        TopicNameType[] GetSystemTopicTypes();

        string[] GetSystemParameterList(string? node = null);

        SystemState? GetSystemState(bool withRefresh = true);

        ValueTask<TopicNameType[]> GetSystemPublishedTopicTypesAsync(int timeoutInMs = 2000,
            CancellationToken token = default);

        ValueTask<(RosValue result, string? errorMsg)> GetParameterAsync(string parameter,
            string? nodeName = null, int timeoutInMs = 2000, CancellationToken token = default);

        void AdvertiseService<T>(string service, Func<T, ValueTask> callback) where T : class, IService, new();

        void Disconnect();

        internal static void RaiseConnectionStateChanged(ConnectionState c) => ConnectionStateChanged?.Invoke(c);

        internal static void RaiseConnectionWarningStateChanged(bool c) => ConnectionWarningStateChanged?.Invoke(c);

        internal static void ClearEvents()
        {
            ConnectionStateChanged = null;
            ConnectionWarningStateChanged = null;
        }
    }
}