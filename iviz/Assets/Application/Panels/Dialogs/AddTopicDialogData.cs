using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public class AddTopicDialogData : DialogData
    {
        private const int Size = 30;

        DialogItemList itemList;
        public override IDialogPanelContents Panel => itemList;
        DateTime lastTime = DateTime.MinValue;

        class TopicWithResource
        {
            public readonly string topic;
            public readonly string type;
            public readonly Resource.Module resource;

            public TopicWithResource(string topic, string type, Resource.Module resource)
            {
                this.topic = topic;
                this.type = type;
                this.resource = resource;
            }

            public override string ToString()
            {
                return $"{UnityUtils.SanitizedText(topic, Size)}\n<b>{type}</b>";
            }
        }

        readonly List<TopicWithResource> topics = new List<TopicWithResource>();

        public override void Initialize(DisplayListPanel panel)
        {
            base.Initialize(panel);
            itemList = (DialogItemList)DialogPanelManager.GetPanelByType(DialogPanelType.ItemList);
        }

        void GetTopics()
        {
            topics.Clear();
            var newTopics = ConnectionManager.GetSystemPublishedTopics();
            foreach (var entry in newTopics)
            {
                string topic = entry.Topic;
                string msgType = entry.Type;
                if (!Resource.ResourceByRosMessageType.TryGetValue(msgType, out Resource.Module resource) ||
                    DisplayListPanel.DisplayedTopics.Contains(topic))
                {
                    continue;
                }
                topics.Add(new TopicWithResource(topic, msgType, resource));
            }
        }

        public override void SetupPanel()
        {
            GetTopics();
            itemList.Title = "Available Topics";
            itemList.Items = topics.Select(x => x.ToString());
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;
            lastTime = DateTime.Now;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();

            GetTopics();
            itemList.Items = topics.Select(x => x.ToString());
        }


        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            DisplayListPanel.CreateDisplayForTopic(topics[index].topic, topics[index].type);
            Close();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}