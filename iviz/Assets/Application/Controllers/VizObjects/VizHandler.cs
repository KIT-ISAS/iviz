#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XR;
using Iviz.Msgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
    public abstract class VizHandler
    {
        protected readonly Dictionary<string, VizObject> vizObjects = new();

        bool interactable;
        bool visible;

        public int Count => vizObjects.Count;
        
        public abstract string BriefDescription { get; }
        
        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                foreach (var vizObject in vizObjects.Values)
                {
                    vizObject.Visible = value;
                }
            }
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                foreach (var vizObject in vizObjects.Values)
                {
                    vizObject.Interactable = value;
                }
            }
        }

        protected void HandleRemove(string id)
        {
            if (!vizObjects.TryGetValue(id, out var vizObject))
            {
                return;
            }

            vizObject.Dispose();
            vizObjects.Remove(id);
        }

        public void RemoveAll()
        {
            foreach (var vizObject in vizObjects.Values)
            {
                vizObject.Dispose();
            }

            vizObjects.Clear();
        }

        public void GenerateLog(StringBuilder description, int minIndex, int numEntries)
        {
            ThrowHelper.ThrowIfNull(description, nameof(description));

            foreach (var vizObject in vizObjects.Values.Skip(minIndex).Take(numEntries))
            {
                vizObject.GenerateLog(description);
                description.AppendLine();
            }

            description.AppendLine().AppendLine();
        }

        public virtual void Dispose()
        {
            RemoveAll();
        }

        // ----------------------------------------------

        protected class VizObject
        {
            readonly ResourceKey<GameObject> resourceKey;
            readonly string typeDescription;
            protected readonly FrameNode node;
            protected readonly IDisplay display;
            protected float scale = 1;
            public readonly string id;

            protected string FrameId => node.Parent?.Id ?? TfModule.DefaultFrame.Id;

            public bool Visible
            {
                set => node.Visible = value;
            }

            public bool Interactable
            {
                set
                {
                    if (display is IIsInteractable interactable)
                    {
                        interactable.Interactable = value;
                    }
                }
            }

            protected VizObject(string id, ResourceKey<GameObject> resourceKey, string typeDescription)
            {
                node = new FrameNode(id);
                this.resourceKey = resourceKey;
                this.typeDescription = typeDescription;
                this.id = id;

                if (!ResourcePool.Rent(resourceKey, node.Transform).TryGetComponent(out display))
                {
                    ThrowHelper.ThrowMissingAssetField("Viz object does not have a display!");
                }
            }

            public void Dispose()
            {
                Interactable = true;
                display.ReturnToPool(resourceKey);
                node.Dispose();
            }

            public void GenerateLog(StringBuilder description)
            {
                description.Append("<b>").Append(id).Append("</b>").AppendLine();
                description.Append(typeDescription).AppendLine();
                description.AppendLine();
            }

            public override string ToString() => $"[{nameof(VizObject)} '{id}']";
        }
    }
}