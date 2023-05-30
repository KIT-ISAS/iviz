#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Core;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Roslib2;
using Iviz.Roslib2.RclInterop;
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
            if (!RosManager.TryGetRosClient(out var client))
            {
                description.Append("<b>State: </b> Disconnected. Nothing to show!").AppendLine();
            }
            else
            {
                try
                {
                    switch (client)
                    {
                        case RosClient client1:
                            GenerateReportRos1(description, client1);
                            break;
                        case Ros2Client client2:
                            GenerateReportRos2(description, client2);
                            break;
                    }
                }
                catch (InvalidOperationException)
                {
                    description.Append("EE Interrupted!").AppendLine().AppendLine();
                }
            }

            description.AppendLine().AppendLine();

            panel.Text.SetTextRent(description);
        }

        static void GenerateReportRos1(StringBuilder builder, RosClient client)
        {
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

            foreach (var stat in subscriberStats)
            {
                builder.Append("<color=#000080ff><b><< Subscribed to ")
                    .Append(stat.Topic).Append("</b></color>")
                    .AppendLine();
                builder.Append("<b>Type: </b>[").Append(stat.Type).Append("]").AppendLine();

                var receivers = (IReadOnlyList<Ros1ReceiverState>)stat.Receivers;

                long totalMessages = 0;
                long totalBytes = 0;
                long totalDropped = 0;
                foreach (var receiver in receivers)
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

                if (stat.Receivers.Count == 0)
                {
                    builder.Append("  (No publishers)").AppendLine().AppendLine();
                    continue;
                }

                bool anyErrors = receivers.Any(receiver => receiver.ErrorDescription != null);
                foreach (var receiver in receivers)
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

            foreach (var stat in publisherStats)
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

                var senders = (IReadOnlyList<Ros1SenderState>)stat.Senders;

                foreach (var sender in senders)
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

        static void GenerateReportRos2(StringBuilder builder, Ros2Client client)
        {
            if (!RosProvider.IsRos2VersionSupported)
            {
                builder.Append("<b>Error:</b> ROS version not supported!");
                return;
            }
            
            var subscriberStats = client.GetSubscriberStatistics().Cast<Ros2SubscriberState>();
            var publisherStats = client.GetPublisherStatistics().Cast<Ros2PublisherState>();

            foreach (var stat in subscriberStats)
            {
                builder.Append("<color=#000080ff><b><< Subscribed to ")
                    .Append(stat.Topic).Append("</b></color>")
                    .AppendLine();

                builder.Append("<b>Type: </b>[").Append(stat.Type).Append("]").AppendLine();

                builder.Append("<b>QOS: </b>");
                Append(stat.Profile);
                builder.AppendLine();

                var receivers = (IReadOnlyList<Ros2ReceiverState>)stat.Receivers;

                long totalMessages = 0;
                long totalBytes = 0;
                foreach (var receiver in receivers)
                {
                    totalMessages += receiver.NumReceived;
                    totalBytes += receiver.BytesReceived;
                }

                builder.Append("<b>Received ").Append(totalMessages.ToString("N0")).Append(" msgs | ");
                builder.AppendBandwidth(totalBytes).Append(" total</b>").AppendLine();

                if (stat.Receivers.Count == 0)
                {
                    builder.Append("  (No publishers)").AppendLine().AppendLine();
                    continue;
                }

                foreach (var receiver in receivers)
                {
                    builder.Append("    <b>[");
                    if (receiver.RemoteId == client.CallerId)
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

                    builder.Append(receiver.Guid.ToString()).Append('\n');

                    builder.Append("        <b>").AppendBandwidth(receiver.BytesReceived).Append("</b> ");

                    Append(receiver.Profile);

                    builder.AppendLine();
                }

                builder.AppendLine();
            }

            foreach (var stat in publisherStats)
            {
                builder.Append("<color=#800000ff><b>>> Publishing to ").Append(stat.Topic)
                    .Append("</b></color>")
                    .AppendLine();

                builder.Append("<b>Type: </b>[").Append(stat.Type).Append("]").AppendLine();

                builder.Append("<b>QOS: </b>");
                Append(stat.Profile);
                builder.AppendLine();

                (long totalMessages, long totalBytes) = stat.Senders.Count == 0
                    ? (0, 0)
                    : (stat.Senders[0].NumSent, stat.Senders[0].BytesSent);

                builder.Append("<b>Sent ").Append(totalMessages.ToString("N0")).Append(" msgs | ")
                    .AppendBandwidth(totalBytes).Append("</b> total").AppendLine();

                if (stat.Senders.Count == 0)
                {
                    builder.Append("  (No subscribers)").AppendLine().AppendLine();
                    continue;
                }

                var senders = (IReadOnlyList<Ros2SenderState>)stat.Senders;

                foreach (var sender in senders)
                {
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

                    builder.Append(sender.Guid.ToString()).Append('\n');

                    builder.Append("        ");
                    Append(sender.Profile);

                    if (sender.TopicType != stat.Type)
                    {
                        builder.Append($"        <color=red>Topic type mismatch. " +
                                       $"Expects [{sender.TopicType}].</color>");
                    }

                    builder.AppendLine();
                }

                builder.AppendLine();
            }

            void Append(QosProfile profile)
            {
                builder.Append(profile.Reliability switch
                {
                    ReliabilityPolicy.SystemDefault => "ReliabilityPolicy: Default",
                    ReliabilityPolicy.BestEffort => "BestEffort",
                    ReliabilityPolicy.Reliable => "Reliable",
                    _ => "ReliabilityPolicy: Unknown (" + (int)profile.Reliability + ")"
                });

                builder.Append(profile.Durability switch
                {
                    DurabilityPolicy.SystemDefault => " | DurabilityPolicy: Default",
                    DurabilityPolicy.Volatile => " | Volatile",
                    DurabilityPolicy.TransientLocal => $" | TransientLocal({profile.Depth.ToString()})",
                    DurabilityPolicy.Unknown => "",
                    _ => " | DurabilityPolicy: Unknown (" + (int)profile.Durability + ")"
                });

                builder.Append(profile.History switch
                {
                    HistoryPolicy.SystemDefault => " | HistoryPolicy: Default",
                    HistoryPolicy.KeepAll => " | KeepAll",
                    HistoryPolicy.KeepLast => $" | KeepLast({profile.Depth.ToString()})",
                    HistoryPolicy.Unknown => "",
                    _ => " | HistoryPolicy: Unknown (" + (int)profile.History + ")"
                });
            }
        }
    }
}