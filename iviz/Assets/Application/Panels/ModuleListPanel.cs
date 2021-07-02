﻿using Iviz.Roslib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.MarkerDetection;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Logger = Iviz.Core.Logger;
using Quaternion = UnityEngine.Quaternion;

namespace Iviz.App
{
    public sealed class ModuleListPanel : MonoBehaviour
    {
        public const int ModuleDataCaptionWidth = 200;

        static readonly Color ConnectedColor = new Color(0.6f, 1f, 0.5f, 0.4f);
        static readonly Color ConnectedOwnMasterColor = new Color(0.4f, 0.95f, 1f, 0.4f);
        static readonly Color DisconnectedColor = new Color(0.9f, 0.95f, 1f, 0.4f);
        static readonly Color ConnectedWarningColor = new Color(1f, 0.8f, 0.3f, 0.4f);

        [SerializeField] DataLabelWidget masterUriStr = null;
        [SerializeField] DraggableButtonWidget dragButton = null;
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
        [SerializeField] Button showSystem = null;

        [SerializeField] Button recordBag = null;
        [SerializeField] Text recordBagText = null;
        [SerializeField] Image recordBagImage = null;

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

        [SerializeField] Canvas contentCanvas;
        [SerializeField] GameObject moduleListCanvas;
        [SerializeField] GameObject dataPanelCanvas;

        [ItemNotNull] readonly List<ModuleData> moduleDatas = new List<ModuleData>();
        [ItemNotNull] readonly HashSet<string> topicsWithModule = new HashSet<string>();

        int frameCounter;
        bool allGuiVisible = true;

        DialogData[] dialogDatas;
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
        SystemDialogData systemData;

        public Controllers.ModelService ModelService { get; private set; }
        ControllerService controllerService;
        ModuleListButtons buttons;

        [SerializeField] GameObject menuObject = null;
        IMenuDialogContents menuDialog;

        bool initialized;
        static event Action InitFinished;

        public IMenuDialogContents MenuDialog
        {
            get => menuDialog;
            set => menuDialog = value;
        }

        [CanBeNull]
        public static GuiInputModule GuiInputModule => GuiInputModule.Instance;

        bool dialogIsDragged;

        public bool DialogIsDragged
        {
            get => dialogIsDragged;
            set
            {
                dialogIsDragged = value;
                moduleListCanvas.gameObject.SetActive(!value);
                dataPanelCanvas.gameObject.SetActive(!value);
            }
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
                HideGuiButton.State = value;
                contentCanvas.gameObject.SetActive(value);
            }
        }

        [CanBeNull] static ModuleListPanel instance;

        [NotNull]
        public static ModuleListPanel Instance =>
            instance.CheckedNull() ?? throw new InvalidOperationException("Module list panel has not been set!");

        //public static bool Initialized => Instance != null && Instance.initialized;
        public static AnchorCanvas AnchorCanvas => Instance.anchorCanvas;
        AnchorToggleButton HideGuiButton => anchorCanvas.HideGui;
        public AnchorToggleButton ShowARJoystickButton => anchorCanvas.ShowMarker;
        public AnchorToggleButton PinControlButton => anchorCanvas.PinMarker;
        AnchorToggleButton InteractableButton => anchorCanvas.Interact;
        public Button UnlockButton => anchorCanvas.Unlock;
        public DataPanelManager DataPanelManager => dataPanelManager;
        [NotNull] public DialogPanelManager DialogPanelManager => dialogPanelManager;
        public TwistJoystick TwistJoystick => twistJoystick;
        public ARJoystick ARJoystick => arJoystick;
        [NotNull] public ReadOnlyCollection<ModuleData> ModuleDatas { get; }
        [NotNull] TfModuleData TfData => (TfModuleData) moduleDatas[0];
        [NotNull] public IEnumerable<string> DisplayedTopics => topicsWithModule;
        [NotNull] ModuleListButtons Buttons => buttons ?? (buttons = new ModuleListButtons(contentObject));

        bool sceneInteractable;

        public bool SceneInteractable
        {
            get => sceneInteractable;
            private set
            {
                sceneInteractable = value;
                foreach (var moduleData in moduleDatas.OfType<IInteractableModuleData>())
                {
                    moduleData.Interactable = value;
                }
            }
        }


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

            foreach (var dialogData in dialogDatas)
            {
                dialogData.FinalizePanel();
            }
        }

        [NotNull]
        static string MasterUriToString([CanBeNull] Uri uri) =>
            uri == null || uri.AbsolutePath.Length == 0 ? $"{uri} →" : $"{uri.Host}:{uri.Port} →";

        void Start()
        {
            Resource.ClearResources();
            GuiDialogListener.ClearResources();

            //parentCanvas = transform.parent.parent.GetComponentInParent<Canvas>();
            availableModules = new AddModuleDialogData();
            availableTopics = new AddTopicDialogData();

            dialogDatas = new DialogData[]
            {
                imageData = new ImageDialogData(),
                tfTreeData = new TfDialogData(),
                loadConfigData = new LoadConfigDialogData(),
                saveConfigData = new SaveConfigDialogData(),
                markerData = new MarkerDialogData(),
                networkData = new NetworkDialogData(),
                connectionData = new ConnectionDialogData(),
                consoleData = new ConsoleDialogData(),
                settingsData = new SettingsDialogData(),
                echoData = new EchoDialogData(),
                systemData = new SystemDialogData(),
            };

            Directory.CreateDirectory(Settings.SavedFolder);
            LoadSimpleConfiguration();

            Logger.Internal("<b>Welcome to iviz</b>");
            Logger.Info("Welcome to iviz! This is the log console.");

            CreateModule(ModuleType.TF, TfListener.DefaultTopic);

            if (!Settings.IsHololens)
            {
                CreateModule(ModuleType.Grid);
            }

            save.onClick.AddListener(saveConfigData.Show);
            load.onClick.AddListener(loadConfigData.Show);

            HideGuiButton.Clicked += OnHideGuiButtonClick;
            HideGuiButton.State = true;

            SceneInteractable = true;
            InteractableButton.Visible = false;
            InteractableButton.Clicked += () => SceneInteractable = !SceneInteractable;

            addDisplayByTopic.onClick.AddListener(availableTopics.Show);
            addDisplay.onClick.AddListener(availableModules.Show);
            showTfTree.onClick.AddListener(tfTreeData.Show);
            resetAll.onClick.AddListener(ResetAllModules);
            showNetwork.onClick.AddListener(networkData.Show);
            showConsole.onClick.AddListener(consoleData.Show);
            showSettings.onClick.AddListener(settingsData.Show);
            showEcho.onClick.AddListener(echoData.Show);
            recordBag.onClick.AddListener(OnStartRecordBag);
            showSystem.onClick.AddListener(systemData.Show);

            ShowARJoystickButton.Clicked += () =>
            {
                // should be !Visible, but the new Visible hasn't been set yet
                TwistJoystick.RightJoystickVisible = ARJoystick.Visible;
            };

            masterUriStr.Label = MasterUriToString(connectionData.MasterUri);
            masterUriButton.Clicked += () => connectionData.Show();
            dragButton.Dragged += OnHideGuiButtonClick;

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
                    Logger.Internal("First character must be alphanumeric [a-z A-Z] or a '/'");
                    Logger.Internal("Remaining characters must be alphanumeric, digits, '_' or '/'");
                    return;
                }

                ConnectionManager.Connection.MyId = id;
                KeepReconnecting = false;
                Logger.Internal($"Changing my ROS id to '{id}'");
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

            connectionData.MasterActiveChanged += _ => ConnectionManager.Connection.Disconnect();

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;
            ConnectionManager.Connection.ConnectionWarningStateChanged += OnConnectionWarningChanged;
            ARController.ARModeChanged += OnARModeChanged;
            GameThread.LateEverySecond += UpdateFpsStats;
            GameThread.EveryFrame += UpdateFpsCounter;
            UpdateFpsStats();

            controllerService = new ControllerService();
            ModelService = new Controllers.ModelService();

            menuDialog = menuObject.GetComponent<IMenuDialogContents>();
            menuObject.SetActive(false);

            AllGuiVisible = AllGuiVisible; // initialize value

            initialized = true;

            InitFinished?.Invoke();
            InitFinished = null;
        }

        public static void CallWhenInitialized(Action action)
        {
            if (instance != null && instance.initialized)
            {
                action();
            }
            else
            {
                InitFinished += action;
            }
        }

        void OnStartRecordBag()
        {
            if (ConnectionManager.Connection.BagListener != null)
            {
                ConnectionManager.Connection.BagListener = null;
                recordBagImage.color = Color.black;
                recordBagText.text = "Rec Bag";
            }
            else
            {
                string filename = $"{GameThread.Now:yyyy-MM-dd-HH-mm-ss}.bag";
                Directory.CreateDirectory(Settings.BagsFolder);
                ConnectionManager.Connection.BagListener = new BagListener($"{Settings.BagsFolder}/{filename}");
                recordBagImage.color = Color.red;
                recordBagText.text = "0 MB";
            }
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
            topPanel.color = value
                ? ConnectedWarningColor
                : RosServerManager.IsActive
                    ? ConnectedOwnMasterColor
                    : ConnectedColor;
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

        public void SetConnectionData([NotNull] string masterUri, [NotNull] string myUri, string myId)
        {
            connectionData.MasterUri = new Uri(masterUri);
            connectionData.MyUri = new Uri(myUri);
            connectionData.MyId = myId;
            //ConnectionManager.Connection.KeepReconnecting = true;
        }

        public async void SaveStateConfiguration([NotNull] string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            StateConfiguration config = new StateConfiguration
            {
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
                await FileUtils.WriteAllTextAsync($"{Settings.SavedFolder}/{file}", text, default);
                Logger.Internal("Done.");
            }
            catch (Exception e)
            {
                Logger.Internal("Error saving state configuration", e);
                return;
            }

            Logger.Debug("DisplayListPanel: Writing config to " + Settings.SavedFolder + "/" + file);
        }

        public async void LoadStateConfiguration([NotNull] string file, CancellationToken token = default)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            Logger.Debug($"DisplayListPanel: Reading config from {Settings.SavedFolder}/{file}");
            string text;
            try
            {
                Logger.Internal("Loading config file...");
                text = await FileUtils.ReadAllTextAsync($"{Settings.SavedFolder}/{file}", token);
                Logger.Internal("Done.");
            }
            catch (FileNotFoundException)
            {
                Logger.Internal("<b>Error:</b> Config file not found.");
                return;
            }
            catch (Exception e)
            {
                Logger.Internal("Error loading state configuration", e);
                return;
            }

            while (moduleDatas.Count > 1)
            {
                // TODO: refine this
                RemoveModule(1);
            }

            StateConfiguration stateConfig = JsonConvert.DeserializeObject<StateConfiguration>(text);

            TfData.UpdateConfiguration(stateConfig.Tf);

            var configurations = stateConfig.CreateListOfEntries()
                .SelectMany(config => config)
                .Where(config => config != null);

            foreach (var config in configurations)
            {
                CreateModule(config.ModuleType, configuration: config);
            }
        }

        void LoadSimpleConfiguration()
        {
            string path = Settings.SimpleConfigurationPath;

            try
            {
                if (!File.Exists(path))
                {
                    return;
                }

                Debug.Log("Using settings from " + path);

                string text = File.ReadAllText(path);
                ConnectionConfiguration config = JsonConvert.DeserializeObject<ConnectionConfiguration>(text);
                if (config == null)
                {
                    return; // empty text
                }

                connectionData.MasterUri = string.IsNullOrEmpty(config.MasterUri)
                    ? null
                    : new Uri(config.MasterUri);
                connectionData.MyUri = string.IsNullOrEmpty(config.MyUri)
                    ? null
                    : new Uri(config.MyUri);
                connectionData.MyId = config.MyId;
                if (config.LastMasterUris.Count != 0)
                {
                    connectionData.LastMasterUris = config.LastMasterUris;
                }

                if (Settings.SettingsManager != null)
                {
                    Settings.SettingsManager.Config = config.Settings;
                }

                systemData.HostAliases = config.HostAliases;
            }
            catch (Exception e) when
                (e is IOException || e is SecurityException || e is JsonException)
            {
                Logger.Debug($"{this}: Error loading simple configuration", e);
                File.Delete(path);
            }
        }

        async void SaveSimpleConfiguration()
        {
            connectionData.UpdateLastMasterUris();

            try
            {
                ConnectionConfiguration config = new ConnectionConfiguration
                {
                    MasterUri = connectionData.MasterUri?.ToString() ?? "",
                    MyUri = connectionData.MyUri?.ToString() ?? "",
                    MyId = connectionData.MyId ?? "",
                    LastMasterUris = new List<Uri>(connectionData.LastMasterUris),
                    Settings = Settings.SettingsManager?.Config ?? new SettingsConfiguration(),
                    HostAliases = systemData.HostAliases,
                };

                string text = JsonConvert.SerializeObject(config, Formatting.Indented);
                await FileUtils.WriteAllTextAsync(Settings.SimpleConfigurationPath, text, default);
            }
            catch (Exception e) when
                (e is IOException || e is SecurityException || e is JsonException)
            {
                Logger.Debug($"{this}: Error saving simple configuration", e);
            }
        }

        void UpdateSimpleConfigurationSettings()
        {
            string path = Settings.SimpleConfigurationPath;
            if (Settings.SettingsManager == null || !File.Exists(path))
            {
                return;
            }

            try
            {
                string inText = File.ReadAllText(path);
                ConnectionConfiguration config = JsonConvert.DeserializeObject<ConnectionConfiguration>(inText);
                config.Settings = Settings.SettingsManager.Config;
                config.HostAliases = systemData.HostAliases;
                string outText = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(path, outText);
            }
            catch (Exception e) when
                (e is IOException || e is SecurityException || e is JsonException)
            {
                Logger.Debug("ModuleListPanel: Error updating simple configuration", e);
            }
        }

        public void UpdateSettings()
        {
            UpdateSimpleConfigurationSettings();
            foreach (var gridModuleData in moduleDatas.OfType<GridModuleData>())
            {
                gridModuleData.GridController.OnSettingsChanged();
            }
        }

        public void UpdateAddresses()
        {
            UpdateSimpleConfigurationSettings();
        }

        public int NumMastersInCache => connectionData.LastMasterUris.Count;

        public async Task ClearMastersCacheAsync(CancellationToken token = default)
        {
            string path = Settings.SimpleConfigurationPath;
            if (Settings.SettingsManager == null || !File.Exists(path))
            {
                return;
            }

            connectionData.LastMasterUris = new List<Uri>();

            try
            {
                string inText = await FileUtils.ReadAllTextAsync(path, token);
                ConnectionConfiguration config = JsonConvert.DeserializeObject<ConnectionConfiguration>(inText);
                config.LastMasterUris = new List<Uri>();

                string outText = JsonConvert.SerializeObject(config, Formatting.Indented);
                await FileUtils.WriteAllTextAsync(path, outText, token);
            }
            catch (Exception e) when
                (e is IOException || e is SecurityException || e is JsonException)
            {
            }
        }

        public static int NumSavedFiles => LoadConfigDialogData.SavedFiles.Count();

        public static void ClearSavedFiles()
        {
            foreach (string file in LoadConfigDialogData.SavedFiles)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    Logger.Error($"Error deleting file '{file}'", e);
                }
            }
        }

        void ResetAllModules()
        {
            foreach (ModuleData m in moduleDatas)
            {
                m.ResetController();
            }
        }

        void CheckIfInteractableNeeded()
        {
            InteractableButton.Visible = ModuleDatas.Any(module => module is IInteractableModuleData);
        }

        [NotNull]
        public ModuleData CreateModule(ModuleType resource,
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
            else if (configuration != null)
            {
                moduleData.Configuration.Id = configuration.Id;
            }

            moduleDatas.Add(moduleData);
            Buttons.CreateButtonForModule(moduleData);

            if (moduleData is IInteractableModuleData)
            {
                InteractableButton.Visible = true;
            }

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

            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out var resource))
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

            if (entry.ModuleType == ModuleType.InteractiveMarker)
            {
                CheckIfInteractableNeeded();
            }
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

            Buttons.UpdateButton(index, content);
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

        void UpdateFpsStats()
        {
#if UNITY_EDITOR
            long memBytesKb = GC.GetTotalMemory(false) / (1024 * 1024);
            bottomTime.text = $"M: {memBytesKb.ToString()}M";
#else
            bottomTime.text = GameThread.Now.ToString("HH:mm:ss");
#endif
            bottomFps.text = $"{frameCounter.ToString()} FPS";
            frameCounter = 0;

            (long downB, long upB) = ConnectionManager.CollectBandwidthReport();
            long downKb = downB / 1000;
            long upKb = upB / 1000;
            bottomBandwidth.text = $"↓{downKb.ToString("N0")}kB/s ↑{upKb.ToString("N0")}kB/s";

            if (ConnectionManager.Connection.BagListener != null)
            {
                long bagSizeMb = ConnectionManager.Connection.BagListener.Length / (1024 * 1024);
                recordBagText.text = $"{bagSizeMb.ToString()} MB";
            }

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
                        ? $"<color=#005500>{level.ToString()}%</color>"
                        : $"{level.ToString()}%";
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

        public void ShowMenu([NotNull] MenuEntryList menuEntries, [NotNull] Action<uint> callback,
            Vector3 unityPositionHint)
        {
            if (menuEntries == null)
            {
                throw new ArgumentNullException(nameof(menuEntries));
            }

            menuDialog.Set(menuEntries, unityPositionHint, callback);
        }
    }
}