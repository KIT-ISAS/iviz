#nullable enable

using System;
using System.Text;
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
        const int MaxEntriesToDisplay = 50;

        readonly MarkerDialogPanel panel;
        IMarkerDialogListener? listener;
        int minIndex;
        uint? textHash;

        public override IDialogPanel Panel => panel;

        public MarkerDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<MarkerDialogPanel>(DialogPanelType.Marker);
        }

        public override void SetupPanel()
        {
            if (listener == null)
            {
                return;
            }

            ResetPanelPosition();

            panel.Title = listener.Title;
            
            const int maxLabelWidth = 300;
            using (var description = BuilderPool.Rent())
            {
                Resource.Font.Split(description, listener.Topic, maxLabelWidth);
                panel.Subtitle.SetText(description);
            }

            panel.Close.Clicked += Close;
            panel.ResetAll += listener.ResetController;
            panel.LinkClicked += markerId => HighlightMarker(listener, markerId);
            panel.Flipped += OnFlipped;

            textHash = null;
            UpdatePanel();
            UpdateIndex();
        }

        void OnFlipped(int sign)
        {
            if (listener == null)
            {
                return;
            }

            int numEntries = listener.NumEntriesForLog;

            switch (sign)
            {
                case -1 when minIndex == 0:
                    break;
                case -1:
                    int maxPossibleIndex = numEntries / MaxEntriesToDisplay * MaxEntriesToDisplay;
                    minIndex = Mathf.Min(minIndex - MaxEntriesToDisplay, maxPossibleIndex);
                    UpdatePanel();
                    break;
                case 1 when minIndex + MaxEntriesToDisplay >= numEntries:
                    break;
                case 1:
                    minIndex += MaxEntriesToDisplay;
                    UpdatePanel();
                    break;
            }

            UpdateIndex();
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

            int numEntries = listener.NumEntriesForLog;
            int currentMaxIndex = minIndex + MaxEntriesToDisplay;

            using var description = BuilderPool.Rent();
            int maxIndex = Mathf.Min(currentMaxIndex, numEntries);
            listener.GenerateLog(description, minIndex, maxIndex - minIndex);

            uint newHash = HashCalculator.Compute(description);
            if (newHash == textHash)
            {
                return;
            }

            textHash = newHash;
            panel.Text.SetTextRent(description);
        }

        void UpdateIndex()
        {
            if (listener == null)
            {
                return;
            }

            int numEntries = listener.NumEntriesForLog;
            int currentMaxIndex = minIndex + MaxEntriesToDisplay;

            int leftMinIndex = minIndex - MaxEntriesToDisplay;
            int leftMaxIndex = minIndex;
            panel.LeftCaption = (minIndex == 0) ? "" : $"< {leftMinIndex.ToString()}-{leftMaxIndex.ToString()}";

            int rightMinIndex = currentMaxIndex;
            int rightMaxIndex = Mathf.Min(currentMaxIndex + MaxEntriesToDisplay, numEntries);
            panel.RightCaption =
                (rightMaxIndex == numEntries) ? "" : $"{rightMinIndex.ToString()}-{rightMaxIndex.ToString()} >";
        }


        public void Show(IMarkerDialogListener newListener)
        {
            ThrowHelper.ThrowIfNull(newListener, nameof(newListener));
            listener = newListener;
            Show();
        }
    }
}