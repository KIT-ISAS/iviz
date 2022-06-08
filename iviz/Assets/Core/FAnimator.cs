#nullable enable

using System;
using System.Threading;
using Iviz.Common;
using Iviz.Msgs;
using Iviz.TfHelpers;
using UnityEngine;

namespace Iviz.Core
{
    /// <summary>
    /// Convenience class that will call back a given function each frame for a given time interval.
    /// Used by displays to implement animations. 
    /// </summary>
    public sealed class FAnimator
    {
        readonly Action<float> update;
        readonly Action? dispose;
        readonly CancellationToken token;
        readonly float startTime;
        readonly float duration;
        
        FAnimator(Action<float> update, Action? dispose, CancellationToken token, float duration)
        {
            this.update = update;
            this.dispose = dispose;
            this.token = token;
            this.duration = duration;
            startTime = GameThread.GameTime;

            if (token.IsCancellationRequested)
            {
                TryCallDispose();
                return;
            }
            
            TryCallUpdate(0);

            if (duration <= 0)
            {
                TryCallUpdate(1);
                TryCallDispose();
                return;
            }
            
            GameThread.AfterFramesUpdated += OnFrameUpdate;
        }

        void OnFrameUpdate()
        {
            if (token.IsCancellationRequested)
            {
                TryCallDispose();
                GameThread.AfterFramesUpdated -= OnFrameUpdate;
                return;
            }

            float t = Mathf.Min((GameThread.GameTime - startTime) / duration, 1);

            TryCallUpdate(t);

            if (t < 1)
            {
                return;
            }
            
            TryCallDispose();
            GameThread.AfterFramesUpdated -= OnFrameUpdate;
        }

        void TryCallUpdate(float t)
        {
            try
            {
                update(t);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error during Animator update", e);
            }            
        }

        void TryCallDispose()
        {
            if (dispose == null)
            {
                return;
            }
            
            try
            {
                dispose();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error during Animator dispose", e);
            }            
        }

        public static void Spawn(CancellationToken token, float durationInSec, Action<float> update, Action? dispose = null)
        {
            ThrowHelper.ThrowIfNull(update, nameof(update));

            _ = new FAnimator(update, dispose, token, durationInSec); // won't be GC'd as long as it remains in EveryFrame
        }

        public static void Start(IAnimatable highlighter) =>
            Spawn(highlighter.Token, highlighter.Duration, highlighter.Update, highlighter.Dispose);
        
        public override string ToString() => $"[{nameof(FAnimator)} " +
                                             $"{(GameThread.GameTime - startTime).ToString(BuiltIns.Culture)}/" +
                                             $"{duration.ToString(BuiltIns.Culture)}]";
    }
    
    public interface IAnimatable
    {
        CancellationToken Token { get; }
        float Duration { get; }
        void Update(float t);
        void Dispose();
    }
}