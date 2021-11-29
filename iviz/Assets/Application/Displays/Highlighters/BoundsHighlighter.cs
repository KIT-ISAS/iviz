#nullable enable

using System.Threading;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public class BoundsHighlighter : IAnimatable
    {
        readonly FrameNode node;
        readonly SelectionFrame frame;
        readonly CancellationTokenSource tokenSource;
        readonly IHasBounds holder;
        
        public CancellationToken Token => tokenSource.Token;
        public float Duration => 0.5f;              

        public BoundsHighlighter(IHasBounds holder)
        {
            this.holder = holder;
            node = FrameNode.Instantiate("[Bounds Highlighter]");
            node.Transform.SetParentLocal(holder.BoundsTransform);
            
            frame = ResourcePool.RentDisplay<SelectionFrame>(node.Transform);
            frame.ShadowsEnabled = false;
            tokenSource = new CancellationTokenSource();
        }
        
        public void UpdateFrame(float t)
        {
            if (holder.BoundsTransform is not { } transform
                || holder.Bounds is not { } bounds)
            {
                node.gameObject.SetActive(false);
                return;
            }
            
            node.gameObject.SetActive(true);
            node.Transform.SetParentLocal(transform);
            frame.SetBounds(bounds);
            
            float alpha = Mathf.Sqrt(1 - t);
            frame.Color = Color.white.WithAlpha(alpha);
            frame.EmissiveColor = Color.blue;
        }
        
        public void Dispose()
        {
            tokenSource.Cancel();
            frame.ReturnToPool();
            node.DestroySelf();
        }        
    }
}