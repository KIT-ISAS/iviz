#nullable enable

using Iviz.Core;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ListenerWidget : MonoBehaviour, IWidget
    {
        const int MaxTopicWidth = 220;

        [SerializeField] TMP_Text? text;
        [SerializeField] Image? panel;
        Listener? listener;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        Image Panel => panel.AssertNotNull(nameof(text));

        public Listener? Listener
        {
            private get => listener;
            set
            {
                if (listener == null && value != null)
                {
                    GameThread.EverySecond += UpdateStats;
                }
                else if (listener != null && value == null)
                {
                    GameThread.EverySecond -= UpdateStats;
                }

                listener = value;
                UpdateStats();
            }
        }

        string? Topic => Listener?.Topic;
        int MessagesPerSecond => Listener?.Stats.MessagesPerSecond ?? 0;
        long BytesPerSecond => Listener?.Stats.BytesPerSecond ?? 0;

        void UpdateStats()
        {
            if (listener == null)
            {
                Text.text = "[No Topic Set]\n" +
                            "<b>Off</b>";
                Panel.color = Resource.Colors.DisabledPanel;
                return;
            }

            using var description = BuilderPool.Rent();
            Resource.Font.Split(description, Topic ?? "", MaxTopicWidth);
            description.Append("\n<b>");

            listener.WriteDescriptionTo(description);

            description.Append(" | ")
                .Append(MessagesPerSecond)
                .Append(" msg/s | ");

            description.AppendBandwidth(BytesPerSecond);

            description.Append("/s</b>");
            Text.SetTextRent(description);

            Panel.color = listener.Subscribed ? Resource.Colors.EnabledListener : Resource.Colors.DisabledPanel;
        }

        public void OnClick()
        {
            if (listener == null)
            {
                return;
            }

            listener.SetSuspend(listener.Subscribed);
            Panel.color = listener.Subscribed ? Resource.Colors.EnabledListener : Resource.Colors.DisabledPanel;
        }

        public void ClearSubscribers()
        {
            Listener = null;
        }

        void OnDestroy()
        {
            GameThread.EverySecond -= UpdateStats;
        }
    }
}