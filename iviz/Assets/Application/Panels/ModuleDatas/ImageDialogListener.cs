#nullable enable

using UnityEngine;

namespace Iviz.App
{
    public abstract class ImageDialogListener
    {
        public ImageDialogData DialogData { get; }
        public abstract Material Material { get; }
        public abstract Vector2Int ImageSize { get; }
        
        protected ImageDialogListener()
        {
            DialogData = ModuleListPanel.Instance.CreateImageDialog(this);
            DialogData.Closed += () =>
            {
                DialogData.Dispose();
                Dispose();
            };
        }

        public abstract bool TrySampleColor(in Vector2 rawUV, out Vector2Int uv, out TextureFormat format,
            out Vector4 color);

        protected abstract void Dispose();
    }
}