#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using UnityEngine;
using Logger = Iviz.Tools.Logger;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ConnectionDialogPanel"/> 
    /// </summary>
    public sealed class ConnectionDialogData : DialogData
    {
        const int DefaultPort = 7613;

        static Uri LocalhostMaster { get; } = new Uri("http://127.0.0.1:11311/");

        static readonly string[] RosModes = RosProvider.IsRos2VersionSupported
            ? new[] { "ROS", "ROS2", "ROSBridge" }
            : new[] { "ROS", "ROSBridge" };

        public static Uri DefaultMasterUri => RosClient.TryGetMasterUri();
        public static Uri DefaultMyUri => RosClient.TryGetCallerUri(DefaultPort);
        public static string DefaultMyId => "iviz_" + UnityUtils.GetPlatformName();
        public static Uri DefaultBridgeUri { get; } = new("ws://127.0.0.1:9090/");

        readonly ConnectionDialogPanel panel;

        Uri? masterUri = DefaultMasterUri;
        Uri? myUri = DefaultMyUri;
        string? myId = DefaultMyId;
        readonly List<Uri> lastMasterUris = new();

        int domainId;
        Endpoint? discoveryServer;
        RosVersion rosVersion;
        readonly List<Endpoint?> lastDiscoveryServers = new() { null };

        Uri? bridgeUri = DefaultBridgeUri;
        readonly List<Uri> lastBridgeUris = new();

        public override IDialogPanel Panel => panel;

        public Uri? MasterUri
        {
            get => masterUri;
            set
            {
                masterUri = value;

                RosManager.Connection.MasterUri = value;
                RosManager.Connection.KeepReconnecting = false;
                if (value != null)
                {
                    RosLogger.Internal(RosManager.Server.IsActive
                        ? $"Changing master uri to local master '{value}'."
                        : $"Changing master uri to '{value}'.");
                }

                MasterUriChanged.TryRaise(this);
            }
        }

        public Uri? MyUri
        {
            get => myUri;
            set
            {
                myUri = value;

                RosManager.Connection.MyUri = value;
                RosManager.Connection.KeepReconnecting = false;
                if (value != null)
                {
                    RosLogger.Internal($"Changing caller uri to '{value}'.");
                }

                MyUriChanged.TryRaise(this);
            }
        }

        public string? MyId
        {
            get => myId;
            set
            {
                myId = value;

                RosManager.Connection.MyId = value;
                RosManager.Connection.KeepReconnecting = false;

                if (value != null)
                {
                    RosLogger.Internal($"Changing my ROS id to '{value}'.");
                }

                MyIdChanged.TryRaise(this);
            }
        }

        public List<Uri> LastMasterUris
        {
            get => lastMasterUris;
            set
            {
                lastMasterUris.Clear();
                lastMasterUris.AddRange(value);
                if (!lastMasterUris.Contains(LocalhostMaster))
                {
                    lastMasterUris.Add(LocalhostMaster);
                }
            }
        }

        public List<Endpoint?> LastDiscoveryServers
        {
            get => lastDiscoveryServers;
            set
            {
                lastDiscoveryServers.Clear();
                lastDiscoveryServers.AddRange(value);
                if (!lastDiscoveryServers.Contains(null))
                {
                    lastDiscoveryServers.Add(null);
                }
            }
        }

        public Endpoint? DiscoveryServer
        {
            get => discoveryServer;
            set
            {
                if (discoveryServer == value)
                {
                    return;
                }

                discoveryServer = value;

                RosManager.Connection.DiscoveryServer = value;
                RosLogger.Internal(value is { } endpoint
                    ? $"Setting ROS2 discovery server to {endpoint.Description()}."
                    : "Disabling ROS2 discovery server.");

                DiscoveryServerChanged.TryRaise(this);
            }
        }

        public int DomainId
        {
            get => domainId;
            set
            {
                domainId = Mathf.Clamp(value, 0, 9);

                RosManager.Connection.DomainId = value;
                RosLogger.Internal($"Setting ROS2 domain id to {value.ToString()}.");

                DomainIdChanged.TryRaise(this);
            }
        }

        public Uri? BridgeUri
        {
            get => bridgeUri;
            set
            {
                bridgeUri = value;

                RosManager.Connection.BridgeUri = value;
                RosManager.Connection.KeepReconnecting = false;
                if (value != null)
                {
                    RosLogger.Internal($"Changing bridge uri to '{value}'.");
                }

                BridgeUriChanged.TryRaise(this);
            }
        }

        public List<Uri> LastBridgeUris
        {
            get => lastBridgeUris;
            set
            {
                lastBridgeUris.Clear();
                lastBridgeUris.AddRange(value);
                if (!lastBridgeUris.Contains(DefaultBridgeUri))
                {
                    lastBridgeUris.Add(DefaultBridgeUri);
                }
            }
        }

        public RosVersion RosVersion
        {
            get => rosVersion;
            set
            {
                if (rosVersion == value)
                {
                    return;
                }

                string currentId = rosVersion switch
                {
                    RosVersion.ROS1 => panel.MyId.Value,
                    RosVersion.ROS2 => panel.MyId2.Value,
                    RosVersion.ROSBridge => panel.MyIdBridge.Value,
                    _ => throw new IndexOutOfRangeException()
                };

                rosVersion = value;
                switch (value)
                {
                    case RosVersion.ROS1:
                        panel.RosPanel1.SetActive(true);
                        panel.RosPanel2.SetActive(false);
                        panel.RosPanelBridge.SetActive(false);
                        panel.MyId.Value = currentId;
                        break;
                    case RosVersion.ROS2:
                        panel.RosPanel1.SetActive(false);
                        panel.RosPanel2.SetActive(true);
                        panel.RosPanelBridge.SetActive(false);
                        panel.MyId2.Value = currentId;
                        break;
                    case RosVersion.ROSBridge:
                        panel.RosPanel1.SetActive(false);
                        panel.RosPanel2.SetActive(false);
                        panel.RosPanelBridge.SetActive(true);
                        panel.MyIdBridge.Value = currentId;
                        break;
                }

                if (RosManager.Connection.BagListener != null)
                {
                    RosLogger.Internal("Closing rosbag due to ROS version change.");
                    ModuleListPanel.Instance.ShutdownRosbag();
                }

                if (RosManager.Server.IsActive)
                {
                    RosManager.Server.Dispose();
                    OnMasterDisconnected();
                    RosLogger.Internal("Finished removing ROS1 master node.");
                }

                RosManager.Connection.RosVersion = value;
                RosVersionChanged.TryRaise(this);
            }
        }

        public event Action? MasterUriChanged;
        public event Action? MyUriChanged;
        public event Action? MyIdChanged;
        public event Action? MasterActiveChanged;
        public event Action? RosVersionChanged;
        public event Action? DomainIdChanged;
        public event Action? DiscoveryServerChanged;
        public event Action? BridgeUriChanged;

        public ConnectionDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<ConnectionDialogPanel>(DialogPanelType.Connection);
            panel.LineLog.Text.vertexBufferAutoSizeReduction = false;
            GameThread.ApplicationPaused += OnPaused;
            RosLogger.LogInternal = OnLogInternal;
        }

        public override void Dispose()
        {
            GameThread.ApplicationPaused -= OnPaused;
            RosLogger.LogInternal = null;
        }

        void OnLogInternal(string msg)
        {
            GameThread.Post(() =>
            {
                panel.LineLog.Add(msg);
                panel.LineLog.Flush();
            });
        }

        public void UpdateLastMasterUris()
        {
            if (MasterUri == null)
            {
                return;
            }

            if (!lastMasterUris.Contains(MasterUri))
            {
                lastMasterUris.Add(MasterUri);
            }
        }

        public void UpdateLastDiscoveryServers()
        {
            if (DiscoveryServer == null)
            {
                return;
            }

            if (!lastDiscoveryServers.Contains(DiscoveryServer))
            {
                lastDiscoveryServers.Add(DiscoveryServer);
            }
        }

        public void UpdateLastBridgeUris()
        {
            if (BridgeUri == null)
            {
                return;
            }

            if (!lastBridgeUris.Contains(BridgeUri))
            {
                lastBridgeUris.Add(BridgeUri);
            }
        }


        public override void SetupPanel()
        {
            const string noneStr = "(none)";

            panel.RosMode.Options = RosModes;
            panel.RosMode.Index = RosVersion switch
            {
                RosVersion.ROS1 => 0,
                RosVersion.ROS2 => 1,
                RosVersion.ROSBridge => RosProvider.IsRos2VersionSupported ? 2 : 1,
                _ => throw new IndexOutOfRangeException()
            };
            panel.RosMode.ValueChanged += (i, _) => RosVersion = i switch
            {
                0 => RosVersion.ROS1,
                1 => RosProvider.IsRos2VersionSupported ? RosVersion.ROS2 : RosVersion.ROSBridge,
                2 => RosVersion.ROSBridge,
                _ => throw new IndexOutOfRangeException()
            };

            panel.MyUri.Value = MyUri == null ? "" : MyUri.ToString();
            panel.MyUri.Hints = GetUriHints();
            panel.MyId.Value = MyId ?? "";
            panel.MyId.Hints = new[] { DefaultMyId };
            panel.MasterUri.Value = MasterUri == null ? "" : MasterUri.ToString();
            panel.MasterUri.Interactable = !RosManager.Server.IsActive;
            panel.MasterUri.Hints = LastMasterUris.Select(uri => uri.ToString());
            panel.LineLog.Active = true;
            panel.ServerMode.State = RosManager.Server.IsActive;

            panel.MyId2.Value = MyId ?? "";
            panel.MyId2.Hints = new[] { DefaultMyId };
            panel.DomainId.Index = DomainId;
            panel.DiscoveryServer.Value = DiscoveryServer?.Description() ?? "";
            panel.DiscoveryServer.Hints = LastDiscoveryServers.Select(
                endpoint => endpoint?.Description() ?? noneStr);


            panel.MyIdBridge.Value = MyId ?? "";
            panel.MyIdBridge.Hints = new[] { DefaultMyId };
            panel.BridgeUri.Value = BridgeUri == null ? "" : BridgeUri.ToString();
            panel.BridgeUri.Hints = LastBridgeUris.Select(uri => uri.ToString());

            /*
            if (!RosProvider.IsRos2VersionSupported)
            {
                RosVersion = RosVersion.Ros1;
                panel.RosVersion1.Interactable = false;
                panel.RosVersion2.Interactable = false;
            }
            */

            panel.Close.Clicked += Close;
            panel.MyUri.EndEdit += text =>
            {
                string trimmed = text.Trim();
                if (!Uri.TryCreate(trimmed, UriKind.Absolute, out var uri))
                {
                    RosLogger.Internal("<b>Error:</b> Failed to set own uri. Reason: Uri is not valid.");
                    MyUri = null;
                    return;
                }

                if (uri.Scheme != "http")
                {
                    RosLogger.Internal("<b>Error:</b> Failed to set own uri. Reason: Scheme must be 'http'.");
                    MyUri = null;
                    return;
                }

                MyUri = uri;
            };

            panel.MasterUri.EndEdit += text =>
            {
                string trimmed = text.Trim();
                if (!Uri.TryCreate(trimmed, UriKind.Absolute, out var uri))
                {
                    RosLogger.Internal("<b>Error:</b> Failed to set master uri. Reason: Uri is not valid.");
                    MasterUri = null;
                    return;
                }

                if (uri.Scheme != "http")
                {
                    RosLogger.Internal("<b>Error:</b> Failed to set master uri. Reason: Scheme must be 'http'.");
                    MasterUri = null;
                    return;
                }

                var newCallerUri = RosClient.TryGetCallerUriFor(uri, DefaultPort);
                if (newCallerUri != null)
                {
                    panel.MyUri.Value = newCallerUri.ToString();
                    MyUri = newCallerUri;
                }

                MasterUri = uri;
            };

            panel.MyId.Submit += SetMyId;
            panel.MyId2.Submit += SetMyId;
            panel.MyIdBridge.Submit += SetMyId;

            void SetMyId(string text)
            {
                string trimmed = text.Trim();
                if (RosNameUtils.IsValidResourceName(trimmed) && trimmed[0] is not ('/' or '~'))
                {
                    MyId = trimmed;
                }
                else
                {
                    RosLogger.Internal(
                        "<b>Error:</b> Failed to set caller id. Reason: Id is not a valid resource name.");
                    RosLogger.Internal("First character must be alphanumeric [a-z A-Z]");
                    RosLogger.Internal("Remaining characters must be alphanumeric, digits, '_' or '/'");
                    MyId = null;
                }
            }

            panel.ServerMode.Clicked += () =>
            {
                _ = !RosManager.Server.IsActive
                    ? TryCreateMasterAsync().AwaitNoThrow(this)
                    : TryDisposeMasterAsync().AwaitNoThrow(this);
            };

            panel.DomainId.ValueChanged += (i, _) => DomainId = i;

            panel.DiscoveryServer.Submit += text =>
            {
                if (text.Length == 0)
                {
                    DiscoveryServer = null;
                    return;
                }

                if (text == noneStr)
                {
                    DiscoveryServer = null;
                    panel.DiscoveryServer.Value = "";
                    return;
                }

                string trimmed = text.Trim();
                string[] split = trimmed.Split(":");

                if (split.Length != 2)
                {
                    RosLogger.Internal("<b>Error:</b> Failed to set discovery server. " +
                                       "Reason: Value should have the form 'address:port'");
                    DiscoveryServer = null;
                    return;
                }

                if (!ushort.TryParse(split[1], out ushort port))
                {
                    RosLogger.Internal("<b>Error:</b> Failed to set discovery server. " +
                                       "Reason: Port is not valid.");
                    DiscoveryServer = null;
                    return;
                }

                DiscoveryServer = new Endpoint(split[0].Trim(), port);
            };

            panel.BridgeUri.EndEdit += text =>
            {
                string trimmed = text.Trim();
                if (!Uri.TryCreate(trimmed, UriKind.Absolute, out var uri))
                {
                    RosLogger.Internal("<b>Error:</b> Failed to set bridge uri. Reason: Uri is not valid.");
                    BridgeUri = null;
                    return;
                }

                if (uri.Scheme != "ws")
                {
                    RosLogger.Internal("<b>Error:</b> Failed to set bridge uri. Reason: Scheme must be 'ws'.");
                    BridgeUri = null;
                    return;
                }

                BridgeUri = uri;
            };
            /*
            panel.RosVersion1.Clicked += () =>
            {
                if (!RosProvider.IsRos2VersionSupported)
                {
                    RosLogger.Internal("<b>Error:</b> ROS2 is not supported on this platform.");
                    return;
                }

                RosVersion = RosVersion.Ros2;
            };
            panel.RosVersion2.Clicked += () => RosVersion = RosVersion.Ros1;
            */
        }

        void OnPaused(bool pauseStatus)
        {
            if (pauseStatus) return;
            panel.MyUri.Hints = GetUriHints();
        }

        static IEnumerable<string> GetUriHints() =>
            RosClient.GetCallerUriCandidates(DefaultPort).Select(uri => uri.ToString());

        async Task TryDisposeMasterAsync()
        {
            var connection = RosManager.Connection;
            if (connection.IsConnected)
            {
                // this is a bit of a hack, but the roslib module will complain
                // hard if we just yank the master away and it cannot unregister its stuff
                connection.KeepReconnecting = false; // do not retry
                connection.Disconnect();

                // wait 200 ms for roslib to disconnect gracefully and hope the user
                // does not click to create a new master until we're finished 
                await Task.Delay(200);
            }

            await RosManager.Server.DisposeAsync();
            OnMasterDisconnected();
        }

        void OnMasterDisconnected()
        {
            RosLogger.Internal("Master node removed. Switched to <b>client mode</b>.");
            panel.ServerMode.State = false;
            panel.MasterUri.Interactable = true;
            RosManager.Connection.Disconnect();
            MasterActiveChanged.TryRaise(this);
        }

        public async Task TryCreateMasterAsync()
        {
            if (RosVersion != RosVersion.ROS1)
            {
                RosLogger.Internal("ROS master can only be created with ROS1.");
                return;
            }

            const string defaultPort = "11311";
            string ownHost = MyUri?.Host ?? RosClient.TryGetCallerUri().Host;
            string ownMasterUriStr = $"http://{ownHost}:{defaultPort}/";
            var ownMasterUri = new Uri(ownMasterUriStr);

            const string ownMasterId = "iviz_master";

            try
            {
                if (!await RosManager.Server.StartAsync(ownMasterUri, ownMasterId))
                {
                    RosLogger.Internal("<b>Error:</b> Failed to initialize ROS master. " +
                                       "Make sure that <b>My Caller URI</b> is a reachable address and that " +
                                       "you are in the right network.");
                    return;
                }
            }
            catch (Exception e)
            {
                RosLogger.Internal("<b>Error</b>: Failed to initialize ROS master!", e);
            }

            if (MasterUri != ownMasterUri)
            {
                panel.MasterUri.Value = ownMasterUriStr;
                MasterUri = ownMasterUri;
            }

            panel.ServerMode.State = true;
            panel.MasterUri.Interactable = false;
            RosLogger.Internal($"Created <b>master node</b> using my uri {ownMasterUriStr}.");
            MasterActiveChanged.TryRaise(this);

            RosManager.Connection.TryOnceToConnect();
        }

        public void TryStartupConnection()
        {
            if (MyId == null)
            {
                return;
            }

            var currentRosVersion = RosVersion;

            if (currentRosVersion == RosVersion.ROS1)
            {
                if (MasterUri == null || MyUri == null)
                {
                    return;
                }

                bool ownHost = MyUri.Host == MasterUri.Host;
                RosLogger.Internal(ownHost
                    ? "Trying to connect to local ROS server."
                    : "Trying to connect to previous ROS server.");

                if (RosProvider.OwnMasterEnabledByDefault && ownHost)
                {
                    _ = TryCreateMasterAsync().AwaitNoThrow(this); // create master and connect
                    return;
                }
            }

            if (currentRosVersion == RosVersion.ROSBridge)
            {
                if (BridgeUri == null)
                {
                    return;
                }

                RosLogger.Internal("Trying to connect to previous ROSBridge server.");
            }

            RosManager.Connection.TryOnceToConnect();
        }

        public void ToggleConnection(bool value)
        {
            var connection = RosManager.Connection;
            if (value)
            {
                RosLogger.Internal(connection.IsConnected ? "Reconnection requested." : "Connection requested.");
                connection.Disconnect();
                connection.KeepReconnecting = true;
            }
            else
            {
                RosLogger.Internal(connection.IsConnected ? "Disconnection requested." : "Already disconnected.");
                connection.KeepReconnecting = false;
                connection.Disconnect();
            }
        }

        public async Task TryResetConnectionsAsync()
        {
            // make sure we're not recording, this can get messy
            var connection = RosManager.Connection;
            if (connection.BagListener != null)
            {
                RosLogger.Internal("Unsuspend detected. Closing rosbag!");
                ModuleListPanel.Instance.ShutdownRosbag();
            }

            if (!connection.IsConnected)
            {
                return;
            }

            // this is difficult to handle, because every connection
            // died in the background while the app was paused.
            // now we woke up, everything is dead, and we will get a hundred errors
            RosLogger.Internal("Unsuspend detected. Stopping all connections!");

            connection.KeepReconnecting = false; // do not try to reconnect (yet)
            connection.Disconnect();

            if (!RosManager.Server.IsActive)
            {
                // no own ROS master? wait a bit and reconnect
                await Task.Delay(300);
                connection.TryOnceToConnect();
                return;
            }

            // try to gracefully restart the ROS master without
            // having iviz freak out and start vomiting exceptions

            await TryDisposeMasterAsync();
            await Task.Delay(300);
            await TryCreateMasterAsync();
        }
    }
}