#nullable enable

using System;
using System.Threading;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Displays.Highlighters
{
    public class BoundsHighlighter : IAnimatable
    {
        readonly Transform nodeTransform;
        readonly SelectionFrame frame;
        readonly CancellationTokenSource tokenSource;
        readonly Tooltip? tooltip;
        readonly IHasBounds holder;
        readonly bool isPermanent;

        public CancellationToken Token => tokenSource.Token;
        public float Duration { get; }

        public BoundsHighlighter(IHasBounds holder, bool isPermanent = false, float duration = 0.5f)
        {
            ThrowHelper.ThrowIfNull(holder, nameof(holder));
            
            this.holder = holder;
            Duration = duration;
            
            var node = new GameObject("[Bounds Highlighter]");
            nodeTransform = node.transform;
            nodeTransform.SetParentLocal(holder.BoundsTransform);

            frame = ResourcePool.RentDisplay<SelectionFrame>(nodeTransform);
            frame.EnableShadows = false;
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
                tooltip = ResourcePool.RentDisplay<Tooltip>();
                tooltip.CaptionColor = Color.white;
                tooltip.Color = Resource.Colors.TooltipBackground;
                tooltip.Caption = caption;
            }

            Update();

            if (tooltip != null)
            {
                tooltip.PointToCamera();
            }
        }

        void Update()
        {
            if (holder.BoundsTransform is not { } transform 
                || holder.Bounds is not { } bounds)
            {
                nodeTransform.gameObject.SetActive(false);
                return;
            }

            nodeTransform.gameObject.SetActive(true);
            frame.SetBounds(bounds);

            if (tooltip == null)
            {
                return;
            }

            var worldBounds = bounds.TransformBound(transform.AsPose(), transform.lossyScale);
            float labelSize = Tooltip.GetRecommendedSize(transform.position);
            tooltip.Scale = labelSize;
            tooltip.Transform.position = worldBounds.center +
                                         2f * (worldBounds.size.y * 0.3f + labelSize) * Vector3.up;
        }

        public void Update(float t)
        {
            Update();

            float alpha = Mathf.Sqrt(1 - t);
            if (tooltip != null)
            {
                tooltip.CaptionColor = Color.white.WithAlpha(alpha);
                tooltip.Color = Resource.Colors.TooltipBackground.WithAlpha(alpha);
            }

            frame.Color = Color.white.WithAlpha(alpha);
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            frame.ReturnToPool();
            tooltip.ReturnToPool();
            Object.Destroy(nodeTransform.gameObject);

            if (isPermanent)
            {
                GameThread.EveryFrame -= Update;
            }
        }
    }
}