#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Iviz.Displays.XR
{
    public sealed class XRShortDialog : XRDialog, IDialogWithCaption, IDialogWithTitle
    {
        [SerializeField] TMP_Text? title;
        [SerializeField] TMP_Text? caption;

        TMP_Text TitleObject => title.AssertNotNull(nameof(title));
        TMP_Text CaptionObject => caption.AssertNotNull(nameof(caption));

        public string Caption
        {
            set
            {
                CaptionObject.text = value;
                UpdateSize();
            }
        }

        public string Title
        {
            set => TitleObject.text = value;
        }

        void UpdateSize()
        {
            Span<Rect> bounds = stackalloc[]
            {
                GetTitleBounds(TitleObject),
                GetCaptionBounds(CaptionObject)
            };

            const float padding = 0.1f;
            var (center, size) = bounds.Combine(padding);

            Background.Transform.localPosition = center; // - padding / 4 * Vector2.up;
            Background.Size = size;
            SocketPosition = new Vector3(0, center.y - size.y / 2, 0);
        }
    }
}