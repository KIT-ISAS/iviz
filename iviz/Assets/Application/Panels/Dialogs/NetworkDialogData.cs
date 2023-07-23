#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iviz.Bridge.Client;
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
            if (!RosManager.TryGetRosClient(out var client))
            {
                panel.Text.text = "<b>State: </b> Disconnected. Nothing to show!\n\n";
                return;
            }

            GameThread.PostAsync(() => UpdatePanelAsync(client));
        }

        async ValueTask UpdatePanelAsync(IRosClient client)
        {
            try
            {
                var subscriberStates = await client.GetSubscriberStatisticsAsync();
                var publisherStates = await client.GetPublisherStatisticsAsync();


                using var description = BuilderPool.Rent();
                switch (client)
                {
                    case RosClient client1:
                        GenerateReportRos1(description, client1, subscriberStates, publisherStates);
                        break;
                    case Ros2Client client2:
                        GenerateReportRos2(description, client2, subscriberStates, publisherStates);
                        break;
                    case RosbridgeClient rosbridgeClient:
                        GenerateReportRosbridge(description, rosbridgeClient, subscriberStates, publisherStates);
                        break;
                    default:
                        GenerateReportUnknown(description, client);
                        break;
                }

                description.AppendLine().AppendLine();
                panel.Text.SetTextRent(description);
            }
            catch (InvalidOperationException)
            {
                panel.Text.text = "\n\n<b>Internal error: </b> Interrupted!\n\n";
            }
        }

        static void GenerateReportRos1(StringBuilder builder, RosClient client,
            SubscriberState[] subscriberStates, PublisherState[] publisherStats)
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

            foreach (var state in subscriberStates)
            {
                builder.Append("<color=#000080ff><b><< Subscribed to ")
                    .Append(state.Topic).Append("</b></color>")
                    .AppendLine();
                builder.Append("<b>Type: </b>[").Append(state.Type).Append("]").AppendLine();

                var receivers = state.Receivers;

                long totalMessages = 0;
                long totalBytes = 0;
                long totalDropped = 0;
                foreach (var receiver in receivers)
                {
                    totalMessages += receiver.NumReceived;
                    totalBytes += receiver.BytesReceived;
                    totalDropped += ((Ros1ReceiverState)receiver).NumDropped;
                }

                builder.Append("<b>Received ").Append(totalMessages.ToString("N0")).Append(" msgs | ");
                builder.AppendBandwidth(totalBytes).Append(" total</b>").AppendLine();

                if (totalDropped != 0)
                {
                    long percentage = totalDropped * 100 / totalMessages;
                    builder.Append("<b>Dropped ").Append(totalDropped.ToString("N0")).Append(" msgs (")
                        .Append(percentage).Append("%)</b>").AppendLine();
                }

                if (state.Receivers.Length == 0)
                {
                    builder.Append("  (No publishers)").AppendLine().AppendLine();
                    continue;
                }

                bool anyErrors = receivers.Any(receiver => ((Ros1ReceiverState)receiver).ErrorDescription != null);
                foreach (var receiver in receivers)
                {
                    var ros1Receiver = (Ros1ReceiverState)receiver;
                    
                    builder.Append("    <b>[");
                    if (ros1Receiver.RemoteUri == client.CallerUri)
                    {
                        builder.Append("<i>Me</i>]</b> ");
                    }
                    else if (ros1Receiver.RemoteId != null)
                    {
                        builder.Append(ros1Receiver.RemoteId).Append("]</b> ");
                    }
                    else
                    {
                        builder.Append("unknown]</b> ");
                    }

                    builder.Append(ros1Receiver.RemoteUri.Host).Append(':').Append(ros1Receiver.RemoteUri.Port);

                    switch (ros1Receiver.Status)
                    {
                        case ReceiverStatus.Running:
                            if (ros1Receiver.TransportType is TransportType.Udp)
                            {
                                builder.Append(" UDP");
                            }

                            builder.Append(" | ").AppendBandwidth(ros1Receiver.BytesReceived);
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
                        if (ros1Receiver.ErrorDescription != null)
                        {
                            var (time, description) = ros1Receiver.ErrorDescription;
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

            foreach (var state in publisherStats)
            {
                builder.Append("<color=#800000ff><b>>> Publishing to ").Append(state.Topic)
                    .Append("</b></color>")
                    .AppendLine();
                builder.Append("<b>Type: </b>[").Append(state.Type).Append("]").AppendLine();

                long totalMessages = 0;
                long totalBytes = 0;

                var senders = state.Senders;

                foreach (var sender in senders)
                {
                    totalMessages += sender.NumSent;
                    totalBytes += sender.BytesSent;
                }

                builder.Append("<b>Sent ").Append(totalMessages.ToString("N0")).Append(" msgs | ")
                    .AppendBandwidth(totalBytes).Append("</b> total").AppendLine();

                if (senders.Length == 0)
                {
                    builder.Append("  (No subscribers)").AppendLine().AppendLine();
                    continue;
                }


                foreach (var sender in senders)
                {
                    var ros1Sender = (Ros1SenderState)sender;
                    
                    bool isAlive = ros1Sender.IsAlive;
                    builder.Append("    <b>[");
                    if (ros1Sender.RemoteId == client.CallerId)
                    {
                        builder.Append("<i>Me</i>]</b> ");
                    }
                    else if (ros1Sender.RemoteId.Length != 0)
                    {
                        builder.Append(ros1Sender.RemoteId).Append("]</b> ");
                    }
                    else
                    {
                        builder.Append("unknown]</b> ");
                    }

                    string remoteHostname = ros1Sender.RemoteEndpoint.Hostname;
                    if (remoteHostname.StartsWith("::ffff:")) // remove ipv6 prefix of ipv4
                    {
                        remoteHostname = remoteHostname[7..];
                    }

                    builder.Append(ros1Sender.RemoteEndpoint != default
                        ? remoteHostname
                        : "(Unknown address)");

                    if (ros1Sender.TransportType == TransportType.Udp)
                    {
                        builder.Append(" UDP");
                    }

                    if (isAlive)
                    {
                        builder.Append(" | ").AppendBandwidth(ros1Sender.BytesSent);
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

        static void GenerateReportRos2(StringBuilder builder, Ros2Client client,
            SubscriberState[] subscriberStats, PublisherState[] publisherStats)
        {
            if (!RosProvider.IsRos2VersionSupported)
            {
                builder.Append("<b>Error:</b> ROS version not supported!");
                return;
            }

            foreach (var state in subscriberStats)
            {
                builder.Append("<color=#000080ff><b><< Subscribed to ")
                    .Append(state.Topic).Append("</b></color>")
                    .AppendLine();

                builder.Append("<b>Type: </b>[").Append(state.Type).Append("]").AppendLine();

                builder.Append("<b>QOS: </b>");
                Append(((Ros2SubscriberState)state).Profile);
                builder.AppendLine();

                var receivers = state.Receivers;

                long totalMessages = 0;
                long totalBytes = 0;
                foreach (var receiver in receivers)
                {
                    totalMessages += receiver.NumReceived;
                    totalBytes += receiver.BytesReceived;
                }

                builder.Append("<b>Received ").Append(totalMessages.ToString("N0")).Append(" msgs | ");
                builder.AppendBandwidth(totalBytes).Append(" total</b>").AppendLine();

                if (state.Receivers.Length == 0)
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

                    var ros2Receiver = (Ros2ReceiverState)receiver;
                    
                    builder.Append(ros2Receiver.Guid.ToString()).Append('\n');

                    builder.Append("        <b>").AppendBandwidth(receiver.BytesReceived).Append("</b> ");

                    Append(ros2Receiver.Profile);

                    builder.AppendLine();
                }

                builder.AppendLine();
            }

            foreach (var state in publisherStats)
            {
                builder.Append("<color=#800000ff><b>>> Publishing to ").Append(state.Topic)
                    .Append("</b></color>")
                    .AppendLine();

                builder.Append("<b>Type: </b>[").Append(state.Type).Append("]").AppendLine();

                builder.Append("<b>QOS: </b>");
                Append(((Ros2PublisherState)state).Profile);
                builder.AppendLine();

                (long totalMessages, long totalBytes) = state.Senders.Length == 0
                    ? (0, 0)
                    : (state.Senders[0].NumSent, state.Senders[0].BytesSent);

                builder.Append("<b>Sent ").Append(totalMessages.ToString("N0")).Append(" msgs | ")
                    .AppendBandwidth(totalBytes).Append("</b> total").AppendLine();

                if (state.Senders.Length == 0)
                {
                    builder.Append("  (No subscribers)").AppendLine().AppendLine();
                    continue;
                }

                var senders = (IReadOnlyList<Ros2SenderState>)state.Senders;

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

                    if (sender.TopicType != state.Type)
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

        static void GenerateReportRosbridge(StringBuilder builder, RosbridgeClient client,
            SubscriberState[] subscriberStates, PublisherState[] publisherStates)
        {
            foreach (var state in subscriberStates)
            {
                builder.Append("<color=#000080ff><b><< Subscribed to ")
                    .Append(state.Topic).Append("</b></color>")
                    .AppendLine();
                builder.Append("<b>Type: </b>[").Append(state.Type).Append("]").AppendLine();

                var receivers = (ReceiverState[])state.Receivers;

                if (state.Receivers.Length == 0)
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

                    builder.AppendLine();
                }

                builder.AppendLine();
            }

            foreach (var state in publisherStates)
            {
                builder.Append("<color=#800000ff><b>>> Publishing to ").Append(state.Topic)
                    .Append("</b></color>")
                    .AppendLine();
                builder.Append("<b>Type: </b>[").Append(state.Type).Append("]").AppendLine();

                var senders = state.Senders;

                if (senders.Length == 0)
                {
                    builder.Append("  (No subscribers)").AppendLine().AppendLine();
                    continue;
                }

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

                    builder.AppendLine();
                }

                builder.AppendLine();
            }
        }

        static void GenerateReportUnknown(StringBuilder builder, IRosClient client)
        {
            builder.Append("<b>Error:</b> Report generation not implemented for type ")
                .Append(client.GetType().Name)
                .Append("!");
            builder.AppendLine();
        }
    }
}