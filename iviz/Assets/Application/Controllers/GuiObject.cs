#nullable enable

using System;
using System.Linq;
using System.Text;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

// ReSharper disable ConvertIfStatementToSwitchStatement
namespace Iviz.Controllers
{
    internal sealed class GuiObject
    {
        readonly FrameNode node;
        readonly ResourceKey<GameObject> resourceKey;
        readonly IDisplay display;
        readonly string typeDescription;
        float scale = 1;
        float expirationTime;

        public string ParentId => node.Parent?.Id ?? TfModule.DefaultFrame.Id;
        public string Id { get; }
        public WidgetType Type { get; }

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
                else if (display is IDialog dialog)
                {
                    dialog.Interactable = value;
                }
            }
        }

        public GuiObject(GuiWidgetListener parent, Widget msg, ResourceKey<GameObject> resourceKey,
            string typeDescription)
        {
            this.resourceKey = resourceKey;
            this.typeDescription = typeDescription;
            node = new FrameNode(msg.Id);
            Id = msg.Id;
            expirationTime = float.MaxValue;
            Type = (WidgetType)msg.Type;

            var widget = ResourcePool.Rent(resourceKey, node.Transform).GetComponent<IWidget>();
            if ((Component)widget == null)
            {
                ThrowHelper.ThrowMissingAssetField("Gui object does not have a widget!");
            }

            display = widget;

            if (widget is IWidgetCanBeMoved canBeMoved)
            {
                canBeMoved.Moved += direction => parent.OnWidgetMoved(this, direction * scale);
            }

            if (widget is IWidgetCanBeRotated canBeRotated)
            {
                canBeRotated.Moved += angle => parent.OnWidgetRotated(this, angle);
            }

            if (widget is IWidgetCanBeResized canBeResized)
            {
                canBeResized.Resized += bounds =>
                {
                    var localPose = node.Transform.AsLocalPose();
                    var transformedCenter = localPose.Multiply(scale * bounds.center);
                    parent.OnWidgetResized(this, new Bounds(transformedCenter, bounds.size * scale));
                };
            }

            if (widget is IWidgetCanBeClicked canBeClicked)
            {
                canBeClicked.Clicked += entry => parent.OnWidgetClicked(this, entry);
            }

            UpdateWidget(msg);
        }

        public void UpdateWidget(Widget msg)
        {
            node.AttachTo(msg.Header.FrameId);

            scale = msg.Scale == 0 ? 1f : (float)msg.Scale;

            if (display is IWidgetWithColor withColor)
            {
                if (msg.Color.A != 0)
                {
                    withColor.Color = msg.Color.ToUnity();
                }

                if (msg.SecondaryColor.A != 0)
                {
                    withColor.SecondaryColor = msg.SecondaryColor.ToUnity();
                }
            }

            if (msg.Scale != 0 && display is IWidgetWithScale withScale)
            {
                withScale.Scale = scale;
            }

            if (msg.SecondaryScale != 0 && display is IWidgetWithSecondaryScale withSecondaryScale)
            {
                withSecondaryScale.SecondaryScale = (float)msg.SecondaryScale;
            }

            if (!msg.Boundary.Size.ApproximatelyZero() && display is IWidgetWithBoundary withBoundary)
            {
                withBoundary.Boundary = msg.Boundary;
            }

            if (msg.SecondaryBoundaries.Length != 0 && display is IWidgetWithBoundaries withBoundaries)
            {
                withBoundaries.Set(new BoundingBoxStamped(msg.Header, msg.Boundary), msg.SecondaryBoundaries);
            }

            if (display is IWidgetWithCaption withCaption)
            {
                withCaption.Caption = msg.Caption;
            }

            var transform = node.Transform;
            transform.SetLocalPose(msg.Pose.Ros2Unity());
            transform.localScale = Vector3.one * scale;
        }

        public GuiObject(GuiWidgetListener parent, Dialog msg, ResourceKey<GameObject> resourceKey,
            string typeDescription)
        {
            this.resourceKey = resourceKey;
            this.typeDescription = typeDescription;
            node = new FrameNode(msg.Id);
            node.AttachTo(msg.Header.FrameId);
            Id = msg.Id;

            var dialog = ResourcePool.Rent(resourceKey, node.Transform).GetComponent<IDialog>();
            if ((Component)dialog == null)
            {
                ThrowHelper.ThrowMissingAssetField("Gui object does not have a dialog!");
            }

            dialog.Scale = msg.Scale == 0 ? 1f : (float)msg.Scale;
            dialog.PivotFrameId = msg.Header.FrameId;
            dialog.TfFrameOffset = msg.TfOffset.Ros2Unity();
            dialog.TfDisplacement = AdjustDisplacement(msg.TfDisplacement);
            dialog.DialogDisplacement = AdjustDisplacement(msg.DialogDisplacement);

            if (msg.BackgroundColor.A != 0)
            {
                dialog.Color = msg.BackgroundColor.ToUnity();
            }

            if (dialog is IDialogWithTitle withTitle)
            {
                withTitle.Title = msg.Title;
            }

            if (dialog is IDialogWithCaption withCaption)
            {
                if (msg.Caption.Length == 0)
                {
                    RosLogger.Info($"{this}: Dialog '{Id}' supports captions but the caption is empty");
                }

                withCaption.Caption = msg.Caption;
            }

            if (dialog is IDialogWithAlignment withAlignment)
            {
                var alignment = (CaptionAlignmentType)msg.CaptionAlignment;
                withAlignment.CaptionAlignment = alignment == CaptionAlignmentType.Default
                    ? CaptionAlignmentType.Mid | CaptionAlignmentType.Center
                    : alignment;
            }

            if (dialog is IDialogWithIcon withIcon)
            {
                withIcon.Icon = (XRIcon)msg.Icon;
            }

            if (dialog is IDialogWithButtonSetup withButtonSetup)
            {
                withButtonSetup.ButtonSetup = (ButtonSetup)msg.Buttons;
            }

            if (dialog is IDialogWithEntries withEntries)
            {
                withEntries.Entries = msg.MenuEntries;
            }

            if (dialog is IDialogCanBeClicked canBeClicked)
            {
                canBeClicked.Clicked += index => parent.OnDialogButtonClicked(this, index);
            }

            if (dialog is IDialogCanBeMenuClicked canBeMenuClicked)
            {
                canBeMenuClicked.MenuClicked += index => parent.OnDialogMenuEntryClicked(this, index);
            }

            dialog.Expired += () => parent.OnDialogExpired(this);

            dialog.Initialize();

            expirationTime = msg.Lifetime == default
                ? float.MaxValue
                : GameThread.GameTime + (float)msg.Lifetime.ToTimeSpan().TotalSeconds;

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

        public IDialog AsDialog()
        {
            return (IDialog)display;
        }

        public void Dispose()
        {
            Interactable = true;
            display.ReturnToPool(resourceKey);
            node.Dispose();
        }

        public void GenerateLog(StringBuilder description)
        {
            description.Append("<b>").Append(Id).Append("</b>").AppendLine();
            description.Append(typeDescription).AppendLine();
            description.AppendLine();
        }

        public bool Expired => expirationTime < GameThread.GameTime;

        public void MarkAsExpired() => expirationTime = float.MinValue;

        public override string ToString() => $"[{nameof(GuiObject)} '{Id}']";
    }
}