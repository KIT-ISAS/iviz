using System;
using System.Collections.Concurrent;
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

        void Awake()
        {
            Instance = this;
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
        /// Run this action on the main thread.
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
    }
}