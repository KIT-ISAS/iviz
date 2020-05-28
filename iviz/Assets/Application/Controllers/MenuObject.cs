using System.Collections.Generic;
using Iviz.App.Displays;
using Iviz.Msgs.VisualizationMsgs;
using UnityEngine;

namespace Iviz.App.Listeners
{
    public class MenuObject : MonoBehaviour
    {
        readonly List<MenuEntryObject> children = new List<MenuEntryObject>();

        public void Set(MenuEntry[] entries, InteractiveMarkerObject parent, Camera camera)
        {
            Billboard billboard = GetComponent<Billboard>();
            billboard.parent = parent.transform;
            billboard.offset = new Vector3(0, 0.1f, 0);

            Cleanup();

            const float offset = 0.0f;
            float y0 = 0;
            foreach (MenuEntry entry in entries)
            {
                GameObject gameObject = Instantiate(UnityEngine.Resources.Load<GameObject>("MenuEntry"));

                MenuEntryObject menuEntry = gameObject.GetComponent<MenuEntryObject>();
                menuEntry.transform.SetParentLocal(transform);
                menuEntry.Set(entry, parent, camera);
                children.Add(menuEntry);

                y0 += menuEntry.Height + offset;
            }
            foreach (MenuEntryObject menuEntry in children)
            {
                menuEntry.transform.localPosition = new Vector3(0, y0, 0);
                y0 -= menuEntry.Height + offset;
            }
        }

        public void Show(bool b)
        {
            gameObject.SetActive(b);
        }

        public void Cleanup()
        {
            children.ForEach(x => x.Cleanup());
            children.ForEach(Destroy);
            children.Clear();
        }
    }
}