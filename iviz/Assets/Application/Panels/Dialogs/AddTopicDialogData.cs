#nullable enable

using System.Collections.Generic;
using System.Linq;
using BigGustave;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public sealed class AddTopicDialogData : DialogData
    {
        const int MaxLineWidth = 250;

        static readonly Color[] ColorList =
        {
            Color.Lerp(Color.yellow, Color.white, 0.95f),
            Color.Lerp(Color.magenta, Color.white, 0.95f),
            Color.Lerp(Color.cyan, Color.white, 0.95f),
            Color.Lerp(Color.red, Color.white, 0.95f),
            Color.Lerp(Color.green, Color.white, 0.95f),
            Color.Lerp(Color.blue, Color.white, 0.95f),
        };

        readonly AddTopicDialogContents panel;
        readonly List<TopicWithResource> topics = new();
        readonly List<TopicWithResource> newTopics = new();
        uint? previousHash;

        public override IDialogPanelContents Panel => panel;

        public AddTopicDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<AddTopicDialogContents>(DialogPanelType.AddTopic);
            panel.ShowAll.Value = false;
        }

        static void GetTopicCandidates(List<TopicWithResource> result)
        {
            var newTopics = ConnectionManager.Connection.GetSystemPublishedTopicTypes();
            foreach ((string topic, string msgType) in newTopics)
            {
                if (ModuleListPanel.Instance.DisplayedTopics.Contains(topic))
                {
                    continue;
                }

                bool resourceFound =
                    Resource.ResourceByRosMessageType.TryGetValue(msgType, out ModuleType resource);
                result.Add(new TopicWithResource(topic, msgType, resourceFound ? resource : ModuleType.Invalid));
            }
        }


        public override void SetupPanel()
        {
            panel.Title = "Available Topics";
            panel.ItemClicked += OnItemClicked;
            panel.CloseClicked += Close;

            UpdatePanel();

            panel.ShowAll.ValueChanged += _ => RebuildTopics(true);
            panel.SortByType.ValueChanged += _ => RebuildTopics(true);
        }

        bool TopicsHaveChanged()
        {
            uint hash = Crc32Calculator.DefaultSeed;
            foreach (var topic in newTopics)
            {
                hash = Crc32Calculator.Compute(topic.topic, hash);
                hash = Crc32Calculator.Compute(topic.type, hash);
            }

            if (previousHash == hash)
            {
                return false;
            }

            previousHash = hash;
            return true;
        }

        public override void UpdatePanel()
        {
            RebuildTopics();
        }

        void RebuildTopics(bool forceRebuild = false)
        {
            newTopics.Clear();
            GetTopicCandidates(newTopics);
            if (!forceRebuild && !TopicsHaveChanged())
            {
                return;
            }

            topics.Clear();
            topics.AddRange(newTopics);

            topics.Sort((x, y) => string.CompareOrdinal(x.topic, y.topic));
            if (panel.SortByType.Value)
            {
                topics.Sort((x, y) => string.CompareOrdinal(x.shortType, y.shortType));
            }

            bool showAll = panel.ShowAll.Value;

            if (!showAll)
            {
                topics.RemoveAll(topic => topic.resourceType == ModuleType.Invalid);
            }

            panel.SetItems(topics.Select(topic => topic.ToString()));

            if (showAll)
            {
                foreach (var (item, topic) in panel.Zip(topics))
                {
                    item.Color = ColorList[(int)topic.resourceType % ColorList.Length];
                    item.Interactable = topic.resourceType != ModuleType.Invalid;
                }
            }
            else
            {
                foreach (var (item, topic) in panel.Zip(topics))
                {
                    item.Color = ColorList[(int)topic.resourceType % ColorList.Length];
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
                    .Where(topic => topic.resourceType != ModuleType.Invalid)
                    .ElementAtOrDefault(index);
            }

            if (clickedTopic.resourceType == ModuleType.Invalid)
            {
                return;
            }

            var moduleData = ModuleListPanel.CreateModuleForTopic(clickedTopic.topic, clickedTopic.type);
            Close();
            moduleData.ShowPanel();
        }

        readonly struct TopicWithResource
        {
            public readonly string topic;
            public readonly string type;
            public readonly string shortType;
            public readonly ModuleType resourceType;

            public TopicWithResource(string topic, string type, ModuleType resourceType)
            {
                this.topic = topic;
                this.type = type;
                this.resourceType = resourceType;

                int lastSlash = type.LastIndexOf('/');
                shortType = (lastSlash == -1) ? type : type[(lastSlash + 1)..];
            }

            public override string ToString()
            {
                string typeStr = (resourceType == ModuleType.Invalid) ? type : shortType;
                return $"{Resource.Font.Split(topic, MaxLineWidth)}\n" +
                       $"<b>{Resource.Font.Split(typeStr, MaxLineWidth)}</b>";
            }
        }
    }
}