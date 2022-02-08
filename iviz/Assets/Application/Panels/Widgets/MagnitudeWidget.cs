#nullable enable

using System.Text;
using Iviz.Common;
using Iviz.Core;
using Iviz.Tools;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Iviz.App
{
    public interface IMagnitudeUpdater
    {
        public void UpdateDescription(StringBuilder str);
    }
    
    public sealed class MagnitudeWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? text;
        [SerializeField] Button? button;
        IMagnitudeUpdater? updater;
        
        TMP_Text Text => text.AssertNotNull(nameof(text));
        Button Button => button.AssertNotNull(nameof(button));

        public IMagnitudeUpdater? MagnitudeUpdater
        {
            set
            {
                if (updater == null && value != null)
                {
                    GameThread.EveryTenthOfASecond += UpdateMagnitude;
                }
                else if (updater != null && value == null)
                {
                    GameThread.EveryTenthOfASecond -= UpdateMagnitude;
                }                
                
                updater = value;
                if (value != null)
                {
                    UpdateMagnitude();
                }
            }
        }

        void Awake()
        {
            Button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
        }

        void UpdateMagnitude()
        {
            if (updater == null)
            {
                return;
            }

            using var builder = BuilderPool.Rent();
            updater.UpdateDescription(builder);
            Text.SetText(builder);
        }

        public void ClearSubscribers()
        {
            MagnitudeUpdater = null;
        }
    }
}