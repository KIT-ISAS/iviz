#nullable enable

using System;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public sealed class ImageDialogData : DialogData
    {
        readonly ImageDialogPanel panel;
        readonly GameObject canvas;
        readonly ImageDialogListener listener;
        string title = "";
        string lastSample = "";

        public override IDialogPanel Panel => panel;
        public event Action? Closed;

        public string Title
        {
            set
            {
                title = value;
                UpdateTitle();
            }
        }

        public ImageDialogData(ImageDialogListener listener, Transform? holder)
        {
            this.listener = listener ?? throw new ArgumentNullException(nameof(listener));
            canvas = ResourcePool.Rent(Resource.Widgets.ImageCanvas, holder);
            if (Settings.IsXR)
            {
                canvas.ProcessCanvasForXR();
            }

            panel = canvas.GetComponentInChildren<ImageDialogPanel>();
            panel.Closed += () => Closed?.Invoke();


            panel.Clicked += rawUV =>
            {
                lastSample = GetSampleText(rawUV);
                UpdateTitle();
            };
        }

        string GetSampleText(in Vector2 rawUV)
        {
            if (!listener.TrySampleColor(rawUV, out var uv, out var format, out var color))
            {
                return "";
            }

            return $"<b>u: </b>{FormatInt(uv.x)} <b>v: </b>{FormatInt(uv.y)} <b>val: </b>" + format switch
            {
                TextureFormat.R8 or TextureFormat.R16 => FormatInt((int) color.x),
                TextureFormat.RFloat => FormatFloat(color.x),
                TextureFormat.RGB24 => $"[{FormatFloat(color.x)}, {FormatFloat(color.y)}, {FormatFloat(color.z)}]",
                TextureFormat.RGBA32 =>
                    $"[{FormatFloat(color.x)}, {FormatFloat(color.y)}, {FormatFloat(color.z)}, {FormatFloat(color.w)}]",
                _ => ""
            };

            static string FormatInt(int f) => f.ToString("#,0", BuiltIns.Culture);
            static string FormatFloat(float f) => f.ToString("#,0.###", BuiltIns.Culture);
        }


        public override void SetupPanel()
        {
            panel.Material = listener.Material;
            panel.ImageSize = listener.ImageSize;
        }

        public void ToggleImageEnabled()
        {
            panel.ToggleImageEnabled();
            panel.ImageSize = listener.ImageSize;
        }

        void UpdateTitle()
        {
            panel.Title = lastSample.Length != 0 ? 
                $"<b>{title}</b>\n{lastSample}" : 
                $"<b>{title}</b>";
        }

        public override void Dispose()
        {
            Closed = null;
            panel.ClearSubscribers();
            ModuleListPanel.Instance.DisposeImageDialog(this);
            ResourcePool.Return(Resource.Widgets.ImageCanvas, canvas);
        }
    }
}