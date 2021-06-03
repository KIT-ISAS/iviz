using System;
using System.Text;
using JetBrains.Annotations;

namespace Iviz.App
{
    public interface IMarkerDialogListener
    {
        [NotNull] string Topic { get; }
        void GenerateLog([NotNull] StringBuilder description);
        [NotNull] string BriefDescription { get; }
        void Reset();
    }

    public sealed class MarkerDialogData : DialogData
    {
        [NotNull] readonly MarkerDialogContents panel;
        readonly StringBuilder description = new StringBuilder(65536);
        public override IDialogPanelContents Panel => panel;

        public MarkerDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<MarkerDialogContents>(DialogPanelType.Marker);
        }
        
        [CanBeNull] IMarkerDialogListener Listener { get; set; }

        public override void SetupPanel()
        {
            ResetPanelPosition();
            
            if (Listener == null)
            {
                throw new InvalidOperationException("Cannot setup panel without a listener!");
            }

            panel.Label.Label = $"<b>Topic:</b>: {Listener.Topic}";
            panel.Close.Clicked += Close;
            panel.ResetAll += () =>
            {
                Listener.Reset();
            };

            UpdatePanel();
        }

        public override void UpdatePanel()
        {
            if (Listener == null)
            {
                return;
            }

            description.Length = 0;
            Listener.GenerateLog(description);
            panel.Text.SetText(description);
        }

        
        public void Show([NotNull] IMarkerDialogListener listener)
        {
            Listener = listener ?? throw new ArgumentNullException(nameof(listener));
            Show();
        }        
    }
}
