#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.Markers;
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
        readonly FrameNode node;
        readonly InteractiveMarkerListener listener;
        readonly string rosId;
        MenuEntryList? menuEntries;
        TextMarkerDisplay? text;

        bool visible;
        bool descriptionVisible;
        Pose? bufferedPose;
        bool poseUpdateEnabled = true;
        bool interactable;
        string caption = "";

        InteractiveMarker? lastMessage;

        public Transform ControlNode { get; }

        internal bool PoseUpdateEnabled
        {
            get => poseUpdateEnabled;
            set
            {
                poseUpdateEnabled = value;
                if (poseUpdateEnabled && bufferedPose != null)
                {
                    ControlNode.transform.SetLocalPose(bufferedPose.Value);
                    bufferedPose = null;
                }
            }
        }

        public bool DescriptionVisible
        {
            get => descriptionVisible;
            set
            {
                descriptionVisible = value;
                if (value && !string.IsNullOrWhiteSpace(caption))
                {
                    if (text == null)
                    {
                        text = ResourcePool.RentDisplay<TextMarkerDisplay>(ControlNode);
                        text.BillboardEnabled = true;
                        text.BillboardOffset = Vector3.up * 0.1f;
                        text.ElementSize = 0.1f;
                        text.Visible = true;
                        text.AlwaysVisible = true;
                    }

                    text.Text = caption;
                }
                else if (text != null)
                {
                    text.ReturnToPool();
                    text = null;
                }
            }
        }

        Pose LocalPose
        {
            set
            {
                if (PoseUpdateEnabled)
                {
                    ControlNode.transform.SetLocalPose(value);
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
                foreach (var control in controls.Values)
                {
                    control.Interactable = value;
                }
            }
        }

        public InteractiveMarkerObject(InteractiveMarkerListener listener, string rosId, TfFrame parent)
        {
            this.listener = listener;
            this.rosId = rosId;

            node = new FrameNode($"{nameof(InteractiveMarkerObject)} '{rosId}'", parent);

            ControlNode = new GameObject("[ControlNode]").transform;
            ControlNode.SetParentLocal(node.Transform);

            Interactable = true;
        }

        public void Set(InteractiveMarker msg)
        {
            lastMessage = msg;

            caption = msg.Description;
            DescriptionVisible = DescriptionVisible;
            LocalPose = msg.Pose.Ros2Unity();

            node.AttachTo(msg.Header);

            var controlsToDelete = new HashSet<string>(controls.Keys);
            float defaultScale = msg.Scale.ApproximatelyZero() ? 1 : msg.Scale;
            int numUnnamed = 0;
            foreach (var controlMsg in msg.Controls)
            {
                string controlId = controlMsg.Name.Length != 0
                    ? controlMsg.Name
                    : $"[{(numUnnamed++).ToString()}]";

                controlsToDelete.Remove(controlId);

                InteractiveMarkerControlObject controlObject;
                if (controls.TryGetValue(controlId, out var existingControl))
                {
                    controlObject = existingControl;
                }
                else
                {
                    controlObject = new InteractiveMarkerControlObject(this, controlId)
                    {
                        Visible = Visible,
                        Interactable = Interactable
                    };
                    controls[controlId] = controlObject;
                    controlObject.Transform.SetParentLocal(ControlNode);
                }

                controlObject.Set(controlMsg);

                controlObject.Scale = controlObject.InteractionMode == InteractionMode.None ? defaultScale : 1;
            }

            foreach (string controlId in controlsToDelete)
            {
                controls[controlId].Dispose();
                controls.Remove(controlId);
            }

            if (msg.MenuEntries.Length != 0)
            {
                menuEntries = new MenuEntryList(msg.MenuEntries);
            }
        }

        internal void ShowMenu()
        {
            if (menuEntries == null)
            {
                return;
            }

            ModuleListPanel.Instance.ShowMenu(menuEntries.RootEntries, OnMenuClick);
        }

        public void Set(string frameId, in Msgs.GeometryMsgs.Pose rosPose)
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

            listener.OnControlMouseEvent(rosId, rosControlId, node.Parent?.Id, ControlNode.AsLocalPose(), point, type);
        }

        internal void OnMoved(string rosControlId)
        {
            if (!node.IsAlive)
            {
                return; // destroyed while interacting
            }

            listener.OnControlMoved(rosId, rosControlId, node.Parent?.Id, ControlNode.AsLocalPose());
        }

        void OnMenuClick(uint entryId)
        {
            if (!node.IsAlive)
            {
                return; // destroyed while interacting
            }

            listener.OnControlMenuSelect(rosId, node.Parent?.Id, entryId, ControlNode.AsLocalPose());
        }

        public void Dispose()
        {
            foreach (var control in controls.Values)
            {
                control.Dispose();
            }

            controls.Clear();

            text.ReturnToPool();

            Object.Destroy(ControlNode.gameObject);
            node.Dispose();
        }

        public void GenerateLog(StringBuilder description)
        {
            if (lastMessage == null)
            {
                return;
            }

            description.Append("<color=blue><b>---- ");

            description.Append(string.IsNullOrWhiteSpace(lastMessage.Name)
                ? "(empty name)"
                : lastMessage.Name);

            description.Append(" ----</b></color>").AppendLine();

            string msgDescription = lastMessage.Description.Length != 0
                ? lastMessage.Description.Replace("\t", "\\t").Replace("\n", "\\n")
                : "[]";
            description.Append("Description: ").Append(msgDescription).AppendLine();

            if (lastMessage.Scale.ApproximatelyZero())
            {
                description.Append("Scale: 0 <i>(1)</i>").AppendLine();
            }
            else
            {
                description.Append("Scale: ").Append(lastMessage.Scale).AppendLine();
            }

            description.Append("Attached to: <i>").Append(node.Parent != null ? node.Parent.Id : "none")
                .Append("</i>")
                .AppendLine();


            if (menuEntries != null)
            {
                description.Append("+ ").Append(menuEntries.Count).Append(" menu entries").AppendLine();
                if (!controls.Values.Any(control => control.InteractionMode == InteractionMode.Menu))
                {
                    description.Append(WarnStr)
                        .Append("Menu entries were set, but no control has interaction mode MENU").AppendLine();
                    if (controls.Values.Any(control => control.InteractionMode != InteractionMode.Button))
                    {
                        description.Append(WarnStr).Append("Note: Buttons as menus are not implemented").AppendLine();
                    }
                }

                if (menuEntries.ErrorMessages != null)
                {
                    description.Append(menuEntries.ErrorMessages);
                }
            }

            foreach (var control in controls.Values)
            {
                control.GenerateLog(description);
            }
        }

        public void GetErrorCount(out int totalErrors, out int totalWarnings)
        {
            totalErrors = 0;
            totalWarnings = 0;

            foreach (var control in controls.Values)
            {
                control.GetErrorCount(out int newNumErrors, out int newNumWarnings);
                totalErrors += newNumErrors;
                totalWarnings += newNumWarnings;
            }
        }
        
        public override string ToString() => $"[{nameof(InteractiveMarkerObject)} '{rosId}']";

        internal IEnumerable<MarkerObject> GetAllMarkers() =>
            controls.Values.SelectMany(control => control.GetAllMarkers());
    }
}