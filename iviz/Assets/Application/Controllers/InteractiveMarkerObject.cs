using UnityEngine;
using System.Collections.Generic;
using System;
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
        public event Action<string, Pose, Vector3, int> Clicked;

        MenuObject menuObject = null;
        public bool HasMenu => menuObject != null;

        public DateTime ExpirationTime { get; private set; }

        public void Set(InteractiveMarker msg)
        {
            name = msg.Name;
            Id = msg.Name;
            Description = msg.Description;

            transform.SetLocalPose(msg.Pose.Ros2Unity());
            transform.localScale = msg.Scale * Vector3.one;

            AttachTo(msg.Header.FrameId);

            controlsToDelete.Clear();
            controls.Keys.ForEach(x => controlsToDelete.Add(x));

            foreach (InteractiveMarkerControl controlMsg in msg.Controls)
            {
                string id = controlMsg.Name;
                if (!controls.TryGetValue(id, out InteractiveMarkerControlObject control))
                {
                    control = CreateControlObject();
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
                DeleteControlObject(control);
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
            Clicked = null;
            controls.Values.ForEach(DeleteControlObject);
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
