#nullable enable

using System;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class AnchorCanvasPanel : MonoBehaviour
    {
        [SerializeField] Button? sideHideGui = null;
        [SerializeField] AnchorToggleButton? hideGui = null;
        [SerializeField] AnchorToggleButton? arSet = null;
        [SerializeField] Button? unlock = null;
        [SerializeField] AnchorToggleButton? interact = null;
        [SerializeField] GameObject? arInfoPanel = null;

        public AnchorToggleButton BottomHideGui => hideGui.AssertNotNull(nameof(hideGui));
        public Button LeftHideGui => sideHideGui.AssertNotNull(nameof(sideHideGui));
        public AnchorToggleButton ARSet => arSet.AssertNotNull(nameof(arSet));
        public Button Unlock => unlock.AssertNotNull(nameof(unlock));
        public AnchorToggleButton Interact => interact.AssertNotNull(nameof(interact));
        public GameObject ARInfoPanel => arInfoPanel.AssertNotNull(nameof(arInfoPanel));
    }
}
 