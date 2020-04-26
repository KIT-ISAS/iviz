using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.RoslibSharp;

namespace Iviz.App
{
    public abstract class DialogData
    {
        protected DisplayListPanel DisplayListPanel { get; private set; }
        protected DialogPanelManager DialogPanelManager => DisplayListPanel.dialogPanelManager;
        public abstract IDialogPanelContents Panel { get; }

        public virtual void Initialize(DisplayListPanel panel)
        {
            DisplayListPanel = panel;
        }

        public abstract void SetupPanel();
        public virtual void CleanupPanel() { }
        public virtual void UpdatePanel() { }

        public virtual void Start()
        {

        }

        public virtual void Cleanup()
        {
            DialogPanelManager.HidePanelFor(this);
            DisplayListPanel = null;
        }

        public void Select()
        {
            DialogPanelManager.SelectPanelFor(this);
            DisplayListPanel.AllGuiVisible = true;
        }
    }

    public class AddDisplayDialogData : DialogData
    {
        static readonly List<Tuple<string, Resource.Module>> Displays = new List<Tuple<string, Resource.Module>>()
        {
            Tuple.Create("<b>Robot</b>\nA robot object", Resource.Module.Robot),
            Tuple.Create("<b>Grid</b>\nA reference plane", Resource.Module.Grid),
            Tuple.Create("<b>DepthProjector</b>\nPoint cloud generator for depth images", Resource.Module.DepthImageProjector),
        };

        DialogItemList itemList;
        public override IDialogPanelContents Panel => itemList;

        public override void Initialize(DisplayListPanel panel)
        {
            base.Initialize(panel);
            itemList = (DialogItemList)DialogPanelManager.GetPanelByType(DialogPanelType.ItemList);
        }

        public override void SetupPanel()
        {
            itemList.Title = "Available Displays";
            itemList.Items = Displays.Select(x => x.Item1);
            itemList.ItemClicked += OnItemClicked;
            itemList.CloseClicked += OnCloseClicked;
        }

        void OnCloseClicked()
        {
            Close();
        }

        void OnItemClicked(int index, string _)
        {
            DisplayListPanel.CreateDisplay(Displays[index].Item2);
            Close();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }

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
                string topic = entry.topic;
                string msgType = entry.type;
                if (!DisplayableListener.ResourceByRosMessageType.TryGetValue(msgType, out Resource.Module resource) ||
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
            itemList.Items = topics.Select(x => $"{x.topic}\n<b>{x.resource}</b>{x.resource}");
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
            DisplayListPanel.CreateDisplayForTopic(topics[index].topic, topics[index].type);
            Close();
        }

        void Close()
        {
            DialogPanelManager.HidePanelFor(this);
        }

    }
}