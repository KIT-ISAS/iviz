using UnityEngine;
using System.Collections;
using Iviz.App.Listeners;
using UnityEngine.UI;

namespace Iviz.App
{
    public class FrameWidget : MonoBehaviour, IWidget
    {
        public Text text;

        string frameId;
        TFFrame frame;
        public TFFrame Frame
        {
            get => frame;
            set
            {
                if (frame == null && value != null)
                {
                    GameThread.EverySecond += UpdateStats;
                }
                else if (frame != null && value == null)
                {
                    GameThread.EverySecond -= UpdateStats;
                }

                frame = value;
                if (frame != null)
                {
                    frameId = value.Id;
                    text.text = "<b>►</b>" + frameId;
                    UpdateStats();
                }
                else
                {
                    frameId = null;
                }
            }
        }

        void UpdateStats()
        {
            if (Frame == null)
            {
                return;
            }
            
            if (Frame.Id != frameId) // disposed
            {
                Frame = null;
            }
        }

        public void ClearSubscribers()
        {
            Frame = null;
        }

        public void OnGotoClick()
        {
        }

        public void OnTrailClick()
        {

        }

    }
}
