#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    /// <summary>
    /// Handles the buttons on the panel on the left in the iviz GUI.
    /// In practice a central hub for all the modules. Needs heavy refactoring.
    /// <see cref="Start"/> is basically the "starting point" of the program.
    /// </summary>
    public sealed class ModuleListPanel : MonoBehaviour
    {
        const int ModuleDataCaptionWidth = 220;

        static ModuleListPanel? instance;

        static event Action? InitFinished;

        public static ModuleListPanel Instance => TryGetInstance() ??
                                                  throw new MissingAssetFieldException(
                                                      "Module list panel has not been set!");

        public static ModuleListPanel? TryGetInstance()
        {
            if (instance != null)
            {
                return instance;
            }

            if (GameObject.Find("ModuleList Panel") is { } instanceObject
                && instanceObject.GetComponent<ModuleListPanel>() is { } newInstance)
            {
                return (instance = newInstance);
            }

            return null;
        }

        public static float CanvasScale => Instance.RootCanvas.scaleFactor;

        public static Rect CanvasSize => ((RectTransform)Instance.RootCanvas.transform).rect;

        [SerializeField] Button? middleHideGuiButton;

        [SerializeField] AnchorCanvasPanel? anchorCanvasPanel;
        [SerializeField] UpperCanvasPanel? upperCanvasPanel;
        [SerializeField] BottomCanvasPanel? bottomCanvasPanel;
        [SerializeField] ARToolbarPanel? arSidePanel;
        [SerializeField] ModulePanelManager? dataPanelManager;
        [SerializeField] DialogPanelManager? dialogPanelManager;
        [SerializeField] ARJoystick? arJoystick;
        [SerializeField] TwistJoystick? twistJoystick;
        [SerializeField] GameObject? contentObject;
        [SerializeField] Canvas? rootCanvas;
        [SerializeField] XRContents? xrController;
        [SerializeField] GameObject? imageCanvasHolder;

        [SerializeField] GameObject? moduleListCanvas;
        [SerializeField] GameObject? dataPanelCanvas;
        [SerializeField] GameObject? menuObject;

        readonly List<ModuleData> moduleDatas = new();
        readonly HashSet<string> topicsWithModule = new();
        readonly HashSet<ImageDialogData> imageDatas = new();
        readonly TfPublisher tfPublisher = new();

        RosManager? connectionManager;
        TfModule? tfModule;

        int frameCounter;
        bool allGuiVisible = true;
        bool initialized;
        bool sceneInteractable;

        ModuleListButtons? buttons;
        DialogManager? dialogs;
        IMenuDialogContents? menuDialog;
        CameraPanelData? cameraPanelData;

        CameraPanelData CameraPanelData => cameraPanelData ??= new CameraPanelData();
        UpperCanvasPanel UpperCanvas => upperCanvasPanel.AssertNotNull(nameof(upperCanvasPanel));
        BottomCanvasPanel BottomCanvas => bottomCanvasPanel.AssertNotNull(nameof(bottomCanvasPanel));
        AnchorToggleButton BottomHideGuiButton => AnchorCanvasPanel.BottomHideGui;
        Button LeftHideGuiButton => AnchorCanvasPanel.LeftHideGui;
        Button MiddleHideGuiButton => middleHideGuiButton.AssertNotNull(nameof(middleHideGuiButton));
        AnchorToggleButton InteractableButton => AnchorCanvasPanel.Interact;
        GameObject ModuleListCanvas => moduleListCanvas.AssertNotNull(nameof(moduleListCanvas));
        GameObject DataPanelCanvas => dataPanelCanvas.AssertNotNull(nameof(dataPanelCanvas));
        DialogManager Dialogs => dialogs ??= new DialogManager();
        TfModuleData TfData => (TfModuleData)moduleDatas[0];
        Canvas RootCanvas => rootCanvas.AssertNotNull(nameof(rootCanvas));

        ModuleListButtons Buttons =>
            buttons ??= new ModuleListButtons(contentObject.AssertNotNull(nameof(contentObject)));

        static RoslibConnection Connection => RosManager.Connection;

        public AnchorCanvasPanel AnchorCanvasPanel => anchorCanvasPanel.AssertNotNull(nameof(anchorCanvasPanel));
        public Button UnlockButton => AnchorCanvasPanel.Unlock;
        public ModulePanelManager ModulePanelManager => dataPanelManager.AssertNotNull(nameof(dataPanelManager));
        public DialogPanelManager DialogPanelManager => dialogPanelManager.AssertNotNull(nameof(dialogPanelManager));
        public TwistJoystick TwistJoystick => twistJoystick.AssertNotNull(nameof(twistJoystick));
        public ARJoystick ARJoystick => arJoystick.AssertNotNull(nameof(arJoystick));
        public ARToolbarPanel ARToolbarPanel => arSidePanel.AssertNotNull(nameof(arSidePanel));

        public XRContents XRController => xrController != null
            ? xrController
            : throw new MissingAssetFieldException("Tried to access XRController, but the scene has not set any!");

        public IReadOnlyCollection<ModuleData> ModuleDatas => moduleDatas;
        public IEnumerable<string> DisplayedTopics => topicsWithModule;

        public bool AllGuiVisible
        {
            get => allGuiVisible;
            set
            {
                allGuiVisible = value;
                BottomHideGuiButton.State = value;
                ModuleListCanvas.SetActive(value);
                DataPanelCanvas.SetActive(value);
                DialogPanelManager.Active = value;
                ARToolbarPanel.Visible = !value;
            }
        }

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

        static bool KeepReconnecting
        {
            set => Connection.KeepReconnecting = value;
        }

        public int NumMastersInCache => Dialogs.ConnectionData.LastMasterUris.Count;

        void Awake()
        {
            // clear static stuff in case domain reloading is disabled
            // this is only really needed in the editor
            Settings.ClearResources();
            Resource.ClearResources();
            GuiWidgetListener.ClearResources();
            ARController.ClearResources();
            ResourcePool.ClearResources();

            Settings.SettingsManager = new SettingsManager();
        }

        public void Dispose()
        {
            GameThread.LateEverySecond -= UpdateFpsStats;
            GameThread.EveryFrame -= UpdateFpsCounter;
            GameThread.EveryTenthOfASecond -= UpdateCameraStats;

            foreach (var moduleData in moduleDatas)
            {
                moduleData.Dispose();
            }

            foreach (var dialogData in Dialogs.DialogDatas)
            {
                dialogData.Dispose();
            }

            foreach (var imageData in imageDatas)
            {
                imageData.Dispose();
            }

            tfPublisher.Dispose();
            cameraPanelData?.Dispose();
            connectionManager?.Dispose();

            tfModule?.Dispose();

            GuiWidgetListener.DisposeDefaultHandler();
            instance = null;
        }

        static string MasterUriToString(Uri? uri) =>
            uri != null && uri.AbsolutePath.Length != 0 ? $"{uri.Host}:{uri.Port.ToString()}" : "";

        void Start()
        {
            if (Settings.IsHololens)
            {
                XRUtils.SetupForHololens();
            }

            connectionManager = new RosManager();
            tfModule = new TfModule(id => new TfFrameDisplay(id));
            cameraPanelData = new CameraPanelData();

            Directory.CreateDirectory(Settings.SavedFolder);
            LoadSimpleConfiguration();

            RosLogger.Internal("<b>Welcome to iviz!</b>");
            RosLogger.Internal("This is the log for connection messages. " +
                               "For general ROS log messages check the Log dialog.");

            CreateModule(ModuleType.TF);

            if (!Settings.IsHololens)
            {
                CreateModule(ModuleType.Grid, configuration: new GridConfiguration { Id = "Grid" });
            }

            UpperCanvas.Save.onClick.AddListener(Dialogs.SaveConfigData.Show);
            UpperCanvas.Load.onClick.AddListener(Dialogs.LoadConfigData.Show);

            BottomHideGuiButton.Clicked += OnHideGuiButtonClick;
            BottomHideGuiButton.State = true;

            LeftHideGuiButton.onClick.AddListener(OnHideGuiButtonClick);
            MiddleHideGuiButton.onClick.AddListener(OnHideGuiButtonClick);

            ARController.ARStateChanged += OnARStateChanged;

            BottomHideGuiButton.Visible = !Settings.IsMobile && !Settings.IsXR;
            MiddleHideGuiButton.gameObject.SetActive(Settings.IsMobile && !Settings.IsXR);
            UpdateLeftHideVisible();

            SceneInteractable = true;
            InteractableButton.Visible = false;
            InteractableButton.Clicked += () => SceneInteractable = !SceneInteractable;

            UpperCanvas.AddDisplayByTopic.onClick.AddListener(Dialogs.AvailableTopics.Show);
            UpperCanvas.AddModule.onClick.AddListener(Dialogs.AvailableModules.Show);
            UpperCanvas.ShowTfTree.onClick.AddListener(Dialogs.TfTreeData.Show);
            UpperCanvas.EnableAR.onClick.AddListener(OnToggleARClicked);
            UpperCanvas.ShowNetwork.onClick.AddListener(Dialogs.NetworkData.Show);
            UpperCanvas.ShowConsole.onClick.AddListener(Dialogs.ConsoleData.Show);
            UpperCanvas.ShowSettings.onClick.AddListener(Dialogs.SettingsData.Show);
            UpperCanvas.ShowEcho.onClick.AddListener(Dialogs.EchoData.Show);
            UpperCanvas.RecordBag.onClick.AddListener(OnStartRecordBag);
            UpperCanvas.ShowSystem.onClick.AddListener(Dialogs.SystemData.Show);

            var connectionData = Dialogs.ConnectionData;
            UpperCanvas.MasterUriStr.Text = MasterUriToString(connectionData.MasterUri);
            UpperCanvas.MasterUriButton.Clicked += connectionData.Show;

            Connection.MasterUri = connectionData.MasterUri;
            Connection.MyUri = connectionData.MyUri;
            Connection.MyId = connectionData.MyId;
            KeepReconnecting = false;

            connectionData.MasterUriChanged += uri =>
            {
                Connection.MasterUri = uri;
                KeepReconnecting = false;
                if (uri == null)
                {
                    RosLogger.Internal("<b>Error:</b> Failed to set master uri. Reason: Uri is not valid.");
                    UpperCanvas.MasterUriStr.Text = "(?) →";
                }
                else if (RosManager.Server.IsActive)
                {
                    RosLogger.Internal($"Changing master uri to local master '{uri}'");
                    UpperCanvas.MasterUriStr.Text = MasterUriToString(uri);
                }
                else
                {
                    RosLogger.Internal($"Changing master uri to '{uri}'");
                    UpperCanvas.MasterUriStr.Text = MasterUriToString(uri);
                }
            };
            connectionData.MyIdChanged += id =>
            {
                if (id == null)
                {
                    RosLogger.Internal(
                        "<b>Error:</b> Failed to set caller id. Reason: Id is not a valid resource name.");
                    RosLogger.Internal("First character must be alphanumeric [a-z A-Z] or a '/'");
                    RosLogger.Internal("Remaining characters must be alphanumeric, digits, '_' or '/'");
                    return;
                }

                Connection.MyId = id;
                KeepReconnecting = false;
                RosLogger.Internal($"Changing my ROS id to '{id}'");
            };
            connectionData.MyUriChanged += uri =>
            {
                Connection.MyUri = uri;
                KeepReconnecting = false;
                RosLogger.Internal(uri == null
                    ? "<b>Error:</b> Failed to set caller uri. Reason: Uri is not valid."
                    : $"Changing caller uri to '{uri}'"
                );
            };
            UpperCanvas.StopButton.Clicked += () =>
            {
                RosLogger.Internal(
                    RosManager.IsConnected
                        ? "Disconnection requested."
                        : "Already disconnected."
                );
                KeepReconnecting = false;
                Connection.Disconnect();
            };
            UpperCanvas.ConnectButton.Clicked += () =>
            {
                RosLogger.Internal(
                    RosManager.IsConnected ? "Reconnection requested." : "Connection requested."
                );
                Connection.Disconnect();
                KeepReconnecting = true;
            };

            BottomCanvas.CameraButtonClicked += () => { CameraPanelData.ToggleShowPanel(); };

            connectionData.MasterActiveChanged += _ => Connection.Disconnect();
            RosConnection.ConnectionStateChanged += OnConnectionStateChanged;
            RosConnection.ConnectionWarningStateChanged += OnConnectionWarningChanged;
            GameThread.LateEverySecond += UpdateFpsStats;
            GameThread.EveryFrame += UpdateFpsCounter;
            GameThread.EveryTenthOfASecond += UpdateCameraStats;
            UpdateFpsStats();

            ControllerService.Start();

            if (menuObject != null)
            {
                menuDialog = menuObject.AssertHasComponent<IMenuDialogContents>(nameof(menuObject));
                menuObject.SetActive(false);
            }
            else
            {
                throw new NullReferenceException("The menu dialog is not set!");
            }

            AllGuiVisible = AllGuiVisible; // initialize value

            if (Settings.IsXR)
            {
                if (xrController == null)
                {
                    throw new NullReferenceException("XR is enabled, but the XR controller is not set");
                }

                UpperCanvas.EnableAR.gameObject.SetActive(false);
                CreateModule(ModuleType.XR);
                RootCanvas.ProcessCanvasForXR();
            }

            TryLoadDefaultStateConfiguration();

            initialized = true;

            InitFinished?.Invoke();
            InitFinished = null;

            if (Connection.MasterUri == null || Connection.MyUri == null || Connection.MyId == null)
            {
                return;
            }

            RosLogger.Internal("Trying to connect to previous ROS server.");
            if ((Settings.IsMacOS || Settings.IsMobile) && Connection.MyUri.Host == Connection.MasterUri.Host)
            {
                _ = connectionData.TryCreateMasterAsync(); // create master and connect
            }
            else
            {
                Connection.TryOnceToConnect();
            }
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
            if (ARController.Instance is not { } arController)
            {
                CreateModule(ModuleType.AR);
            }
            else
            {
                arController.Visible = !arController.Visible;
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
            if (Connection.BagListener != null)
            {
                ShutdownRosbag();
            }
            else
            {
                StartRosbag();
            }
        }

        public void ShutdownRosbag()
        {
            Connection.BagListener = null;
            UpperCanvas.RecordBagImage.color = Color.black;
            UpperCanvas.RecordBagText.text = "Rec Bag";
        }

        void StartRosbag()
        {
            string filename = $"iviz-{GameThread.Now:yyyy-MM-dd-HH-mm-ss}.bag";
            Directory.CreateDirectory(Settings.BagsFolder);
            Connection.BagListener = new BagListener($"{Settings.BagsFolder}/{filename}");
            UpperCanvas.RecordBagImage.color = Color.red;
            UpperCanvas.RecordBagText.text = "0 MB";
        }

        void OnConnectionStateChanged(ConnectionState state)
        {
            UpperCanvas.Status.rectTransform.localRotation = Quaternion.identity;

            if (Connection.MasterUri == null ||
                Connection.MyUri == null ||
                Connection.MyId == null)
            {
                UpperCanvas.StatusSprite = UpperCanvas.QuestionSprite;
                return;
            }

            switch (state)
            {
                case ConnectionState.Connected:
                    GameThread.EveryTenthOfASecond -= RotateSprite;
                    UpperCanvas.StatusSprite = UpperCanvas.ConnectedSprite;
                    UpperCanvas.TopPanel.color = RosManager.Server.IsActive
                        ? Resource.Colors.ConnectionPanelOwnMaster
                        : Resource.Colors.ConnectionPanelConnected;
                    SaveSimpleConfiguration();
                    break;
                case ConnectionState.Disconnected:
                    GameThread.EveryTenthOfASecond -= RotateSprite;
                    UpperCanvas.StatusSprite = UpperCanvas.DisconnectedSprite;
                    UpperCanvas.TopPanel.color = Resource.Colors.ConnectionPanelDisconnected;
                    break;
                case ConnectionState.Connecting:
                    UpperCanvas.StatusSprite = UpperCanvas.ConnectingSprite;
                    GameThread.EveryTenthOfASecond += RotateSprite;
                    break;
            }
        }

        void OnConnectionWarningChanged(bool value)
        {
            UpperCanvas.TopPanel.color = value
                ? Resource.Colors.ConnectionPanelWarning
                : RosManager.Server.IsActive
                    ? Resource.Colors.ConnectionPanelOwnMaster
                    : Resource.Colors.ConnectionPanelConnected;
        }

        void RotateSprite()
        {
            const float rotationSpeedInDeg = 10;
            var euler = new Vector3(0, 0, rotationSpeedInDeg);
            UpperCanvas.Status.rectTransform.Rotate(euler, Space.Self);
        }

        void OnHideGuiButtonClick()
        {
            AllGuiVisible = !AllGuiVisible;

            UpdateLeftHideVisible();

            EventSystem.current.SetSelectedGameObject(null);
        }

        void UpdateLeftHideVisible()
        {
            bool isMobile = Settings.IsMobile && !ARController.IsActive && !AllGuiVisible;
            LeftHideGuiButton.gameObject.SetActive(isMobile);
        }

        public async void SaveStateConfiguration(string file)
        {
            ThrowHelper.ThrowIfNull(file, nameof(file));

            var config = new StateConfiguration();

            foreach (var moduleData in moduleDatas)
            {
                moduleData.AddToState(config);
            }

            config.TfPublisher = TfPublisher.Instance.Configuration;
            config.Camera = CameraPanelData.Configuration;

            try
            {
                RosLogger.Internal("Saving config file...");
                string text = BuiltIns.ToJsonString(config);
                await FileUtils.WriteAllTextAsync($"{Settings.SavedFolder}/{file}", text, default);
                RosLogger.Internal("Done.");
            }
            catch (Exception e)
            {
                RosLogger.Internal("Error saving state configuration", e);
                return;
            }

            RosLogger.Debug($"{this}: Writing config to {Settings.SavedFolder}/{file}");
        }

        void TryLoadDefaultStateConfiguration()
        {
            const string defaultConfigPrefix = "_default";
            string fullPath = $"{Settings.SavedFolder}/{defaultConfigPrefix}{LoadConfigDialogData.Suffix}";
            if (File.Exists(fullPath))
            {
                LoadStateConfiguration($"{defaultConfigPrefix}{LoadConfigDialogData.Suffix}");
            }
        }

        public async void LoadStateConfiguration(string file, CancellationToken token = default)
        {
            ThrowHelper.ThrowIfNull(file, nameof(file));

            string fullPath = $"{Settings.SavedFolder}/{file}";
            RosLogger.Debug($"{this}: Reading config from {fullPath}");

            RosLogger.Internal("Loading config file...");
            if (!File.Exists(fullPath))
            {
                RosLogger.Internal("<b>Error:</b> Config file not found.");
                return;
            }

            string text;
            try
            {
                text = await FileUtils.ReadAllTextAsync(fullPath, token);
                RosLogger.Internal("Done.");
            }
            catch (Exception e)
            {
                RosLogger.Internal("Error loading state configuration", e);
                return;
            }

            RemoveAllModules();

            var stateConfig = JsonConvert.DeserializeObject<StateConfiguration>(text);

            // TF, AR and XR are treated specially
            // TF cannot be destroyed, resetting AR and XR loses world info

            TfData.UpdateConfiguration(stateConfig.Tf);
            TfPublisher.Instance.UpdateConfiguration(stateConfig.TfPublisher);
            CameraPanelData.UpdateConfiguration(stateConfig.Camera);

            if (Settings.IsMobile && stateConfig.AR != null)
            {
                if (moduleDatas.TryGetFirst(module => module.ModuleType == ModuleType.AR, out var arModule))
                {
                    ((ARModuleData)arModule).UpdateConfiguration(stateConfig.AR);
                }
                else
                {
                    CreateModule(ModuleType.AR, configuration: stateConfig.AR);
                }
            }

            if (Settings.IsXR
                && moduleDatas.TryGetFirst(module => module.ModuleType == ModuleType.XR, out var xrModule)
                && stateConfig.XR != null)
            {
                ((XRModuleData)xrModule).UpdateConfiguration(stateConfig.XR);
            }

            var configs = stateConfig.CreateListOfEntries()
                .Where(config => config.ModuleType is not
                    (ModuleType.TF or ModuleType.XR or ModuleType.AR or ModuleType.TFPublisher));

            foreach (var config in configs)
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

                RosLogger.Debug($"{this}: Using settings from {path}");

                string text = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<ConnectionConfiguration?>(text);
                if (config == null)
                {
                    return; // empty text
                }

                var connectionData = Dialogs.ConnectionData;
                connectionData.MasterUri = string.IsNullOrEmpty(config.MasterUri) ? null : new Uri(config.MasterUri);
                connectionData.MyUri = string.IsNullOrEmpty(config.MyUri) ? null : new Uri(config.MyUri);
                connectionData.MyId = config.MyId;
                if (config.LastMasterUris.Count != 0)
                {
                    connectionData.LastMasterUris = config.LastMasterUris;
                }

                Settings.SettingsManager.Config = config.Settings;

                var validHostAliases = config.HostAliases
                    .Where(alias => alias is { Hostname: { }, Address: { } })
                    .ToArray();
                Dialogs.SystemData.HostAliases = validHostAliases;

                var validHostPairs = validHostAliases
                    .Select(alias => (alias!.Hostname, alias.Address))
                    .ToArray();
                Connection.SetHostAliases(validHostPairs);

                return;
            }
            catch (Exception e) when
                (e is IOException or SecurityException or JsonException)
            {
                RosLogger.Debug($"{this}: Error loading simple configuration", e);
                // pass through
            }

            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                RosLogger.Debug($"{this}: Failed to reset simple configuration", e);
            }
        }

        async void SaveSimpleConfiguration()
        {
            Dialogs.ConnectionData.UpdateLastMasterUris();

            var config = new ConnectionConfiguration
            {
                MasterUri = Dialogs.ConnectionData.MasterUri?.ToString() ?? "",
                MyUri = Dialogs.ConnectionData.MyUri?.ToString() ?? "",
                MyId = Dialogs.ConnectionData.MyId ?? "",
                LastMasterUris = new List<Uri>(Dialogs.ConnectionData.LastMasterUris),
                Settings = Settings.SettingsManager.Config,
                HostAliases = Dialogs.SystemData.HostAliases,
            };

            try
            {
                string text = BuiltIns.ToJsonString(config);
                await FileUtils.WriteAllTextAsync(Settings.SimpleConfigurationPath, text, default);
            }
            catch (Exception e) when
                (e is IOException or SecurityException or JsonException)
            {
                RosLogger.Debug($"{this}: Error saving simple configuration", e);
            }
        }

        public void UpdateSimpleConfigurationSettings()
        {
            string path = Settings.SimpleConfigurationPath;
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                string inText = File.ReadAllText(path);
                ConnectionConfiguration config = JsonConvert.DeserializeObject<ConnectionConfiguration>(inText);
                config.Settings = Settings.SettingsManager.Config;
                config.HostAliases = Dialogs.SystemData.HostAliases;
                string outText = BuiltIns.ToJsonString(config);
                File.WriteAllText(path, outText);
            }
            catch (Exception e) when (e is IOException or SecurityException or JsonException)
            {
                RosLogger.Debug($"{this}: Error updating simple configuration", e);
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

        public bool TryLoadXRConfiguration(out Pose unityPose)
        {
            string path = Settings.XRStartConfigurationPath;

            try
            {
                if (!File.Exists(path))
                {
                    unityPose = Pose.identity;
                    return false;
                }

                string text = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<XRStartConfiguration?>(text);
                if (config == null)
                {
                    unityPose = Pose.identity;
                    return false; // empty text
                }

                unityPose.position = config.AnchorPosition.ToUnity();
                unityPose.rotation = config.AnchorOrientation.ToUnity();
                return true;
            }
            catch (Exception e) when
                (e is IOException or SecurityException or JsonException)
            {
                RosLogger.Debug($"{this}: Error loading XR configuration", e);
                unityPose = Pose.identity;
                // pass through
            }

            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                RosLogger.Debug($"{this}: Failed to reset XR configuration", e);
            }

            return false; // empty text
        }

        public async void SaveXRConfiguration(Pose unityPose)
        {
            var (position, rotation) = unityPose;
            var config = new XRStartConfiguration
            {
                AnchorOrientation = rotation.ToRos(),
                AnchorPosition = position.ToRos()
            };

            try
            {
                string text = BuiltIns.ToJsonString(config);
                await FileUtils.WriteAllTextAsync(Settings.XRStartConfigurationPath, text, default);
            }
            catch (Exception e) when
                (e is IOException or SecurityException or JsonException)
            {
                RosLogger.Debug($"{this}: Error saving XR configuration", e);
            }
        }

        public async ValueTask ClearMastersCacheAsync(CancellationToken token = default)
        {
            string path = Settings.SimpleConfigurationPath;
            if (!File.Exists(path))
            {
                return;
            }

            Dialogs.ConnectionData.LastMasterUris = new List<Uri>();

            try
            {
                string inText = await FileUtils.ReadAllTextAsync(path, token);
                var config = JsonConvert.DeserializeObject<ConnectionConfiguration>(inText);
                config.LastMasterUris.Clear();

                string outText = BuiltIns.ToJsonString(config);
                await FileUtils.WriteAllTextAsync(path, outText, token);
            }
            catch (Exception e) when (e is IOException or SecurityException or JsonException)
            {
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error clearing cache", e);
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
                    RosLogger.Error($"{nameof(ModuleListPanel)}: Error deleting saved file '{file.FullPath}'", e);
                }
            }
        }

        void CheckIfInteractableNeeded()
        {
            InteractableButton.Visible = ModuleDatas.Any(module => module is IInteractableModuleData);
        }

        public ModuleData CreateModule(ModuleType resource, string topic = "", string type = "",
            IConfiguration? configuration = null, string? requestedId = null)
        {
            var constructor = new ModuleDataConstructor(resource, topic, type, configuration);

            ModuleData moduleData;
            try
            {
                moduleData = ModuleData.CreateFromResource(constructor);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Failed to create module of type " + resource + ". " +
                                                    "Topic='" + topic + "' Type='" + type + "' Configuration: '" +
                                                    configuration + "'", e);
            }

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

        public ModuleData CreateModuleForTopic(string topic, string type)
        {
            ThrowHelper.ThrowIfNull(topic, nameof(topic));
            ThrowHelper.ThrowIfNull(type, nameof(type));

            if (!Resource.TryGetResourceByRosMessageType(type, out var resource))
            {
                throw new ArgumentException(nameof(type));
            }

            return CreateModule(resource, topic, type);
        }

        public void RemoveModule(ModuleData entry)
        {
            ThrowHelper.ThrowIfNull(entry, nameof(entry));

            int index = moduleDatas.IndexOf(entry);
            if (index == -1)
            {
                throw new InvalidOperationException($"Tried to remove non-existing module '{entry}'");
            }

            RemoveModule(index);

            if (entry.ModuleType == ModuleType.InteractiveMarker)
            {
                CheckIfInteractableNeeded();
            }
        }

        void RemoveModule(int index)
        {
            var moduleData = moduleDatas[index];
            if (moduleData is ListenerModuleData listenerData)
            {
                topicsWithModule.Remove(listenerData.Topic);
            }

            moduleData.Dispose();
            moduleDatas.RemoveAt(index);
            Buttons.RemoveButton(index);
        }

        void RemoveAllModules()
        {
            var newModuleDatas = new List<ModuleData>();
            foreach (var moduleData in moduleDatas)
            {
                if (moduleData.ModuleType is ModuleType.TF or ModuleType.AR or ModuleType.XR)
                {
                    newModuleDatas.Add(moduleData);
                    continue;
                }

                if (moduleData is ListenerModuleData listenerData)
                {
                    topicsWithModule.Remove(listenerData.Topic);
                }

                moduleData.Dispose();
            }

            moduleDatas.Clear();
            moduleDatas.AddRange(newModuleDatas);

            Buttons.RemoveAllButtons();
            foreach (var moduleData in moduleDatas)
            {
                Buttons.CreateButtonForModule(moduleData);
            }
        }

        public void UpdateModuleButtonText(ModuleData entry, string content)
        {
            ThrowHelper.ThrowIfNull(entry, nameof(entry));

            int index = moduleDatas.IndexOf(entry);
            if (index == -1)
            {
                return;
            }

            Buttons.UpdateButton(index, content);
        }

        public void RegisterDisplayedTopic(string topic)
        {
            ThrowHelper.ThrowIfNull(topic, nameof(topic));
            topicsWithModule.Add(topic);
        }

        public ImageDialogData CreateImageDialog(ImageDialogListener caller)
        {
            var canvasHolder = imageCanvasHolder.AssertNotNull(nameof(imageCanvasHolder));
            var newImageData = new ImageDialogData(caller, canvasHolder.transform);
            imageDatas.Add(newImageData);
            return newImageData;
        }

        public void DisposeImageDialog(ImageDialogData dialogData)
        {
            ThrowHelper.ThrowIfNull(dialogData, nameof(dialogData));
            imageDatas.Remove(dialogData);
        }

        public void ShowMarkerDialog(IMarkerDialogListener caller)
        {
            ThrowHelper.ThrowIfNull(caller, nameof(caller));
            Dialogs.MarkerData.Show(caller);
        }

        public void ShowARMarkerDialog()
        {
            Dialogs.ARMarkerData.Show();
        }

        public void ShowTfDialog()
        {
            Dialogs.TfTreeData.Show();
        }

        public void FlushTfDialog()
        {
            if (DialogPanelManager.IsActive(Dialogs.TfTreeData))
            {
                Dialogs.TfTreeData.UpdatePanel();
            }
        }

        public void ShowARPanel()
        {
            if (ModuleDatas.TryGetFirst(moduleData => moduleData is ARModuleData, out var arModuleData))
            {
                arModuleData.ShowPanel();
            }
        }

        public void ResetTfPanel()
        {
            TfData.ResetPanel();
        }

        public void ShowMenu(MenuEntryDescription[] menuEntries, Action<uint> callback)
        {
            ThrowHelper.ThrowIfNull(menuEntries, nameof(menuEntries));
            if (menuDialog == null)
            {
                throw new NullReferenceException("Menu dialog has not been set!");
            }

            menuDialog.Set(menuEntries, callback);
        }

        void UpdateCameraStats()
        {
            using var description = BuilderPool.Rent();
            description.Append(
                Settings.IsXR
                    ? "<b>XR View</b>\n"
                    : ARController.Instance is { Visible: true }
                        ? "<b>AR View</b>\n"
                        : "<b>Virtual View</b>\n"
            );

            var currentCamera = Settings.MainCameraTransform;
            var cameraPose = TfModule.RelativeToFixedFrame(currentCamera);
            RosUtils.FormatPose(cameraPose, description,
                TfModule.Instance.FlipZ ? RosUtils.PoseFormat.All : RosUtils.PoseFormat.AllWithoutRoll);
            BottomCanvas.CameraText.SetTextRent(description);
        }

        void UpdateFpsStats()
        {
#if UNITY_EDITOR
            long memBytesKb = GC.GetTotalMemory(false) / (1024 * 1024);
            BottomCanvas.Time.text = $"{memBytesKb.ToString()}M";
#else
            BottomCanvas.Time.text = GameThread.Now.ToString("HH:mm");
#endif
            BottomCanvas.Fps.text = frameCounter < 100
                ? $"{frameCounter.ToString()} FPS"
                : ">99FPS";
            frameCounter = 0;

            (long downB, long upB) = RosManager.CollectBandwidthReport();
            BottomCanvas.Bandwidth.text = $"{FormatBandwidth(downB)} ^{FormatBandwidth(upB)}";

            var bagListener = Connection.BagListener;
            if (bagListener != null)
            {
                long bagSizeMb = bagListener.Length / (1024 * 1024);
                UpperCanvas.RecordBagText.text = $"{bagSizeMb.ToString()} MB";
            }

            var state = SystemInfo.batteryStatus;

            float batteryLevel = SystemInfo.batteryLevel;
            BottomCanvas.Battery.text = batteryLevel switch
            {
                -1 => "---",
                1 when state is BatteryStatus.Full or BatteryStatus.Charging => "<color=#005500>Full</color>",
                1 => "Full",
                _ when state is BatteryStatus.Charging =>
                    $"<color=#005500>{((int)(batteryLevel * 100)).ToString()}%</color>",
                _ => $"{((int)(batteryLevel * 100)).ToString()}%"
            };
        }

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

        async void OnApplicationPause(bool pauseStatus)
        {
            // app unpausing needs to be handled carefully because in mobile,
            // getting suspended sends us to the background and kills all connections

            if (!RosManager.HasInstance)
            {
                // not a real unpausing, just initializing. out!
                return;
            }

            // try to recover!
            await Dialogs.ConnectionData.TryResetConnectionsAsync();
        }

        public override string ToString() => $"[{nameof(ModuleListPanel)}]";

        public static string CreateButtonTextForModule(ModuleData moduleData)
        {
            if (moduleData is ListenerModuleData listenerData
                && listenerData.Topic != ""
                && listenerData.TopicType != "")
            {
                return CreateButtonTextForListenerModule(listenerData, listenerData.Topic, listenerData.TopicType);
            }

            string buttonText = $"<b>{moduleData.ModuleType}</b>";
            return moduleData.Controller.Visible ? buttonText : $"<color=grey>{buttonText}</color>";
        }

        public static string CreateButtonTextForListenerModule(ModuleData moduleData, string topic, string type)
        {
            string topicShort = Resource.Font.Split(topic, ModuleDataCaptionWidth);
            int lastSlash = type.LastIndexOf('/');
            string shortType = (lastSlash == -1) ? type : type[(lastSlash + 1)..];
            string clampedType = Resource.Font.Split(shortType, ModuleDataCaptionWidth);
            string buttonText = topicShort.Length == 0
                ? $"<b>{clampedType}</b>"
                : $"{topicShort}\n<b>{clampedType}</b>";

            return moduleData.Controller.Visible ? buttonText : $"<color=grey>{buttonText}</color>";
        }

        public static string CreateButtonTextForModule(ModuleData moduleData, string customTitle)
        {
            string buttonText =
                $"{Resource.Font.Split(customTitle, ModuleDataCaptionWidth)}\n" +
                $"<b>{moduleData.ModuleType}</b>";

            return moduleData.Controller.Visible ? buttonText : $"<color=grey>{buttonText}</color>";
        }
    }
}