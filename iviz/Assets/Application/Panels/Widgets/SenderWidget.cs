#nullable enable

using Iviz.Core;
using Iviz.Msgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SenderWidget : MonoBehaviour, IWidget
    {
        const int MaxTopicWidth = 220;

        [SerializeField] TMP_Text? text;
        [SerializeField] Image? panel;

        ISender? sender;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        Image Panel => panel.AssertNotNull(nameof(panel));
        string? Topic => Sender?.Topic;

        ISender? Sender
        {
            get => sender;
            set
            {
                if (sender == null && value != null)
                {
                    GameThread.EverySecond += UpdateStats;
                }
                else if (sender != null && value == null)
                {
                    GameThread.EverySecond -= UpdateStats;
                }

                sender = value;
                UpdateStats();
            }
        }

        int NumSubscribers =>
            (!RosManager.IsConnected || Sender == null) ? -1 : Sender.NumSubscribers;

        int MessagesPerSecond => Sender?.Stats.MessagesPerSecond ?? 0;
        long BytesPerSecond => Sender?.Stats.BytesPerSecond ?? 0;

        public void Set(ISender? newSender)
        {
            Sender = newSender;
            if (newSender == null)
            {
                Text.text = "<i>Empty</i>\n" +
                            "<b>Off</b>";
            }
        }

        public void Set<T>(Sender<T>? newSender) where T : IMessage, new()
        {
            Sender = newSender;
            if (newSender == null)
            {
                Text.text = "<i>Empty</i>\n"  +
                            $"<b>{BuiltIns.GetMessageType<T>()}</b>";
            }
        }

        void UpdateStats()
        {
            if (sender == null)
            {
                Text.text = "[No Topic Set]\n" +
                            "<b>Off</b>";
                Panel.color = Resource.Colors.DisabledPanel;
                return;
            }

            using var description = BuilderPool.Rent();
            description
                .Append(Resource.Font.Split(Topic ?? "", MaxTopicWidth))
                .Append("\n<b>");

            sender.WriteDescriptionTo(description);

            description.Append(" | ")
                .Append(MessagesPerSecond)
                .Append(" Hz | ");

            RosUtils.AppendBandwidth(description, BytesPerSecond);

            description.Append("/s</b>");

            Text.SetTextRent(description);

            Panel.color = Resource.Colors.EnabledSender;
        }

        public void ClearSubscribers()
        {
            Sender = null;
        }
    }
}