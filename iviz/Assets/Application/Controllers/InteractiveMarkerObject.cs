using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Iviz.App.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;

namespace Iviz.App.Listeners
{
    public sealed class InteractiveMarkerObject : DisplayNode
    {
        readonly Dictionary<string, InteractiveMarkerControlObject> controls =
            new Dictionary<string, InteractiveMarkerControlObject>();

        readonly HashSet<string> controlsToDelete = new HashSet<string>();
        const int LifetimeInSec = 15;

        public string Description { get; private set; }
        public string Id { get; private set; }

        public delegate void MouseEventAction(string id, in Pose pose, in Vector3 point, MouseEventType type);

        public event MouseEventAction MouseEvent;

        public delegate void MovedAction(string id, in Pose pose);

        public event MovedAction Moved;

        MenuObject menuObject = null;
        public bool HasMenu => menuObject != null;

        public DateTime ExpirationTime { get; private set; }

        GameObject controlNode;

        void Awake()
        {
            controlNode = new GameObject("[ControlNode]");
            controlNode.transform.parent = transform;
        }

        public void Set(InteractiveMarker msg)
        {
            name = msg.Name;
            Id = msg.Name;
            Description = msg.Description;

            transform.SetLocalPose(msg.Pose.Ros2Unity());
            transform.localScale = msg.Scale * Vector3.one;

            AttachTo(msg.Header.FrameId);

            controlsToDelete.Clear();
            foreach (string id in controls.Keys)
            {
                controlsToDelete.Add(id);
            }

            foreach (InteractiveMarkerControl controlMsg in msg.Controls)
            {
                string id = controlMsg.Name;
                if (!controls.TryGetValue(id, out InteractiveMarkerControlObject control))
                {
                    control = CreateControlObject();
                    control.MouseEvent += (in Pose pose, in Vector3 point, MouseEventType type) =>
                    {
                        MouseEvent?.Invoke(id, pose, point, type);
                    };
                    control.Moved += (in Pose pose) => 
                    {
                        Moved?.Invoke(id, pose);
                    };
                    control.transform.SetParentLocal(controlNode.transform);
                    controls[id] = control;
                    //Debug.Log("Creating " + id);
                }

                control.Set(controlMsg);
                controlsToDelete.Remove(id);
            }

            foreach (string id in controlsToDelete)
            {
                InteractiveMarkerControlObject control = controls[id];
                DeleteControlObject(control);
                controls.Remove(id);
            }

            UpdateExpirationTime();
            /*
            if (msg.menu_entries.Length != 0)
            {
                GameObject gameObject = Instantiate(Resources.Load<GameObject>("MenuObject"));
                menuObject = gameObject.GetComponent<MenuObject>();
                menuObject.transform.SetParentLocal(transform);

                menuObject.Set(msg.menu_entries, this, null);
                menuObject.Show(false);
            }
            */
        }

        static void DeleteControlObject(InteractiveMarkerControlObject control)
        {
            control.Stop();
            Destroy(control.gameObject);
        }

        static InteractiveMarkerControlObject CreateControlObject()
        {
            GameObject gameObject = new GameObject("InteractiveMarkerControlObject");
            return gameObject.AddComponent<InteractiveMarkerControlObject>();
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
            MouseEvent = null;
            Moved = null;

            Destroy(controlNode.gameObject);

            /*
            if (menuObject != null)
            {
                menuObject.Cleanup();
                Destroy(menuObject.gameObject);
                menuObject = null;
            }
            */
        }

        public void UpdateExpirationTime()
        {
            ExpirationTime = DateTime.Now.AddSeconds(LifetimeInSec);
        }

        public void ShowMenu(Vector3 hint)
        {
            /*
            menuObject?.Show(true);
            if (menuObject != null)
            {
                menuObject.transform.localPosition = hint;
            }
            */
        }

        public void HideMenu()
        {
            /*
            menuObject?.Show(false);
            */
        }
    }
}