using System;
using System.Collections.Generic;
using System.Net;
using Iviz.Core;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.XmlRpc;
using JetBrains.Annotations;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ConnectionDialogContents"/> 
    /// </summary>
    public sealed class ConnectionDialogData : DialogData
    {
        const int DefaultPort = 7613;
        static Uri DefaultMasterUri => RosClient.TryGetMasterUri();
        [NotNull] static Uri DefaultMyUri => RosClient.TryGetCallerUri(DefaultPort);
        [NotNull] static string DefaultMyId => "iviz_" + UnityEngine.Application.platform.ToString().ToLower();

        [NotNull] readonly ConnectionDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        readonly List<Uri> lastMasterUris = new List<Uri>();

        [CanBeNull] Uri masterUri = DefaultMasterUri;

        [CanBeNull]
        public Uri MasterUri
        {
            get => masterUri;
            set
            {
                masterUri = value;
                MasterUriChanged?.Invoke(value);
            }
        }

        [CanBeNull] Uri myUri = DefaultMyUri;

        [CanBeNull]
        public Uri MyUri
        {
            get => myUri;
            set
            {
                myUri = value;
                MyUriChanged?.Invoke(value);
            }
        }

        [CanBeNull] string myId = DefaultMyId;

        [CanBeNull]
        public string MyId
        {
            get => myId;
            set
            {
                myId = value;
                MyIdChanged?.Invoke(value);
            }
        }
        
        
        [NotNull, ItemNotNull]
        public List<Uri> LastMasterUris
        {
            get => lastMasterUris;
            set
            {
                lastMasterUris.Clear();
                lastMasterUris.AddRange(value);
            }
        }

        public event Action<Uri> MasterUriChanged;
        public event Action<Uri> MyUriChanged;
        public event Action<string> MyIdChanged;
        public event Action<bool> MasterActiveChanged;

        public ConnectionDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<ConnectionDialogContents>(DialogPanelType.Connection);

            Logger.LogInternal += OnLogInternal;
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
            panel.MyUri.Hints = RosClient.GetCallerUriCandidates(DefaultPort).Select(uri => uri.ToString());
            panel.MyId.Value = MyId ?? "";
            panel.MyId.Hints = new[] {DefaultMyId};
            panel.MasterUri.Value = MasterUri == null ? "" : MasterUri.ToString();
            panel.MasterUri.Interactable = !RosServerManager.IsActive;
            panel.MasterUri.Hints = LastMasterUris.Select(uri => uri.ToString());
            panel.LineLog.Active = true;

            panel.Close.Clicked += Close;
            panel.MyUri.EndEdit += text =>
            {
                MyUri = (Uri.TryCreate(text, UriKind.Absolute, out Uri uri) && uri.Scheme == "http") ? uri : null;
            };
            panel.MasterUri.EndEdit += text =>
            {
                MasterUri = (Uri.TryCreate(text, UriKind.Absolute, out Uri uri) && uri.Scheme == "http") ? uri : null;
                if (MasterUri == null)
                {
                    return;
                }

                Uri newCallerUri = RosClient.TryGetCallerUriFor(MasterUri, DefaultPort);
                if (newCallerUri != null)
                {
                    MyUri = newCallerUri;
                    panel.MyUri.Value = newCallerUri.ToString();
                }
            };
            panel.MyId.EndEdit += text =>
            {
                MyId = RosClient.IsValidResourceName(text) ? text : null;
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
                if (!panel.ServerMode.State)
                {
                    if (ConnectionManager.IsConnected)
                    {
                        Logger.Internal("Cannot create master node while connected!");
                        return;
                    }

                    string ownHost = MyUri?.Host ?? RosClient.TryGetCallerUri().Host; 
                    Uri ownMasterUri = new Uri($"http://{ownHost}:{RosServerManager.DefaultPort}/");
                    const string ownMasterId = "/iviz_master";

                    if (RosServerManager.Create(ownMasterUri, ownMasterId))
                    {
                        if (MasterUri != ownMasterUri)
                        {
                            panel.MasterUri.Value = ownMasterUri.ToString();
                            MasterUri = ownMasterUri;
                        }

                        panel.ServerMode.State = true;
                        Logger.Internal("Created <b>master node</b>. You can connect now!");
                        MasterActiveChanged?.Invoke(true);
                    }
                }
                else
                {
                    if (ConnectionManager.IsConnected)
                    {
                        Logger.Internal("Cannot remove master node while connected!");
                        return;
                    }

                    RosServerManager.Dispose();
                    Logger.Internal("Master node removed.");
                    panel.ServerMode.State = false;
                    MasterActiveChanged?.Invoke(false);
                }

                panel.MasterUri.Interactable = !RosServerManager.IsActive;
            };
        }
    }
}