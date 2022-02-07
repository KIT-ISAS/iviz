#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using Iviz.Tools;

namespace Iviz.App
{
    public sealed class MarkerDialogData : DialogData
    {
        readonly MarkerDialogPanel panel;
        IMarkerDialogListener? listener;
        public override IDialogPanel Panel => panel;

        public MarkerDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<MarkerDialogPanel>(DialogPanelType.Marker);
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();

            if (listener == null)
            {
                throw new InvalidOperationException("Cannot setup panel without a listener!");
            }

            panel.Label.Text = $"<b>Topic:</b> {listener.Topic}";
            panel.Close.Clicked += Close;
            panel.ResetAll += listener.Reset;
            panel.LinkClicked += markerId => HighlightMarker(listener, markerId);

            UpdatePanel();
        }

        static void HighlightMarker(IMarkerDialogListener listener, string markerId)
        {
            if (!listener.TryGetBoundsFromId(markerId, out var bounds)
                || bounds.BoundsTransform is not { } transform)
            {
                return;
            }

            GuiInputModule.Instance.LookAt(transform);
            if (bounds.AcceptsHighlighter)
            {
                FAnimator.Start(new BoundsHighlighter(bounds));
            }
        }

        public override void UpdatePanel()
        {
            if (listener == null)
            {
                return;
            }

            using var description = BuilderPool.Rent();
            listener.GenerateLog(description);
            panel.Text.SetText(description);
        }


        public void Show(IMarkerDialogListener newListener)
        {
            listener = newListener ?? throw new ArgumentNullException(nameof(newListener));
            Show();
        }
    }
}