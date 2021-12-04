#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.App
{
    public sealed class MarkerDialogData : DialogData
    {
        readonly MarkerDialogContents panel;
        public override IDialogPanelContents Panel => panel;
        IMarkerDialogListener? Listener { get; set; }

        public MarkerDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<MarkerDialogContents>(DialogPanelType.Marker);
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();

            if (Listener == null)
            {
                throw new InvalidOperationException("Cannot setup panel without a listener!");
            }

            panel.Label.Text = $"<b>Topic:</b> {Listener.Topic}";
            panel.Close.Clicked += Close;
            panel.ResetAll += Listener.Reset;
            panel.LinkClicked += markerId => HighlightMarker(Listener, markerId);

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
            if (Listener == null)
            {
                return;
            }

            using var description = BuilderPool.Rent();
            Listener.GenerateLog(description);
            panel.Text.SetText(description);
        }


        public void Show(IMarkerDialogListener listener)
        {
            Listener = listener ?? throw new ArgumentNullException(nameof(listener));
            Show();
        }
    }
}