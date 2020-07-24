using Iviz.Msgs.VisualizationMsgs;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.Controllers
{
    public class MenuEntryObject : InteractableObject
    {
        public uint Id { get; private set; }
        public Text Text { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }

        Vector3 hoverPoint;

        public void Set(MenuEntry msg, InteractiveMarkerObject parent, Camera camera)
        {
            Canvas canvas = GetComponentInChildren<Canvas>();
            RectTransform rectTransform = canvas.GetComponent<RectTransform>();
            Width = rectTransform.rect.width * transform.localScale.x;
            Height = rectTransform.rect.height * transform.localScale.y;

            canvas.worldCamera = camera;
            Id = msg.Id;
            Text = GetComponentInChildren<Text>();
            Text.text = msg.Title;
            Parent = parent;
        }

        public void Cleanup()
        {

        }

        public override InteractiveMarkerFeedback OnClick(Vector3 point, int button)
        {
            if (button != 0)
            {
                return null;
            }

            Debug.Log("Menu Entry: Sending feedback");
            Parent.HideMenu();
            return null;
            /*            

            return new InteractiveMarkerFeedback
            {
                header = Utils.CreateHeader(),
                client_id = ConnectionManager.MyId,
                marker_name = Parent.Id,
                event_type = InteractiveMarkerFeedback.MENU_SELECT,
                pose = Parent.transform.AsPose().Unity2RosPose(),
                menu_entry_id = Id,
                mouse_point = hoverPoint.Unity2RosPoint(),
                mouse_point_valid = true
            };
            */
        }

        public override void Select()
        {
            GetComponentInChildren<Image>().color = new Color(1, 0.82f, 0.25f);
        }

        public override void Deselect(InteractableObject newSelection)
        {
            GetComponentInChildren<Image>().color = Color.white;
            if (newSelection == null || newSelection.Parent != Parent)
            {
                Parent.HideMenu();
            }
        }
    }
}