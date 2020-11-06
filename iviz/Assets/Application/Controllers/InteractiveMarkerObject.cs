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
        public delegate void MouseEventAction(string id, in Pose pose, in Vector3 point, MouseEventType type);

        public delegate void MovedAction(string id, in Pose pose);

        //const int LifetimeInSec = 15;

        readonly Dictionary<string, InteractiveMarkerControlObject> controls =
            new Dictionary<string, InteractiveMarkerControlObject>();

        readonly HashSet<string> controlsToDelete = new HashSet<string>();
        readonly StringBuilder description = new StringBuilder();

        GameObject controlNode;

        //public DateTime ExpirationTime { get; private set; }

        void Awake()
        {
            controlNode = new GameObject("[ControlNode]");
            controlNode.transform.parent = transform;
        }

        public event MouseEventAction MouseEvent;
        public event MovedAction Moved;

        public void Set([NotNull] InteractiveMarker msg)
        {
            name = msg.Name;

            description.Clear();
            description.Append("<b>**** InteractiveMarker '").Append(msg.Name).Append("'</b>").AppendLine();
            description.Append("Description: ").Append(msg.Description.Length == 0 ? "[]" : msg.Description)
                .AppendLine();

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

            transform.SetLocalPose(msg.Pose.Ros2Unity());

            description.Append("Attached to: ").Append(msg.Header.FrameId).AppendLine();
            AttachTo(msg.Header.FrameId);

            controlsToDelete.Clear();
            foreach (string id in controls.Keys)
            {
                controlsToDelete.Add(id);
            }

            foreach (InteractiveMarkerControl controlMsg in msg.Controls)
            {
                string id = controlMsg.Name;

                if (controls.TryGetValue(id, out InteractiveMarkerControlObject existingControl))
                {
                    existingControl.Set(controlMsg);
                    controlsToDelete.Remove(id);
                    continue;
                }

                InteractiveMarkerControlObject newControl = CreateControlObject();
                newControl.MouseEvent += (in Pose pose, in Vector3 point, MouseEventType type) =>
                {
                    MouseEvent?.Invoke(id, pose, point, type);
                };
                newControl.Moved += (in Pose pose) =>
                {
                    Moved?.Invoke(id, pose);
                };
                newControl.transform.SetParentLocal(controlNode.transform);
                controls[id] = newControl;

                newControl.Set(controlMsg);
                controlsToDelete.Remove(id);
            }

            foreach (string id in controlsToDelete)
            {
                InteractiveMarkerControlObject control = controls[id];
                DeleteControlObject(control);
                controls.Remove(id);
            }
        }

        public override void Stop()
        {
            base.Stop();
            foreach (var controlObject in controls.Values) DeleteControlObject(controlObject);
            controls.Clear();
            controlsToDelete.Clear();
            MouseEvent = null;
            Moved = null;

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

            foreach (var control in controls.Values) control.GenerateLog(baseDescription);
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