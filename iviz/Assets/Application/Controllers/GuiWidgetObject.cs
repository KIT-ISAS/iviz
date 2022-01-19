#nullable enable

using System;
using Iviz.App.ARDialogs;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.ARDialogs;
using Iviz.Displays.XRDialogs;
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

        public string ParentId => node.Parent?.Id ?? TfListener.DefaultFrame.Id;
        public string Id { get; }
        public DateTime ExpirationTime { get; }

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


        public GuiWidgetObject(GuiWidgetListener parent, Widget msg, ResourceKey<GameObject> resourceKey)
        {
            this.resourceKey = resourceKey;
            node = new FrameNode(msg.Id);
            node.AttachTo(msg.Header.FrameId);
            Id = msg.Id;
            ExpirationTime = DateTime.MaxValue;

            float scale = (float)msg.Scale;

            var widget = ResourcePool.Rent(resourceKey, node.Transform).GetComponent<IWidget>();
            if (widget == null)
            {
                throw new MissingAssetFieldException("Gui object does not have a widget!");
            }

            if (widget is IWidgetWithCaption withCaption && msg.Caption.Length != 0)
            {
                withCaption.Caption = msg.Caption;
            }

            if (widget is IWidgetWithColor withColor)
            {
                if (msg.Color.A != 0)
                {
                    withColor.Color = msg.Color.ToUnityColor();
                }

                if (msg.SecondaryColor.A != 0)
                {
                    withColor.SecondaryColor = msg.SecondaryColor.ToUnityColor();
                }
            }

            if (widget is IWidgetWithScale withScale && msg.SecondaryScale != 0)
            {
                withScale.SecondaryScale = (float)msg.SecondaryScale;
            }

            if (widget is IWidgetCanBeMoved canBeMoved)
            {
                canBeMoved.Moved += direction => parent.OnWidgetMoved(this, direction * scale);
            }

            if (widget is IWidgetCanBeRotated canBeRotated)
            {
                canBeRotated.Moved += angle => parent.OnWidgetRotated(this, angle);
            }

            display = widget;

            var transform = node.Transform;
            transform.SetLocalPose(msg.Pose.Ros2Unity());
            transform.localScale = Vector3.one * scale;
        }

        public GuiWidgetObject(GuiWidgetListener parent, Dialog msg, ResourceKey<GameObject> resourceKey)
        {
            this.resourceKey = resourceKey;
            node = new FrameNode(msg.Id);
            node.AttachTo(msg.Header.FrameId);
            Id = msg.Id;

            var dialog = ResourcePool.Rent(resourceKey, node.Transform).GetComponent<IDialog>();
            if (display == null)
            {
                throw new MissingAssetFieldException("Gui object does not have a dialog!");
            }

            dialog.Scale = (float)msg.Scale;
            dialog.PivotFrameId = msg.Header.FrameId;
            dialog.PivotFrameOffset = msg.TfOffset.Ros2Unity();
            dialog.PivotDisplacement = AdjustDisplacement(msg.TfDisplacement);
            dialog.DialogDisplacement = AdjustDisplacement(msg.DialogDisplacement);
            dialog.Color = msg.BackgroundColor.ToUnityColor();

            if (dialog is IDialogWithCaption withCaption)
            {
                withCaption.Caption = msg.Caption;
            }

            if (dialog is IDialogWithIcon withIcon)
            {
                withIcon.Icon = (XRButtonIcon)msg.Icon;
            }

            dialog.Expired += () => parent.OnDialogExpired(this);

            if (dialog is IDialogCanBeClicked canBeClicked)
            {
                canBeClicked.Clicked += index => parent.OnDialogButtonClicked(this, index);
            }
            
            dialog.Initialize();

            ExpirationTime = msg.Lifetime == default
                ? DateTime.MaxValue
                : GameThread.Now + msg.Lifetime.ToTimeSpan();

            display = dialog;
        }

        static Vector3 AdjustDisplacement(in Msgs.GeometryMsgs.Vector3 displacement)
        {
            var (x, y, z) = displacement.ToUnity();
            return new Vector3(x, y, -z);
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