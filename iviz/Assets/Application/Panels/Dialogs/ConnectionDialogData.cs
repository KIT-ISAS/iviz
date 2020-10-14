using System;
using Iviz.Controllers;
using Iviz.Displays;
using Iviz.Roslib;
using Logger = Iviz.Displays.Logger;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ConnectionDialogContents"/> 
    /// </summary>
    public sealed class ConnectionDialogData : DialogData
    {
        const int DefaultPort = 7613;
        static Uri DefaultMasterUri => RosClient.TryGetMasterUri();
        static Uri DefaultMyUri => RosClient.TryGetCallerUri(DefaultPort);
        static string DefaultMyId { get; } = "/iviz_" + UnityEngine.Application.platform.ToString().ToLower();

        ConnectionDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        Uri masterUri = DefaultMasterUri;
        public Uri MasterUri
        {
            get => masterUri;
            set
            {
                masterUri = value;
                MasterUriChanged?.Invoke(value);
            }
        }
        Uri myUri = DefaultMyUri;
        public Uri MyUri
        {
            get => myUri;
            set
            {
                myUri = value;
                MyUriChanged?.Invoke(value);
            }
        }
        string myId = DefaultMyId;
        public string MyId
        {
            get => myId;
            set
            {
                myId = value;
                MyIdChanged?.Invoke(value);
            }
        }

        public event Action<Uri> MasterUriChanged;
        public event Action<Uri> MyUriChanged;
        public event Action<string> MyIdChanged;
        public event Action<bool> MasterActiveChanged;
        
        public override void Initialize(ModuleListPanel newPanel)
        {
            base.Initialize(newPanel);
            this.panel = (ConnectionDialogContents)DialogPanelManager.GetPanelByType(DialogPanelType.Connection);

            Logger.LogInternal += Logger_Log;
        }

        void Logger_Log(string msg)
        {
            GameThread.RunOnce(() =>
            {
                panel.LineLog.Add(msg);
                panel.LineLog.Flush();
            });
        }

        public override void SetupPanel()
        {
            panel.MyUri.Value = MyUri.ToString();
            panel.MyId.Value = MyId;
            panel.MasterUri.Value = MasterUri.ToString();
            panel.MasterUri.Interactable = !RosServerManager.IsActive;
            panel.LineLog.Active = true;

            panel.Close.Clicked += Close;
            panel.MyUri.EndEdit += text =>
            {
                MyUri = (Uri.TryCreate(text, UriKind.Absolute, out Uri uri) && uri.Scheme == "http") ? uri : null;
            };
            panel.MasterUri.EndEdit += text =>
            {
                MasterUri = (Uri.TryCreate(text, UriKind.Absolute, out Uri uri) && uri.Scheme == "http") ? uri : null;
            };
            panel.MyId.EndEdit += text =>
            {
                MyId = RosClient.IsValidGlobalResourceName(text) ? text : null;
                MyIdChanged?.Invoke(MyId);
            };
            panel.RefreshMyId.Clicked += () =>
            {
                MyId = DefaultMyId;
                panel.MyId.Value = MyId;
                MyIdChanged?.Invoke(MyId);
            };
            panel.RefreshMyUri.Clicked += () =>
            {
                MyUri = DefaultMyUri;
                panel.MyUri.Value = MyUri.ToString();
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

                    if (MyUri.Port == RosServerManager.DefaultPort)
                    {
                        Logger.Internal($"Master port {RosServerManager.DefaultPort} is already being used by the caller!");
                        return;
                    }
                    
                    Uri ownMasterUri = new Uri($"http://{MyUri.Host}:{RosServerManager.DefaultPort}/");
                    string ownMasterId = "/iviz_master";

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
                        MasterUriChanged?.Invoke(MasterUri);
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
                    MasterUriChanged?.Invoke(MasterUri);
                }

                panel.MasterUri.Interactable = !RosServerManager.IsActive;
            };
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }
    }
}
