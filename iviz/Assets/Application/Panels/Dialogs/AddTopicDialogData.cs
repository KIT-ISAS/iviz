using Iviz.Roslib;
using System.Collections.Generic;
using System.Linq;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public sealed class AddTopicDialogData : DialogData
    {
        const int MaxLineWidth = 250;

        readonly AddTopicDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        static readonly Color[] ColorList =
        {
            Color.Lerp(Color.yellow, Color.white, 0.95f),
            Color.Lerp(Color.magenta, Color.white, 0.95f),
            Color.Lerp(Color.cyan, Color.white, 0.95f),
            Color.Lerp(Color.red, Color.white, 0.95f),
            Color.Lerp(Color.green, Color.white, 0.95f),
            Color.Lerp(Color.blue, Color.white, 0.95f),
        };

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
                    Resource.ResourceByRosMessageType.TryGetValue(msgType, out ModuleType resource);
                yield return new TopicWithResource(topic, msgType,
                    resourceFound ? resource : ModuleType.Invalid);
            }
        }

        public override void SetupPanel()
        {
            panel.Title = "Available Topics";
            panel.ItemClicked += OnItemClicked;
            panel.CloseClicked += Close;

            UpdatePanel();

            panel.ShowAll.ValueChanged += _ => UpdatePanel();
            panel.SortByType.ValueChanged += _ => UpdatePanel();
        }

        public override void UpdatePanel()
        {
            topics.Clear();

            topics.AddRange(GetTopicCandidates());

            topics.Sort((x, y) => string.CompareOrdinal(x.Topic, y.Topic));
            if (panel.SortByType.Value)
            {
                topics.Sort((x, y) => string.CompareOrdinal(x.ShortType, y.ShortType));
            }

            bool showAll = panel.ShowAll.Value;

            if (!showAll)
            {
                topics.RemoveAll(topic => topic.ResourceType == ModuleType.Invalid);
            }

            panel.Items = topics.Select(topic => topic.ToString());

            if (showAll)
            {
                foreach ((var item, TopicWithResource topic) in panel.Zip(topics))
                {
                    item.Color = ColorList[(int) topic.ResourceType % ColorList.Length];
                    item.Interactable = topic.ResourceType != ModuleType.Invalid;
                }
            }
            else
            {
                foreach ((var item, TopicWithResource topic) in panel.Zip(topics))
                {
                    item.Color = ColorList[(int) topic.ResourceType % ColorList.Length];
                }
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
                    .Where(topic => topic.ResourceType != ModuleType.Invalid)
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
        
        public readonly struct TopicWithResource
        {
            public string Topic { get; }
            public string Type { get; }
            public string ShortType { get; }
            public ModuleType ResourceType { get; }

            public TopicWithResource([NotNull] string topic, [NotNull] string type, ModuleType resourceType)
            {
                Topic = topic;
                Type = type;
                ResourceType = resourceType;

                int lastSlash = Type.LastIndexOf('/');
                ShortType = (lastSlash == -1) ? Type : Type.Substring(lastSlash + 1);
            }

            [NotNull]
            public override string ToString()
            {
                string type = (ResourceType == ModuleType.Invalid) ? Type : ShortType;
                return $"{Resource.Font.Split(Topic, MaxLineWidth)}\n" +
                       $"<b>{Resource.Font.Split(type, MaxLineWidth)}</b>";
            }
        }        
    }
}