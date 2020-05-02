using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        const float yOffset = 5;

        public InputField address;
        public Button save;
        public Button load;
        public Button hide;
        public Image status;

        public GameObject contentObject;
        public DataPanelManager dataPanelManager;
        public DialogPanelManager dialogPanelManager;
        public Button addDisplayByTopic;
        public Button addDisplay;
        public Toggle keepReconnecting;

        public Sprite ConnectedSprite;
        public Sprite ConnectingSprite;
        public Sprite DisconnectedSprite;
        public Sprite QuestionSprite;

        public string Address { get; private set; }

        readonly List<DisplayData> displayDatas = new List<DisplayData>();
        public IReadOnlyList<DisplayData> DisplayDatas => displayDatas;

        DialogData availableDisplays;
        DialogData availableTopics;

        readonly List<GameObject> buttons = new List<GameObject>();

        DisplayData TFData => displayDatas[0];

        readonly HashSet<string> displayedTopics = new HashSet<string>();
        public IReadOnlyCollection<string> DisplayedTopics => displayedTopics;

        void Start()
        {
            parentCanvas = transform.parent.parent.GetComponentInParent<Canvas>();

            buttonHeight = Resource.Widgets.DisplayButton.GameObject.GetComponent<RectTransform>().rect.height;

            CreateDisplay(Resource.Module.TF, "/tf").Start();
            CreateDisplay(Resource.Module.Grid).Start();

            availableDisplays = CreateDialog<AddDisplayDialogData>();
            availableTopics = CreateDialog<AddTopicDialogData>();

            save.onClick.AddListener(OnSaveClick);
            load.onClick.AddListener(OnLoadClick);
            hide.onClick.AddListener(OnHideClick);

            addDisplayByTopic.onClick.AddListener(OnAddDisplayByTopicClick);
            addDisplay.onClick.AddListener(OnAddDisplayClick);

            //ConnectionManager.Instance.KeepReconnecting = keepReconnecting.isOn;
            //keepReconnecting.onValueChanged.AddListener(x => ConnectionManager.Instance.KeepReconnecting = x);

            address.onEndEdit.AddListener(OnAddressChanged);
            status.enabled = false;

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;

            TFListener.GuiManager.Canvases.Add(GetComponentInParent<Canvas>());
            TFListener.GuiManager.Canvases.Add(dataPanelManager.GetComponentInParent<Canvas>());
            TFListener.GuiManager.Canvases.Add(dialogPanelManager.GetComponentInParent<Canvas>());
        }

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

        void OnConnectionStateChanged(ConnectionState state)
        {
            status.rectTransform.localRotation = Quaternion.identity;

            if (address.text == "")
            {
                return;
            }
            if (ConnectionManager.Connection.Uri == null)
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
            JObject root = new JObject
            {
                ["address"] = JToken.FromObject(address.text),
                ["keepReconnecting"] = JToken.FromObject(keepReconnecting.isOn),
                ["displays"] = new JArray(displayDatas.Select(x => x.Serialize()))
            };

            string text = root.ToString();
            File.WriteAllText(Application.persistentDataPath + "/config.json", text);
            Debug.Log("DisplayListPanel: Writing config to " + Application.persistentDataPath + "/config.json");
        }

        void OnLoadClick()
        {
            while (displayDatas.Count > 1)
            {
                RemoveDisplay(1);
            }

            Debug.Log("DisplayListPanel: Writing config from " + Application.persistentDataPath + "/config.json");
            string text;
            try
            {
                text = File.ReadAllText(Application.persistentDataPath + "/config.json");
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return;
            }
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
        }

        void OnAddDisplayByTopicClick()
        {
            availableTopics.Select();
        }

        void OnAddDisplayClick()
        {
            availableDisplays.Select();
        }

        public DisplayData CreateDisplay(Resource.Module resource, string topic = "", string type = "")
        {
            DisplayData displayData = DisplayData.CreateFromResource(resource);
            displayData.Initialize(this, topic, type);
            displayDatas.Add(displayData);

            CreateButtonObject(displayData).name = $"Button:{resource}";

            return displayData;
        }

        T CreateDialog<T>() where T : DialogData, new()
        {
            T dialogData = new T();
            dialogData.Initialize(this);
            dialogData.Start();
            return dialogData;
        }

        GameObject CreateButtonObject(DisplayData displayData)
        {
            GameObject buttonObject = ResourcePool.GetOrCreate(Resource.Widgets.DisplayButton, contentObject.transform, false);

            int size = buttons.Count();
            float y = yOffset + size * buttonHeight;

            (buttonObject.transform as RectTransform).anchoredPosition = new Vector2(0, -y);

            Text buttonObjectText = buttonObject.GetComponentInChildren<Text>();
            buttonObjectText.text = $"<b>{displayData.Module.ToString()}</b>";
            buttonObject.SetActive(true);

            buttons.Add(buttonObject);

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                displayData.Select();
            });
            (contentObject.transform as RectTransform).sizeDelta = new Vector2(0, y + buttonHeight + yOffset);

            return buttonObject;
        }

        public DisplayData CreateDisplayForTopic(string topic, string type)
        {
            if (!DisplayableListener.ResourceByRosMessageType.TryGetValue(type, out Resource.Module resource))
            {
                return null;
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
            displayDatas[index].Cleanup();
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
            GameObject buttonObject = buttons[index];
            buttonObject.GetComponentInChildren<Text>().text = text;
        }

        public void RegisterDisplayedTopic(string topic)
        {
            displayedTopics.Add(topic);
        }
    }
}
