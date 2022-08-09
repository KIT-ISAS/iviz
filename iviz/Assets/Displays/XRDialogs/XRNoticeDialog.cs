#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRNoticeDialog : XRDialog, IDialogWithCaption, IDialogWithIcon
    {
        [SerializeField] TMP_Text? caption;
        [SerializeField] XRIconPlane? iconObject;

        TMP_Text CaptionObject => caption.AssertNotNull(nameof(caption));
        XRIconPlane IconObject => iconObject.AssertNotNull(nameof(iconObject));

        public string Caption
        {
            set
            {
                CaptionObject.text = value;
                UpdateSize();
            }
        }

        public XRIcon Icon
        {
            set => IconObject.Icon = value;
        }

        void UpdateSize()
        {
            Span<Rect> bounds = stackalloc[]
            {
                GetIconBounds(IconObject),
                GetCaptionBounds(CaptionObject),
            };
            
            const float padding = 0.15f;
            var (center, size) = bounds.Combine(padding);

            Background.Transform.localPosition = center;
            Background.Size = size;
            SocketPosition = new Vector3(0, center.y - size.y / 2, 0);
        }
    }
}