﻿#nullable enable

using System;
using System.Linq;
using System.Text;
using Iviz.Core;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.App
{
    public sealed class NetworkDialogData : DialogData
    {
        readonly NetworkDialogPanel panel;
        public override IDialogPanel Panel => panel;

        public NetworkDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<NetworkDialogPanel>(DialogPanelType.Network);
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();
            panel.Close.Clicked += Close;
            UpdatePanel();
        }

        public override void UpdatePanel()
        {
            using var description = BuilderPool.Rent();
            if (!RosManager.IsConnected)
            {
                description.Append("<b>State: </b> Disconnected. Nothing to show!").AppendLine();
            }
            else
            {
                try
                {
                    GenerateReport(description, RosManager.Connection.Client);
                }
                catch (InvalidOperationException)
                {
                    description.Append("EE Interrupted!").AppendLine().AppendLine();
                }
            }

            description.AppendLine().AppendLine();

            panel.Text.SetTextRent(description);
        }

        static void GenerateReport(StringBuilder builder, RosClient? client)
        {
            if (client == null)
            {
                return;
            }

            var masterApi = client.RosMasterClient;
            builder.Append("<b>Master</b> (").Append(masterApi.TotalRequests.ToString("N0"))
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

                builder.Append("<b>Received ").Append(totalMessages.ToString("N0")).Append(" msgs | ");
                    builder.AppendBandwidth(totalBytes).Append(" total</b>").AppendLine();

                if (totalDropped != 0)
                {
                    long percentage = totalDropped * 100 / totalMessages;
                    builder.Append("<b>Dropped ").Append(totalDropped.ToString("N0")).Append(" msgs (")
                        .Append(percentage).Append("%)</b>").AppendLine();
                }

                if (stat.Receivers.Length == 0)
                {
                    builder.Append("  (No publishers)").AppendLine().AppendLine();
                    continue;
                }

                bool anyErrors = stat.Receivers.Any(receiver => receiver.ErrorDescription != null);
                foreach (var receiver in stat.Receivers)
                {
                    builder.Append("    <b>[");
                    if (receiver.RemoteUri == client.CallerUri)
                    {
                        builder.Append("<i>Me</i>]</b> ");
                    }
                    else if (receiver.RemoteId != null)
                    {
                        builder.Append(receiver.RemoteId).Append("]</b> ");
                    }
                    else
                    {
                        builder.Append("unknown]</b> ");
                    }

                    builder.Append(receiver.RemoteUri.Host).Append(':').Append(receiver.RemoteUri.Port);

                    switch (receiver.Status)
                    {
                        case ReceiverStatus.Running:
                            if (receiver.TransportType is TransportType.Udp)
                            {
                                builder.Append(" UDP");
                            }

                            builder.Append(" | ").AppendBandwidth(receiver.BytesReceived);
                            break;
                        case ReceiverStatus.ConnectingRpc:
                            // no special message, clear from context
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

                    if (anyErrors)
                    {
                        builder.AppendLine();
                        if (receiver.ErrorDescription != null)
                        {
                            var (time, description) = receiver.ErrorDescription;
                            builder.Append("      <color=#a52a2aff>[").Append(time.ToString("HH:mm:ss"))
                                .Append("] ").Append(description).Append("</color>");
                        }
                        else
                        {
                            builder.Append("      [Ok]");
                        }
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

                builder.Append("<b>Sent ").Append(totalMessages.ToString("N0")).Append(" msgs | ")
                    .AppendBandwidth(totalBytes).Append("</b> total").AppendLine();

                if (stat.Senders.Count == 0)
                {
                    builder.Append("  (No subscribers)").AppendLine().AppendLine();
                    continue;
                }

                foreach (var sender in stat.Senders)
                {
                    bool isAlive = sender.IsAlive;
                    builder.Append("    <b>[");
                    if (sender.RemoteId == client.CallerId)
                    {
                        builder.Append("<i>Me</i>]</b> ");
                    }
                    else if (sender.RemoteId.Length != 0)
                    {
                        builder.Append(sender.RemoteId).Append("]</b> ");
                    }
                    else
                    {
                        builder.Append("unknown]</b> ");
                    }

                    string remoteHostname = sender.RemoteEndpoint.Hostname;
                    if (remoteHostname.StartsWith("::ffff:")) // remove ipv6 prefix of ipv4
                    {
                        remoteHostname = remoteHostname[7..];
                    }
                    
                    builder.Append(sender.RemoteEndpoint != default
                        ? remoteHostname
                        : "(Unknown address)");

                    if (sender.TransportType == TransportType.Udp)
                    {
                        builder.Append(" UDP");
                    }

                    if (isAlive)
                    {
                        builder.Append(" | ").AppendBandwidth(sender.BytesSent);
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