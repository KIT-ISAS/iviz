using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Iviz.App
{
    public class SenderWidget : MonoBehaviour, IWidget
    {
        const int Size = 30;
        [SerializeField] Text text;

        RosSender sender;
        public RosSender RosSender
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
                if (value != null)
                {
                    UpdateStats();
                }
            }
        }

        public string Topic => RosSender?.Topic;
        public int NumSubscribers => (!ConnectionManager.Connected || RosSender == null) ? -1 : RosSender.NumSubscribers;
        public int MessagesPerSecond => RosSender?.Stats.MessagesPerSecond ?? 0;
        public int BytesPerSecond => RosSender?.Stats.BytesPerSecond ?? 0;

        void UpdateStats()
        {
            string publisherStatus;
            int numSubscribers = NumSubscribers;
            if (numSubscribers == -1)
            {
                publisherStatus = "Off";
            }
            else
            {
                publisherStatus = "On ← " + numSubscribers;
            }
            string messagesPerSecond = MessagesPerSecond.ToString(UnityUtils.Culture);
            string kbPerSecond = (BytesPerSecond * 0.001f).ToString("#,0.#", UnityUtils.Culture);

            text.text = $"{RosUtils.SanitizedText(Topic ?? "", Size)}\n" +
                $"<b>Out: {publisherStatus} | {messagesPerSecond} Hz | {kbPerSecond} kB/s</b>";
        }

        public void ClearSubscribers()
        {
            RosSender = null;
        }
    }
}
