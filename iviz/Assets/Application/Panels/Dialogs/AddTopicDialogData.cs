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

        AddTopicDialogContents panel;
        public override IDialogPanelContents Panel => panel;

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
            this.panel = (AddTopicDialogContents)DialogPanelManager.GetPanelByType(DialogPanelType.AddTopic);
            this.panel.ShowAll.Value = false;
        }

        void GetTopics()
        {
            topics.Clear();
            var newTopics = ConnectionManager.GetSystemPublishedTopics();
            foreach (var entry in newTopics)
            {
                string topic = entry.Topic;
                string msgType = entry.Type;

                if (ModuleListPanel.DisplayedTopics.Contains(topic))
                {
                    continue;
                }
                bool resourceFound =
                    Resource.ResourceByRosMessageType.TryGetValue(msgType, out Resource.Module resource);
                if (!resourceFound && !panel.ShowAll.Value)
                {
                    continue;
                }
                topics.Add(new TopicWithResource(topic, msgType, resource));
            }
        }

        public override void SetupPanel()
        {
            panel.Title = "Available Topics";
            panel.ItemClicked += OnItemClicked;
            panel.CloseClicked += OnClose;

            UpdatePanel();

            panel.ShowAll.ValueChanged += _ =>
            {
                UpdatePanel();
            };
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();

            GetTopics();
            panel.Items = topics.Select(x => x.ToString());
            if (panel.ShowAll.Value)
            {
                for (int i = 0; i < topics.Count; i++)
                {
                    if (topics[i].resource == Resource.Module.Invalid)
                    {
                        panel[i].Interactable = false;
                    }
                }
            }
            panel.EmptyText = ConnectionManager.Connected ?
                "No Topics Available" :
                "(Not Connected)";
        }

        void OnItemClicked(int index, string _)
        {
            ModuleListPanel.CreateModuleForTopic(topics[index].topic, topics[index].type);
            OnClose();
        }

        void OnClose()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}