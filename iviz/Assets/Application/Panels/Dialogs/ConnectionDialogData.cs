#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ConnectionDialogPanel"/> 
    /// </summary>
    public sealed class ConnectionDialogData : DialogData
    {
        const int DefaultPort = 7613;
        static Uri DefaultMasterUri => RosClient.TryGetMasterUri();
        static Uri DefaultMyUri => RosClient.TryGetCallerUri(DefaultPort);
        static string DefaultMyId => "iviz_" + Application.platform.ToString().ToLower();

        readonly ConnectionDialogPanel panel;
        readonly List<Uri> lastMasterUris = new();

        Uri? masterUri = DefaultMasterUri;
        Uri? myUri = DefaultMyUri;
        string? myId = DefaultMyId;

        public override IDialogPanel Panel => panel;

        public Uri? MasterUri
        {
            get => masterUri;
            set
            {
                masterUri = value;
                MasterUriChanged?.Invoke(value);
            }
        }

        public Uri? MyUri
        {
            get => myUri;
            set
            {
                myUri = value;
                MyUriChanged?.Invoke(value);
            }
        }

        public string? MyId
        {
            get => myId;
            set
            {
                myId = value;
                MyIdChanged?.Invoke(value);
            }
        }

        public List<Uri> LastMasterUris
        {
            get => lastMasterUris;
            set
            {
                lastMasterUris.Clear();
                lastMasterUris.AddRange(value);
            }
        }

        public event Action<Uri?>? MasterUriChanged;
        public event Action<Uri?>? MyUriChanged;
        public event Action<string?>? MyIdChanged;
        public event Action<bool>? MasterActiveChanged;

        public ConnectionDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<ConnectionDialogPanel>(DialogPanelType.Connection);
            RosLogger.LogInternal += OnLogInternal;
        }

        public override void Dispose()
        {
            RosLogger.LogInternal -= OnLogInternal;
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

        public override void SetupPanel()
        {
            panel.MyUri.Value = MyUri == null ? "" : MyUri.ToString();
            panel.MyUri.SetHints(RosClient.GetCallerUriCandidates(DefaultPort).Select(uri => uri.ToString()));
            panel.MyId.Value = MyId ?? "";
            panel.MyId.Hints = new[] { DefaultMyId };
            panel.MasterUri.Value = MasterUri == null ? "" : MasterUri.ToString();
            panel.MasterUri.Interactable = !RosManager.Server.IsActive;
            panel.MasterUri.SetHints(LastMasterUris.Select(uri => uri.ToString()));
            panel.LineLog.Active = true;
            panel.ServerMode.State = RosManager.Server.IsActive;

            panel.Close.Clicked += Close;
            panel.MyUri.EndEdit += text =>
            {
                string trimmed = text.Trim();
                var newUri = (Uri.TryCreate(trimmed, UriKind.Absolute, out Uri uri) && uri.Scheme == "http")
                    ? uri
                    : null;
                MyUri = newUri;
            };
            panel.MasterUri.EndEdit += text =>
            {
                string trimmed = text.Trim();
                MasterUri = (Uri.TryCreate(trimmed, UriKind.Absolute, out Uri uri) && uri.Scheme == "http")
                    ? uri
                    : null;
                if (MasterUri == null)
                {
                    return;
                }

                var newCallerUri = RosClient.TryGetCallerUriFor(MasterUri, DefaultPort);
                if (newCallerUri == null)
                {
                    return;
                }

                MyUri = newCallerUri;
                panel.MyUri.Value = newCallerUri.ToString();
            };
            panel.MyId.EndEdit += text =>
            {
                string trimmed = text.Trim();
                MyId = RosClient.IsValidResourceName(trimmed) ? trimmed : null;
            };
            panel.RefreshMyId.Clicked += () =>
            {
                MyId = DefaultMyId;
                panel.MyId.Value = DefaultMyId;
            };
            panel.RefreshMyUri.Clicked += () =>
            {
                MyUri = DefaultMyUri;
                panel.MyUri.Value = DefaultMyUri.ToString();
            };
            panel.ServerMode.Clicked += () =>
            {
                if (!RosManager.Server.IsActive)
                {
                    _ = TryCreateMasterAsync();
                }
                else
                {
                    _ = TryDisposeMasterAsync();
                }
            };
        }

        async Task TryDisposeMasterAsync()
        {
            if (RosManager.IsConnected)
            {
                // this is a bit of a hack, but the roslib module will complain
                // hard if we just yank the master away and it cannot unregister its stuff
                RosManager.Connection.KeepReconnecting = false; // do not retry
                RosManager.Connection.Disconnect();
                
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
            MasterActiveChanged?.Invoke(false);
        }

        public async Task TryCreateMasterAsync()
        {
            const string defaultPort = "11311";
            string ownHost = MyUri?.Host ?? RosClient.TryGetCallerUri().Host;
            string ownMasterUriStr = $"http://{ownHost}:{defaultPort}/";
            var ownMasterUri = new Uri(ownMasterUriStr);

            const string ownMasterId = "iviz_master";

            if (!await RosManager.Server.StartAsync(ownMasterUri, ownMasterId))
            {
                RosLogger.Internal("<b>Error:</b> Failed to initialize ROS master. " +
                                   "Make sure that <b>My Caller URI</b> is a reachable address and that " +
                                   "you are in the right network.");
                return;
            }

            if (MasterUri != ownMasterUri)
            {
                panel.MasterUri.Value = ownMasterUriStr;
                MasterUri = ownMasterUri;
            }

            panel.ServerMode.State = true;
            panel.MasterUri.Interactable = false;
            RosLogger.Internal($"Created <b>master node</b> using my uri {ownMasterUriStr}.");
            MasterActiveChanged?.Invoke(true);

            RosManager.Connection.TryOnceToConnect();
        }

        public async Task TryResetConnectionsAsync()
        {
            // make sure we're not recording, this can get messy
            if (RosManager.Connection.BagListener != null)
            {
                RosLogger.Internal("Unsuspend detected. Closing rosbag!");
                ModuleListPanel.Instance.ShutdownRosbag();
            }

            if (!RosManager.IsConnected)
            {
                return;
            }

            // this is difficult to handle, because every connection
            // died in the background while the app was paused.
            // now we woke up, everything is dead, and we will get a hundred errors
            RosLogger.Internal("Unsuspend detected. Stopping all connections!");

            RosManager.Connection.KeepReconnecting = false; // do not try to reconnect (yet)
            RosManager.Connection.Disconnect();

            if (!RosManager.Server.IsActive)
            {
                // no own ROS master? wait a bit and reconnect
                await Task.Delay(300);
                RosManager.Connection.TryOnceToConnect();
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