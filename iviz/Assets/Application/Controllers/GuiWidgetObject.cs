#nullable enable

using System;
using System.Numerics;
using Iviz.App.ARDialogs;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.ARDialogs;
using Iviz.Displays.XRDialogs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

// ReSharper disable ConvertIfStatementToSwitchStatement
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

            float scale = msg.Scale == 0 ? 1f : (float)msg.Scale;

            var widget = ResourcePool.Rent(resourceKey, node.Transform).GetComponent<IWidget>();
            if (widget == null)
            {
                throw new MissingAssetFieldException("Gui object does not have a widget!");
            }

            /*
            if (widget is IWidgetWithCaption withCaption && msg.Caption.Length != 0)
            {
                withCaption.Caption = msg.Caption;
            }
            */

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

            if (msg.SecondaryScale != 0 && widget is IWidgetWithScale withScale)
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

            dialog.Scale = msg.Scale == 0 ? 1f : (float)msg.Scale;
            dialog.PivotFrameId = msg.Header.FrameId;
            dialog.PivotFrameOffset = msg.TfOffset.Ros2Unity();
            dialog.PivotDisplacement = AdjustDisplacement(msg.TfDisplacement);
            dialog.DialogDisplacement = AdjustDisplacement(msg.DialogDisplacement);
            dialog.Color = msg.BackgroundColor.ToUnityColor();

            if (dialog is IDialogWithTitle withTitle)
            {
                withTitle.Title = msg.Title;
            }

            if (dialog is IDialogWithCaption withCaption)
            {
                withCaption.Caption = msg.Caption;
            }

            if (dialog is IDialogWithAlignment withAlignment)
            {
                withAlignment.CaptionAlignment = (CaptionAlignmentType)msg.CaptionAlignment;
            }
            
            if (dialog is IDialogWithIcon withIcon)
            {
                withIcon.Icon = (XRIcon)msg.Icon;
            }

            if (dialog is IDialogHasButtonSetup hasButtonSetup)
            {
                hasButtonSetup.ButtonSetup = (XRButtonSetup)msg.Buttons;
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
            Vector3 v;
            v.x = (float)displacement.X;
            v.y = (float)displacement.Y;
            v.z = -(float)displacement.Z;
            return v;
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