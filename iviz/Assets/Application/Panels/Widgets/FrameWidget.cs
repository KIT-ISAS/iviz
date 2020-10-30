using Iviz.Controllers;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class FrameWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text text = null;

        TfFrame frame;
        IHasFrame owner;

        [CanBeNull]
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

        [CanBeNull]
        public TfFrame Frame
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
                    text.text = "<b>⮑" + frame.Id + "</b>";
                    UpdateStats();
                }
                else
                {
                    text.text = "<i>⮑ (none)</i>";
                }
            }
        }

        void Awake()
        {
            Frame = null;
        }

        public void ClearSubscribers()
        {
            Owner = null;
            Frame = null;
        }

        void UpdateStats()
        {
            Frame = Owner?.Frame;
        }

        public void OnGotoClick()
        {
            if (Frame != null)
            {
                TfListener.GuiCamera.LookAt(Frame.WorldPose.position);
            }
        }

        public void OnTrailClick()
        {
        }
    }
}