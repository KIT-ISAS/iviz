﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;

namespace Iviz.App
{
    public sealed class ModuleListPanel : MonoBehaviour
    {
        public const int ModuleDataCaptionWidth = 200;

        static ModuleListPanel? instance;

        static event Action? InitFinished;

        public static ModuleListPanel Instance
        {
            get
            {
                GameObject instanceObject;
                return instance != null
                    ? instance
                    : (instanceObject = GameObject.Find("ModuleList Panel")) != null
                      && (instance = instanceObject.GetComponent<ModuleListPanel>()) != null
                        ? instance
                        : throw new MissingAssetFieldException("Module list panel has not been set!");
            }
        }

        public static float CanvasScale => Instance.RootCanvas.scaleFactor;

        [SerializeField] Button? middleHideGuiButton = null;

        [SerializeField] AnchorCanvasPanel? anchorCanvasPanel = null;
        [SerializeField] UpperCanvasPanel? upperCanvasPanel = null;
        [SerializeField] ARSidePanel? arSidePanel = null;
        [SerializeField] DataPanelManager? dataPanelManager = null;
        [SerializeField] DialogPanelManager? dialogPanelManager = null;
        [SerializeField] ARJoystick? arJoystick = null;
        [SerializeField] TwistJoystick? twistJoystick = null;
        [SerializeField] GameObject? contentObject = null;
        [SerializeField] Canvas? rootCanvas = null;

        [SerializeField] TMP_Text cameraText = null;
        [SerializeField] Text bottomTime = null;
        [SerializeField] Text bottomBattery = null;
        [SerializeField] Text bottomFps = null;
        [SerializeField] Text bottomBandwidth = null;

        [SerializeField] GameObject moduleListCanvas = null;
        [SerializeField] GameObject dataPanelCanvas = null;
        [SerializeField] GameObject imageCanvasHolder = null;


        [SerializeField] GameObject menuObject = null;

        readonly List<ModuleData> moduleDatas = new();
        readonly HashSet<string> topicsWithModule = new();
        readonly HashSet<ImageDialogData> imageDatas = new();

        int frameCounter;
        bool allGuiVisible = true;
        bool initialized;
        bool sceneInteractable;

        ModuleListButtons? buttons;
        DialogManager? dialogs;
        IMenuDialogContents? menuDialog;

        UpperCanvasPanel UpperCanvas => upperCanvasPanel.AssertNotNull(nameof(upperCanvasPanel));
        AnchorToggleButton BottomHideGuiButton => AnchorCanvasPanel.BottomHideGui;
        Button LeftHideGuiButton => AnchorCanvasPanel.LeftHideGui;
        Button MiddleHideGuiButton => middleHideGuiButton.AssertNotNull(nameof(middleHideGuiButton));
        AnchorToggleButton InteractableButton => AnchorCanvasPanel.Interact;

        ModuleListButtons Buttons =>
            buttons ??= new ModuleListButtons(contentObject.AssertNotNull(nameof(contentObject)));

        DialogManager Dialogs => dialogs ??= new DialogManager();
        TfModuleData TfData => (TfModuleData)moduleDatas[0];
        Canvas RootCanvas => rootCanvas.AssertNotNull(nameof(rootCanvas));
        public AnchorCanvasPanel AnchorCanvasPanel => anchorCanvasPanel.AssertNotNull(nameof(anchorCanvasPanel));
        public Button UnlockButton => AnchorCanvasPanel.Unlock;
        public DataPanelManager DataPanelManager => dataPanelManager.AssertNotNull(nameof(dataPanelManager));
        public DialogPanelManager DialogPanelManager => dialogPanelManager.AssertNotNull(nameof(dialogPanelManager));
        public TwistJoystick TwistJoystick => twistJoystick.AssertNotNull(nameof(twistJoystick));
        public ARJoystick ARJoystick => arJoystick.AssertNotNull(nameof(arJoystick));
        public ARSidePanel ARSidePanel => arSidePanel.AssertNotNull(nameof(arSidePanel));
        public ReadOnlyCollection<ModuleData> ModuleDatas { get; }
        public IEnumerable<string> DisplayedTopics => topicsWithModule;

        public bool AllGuiVisible
        {
            get => allGuiVisible;
            set
            {
                allGuiVisible = value;
                BottomHideGuiButton.State = value;
                moduleListCanvas.SetActive(value);
                dataPanelCanvas.SetActive(value);
                DialogPanelManager.Active = value;
                ARSidePanel.Visible = !value;
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

        bool KeepReconnecting
        {
            set
            {
                ConnectionManager.Connection.KeepReconnecting = value;
                UpperCanvas.Status.enabled = value;
            }
        }

        public ModuleListPanel()
        {
            ModuleDatas = moduleDatas.AsReadOnly();
        }

        void Awake()
        {
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

            foreach (var dialogData in Dialogs.DialogDatas)
            {
                dialogData.FinalizePanel();
            }
        }

        static string MasterUriToString(Uri? uri) =>
            uri is null || uri.AbsolutePath.Length == 0 ? "" : $"{uri.Host}:{uri.Port.ToString()}";

        void Start()
        {
            Directory.CreateDirectory(Settings.SavedFolder);
            LoadSimpleConfiguration();

            RosLogger.Internal("<b>Welcome to iviz!</b>");
            RosLogger.Internal("This is the log for connection messages. " +
                               "For general ROS log messages check the Log dialog.");

            CreateModule(ModuleType.TF, TfListener.DefaultTopic);

            if (!Settings.IsHololens)
            {
                CreateModule(ModuleType.Grid);
            }

            UpperCanvas.Save.onClick.AddListener(Dialogs.SaveConfigData.Show);
            UpperCanvas.Load.onClick.AddListener(Dialogs.LoadConfigData.Show);

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
                    RosLogger.Internal("<b>Error:</b> Failed to set master uri. Reason: Uri is not valid.");
                    UpperCanvas.MasterUriStr.Text = "(?) →";
                }
                else if (RosServerManager.IsActive)
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

                ConnectionManager.Connection.MyId = id;
                KeepReconnecting = false;
                RosLogger.Internal($"Changing my ROS id to '{id}'");
            };
            connectionData.MyUriChanged += uri =>
            {
                ConnectionManager.Connection.MyUri = uri;
                KeepReconnecting = false;
                RosLogger.Internal(uri == null
                    ? "<b>Error:</b> Failed to set caller uri. Reason: Uri is not valid."
                    : $"Changing caller uri to '{uri}'"
                );
            };
            UpperCanvas.StopButton.Clicked += () =>
            {
                RosLogger.Internal(
                    ConnectionManager.IsConnected
                        ? "Disconnection requested."
                        : "Already disconnected."
                );
                KeepReconnecting = false;
                ConnectionManager.Connection.Disconnect();
            };
            UpperCanvas.ConnectButton.Clicked += () =>
            {
                RosLogger.Internal(
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

            ServiceFunctions.Start();

            menuDialog = menuObject.GetComponent<IMenuDialogContents>();
            menuObject.SetActive(false);

            AllGuiVisible = AllGuiVisible; // initialize value

            if (Settings.IsXR)
            {
                RootCanvas.ProcessCanvasForXR();
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
                UpperCanvas.RecordBagImage.color = Color.black;
                UpperCanvas.RecordBagText.text = "Rec Bag";
            }
            else
            {
                string filename = $"iviz-{GameThread.Now:yyyy-MM-dd-HH-mm-ss}.bag";
                Directory.CreateDirectory(Settings.BagsFolder);
                ConnectionManager.Connection.BagListener = new BagListener($"{Settings.BagsFolder}/{filename}");
                UpperCanvas.RecordBagImage.color = Color.red;
                UpperCanvas.RecordBagText.text = "0 MB";
            }
        }

        void OnConnectionStateChanged(ConnectionState state)
        {
            UpperCanvas.Status.rectTransform.localRotation = Quaternion.identity;

            if (ConnectionManager.Connection.MasterUri == null ||
                ConnectionManager.Connection.MyUri == null ||
                ConnectionManager.Connection.MyId == null)
            {
                UpperCanvas.Status.sprite = UpperCanvas.QuestionSprite;
                return;
            }

            switch (state)
            {
                case ConnectionState.Connected:
                    GameThread.EverySecond -= RotateSprite;
                    UpperCanvas.Status.sprite = UpperCanvas.ConnectedSprite;
                    UpperCanvas.TopPanel.color = RosServerManager.IsActive
                        ? Resource.Colors.ConnectionPanelOwnMaster
                        : Resource.Colors.ConnectionPanelConnected;
                    SaveSimpleConfiguration();
                    break;
                case ConnectionState.Disconnected:
                    GameThread.EverySecond -= RotateSprite;
                    UpperCanvas.Status.sprite = UpperCanvas.DisconnectedSprite;
                    UpperCanvas.TopPanel.color = Resource.Colors.ConnectionPanelDisconnected;
                    break;
                case ConnectionState.Connecting:
                    UpperCanvas.Status.sprite = UpperCanvas.ConnectingSprite;

                    GameThread.EverySecond += RotateSprite;
                    break;
            }
        }

        void OnConnectionWarningChanged(bool value)
        {
            UpperCanvas.TopPanel.color = value
                ? Resource.Colors.ConnectionPanelWarning
                : RosServerManager.IsActive
                    ? Resource.Colors.ConnectionPanelOwnMaster
                    : Resource.Colors.ConnectionPanelConnected;
        }

        void RotateSprite()
        {
            UpperCanvas.Status.rectTransform.Rotate(new Vector3(0, 0, 10.0f), Space.Self);
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

        public async void SaveStateConfiguration(string file)
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
                RosLogger.Internal("Saving config file...");
                string text = JsonConvert.SerializeObject(config, Formatting.Indented);
                await FileUtils.WriteAllTextAsync($"{Settings.SavedFolder}/{file}", text, default);
                RosLogger.Internal("Done.");
            }
            catch (Exception e)
            {
                RosLogger.Internal("Error saving state configuration", e);
                return;
            }

            RosLogger.Debug("DisplayListPanel: Writing config to " + Settings.SavedFolder + "/" + file);
        }

        public async void LoadStateConfiguration(string file, CancellationToken token = default)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            RosLogger.Debug($"DisplayListPanel: Reading config from {Settings.SavedFolder}/{file}");
            string text;
            try
            {
                RosLogger.Internal("Loading config file...");
                text = await FileUtils.ReadAllTextAsync($"{Settings.SavedFolder}/{file}", token);
                RosLogger.Internal("Done.");
            }
            catch (FileNotFoundException)
            {
                RosLogger.Internal("<b>Error:</b> Config file not found.");
                return;
            }
            catch (Exception e)
            {
                RosLogger.Internal("Error loading state configuration", e);
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

                var validHostPairs = validHostAliases.Select(alias => (alias!.Hostname, alias.Address));
                ConnectionManager.Connection.SetHostAliases(validHostPairs);

                Dialogs.ARMarkerData.Configuration = config.MarkersConfiguration;
            }
            catch (Exception e) when
                (e is IOException or SecurityException or JsonException)
            {
                RosLogger.Debug($"{this}: Error loading simple configuration", e);
                File.Delete(path);
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
                MarkersConfiguration = Dialogs.ARMarkerData.Configuration,
            };

            try
            {
                string text = JsonConvert.SerializeObject(config, Formatting.Indented);
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
                config.MarkersConfiguration = Dialogs.ARMarkerData.Configuration;
                string outText = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(path, outText);
            }
            catch (Exception e) when (e is IOException or SecurityException or JsonException)
            {
                RosLogger.Debug("ModuleListPanel: Error updating simple configuration", e);
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

        public int NumMastersInCache => Dialogs.ConnectionData.LastMasterUris.Count;

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

                string outText = JsonConvert.SerializeObject(config, Formatting.Indented);
                await FileUtils.WriteAllTextAsync(path, outText, token);
            }
            catch (Exception e) when (e is IOException or SecurityException or JsonException)
            {
            }
            catch (Exception e)
            {
                RosLogger.Error($"Error clearing cache", e);
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
                    RosLogger.Error($"Error deleting file '{file}'", e);
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

        public ModuleData CreateModuleForTopic(string topic, string type)
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

        public void RemoveModule(ModuleData entry)
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


        public void UpdateModuleButton(ModuleData entry, string content)
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

        public void RegisterDisplayedTopic(string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            topicsWithModule.Add(topic);
        }

        public ImageDialogData CreateImageDialog(ImageDialogListener caller)
        {
            var newImageData = new ImageDialogData(caller, imageCanvasHolder.transform);
            imageDatas.Add(newImageData);
            return newImageData;
        }

        public void DisposeImageDialog(ImageDialogData dialogData)
        {
            imageDatas.Remove(dialogData);
        }

        public void ShowMarkerDialog(IMarkerDialogListener caller)
        {
            Dialogs.MarkerData.Show(caller ?? throw new ArgumentNullException(nameof(caller)));
        }

        public void ShowARMarkerDialog()
        {
            Dialogs.ARMarkerData.Show();
        }

        void UpdateCameraStats()
        {
            var description = BuilderPool.Rent();
            try
            {
                description.Append(
                    Settings.IsXR
                        ? "<b>XR View</b>\n"
                        : ARController.IsVisible
                            ? "<b>AR View</b>\n"
                            : "<b>Virtual View</b>\n"
                );

                var currentCamera = Settings.MainCameraTransform;
                var cameraPose = TfListener.RelativePoseToFixedFrame(currentCamera.AsPose());
                RosUtils.FormatPose(cameraPose, description,
                    TfListener.Instance.FlipZ ? RosUtils.PoseFormat.All : RosUtils.PoseFormat.WithoutRoll);
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
                UpperCanvas.RecordBagText.text = $"{bagSizeMb.ToString()} MB";
            }

            var state = SystemInfo.batteryStatus;
            switch (SystemInfo.batteryLevel)
            {
                case -1:
                    bottomBattery.text = "---";
                    break;
                case 1 when state is BatteryStatus.Full or BatteryStatus.Charging:
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

        public void ShowMenu(MenuEntryList menuEntries, Action<uint> callback, Vector3 unityPositionHint)
        {
            if (menuEntries == null)
            {
                throw new ArgumentNullException(nameof(menuEntries));
            }

            if (menuDialog == null)
            {
                throw new NullReferenceException("Menu dialog has not been set!");
            }

            menuDialog.Set(menuEntries, unityPositionHint, callback);
        }
    }
}