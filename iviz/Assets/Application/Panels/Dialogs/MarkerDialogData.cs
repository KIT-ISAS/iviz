#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

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

            const int maxLabelWidth = 300;
            panel.Label.Text = Resource.Font.Split(listener.Topic, maxLabelWidth);
            panel.Close.Clicked += Close;
            panel.ResetAll += listener.ResetController;
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
            
            const int maxToDisplay = 50;

            using var description = BuilderPool.Rent();
            listener.GenerateLog(description, 0, Mathf.Min(listener.NumEntriesForLog, maxToDisplay));
            panel.Text.SetTextRent(description);
        }


        public void Show(IMarkerDialogListener newListener)
        {
            ThrowHelper.ThrowIfNull(newListener, nameof(newListener));
            listener = newListener;
            Show();
        }
    }
}