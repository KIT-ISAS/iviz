using System.Text;
using Iviz.Core;
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
        static readonly StringBuilder CachedStr = new StringBuilder(100);

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
                UpdateStats();
            }
        }

        [CanBeNull] string Topic => Listener?.Topic;

        (int Active, int Total) NumPublishers =>
            (!ConnectionManager.IsConnected || Listener == null) ? (-1, -1) : Listener.NumPublishers;

        int MessagesPerSecond => Listener?.Stats.MessagesPerSecond ?? 0;
        long BytesPerSecond => Listener?.Stats.BytesPerSecond ?? 0; 
        int Dropped => Listener?.Stats.Dropped ?? 0;
        bool Subscribed => Listener?.Subscribed ?? false;


        void UpdateStats()
        {
            if (listener == null)
            {
                text.text = "[No Topic Set]\n<b>Off</b>";
                panel.color = Resource.Colors.DisabledPanelColor;
                return;
            }
            
            
            CachedStr.Length = 0;
            CachedStr.Append(Resource.Font.Split(Topic ?? "", Size)).Append("\n<b>");

            listener.WriteDescriptionTo(CachedStr);

            string kbPerSecond = (BytesPerSecond * 0.001f).ToString("#,0.#", UnityUtils.Culture);
            CachedStr.Append(" | ")
                .Append(MessagesPerSecond)
                .Append(" Hz | ")
                .Append(kbPerSecond)
                .Append(" kB/s</b>");

            text.text = CachedStr.ToString();

            panel.color = listener.Subscribed ? EnabledColor : Resource.Colors.DisabledPanelColor;
        }

        public void OnClick()
        {
            if (listener == null)
            {
                return;
            }

            listener.SetSuspend(listener.Subscribed);
            panel.color = listener.Subscribed ? EnabledColor : Resource.Colors.DisabledPanelColor;
        }

        public void ClearSubscribers()
        {
            Listener = null;
        }
    }
}