using UnityEngine;
using System.Collections;
using Iviz.App;
using System;
using System.Net;
using System.Text;
using System.IO;

namespace Iviz.App
{
    public class ConnectionDialogData : DialogData
    {
        public static Uri DefaultMasterUri { get; } = new Uri("http://localhost:11311/");
        public static Uri DefaultMyUri => new Uri($"http://{Dns.GetHostName()}:7614/");
        public static string DefaultMyId { get; } = "/iviz_" + Application.platform.ToString().ToLower();

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
        public event Action ConnectClicked;
        public event Action StopClicked;
        
        public override void Initialize(DisplayListPanel panel)
        {
            base.Initialize(panel);
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
            panel.Stop.Clicked += () =>
            {
                StopClicked?.Invoke();
            };
            panel.Connect.Clicked += () =>
            {
                ConnectClicked?.Invoke();
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
