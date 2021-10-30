using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class AnchorCanvas : MonoBehaviour
    {
        [SerializeField] Button sideHideGui = null;
        [SerializeField] AnchorToggleButton hideGui = null;
        [SerializeField] AnchorToggleButton arSet = null;
        [SerializeField] Button unlock  = null;
        [SerializeField] AnchorToggleButton interact = null;
        [SerializeField] GameObject arInfoPanel = null;

        public AnchorToggleButton HideGui => hideGui;
        public Button SideHideGui => sideHideGui;
        public AnchorToggleButton ArSet => arSet;
        public Button Unlock => unlock;
        public AnchorToggleButton Interact => interact;
        public GameObject ArInfoPanel => arInfoPanel;
    }
}
 