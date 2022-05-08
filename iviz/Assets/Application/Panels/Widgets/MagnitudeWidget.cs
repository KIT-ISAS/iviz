#nullable enable

using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = Iviz.Msgs.GeometryMsgs.Vector3;

namespace Iviz.App
{
    public readonly struct Magnitude
    {
        public readonly string name;
        public readonly Vector3 position;

        public readonly Vector3? orientation;
        //readonly Twist? twist; // TODO

        public Magnitude(string name, in Vector3 position, Vector3? orientation = null, Twist? twist = null)
        {
            this.name = name;
            this.position = position;
            this.orientation = orientation;
            //this.twist = twist;
        }
    }

    public interface IMagnitudeDataSource : IHasFrame
    {
        Magnitude? Magnitude { get; }
    }

    public sealed class MagnitudeWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? text;
        [SerializeField] Button? button;
        IMagnitudeDataSource? dataSource;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        Button Button => button.AssertNotNull(nameof(button));

        public IMagnitudeDataSource? Owner
        {
            set
            {
                if (dataSource == null && value != null)
                {
                    GameThread.EveryTenthOfASecond += UpdateMagnitude;
                }
                else if (dataSource != null && value == null)
                {
                    GameThread.EveryTenthOfASecond -= UpdateMagnitude;
                }

                dataSource = value;
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
            if (dataSource is { Magnitude: not null } and { Frame: not null })
            {
                GuiInputModule.Instance.LookAt(
                    dataSource.Frame.Transform,
                    dataSource.Magnitude.Value.position.Ros2Unity());
            }
        }

        void UpdateMagnitude()
        {
            if (dataSource is not { Magnitude: { } magnitude })
            {
                Text.text = "<b>[no message]</b>";
                return;
            }

            using var description = BuilderPool.Rent();
            description.Append("<b>- ").Append(magnitude.name).Append(" -</b>").AppendLine();

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
                description.Append("r: ").Append(xStr).Append(" p: ").Append(yStr).Append(" y: ").Append(zStr)
                    .AppendLine();
            }

            description.Length--;
            Text.SetTextRent(description);
        }

        public void ClearSubscribers()
        {
            Owner = null;
        }
    }
}