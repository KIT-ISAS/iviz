#nullable enable

using System;
using System.Threading;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.Highlighters
{
    public class BoundsHighlighter : IAnimatable
    {
        readonly FrameNode node;
        readonly SelectionFrame frame;
        readonly CancellationTokenSource tokenSource;
        readonly Tooltip? tooltip;
        readonly IHasBounds holder;
        readonly bool isPermanent;

        public CancellationToken Token => tokenSource.Token;
        public float Duration => 0.5f;

        public BoundsHighlighter(IHasBounds holder, bool isPermanent = false)
        {
            this.holder = holder ?? throw new ArgumentNullException(nameof(holder));
            node = FrameNode.Instantiate("[Bounds Highlighter]");
            node.Transform.SetParentLocal(holder.BoundsTransform);

            frame = ResourcePool.RentDisplay<SelectionFrame>(node.Transform);
            frame.ShadowsEnabled = false;
            frame.EmissiveColor = Color.blue;
            frame.Color = Color.white;
            
            tokenSource = new CancellationTokenSource();

            this.isPermanent = isPermanent;
            if (isPermanent)
            {
                GameThread.EveryFrame += Update;
            }
            
            if (holder.Caption is { } caption)
            {
                tooltip = ResourcePool.RentDisplay<Tooltip>(node.Transform);
                tooltip.CaptionColor = Color.white;
                tooltip.BackgroundColor = Resource.Colors.HighlighterBackground;
                tooltip.Layer = LayerType.IgnoreRaycast;
                tooltip.Caption = caption;
                tooltip.PointToCamera();
            }

            if (isPermanent)
            {
                Update();
            }
        }

        void Update()
        {
            if (holder.BoundsTransform is not { } transform || holder.Bounds is not { } bounds)
            {
                node.gameObject.SetActive(false);
                return;
            }

            node.gameObject.SetActive(true);
            node.Transform.SetParentLocal(transform);
            frame.SetBounds(bounds);

            if (tooltip == null)
            {
                return;
            }
            
            float labelSize = Tooltip.GetRecommendedSize(transform.position);
            var worldBounds = bounds.TransformBound(transform);
            tooltip.Scale = labelSize;
            tooltip.Transform.position = worldBounds.center +
                                         2f * (worldBounds.size.y * 0.3f + labelSize) * Vector3.up;
        }

        public void Update(float t)
        {
            Update();

            float alpha = Mathf.Sqrt(1 - t);
            frame.Color = Color.white.WithAlpha(alpha);
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            frame.ReturnToPool();
            tooltip.ReturnToPool();
            node.DestroySelf();
            
            if (isPermanent)
            {
                GameThread.EveryFrame -= Update;
            }
        }
    }
}