#nullable enable

using System;
using System.Collections.Generic;
using Iviz.App;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Controllers
{
    public class TfPublisher : IController
    {
        const int PublishTimeInSec = 5;
        
        readonly Dictionary<string, TfPublishedFrame> frames = new();
        PublishedFramePanelData? panelData;
        int secondCounter;

        public int NumPublishedFrames => frames.Count;

        public TfPublisher()
        {
            GameThread.EverySecond += OnEverySecond;
        }

        void OnEverySecond()
        {
            if (secondCounter++ != PublishTimeInSec)
            {
                return;
            }
            
            secondCounter = 0;
            Publish();
        }

        void Publish()
        {
            foreach (var frame in frames.Values)
            {
                TfListener.Publish(frame.TfFrame);
            }
        }

        public TfPublishedFrame Add(string frameId, bool show = false)
        {
            if (frames.TryGetValue(frameId, out var oldFrame))
            {
                return oldFrame;
            }

            var newFrame = new TfPublishedFrame(frameId);
            frames[frameId] = newFrame;

            var newTfFrame = newFrame.TfFrame;
            if (show)
            {
                panelData = new PublishedFramePanelData(newTfFrame);
                panelData.ShowPanel();
            }
            
            TfListener.Publish(newTfFrame);
            return newFrame;
        }

        public bool Remove(string frameId)
        {
            if (!frames.TryGetValue(frameId, out var frame))
            {
                return false;
            }

            frame.Dispose();
            frames.Remove(frameId);
            
            if (panelData != null && panelData.Frame.Id == frameId)
            {
                panelData.HidePanel();
            }
            
            return true;
        }

        public void ShowPanelIfPublishing(string frameId)
        {
            if (!frames.TryGetValue(frameId, out var frame))
            {
                panelData?.HidePanel();
                return;
            }

            if (panelData != null && panelData.Frame.Id == frameId)
            {
                panelData.ToggleShowPanel();
                return;
            }
            
            panelData = new PublishedFramePanelData(frame.TfFrame);
            panelData.ShowPanel();
        }

        public bool IsPublishing(string frameId) => frames.ContainsKey(frameId);

        public void Dispose()
        {
            GameThread.EverySecond -= OnEverySecond;
        }

        public void ResetController()
        {
            throw new NotImplementedException();
        }

        public bool Visible { get; set; }
    }

    public sealed class TfPublishedFrame
    {
        readonly FrameNode frameNode;
        public TfFrame TfFrame { get; }

        public TfPublishedFrame(string id)
        {
            frameNode = FrameNode.Instantiate("[Publisher]");
            frameNode.AttachTo(id);
            TfFrame = frameNode.Parent.CheckedNull()
                      ?? throw new NullReferenceException("Failed to set origin parent"); // shouldn't happen
        }
        
        public void Dispose()
        {
            frameNode.DestroySelf();
        }
    }

    public class TfPublisherConfiguration : IConfiguration
    {
        public string Id { get; set; } = "";
        public ModuleType ModuleType => ModuleType.TFPublisher;
        public bool Visible => true;
    }
}