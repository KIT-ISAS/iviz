using UnityEngine;
using System.Collections;
using Iviz.App.Listeners;
using UnityEngine.UI;

namespace Iviz.App
{
    public class FrameWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text text;

        IHasFrame owner;
        public IHasFrame Owner
        {
            get => owner;
            set
            {
                if (owner == null && value != null)
                {
                    GameThread.EverySecond += UpdateStats;
                }
                else if (owner != null && value == null)
                {
                    GameThread.EverySecond -= UpdateStats;
                }

                owner = value;
                Frame = owner?.Frame;
            }
        }

        TFFrame frame;
        public TFFrame Frame
        {
            get => frame;
            private set
            {
                if (frame == value)
                {
                    return;
                }
                frame = value;
                if (frame != null)
                {
                    text.text = "<b>" + frame.Id + "</b>";
                    UpdateStats();
                }
                else
                {
                    text.text = "<i>(none)</i>";
                }
            }
        }

        void Awake()
        {
            Frame = null;
        }

        void UpdateStats()
        {
            Frame = Owner?.Frame;
        }

        public void ClearSubscribers()
        {
            Owner = null;
            Frame = null;
        }

        public void OnGotoClick()
        {
            if (Frame != null)
            {
                TFListener.GuiManager.LookAt(Frame.WorldPose.position);
            }
        }

        public void OnTrailClick()
        {

        }

    }
}
