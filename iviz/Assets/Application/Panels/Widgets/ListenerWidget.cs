using System.Text;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class ListenerWidget : MonoBehaviour, IWidget
    {
        const int Size = 200;

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
        int MessagesPerSecond => Listener?.Stats.MessagesPerSecond ?? 0;
        long BytesPerSecond => Listener?.Stats.BytesPerSecond ?? 0;

        void UpdateStats()
        {
            if (listener == null)
            {
                text.text = "[No Topic Set]\n<b>Off</b>";
                panel.color = Resource.Colors.DisabledPanel;
                return;
            }
            
            var description = BuilderPool.Rent();
            try
            {
                description.Append(Resource.Font.Split(Topic ?? "", Size)).Append("\n<b>");

                listener.WriteDescriptionTo(description);

                string kbPerSecond = (BytesPerSecond * 0.001f).ToString("#,0.#", UnityUtils.Culture);
                description.Append(" | ")
                    .Append(MessagesPerSecond)
                    .Append(" Hz | ")
                    .Append(kbPerSecond)
                    .Append(" kB/s</b>");

                text.text = description.ToString();
            }
            finally
            {
                BuilderPool.Return(description);
            }

            panel.color = listener.Subscribed ? Resource.Colors.EnabledListener : Resource.Colors.DisabledPanel;
        }

        public void OnClick()
        {
            if (listener == null)
            {
                return;
            }

            listener.SetSuspend(listener.Subscribed);
            panel.color = listener.Subscribed ? Resource.Colors.EnabledListener : Resource.Colors.DisabledPanel;
        }

        public void ClearSubscribers()
        {
            Listener = null;
        }
    }
}