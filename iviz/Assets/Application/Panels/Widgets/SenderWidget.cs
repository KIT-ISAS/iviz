using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Iviz.Resources;
using Iviz.Msgs;

namespace Iviz.App
{
    public class SenderWidget : MonoBehaviour, IWidget
    {
        const int Size = 200;
        [SerializeField] Text text = null;

        RosSender sender;
        public RosSender RosSender
        {
            get => sender;
            private set
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

        public void Set(RosSender sender)
        {
            RosSender = sender;
            if (sender == null)
            {
                text.text = $"<i>Empty</i>\n" +
                    $"<b>(?)</b>";
            }
        }

        public void Set<T>(RosSender<T> sender) where T : IMessage, new()
        {
            RosSender = sender;
            if (sender == null)
            {
                string messageType = BuiltIns.GetMessageType(typeof(T));
                text.text = $"<i>Empty</i>\n" +
                    $"<b>{messageType}</b>";
            }
        }

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
                publisherStatus = numSubscribers + "↑ ";
            }
            string messagesPerSecond = MessagesPerSecond.ToString(UnityUtils.Culture);
            string kbPerSecond = (BytesPerSecond * 0.001f).ToString("#,0.#", UnityUtils.Culture);

            text.text = $"{Resource.Font.Split(Topic ?? "", Size)}\n" +
                $"<b>Out: {publisherStatus} | {messagesPerSecond} Hz | {kbPerSecond} kB/s</b>";
        }

        public void ClearSubscribers()
        {
            RosSender = null;
        }
    }
}
