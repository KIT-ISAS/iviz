#nullable enable

using System;
using System.Collections.Generic;
using Iviz.App;
using Iviz.Controllers.TF;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Controllers
{
    public class TfPublisher
    {
        int counter;
        readonly Dictionary<string, TfPublishedFrame> frames = new();
        TfPublisherModuleData? currentModuleData;

        public TfPublisher()
        {
            GameThread.EverySecond += OnEverySecond;
        }

        void OnEverySecond()
        {
            if (counter++ != 5)
            {
                return;
            }
            
            counter = 0;
            Publish();
        }

        void Publish()
        {
            foreach (var frame in frames.Values)
            {
                TfListener.Publish(frame.TfFrame);
            }
        }

        public TfFrame Add(string frameId, bool show = false)
        {
            if (frames.TryGetValue(frameId, out var oldFrame))
            {
                return oldFrame.TfFrame;
            }

            var newFrame = new TfPublishedFrame(frameId);
            frames[frameId] = newFrame;

            var newTfFrame = newFrame.TfFrame;
            if (show)
            {
                currentModuleData = new TfPublisherModuleData(newTfFrame);
                currentModuleData.ShowPanel();
            }
            
            TfListener.Publish(newTfFrame);
            return newTfFrame;
        }

        public bool Remove(string frameId)
        {
            if (!frames.TryGetValue(frameId, out var frame))
            {
                return false;
            }

            frame.Dispose();
            frames.Remove(frameId);
            
            if (currentModuleData != null && currentModuleData.Frame.Id == frameId)
            {
                currentModuleData.HidePanel();
            }
            
            return true;
        }

        public void ShowPanelIfPublishing(string frameId)
        {
            if (!frames.TryGetValue(frameId, out var frame))
            {
                currentModuleData?.HidePanel();
                return;
            }

            if (currentModuleData != null && currentModuleData.Frame.Id == frameId)
            {
                currentModuleData.ToggleShowPanel();
                return;
            }
            
            currentModuleData = new TfPublisherModuleData(frame.TfFrame);
            currentModuleData.ShowPanel();
        }

        public void HidePanel()
        {
            currentModuleData?.HidePanel();
        }

        public bool IsPublishing(string frameId) => frames.ContainsKey(frameId);

        public void Dispose()
        {
            GameThread.EverySecond -= OnEverySecond;
        }
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
}