using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerObject : FrameNode
    {
        const string WarnStr = "<b>Warning:</b> ";
        const string ErrorStr = "<color=red>Error:</color> ";

        readonly Dictionary<string, InteractiveMarkerControlObject> controls =
            new Dictionary<string, InteractiveMarkerControlObject>();

        readonly HashSet<string> controlsToDelete = new HashSet<string>();
        readonly StringBuilder description = new StringBuilder();
        int numWarnings;
        int numErrors;

        MenuEntryList menuEntries;

        InteractiveMarkerListener listener;
        TextMarkerResource text;
        GameObject controlNode;

        string rosId;
        bool visible;

        Pose? bufferedPose;
        bool poseUpdateEnabled = true;

        bool interactable;

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
            get => transform.AsLocalPose();
            set
            {
                if (poseUpdateEnabled)
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

        void Awake()
        {
            controlNode = new GameObject("[ControlNode]");
            controlNode.transform.parent = transform;

            text = ResourcePool.GetOrCreateDisplay<TextMarkerResource>(controlNode.transform);
            text.BillboardEnabled = true;
            text.BillboardOffset = Vector3.up * 0.1f;
            text.ElementSize = 0.1f;
            text.Visible = false;
            text.VisibleOnTop = true;

            Interactable = true;
        }

        internal void Initialize(InteractiveMarkerListener newListener, string realId)
        {
            listener = newListener;
            rosId = realId;
        }

        public void Set([NotNull] InteractiveMarker msg)
        {
            name = msg.Name;
            numWarnings = 0;
            numErrors = 0;

            description.Clear();
            description.Append("<color=blue><b>**** InteractiveMarker '").Append(msg.Name).Append("'</b></color>")
                .AppendLine();
            string msgDescription = msg.Description.Length != 0
                ? msg.Description.ToString().Replace("\t", "\\t").Replace("\n", "\\n")
                : "[]";
            description.Append("Description: ").Append(msgDescription).AppendLine();

            text.Text = msg.Description.Length != 0 ? msg.Description : msg.Name;

            if (Mathf.Approximately(msg.Scale, 0))
            {
                controlNode.transform.localScale = Vector3.one;
                description.Append("Scale: 0 <i>(1)</i>").AppendLine();
            }
            else
            {
                controlNode.transform.localScale = msg.Scale * Vector3.one;
                description.Append("Scale: ").Append(msg.Scale).AppendLine();
            }

            LocalPose = msg.Pose.Ros2Unity();

            AttachTo(msg.Header.FrameId);

            controlsToDelete.Clear();
            foreach (string controlId in controls.Keys)
            {
                controlsToDelete.Add(controlId);
            }

            int numUnnamed = 0;
            foreach (InteractiveMarkerControl controlMsg in msg.Controls)
            {
                string controlId = controlMsg.Name.Length != 0 ? controlMsg.Name.ToString() : $"[Unnamed-{(numUnnamed++)}]";

                if (controls.TryGetValue(controlId, out var existingControl))
                {
                    existingControl.Set(controlMsg);
                    controlsToDelete.Remove(controlId);
                    continue;
                }

                InteractiveMarkerControlObject newControl = CreateControlObject();
                newControl.Initialize(this, controlMsg.Name);
                newControl.transform.SetParentLocal(controlNode.transform);
                newControl.Visible = Visible;
                newControl.Interactable = Interactable;
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
                InteractiveMarkerControlObject control = controls[controlId];
                DeleteControlObject(control);
                controls.Remove(controlId);
            }


            // update the dimensions of the controls
            IEnumerable<(Bounds? bounds, Transform transform)> innerBounds =
                controls.Values.Select(control => (control.Bounds, control.transform));

            Bounds? totalBounds =
                UnityUtils.CombineBounds(
                    innerBounds.Select(tuple => UnityUtils.TransformBound(tuple.bounds, tuple.transform))
                );

            foreach (var control in controls.Values)
            {
                control.UpdateControlBounds(totalBounds);
            }

            if (msg.MenuEntries.Length != 0)
            {
                menuEntries = new MenuEntryList(msg.MenuEntries, description, out int newNumErrors);
                numErrors += newNumErrors;

                description.Append("+ ").Append(menuEntries.Count).Append(" menu entries").AppendLine();

                IControlMarker controlMarker =
                    controls.Values.FirstOrDefault(control => control.Control != null)?.Control;
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
            ModuleListPanel.Instance.ShowMenu(menuEntries, OnMenuClick, unityPositionHint);
        }

        public void Set(string frameId, in Iviz.Msgs.GeometryMsgs.Pose rosPose)
        {
            /*
            if (Parent != null && frameId == Parent.Id)
            {
                LocalPose = rosPose.Ros2Unity();
                return;
            }
            //AttachTo(frameId);
            if (TfListener.TryGetFrame(frameId, out var newFrame))
            {
                
                LocalPose = rosPose.Ros2Unity();
                
            }
            */
            AttachTo(frameId);
            LocalPose = rosPose.Ros2Unity();
        }

        internal void OnMouseEvent(string rosControlId, in Vector3? point, MouseEventType type)
        {
            if (this == null)
            {
                return; // destroyed while interacting
            }

            var pose = controlNode.transform.AsLocalPose();
            listener?.OnInteractiveControlObjectMouseEvent(rosId, rosControlId,
                Parent != null ? Parent.Id : null, pose, point, type);
        }

        internal void OnMoved(string rosControlId)
        {
            if (this == null)
            {
                return; // destroyed while interacting
            }

            var pose = controlNode.transform.AsLocalPose();
            listener?.OnInteractiveControlObjectMoved(rosId, rosControlId,
                Parent != null ? Parent.Id : null, pose);
        }

        void OnMenuClick(uint entryId)
        {
            if (this == null)
            {
                return; // destroyed while interacting
            }

            listener?.OnInteractiveControlObjectMenuSelect(rosId,
                Parent != null ? Parent.Id : null, entryId,
                controlNode.transform.AsLocalPose());
        }

        public override void Stop()
        {
            base.Stop();
            foreach (var controlObject in controls.Values)
            {
                DeleteControlObject(controlObject);
            }

            controls.Clear();
            controlsToDelete.Clear();

            text.DisposeDisplay();

            Destroy(controlNode.gameObject);
        }

        public void GenerateLog([NotNull] StringBuilder baseDescription)
        {
            baseDescription.Append(description);
            baseDescription.Append("Attached to: <i>").Append(Parent != null ? Parent.Id : "none").Append("</i>")
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

        static void DeleteControlObject([NotNull] InteractiveMarkerControlObject control)
        {
            control.Stop();
            Destroy(control.gameObject);
        }

        static InteractiveMarkerControlObject CreateControlObject()
        {
            GameObject gameObject = new GameObject("InteractiveMarkerControlObject");
            return gameObject.AddComponent<InteractiveMarkerControlObject>();
        }
    }
}