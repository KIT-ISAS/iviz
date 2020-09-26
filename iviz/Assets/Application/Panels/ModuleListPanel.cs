using Iviz.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Iviz.Controllers;
using Iviz.Displays;
using Iviz.Roslib.XmlRpc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Logger = Iviz.Controllers.Logger; 

namespace Iviz.App
{
    public sealed class ModuleListPanel : MonoBehaviour
    {
        float buttonHeight;

        Canvas parentCanvas;
        public bool AllGuiVisible
        {
            get => parentCanvas.gameObject.activeSelf;
            set => parentCanvas.gameObject.SetActive(value);
        }

        public static ModuleListPanel Instance { get; private set; }

        //[SerializeField] InputField address = null;
        [SerializeField] DataLabelWidget MasterUriStr = null;
        [SerializeField] TrashButtonWidget MasterUriButton = null;
        [SerializeField] TrashButtonWidget ConnectButton = null;
        [SerializeField] TrashButtonWidget StopButton = null;

        [SerializeField] Image topPanel = null;

        [SerializeField] Button save = null;
        [SerializeField] Button load = null;
        [SerializeField] Image status = null;

        [SerializeField] AnchorToggleButton hideGuiButton = null;
        [SerializeField] AnchorToggleButton showControlButton = null;
        [SerializeField] AnchorToggleButton pinControlButton = null;

        [SerializeField] Button unlock = null;
        public Button UnlockButton => unlock;

        [SerializeField] GameObject contentObject = null;

        [SerializeField] DataPanelManager dataPanelManager = null;
        public DataPanelManager DataPanelManager => dataPanelManager;

        [SerializeField] DialogPanelManager dialogPanelManager = null;
        public DialogPanelManager DialogPanelManager => dialogPanelManager;

        [SerializeField] Button addDisplayByTopic = null;
        [SerializeField] Button addDisplay = null;
        [SerializeField] Button showTFTree = null;
        [SerializeField] Button resetAll = null;

        [SerializeField] Sprite ConnectedSprite = null;
        [SerializeField] Sprite ConnectingSprite = null;
        [SerializeField] Sprite DisconnectedSprite = null;
        [SerializeField] Sprite QuestionSprite = null;

        [SerializeField] Text bottomTime = null;
        [SerializeField] Text bottomFps = null;
        [SerializeField] Text bottomBandwidth = null;

        [SerializeField] Joystick joystick = null;
        public Joystick Joystick => joystick;

        readonly List<ModuleData> moduleDatas = new List<ModuleData>();
        public IReadOnlyCollection<ModuleData> ModuleDatas { get; }

        DialogData availableModules;
        DialogData availableTopics;
        ConnectionDialogData connectionData;
        ImageDialogData imageData;
        TFDialogData tfTreeData;
        LoadConfigDialogData loadConfigData;
        SaveConfigDialogData saveConfigData;

        readonly List<GameObject> buttons = new List<GameObject>();

        TFModuleData TFData => (TFModuleData)moduleDatas[0];

        readonly HashSet<string> topicsWithModule = new HashSet<string>();
        public IEnumerable<string> DisplayedTopics => topicsWithModule;

        static readonly Color ConnectedColor = new Color(0.6f, 1f, 0.5f, 0.4f);
        static readonly Color ConnectedOwnMasterColor = new Color(0.4f, 0.95f, 1f, 0.4f);
        static readonly Color DisconnectedColor = new Color(0.9f, 0.95f, 1f, 0.4f);

        public bool UnlockButtonVisible
        {
            get => unlock.gameObject.activeSelf;
            set => unlock.gameObject.SetActive(value);
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

        public ModuleListPanel()
        {
            ModuleDatas = moduleDatas.AsReadOnly();            
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
            
            hideGuiButton.Clicked += OnHideGuiButtonClick;
            hideGuiButton.State = true;

            pinControlButton.Clicked += () =>
            {
                if (ARController.Instance != null)
                {
                    ARController.Instance.PinRootMarker = pinControlButton.State;
                }
            };
            showControlButton.Clicked += () =>
            {
                if (ARController.Instance != null)
                {
                    ARController.Instance.ShowRootMarker = showControlButton.State;
                    TFListener.UpdateRootMarkerVisibility();
                }
            };

            addDisplayByTopic.onClick.AddListener(availableTopics.Show);
            addDisplay.onClick.AddListener(availableModules.Show);
            showTFTree.onClick.AddListener(tfTreeData.Show);
            resetAll.onClick.AddListener(ResetAllModules);
    
            
            MasterUriStr.Label = connectionData.MasterUri + " →";
            MasterUriButton.Clicked += () =>
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
                    Logger.Internal($"Failed to set master uri.");
                    MasterUriStr.Label = "(?) →";
                }
                else if (RosServerManager.IsActive)
                {
                    Logger.Internal($"Changing master uri to local master '{uri}'");
                    MasterUriStr.Label = "Master Mode\n" + uri + " →";
                }
                else
                {
                    Logger.Internal($"Changing master uri to '{uri}'");
                    MasterUriStr.Label = uri + " →";
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
                    uri == null ? 
                    "Failed to set caller uri." : 
                    $"Changing caller uri to '{uri}'"
                    );
            };
            StopButton.Clicked += () =>
            {
                Logger.Internal(
                    ConnectionManager.IsConnected ?
                    "Disconnection requested." :
                    "Disconnection requested (but already disconnected)."
                    );
                KeepReconnecting = false;
                ConnectionManager.Connection.Disconnect();
            };
            ConnectButton.Clicked += () =>
            {
                Logger.Internal(
                    ConnectionManager.IsConnected ? 
                    "Reconnection requested." : 
                    "Connection requested."
                );
                ConnectionManager.Connection.Disconnect();
                KeepReconnecting = true;
            };

            connectionData.MasterActiveChanged += _ =>
            {
                ConnectionManager.Connection.Disconnect();
            };


            //address.onEndEdit.AddListener(OnAddressChanged);

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;

            TFListener.GuiCamera.Canvases.Add(GetComponentInParent<Canvas>());
            TFListener.GuiCamera.Canvases.Add(dataPanelManager.GetComponentInParent<Canvas>());
            TFListener.GuiCamera.Canvases.Add(dialogPanelManager.GetComponentInParent<Canvas>());
            TFListener.GuiCamera.GuiPointerBlockers.Add(Joystick);

            TFListener.GuiCamera.Raycasters.Add(GetComponentInParent<GraphicRaycaster>());
            TFListener.GuiCamera.Raycasters.Add(dataPanelManager.GetComponentInParent<GraphicRaycaster>());
            TFListener.GuiCamera.Raycasters.Add(dialogPanelManager.GetComponentInParent<GraphicRaycaster>());

            
            GameThread.LateEverySecond += UpdateFpsStats;
            GameThread.EveryFrame += UpdateFpsCounter;
            UpdateFpsStats();
        }

        void OnConnectionStateChanged(ConnectionState state)
        {
            status.rectTransform.localRotation = Quaternion.identity;

            if (ConnectionManager.Connection.MasterUri == null ||
                ConnectionManager.Connection.MyUri == null ||
                ConnectionManager.Connection.MyId == null)
            {
                status.sprite = QuestionSprite;
                return;
            }

            switch (state)
            {
                case ConnectionState.Connected:
                    GameThread.EverySecond -= RotateSprite;
                    status.sprite = ConnectedSprite;
                    topPanel.color = RosServerManager.IsActive ? ConnectedOwnMasterColor : ConnectedColor;
                    SaveSimpleConfiguration();
                    break;
                case ConnectionState.Disconnected:
                    GameThread.EverySecond -= RotateSprite;
                    status.sprite = DisconnectedSprite;
                    topPanel.color = DisconnectedColor;
                    break;
                case ConnectionState.Connecting:
                    status.sprite = ConnectingSprite;
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
            (e is IOException || e is System.Security.SecurityException || e is JsonException)
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
            (e is IOException || e is System.Security.SecurityException || e is JsonException)
            {
                Logger.Error(e);
                Logger.Internal("Error:", e);
                return;
            }

            while (moduleDatas.Count > 1)
            {
                RemoveModule(1);
            }

            StateConfiguration stateConfig = JsonConvert.DeserializeObject<StateConfiguration>(text);

            connectionData.MasterUri = stateConfig.MasterUri;
            connectionData.MyUri = stateConfig.MyUri;
            connectionData.MyId = stateConfig.MyId;


            TFData.UpdateConfiguration(stateConfig.Tf);
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
            (e is IOException || e is System.Security.SecurityException || e is JsonException)
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
                    MyId = connectionData.MyId,
                };

                string text = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(Settings.SimpleConfigurationPath, text);
            }
            catch (Exception e) when
            (e is IOException || e is System.Security.SecurityException || e is JsonException)
            {
                //Debug.Log(e);
            }
        }

        public void ResetAllModules()
        {
            foreach (ModuleData m in moduleDatas)
            {
                m.ResetController();
            }
        }
        
        public ModuleData CreateModule(Resource.Module resource, string topic = "", string type = "", IConfiguration configuration = null)
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

        const float YOffset = 2;

        void CreateButtonObject(ModuleData moduleData)
        {
            GameObject buttonObject = ResourcePool.GetOrCreate(Resource.Widgets.DisplayButton, contentObject.transform, false);

            int size = buttons.Count();
            float y = 2 * YOffset + size * (buttonHeight + YOffset);

            ((RectTransform)buttonObject.transform).anchoredPosition = new Vector2(0, -y);

            Text buttonObjectText = buttonObject.GetComponentInChildren<Text>();
            buttonObjectText.text = moduleData.ButtonText; // $"<b>{displayData.Module}</b>";
            buttonObject.name = $"Button:{moduleData.Module}";
            buttonObject.SetActive(true);
            buttons.Add(buttonObject);

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(moduleData.ToggleShowPanel);
            ((RectTransform)contentObject.transform).sizeDelta = new Vector2(0, y + buttonHeight + YOffset);

            //return buttonObject;
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
                ((RectTransform)buttonObject.transform).anchoredPosition = new Vector3(0, -y);
            }
            ((RectTransform)contentObject.transform).sizeDelta = new Vector2(0, 2 * YOffset + i * (buttonHeight + YOffset));
        }

        public const int ModuleDataCaptionWidth = 200;

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

        public void ShowFrame(TFFrame frame)
        {
            tfTreeData.Show(frame);
        }
        
        int frames = 0;

        void UpdateFpsStats()
        {
            bottomTime.text = $"<b>{DateTime.Now:HH:mm:ss}</b>";
            bottomFps.text = $"<b>{frames} FPS</b>";
            frames = 0;

            var (downB, upB) = ConnectionManager.CollectBandwidthReport();
            int downKb = downB / 1000;
            int upKb = upB / 1000;
            bottomBandwidth.text = $"<b>↓{downKb:N0}kB/s ↑{upKb:N0}kB/s</b>";
        }

        void UpdateFpsCounter()
        {
            frames++;
        }

        public void OnARModeChanged(bool value)
        {
            pinControlButton.Visible = value;
            showControlButton.Visible = value;
        } 
    }
}
