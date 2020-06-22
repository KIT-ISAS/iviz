using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public class AddTopicDialogData : DialogData
    {
        const int MaxLineWidth = 250;

        DialogItemList itemList;
        public override IDialogPanelContents Panel => itemList;

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
                return $"{Resource.Font.Split(topic, MaxLineWidth)}\n<b>{type}</b>";
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
                    ModuleListPanel.DisplayedTopics.Contains(topic))
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
            itemList.EmptyText = ConnectionManager.Connected ?
                "No Topics Available" :
                "(Not Connected)";
            itemList.Items = topics.Select(x => x.ToString());
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();

            GetTopics();
            itemList.Items = topics.Select(x => x.ToString());
            itemList.EmptyText = ConnectionManager.Connected ?
                "No Topics Available" :
                "(Not Connected)";
        }


        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            ModuleListPanel.CreateModuleForTopic(topics[index].topic, topics[index].type);
            Close();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}