using Iviz.Controllers;
using Iviz.Displays;
using UnityEngine;
using UnityEngine.UI;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class ListenerWidget : MonoBehaviour, IWidget
    {
        const int Size = 200;

        [SerializeField] Text text = null;
        [SerializeField] Image panel = null;
        [CanBeNull] IRosListener listener;
        
        [CanBeNull]
        public IRosListener RosListener
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

        [CanBeNull] string Topic => RosListener?.Topic;
        int NumPublishers => (!ConnectionManager.IsConnected || RosListener == null) ? -1 : RosListener.NumPublishers;
        int MessagesPerSecond => RosListener?.Stats.MessagesPerSecond ?? 0;
        int BytesPerSecond => RosListener?.Stats.BytesPerSecond ?? 0;
        int Dropped => RosListener?.Stats.Dropped ?? 0;
        bool Subscribed => RosListener?.Subscribed ?? false;

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
                panel.color = Resource.Colors.DisabledPanelColor;
            }
            else
            {
                listener.Unpause();
                panel.color = new Color(0.71f, 0.98f, 1, 0.733f);
            }
        }

        public void ClearSubscribers()
        {
            RosListener = null;
        }
    }
}
