#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.App;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerObject
    {
        const string WarnStr = "<b>Warning:</b> ";
        const string ErrorStr = "<color=red>Error:</color> ";

        readonly Dictionary<string, InteractiveMarkerControlObject> controls = new();
        readonly HashSet<string> controlsToDelete = new();
        readonly StringBuilder description = new(250);
        readonly FrameNode node;
        readonly InteractiveMarkerListener listener;
        readonly TextMarkerResource text;
        readonly Transform controlNode;
        readonly string rosId;
        MenuEntryList? menuEntries;

        int numWarnings;
        int numErrors;
        bool visible;
        Pose? bufferedPose;
        bool poseUpdateEnabled = true;
        bool interactable;

        public Transform Transform => node.transform;

        internal bool PoseUpdateEnabled
        {
            get => poseUpdateEnabled;
            set
            {
                poseUpdateEnabled = value;
                if (poseUpdateEnabled && bufferedPose != null)
                {
                    controlNode.transform.SetLocalPose(bufferedPose.Value);
                    bufferedPose = null;
                }
            }
        }

        public bool DescriptionVisible
        {
            get => text.Visible;
            set => text.Visible = value;
        }

        Pose LocalPose
        {
            set
            {
                if (PoseUpdateEnabled)
                {
                    controlNode.transform.SetLocalPose(value);
                }
                else
                {
                    bufferedPose = value;
                }
            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;

                foreach (var control in controls.Values)
                {
                    control.Visible = value;
                }
            }
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                foreach (var controlObject in controls.Values)
                {
                    controlObject.Interactable = value;
                }
            }
        }

        public InteractiveMarkerObject(InteractiveMarkerListener newListener, string realId, TfFrame parent)
        {
            listener = newListener;
            rosId = realId;

            node = FrameNode.Instantiate("[InteractiveMarkerObject]");
            node.Parent = parent;

            controlNode = new GameObject("[ControlNode]").transform;
            controlNode.parent = Transform;

            text = ResourcePool.RentDisplay<TextMarkerResource>(controlNode);
            text.BillboardEnabled = true;
            text.BillboardOffset = Vector3.up * 0.1f;
            text.ElementSize = 0.1f;
            text.Visible = false;
            text.AlwaysVisible = true;

            Interactable = true;
        }

        public void Set(InteractiveMarker msg)
        {
            node.gameObject.name = msg.Name;
            numWarnings = 0;
            numErrors = 0;

            description.Clear();
            description.Append("<color=blue><b>** InteractiveMarker ");

            if (string.IsNullOrEmpty(msg.Name))
            {
                description.Append("(empty name)");
            }
            else
            {
                description.Append("'").Append(msg.Name).Append("'");
            }

            description.Append("</b></color>").AppendLine();

            string msgDescription = msg.Description.Length != 0
                ? msg.Description.Replace("\t", "\\t").Replace("\n", "\\n")
                : "[]";
            description.Append("Description: ").Append(msgDescription).AppendLine();

            text.Text = msg.Description.Length != 0 ? msg.Description : msg.Name;

            if (Mathf.Approximately(msg.Scale, 0))
            {
                controlNode.localScale = Vector3.one;
                description.Append("Scale: 0 <i>(1)</i>").AppendLine();
            }
            else
            {
                controlNode.localScale = msg.Scale * Vector3.one;
                description.Append("Scale: ").Append(msg.Scale).AppendLine();
            }

            LocalPose = msg.Pose.Ros2Unity();

            node.AttachTo(msg.Header);

            controlsToDelete.Clear();
            foreach (string controlId in controls.Keys)
            {
                controlsToDelete.Add(controlId);
            }

            int numUnnamed = 0;
            foreach (var controlMsg in msg.Controls)
            {
                string controlId = controlMsg.Name.Length != 0
                    ? controlMsg.Name
                    : $"[Unnamed-{(numUnnamed++).ToString()}]";

                if (controls.TryGetValue(controlId, out var existingControl))
                {
                    existingControl.Set(controlMsg);
                    controlsToDelete.Remove(controlId);
                    continue;
                }

                var newControl = new InteractiveMarkerControlObject(this, controlMsg.Name)
                {
                    Visible = Visible,
                    Interactable = Interactable
                };
                newControl.Transform.SetParentLocal(controlNode);
                controls[controlId] = newControl;

                newControl.Set(controlMsg);
                controlsToDelete.Remove(controlId);
            }

            if (numUnnamed > 1)
            {
                description.Append(WarnStr).Append(numUnnamed).Append(" controls have empty ids").AppendLine();
                numWarnings++;
            }

            foreach (string controlId in controlsToDelete)
            {
                controls[controlId].Stop();
                controls.Remove(controlId);
            }

            // update the dimensions of the controls
            var totalBounds =
                controls.Values
                    .Select(control => control.Bounds.TransformBound(control.Transform))
                    .CombineBounds();

            foreach (var control in controls.Values)
            {
                control.UpdateControlBounds(totalBounds);
            }

            {
                var controlObject = controls.Values.FirstOrDefault(control =>
                    control.ControlInteractionMode == InteractionModeType.ClickOnly);
                var controlMarker = controlObject?.ControlMarker != null
                    ? controlObject.ControlMarker
                    : controls.Values.FirstOrDefault(control => control.ControlColliderCanInteract)?.ControlMarker;

                if (controlMarker != null)
                {
                    controlMarker.SetColliderInteractable();
                }
            }

            if (msg.MenuEntries.Length == 0)
            {
                return;
            }

            {
                menuEntries = new MenuEntryList(msg.MenuEntries, description, out int newNumErrors);
                numErrors += newNumErrors;

                description.Append("+ ").Append(menuEntries.Count).Append(" menu entries").AppendLine();

                var controlMarker = controls.Values
                    .FirstOrDefault(control => control.ControlMarker != null)
                    ?.ControlMarker;
                if (controlMarker != null)
                {
                    controlMarker.EnableMenu = true;
                }
                else
                {
                    numWarnings++;
                    description.Append(WarnStr).Append("Menu requested without a control").AppendLine();
                }
            }
        }

        internal void ShowMenu(Vector3 unityPositionHint)
        {
            if (menuEntries == null)
            {
                return;
            }

            ModuleListPanel.Instance.ShowMenu(menuEntries, OnMenuClick, unityPositionHint);
        }

        public void Set(string frameId, in Iviz.Msgs.GeometryMsgs.Pose rosPose)
        {
            node.AttachTo(frameId);
            LocalPose = rosPose.Ros2Unity();
        }

        internal void OnMouseEvent(string rosControlId, in Vector3? point, MouseEventType type)
        {
            if (!node.IsAlive)
            {
                return; // destroyed while interacting
            }

            var pose = controlNode.AsLocalPose();
            listener.OnInteractiveControlObjectMouseEvent(rosId, rosControlId,
                node.Parent != null ? node.Parent.Id : null, pose, point, type);
        }

        internal void OnMoved(string rosControlId)
        {
            if (!node.IsAlive)
            {
                return; // destroyed while interacting
            }

            var pose = controlNode.AsLocalPose();
            listener.OnInteractiveControlObjectMoved(rosId, rosControlId,
                node.Parent != null ? node.Parent.Id : null, pose);
        }

        void OnMenuClick(uint entryId)
        {
            if (!node.IsAlive)
            {
                return; // destroyed while interacting
            }

            listener.OnInteractiveControlObjectMenuSelect(rosId,
                node.Parent != null ? node.Parent.Id : null, entryId,
                controlNode.AsLocalPose());
        }

        public void Stop()
        {
            foreach (var controlObject in controls.Values)
            {
                controlObject.Stop();
            }

            controls.Clear();
            controlsToDelete.Clear();

            text.ReturnToPool();

            Object.Destroy(controlNode.gameObject);
            node.DestroySelf();
        }

        public void GenerateLog(StringBuilder baseDescription)
        {
            baseDescription.Append(description);
            baseDescription.Append("Attached to: <i>").Append(node.Parent != null ? node.Parent.Id : "none")
                .Append("</i>")
                .AppendLine();

            foreach (var control in controls.Values)
            {
                control.GenerateLog(baseDescription);
            }
        }

        public void GetErrorCount(out int totalErrors, out int totalWarnings)
        {
            totalErrors = numErrors;
            totalWarnings = numWarnings;

            foreach (var control in controls.Values)
            {
                control.GetErrorCount(out int newNumErrors, out int newNumWarnings);
                totalErrors += newNumErrors;
                totalWarnings += newNumWarnings;
            }
        }
    }
}