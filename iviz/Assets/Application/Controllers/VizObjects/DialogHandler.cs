#nullable enable
using System;
using System.Collections.Generic;
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
    public interface IDialogFeedback
    {
        void OnDialogButtonClicked(string id, string frameId, int buttonId);
        void OnDialogMenuEntryClicked(string id, string frameId, int buttonId);
        void OnDialogExpired(string id, string frameId);
    }

    public sealed class DialogHandler : VizHandler
    {
        readonly IDialogFeedback feedback;

        public override string BriefDescription
        {
            get
            {
                string vizObjectsStr = vizObjects.Count switch
                {
                    0 => "<b>No dialogs</b>",
                    1 => "<b>1 dialog</b>",
                    _ => $"<b>{vizObjects.Count.ToString()} dialogs</b>"
                };

                const string errorStr = "No errors";

                return $"{vizObjectsStr}\n{errorStr}";
            }
        }

        public DialogHandler(IDialogFeedback feedback)
        {
            this.feedback = feedback;

            GameThread.EveryFrame += CheckDeadDialogs;
        }

        public void Handler(DialogArray msg)
        {
            foreach (var dialog in msg.Dialogs)
            {
                Handler(dialog);
            }
        }

        public void Handler(Dialog msg)
        {
            switch ((ActionType)msg.Action)
            {
                case ActionType.Remove:
                    HandleRemove(msg.Id);
                    break;
                case ActionType.RemoveAll:
                    RemoveAll();
                    break;
                case ActionType.Add:
                    HandleAdd(msg);
                    break;
                default:
                    RosLogger.Error(
                        $"{ToString()}: Object '{msg.Id}' requested unknown action {msg.Action.ToString()}");
                    break;
            }
        }

        void HandleAdd(Dialog msg)
        {
            if (string.IsNullOrWhiteSpace(msg.Id))
            {
                RosLogger.Info($"{this}: Cannot add dialog with empty id");
                return;
            }

            if (msg.Icon > (byte)XRIcon.Question)
            {
                RosLogger.Info($"{this}: Dialog '{msg.Id}' has unknown icon id {msg.Action.ToString()}");
                return;
            }

            if (msg.Buttons > (byte)ButtonSetup.Backward)
            {
                RosLogger.Info($"{this}: Dialog '{msg.Id}' has unknown button setup id {msg.Buttons.ToString()}");
                return;
            }

            if (msg.BackgroundColor.IsInvalid())
            {
                RosLogger.Info($"{this}: Color of dialog '{msg.Id}' contains invalid values");
                return;
            }

            if (msg.TfDisplacement.IsInvalid() || msg.TfOffset.IsInvalid() || msg.DialogDisplacement.IsInvalid())
            {
                RosLogger.Info($"{this}: One of the offset fields of dialog '{msg.Id}' contains invalid values");
                return;
            }

            var resourceKey = (DialogType)msg.Type switch
            {
                DialogType.Button => Resource.Displays.XRButtonDialog,
                DialogType.Notice => Resource.Displays.XRDialogNotice,
                DialogType.Plain => Resource.Displays.XRDialog,
                DialogType.Short => Resource.Displays.XRDialogShort,
                DialogType.Menu => Resource.Displays.XRDialogMenu,
                DialogType.Icon => Resource.Displays.XRDialogIcon,
                _ => null
            };

            if (resourceKey == null)
            {
                RosLogger.Error($"{this}: Dialog '{msg.Id}' has unknown type {msg.Type.ToString()}");
                return;
            }

            if (vizObjects.TryGetValue(msg.Id, out var existingObject))
            {
                var dialogObject = (DialogObject)existingObject;
                if (msg.Lifetime.ToTimeSpan() < TimeSpan.Zero)
                {
                    dialogObject.MarkAsExpired();
                    return;
                }

                dialogObject.Dispose();
            }

            var vizObject = new DialogObject(feedback, msg, resourceKey, "Dialog." + (DialogType)msg.Type)
                { Interactable = Interactable, Visible = Visible };
            vizObjects[vizObject.id] = vizObject;
        }

        void CheckDeadDialogs()
        {
            List<VizObject>? deadObjects = null;
            foreach (var vizObject in vizObjects.Values)
            {
                var dialogObject = (DialogObject)vizObject;
                if (!dialogObject.Expired)
                {
                    return;
                }

                deadObjects ??= new List<VizObject>();
                deadObjects.Add(vizObject);
            }

            if (deadObjects == null)
            {
                return;
            }

            foreach (var vizObject in deadObjects)
            {
                vizObject.Dispose();
                vizObjects.Remove(vizObject.id);
            }
        }

        public void MarkAsExpired(string id)
        {
            if (vizObjects.TryGetValue(id, out var vizObject))
            {
                ((DialogObject)vizObject).MarkAsExpired();
            }
        }

        public IDialog? AddDialog(Dialog msg)
        {
            Handler(msg);
            return (ActionType)msg.Action == ActionType.Add
                ? ((DialogObject)vizObjects[msg.Id]).AsDialog()
                : null;
        }

        public override void Dispose()
        {
            base.Dispose();
            GameThread.EveryFrame -= CheckDeadDialogs;
        }

        // ----------------------------------------------

        sealed class DialogObject : VizObject
        {
            float expirationTime;

            public DialogObject(IDialogFeedback feedback, Dialog msg, ResourceKey<GameObject> resourceKey,
                string typeDescription) : base(msg.Id, typeDescription, resourceKey)
            {
                if (display is not IDialog dialog)
                {
                    ThrowHelper.ThrowMissingAssetField("Viz object does not have a dialog!");
                    return; // unreachable
                }

                dialog.BindingType = (BindingType)msg.BindingType;
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

                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (dialog is IDialogWithCaption withCaption)
                {
                    if (msg.Caption.Length == 0)
                    {
                        RosLogger.Info($"{this}: Dialog '{id}' supports captions but the caption is empty");
                    }

                    withCaption.Caption = msg.Caption;
                }

                /*
                if (dialog is IDialogWithAlignment withAlignment)
                {
                    var alignment = (CaptionAlignmentType)msg.CaptionAlignment;
                    withAlignment.CaptionAlignment = alignment == CaptionAlignmentType.Default
                        ? CaptionAlignmentType.Mid | CaptionAlignmentType.Center
                        : alignment;
                }
                */

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
                    canBeClicked.Clicked += index => feedback.OnDialogButtonClicked(id, FrameId, index);
                }

                if (dialog is IDialogCanBeMenuClicked canBeMenuClicked)
                {
                    canBeMenuClicked.MenuClicked += index => feedback.OnDialogMenuEntryClicked(id, FrameId, index);
                }

                dialog.Expired += () => feedback.OnDialogExpired(id, FrameId);

                dialog.Initialize();

                expirationTime = msg.Lifetime == default
                    ? float.MaxValue
                    : GameThread.GameTime + (float)msg.Lifetime.ToTimeSpan().TotalSeconds;
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
                return (IDialog?)display ?? throw new MissingAssetFieldException("Display is missing!");
            }

            public bool Expired => expirationTime < GameThread.GameTime;

            public void MarkAsExpired() => expirationTime = float.MinValue;
        }
    }
}