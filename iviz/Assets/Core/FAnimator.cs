#nullable enable

using System;
using System.Threading;
using UnityEngine;

namespace Iviz.Core
{
    public sealed class FAnimator
    {
        readonly Action<float> update;
        readonly Action dispose;
        readonly CancellationToken token;
        readonly float startTime;
        readonly float duration;

        FAnimator(Action<float> update, Action dispose, CancellationToken token, float duration)
        {
            this.update = update;
            this.dispose = dispose;
            this.token = token;
            this.duration = duration;
            startTime = GameThread.GameTime;

            if (!token.IsCancellationRequested)
            {
                GameThread.EveryFrame += FrameUpdate;
            }
        }

        void FrameUpdate()
        {
            if (token.IsCancellationRequested)
            {
                GameThread.EveryFrame -= FrameUpdate;
                return;
            }

            float t = Mathf.Min((GameThread.GameTime - startTime) / duration, 1);
            try
            {
                update(t);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error during Animator update", e);
            }

            if (t < 1)
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

            GameThread.EveryFrame -= FrameUpdate;
        }

        public static void Spawn(CancellationToken token, float duration, Action<float> update, Action dispose)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            if (dispose == null)
            {
                throw new ArgumentNullException(nameof(dispose));
            }

            _ = new FAnimator(update, dispose, token, duration); // won't be GC'd as long as it remains in EveryFrame
        }

        public static void Start(IAnimatable highlighter)
        {
            Spawn(highlighter.Token, highlighter.Duration, highlighter.UpdateFrame, highlighter.Dispose);
        }

        public override string ToString() => "[Animator " + (GameThread.GameTime - startTime) + "/" + duration + "]";
    }


    public interface IAnimatable
    {
        float Duration { get; }
        CancellationToken Token { get; }
        void UpdateFrame(float t);
        void Dispose();
    }
}