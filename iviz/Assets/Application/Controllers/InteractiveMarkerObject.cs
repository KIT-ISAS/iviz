using System;
using System.Collections.Generic;
using System.Text;
using Iviz.Core;
using Iviz.Msgs.VisualizationMsgs;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerObject : DisplayNode
    {
        readonly Dictionary<string, InteractiveMarkerControlObject> controls =
            new Dictionary<string, InteractiveMarkerControlObject>();

        readonly HashSet<string> controlsToDelete = new HashSet<string>();
        readonly StringBuilder description = new StringBuilder();
        
        InteractiveMarkerListener listener;
        GameObject controlNode;
        string id;

        Pose? cachedPose;
        bool poseUpdateEnabled = true;

        internal bool PoseUpdateEnabled
        {
            get => poseUpdateEnabled;
            set
            {
                poseUpdateEnabled = value;
                if (poseUpdateEnabled && cachedPose != null)
                {
                    controlNode.transform.SetLocalPose(cachedPose.Value);
                    cachedPose = null;
                }
            }
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
                    cachedPose = value;
                }
            }
        }

        void Awake()
        {
            controlNode = new GameObject("[ControlNode]");
            controlNode.transform.parent = transform;
        }

        internal void Initialize(InteractiveMarkerListener newListener, string newId)
        {
            listener = newListener;
            id = newId;
        }

        public void Set([NotNull] InteractiveMarker msg)
        {
            name = msg.Name;

            description.Clear();
            description.Append("<color=blue><b>**** InteractiveMarker '").Append(msg.Name).Append("'</b></color>")
                .AppendLine();
            string msgDescription = msg.Description.Length != 0
                ? msg.Description.Replace("\t", "\\t").Replace("\n", "\\n")
                : "[]";
            description.Append("Description: ").Append(msgDescription).AppendLine();

            if (Mathf.Approximately(msg.Scale, 0))
            {
                transform.localScale = Vector3.one;
                description.Append("Scale: 0 <i>(1)</i>").AppendLine();
            }
            else
            {
                transform.localScale = msg.Scale * Vector3.one;
                description.Append("Scale: ").Append(msg.Scale).AppendLine();
            }

            LocalPose = msg.Pose.Ros2Unity();

            description.Append("Attached to: <i>").Append(msg.Header.FrameId).Append("</i>").AppendLine();
            AttachTo(msg.Header.FrameId);

            controlsToDelete.Clear();
            foreach (string id in controls.Keys)
            {
                controlsToDelete.Add(id);
            }

            foreach (InteractiveMarkerControl controlMsg in msg.Controls)
            {
                string controlId = controlMsg.Name;

                if (controls.TryGetValue(controlId, out InteractiveMarkerControlObject existingControl))
                {
                    existingControl.Set(controlMsg);
                    controlsToDelete.Remove(controlId);
                    continue;
                }

                InteractiveMarkerControlObject newControl = CreateControlObject();
                /*
                newControl.MouseEvent += (in Pose pose, in Vector3 point, MouseEventType type) =>
                {
                    MouseEvent?.Invoke(id, pose, point, type);
                };
                newControl.Moved += (in Pose pose) =>
                {
                    Moved?.Invoke(id, pose);
                };
                */
                newControl.Initialize(this, controlId);
                newControl.transform.SetParentLocal(controlNode.transform);
                controls[controlId] = newControl;

                newControl.Set(controlMsg);
                controlsToDelete.Remove(controlId);
            }

            foreach (string id in controlsToDelete)
            {
                InteractiveMarkerControlObject control = controls[id];
                DeleteControlObject(control);
                controls.Remove(id);
            }
        }

        public void Set(in Iviz.Msgs.GeometryMsgs.Pose rosPose)
        {
            Debug.Log("Received: " + rosPose.Ros2Unity() + "  ---   Actual:" + controlNode.transform.AsLocalPose());
            LocalPose = rosPose.Ros2Unity();
        }

        internal void OnMouseEvent(string controlId, in Pose pose, in Vector3 point, MouseEventType type)
        {
            listener?.OnInteractiveControlObjectMouseEvent(id, controlId, pose, point, type);
        }

        internal void OnMoved(string controlId)
        {
            listener?.OnInteractiveControlObjectMoved(id, controlId, controlNode.transform.AsLocalPose());
        }

        public override void Stop()
        {
            base.Stop();
            foreach (var controlObject in controls.Values)
                DeleteControlObject(controlObject);
            controls.Clear();
            controlsToDelete.Clear();

            Destroy(controlNode.gameObject);
        }

        /*
        public void UpdateExpirationTime()
        {
            ExpirationTime = DateTime.Now.AddSeconds(LifetimeInSec);
        }
        */

        public void GenerateLog([NotNull] StringBuilder baseDescription)
        {
            baseDescription.Append(description);

            foreach (var control in controls.Values)
            {
                control.GenerateLog(baseDescription);
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