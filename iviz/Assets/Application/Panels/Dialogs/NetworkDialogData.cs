using System;
using System.Text;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class NetworkDialogData : DialogData
    {
        [NotNull] readonly NetworkDialogContents panel;
        readonly StringBuilder description = new StringBuilder(65536);
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
                    GenerateReport(description, ConnectionManager.Connection.Client);
                }
                catch (InvalidOperationException)
                {
                    description.AppendLine("EE Interrupted!").AppendLine();
                }
            }

            description.AppendLine().AppendLine();

            panel.Text.SetText(description);
        }
        
        static void GenerateReport([NotNull] StringBuilder builder, [CanBeNull] RosClient client)
        {
            if (client == null)
            {
                return;
            }

            var masterApi = client.RosMasterClient;
            builder.Append("<font=Bold>== Master</font> (").Append(masterApi.TotalRequests.ToString("N0"))
                .Append(" reqs | Ping ")
                .Append(masterApi.AvgTimeInQueueInMs).Append(" ms)")
                .AppendLine();

            long masterReceivedKb = masterApi.BytesReceived / 1000;
            long masterSentKb = masterApi.BytesSent / 1000;
            builder.Append("<b>Received ").Append(masterReceivedKb.ToString("N0")).Append(" kB | ");
            builder.Append("Sent ").Append(masterSentKb.ToString("N0")).Append(" kB</b>").AppendLine();
            builder.AppendLine();

            var subscriberStats = client.GetSubscriberStatistics();
            var publisherStats = client.GetPublisherStatistics();

            foreach (var stat in subscriberStats.Topics)
            {
                builder.Append("<color=#000080ff><font=Bold><< Subscribed to ")
                    .Append(stat.Topic).Append("</font></color>")
                    .AppendLine();
                builder.Append("<b>Type: </b>[").Append(stat.Type).Append("]").AppendLine();

                long totalMessages = 0;
                long totalBytes = 0;
                foreach (var receiver in stat.Receivers)
                {
                    totalMessages += receiver.NumReceived;
                    totalBytes += receiver.BytesReceived;
                }

                long totalKbytes = totalBytes / 1000;
                builder.Append("<b>Received ").Append(totalMessages.ToString("N0")).Append(" msgs | ")
                    .Append(totalKbytes.ToString("N0")).Append(" kB</b> total").AppendLine();

                if (stat.Receivers.Count == 0)
                {
                    builder.Append("  (None)").AppendLine();
                    builder.AppendLine();
                    continue;
                }

                foreach (var receiver in stat.Receivers)
                {
                    if (receiver.EndPoint == null &&
                        receiver.RemoteEndpoint == null &&
                        receiver.ErrorDescription == null)
                    {
                        builder.Append("<color=#808080><b>←</b> [")
                            .Append(receiver.RemoteUri.Host).Append(":")
                            .Append(receiver.RemoteUri.Port).Append("] (Unreachable)</color>")
                            .AppendLine();
                        continue;
                    }

                    bool isConnected = receiver.IsConnected;
                    bool isAlive = receiver.IsAlive;
                    builder.Append("  [");
                    if (receiver.RemoteUri == client.CallerUri)
                    {
                        builder.Append("<i>Me</i>] [");
                    }

                    builder.Append(receiver.RemoteUri.Host);
                    builder.Append(":").Append(receiver.RemoteUri.Port).Append("]");
                    if (isAlive && isConnected)
                    {
                        long kbytes = receiver.BytesReceived / 1000;
                        builder.Append(" | ").Append(kbytes.ToString("N0")).Append("kB");
                    }
                    else if (!isAlive)
                    {
                        builder.Append(" <color=#ff0000ff>(dead)</color>");
                    }
                    else
                    {
                        builder.Append(" <color=#000080ff>(Trying to connect...)</color>");
                    }

                    if (receiver.ErrorDescription != null)
                    {
                        builder.AppendLine();
                        builder.Append("<color=#a52a2aff>\"").Append(receiver.ErrorDescription).Append("\"</color>");
                    }

                    builder.AppendLine();
                }

                builder.AppendLine();
            }

            foreach (var stat in publisherStats.Topics)
            {
                builder.Append("<color=#800000ff><font=Bold>>> Publishing to ").Append(stat.Topic).Append("</font></color>")
                    .AppendLine();
                builder.Append("<b>Type: </b>[").Append(stat.Type).Append("]").AppendLine();

                long totalMessages = 0;
                long totalBytes = 0;
                foreach (var sender in stat.Senders)
                {
                    totalMessages += sender.NumSent;
                    totalBytes += sender.BytesSent;
                }

                long totalKbytes = totalBytes / 1000;
                builder.Append("<b>Sent ").Append(totalMessages.ToString("N0")).Append(" msgs | ")
                    .Append(totalKbytes.ToString("N0")).Append(" kB</b> total").AppendLine();

                if (stat.Senders.Count == 0)
                {
                    builder.Append("  (None)").AppendLine();
                    continue;
                }

                foreach (var receiver in stat.Senders)
                {
                    bool isAlive = receiver.IsAlive;
                    builder.Append("  ");
                    if (receiver.RemoteId == client.CallerId)
                    {
                        builder.Append("[<i>Me</i>] ");
                    }
                    else
                    {
                        builder.Append("[").Append(receiver.RemoteId).Append("] ");
                    }

                    builder.Append(receiver.RemoteEndpoint != null
                        ? receiver.RemoteEndpoint.Value.Hostname
                        : "(Unknown address)");

                    if (isAlive)
                    {
                        long kbytes = receiver.BytesSent / 1000;
                        builder.Append(" | ").Append(kbytes.ToString("N0")).Append("kB");
                    }
                    else
                    {
                        builder.Append(" <color=#ff0000ff>(dead)</color>");
                    }

                    builder.AppendLine();
                }

                builder.AppendLine();
            }
        }
        
    }
}
