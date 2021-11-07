#nullable enable

using System;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class AnchorCanvas : MonoBehaviour
    {
        [SerializeField] Button? sideHideGui = null;
        [SerializeField] AnchorToggleButton? hideGui = null;
        [SerializeField] AnchorToggleButton? arSet = null;
        [SerializeField] Button? unlock = null;
        [SerializeField] AnchorToggleButton? interact = null;
        [SerializeField] GameObject? arInfoPanel = null;

        public AnchorToggleButton HideGui => hideGui.AssertNotNull(nameof(hideGui));
        public Button SideHideGui => sideHideGui.AssertNotNull(nameof(sideHideGui));
        public AnchorToggleButton ArSet => arSet.AssertNotNull(nameof(arSet));
        public Button Unlock => unlock.AssertNotNull(nameof(unlock));
        public AnchorToggleButton Interact => interact.AssertNotNull(nameof(interact));
        public GameObject ArInfoPanel => arInfoPanel.AssertNotNull(nameof(arInfoPanel));
    }
}
 