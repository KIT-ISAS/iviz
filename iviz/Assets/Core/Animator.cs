#nullable enable

using System;
using System.Threading;
using UnityEngine;

namespace Iviz.Core
{
    public sealed class Animator
    {
        readonly Action<float> callback;
        readonly CancellationToken token;
        readonly float startTime;
        readonly float duration;

        Animator(Action<float> callback, CancellationToken token, float duration)
        {
            this.callback = callback;
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
                callback(t);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error during Animator", e);
            }

            if (t >= 1)
            {
                GameThread.EveryFrame -= FrameUpdate;
            }
        }

        public static void Spawn(CancellationToken token, float duration, Action<float> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            
            _ = new Animator(callback, token, duration); // won't be GC'd as long as it remains in EveryFrame
        }

        public override string ToString() => "[Animator " + (GameThread.GameTime - startTime) + "/" + duration + "]";
    }
}