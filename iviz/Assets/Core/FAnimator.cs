#nullable enable

using System;
using System.Threading;
using UnityEngine;

namespace Iviz.Core
{
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

            try
            {
                update(0);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error during Animator update", e);
            }

            GameThread.EveryFrame += OnFrameUpdate;
            
        }

        void OnFrameUpdate()
        {
            if (token.IsCancellationRequested)
            {
                TryCallDispose();
                GameThread.EveryFrame -= OnFrameUpdate;
                return;
            }

            float t = Mathf.Min((GameThread.GameTime - startTime) / duration, 1);

            TryCallUpdate(t);

            if (t < 1)
            {
                return;
            }
            
            TryCallDispose();
            GameThread.EveryFrame -= OnFrameUpdate;
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
            try
            {
                dispose?.Invoke();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error during Animator dispose", e);
            }            
        }

        public static void Spawn(CancellationToken token, float duration, Action<float> update, Action? dispose = null)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            _ = new FAnimator(update, dispose, token, duration); // won't be GC'd as long as it remains in EveryFrame
        }

        public static void Start(IAnimatable highlighter) =>
            Spawn(highlighter.Token, highlighter.Duration, highlighter.Update, highlighter.Dispose);

        public override string ToString() => "[Animator " + (GameThread.GameTime - startTime) + "/" + duration + "]";
    }


    public interface IAnimatable
    {
        CancellationToken Token { get; }
        float Duration { get; }
        void Update(float t);
        void Dispose();
    }
}