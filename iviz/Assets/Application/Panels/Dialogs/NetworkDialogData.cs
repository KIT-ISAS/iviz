using System;
using System.Text;
using Iviz.Ros;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class NetworkDialogData : DialogData
    {
        [NotNull] readonly NetworkDialogContents panel;
        readonly StringBuilder description = new StringBuilder();
        public override IDialogPanelContents Panel => panel;

        public NetworkDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<NetworkDialogContents>(DialogPanelType.Network);
        }
        
        public override void SetupPanel()
        {
            panel.Close.Clicked += Close;
            UpdatePanel();
        }

        public override void UpdatePanel()
        {
            description.Length = 0;
            if (!ConnectionManager.IsConnected)
            {
                description.Append("<b>State: </b> Disconnected. Nothing to show!").AppendLine();
            }
            else
            {

                try
                {
                    ConnectionManager.Connection.GenerateReport(description);
                }
                catch (InvalidOperationException)
                {
                    description.AppendLine("EE Interrupted!").AppendLine();
                }
            }

            description.AppendLine().AppendLine();

            panel.Text.text = description.ToString();
        }
    }
}
