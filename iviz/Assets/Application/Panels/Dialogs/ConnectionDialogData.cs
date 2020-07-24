using UnityEngine;
using System;
using System.Net;
using Iviz.Displays;
using Iviz.Roslib;

namespace Iviz.App
{
    public sealed class ConnectionDialogData : DialogData
    {
        static Uri DefaultMasterUri => RosClient.TryGetMasterUri();
        static Uri DefaultMyUri => RosClient.TryGetCallerUri();
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
        
        public override void Initialize(ModuleListPanel newPanel)
        {
            base.Initialize(newPanel);
            this.panel = (ConnectionDialogContents)DialogPanelManager.GetPanelByType(DialogPanelType.Connection);

            Controllers.Logger.LogInternal += Logger_Log;
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
                MyId = text;
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
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }
    }
}
