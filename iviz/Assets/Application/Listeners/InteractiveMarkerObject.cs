using UnityEngine;
using System.Collections.Generic;
using System;
using Iviz.Msgs.visualization_msgs;
using Iviz.App.Displays;

namespace Iviz.App
{
    public class InteractiveMarkerObject : DisplayNode
    {
        public string Description { get; private set; }
        public string Id { get; private set; }

        public event Action<string, Pose, Vector3, int> Clicked;

        readonly Dictionary<string, InteractiveMarkerControlObject> controls = new Dictionary<string, InteractiveMarkerControlObject>();
        readonly HashSet<string> controlsToDelete = new HashSet<string>();

        MenuObject menuObject;
        public bool HasMenu => menuObject != null;

        const int LifetimeInSec = 15;
        public DateTime ExpirationTime { get; private set; }

        public void Set(InteractiveMarker msg)
        {
            name = msg.name;
            Id = msg.name;
            Description = msg.description;

            transform.SetLocalPose(msg.pose.Ros2Unity());
            transform.localScale = msg.scale * Vector3.one;

            SetParent(msg.header.frame_id);

            controlsToDelete.Clear();
            controls.Keys.ForEach(x => controlsToDelete.Add(x));

            foreach (InteractiveMarkerControl controlMsg in msg.controls)
            {
                string id = controlMsg.name;
                if (!controls.TryGetValue(id, out InteractiveMarkerControlObject control))
                {
                    control = ResourcePool.
                        GetOrCreate(Resource.Listeners.InteractiveMarkerControlObject, transform).
                        GetComponent<InteractiveMarkerControlObject>();
                    control.Parent = TFListener.ListenersFrame;
                    control.Clicked += (pose, point, button) => Clicked?.Invoke(id, pose, point, button);
                    control.transform.SetParentLocal(transform);
                    controls[id] = control;
                }
                control.Set(controlMsg);
                controlsToDelete.Remove(id);
            }

            controlsToDelete.ForEach(x =>
            {
                InteractiveMarkerControlObject control = controls[x];
                control.Stop();
                ResourcePool.Dispose(Resource.Listeners.InteractiveMarkerControlObject, control.gameObject);
                controls.Remove(x);
            });

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

        public override void Stop()
        {
            controls.Values.ForEach(control =>
            {
                control.Stop();
                ResourcePool.Dispose(Resource.Listeners.InteractiveMarkerControlObject, control.gameObject);
            });
            controls.Clear();
            controlsToDelete.Clear();
            Clicked = null;

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
