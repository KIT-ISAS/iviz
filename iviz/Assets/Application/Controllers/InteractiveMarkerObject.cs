using UnityEngine;
using System.Collections.Generic;
using System;
using Iviz.App.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;

namespace Iviz.App.Listeners
{
    public class InteractiveMarkerObject : DisplayNode
    {
        public string Description { get; private set; }
        public string Id { get; private set; }

        public event Action<string, Pose, Vector3, int> Clicked;

        readonly Dictionary<string, InteractiveMarkerControlObject> controls = new Dictionary<string, InteractiveMarkerControlObject>();
        readonly HashSet<string> controlsToDelete = new HashSet<string>();

        MenuObject menuObject = null;
        public bool HasMenu => menuObject != null;

        const int LifetimeInSec = 15;
        public DateTime ExpirationTime { get; private set; }

        public void Set(InteractiveMarker msg)
        {
            name = msg.Name;
            Id = msg.Name;
            Description = msg.Description;

            transform.SetLocalPose(msg.Pose.Ros2Unity());
            transform.localScale = msg.Scale * Vector3.one;

            SetParent(msg.Header.FrameId);

            controlsToDelete.Clear();
            controls.Keys.ForEach(x => controlsToDelete.Add(x));

            foreach (InteractiveMarkerControl controlMsg in msg.Controls)
            {
                string id = controlMsg.Name;
                if (!controls.TryGetValue(id, out InteractiveMarkerControlObject control))
                {
                    control = ResourcePool.GetOrCreate<InteractiveMarkerControlObject>(Resource.Listeners.InteractiveMarkerControlObject, transform);
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
