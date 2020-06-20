using Iviz.App.Displays;
using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    public class DisplayListPanel : MonoBehaviour
    {
        float buttonHeight;

        Canvas parentCanvas;
        public bool AllGuiVisible
        {
            get => parentCanvas.gameObject.activeSelf;
            set
            {
                parentCanvas.gameObject.SetActive(value);
            }
        }

        //[SerializeField] InputField address = null;
        [SerializeField] DataLabelWidget MasterUriStr = null;
        [SerializeField] TrashButtonWidget MasterUriButton = null;
        [SerializeField] TrashButtonWidget ConnectButton = null;
        [SerializeField] TrashButtonWidget StopButton = null;


        [SerializeField] Button save = null;
        [SerializeField] Button load = null;
        [SerializeField] Button hide = null;
        [SerializeField] Image status = null;

        [SerializeField] GameObject contentObject = null;

        [SerializeField] DataPanelManager dataPanelManager = null;
        public DataPanelManager DataPanelManager => dataPanelManager;

        [SerializeField] DialogPanelManager dialogPanelManager = null;
        public DialogPanelManager DialogPanelManager => dialogPanelManager;

        [SerializeField] Button addDisplayByTopic = null;
        [SerializeField] Button addDisplay = null;
        [SerializeField] Button showTFTree = null;
        //[SerializeField] Toggle keepReconnecting = null;

        [SerializeField] Sprite ConnectedSprite = null;
        [SerializeField] Sprite ConnectingSprite = null;
        [SerializeField] Sprite DisconnectedSprite = null;
        [SerializeField] Sprite QuestionSprite = null;

        [SerializeField] Joystick joystick = null;
        public Joystick Joystick => joystick;

        public string Address { get; private set; }

        readonly List<ModuleData> moduleDatas = new List<ModuleData>();
        public ReadOnlyCollection<ModuleData> ModuleDatas => new ReadOnlyCollection<ModuleData>(moduleDatas);

        DialogData availableModules;
        DialogData availableTopics;
        ConnectionDialogData connectionData;
        ImageDialogData imageData;
        TFDialogData tfTreeData;

        readonly List<GameObject> buttons = new List<GameObject>();

        TFModuleData TFData => (TFModuleData)moduleDatas[0];

        readonly HashSet<string> topicsWithModule = new HashSet<string>();
        public IReadOnlyCollection<string> DisplayedTopics => topicsWithModule;

        bool KeepReconnecting
        {
            get => ConnectionManager.Connection.KeepReconnecting;
            set
            {
                ConnectionManager.Connection.KeepReconnecting = value;
                status.enabled = value;
            }
        }

        void Start()
        {
            parentCanvas = transform.parent.parent.GetComponentInParent<Canvas>();

            buttonHeight = Resource.Widgets.DisplayButton.Object.GetComponent<RectTransform>().rect.height;

            availableModules = CreateDialog<AddModuleDialogData>();
            availableTopics = CreateDialog<AddTopicDialogData>();

            imageData = CreateDialog<ImageDialogData>();
            tfTreeData = CreateDialog<TFDialogData>();

            connectionData = CreateDialog<ConnectionDialogData>();
            LoadSimpleConfiguration();

            Logger.Internal("<b>Welcome to iviz</b>");

            CreateModule(Resource.Module.TF, TFListener.DefaultTopic);
            CreateModule(Resource.Module.Grid);


            save.onClick.AddListener(SaveStateConfiguration);
            load.onClick.AddListener(LoadStateConfiguration);
            hide.onClick.AddListener(OnHideClick);


            addDisplayByTopic.onClick.AddListener(() => { availableTopics.Show(); });
            addDisplay.onClick.AddListener(() => { availableModules.Show(); });
            showTFTree.onClick.AddListener(() => { tfTreeData.Show(); });

            MasterUriStr.Label = connectionData.MasterUri + " →";
            MasterUriButton.Clicked += () =>
            {
                connectionData.Show();
            };

            ConnectionManager.Connection.MasterUri = connectionData.MasterUri;
            ConnectionManager.Connection.MyUri = connectionData.MyUri;
            ConnectionManager.Connection.MyId = connectionData.MyId;
            KeepReconnecting = false;

            //ConnectionManager.Instance.KeepReconnecting = keepReconnecting.isOn;
            //keepReconnecting.onValueChanged.AddListener(x => ConnectionManager.Instance.KeepReconnecting = x);
            connectionData.MasterUriChanged += uri =>
            {
                ConnectionManager.Connection.MasterUri = uri;
                KeepReconnecting = false;
                if (uri == null)
                {
                    Logger.Internal($"Failed to set master uri.");
                    MasterUriStr.Label = "(?) →";
                }
                else
                {
                    Logger.Internal($"Changing master uri to '{uri}'");
                    MasterUriStr.Label = uri + " →";
                }
            };
            connectionData.MyIdChanged += id =>
            {
                ConnectionManager.Connection.MyId = id;
                KeepReconnecting = false;
                Logger.Internal($"Changing caller id to '{id}'");
            };
            connectionData.MyUriChanged += uri =>
            {
                ConnectionManager.Connection.MyUri = uri;
                KeepReconnecting = false;
                if (uri == null)
                {
                    Logger.Internal($"Failed to set caller uri.");
                }
                else
                {
                    Logger.Internal($"Changing caller uri to '{uri}'");
                }
            };
            StopButton.Clicked += () =>
            {
                if (ConnectionManager.Connected)
                {
                    Logger.Internal("Disconnection requested.");
                }
                else
                {
                    Logger.Internal("Disconnection requested (but already disconnected).");
                }
                KeepReconnecting = false;
                ConnectionManager.Connection.Disconnect();
            };
            ConnectButton.Clicked += () =>
            {
                if (ConnectionManager.Connected)
                {
                    Logger.Internal("Reconnection requested.");
                }
                else
                {
                    Logger.Internal("Connection requested.");
                }
                ConnectionManager.Connection.Disconnect();
                KeepReconnecting = true;
            };


            //address.onEndEdit.AddListener(OnAddressChanged);

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;

            TFListener.GuiManager.Canvases.Add(GetComponentInParent<Canvas>());
            TFListener.GuiManager.Canvases.Add(dataPanelManager.GetComponentInParent<Canvas>());
            TFListener.GuiManager.Canvases.Add(dialogPanelManager.GetComponentInParent<Canvas>());
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
                    Logger.Internal("Connected!");
                    GameThread.EverySecond -= RotateSprite;
                    status.sprite = ConnectedSprite;
                    SaveSimpleConfiguration();
                    break;
                case ConnectionState.Disconnected:
                    Logger.Internal("Disconnected.");
                    GameThread.EverySecond -= RotateSprite;
                    status.sprite = DisconnectedSprite;
                    break;
                case ConnectionState.Connecting:
                    Logger.Internal("Connecting...");
                    status.sprite = ConnectingSprite;
                    GameThread.EverySecond += RotateSprite;
                    break;
            }
        }

        void RotateSprite()
        {
            status.rectTransform.Rotate(new Vector3(0, 0, 10.0f), Space.Self);
        }

        void OnHideClick()
        {
            AllGuiVisible = !AllGuiVisible;
            EventSystem.current.SetSelectedGameObject(null);
        }

        void SaveStateConfiguration()
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
                File.WriteAllText(Application.persistentDataPath + "/config.json", text);
                Logger.Internal("Done.");
            }
            catch (Exception e) when
            (e is IOException || e is System.Security.SecurityException || e is JsonException)
            {
                Logger.Error(e);
                Logger.Internal("Error:", e);
                return;
            }
            Logger.Debug("DisplayListPanel: Writing config to " + Application.persistentDataPath + "/config.json");

        }

        void LoadStateConfiguration()
        {
            Logger.Debug("DisplayListPanel: Reading config from " + Application.persistentDataPath + "/config.json");
            string text;
            try
            {
                Logger.Internal("Loading config file...");
                text = File.ReadAllText(Application.persistentDataPath + "/config.json");
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
            try
            {
                string text = File.ReadAllText(Application.persistentDataPath + "/connection.json");
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
                File.WriteAllText(Application.persistentDataPath + "/connection.json", text);
            }
            catch (Exception e) when
            (e is IOException || e is System.Security.SecurityException || e is JsonException)
            {
                //Debug.Log(e);
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

        const float yOffset = 5;

        void CreateButtonObject(ModuleData moduleData)
        {
            GameObject buttonObject = ResourcePool.GetOrCreate(Resource.Widgets.DisplayButton, contentObject.transform, false);

            int size = buttons.Count();
            float y = yOffset + size * buttonHeight;

            (buttonObject.transform as RectTransform).anchoredPosition = new Vector2(0, -y);

            Text buttonObjectText = buttonObject.GetComponentInChildren<Text>();
            buttonObjectText.text = moduleData.ButtonText; // $"<b>{displayData.Module}</b>";
            buttonObject.name = $"Button:{moduleData.Module}";
            buttonObject.SetActive(true);
            buttons.Add(buttonObject);

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                moduleData.ToggleSelect();
            });
            (contentObject.transform as RectTransform).sizeDelta = new Vector2(0, y + buttonHeight + yOffset);

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
            for (i = index; i < buttons.Count(); i++)
            {
                GameObject buttonObject = buttons[i];
                float y = yOffset + i * buttonHeight;
                (buttonObject.transform as RectTransform).anchoredPosition = new Vector3(0, -y);
            }
            (contentObject.transform as RectTransform).sizeDelta = new Vector2(0, 2 * yOffset + i * buttonHeight);
        }

        public const int DisplayDataCaptionWidth = 200;

        public void SetDisplayDataCaption(ModuleData entry, string text)
        {
            int index = moduleDatas.IndexOf(entry);
            if (index == -1)
            {
                return;
            }
            GameObject buttonObject = buttons[index];
            buttonObject.GetComponentInChildren<Text>().text = text;
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
    }
}
