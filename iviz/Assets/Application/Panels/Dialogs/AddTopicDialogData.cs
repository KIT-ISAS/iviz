﻿#nullable enable

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
                result.Add(new TopicWithResource(topic, msgType,
                    resourceFound ? resource : ModuleType.Invalid));
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

        bool TopicsHaveChanged()
        {
            uint hash = Crc32Calculator.DefaultSeed;
            foreach (var topic in newTopics)
            {
                hash = Crc32Calculator.Compute(topic.Topic, hash);
                hash = Crc32Calculator.Compute(topic.Type, hash);
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
            newTopics.Clear();
            GetTopicCandidates(newTopics);
            if (!TopicsHaveChanged())
            {
                return;
            }

            topics.Clear();
            topics.AddRange(newTopics);

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

            panel.SetItems(topics.Select(topic => topic.ToString()));

            if (showAll)
            {
                foreach (var (item, topic) in panel.Zip(topics))
                {
                    item.Color = ColorList[(int)topic.ResourceType % ColorList.Length];
                    item.Interactable = topic.ResourceType != ModuleType.Invalid;
                }
            }
            else
            {
                foreach (var (item, topic) in panel.Zip(topics))
                {
                    item.Color = ColorList[(int)topic.ResourceType % ColorList.Length];
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

            if (clickedTopic.ResourceType == ModuleType.Invalid)
            {
                return;
            }

            var moduleData = ModuleListPanel.CreateModuleForTopic(clickedTopic.Topic, clickedTopic.Type);
            Close();
            moduleData.ShowPanel();
        }

        readonly struct TopicWithResource
        {
            public string Topic { get; }
            public string Type { get; }
            public string ShortType { get; }
            public ModuleType ResourceType { get; }

            public TopicWithResource(string topic, string type, ModuleType resourceType)
            {
                Topic = topic;
                Type = type;
                ResourceType = resourceType;

                int lastSlash = Type.LastIndexOf('/');
                ShortType = (lastSlash == -1) ? Type : Type[(lastSlash + 1)..];
            }

            public override string ToString()
            {
                string type = (ResourceType == ModuleType.Invalid) ? Type : ShortType;
                return $"{Resource.Font.Split(Topic, MaxLineWidth)}\n" +
                       $"<b>{Resource.Font.Split(type, MaxLineWidth)}</b>";
            }
        }
    }
}