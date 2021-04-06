using Iviz.Roslib;
using System.Collections.Generic;
using System.Linq;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.XmlRpc;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class AddTopicDialogData : DialogData
    {
        const int MaxLineWidth = 250;
        const bool SortByType = false;

        readonly AddTopicDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public readonly struct TopicWithResource
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

        public AddTopicDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<AddTopicDialogContents>(DialogPanelType.AddTopic);
            panel.ShowAll.Value = false;
        }

        public static IEnumerable<TopicWithResource> GetTopicCandidates()
        {
            var newTopics = ConnectionManager.Connection.GetSystemTopicTypes();
            foreach (var entry in newTopics)
            {
                string topic = entry.Topic;
                string msgType = entry.Type;

                if (ModuleListPanel.Instance.DisplayedTopics.Contains(topic))
                {
                    continue;
                }

                bool resourceFound =
                    Resource.ResourceByRosMessageType.TryGetValue(msgType, out Resource.ModuleType resource);
                yield return new TopicWithResource(topic, msgType,
                    resourceFound ? resource : Resource.ModuleType.Invalid);
            }
        }

        public override void SetupPanel()
        {
            panel.Title = "Available Topics";
            panel.ItemClicked += OnItemClicked;
            panel.CloseClicked += Close;

            UpdatePanel();

            panel.ShowAll.ValueChanged += _ => { UpdatePanel(); };
        }

        public override void UpdatePanel()
        {
            topics.Clear();

            topics.AddRange(GetTopicCandidates());

            topics.Sort((x, y) => string.CompareOrdinal(x.Topic, y.Topic));
            if (SortByType)
            {
                topics.Sort((x, y) => string.CompareOrdinal(x.ShortType, y.ShortType));
            }

            if (panel.ShowAll.Value)
            {
                panel.Items = topics.Select(topic => topic.ToString());

                foreach ((var item, TopicWithResource topic) in panel.Zip(topics))
                {
                    if (topic.ResourceType == Resource.ModuleType.Invalid)
                    {
                        item.Interactable = false;
                    }
                }
            }
            else
            {
                panel.Items = topics
                    .Where(topic => topic.ResourceType != Resource.ModuleType.Invalid)
                    .Select(topic => topic.ToString());
            }

            panel.EmptyText = ConnectionManager.IsConnected ? "No Topics Available" : "(Not Connected)";
        }

        void OnItemClicked(int index, int _)
        {
            TopicWithResource clickedTopic;
            if (panel.ShowAll.Value)
            {
                clickedTopic = topics[index];
            }
            else
            {
                clickedTopic = topics
                    .Where(topic => topic.ResourceType != Resource.ModuleType.Invalid)
                    .ElementAtOrDefault(index);
            }

            if (clickedTopic.Topic == null)
            {
                return;
            }

            var moduleData = ModuleListPanel.CreateModuleForTopic(clickedTopic.Topic, clickedTopic.Type);
            Close();
            moduleData.ShowPanel();
        }
    }
}