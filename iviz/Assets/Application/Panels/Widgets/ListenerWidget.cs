using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;
using UnityEngine.UI;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class ListenerWidget : MonoBehaviour, IWidget
    {
        const int Size = 200;

        static readonly Color EnabledColor = new Color(0.71f, 0.98f, 1, 0.733f);

        [SerializeField] Text text = null;
        [SerializeField] Image panel = null;
        [CanBeNull] IListener listener;

        [CanBeNull]
        public IListener Listener
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
                if (value != null)
                {
                    UpdateStats();
                }
            }
        }

        [CanBeNull] string Topic => Listener?.Topic;
        int NumPublishers => (!ConnectionManager.IsConnected || Listener == null) ? -1 : Listener.NumPublishers;
        int MessagesPerSecond => Listener?.Stats.MessagesPerSecond ?? 0;
        int BytesPerSecond => Listener?.Stats.BytesPerSecond ?? 0;
        int Dropped => Listener?.Stats.Dropped ?? 0;
        bool Subscribed => Listener?.Subscribed ?? false;

        void UpdateStats()
        {
            string subscriberStatus;
            int numPublishers = NumPublishers;
            if (numPublishers == -1)
            {
                subscriberStatus = "Off";
            }
            else if (!Subscribed)
            {
                subscriberStatus = "PAUSED";
            }
            else
            {
                subscriberStatus = $"{numPublishers.ToString()}↓ ";
            }

            string messagesPerSecond = MessagesPerSecond.ToString(UnityUtils.Culture);
            string kbPerSecond = (BytesPerSecond * 0.001f).ToString("#,0.#", UnityUtils.Culture);
            string dropped = Dropped.ToString(UnityUtils.Culture);

            text.text = $"{Resource.Font.Split(Topic ?? "", Size)}\n" +
                        $"<b>{subscriberStatus} | {messagesPerSecond} Hz | {kbPerSecond} kB/s | {dropped} drop</b>";

            if (listener == null)
            {
                panel.color = EnabledColor;
                return;
            }

            panel.color = listener.Subscribed ? EnabledColor : Resource.Colors.DisabledPanelColor;
        }

        public void OnClick()
        {
            if (listener == null)
            {
                return;
            }

            if (listener.Subscribed)
            {
                listener.Pause();
            }
            else
            {
                listener.Unpause();
            }

            panel.color = listener.Subscribed ? EnabledColor : Resource.Colors.DisabledPanelColor;
        }

        public void ClearSubscribers()
        {
            Listener = null;
        }
    }
}