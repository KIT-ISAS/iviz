#nullable enable

using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class UpperCanvasPanel : MonoBehaviour
    {
        [SerializeField] DataLabelWidget? masterUriStr = null;
        [SerializeField] TrashButtonWidget? masterUriButton = null;
        [SerializeField] TrashButtonWidget? connectButton = null;
        [SerializeField] TrashButtonWidget? stopButton = null;
        [SerializeField] Image? topPanel = null;
        [SerializeField] Button? save = null;
        [SerializeField] Button? load = null;
        [SerializeField] Image? status = null;

        [SerializeField] Button? addDisplayByTopic = null;
        [SerializeField] Button? addModule = null;
        [SerializeField] Button? showTfTree = null;
        [SerializeField] Button? enableAR = null;
        [SerializeField] Button? showNetwork = null;
        [SerializeField] Button? showConsole = null;
        [SerializeField] Button? showSettings = null;
        [SerializeField] Button? showEcho = null;
        [SerializeField] Button? showSystem = null;

        [SerializeField] Button? recordBag = null;
        [SerializeField] Text? recordBagText = null;
        [SerializeField] Image? recordBagImage = null;

        [SerializeField] Sprite? connectedSprite = null;
        [SerializeField] Sprite? connectingSprite = null;
        [SerializeField] Sprite? disconnectedSprite = null;
        [SerializeField] Sprite? questionSprite = null;

        public DataLabelWidget MasterUriStr => masterUriStr.AssertNotNull(nameof(masterUriStr));
        public TrashButtonWidget MasterUriButton => masterUriButton.AssertNotNull(nameof(masterUriButton));
        public TrashButtonWidget ConnectButton => connectButton.AssertNotNull(nameof(connectButton));
        public TrashButtonWidget StopButton => stopButton.AssertNotNull(nameof(stopButton));
        public Image TopPanel => topPanel.AssertNotNull(nameof(topPanel));
        public Button Save => save.AssertNotNull(nameof(save));
        public Button Load => load.AssertNotNull(nameof(load));
        public Image Status => status.AssertNotNull(nameof(status));

        public Button AddDisplayByTopic => addDisplayByTopic.AssertNotNull(nameof(addDisplayByTopic));
        public Button AddModule => addModule.AssertNotNull(nameof(addModule));
        public Button ShowTfTree => showTfTree.AssertNotNull(nameof(showTfTree));
        public Button EnableAR => enableAR.AssertNotNull(nameof(enableAR));
        public Button ShowNetwork => showNetwork.AssertNotNull(nameof(showNetwork));
        public Button ShowConsole => showConsole.AssertNotNull(nameof(showConsole));
        public Button ShowSettings => showSettings.AssertNotNull(nameof(showSettings));
        public Button ShowEcho => showEcho.AssertNotNull(nameof(showEcho));
        public Button ShowSystem => showSystem.AssertNotNull(nameof(showSystem));

        public Button RecordBag => recordBag.AssertNotNull(nameof(recordBag));
        public Text RecordBagText => recordBagText.AssertNotNull(nameof(recordBagText));
        public Image RecordBagImage => recordBagImage.AssertNotNull(nameof(recordBagImage));

        public Sprite ConnectedSprite => connectedSprite.AssertNotNull(nameof(connectedSprite));
        public Sprite ConnectingSprite => connectingSprite.AssertNotNull(nameof(connectingSprite));
        public Sprite DisconnectedSprite => disconnectedSprite.AssertNotNull(nameof(disconnectedSprite));
        public Sprite QuestionSprite => questionSprite.AssertNotNull(nameof(questionSprite));
    }
}