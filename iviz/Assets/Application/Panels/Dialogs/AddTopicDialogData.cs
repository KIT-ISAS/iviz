using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class AddTopicDialogData : DialogData
    {
        const int MaxLineWidth = 250;

        readonly AddTopicDialogContents panel;
        public override IDialogPanelContents Panel => panel;
        bool sortByType = false;

        class TopicWithResource
        {
            public string Topic { get; }
            public string Type { get; }
            public string ShortType { get; }
            public Resource.ModuleType ResourceType { get; }

            public TopicWithResource([NotNull] string topic, [NotNull] string type, Resource.ModuleType resourceType)
            {
                Topic = topic;
                Type = type;
                ResourceType = resourceType;

                int lastSlash = Type.LastIndexOf('/');
                ShortType = (lastSlash == -1) ? Type : Type.Substring(lastSlash + 1);
            }

            public override string ToString()
            {
                string type = (ResourceType == Resource.ModuleType.Invalid) ? Type : ShortType;
                return $"{Resource.Font.Split(Topic, MaxLineWidth)}\n" +
                       $"<b>{Resource.Font.Split(type, MaxLineWidth)}</b>";
            }
        }

        readonly List<TopicWithResource> topics = new List<TopicWithResource>();

        public AddTopicDialogData([NotNull] ModuleListPanel newModuleListPanel) : base(newModuleListPanel)
        {
            panel = DialogPanelManager.GetPanelByType<AddTopicDialogContents>(DialogPanelType.AddTopic);
            panel.ShowAll.Value = false;
        }

        void GetTopics()
        {
            topics.Clear();
            var newTopics = ConnectionManager.Connection.GetSystemTopicTypes();
            foreach (var entry in newTopics)
            {
                string topic = entry.Topic;
                string msgType = entry.Type;

                if (ModuleListPanel.DisplayedTopics.Contains(topic))
                {
                    continue;
                }

                bool resourceFound =
                    Resource.ResourceByRosMessageType.TryGetValue(msgType, out Resource.ModuleType resource);
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
                    if (topics[i].ResourceType == Resource.ModuleType.Invalid)
                    {
                        panel[i].Interactable = false;
                    }
                }
            }

            panel.EmptyText = ConnectionManager.IsConnected ? "No Topics Available" : "(Not Connected)";
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