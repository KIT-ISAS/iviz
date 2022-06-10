#nullable enable

using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class AnchorCanvasPanel : MonoBehaviour
    {
        [SerializeField] Button? sideHideGui;
        [SerializeField] AnchorToggleButton? hideGui;
        [SerializeField] AnchorToggleButton? arSet;
        [SerializeField] Button? unlock;
        [SerializeField] AnchorToggleButton? interact;
        [SerializeField] GameObject? arInfoPanel;

        public AnchorToggleButton BottomHideGui => hideGui.AssertNotNull(nameof(hideGui));
        public Button LeftHideGui => sideHideGui.AssertNotNull(nameof(sideHideGui));
        public AnchorToggleButton ARStartSession => arSet.AssertNotNull(nameof(arSet));
        public Button Unlock => unlock.AssertNotNull(nameof(unlock));
        public AnchorToggleButton Interact => interact.AssertNotNull(nameof(interact));
        public GameObject ARInfoPanel => arInfoPanel.AssertNotNull(nameof(arInfoPanel));
    }
}
 