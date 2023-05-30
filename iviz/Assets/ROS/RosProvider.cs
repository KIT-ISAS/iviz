#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
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

    public abstract class RosProvider : Displays.ServiceProvider
    {
        public const bool IsRos2VersionSupported = RosConnection.HasRos2Implementation;

        public static bool OwnMasterEnabledByDefault => Settings.IsMacOS || Settings.IsMobile;
        
        // ROS1 stuff
        Uri? masterUri;
        string? myId;
        Uri? myUri;

        // ROS2 stuff
        int domainId;
        Endpoint? discoveryServer;

        protected IRosClient? client;
        protected IRosClient Client => client ?? throw new InvalidOperationException("Client not connected");
        
        public static event Action<ConnectionState>? ConnectionStateChanged;
        public static event Action<bool>? ConnectionWarningStateChanged;

        public Uri? MasterUri
        {
            get => masterUri;
            set
            {
                masterUri = value;
                Disconnect();
            }
        }

        public string? MyId
        {
            get => myId;
            set
            {
                myId = value;
                Disconnect();
            }
        }

        public Uri? MyUri
        {
            get => myUri;
            set
            {
                myUri = value;
                Disconnect();
            }
        }

        public Endpoint? DiscoveryServer
        {
            get => discoveryServer;
            set
            {
                discoveryServer = value;
                Disconnect();
            }
        }

        public int DomainId
        {
            get => domainId;
            set
            {
                domainId = value;
                Disconnect();
            }
        }

        public bool KeepReconnecting { get; set; }

        public abstract BagListener? BagListener { get; set; }
        public abstract RosVersion RosVersion { get; set; }

        public abstract void SetHostAliases(IEnumerable<(string hostname, string address)> newHostAliases);
        public abstract void TryOnceToConnect();
        public abstract TopicNameType[] GetSystemPublishedTopicTypes(bool withRefresh = true);
        public abstract TopicNameType[] GetSystemTopicTypes();
        public abstract string[] GetSystemParameterList(string? node = null);
        public abstract SystemState? GetSystemState(bool withRefresh = true);

        public abstract ValueTask<TopicNameType[]> GetSystemPublishedTopicTypesAsync(int timeoutInMs = 2000,
            CancellationToken token = default);

        public abstract ValueTask<(RosValue result, string? errorMsg)> GetParameterAsync(string parameter,
            string? nodeName = null, int timeoutInMs = 2000, CancellationToken token = default);

        public abstract void AdvertiseService<T>(string service, Func<T, ValueTask> callback)
            where T : class, IService, new();

        public abstract void Disconnect();

        internal static void RaiseConnectionStateChanged(ConnectionState c)
        {
            ConnectionStateChanged.TryRaise(c, nameof(RosProvider));
        }

        internal static void RaiseConnectionWarningStateChanged(bool c)
        {
            ConnectionWarningStateChanged.TryRaise(c, nameof(RosProvider));
        }

        internal static void ClearEvents()
        {
            ConnectionStateChanged = null;
            ConnectionWarningStateChanged = null;
        }
    }
}