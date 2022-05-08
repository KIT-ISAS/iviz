#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class UpperCanvasPanel : MonoBehaviour
    {
        [SerializeField] DataLabelWidget? masterUriStr;
        [SerializeField] TrashButtonWidget? masterUriButton;
        [SerializeField] TrashButtonWidget? connectButton;
        [SerializeField] TrashButtonWidget? stopButton;
        [SerializeField] Image? topPanel;
        [SerializeField] Button? save;
        [SerializeField] Button? load;
        [SerializeField] Image? status;

        [SerializeField] Button? addDisplayByTopic;
        [SerializeField] Button? addModule;
        [SerializeField] Button? showTfTree;
        [SerializeField] Button? enableAR;
        [SerializeField] Button? showNetwork;
        [SerializeField] Button? showConsole;
        [SerializeField] Button? showSettings;
        [SerializeField] Button? showEcho;
        [SerializeField] Button? showSystem;

        [SerializeField] Button? recordBag;
        [SerializeField] Text? recordBagText;
        [SerializeField] Image? recordBagImage;

        [SerializeField] Sprite? connectedSprite;
        [SerializeField] Sprite? connectingSprite;
        [SerializeField] Sprite? disconnectedSprite;
        [SerializeField] Sprite? questionSprite;

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

        CancellationTokenSource? tokenSource;

        public Sprite StatusSprite
        {
            set
            {
                Status.enabled = true;
                Status.sprite = value;

                tokenSource?.Cancel();
                tokenSource = new CancellationTokenSource();
                _ = HideStatus(Status, tokenSource.Token);
            }
        }

        static async Task HideStatus(Behaviour status, CancellationToken token)
        {
            try
            {
                await Task.Delay(4000, token);
                status.enabled = false;
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}