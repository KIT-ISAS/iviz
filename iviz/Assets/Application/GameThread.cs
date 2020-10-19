using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Singleton that other threads can use to post actions to the main thread.
    /// </summary>
    public class GameThread : MonoBehaviour
    {
        static GameThread Instance;
        readonly ConcurrentQueue<Action> actionsOnlyOnce = new ConcurrentQueue<Action>();
        float lastRunTime;
        Thread gameThread;

        void Awake()
        {
            Instance = this;
            gameThread = Thread.CurrentThread;
        }

        void Update()
        {
            EveryFrame?.Invoke();

            float newRunTime = Time.time;
            if (newRunTime - lastRunTime > 1)
            {
                EverySecond?.Invoke();
                LateEverySecond?.Invoke();
                lastRunTime = newRunTime;
            }

            while (actionsOnlyOnce.TryDequeue(out Action action))
            {
                action();
            }
        }

        void LateUpdate()
        {
            LateEveryFrame?.Invoke();
        }

        void OnDestroy()
        {
            Instance = null;
        }

        /// <summary>
        /// Run every frame.
        /// </summary>
        public static event Action EveryFrame;

        /// <summary>
        /// Run every frame after the rest.
        /// </summary>
        public static event Action LateEveryFrame;

        /// <summary>
        /// Run once per second.
        /// </summary>
        public static event Action EverySecond;

        /// <summary>
        /// Run once per second after the others.
        /// </summary>
        public static event Action LateEverySecond;

        /// <summary>
        /// Puts this action in a queue to be run on the main thread.
        /// If it is already on the main thread, it will run on the next frame.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        /// <exception cref="ArgumentNullException">If action is null.</exception>
        public static void Post(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (Instance == null)
            {
                return;
            }

            Instance.actionsOnlyOnce.Enqueue(action);
        }

        /// <summary>
        /// Puts this action in a queue to be run on the main thread.
        /// If it is already on the main thread, runs it immediately.
        /// </summary>
        /// <param name="action">Action to be run.</param>
        /// <exception cref="ArgumentNullException">If action is null.</exception>        
        public static void PostImmediate(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (IsGameThread)
            {
                action();
            }
            else
            {
                if (Instance == null)
                {
                    return;
                }

                Instance.actionsOnlyOnce.Enqueue(action);
            }
        }

        static bool IsGameThread => Thread.CurrentThread == Instance.gameThread;
    }
}