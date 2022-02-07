﻿#nullable enable

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
                    TryCreateMaster();
                }
                else
                {
                    if (RosManager.IsConnected)
                    {
                        // this is a bit of a hack, but the roslib module will complain
                        // hard if we just yank the master away and it cannot unregister its stuff
                        RosManager.Connection.KeepReconnecting = false; // do not retry
                        RosManager.Connection.Disconnect();
                        DoDisposeMaster();

                        async void DoDisposeMaster()
                        {
                            // wait 200 ms for roslib to disconnect gracefully and hope the user
                            // does not click to create a new master until we're finished 
                            await Task.Delay(200);
                            await RosManager.Server.DisposeAsync();
                            OnMasterDisconnected();
                        }
                    }
                    else
                    {
                        // if we're not connected then we don't need to worry
                        RosManager.Server.Dispose();
                        OnMasterDisconnected();
                    }
                }
            };
        }
        
        void OnMasterDisconnected()
        {
            RosLogger.Internal("Master node removed. Switched to <b>client mode</b>.");
            panel.ServerMode.State = false;
            panel.MasterUri.Interactable = true;
            MasterActiveChanged?.Invoke(false);                        
        }        

        public void TryCreateMaster()
        {
            string ownHost = MyUri?.Host ?? RosClient.TryGetCallerUri().Host;
            Uri ownMasterUri = new Uri($"http://{ownHost}:{RosServerManager.DefaultPort.ToString()}/");
            
            const string ownMasterId = "iviz_master";

            if (!RosManager.Server.Start(ownMasterUri, ownMasterId))
            {
                return;
            }
            
            if (MasterUri != ownMasterUri)
            {
                panel.MasterUri.Value = ownMasterUri.ToString();
                MasterUri = ownMasterUri;
            }

            panel.ServerMode.State = true;
            panel.MasterUri.Interactable = false;
            RosLogger.Internal(
                $"Created <b>master node</b> using my uri {ownMasterUri}. You can connect now!");
            MasterActiveChanged?.Invoke(true);
        }
    }
}