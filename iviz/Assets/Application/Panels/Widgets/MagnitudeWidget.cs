#nullable enable

using Iviz.Core;
using Iviz.Msgs;
using Iviz.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public struct Magnitude
    {
        public string? name;
        public Msgs.GeometryMsgs.Vector3 position;
        public Msgs.GeometryMsgs.Vector3? orientation;
        public Msgs.GeometryMsgs.Twist? twist;
    }

    public interface IMagnitudeUpdater
    {
        public Magnitude? Magnitude { get; }
    }

    public sealed class MagnitudeWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? text;
        [SerializeField] Button? button;
        IMagnitudeUpdater? updater;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        Button Button => button.AssertNotNull(nameof(button));

        public IMagnitudeUpdater? Owner
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
            if (updater is not { Magnitude: { } magnitude })
            {
                Text.text = "<b>[no message]</b>";
                return;
            }
            
            using var description = BuilderPool.Rent();
            if (magnitude.name != null)
            {
                description.Append("<b>- ").Append(magnitude.name).Append(" -</b>").AppendLine();
            }

            {
                var (x, y, z) = magnitude.position;
                const string format = "#,0.###";
                string xStr = (x == 0) ? "0" : x.ToString(format, UnityUtils.Culture);
                string yStr = (y == 0) ? "0" : y.ToString(format, UnityUtils.Culture);
                string zStr = (z == 0) ? "0" : z.ToString(format, UnityUtils.Culture);
                description.Append(xStr).Append(", ").Append(yStr).Append(", ").Append(zStr).AppendLine();
            }

            if (magnitude.orientation is { } orientation)
            {
                var (x, y, z) = orientation;
                const string format = "#,0.###";
                string xStr = (x == 0) ? "0" : x.ToString(format, UnityUtils.Culture);
                string yStr = (y == 0) ? "0" : y.ToString(format, UnityUtils.Culture);
                string zStr = (z == 0) ? "0" : z.ToString(format, UnityUtils.Culture);
                description.Append("r: ").Append(xStr).Append(" p: ").Append(yStr).Append(" y: ").Append(zStr).AppendLine();
            }

            description.Length--;
            Text.SetText(description);
        }

        public void ClearSubscribers()
        {
            Owner = null;
        }
    }
}