using Iviz.Roslib;
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
using Iviz.Tools;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;
using Logger = Iviz.Core.Logger;
using Quaternion = UnityEngine.Quaternion;

namespace Iviz.App
{
    public sealed class ModuleListPanel : MonoBehaviour
    {
        public const int ModuleDataCaptionWidth = 200;

        [SerializeField] DataLabelWidget masterUriStr = null;
        [SerializeField] TopButtonWidget dragButton = null;
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
        [SerializeField] Button enableAR = null;
        [SerializeField] Button showNetwork = null;
        [SerializeField] Button showConsole = null;
        [SerializeField] Button showSettings = null;
        [SerializeField] Button showEcho = null;
        [SerializeField] Button showSystem = null;
        [SerializeField] Button middleHideGuiButton = null;

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

        [SerializeField] Canvas contentCanvas = null;
        [SerializeField] GameObject moduleListCanvas = null;
        [SerializeField] GameObject dataPanelCanvas = null;

        [SerializeField] GameObject imageCanvasHolder = null;

        [SerializeField] ARSidePanel arSidePanel = null;
        [SerializeField] Canvas rootCanvas = null;

        [SerializeField] TMP_Text cameraText = null;

        [ItemNotNull] readonly List<ModuleData> moduleDatas = new();
        [ItemNotNull] readonly HashSet<string> topicsWithModule = new();

        int frameCounter;
        bool allGuiVisible = true;

        DialogData[] dialogDatas;
        DialogData availableModules;
        DialogData availableTopics;

        ConnectionDialogData connectionData;

        LoadConfigDialogData loadConfigData;
        SaveConfigDialogData saveConfigData;
        TfDialogData tfTreeData;
        MarkerDialogData markerData;
        NetworkDialogData networkData;
        ConsoleDialogData consoleData;
        SettingsDialogData settingsData;
        EchoDialogData echoData;
        SystemDialogData systemData;
        ARMarkerDialogData arMarkerData;

        readonly HashSet<ImageDialogData> imageDatas = new();

        public Controllers.ModelService ModelService { get; private set; }
        ControllerService controllerService;
        ModuleListButtons buttons;

        [SerializeField] GameObject menuObject = null;
        IMenuDialogContents menuDialog;

        bool initialized;
        static event Action InitFinished;

        [CanBeNull] public static GuiInputModule GuiInputModule => GuiInputModule.Instance;

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
                BottomHideGuiButton.State = value;

                moduleListCanvas.SetActive(value);
                dataPanelCanvas.SetActive(value);
                dialogPanelManager.Active = value;
                arSidePanel.Visible = !value;
            }
        }

        [CanBeNull] static ModuleListPanel instance;

        [NotNull]
        public static ModuleListPanel Instance =>
            instance.CheckedNull() ?? throw new InvalidOperationException("Module list panel has not been set!");

        public static AnchorCanvas AnchorCanvas => Instance.anchorCanvas;

        AnchorToggleButton BottomHideGuiButton => anchorCanvas.HideGui;
        Button LeftHideGuiButton => anchorCanvas.SideHideGui;
        Button MiddleHideGuiButton => middleHideGuiButton;

        AnchorToggleButton InteractableButton => anchorCanvas.Interact;
        public Button UnlockButton => anchorCanvas.Unlock;
        public DataPanelManager DataPanelManager => dataPanelManager;
        [NotNull] public DialogPanelManager DialogPanelManager => dialogPanelManager;
        public TwistJoystick TwistJoystick => twistJoystick;
        public ARJoystick ARJoystick => arJoystick;
        public ARSidePanel ARSidePanel => arSidePanel;
        [NotNull] public ReadOnlyCollection<ModuleData> ModuleDatas { get; }
        [NotNull] TfModuleData TfData => (TfModuleData)moduleDatas[0];
        [NotNull] public IEnumerable<string> DisplayedTopics => topicsWithModule;
        [NotNull] ModuleListButtons Buttons => buttons ??= new ModuleListButtons(contentObject);

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

        public static float CanvasScale => Instance.rootCanvas.scaleFactor;

        void Awake()
        {
            instance = this;
            Resource.ClearResources();
            GuiDialogListener.ClearResources();
            ARController.ClearResources();
        }

        void OnDestroy()
        {
            instance = null;
            GameThread.LateEverySecond -= UpdateFpsStats;
            GameThread.EveryFrame -= UpdateFpsCounter;
            GameThread.EveryFastTick -= UpdateCameraStats;

            foreach (var dialogData in dialogDatas)
            {
                dialogData.FinalizePanel();
            }
        }

        [NotNull]
        static string MasterUriToString([CanBeNull] Uri uri) =>
            uri == null || uri.AbsolutePath.Length == 0 ? $"{uri} →" : $"{uri.Host}:{uri.Port.ToString()} →";

        void Start()
        {
            availableModules = new AddModuleDialogData();
            availableTopics = new AddTopicDialogData();

            dialogDatas = new DialogData[]
            {
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
                arMarkerData = new ARMarkerDialogData(),
            };

            Directory.CreateDirectory(Settings.SavedFolder);
            LoadSimpleConfiguration();

            Logger.Internal("<b>Welcome to iviz!</b>");
            Logger.Internal("This is the log for connection messages. " +
                            "For general ROS log messages check the Log dialog.");

            CreateModule(ModuleType.TF, TfListener.DefaultTopic);

            if (!Settings.IsHololens)
            {
                CreateModule(ModuleType.Grid);
            }

            save.onClick.AddListener(saveConfigData.Show);
            load.onClick.AddListener(loadConfigData.Show);

            BottomHideGuiButton.Clicked += OnHideGuiButtonClick;
            BottomHideGuiButton.State = true;

            LeftHideGuiButton.onClick.AddListener(OnHideGuiButtonClick);
            MiddleHideGuiButton.onClick.AddListener(OnHideGuiButtonClick);

            ARController.ARStateChanged += OnARStateChanged;

            BottomHideGuiButton.Visible = !Settings.IsMobile;
            MiddleHideGuiButton.gameObject.SetActive(Settings.IsMobile);
            UpdateLeftHideVisible();

            SceneInteractable = true;
            InteractableButton.Visible = false;
            InteractableButton.Clicked += () => SceneInteractable = !SceneInteractable;

            addDisplayByTopic.onClick.AddListener(availableTopics.Show);
            addDisplay.onClick.AddListener(availableModules.Show);
            showTfTree.onClick.AddListener(tfTreeData.Show);
            enableAR.onClick.AddListener(OnToggleARClicked);
            showNetwork.onClick.AddListener(networkData.Show);
            showConsole.onClick.AddListener(consoleData.Show);
            showSettings.onClick.AddListener(settingsData.Show);
            showEcho.onClick.AddListener(echoData.Show);
            recordBag.onClick.AddListener(OnStartRecordBag);
            showSystem.onClick.AddListener(systemData.Show);

            masterUriStr.Label = MasterUriToString(connectionData.MasterUri);
            masterUriButton.Clicked += connectionData.Show;
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
            GameThread.LateEverySecond += UpdateFpsStats;
            GameThread.EveryFrame += UpdateFpsCounter;
            GameThread.EveryFastTick += UpdateCameraStats;
            UpdateFpsStats();

            controllerService = new ControllerService();
            ModelService = new Controllers.ModelService();

            menuDialog = menuObject.GetComponent<IMenuDialogContents>();
            menuObject.SetActive(false);

            AllGuiVisible = AllGuiVisible; // initialize value

            if (Settings.IsXR)
            {
                foreach (var subCanvas in rootCanvas.GetComponentsInChildren<Canvas>(true))
                {
                    subCanvas.gameObject.AddComponent<TrackedDeviceGraphicRaycaster>();
                }
            }

            initialized = true;

            InitFinished?.Invoke();
            InitFinished = null;
        }

        public static void CallAfterInitialized(Action action)
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

        void OnToggleARClicked()
        {
            if (ARController.Instance == null)
            {
                CreateModule(ModuleType.AugmentedReality);
            }
            else
            {
                ARController.Instance.Visible = !ARController.Instance.Visible;
            }
        }

        void OnARStateChanged(bool value)
        {
            if (value)
            {
                AllGuiVisible = false;
            }

            UpdateLeftHideVisible();
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
                string filename = $"iviz-{GameThread.Now:yyyy-MM-dd-HH-mm-ss}.bag";
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
                    topPanel.color = RosServerManager.IsActive
                        ? Resource.Colors.ConnectionPanelOwnMaster
                        : Resource.Colors.ConnectionPanelConnected;
                    SaveSimpleConfiguration();
                    break;
                case ConnectionState.Disconnected:
                    GameThread.EverySecond -= RotateSprite;
                    status.sprite = disconnectedSprite;
                    topPanel.color = Resource.Colors.ConnectionPanelDisconnected;
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
                ? Resource.Colors.ConnectionPanelWarning
                : RosServerManager.IsActive
                    ? Resource.Colors.ConnectionPanelOwnMaster
                    : Resource.Colors.ConnectionPanelConnected;
        }

        void RotateSprite()
        {
            status.rectTransform.Rotate(new Vector3(0, 0, 10.0f), Space.Self);
        }

        void OnHideGuiButtonClick()
        {
            AllGuiVisible = !AllGuiVisible;

            UpdateLeftHideVisible();

            EventSystem.current.SetSelectedGameObject(null);
        }

        void UpdateLeftHideVisible()
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            bool isMobile = Settings.IsMobile && !ARController.IsActive && !AllGuiVisible;
            LeftHideGuiButton.gameObject.SetActive(isMobile);
        }

        public async void SaveStateConfiguration([NotNull] string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var config = new StateConfiguration
            {
                Entries = moduleDatas.Select(moduleData => moduleData.Configuration.Id).ToList()
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

                var validHostAliases = config.HostAliases
                    .Where(alias => alias is { Hostname: { }, Address: { } })
                    .ToArray();
                systemData.HostAliases = validHostAliases;

                var validHostPairs =
                    validHostAliases.Select(alias => (alias.Hostname, alias.Address));
                ConnectionManager.Connection.SetHostAliases(validHostPairs);

                arMarkerData.Configuration = config.MarkersConfiguration;
            }
            catch (Exception e) when
                (e is IOException or SecurityException or JsonException)
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
                ConnectionConfiguration config = new()
                {
                    MasterUri = connectionData.MasterUri?.ToString() ?? "",
                    MyUri = connectionData.MyUri?.ToString() ?? "",
                    MyId = connectionData.MyId ?? "",
                    LastMasterUris = new List<Uri>(connectionData.LastMasterUris),
                    Settings = Settings.SettingsManager?.Config ?? new SettingsConfiguration(),
                    HostAliases = systemData.HostAliases,
                    MarkersConfiguration = arMarkerData.Configuration,
                };

                string text = JsonConvert.SerializeObject(config, Formatting.Indented);
                await FileUtils.WriteAllTextAsync(Settings.SimpleConfigurationPath, text, default);
            }
            catch (Exception e) when
                (e is IOException or SecurityException or JsonException)
            {
                Logger.Debug($"{this}: Error saving simple configuration", e);
            }
        }

        public void UpdateSimpleConfigurationSettings()
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
                config.MarkersConfiguration = arMarkerData.Configuration;
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

        public int NumMastersInCache => connectionData.LastMasterUris.Count;

        public async ValueTask ClearMastersCacheAsync(CancellationToken token = default)
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
                var config = JsonConvert.DeserializeObject<ConnectionConfiguration>(inText);
                config.LastMasterUris.Clear();

                string outText = JsonConvert.SerializeObject(config, Formatting.Indented);
                await FileUtils.WriteAllTextAsync(path, outText, token);
            }
            catch (Exception e) when
                (e is IOException or SecurityException or JsonException)
            {
            }
            catch (Exception e)
            {
                Logger.Error($"Error clearing cache", e);
            }
        }

        public static int NumSavedFiles => LoadConfigDialogData.SavedFiles.Count();

        public static void ClearSavedFiles()
        {
            foreach (var file in LoadConfigDialogData.SavedFiles)
            {
                try
                {
                    File.Delete(file.FullPath);
                }
                catch (Exception e)
                {
                    Logger.Error($"Error deleting file '{file}'", e);
                }
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

            var constructor = new ModuleDataConstructor(resource, topic, type, configuration);
            var moduleData = ModuleData.CreateFromResource(constructor);

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

        [NotNull]
        public ImageDialogData CreateImageDialog([NotNull] ImageDialogListener caller)
        {
            var newImageData = new ImageDialogData(caller, imageCanvasHolder.transform);
            imageDatas.Add(newImageData);
            return newImageData;
        }

        public void DisposeImageDialog([NotNull] ImageDialogData dialogData)
        {
            imageDatas.Remove(dialogData);
        }

        public void ShowMarkerDialog([NotNull] IMarkerDialogListener caller)
        {
            markerData.Show(caller ?? throw new ArgumentNullException(nameof(caller)));
        }

        public void ShowARMarkerDialog()
        {
            arMarkerData.Show();
        }

        void UpdateCameraStats()
        {
            var description = BuilderPool.Rent();
            try
            {
                description.Append(
                    Settings.IsXR
                        ? "<font=Bold>XR View</font>\n"
                        : ARController.IsVisible
                            ? "<font=Bold>AR View</font>\n"
                            : "<font=Bold>Virtual View</font>\n"
                );

                var currentCamera = Settings.MainCameraTransform;
                var cameraPose = TfListener.RelativePoseToFixedFrame(currentCamera.AsPose());
                TfLog.FormatPose(cameraPose, description, false);
                cameraText.SetText(description);
            }
            finally
            {
                BuilderPool.Return(description);
            }
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
            bottomBandwidth.text = $"↓{FormatBandwidth(downB)} ↑{FormatBandwidth(upB)}";

            var bagListener = ConnectionManager.Connection.BagListener;
            if (bagListener != null)
            {
                long bagSizeMb = bagListener.Length / (1024 * 1024);
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
                    int level = (int)(SystemInfo.batteryLevel * 100);
                    bottomBattery.text = state == BatteryStatus.Charging
                        ? $"<color=#005500>{level.ToString()}%</color>"
                        : $"{level.ToString()}%";
                    break;
            }
        }

        [NotNull]
        static string FormatBandwidth(long speedB)
        {
            if (speedB >= 1024 * 1024)
            {
                double speedMb = speedB / (1024d * 1024d);
                return $"{speedMb.ToString("N01")}MB/s";
            }

            long speedKb = speedB / 1024;
            return $"{speedKb.ToString("N0")}kB/s";
        }

        void UpdateFpsCounter()
        {
            frameCounter++;
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