using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ListenerWidget : MonoBehaviour, IWidget
    {
        const int Size = 30;

        [SerializeField] Text text;

        RosListener listener;
        public RosListener RosListener
        {
            get => listener;
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

        public string Topic => RosListener?.Topic;
        public int NumPublishers => (!ConnectionManager.Connected || RosListener == null) ? -1 : RosListener.NumPublishers;
        public int MessagesPerSecond => RosListener?.Stats.MessagesPerSecond ?? 0;
        public int BytesPerSecond => RosListener?.Stats.BytesPerSecond ?? 0;

        void UpdateStats()
        {
            string subscriberStatus;
            int numPublishers = NumPublishers;
            if (numPublishers == -1)
            {
                subscriberStatus = "Off";
            }
            else
            {
                subscriberStatus = "On → " + numPublishers;
            }
            string messagesPerSecond = MessagesPerSecond.ToString(UnityUtils.Culture);
            string kbPerSecond = (BytesPerSecond * 0.001f).ToString("#,0.#", UnityUtils.Culture);

            text.text = $"{UnityUtils.SanitizedText(Topic ?? "", Size)}\n" +
                $"<b>In: {subscriberStatus} | {messagesPerSecond} Hz | {kbPerSecond} kB/s</b>";
        }

        public void ClearSubscribers()
        {
            RosListener = null;
        }
    }
}
