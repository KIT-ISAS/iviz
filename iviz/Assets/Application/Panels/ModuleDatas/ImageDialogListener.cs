using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public abstract class ImageDialogListener 
    {
        [NotNull] public ImageDialogData DialogData { get; }

        protected ImageDialogListener()
        {
            DialogData = ModuleListPanel.Instance.CreateImageDialog(this);
            DialogData.Closed += () =>
            {
                DialogData.Stop();
                Stop();
            };
        }

        [NotNull] public abstract Material Material { get; }
        public abstract Vector2Int ImageSize { get; }
        protected abstract void Stop();
    }
}