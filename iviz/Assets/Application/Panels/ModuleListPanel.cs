using Iviz.Roslib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Logger = Iviz.Core.Logger;

namespace Iviz.App
{
    public sealed class ModuleListPanel : MonoBehaviour
    {
        const float YOffset = 2;

        public const int ModuleDataCaptionWidth = 200;

        static readonly Color ConnectedColor = new Color(0.6f, 1f, 0.5f, 0.4f);
        static readonly Color ConnectedOwnMasterColor = new Color(0.4f, 0.95f, 1f, 0.4f);
        static readonly Color DisconnectedColor = new Color(0.9f, 0.95f, 1f, 0.4f);
        static readonly Color ConnectedWarningColor = new Color(1f, 0.8f, 0.3f, 0.4f);

        [SerializeField] DataLabelWidget masterUriStr = null;
        [SerializeField] TrashButtonWidget masterUriButton = null;
        [SerializeField] TrashButtonWidget connectButton = null;
        [SerializeField] TrashButtonWidget stopButton = null;
        [SerializeField] Image topPanel = null;
        [SerializeField] Button save = null;
        [SerializeField] Button load = null;
        [SerializeField] Image status = null;

        [SerializeField] AnchorCanvas anchorCanvas = null;
        [SerializeField] GameObject contentObject = null;
        [SerializeField] DataPanelManager dataPanelManager = null;
        [SerializeField] DialogPanelManager dialogPanelManager = null;
        [SerializeField] Button addDisplayByTopic = null;
        [SerializeField] Button addDisplay = null;
        [SerializeField] Button showTfTree = null;
        [SerializeField] Button resetAll = null;
        [SerializeField] Button showNetwork = null;
        [SerializeField] Button showConsole = null;
        [SerializeField] Button showSettings = null;
        [SerializeField] Button showEcho = null;

        [SerializeField] Sprite connectedSprite = null;
        [SerializeField] Sprite connectingSprite = null;
        [SerializeField] Sprite disconnectedSprite = null;
        [SerializeField] Sprite questionSprite = null;

        [SerializeField] Text bottomTime = null;
        [SerializeField] Text bottomBattery = null;
        [SerializeField] Text bottomFps = null;
        [SerializeField] Text bottomBandwidth = null;

        [FormerlySerializedAs("joystick")] [SerializeField]
        TwistJoystick twistJoystick = null;

        [SerializeField] ARJoystick arJoystick = null;

        [ItemNotNull] readonly List<ModuleData> moduleDatas = new List<ModuleData>();
        [ItemNotNull] readonly HashSet<string> topicsWithModule = new HashSet<string>();

        int frameCounter;
        bool allGuiVisible = true;

        Canvas parentCanvas;
        DialogData availableModules;
        DialogData availableTopics;
        ConnectionDialogData connectionData;
        ImageDialogData imageData;
        LoadConfigDialogData loadConfigData;
        SaveConfigDialogData saveConfigData;
        TfDialogData tfTreeData;
        MarkerDialogData markerData;
        NetworkDialogData networkData;
        ConsoleDialogData consoleData;
        SettingsDialogData settingsData;
        EchoDialogData echoData;

        Controllers.ControllerService controllerService;
        Controllers.ModelService modelService;
        ModuleListButtons buttons;

        [SerializeField] GameObject menuObject = null;
        IMenuDialogContents menuDialog;

        bool initialized;
        public static event Action InitFinished;

        public IMenuDialogContents MenuDialog
        {
            get => menuDialog;
            set => menuDialog = value;
        }
        
        public ModuleListPanel()
        {
            ModuleDatas = moduleDatas.AsReadOnly();
        }

        public bool AllGuiVisible
        {
            get => allGuiVisible;
            set
            {
                allGuiVisible = value;
                if (parentCanvas == null)
                {
                    // not initialized yet
                    return;
                }

                parentCanvas.gameObject.SetActive(value);
            }
        }

        static ModuleListPanel instance;

        [NotNull]
        public static ModuleListPanel Instance =>
            instance.SafeNull() ?? throw new InvalidOperationException("Module list panel has not been set!");

        public static bool Initialized => Instance != null && Instance.initialized;
        public static AnchorCanvas AnchorCanvas => Instance.anchorCanvas;
        AnchorToggleButton HideGuiButton => anchorCanvas.HideGui;
        public AnchorToggleButton ShowARJoystickButton => anchorCanvas.ShowMarker;
        public AnchorToggleButton PinControlButton => anchorCanvas.PinMarker;
        public Button UnlockButton => anchorCanvas.Unlock;
        public DataPanelManager DataPanelManager => dataPanelManager;
        [NotNull] public DialogPanelManager DialogPanelManager => dialogPanelManager;
        public TwistJoystick TwistJoystick => twistJoystick;
        public ARJoystick ARJoystick => arJoystick;
        [NotNull] public ReadOnlyCollection<ModuleData> ModuleDatas { get; }
        [NotNull] TfModuleData TfData => (TfModuleData) moduleDatas[0];
        [NotNull] public IEnumerable<string> DisplayedTopics => topicsWithModule;

        [NotNull] ModuleListButtons Buttons => buttons ?? (buttons = new ModuleListButtons(contentObject));

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
            instance = this;
        }

        void OnDestroy()
        {
            instance = null;
            ConnectionManager.Connection.ConnectionStateChanged -= OnConnectionStateChanged;
            ConnectionManager.Connection.ConnectionWarningStateChanged -= OnConnectionWarningChanged;
            ARController.ARModeChanged -= OnARModeChanged;
            GameThread.LateEverySecond -= UpdateFpsStats;
            GameThread.EveryFrame -= UpdateFpsCounter;            
        }

        void Start()
        {
            parentCanvas = transform.parent.parent.GetComponentInParent<Canvas>();
            availableModules = new AddModuleDialogData();
            availableTopics = new AddTopicDialogData();

            imageData = new ImageDialogData();
            tfTreeData = new TfDialogData();
            loadConfigData = new LoadConfigDialogData();
            saveConfigData = new SaveConfigDialogData();
            markerData = new MarkerDialogData();
            networkData = new NetworkDialogData();
            connectionData = new ConnectionDialogData();
            consoleData = new ConsoleDialogData();
            settingsData = new SettingsDialogData();
            echoData = new EchoDialogData();

            Directory.CreateDirectory(Settings.SavedFolder);
            LoadSimpleConfiguration();

            Logger.Internal("<b>Welcome to iviz</b>");
            Logger.Info("Welcome to iviz! This is the log console.");

            CreateModule(Resource.ModuleType.TF, TfListener.DefaultTopic);

            if (!Settings.IsHololens)
            {
                CreateModule(Resource.ModuleType.Grid);
            }

            save.onClick.AddListener(saveConfigData.Show);
            load.onClick.AddListener(loadConfigData.Show);

            HideGuiButton.Clicked += OnHideGuiButtonClick;
            HideGuiButton.State = true;

            addDisplayByTopic.onClick.AddListener(availableTopics.Show);
            addDisplay.onClick.AddListener(availableModules.Show);
            showTfTree.onClick.AddListener(tfTreeData.Show);
            resetAll.onClick.AddListener(ResetAllModules);
            showNetwork.onClick.AddListener(networkData.Show);
            showConsole.onClick.AddListener(consoleData.Show);
            showSettings.onClick.AddListener(settingsData.Show);
            showEcho.onClick.AddListener(echoData.Show);

            string MasterUriToString(Uri uri) =>
                uri.AbsolutePath.Length == 0 ? $"{uri} →" : $"{uri.Host}:{uri.Port} →";

            masterUriStr.Label = MasterUriToString(connectionData.MasterUri);
            masterUriButton.Clicked += () => { connectionData.Show(); };

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
                    Logger.Internal("<b>Error:</b> Failed to set master uri. Reason: Uri is not valid.");
                    masterUriStr.Label = "(?) →";
                }
                else if (RosServerManager.IsActive)
                {
                    Logger.Internal($"Changing master uri to local master '{uri}'");
                    masterUriStr.Label = MasterUriToString(uri);
                }
                else
                {
                    Logger.Internal($"Changing master uri to '{uri}'");
                    masterUriStr.Label = MasterUriToString(uri);
                }
            };
            connectionData.MyIdChanged += id =>
            {
                if (id == null)
                {
                    Logger.Internal("<b>Error:</b> Failed to set caller id. Reason: Id is not a valid resource name.");
                    Logger.Internal("* First character must be alphanumeric [a-z A-Z] or a /");
                    Logger.Internal("* Remaining characters must be alphanumeric, digits, _ or /");
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
                Logger.Internal(uri == null 
                    ? "<b>Error:</b> Failed to set caller uri. Reason: Uri is not valid." 
                    : $"Changing caller uri to '{uri}'"
                );
            };
            stopButton.Clicked += () =>
            {
                Logger.Internal(
                    ConnectionManager.IsConnected
                        ? "Disconnection requested."
                        : "Already disconnected."
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

            connectionData.MasterActiveChanged += _ => { ConnectionManager.Connection.Disconnect(); };

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;
            ConnectionManager.Connection.ConnectionWarningStateChanged += OnConnectionWarningChanged;
            ARController.ARModeChanged += OnARModeChanged;
            GameThread.LateEverySecond += UpdateFpsStats;
            GameThread.EveryFrame += UpdateFpsCounter;
            UpdateFpsStats();

            controllerService = new ControllerService();
            modelService = new Controllers.ModelService();

            menuDialog = menuObject.GetComponent<IMenuDialogContents>();
            menuObject.SetActive(false);

            AllGuiVisible = AllGuiVisible; // initialize value

            initialized = true;
            
            InitFinished?.Invoke();
            InitFinished = null;
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

        void OnConnectionWarningChanged(bool value)
        {
            topPanel.color = value ? ConnectedWarningColor :
                RosServerManager.IsActive ? ConnectedOwnMasterColor : ConnectedColor;
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

        public void SetConnectionData(string masterUri, string myUri, string myId)
        {
            connectionData.MasterUri = new Uri(masterUri);
            connectionData.MyUri = new Uri(myUri);
            connectionData.MyId = myId;
            ConnectionManager.Connection.KeepReconnecting = true;
        }

        public void SaveStateConfiguration([NotNull] string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            StateConfiguration config = new StateConfiguration
            {
                MasterUri = connectionData.MasterUri,
                MyUri = connectionData.MyUri,
                MyId = connectionData.MyId,
                Entries = moduleDatas.Select(x => x.Configuration.Id).ToList()
            };
            foreach (var moduleData in moduleDatas)
            {
                moduleData.AddToState(config);
            }

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

        public void LoadStateConfiguration([NotNull] string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

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

            while (moduleDatas.Count > 1)
            {
                // TODO: refine this
                RemoveModule(1);
            }

            StateConfiguration stateConfig = JsonConvert.DeserializeObject<StateConfiguration>(text);

            connectionData.MasterUri = stateConfig.MasterUri;
            connectionData.MyUri = stateConfig.MyUri;
            connectionData.MyId = stateConfig.MyId;

            TfData.UpdateConfiguration(stateConfig.Tf);

            var configurations = stateConfig.CreateListOfEntries()
                .SelectMany(config => config)
                .Where(config => config != null);

            foreach (var config in configurations)
            {
                CreateModule(config.ModuleType, configuration: config);
            }

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
                Debug.Log("Using settings from " + path);

                string text = File.ReadAllText(path);
                ConnectionConfiguration config = JsonConvert.DeserializeObject<ConnectionConfiguration>(text);
                connectionData.MasterUri = config.MasterUri;
                connectionData.MyUri = config.MyUri;
                connectionData.MyId = config.MyId;

                if (config.LastMasterUris != null)
                {
                    connectionData.LastMasterUris = config.LastMasterUris;
                }
            }
            catch (Exception e) when
                (e is IOException || e is SecurityException || e is JsonException)
            {
                //Debug.Log(e);
            }
        }

        void SaveSimpleConfiguration()
        {
            connectionData.UpdateLastMasterUris();

            try
            {
                ConnectionConfiguration config = new ConnectionConfiguration
                {
                    MasterUri = connectionData.MasterUri,
                    MyUri = connectionData.MyUri,
                    MyId = connectionData.MyId,
                    LastMasterUris = new List<Uri>(connectionData.LastMasterUris)
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
            foreach (ModuleData m in moduleDatas)
            {
                m.ResetController();
            }
        }

        [NotNull]
        public ModuleData CreateModule(Resource.ModuleType resource,
            [NotNull] string topic = "",
            [NotNull] string type = "",
            [CanBeNull] IConfiguration configuration = null,
            [CanBeNull] string requestedId = null)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            ModuleDataConstructor constructor =
                new ModuleDataConstructor(resource, topic, type, configuration);

            ModuleData moduleData = ModuleData.CreateFromResource(constructor);

            if (requestedId != null)
            {
                moduleData.Configuration.Id = requestedId;
            }

            moduleDatas.Add(moduleData);
            Buttons.CreateButtonObject(moduleData);

            return moduleData;
        }

        [NotNull]
        public ModuleData CreateModuleForTopic([NotNull] string topic, [NotNull] string type)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out Resource.ModuleType resource))
            {
                throw new ArgumentException(nameof(type));
            }

            return CreateModule(resource, topic, type);
        }

        public void RemoveModule([NotNull] ModuleData entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            RemoveModule(moduleDatas.IndexOf(entry));
        }

        void RemoveModule(int index)
        {
            topicsWithModule.Remove(moduleDatas[index].Topic);
            moduleDatas[index].Stop();
            moduleDatas.RemoveAt(index);

            Buttons.RemoveButton(index);
        }


        public void UpdateModuleButton([NotNull] ModuleData entry, [NotNull] string content)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            int index = moduleDatas.IndexOf(entry);
            if (index == -1)
            {
                return;
            }
            
            Buttons.UpdateModuleButton(index, content);
        }

        public void RegisterDisplayedTopic([NotNull] string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            topicsWithModule.Add(topic);
        }

        public void ShowImageDialog([NotNull] IImageDialogListener caller)
        {
            imageData.Show(caller ?? throw new ArgumentNullException(nameof(caller)));
        }

        public void ShowMarkerDialog([NotNull] IMarkerDialogListener caller)
        {
            markerData.Show(caller ?? throw new ArgumentNullException(nameof(caller)));
        }

        void ShowFrame([NotNull] TfFrame frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            tfTreeData.Show(frame);
        }

        void UpdateFpsStats()
        {
            long memBytesKb = GC.GetTotalMemory(false) / (1024 * 1024);
            bottomTime.text = $"{memBytesKb:N0} MB"; 

            //bottomTime.text = DateTime.Now.ToString("HH:mm:ss");
            
            bottomFps.text = $"{frameCounter.ToString()} FPS";
            frameCounter = 0;

            var (downB, upB) = ConnectionManager.CollectBandwidthReport();
            long downKb = downB / 1000;
            long upKb = upB / 1000;
            bottomBandwidth.text = $"↓{downKb:N0}kB/s ↑{upKb:N0}kB/s";

            var state = SystemInfo.batteryStatus;
            switch (SystemInfo.batteryLevel)
            {
                case -1:
                    bottomBattery.text = "---";
                    break;
                case 1 when state == BatteryStatus.Full || state == BatteryStatus.Charging:
                    bottomBattery.text = "<color=#005500>Full</color>";
                    break;
                case 1:
                    bottomBattery.text = "Full";
                    break;
                default:
                    int level = (int) (SystemInfo.batteryLevel * 100);
                    bottomBattery.text = state == BatteryStatus.Charging
                        ? $"<color=#005500>{level}%</color>"
                        : $"{level}%";
                    break;
            }
        }

        void UpdateFpsCounter()
        {
            frameCounter++;
        }

        void OnARModeChanged(bool value)
        {
            foreach (var module in ModuleDatas)
            {
                module.OnARModeChanged(value);
            }
        }

        public void ShowMenu([NotNull] MenuEntryList menuEntries, Action<uint> callback, Vector3 unityPositionHint)
        {
            if (menuEntries == null)
            {
                throw new ArgumentNullException(nameof(menuEntries));
            }

            menuDialog.Set(menuEntries, unityPositionHint, callback);
        }
        
        class ModuleListButtons
        {
            [ItemNotNull] readonly List<GameObject> buttons = new List<GameObject>();
            readonly GameObject contentObject;
            readonly float buttonHeight;

            public ModuleListButtons(GameObject contentObject)
            {
                buttonHeight = Resource.Widgets.DisplayButton.Object.GetComponent<RectTransform>().rect.height;
                this.contentObject = contentObject;
            }
            
            public void CreateButtonObject([NotNull] ModuleData moduleData)
            {
                GameObject buttonObject =
                    ResourcePool.GetOrCreate(Resource.Widgets.DisplayButton, contentObject.transform, false);

                int size = buttons.Count;
                float y = 2 * YOffset + size * (buttonHeight + YOffset);

                ((RectTransform) buttonObject.transform).anchoredPosition = new Vector2(0, -y);

                Text buttonObjectText = buttonObject.GetComponentInChildren<Text>();
                buttonObjectText.text = moduleData.ButtonText;
                buttonObject.name = $"Button:{moduleData.ModuleType}";
                buttonObject.SetActive(true);
                buttons.Add(buttonObject);

                Button button = buttonObject.GetComponent<Button>();
                button.onClick.AddListener(moduleData.ToggleShowPanel);
                ((RectTransform) contentObject.transform).sizeDelta = new Vector2(0, y + buttonHeight + YOffset);
            }
            
            public void RemoveButton(int index)
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
            
            public void UpdateModuleButton(int index, [NotNull] string content)
            {
                if (index < 0 || index >= buttons.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                if (content == null)
                {
                    throw new ArgumentNullException(nameof(content));
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
        }
    }
}