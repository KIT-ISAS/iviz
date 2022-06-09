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
        public readonly Vector3 referencePoint;
        public readonly Vector3 position;
        public readonly Vector3? orientation;
        public readonly string? childFrame;

        public Magnitude(string name, in Vector3 position, Vector3? orientation = null, string? childFrame = null,
            Vector3 referencePoint = default)
        {
            this.name = name;
            this.referencePoint = referencePoint;
            this.position = position;
            this.orientation = orientation;
            this.childFrame = childFrame;
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
            if (dataSource?.Magnitude is not { } magnitude)
            {
                return;
            }

            TfFrame frame;
            if (magnitude.childFrame is { } childFrameId
                && TfModule.TryGetFrame(childFrameId, out var childFrame))
            {
                frame = childFrame;
            }
            else if (dataSource.Frame != null)
            {
                frame = dataSource.Frame;
            }
            else
            {
                return;
            }

            GuiInputModule.Instance.LookAt(frame.Transform, magnitude.referencePoint.Ros2Unity());
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
                string xStr = (x == 0) ? "0" : UnityUtils.FormatFloat(x);
                string yStr = (y == 0) ? "0" : UnityUtils.FormatFloat(y);
                string zStr = (z == 0) ? "0" : UnityUtils.FormatFloat(z);
                description.Append(xStr).Append(", ").Append(yStr).Append(", ").Append(zStr).AppendLine();
            }

            if (magnitude.orientation is { } orientation)
            {
                var (x, y, z) = orientation;
                string xStr = (x == 0) ? "0" : UnityUtils.FormatFloat(x);
                string yStr = (y == 0) ? "0" : UnityUtils.FormatFloat(y);
                string zStr = (z == 0) ? "0" : UnityUtils.FormatFloat(z);
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