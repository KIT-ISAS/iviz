using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Iviz.App
{

    public class TopicSelectionPanel : MonoBehaviour
    {
        public DisplayListPanel displaysPanel;
        public GameObject contentObject;
        public Text noTopicsText;
        public TrashButtonWidget closeButton;
        readonly List<GameObject> topicButtons = new List<GameObject>();
        const float yOffset = 5;
        Canvas parentCanvas;
        float buttonHeight;

        public bool Active
        {
            get => parentCanvas.gameObject.activeSelf;
            set
            {
                parentCanvas.gameObject.SetActive(value);
                parentCanvas.enabled = value;
                if (value)
                {
                    UpdateDisplays();
                    GameThread.EverySecond += UpdateDisplays;
                } else
                {
                    GameThread.EverySecond -= UpdateDisplays;
                }
            }
        }

        void Start()
        {
            Resource.Widgets.Initialize();

            parentCanvas = GetComponentInParent<Canvas>();
            buttonHeight = (Resource.Widgets.TopicsButton.GameObject.transform as RectTransform).rect.height;
            closeButton.Clicked += OnCloseClick;
            Active = false;
        }

        void UpdateDisplays()
        {
            topicButtons.ForEach(x => Destroy(x.gameObject));
            topicButtons.Clear();

            var topics = ConnectionManager.GetSystemPublishedTopics();
            int i = 0;
            foreach (var entry in topics)
            {
                string topic = entry.topic;
                string msgType = entry.type;
                if (!DisplayableListener.ResourceByRosMessageType.TryGetValue(msgType, out Resource.Module resource) ||
                    displaysPanel.DisplayedTopics.Contains(topic))
                {
                    continue;
                }

                float y = yOffset + i * (yOffset + buttonHeight);

                GameObject buttonObject = ResourcePool.GetOrCreate(Resource.Widgets.TopicsButton, contentObject.transform, false);
                (buttonObject.transform as RectTransform).anchoredPosition = new Vector2(0, -y);
                buttonObject.GetComponentInChildren<Text>().text = $"{topic}\n<b>{resource}</b>"; 
                buttonObject.SetActive(true);
                
                topicButtons.Add(buttonObject);

                buttonObject.GetComponent<Button>().onClick.AddListener(() => OnTopicClick(buttonObject, topic, msgType));
                
                i++;
            }
            noTopicsText.gameObject.SetActive(i == 0);
            (contentObject.transform as RectTransform).sizeDelta = new Vector2(0, 2*yOffset + (i+1) * buttonHeight);
        }

        void RemoveEntry(GameObject entry)
        {
            int index = topicButtons.IndexOf(entry);
            topicButtons.RemoveAt(index);

            entry.GetComponent<Button>().onClick.RemoveAllListeners();
            ResourcePool.Dispose(Resource.Widgets.TopicsButton, entry);

            int i;
            for (i = index; i < topicButtons.Count(); i++)
            {
                GameObject buttonObject = topicButtons[i].gameObject;
                float y = yOffset + i * buttonHeight;
                (buttonObject.transform as RectTransform).anchoredPosition = new Vector3(0, -y);
            }
            (contentObject.transform as RectTransform).sizeDelta = new Vector2(0, 2*yOffset + i * buttonHeight);
        }

        void OnCloseClick()
        {
            Active = false;
        }

        void OnTopicClick(GameObject buttonObject, string topic, string msgType)
        {
            DisplayData displayData = displaysPanel.CreateDisplayForTopic(topic, msgType);
            displayData.Start();
            displayData.Select();
            RemoveEntry(buttonObject);
            Active = false;
        }
    }
}
 