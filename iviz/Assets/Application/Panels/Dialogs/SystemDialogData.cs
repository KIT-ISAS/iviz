#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Ros;
using Iviz.Tools;
using Iviz.XmlRpc;
using Newtonsoft.Json;

namespace Iviz.App
{
    public sealed class SystemDialogData : DialogData
    {
        const string EmptyTopText = "<b>State:</b> Disconnected. Nothing to show!";
        const string EmptyBottomText = "(Click on an item for more information)";

        static readonly XmlRpcValue.JsonConverter JsonConverter = new();

        readonly SystemDialogPanel panel;
        public override IDialogPanel Panel => panel;

        readonly SortedSet<string> hostsBuffer = new();

        uint? descriptionHash;

        CancellationTokenSource? tokenSource;

        string? nodeAddress;
        string? providerAddress;
        XmlRpcValue paramValue;

        public HostAlias?[] HostAliases { get; set; } = Array.Empty<HostAlias?>();

        public SystemDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<SystemDialogPanel>(DialogPanelType.System);
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();

            panel.Close.Clicked += Close;
            panel.LinkClicked += LinkClicked;
            panel.HostnameEndEdit += (i, _) => UpdateAliasesLink(i);
            panel.AddressEndEdit += (i, _) => UpdateAliasesLink(i);
            panel.ModeChanged += _ =>
            {
                descriptionHash = null;

                panel.TextBottom.text = EmptyBottomText;
                if (panel.Mode == SystemDialogPanel.ModeType.Aliases)
                {
                    SetupAliases();
                }
                else
                {
                    UpdatePanel();
                }
            };
            UpdatePanel();
        }

        public override void UpdatePanel()
        {
            switch (panel.Mode)
            {
                case SystemDialogPanel.ModeType.Topics:
                    UpdateTopics();
                    break;
                case SystemDialogPanel.ModeType.Services:
                    UpdateServices();
                    break;
                case SystemDialogPanel.ModeType.Params:
                    UpdateParameters();
                    break;
                case SystemDialogPanel.ModeType.Nodes:
                    UpdateNodes();
                    break;
                case SystemDialogPanel.ModeType.Aliases:
                    UpdateAliases();
                    break;
            }
        }

        void UpdateTopics()
        {
            var systemState = ConnectionManager.Connection.GetSystemState();
            if (systemState == null)
            {
                panel.TextTop.text = EmptyTopText;
                panel.TextBottom.text = EmptyBottomText;
                return;
            }

            var topicTypes = ConnectionManager.Connection.GetSystemTopicTypes()
                .ToDictionary(info => info.Topic, info => info.Type);

            var topicHasSubscribers = new SortedDictionary<string, bool>();
            foreach (var tuple in systemState.Publishers)
            {
                topicHasSubscribers[tuple.Topic] = true;
            }

            foreach ((string topic, _) in systemState.Subscribers)
            {
                if (!topicHasSubscribers.ContainsKey(topic))
                {
                    topicHasSubscribers[topic] = false;
                }
            }

            using var description = BuilderPool.Rent();

            foreach (var (key, value) in topicHasSubscribers)
            {
                if (value)
                {
                    description.Append("<b><u><link=").Append(key).Append(">").Append(key)
                        .Append("</link></u></b>\n      [");
                }
                else
                {
                    description.Append("<color=grey><b><u><link=").Append(key).Append(">").Append(key)
                        .Append("</link></u></b></color>\n      [");
                }

                description.Append(topicTypes.TryGetValue(key, out string type) ? type : "unknown")
                    .Append("]").AppendLine();
            }

            UpdateTop(description);
        }

        void UpdateServices()
        {
            var systemState = ConnectionManager.Connection.GetSystemState();
            if (systemState == null)
            {
                panel.TextTop.text = EmptyTopText;
                panel.TextBottom.text = EmptyBottomText;
                return;
            }

            var services = new SortedSet<string>(systemState.Services.Select(service => service.Topic));

            using var description = BuilderPool.Rent();

            foreach (string service in services)
            {
                description.Append("<b><u><link=")
                    .Append(service).Append(">").Append(service)
                    .Append("</link></u></b>").AppendLine();
            }

            UpdateTop(description);
        }

        void UpdateParameters()
        {
            if (!ConnectionManager.IsConnected)
            {
                panel.TextTop.text = EmptyTopText;
                panel.TextBottom.text = EmptyBottomText;
                return;
            }

            var parameters = new SortedSet<string>(ConnectionManager.Connection.GetSystemParameterList());

            using var description = BuilderPool.Rent();

            foreach (string parameter in parameters)
            {
                description.Append("<b><u><link=").Append(parameter).Append(">").Append(parameter)
                    .Append("</link></u></b>").AppendLine();
            }

            UpdateTop(description);
        }

        void UpdateNodes()
        {
            var systemState = ConnectionManager.Connection.GetSystemState();
            if (systemState == null)
            {
                panel.TextTop.text = EmptyTopText;
                panel.TextBottom.text = EmptyBottomText;
                return;
            }

            var nodesEnumerator =
                systemState.Publishers.SelectMany(tuple => tuple.Members).Concat(
                    systemState.Subscribers.SelectMany(tuple => tuple.Members).Concat(
                        systemState.Services.SelectMany(tuple => tuple.Members)
                    ));
            var nodes = new SortedSet<string>(nodesEnumerator);

            using var description = BuilderPool.Rent();

            foreach (string node in nodes)
            {
                description.Append("<b><u><link=").Append(node).Append(">")
                    .Append(node).Append("</link></u></b>")
                    .AppendLine();
            }

            UpdateTop(description);
        }

        void UpdateTop(BuilderPool.BuilderRent description)
        {
            uint newHash = Crc32Calculator.Compute(description);
            if (descriptionHash == newHash)
            {
                return;
            }

            descriptionHash = newHash;
            panel.TextTop.SetText(description);
        }

        void LinkClicked(string link)
        {
            switch (panel.Mode)
            {
                case SystemDialogPanel.ModeType.Topics:
                    UpdateTopicsLink(link);
                    break;
                case SystemDialogPanel.ModeType.Services:
                    providerAddress = null;
                    UpdateServicesLink(link);
                    break;
                case SystemDialogPanel.ModeType.Params:
                    paramValue = default;
                    UpdateParametersLink(link);
                    break;
                case SystemDialogPanel.ModeType.Nodes:
                    nodeAddress = null;
                    UpdateNodesLink(link);
                    break;
            }
        }

        void UpdateTopicsLink(string link)
        {
            using var description = BuilderPool.Rent();

            var systemState = ConnectionManager.Connection.GetSystemState(RequestType.CachedOnly);
            if (systemState == null)
            {
                panel.TextBottom.text = EmptyBottomText;
                return;
            }

            description.Append("<b>").Append(link).Append("</b>").AppendLine();

            description.Append("<color=#800000ff><b>Publishers:</b></color>").AppendLine();
            var publishers = systemState.Publishers.FirstOrDefault(tuple => tuple.Topic == link);
            if (publishers != null && publishers.Members.Count != 0)
            {
                foreach (string publisher in publishers.Members)
                {
                    description.Append("  ").Append(publisher).AppendLine();
                }
            }
            else
            {
                description.Append("  (none)").AppendLine();
            }

            description.Append("<color=#000080ff><b>Subscribers:</b></color>").AppendLine();
            var subscribers = systemState.Subscribers.FirstOrDefault(tuple => tuple.Topic == link);
            if (subscribers != null && subscribers.Members.Count != 0)
            {
                foreach (string subscriber in subscribers.Members)
                {
                    description.Append("  ").Append(subscriber).AppendLine();
                }
            }
            else
            {
                description.Append("  (none)").AppendLine();
            }

            panel.TextBottom.SetText(description);
        }

        void UpdateServicesLink(string link)
        {
            using var description = BuilderPool.Rent();

            var systemState = ConnectionManager.Connection.GetSystemState(RequestType.CachedOnly);
            if (systemState == null)
            {
                panel.TextBottom.text = EmptyBottomText;
                return;
            }

            description.Append("<b>").Append(link).Append("</b>").AppendLine();

            if (providerAddress != null)
            {
                description.Append("  [").Append(providerAddress).Append("]").AppendLine();
            }
            else
            {
                tokenSource?.Cancel();
                tokenSource = new CancellationTokenSource(5000);
                GetServiceInfo(link, tokenSource.Token);
            }

            description.Append("<color=#008000ff><b>Provider:</b></color>").AppendLine();
            var providers = systemState.Services.FirstOrDefault(tuple => tuple.Topic == link);
            if (providers != null && providers.Members.Count != 0)
            {
                foreach (string publisher in providers.Members)
                {
                    description.Append("  ").Append(publisher).AppendLine();
                }
            }
            else
            {
                description.Append("  (none)").AppendLine();
            }

            panel.TextBottom.SetText(description);

            async void GetServiceInfo(string service, CancellationToken token)
            {
                if (!ConnectionManager.IsConnected)
                {
                    return;
                }

                var client = ConnectionManager.Connection.Client;
                try
                {
                    var response = await client.RosMasterClient.LookupServiceAsync(service, token);
                    if (response.ServiceUri == null)
                    {
                        return;
                    }

                    providerAddress = response.ServiceUri.ToString();
                    UpdateServicesLink(service);
                }
                catch (OperationCanceledException)
                {
                }
            }
        }

        void UpdateParametersLink(string link)
        {
            using var description = BuilderPool.Rent();

            if (!ConnectionManager.IsConnected)
            {
                panel.TextBottom.text = EmptyBottomText;
                return;
            }

            description.Append("<b>").Append(link).Append("</b>").AppendLine();

            if (!paramValue.IsEmpty)
            {
                description.Append("<b>Value:</b>").AppendLine();

                string value = JsonConvert
                    .SerializeObject(paramValue, Formatting.Indented, JsonConverter);
                if (paramValue.ValueType != XmlRpcValue.Type.String)
                {
                    description.Append(value);
                }
                else
                {
                    description.Append(value
                        .Replace("<", "<noparse><</noparse>")
                        .Replace("\\n", "\n")
                        .Replace("\\\"", "\"")
                    );
                }
            }
            else
            {
                tokenSource?.Cancel();
                tokenSource = new CancellationTokenSource(5000);
                GetParamValue(link, tokenSource.Token);
            }

            panel.TextBottom.SetText(description);
            //panel.TextBottom.UpdateVertexData();

            async void GetParamValue(string param, CancellationToken token)
            {
                if (!ConnectionManager.IsConnected)
                {
                    return;
                }

                try
                {
                    (paramValue, _) = await ConnectionManager.Connection.GetParameterAsync(param, 5000, token);
                    GameThread.Post(() => UpdateParametersLink(link));
                }
                catch (OperationCanceledException)
                {
                }
            }
        }


        void UpdateNodesLink(string link)
        {
            var systemState = ConnectionManager.Connection.GetSystemState(RequestType.CachedOnly);
            if (systemState == null)
            {
                panel.TextBottom.text = EmptyBottomText;
                return;
            }

            using var description = BuilderPool.Rent();

            description.Append("<b>").Append(link).Append("</b>").AppendLine();

            if (nodeAddress != null)
            {
                description.Append("  [").Append(nodeAddress).Append("]").AppendLine();
            }
            else
            {
                tokenSource?.Cancel();
                tokenSource = new CancellationTokenSource(5000);
                GetNodeInfo(link, tokenSource.Token);
            }

            description.Append("<color=#800000ff><b>Advertises:</b></color>").AppendLine();
            var published = systemState.Publishers.Where(tuple => tuple.Members.Contains(link)).ToArray();
            if (published.Length != 0)
            {
                foreach ((string topic, _) in published)
                {
                    description.Append("  ").Append(topic).AppendLine();
                }
            }
            else
            {
                description.Append("  (none)").AppendLine();
            }

            description.Append("<color=#000080ff><b>Subscribed to:</b></color>").AppendLine();
            var subscribed = systemState.Subscribers.Where(tuple => tuple.Members.Contains(link)).ToArray();
            if (subscribed.Length != 0)
            {
                foreach ((string topic, _) in subscribed)
                {
                    description.Append("  ").Append(topic).AppendLine();
                }
            }
            else
            {
                description.Append("  (none)").AppendLine();
            }

            description.Append("<color=#008000ff><b>Provides Services:</b></color>").AppendLine();
            var services = systemState.Services.Where(tuple => tuple.Members.Contains(link)).ToArray();
            if (services.Length != 0)
            {
                foreach ((string topic, _) in services)
                {
                    description.Append("  ").Append(topic).AppendLine();
                }
            }
            else
            {
                description.Append("  (none)").AppendLine();
            }

            panel.TextBottom.SetText(description);


            async void GetNodeInfo(string node, CancellationToken token)
            {
                if (!ConnectionManager.IsConnected)
                {
                    return;
                }

                var client = ConnectionManager.Connection.Client;
                try
                {
                    var response = await client.RosMasterClient.LookupNodeAsync(node, token);
                    if (response.Uri == null)
                    {
                        return;
                    }

                    nodeAddress = response.Uri.ToString();
                    UpdateNodesLink(node);
                }
                catch (OperationCanceledException)
                {
                }
            }
        }

        void SetupAliases()
        {
            if (HostAliases.Length != panel.Addresses.Count)
            {
                var hostAliases = HostAliases;
                Array.Resize(ref hostAliases, panel.Addresses.Count);
                HostAliases = hostAliases;
            }

            foreach (int i in ..HostAliases.Length)
            {
                if (HostAliases[i] is not ({ } hostname, { } address))
                {
                    return;
                }

                panel.HostNames[i].Value = hostname;
                panel.Addresses[i].Value = address;
                ConnectionUtils.GlobalResolver[hostname] = address;
            }
        }

        void UpdateAliases()
        {
            if (!ConnectionManager.IsConnected)
            {
                return;
            }

            var state = ConnectionManager.Connection.Client.GetSubscriberStatistics();
            var hostsEnum = state.Topics
                .SelectMany(topic => topic.Receivers)
                .Select(receiver => receiver.RemoteUri.Host)
                .Where(host => !IPAddress.TryParse(host, out _));

            hostsBuffer.Clear();
            foreach (string hostname in hostsEnum)
            {
                hostsBuffer.Add(hostname);
            }

            if (hostsBuffer.Count == 0)
            {
                return;
            }

            foreach (var widget in panel.HostNames)
            {
                widget.Hints = hostsBuffer;
            }
        }

        void UpdateAliasesLink(int index)
        {
            string hostname = panel.HostNames[index].Value;
            string address = panel.Addresses[index].Value;
            if (!string.IsNullOrWhiteSpace(hostname) && !string.IsNullOrWhiteSpace(address))
            {
                var newHostAlias = new HostAlias { Hostname = hostname, Address = address };
                if (!newHostAlias.Equals(HostAliases[index]))
                {
                    return;
                }

                HostAliases[index] = newHostAlias;
                RosLogger.Info($"{this}: Adding pair {hostname} -> {address} to resolver list.");
                ConnectionUtils.GlobalResolver[hostname] = address;
                ModuleListPanel.UpdateSimpleConfigurationSettings();
            }
            else if (HostAliases[index] is var (aliasHostname, _))
            {
                ConnectionUtils.GlobalResolver.Remove(aliasHostname);
                RosLogger.Info($"{this}: Removing {aliasHostname} from resolver list.");
                HostAliases[index] = null;
                ModuleListPanel.UpdateSimpleConfigurationSettings();
            }
        }

        [Preserve]
        public static object[] AotHelper => new object[]
        {
            new List<HostAlias>()
        };
    }

    [DataContract]
    public sealed class HostAlias
    {
        [DataMember] public string Hostname { get; set; } = "";
        [DataMember] public string Address { get; set; } = "";

        public void Deconstruct(out string hostname, out string address) => (hostname, address) = (Hostname, Address);
        public bool Equals(HostAlias? a) => a is var (hostname, address) && hostname == Hostname && address == Address;
    }
}