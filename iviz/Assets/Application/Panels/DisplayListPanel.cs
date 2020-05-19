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
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    public interface IConfiguration
    {
        Guid Id { get; }
        Resource.Module Module { get; }
        bool Visible { get; set; }
    }

    [DataContract]
    public class ConnectionConfiguration
    {
        [DataMember] public Uri MasterUri { get; set; } = null;
        [DataMember] public Uri MyUri { get; set; } = null;
        [DataMember] public string MyId { get; set; } = null;
    }

    [DataContract]
    public class StateConfiguration
    {
        [DataMember] public Uri MasterUri { get; set; } = null;
        [DataMember] public Uri MyUri { get; set; } = null;
        [DataMember] public string MyId { get; set; } = null;

        [DataMember] public List<Guid> Entries { get; set; } = new List<Guid>();

        [DataMember] public TFConfiguration Tf { get; set; } = null;
        [DataMember] public List<GridConfiguration> Grids { get; set; } = new List<GridConfiguration>();
        [DataMember] public List<RobotConfiguration> Robots { get; set; } = new List<RobotConfiguration>();
        [DataMember] public List<PointCloudConfiguration> PointClouds { get; set; } = new List<PointCloudConfiguration>();
        [DataMember] public List<LaserScanConfiguration> LaserScans { get; set; } = new List<LaserScanConfiguration>();
        [DataMember] public List<JointStateConfiguration> JointStates { get; set; } = new List<JointStateConfiguration>();
        [DataMember] public List<ImageConfiguration> Images { get; set; } = new List<ImageConfiguration>();
        [DataMember] public List<MarkerConfiguration> Markers { get; set; } = new List<MarkerConfiguration>();
        [DataMember] public List<InteractiveMarkerConfiguration> InteractiveMarkers { get; set; } = new List<InteractiveMarkerConfiguration>();
        [DataMember] public List<DepthImageProjectorConfiguration> DepthImageProjectors { get; set; } = new List<DepthImageProjectorConfiguration>();

        public List<IReadOnlyList<IConfiguration>> CreateListOfEntries() => new List<IReadOnlyList<IConfiguration>>
        {
            Grids,
            Robots,
            PointClouds,
            LaserScans,
            JointStates,
            Images,
            Markers,
            InteractiveMarkers,
            DepthImageProjectors
        };

    }


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
        //[SerializeField] Toggle keepReconnecting = null;

        [SerializeField] Sprite ConnectedSprite = null;
        [SerializeField] Sprite ConnectingSprite = null;
        [SerializeField] Sprite DisconnectedSprite = null;
        [SerializeField] Sprite QuestionSprite = null;

        public string Address { get; private set; }

        readonly List<DisplayData> displayDatas = new List<DisplayData>();
        public IReadOnlyList<DisplayData> DisplayDatas => displayDatas;

        DialogData availableDisplays;
        DialogData availableTopics;
        ConnectionDialogData connectionData;

        readonly List<GameObject> buttons = new List<GameObject>();

        TFDisplayData TFData => (TFDisplayData)displayDatas[0];

        readonly HashSet<string> displayedTopics = new HashSet<string>();
        public IReadOnlyCollection<string> DisplayedTopics => displayedTopics;

        void Start()
        {
            parentCanvas = transform.parent.parent.GetComponentInParent<Canvas>();

            buttonHeight = Resource.Widgets.DisplayButton.GameObject.GetComponent<RectTransform>().rect.height;

            CreateDisplay(Resource.Module.TF, "/tf");
            CreateDisplay(Resource.Module.Grid);

            availableDisplays = CreateDialog<AddDisplayDialogData>();
            availableTopics = CreateDialog<AddTopicDialogData>();
            connectionData = CreateDialog<ConnectionDialogData>();

            save.onClick.AddListener(OnSaveClick);
            load.onClick.AddListener(OnLoadClick);
            hide.onClick.AddListener(OnHideClick);

            addDisplayByTopic.onClick.AddListener(OnAddDisplayByTopicClick);
            addDisplay.onClick.AddListener(OnAddDisplayClick);

            MasterUriStr.Label = connectionData.MasterUri + " →";
            MasterUriButton.Clicked += () =>
            {
                connectionData.Select();
            };

            ConnectionManager.Connection.MasterUri = connectionData.MasterUri;
            ConnectionManager.Connection.MyUri = connectionData.MyUri;
            ConnectionManager.Connection.MyId = connectionData.MyId;
            ConnectionManager.Connection.KeepReconnecting = false;

            //ConnectionManager.Instance.KeepReconnecting = keepReconnecting.isOn;
            //keepReconnecting.onValueChanged.AddListener(x => ConnectionManager.Instance.KeepReconnecting = x);
            connectionData.MasterUriChanged += uri =>
            {
                ConnectionManager.Connection.MasterUri = uri;
                ConnectionManager.Connection.KeepReconnecting = false;
                MasterUriStr.Label = (uri == null) ? "(?) →" : uri + " →";
            };
            connectionData.MyIdChanged += id =>
            {
                ConnectionManager.Connection.MyId = id;
                ConnectionManager.Connection.KeepReconnecting = false;
            };
            connectionData.MyUriChanged += uri =>
            {
                ConnectionManager.Connection.MyUri = uri;
                ConnectionManager.Connection.KeepReconnecting = false;
            };
            connectionData.ConnectClicked += () =>
            {
                ConnectionManager.Connection.KeepReconnecting = true;
            };
            connectionData.StopClicked += () =>
            {
                ConnectionManager.Connection.KeepReconnecting = false;
                ConnectionManager.Connection.Disconnect();
            };
            ConnectButton.Clicked += () =>
            {
                ConnectionManager.Connection.KeepReconnecting = true;
            };


            //address.onEndEdit.AddListener(OnAddressChanged);
            status.enabled = false;

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;

            TFListener.GuiManager.Canvases.Add(GetComponentInParent<Canvas>());
            TFListener.GuiManager.Canvases.Add(dataPanelManager.GetComponentInParent<Canvas>());
            TFListener.GuiManager.Canvases.Add(dialogPanelManager.GetComponentInParent<Canvas>());
        }

        /*
        void OnAddressChanged(string newUri)
        {
            if (newUri == "")
            {
                status.enabled = false;
                return;
            }
            status.enabled = true;
            if (!ConnectionManager.Connection.TrySetUri(newUri))
            {
                status.sprite = QuestionSprite;
            }
        }
        */


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
                    break;
                case ConnectionState.Disconnected:
                    GameThread.EverySecond -= RotateSprite;
                    status.sprite = DisconnectedSprite;
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

        void OnHideClick()
        {
            AllGuiVisible = !AllGuiVisible;
            //topicsPanel.Active = false;
            EventSystem.current.SetSelectedGameObject(null);
        }

        void OnSaveClick()
        {
            /*
            JObject root = new JObject
            {
                ["address"] = JToken.FromObject(address.text),
                ["keepReconnecting"] = JToken.FromObject(keepReconnecting.isOn),
                ["displays"] = new JArray(displayDatas.Select(x => x.Serialize()))
            };

            string text = root.ToString();
            File.WriteAllText(Application.persistentDataPath + "/config.json", text);
            Debug.Log("DisplayListPanel: Writing config to " + Application.persistentDataPath + "/config.json");
            */

            StateConfiguration config = new StateConfiguration
            {
                MasterUri = connectionData.MasterUri,
                MyUri = connectionData.MyUri,
                MyId = connectionData.MyId,
                Entries = displayDatas.Select(x => x.Configuration.Id).ToList()
            };
            displayDatas.ForEach(x => x.AddToState(config));

            string text = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(Application.persistentDataPath + "/config.json", text);
            Logger.Debug("DisplayListPanel: Writing config to " + Application.persistentDataPath + "/config.json");

        }

        void OnLoadClick()
        {
            while (displayDatas.Count > 1)
            {
                RemoveDisplay(1);
            }

            Logger.Debug("DisplayListPanel: Reading config from " + Application.persistentDataPath + "/config.json");
            string text;
            try
            {
                text = File.ReadAllText(Application.persistentDataPath + "/config.json");
            }
            catch (Exception e) when (e is IOException || e is System.Security.SecurityException)
            {
                Debug.Log(e);
                return;
            }

            StateConfiguration config = JsonConvert.DeserializeObject<StateConfiguration>(text);

            connectionData.MasterUri = config.MasterUri;
            connectionData.MyUri = config.MyUri;
            connectionData.MyId = config.MyId;

            TFData.UpdateConfiguration(config.Tf);

            void createEntry(IConfiguration x)
            {
                CreateDisplay(x.Module, configuration: x);
            }

            config.CreateListOfEntries().ForEach(x => x.ForEach(createEntry));

            /*
            JObject root = JObject.Parse(text);
            JToken value;

            if (root.TryGetValue("address", out value))
            {
                address.text = value.ToObject<string>();
            }
            OnAddressChanged(address.text);

            //keepReconnecting.isOn = root["keepReconnecting"].ToObject<bool>();

            if (root.TryGetValue("displays", out value))
            {
                foreach (JObject entry in value)
                {
                    if (!entry.TryGetValue("module", out JToken m))
                    {
                        Debug.Log("DisplayListPanel: Display entry missing 'module' property!");
                        continue;
                    }
                    Resource.Module module = m.ToObject<Resource.Module>();
                    switch (module)
                    {
                        case Resource.Module.TF:
                            TFData.Deserialize(entry);
                            break;
                        default:
                            CreateDisplay(module).Deserialize(entry).Start();
                            break;
                    }
                }
            }
            */
        }

        void OnAddDisplayByTopicClick()
        {
            availableTopics.Select();
        }

        void OnAddDisplayClick()
        {
            availableDisplays.Select();
        }

        public DisplayData CreateDisplay(Resource.Module resource, string topic = "", string type = "", IConfiguration configuration = null)
        {
            DisplayDataConstructor constructor = new DisplayDataConstructor()
            {
                Module = resource,
                DisplayList = this,
                Topic = topic,
                Type = type,
                Configuration = configuration
            };
            DisplayData displayData = DisplayData.CreateFromResource(constructor);
            displayDatas.Add(displayData);
            CreateButtonObject(displayData);
            return displayData;
        }

        T CreateDialog<T>() where T : DialogData, new()
        {
            T dialogData = new T();
            dialogData.Initialize(this);
            return dialogData;
        }

        const float yOffset = 5;

        void CreateButtonObject(DisplayData displayData)
        {
            GameObject buttonObject = ResourcePool.GetOrCreate(Resource.Widgets.DisplayButton, contentObject.transform, false);

            int size = buttons.Count();
            float y = yOffset + size * buttonHeight;

            (buttonObject.transform as RectTransform).anchoredPosition = new Vector2(0, -y);

            Text buttonObjectText = buttonObject.GetComponentInChildren<Text>();
            buttonObjectText.text = displayData.ButtonText; // $"<b>{displayData.Module}</b>";
            buttonObject.name = $"Button:{displayData.Module}";
            buttonObject.SetActive(true);
            buttons.Add(buttonObject);

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                displayData.Select();
            });
            (contentObject.transform as RectTransform).sizeDelta = new Vector2(0, y + buttonHeight + yOffset);

            //return buttonObject;
        }

        public DisplayData CreateDisplayForTopic(string topic, string type)
        {
            if (!Resource.ResourceByRosMessageType.TryGetValue(type, out Resource.Module resource))
            {
                throw new ArgumentException(nameof(type));
            }

            return CreateDisplay(resource, topic, type);
        }

        public void RemoveDisplay(DisplayData entry)
        {
            RemoveDisplay(displayDatas.IndexOf(entry));
        }

        public void RemoveDisplay(int index)
        {
            displayedTopics.Remove(displayDatas[index].Topic);
            displayDatas[index].Stop();
            displayDatas.RemoveAt(index);

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

        public void SetButtonText(DisplayData entry, string text)
        {
            int index = displayDatas.IndexOf(entry);
            if (index == -1)
            {
                return;
            }
            GameObject buttonObject = buttons[index];
            buttonObject.GetComponentInChildren<Text>().text = text;
        }

        public void RegisterDisplayedTopic(string topic)
        {
            displayedTopics.Add(topic);
        }
    }
}
