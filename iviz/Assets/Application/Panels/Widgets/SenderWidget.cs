using Iviz.Core;
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
        [SerializeField] Image panel = null;

        [CanBeNull] ISender sender;

        [CanBeNull]
        ISender Sender
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
                panel.color = value != null 
                    ? Resource.Colors.EnabledSender 
                    : Resource.Colors.DisabledPanel;
                
                if (value != null)
                {
                    UpdateStats();
                }
            }
        }

        [CanBeNull] string Topic => Sender?.Topic;

        int NumSubscribers =>
            (!ConnectionManager.IsConnected || Sender == null) ? -1 : Sender.NumSubscribers;

        int MessagesPerSecond => Sender?.Stats.MessagesPerSecond ?? 0;
        int BytesPerSecond => Sender?.Stats.BytesPerSecond ?? 0;

        public void Set([CanBeNull] ISender newSender)
        {
            Sender = newSender;
            if (newSender == null)
            {
                text.text = $"<i>Empty</i>\n" +
                            $"<b>(?)</b>";
            }
        }

        public void Set<T>([CanBeNull] Sender<T> newSender) where T : IMessage, new()
        {
            Sender = newSender;
            if (newSender == null)
            {
                text.text = "<i>Empty</i>\n" +
                            $"<b>{BuiltIns.GetMessageType(typeof(T))}</b>";
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
                publisherStatus = numSubscribers.ToString() + "↑ ";
            }

            string messagesPerSecond = MessagesPerSecond.ToString(UnityUtils.Culture);
            string kbPerSecond = (BytesPerSecond * 0.001f).ToString("#,0.#", UnityUtils.Culture);

            text.text = $"{Resource.Font.Split(Topic ?? "", Size)}\n" +
                        $"<b>Out: {publisherStatus} | {messagesPerSecond} Hz | {kbPerSecond} kB/s</b>";
        }

        public void ClearSubscribers()
        {
            Sender = null;
        }
    }
}