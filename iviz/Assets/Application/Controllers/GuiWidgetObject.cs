#nullable enable

using System;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.ARDialogs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
    internal sealed class GuiWidgetObject
    {
        readonly FrameNode node;
        readonly ResourceKey<GameObject> resourceKey;
        readonly IDisplay display;
        float scale = 1;

        public Transform Transform => node.Transform;
        public string ParentId => node.Parent?.Id ?? TfListener.DefaultFrame.Id;
        public string Id { get; }

        public bool Visible
        {
            set => node.Visible = value;
        }

        public bool Interactable
        {
            set
            {
                if (display is IWidget widget)
                {
                    widget.Interactable = value;
                }
            }
        }

        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                Transform.localScale = Vector3.one * scale;
            }
        }

        public DateTime ExpirationTime { get; }

        public GuiWidgetObject(Widget msg, ResourceKey<GameObject> resourceKey)
        {
            this.resourceKey = resourceKey;
            node = new FrameNode(msg.Id);
            node.AttachTo(msg.Header.FrameId);
            Id = msg.Id;

            display = ResourcePool.Rent(resourceKey, node.Transform).GetComponent<IDisplay>();
            if (display is IWidgetWithCaption withCaption && msg.Caption.Length != 0)
            {
                withCaption.Caption = msg.Caption;
            }

            if (display is IWidgetWithColor withColor)
            {
                if (msg.MainColor.A != 0)
                {
                    withColor.Color = msg.MainColor.ToUnityColor();
                }

                if (msg.SecondaryColor.A != 0)
                {
                    withColor.SecondaryColor = msg.SecondaryColor.ToUnityColor();
                }
            }

            ExpirationTime = DateTime.MaxValue;
        }

        GuiWidgetObject(GuiWidgetObject source)
        {
            node = source.node;
            resourceKey = source.resourceKey;
            display = source.display;
            Id = source.Id;
            ExpirationTime = DateTime.MinValue;
        }

        public T As<T>() where T : IDisplay
        {
            return (T)display;
        }

        public void Dispose()
        {
            display.ReturnToPool(resourceKey);
            node.Dispose();
        }

        public GuiWidgetObject AsExpired() => new(this);
    }
}