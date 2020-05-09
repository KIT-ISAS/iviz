using System;
using System.Collections.Generic;
using System.Linq;

namespace Iviz.App
{
    public class AddTopicDialogData : DialogData
    {
        static readonly TimeSpan RefreshTime = TimeSpan.FromSeconds(2.5);

        DialogItemList itemList;
        public override IDialogPanelContents Panel => itemList;
        DateTime lastTime;

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
            itemList.Items = topics.Select(x => $"{x.topic}\n<b>{x.resource}</b>");
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;
            lastTime = DateTime.Now;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            DateTime now = DateTime.Now;
            if (now - lastTime > RefreshTime)
            {
                GetTopics();
                lastTime = now;
            }
        }


        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            DisplayListPanel.CreateDisplayForTopic(topics[index].topic, topics[index].type).Start();
            Close();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}