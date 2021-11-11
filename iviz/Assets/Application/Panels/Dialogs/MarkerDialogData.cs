#nullable  enable

using System;
using System.Text;
using Iviz.Tools;

namespace Iviz.App
{
    public interface IMarkerDialogListener
    {
        string Topic { get; }
        void GenerateLog(StringBuilder description);
        string BriefDescription { get; }
        void Reset();
    }

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

            panel.Label.Text = $"<b>Topic:</b>: {Listener.Topic}";
            panel.Close.Clicked += Close;
            panel.ResetAll += Listener.Reset;

            UpdatePanel();
        }

        public override void UpdatePanel()
        {
            if (Listener == null)
            {
                return;
            }

            var description = BuilderPool.Rent();
            try
            {
                Listener.GenerateLog(description);
                panel.Text.SetText(description);
            }
            finally
            {
                BuilderPool.Return(description);
            }
        }


        public void Show(IMarkerDialogListener listener)
        {
            Listener = listener ?? throw new ArgumentNullException(nameof(listener));
            Show();
        }
    }
}