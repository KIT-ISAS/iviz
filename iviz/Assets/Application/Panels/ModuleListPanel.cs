using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using Iviz.Controllers;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Roslib;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ModuleListPanel : MonoBehaviour
    {
        const float YOffset = 2;

        public const int ModuleDataCaptionWidth = 200;

        static readonly Color ConnectedColor = new Color(0.6f, 1f, 0.5f, 0.4f);
        static readonly Color ConnectedOwnMasterColor = new Color(0.4f, 0.95f, 1f, 0.4f);
        static readonly Color DisconnectedColor = new Color(0.9f, 0.95f, 1f, 0.4f);

        [SerializeField] DataLabelWidget masterUriStr;
        [SerializeField] TrashButtonWidget masterUriButton;
        [SerializeField] TrashButtonWidget connectButton;
        [SerializeField] TrashButtonWidget stopButton;
        [SerializeField] Image topPanel;
        [SerializeField] Button save;
        [SerializeField] Button load;
        [SerializeField] Image status;

        [SerializeField] AnchorCanvas anchorCanvas;
        [SerializeField] GameObject contentObject;
        [SerializeField] DataPanelManager dataPanelManager;
        [SerializeField] DialogPanelManager dialogPanelManager;
        [SerializeField] Button addDisplayByTopic;
        [SerializeField] Button addDisplay;
        [SerializeField] Button showTfTree;
        [SerializeField] Button resetAll;

        [SerializeField] Sprite connectedSprite;
        [SerializeField] Sprite connectingSprite;
        [SerializeField] Sprite disconnectedSprite;
        [SerializeField] Sprite questionSprite;

        [SerializeField] Text bottomTime;
        [SerializeField] Text bottomFps;
        [SerializeField] Text bottomBandwidth;

        [SerializeField] Joystick joystick;

        readonly List<GameObject> buttons = new List<GameObject>();
        readonly List<ModuleData> moduleDatas = new List<ModuleData>();
        readonly HashSet<string> topicsWithModule = new HashSet<string>();

        int frameCounter;
        float buttonHeight;

        Canvas parentCanvas;
        DialogData availableModules;
        DialogData availableTopics;
        ConnectionDialogData connectionData;
        ImageDialogData imageData;
        LoadConfigDialogData loadConfigData;
        SaveConfigDialogData saveConfigData;
        TFDialogData tfTreeData;

        ControllerService controllerService;

        public ModuleListPanel()
        {
            ModuleDatas = moduleDatas.AsReadOnly();
        }

        public bool AllGuiVisible
        {
            get => parentCanvas.gameObject.activeSelf;
            set => parentCanvas.gameObject.SetActive(value);
        }

        public static ModuleListPanel Instance { get; private set; }
        public static AnchorCanvas AnchorCanvas => Instance.anchorCanvas;
        AnchorToggleButton HideGuiButton => anchorCanvas.HideGui;
        AnchorToggleButton ShowControlButton => anchorCanvas.ShowMarker;
        AnchorToggleButton PinControlButton => anchorCanvas.PinMarker;
        public Button UnlockButton => anchorCanvas.Unlock;
        public DataPanelManager DataPanelManager => dataPanelManager;
        public DialogPanelManager DialogPanelManager => dialogPanelManager;
        public Joystick Joystick => joystick;
        public IReadOnlyCollection<ModuleData> ModuleDatas { get; }
        TFModuleData TfData => (TFModuleData) moduleDatas[0];
        public IEnumerable<string> DisplayedTopics => topicsWithModule;

        public bool UnlockButtonVisible
        {
            get => UnlockButton.gameObject.activeSelf;
            set => UnlockButton.gameObject.SetActive(value);
        }

        bool KeepReconnecting
        {
            get => ConnectionManager.Connection.KeepReconnecting;
            set
            {
                ConnectionManager.Connection.KeepReconnecting = value;
                status.enabled = value;
            }
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            parentCanvas = transform.parent.parent.GetComponentInParent<Canvas>();

            buttonHeight = Resource.Widgets.DisplayButton.Object.GetComponent<RectTransform>().rect.height;

            availableModules = CreateDialog<AddModuleDialogData>();
            availableTopics = CreateDialog<AddTopicDialogData>();

            imageData = CreateDialog<ImageDialogData>();
            tfTreeData = CreateDialog<TFDialogData>();
            loadConfigData = CreateDialog<LoadConfigDialogData>();
            saveConfigData = CreateDialog<SaveConfigDialogData>();

            connectionData = CreateDialog<ConnectionDialogData>();

            Directory.CreateDirectory(Settings.SavedFolder);
            LoadSimpleConfiguration();

            Logger.Internal("<b>Welcome to iviz</b>");

            CreateModule(Resource.Module.TF, TFListener.DefaultTopic);
            CreateModule(Resource.Module.Grid);

            if (Settings.IsHololens)
            {
                ARController controller = (ARController) CreateModule(Resource.Module.AugmentedReality).Controller;
                controller.Visible = true;
            }

            if (Resource.External == null)
            {
                Debug.LogError("Failed to load external manager!");
            }

            save.onClick.AddListener(saveConfigData.Show);
            load.onClick.AddListener(loadConfigData.Show);

            HideGuiButton.Clicked += OnHideGuiButtonClick;
            HideGuiButton.State = true;

            PinControlButton.Clicked += () =>
            {
                if (ARController.Instance != null)
                {
                    ARController.Instance.PinRootMarker = PinControlButton.State;
                }
            };
            ShowControlButton.Clicked += () =>
            {
                if (ARController.Instance != null)
                {
                    ARController.Instance.ShowRootMarker = ShowControlButton.State;
                    TFListener.UpdateRootMarkerVisibility();
                }
            };

            addDisplayByTopic.onClick.AddListener(availableTopics.Show);
            addDisplay.onClick.AddListener(availableModules.Show);
            showTfTree.onClick.AddListener(tfTreeData.Show);
            resetAll.onClick.AddListener(ResetAllModules);


            masterUriStr.Label = connectionData.MasterUri + " →";
            masterUriButton.Clicked += () =>
            {
                connectionData.Show();
            };

            ConnectionManager.Connection.MasterUri = connectionData.MasterUri;
            ConnectionManager.Connection.MyUri = connectionData.MyUri;
            ConnectionManager.Connection.MyId = connectionData.MyId;
            KeepReconnecting = false;

            connectionData.MasterUriChanged += uri =>
            {
                ConnectionManager.Connection.MasterUri = uri;
                KeepReconnecting = false;
                if (uri == null)
                {
                    Logger.Internal("Failed to set master uri.");
                    masterUriStr.Label = "(?) →";
                }
                else if (RosServerManager.IsActive)
                {
                    Logger.Internal($"Changing master uri to local master '{uri}'");
                    masterUriStr.Label = "Master Mode\n" + uri + " →";
                }
                else
                {
                    Logger.Internal($"Changing master uri to '{uri}'");
                    masterUriStr.Label = uri + " →";
                }
            };
            connectionData.MyIdChanged += id =>
            {
                if (id == null)
                {
                    Logger.Internal("Failed to set caller id.");
                    Logger.Internal("* First character must be /");
                    Logger.Internal("* Second character must be alpha [a-z A-Z]");
                    Logger.Internal("* Remaining characters must be alpha, digits, _ or /");
                    return;
                }

                ConnectionManager.Connection.MyId = id;
                KeepReconnecting = false;
                Logger.Internal($"Changing caller id to '{id}'");
            };
            connectionData.MyUriChanged += uri =>
            {
                ConnectionManager.Connection.MyUri = uri;
                KeepReconnecting = false;
                Logger.Internal(
                    uri == null ? "Failed to set caller uri." : $"Changing caller uri to '{uri}'"
                );
            };
            stopButton.Clicked += () =>
            {
                Logger.Internal(
                    ConnectionManager.IsConnected
                        ? "Disconnection requested."
                        : "Disconnection requested (but already disconnected)."
                );
                KeepReconnecting = false;
                ConnectionManager.Connection.Disconnect();
            };
            connectButton.Clicked += () =>
            {
                Logger.Internal(
                    ConnectionManager.IsConnected ? "Reconnection requested." : "Connection requested."
                );
                ConnectionManager.Connection.Disconnect();
                KeepReconnecting = true;
            };

            connectionData.MasterActiveChanged += _ =>
            {
                ConnectionManager.Connection.Disconnect();
            };

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;
            ARController.ARModeChanged += OnARModeChanged;
            GameThread.LateEverySecond += UpdateFpsStats;
            GameThread.EveryFrame += UpdateFpsCounter;
            UpdateFpsStats();
            
            controllerService = new ControllerService();
        }

        void OnConnectionStateChanged(ConnectionState state)
        {
            status.rectTransform.localRotation = Quaternion.identity;

            if (ConnectionManager.Connection.MasterUri == null ||
                ConnectionManager.Connection.MyUri == null ||
                ConnectionManager.Connection.MyId == null)
            {
                status.sprite = questionSprite;
                return;
            }

            switch (state)
            {
                case ConnectionState.Connected:
                    GameThread.EverySecond -= RotateSprite;
                    status.sprite = connectedSprite;
                    topPanel.color = RosServerManager.IsActive ? ConnectedOwnMasterColor : ConnectedColor;
                    SaveSimpleConfiguration();
                    break;
                case ConnectionState.Disconnected:
                    GameThread.EverySecond -= RotateSprite;
                    status.sprite = disconnectedSprite;
                    topPanel.color = DisconnectedColor;
                    break;
                case ConnectionState.Connecting:
                    status.sprite = connectingSprite;
                    GameThread.EverySecond += RotateSprite;
                    break;
            }
        }

        void RotateSprite()
        {
            status.rectTransform.Rotate(new Vector3(0, 0, 10.0f), Space.Self);
        }

        void OnHideGuiButtonClick()
        {
            AllGuiVisible = !AllGuiVisible;
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void SaveStateConfiguration(string file)
        {
            StateConfiguration config = new StateConfiguration
            {
                MasterUri = connectionData.MasterUri,
                MyUri = connectionData.MyUri,
                MyId = connectionData.MyId,
                Entries = moduleDatas.Select(x => x.Configuration.Id).ToList()
            };
            moduleDatas.ForEach(x => x.AddToState(config));

            try
            {
                Logger.Internal("Saving config file...");
                string text = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText($"{Settings.SavedFolder}/{file}", text);
                Logger.Internal("Done.");
            }
            catch (Exception e) when
                (e is IOException || e is SecurityException || e is JsonException)
            {
                Logger.Error(e);
                Logger.Internal("Error:", e);
                return;
            }

            Logger.Debug("DisplayListPanel: Writing config to " + Settings.SavedFolder + "/" + file);
        }

        public void LoadStateConfiguration(string file)
        {
            Logger.Debug("DisplayListPanel: Reading config from " + Settings.SavedFolder + "/" + file);
            string text;
            try
            {
                Logger.Internal("Loading config file...");
                text = File.ReadAllText(Settings.SavedFolder + "/" + file);
                Logger.Internal("Done.");
            }
            catch (FileNotFoundException)
            {
                Logger.Internal("Error: No config file found.");
                return;
            }
            catch (Exception e) when
                (e is IOException || e is SecurityException || e is JsonException)
            {
                Logger.Error(e);
                Logger.Internal("Error:", e);
                return;
            }

            while (moduleDatas.Count > 1) RemoveModule(1);

            StateConfiguration stateConfig = JsonConvert.DeserializeObject<StateConfiguration>(text);

            connectionData.MasterUri = stateConfig.MasterUri;
            connectionData.MyUri = stateConfig.MyUri;
            connectionData.MyId = stateConfig.MyId;


            TfData.UpdateConfiguration(stateConfig.Tf);
            stateConfig.CreateListOfEntries().ForEach(
                displayConfigList => displayConfigList?.ForEach(
                    displayConfig =>
                    {
                        if (displayConfig != null)
                        {
                            CreateModule(displayConfig.Module, configuration: displayConfig);
                        }
                    }));

            if (connectionData.MasterUri != null &&
                connectionData.MyUri != null &&
                connectionData.MyId != null)
            {
                KeepReconnecting = true;
            }
        }

        void LoadSimpleConfiguration()
        {
            string path = Settings.SimpleConfigurationPath;
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                string text = File.ReadAllText(path);
                ConnectionConfiguration config = JsonConvert.DeserializeObject<ConnectionConfiguration>(text);
                connectionData.MasterUri = config.MasterUri;
                connectionData.MyUri = config.MyUri;
                connectionData.MyId = config.MyId;
            }
            catch (Exception e) when
                (e is IOException || e is SecurityException || e is JsonException)
            {
                //Debug.Log(e);
            }
        }

        void SaveSimpleConfiguration()
        {
            try
            {
                ConnectionConfiguration config = new ConnectionConfiguration
                {
                    MasterUri = connectionData.MasterUri,
                    MyUri = connectionData.MyUri,
                    MyId = connectionData.MyId
                };

                string text = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(Settings.SimpleConfigurationPath, text);
            }
            catch (Exception e) when
                (e is IOException || e is SecurityException || e is JsonException)
            {
                //Debug.Log(e);
            }
        }

        void ResetAllModules()
        {
            foreach (ModuleData m in moduleDatas) m.ResetController();
        }
        
        public ModuleData CreateModule(Resource.Module resource, string topic = "", string type = "",
            IConfiguration configuration = null)
        {
            ModuleDataConstructor constructor =
                new ModuleDataConstructor(resource, this, topic, type, configuration);

            ModuleData moduleData;
            try
            {
                moduleData = ModuleData.CreateFromResource(constructor);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }

            moduleDatas.Add(moduleData);
            CreateButtonObject(moduleData);
            return moduleData;
        }

        T CreateDialog<T>() where T : DialogData, new()
        {
            T dialogData = new T();
            dialogData.Initialize(this);
            return dialogData;
        }

        void CreateButtonObject(ModuleData moduleData)
        {
            GameObject buttonObject =
                ResourcePool.GetOrCreate(Resource.Widgets.DisplayButton, contentObject.transform, false);

            int size = buttons.Count;
            float y = 2 * YOffset + size * (buttonHeight + YOffset);

            ((RectTransform) buttonObject.transform).anchoredPosition = new Vector2(0, -y);

            Text buttonObjectText = buttonObject.GetComponentInChildren<Text>();
            buttonObjectText.text = moduleData.ButtonText;
            buttonObject.name = $"Button:{moduleData.Module}";
            buttonObject.SetActive(true);
            buttons.Add(buttonObject);

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(moduleData.ToggleShowPanel);
            ((RectTransform) contentObject.transform).sizeDelta = new Vector2(0, y + buttonHeight + YOffset);
        }

        public ModuleData CreateModuleForTopic(string topic, string type)
        {
            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out Resource.Module resource))
            {
                throw new ArgumentException(nameof(type));
            }

            return CreateModule(resource, topic, type);
        }

        public void RemoveModule(ModuleData entry)
        {
            RemoveModule(moduleDatas.IndexOf(entry));
        }

        void RemoveModule(int index)
        {
            topicsWithModule.Remove(moduleDatas[index].Topic);
            moduleDatas[index].Stop();
            moduleDatas.RemoveAt(index);

            RemoveButton(index);
        }

        void RemoveButton(int index)
        {
            GameObject displayButton = buttons[index];
            buttons.RemoveAt(index);

            displayButton.GetComponent<Button>().onClick.RemoveAllListeners();
            ResourcePool.Dispose(Resource.Widgets.DisplayButton, displayButton);

            int i;
            for (i = index; i < buttons.Count; i++)
            {
                GameObject buttonObject = buttons[i];
                float y = 2 * YOffset + i * (buttonHeight + YOffset);
                ((RectTransform) buttonObject.transform).anchoredPosition = new Vector3(0, -y);
            }

            ((RectTransform) contentObject.transform).sizeDelta =
                new Vector2(0, 2 * YOffset + i * (buttonHeight + YOffset));
        }

        public void UpdateModuleButton(ModuleData entry, string content)
        {
            int index = moduleDatas.IndexOf(entry);
            if (index == -1)
            {
                return;
            }

            GameObject buttonObject = buttons[index];
            Text text = buttonObject.GetComponentInChildren<Text>();
            text.text = content;
            int lineBreaks = content.Count(x => x == '\n');
            switch (lineBreaks)
            {
                case 2:
                    text.fontSize = 11;
                    break;
                case 3:
                    text.fontSize = 10;
                    break;
                default:
                    text.fontSize = 12;
                    break;
            }
        }

        public void RegisterDisplayedTopic(string topic)
        {
            topicsWithModule.Add(topic);
        }

        public void ShowImageDialog(IImageDialogListener caller)
        {
            imageData.Listener = caller;
            imageData.Show();
        }

        public void ShowFrame(TfFrame frame)
        {
            tfTreeData.Show(frame);
        }

        void UpdateFpsStats()
        {
            bottomTime.text = $"<b>{DateTime.Now:HH:mm:ss}</b>";
            bottomFps.text = $"<b>{frameCounter} FPS</b>";
            frameCounter = 0;

            var (downB, upB) = ConnectionManager.CollectBandwidthReport();
            int downKb = downB / 1000;
            int upKb = upB / 1000;
            bottomBandwidth.text = $"<b>↓{downKb:N0}kB/s ↑{upKb:N0}kB/s</b>";
        }

        void UpdateFpsCounter()
        {
            frameCounter++;
        }

        void OnARModeChanged(bool value)
        {
            PinControlButton.Visible = value;
            ShowControlButton.Visible = value;

            foreach (var module in ModuleDatas)
            {
                module.OnARModeChanged(value);
            }
        }
    }
}