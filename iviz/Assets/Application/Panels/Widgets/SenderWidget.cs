using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;
using UnityEngine.UI;
using Iviz.Resources;
using Iviz.Msgs;
using Iviz.Ros;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class SenderWidget : MonoBehaviour, IWidget
    {
        const int Size = 200;
        [SerializeField] Text text = null;

        [CanBeNull]
        IRosSender rosSender;

        [CanBeNull]
        IRosSender RosSender
        {
            get => rosSender;
            set
            {
                if (rosSender == null && value != null)
                {
                    GameThread.EverySecond += UpdateStats;
                }
                else if (rosSender != null && value == null)
                {
                    GameThread.EverySecond -= UpdateStats;
                }

                rosSender = value;
                if (value != null)
                {
                    UpdateStats();
                }
            }
        }

        [CanBeNull] string Topic => RosSender?.Topic;

        int NumSubscribers =>
            (!ConnectionManager.IsConnected || RosSender == null) ? -1 : RosSender.NumSubscribers;

        int MessagesPerSecond => RosSender?.Stats.MessagesPerSecond ?? 0;
        int BytesPerSecond => RosSender?.Stats.BytesPerSecond ?? 0;

        public void Set([CanBeNull] IRosSender sender)
        {
            RosSender = sender;
            if (sender == null)
            {
                text.text = $"<i>Empty</i>\n" +
                            $"<b>(?)</b>";
            }
        }

        public void Set<T>([CanBeNull] RosSender<T> sender) where T : IMessage, new()
        {
            RosSender = sender;
            if (sender == null)
            {
                string messageType = BuiltIns.GetMessageType(typeof(T));
                text.text = "<i>Empty</i>\n" +
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