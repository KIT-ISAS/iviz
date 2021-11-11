#nullable enable

using System;
using System.Text;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.App
{
    public sealed class NetworkDialogData : DialogData
    {
        readonly NetworkDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public NetworkDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<NetworkDialogContents>(DialogPanelType.Network);
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();
            panel.Close.Clicked += Close;
            UpdatePanel();
        }

        public override void UpdatePanel()
        {
            var description = BuilderPool.Rent();
            try
            {
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
            finally
            {
                BuilderPool.Return(description);
            }
        }

        static void GenerateReport(StringBuilder builder, RosClient? client)
        {
            if (client == null)
            {
                return;
            }

            var masterApi = client.RosMasterClient;
            builder.Append("<b>== Master</b> (").Append(masterApi.TotalRequests.ToString("N0"))
                .Append(" queries | Ping ")
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
                builder.Append("<color=#000080ff><b><< Subscribed to ")
                    .Append(stat.Topic).Append("</b></color>")
                    .AppendLine();
                builder.Append("<b>Type: </b>[").Append(stat.Type).Append("]").AppendLine();

                long totalMessages = 0;
                long totalBytes = 0;
                long totalDropped = 0;
                foreach (var receiver in stat.Receivers)
                {
                    totalMessages += receiver.NumReceived;
                    totalBytes += receiver.BytesReceived;
                    totalDropped += receiver.NumDropped;
                }

                long totalKBytes = totalBytes / 1000;
                builder.Append("<b>Received ").Append(totalMessages.ToString("N0")).Append(" msgs | ")
                    .Append(totalKBytes.ToString("N0")).Append(" kB total</b>").AppendLine();

                if (totalDropped != 0)
                {
                    long percentage = totalDropped * 100 / totalMessages;
                    builder.Append("<b>Dropped ").Append(totalDropped.ToString("N0")).Append(" msgs (")
                        .Append(percentage).Append("%)</b>").AppendLine();
                }

                if (stat.Receivers.Count == 0)
                {
                    builder.Append("  (No publishers)").AppendLine().AppendLine();
                    continue;
                }

                foreach (var receiver in stat.Receivers)
                {
                    builder.Append("  [");
                    if (receiver.RemoteUri == client.CallerUri)
                    {
                        builder.Append("<i>Me</i> @ ");
                    }

                    builder.Append(receiver.RemoteUri.Host).Append(':').Append(receiver.RemoteUri.Port).Append(']');

                    switch (receiver.Status)
                    {
                        case ReceiverStatus.Running:
                            if (receiver.TransportType != null)
                            {
                                builder.Append(receiver.TransportType == TransportType.Tcp
                                    ? " TCP"
                                    : " UDP"
                                );
                            }

                            long kbytes = receiver.BytesReceived / 1000;
                            builder.Append(" | ").Append(kbytes.ToString("N0")).Append("kB");
                            break;
                        case ReceiverStatus.ConnectingRpc:
                            builder.Append(" (Connecting)");
                            break;
                        case ReceiverStatus.ConnectingTcp:
                            builder.Append(" (Connecting to TCP listener)");
                            break;
                        case ReceiverStatus.OutOfRetries:
                            builder.Append(" <color=#ff0000ff>(unreachable)</color>");
                            break;
                        default:
                            builder.Append(" <color=#ff0000ff>(dead)</color>");
                            break;
                    }

                    if (receiver.ErrorDescription != null)
                    {
                        var (time, description) = receiver.ErrorDescription;
                        builder.AppendLine();
                        builder.Append("    <color=#a52a2aff><b>[").Append(time.ToString("HH:mm:ss"))
                            .Append("]</b> ").Append(description).Append("</color>");
                    }

                    builder.AppendLine();
                }

                builder.AppendLine();
            }

            foreach (var stat in publisherStats.Topics)
            {
                builder.Append("<color=#800000ff><b>>> Publishing to ").Append(stat.Topic)
                    .Append("</b></color>")
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
                    builder.Append("  (No subscribers)").AppendLine().AppendLine();
                    continue;
                }

                foreach (var sender in stat.Senders)
                {
                    bool isAlive = sender.IsAlive;
                    builder.Append("  ");
                    if (sender.RemoteId == client.CallerId)
                    {
                        builder.Append("[<i>Me</i> @ ");
                    }
                    else
                    {
                        builder.Append("[").Append(sender.RemoteId).Append(" @ ");
                    }

                    builder.Append(sender.RemoteEndpoint != default
                        ? sender.RemoteEndpoint.Hostname
                        : "(Unknown address)");

                    builder.Append(sender.TransportType == TransportType.Tcp
                        ? "] TCP"
                        : "] UDP"
                    );

                    if (isAlive)
                    {
                        long kbytes = sender.BytesSent / 1000;
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