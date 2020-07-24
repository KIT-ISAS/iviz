using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Resources;

namespace Iviz.App
{
    public sealed class AddTopicDialogData : DialogData
    {
        const int MaxLineWidth = 250;

        AddTopicDialogContents panel;
        public override IDialogPanelContents Panel => panel;
        bool sortByType = false;

        class TopicWithResource
        {
            public string Topic { get; }
            public string Type { get; }
            public string ShortType { get; }
            public Resource.Module Resource { get; }

            public TopicWithResource(string topic, string type, Resource.Module resource)
            {
                Topic = topic;
                Type = type;
                Resource = resource;

                int lastSlash = Type.LastIndexOf('/');
                ShortType = (lastSlash == -1) ? Type : Type.Substring(lastSlash + 1);
            }

            public override string ToString()
            {
                return $"{Iviz.Resources.Resource.Font.Split(Topic, MaxLineWidth)}\n" +
                       $"<b>{Iviz.Resources.Resource.Font.Split(ShortType, MaxLineWidth)}</b>";
            }
        }

        readonly List<TopicWithResource> topics = new List<TopicWithResource>();

        public override void Initialize(ModuleListPanel newPanel)
        {
            base.Initialize(newPanel);
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
            panel.CloseClicked += Close;

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

            topics.Sort((x, y) => string.CompareOrdinal(x.Topic, y.Topic));
            if (sortByType)
            {
                topics.Sort((x, y) => string.CompareOrdinal(x.ShortType, y.ShortType));
            }
            
            panel.Items = topics.Select(x => x.ToString());
            
            if (panel.ShowAll.Value)
            {
                for (int i = 0; i < topics.Count; i++)
                {
                    if (topics[i].Resource == Resource.Module.Invalid)
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
            var moduleData = ModuleListPanel.CreateModuleForTopic(topics[index].Topic, topics[index].Type);
            Close();
            moduleData.ShowPanel();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}